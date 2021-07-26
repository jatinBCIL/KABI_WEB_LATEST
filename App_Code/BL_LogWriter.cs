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

/// <summary>
/// Summary description for BL_LogWriter
/// </summary>
public class BL_LogWriter
{

    public static int BL_Write(PL_LogWriter objFields)
    {
        SqlDataLayer objSql = new SqlDataLayer();
        string strQuery = string.Empty;
        try
        {
            strQuery = "INSERT INTO TBLEVENTS_LOGING (MODULE, METHOD, TYPE, DETAILS, CRDATE, CRUSER, PROGRAM,PLANT) VALUES (" +
                       "'" + objFields.strModuleName.Trim() + "', '" + objFields.strFunctionName.Trim() + "', " +
                       "'" + objFields.strErrorType.Trim() + "', '" + objFields.strDescription.Replace("System.Exception:", "").Trim() + "', GETDATE(), '" + clsStandards.current_Username() + "','" + objFields.strProgram.ToString() + "','" + objFields.strPlants.ToString() + "')";
            return objSql.ExecuteNonQuery(objSql.strLocal, strQuery);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            strQuery = string.Empty;
            objSql = null;
        }
    }

    public static DataTable BL_ReadLog(string dtstart, string dtEnd, string strCriteria, string strValue, string strFetch)
    {
        SqlDataLayer objSql = new SqlDataLayer();
        string strQuery = string.Empty;
        try
        {
            //Convert.ToDateTime(dtstart)
            if (strFetch != "0")
            {
                strQuery = "SELECT TOP (" + strFetch.Trim() + ") EVENTID, MODULE, METHOD, TYPE, DETAILS, CRDATE, CRUSER, PLANT, PROGRAM FROM dbo.VW_EVENTS ";
            }
            else
            {
                strQuery = "SELECT EVENTID, MODULE, METHOD, TYPE, DETAILS, CRDATE, CRUSER, PLANT, PROGRAM FROM dbo.VW_EVENTS ";
            }

            string strOldRef = string.Empty;
            if (strValue.Trim() != string.Empty)
            {
                strQuery = strQuery + " WHERE " + strCriteria.Trim() + " LIKE '%" + strValue.Trim() + "%' ";
                strOldRef = "1";
            }

            if (dtstart.Trim() != string.Empty && dtEnd.Trim() != string.Empty)
            {
                string AddedField = string.Empty;
                if (strOldRef == "1")
                {
                    AddedField = " AND";
                }
                else
                {
                    AddedField = " WHERE ";
                }
                if (dtstart.Trim() == dtEnd.Trim())
                {
                    strQuery = strQuery + AddedField + " CONVERT(VARCHAR(25), CRDATE, 101) = CONVERT(VARCHAR(25), REPLACE('" + dtstart + "', ' 12:00:00 AM', ''), 101) ";
                }
                else
                {
                    strQuery = strQuery + AddedField + " CONVERT(VARCHAR(25), CRDATE, 101) >= CONVERT(VARCHAR(25), REPLACE('" + dtstart + "', ' 12:00:00 AM', ''), 101)" +
                    "AND CONVERT(VARCHAR(25), CRDATE, 101) <=  CONVERT(VARCHAR(25), REPLACE('" + dtEnd + "', ' 12:00:00 AM', ''), 101)";
                }
            }

            return objSql.ExecuteDataset(objSql.strLocal, strQuery + " ORDER BY EVENTID DESC").Tables[0];
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

    //public static int BL_WriteQuery(string strQuery)
    //{
    //    SqlDataLayer objSql = new SqlDataLayer();
    //    try
    //    {
    //        string strCompany = string.Empty;
    //        string strUsername = string.Empty;

    //        try { strCompany = Convert.ToString(clsStandards.current_SourceLocation()); strUsername = Convert.ToString(clsStandards.current_Username()); }
    //        catch { }
    //        return objSql.ExecuteNonQuery(objSql.strLocal, "INSERT INTO TBLDEVLOG (QUERYSTRING, SITENAME, USERNAME, EXECUTEDON) VALUES('" + strQuery.Trim() + "', '" + strCompany + "', '" + strUsername + "', GETDATE())");
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.ToString());
    //    }
    //    finally
    //    {
    //        strQuery = string.Empty;
    //        objSql = null;
    //    }
    //}
}
