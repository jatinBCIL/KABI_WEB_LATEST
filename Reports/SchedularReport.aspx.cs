using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using BusinessLayer;
using PropertyLayer;
using System.IO;
using System.Globalization;

public partial class Reports_SchedularReport : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["strLocal"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)
        {
            //dwnBatch.Items.Add(new ListItem("--Select--", "0", true));
           // BatchData();
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        SchedularData();
    }

    //public void BatchData()
    //{

    //    SqlCommand cmd = new SqlCommand("select batchno from TBL_Scheduler_Details", con);
    //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    sda.Fill(dt);
    //    dwnBatch.DataSource = dt;
    //    dwnBatch.DataTextField = "batchno";  
    //    dwnBatch.DataBind(); 
    //}

    public void SchedularData()
    {
        string todate ="";
        string fromdate = "";
        string batch = "";
        string tdate = "";
        string fdate = "";
        SqlCommand cmd;

        try
        {
             todate = txtToDate.Text.Trim();
             fromdate = txtFromDate.Text.Trim();
             batch = txtBatch.Text.Trim();
           
            //DateTime tdate = DateTime.Parse(todate);
            //DateTime fdate = DateTime.Parse(fromdate);

             tdate=String.Format("{0:yyyy-MM-dd}", todate);
             fdate = String.Format("{0:yyyy-MM-dd}", fromdate);

             if (txtBatch.Text == "")
             {
                 cmd = new SqlCommand("select * from TBL_Scheduler_Details where ItemCode not like 'IO%' and cast(CreatedOn as date) between '" + tdate + "' and '" + fdate + "'order by CreatedOn desc", con);
             }
             else
             {
                 cmd = new SqlCommand("select * from TBL_Scheduler_Details where  BatchNo='" + batch + "' and cast(CreatedOn as date) between '" + tdate + "' and '" + fdate + "'order by CreatedOn desc", con);
             }

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }    
}