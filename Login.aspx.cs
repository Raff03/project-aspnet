using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject_WA
{
	public partial class Login : System.Web.UI.Page
	{
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            string connStr = ConfigurationManager.ConnectionStrings["SilkTheoryConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Users WHERE Username=@Username AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // Save session values
                    Session["UserID"] = dr["UserID"];
                    Session["Username"] = dr["Username"];
                    Session["Role"] = dr["Role"];

                    // Redirect based on role
                    if (dr["Role"].ToString() == "Admin")
                    {
                        Response.Redirect("Admin.aspx");
                    }
                    else
                    {
                        Response.Redirect("MainPage.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid login. Try again.";
                }
            }
        }
    }
}