using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;

public partial class Modules_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //txtUsername.Focus();
            btnLoginAgain.Visible = false;
            txtUsername.Focus();
        }
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        if (txtNewPassword.Text.Trim() == string.Empty)
        {
            clsStandards.WriteLog(this, new Exception("New Password is required."), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;
        }

        if (txtPassword.Text.Trim() == string.Empty)
        {
            clsStandards.WriteLog(this, new Exception("Old Password is required."), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;
        }

        if (txtConfirmPassword.Text.Trim() == string.Empty)
        {
            clsStandards.WriteLog(this, new Exception("Confirm new password is required."), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;
        }

        clsBLCommon _Common = new clsBLCommon();
        try
        {
            clsStandards.WriteLog(this, new Exception(_Common.ChangePassword(txtUsername.Text.Trim(), txtPassword.Text.Trim(), txtNewPassword.Text.Trim())), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
            txtPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtUsername.Text = string.Empty;
            btnLoginAgain.Visible = true;
        }
        catch (Exception ex)
        {
            string strError = ex.ToString();
            strError = strError.Replace("System.Exception: System.Data.SqlClient.SqlException (0x80131904): ", "");
            clsStandards.WriteLog(this, new Exception(strError), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            _Common = null;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //HttpContext.Current.Response.Redirect(ResolveUrl(System.Web.Configuration.WebConfigurationManager.AppSettings["PostBackURL"].ToString()));
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Application["Users"] = null;

        clsBLCommon _Common = new clsBLCommon();
        try
        {
            if (_Common.CloseSession(clsStandards.current_Username(), clsStandards.GetMAC(), 0) == true)
            {
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Expires = -1500;
                HttpContext.Current.Response.CacheControl = "no-cache";

                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Cookies.Clear();
                HttpContext.Current.Response.Redirect("~/Login.aspx");
            }
            else
            {
                clsStandards.WriteLog(Page, new Exception("Could not close current session."), "Main Page", "Logout", 0, false);
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(Page, ex, "Main Page", "Logout", 0, false);
        }
        finally
        {
            _Common = null;
        }
    }
    protected void btnLoginAgain_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("~/Modules/Login.aspx");
    }
}