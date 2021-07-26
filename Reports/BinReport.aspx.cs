using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using PropertyLayer;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Web.UI.HtmlControls;


public partial class Reports_BinReport : System.Web.UI.Page
{
    BL_For_Reports objReports = new BL_For_Reports();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnpdfGenerate);
        if (!IsPostBack)
        {
            ddlbinfunction();
            ddlMaterialfunction();
        }   
    }
    public void ddlbinfunction()
    {
        try
        {
            DataTable dt = objReports.BL_DROPddlbin(HttpContext.Current.Session["MyPlant"].ToString());
            ddlbin.DataSource = dt;
            ddlbin.DataValueField = "Bin";
            ddlbin.DataTextField = "Bin";
            ddlbin.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    public void ddlMaterialfunction()
    {
        try
        {
            DataTable dt = objReports.BL_DROPddlMaterial(HttpContext.Current.Session["MyPlant"].ToString());
            ddlMaterial.DataSource = dt;
            ddlMaterial.DataValueField = "MaterialCode";
            ddlMaterial.DataTextField = "MaterialCode";
            ddlMaterial.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void imgSearch1_Click(object sender, EventArgs e)
    {
        try
        {
            tblgrid.Visible = true;
            DataTable dt = objReports.BL_searchBinGrid(ddlbin.SelectedItem.Text, HttpContext.Current.Session["MyPlant"].ToString());
            GrItemData.DataSource = dt;
            GrItemData.DataBind();
        }
        catch (Exception ex)
        {
 
        }
    }
    protected void imgSearch2_Click(object sender, EventArgs e)
    {
        try
        {
            tblgrid.Visible = true;
            DataTable dt = objReports.BL_searchMaterialGrid(ddlMaterial.SelectedItem.Text, HttpContext.Current.Session["MyPlant"].ToString());
            GrItemData.DataSource = dt;
            GrItemData.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    
    protected void btnpdfGenerate_click(object sender, EventArgs e)
        {
        try
        {
            GrItemData.AllowPaging = false;
            GrItemData.AllowSorting = false;
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "BinDetail" + ".pdf");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);
            clsxlsExport.ClearControls(GrItemData);
            GrItemData.HeaderStyle.BackColor = System.Drawing.Color.White;
            GrItemData.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            GrItemData.HeaderStyle.Font.Bold = true;
            GrItemData.HeaderStyle.Font.Underline = false;

            GrItemData.HeaderStyle.Width = 100;
            GrItemData.HeaderRow.Style.Add("word-wrap", "true");

            GrItemData.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            //Document pdfDoc = new Document(PageSize.A4, 8f, 8f, 8f, 2f);
            Document pdfDoc = new Document(PageSize.A4, 5f, 5f, 5f, 5f);
            //Header tittle = "Bin Report"; 
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.AddTitle("Bin REport");
            htmlparser.Parse(sr);
            pdfDoc.Close();
            GrItemData.AllowPaging = true;
            GrItemData.AllowSorting = true;
            HttpContext.Current.Response.Write(pdfDoc);
            HttpContext.Current.Response.End();


        }
        catch(Exception ex)
        {

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}