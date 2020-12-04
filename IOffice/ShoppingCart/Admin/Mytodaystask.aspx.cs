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
using System.Drawing;
using System.ServiceProcess;
using System.Diagnostics;
using System.Windows;
using System.Data.SqlClient;

public partial class Admin_Admin_files_FrmEmployeeloginTask : System.Web.UI.Page
{
    static DataTable dttemp;
    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    string wweid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmployeeId"] == "")
        {
            Response.Redirect("~/Login.aspx");
        }

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";

            //txtestartdate.Text = System.DateTime.Now.ToShortDateString();
            //txteenddate.Text = System.DateTime.Now.ToShortDateString();

            gethours();

            BindGrid();


        }
    }

    protected void gethours()
    {
        string strrr = "select TaskId from TaskAllocationMaster where EmployeeId='" + Session["EmployeeId"].ToString() + "'  and TaskAllocationMaster.TaskAllocationDate='" + DateTime.Now.ToShortDateString() + "'";
        SqlDataAdapter das = new SqlDataAdapter(strrr, con);
        DataTable dts = new DataTable();
        das.Fill(dts);

        if (dts.Rows.Count > 0)
        {
            SqlDataAdapter da = new SqlDataAdapter("select EmpRate from TaskAllocationMaster where EmployeeId='" + Session["EmployeeId"].ToString() + "' and TaskId='" + Convert.ToString(dts.Rows[0]["TaskId"]) + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(dt.Rows[0]["EmpRate"]) != "")
                {
                    lblhourrate.Text = dt.Rows[0]["EmpRate"].ToString();
                }
                else
                {
                    lblhourrate.Text = "0";
                }
            }
        }
        else
        {
            lblhourrate.Text = "0";
        }
    }

    protected void BindGrid()
    {
        string str2 = " Select EmployeeMaster.Whid, EmployeeMaster.EmployeeName,WareHouseMaster.Name as Wname from WareHouseMaster inner join EmployeeMaster on EmployeeMaster.Whid=WareHouseMaster.WareHouseId where  EmployeeMaster.EmployeeMasterID= '" + Session["EmployeeId"] + "'  ";

        SqlCommand cmd2 = new SqlCommand(str2, con);
        SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
        DataTable dtt2 = new DataTable();
        adp2.Fill(dtt2);
        if (dtt2.Rows.Count > 0)
        {
            lblcompanyname.Text = Session["Comid"].ToString();
            lblBusiness.Text = Convert.ToString(dtt2.Rows[0]["Wname"]);
            lblemployeenameprint.Text = "Employee Name : " + " " + Convert.ToString(dtt2.Rows[0]["EmployeeName"]);

            ViewState["idd"] = Convert.ToString(dtt2.Rows[0]["Whid"]);


            string str = "SELECT TaskAllocationMaster.*,TaskMaster.TaskName as TaskMasterName,TaskMaster.Rate,TaskMaster.Status ,StatusMaster.StatusName, EmployeeMaster.EmployeeMasterID as DocumentId FROM TaskAllocationMaster INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId  INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID left outer join StatusMaster on StatusMaster.StatusId=TaskMaster.Status where TaskMaster.Whid='" + Convert.ToString(dtt2.Rows[0]["Whid"]) + "' and  TaskAllocationMaster.EmployeeId='" + Session["EmployeeId"] + "'";



            //if (ddlemployee.SelectedIndex >-1)
            //{
            //    str += " and TaskAllocationMaster.EmployeeId='" + ddlemployee.SelectedValue + "' ";
            //}

            str += " and TaskAllocationMaster.TaskAllocationDate='" + DateTime.Now.ToShortDateString() + "' ";

            //str += " and TaskAllocationMaster.TaskAllocationDate between '" + txtestartdate.Text + "' and '" + txteenddate.Text + "'";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dtt = new DataTable();
            adp.Fill(dtt);

            //if (dtt.Rows.Count > 0)
            //{
            //    ViewState["idd1"] = Convert.ToString(dtt.Rows[0]["TaskId"]);
            //}

            for (int rowindex = 0; rowindex < dtt.Rows.Count; rowindex++)
            {


                DataTable dtcrNew11 = clsDocument.SelectOfficeManagerDocumentswithtaskmasterid(Convert.ToString(dtt.Rows[rowindex]["TaskId"]));

                dtt.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];


            }

            DataView myDataView = new DataView();
            myDataView = dtt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            grid.DataSource = myDataView;
            grid.DataBind();

        }
    }

    // ENTER ROW IN EDIT MODE
    protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        statuslable.Text = "";
        grid.EditIndex = e.NewEditIndex;
        int dk1 = Convert.ToInt32(grid.DataKeys[e.NewEditIndex].Value);

        ViewState["idd1"] = dk1;

        BindGrid();



        DropDownList ddlstatusfill = (DropDownList)grid.Rows[grid.EditIndex].FindControl("ddlstatusfill");
        Label lblstatusid123 = (Label)grid.Rows[grid.EditIndex].FindControl("lblstatusid123");

        string strmethod = "SELECT [StatusId],[StatusName]  FROM [StatusMaster] inner join StatusCategory on  StatusCategory.StatusCategoryMasterId=StatusMaster.StatusCategoryMasterId where StatusCategory.StatusCategoryMasterId ='170'";
        SqlCommand cmdallocate = new SqlCommand(strmethod, con);
        SqlDataAdapter adpallocate = new SqlDataAdapter(cmdallocate);
        DataTable dtallocate = new DataTable();
        adpallocate.Fill(dtallocate);
        if (dtallocate.Rows.Count > 0)
        {
            ddlstatusfill.DataSource = dtallocate;
            ddlstatusfill.DataTextField = "statusname";
            ddlstatusfill.DataValueField = "statusid";
            ddlstatusfill.DataBind();
        }

        ddlstatusfill.SelectedIndex = ddlstatusfill.Items.IndexOf(ddlstatusfill.Items.FindByValue(lblstatusid123.Text.ToString()));




    }

    // CANCEL EDIT MODE
    protected void grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        statuslable.Text = "";
        // Cancel edit mode
        grid.EditIndex = -1;
        // Reload the grid
        BindGrid();
    }

    // UPDATE DATA IN BRANDMASTER TABLE
    protected void grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        string id = grid.DataKeys[e.RowIndex].Value.ToString();

        Label lblbudgetedminute123 = (Label)grid.Rows[grid.EditIndex].FindControl("lblbudgetedminute123");

        TextBox txttaskreport = (TextBox)grid.Rows[grid.EditIndex].FindControl("txttaskreport");

        TextBox txtactualunit123 = (TextBox)grid.Rows[grid.EditIndex].FindControl("txtactualunit123");
        DropDownList ddlstatusfill = (DropDownList)grid.Rows[grid.EditIndex].FindControl("ddlstatusfill");

        int excess = Convert.ToInt32(lblbudgetedminute123.Text) - Convert.ToInt32(txtactualunit123.Text);

        Label lblactempcost = (Label)grid.Rows[grid.EditIndex].FindControl("lblactempcost");

        double TotalMinutes132 = Convert.ToDouble(txtactualunit123.Text);
        double FinalHours = 0;

        FinalHours = (TotalMinutes132 / 60);
        double ddd = Convert.ToDouble(lblhourrate.Text);
        double finalcost = FinalHours * ddd;
        // lblactempcost.Text = finalcost.ToString("###,###.##");
        lblactempcost.Text = String.Format("{0:n}", Convert.ToDecimal(finalcost));


        string str = "update TaskAllocationMaster set TaskReport='" + txttaskreport.Text + "',UnitsUsed='" + txtactualunit123.Text + "',ActualRate='" + lblactempcost.Text + "' where TaskId='" + id + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }

        cmd.ExecuteNonQuery();
        con.Close();


        string strupdate = "update TaskMaster set Status='" + ddlstatusfill.SelectedValue + "' where TaskId='" + id + "' ";
        SqlCommand cmdupdate = new SqlCommand(strupdate, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }

        cmdupdate.ExecuteNonQuery();
        con.Close();

        statuslable.Visible = true;
        statuslable.Text = "Record updated successfully";

        // Execute the update command

        grid.EditIndex = -1;

        BindGrid();


    }


    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();

    }



    protected void txteenddate_TextChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnprintableversion_Click(object sender, EventArgs e)
    {
        if (btnprintableversion.Text == "Printable Version")
        {
            statuslable.Text = "";

            btnprintableversion.Text = "Hide Printable Version";
            Button7.Visible = true;

            if (grid.Columns[9].Visible == true)
            {
                ViewState["viewm"] = "tt";
                grid.Columns[9].Visible = false;
            }
            if (grid.Columns[10].Visible == true)
            {
                ViewState["viewm1"] = "tt";
                grid.Columns[10].Visible = false;
            }
            if (grid.Columns[11].Visible == true)
            {
                ViewState["viewm2"] = "tt";
                grid.Columns[11].Visible = false;
            }
        }
        else
        {
            btnprintableversion.Text = "Printable Version";
            Button7.Visible = false;

            if (ViewState["viewm"] != null)
            {
                grid.Columns[9].Visible = true;
            }
            if (ViewState["viewm1"] != null)
            {
                grid.Columns[10].Visible = true;
            }
            if (ViewState["viewm2"] != null)
            {
                grid.Columns[11].Visible = true;
            }
        }
    }
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }
    protected void grid_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        BindGrid();
    }
    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Send")
        {
            ViewState["TaskId"] = Convert.ToInt32(e.CommandArgument);
            DataTable dtcrNew11 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["TaskId"]), 9);
            GridView1.DataSource = dtcrNew11;
            GridView1.DataBind();
            ModalPopupExtenderAddnew.Show();
        }
        if (e.CommandName == "Add")
        {
            string te = "AddDocMaster.aspx?takid=" + ViewState["idd1"].ToString() + "&storeid=" + ViewState["idd"].ToString() + "";
            // objm=" + ViewState["idd1"].ToString() + "&storeid=" +  + "";

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtenderAddnew.Hide();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            DataTable dt = new DataTable();
            dt = SelectDoucmentMasterByID(Convert.ToInt32(e.CommandArgument));

            string docname = dt.Rows[0]["DocumentName"].ToString();
            string filepath = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + docname);
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
                    //Response.ClearContent();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //Response.AddHeader("Content-Length", file.Length.ToString());
                    //Response.ContentType = ReturnExtension(file.Extension.ToLower());
                    //Response.TransmitFile(file.FullName);
                    //Response.End();
                    String temp = "http://license.busiwiz.com/ioffice/Account/" + Session["comid"] + "/UploadedDocuments//" + docname + "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('http://license.busiwiz.com/Account/" + Session["comid"] + "/UploadedDocuments/" + docname + "');", true);
              
                }
            }
        }
    }
    public DataTable SelectDoucmentMasterByID(int DocumentId)
    {

        SqlCommand cmd = new SqlCommand();
        dttemp = new DataTable();
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

    //protected void btngo_Click(object sender, EventArgs e)
    //{
    //    BindGrid();
    //}
}
