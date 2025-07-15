using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace FinalProject_WA
{
    public partial class GetOrderHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAdminOrderHistory();
            }
        }

        private void LoadAdminOrderHistory()
        {
            string connStr = ConfigurationManager.ConnectionStrings["SilkTheoryConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("GetOrderHistoryByAdmin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvOrders.DataSource = dt;
                    gvOrders.DataBind();
                }
            }
        }

        protected void btnGoToAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }
    }
}
