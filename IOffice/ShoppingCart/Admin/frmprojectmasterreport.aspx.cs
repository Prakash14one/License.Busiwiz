using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Data.Common;

//using System.ServiceProcess;
using System.Diagnostics;
using System.Windows;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Text;
using System.Net;
using System.Net.Mail;

public partial class ShoppingCart_Admin_FrmProjectMasterReport : System.Web.UI.Page
{
    static int temp;
    static DataTable dt;
    DataByCompany obj = new DataByCompany();
    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int ik = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[ik - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);

        if (!IsPostBack)
        {
            pageMailAccess();

            if (Request.QueryString["projectid"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["projectid"]);

                DataTable dtwh = ClsProject.SpProjectMasterGetDataStructById(id.ToString());

                if (dtwh.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtwh.Rows[0]["EmployeeId"].ToString()) > 0)
                    {
                        DropDownList1.SelectedIndex = 3;
                        DropDownList1_SelectedIndexChanged1(sender, e);
                        singlefilterbyemployee();

                        ddlbusunesswithdept.SelectedIndex = ddlbusunesswithdept.Items.IndexOf(ddlbusunesswithdept.Items.FindByValue(dtwh.Rows[0]["EmployeeId"].ToString()));
                        ddlbusunesswithdept.Enabled = false;  

                    }
                    else if (Convert.ToInt32(dtwh.Rows[0]["BusinessId"].ToString()) > 0)
                    {
                        DropDownList1.SelectedIndex = 2;
                        DropDownList1_SelectedIndexChanged1(sender, e);
                        singlefilterbydivision();

                        ddlbusunesswithdept.SelectedIndex = ddlbusunesswithdept.Items.IndexOf(ddlbusunesswithdept.Items.FindByValue(dtwh.Rows[0]["BusinessId"].ToString()));
                    }
                    else if (Convert.ToInt32(dtwh.Rows[0]["DeptId"].ToString()) > 0)
                    {
                        DropDownList1.SelectedIndex = 1;
                        DropDownList1_SelectedIndexChanged1(sender, e);
                        singlefilterbydepartment();

                        ddlbusunesswithdept.SelectedIndex = ddlbusunesswithdept.Items.IndexOf(ddlbusunesswithdept.Items.FindByValue(dtwh.Rows[0]["DeptId"].ToString()));
                    }

                    else if (Convert.ToInt32(dtwh.Rows[0]["Whid"].ToString()) > 0)
                    {
                        DropDownList1.SelectedIndex = 0;
                        DropDownList1_SelectedIndexChanged1(sender, e);
                        singlefilterbybusiness();

                        ddlbusunesswithdept.SelectedIndex = ddlbusunesswithdept.Items.IndexOf(ddlbusunesswithdept.Items.FindByValue(dtwh.Rows[0]["Whid"].ToString()));
                    }
                    fillobj();

                    ddlproject.SelectedIndex = ddlproject.Items.IndexOf(ddlproject.Items.FindByValue(Convert.ToInt32(dtwh.Rows[0]["ProjectId"]).ToString()));
                    ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByText(Convert.ToString(dtwh.Rows[0]["Status"]).ToString()));

                }
                fillemptask();
                fillmaster();
                fillevaluation();
                fillattach1();
                fillmasterdocs();
                fillmasterdocs111();

                DropDownList1.Enabled = false;
                ddlbusunesswithdept.Enabled = false;
                ddlproject.Enabled = false;
                ddlstatus.Enabled = false; 
            }

        }

    }
    protected void fillobj()
    {

        ddlproject.Items.Clear();
        DataTable ds12 = new DataTable();
        if (DropDownList1.SelectedIndex == 0)
        {
            ds12 = ClsProject.SpProjectbyddnotcomplete(ddlbusunesswithdept.SelectedValue, "0", "0", "0");
        }

        if (DropDownList1.SelectedIndex == 1)
        {
            ds12 = ClsProject.SpProjectbyddnotcomplete("0", ddlbusunesswithdept.SelectedValue, "0", "0");

        }

        if (DropDownList1.SelectedIndex == 2)
        {
            ds12 = ClsProject.SpProjectbyddnotcomplete("0", "0", ddlbusunesswithdept.SelectedValue, "0");

        }
        if (DropDownList1.SelectedIndex == 3)
        {
            ds12 = ClsProject.SpProjectbyddnotcomplete("0", "0", "0", ddlbusunesswithdept.SelectedValue);

        }
        if (ds12.Rows.Count > 0)
        {
            ddlproject.DataSource = ds12;

            ddlproject.DataMember = "projectname";
            ddlproject.DataTextField = "projectname";
            ddlproject.DataValueField = "ProjectId";
            ddlproject.DataBind();
        }
    }
    protected void singlefilterbybusiness()
    {
        DataTable ds = ClsStore.SelectStorename();
        ddlbusunesswithdept.DataSource = ds;
        ddlbusunesswithdept.DataTextField = "Name";
        ddlbusunesswithdept.DataValueField = "WareHouseId";
        ddlbusunesswithdept.DataBind();

        //ddlbusunesswithdept.Items.Insert(0, "All");
        //ddlbusunesswithdept.Items[0].Value = "0";

    }
    protected void singlefilterbydepartment()
    {

        string str12 = " select WareHouseMaster.Name, DepartmentmasterMNC.Departmentname as Dept,WareHouseMaster.Name +'/'+DepartmentmasterMNC.Departmentname as Departmentname,DepartmentmasterMNC.id as Id from WareHouseMaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.Whid=WareHouseMaster.WareHouseId where WarehouseMaster.Status=1 AND  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' order by WareHouseMaster.Name,DepartmentmasterMNC.Departmentname ";
        SqlCommand cmd1 = new SqlCommand(str12, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);

        ddlbusunesswithdept.DataSource = ds1;
        ddlbusunesswithdept.DataTextField = "Departmentname";
        ddlbusunesswithdept.DataValueField = "Id";
        ddlbusunesswithdept.DataBind();

        //ddlbusunesswithdept.Items.Insert(0, "All");
        //ddlbusunesswithdept.Items[0].Value = "0";

        //   fillfiltermission();

    }
    protected void singlefilterbydivision()
    {
        string str12 = " Select WareHouseMaster.Name+'/'+DepartmentmasterMNC.Departmentname+'/'+BusinessMaster.BusinessName as BusinessName ,BusinessMaster.BusinessID from BusinessMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=BusinessMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=BusinessMaster.Whid where WarehouseMaster.Status=1 AND BusinessMaster.company_id='" + Session["Comid"].ToString() + "' order by WareHouseMaster.Name,DepartmentmasterMNC.Departmentname,BusinessMaster.BusinessName";

        SqlCommand cmd1 = new SqlCommand(str12, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);

        ddlbusunesswithdept.DataSource = ds1;
        ddlbusunesswithdept.DataTextField = "BusinessName";
        ddlbusunesswithdept.DataValueField = "BusinessID";
        ddlbusunesswithdept.DataBind();

        //ddlbusunesswithdept.Items.Insert(0, "All");
        //ddlbusunesswithdept.Items[0].Value = "0";

    }
    protected void singlefilterbyemployee()
    {
        string str12 = " select Left(WareHouseMaster.Name,4)+'/'+Left(DepartmentmasterMNC.Departmentname,4)+'/'+ EmployeeMaster.EmployeeName as EmployeeName,EmployeeMaster.EmployeeMasterID from EmployeeMaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=EmployeeMaster.DeptID inner join WareHouseMaster on WareHouseMaster.WareHouseId=EmployeeMaster.Whid  where WarehouseMaster.Status=1 AND WareHouseMaster.comid='" + Session["Comid"].ToString() + "' order by WareHouseMaster.Name,DepartmentmasterMNC.Departmentname,EmployeeMaster.EmployeeName";

        SqlCommand cmd1 = new SqlCommand(str12, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);

        ddlbusunesswithdept.DataSource = ds1;
        ddlbusunesswithdept.DataTextField = "EmployeeName";
        ddlbusunesswithdept.DataValueField = "EmployeeMasterID";
        ddlbusunesswithdept.DataBind();

        //ddlbusunesswithdept.Items.Insert(0, "All");
        //ddlbusunesswithdept.Items[0].Value = "0";

    }
    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {

        if (DropDownList1.SelectedValue == "0")
        {
            Label3.Text = "Business Name";
            singlefilterbybusiness();

        }
        if (DropDownList1.SelectedValue == "1")
        {
            Label3.Text = "Business/Department";
            singlefilterbydepartment();

        }
        if (DropDownList1.SelectedValue == "2")
        {
            Label3.Text = "Business/Department/Division";
            singlefilterbydivision();

        }
        if (DropDownList1.SelectedValue == "3")
        {
            Label3.Text = "Business/Department/Employee";
            singlefilterbyemployee();

        }
        fillemptask();
        ddlbusunesswithdept_SelectedIndexChanged(sender, e);
    }


    protected void ddlbusunesswithdept_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillobj();
        ddlproject_SelectedIndexChanged(sender, e);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Print and Export")
        {
            Button1.Text = "Hide Print and Export";
            Button7.Visible = true;

            ddlExport.Visible = true;

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            fillevaluation();

            GridView6.AllowPaging = false;
            GridView6.PageSize = 1000;
            fillemptask();
            
            imgadddivision.Visible = false;            
            imgdivisionrefresh.Visible = false;

            ImgFIle.Visible = false;

            lblBusiness.Text = ddlbusunesswithdept.SelectedItem.Text;

        }
        else
        {
            Button1.Text = "Print and Export";
            Button7.Visible = false;

            ddlExport.Visible = false;
            ImgFIle.Visible = true;

            GridView1.AllowPaging = true;
            GridView1.PageSize = 5;
            fillevaluation();

            GridView6.AllowPaging = true;
            GridView6.PageSize = 5;
            fillemptask();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            DataTable dt = new DataTable();
            dt = SelectDoucmentMasterByID(Convert.ToInt32(e.CommandArgument));

            string docname = dt.Rows[0]["DocumentName"].ToString();
            string filepath = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + docname);
            string filewebpath = "Account/" + Session["comid"] + "/UploadedDocuments/" + docname;
            string name = docname.Trim();
            string extension = name.Substring(name.Length - 3);
            if (Convert.ToString(extension) == "pdf")
            {
                Session["ABCDE"] = "ABCDE";

                //                    string popupScript = "<script language='javascript'>" +
                //"newWindow=window.open('ViewDocument.aspx?id='" + e.CommandArgument + ", 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" + "</script>";
                int docid = 0;
                docid = Convert.ToInt32(e.CommandArgument);

                //                    Page.RegisterClientScriptBlock("newWindow", popupScript);
                //LinkButton lnkbtn = (LinkButton)Gridreqinfo.FindControl("LinkButton1");
                //lnkbtn.Attributes.Add("onclick", "window.open('ViewDocument.aspx?id='" + e.CommandArgument + ",, 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')");


                String temp = "ViewDocument.aspx?id=" + docid + "&Siddd=VHDS";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + temp + "');", true);


                //    Response.Redirect("ViewDocument.aspx?id=" + docid + "&Siddd=VHDS");
            }
            else
            {
                FileInfo file = new FileInfo(filepath);

                if (file.Exists)
                {
                    filewebpath = "http://" + Request.Url.Host.ToString() + "/" + filewebpath;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + filewebpath + "');", true);
                    //Response.ClearContent();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //Response.AddHeader("Content-Length", file.Length.ToString());
                    //Response.ContentType = ReturnExtension(file.Extension.ToLower());
                    //Response.TransmitFile(file.FullName);

                    //Response.End();

                }
            }
        }
    }
    public DataTable SelectDoucmentMasterByID(int DocumentId)
    {

        SqlCommand cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByID";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        DataTable dttable;
        SqlDataAdapter adp = new SqlDataAdapter((SqlCommand)cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds, cmd.CommandText);
        dttable = ds.Tables[0];

        adp.Fill(dttable);

        return dttable;
    }
    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {

        string te = "FrmTacticDetail.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgadddivision_Click(object sender, ImageClickEventArgs e)
    {

        string te = "FrmProjectEvalution.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgdivisionrefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillevaluation();
    }
    protected void fillmaster()
    {
        lblltgtitile.Text = "";
        lblstatus.Text = "";
        lbldescription.Text = "";
        lblbudgetedcost.Text = "";
        lblactualcost.Text = "";
        lblshortageexcess.Text = "";
        if (ddlproject.SelectedIndex != -1)
        {
            string strmaster = "Select ProjectMaster.* from ProjectMaster where ProjectMaster.ProjectId ='" + ddlproject.SelectedValue + "' ";
            SqlCommand cmdmaster = new SqlCommand(strmaster, con);
            SqlDataAdapter adpmaster = new SqlDataAdapter(cmdmaster);
            DataTable dtmaster = new DataTable();
            adpmaster.Fill(dtmaster);
            if (dtmaster.Rows.Count > 0)
            {
                lblltgtitile.Text = dtmaster.Rows[0]["ProjectName"].ToString();
                lblstatus.Text = dtmaster.Rows[0]["Status"].ToString();
                lbldescription.Text = dtmaster.Rows[0]["Description"].ToString();

                if (Convert.ToString(dtmaster.Rows[0]["BudgetedAmount"]) != "")
                {
                    lblbudgetedcost.Text = dtmaster.Rows[0]["BudgetedAmount"].ToString();
                }
                else
                {
                    lblbudgetedcost.Text = "0";
                }
            }

            string strmaster11 = "select sum(cast(TaskAllocationMaster.ActualRate as float)) as ActualAmount from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join ProjectMaster on ProjectMaster.ProjectId=TaskMaster.ProjectId where ProjectMaster.ProjectId='" + ddlproject.SelectedValue + "' ";
            SqlCommand cmdmaster11 = new SqlCommand(strmaster11, con);
            SqlDataAdapter adpmaster11 = new SqlDataAdapter(cmdmaster11);
            DataTable dtmaster11 = new DataTable();
            adpmaster11.Fill(dtmaster11);
            if (dtmaster11.Rows.Count > 0)
            {
                if (Convert.ToString(dtmaster11.Rows[0]["ActualAmount"]) != "")
                {
                    lblactualcost.Text = dtmaster11.Rows[0]["ActualAmount"].ToString();
                }
                else
                {
                    lblactualcost.Text = "0";
                }
            }

            double value = Convert.ToDouble(lblbudgetedcost.Text) - Convert.ToDouble(lblactualcost.Text);
            lblshortageexcess.Text = value.ToString();
        }

    }

    private int GetRowCount1(string str)
    {
        int count = 0;
        DataTable dte = new DataTable();
        dte = select(str);
        if (dte.Rows.Count > 0)
        {
            count += Convert.ToInt32(dte.Rows[0]["ci"]);
        }
        ViewState["count"] = count;
        return count;

    }

    private int GetRowCount2(string str)
    {
        int count = 0;
        DataTable dte = new DataTable();
        dte = select(str);
        if (dte.Rows.Count > 0)
        {
            count += Convert.ToInt32(dte.Rows[0]["ci"]);
        }
        ViewState["count"] = count;
        return count;

    }

    private DataTable GetDataPage1(int pageIndex, int pageSize, string sortExpression, string query)
    {
        DataTable dt = select(string.Format("SELECT * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));
        dt.Columns.Remove("ROW_NUM");
        return dt;

        ViewState["dt"] = dt;
    }

    private DataTable GetDataPage2(int pageIndex, int pageSize, string sortExpression, string query)
    {
        DataTable dt = select(string.Format("SELECT * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));
        dt.Columns.Remove("ROW_NUM");
        return dt;

        ViewState["dt"] = dt;
    }

    protected void fillevaluation()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();

        if (ddlproject.SelectedIndex != -1)
        {
            string str = "ProjectEvaluation.*,ProjectMaster.BusinessId as DocumentId,ProjectMaster.ActualAmount,ProjectMaster.BudgetedAmount,ProjectMaster.ShortageExcess from ProjectEvaluation inner join ProjectMaster on ProjectMaster.ProjectId=ProjectEvaluation.ProjectId where ProjectMaster.ProjectId='" + ddlproject.SelectedValue + "' ";

            string str2 = "select Count(ProjectEvaluation.EvaluationId) as ci from ProjectEvaluation inner join ProjectMaster on ProjectMaster.ProjectId=ProjectEvaluation.ProjectId where ProjectMaster.ProjectId='" + ddlproject.SelectedValue + "' ";


            GridView1.VirtualItemCount = GetRowCount1(str2);

            string sortExpression = " ProjectEvaluation.StatusDate";

            if (Convert.ToInt32(ViewState["count"]) > 0)
            {
                DataTable dt = GetDataPage1(GridView1.PageIndex, GridView1.PageSize, sortExpression, str);

                GridView1.DataSource = dt;

                for (int rowindex = 0; rowindex < dt.Rows.Count; rowindex++)
                {
                    int evaid = Convert.ToInt32(dt.Rows[rowindex]["EvaluationId"].ToString());

                    DataTable dtcrNew11 = ClsProject.SelectOfficeManagerDocumentswithprevaid(Convert.ToString(evaid));

                    dt.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];

                }

                GridView1.DataBind();

                Panel1.Visible = true;
                lblevaluation.Visible = true;

                imgadddivision.Visible = true;
                imgdivisionrefresh.Visible = true;
            }
            else
            {
                Panel1.Visible = false;
                imgadddivision.Visible = false;
                imgdivisionrefresh.Visible = false;
                lblevaluation.Visible = false;
            }
        }
        else
        {
            Panel1.Visible = false;
            imgadddivision.Visible = false;
            imgdivisionrefresh.Visible = false;
            lblevaluation.Visible = false;
        }
    }
    //    string strmaster11 = "select sum(cast(TaskAllocationMaster.ActualRate as decimal)) as ActualAmount from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join ProjectMaster on ProjectMaster.ProjectId=TaskMaster.ProjectId inner join ProjectEvaluation on ProjectEvaluation.ProjectId=ProjectMaster.ProjectId where ProjectEvaluation.EvaluationId='" + evaid + "' ";
    //    SqlCommand cmdmaster11 = new SqlCommand(strmaster11, con);
    //    SqlDataAdapter adpmaster11 = new SqlDataAdapter(cmdmaster11);
    //    DataTable dtmaster11 = new DataTable();
    //    adpmaster11.Fill(dtmaster11);

    //    if (Convert.ToString(dtmaster11.Rows[0]["ActualAmount"]) != "")
    //    {
    //        dteve.Rows[rowindex]["ActualAmount"] = Convert.ToString(dtmaster11.Rows[0]["ActualAmount"]);
    //    }
    //    else
    //    {
    //        dteve.Rows[rowindex]["ActualAmount"] = "0";
    //    }
    //    int mm = Convert.ToInt32(dteve.Rows[rowindex]["BudgetedAmount"]) - Convert.ToInt32(dteve.Rows[rowindex]["ActualAmount"]);
    //    dteve.Rows[rowindex]["ShortageExcess"] = mm.ToString();


    protected void fillmasterdocs()
    {
        GridView3.DataSource = null;
        GridView3.DataBind();
        if (ddlproject.SelectedIndex != -1)
        {
            string strltgdoc = "Select  OfficeManagerDocuments.Id,DocumentMaster.DocumentName, DocumentMaster.DocumentId,CONVERT(nvarchar, DocumentMaster.DocumentDate,101) as DocumentDate,DocumentMaster.DocumentTitle  from OfficeManagerDocuments inner join DocumentMaster on DocumentMaster.DocumentId=OfficeManagerDocuments.DocumentId  where OfficeManagerDocuments.ProjectId='" + ddlproject.SelectedValue + "' ";
            SqlCommand cmdltgdoc = new SqlCommand(strltgdoc, con);
            SqlDataAdapter adpltgdoc = new SqlDataAdapter(cmdltgdoc);
            DataTable dtdoc = new DataTable();
            adpltgdoc.Fill(dtdoc);
            if (dtdoc.Rows.Count > 0)
            {
                if (Convert.ToString(dtdoc.Rows[0]["DocumentName"]) != "")
                {
                    //lblltgdocs.Visible = false;
                    //Panel2.Visible = true;

                    GridView3.Visible = true;
                    GridView3.DataSource = dtdoc;
                    GridView3.DataBind();
                }
            }
            else
            {
                //Panel2.Visible = false;
                GridView3.Visible = false;
                //lblltgdocs.Visible = false;
            }
        }

    }


    protected void fillmasterdocs111()
    {
        GridView2.DataSource = null;
        GridView2.DataBind();

        if (ddlproject.SelectedIndex != -1)
        {
            string strltgdoc = "Select  OfficeManagerDocuments.Id,DocumentMaster.DocumentName, DocumentMaster.DocumentId,CONVERT(nvarchar, DocumentMaster.DocumentDate,101) as DocumentDate,DocumentMaster.DocumentTitle  from OfficeManagerDocuments inner join DocumentMaster on DocumentMaster.DocumentId=OfficeManagerDocuments.DocumentId  where OfficeManagerDocuments.ProjectId='" + ddlproject.SelectedValue + "' ";
            SqlCommand cmdltgdoc = new SqlCommand(strltgdoc, con);
            SqlDataAdapter adpltgdoc = new SqlDataAdapter(cmdltgdoc);
            DataTable dtdoc = new DataTable();
            adpltgdoc.Fill(dtdoc);

            if (dtdoc.Rows.Count > 0)
            {
                if (Convert.ToString(dtdoc.Rows[0]["DocumentName"]) != "")
                {
                    //lblltgdocs.Visible = false;
                    //Panel2.Visible = true;

                    GridView2.Visible = true;
                    GridView2.DataSource = dtdoc;
                    GridView2.DataBind();
                }
            }
            else
            {
                //Panel2.Visible = false;
                GridView2.Visible = false;
                //lblltgdocs.Visible = false;
            }
        }

    }
    protected void ddlproject_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillmaster();
        fillattach123();
        fillevaluation();
        fillmasterdocs();
        fillemptask();
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillmaster();
        fillevaluation();
        fillmasterdocs();
    }
    protected void linkjob_Click(object sender, EventArgs e)
    {
        string te = "JobManage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }

    protected void ddlExport_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (GridView1.Rows.Count > 0 || GridView2.Rows.Count > 0)
        //{
        //Button1.Text = "Printable Version";
        //Button1_Click(sender, e);
        //Button7.Visible = false;

        if (ddlExport.SelectedValue == "1")
        {
            System.IO.MemoryStream msReport = new System.IO.MemoryStream();

            Document document = new Document(PageSize.A2, 0f, 0f, 30f, 30f);

            PdfWriter writer = PdfWriter.GetInstance(document, msReport);

            this.EnableViewState = false;

            Response.Charset = string.Empty;

            document.AddSubject("Export to PDF");
            document.Open();

            iTextSharp.text.Table datatable4 = new iTextSharp.text.Table(3);

            datatable4.Padding = 2;
            datatable4.Spacing = 1;
            datatable4.Width = 90;

            float[] headerwidths4 = new float[3];

            headerwidths4[0] = 10;
            headerwidths4[1] = 80;
            headerwidths4[2] = 10;


            datatable4.Widths = headerwidths4;

            Cell cell = new Cell(new Phrase("Business Name :" + ddlbusunesswithdept.SelectedItem.Text, FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;

            cell.Colspan = 3;
            cell.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell);

            datatable4.DefaultCellBorderWidth = 1;
            datatable4.DefaultHorizontalAlignment = 1;

            Cell cell3 = new Cell(new Phrase("Project Master Report", FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;

            cell3.Colspan = 3;
            cell3.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell3);

            Cell cell31 = new Cell(new Phrase("Project Title : " + lblltgtitile.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell31.HorizontalAlignment = Element.ALIGN_LEFT;

            cell31.Colspan = 3;
            cell31.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell31);

            Cell cell310 = new Cell(new Phrase("Status : " + lblstatus.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell310.HorizontalAlignment = Element.ALIGN_LEFT;

            cell310.Colspan = 3;
            cell310.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell310);


            Cell cell311 = new Cell(new Phrase("Description : " + lbldescription.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell311.HorizontalAlignment = Element.ALIGN_LEFT;

            cell311.Colspan = 3;
            cell311.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell311);



            Cell cell312 = new Cell(new Phrase("Attachments : " + LinkButton2.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell312.HorizontalAlignment = Element.ALIGN_LEFT;

            cell312.Colspan = 3;
            cell312.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell312);


            Cell cell313 = new Cell(new Phrase("Budgeted Cost : " + lblbudgetedcost.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell313.HorizontalAlignment = Element.ALIGN_LEFT;

            cell313.Colspan = 3;
            cell313.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell313);

            Cell cell314 = new Cell(new Phrase("Actual Cost : " + lblactualcost.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell314.HorizontalAlignment = Element.ALIGN_LEFT;

            cell314.Colspan = 3;
            cell314.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell314);


            Cell cell315 = new Cell(new Phrase("Shortage/Excess : " + lblshortageexcess.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell315.HorizontalAlignment = Element.ALIGN_LEFT;

            cell315.Colspan = 3;
            cell315.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell315);            
            document.Add(datatable4);

            if (GridView1.Rows.Count > 0)
            {
                iTextSharp.text.Table datatable = new iTextSharp.text.Table(5);
                datatable.Padding = 2;
                datatable.Spacing = 1;
                datatable.Width = 90;

                float[] headerwidths = new float[5];

                headerwidths[0] = 10;
                headerwidths[1] = 60;
                headerwidths[2] = 8;
                headerwidths[3] = 12;
                headerwidths[4] = 10;

                datatable.Widths = headerwidths;

                Cell cell1 = new Cell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                cell1.HorizontalAlignment = Element.ALIGN_LEFT;

                cell1.Colspan = 5;
                cell1.Border = Rectangle.NO_BORDER;

                datatable.AddCell(cell1);

                Cell cell2 = new Cell(new Phrase("Status Notes", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                cell2.Colspan = 5;
                cell2.Border = Rectangle.TOP_BORDER;

                datatable.AddCell(cell2);

                datatable.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                datatable.AddCell(new Cell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase("Status Note", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase("Actual Cost", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase("Shortage/Excess", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase("Docs", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    Label lblmasterdate = (Label)GridView1.Rows[i].FindControl("lblmasterdate");
                    Label lblevaluationnote123 = (Label)GridView1.Rows[i].FindControl("lblevaluationnote123");
                    Label lblevActualCost123 = (Label)GridView1.Rows[i].FindControl("lblevActualCost123");
                    Label lShortageExcesste123 = (Label)GridView1.Rows[i].FindControl("lShortageExcesste123");
                    LinkButton LinkButton1 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton1");


                    datatable.AddCell(lblmasterdate.Text);
                    datatable.AddCell(lblevaluationnote123.Text);
                    datatable.AddCell(lblevActualCost123.Text);
                    datatable.AddCell(lShortageExcesste123.Text);
                    datatable.AddCell(LinkButton1.Text);

                }
                document.Add(datatable);
            }

            if (GridView6.Rows.Count > 0)
            {

                iTextSharp.text.Table datatable2 = new iTextSharp.text.Table(8);

                datatable2.Padding = 2;
                datatable2.Spacing = 1;
                datatable2.Width = 90;

                float[] headerwidths2 = new float[8];

                headerwidths2[0] = 7;
                headerwidths2[1] = 13;
                headerwidths2[2] = 15;
                headerwidths2[3] = 20;
                headerwidths2[4] = 8;
                headerwidths2[5] = 8;
                headerwidths2[6] = 7;
                headerwidths2[7] = 7;

                datatable2.Widths = headerwidths2;

                Cell cell1x = new Cell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                cell1x.HorizontalAlignment = Element.ALIGN_LEFT;

                cell1x.Colspan = 8;
                cell1x.Border = Rectangle.NO_BORDER;

                datatable2.AddCell(cell1x);


                Cell cello2 = new Cell(new Phrase("Task Done", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                cello2.HorizontalAlignment = Element.ALIGN_LEFT;
                cello2.Colspan = 8;
                cello2.Border = Rectangle.TOP_BORDER;

                datatable2.AddCell(cello2);

                datatable2.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                datatable2.AddCell(new Cell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Employee Name", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Task", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Task Report", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Budgeted Minute", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Actual Minute", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Actual Cost", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Employee Avg Rate", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));


                for (int i = 0; i < GridView6.Rows.Count; i++)
                {

                    Label lblmasterdate2 = (Label)GridView6.Rows[i].FindControl("lblmasterdate2");
                    Label lblempname = (Label)GridView6.Rows[i].FindControl("lblempname");
                    Label lblTask = (Label)GridView6.Rows[i].FindControl("lblTask");
                    Label lblTaskReport = (Label)GridView6.Rows[i].FindControl("lblTaskReport");
                    Label lblbudgetedminute123 = (Label)GridView6.Rows[i].FindControl("lblbudgetedminute123");
                    Label lblunitsused = (Label)GridView6.Rows[i].FindControl("lblunitsused");
                    Label lblactempcost = (Label)GridView6.Rows[i].FindControl("lblactempcost");
                    Label lblavgrate = (Label)GridView6.Rows[i].FindControl("lblavgrate");


                    datatable2.AddCell(lblmasterdate2.Text);
                    datatable2.AddCell(lblempname.Text);
                    datatable2.AddCell(lblTask.Text);
                    datatable2.AddCell(lblTaskReport.Text);
                    datatable2.AddCell(lblbudgetedminute123.Text);
                    datatable2.AddCell(lblunitsused.Text);
                    datatable2.AddCell(lblactempcost.Text);
                    datatable2.AddCell(lblavgrate.Text);

                }
                document.Add(datatable2);
            }

            document.Close();

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(msReport.ToArray());

            Response.End();

        }
        else if (ddlExport.SelectedValue == "2")
        {

            Response.Clear();

            Response.Buffer = true;

            Response.AddHeader("content-disposition",

            "attachment;filename=GridViewExport.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow row = GridView1.Rows[i];

                    row.BackColor = System.Drawing.Color.White;

                    row.Attributes.Add("class", "textmode");
                }
            }

            if (GridView6.Rows.Count > 0)
            {

                GridView6.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < GridView6.Rows.Count; i++)
                {
                    GridViewRow row = GridView6.Rows[i];

                    row.BackColor = System.Drawing.Color.White;

                    row.Attributes.Add("class", "textmode");
                }
            }

            pnlgrid.RenderControl(hw);

            string style = @"<style> .textmode { mso-number-format:\@; } </style>";

            Response.Write(style);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();
        }
        else if (ddlExport.SelectedValue == "3")
        {
            Response.Clear();

            Response.Buffer = true;

            Response.AddHeader("content-disposition",

            "attachment;filename=GridViewExport.doc");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-word ";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);


            pnlgrid.RenderControl(hw);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();

        }
        else if (ddlExport.SelectedValue == "4")
        {

            Document document = new Document(PageSize.A2, 0f, 0f, 30f, 30f);
            string filename = "GrdM_" + System.DateTime.Today.Day + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second;
            Session["Emfile"] = filename + ".pdf";
            Session["GrdmailA"] = null;
            PdfWriter.GetInstance(document, new FileStream(HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".pdf"), FileMode.Create));

            try
            {
                document.AddSubject("Export to PDF");
                document.Open();

                iTextSharp.text.Table datatable4 = new iTextSharp.text.Table(3);

                datatable4.Padding = 2;
                datatable4.Spacing = 1;
                datatable4.Width = 90;

                float[] headerwidths4 = new float[3];

                headerwidths4[0] = 10;
                headerwidths4[1] = 80;
                headerwidths4[2] = 10;


                datatable4.Widths = headerwidths4;

                Cell cell = new Cell(new Phrase("Business Name :" + ddlbusunesswithdept.SelectedItem.Text, FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;

                cell.Colspan = 3;
                cell.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell);

                datatable4.DefaultCellBorderWidth = 1;
                datatable4.DefaultHorizontalAlignment = 1;

                Cell cell3 = new Cell(new Phrase("Project Master Report", FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
                cell3.HorizontalAlignment = Element.ALIGN_CENTER;

                cell3.Colspan = 3;
                cell3.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell3);

                Cell cell31 = new Cell(new Phrase("Project Title : " + lblltgtitile.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell31.HorizontalAlignment = Element.ALIGN_LEFT;

                cell31.Colspan = 3;
                cell31.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell31);


                Cell cell310 = new Cell(new Phrase("Status : " + lblstatus.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell310.HorizontalAlignment = Element.ALIGN_LEFT;

                cell310.Colspan = 3;
                cell310.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell310);


                Cell cell311 = new Cell(new Phrase("Description : " + lbldescription.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell311.HorizontalAlignment = Element.ALIGN_LEFT;

                cell311.Colspan = 3;
                cell311.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell311);



                Cell cell312 = new Cell(new Phrase("Attachments : " + LinkButton2.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell312.HorizontalAlignment = Element.ALIGN_LEFT;

                cell312.Colspan = 3;
                cell312.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell312);


                Cell cell313 = new Cell(new Phrase("Budgeted Cost : " + lblbudgetedcost.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell313.HorizontalAlignment = Element.ALIGN_LEFT;

                cell313.Colspan = 3;
                cell313.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell313);

                Cell cell314 = new Cell(new Phrase("Actual Cost : " + lblactualcost.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell314.HorizontalAlignment = Element.ALIGN_LEFT;

                cell314.Colspan = 3;
                cell314.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell314);


                Cell cell315 = new Cell(new Phrase("Shortage/Excess : " + lblshortageexcess.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell315.HorizontalAlignment = Element.ALIGN_LEFT;

                cell315.Colspan = 3;
                cell315.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell315);                

                document.Add(datatable4);


                if (GridView1.Rows.Count > 0)
                {
                    iTextSharp.text.Table datatable = new iTextSharp.text.Table(5);
                    datatable.Padding = 2;
                    datatable.Spacing = 1;
                    datatable.Width = 90;

                    float[] headerwidths = new float[5];

                    headerwidths[0] = 10;
                    headerwidths[1] = 60;
                    headerwidths[2] = 8;
                    headerwidths[3] = 12;
                    headerwidths[4] = 10;

                    datatable.Widths = headerwidths;

                    Cell cell1 = new Cell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;

                    cell1.Colspan = 5;
                    cell1.Border = Rectangle.NO_BORDER;

                    datatable.AddCell(cell1);

                    Cell cell2 = new Cell(new Phrase("Status Notes", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                    cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell2.Colspan = 5;
                    cell2.Border = Rectangle.TOP_BORDER;

                    datatable.AddCell(cell2);

                    datatable.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                    datatable.AddCell(new Cell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable.AddCell(new Cell(new Phrase("Status Note", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable.AddCell(new Cell(new Phrase("Actual Cost", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable.AddCell(new Cell(new Phrase("Shortage/Excess", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable.AddCell(new Cell(new Phrase("Docs", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        Label lblmasterdate = (Label)GridView1.Rows[i].FindControl("lblmasterdate");
                        Label lblevaluationnote123 = (Label)GridView1.Rows[i].FindControl("lblevaluationnote123");
                        Label lblevActualCost123 = (Label)GridView1.Rows[i].FindControl("lblevActualCost123");
                        Label lShortageExcesste123 = (Label)GridView1.Rows[i].FindControl("lShortageExcesste123");
                        LinkButton LinkButton1 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton1");


                        datatable.AddCell(lblmasterdate.Text);
                        datatable.AddCell(lblevaluationnote123.Text);
                        datatable.AddCell(lblevActualCost123.Text);
                        datatable.AddCell(lShortageExcesste123.Text);
                        datatable.AddCell(LinkButton1.Text);

                    }
                    document.Add(datatable);
                }

                if (GridView6.Rows.Count > 0)
                {

                    iTextSharp.text.Table datatable2 = new iTextSharp.text.Table(8);

                    datatable2.Padding = 2;
                    datatable2.Spacing = 1;
                    datatable2.Width = 90;

                    float[] headerwidths2 = new float[8];

                    headerwidths2[0] = 7;
                    headerwidths2[1] = 13;
                    headerwidths2[2] = 15;
                    headerwidths2[3] = 20;
                    headerwidths2[4] = 8;
                    headerwidths2[5] = 8;
                    headerwidths2[6] = 7;
                    headerwidths2[7] = 7;

                    datatable2.Widths = headerwidths2;

                    Cell cell1x = new Cell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                    cell1x.HorizontalAlignment = Element.ALIGN_LEFT;

                    cell1x.Colspan = 8;
                    cell1x.Border = Rectangle.NO_BORDER;

                    datatable2.AddCell(cell1x);


                    Cell cello2 = new Cell(new Phrase("Task Done", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                    cello2.HorizontalAlignment = Element.ALIGN_LEFT;
                    cello2.Colspan = 8;
                    cello2.Border = Rectangle.TOP_BORDER;

                    datatable2.AddCell(cello2);

                    datatable2.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                    datatable2.AddCell(new Cell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Employee Name", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Task", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Task Report", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Budgeted Minute", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Actual Minute", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Actual Cost", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Employee Avg Rate", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));


                    for (int i = 0; i < GridView6.Rows.Count; i++)
                    {

                        Label lblmasterdate2 = (Label)GridView6.Rows[i].FindControl("lblmasterdate2");
                        Label lblempname = (Label)GridView6.Rows[i].FindControl("lblempname");
                        Label lblTask = (Label)GridView6.Rows[i].FindControl("lblTask");
                        Label lblTaskReport = (Label)GridView6.Rows[i].FindControl("lblTaskReport");
                        Label lblbudgetedminute123 = (Label)GridView6.Rows[i].FindControl("lblbudgetedminute123");
                        Label lblunitsused = (Label)GridView6.Rows[i].FindControl("lblunitsused");
                        Label lblactempcost = (Label)GridView6.Rows[i].FindControl("lblactempcost");
                        Label lblavgrate = (Label)GridView6.Rows[i].FindControl("lblavgrate");


                        datatable2.AddCell(lblmasterdate2.Text);
                        datatable2.AddCell(lblempname.Text);
                        datatable2.AddCell(lblTask.Text);
                        datatable2.AddCell(lblTaskReport.Text);
                        datatable2.AddCell(lblbudgetedminute123.Text);
                        datatable2.AddCell(lblunitsused.Text);
                        datatable2.AddCell(lblactempcost.Text);
                        datatable2.AddCell(lblavgrate.Text);

                    }
                    document.Add(datatable2);
                }

                document.Close();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
            document.Close();
            string te = "MessageComposeExt.aspx?ema=Azxcvyute";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }

        // }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected DataTable selectbcon(string str)
    {
        SqlCommand cmd = new SqlCommand(str, PageConn.licenseconn());
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;

    }
    protected void pageMailAccess()
    {
        ddlExport.Items.Insert(0, "Export Type");
        ddlExport.Items[0].Value = "0";
        ddlExport.Items.Insert(1, "Export to PDF");
        ddlExport.Items[1].Value = "1";
        ddlExport.Items.Insert(2, "Export to Excel");
        ddlExport.Items[2].Value = "2";
        ddlExport.Items.Insert(3, "Export to Word");
        ddlExport.Items[3].Value = "3";


        string avfr = "  and PageMaster.PageName='" + ClsEncDesc.EncDyn("MessageCompose.aspx") + "'";
        DataTable drt = selectbcon("SELECT distinct " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId,PageMaster.PageName FROM MainMenuMaster inner join " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=" + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'" + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");
        if (drt.Rows.Count <= 0)
        {

            drt = selectbcon("SELECT PageMaster.PageName FROM PageMaster inner join " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl on " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PageId=PageMaster.PageId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId INNER JOIN " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' " + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");
            if (drt.Rows.Count <= 0)
            {
                drt = selectbcon("SELECT distinct PageMaster.PageName FROM MainMenuMaster inner join  SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.SubMenuId=SubMenuMaster.SubMenuId inner join PageMaster on PageMaster.SubMenuId=" + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.SubMenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'" + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");


                if (drt.Rows.Count <= 0)
                {


                }
                else
                {
                    ddlExport.Items.Insert(4, "Email with PDF");
                    ddlExport.Items[4].Value = "4";
                }

            }
            else
            {
                ddlExport.Items.Insert(4, "Email with PDF");
                ddlExport.Items[4].Value = "4";

            }


        }
        else
        {

            ddlExport.Items.Insert(4, "Email with PDF");
            ddlExport.Items[4].Value = "4";

        }
    }

    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }

    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Send")
        {
            ViewState["MissionId"] = Convert.ToInt32(e.CommandArgument);

            DataTable dtcrNew11 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["MissionId"]), 116);

            GridView3.DataSource = dtcrNew11;
            GridView3.DataBind();

            ModalPopupExtenderAddnew.Show();
        }
    }


    protected void fillattach1()
    {
        DataTable dtcrNew11 = ClsProject.SelectOfficeManagerDocumentswithprid(Convert.ToString(Request.QueryString["projectid"].ToString()));

        LinkButton2.Text = dtcrNew11.Rows[0]["DocumentCount"].ToString();

        ViewState["att"] = Convert.ToString(Request.QueryString["projectid"].ToString());
    }

    protected void fillattach123()
    {
        DataTable dtcrNew11 = new DataTable();

        if (ddlproject.SelectedIndex != -1)
        {
            dtcrNew11 = ClsProject.SelectOfficeManagerDocumentswithprid(Convert.ToString(ddlproject.SelectedValue));

            LinkButton2.Text = dtcrNew11.Rows[0]["DocumentCount"].ToString();

            ViewState["att"] = Convert.ToString(ddlproject.SelectedValue);
        }

        else
        {
            ViewState["att"] = "0";

            LinkButton2.Text = "0";
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        if (LinkButton2.Text == "0")
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
        else
        {
            ViewState["MissionId"] = ViewState["att"];

            DataTable dtcrNew11 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["MissionId"]), 10);

            GridView2.DataSource = dtcrNew11;
            GridView2.DataBind();
        }

        ModalPopupExtender1.Show();
    }

    protected void fillemptask()
    {
        //string str2 = " Select EmployeeMaster.Whid from WareHouseMaster inner join EmployeeMaster on EmployeeMaster.Whid=WareHouseMaster.WareHouseId where  EmployeeMaster.EmployeeMasterID= '" + ddlbusunesswithdept.SelectedValue + "'  ";

        //SqlCommand cmd2 = new SqlCommand(str2, con);
        //SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
        //DataTable dtt2 = new DataTable();
        //adp2.Fill(dtt2);

        //if (dtt2.Rows.Count > 0)
        //{
        if (ddlproject.SelectedIndex != -1)
        {
            string str = "TaskAllocationMaster.*,TaskMaster.TaskName as TaskMasterName,TaskMaster.Rate,EmployeeMaster.EmployeeName as Employee FROM TaskAllocationMaster INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID left outer join StatusMaster on StatusMaster.StatusId=TaskMaster.Status inner join ProjectMaster on ProjectMaster.ProjectId=TaskMaster.ProjectId where ProjectMaster.ProjectId='" + ddlproject.SelectedValue + "'";

            string str3 = "select Count(TaskAllocationMaster.TaskAllocationId) as ci FROM TaskAllocationMaster INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID left outer join StatusMaster on StatusMaster.StatusId=TaskMaster.Status inner join ProjectMaster on ProjectMaster.ProjectId=TaskMaster.ProjectId where ProjectMaster.ProjectId='" + ddlproject.SelectedValue + "'";

            GridView6.VirtualItemCount = GetRowCount2(str3);

            string sortExpression = " TaskAllocationMaster.TaskAllocationDate";

            if (Convert.ToInt32(ViewState["count"]) > 0)
            {
                GridView6.DataSource = GetDataPage2(GridView6.PageIndex, GridView6.PageSize, sortExpression, str);
                GridView6.DataBind();

                Panel8.Visible = true;
                lblltgdocs.Visible = true;
            }
            else
            {
                GridView6.DataSource = null;
                GridView6.DataBind();

                Panel8.Visible = false;
                lblltgdocs.Visible = false;
            }
        }
        else
        {
            GridView6.DataSource = null;
            GridView6.DataBind();

            Panel8.Visible = false;
            lblltgdocs.Visible = false;
        }
        //}
        //else
        //{
        //    GridView6.DataSource = null;
        //    GridView6.DataBind();

        //    Panel8.Visible = false;
        //    Label14.Visible = false;
        //}
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillevaluation();
    }
    protected void GridView6_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView6.PageIndex = e.NewPageIndex;
        fillemptask();
    }
}
