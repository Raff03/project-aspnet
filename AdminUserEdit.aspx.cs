using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject_WA
{
    public partial class AdminUserEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnBackToAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;
            gvUsers.DataBind();
            lblError.Visible = false;
        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            gvUsers.DataBind();
            lblError.Visible = false;
        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = gvUsers.Rows[e.RowIndex];
                int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

                string username = ((TextBox)row.Cells[1].Controls[0]).Text;
                string email = ((TextBox)row.Cells[2].Controls[0]).Text;
                string role = ((TextBox)row.Cells[3].Controls[0]).Text;

                SqlDataSource sds = (SqlDataSource)FindControl("SqlUsers");
                sds.UpdateParameters["UserId"].DefaultValue = userId.ToString();
                sds.UpdateParameters["Username"].DefaultValue = username;
                sds.UpdateParameters["Email"].DefaultValue = email;
                sds.UpdateParameters["Role"].DefaultValue = role;

                sds.Update();

                gvUsers.EditIndex = -1;
                gvUsers.DataBind();
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error updating user: " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

            string connStr = ConfigurationManager.ConnectionStrings["SilkTheoryConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Delete OrderDetails related to this user's orders
                string deleteOrderDetailsSql = @"
                    DELETE OD FROM OrderDetails OD
                    INNER JOIN Orders O ON OD.OrderID = O.OrderID
                    WHERE O.UserID = @UserId";
                using (SqlCommand cmd = new SqlCommand(deleteOrderDetailsSql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }

                // Delete Orders
                string deleteOrdersSql = "DELETE FROM Orders WHERE UserID = @UserId";
                using (SqlCommand cmd = new SqlCommand(deleteOrdersSql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }

                // Delete User
                string deleteUserSql = "DELETE FROM Users WHERE UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(deleteUserSql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }

            e.Cancel = true; // Prevent SqlDataSource from doing it again
            gvUsers.DataBind();
        }
    }
}
