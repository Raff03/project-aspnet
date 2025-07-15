using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject_WA
{
    public partial class ProductListing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
            }
        }

        private void LoadCategories()
        {
            string connStr = ConfigurationManager.ConnectionStrings["SilkTheoryConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT CategoryID, CategoryName FROM Categories";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                ddlCategories.DataSource = reader;
                ddlCategories.DataTextField = "CategoryName";
                ddlCategories.DataValueField = "CategoryID";
                ddlCategories.DataBind();
            }

            ddlCategories.Items.Insert(0, new ListItem("-- All Categories --", "0"));
        }

        protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataSource1.DataBind();
            rptProducts.DataBind();
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int productId = int.Parse(btn.CommandArgument);

            List<int> cart = Session["Cart"] as List<int> ?? new List<int>();
            cart.Add(productId);
            Session["Cart"] = cart;

            Response.Redirect("ShoppingCart.aspx");
        }
    }
}
