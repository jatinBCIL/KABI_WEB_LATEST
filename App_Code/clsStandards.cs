using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Management;
using System.Management.Instrumentation;
using System.Data.OleDb;
using Microsoft.VisualBasic;
using System.Collections;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Net;
using BusinessLayer;



public class clsStandards
{
    public static string strMyId;

    public static string current_Username()
    {
        try
        {
            if (HttpContext.Current.Session["UserId"] != null)
            {

                return clsStandards.fn_Decrypt_String(Convert.ToString(HttpContext.Current.Session["UserId"]), "B0C0I0L0");
            }
            else
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute(System.Web.Configuration.WebConfigurationManager.AppSettings["Login"].ToString()));
                throw new Exception("Login session expired.");
            }
        }
        catch
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute(System.Web.Configuration.WebConfigurationManager.AppSettings["Login"].ToString()));
            throw new Exception("Login session expired.");
        }
    }

    public static string current_UserType()
    {
        try
        {
            if (HttpContext.Current.Session["UserId"] != null)
            {
                return clsBLRejection.GetUserType(clsStandards.WhereStatement_NoST("[USER_ID]", "=", HttpContext.Current.Session["UserID"].ToString()));

            }
            else
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute(System.Web.Configuration.WebConfigurationManager.AppSettings["Login"].ToString()));
                throw new Exception("Login session expired.");
            }
        }
        catch
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute(System.Web.Configuration.WebConfigurationManager.AppSettings["Login"].ToString()));
            throw new Exception("Login session expired.");
        }
    }

    public static string current_Plant()
    {
        try
        {
            if (HttpContext.Current.Session["MyPlant"] != null)
            {

                return (Convert.ToString(HttpContext.Current.Session["MyPlant"]));
            }
            else
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute(System.Web.Configuration.WebConfigurationManager.AppSettings["Login"].ToString()));
                throw new Exception("Login session expired.");
            }
        }
        catch
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute(System.Web.Configuration.WebConfigurationManager.AppSettings["Login"].ToString()));
            throw new Exception("Login session expired.");
        }
    }

    public static string current_Dept()
    {
        try
        {
            if (HttpContext.Current.Session["MyDept"] != null)
            {

                return (Convert.ToString(HttpContext.Current.Session["MyDept"]));
            }
            else
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute(System.Web.Configuration.WebConfigurationManager.AppSettings["Login"].ToString()));
                throw new Exception("Login session expired.");
            }
        }
        catch
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute(System.Web.Configuration.WebConfigurationManager.AppSettings["Login"].ToString()));
            throw new Exception("Login session expired.");
        }
    }

    public static string current_User()
    {
        try
        {
            if (HttpContext.Current.Session["MyName"] != null)
            {

                return (Convert.ToString(HttpContext.Current.Session["MyName"]));
            }
            else
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute(System.Web.Configuration.WebConfigurationManager.AppSettings["Login"].ToString()));
                throw new Exception("Login session expired.");
            }
        }
        catch
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute(System.Web.Configuration.WebConfigurationManager.AppSettings["Login"].ToString()));
            throw new Exception("Login session expired.");
        }
    }

    public static string GenerateHashKey()
    {
        StringBuilder mystr = new StringBuilder();
        mystr.Append(HttpContext.Current.Request.Browser.Browser);
        mystr.Append(HttpContext.Current.Request.Browser.Platform);
        mystr.Append(HttpContext.Current.Request.Browser.MajorVersion);
        mystr.Append(HttpContext.Current.Request.Browser.MinorVersion);
        mystr.Append(HttpContext.Current.Request.LogonUserIdentity.User.Value);

        SHA1 sha = new SHA1CryptoServiceProvider();
        byte[] hashdata = sha.ComputeHash(Encoding.UTF8.GetBytes(mystr.ToString()));
        return Convert.ToBase64String(hashdata);
    }

    public static string Encryptdata(string retUsrId)
    {
        string strmsg = string.Empty;
        byte[] encode = new byte[retUsrId.Length];
        encode =
        Encoding.UTF8.GetBytes(retUsrId);
        strmsg =
        Convert.ToBase64String(encode);
        return strmsg;
    }

    public static string Auto_Generate_Password()
    {
        string strPwdchar = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string strPwd = "";
        Random rnd = new Random();
        for (int i = 0; i <= 7; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            strPwd += strPwdchar.Substring(iRandom, 1);
        }
        return strPwd;
    }

    public static string Generate_Token()
    {
        string strPwdchar = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string strPwd = "";
        Random rnd = new Random();
        for (int i = 0; i <= 7; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            strPwd += strPwdchar.Substring(iRandom, 1);
        }
        return strPwd;
    }

    public static Boolean IsConnected()
    {
        SqlDataLayer objSql = new SqlDataLayer();
        try
        {
            return objSql.Connect(objSql.strLocal);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            objSql = null;
        }
    }

    public static bool ValidateCheckByLogin(string strCheckByuser)
    {
        try
        {
            if (current_Username().ToString() == strCheckByuser)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }


    public static bool IsNumeric(string anyString)
    {
        bool isNumeric = false;
        Double outValue;
        isNumeric = Double.TryParse(anyString, out outValue);

        if (isNumeric)
        {
            isNumeric = !(Double.IsNaN(outValue) || Double.IsInfinity(outValue) || Double.IsPositiveInfinity(outValue) || Double.IsNegativeInfinity(outValue));
        }
        return isNumeric;
    }

    private static byte[] key = { };
    private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
    public static string fn_Decrypt_String(string stringToDecrypt, string sEncryptionKey)
    {
        byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms,
              des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public static string fn_Encrypt_String(string stringToEncrypt, string EncryptionKey)
    {
        string strResult = string.Empty;
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms,
            des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            strResult = Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception e)
        {
            return e.Message;
        }
        return strResult;
    }

    public static string GetMAC()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        } return sMacAddress;
    }

    public static string ConvertDateTime(string strdate, string Inputformat, string OutputFormat)
    {
        DateTime dtPODate = DateTime.ParseExact(strdate, Inputformat, CultureInfo.InvariantCulture);
        strdate = dtPODate.ToString(OutputFormat, CultureInfo.InvariantCulture);
        return strdate;
    }
    

    public static int Allow_Login(string strUserid, string strDate)
    {
        int result;
        result = 0;

        ///Validating link expiry.
        TimeSpan ts;
        try
        {
            ts = DateTime.Now.Subtract(Convert.ToDateTime(strDate));
        }
        catch
        { //System.Web.HttpContext.Current.Response.Redirect("~/Errors/Error.aspx?id=Link validation failed."); 
            //link validation failed.
            return 0;
        }

        if (ts.Minutes > int.Parse(ConfigurationManager.AppSettings["Interval"]))
        {
            ///Redirecting to expiry error page.
            //System.Web.HttpContext.Current.Response.Redirect("~/Errors/Error.aspx?id=Link has been expired.");
            //Link has been expired.
            return 1;
        }

        if (strUserid.Trim() != string.Empty)
        {
            if (strDate.Trim() != string.Empty)
            {
                //clsCommon _Common = new clsCommon();
                //try
                //{
                //    ///Checking user existance in database.
                //    if (clsCommon.IsUserExists(strUserid.Trim()) == true)
                //    {
                //        ///Performing login.
                //        HttpContext.Current.Session["UserID"] = strUserid.Trim();
                //        result = 2;
                //    }
                //    else
                //    {
                //        //User id does not exists.
                //        return 3;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    if (ex.Message.Contains("Unable to evaluate expression because the code is optimized or a native frame is on top of the call stack.") == false)
                //    {
                //        return 0; 
                //    }
                //}
                //finally
                //{
                //    _Common = null;
                //}
            }
        }
        return result;
    }

    public static string GetIndianDate()
    {
        return DateTime.Now.ToString(@"dd/MM/yyyy hh:mm:ss tt", new CultureInfo("en-US"));
    }

    public static string Convert_Indian_Date(string strDate)
    {
        return strDate.Split('/').GetValue(1).ToString() + "/" + strDate.Split('/').GetValue(0).ToString() + "/" + strDate.Split('/').GetValue(2).ToString();
    }

    public static string getDateStringFormatted()
    {
        return DateTime.Now.ToShortDateString().Replace("/", "_").ToString() + "_" + DateTime.Now.ToLongTimeString().Replace(":", "_").Replace(" ", "_");
    }

    public static Boolean IsLoginExist(Page Me)
    {
        if (Convert.ToString(HttpContext.Current.Session["Username"]) == null || Convert.ToString(HttpContext.Current.Session["Username"]) == string.Empty)
        {
            HttpContext.Current.Response.Redirect(Me.ResolveUrl(System.Web.Configuration.WebConfigurationManager.AppSettings["Expired"].ToString()));
            return false;
        }
        else
        {
            return true;
        }
    }

    public static void CreatePRN(Exception exc, string strPRNData, string strFilename)
    {
        //Getting local log field identification from web.config.
        string logFile = HttpContext.Current.Server.MapPath(System.Web.Configuration.WebConfigurationManager.AppSettings["PRN"].ToString());

        // Include enterprise logic for logging exceptions
        // Open the log file for append and write the log
        StreamWriter sw = new StreamWriter(logFile + strFilename, true);
        sw.WriteLine(strPRNData);
        sw.Close();
    }

    public static void WriteLog(Page Me, Exception exc, string strModule, string strFunction, int Result, Boolean boolShow)
    {
        if (exc.ToString().Contains("Thread was being aborted.") == false)
        {
            //Class object declaration.
            PL_LogWriter objFields = new PL_LogWriter();
            string strDescription = string.Empty;
            try
            {
                //Assigning property values.
                objFields.strModuleName = strModule.Trim();
                objFields.strFunctionName = strFunction.Trim();
                switch (Result)
                {
                    case 0:
                        objFields.strErrorType = "Error";
                        break;
                    case 1:
                        objFields.strErrorType = "Warning";
                        break;
                    case 2:
                        objFields.strErrorType = "Success";
                        break;
                    default:
                        objFields.strErrorType = "Error";
                        break;
                }

                if (exc.ToString().Replace("  at", "~").Contains("~") == true)
                {
                    strDescription = exc.ToString().Replace("  at", "~").Split('~').GetValue(0).ToString();
                    if (strDescription.Contains("Query:") == true)
                    {
                        strDescription = strDescription + " Query: " + exc.ToString().Replace("Query:", "@").Split('@').GetValue(1).ToString();
                    }
                }
                else
                {
                    strDescription = exc.ToString().Replace("'", "").ToString();
                }

                if (strDescription.Length > 1000)
                {
                    objFields.strDescription = strDescription.Substring(0, 1000).ToString();
                }
                else
                {
                    objFields.strDescription = strDescription.ToString();
                }
                objFields.strUsername = clsStandards.current_Username();
                objFields.strProgram = "Website";
                objFields.strPlants = HttpContext.Current.Session["MyPlant"].ToString();
                //Writing exception in database.
                if (BL_LogWriter.BL_Write(objFields) == 0)
                {
                    //Writing exception on local space if refused to store from database.
                    Offline_Writer(exc, strModule, strFunction);
                }
                Offline_Writer(exc, strModule, strFunction);
            }
            catch
            {
                //Writing exception on local space if refused to store from database.
                Offline_Writer(exc, strModule, strFunction);
            }
            finally
            {
                //Disposing created object references.
                objFields = null;
            }

            if (boolShow == true)
            {
                string Message = strDescription.ToString();
                Message = Message.Replace("System.Exception:", "");
                Message = Message.Replace("'", "");
                Message = Message.Replace("\n", "");
                Message = Message.Replace("\r", "");
                Message = Message.Replace("\n\r", "");
                Message = Message.Replace("\r\n", "");
                string[] lines = Regex.Split(Message, "\r\n");
                Message = lines[0];

                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
            }
        }
    }

    public static void Offline_Writer(Exception exc, string strModule, string strFunction)
    {

        //Getting local log field identification from web.config.
        string logFile = HttpContext.Current.Server.MapPath(System.Web.Configuration.WebConfigurationManager.AppSettings["LogFile"].ToString());

        // Include enterprise logic for logging exceptions
        // Open the log file for append and write the log
        string strFilename = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt";
        StreamWriter sw = new StreamWriter(logFile + strFilename, true);
        sw.WriteLine("========================= {0} =========================", DateTime.Now);
        sw.WriteLine("Exception: " + exc.Message.Replace("System.Exception:", "").Trim());
        sw.WriteLine("Source: " + strModule + "| Function Name: " + strFunction);
        sw.Close();
    }

    /// <summary>
    /// Purpose : Filling datatable contents into combobox.
    /// </summary>
    /// <param name="objCombo">Combobox field reference.</param>
    /// <param name="dt">Datatable reference.</param>
    /// <param name="strColumn">Column name reference to fill.</param>
    public static void fillCombobox(DropDownList objCombo, DataTable dt, string strColumn)
    {
        //Clearing combobox.
        objCombo.Items.Clear();
        objCombo.Text = string.Empty;
        objCombo.Items.Add("Select");
        DataView dv = new DataView(dt);
        if (dt.Rows.Count != 0)
        {
            dv.Sort = dt.Columns[0].ColumnName + " ASC";
        }
        try
        {
            for (int i = 0; i <= dv.Table.Rows.Count - 1; i++)
            {
                objCombo.Items.Add(dv.Table.Rows[i][strColumn].ToString());
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            dv = null;
        }
    }

    public static void fillCombobox_Ajex(AjaxControlToolkit.ComboBox objCombo, DataTable dt, string strColumn)
    {
        //Clearing combobox.
        objCombo.Items.Clear();
        objCombo.Text = string.Empty;
        objCombo.Items.Add("Select");
        DataView dv = new DataView(dt);
        if (dt.Rows.Count != 0)
        {
            dv.Sort = dt.Columns[0].ColumnName + " ASC";
        }
        try
        {
            for (int i = 0; i <= dv.Table.Rows.Count - 1; i++)
            {
                objCombo.Items.Add(dv.Table.Rows[i][strColumn].ToString());
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            dv = null;
        }
    }

    /// <summary>
    /// Purpose : Filling datatable contents into combobox.
    /// </summary>
    /// <param name="objCombo">Combobox field reference.</param>
    /// <param name="dt">Datatable reference.</param>
    /// <param name="strColumn">Column name reference to fill.</param>
    public static void fillComboboxForReports(DropDownList objCombo, DataTable dt, string strColumn, string FirstRow)
    {
        //Clearing combobox.
        objCombo.Items.Clear();
        objCombo.Text = string.Empty;
        objCombo.Items.Add(FirstRow);
        try
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                objCombo.Items.Add(dt.Rows[i][strColumn].ToString());
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public static void setAvailable(Label lblResult, int intStatus)
    {
        if (intStatus == 0)
        {
            lblResult.Text = "Available";
            lblResult.BackColor = System.Drawing.Color.Green;
            lblResult.ForeColor = System.Drawing.Color.White;
            lblResult.ToolTip = "Record is available.";
        }
        else
        {
            lblResult.Text = "Not Available";
            lblResult.BackColor = System.Drawing.Color.Red;
            lblResult.ForeColor = System.Drawing.Color.White;
            lblResult.ToolTip = "The record with this information is already in the database.";
        }
    }
    /// <summary>
    /// Purpose : Function is created to get the check box status.
    /// </summary>
    /// <param name="chk">Checkbox field reference.</param>
    /// <returns>Returning result in integer format.</returns>
    public static int ActivationStatus(CheckBox chk)
    {
        if (chk.Checked == true)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    /// <summary>
    /// Purpose : Function is created to set activation status.
    /// </summary>
    /// <param name="chk">Checkbox field reference.</param>
    /// <param name="strValue">Check Status value.</param>
    public static void setActivationStatus(CheckBox chk, string strValue)
    {
        if (strValue == "Activated")
        {
            chk.Checked = true;
        }
        else
        {
            chk.Checked = false;
        }
    }

    //Encoding URL string.
    public static string UrlEncode(string text)
    {
        // Sytem.Uri provides reliable parsing
        return System.Uri.EscapeDataString(text);
    }
    //Decoding URL String
    public static string UrlDecode(string text)
    {
        // pre-process for + sign space formatting since System.Uri doesn't handle it
        // plus literals are encoded as %2b normally so this should be safe
        text = text.Replace("+", " ");
        return System.Uri.UnescapeDataString(text);
    }

    /// <summary>
    /// Purpose : function created to get the current page name.
    /// </summary>
    /// <returns>Returning result in string format.</returns>
    public static string GetCurrentPageName()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
        return sRet;
    }

    public static string getCheckedTree(TreeView objtr)
    {
        string strResult = string.Empty;
        foreach (TreeNode tn in objtr.Nodes)
        {
            if (tn.Checked == true)
            {
                strResult = strResult + tn.ToolTip + ",";
            }
            foreach (TreeNode tn1 in tn.ChildNodes)
            {
                if (tn1.Checked == true)
                {
                    strResult = strResult + tn1.ToolTip + ",";
                }
            }
        }
        if (strResult.Length != 0)
        {
            return Microsoft.VisualBasic.Strings.Left(strResult, strResult.Length - 1);
        }
        else
        {
            return string.Empty;
        }
    }

    public static void setRights(TreeView objtr, string strSelectedRights)
    {
        if (strSelectedRights != string.Empty && strSelectedRights != null)
        {
            string[] strTemp = strSelectedRights.Trim().Split(',');
            for (int i = 0; i <= strTemp.Count() - 1; i++)
            {
                foreach (TreeNode tn in objtr.Nodes)
                {
                    if (tn.ToolTip == strTemp.GetValue(i).ToString())
                    {
                        tn.Checked = true;
                    }
                    foreach (TreeNode tn1 in tn.ChildNodes)
                    {
                        if (tn1.ToolTip == strTemp.GetValue(i).ToString())
                        {
                            tn1.Checked = true;
                        }
                    }
                }
            }
        }
    }

    public static void ClearTree(TreeView tr_Permissions)
    {
        foreach (TreeNode tn in tr_Permissions.Nodes)
        {
            tn.Checked = false;
            foreach (TreeNode tn1 in tn.ChildNodes)
            {
                tn1.Checked = false;
            }
        }
        tr_Permissions.Enabled = true;
    }

    public static void ClearAll(Control objPage)
    {
        foreach (Control container in objPage.Controls)
        {
            foreach (Control c in container.Controls)
            {
                foreach (Control ctrl in c.Controls)
                {
                    if (ctrl is TextBox)
                    {
                        if (((TextBox)ctrl).ValidationGroup != "Search")
                        {
                            ((TextBox)ctrl).Text = string.Empty;
                        }
                    }
                    if (ctrl is Label)
                    {
                        if (((Label)ctrl).ToolTip == "Save" || ((Label)ctrl).ID == "lblResult")
                        {
                            ((Label)ctrl).Text = string.Empty;
                        }
                    }
                    if (ctrl is CheckBox)
                    {
                        ((CheckBox)ctrl).Checked = false;
                    }
                    if (ctrl is Panel)
                    {
                        foreach (Control ctrl1 in ctrl.Controls)
                        {
                            if (ctrl1 is Button)
                            {
                                //if (((Button)ctrl1).ValidationGroup == "Save")
                                //{
                                //    ((Button)ctrl1).Visible = false;
                                //}
                                //else
                                //{
                                //    ((Button)ctrl1).Visible = true;
                                //}
                            }
                            if (ctrl1 is CheckBox)
                            {
                                ((CheckBox)ctrl1).Checked = false;
                            }
                            if (ctrl is TextBox)
                            {
                                if (((TextBox)ctrl).ValidationGroup != "Search")
                                {
                                    ((TextBox)ctrl).Text = string.Empty;
                                }
                            }
                            if (ctrl is Label)
                            {
                                if (((Label)ctrl).ToolTip == "Save" || ((Label)ctrl).ID == "lblResult")
                                {
                                    ((Label)ctrl).Text = string.Empty;
                                }
                            }
                            if (ctrl is CheckBox)
                            {
                                ((CheckBox)ctrl).Checked = false;
                            }
                            if (ctrl is Button)
                            {
                                //if (((Button)ctrl).ValidationGroup == "Save")
                                //{
                                //    //((Button)ctrl).Visible = false;
                                //}
                                //else
                                //{
                                //    ((Button)ctrl).Visible = true;
                                //}
                            }

                            if (ctrl is GridView)
                            {
                                if (((GridView)ctrl).ID == "Upload_Grid")
                                {
                                    DataTable dt = new DataTable();
                                    ((GridView)ctrl).DataSource = dt;
                                    ((GridView)ctrl).DataBind();
                                    ((GridView)ctrl).SelectedIndex = -1;
                                }
                                ((GridView)ctrl).Enabled = true;
                            }

                            if (ctrl is DropDownList)
                            {
                                if (((DropDownList)ctrl).ValidationGroup == "Save")
                                {
                                    if (((DropDownList)ctrl).Items.Count != 0)
                                    {
                                        ((DropDownList)ctrl).Text = ((DropDownList)ctrl).Items[0].Text;
                                    }
                                }
                            }

                            if (ctrl is AjaxControlToolkit.ComboBox)
                            {
                                if (((AjaxControlToolkit.ComboBox)ctrl).ValidationGroup == "Save")
                                {
                                    ((AjaxControlToolkit.ComboBox)ctrl).Items.Clear();
                                    ((AjaxControlToolkit.ComboBox)ctrl).Text = string.Empty; ;
                                }
                            }

                            if (ctrl is TreeView)
                            {
                                foreach (TreeNode tn in ((TreeView)ctrl).Nodes)
                                {
                                    tn.Checked = false;
                                    foreach (TreeNode tn1 in tn.ChildNodes)
                                    {
                                        tn1.Checked = false;
                                    }
                                }
                            }
                        }
                    }

                    if (ctrl is MultiView)
                    {
                        foreach (View ctrl2 in ctrl.Controls)
                        {
                            foreach (Control ctrl1 in ctrl2.Controls)
                            {
                                if (ctrl1 is Button)
                                {
                                    //if (((Button)ctrl1).ValidationGroup == "Save")
                                    //{
                                    //    ((Button)ctrl1).Visible = false;
                                    //}
                                    //else
                                    //{
                                    //    ((Button)ctrl1).Visible = true;
                                    //}
                                }
                                if (ctrl1 is CheckBox)
                                {
                                    ((CheckBox)ctrl1).Checked = false;
                                }
                                if (ctrl1 is TextBox)
                                {
                                    if (((TextBox)ctrl1).ValidationGroup != "Search")
                                    {
                                        ((TextBox)ctrl1).Text = string.Empty;
                                    }
                                }
                                if (ctrl1 is Label)
                                {
                                    if (((Label)ctrl1).ToolTip == "Save" || ((Label)ctrl1).ID == "lblResult")
                                    {
                                        ((Label)ctrl1).Text = string.Empty;
                                    }
                                }
                                if (ctrl1 is CheckBox)
                                {
                                    ((CheckBox)ctrl1).Checked = false;
                                }
                                if (ctrl1 is Button)
                                {
                                    //if (((Button)ctrl).ValidationGroup == "Save")
                                    //{
                                    //    //((Button)ctrl).Visible = false;
                                    //}
                                    //else
                                    //{
                                    //    ((Button)ctrl).Visible = true;
                                    //}
                                }

                                if (ctrl1 is GridView)
                                {
                                    if (((GridView)ctrl1).ID == "Upload_Grid")
                                    {
                                        DataTable dt = new DataTable();
                                        ((GridView)ctrl1).DataSource = dt;
                                        ((GridView)ctrl1).DataBind();
                                        ((GridView)ctrl1).SelectedIndex = -1;
                                    }
                                    ((GridView)ctrl1).Enabled = true;
                                }

                                if (ctrl1 is DropDownList)
                                {
                                    if (((DropDownList)ctrl1).ValidationGroup == "Save")
                                    {
                                        if (((DropDownList)ctrl1).Items.Count != 0)
                                        {
                                            ((DropDownList)ctrl1).Text = ((DropDownList)ctrl1).Items[0].Text;
                                        }
                                    }
                                }

                                if (ctrl1 is AjaxControlToolkit.ComboBox)
                                {
                                    if (((AjaxControlToolkit.ComboBox)ctrl1).ValidationGroup == "Save")
                                    {
                                        ((AjaxControlToolkit.ComboBox)ctrl1).Items.Clear();
                                        ((AjaxControlToolkit.ComboBox)ctrl1).Text = string.Empty; ;
                                    }
                                }

                                if (ctrl1 is TreeView)
                                {
                                    foreach (TreeNode tn in ((TreeView)ctrl1).Nodes)
                                    {
                                        tn.Checked = false;
                                        foreach (TreeNode tn1 in tn.ChildNodes)
                                        {
                                            tn1.Checked = false;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (ctrl is Button)
                    {
                        //if (((Button)ctrl).ValidationGroup == "Save")
                        //{
                        //    //((Button)ctrl).Visible = false;
                        //}
                        //else
                        //{
                        //    ((Button)ctrl).Visible = true;
                        //}
                    }

                    if (ctrl is GridView)
                    {
                        if (((GridView)ctrl).ID == "Upload_Grid")
                        {
                            DataTable dt = new DataTable();
                            ((GridView)ctrl).DataSource = dt;
                            ((GridView)ctrl).DataBind();
                            ((GridView)ctrl).SelectedIndex = -1;
                        }
                        ((GridView)ctrl).Enabled = true;
                    }

                    if (ctrl is DropDownList)
                    {
                        if (((DropDownList)ctrl).ValidationGroup == "Save")
                        {
                            if (((DropDownList)ctrl).Items.Count != 0)
                            {
                                ((DropDownList)ctrl).Text = ((DropDownList)ctrl).Items[0].Text;
                            }
                        }
                    }

                    if (ctrl is AjaxControlToolkit.ComboBox)
                    {
                        if (((AjaxControlToolkit.ComboBox)ctrl).ValidationGroup == "Save")
                        {
                            ((AjaxControlToolkit.ComboBox)ctrl).Items.Clear();
                            ((AjaxControlToolkit.ComboBox)ctrl).Text = string.Empty; ;
                        }
                    }

                    if (ctrl is TreeView)
                    {
                        foreach (TreeNode tn in ((TreeView)ctrl).Nodes)
                        {
                            tn.Checked = false;
                            foreach (TreeNode tn1 in tn.ChildNodes)
                            {
                                tn1.Checked = false;
                            }
                        }
                    }
                }
            }
        }
    }

    public static string getWeight(string WeighID, string Port, string strWeighingType)
    {
        clsWeighing objServer = new clsWeighing();
        //DLServerFunctions objServer = new DLServerFunctions();
        string strResult = string.Empty;

        try
        {
            for (int i = 0; i < 3; i++)
            {


                if ((objServer.InitializeTCPIPClient(WeighID, Port) == 1))
                {
                    //objServer.Add_Log_Entry(ListView1, dr("LINE_CODE").ToString(), "Weighing scale disconnected", 1);
                    // MessageBox.Show("Weighing scale disconnected");
                    //clsStandards.WriteLog(this, new Exception("Not Connected"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    return "Not Connected";

                }
                else
                {
                    if (strWeighingType == "0")
                    {
                        //objServer.SendDataToServer("1B500D0A");
                        System.Threading.Thread.Sleep(5000);
                        strResult = objServer.ReceiveDataFromServer();
                        //lblActResult.Text = strResult;
                        strResult = strResult.Substring(4);
                        strResult = strResult.Substring(0, strResult.Length - 4);
                    }
                    else
                    {
                        //System.Threading.Thread.Sleep(10000);
                        //objServer.SendDataToServer("1B500D0A");
                        strResult = objServer.ReceiveDataFromServer();
                        //lblActResult.Text = strResult;
                        strResult = Regex.Replace(strResult.ToString(), "[^0-9. ]", "").Trim(); ;

                        //strResult = objServer.ReceiveDataFromServer();
                        //lblActResult.Text = strResult;
                        //strResult = Regex.Replace(strResult.ToString(), "[^0-9. ]", "").Trim(); ;



                    }
                    //objServer.Calculate_Weight(strResult).ToString();

                }

            }
            return strResult.Trim();
        }
        catch (Exception ex)
        {
            //clsStandards.WriteLog(this, new Exception("Not Connected"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            throw new Exception(ex.ToString());
            //MessageBox.Show(ex.ToString());
        }
        finally
        {
        }

    }

    public static void FillMultiCheckList(CheckBoxList CheckBoxList1, DataTable dt, string strColumn)
    {
        CheckBoxList1.DataTextField = "Name";
        CheckBoxList1.DataValueField = "ID";
        DataTable dt1 = new DataTable();
        dt1.Columns.Add(new DataColumn("Name", typeof(System.String)));
        dt1.Columns.Add(new DataColumn("ID", typeof(System.String)));
        DataView dv = new DataView(dt);
        if (dt.Rows.Count != 0)
        {
            dv.Sort = dt.Columns[0].ColumnName + " ASC";
        }
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            if (dv.Table.Rows[i][strColumn].ToString().Trim() != string.Empty)
            {
                dt1.Rows.Add(new string[] { dv.Table.Rows[i][strColumn].ToString(), dv.Table.Rows[i][strColumn].ToString() });
            }
        }

        CheckBoxList1.DataSource = dt1;
        CheckBoxList1.DataBind();
    }

    public static void fillCombo(DropDownList objCombo, string strData)
    {
        objCombo.Items.Clear();
        objCombo.Text = string.Empty;
        objCombo.Items.Add("Select");

        if (strData != "")
        {
            if (strData.Contains(",") == true)
            {
                string[] record = strData.ToString().Split(',');

                for (int i = 0; i <= record.Count() - 1; i++)
                {
                    objCombo.Items.Add(record[i].ToString());
                }
            }
            else
            {
                objCombo.Items.Add(strData.ToString());
            }
        }
    }

    public static void SetMultiCheckList(CheckBoxList CheckBoxList1, string strData)
    {
        ClearCheckbox(CheckBoxList1);

        if (strData.Contains(",") == true)
        {
            string[] record = strData.ToString().Split(',');

            for (int i = 0; i <= record.Count() - 1; i++)
            {
                for (int j = 0; j <= CheckBoxList1.Items.Count - 1; j++)
                {
                    if (CheckBoxList1.Items[j].ToString().ToUpper() == record[i].ToString().ToUpper())
                    {
                        CheckBoxList1.Items[j].Selected = true;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                if (CheckBoxList1.Items[i].ToString().ToUpper() == strData.ToString().ToUpper())
                {
                    CheckBoxList1.Items[i].Selected = true;
                }
            }
        }
    }

    public static void ClearCheckbox(CheckBoxList CheckBoxList1)
    {
        for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
        {
            CheckBoxList1.Items[i].Selected = false;
        }
    }

    public static string GetSelectedItems(CheckBoxList CheckBoxList1)
    {
        string str = string.Empty;
        for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
        {
            if (CheckBoxList1.Items[i].Selected == true)
            {
                str = str + CheckBoxList1.Items[i].ToString() + ",";
            }
        }
        if (str != string.Empty)
        {
            return Microsoft.VisualBasic.Strings.Left(str, str.Length - 1);
        }
        else
        {
            return string.Empty;
        }
        //ClearCheckbox(CheckBoxList1);
    }

    public static string GetSelectedItems_WODESC(CheckBoxList CheckBoxList1)
    {
        string str = string.Empty;
        for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
        {
            if (CheckBoxList1.Items[i].Selected == true)
            {
                str = str + CheckBoxList1.Items[i].ToString().Trim().Split('-').GetValue(0).ToString() + ",";
                //str = str + CheckBoxList1.Items[i].ToString() + ",";
            }
        }
        if (str != string.Empty)
        {
            return Microsoft.VisualBasic.Strings.Left(str, str.Length - 1);
        }
        else
        {
            return string.Empty;
        }
        //ClearCheckbox(CheckBoxList1);
    }

    public static void ChangeCheckStatus(CheckBoxList cbl, Boolean objValue)
    {
        if (objValue == true)
        {
            for (int i = 0; i <= cbl.Items.Count - 1; i++)
            {
                cbl.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= cbl.Items.Count - 1; i++)
            {
                cbl.Items[i].Selected = false;
            }
        }
    }

    public static void refreshGrid(GridView objGrid)
    {
        DataTable dt = new DataTable();
        objGrid.DataSource = dt;
        objGrid.DataBind();
        objGrid.SelectedIndex = 0;
        objGrid.Enabled = true;
    }

    public static void populateGrid(DataTable dt, GridView objGrid, string strExpression)
    {
        if (dt.Rows.Count != 0)
        {
            try
            {
                DataView dv = new DataView(dt);
                if (strExpression != string.Empty)
                {
                    dv.Sort = strExpression + " " + clsStandards.ConvertSortDirectionToSql(objGrid.SortDirection);
                }
                else
                {
                    //dv.Sort = dt.Columns[0].ColumnName + " " + clsStandards.ConvertSortDirectionToSql(objGrid.SortDirection);
                }
                objGrid.DataSource = dv;
                objGrid.ToolTip = dt.Rows.Count.ToString();
                objGrid.ShowFooter = true;
                objGrid.DataBind();
                objGrid.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        else
        {
            dt.Rows.Add(dt.NewRow());
            objGrid.ToolTip = "0";
            objGrid.DataSource = dt;
            objGrid.ShowFooter = false;
            objGrid.DataBind();
            objGrid.Rows[0].Visible = false;
        }
    }

    public static void populateGrid_Sap(DataTable dt, GridView objGrid, string strExpression)
    {       
       
        objGrid.DataSource = dt;
        objGrid.DataBind();
    }

    public static string ConvertSortDirectionToSql(SortDirection sortDireciton)
    {
        string newSortDirection = String.Empty;

        switch (sortDireciton)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }

    public static string FooterInfo(GridView gr)
    {
        try
        {
            int iEndRecord = gr.PageSize * (gr.PageIndex + 1);
            int iStartsRecods = iEndRecord - gr.PageSize;
            if (iEndRecord > Convert.ToInt32(gr.ToolTip)) { iEndRecord = Convert.ToInt32(gr.ToolTip); }
            if (iStartsRecods == 0) { iStartsRecods = 0; }
            if (iEndRecord == 0) { iEndRecord = Convert.ToInt32(gr.ToolTip); }
            return "Showing " + (iStartsRecods + 1) + " to " + iEndRecord.ToString() + " of " + gr.ToolTip + " record(s).";
        }
        catch { return string.Empty; }
    }

    public static string filter(string strValue)
    {
        strValue = strValue.Replace("&amp;", "&");
        strValue = strValue.Replace("&nbsp;", "");
        strValue = strValue.Replace("&quot;", "''");
        strValue = strValue.Replace("&iexcl;", "!");
        strValue = strValue.Replace("&brvbar;", "|");
        strValue = strValue.Replace("&acute;", "`");
        strValue = strValue.Replace("&lt;", "<");
        strValue = strValue.Replace("&gt;", ">");
        strValue = strValue.Replace("&tilde;", "~");
        strValue = strValue.Replace("&sbquo;", ",");
        strValue = strValue.Replace("'", "");
        strValue = strValue.Replace("''", "");
        strValue = strValue.Replace("&#160;", " ");
        strValue = strValue.Replace("&#181;", " ");
        strValue = strValue.Replace("&#39;&#39;", "");
        strValue = strValue.Replace("&#39;", "");
        strValue = strValue.Replace("  ", " ");
        strValue = HttpUtility.HtmlDecode(strValue);
        return strValue;
    }

    public static string filter_dateOnly(string strValue)
    {
        if (strValue.Contains(" ") == true)
        {
            return strValue.Split(' ').GetValue(0).ToString();
        }
        else
        {
            return strValue;
        }
    }

    public static string TextFormat(string strValue)
    {
        strValue = strValue.Replace(",", "\n");
        strValue = strValue.Replace(", ", "\n");
        strValue = strValue.Replace(",  ", "\n");
        return strValue;
    }

    public static string TitleCase(string strValue)
    {
        return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(strValue).ToLower());
    }

    public static string WhereStatement(int activate, string strField, string criteria, string strValue)
    {
        string strWhereStatement = "WHERE ";

        if (activate == 1) { strWhereStatement = strWhereStatement + " STATUS = 1"; } else { strWhereStatement = strWhereStatement + " STATUS IN (0, 1, 2)"; }

        if (strValue.Trim() != string.Empty)
        {
            strWhereStatement = strWhereStatement + " AND " + strField.Trim();

            switch (criteria)
            {
                case "Equal to":
                    strWhereStatement = strWhereStatement + " = '" + strValue.Trim() + "'";
                    break;
                case "Not equal to":
                    strWhereStatement = strWhereStatement + " <> '" + strValue.Trim() + "'";
                    break;
                case "Contains":
                    strWhereStatement = strWhereStatement + " LIKE '%" + strValue.Trim() + "%'";
                    break;
                case "Start with":
                    strWhereStatement = strWhereStatement + " LIKE '" + strValue.Trim() + "%'";
                    break;
                case "End with":
                    strWhereStatement = strWhereStatement + " LIKE '%" + strValue.Trim() + "'";
                    break;
                default:
                    strWhereStatement = strWhereStatement + " = '" + strValue.Trim() + "'";
                    break;
            }

            return strWhereStatement;
        }
        else
        {
            return strWhereStatement;
        }
    }

    public static string WhereStatementUserMaster(int activate, string strField, string criteria, string strValue)
    {
        string strWhereStatement = "WHERE ";

        if (activate == 1) { strWhereStatement = strWhereStatement + " STATUS = 1"; } 

        if (strValue.Trim() != string.Empty)
        {
            strWhereStatement = strWhereStatement + " AND " + strField.Trim();

            switch (criteria)
            {
                case "Equal to":
                    strWhereStatement = strWhereStatement + " = '" + strValue.Trim() + "'";
                    break;
                case "Not equal to":
                    strWhereStatement = strWhereStatement + " <> '" + strValue.Trim() + "'";
                    break;
                case "Contains":
                    strWhereStatement = strWhereStatement + " LIKE '%" + strValue.Trim() + "%'";
                    break;
                case "Start with":
                    strWhereStatement = strWhereStatement + " LIKE '" + strValue.Trim() + "%'";
                    break;
                case "End with":
                    strWhereStatement = strWhereStatement + " LIKE '%" + strValue.Trim() + "'";
                    break;
                default:
                    strWhereStatement = strWhereStatement + " = '" + strValue.Trim() + "'";
                    break;
            }

            return strWhereStatement;
        }
        else
        {
            return strWhereStatement;
        }
    }

    public static string WhereStatementNew(string strField, string criteria, string strValue,string strWhere)
    {
        string strWhereStatement = "";
        if (strWhere.Trim() == string.Empty)
        {
            strWhereStatement = "WHERE ";
        }
        else
        {
            if (strWhereStatement.Contains(strField) && criteria == "IN")
            {

            }
            else
            {

            }
        }

        if (strValue.Trim() != string.Empty)
        {
            strWhereStatement = strWhereStatement + " AND " + strField.Trim();

            switch (criteria)
            {
                case "Equal to":
                    strWhereStatement = strWhereStatement + " = '" + strValue.Trim() + "'";
                    break;
                case "Not equal to":
                    strWhereStatement = strWhereStatement + " <> '" + strValue.Trim() + "'";
                    break;
                case "Contains":
                    strWhereStatement = strWhereStatement + " LIKE '%" + strValue.Trim() + "%'";
                    break;
                case "Start with":
                    strWhereStatement = strWhereStatement + " LIKE '" + strValue.Trim() + "%'";
                    break;
                case "End with":
                    strWhereStatement = strWhereStatement + " LIKE '%" + strValue.Trim() + "'";
                    break;
                default:
                    strWhereStatement = strWhereStatement + " = '" + strValue.Trim() + "'";
                    break;
            }

            return strWhereStatement;
        }
        else
        {
            return strWhereStatement;
        }
    }

    public static string WhereStatement2(int activate, string strField, string criteria, string strValue)
    {
        string strWhereStatement = "WHERE ";

        if (activate == 1) { strWhereStatement = strWhereStatement + " DOWNLOAD_ST = 0"; } else { strWhereStatement = strWhereStatement + " DOWNLOAD_ST IN (0, 1, 2)"; }

        if (strValue.Trim() != string.Empty)
        {
            strWhereStatement = strWhereStatement + " AND " + strField.Trim();

            switch (criteria)
            {
                case "Equal to":
                    strWhereStatement = strWhereStatement + " = '" + strValue.Trim() + "'";
                    break;
                case "Not equal to":
                    strWhereStatement = strWhereStatement + " <> '" + strValue.Trim() + "'";
                    break;
                case "Contains":
                    strWhereStatement = strWhereStatement + " LIKE '%" + strValue.Trim() + "%'";
                    break;
                case "Start with":
                    strWhereStatement = strWhereStatement + " LIKE '" + strValue.Trim() + "%'";
                    break;
                case "End with":
                    strWhereStatement = strWhereStatement + " LIKE '%" + strValue.Trim() + "'";
                    break;
                default:
                    strWhereStatement = strWhereStatement + " = '" + strValue.Trim() + "'";
                    break;
            }

            return strWhereStatement;
        }
        else
        {
            return strWhereStatement;
        }
    }

    public static string WhereStatement_NoST(string strField, string criteria, string strValue)
    {
        string strWhereStatement = string.Empty;

        if (strValue.Trim() != string.Empty)
        {

            strWhereStatement = " WHERE ";

            strWhereStatement = strWhereStatement + strField.Trim();

            switch (criteria)
            {
                case "Equal to":
                    strWhereStatement = strWhereStatement + " = '" + strValue.Trim() + "'";
                    break;
                case "Not equal to":
                    strWhereStatement = strWhereStatement + " <> '" + strValue.Trim() + "'";
                    break;
                case "Contains":
                    strWhereStatement = strWhereStatement + " LIKE '%" + strValue.Trim() + "%'";
                    break;
                case "Start with":
                    strWhereStatement = strWhereStatement + " LIKE '" + strValue.Trim() + "%'";
                    break;
                case "End with":
                    strWhereStatement = strWhereStatement + " LIKE '%" + strValue.Trim() + "'";
                    break;
                case "In":
                    strWhereStatement = strWhereStatement + " In (" + strValue.Trim() + ")";
                    break;
                default:
                    strWhereStatement = strWhereStatement + " = '" + strValue.Trim() + "'";
                    break;
            }

            return strWhereStatement;
        }
        else
        {
            return strWhereStatement;
        }
    }

    public static string WhereStatement_NoST_WithDate(string strField, string criteria, string strValue, string Date1, string Date2)
    {
        string strWhereStatement = string.Empty;

        if (strValue.Trim() != string.Empty)
        {

            strWhereStatement = "WHERE ";

            strWhereStatement = strWhereStatement + strField.Trim();

            switch (criteria)
            {
                case "Equal to":
                    strWhereStatement = strWhereStatement + " = '" + strValue.Trim() + "'";
                    break;
                case "Not equal to":
                    strWhereStatement = strWhereStatement + " <> '" + strValue.Trim() + "'";
                    break;
                case "Contains":
                    strWhereStatement = strWhereStatement + " LIKE '%" + strValue.Trim() + "%'";
                    break;
                case "Start with":
                    strWhereStatement = strWhereStatement + " LIKE '" + strValue.Trim() + "%'";
                    break;
                case "End with":
                    strWhereStatement = strWhereStatement + " LIKE '%" + strValue.Trim() + "'";
                    break;
                default:
                    strWhereStatement = strWhereStatement + " = '" + strValue.Trim() + "'";
                    break;
            }
            if (Date1 != "" || Date2 != "")
            {

                return strWhereStatement + " AND CONVERT(VARCHAR(10),Y_MANF_DATE,104) BETWEEN CONVERT(VARCHAR(10),'" + Date1 + "',104) AND CONVERT(VARCHAR(10),'" + Date2 + "',104)";

            }
            return strWhereStatement;
        }
        else if (Date1 != "" || Date2 != "")
        {
            strWhereStatement = " WHERE CONVERT(VARCHAR(10),Y_MANF_DATE,104) BETWEEN CONVERT(VARCHAR(10),'" + Date1 + "',104) AND CONVERT(VARCHAR(10),'" + Date2 + "',104)";
        }
        else
        {
            return strWhereStatement;
        }
        return strWhereStatement;
    }

    public static string WhereStatement_NoST_Parameter(string strField, string criteria)
    {
        string strWhereStatement = "WHERE ";


        strWhereStatement = strWhereStatement + strField.Trim();

        switch (criteria)
        {
            case "Equal to":
                strWhereStatement = strWhereStatement + " = @PARAM";
                break;
            case "Not equal to":
                strWhereStatement = strWhereStatement + " <> @PARAM";
                break;
            case "Contains":
                strWhereStatement = strWhereStatement + " LIKE '%' + @PARAM + '%'";
                break;
            case "Start with":
                strWhereStatement = strWhereStatement + " LIKE @PARAM + '%'";
                break;
            case "End with":
                strWhereStatement = strWhereStatement + " LIKE '%' + @PARAM";
                break;
            default:
                strWhereStatement = strWhereStatement + " = @PARAM";
                break;
        }

        return strWhereStatement;
    }

    public static string GetInData(string strValue)
    {
        string[] strArr;
        string strResult="";

        if (strValue.Contains(','))
        {
            strArr = strValue.Split(',');

            for (int i = 0; i < strArr.Length; i++)
            {
                if (strResult == "")
                {
                    strResult = "'" + strArr[i].ToString() + "'";
                }
                else
                {
                    strResult = strResult + ",'" + strArr[i].ToString() + "'";
                }
            }

        }
        else
        {
            strResult = "'" + strValue + "'";
        }

        return strResult;
    }

    public static void RemoveItemTree(TreeView tr_Permissions)
    {
        for (int i = 0; i < tr_Permissions.Nodes.Count; i++)
        {
            tr_Permissions.Nodes[i].ChildNodes.Clear();
        }
        tr_Permissions.Nodes.Clear();
    }

    public static string GetSelectedItemsSplitbydash(CheckBoxList CheckBoxList1)
    {
        string str = string.Empty;
        for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
        {
            if (CheckBoxList1.Items[i].Selected == true)
            {
                if (CheckBoxList1.Items[i].ToString().Contains("-"))
                {
                    str = str + CheckBoxList1.Items[i].ToString().Split('-').GetValue(0).ToString() + ",";
                }
                else
                {
                    str = str + CheckBoxList1.Items[i].ToString() + ",";
                }

            }
        }
        if (str != string.Empty)
        {
            return Microsoft.VisualBasic.Strings.Left(str, str.Length - 1);
        }
        else
        {
            return string.Empty;
        }
        //ClearCheckbox(CheckBoxList1);
    }


    //public static string WhereStatement_NoST_Parameter(string strField, string criteria)
    //{
    //    string strWhereStatement = "WHERE ";


    //    strWhereStatement = strWhereStatement + strField.Trim();

    //    switch (criteria)
    //    {
    //        case "Equal to":
    //            strWhereStatement = strWhereStatement + " = @PARAM";
    //            break;
    //        case "Not equal to":
    //            strWhereStatement = strWhereStatement + " <> @PARAM";
    //            break;
    //        case "Contains":
    //            strWhereStatement = strWhereStatement + " LIKE '%' + @PARAM + '%'";
    //            break;
    //        case "Start with":
    //            strWhereStatement = strWhereStatement + " LIKE @PARAM + '%'";
    //            break;
    //        case "End with":
    //            strWhereStatement = strWhereStatement + " LIKE '%' + @PARAM";
    //            break;
    //        default:
    //            strWhereStatement = strWhereStatement + " = @PARAM";
    //            break;
    //    }

    //    return strWhereStatement;
    //}

    public static void WriteSoapXML(string xml, string type ,string strModule, string strFunction)
    {


        //Getting local log field identification from web.config.
        string logFile = HttpContext.Current.Server.MapPath(System.Web.Configuration.WebConfigurationManager.AppSettings["SOAPXMLLogFile"].ToString());

        // Include enterprise logic for logging exceptions
        // Open the log file for append and write the log
        string strFilename = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt";
        StreamWriter sw = new StreamWriter(logFile + strFilename, true);
        sw.WriteLine("================================================================");
        sw.WriteLine("========================" +type );
        sw.WriteLine("=================" + System.DateTime.Now);
        sw.WriteLine("Soap XML: " + xml + "");
        sw.WriteLine("Source: " + strModule + "| Function Name: " + strFunction + " " + System.DateTime.Now);
        sw.WriteLine("================================================================", DateTime.Now);
        sw.Close();
    }

   
}