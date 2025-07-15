using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject_WA
{
	public partial class SilkTheory : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                lblUsername.Text = "Welcome, " + Session["Username"].ToString();
                lblUsername.Visible = true;
                lnkRegister.Visible = false; // Register
                lnkLogin.Visible = false; // Login
                lnkLogout.Visible = true;  // Logout

                // Show admin panel link only if user is an admin
                if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
                {
                    pnlAdminLinks.Visible = true;
                }
                else
                {
                    pnlAdminLinks.Visible = false;
                }
            }
            else
            {
                lblUsername.Visible = false;
                lnkRegister.Visible = true;
                lnkLogin.Visible = true;
                lnkLogout.Visible = false;
                pnlAdminLinks.Visible = false;
            }
        }
	}
}