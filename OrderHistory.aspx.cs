﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject_WA
{
	public partial class OrderHistory : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }

        }
	}
}