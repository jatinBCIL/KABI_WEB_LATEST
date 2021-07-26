using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.Services;
using System.Web.Security;
using System.Net.NetworkInformation;
using PropertyLayer;
using System.Data;

public partial class MasterPage : System.Web.UI.MasterPage
{
    #region "AntiXss"
    //private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    //private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    //private string _antiXsrfTokenValue;
    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    //First, check for the existence of the Anti-XSS cookie         
    //    var requestCookie = Request.Cookies[AntiXsrfTokenKey];
    //    Guid requestCookieGuidValue;
    //    //If the CSRF cookie is found, parse the token from the cookie.         
    //    //Then, set the global page variable and view state user         
    //    //key. The global variable will be used to validate that it matches in the view state form field in the Page.PreLoad         
    //    //method.         
    //    if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
    //    {
    //        //Set the global token variable so the cookie value can be             
    //        //validated against the value in the view state form field in             
    //        //the Page.PreLoad method.             
    //        _antiXsrfTokenValue = requestCookie.Value;
    //        //Set the view state user key, which will be validated by the             
    //        //framework during each request             
    //        Page.ViewStateUserKey = _antiXsrfTokenValue;
    //    }
    //    //If the CSRF cookie is not found, then this is a new session.        
    //    else
    //    {
    //        //Generate a new Anti-XSRF token             
    //        _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
    //        //Set the view state user key, which will be validated by the             
    //        //framework during each request             
    //        Page.ViewStateUserKey = _antiXsrfTokenValue;
    //        //Create the non-persistent CSRF cookie             
    //        var responseCookie = new HttpCookie(AntiXsrfTokenKey)
    //        {
    //            //Set the HttpOnly property to prevent the cookie from                 
    //            //being accessed by client side script                 
    //            HttpOnly = true,
    //            //Add the Anti-XSRF token to the cookie value                 
    //            Value = _antiXsrfTokenValue
    //        };
    //        //If we are using SSL, the cookie should be set to secure to             
    //        //prevent it from being sent over HTTP connections             
    //        if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
    //            responseCookie.Secure = true;
    //        //Add the CSRF cookie to the response             
    //        Response.Cookies.Set(responseCookie);
    //    }
    //    Page.PreLoad += master_Page_PreLoad;
    //}

