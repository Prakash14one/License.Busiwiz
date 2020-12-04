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
public partial class ShoppingCart_Admin_JobfunctionAllocation : System.Web.UI.Page
{

    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        lblmsg.Visible = false;

        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                ViewState["sortOrder"] = "";

                fillstore();

                ddlwarehouse_SelectedIndexChanged(sender, e);
            }


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
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;

    }



    protected void btngo_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        FillDept();
        jobcate();
        fillGrid();
    }
    protected void jobcate()
    {

        ddljobcate.Items.Clear();
        DataTable ds = select("Select Id,[CategoryName] from [JobFunctionCategory]  where Whid='" + ddlwarehouse.SelectedValue + "' and [Status]='1' order by CategoryName");
        if (ds.Rows.Count > 0)
        {
            ddljobcate.DataSource = ds;
            ddljobcate.DataTextField = "CategoryName";
            ddljobcate.DataValueField = "Id";
            ddljobcate.DataBind();
        }
        ddljobcate.Items.Insert(0, "-Select-");
        ddljobcate.Items[0].Value = "0";
        EventArgs e = new EventArgs();
        object sender = new object();
        ddljobcate_SelectedIndexChanged(sender, e);

    }
    protected void fillstore()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = ds;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataBind();


            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
        }
    }

    protected void FillDept()
    {


        DataTable ds = select("Select DesignationMaster.DesignationMasterId,DepartmentmasterMNC.Departmentname+':'+DesignationMaster.DesignationName as desname from DesignationMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=DesignationMaster.DeptId where DepartmentmasterMNC.Whid='" + ddlwarehouse.SelectedValue + "' order by desname");
        if (ds.Rows.Count > 0)
        {
            ddldesi.DataSource = ds;
            ddldesi.DataTextField = "desname";
            ddldesi.DataValueField = "DesignationMasterId";
            ddldesi.DataBind();
        }


    }





    protected void fillGrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        string pera = "";
        if (ddljobcate.SelectedIndex > 0)
        {
            pera = " and Jobfunction.JobfunctioncategoryId='" + ddljobcate.SelectedValue + "'";
        }
        else if (ddljobsubcat.SelectedIndex > 0)
        {
            pera += " and Jobfunction.JobfunctionsubcategoryId='" + ddljobsubcat.SelectedValue + "'";
        }


        DataTable dtp = select("Select Jobfunction.Id,Jobfunction.Jobfunctiontitle,Left(JobFunctionCategory.CategoryName,30) AS CategoryName,LEFT(JobFunctionSubCategory.SubCategoryName,30) AS SubCategoryName from Jobfunction inner join [JobFunctionCategory] on [JobFunctionCategory].Id=Jobfunction.JobfunctioncategoryId inner join [JobFunctionSubCategory] on [JobFunctionSubCategory].Id=Jobfunction.JobfunctionsubcategoryId where Jobfunction.Status='1' and Jobfunction.Whid='" + ddlwarehouse.SelectedValue + "' and Jobfunction.DesignationId='" + ddldesi.SelectedValue + "'" + pera);
        if (dtp.Rows.Count > 0)
        {
            txtdatetime.Visible = true;
            lblg.Visible = true;
            txtdatetime.Text = DateTime.Now.ToString();
            btnedit.Visible = true;
            GridView1.DataSource = dtp;
            GridView1.DataBind();

        }
        else
        {
            txtdatetime.Visible = false;
            lblg.Visible = false;
        }



        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["data"]) == "")
        {
            dt = CreateDatatable();
        }
        else
        {
            dt = (DataTable)ViewState["data"];
        }
        foreach (GridViewRow dtr in GridView1.Rows)
        {
            dt.Rows.Clear();
            string emplevel2 = " and EmployeeMaster.EmployeeMasterId Not In(";
            string emplevel3 = " and EmployeeMaster.EmployeeMasterId Not In(";
            String level2val = "";
            String level3val = "";
            string jobid = GridView1.DataKeys[dtr.RowIndex].Value.ToString();

            DataTable dtlevel1 = select("Select distinct JobFunctionEmployeeResponsibilityMaster.Active, Activestartdatetime, BatchMaster.Id as BatchId, [EmployeeMaster].EmployeeMasterID as EmpId,[EmployeeMaster].EmployeeName as EmployeeName,Case When(JobFunctionEmployeeResponsibilityMaster.Id IS NULL) then'0' else JobFunctionEmployeeResponsibilityMaster.Id end as Id,Case When(JobFunctionEmployeeResponsibilityMaster.EmpId IS NULL) then'0' else '1' end as chk from Jobfunction inner join JobFunctionEmployeeResponsibilityMaster on JobFunctionEmployeeResponsibilityMaster.JobfunctionId=Jobfunction.Id and JobFunctionEmployeeResponsibilityMaster.ResponsibilitylevelId='1' and JobFunctionEmployeeResponsibilityMaster.Active='1' AND  Jobfunction.Id='" + jobid + "' " +
           " Right join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterId=JobFunctionEmployeeResponsibilityMaster.EmpId " +
           "inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join BatchMaster on BatchMaster.ID=EmployeeBatchMaster.Batchmasterid where EmployeeMaster.designationMasterId='" + ddldesi.SelectedValue + "' order by JobFunctionEmployeeResponsibilityMaster.Active DEsc");

            if (dtlevel1.Rows.Count > 0)
            {
                txtdatetime.Text = Convert.ToString(dtlevel1.Rows[0]["Activestartdatetime"]);
                foreach (DataRow gr1 in dtlevel1.Rows)
                {
                    DataRow Drow = dt.NewRow();

                    Drow["EmployeeName"] = Convert.ToString(gr1["EmployeeName"]);
                    Drow["No"] = dtr.RowIndex.ToString();
                    Drow["BatchId"] = Convert.ToString(gr1["BatchId"]);

                    Drow["EmpId"] = Convert.ToString(gr1["EmpId"]);
                    Drow["Id"] = Convert.ToString(gr1["Id"]);
                    if (Convert.ToString(gr1["chk"]) == "1")
                    {

                        Drow["chk"] = Convert.ToBoolean("True");
                        level2val += "'" + Convert.ToString(gr1["EmpId"]) + "',";
                    }

                    else
                    {
                        Drow["chk"] = Convert.ToBoolean("False");
                    }


                    dt.Rows.Add(Drow);
                }

                GridView grdlevel = (GridView)dtr.FindControl("grdf1");
                grdlevel.DataSource = dt;
                grdlevel.DataBind();
            }
            if (level2val.Length > 0)
            {
                level3val = level2val;
                level2val = level2val.Remove(level2val.Length - 1, 1);
                emplevel2 = emplevel2 + level2val + ")";
            }
            else
            {
                emplevel2 = "";
            }
            dt.Rows.Clear();
            DataTable dtlevel2 = select("Select distinct JobFunctionEmployeeResponsibilityMaster.Active, Activestartdatetime, BatchMaster.Id as BatchId, [EmployeeMaster].EmployeeMasterID as EmpId,[EmployeeMaster].EmployeeName as EmployeeName,Case When(JobFunctionEmployeeResponsibilityMaster.Id IS NULL) then'0' else JobFunctionEmployeeResponsibilityMaster.Id end as Id,Case When(JobFunctionEmployeeResponsibilityMaster.EmpId IS NULL) then'0' else '1' end as chk from Jobfunction inner join JobFunctionEmployeeResponsibilityMaster on JobFunctionEmployeeResponsibilityMaster.JobfunctionId=Jobfunction.Id and JobFunctionEmployeeResponsibilityMaster.ResponsibilitylevelId='2' and JobFunctionEmployeeResponsibilityMaster.Active='1' and Jobfunction.Id='" + jobid + "' " +
          " Right join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterId=JobFunctionEmployeeResponsibilityMaster.EmpId " +
          "inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join BatchMaster on BatchMaster.ID=EmployeeBatchMaster.Batchmasterid where  EmployeeMaster.designationMasterId='" + ddldesi.SelectedValue + "' " + emplevel2 + " order by JobFunctionEmployeeResponsibilityMaster.Active DEsc");
            if (dtlevel2.Rows.Count > 0)
            {
                txtdatetime.Text = Convert.ToString(dtlevel1.Rows[0]["Activestartdatetime"]);
                foreach (DataRow gr1 in dtlevel2.Rows)
                {
                    DataRow Drow = dt.NewRow();

                    Drow["EmployeeName"] = Convert.ToString(gr1["EmployeeName"]);
                    Drow["No"] = dtr.RowIndex.ToString();
                    Drow["BatchId"] = Convert.ToString(gr1["BatchId"]);

                    Drow["EmpId"] = Convert.ToString(gr1["EmpId"]);
                    Drow["Id"] = Convert.ToString(gr1["Id"]);
                    if (Convert.ToString(gr1["chk"]) == "1")
                    {

                        Drow["chk"] = Convert.ToBoolean("True");
                        level3val += "'" + Convert.ToString(gr1["EmpId"]) + "',";
                    }

                    else
                    {
                        Drow["chk"] = Convert.ToBoolean("False");
                    }


                    dt.Rows.Add(Drow);
                }

                GridView grdlevel = (GridView)dtr.FindControl("grdf2");
                grdlevel.DataSource = dt;
                grdlevel.DataBind();
            }
            if (level3val.Length > 0)
            {
                level3val = level3val.Remove(level3val.Length - 1, 1);
                emplevel3 = emplevel3 + level3val + ")";
            }
            else
            {
                emplevel3 = "";
            }
            dt.Rows.Clear();
            DataTable dtlevel3 = select("Select distinct JobFunctionEmployeeResponsibilityMaster.Active, Activestartdatetime, BatchMaster.Id as BatchId, [EmployeeMaster].EmployeeMasterID as EmpId,[EmployeeMaster].EmployeeName as EmployeeName,Case When(JobFunctionEmployeeResponsibilityMaster.Id IS NULL) then'0' else JobFunctionEmployeeResponsibilityMaster.Id end as Id,Case When(JobFunctionEmployeeResponsibilityMaster.EmpId IS NULL) then'0' else '1' end as chk from Jobfunction inner join JobFunctionEmployeeResponsibilityMaster on JobFunctionEmployeeResponsibilityMaster.JobfunctionId=Jobfunction.Id and JobFunctionEmployeeResponsibilityMaster.ResponsibilitylevelId='3' and JobFunctionEmployeeResponsibilityMaster.Active='1' and Jobfunction.Id='" + jobid + "'" +
         " Right join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterId=JobFunctionEmployeeResponsibilityMaster.EmpId " +
         "inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join BatchMaster on BatchMaster.ID=EmployeeBatchMaster.Batchmasterid where   EmployeeMaster.designationMasterId='" + ddldesi.SelectedValue + "'  " + emplevel3 + " order by JobFunctionEmployeeResponsibilityMaster.Active DEsc");
            if (dtlevel3.Rows.Count > 0)
            {
                txtdatetime.Text = Convert.ToString(dtlevel1.Rows[0]["Activestartdatetime"]);
                foreach (DataRow gr1 in dtlevel3.Rows)
                {
                    DataRow Drow = dt.NewRow();

                    Drow["EmployeeName"] = Convert.ToString(gr1["EmployeeName"]);
                    Drow["No"] = dtr.RowIndex.ToString();
                    Drow["BatchId"] = Convert.ToString(gr1["BatchId"]);

                    Drow["EmpId"] = Convert.ToString(gr1["EmpId"]);
                    Drow["Id"] = Convert.ToString(gr1["Id"]);
                    if (Convert.ToString(gr1["chk"]) == "1")
                    {

                        Drow["chk"] = Convert.ToBoolean("True");

                    }

                    else
                    {
                        Drow["chk"] = Convert.ToBoolean("False");
                    }


                    dt.Rows.Add(Drow);
                }


                GridView grdlevel = (GridView)dtr.FindControl("grdf3");
                grdlevel.DataSource = dt;
                grdlevel.DataBind();
            }
            if (txtdatetime.Text.Length <= 0)
            {
                txtdatetime.Text = DateTime.Now.ToString();
            }
        }


    }
    protected void chkSendMail_CheckedChanged(object sender, EventArgs e)
    {
        string emplevel2 = " and EmployeeMaster.EmployeeMasterId Not In(";
        string emplevel3 = " and EmployeeMaster.EmployeeMasterId Not In(";
        String level2val = "";
        String level3val = "";
        GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        CheckBox chkprocess = (CheckBox)sender;
        GridViewRow RV = (GridViewRow)chkprocess.NamingContainer;
        GridView GV = (GridView)RV.NamingContainer;
        Label lnlno = (Label)RV.FindControl("lnlno");
        int rid = RV.RowIndex;
        GridView empg1 = (GridView)GridView1.Rows[Convert.ToInt32(lnlno.Text)].FindControl("grdf1");

        string jobid = GridView1.DataKeys[Convert.ToInt32(lnlno.Text)].Value.ToString();
        foreach (GridViewRow grs in empg1.Rows)
        {
            CheckBox chk = (CheckBox)grs.FindControl("chkf");
            Label lblempid = (Label)grs.FindControl("lblempid");

            if (chk.Checked == true)
            {
                level2val += "'" + Convert.ToString(lblempid.Text) + "',";


            }
        }
        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["data"]) == "")
        {
            dt = CreateDatatable();
        }
        else
        {
            dt = (DataTable)ViewState["data"];
        }
        GridView empg2 = (GridView)GridView1.Rows[Convert.ToInt32(lnlno.Text)].FindControl("grdf2");
        if (level2val.Length > 0)
        {
            level3val = level2val;
            level2val = level2val.Remove(level2val.Length - 1, 1);
            emplevel2 = emplevel2 + level2val + ")";
        }
        else
        {
            emplevel2 = "";
        }

        DataTable dtlevel2 = select("Select distinct JobFunctionEmployeeResponsibilityMaster.Active, BatchMaster.Id as BatchId, [EmployeeMaster].EmployeeMasterID as EmpId,[EmployeeMaster].EmployeeName as EmployeeName,Case When(JobFunctionEmployeeResponsibilityMaster.Id IS NULL) then'0' else JobFunctionEmployeeResponsibilityMaster.Id end as Id,Case When(JobFunctionEmployeeResponsibilityMaster.EmpId IS NULL) then'0' else '1' end as chk from Jobfunction inner join JobFunctionEmployeeResponsibilityMaster on JobFunctionEmployeeResponsibilityMaster.JobfunctionId=Jobfunction.Id and JobFunctionEmployeeResponsibilityMaster.ResponsibilitylevelId='2' and JobFunctionEmployeeResponsibilityMaster.Active='1' and Jobfunction.Id='" + jobid + "' " +
      " Right join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterId=JobFunctionEmployeeResponsibilityMaster.EmpId " +
      "inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join BatchMaster on BatchMaster.ID=EmployeeBatchMaster.Batchmasterid where  EmployeeMaster.designationMasterId='" + ddldesi.SelectedValue + "' " + emplevel2 + " order by JobFunctionEmployeeResponsibilityMaster.Active DEsc");


        if (dtlevel2.Rows.Count > 0)
        {
            foreach (DataRow gr1 in dtlevel2.Rows)
            {
                DataRow Drow = dt.NewRow();

                Drow["EmployeeName"] = Convert.ToString(gr1["EmployeeName"]);
                Drow["No"] = lnlno.Text;
                Drow["BatchId"] = Convert.ToString(gr1["BatchId"]);

                Drow["EmpId"] = Convert.ToString(gr1["EmpId"]);
                Drow["Id"] = Convert.ToString(gr1["Id"]);
                if (Convert.ToString(gr1["chk"]) == "1")
                {

                    Drow["chk"] = Convert.ToBoolean("True");
                    level3val += "'" + Convert.ToString(gr1["EmpId"]) + "',";
                }

                else
                {
                    Drow["chk"] = Convert.ToBoolean("False");
                }


                dt.Rows.Add(Drow);
            }



            empg2.DataSource = dt;
            empg2.DataBind();

        }
        else
        {
            empg2.DataSource = null;
            empg2.DataBind();
        }

        dt.Rows.Clear();

        if (level3val.Length > 0)
        {
            level3val = level3val.Remove(level3val.Length - 1, 1);
            emplevel3 = emplevel3 + level3val + ")";
        }
        else
        {
            emplevel3 = "";
        }
        GridView empg3 = (GridView)GridView1.Rows[Convert.ToInt32(lnlno.Text)].FindControl("grdf3");

        DataTable dtlevel3 = select("Select distinct JobFunctionEmployeeResponsibilityMaster.Active, BatchMaster.Id as BatchId, [EmployeeMaster].EmployeeMasterID as EmpId,[EmployeeMaster].EmployeeName as EmployeeName,Case When(JobFunctionEmployeeResponsibilityMaster.Id IS NULL) then'0' else JobFunctionEmployeeResponsibilityMaster.Id end as Id,Case When(JobFunctionEmployeeResponsibilityMaster.EmpId IS NULL) then'0' else '1' end as chk from Jobfunction inner join JobFunctionEmployeeResponsibilityMaster on JobFunctionEmployeeResponsibilityMaster.JobfunctionId=Jobfunction.Id and JobFunctionEmployeeResponsibilityMaster.ResponsibilitylevelId='3' and JobFunctionEmployeeResponsibilityMaster.Active='1' and Jobfunction.Id='" + jobid + "' " +
  " Right join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterId=JobFunctionEmployeeResponsibilityMaster.EmpId " +
  "inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join BatchMaster on BatchMaster.ID=EmployeeBatchMaster.Batchmasterid where  EmployeeMaster.designationMasterId='" + ddldesi.SelectedValue + "' " + emplevel3 + " order by JobFunctionEmployeeResponsibilityMaster.Active DEsc");


        if (dtlevel3.Rows.Count > 0)
        {
            foreach (DataRow gr1 in dtlevel3.Rows)
            {
                DataRow Drow = dt.NewRow();

                Drow["EmployeeName"] = Convert.ToString(gr1["EmployeeName"]);
                Drow["No"] = lnlno.Text;
                Drow["BatchId"] = Convert.ToString(gr1["BatchId"]);

                Drow["EmpId"] = Convert.ToString(gr1["EmpId"]);
                Drow["Id"] = Convert.ToString(gr1["Id"]);
                if (Convert.ToString(gr1["chk"]) == "1")
                {

                    Drow["chk"] = Convert.ToBoolean("True");

                }

                else
                {
                    Drow["chk"] = Convert.ToBoolean("False");
                }


                dt.Rows.Add(Drow);
            }



            empg3.DataSource = dt;
            empg3.DataBind();
        }
        else
        {
            empg3.DataSource = null;
            empg3.DataBind();
        }


    }
    protected void chkSendMail3_CheckedChanged(object sender, EventArgs e)
    {

        string emplevel3 = " and EmployeeMaster.EmployeeMasterId Not In(";

        String level3val = "";
        GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        CheckBox chkprocess = (CheckBox)sender;
        GridViewRow RV = (GridViewRow)chkprocess.NamingContainer;
        GridView GV = (GridView)RV.NamingContainer;
        Label lnlno = (Label)RV.FindControl("lnlno");
        GridView empg2 = (GridView)GridView1.Rows[Convert.ToInt32(lnlno.Text)].FindControl("grdf2");

        string jobid = GridView1.DataKeys[Convert.ToInt32(lnlno.Text)].Value.ToString();


        GridView empg1 = (GridView)GridView1.Rows[Convert.ToInt32(lnlno.Text)].FindControl("grdf1");

        foreach (GridViewRow grs in empg1.Rows)
        {
            CheckBox chk = (CheckBox)grs.FindControl("chkf");
            Label lblempid = (Label)grs.FindControl("lblempid");

            if (chk.Checked == true)
            {
                level3val += "'" + Convert.ToString(lblempid.Text) + "',";


            }
        }
        foreach (GridViewRow grs in empg2.Rows)
        {
            CheckBox chk = (CheckBox)grs.FindControl("chkf");
            Label lblempid = (Label)grs.FindControl("lblempid");

            if (chk.Checked == true)
            {
                level3val += "'" + Convert.ToString(lblempid.Text) + "',";


            }
        }
        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["data"]) == "")
        {
            dt = CreateDatatable();
        }
        else
        {
            dt = (DataTable)ViewState["data"];
        }

        if (level3val.Length > 0)
        {

            level3val = level3val.Remove(level3val.Length - 1, 1);
            emplevel3 = emplevel3 + level3val + ")";
        }
        else
        {
            emplevel3 = "";
        }


        GridView empg3 = (GridView)GridView1.Rows[Convert.ToInt32(lnlno.Text)].FindControl("grdf3");

        DataTable dtlevel3 = select("Select distinct JobFunctionEmployeeResponsibilityMaster.Active, BatchMaster.Id as BatchId, [EmployeeMaster].EmployeeMasterID as EmpId,[EmployeeMaster].EmployeeName as EmployeeName,Case When(JobFunctionEmployeeResponsibilityMaster.Id IS NULL) then'0' else JobFunctionEmployeeResponsibilityMaster.Id end as Id,Case When(JobFunctionEmployeeResponsibilityMaster.EmpId IS NULL) then'0' else '1' end as chk from Jobfunction inner join JobFunctionEmployeeResponsibilityMaster on JobFunctionEmployeeResponsibilityMaster.JobfunctionId=Jobfunction.Id and JobFunctionEmployeeResponsibilityMaster.ResponsibilitylevelId='3' and JobFunctionEmployeeResponsibilityMaster.Active='1' and Jobfunction.Id='" + jobid + "' " +
  " Right join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterId=JobFunctionEmployeeResponsibilityMaster.EmpId " +
  "inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join BatchMaster on BatchMaster.ID=EmployeeBatchMaster.Batchmasterid where  EmployeeMaster.designationMasterId='" + ddldesi.SelectedValue + "' " + emplevel3 + " order by JobFunctionEmployeeResponsibilityMaster.Active DEsc");


        if (dtlevel3.Rows.Count > 0)
        {
            foreach (DataRow gr1 in dtlevel3.Rows)
            {
                DataRow Drow = dt.NewRow();

                Drow["EmployeeName"] = Convert.ToString(gr1["EmployeeName"]);
                Drow["No"] = lnlno.Text;
                Drow["BatchId"] = Convert.ToString(gr1["BatchId"]);

                Drow["EmpId"] = Convert.ToString(gr1["EmpId"]);
                Drow["Id"] = Convert.ToString(gr1["Id"]);
                if (Convert.ToString(gr1["chk"]) == "1")
                {

                    Drow["chk"] = Convert.ToBoolean("True");

                }

                else
                {
                    Drow["chk"] = Convert.ToBoolean("False");
                }


                dt.Rows.Add(Drow);
            }



            empg3.DataSource = dt;
            empg3.DataBind();
        }
        else
        {
            empg3.DataSource = null;
            empg3.DataBind();
        }

    }
    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "Id";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "BatchId";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "No";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "EmpId";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "EmployeeName";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;
        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.Boolean");
        Dcom5.ColumnName = "chk";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;


        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);

        return dt;
    }

    public DataTable itemdy()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "ID";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "Name";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        //DataColumn Dcom2 = new DataColumn();
        //Dcom2.DataType = System.Type.GetType("System.String");
        //Dcom2.ColumnName = "passGrade";
        //Dcom2.AllowDBNull = true;
        //Dcom2.Unique = false;
        //Dcom2.ReadOnly = false;
        //DataColumn Dcom3 = new DataColumn();
        //Dcom3.DataType = System.Type.GetType("System.String");
        //Dcom3.ColumnName = "spsubject";
        //Dcom3.AllowDBNull = true;
        //Dcom3.Unique = false;
        //Dcom3.ReadOnly = false;





        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        //dt.Columns.Add(Dcom2);
        //dt.Columns.Add(Dcom3);


        return dt;
    }



    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow drs in GridView1.Rows)
        {
            string jobid = GridView1.DataKeys[drs.RowIndex].Value.ToString();
            GridView grdf1 = (GridView)GridView1.Rows[drs.RowIndex].FindControl("grdf1");
            GridView grdf2 = (GridView)GridView1.Rows[drs.RowIndex].FindControl("grdf2");
            GridView grdf3 = (GridView)GridView1.Rows[drs.RowIndex].FindControl("grdf3");

            foreach (GridViewRow drp in grdf1.Rows)
            {
                string jobreqid = grdf1.DataKeys[drs.RowIndex].Value.ToString();
                Label lblempid = (Label)drp.FindControl("lblempid");
                CheckBox chkf = (CheckBox)drp.FindControl("chkf");
                if (chkf.Checked == true)
                {
                    String str1 = "";
                    if (jobreqid == "0")
                    {
                        str1 = "insert into [JobFunctionEmployeeResponsibilityMaster]([Whid],[EmpId],[JobfunctionId],[ResponsibilitylevelId] ,[Activestartdatetime],[Active])values" +
                     " ('" + ddlwarehouse.SelectedValue + "','" + lblempid.Text + "','" + jobid + "','1','" + txtdatetime.Text + "','1')";

                    }
                    else
                    {
                        str1 = "Update  [JobFunctionEmployeeResponsibilityMaster] Set [Activestartdatetime]='" + txtdatetime.Text + "',[Active]='1' where  Id='" + jobreqid + "'";

                    }
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();

                    }
                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    if (jobreqid != "0")
                    {
                        string str1 = "Update  [JobFunctionEmployeeResponsibilityMaster] Set [Activestartdatetime]='" + txtdatetime.Text + "',[Active]='0' where  Id='" + jobreqid + "'";
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();

                        }
                        SqlCommand cmd1 = new SqlCommand(str1, con);
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }

                }

            }
            foreach (GridViewRow drp in grdf2.Rows)
            {
                string jobreqid = grdf2.DataKeys[drs.RowIndex].Value.ToString();
                Label lblempid = (Label)drp.FindControl("lblempid");
                CheckBox chkf = (CheckBox)drp.FindControl("chkf");
                if (chkf.Checked == true)
                {
                    String str1 = "";
                    if (jobreqid == "0")
                    {
                        str1 = "insert into [JobFunctionEmployeeResponsibilityMaster]([Whid],[EmpId],[JobfunctionId],[ResponsibilitylevelId] ,[Activestartdatetime],[Active])values" +
                     " ('" + ddlwarehouse.SelectedValue + "','" + lblempid.Text + "','" + jobid + "','2','" + txtdatetime.Text + "','1')";

                    }
                    else
                    {
                        str1 = "Update  [JobFunctionEmployeeResponsibilityMaster] Set [Activestartdatetime]='" + txtdatetime.Text + "',[Active]='1' where  Id='" + jobreqid + "'";

                    }
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();

                    }
                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    if (jobreqid != "0")
                    {
                        string str1 = "Update  [JobFunctionEmployeeResponsibilityMaster] Set [Activestartdatetime]='" + txtdatetime.Text + "',[Active]='0' where  Id='" + jobreqid + "'";
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();

                        }
                        SqlCommand cmd1 = new SqlCommand(str1, con);
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }

                }

            }
            foreach (GridViewRow drp in grdf3.Rows)
            {
                string jobreqid = grdf3.DataKeys[drs.RowIndex].Value.ToString();
                Label lblempid = (Label)drp.FindControl("lblempid");
                CheckBox chkf = (CheckBox)drp.FindControl("chkf");
                if (chkf.Checked == true)
                {
                    String str1 = "";
                    if (jobreqid == "0")
                    {
                        str1 = "insert into [JobFunctionEmployeeResponsibilityMaster]([Whid],[EmpId],[JobfunctionId],[ResponsibilitylevelId] ,[Activestartdatetime],[Active])values" +
                     " ('" + ddlwarehouse.SelectedValue + "','" + lblempid.Text + "','" + jobid + "','3','" + txtdatetime.Text + "','1')";

                    }
                    else
                    {
                        str1 = "Update  [JobFunctionEmployeeResponsibilityMaster] Set [Activestartdatetime]='" + txtdatetime.Text + "',[Active]='1' where  Id='" + jobreqid + "'";

                    }
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();

                    }
                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    if (jobreqid != "0")
                    {
                        string str1 = "Update  [JobFunctionEmployeeResponsibilityMaster] Set [Activestartdatetime]='" + txtdatetime.Text + "',[Active]='0' where  Id='" + jobreqid + "'";
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();

                        }
                        SqlCommand cmd1 = new SqlCommand(str1, con);
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }

                }

            }
            lblmsg.Text = "Record inserted successfully";
            lblmsg.Visible = true;
            btnsubmit.Visible = false;
            btnedit.Visible = true;
            btncancel.Visible = false;

        }
        fillGrid();
        GridView1.Enabled = false;
        txtdatetime.Enabled = false;
    }




    protected void ddljobcate_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddljobsubcat.Items.Clear();
        if (ddljobcate.SelectedIndex > 0)
        {
            DataTable ds = select("Select Id,[SubCategoryName] from [JobFunctionSubCategory]  where JobCategoryId='" + ddljobcate.SelectedValue + "' and [Status]='1' order by SubCategoryName");
            if (ds.Rows.Count > 0)
            {
                ddljobsubcat.DataSource = ds;
                ddljobsubcat.DataTextField = "SubCategoryName";
                ddljobsubcat.DataValueField = "Id";
                ddljobsubcat.DataBind();
            }
        }
        ddljobsubcat.Items.Insert(0, "-Select-");
        ddljobsubcat.Items[0].Value = "0";
    }
    protected void ddljobsubcat_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void ddldesi_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        GridView1.Enabled = true;
        btnsubmit.Visible = true;
        btnedit.Visible = false;
        btncancel.Visible = true;
        GridView1.Enabled = true;
        txtdatetime.Enabled = true;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        GridView1.Enabled = false;
        btnsubmit.Visible = false;
        btnedit.Visible = true;
        btncancel.Visible = false;
        GridView1.Enabled = false;
        txtdatetime.Enabled = false;
        fillGrid();
    }
    protected void btncancel0_Click(object sender, EventArgs e)
    {

    }
}


