using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class logoutadmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["name"] = null;
                Session["img"] = null;
                Session["admin"] = null;
                Response.Redirect("loginadmin.aspx");
            }
        }
    }
}