    //protected void master_Page_PreLoad(object sender, EventArgs e)
    //{
    //    //During the initial page load, add the Anti-XSRF token and user             
    //    //name to the ViewState             
    //    if (!IsPostBack)
    //    {
    //        //Set Anti-XSRF token                 
    //        ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
    //        //If a user name is assigned, set the user name                 
    //        ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
    //    }
    //    //During all subsequent post backs to the page, the token value from             
    //    //the cookie should be validated against the token in the view state             
    //    //form field. Additionally user name should be compared to the            
    //    //authenticated users name             
    //    else
    //    {
    //        //Validate the Anti-XSRF token                 
    //        if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
    //        {
    //            //HttpContext.Current.Response.Redirect("~/Default.aspx");
    //            throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
    //        }
    //    }
    //}
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            try
            {
                if (clsBLCommon.IsSessionIdExists(clsStandards.current_Username(), HttpContext.Current.Session["Token_Number"].ToString(), clsStandards.GetMAC()) == false)
                {
                    HttpContext.Current.Session["UserID"] = null;
                    HttpContext.Current.Session["UserType"] = null;
                    HttpContext.Current.Session["Token_Number"] = null;
                    HttpContext.Current.Session["PlantID"] = null;
                    if (!string.IsNullOrEmpty(Session["Password"].ToString()))
                    {
                        HttpContext.Current.Response.Redirect("~/Modules/frmPassword.aspx", false);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/Modules/Login.aspx");
                    }

                }

                //Label1.Text = ConfigurationManager.AppSettings["Environment"].ToString();
                Label7.Text = "[ Logged in as " + HttpContext.Current.Session["MyName"] + "] [Plant : " + HttpContext.Current.Session["MyPlant"] + " ]";
                lblDate.Text = HttpContext.Current.Session["DATETIME"].ToString();
                lblExipry.Text = "Your Password will Expire in " + PL_Username.iExpiry.ToString() + " Days";

                if (clsBLCommon.isExpired(clsStandards.current_Username(), HttpContext.Current.Session["LoginFlag"].ToString()) == true)
                {
                    MenuBar1.Visible = false;
                }
                else
                {
                    MenuBar1.Visible = true;
                }

                //((HyperLink)this.MenuBar1.FindControl("Menu403")).NavigateUrl = ResolveUrl("~/Tools/frmPassword.aspx?id=" + clsStandards.current_Username());

                //Loading rights in string variable.
                DataSet ds = clsBLCommon.getMyRights(clsStandards.current_Username());

                if (ds != null)
                {
                    HttpContext.Current.Session["QC-RM"] = "0";
                    HttpContext.Current.Session["QC-PO"] = "0";
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (dr[0].ToString().Trim() != null)
                                {
                                    //Converting string value into array of string.
                                    string[] Filter = dr[0].ToString().Split(',');

                                    //Loading rights on master page menu control.
                                    for (int i = 0; i <= Filter.Length - 1; i++)
                                    {
                                        try
                                        {
                                            if (dr[0].ToString().Split(',').GetValue(i).ToString() == "8081")
                                            {
                                                HttpContext.Current.Session["QC-RM"] = "1";
                                            }
                                            if (dr[0].ToString().Split(',').GetValue(i).ToString() == "8082")
                                            {
                                                HttpContext.Current.Session["QC-PO"] = "1";
                                            }
                                            ((HtmlGenericControl)this.MenuBar1.FindControl("Menu" + dr[0].ToString().Split(',').GetValue(i).ToString() + "A")).Visible = true;
                                        }
                                        catch { }
                                    }
                                }

                            }
                        }
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[1].Rows)
                            {
                                if (dr[0].ToString().Trim() != null)
                                {
                                    //Converting string value into array of string.
                                    string[] Filter = dr[0].ToString().Split(',');

                                    //Loading rights on master page menu control.
                                    for (int i = 0; i <= Filter.Length - 1; i++)
                                    {
                                        try
                                        {
                                            if (dr[0].ToString().Split(',').GetValue(i).ToString() == "8081")
                                            {
                                                HttpContext.Current.Session["QC-RM"] = "1";
                                            }
                                            if (dr[0].ToString().Split(',').GetValue(i).ToString() == "8082")
                                            {
                                                HttpContext.Current.Session["QC-PO"] = "1";
                                            }
                                            ((HtmlGenericControl)this.MenuBar1.FindControl("Menu" + dr[0].ToString().Split(',').GetValue(i).ToString() + "A")).Visible = true;
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }

                    }
                }
             
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Redirect("~/Modules/Login.aspx");
            }
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Application["Users"] = null;

        clsBLCommon _BusinessLayer = new clsBLCommon();
        try
        {
            if (_BusinessLayer.CloseSession(clsStandards.current_Username(), clsStandards.GetMAC(), 0) == true)
            {

                clsStandards.WriteLog(new Page(), new Exception("User Logged Out Successfully"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
                //clsStandards.WriteLog('1', new Exception("User Logged Out Successfully"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);
                string strFlag = "0";

                if (HttpContext.Current.Session["LoginFlag"].ToString() == "1")
                {
                    strFlag = "1";
                }

                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Expires = -1500;
                HttpContext.Current.Response.CacheControl = "no-cache";

                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Cookies.Clear();

                if (strFlag == "0")
                {
                    HttpContext.Current.Response.Redirect("~/Modules/myClose.aspx");
                }
                else
                {
                    HttpContext.Current.Response.Redirect("~/Modules/myClose.aspx");
                }
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
            _BusinessLayer = null;
        }
    }
}
