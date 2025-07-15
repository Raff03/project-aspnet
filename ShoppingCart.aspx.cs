using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject_WA
{
	public partial class ShoppingCart : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadCart();
            }
        }

        private void LoadCart()
        {
            List<int> cart = Session["Cart"] as List<int>;
            Dictionary<int, int> quantities = Session["Quantities"] as Dictionary<int, int> ?? new Dictionary<int, int>();

            if (cart == null || cart.Count == 0)
            {
                lblMessage.Text = "Your cart is empty.";
                gvCart.Visible = false;
                btnCheckout.Visible = false;
                lblTotal.Text = "Total: RM 0.00";
                lblTotal.Visible = true;
                return;
            }

            string idList = string.Join(",", cart.Distinct());
            string connStr = ConfigurationManager.ConnectionStrings["SilkTheoryConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = $"SELECT ProductID, ProductName, Price, ImageUrl FROM Products WHERE ProductID IN ({idList})";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dt.Columns.Add("Quantity", typeof(int));

                foreach (DataRow row in dt.Rows)
                {
                    int productId = Convert.ToInt32(row["ProductID"]);
                    row["Quantity"] = quantities.ContainsKey(productId) ? quantities[productId] : 1;
                }

                gvCart.DataSource = dt;
                gvCart.DataBind();

                CalculateTotal(dt);
            }
        }

        private void CalculateTotal(DataTable dt)
        {
            decimal total = 0;
            foreach (DataRow row in dt.Rows)
            {
                decimal price = Convert.ToDecimal(row["Price"]);
                int quantity = Convert.ToInt32(row["Quantity"]);
                total += price * quantity;
            }

            lblTotal.Text = "Total: RM " + total.ToString("F2");
            lblTotal.Visible = true;
            btnCheckout.Visible = true;
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox txtQuantity = (TextBox)sender;
            GridViewRow row = (GridViewRow)txtQuantity.NamingContainer;
            int index = row.RowIndex;

            if (gvCart.DataKeys.Count > index)
            {
                int productId = Convert.ToInt32(gvCart.DataKeys[index].Value);

                Dictionary<int, int> quantities = Session["Quantities"] as Dictionary<int, int> ?? new Dictionary<int, int>();
                int qty = 1;
                int.TryParse(txtQuantity.Text, out qty);
                if (qty < 1) qty = 1;

                quantities[productId] = qty;
                Session["Quantities"] = quantities;

                LoadCart();
            }
        }

        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                if (gvCart.DataKeys.Count > index)
                {
                    int productId = Convert.ToInt32(gvCart.DataKeys[index].Value);
                    List<int> cart = Session["Cart"] as List<int>;
                    if (cart != null)
                    {
                        cart.RemoveAll(id => id == productId); // remove all occurrence
                        Session["Cart"] = cart;
                    }

                    Dictionary<int, int> quantities = Session["Quantities"] as Dictionary<int, int> ?? new Dictionary<int, int>();
                    if (quantities.ContainsKey(productId))
                    {
                        quantities.Remove(productId);
                        Session["Quantities"] = quantities;
                    }

                    LoadCart();
                }
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["Cart"] == null)
            {
                lblMessage.Text = "Please login and add items to cart.";
                return;
            }

            List<int> cart = (List<int>)Session["Cart"];
            Dictionary<int, int> quantities = Session["Quantities"] as Dictionary<int, int> ?? new Dictionary<int, int>();
            int userId = Convert.ToInt32(Session["UserID"]);

            decimal total = 0;

            string connStr = ConfigurationManager.ConnectionStrings["SilkTheoryConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string insertOrderSql = "INSERT INTO Orders (UserID, TotalAmount) VALUES (@UserID, 0); SELECT SCOPE_IDENTITY();";
                SqlCommand cmdOrder = new SqlCommand(insertOrderSql, conn);
                cmdOrder.Parameters.AddWithValue("@UserID", userId);
                int orderId = Convert.ToInt32(cmdOrder.ExecuteScalar());


                foreach (int productId in cart.Distinct())
                {
                    SqlCommand getPriceCmd = new SqlCommand("SELECT Price FROM Products WHERE ProductID = @pid", conn);
                    getPriceCmd.Parameters.AddWithValue("@pid", productId);
                    decimal price = Convert.ToDecimal(getPriceCmd.ExecuteScalar());

                    int qty = quantities.ContainsKey(productId) ? quantities[productId] : 1;

                    SqlCommand insertDetailCmd = new SqlCommand("INSERT INTO OrderDetails (OrderID, ProductID, Quantity, Price) VALUES (@oid, @pid, @qty, @price)", conn);
                    insertDetailCmd.Parameters.AddWithValue("@oid", orderId);
                    insertDetailCmd.Parameters.AddWithValue("@pid", productId);
                    insertDetailCmd.Parameters.AddWithValue("@qty", qty);
                    insertDetailCmd.Parameters.AddWithValue("@price", price);
                    insertDetailCmd.ExecuteNonQuery();

                    total += price * qty;
                }

                SqlCommand updateTotalCmd = new SqlCommand("UPDATE Orders SET TotalAmount = @total WHERE OrderID = @oid", conn);
                updateTotalCmd.Parameters.AddWithValue("@total", total);
                updateTotalCmd.Parameters.AddWithValue("@oid", orderId);
                updateTotalCmd.ExecuteNonQuery();

                conn.Close();
            }


            lblTotal.Text = "Total: RM " + total.ToString("F2");

            Session["Cart"] = null;
            Session["Quantities"] = null;
            lblMessage.Text = "Order placed successfully!";
            gvCart.Visible = false;
            btnCheckout.Visible = false;
            // lblTotal.Text = "Total: RM 0.00";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductListing.aspx");
        }
    }
}
