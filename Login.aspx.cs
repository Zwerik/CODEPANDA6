using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Logged_In"] as User != null)
        {
            Response.Redirect("~/Beheer.aspx");
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        User user;
        user = Remise.User_Authenticate(tbUsername.Text, tbPassword.Text);
        if (user == null)
        {
            lblCredInfo.Text = "Invalid login credentials!";
        }
        else
        {
            Session["Logged_In"] = user;
            Response.Redirect("~/Beheer.aspx");
        }
    }
}