using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject_WA
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Optional: check if user is admin
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void fvAddProduct_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            TextBox txtName = (TextBox)fvAddProduct.FindControl("txtProductName");
            TextBox txtPrice = (TextBox)fvAddProduct.FindControl("txtPrice");
            FileUpload fuImage = (FileUpload)fvAddProduct.FindControl("fuImage");
            DropDownList ddlCategory = (DropDownList)fvAddProduct.FindControl("ddlCategory");

            string imagePath = "";
            if (fuImage.HasFile)
            {
                string fileName = Path.GetFileName(fuImage.FileName);
                imagePath = "Images/" + fileName;
                fuImage.SaveAs(Server.MapPath("~/" + imagePath));
            }

            string connStr = ConfigurationManager.ConnectionStrings["SilkTheoryConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "INSERT INTO Products (ProductName, Price, ImageUrl, CategoryID) VALUES (@name, @price, @image, @cat)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text));
                cmd.Parameters.AddWithValue("@image", imagePath);
                cmd.Parameters.AddWithValue("@cat", ddlCategory.SelectedValue);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            gvProducts.DataBind(); // Refresh product list
            fvAddProduct.ChangeMode(FormViewMode.Insert); // Reset the form
        }
        protected void btnGoToUserEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminUserEdit.aspx");

        }
        protected void btnGoToOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("GetOrderHistory.aspx");
        }

        protected void fvAddProduct_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }
    }
}