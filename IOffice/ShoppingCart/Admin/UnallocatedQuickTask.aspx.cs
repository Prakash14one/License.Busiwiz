using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Runtime.InteropServices;
using System.Net;
using System.Web.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class ShoppingCart_Admin_AddQuickTask : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
    SqlConnection con;
    string currentmonth = System.DateTime.Now.Month.ToString();
    string currentdate = System.DateTime.Now.ToShortDateString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == "")
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
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
            // txteenddate.Text = System.DateTime.Now.ToShortDateString();
            deletetaskdescription();
            deleteDocumentMasterTempforTask();

            fillstore();
            ddlStore_SelectedIndexChanged(sender, e);
            //RadioButtonList1_SelectedIndexChanged(sender, e);

            fillemployee();
            //fillallocatedhour1();

            //fillweeklygoal();
            //fillweeklygoal2();
            //fillweeklygoal3();
            //fillweeklygoal4();
            //fillweeklygoal5();
            //fillweeklygoal6();
            //fillweeklygoal7();
            //fillweeklygoal8();
            //fillweeklygoal9();
            //fillproject();
            //fillproject2();
            //fillproject3();
            //fillproject4();
            //fillproject5();
            //fillproject6();
            //fillproject7();
            //fillproject8();
            //fillproject9();

            txtbudgetminute.Text = "0";
            txtbudgetedminute2.Text = "0";
            txtbudgetedminute3.Text = "0";
            txtbudgetedminute4.Text = "0";
            txtbudgetedminute5.Text = "0";
            txtbudgetedminute6.Text = "0";
            txtbudgetedminute7.Text = "0";
            txtbudgetedminute8.Text = "0";
            txtbudgetedminute9.Text = "0";
        }

    }
    protected void fillstore()
    {
        ddlStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlStore.DataSource = ds;
        ddlStore.DataTextField = "Name";
        ddlStore.DataValueField = "WareHouseId";
        ddlStore.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }
    protected void fillemployee()
    {
        ddlnewtaskempname.Items.Clear();

        string str12 = "";

        if (ddltaskfor.SelectedValue == "1")
        {
            if (ddldepartment.SelectedIndex > -1)
            {
                str12 = "select DepartmentmasterMNC.Departmentname+':'+EmployeeMaster.EmployeeName as EmployeeName ,EmployeeMaster.EmployeeMasterID from EmployeeMaster inner join DepartmentmasterMNC on EmployeeMaster.DeptID=DepartmentmasterMNC.id where EmployeeMaster.Active='1' and EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' and EmployeeMaster.DeptID='" + ddldepartment.SelectedValue + "' ";
            }
            else
            {
                str12 = "select DepartmentmasterMNC.Departmentname+':'+EmployeeMaster.EmployeeName as EmployeeName ,EmployeeMaster.EmployeeMasterID from EmployeeMaster inner join DepartmentmasterMNC on EmployeeMaster.DeptID=DepartmentmasterMNC.id where EmployeeMaster.Active='1' and EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' ";
            }
        }
        else
        {
            str12 = "select DepartmentmasterMNC.Departmentname+':'+EmployeeMaster.EmployeeName as EmployeeName ,EmployeeMaster.EmployeeMasterID from EmployeeMaster inner join DepartmentmasterMNC on EmployeeMaster.DeptID=DepartmentmasterMNC.id where EmployeeMaster.Active='1' and EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' ";
        }

        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        if (ds12.Rows.Count > 0)
        {
            ddlnewtaskempname.DataSource = ds12;
            ddlnewtaskempname.DataTextField = "EmployeeName";
            ddlnewtaskempname.DataValueField = "EmployeeMasterID";
            ddlnewtaskempname.DataBind();
        }

        ddlnewtaskempname.Items.Insert(0, "-Select-");
        ddlnewtaskempname.Items[0].Value = "0";
    }
    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        projetbusiness();
        projet();

        filldepartment();
        ddldepartment_SelectedIndexChanged(sender, e);
        filldivision();
        ddldivision_SelectedIndexChanged(sender, e);
        fillemployee();
        ddlnewtaskempname_SelectedIndexChanged(sender, e);

        //fillweeklygoal();
        //fillweeklygoal2();
        //fillweeklygoal3();
        //fillweeklygoal4();
        //fillweeklygoal5();
        //fillweeklygoal6();
        //fillweeklygoal7();
        //fillweeklygoal8();
        //fillweeklygoal9();

        // fillproject();

    }

    //protected void fillproject()
    //{
    //    ddlproject.Items.Clear();
    //    string tempstring = "select (EmployeeMaster.EmployeeName+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=ProjectMaster.EmployeeID where  ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.EmployeeID='" + ddlnewtaskempname.SelectedValue + "' and Status='Pending' ";

    //    string order = " Order by EmployeeMaster.EmployeeName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {

    //        ddlproject.DataSource = ds12;
    //        ddlproject.DataTextField = "ProjectName";
    //        ddlproject.DataValueField = "ProjectId";
    //        ddlproject.DataBind();
    //    }
    //    ddlproject.Items.Insert(0, "-Select-");
    //    ddlproject.Items[0].Value = "0";
    //}

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtTaskName.Text.ToString() != "")
        {
            if (Panel5.Visible == true && txteenddate.Text != "")
            {
                fillallocatedhour1();
                fillbatchhour1();
                fillavailhour();
                fillrestriction1();
                minitecheck();

                if (Convert.ToInt32(ViewState["mastertemp"].ToString()) > 1439)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                    txteenddate.ForeColor = System.Drawing.Color.Red;
                    txteenddate.Focus();
                }
                else
                {
                    TimeSpan t3 = TimeSpan.Parse(lblavalablehours.Text);
                    TimeSpan t4 = TimeSpan.Parse(lblinserttime.Text);
                    if (Convert.ToInt32(ViewState["availableminute"].ToString()) < Convert.ToInt32(ViewState["mastertemp"].ToString()))
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                        txteenddate.ForeColor = System.Drawing.Color.Red;
                        txteenddate.Focus();
                        lblinserttime.Text = "00:00";
                    }
                    else
                    {
                        insertdata();
                        fillinsertdocument();
                        deletetaskdescription();
                        deleteDocumentMasterTempforTask();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully";
                        txtTaskName.Text = "";
                        txteenddate.Text = "";
                        txtbudgetminute.Text = "0";
                    }
                }
            }
            else
            {
                insertdata();
                fillinsertdocument();
                deletetaskdescription();
                deleteDocumentMasterTempforTask();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                txtTaskName.Text = "";
                txteenddate.Text = "";
                txtbudgetminute.Text = "0";
            }
        }
        if (txttaskname2.Text.ToString() != "")
        {
            if (Panel5.Visible == true && txteenddate2.Text != "")
            {
                fillallocatedhour2();
                fillbatchhour2();
                fillavailhour();
                fillrestriction2();
                minitecheck();

                if (Convert.ToInt32(ViewState["mastertemp"].ToString()) > 1439)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                    txteenddate2.ForeColor = System.Drawing.Color.Red;
                    txteenddate2.Focus();
                }
                else
                {
                    TimeSpan t3 = TimeSpan.Parse(lblavalablehours.Text);
                    TimeSpan t4 = TimeSpan.Parse(lblinserttime.Text);
                    if (Convert.ToInt32(ViewState["availableminute"].ToString()) < Convert.ToInt32(ViewState["mastertemp"].ToString()))
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                        txteenddate2.ForeColor = System.Drawing.Color.Red;
                        txteenddate2.Focus();
                        lblinserttime.Text = "00:00";
                    }
                    else
                    {
                        insertdata2();
                        fillinsertdocument2();
                        deletetaskdescription();
                        deleteDocumentMasterTempforTask();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully";
                        txttaskname2.Text = "";
                        txteenddate2.Text = "";
                        txtbudgetedminute2.Text = "0";
                    }
                }
            }
            else
            {
                insertdata2();
                fillinsertdocument2();
                deletetaskdescription();
                deleteDocumentMasterTempforTask();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                txttaskname2.Text = "";
                txteenddate2.Text = "";
                txtbudgetedminute2.Text = "0";
            }
        }
        if (txttaskname3.Text.ToString() != "")
        {
            if (Panel5.Visible == true && txteenddate3.Text != "")
            {
                fillallocatedhour3();
                fillbatchhour3();
                fillavailhour();
                fillrestriction3();
                minitecheck();

                if (Convert.ToInt32(ViewState["mastertemp"].ToString()) > 1439)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                    txteenddate3.ForeColor = System.Drawing.Color.Red;
                    txteenddate3.Focus();
                }
                else
                {
                    TimeSpan t3 = TimeSpan.Parse(lblavalablehours.Text);
                    TimeSpan t4 = TimeSpan.Parse(lblinserttime.Text);
                    if (Convert.ToInt32(ViewState["availableminute"].ToString()) < Convert.ToInt32(ViewState["mastertemp"].ToString()))
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                        txteenddate3.ForeColor = System.Drawing.Color.Red;
                        txteenddate3.Focus();
                        lblinserttime.Text = "00:00";
                    }
                    else
                    {
                        insertdata3();
                        fillinsertdocument3();
                        deletetaskdescription();
                        deleteDocumentMasterTempforTask();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully";
                        txttaskname3.Text = "";
                        txteenddate3.Text = "";
                        txtbudgetedminute3.Text = "0";
                    }
                }
            }
            else
            {
                insertdata3();
                fillinsertdocument3();
                deletetaskdescription();
                deleteDocumentMasterTempforTask();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                txttaskname3.Text = "";
                txteenddate3.Text = "";
                txtbudgetedminute3.Text = "0";
            }
        }
        if (txttaskname4.Text.ToString() != "")
        {
            if (Panel5.Visible == true && txteenddate4.Text != "")
            {
                fillallocatedhour4();
                fillbatchhour4();
                fillavailhour();
                fillrestriction4();
                minitecheck();

                if (Convert.ToInt32(ViewState["mastertemp"].ToString()) > 1439)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                    txteenddate4.ForeColor = System.Drawing.Color.Red;
                    txteenddate4.Focus();
                }
                else
                {
                    TimeSpan t3 = TimeSpan.Parse(lblavalablehours.Text);
                    TimeSpan t4 = TimeSpan.Parse(lblinserttime.Text);
                    if (Convert.ToInt32(ViewState["availableminute"].ToString()) < Convert.ToInt32(ViewState["mastertemp"].ToString()))
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                        txteenddate4.ForeColor = System.Drawing.Color.Red;
                        txteenddate4.Focus();
                        lblinserttime.Text = "00:00";
                    }
                    else
                    {
                        insertdata4();
                        fillinsertdocument4();
                        deletetaskdescription();
                        deleteDocumentMasterTempforTask();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully";
                        txttaskname4.Text = "";
                        txteenddate4.Text = "";
                        txtbudgetedminute4.Text = "0";
                    }
                }
            }
            else
            {
                insertdata4();
                fillinsertdocument4();
                deletetaskdescription();
                deleteDocumentMasterTempforTask();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                txttaskname4.Text = "";
                txteenddate4.Text = "";
                txtbudgetedminute4.Text = "0";
            }
        }
        if (txttaskname5.Text.ToString() != "")
        {
            if (Panel5.Visible == true && txteenddate5.Text != "")
            {
                fillallocatedhour5();
                fillbatchhour5();
                fillavailhour();
                fillrestriction5();
                minitecheck();

                if (Convert.ToInt32(ViewState["mastertemp"].ToString()) > 1439)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                    txteenddate5.ForeColor = System.Drawing.Color.Red;
                    txteenddate5.Focus();
                }
                else
                {
                    TimeSpan t3 = TimeSpan.Parse(lblavalablehours.Text);
                    TimeSpan t4 = TimeSpan.Parse(lblinserttime.Text);
                    if (Convert.ToInt32(ViewState["availableminute"].ToString()) < Convert.ToInt32(ViewState["mastertemp"].ToString()))
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                        txteenddate5.ForeColor = System.Drawing.Color.Red;
                        txteenddate5.Focus();
                        lblinserttime.Text = "00:00";
                    }
                    else
                    {
                        insertdata5();
                        fillinsertdocument5();
                        deletetaskdescription();
                        deleteDocumentMasterTempforTask();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully";
                        txttaskname5.Text = "";
                        txteenddate5.Text = "";
                        txtbudgetedminute5.Text = "0";
                    }
                }
            }
            else
            {
                insertdata5();
                fillinsertdocument5();
                deletetaskdescription();
                deleteDocumentMasterTempforTask();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                txttaskname5.Text = "";
                txteenddate5.Text = "";
                txtbudgetedminute5.Text = "0";
            }
        }
        if (txttaskname6.Text.ToString() != "")
        {
            if (Panel5.Visible == true && txteenddate6.Text != "")
            {
                fillallocatedhour6();
                fillbatchhour6();
                fillavailhour();
                fillrestriction6();
                minitecheck();

                if (Convert.ToInt32(ViewState["mastertemp"].ToString()) > 1439)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                    txteenddate6.ForeColor = System.Drawing.Color.Red;
                    txteenddate6.Focus();
                }
                else
                {
                    TimeSpan t3 = TimeSpan.Parse(lblavalablehours.Text);
                    TimeSpan t4 = TimeSpan.Parse(lblinserttime.Text);
                    if (Convert.ToInt32(ViewState["availableminute"].ToString()) < Convert.ToInt32(ViewState["mastertemp"].ToString()))
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                        txteenddate6.ForeColor = System.Drawing.Color.Red;
                        txteenddate6.Focus();
                        lblinserttime.Text = "00:00";
                    }
                    else
                    {
                        insertdata6();
                        fillinsertdocument6();
                        deletetaskdescription();
                        deleteDocumentMasterTempforTask();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully";
                        txttaskname6.Text = "";
                        txteenddate6.Text = "";
                        txtbudgetedminute6.Text = "0";
                    }
                }
            }
            else
            {
                insertdata6();
                fillinsertdocument6();
                deletetaskdescription();
                deleteDocumentMasterTempforTask();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                txttaskname6.Text = "";
                txteenddate6.Text = "";
                txtbudgetedminute6.Text = "0";
            }
        }
        if (txttaskname7.Text.ToString() != "")
        {
            if (Panel5.Visible == true && txteenddate7.Text != "")
            {
                fillallocatedhour7();
                fillbatchhour7();
                fillavailhour();
                fillrestriction7();
                minitecheck();

                if (Convert.ToInt32(ViewState["mastertemp"].ToString()) > 1439)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                    txteenddate7.ForeColor = System.Drawing.Color.Red;
                    txteenddate7.Focus();
                }
                else
                {
                    TimeSpan t3 = TimeSpan.Parse(lblavalablehours.Text);
                    TimeSpan t4 = TimeSpan.Parse(lblinserttime.Text);
                    if (Convert.ToInt32(ViewState["availableminute"].ToString()) < Convert.ToInt32(ViewState["mastertemp"].ToString()))
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                        txteenddate7.ForeColor = System.Drawing.Color.Red;
                        txteenddate7.Focus();
                        lblinserttime.Text = "00:00";
                    }
                    else
                    {
                        insertdata7();
                        fillinsertdocument7();
                        deletetaskdescription();
                        deleteDocumentMasterTempforTask();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully";
                        txttaskname7.Text = "";
                        txteenddate7.Text = "";
                        txtbudgetedminute7.Text = "0";
                    }
                }
            }
            else
            {
                insertdata7();
                fillinsertdocument7();
                deletetaskdescription();
                deleteDocumentMasterTempforTask();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                txttaskname7.Text = "";
                txteenddate7.Text = "";
                txtbudgetedminute7.Text = "0";
            }
        }
        if (txttaskname8.Text.ToString() != "")
        {
            if (Panel5.Visible == true && txteenddate8.Text != "")
            {
                fillallocatedhour8();
                fillbatchhour8();
                fillavailhour();
                fillrestriction8();
                minitecheck();

                if (Convert.ToInt32(ViewState["mastertemp"].ToString()) > 1439)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                    txteenddate8.ForeColor = System.Drawing.Color.Red;
                    txteenddate8.Focus();
                }
                else
                {
                    TimeSpan t3 = TimeSpan.Parse(lblavalablehours.Text);
                    TimeSpan t4 = TimeSpan.Parse(lblinserttime.Text);
                    if (Convert.ToInt32(ViewState["availableminute"].ToString()) < Convert.ToInt32(ViewState["mastertemp"].ToString()))
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                        txteenddate8.ForeColor = System.Drawing.Color.Red;
                        txteenddate8.Focus();
                        lblinserttime.Text = "00:00";
                    }
                    else
                    {
                        insertdata8();
                        fillinsertdocument8();
                        deletetaskdescription();
                        deleteDocumentMasterTempforTask();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully";
                        txttaskname8.Text = "";
                        txteenddate8.Text = "";
                        txtbudgetedminute8.Text = "0";
                    }
                }
            }
            else
            {
                insertdata8();
                fillinsertdocument8();
                deletetaskdescription();
                deleteDocumentMasterTempforTask();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                txttaskname8.Text = "";
                txteenddate8.Text = "";
                txtbudgetedminute8.Text = "0";
            }
        }
        if (txttaskname9.Text.ToString() != "")
        {
            if (Panel5.Visible == true && txteenddate9.Text != "")
            {
                fillallocatedhour9();
                fillbatchhour9();
                fillavailhour();
                fillrestriction9();
                minitecheck();

                if (Convert.ToInt32(ViewState["mastertemp"].ToString()) > 1439)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                    txteenddate9.ForeColor = System.Drawing.Color.Red;
                    txteenddate9.Focus();
                }
                else
                {
                    TimeSpan t3 = TimeSpan.Parse(lblavalablehours.Text);
                    TimeSpan t4 = TimeSpan.Parse(lblinserttime.Text);
                    if (Convert.ToInt32(ViewState["availableminute"].ToString()) < Convert.ToInt32(ViewState["mastertemp"].ToString()))
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Please change the date for the selected task as not enough time available for that employee.";
                        txteenddate9.ForeColor = System.Drawing.Color.Red;
                        txteenddate9.Focus();
                        lblinserttime.Text = "00:00";
                    }
                    else
                    {
                        insertdata9();
                        fillinsertdocument9();
                        deletetaskdescription();
                        deleteDocumentMasterTempforTask();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully";
                        txttaskname9.Text = "";
                        txteenddate9.Text = "";
                        txtbudgetedminute9.Text = "0";
                    }
                }
            }
            else
            {
                insertdata9();
                fillinsertdocument9();
                deletetaskdescription();
                deleteDocumentMasterTempforTask();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                txttaskname9.Text = "";
                txteenddate9.Text = "";
                txtbudgetedminute9.Text = "0";
            }
        }

    }

    protected void cleardata()
    {
        txtTaskName.Text = "";
        txttaskname2.Text = "";
        txttaskname3.Text = "";
        txttaskname4.Text = "";
        txttaskname5.Text = "";
        txttaskname6.Text = "";
        txttaskname7.Text = "";
        txttaskname8.Text = "";
        txttaskname9.Text = "";

        txtbudgetminute.Text = "0";
        txtbudgetedminute2.Text = "0";
        txtbudgetedminute3.Text = "0";
        txtbudgetedminute4.Text = "0";
        txtbudgetedminute5.Text = "0";
        txtbudgetedminute6.Text = "0";
        txtbudgetedminute7.Text = "0";
        txtbudgetedminute8.Text = "0";
        txtbudgetedminute9.Text = "0";

        //ddlweeklygoal.SelectedIndex = 0;
        //ddlweeklygoal2.SelectedIndex = 0;
        //ddlweeklygoal3.SelectedIndex = 0;
        //ddlweeklygoal4.SelectedIndex = 0;
        //ddlweeklygoal5.SelectedIndex = 0;
        //ddlweeklygoal6.SelectedIndex = 0;
        //ddlweeklygoal7.SelectedIndex = 0;
        //ddlweeklygoal8.SelectedIndex = 0;
        //ddlweeklygoal9.SelectedIndex = 0;

        //ddlproject.SelectedIndex = 0;
        //ddlproject2.SelectedIndex = 0;
        //ddlproject3.SelectedIndex = 0;
        //ddlproject4.SelectedIndex = 0;
        //ddlproject5.SelectedIndex = 0;
        //ddlproject6.SelectedIndex = 0;
        //ddlproject7.SelectedIndex = 0;
        //ddlproject8.SelectedIndex = 0;
        //ddlproject9.SelectedIndex = 0;


    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        lbltablerow.Text = "1";
        fillforupdatedescription();
        ModalPopupExtenderAddnew.Show();
        lbltempmessage.Text = "";
        TextBox1.Text = "";
        chkattchment.Checked = false;
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtenderAddnew.Hide();
    }
    protected void ddlnewtaskempname_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillweeklygoal();
        //fillweeklygoal2();
        //fillweeklygoal3();
        //fillweeklygoal4();
        //fillweeklygoal5();
        //fillweeklygoal6();
        //fillweeklygoal7();
        //fillweeklygoal8();
        //fillweeklygoal9();

        //fillproject();
        //fillproject2();
        //fillproject3();
        //fillproject4();
        //fillproject5();
        //fillproject6();
        //fillproject7();
        //fillproject8();
        //fillproject9();

        if (ddlnewtaskempname.SelectedIndex > 0)
        {
            projet();
            weeklygoal();
        }
        else
        {

        }

    }
    protected void ddlweeklygoal_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillproject();
    }

    protected void fillallocatedhour1()
    {
        string str12 = "  select sum(EUnitsAlloted) as EUnitsAlloted from   TaskAllocationMaster where EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and TaskAllocationDate='" + txteenddate.Text + "'";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            if (ds12.Rows[0]["EUnitsAlloted"].ToString() != "")
            {
                Int32 in1 = 0;
                Int32 HourtoMinute1 = 0;
                Int32 Minute1 = 0;
                Int32 TotalMinutes132 = 0;
                Int32 FinalHours = 0;
                Int32 FinalMinute = 0;



                string TotalHour = "0";
                string TotalMinutes = ds12.Rows[0]["EUnitsAlloted"].ToString();

                if (TotalHour == "")
                {

                }
                else
                {
                    in1 = Convert.ToInt32(TotalHour.ToString());
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(TotalMinutes.ToString());
                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                }

                if (TotalMinutes132 < 1439)
                {

                    FinalHours = (TotalMinutes132 / 60);
                    FinalMinute = (TotalMinutes132 % 60);

                    string s1 = Convert.ToString(FinalHours).ToString();

                    string s2 = Convert.ToString(FinalMinute).ToString();

                    TimeSpan t1 = TimeSpan.Parse(s1);
                    TimeSpan t2 = TimeSpan.Parse(s2);




                    FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


                    string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
                    string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

                    outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "sorry you have enter exceed time. ";

                }

            }
        }

        lblallocatedhours.Text = outdifftime1;

    }

    protected void fillallocatedhour2()
    {
        string str12 = "  select sum(EUnitsAlloted) as EUnitsAlloted from   TaskAllocationMaster where EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and TaskAllocationDate='" + txteenddate2.Text + "'";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            if (ds12.Rows[0]["EUnitsAlloted"].ToString() != "")
            {
                Int32 in1 = 0;
                Int32 HourtoMinute1 = 0;
                Int32 Minute1 = 0;
                Int32 TotalMinutes132 = 0;
                Int32 FinalHours = 0;
                Int32 FinalMinute = 0;



                string TotalHour = "0";
                string TotalMinutes = ds12.Rows[0]["EUnitsAlloted"].ToString();

                if (TotalHour == "")
                {

                }
                else
                {
                    in1 = Convert.ToInt32(TotalHour.ToString());
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(TotalMinutes.ToString());
                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                }

                if (TotalMinutes132 < 1439)
                {

                    FinalHours = (TotalMinutes132 / 60);
                    FinalMinute = (TotalMinutes132 % 60);

                    string s1 = Convert.ToString(FinalHours).ToString();

                    string s2 = Convert.ToString(FinalMinute).ToString();

                    TimeSpan t1 = TimeSpan.Parse(s1);
                    TimeSpan t2 = TimeSpan.Parse(s2);




                    FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


                    string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
                    string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

                    outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "sorry you have enter exceed time. ";

                }

            }
        }

        lblallocatedhours.Text = outdifftime1;

    }

    protected void fillallocatedhour3()
    {
        string str12 = "  select sum(EUnitsAlloted) as EUnitsAlloted from   TaskAllocationMaster where EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and TaskAllocationDate='" + txteenddate3.Text + "'";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            if (ds12.Rows[0]["EUnitsAlloted"].ToString() != "")
            {
                Int32 in1 = 0;
                Int32 HourtoMinute1 = 0;
                Int32 Minute1 = 0;
                Int32 TotalMinutes132 = 0;
                Int32 FinalHours = 0;
                Int32 FinalMinute = 0;



                string TotalHour = "0";
                string TotalMinutes = ds12.Rows[0]["EUnitsAlloted"].ToString();

                if (TotalHour == "")
                {

                }
                else
                {
                    in1 = Convert.ToInt32(TotalHour.ToString());
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(TotalMinutes.ToString());
                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                }

                if (TotalMinutes132 < 1439)
                {

                    FinalHours = (TotalMinutes132 / 60);
                    FinalMinute = (TotalMinutes132 % 60);

                    string s1 = Convert.ToString(FinalHours).ToString();

                    string s2 = Convert.ToString(FinalMinute).ToString();

                    TimeSpan t1 = TimeSpan.Parse(s1);
                    TimeSpan t2 = TimeSpan.Parse(s2);




                    FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


                    string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
                    string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

                    outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "sorry you have enter exceed time. ";

                }

            }
        }

        lblallocatedhours.Text = outdifftime1;

    }

    protected void fillallocatedhour4()
    {
        string str12 = "  select sum(EUnitsAlloted) as EUnitsAlloted from   TaskAllocationMaster where EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and TaskAllocationDate='" + txteenddate4.Text + "'";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            if (ds12.Rows[0]["EUnitsAlloted"].ToString() != "")
            {
                Int32 in1 = 0;
                Int32 HourtoMinute1 = 0;
                Int32 Minute1 = 0;
                Int32 TotalMinutes132 = 0;
                Int32 FinalHours = 0;
                Int32 FinalMinute = 0;



                string TotalHour = "0";
                string TotalMinutes = ds12.Rows[0]["EUnitsAlloted"].ToString();

                if (TotalHour == "")
                {

                }
                else
                {
                    in1 = Convert.ToInt32(TotalHour.ToString());
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(TotalMinutes.ToString());
                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                }

                if (TotalMinutes132 < 1439)
                {

                    FinalHours = (TotalMinutes132 / 60);
                    FinalMinute = (TotalMinutes132 % 60);

                    string s1 = Convert.ToString(FinalHours).ToString();

                    string s2 = Convert.ToString(FinalMinute).ToString();

                    TimeSpan t1 = TimeSpan.Parse(s1);
                    TimeSpan t2 = TimeSpan.Parse(s2);




                    FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


                    string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
                    string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

                    outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "sorry you have enter exceed time. ";

                }

            }
        }

        lblallocatedhours.Text = outdifftime1;

    }

    protected void fillallocatedhour5()
    {
        string str12 = "  select sum(EUnitsAlloted) as EUnitsAlloted from   TaskAllocationMaster where EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and TaskAllocationDate='" + txteenddate5.Text + "'";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            if (ds12.Rows[0]["EUnitsAlloted"].ToString() != "")
            {
                Int32 in1 = 0;
                Int32 HourtoMinute1 = 0;
                Int32 Minute1 = 0;
                Int32 TotalMinutes132 = 0;
                Int32 FinalHours = 0;
                Int32 FinalMinute = 0;



                string TotalHour = "0";
                string TotalMinutes = ds12.Rows[0]["EUnitsAlloted"].ToString();

                if (TotalHour == "")
                {

                }
                else
                {
                    in1 = Convert.ToInt32(TotalHour.ToString());
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(TotalMinutes.ToString());
                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                }

                if (TotalMinutes132 < 1439)
                {

                    FinalHours = (TotalMinutes132 / 60);
                    FinalMinute = (TotalMinutes132 % 60);

                    string s1 = Convert.ToString(FinalHours).ToString();

                    string s2 = Convert.ToString(FinalMinute).ToString();

                    TimeSpan t1 = TimeSpan.Parse(s1);
                    TimeSpan t2 = TimeSpan.Parse(s2);




                    FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


                    string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
                    string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

                    outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "sorry you have enter exceed time. ";

                }

            }
        }

        lblallocatedhours.Text = outdifftime1;

    }

    protected void fillallocatedhour6()
    {
        string str12 = "  select sum(EUnitsAlloted) as EUnitsAlloted from   TaskAllocationMaster where EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and TaskAllocationDate='" + txteenddate6.Text + "'";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            if (ds12.Rows[0]["EUnitsAlloted"].ToString() != "")
            {
                Int32 in1 = 0;
                Int32 HourtoMinute1 = 0;
                Int32 Minute1 = 0;
                Int32 TotalMinutes132 = 0;
                Int32 FinalHours = 0;
                Int32 FinalMinute = 0;



                string TotalHour = "0";
                string TotalMinutes = ds12.Rows[0]["EUnitsAlloted"].ToString();

                if (TotalHour == "")
                {

                }
                else
                {
                    in1 = Convert.ToInt32(TotalHour.ToString());
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(TotalMinutes.ToString());
                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                }

                if (TotalMinutes132 < 1439)
                {

                    FinalHours = (TotalMinutes132 / 60);
                    FinalMinute = (TotalMinutes132 % 60);

                    string s1 = Convert.ToString(FinalHours).ToString();

                    string s2 = Convert.ToString(FinalMinute).ToString();

                    TimeSpan t1 = TimeSpan.Parse(s1);
                    TimeSpan t2 = TimeSpan.Parse(s2);




                    FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


                    string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
                    string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

                    outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "sorry you have enter exceed time. ";

                }

            }
        }

        lblallocatedhours.Text = outdifftime1;

    }

    protected void fillallocatedhour7()
    {
        string str12 = "  select sum(EUnitsAlloted) as EUnitsAlloted from   TaskAllocationMaster where EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and TaskAllocationDate='" + txteenddate7.Text + "'";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            if (ds12.Rows[0]["EUnitsAlloted"].ToString() != "")
            {
                Int32 in1 = 0;
                Int32 HourtoMinute1 = 0;
                Int32 Minute1 = 0;
                Int32 TotalMinutes132 = 0;
                Int32 FinalHours = 0;
                Int32 FinalMinute = 0;



                string TotalHour = "0";
                string TotalMinutes = ds12.Rows[0]["EUnitsAlloted"].ToString();

                if (TotalHour == "")
                {

                }
                else
                {
                    in1 = Convert.ToInt32(TotalHour.ToString());
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(TotalMinutes.ToString());
                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                }

                if (TotalMinutes132 < 1439)
                {

                    FinalHours = (TotalMinutes132 / 60);
                    FinalMinute = (TotalMinutes132 % 60);

                    string s1 = Convert.ToString(FinalHours).ToString();

                    string s2 = Convert.ToString(FinalMinute).ToString();

                    TimeSpan t1 = TimeSpan.Parse(s1);
                    TimeSpan t2 = TimeSpan.Parse(s2);




                    FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


                    string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
                    string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

                    outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "sorry you have enter exceed time. ";

                }

            }
        }

        lblallocatedhours.Text = outdifftime1;

    }

    protected void fillallocatedhour8()
    {
        string str12 = "  select sum(EUnitsAlloted) as EUnitsAlloted from   TaskAllocationMaster where EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and TaskAllocationDate='" + txteenddate8.Text + "'";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            if (ds12.Rows[0]["EUnitsAlloted"].ToString() != "")
            {
                Int32 in1 = 0;
                Int32 HourtoMinute1 = 0;
                Int32 Minute1 = 0;
                Int32 TotalMinutes132 = 0;
                Int32 FinalHours = 0;
                Int32 FinalMinute = 0;



                string TotalHour = "0";
                string TotalMinutes = ds12.Rows[0]["EUnitsAlloted"].ToString();

                if (TotalHour == "")
                {

                }
                else
                {
                    in1 = Convert.ToInt32(TotalHour.ToString());
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(TotalMinutes.ToString());
                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                }

                if (TotalMinutes132 < 1439)
                {

                    FinalHours = (TotalMinutes132 / 60);
                    FinalMinute = (TotalMinutes132 % 60);

                    string s1 = Convert.ToString(FinalHours).ToString();

                    string s2 = Convert.ToString(FinalMinute).ToString();

                    TimeSpan t1 = TimeSpan.Parse(s1);
                    TimeSpan t2 = TimeSpan.Parse(s2);




                    FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


                    string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
                    string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

                    outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "sorry you have enter exceed time. ";

                }

            }
        }

        lblallocatedhours.Text = outdifftime1;

    }

    protected void fillallocatedhour9()
    {
        string str12 = "  select sum(EUnitsAlloted) as EUnitsAlloted from   TaskAllocationMaster where EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and TaskAllocationDate='" + txteenddate9.Text + "'";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            if (ds12.Rows[0]["EUnitsAlloted"].ToString() != "")
            {
                Int32 in1 = 0;
                Int32 HourtoMinute1 = 0;
                Int32 Minute1 = 0;
                Int32 TotalMinutes132 = 0;
                Int32 FinalHours = 0;
                Int32 FinalMinute = 0;



                string TotalHour = "0";
                string TotalMinutes = ds12.Rows[0]["EUnitsAlloted"].ToString();

                if (TotalHour == "")
                {

                }
                else
                {
                    in1 = Convert.ToInt32(TotalHour.ToString());
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(TotalMinutes.ToString());
                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                }

                if (TotalMinutes132 < 1439)
                {

                    FinalHours = (TotalMinutes132 / 60);
                    FinalMinute = (TotalMinutes132 % 60);

                    string s1 = Convert.ToString(FinalHours).ToString();

                    string s2 = Convert.ToString(FinalMinute).ToString();

                    TimeSpan t1 = TimeSpan.Parse(s1);
                    TimeSpan t2 = TimeSpan.Parse(s2);




                    FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


                    string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
                    string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

                    outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "sorry you have enter exceed time. ";

                }

            }
        }

        lblallocatedhours.Text = outdifftime1;

    }

    protected void fillbatchhour1()
    {
        string str12 = "select EmployeeBatchMaster.* from EmployeeBatchMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + ddlnewtaskempname.SelectedValue + "' and EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' ";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            ViewState["BatchId"] = ds12.Rows[0]["Batchmasterid"].ToString();

            string str123 = "select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "'  and DateMasterTbl.Date='" + txteenddate.Text + "' ";
            DataTable ds123 = new DataTable();
            SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
            da123.Fill(ds123);
            if (ds123.Rows.Count > 0)
            {
                DataTable dsday = new DataTable();
                if (ds123.Rows[0]["day"].ToString() == "Monday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Tuesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);


                }
                if (ds123.Rows[0]["day"].ToString() == "Wednesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Thursday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Friday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Saturday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);
                }
                if (ds123.Rows[0]["day"].ToString() == "Sunday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                string TotalMinutes = "00:00";
                if (dsday.Rows.Count > 0)
                {
                    if (Convert.ToString(dsday.Rows[0]["totalhours"]) != "")
                    {
                        TotalMinutes = Convert.ToString(dsday.Rows[0]["totalhours"]);

                    }
                    else
                    {
                        TotalMinutes = "00:00";

                    }
                    lblbatchtimeschedule.Text = Convert.ToString(dsday.Rows[0]["SchedulName"]);
                }
                else
                {
                    lblbatchtimeschedule.Text = "";

                }

                outdifftime1 = Convert.ToDateTime(TotalMinutes).ToString("HH:mm");



            }


        }
        lblregularbatchhour.Text = outdifftime1;

    }

    protected void fillbatchhour2()
    {
        string str12 = "select EmployeeBatchMaster.* from EmployeeBatchMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + ddlnewtaskempname.SelectedValue + "' and EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' ";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            ViewState["BatchId"] = ds12.Rows[0]["Batchmasterid"].ToString();

            string str123 = "select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "'  and DateMasterTbl.Date='" + txteenddate2.Text + "' ";
            DataTable ds123 = new DataTable();
            SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
            da123.Fill(ds123);
            if (ds123.Rows.Count > 0)
            {
                DataTable dsday = new DataTable();
                if (ds123.Rows[0]["day"].ToString() == "Monday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Tuesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);


                }
                if (ds123.Rows[0]["day"].ToString() == "Wednesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Thursday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Friday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Saturday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);
                }
                if (ds123.Rows[0]["day"].ToString() == "Sunday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                string TotalMinutes = "00:00";
                if (dsday.Rows.Count > 0)
                {
                    if (Convert.ToString(dsday.Rows[0]["totalhours"]) != "")
                    {
                        TotalMinutes = Convert.ToString(dsday.Rows[0]["totalhours"]);

                    }
                    else
                    {
                        TotalMinutes = "00:00";

                    }
                    lblbatchtimeschedule.Text = Convert.ToString(dsday.Rows[0]["SchedulName"]);
                }
                else
                {
                    lblbatchtimeschedule.Text = "";

                }

                outdifftime1 = Convert.ToDateTime(TotalMinutes).ToString("HH:mm");



            }


        }
        lblregularbatchhour.Text = outdifftime1;

    }

    protected void fillbatchhour3()
    {
        string str12 = "select EmployeeBatchMaster.* from EmployeeBatchMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + ddlnewtaskempname.SelectedValue + "' and EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' ";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            ViewState["BatchId"] = ds12.Rows[0]["Batchmasterid"].ToString();

            string str123 = "select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "'  and DateMasterTbl.Date='" + txteenddate3.Text + "' ";
            DataTable ds123 = new DataTable();
            SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
            da123.Fill(ds123);
            if (ds123.Rows.Count > 0)
            {
                DataTable dsday = new DataTable();
                if (ds123.Rows[0]["day"].ToString() == "Monday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Tuesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);


                }
                if (ds123.Rows[0]["day"].ToString() == "Wednesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Thursday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Friday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Saturday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);
                }
                if (ds123.Rows[0]["day"].ToString() == "Sunday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                string TotalMinutes = "00:00";
                if (dsday.Rows.Count > 0)
                {
                    if (Convert.ToString(dsday.Rows[0]["totalhours"]) != "")
                    {
                        TotalMinutes = Convert.ToString(dsday.Rows[0]["totalhours"]);

                    }
                    else
                    {
                        TotalMinutes = "00:00";

                    }
                    lblbatchtimeschedule.Text = Convert.ToString(dsday.Rows[0]["SchedulName"]);
                }
                else
                {
                    lblbatchtimeschedule.Text = "";

                }

                outdifftime1 = Convert.ToDateTime(TotalMinutes).ToString("HH:mm");



            }


        }
        lblregularbatchhour.Text = outdifftime1;

    }

    protected void fillbatchhour4()
    {
        string str12 = "select EmployeeBatchMaster.* from EmployeeBatchMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + ddlnewtaskempname.SelectedValue + "' and EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' ";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            ViewState["BatchId"] = ds12.Rows[0]["Batchmasterid"].ToString();

            string str123 = "select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "'  and DateMasterTbl.Date='" + txteenddate4.Text + "' ";
            DataTable ds123 = new DataTable();
            SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
            da123.Fill(ds123);
            if (ds123.Rows.Count > 0)
            {
                DataTable dsday = new DataTable();
                if (ds123.Rows[0]["day"].ToString() == "Monday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Tuesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);


                }
                if (ds123.Rows[0]["day"].ToString() == "Wednesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Thursday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Friday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Saturday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);
                }
                if (ds123.Rows[0]["day"].ToString() == "Sunday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                string TotalMinutes = "00:00";
                if (dsday.Rows.Count > 0)
                {
                    if (Convert.ToString(dsday.Rows[0]["totalhours"]) != "")
                    {
                        TotalMinutes = Convert.ToString(dsday.Rows[0]["totalhours"]);

                    }
                    else
                    {
                        TotalMinutes = "00:00";

                    }
                    lblbatchtimeschedule.Text = Convert.ToString(dsday.Rows[0]["SchedulName"]);
                }
                else
                {
                    lblbatchtimeschedule.Text = "";

                }

                outdifftime1 = Convert.ToDateTime(TotalMinutes).ToString("HH:mm");



            }


        }
        lblregularbatchhour.Text = outdifftime1;

    }

    protected void fillbatchhour5()
    {
        string str12 = "select EmployeeBatchMaster.* from EmployeeBatchMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + ddlnewtaskempname.SelectedValue + "' and EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' ";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            ViewState["BatchId"] = ds12.Rows[0]["Batchmasterid"].ToString();

            string str123 = "select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "'  and DateMasterTbl.Date='" + txteenddate5.Text + "' ";
            DataTable ds123 = new DataTable();
            SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
            da123.Fill(ds123);
            if (ds123.Rows.Count > 0)
            {
                DataTable dsday = new DataTable();
                if (ds123.Rows[0]["day"].ToString() == "Monday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Tuesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);


                }
                if (ds123.Rows[0]["day"].ToString() == "Wednesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Thursday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Friday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Saturday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);
                }
                if (ds123.Rows[0]["day"].ToString() == "Sunday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                string TotalMinutes = "00:00";
                if (dsday.Rows.Count > 0)
                {
                    if (Convert.ToString(dsday.Rows[0]["totalhours"]) != "")
                    {
                        TotalMinutes = Convert.ToString(dsday.Rows[0]["totalhours"]);

                    }
                    else
                    {
                        TotalMinutes = "00:00";

                    }
                    lblbatchtimeschedule.Text = Convert.ToString(dsday.Rows[0]["SchedulName"]);
                }
                else
                {
                    lblbatchtimeschedule.Text = "";

                }

                outdifftime1 = Convert.ToDateTime(TotalMinutes).ToString("HH:mm");



            }


        }
        lblregularbatchhour.Text = outdifftime1;

    }

    protected void fillbatchhour6()
    {
        string str12 = "select EmployeeBatchMaster.* from EmployeeBatchMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + ddlnewtaskempname.SelectedValue + "' and EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' ";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            ViewState["BatchId"] = ds12.Rows[0]["Batchmasterid"].ToString();

            string str123 = "select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "'  and DateMasterTbl.Date='" + txteenddate6.Text + "' ";
            DataTable ds123 = new DataTable();
            SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
            da123.Fill(ds123);
            if (ds123.Rows.Count > 0)
            {
                DataTable dsday = new DataTable();
                if (ds123.Rows[0]["day"].ToString() == "Monday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Tuesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);


                }
                if (ds123.Rows[0]["day"].ToString() == "Wednesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Thursday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Friday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Saturday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);
                }
                if (ds123.Rows[0]["day"].ToString() == "Sunday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                string TotalMinutes = "00:00";
                if (dsday.Rows.Count > 0)
                {
                    if (Convert.ToString(dsday.Rows[0]["totalhours"]) != "")
                    {
                        TotalMinutes = Convert.ToString(dsday.Rows[0]["totalhours"]);

                    }
                    else
                    {
                        TotalMinutes = "00:00";

                    }
                    lblbatchtimeschedule.Text = Convert.ToString(dsday.Rows[0]["SchedulName"]);
                }
                else
                {
                    lblbatchtimeschedule.Text = "";

                }

                outdifftime1 = Convert.ToDateTime(TotalMinutes).ToString("HH:mm");



            }


        }
        lblregularbatchhour.Text = outdifftime1;

    }

    protected void fillbatchhour7()
    {
        string str12 = "select EmployeeBatchMaster.* from EmployeeBatchMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + ddlnewtaskempname.SelectedValue + "' and EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' ";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            ViewState["BatchId"] = ds12.Rows[0]["Batchmasterid"].ToString();

            string str123 = "select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "'  and DateMasterTbl.Date='" + txteenddate7.Text + "' ";
            DataTable ds123 = new DataTable();
            SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
            da123.Fill(ds123);
            if (ds123.Rows.Count > 0)
            {
                DataTable dsday = new DataTable();
                if (ds123.Rows[0]["day"].ToString() == "Monday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Tuesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);


                }
                if (ds123.Rows[0]["day"].ToString() == "Wednesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Thursday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Friday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Saturday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);
                }
                if (ds123.Rows[0]["day"].ToString() == "Sunday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                string TotalMinutes = "00:00";
                if (dsday.Rows.Count > 0)
                {
                    if (Convert.ToString(dsday.Rows[0]["totalhours"]) != "")
                    {
                        TotalMinutes = Convert.ToString(dsday.Rows[0]["totalhours"]);

                    }
                    else
                    {
                        TotalMinutes = "00:00";

                    }
                    lblbatchtimeschedule.Text = Convert.ToString(dsday.Rows[0]["SchedulName"]);
                }
                else
                {
                    lblbatchtimeschedule.Text = "";

                }

                outdifftime1 = Convert.ToDateTime(TotalMinutes).ToString("HH:mm");



            }


        }
        lblregularbatchhour.Text = outdifftime1;

    }

    protected void fillbatchhour8()
    {
        string str12 = "select EmployeeBatchMaster.* from EmployeeBatchMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + ddlnewtaskempname.SelectedValue + "' and EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' ";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            ViewState["BatchId"] = ds12.Rows[0]["Batchmasterid"].ToString();

            string str123 = "select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "'  and DateMasterTbl.Date='" + txteenddate8.Text + "' ";
            DataTable ds123 = new DataTable();
            SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
            da123.Fill(ds123);
            if (ds123.Rows.Count > 0)
            {
                DataTable dsday = new DataTable();
                if (ds123.Rows[0]["day"].ToString() == "Monday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Tuesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);


                }
                if (ds123.Rows[0]["day"].ToString() == "Wednesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Thursday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Friday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Saturday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);
                }
                if (ds123.Rows[0]["day"].ToString() == "Sunday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                string TotalMinutes = "00:00";
                if (dsday.Rows.Count > 0)
                {
                    if (Convert.ToString(dsday.Rows[0]["totalhours"]) != "")
                    {
                        TotalMinutes = Convert.ToString(dsday.Rows[0]["totalhours"]);

                    }
                    else
                    {
                        TotalMinutes = "00:00";

                    }
                    lblbatchtimeschedule.Text = Convert.ToString(dsday.Rows[0]["SchedulName"]);
                }
                else
                {
                    lblbatchtimeschedule.Text = "";

                }

                outdifftime1 = Convert.ToDateTime(TotalMinutes).ToString("HH:mm");



            }


        }
        lblregularbatchhour.Text = outdifftime1;

    }

    protected void fillbatchhour9()
    {
        string str12 = "select EmployeeBatchMaster.* from EmployeeBatchMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + ddlnewtaskempname.SelectedValue + "' and EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' ";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        string outdifftime1 = "00:00";

        if (ds12.Rows.Count > 0)
        {
            ViewState["BatchId"] = ds12.Rows[0]["Batchmasterid"].ToString();

            string str123 = "select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "'  and DateMasterTbl.Date='" + txteenddate9.Text + "' ";
            DataTable ds123 = new DataTable();
            SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
            da123.Fill(ds123);
            if (ds123.Rows.Count > 0)
            {
                DataTable dsday = new DataTable();
                if (ds123.Rows[0]["day"].ToString() == "Monday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Tuesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);


                }
                if (ds123.Rows[0]["day"].ToString() == "Wednesday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Thursday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Friday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                if (ds123.Rows[0]["day"].ToString() == "Saturday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    // DataTable dsday = new DataTable();
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);
                }
                if (ds123.Rows[0]["day"].ToString() == "Sunday")
                {
                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";
                    //
                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                }
                string TotalMinutes = "00:00";
                if (dsday.Rows.Count > 0)
                {
                    if (Convert.ToString(dsday.Rows[0]["totalhours"]) != "")
                    {
                        TotalMinutes = Convert.ToString(dsday.Rows[0]["totalhours"]);

                    }
                    else
                    {
                        TotalMinutes = "00:00";

                    }
                    lblbatchtimeschedule.Text = Convert.ToString(dsday.Rows[0]["SchedulName"]);
                }
                else
                {
                    lblbatchtimeschedule.Text = "";

                }

                outdifftime1 = Convert.ToDateTime(TotalMinutes).ToString("HH:mm");



            }


        }
        lblregularbatchhour.Text = outdifftime1;

    }

    protected void fillavailhour()
    {
        string time = "";
        string outdifftime;
        TimeSpan t3 = TimeSpan.Parse(lblregularbatchhour.Text);
        TimeSpan t4 = TimeSpan.Parse(lblallocatedhours.Text);

        time = t3.Subtract(t4).ToString();
        string SX = time.Substring(0, 1);
        if (SX == "-")
        {
            time = time.Remove(0, 1);
            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");

        }
        else
        {
            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");


        }

        lblavalablehours.Text = outdifftime;

    }

    protected void fillrestriction1()
    {
        int item1 = 0;
        Int32 FinalHours = 0;
        Int32 FinalMinute = 0;
        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (txtbudgetminute.Text != "")
        {
            item1 = Convert.ToInt32(txtbudgetminute.Text);
        }
        int temp = item1;

        ViewState["mastertemp"] = Convert.ToInt32(temp);

        if (temp < 1439)
        {
            FinalHours = (temp / 60);
            FinalMinute = (temp % 60);
            string s1 = Convert.ToString(FinalHours).ToString();
            string s2 = Convert.ToString(FinalMinute).ToString();
            TimeSpan t1 = TimeSpan.Parse(s1);
            TimeSpan t2 = TimeSpan.Parse(s2);

            FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


            string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
            string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

            outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "sorry you have enter exceed time.";
        }
        lblinserttime.Text = outdifftime1;
    }

    protected void fillrestriction2()
    {
        int item1 = 0;
        Int32 FinalHours = 0;
        Int32 FinalMinute = 0;
        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (txtbudgetedminute2.Text != "")
        {
            item1 = Convert.ToInt32(txtbudgetedminute2.Text);
        }
        int temp = item1;

        ViewState["mastertemp"] = Convert.ToInt32(temp);

        if (temp < 1439)
        {
            FinalHours = (temp / 60);
            FinalMinute = (temp % 60);
            string s1 = Convert.ToString(FinalHours).ToString();
            string s2 = Convert.ToString(FinalMinute).ToString();
            TimeSpan t1 = TimeSpan.Parse(s1);
            TimeSpan t2 = TimeSpan.Parse(s2);

            FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


            string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
            string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

            outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "sorry you have enter exceed time.";
        }
        lblinserttime.Text = outdifftime1;
    }

    protected void fillrestriction3()
    {
        int item1 = 0;
        Int32 FinalHours = 0;
        Int32 FinalMinute = 0;
        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (txtbudgetedminute3.Text != "")
        {
            item1 = Convert.ToInt32(txtbudgetedminute3.Text);
        }
        int temp = item1;

        ViewState["mastertemp"] = Convert.ToInt32(temp);

        if (temp < 1439)
        {
            FinalHours = (temp / 60);
            FinalMinute = (temp % 60);
            string s1 = Convert.ToString(FinalHours).ToString();
            string s2 = Convert.ToString(FinalMinute).ToString();
            TimeSpan t1 = TimeSpan.Parse(s1);
            TimeSpan t2 = TimeSpan.Parse(s2);

            FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


            string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
            string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

            outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "sorry you have enter exceed time.";
        }
        lblinserttime.Text = outdifftime1;
    }

    protected void fillrestriction4()
    {
        int item1 = 0;
        Int32 FinalHours = 0;
        Int32 FinalMinute = 0;
        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (txtbudgetedminute4.Text != "")
        {
            item1 = Convert.ToInt32(txtbudgetedminute4.Text);
        }
        int temp = item1;

        ViewState["mastertemp"] = Convert.ToInt32(temp);

        if (temp < 1439)
        {
            FinalHours = (temp / 60);
            FinalMinute = (temp % 60);
            string s1 = Convert.ToString(FinalHours).ToString();
            string s2 = Convert.ToString(FinalMinute).ToString();
            TimeSpan t1 = TimeSpan.Parse(s1);
            TimeSpan t2 = TimeSpan.Parse(s2);

            FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


            string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
            string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

            outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "sorry you have enter exceed time.";
        }
        lblinserttime.Text = outdifftime1;
    }

    protected void fillrestriction5()
    {
        int item1 = 0;
        Int32 FinalHours = 0;
        Int32 FinalMinute = 0;
        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (txtbudgetedminute5.Text != "")
        {
            item1 = Convert.ToInt32(txtbudgetedminute5.Text);
        }
        int temp = item1;

        ViewState["mastertemp"] = Convert.ToInt32(temp);

        if (temp < 1439)
        {
            FinalHours = (temp / 60);
            FinalMinute = (temp % 60);
            string s1 = Convert.ToString(FinalHours).ToString();
            string s2 = Convert.ToString(FinalMinute).ToString();
            TimeSpan t1 = TimeSpan.Parse(s1);
            TimeSpan t2 = TimeSpan.Parse(s2);

            FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


            string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
            string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

            outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "sorry you have enter exceed time.";
        }
        lblinserttime.Text = outdifftime1;
    }

    protected void fillrestriction6()
    {
        int item1 = 0;
        Int32 FinalHours = 0;
        Int32 FinalMinute = 0;
        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (txtbudgetedminute6.Text != "")
        {
            item1 = Convert.ToInt32(txtbudgetedminute6.Text);
        }
        int temp = item1;

        ViewState["mastertemp"] = Convert.ToInt32(temp);

        if (temp < 1439)
        {
            FinalHours = (temp / 60);
            FinalMinute = (temp % 60);
            string s1 = Convert.ToString(FinalHours).ToString();
            string s2 = Convert.ToString(FinalMinute).ToString();
            TimeSpan t1 = TimeSpan.Parse(s1);
            TimeSpan t2 = TimeSpan.Parse(s2);

            FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


            string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
            string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

            outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "sorry you have enter exceed time.";
        }
        lblinserttime.Text = outdifftime1;
    }

    protected void fillrestriction7()
    {
        int item1 = 0;
        Int32 FinalHours = 0;
        Int32 FinalMinute = 0;
        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (txtbudgetedminute7.Text != "")
        {
            item1 = Convert.ToInt32(txtbudgetedminute7.Text);
        }
        int temp = item1;

        ViewState["mastertemp"] = Convert.ToInt32(temp);

        if (temp < 1439)
        {
            FinalHours = (temp / 60);
            FinalMinute = (temp % 60);
            string s1 = Convert.ToString(FinalHours).ToString();
            string s2 = Convert.ToString(FinalMinute).ToString();
            TimeSpan t1 = TimeSpan.Parse(s1);
            TimeSpan t2 = TimeSpan.Parse(s2);

            FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


            string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
            string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

            outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "sorry you have enter exceed time.";
        }
        lblinserttime.Text = outdifftime1;
    }

    protected void fillrestriction8()
    {
        int item1 = 0;
        Int32 FinalHours = 0;
        Int32 FinalMinute = 0;
        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (txtbudgetedminute8.Text != "")
        {
            item1 = Convert.ToInt32(txtbudgetedminute8.Text);
        }
        int temp = item1;

        ViewState["mastertemp"] = Convert.ToInt32(temp);

        if (temp < 1439)
        {
            FinalHours = (temp / 60);
            FinalMinute = (temp % 60);
            string s1 = Convert.ToString(FinalHours).ToString();
            string s2 = Convert.ToString(FinalMinute).ToString();
            TimeSpan t1 = TimeSpan.Parse(s1);
            TimeSpan t2 = TimeSpan.Parse(s2);

            FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


            string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
            string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

            outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "sorry you have enter exceed time.";
        }
        lblinserttime.Text = outdifftime1;
    }

    protected void fillrestriction9()
    {
        int item1 = 0;
        Int32 FinalHours = 0;
        Int32 FinalMinute = 0;
        string FinalTime = "";
        string outdifftime1 = "00:00";

        if (txtbudgetedminute9.Text != "")
        {
            item1 = Convert.ToInt32(txtbudgetedminute9.Text);
        }
        int temp = item1;

        ViewState["mastertemp"] = Convert.ToInt32(temp);

        if (temp < 1439)
        {
            FinalHours = (temp / 60);
            FinalMinute = (temp % 60);
            string s1 = Convert.ToString(FinalHours).ToString();
            string s2 = Convert.ToString(FinalMinute).ToString();
            TimeSpan t1 = TimeSpan.Parse(s1);
            TimeSpan t2 = TimeSpan.Parse(s2);

            FinalTime = FinalHours + ":" + FinalMinute + ":" + "00";


            string temp1 = Convert.ToDateTime(FinalTime).ToString("HH");
            string temp2 = Convert.ToDateTime(FinalTime).ToString("mm");

            outdifftime1 = Convert.ToDateTime(FinalTime).ToString("HH:mm");
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "sorry you have enter exceed time.";
        }
        lblinserttime.Text = outdifftime1;
    }



    protected void minitecheck()
    {
        // TimeSpan t3 = TimeSpan.Parse(lblavalablehours.Text);

        string temp1 = Convert.ToDateTime(lblavalablehours.Text).ToString("HH");
        string temp2 = Convert.ToDateTime(lblavalablehours.Text).ToString("mm");

        //TimeSpan t2 = TimeSpan.Parse(t3);

        // string temp1 = Convert.ToDateTime(t3).ToString("HH");
        //string temp2 = Convert.ToDateTime(t3).ToString("mm");

        int minute123 = Convert.ToInt32(temp1) * 60;
        int minute12345 = Convert.ToInt32(temp2);

        int finalminute = minute123 + minute12345;

        ViewState["availableminute"] = finalminute;

    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        Int32 success = 0;

        string st2 = "Insert into TempTaskDescription(TableRow,Description,Addattachment,UserId) values ('" + lbltablerow.Text + "','" + TextBox1.Text + "','" + chkattchment.Checked + "','" + Convert.ToString(Session["UserId"]) + "')";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();
        lbltempmessage.Visible = true;
        lbltempmessage.Text = "Record inserted successfully";

        SqlDataAdapter da = new SqlDataAdapter("select max(TaskId) as TaskId from TaskMaster", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        success = Convert.ToInt32(dt.Rows[0]["TaskId"].ToString());
        ViewState["success"] = success;

        if (chkattchment.Checked == true)
        {

            string te = "AddDocMaster.aspx?takid=" + success + "&storeid=" + ddlStore.SelectedValue + "";


            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
        ModalPopupExtenderAddnew.Hide();
    }
    protected void fillforupdatedescription()
    {
        string str123 = "select * from TempTaskDescription where TableRow='" + lbltablerow.Text + "' ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);
        if (ds123.Rows.Count > 0)
        {
            TextBox1.Text = ds123.Rows[0]["Description"].ToString();
            chkattchment.Checked = Convert.ToBoolean(ds123.Rows[0]["Addattachment"].ToString());

            btntempupdate.Visible = true;
            btntempadd.Visible = false;

        }
        else
        {
            btntempupdate.Visible = false;
            btntempadd.Visible = true;


        }


    }
    protected void btntempupdate_Click(object sender, EventArgs e)
    {
        string st2 = "Update TempTaskDescription set Description='" + TextBox1.Text + "',Addattachment='" + chkattchment.Checked + "',UserId='" + Convert.ToString(Session["UserId"]) + "' where TableRow= '" + lbltablerow.Text + "'";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();
        lbltempmessage.Visible = true;
        lbltempmessage.Text = "Record updated succesfully";

        Int32 success = 0;

        SqlDataAdapter da1 = new SqlDataAdapter("select max(TaskId) as TaskId from TaskMaster", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        success = Convert.ToInt32(dt1.Rows[0]["TaskId"].ToString());
        ViewState["success"] = success;

        if (chkattchment.Checked == true)
        {

            string te = "AddDocMaster.aspx?takid=" + success + "&storeid=" + ddlStore.SelectedValue + "";


            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
        ModalPopupExtenderAddnew.Hide();
    }

    protected void deletetaskdescription()
    {
        string str12 = " Delete from TempTaskDescription where UserId='" + Convert.ToString(Session["UserId"]) + "'";
        SqlCommand cmd12 = new SqlCommand(str12, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd12.ExecuteNonQuery();
        con.Close();

    }
    protected void deleteDocumentMasterTempforTask()
    {
        string str12 = " Delete from DocumentMasterTempforTask where UserId='" + Convert.ToString(Session["UserId"]) + "'";
        SqlCommand cmd12 = new SqlCommand(str12, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd12.ExecuteNonQuery();
        con.Close();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        lbltablerow.Text = "2";
        fillforupdatedescription();
        ModalPopupExtenderAddnew.Show();
        lbltempmessage.Text = "";
        TextBox1.Text = "";
        chkattchment.Checked = false;
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        lbltablerow.Text = "3";
        fillforupdatedescription();
        ModalPopupExtenderAddnew.Show();
        lbltempmessage.Text = "";
        TextBox1.Text = "";
        chkattchment.Checked = false;
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        lbltablerow.Text = "4";
        fillforupdatedescription();
        ModalPopupExtenderAddnew.Show();
        lbltempmessage.Text = "";
        TextBox1.Text = "";
        chkattchment.Checked = false;
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        lbltablerow.Text = "5";
        fillforupdatedescription();
        ModalPopupExtenderAddnew.Show();
        lbltempmessage.Text = "";
        TextBox1.Text = "";
        chkattchment.Checked = false;
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        lbltablerow.Text = "6";
        fillforupdatedescription();
        ModalPopupExtenderAddnew.Show();
        lbltempmessage.Text = "";
        TextBox1.Text = "";
        chkattchment.Checked = false;
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        lbltablerow.Text = "7";
        fillforupdatedescription();
        ModalPopupExtenderAddnew.Show();
        lbltempmessage.Text = "";
        TextBox1.Text = "";
        chkattchment.Checked = false;
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        lbltablerow.Text = "8";
        fillforupdatedescription();
        ModalPopupExtenderAddnew.Show();
        lbltempmessage.Text = "";
        TextBox1.Text = "";
        chkattchment.Checked = false;
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        lbltablerow.Text = "9";
        fillforupdatedescription();
        ModalPopupExtenderAddnew.Show();
        lbltempmessage.Text = "";
        TextBox1.Text = "";
        chkattchment.Checked = false;
    }

    //protected void fillproject2()
    //{
    //    ddlproject2.Items.Clear();
    //    string tempstring = "select (EmployeeMaster.EmployeeName+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=ProjectMaster.EmployeeID where  ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.EmployeeID='" + ddlnewtaskempname.SelectedValue + "' and Status='Pending' ";

    //    string order = " Order by EmployeeMaster.EmployeeName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {

    //        ddlproject2.DataSource = ds12;
    //        ddlproject2.DataTextField = "ProjectName";
    //        ddlproject2.DataValueField = "ProjectId";
    //        ddlproject2.DataBind();
    //    }
    //    ddlproject2.Items.Insert(0, "-Select-");
    //    ddlproject2.Items[0].Value = "0";
    //}
    //protected void fillproject3()
    //{
    //    ddlproject3.Items.Clear();
    //    string tempstring = "select (EmployeeMaster.EmployeeName+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=ProjectMaster.EmployeeID where  ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.EmployeeID='" + ddlnewtaskempname.SelectedValue + "' and Status='Pending' ";

    //    string order = " Order by EmployeeMaster.EmployeeName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {

    //        ddlproject3.DataSource = ds12;
    //        ddlproject3.DataTextField = "ProjectName";
    //        ddlproject3.DataValueField = "ProjectId";
    //        ddlproject3.DataBind();
    //    }
    //    ddlproject3.Items.Insert(0, "-Select-");
    //    ddlproject3.Items[0].Value = "0";
    //}
    //protected void fillproject4()
    //{
    //    ddlproject4.Items.Clear();
    //    string tempstring = "select (EmployeeMaster.EmployeeName+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=ProjectMaster.EmployeeID where  ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.EmployeeID='" + ddlnewtaskempname.SelectedValue + "' and Status='Pending' ";

    //    string order = " Order by EmployeeMaster.EmployeeName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {

    //        ddlproject4.DataSource = ds12;
    //        ddlproject4.DataTextField = "ProjectName";
    //        ddlproject4.DataValueField = "ProjectId";
    //        ddlproject4.DataBind();
    //    }
    //    ddlproject4.Items.Insert(0, "-Select-");
    //    ddlproject4.Items[0].Value = "0";
    //}
    //protected void fillproject5()
    //{
    //    ddlproject5.Items.Clear();
    //    string tempstring = "select (EmployeeMaster.EmployeeName+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=ProjectMaster.EmployeeID where  ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.EmployeeID='" + ddlnewtaskempname.SelectedValue + "' and Status='Pending' ";

    //    string order = " Order by EmployeeMaster.EmployeeName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {

    //        ddlproject5.DataSource = ds12;
    //        ddlproject5.DataTextField = "ProjectName";
    //        ddlproject5.DataValueField = "ProjectId";
    //        ddlproject5.DataBind();
    //    }
    //    ddlproject5.Items.Insert(0, "-Select-");
    //    ddlproject5.Items[0].Value = "0";
    //}
    //protected void fillproject6()
    //{
    //    ddlproject6.Items.Clear();
    //    string tempstring = "select (EmployeeMaster.EmployeeName+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=ProjectMaster.EmployeeID where  ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.EmployeeID='" + ddlnewtaskempname.SelectedValue + "' and Status='Pending' ";

    //    string order = " Order by EmployeeMaster.EmployeeName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {

    //        ddlproject6.DataSource = ds12;
    //        ddlproject6.DataTextField = "ProjectName";
    //        ddlproject6.DataValueField = "ProjectId";
    //        ddlproject6.DataBind();
    //    }
    //    ddlproject6.Items.Insert(0, "-Select-");
    //    ddlproject6.Items[0].Value = "0";
    //}
    //protected void fillproject7()
    //{
    //    ddlproject7.Items.Clear();
    //    string tempstring = "select (EmployeeMaster.EmployeeName+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=ProjectMaster.EmployeeID where  ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.EmployeeID='" + ddlnewtaskempname.SelectedValue + "' and Status='Pending' ";

    //    string order = " Order by EmployeeMaster.EmployeeName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {

    //        ddlproject7.DataSource = ds12;
    //        ddlproject7.DataTextField = "ProjectName";
    //        ddlproject7.DataValueField = "ProjectId";
    //        ddlproject7.DataBind();
    //    }
    //    ddlproject7.Items.Insert(0, "-Select-");
    //    ddlproject7.Items[0].Value = "0";
    //}
    //protected void fillproject8()
    //{
    //    ddlproject8.Items.Clear();
    //    string tempstring = "select (EmployeeMaster.EmployeeName+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=ProjectMaster.EmployeeID where  ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.EmployeeID='" + ddlnewtaskempname.SelectedValue + "' and Status='Pending' ";

    //    string order = " Order by EmployeeMaster.EmployeeName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {

    //        ddlproject8.DataSource = ds12;
    //        ddlproject8.DataTextField = "ProjectName";
    //        ddlproject8.DataValueField = "ProjectId";
    //        ddlproject8.DataBind();
    //    }
    //    ddlproject8.Items.Insert(0, "-Select-");
    //    ddlproject8.Items[0].Value = "0";
    //}
    //protected void fillproject9()
    //{
    //    ddlproject9.Items.Clear();
    //    string tempstring = "select (EmployeeMaster.EmployeeName+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=ProjectMaster.EmployeeID where  ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.EmployeeID='" + ddlnewtaskempname.SelectedValue + "' and Status='Pending' ";

    //    string order = " Order by EmployeeMaster.EmployeeName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {

    //        ddlproject9.DataSource = ds12;
    //        ddlproject9.DataTextField = "ProjectName";
    //        ddlproject9.DataValueField = "ProjectId";
    //        ddlproject9.DataBind();
    //    }
    //    ddlproject9.Items.Insert(0, "-Select-");
    //    ddlproject9.Items[0].Value = "0";
    //}

    //protected void ddlweeklygoal2_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    //protected void ddlweeklygoal3_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    //protected void ddlweeklygoal4_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    //protected void ddlweeklygoal5_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    //protected void ddlweeklygoal6_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    //protected void ddlweeklygoal7_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    //protected void ddlweeklygoal8_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    //protected void ddlweeklygoal9_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    //protected void fillweeklygoal()
    //{
    //    ddlweeklygoal.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner  join  EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and WMaster.BusinessID='" + ddlStore.SelectedValue + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal.DataSource = ds12;
    //        ddlweeklygoal.DataTextField = "Title";
    //        ddlweeklygoal.DataValueField = "MasterId";
    //        ddlweeklygoal.DataBind();
    //    }
    //    ddlweeklygoal.Items.Insert(0, "-Select-");
    //    ddlweeklygoal.Items[0].Value = "0";
    //}
    //protected void fillweeklygoal2()
    //{
    //    ddlweeklygoal2.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner  join  EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and WMaster.BusinessID='" + ddlStore.SelectedValue + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal2.DataSource = ds12;
    //        ddlweeklygoal2.DataTextField = "Title";
    //        ddlweeklygoal2.DataValueField = "MasterId";
    //        ddlweeklygoal2.DataBind();
    //    }
    //    ddlweeklygoal2.Items.Insert(0, "-Select-");
    //    ddlweeklygoal2.Items[0].Value = "0";
    //}
    //protected void fillweeklygoal3()
    //{

    //    ddlweeklygoal3.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner  join  EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and WMaster.BusinessID='" + ddlStore.SelectedValue + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal3.DataSource = ds12;
    //        ddlweeklygoal3.DataTextField = "Title";
    //        ddlweeklygoal3.DataValueField = "MasterId";
    //        ddlweeklygoal3.DataBind();
    //    }
    //    ddlweeklygoal3.Items.Insert(0, "-Select-");
    //    ddlweeklygoal3.Items[0].Value = "0";
    //}
    //protected void fillweeklygoal4()
    //{
    //    ddlweeklygoal4.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner  join  EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and WMaster.BusinessID='" + ddlStore.SelectedValue + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal4.DataSource = ds12;
    //        ddlweeklygoal4.DataTextField = "Title";
    //        ddlweeklygoal4.DataValueField = "MasterId";
    //        ddlweeklygoal4.DataBind();
    //    }
    //    ddlweeklygoal4.Items.Insert(0, "-Select-");
    //    ddlweeklygoal4.Items[0].Value = "0";
    //}
    //protected void fillweeklygoal5()
    //{
    //    ddlweeklygoal5.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner  join  EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and WMaster.BusinessID='" + ddlStore.SelectedValue + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal5.DataSource = ds12;
    //        ddlweeklygoal5.DataTextField = "Title";
    //        ddlweeklygoal5.DataValueField = "MasterId";
    //        ddlweeklygoal5.DataBind();
    //    }
    //    ddlweeklygoal5.Items.Insert(0, "-Select-");
    //    ddlweeklygoal5.Items[0].Value = "0";
    //}
    //protected void fillweeklygoal6()
    //{
    //    ddlweeklygoal6.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner  join  EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and WMaster.BusinessID='" + ddlStore.SelectedValue + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal6.DataSource = ds12;
    //        ddlweeklygoal6.DataTextField = "Title";
    //        ddlweeklygoal6.DataValueField = "MasterId";
    //        ddlweeklygoal6.DataBind();
    //    }
    //    ddlweeklygoal6.Items.Insert(0, "-Select-");
    //    ddlweeklygoal6.Items[0].Value = "0";
    //}
    //protected void fillweeklygoal7()
    //{
    //    ddlweeklygoal7.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner  join  EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and WMaster.BusinessID='" + ddlStore.SelectedValue + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal7.DataSource = ds12;
    //        ddlweeklygoal7.DataTextField = "Title";
    //        ddlweeklygoal7.DataValueField = "MasterId";
    //        ddlweeklygoal7.DataBind();
    //    }
    //    ddlweeklygoal7.Items.Insert(0, "-Select-");
    //    ddlweeklygoal7.Items[0].Value = "0";
    //}
    //protected void fillweeklygoal8()
    //{
    //    ddlweeklygoal8.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner  join  EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and WMaster.BusinessID='" + ddlStore.SelectedValue + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal8.DataSource = ds12;
    //        ddlweeklygoal8.DataTextField = "Title";
    //        ddlweeklygoal8.DataValueField = "MasterId";
    //        ddlweeklygoal8.DataBind();
    //    }
    //    ddlweeklygoal8.Items.Insert(0, "-Select-");
    //    ddlweeklygoal8.Items[0].Value = "0";
    //}
    //protected void fillweeklygoal9()
    //{
    //    ddlweeklygoal9.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner  join  EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and WMaster.BusinessID='" + ddlStore.SelectedValue + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal9.DataSource = ds12;
    //        ddlweeklygoal9.DataTextField = "Title";
    //        ddlweeklygoal9.DataValueField = "MasterId";
    //        ddlweeklygoal9.DataBind();
    //    }
    //    ddlweeklygoal9.Items.Insert(0, "-Select-");
    //    ddlweeklygoal9.Items[0].Value = "0";
    //}

    protected void insertdata()
    {
        string st2 = " INSERT INTO TaskMaster (TaskName,ProjectId,Eunitsalloted,Isdelete,WeeklygoalId,Whid,Compid) " +
                              "  Values ('" + txtTaskName.Text + "','" + DropDownList2.SelectedValue + "','" + txtbudgetminute.Text + "','false','" + DropDownList1.SelectedValue + "','" + ddlStore.SelectedValue + "','" + Session["Comid"].ToString() + "') ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();

        string str123 = "select Max(TaskId) as TaskId from TaskMaster ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        ViewState["MaxTaskId1"] = ds123.Rows[0]["TaskId"].ToString();

        string sttbldata = "select * from TempTaskDescription where TableRow='1' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable dstabledata = new DataTable();
        SqlDataAdapter datbldata = new SqlDataAdapter(sttbldata, con);
        datbldata.Fill(dstabledata);

        ViewState["TaskDescription1"] = "";
        if (dstabledata.Rows.Count > 0)
        {
            ViewState["TaskDescription1"] = dstabledata.Rows[0]["Description"].ToString();
        }

        string stallocation = "";

        if (Panel5.Visible == true && ddlnewtaskempname.SelectedIndex > 0)
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId1"] + "','" + ddlnewtaskempname.SelectedValue + "','" + txteenddate.Text + "','" + txtbudgetminute.Text + "','" + ViewState["TaskDescription1"] + "') ";
        }
        else
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId1"] + "','0','" + txteenddate.Text + "','" + txtbudgetminute.Text + "','" + ViewState["TaskDescription1"] + "') ";
        }
        SqlCommand cmdallocation = new SqlCommand(stallocation, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdallocation.ExecuteNonQuery();
        con.Close();


    }
    protected void insertdata2()
    {
        string st2 = " INSERT INTO TaskMaster (TaskName,ProjectId,Eunitsalloted,Isdelete,WeeklygoalId,Whid,Compid) " +
                              "  Values ('" + txttaskname2.Text + "','" + DropDownList2.SelectedValue + "','" + txtbudgetedminute2.Text + "','false','" + DropDownList1.SelectedValue + "','" + ddlStore.SelectedValue + "','" + Session["Comid"].ToString() + "') ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();

        string str123 = "select Max(TaskId) as TaskId from TaskMaster ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        ViewState["MaxTaskId2"] = ds123.Rows[0]["TaskId"].ToString();

        string sttbldata = "select * from TempTaskDescription where TableRow='2' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable dstabledata = new DataTable();
        SqlDataAdapter datbldata = new SqlDataAdapter(sttbldata, con);
        datbldata.Fill(dstabledata);


        ViewState["TaskDescription2"] = "";
        if (dstabledata.Rows.Count > 0)
        {
            ViewState["TaskDescription2"] = dstabledata.Rows[0]["Description"].ToString();
        }

        string stallocation = "";

        if (Panel5.Visible == true && ddlnewtaskempname.SelectedIndex > 0)
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId2"] + "','" + ddlnewtaskempname.SelectedValue + "','" + txteenddate2.Text + "','" + txtbudgetedminute2.Text + "','" + ViewState["TaskDescription2"] + "') ";
        }
        else
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId2"] + "','0','" + txteenddate2.Text + "','" + txtbudgetedminute2.Text + "','" + ViewState["TaskDescription2"] + "') ";
        }

        SqlCommand cmdallocation = new SqlCommand(stallocation, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdallocation.ExecuteNonQuery();
        con.Close();


    }
    protected void insertdata3()
    {
        string st2 = " INSERT INTO TaskMaster (TaskName,ProjectId,Eunitsalloted,Isdelete,WeeklygoalId,Whid,Compid) " +
                              "  Values ('" + txttaskname3.Text + "','" + DropDownList2.SelectedValue + "','" + txtbudgetedminute3.Text + "','false','" + DropDownList1.SelectedValue + "','" + ddlStore.SelectedValue + "','" + Session["Comid"].ToString() + "') ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();

        string str123 = "select Max(TaskId) as TaskId from TaskMaster ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        ViewState["MaxTaskId3"] = ds123.Rows[0]["TaskId"].ToString();

        string sttbldata = "select * from TempTaskDescription where TableRow='3' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable dstabledata = new DataTable();
        SqlDataAdapter datbldata = new SqlDataAdapter(sttbldata, con);
        datbldata.Fill(dstabledata);

        ViewState["TaskDescription3"] = "";
        if (dstabledata.Rows.Count > 0)
        {
            ViewState["TaskDescription3"] = dstabledata.Rows[0]["Description"].ToString();
        }

        string stallocation = "";

        if (Panel5.Visible == true && ddlnewtaskempname.SelectedIndex > 0)
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId3"] + "','" + ddlnewtaskempname.SelectedValue + "','" + txteenddate3.Text + "','" + txtbudgetedminute3.Text + "','" + ViewState["TaskDescription3"] + "') ";
        }
        else
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId3"] + "','0','" + txteenddate3.Text + "','" + txtbudgetedminute3.Text + "','" + ViewState["TaskDescription3"] + "') ";
        }

        SqlCommand cmdallocation = new SqlCommand(stallocation, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdallocation.ExecuteNonQuery();
        con.Close();


    }
    protected void insertdata4()
    {
        string st2 = " INSERT INTO TaskMaster (TaskName,ProjectId,Eunitsalloted,Isdelete,WeeklygoalId,Whid,Compid) " +
                              "  Values ('" + txttaskname4.Text + "','" + DropDownList2.SelectedValue + "','" + txtbudgetedminute4.Text + "','false','" + DropDownList1.SelectedValue + "','" + ddlStore.SelectedValue + "','" + Session["Comid"].ToString() + "') ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();

        string str123 = "select Max(TaskId) as TaskId from TaskMaster ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        ViewState["MaxTaskId4"] = ds123.Rows[0]["TaskId"].ToString();

        string sttbldata = "select * from TempTaskDescription where TableRow='4' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable dstabledata = new DataTable();
        SqlDataAdapter datbldata = new SqlDataAdapter(sttbldata, con);
        datbldata.Fill(dstabledata);

        ViewState["TaskDescription4"] = "";
        if (dstabledata.Rows.Count > 0)
        {
            ViewState["TaskDescription4"] = dstabledata.Rows[0]["Description"].ToString();
        }
        string stallocation = "";

        if (Panel5.Visible == true && ddlnewtaskempname.SelectedIndex > 0)
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId4"] + "','" + ddlnewtaskempname.SelectedValue + "','" + txteenddate4.Text + "','" + txtbudgetedminute4.Text + "','" + ViewState["TaskDescription4"] + "') ";
        }
        else
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId4"] + "','0','" + txteenddate4.Text + "','" + txtbudgetedminute4.Text + "','" + ViewState["TaskDescription4"] + "') ";
        }

        SqlCommand cmdallocation = new SqlCommand(stallocation, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdallocation.ExecuteNonQuery();
        con.Close();


    }
    protected void insertdata5()
    {
        string st2 = " INSERT INTO TaskMaster (TaskName,ProjectId,Eunitsalloted,Isdelete,WeeklygoalId,Whid,Compid) " +
                              "  Values ('" + txttaskname5.Text + "','" + DropDownList2.SelectedValue + "','" + txtbudgetedminute5.Text + "','false','" + DropDownList1.SelectedValue + "','" + ddlStore.SelectedValue + "','" + Session["Comid"].ToString() + "') ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();

        string str123 = "select Max(TaskId) as TaskId from TaskMaster ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        ViewState["MaxTaskId5"] = ds123.Rows[0]["TaskId"].ToString();

        string sttbldata = "select * from TempTaskDescription where TableRow='5' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable dstabledata = new DataTable();
        SqlDataAdapter datbldata = new SqlDataAdapter(sttbldata, con);
        datbldata.Fill(dstabledata);


        ViewState["TaskDescription5"] = "";
        if (dstabledata.Rows.Count > 0)
        {
            ViewState["TaskDescription5"] = dstabledata.Rows[0]["Description"].ToString();
        }
        string stallocation = "";

        if (Panel5.Visible == true && ddlnewtaskempname.SelectedIndex > 0)
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId5"] + "','" + ddlnewtaskempname.SelectedValue + "','" + txteenddate5.Text + "','" + txtbudgetedminute5.Text + "','" + ViewState["TaskDescription5"] + "') ";
        }
        else
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId5"] + "','0','" + txteenddate5.Text + "','" + txtbudgetedminute5.Text + "','" + ViewState["TaskDescription5"] + "') ";
        }
        SqlCommand cmdallocation = new SqlCommand(stallocation, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdallocation.ExecuteNonQuery();
        con.Close();


    }
    protected void insertdata6()
    {
        string st2 = " INSERT INTO TaskMaster (TaskName,ProjectId,Eunitsalloted,Isdelete,WeeklygoalId,Whid,Compid) " +
                              "  Values ('" + txttaskname6.Text + "','" + DropDownList2.SelectedValue + "','" + txtbudgetedminute6.Text + "','false','" + DropDownList1.SelectedValue + "','" + ddlStore.SelectedValue + "','" + Session["Comid"].ToString() + "') ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();

        string str123 = "select Max(TaskId) as TaskId from TaskMaster ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        ViewState["MaxTaskId6"] = ds123.Rows[0]["TaskId"].ToString();

        string sttbldata = "select * from TempTaskDescription where TableRow='6' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable dstabledata = new DataTable();
        SqlDataAdapter datbldata = new SqlDataAdapter(sttbldata, con);
        datbldata.Fill(dstabledata);

        ViewState["TaskDescription6"] = "";
        if (dstabledata.Rows.Count > 0)
        {
            ViewState["TaskDescription6"] = dstabledata.Rows[0]["Description"].ToString();
        }
        string stallocation = "";

        if (Panel5.Visible == true && ddlnewtaskempname.SelectedIndex > 0)
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId6"] + "','" + ddlnewtaskempname.SelectedValue + "','" + txteenddate6.Text + "','" + txtbudgetedminute6.Text + "','" + ViewState["TaskDescription6"] + "') ";
        }
        else
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId6"] + "','0','" + txteenddate6.Text + "','" + txtbudgetedminute6.Text + "','" + ViewState["TaskDescription6"] + "') ";
        }
        SqlCommand cmdallocation = new SqlCommand(stallocation, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdallocation.ExecuteNonQuery();
        con.Close();


    }
    protected void insertdata7()
    {
        string st2 = " INSERT INTO TaskMaster (TaskName,ProjectId,Eunitsalloted,Isdelete,WeeklygoalId,Whid,Compid) " +
                              "  Values ('" + txttaskname7.Text + "','" + DropDownList2.SelectedValue + "','" + txtbudgetedminute7.Text + "','false','" + DropDownList1.SelectedValue + "','" + ddlStore.SelectedValue + "','" + Session["Comid"].ToString() + "') ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();

        string str123 = "select Max(TaskId) as TaskId from TaskMaster ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        ViewState["MaxTaskId7"] = ds123.Rows[0]["TaskId"].ToString();

        string sttbldata = "select * from TempTaskDescription where TableRow='7' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable dstabledata = new DataTable();
        SqlDataAdapter datbldata = new SqlDataAdapter(sttbldata, con);
        datbldata.Fill(dstabledata);

        ViewState["TaskDescription7"] = "";
        if (dstabledata.Rows.Count > 0)
        {
            ViewState["TaskDescription7"] = dstabledata.Rows[0]["Description"].ToString();
        }
        string stallocation = "";

        if (Panel5.Visible == true && ddlnewtaskempname.SelectedIndex > 0)
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId7"] + "','" + ddlnewtaskempname.SelectedValue + "','" + txteenddate7.Text + "','" + txtbudgetedminute7.Text + "','" + ViewState["TaskDescription7"] + "') ";
        }
        else
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId7"] + "','0','" + txteenddate7.Text + "','" + txtbudgetedminute7.Text + "','" + ViewState["TaskDescription7"] + "') ";
        }
        SqlCommand cmdallocation = new SqlCommand(stallocation, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdallocation.ExecuteNonQuery();
        con.Close();


    }
    protected void insertdata8()
    {
        string st2 = " INSERT INTO TaskMaster (TaskName,ProjectId,Eunitsalloted,Isdelete,WeeklygoalId,Whid,Compid) " +
                              "  Values ('" + txttaskname8.Text + "','" + DropDownList2.SelectedValue + "','" + txtbudgetedminute8.Text + "','false','" + DropDownList1.SelectedValue + "','" + ddlStore.SelectedValue + "','" + Session["Comid"].ToString() + "') ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();

        string str123 = "select Max(TaskId) as TaskId from TaskMaster ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        ViewState["MaxTaskId8"] = ds123.Rows[0]["TaskId"].ToString();

        string sttbldata = "select * from TempTaskDescription where TableRow='8' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable dstabledata = new DataTable();
        SqlDataAdapter datbldata = new SqlDataAdapter(sttbldata, con);
        datbldata.Fill(dstabledata);

        ViewState["TaskDescription8"] = "";
        if (dstabledata.Rows.Count > 0)
        {
            ViewState["TaskDescription8"] = dstabledata.Rows[0]["Description"].ToString();
        }

        string stallocation = "";

        if (Panel5.Visible == true && ddlnewtaskempname.SelectedIndex > 0)
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId8"] + "','" + ddlnewtaskempname.SelectedValue + "','" + txteenddate8.Text + "','" + txtbudgetedminute8.Text + "','" + ViewState["TaskDescription8"] + "') ";
        }
        else
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId8"] + "','0','" + txteenddate8.Text + "','" + txtbudgetedminute8.Text + "','" + ViewState["TaskDescription8"] + "') ";
        }
        SqlCommand cmdallocation = new SqlCommand(stallocation, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdallocation.ExecuteNonQuery();
        con.Close();


    }
    protected void insertdata9()
    {
        string st2 = " INSERT INTO TaskMaster (TaskName,ProjectId,Eunitsalloted,Isdelete,WeeklygoalId,Whid,Compid) " +
                              "  Values ('" + txttaskname9.Text + "','" + DropDownList2.SelectedValue + "','" + txtbudgetedminute9.Text + "','false','" + DropDownList1.SelectedValue + "','" + ddlStore.SelectedValue + "','" + Session["Comid"].ToString() + "') ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();

        string str123 = "select Max(TaskId) as TaskId from TaskMaster ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        ViewState["MaxTaskId9"] = ds123.Rows[0]["TaskId"].ToString();

        string sttbldata = "select * from TempTaskDescription where TableRow='9' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable dstabledata = new DataTable();
        SqlDataAdapter datbldata = new SqlDataAdapter(sttbldata, con);
        datbldata.Fill(dstabledata);

        ViewState["TaskDescription9"] = "";
        if (dstabledata.Rows.Count > 0)
        {
            ViewState["TaskDescription9"] = dstabledata.Rows[0]["Description"].ToString();
        }

        string stallocation = "";

        if (Panel5.Visible == true && ddlnewtaskempname.SelectedIndex > 0)
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId9"] + "','" + ddlnewtaskempname.SelectedValue + "','" + txteenddate9.Text + "','" + txtbudgetedminute9.Text + "','" + ViewState["TaskDescription9"] + "') ";
        }
        else
        {
            stallocation = " INSERT INTO TaskAllocationMaster (TaskId,EmployeeId,TaskAllocationDate,EUnitsAlloted,TaskName) values ('" + ViewState["MaxTaskId9"] + "','0','" + txteenddate9.Text + "','" + txtbudgetedminute9.Text + "','" + ViewState["TaskDescription9"] + "') ";
        }
        SqlCommand cmdallocation = new SqlCommand(stallocation, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdallocation.ExecuteNonQuery();
        con.Close();


    }

    protected void fillinsertdocument()
    {
        string str123 = "select * from DocumentMasterTempforTask where TableRowTaskId='1' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        if (ds123.Rows.Count > 0)
        {
            foreach (DataRow dr in ds123.Rows)
            {
                ViewState["DocumentTypeId"] = ds123.Rows[0]["DocumentTypeId"].ToString();

                ViewState["DocumentUploadDate"] = ds123.Rows[0]["DocumentUploadDate"].ToString();
                ViewState["DocumentName"] = ds123.Rows[0]["DocumentName"].ToString();

                ViewState["DocumentTitle"] = ds123.Rows[0]["DocumentTitle"].ToString();

                ViewState["Description"] = ds123.Rows[0]["Description"].ToString();
                ViewState["PartyId"] = ds123.Rows[0]["PartyId"].ToString();
                ViewState["DocumentRefNo"] = ds123.Rows[0]["DocumentRefNo"].ToString();
                ViewState["DocumentAmount"] = ds123.Rows[0]["DocumentAmount"].ToString();
                ViewState["DocumentDate"] = ds123.Rows[0]["DocumentDate"].ToString();
                ViewState["FileExtensionType"] = ds123.Rows[0]["FileExtensionType"].ToString();
                ViewState["CID"] = ds123.Rows[0]["CID"].ToString();


                string dctr = "1";

                SqlDataAdapter dass = new SqlDataAdapter("select id from DocumentTypenm where name='Document' and Active='1'", con);
                DataTable dtss = new DataTable();
                dass.Fill(dtss);

                if (dtss.Rows.Count > 0)
                {
                    dctr = Convert.ToString(dtss.Rows[0]["id"]);
                }

                Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(ViewState["DocumentTypeId"].ToString()), 2, Convert.ToDateTime(ViewState["DocumentUploadDate"]), ViewState["DocumentName"].ToString(), ViewState["DocumentTitle"].ToString(), "", Convert.ToInt32(ViewState["PartyId"].ToString()), ViewState["DocumentRefNo"].ToString(), Convert.ToDecimal(ViewState["DocumentAmount"].ToString()), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(ViewState["DocumentDate"].ToString()), ViewState["FileExtensionType"].ToString(), dctr, "");

                if (rst > 0)
                {

                    bool dcaprv = true;
                    bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);


                    int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]),
                   Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);


                    string str12 = "Insert into OfficeManagerDocuments(DocumentId,StoreId,taskid) values ('" + rst + "','" + ddlStore.SelectedValue + "','" + ViewState["MaxTaskId1"] + "')";
                    SqlCommand cmd12 = new SqlCommand(str12, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();

                }

                int ii = 0;
                string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + ViewState["DocumentName"]);
                using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                {
                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                    MatchCollection match = regex.Matches(st.ReadToEnd());
                    ii = match.Count;
                }

                int length = ViewState["DocumentName"].ToString().Length;
                string docnameIn = ViewState["DocumentName"].ToString().Substring(0, length - 4);


                for (int kk = 1; kk <= ii; kk++)
                {
                    string scpf = docnameIn;
                    if (kk >= 1 && kk < 10)
                    {
                        scpf = scpf + "0000" + kk + ".jpg";
                    }
                    else if (kk >= 10 && kk < 100)
                    {
                        scpf = scpf + "000" + kk + ".jpg";
                    }
                    else if (kk >= 100)
                    {
                        scpf = scpf + "00" + kk + ".jpg";
                    }

                    clsEmployee.InserDocumentImageMaster(rst, scpf);

                }

            }

        }

    }
    protected void fillinsertdocument2()
    {
        string str123 = "select * from DocumentMasterTempforTask where TableRowTaskId='2' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        if (ds123.Rows.Count > 0)
        {
            foreach (DataRow dr in ds123.Rows)
            {
                ViewState["DocumentTypeId"] = ds123.Rows[0]["DocumentTypeId"].ToString();

                ViewState["DocumentUploadDate"] = ds123.Rows[0]["DocumentUploadDate"].ToString();
                ViewState["DocumentName"] = ds123.Rows[0]["DocumentName"].ToString();

                ViewState["DocumentTitle"] = ds123.Rows[0]["DocumentTitle"].ToString();

                ViewState["Description"] = ds123.Rows[0]["Description"].ToString();
                ViewState["PartyId"] = ds123.Rows[0]["PartyId"].ToString();
                ViewState["DocumentRefNo"] = ds123.Rows[0]["DocumentRefNo"].ToString();
                ViewState["DocumentAmount"] = ds123.Rows[0]["DocumentAmount"].ToString();
                ViewState["DocumentDate"] = ds123.Rows[0]["DocumentDate"].ToString();
                ViewState["FileExtensionType"] = ds123.Rows[0]["FileExtensionType"].ToString();
                ViewState["CID"] = ds123.Rows[0]["CID"].ToString();


                string dctr = "1";

                SqlDataAdapter dass = new SqlDataAdapter("select id from DocumentTypenm where name='Document' and Active='1'", con);
                DataTable dtss = new DataTable();
                dass.Fill(dtss);

                if (dtss.Rows.Count > 0)
                {
                    dctr = Convert.ToString(dtss.Rows[0]["id"]);
                }

                Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(ViewState["DocumentTypeId"].ToString()), 2, Convert.ToDateTime(ViewState["DocumentUploadDate"]), ViewState["DocumentName"].ToString(), ViewState["DocumentTitle"].ToString(), "", Convert.ToInt32(ViewState["PartyId"].ToString()), ViewState["DocumentRefNo"].ToString(), Convert.ToDecimal(ViewState["DocumentAmount"].ToString()), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(ViewState["DocumentDate"].ToString()), ViewState["FileExtensionType"].ToString(), dctr, "");

                if (rst > 0)
                {

                    bool dcaprv = true;
                    bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);


                    int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]),
                   Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);


                    string str12 = "Insert into OfficeManagerDocuments(DocumentId,StoreId,taskid) values ('" + rst + "','" + ddlStore.SelectedValue + "','" + ViewState["MaxTaskId2"] + "')";
                    SqlCommand cmd12 = new SqlCommand(str12, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();

                }

                int ii = 0;
                string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + ViewState["DocumentName"]);
                using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                {
                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                    MatchCollection match = regex.Matches(st.ReadToEnd());
                    ii = match.Count;
                }

                int length = ViewState["DocumentName"].ToString().Length;
                string docnameIn = ViewState["DocumentName"].ToString().Substring(0, length - 4);


                for (int kk = 1; kk <= ii; kk++)
                {
                    string scpf = docnameIn;
                    if (kk >= 1 && kk < 10)
                    {
                        scpf = scpf + "0000" + kk + ".jpg";
                    }
                    else if (kk >= 10 && kk < 100)
                    {
                        scpf = scpf + "000" + kk + ".jpg";
                    }
                    else if (kk >= 100)
                    {
                        scpf = scpf + "00" + kk + ".jpg";
                    }

                    clsEmployee.InserDocumentImageMaster(rst, scpf);

                }

            }

        }

    }
    protected void fillinsertdocument3()
    {
        string str123 = "select * from DocumentMasterTempforTask where TableRowTaskId='3' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        if (ds123.Rows.Count > 0)
        {
            foreach (DataRow dr in ds123.Rows)
            {
                ViewState["DocumentTypeId"] = ds123.Rows[0]["DocumentTypeId"].ToString();

                ViewState["DocumentUploadDate"] = ds123.Rows[0]["DocumentUploadDate"].ToString();
                ViewState["DocumentName"] = ds123.Rows[0]["DocumentName"].ToString();

                ViewState["DocumentTitle"] = ds123.Rows[0]["DocumentTitle"].ToString();

                ViewState["Description"] = ds123.Rows[0]["Description"].ToString();
                ViewState["PartyId"] = ds123.Rows[0]["PartyId"].ToString();
                ViewState["DocumentRefNo"] = ds123.Rows[0]["DocumentRefNo"].ToString();
                ViewState["DocumentAmount"] = ds123.Rows[0]["DocumentAmount"].ToString();
                ViewState["DocumentDate"] = ds123.Rows[0]["DocumentDate"].ToString();
                ViewState["FileExtensionType"] = ds123.Rows[0]["FileExtensionType"].ToString();
                ViewState["CID"] = ds123.Rows[0]["CID"].ToString();


                string dctr = "1";

                SqlDataAdapter dass = new SqlDataAdapter("select id from DocumentTypenm where name='Document' and Active='1'", con);
                DataTable dtss = new DataTable();
                dass.Fill(dtss);

                if (dtss.Rows.Count > 0)
                {
                    dctr = Convert.ToString(dtss.Rows[0]["id"]);
                }

                Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(ViewState["DocumentTypeId"].ToString()), 2, Convert.ToDateTime(ViewState["DocumentUploadDate"]), ViewState["DocumentName"].ToString(), ViewState["DocumentTitle"].ToString(), "", Convert.ToInt32(ViewState["PartyId"].ToString()), ViewState["DocumentRefNo"].ToString(), Convert.ToDecimal(ViewState["DocumentAmount"].ToString()), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(ViewState["DocumentDate"].ToString()), ViewState["FileExtensionType"].ToString(), dctr, "");

                if (rst > 0)
                {

                    bool dcaprv = true;
                    bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);


                    int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]),
                   Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);


                    string str12 = "Insert into OfficeManagerDocuments(DocumentId,StoreId,taskid) values ('" + rst + "','" + ddlStore.SelectedValue + "','" + ViewState["MaxTaskId3"] + "')";
                    SqlCommand cmd12 = new SqlCommand(str12, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();

                }

                int ii = 0;
                string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + ViewState["DocumentName"]);
                using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                {
                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                    MatchCollection match = regex.Matches(st.ReadToEnd());
                    ii = match.Count;
                }

                int length = ViewState["DocumentName"].ToString().Length;
                string docnameIn = ViewState["DocumentName"].ToString().Substring(0, length - 4);


                for (int kk = 1; kk <= ii; kk++)
                {
                    string scpf = docnameIn;
                    if (kk >= 1 && kk < 10)
                    {
                        scpf = scpf + "0000" + kk + ".jpg";
                    }
                    else if (kk >= 10 && kk < 100)
                    {
                        scpf = scpf + "000" + kk + ".jpg";
                    }
                    else if (kk >= 100)
                    {
                        scpf = scpf + "00" + kk + ".jpg";
                    }

                    clsEmployee.InserDocumentImageMaster(rst, scpf);

                }

            }

        }

    }
    protected void fillinsertdocument4()
    {
        string str123 = "select * from DocumentMasterTempforTask where TableRowTaskId='4' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        if (ds123.Rows.Count > 0)
        {
            foreach (DataRow dr in ds123.Rows)
            {
                ViewState["DocumentTypeId"] = ds123.Rows[0]["DocumentTypeId"].ToString();

                ViewState["DocumentUploadDate"] = ds123.Rows[0]["DocumentUploadDate"].ToString();
                ViewState["DocumentName"] = ds123.Rows[0]["DocumentName"].ToString();

                ViewState["DocumentTitle"] = ds123.Rows[0]["DocumentTitle"].ToString();

                ViewState["Description"] = ds123.Rows[0]["Description"].ToString();
                ViewState["PartyId"] = ds123.Rows[0]["PartyId"].ToString();
                ViewState["DocumentRefNo"] = ds123.Rows[0]["DocumentRefNo"].ToString();
                ViewState["DocumentAmount"] = ds123.Rows[0]["DocumentAmount"].ToString();
                ViewState["DocumentDate"] = ds123.Rows[0]["DocumentDate"].ToString();
                ViewState["FileExtensionType"] = ds123.Rows[0]["FileExtensionType"].ToString();
                ViewState["CID"] = ds123.Rows[0]["CID"].ToString();


                string dctr = "1";

                SqlDataAdapter dass = new SqlDataAdapter("select id from DocumentTypenm where name='Document' and Active='1'", con);
                DataTable dtss = new DataTable();
                dass.Fill(dtss);

                if (dtss.Rows.Count > 0)
                {
                    dctr = Convert.ToString(dtss.Rows[0]["id"]);
                }

                Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(ViewState["DocumentTypeId"].ToString()), 2, Convert.ToDateTime(ViewState["DocumentUploadDate"]), ViewState["DocumentName"].ToString(), ViewState["DocumentTitle"].ToString(), "", Convert.ToInt32(ViewState["PartyId"].ToString()), ViewState["DocumentRefNo"].ToString(), Convert.ToDecimal(ViewState["DocumentAmount"].ToString()), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(ViewState["DocumentDate"].ToString()), ViewState["FileExtensionType"].ToString(), dctr, "");

                if (rst > 0)
                {

                    bool dcaprv = true;
                    bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);


                    int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]),
                   Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);


                    string str12 = "Insert into OfficeManagerDocuments(DocumentId,StoreId,taskid) values ('" + rst + "','" + ddlStore.SelectedValue + "','" + ViewState["MaxTaskId4"] + "')";
                    SqlCommand cmd12 = new SqlCommand(str12, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();

                }

                int ii = 0;
                string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + ViewState["DocumentName"]);
                using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                {
                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                    MatchCollection match = regex.Matches(st.ReadToEnd());
                    ii = match.Count;
                }

                int length = ViewState["DocumentName"].ToString().Length;
                string docnameIn = ViewState["DocumentName"].ToString().Substring(0, length - 4);


                for (int kk = 1; kk <= ii; kk++)
                {
                    string scpf = docnameIn;
                    if (kk >= 1 && kk < 10)
                    {
                        scpf = scpf + "0000" + kk + ".jpg";
                    }
                    else if (kk >= 10 && kk < 100)
                    {
                        scpf = scpf + "000" + kk + ".jpg";
                    }
                    else if (kk >= 100)
                    {
                        scpf = scpf + "00" + kk + ".jpg";
                    }

                    clsEmployee.InserDocumentImageMaster(rst, scpf);

                }

            }

        }

    }
    protected void fillinsertdocument5()
    {
        string str123 = "select * from DocumentMasterTempforTask where TableRowTaskId='5' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        if (ds123.Rows.Count > 0)
        {
            foreach (DataRow dr in ds123.Rows)
            {
                ViewState["DocumentTypeId"] = ds123.Rows[0]["DocumentTypeId"].ToString();

                ViewState["DocumentUploadDate"] = ds123.Rows[0]["DocumentUploadDate"].ToString();
                ViewState["DocumentName"] = ds123.Rows[0]["DocumentName"].ToString();

                ViewState["DocumentTitle"] = ds123.Rows[0]["DocumentTitle"].ToString();

                ViewState["Description"] = ds123.Rows[0]["Description"].ToString();
                ViewState["PartyId"] = ds123.Rows[0]["PartyId"].ToString();
                ViewState["DocumentRefNo"] = ds123.Rows[0]["DocumentRefNo"].ToString();
                ViewState["DocumentAmount"] = ds123.Rows[0]["DocumentAmount"].ToString();
                ViewState["DocumentDate"] = ds123.Rows[0]["DocumentDate"].ToString();
                ViewState["FileExtensionType"] = ds123.Rows[0]["FileExtensionType"].ToString();
                ViewState["CID"] = ds123.Rows[0]["CID"].ToString();


                string dctr = "1";

                SqlDataAdapter dass = new SqlDataAdapter("select id from DocumentTypenm where name='Document' and Active='1'", con);
                DataTable dtss = new DataTable();
                dass.Fill(dtss);

                if (dtss.Rows.Count > 0)
                {
                    dctr = Convert.ToString(dtss.Rows[0]["id"]);
                }

                Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(ViewState["DocumentTypeId"].ToString()), 2, Convert.ToDateTime(ViewState["DocumentUploadDate"]), ViewState["DocumentName"].ToString(), ViewState["DocumentTitle"].ToString(), "", Convert.ToInt32(ViewState["PartyId"].ToString()), ViewState["DocumentRefNo"].ToString(), Convert.ToDecimal(ViewState["DocumentAmount"].ToString()), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(ViewState["DocumentDate"].ToString()), ViewState["FileExtensionType"].ToString(), dctr, "");

                if (rst > 0)
                {

                    bool dcaprv = true;
                    bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);


                    int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]),
                   Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);


                    string str12 = "Insert into OfficeManagerDocuments(DocumentId,StoreId,taskid) values ('" + rst + "','" + ddlStore.SelectedValue + "','" + ViewState["MaxTaskId5"] + "')";
                    SqlCommand cmd12 = new SqlCommand(str12, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();

                }

                int ii = 0;
                string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + ViewState["DocumentName"]);
                using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                {
                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                    MatchCollection match = regex.Matches(st.ReadToEnd());
                    ii = match.Count;
                }

                int length = ViewState["DocumentName"].ToString().Length;
                string docnameIn = ViewState["DocumentName"].ToString().Substring(0, length - 4);


                for (int kk = 1; kk <= ii; kk++)
                {
                    string scpf = docnameIn;
                    if (kk >= 1 && kk < 10)
                    {
                        scpf = scpf + "0000" + kk + ".jpg";
                    }
                    else if (kk >= 10 && kk < 100)
                    {
                        scpf = scpf + "000" + kk + ".jpg";
                    }
                    else if (kk >= 100)
                    {
                        scpf = scpf + "00" + kk + ".jpg";
                    }

                    clsEmployee.InserDocumentImageMaster(rst, scpf);

                }

            }

        }

    }
    protected void fillinsertdocument6()
    {
        string str123 = "select * from DocumentMasterTempforTask where TableRowTaskId='6' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        if (ds123.Rows.Count > 0)
        {
            foreach (DataRow dr in ds123.Rows)
            {
                ViewState["DocumentTypeId"] = ds123.Rows[0]["DocumentTypeId"].ToString();

                ViewState["DocumentUploadDate"] = ds123.Rows[0]["DocumentUploadDate"].ToString();
                ViewState["DocumentName"] = ds123.Rows[0]["DocumentName"].ToString();

                ViewState["DocumentTitle"] = ds123.Rows[0]["DocumentTitle"].ToString();

                ViewState["Description"] = ds123.Rows[0]["Description"].ToString();
                ViewState["PartyId"] = ds123.Rows[0]["PartyId"].ToString();
                ViewState["DocumentRefNo"] = ds123.Rows[0]["DocumentRefNo"].ToString();
                ViewState["DocumentAmount"] = ds123.Rows[0]["DocumentAmount"].ToString();
                ViewState["DocumentDate"] = ds123.Rows[0]["DocumentDate"].ToString();
                ViewState["FileExtensionType"] = ds123.Rows[0]["FileExtensionType"].ToString();
                ViewState["CID"] = ds123.Rows[0]["CID"].ToString();


                string dctr = "1";

                SqlDataAdapter dass = new SqlDataAdapter("select id from DocumentTypenm where name='Document' and Active='1'", con);
                DataTable dtss = new DataTable();
                dass.Fill(dtss);

                if (dtss.Rows.Count > 0)
                {
                    dctr = Convert.ToString(dtss.Rows[0]["id"]);
                }

                Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(ViewState["DocumentTypeId"].ToString()), 2, Convert.ToDateTime(ViewState["DocumentUploadDate"]), ViewState["DocumentName"].ToString(), ViewState["DocumentTitle"].ToString(), "", Convert.ToInt32(ViewState["PartyId"].ToString()), ViewState["DocumentRefNo"].ToString(), Convert.ToDecimal(ViewState["DocumentAmount"].ToString()), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(ViewState["DocumentDate"].ToString()), ViewState["FileExtensionType"].ToString(), dctr, "");

                if (rst > 0)
                {

                    bool dcaprv = true;
                    bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);


                    int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]),
                   Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);


                    string str12 = "Insert into OfficeManagerDocuments(DocumentId,StoreId,taskid) values ('" + rst + "','" + ddlStore.SelectedValue + "','" + ViewState["MaxTaskId6"] + "')";
                    SqlCommand cmd12 = new SqlCommand(str12, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();

                }

                int ii = 0;
                string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + ViewState["DocumentName"]);
                using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                {
                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                    MatchCollection match = regex.Matches(st.ReadToEnd());
                    ii = match.Count;
                }

                int length = ViewState["DocumentName"].ToString().Length;
                string docnameIn = ViewState["DocumentName"].ToString().Substring(0, length - 4);


                for (int kk = 1; kk <= ii; kk++)
                {
                    string scpf = docnameIn;
                    if (kk >= 1 && kk < 10)
                    {
                        scpf = scpf + "0000" + kk + ".jpg";
                    }
                    else if (kk >= 10 && kk < 100)
                    {
                        scpf = scpf + "000" + kk + ".jpg";
                    }
                    else if (kk >= 100)
                    {
                        scpf = scpf + "00" + kk + ".jpg";
                    }

                    clsEmployee.InserDocumentImageMaster(rst, scpf);

                }

            }

        }

    }
    protected void fillinsertdocument7()
    {
        string str123 = "select * from DocumentMasterTempforTask where TableRowTaskId='7' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        if (ds123.Rows.Count > 0)
        {
            foreach (DataRow dr in ds123.Rows)
            {
                ViewState["DocumentTypeId"] = ds123.Rows[0]["DocumentTypeId"].ToString();

                ViewState["DocumentUploadDate"] = ds123.Rows[0]["DocumentUploadDate"].ToString();
                ViewState["DocumentName"] = ds123.Rows[0]["DocumentName"].ToString();

                ViewState["DocumentTitle"] = ds123.Rows[0]["DocumentTitle"].ToString();

                ViewState["Description"] = ds123.Rows[0]["Description"].ToString();
                ViewState["PartyId"] = ds123.Rows[0]["PartyId"].ToString();
                ViewState["DocumentRefNo"] = ds123.Rows[0]["DocumentRefNo"].ToString();
                ViewState["DocumentAmount"] = ds123.Rows[0]["DocumentAmount"].ToString();
                ViewState["DocumentDate"] = ds123.Rows[0]["DocumentDate"].ToString();
                ViewState["FileExtensionType"] = ds123.Rows[0]["FileExtensionType"].ToString();
                ViewState["CID"] = ds123.Rows[0]["CID"].ToString();


                string dctr = "1";

                SqlDataAdapter dass = new SqlDataAdapter("select id from DocumentTypenm where name='Document' and Active='1'", con);
                DataTable dtss = new DataTable();
                dass.Fill(dtss);

                if (dtss.Rows.Count > 0)
                {
                    dctr = Convert.ToString(dtss.Rows[0]["id"]);
                }

                Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(ViewState["DocumentTypeId"].ToString()), 2, Convert.ToDateTime(ViewState["DocumentUploadDate"]), ViewState["DocumentName"].ToString(), ViewState["DocumentTitle"].ToString(), "", Convert.ToInt32(ViewState["PartyId"].ToString()), ViewState["DocumentRefNo"].ToString(), Convert.ToDecimal(ViewState["DocumentAmount"].ToString()), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(ViewState["DocumentDate"].ToString()), ViewState["FileExtensionType"].ToString(), dctr, "");

                if (rst > 0)
                {

                    bool dcaprv = true;
                    bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);


                    int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]),
                   Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);


                    string str12 = "Insert into OfficeManagerDocuments(DocumentId,StoreId,taskid) values ('" + rst + "','" + ddlStore.SelectedValue + "','" + ViewState["MaxTaskId7"] + "')";
                    SqlCommand cmd12 = new SqlCommand(str12, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();

                }

                int ii = 0;
                string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + ViewState["DocumentName"]);
                using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                {
                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                    MatchCollection match = regex.Matches(st.ReadToEnd());
                    ii = match.Count;
                }

                int length = ViewState["DocumentName"].ToString().Length;
                string docnameIn = ViewState["DocumentName"].ToString().Substring(0, length - 4);


                for (int kk = 1; kk <= ii; kk++)
                {
                    string scpf = docnameIn;
                    if (kk >= 1 && kk < 10)
                    {
                        scpf = scpf + "0000" + kk + ".jpg";
                    }
                    else if (kk >= 10 && kk < 100)
                    {
                        scpf = scpf + "000" + kk + ".jpg";
                    }
                    else if (kk >= 100)
                    {
                        scpf = scpf + "00" + kk + ".jpg";
                    }

                    clsEmployee.InserDocumentImageMaster(rst, scpf);

                }

            }

        }

    }
    protected void fillinsertdocument8()
    {
        string str123 = "select * from DocumentMasterTempforTask where TableRowTaskId='8' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        if (ds123.Rows.Count > 0)
        {
            foreach (DataRow dr in ds123.Rows)
            {
                ViewState["DocumentTypeId"] = ds123.Rows[0]["DocumentTypeId"].ToString();

                ViewState["DocumentUploadDate"] = ds123.Rows[0]["DocumentUploadDate"].ToString();
                ViewState["DocumentName"] = ds123.Rows[0]["DocumentName"].ToString();

                ViewState["DocumentTitle"] = ds123.Rows[0]["DocumentTitle"].ToString();

                ViewState["Description"] = ds123.Rows[0]["Description"].ToString();
                ViewState["PartyId"] = ds123.Rows[0]["PartyId"].ToString();
                ViewState["DocumentRefNo"] = ds123.Rows[0]["DocumentRefNo"].ToString();
                ViewState["DocumentAmount"] = ds123.Rows[0]["DocumentAmount"].ToString();
                ViewState["DocumentDate"] = ds123.Rows[0]["DocumentDate"].ToString();
                ViewState["FileExtensionType"] = ds123.Rows[0]["FileExtensionType"].ToString();
                ViewState["CID"] = ds123.Rows[0]["CID"].ToString();


                string dctr = "1";

                SqlDataAdapter dass = new SqlDataAdapter("select id from DocumentTypenm where name='Document' and Active='1'", con);
                DataTable dtss = new DataTable();
                dass.Fill(dtss);

                if (dtss.Rows.Count > 0)
                {
                    dctr = Convert.ToString(dtss.Rows[0]["id"]);
                }

                Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(ViewState["DocumentTypeId"].ToString()), 2, Convert.ToDateTime(ViewState["DocumentUploadDate"]), ViewState["DocumentName"].ToString(), ViewState["DocumentTitle"].ToString(), "", Convert.ToInt32(ViewState["PartyId"].ToString()), ViewState["DocumentRefNo"].ToString(), Convert.ToDecimal(ViewState["DocumentAmount"].ToString()), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(ViewState["DocumentDate"].ToString()), ViewState["FileExtensionType"].ToString(), dctr, "");

                if (rst > 0)
                {

                    bool dcaprv = true;
                    bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);


                    int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]),
                   Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);


                    string str12 = "Insert into OfficeManagerDocuments(DocumentId,StoreId,taskid) values ('" + rst + "','" + ddlStore.SelectedValue + "','" + ViewState["MaxTaskId8"] + "')";
                    SqlCommand cmd12 = new SqlCommand(str12, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();

                }

                int ii = 0;
                string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + ViewState["DocumentName"]);
                using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                {
                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                    MatchCollection match = regex.Matches(st.ReadToEnd());
                    ii = match.Count;
                }

                int length = ViewState["DocumentName"].ToString().Length;
                string docnameIn = ViewState["DocumentName"].ToString().Substring(0, length - 4);


                for (int kk = 1; kk <= ii; kk++)
                {
                    string scpf = docnameIn;
                    if (kk >= 1 && kk < 10)
                    {
                        scpf = scpf + "0000" + kk + ".jpg";
                    }
                    else if (kk >= 10 && kk < 100)
                    {
                        scpf = scpf + "000" + kk + ".jpg";
                    }
                    else if (kk >= 100)
                    {
                        scpf = scpf + "00" + kk + ".jpg";
                    }

                    clsEmployee.InserDocumentImageMaster(rst, scpf);

                }

            }

        }

    }
    protected void fillinsertdocument9()
    {
        string str123 = "select * from DocumentMasterTempforTask where TableRowTaskId='9' and UserId='" + Convert.ToString(Session["UserId"]) + "' ";
        DataTable ds123 = new DataTable();
        SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
        da123.Fill(ds123);

        if (ds123.Rows.Count > 0)
        {
            foreach (DataRow dr in ds123.Rows)
            {
                ViewState["DocumentTypeId"] = ds123.Rows[0]["DocumentTypeId"].ToString();

                ViewState["DocumentUploadDate"] = ds123.Rows[0]["DocumentUploadDate"].ToString();
                ViewState["DocumentName"] = ds123.Rows[0]["DocumentName"].ToString();

                ViewState["DocumentTitle"] = ds123.Rows[0]["DocumentTitle"].ToString();

                ViewState["Description"] = ds123.Rows[0]["Description"].ToString();
                ViewState["PartyId"] = ds123.Rows[0]["PartyId"].ToString();
                ViewState["DocumentRefNo"] = ds123.Rows[0]["DocumentRefNo"].ToString();
                ViewState["DocumentAmount"] = ds123.Rows[0]["DocumentAmount"].ToString();
                ViewState["DocumentDate"] = ds123.Rows[0]["DocumentDate"].ToString();
                ViewState["FileExtensionType"] = ds123.Rows[0]["FileExtensionType"].ToString();
                ViewState["CID"] = ds123.Rows[0]["CID"].ToString();


                string dctr = "1";

                SqlDataAdapter dass = new SqlDataAdapter("select id from DocumentTypenm where name='Document' and Active='1'", con);
                DataTable dtss = new DataTable();
                dass.Fill(dtss);

                if (dtss.Rows.Count > 0)
                {
                    dctr = Convert.ToString(dtss.Rows[0]["id"]);
                }

                Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(ViewState["DocumentTypeId"].ToString()), 2, Convert.ToDateTime(ViewState["DocumentUploadDate"]), ViewState["DocumentName"].ToString(), ViewState["DocumentTitle"].ToString(), "", Convert.ToInt32(ViewState["PartyId"].ToString()), ViewState["DocumentRefNo"].ToString(), Convert.ToDecimal(ViewState["DocumentAmount"].ToString()), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(ViewState["DocumentDate"].ToString()), ViewState["FileExtensionType"].ToString(), dctr, "");

                if (rst > 0)
                {

                    bool dcaprv = true;
                    bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);


                    int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]),
                   Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);


                    string str12 = "Insert into OfficeManagerDocuments(DocumentId,StoreId,taskid) values ('" + rst + "','" + ddlStore.SelectedValue + "','" + ViewState["MaxTaskId9"] + "')";
                    SqlCommand cmd12 = new SqlCommand(str12, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();

                }

                int ii = 0;
                string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + ViewState["DocumentName"]);
                using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                {
                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                    MatchCollection match = regex.Matches(st.ReadToEnd());
                    ii = match.Count;
                }

                int length = ViewState["DocumentName"].ToString().Length;
                string docnameIn = ViewState["DocumentName"].ToString().Substring(0, length - 4);


                for (int kk = 1; kk <= ii; kk++)
                {
                    string scpf = docnameIn;
                    if (kk >= 1 && kk < 10)
                    {
                        scpf = scpf + "0000" + kk + ".jpg";
                    }
                    else if (kk >= 10 && kk < 100)
                    {
                        scpf = scpf + "000" + kk + ".jpg";
                    }
                    else if (kk >= 100)
                    {
                        scpf = scpf + "00" + kk + ".jpg";
                    }

                    clsEmployee.InserDocumentImageMaster(rst, scpf);

                }

            }

        }

    }



    //protected void fillprojectbusi()
    //{
    //    ddlproject.Items.Clear();
    //    string tempstring = "select (Left(WareHouseMaster.Name,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=ProjectMaster.whid where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by WareHouseMaster.Name,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject.DataSource = ds12;
    //        ddlproject.DataTextField = "ProjectName";
    //        ddlproject.DataValueField = "ProjectId";
    //        ddlproject.DataBind();
    //    }
    //    ddlproject.Items.Insert(0, "-Select-");
    //    ddlproject.Items[0].Value = "0";
    //}

    //protected void fillprojectbusi2()
    //{
    //    ddlproject2.Items.Clear();
    //    string tempstring = "select (Left(WareHouseMaster.Name,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=ProjectMaster.whid where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by WareHouseMaster.Name,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject2.DataSource = ds12;
    //        ddlproject2.DataTextField = "ProjectName";
    //        ddlproject2.DataValueField = "ProjectId";
    //        ddlproject2.DataBind();
    //    }
    //    ddlproject2.Items.Insert(0, "-Select-");
    //    ddlproject2.Items[0].Value = "0";
    //}

    //protected void fillprojectbusi3()
    //{
    //    ddlproject3.Items.Clear();
    //    string tempstring = "select (Left(WareHouseMaster.Name,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=ProjectMaster.whid where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by WareHouseMaster.Name,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject3.DataSource = ds12;
    //        ddlproject3.DataTextField = "ProjectName";
    //        ddlproject3.DataValueField = "ProjectId";
    //        ddlproject3.DataBind();
    //    }
    //    ddlproject3.Items.Insert(0, "-Select-");
    //    ddlproject3.Items[0].Value = "0";
    //}

    //protected void fillprojectbusi4()
    //{
    //    ddlproject4.Items.Clear();
    //    string tempstring = "select (Left(WareHouseMaster.Name,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=ProjectMaster.whid where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by WareHouseMaster.Name,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject4.DataSource = ds12;
    //        ddlproject4.DataTextField = "ProjectName";
    //        ddlproject4.DataValueField = "ProjectId";
    //        ddlproject4.DataBind();
    //    }
    //    ddlproject4.Items.Insert(0, "-Select-");
    //    ddlproject4.Items[0].Value = "0";
    //}

    //protected void fillprojectbusi5()
    //{
    //    ddlproject5.Items.Clear();
    //    string tempstring = "select (Left(WareHouseMaster.Name,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=ProjectMaster.whid where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by WareHouseMaster.Name,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject5.DataSource = ds12;
    //        ddlproject5.DataTextField = "ProjectName";
    //        ddlproject5.DataValueField = "ProjectId";
    //        ddlproject5.DataBind();
    //    }
    //    ddlproject5.Items.Insert(0, "-Select-");
    //    ddlproject5.Items[0].Value = "0";
    //}

    //protected void fillprojectbusi6()
    //{
    //    ddlproject6.Items.Clear();
    //    string tempstring = "select (Left(WareHouseMaster.Name,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=ProjectMaster.whid where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by WareHouseMaster.Name,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject6.DataSource = ds12;
    //        ddlproject6.DataTextField = "ProjectName";
    //        ddlproject6.DataValueField = "ProjectId";
    //        ddlproject6.DataBind();
    //    }
    //    ddlproject6.Items.Insert(0, "-Select-");
    //    ddlproject6.Items[0].Value = "0";
    //}

    //protected void fillprojectbusi7()
    //{
    //    ddlproject7.Items.Clear();
    //    string tempstring = "select (Left(WareHouseMaster.Name,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=ProjectMaster.whid where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by WareHouseMaster.Name,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject7.DataSource = ds12;
    //        ddlproject7.DataTextField = "ProjectName";
    //        ddlproject7.DataValueField = "ProjectId";
    //        ddlproject7.DataBind();
    //    }
    //    ddlproject7.Items.Insert(0, "-Select-");
    //    ddlproject7.Items[0].Value = "0";
    //}

    //protected void fillprojectbusi8()
    //{
    //    ddlproject8.Items.Clear();
    //    string tempstring = "select (Left(WareHouseMaster.Name,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=ProjectMaster.whid where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by WareHouseMaster.Name,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject8.DataSource = ds12;
    //        ddlproject8.DataTextField = "ProjectName";
    //        ddlproject8.DataValueField = "ProjectId";
    //        ddlproject8.DataBind();
    //    }
    //    ddlproject8.Items.Insert(0, "-Select-");
    //    ddlproject8.Items[0].Value = "0";
    //}

    //protected void fillprojectbusi9()
    //{
    //    ddlproject9.Items.Clear();
    //    string tempstring = "select (Left(WareHouseMaster.Name,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=ProjectMaster.whid where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by WareHouseMaster.Name,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject9.DataSource = ds12;
    //        ddlproject9.DataTextField = "ProjectName";
    //        ddlproject9.DataValueField = "ProjectId";
    //        ddlproject9.DataBind();
    //    }
    //    ddlproject9.Items.Insert(0, "-Select-");
    //    ddlproject9.Items[0].Value = "0";
    //}


    protected void fillprojectdept()
    {
        DropDownList2.Items.Clear();

        string tempstring = "select (Left(DepartmentmasterMNC.Departmentname,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=ProjectMaster.DeptId where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.DeptId='" + ddldepartment.SelectedValue + "' and projectmaster.businessid='0' and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

        string order = " Order by DepartmentmasterMNC.Departmentname,ProjectMaster.ProjectName ";
        string str12 = tempstring + order;
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        if (ds12.Rows.Count > 0)
        {
            DropDownList2.DataSource = ds12;
            DropDownList2.DataTextField = "ProjectName";
            DropDownList2.DataValueField = "ProjectId";
            DropDownList2.DataBind();
        }
        DropDownList2.Items.Insert(0, "-Select-");
        DropDownList2.Items[0].Value = "0";
    }

    //protected void fillprojectdept2()
    //{
    //    ddlproject2.Items.Clear();
    //    string tempstring = "select (Left(DepartmentmasterMNC.Departmentname,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=ProjectMaster.DeptId where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.DeptId='" + ddldepartment.SelectedValue + "' and projectmaster.businessid='0' and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by DepartmentmasterMNC.Departmentname,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject2.DataSource = ds12;
    //        ddlproject2.DataTextField = "ProjectName";
    //        ddlproject2.DataValueField = "ProjectId";
    //        ddlproject2.DataBind();
    //    }
    //    ddlproject2.Items.Insert(0, "-Select-");
    //    ddlproject2.Items[0].Value = "0";
    //}

    //protected void fillprojectdept3()
    //{
    //    ddlproject3.Items.Clear();
    //    string tempstring = "select (Left(DepartmentmasterMNC.Departmentname,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=ProjectMaster.DeptId where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.DeptId='" + ddldepartment.SelectedValue + "' and projectmaster.businessid='0' and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by DepartmentmasterMNC.Departmentname,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject3.DataSource = ds12;
    //        ddlproject3.DataTextField = "ProjectName";
    //        ddlproject3.DataValueField = "ProjectId";
    //        ddlproject3.DataBind();
    //    }
    //    ddlproject3.Items.Insert(0, "-Select-");
    //    ddlproject3.Items[0].Value = "0";
    //}

    //protected void fillprojectdept4()
    //{
    //    ddlproject4.Items.Clear();
    //    string tempstring = "select (Left(DepartmentmasterMNC.Departmentname,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=ProjectMaster.DeptId where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.DeptId='" + ddldepartment.SelectedValue + "' and projectmaster.businessid='0' and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by DepartmentmasterMNC.Departmentname,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject4.DataSource = ds12;
    //        ddlproject4.DataTextField = "ProjectName";
    //        ddlproject4.DataValueField = "ProjectId";
    //        ddlproject4.DataBind();
    //    }
    //    ddlproject4.Items.Insert(0, "-Select-");
    //    ddlproject4.Items[0].Value = "0";
    //}

    //protected void fillprojectdept5()
    //{
    //    ddlproject5.Items.Clear();
    //    string tempstring = "select (Left(DepartmentmasterMNC.Departmentname,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=ProjectMaster.DeptId where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.DeptId='" + ddldepartment.SelectedValue + "' and projectmaster.businessid='0' and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by DepartmentmasterMNC.Departmentname,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject5.DataSource = ds12;
    //        ddlproject5.DataTextField = "ProjectName";
    //        ddlproject5.DataValueField = "ProjectId";
    //        ddlproject5.DataBind();
    //    }
    //    ddlproject5.Items.Insert(0, "-Select-");
    //    ddlproject5.Items[0].Value = "0";
    //}

    //protected void fillprojectdept6()
    //{
    //    ddlproject6.Items.Clear();
    //    string tempstring = "select (Left(DepartmentmasterMNC.Departmentname,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=ProjectMaster.DeptId where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.DeptId='" + ddldepartment.SelectedValue + "' and projectmaster.businessid='0' and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by DepartmentmasterMNC.Departmentname,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject6.DataSource = ds12;
    //        ddlproject6.DataTextField = "ProjectName";
    //        ddlproject6.DataValueField = "ProjectId";
    //        ddlproject6.DataBind();
    //    }
    //    ddlproject6.Items.Insert(0, "-Select-");
    //    ddlproject6.Items[0].Value = "0";
    //}

    //protected void fillprojectdept7()
    //{
    //    ddlproject7.Items.Clear();
    //    string tempstring = "select (Left(DepartmentmasterMNC.Departmentname,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=ProjectMaster.DeptId where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.DeptId='" + ddldepartment.SelectedValue + "' and projectmaster.businessid='0' and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by DepartmentmasterMNC.Departmentname,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject7.DataSource = ds12;
    //        ddlproject7.DataTextField = "ProjectName";
    //        ddlproject7.DataValueField = "ProjectId";
    //        ddlproject7.DataBind();
    //    }
    //    ddlproject7.Items.Insert(0, "-Select-");
    //    ddlproject7.Items[0].Value = "0";
    //}

    //protected void fillprojectdept8()
    //{
    //    ddlproject8.Items.Clear();
    //    string tempstring = "select (Left(DepartmentmasterMNC.Departmentname,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=ProjectMaster.DeptId where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.DeptId='" + ddldepartment.SelectedValue + "' and projectmaster.businessid='0' and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by DepartmentmasterMNC.Departmentname,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject8.DataSource = ds12;
    //        ddlproject8.DataTextField = "ProjectName";
    //        ddlproject8.DataValueField = "ProjectId";
    //        ddlproject8.DataBind();
    //    }
    //    ddlproject8.Items.Insert(0, "-Select-");
    //    ddlproject8.Items[0].Value = "0";
    //}

    //protected void fillprojectdept9()
    //{
    //    ddlproject9.Items.Clear();
    //    string tempstring = "select (Left(DepartmentmasterMNC.Departmentname,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=ProjectMaster.DeptId where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.DeptId='" + ddldepartment.SelectedValue + "' and projectmaster.businessid='0' and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by DepartmentmasterMNC.Departmentname,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject9.DataSource = ds12;
    //        ddlproject9.DataTextField = "ProjectName";
    //        ddlproject9.DataValueField = "ProjectId";
    //        ddlproject9.DataBind();
    //    }
    //    ddlproject9.Items.Insert(0, "-Select-");
    //    ddlproject9.Items[0].Value = "0";
    //}


    protected void fillprojectdivi()
    {
        DropDownList2.Items.Clear();
        string tempstring = " select (Left(BusinessMaster.BusinessName,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join BusinessMaster on BusinessMaster.BusinessID=ProjectMaster.BusinessID where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and projectmaster.businessid='" + ddldivision.SelectedValue + "'  and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

        string order = " Order by BusinessMaster.BusinessName,ProjectMaster.ProjectName ";
        string str12 = tempstring + order;
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        if (ds12.Rows.Count > 0)
        {
            DropDownList2.DataSource = ds12;
            DropDownList2.DataTextField = "ProjectName";
            DropDownList2.DataValueField = "ProjectId";
            DropDownList2.DataBind();
        }
        DropDownList2.Items.Insert(0, "-Select-");
        DropDownList2.Items[0].Value = "0";
    }

    //protected void fillprojectdivi2()
    //{
    //    ddlproject2.Items.Clear();
    //    string tempstring = " select (Left(BusinessMaster.BusinessName,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join BusinessMaster on BusinessMaster.BusinessID=ProjectMaster.BusinessID where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and projectmaster.businessid='" + ddldivision.SelectedValue + "'  and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by BusinessMaster.BusinessName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject2.DataSource = ds12;
    //        ddlproject2.DataTextField = "ProjectName";
    //        ddlproject2.DataValueField = "ProjectId";
    //        ddlproject2.DataBind();
    //    }
    //    ddlproject2.Items.Insert(0, "-Select-");
    //    ddlproject2.Items[0].Value = "0";
    //}

    //protected void fillprojectdivi3()
    //{
    //    ddlproject3.Items.Clear();
    //    string tempstring = " select (Left(BusinessMaster.BusinessName,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join BusinessMaster on BusinessMaster.BusinessID=ProjectMaster.BusinessID where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and projectmaster.businessid='" + ddldivision.SelectedValue + "'  and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by BusinessMaster.BusinessName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject3.DataSource = ds12;
    //        ddlproject3.DataTextField = "ProjectName";
    //        ddlproject3.DataValueField = "ProjectId";
    //        ddlproject3.DataBind();
    //    }
    //    ddlproject3.Items.Insert(0, "-Select-");
    //    ddlproject3.Items[0].Value = "0";
    //}

    //protected void fillprojectdivi4()
    //{
    //    ddlproject4.Items.Clear();
    //    string tempstring = " select (Left(BusinessMaster.BusinessName,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join BusinessMaster on BusinessMaster.BusinessID=ProjectMaster.BusinessID where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and projectmaster.businessid='" + ddldivision.SelectedValue + "'  and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by BusinessMaster.BusinessName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject4.DataSource = ds12;
    //        ddlproject4.DataTextField = "ProjectName";
    //        ddlproject4.DataValueField = "ProjectId";
    //        ddlproject4.DataBind();
    //    }
    //    ddlproject4.Items.Insert(0, "-Select-");
    //    ddlproject4.Items[0].Value = "0";
    //}

    //protected void fillprojectdivi5()
    //{
    //    ddlproject5.Items.Clear();
    //    string tempstring = " select (Left(BusinessMaster.BusinessName,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join BusinessMaster on BusinessMaster.BusinessID=ProjectMaster.BusinessID where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and projectmaster.businessid='" + ddldivision.SelectedValue + "'  and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by BusinessMaster.BusinessName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject5.DataSource = ds12;
    //        ddlproject5.DataTextField = "ProjectName";
    //        ddlproject5.DataValueField = "ProjectId";
    //        ddlproject5.DataBind();
    //    }
    //    ddlproject5.Items.Insert(0, "-Select-");
    //    ddlproject5.Items[0].Value = "0";
    //}

    //protected void fillprojectdivi6()
    //{
    //    ddlproject6.Items.Clear();
    //    string tempstring = " select (Left(BusinessMaster.BusinessName,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join BusinessMaster on BusinessMaster.BusinessID=ProjectMaster.BusinessID where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and projectmaster.businessid='" + ddldivision.SelectedValue + "'  and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by BusinessMaster.BusinessName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject6.DataSource = ds12;
    //        ddlproject6.DataTextField = "ProjectName";
    //        ddlproject6.DataValueField = "ProjectId";
    //        ddlproject6.DataBind();
    //    }
    //    ddlproject6.Items.Insert(0, "-Select-");
    //    ddlproject6.Items[0].Value = "0";
    //}

    //protected void fillprojectdivi7()
    //{
    //    ddlproject7.Items.Clear();
    //    string tempstring = " select (Left(BusinessMaster.BusinessName,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join BusinessMaster on BusinessMaster.BusinessID=ProjectMaster.BusinessID where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and projectmaster.businessid='" + ddldivision.SelectedValue + "'  and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by BusinessMaster.BusinessName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject7.DataSource = ds12;
    //        ddlproject7.DataTextField = "ProjectName";
    //        ddlproject7.DataValueField = "ProjectId";
    //        ddlproject7.DataBind();
    //    }
    //    ddlproject7.Items.Insert(0, "-Select-");
    //    ddlproject7.Items[0].Value = "0";
    //}

    //protected void fillprojectdivi8()
    //{
    //    ddlproject8.Items.Clear();
    //    string tempstring = " select (Left(BusinessMaster.BusinessName,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join BusinessMaster on BusinessMaster.BusinessID=ProjectMaster.BusinessID where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and projectmaster.businessid='" + ddldivision.SelectedValue + "'  and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by BusinessMaster.BusinessName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject8.DataSource = ds12;
    //        ddlproject8.DataTextField = "ProjectName";
    //        ddlproject8.DataValueField = "ProjectId";
    //        ddlproject8.DataBind();
    //    }
    //    ddlproject8.Items.Insert(0, "-Select-");
    //    ddlproject8.Items[0].Value = "0";
    //}

    //protected void fillprojectdivi9()
    //{
    //    ddlproject9.Items.Clear();
    //    string tempstring = " select (Left(BusinessMaster.BusinessName,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join BusinessMaster on BusinessMaster.BusinessID=ProjectMaster.BusinessID where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and projectmaster.businessid='" + ddldivision.SelectedValue + "'  and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' ";

    //    string order = " Order by BusinessMaster.BusinessName,ProjectMaster.ProjectName ";
    //    string str12 = tempstring + order;
    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
    //    da12.Fill(ds12);
    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlproject9.DataSource = ds12;
    //        ddlproject9.DataTextField = "ProjectName";
    //        ddlproject9.DataValueField = "ProjectId";
    //        ddlproject9.DataBind();
    //    }
    //    ddlproject9.Items.Insert(0, "-Select-");
    //    ddlproject9.Items[0].Value = "0";
    //}

    //protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (RadioButtonList1.SelectedValue == "0")
    //    {
    //        fillemployee();
    //        ddlnewtaskempname_SelectedIndexChanged(sender, e);
    //        pnlemphide.Visible = true;
    //        Panel2.Visible = false;
    //        Panel3.Visible = false;
    //    }
    //    if (RadioButtonList1.SelectedValue == "1")
    //    {
    //        filldepartment();
    //        ddldepartment_SelectedIndexChanged(sender, e);
    //        pnlemphide.Visible = false;
    //        Panel2.Visible = true;
    //        Panel3.Visible = false;
    //    }
    //    if (RadioButtonList1.SelectedValue == "2")
    //    {
    //        filldivision();
    //        ddldivision_SelectedIndexChanged(sender, e);
    //        pnlemphide.Visible = false;
    //        Panel2.Visible = false;
    //        Panel3.Visible = true;
    //    }
    //}

    protected void filldepartment()
    {
        string str = "select distinct DepartmentmasterMNC.id,DepartmentmasterMNC.Departmentname as Departmentname from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where DepartmentmasterMNC.Whid='" + ddlStore.SelectedValue + "' order by Departmentname ";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            ddldepartment.DataSource = dt;
            ddldepartment.DataTextField = "Departmentname";
            ddldepartment.DataValueField = "id";
            ddldepartment.DataBind();
        }
    }

    protected void filldivision()
    {
        string str = "select distinct BusinessMaster.BusinessID,BusinessMaster.BusinessName from  BusinessMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=BusinessMaster.Whid where BusinessMaster.Whid='" + ddlStore.SelectedValue + "' order by BusinessName ";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            ddldivision.DataSource = dt;
            ddldivision.DataTextField = "BusinessName";
            ddldivision.DataValueField = "BusinessID";
            ddldivision.DataBind();
        }
    }
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillemployee();
        projet();
        weeklygoal();

        //fillprojectdept2();
        //fillprojectdept3();
        //fillprojectdept4();
        //fillprojectdept5();
        //fillprojectdept6();
        //fillprojectdept7();
        //fillprojectdept8();
        //fillprojectdept9();

        //fillweekdept2();
        //fillweekdept3();
        //fillweekdept4();
        //fillweekdept5();
        //fillweekdept6();
        //fillweekdept7();
        //fillweekdept8();
        //fillweekdept9();
    }
    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        projet();
        //fillprojectdivi2();
        //fillprojectdivi3();
        //fillprojectdivi4();
        //fillprojectdivi5();
        //fillprojectdivi6();
        //fillprojectdivi7();
        //fillprojectdivi8();
        //fillprojectdivi9();

        weeklygoal();
        //fillweekdivi2();
        //fillweekdivi3();
        //fillweekdivi4();
        //fillweekdivi5();
        //fillweekdivi6();
        //fillweekdivi7();
        //fillweekdivi8();
        //fillweekdivi9();
    }

    protected void fillweekdept()
    {
        DropDownList1.Items.Clear();

        string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  DepartmentmasterMNC on DepartmentmasterMNC.id=WMaster.DepartmentID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DepartmentID='" + ddldepartment.SelectedValue + "' and WMaster.DivisionID IS NULL and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
        da12.Fill(ds12);

        if (ds12.Rows.Count > 0)
        {
            DropDownList1.DataSource = ds12;
            DropDownList1.DataTextField = "Title";
            DropDownList1.DataValueField = "MasterId";
            DropDownList1.DataBind();
        }
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";
    }

    //protected void fillweekdept2()
    //{
    //    ddlweeklygoal2.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  DepartmentmasterMNC on DepartmentmasterMNC.id=WMaster.DepartmentID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DepartmentID='" + ddldepartment.SelectedValue + "' and WMaster.DivisionID IS NULL and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal2.DataSource = ds12;
    //        ddlweeklygoal2.DataTextField = "Title";
    //        ddlweeklygoal2.DataValueField = "MasterId";
    //        ddlweeklygoal2.DataBind();
    //    }
    //    ddlweeklygoal2.Items.Insert(0, "-Select-");
    //    ddlweeklygoal2.Items[0].Value = "0";
    //}

    //protected void fillweekdept3()
    //{
    //    ddlweeklygoal3.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  DepartmentmasterMNC on DepartmentmasterMNC.id=WMaster.DepartmentID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DepartmentID='" + ddldepartment.SelectedValue + "' and WMaster.DivisionID IS NULL and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal3.DataSource = ds12;
    //        ddlweeklygoal3.DataTextField = "Title";
    //        ddlweeklygoal3.DataValueField = "MasterId";
    //        ddlweeklygoal3.DataBind();
    //    }
    //    ddlweeklygoal3.Items.Insert(0, "-Select-");
    //    ddlweeklygoal3.Items[0].Value = "0";
    //}

    //protected void fillweekdept4()
    //{
    //    ddlweeklygoal4.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  DepartmentmasterMNC on DepartmentmasterMNC.id=WMaster.DepartmentID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DepartmentID='" + ddldepartment.SelectedValue + "' and WMaster.DivisionID IS NULL and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal4.DataSource = ds12;
    //        ddlweeklygoal4.DataTextField = "Title";
    //        ddlweeklygoal4.DataValueField = "MasterId";
    //        ddlweeklygoal4.DataBind();
    //    }
    //    ddlweeklygoal4.Items.Insert(0, "-Select-");
    //    ddlweeklygoal4.Items[0].Value = "0";
    //}

    //protected void fillweekdept5()
    //{
    //    ddlweeklygoal5.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  DepartmentmasterMNC on DepartmentmasterMNC.id=WMaster.DepartmentID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DepartmentID='" + ddldepartment.SelectedValue + "' and WMaster.DivisionID IS NULL and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal5.DataSource = ds12;
    //        ddlweeklygoal5.DataTextField = "Title";
    //        ddlweeklygoal5.DataValueField = "MasterId";
    //        ddlweeklygoal5.DataBind();
    //    }
    //    ddlweeklygoal5.Items.Insert(0, "-Select-");
    //    ddlweeklygoal5.Items[0].Value = "0";
    //}

    //protected void fillweekdept6()
    //{
    //    ddlweeklygoal6.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  DepartmentmasterMNC on DepartmentmasterMNC.id=WMaster.DepartmentID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DepartmentID='" + ddldepartment.SelectedValue + "' and WMaster.DivisionID IS NULL and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal6.DataSource = ds12;
    //        ddlweeklygoal6.DataTextField = "Title";
    //        ddlweeklygoal6.DataValueField = "MasterId";
    //        ddlweeklygoal6.DataBind();
    //    }
    //    ddlweeklygoal6.Items.Insert(0, "-Select-");
    //    ddlweeklygoal6.Items[0].Value = "0";
    //}

    //protected void fillweekdept7()
    //{
    //    ddlweeklygoal7.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  DepartmentmasterMNC on DepartmentmasterMNC.id=WMaster.DepartmentID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DepartmentID='" + ddldepartment.SelectedValue + "' and WMaster.DivisionID IS NULL and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal7.DataSource = ds12;
    //        ddlweeklygoal7.DataTextField = "Title";
    //        ddlweeklygoal7.DataValueField = "MasterId";
    //        ddlweeklygoal7.DataBind();
    //    }
    //    ddlweeklygoal7.Items.Insert(0, "-Select-");
    //    ddlweeklygoal7.Items[0].Value = "0";
    //}

    //protected void fillweekdept8()
    //{
    //    ddlweeklygoal8.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  DepartmentmasterMNC on DepartmentmasterMNC.id=WMaster.DepartmentID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DepartmentID='" + ddldepartment.SelectedValue + "' and WMaster.DivisionID IS NULL and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal8.DataSource = ds12;
    //        ddlweeklygoal8.DataTextField = "Title";
    //        ddlweeklygoal8.DataValueField = "MasterId";
    //        ddlweeklygoal8.DataBind();
    //    }
    //    ddlweeklygoal8.Items.Insert(0, "-Select-");
    //    ddlweeklygoal8.Items[0].Value = "0";
    //}

    //protected void fillweekdept9()
    //{
    //    ddlweeklygoal9.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  DepartmentmasterMNC on DepartmentmasterMNC.id=WMaster.DepartmentID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DepartmentID='" + ddldepartment.SelectedValue + "' and WMaster.DivisionID IS NULL and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal9.DataSource = ds12;
    //        ddlweeklygoal9.DataTextField = "Title";
    //        ddlweeklygoal9.DataValueField = "MasterId";
    //        ddlweeklygoal9.DataBind();
    //    }
    //    ddlweeklygoal9.Items.Insert(0, "-Select-");
    //    ddlweeklygoal9.Items[0].Value = "0";
    //}

    protected void fillweekdivi()
    {
        DropDownList1.Items.Clear();

        string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  Businessmaster on Businessmaster.BusinessID=WMaster.DivisionID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DivisionID='" + ddldivision.SelectedValue + "' and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
        da12.Fill(ds12);

        if (ds12.Rows.Count > 0)
        {
            DropDownList1.DataSource = ds12;
            DropDownList1.DataTextField = "Title";
            DropDownList1.DataValueField = "MasterId";
            DropDownList1.DataBind();
        }
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";
    }

    //protected void fillweekdivi2()
    //{
    //    ddlweeklygoal2.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  Businessmaster on Businessmaster.BusinessID=WMaster.DivisionID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DivisionID='" + ddldivision.SelectedValue + "' and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal2.DataSource = ds12;
    //        ddlweeklygoal2.DataTextField = "Title";
    //        ddlweeklygoal2.DataValueField = "MasterId";
    //        ddlweeklygoal2.DataBind();
    //    }
    //    ddlweeklygoal2.Items.Insert(0, "-Select-");
    //    ddlweeklygoal2.Items[0].Value = "0";
    //}

    //protected void fillweekdivi3()
    //{
    //    ddlweeklygoal3.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  Businessmaster on Businessmaster.BusinessID=WMaster.DivisionID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DivisionID='" + ddldivision.SelectedValue + "' and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal3.DataSource = ds12;
    //        ddlweeklygoal3.DataTextField = "Title";
    //        ddlweeklygoal3.DataValueField = "MasterId";
    //        ddlweeklygoal3.DataBind();
    //    }
    //    ddlweeklygoal3.Items.Insert(0, "-Select-");
    //    ddlweeklygoal3.Items[0].Value = "0";
    //}

    //protected void fillweekdivi4()
    //{
    //    ddlweeklygoal4.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  Businessmaster on Businessmaster.BusinessID=WMaster.DivisionID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DivisionID='" + ddldivision.SelectedValue + "' and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal4.DataSource = ds12;
    //        ddlweeklygoal4.DataTextField = "Title";
    //        ddlweeklygoal4.DataValueField = "MasterId";
    //        ddlweeklygoal4.DataBind();
    //    }
    //    ddlweeklygoal4.Items.Insert(0, "-Select-");
    //    ddlweeklygoal4.Items[0].Value = "0";
    //}

    //protected void fillweekdivi5()
    //{
    //    ddlweeklygoal5.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  Businessmaster on Businessmaster.BusinessID=WMaster.DivisionID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DivisionID='" + ddldivision.SelectedValue + "' and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal5.DataSource = ds12;
    //        ddlweeklygoal5.DataTextField = "Title";
    //        ddlweeklygoal5.DataValueField = "MasterId";
    //        ddlweeklygoal5.DataBind();
    //    }
    //    ddlweeklygoal5.Items.Insert(0, "-Select-");
    //    ddlweeklygoal5.Items[0].Value = "0";
    //}

    //protected void fillweekdivi6()
    //{
    //    ddlweeklygoal6.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  Businessmaster on Businessmaster.BusinessID=WMaster.DivisionID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DivisionID='" + ddldivision.SelectedValue + "' and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal6.DataSource = ds12;
    //        ddlweeklygoal6.DataTextField = "Title";
    //        ddlweeklygoal6.DataValueField = "MasterId";
    //        ddlweeklygoal6.DataBind();
    //    }
    //    ddlweeklygoal6.Items.Insert(0, "-Select-");
    //    ddlweeklygoal6.Items[0].Value = "0";
    //}

    //protected void fillweekdivi7()
    //{
    //    ddlweeklygoal7.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  Businessmaster on Businessmaster.BusinessID=WMaster.DivisionID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DivisionID='" + ddldivision.SelectedValue + "' and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal7.DataSource = ds12;
    //        ddlweeklygoal7.DataTextField = "Title";
    //        ddlweeklygoal7.DataValueField = "MasterId";
    //        ddlweeklygoal7.DataBind();
    //    }
    //    ddlweeklygoal7.Items.Insert(0, "-Select-");
    //    ddlweeklygoal7.Items[0].Value = "0";
    //}

    //protected void fillweekdivi8()
    //{
    //    ddlweeklygoal8.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  Businessmaster on Businessmaster.BusinessID=WMaster.DivisionID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DivisionID='" + ddldivision.SelectedValue + "' and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal8.DataSource = ds12;
    //        ddlweeklygoal8.DataTextField = "Title";
    //        ddlweeklygoal8.DataValueField = "MasterId";
    //        ddlweeklygoal8.DataBind();
    //    }
    //    ddlweeklygoal8.Items.Insert(0, "-Select-");
    //    ddlweeklygoal8.Items[0].Value = "0";
    //}

    //protected void fillweekdivi9()
    //{
    //    ddlweeklygoal9.Items.Clear();

    //    string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  Businessmaster on Businessmaster.BusinessID=WMaster.DivisionID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DivisionID='" + ddldivision.SelectedValue + "' and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

    //    DataTable ds12 = new DataTable();
    //    SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
    //    da12.Fill(ds12);

    //    if (ds12.Rows.Count > 0)
    //    {
    //        ddlweeklygoal9.DataSource = ds12;
    //        ddlweeklygoal9.DataTextField = "Title";
    //        ddlweeklygoal9.DataValueField = "MasterId";
    //        ddlweeklygoal9.DataBind();
    //    }
    //    ddlweeklygoal9.Items.Insert(0, "-Select-");
    //    ddlweeklygoal9.Items[0].Value = "0";
    //}


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //ddlweeklygoal.SelectedIndex = 0;
        //ddlproject.SelectedIndex = 0;
        txtTaskName.Text = "";
        txtbudgetminute.Text = "0";
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        //ddlweeklygoal2.SelectedIndex = 0;
        //ddlproject2.SelectedIndex = 0;
        txttaskname2.Text = "";
        txtbudgetedminute2.Text = "0";
    }
    protected void ImageButton3_Click1(object sender, ImageClickEventArgs e)
    {
        //ddlweeklygoal3.SelectedIndex = 0;
        //ddlproject3.SelectedIndex = 0;
        txttaskname3.Text = "";
        txtbudgetedminute3.Text = "0";
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        //ddlweeklygoal4.SelectedIndex = 0;
        //ddlproject4.SelectedIndex = 0;
        txttaskname4.Text = "";
        txtbudgetedminute4.Text = "0";
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        //ddlweeklygoal5.SelectedIndex = 0;
        //ddlproject5.SelectedIndex = 0;
        txttaskname5.Text = "";
        txtbudgetedminute5.Text = "0";
    }
    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {
        //ddlweeklygoal6.SelectedIndex = 0;
        //ddlproject6.SelectedIndex = 0;
        txttaskname6.Text = "";
        txtbudgetedminute6.Text = "0";
    }
    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    {
        //ddlweeklygoal7.SelectedIndex = 0;
        //ddlproject7.SelectedIndex = 0;
        txttaskname7.Text = "";
        txtbudgetedminute7.Text = "0";
    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        //ddlweeklygoal8.SelectedIndex = 0;
        //ddlproject8.SelectedIndex = 0;
        txttaskname8.Text = "";
        txtbudgetedminute8.Text = "0";
    }
    protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
    {
        //ddlweeklygoal9.SelectedIndex = 0;
        //ddlproject9.SelectedIndex = 0;
        txttaskname9.Text = "";
        txtbudgetedminute9.Text = "0";
    }
    protected void ddltaskfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltaskfor.SelectedValue == "0")
        {
            ddldepartment.Items.Clear();
            fillemployee();
            Panel2.Visible = false;
            Panel3.Visible = false;
        }
        if (ddltaskfor.SelectedValue == "1")
        {
            filldepartment();
            ddldepartment_SelectedIndexChanged(sender, e);
            Panel2.Visible = true;
            Panel3.Visible = false;
        }
        if (ddltaskfor.SelectedValue == "2")
        {
            filldivision();
            ddldivision_SelectedIndexChanged(sender, e);
            ddldepartment.Items.Clear();
            fillemployee();
            Panel2.Visible = false;
            Panel3.Visible = true;
        }
    }

    protected void weeklygoal()
    {
        DropDownList1.Items.Clear();
        string tempstring = "";

        if (ddltaskfor.SelectedValue == "1")
        {
            tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  DepartmentmasterMNC on DepartmentmasterMNC.id=WMaster.DepartmentID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DepartmentID='" + ddldepartment.SelectedValue + "' and WMaster.DivisionID IS NULL and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";
        }
        else if (ddltaskfor.SelectedValue == "2")
        {
            tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner  join  Businessmaster on Businessmaster.BusinessID=WMaster.DivisionID inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and WMaster.DivisionID='" + ddldivision.SelectedValue + "' and WMaster.EmployeeID IS NULL and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";
        }
        else if (Panel5.Visible == true)
        {
            tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner  join  EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.EmployeeId='" + ddlnewtaskempname.SelectedValue + "' and WMaster.BusinessID='" + ddlStore.SelectedValue + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";
        }
        else
        {
            tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner  join  EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";
        }
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
        da12.Fill(ds12);

        if (ds12.Rows.Count > 0)
        {
            DropDownList1.DataSource = ds12;
            DropDownList1.DataTextField = "Title";
            DropDownList1.DataValueField = "MasterId";
            DropDownList1.DataBind();
        }
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";
    }

    protected void weeklygoalbusiness()
    {
        DropDownList1.Items.Clear();

        string tempstring = "Select Left(WMaster.Title,40) + ' : ' + Week.Name as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner  join  EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.BusinessID where WMaster.BusinessID='" + ddlStore.SelectedValue + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + System.DateTime.Now.ToShortDateString() + "'";

        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
        da12.Fill(ds12);

        if (ds12.Rows.Count > 0)
        {
            DropDownList1.DataSource = ds12;
            DropDownList1.DataTextField = "Title";
            DropDownList1.DataValueField = "MasterId";
            DropDownList1.DataBind();
        }
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";
    }

    protected void projet()
    {
        DropDownList2.Items.Clear();

        string tempstring = "";

        if (ddltaskfor.SelectedValue == "1")
        {
            tempstring = "select (Left(DepartmentmasterMNC.Departmentname,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=ProjectMaster.DeptId where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.DeptId='" + ddldepartment.SelectedValue + "' and projectmaster.businessid='0' and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' Order by DepartmentmasterMNC.Departmentname,ProjectMaster.ProjectName";
        }
        else if (ddltaskfor.SelectedValue == "2")
        {
            tempstring = " select (Left(BusinessMaster.BusinessName,8)+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join BusinessMaster on BusinessMaster.BusinessID=ProjectMaster.BusinessID where ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and projectmaster.businessid='" + ddldivision.SelectedValue + "'  and projectmaster.EmployeeId='0' and ProjectMaster.Status='Pending' Order by BusinessMaster.BusinessName,ProjectMaster.ProjectName";
        }
        else if (Panel5.Visible == true)
        {
            tempstring = "select (EmployeeMaster.EmployeeName+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=ProjectMaster.EmployeeID where  ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.EmployeeID='" + ddlnewtaskempname.SelectedValue + "' and ProjectMaster.Status='Pending' Order by EmployeeMaster.EmployeeName,ProjectMaster.ProjectName";
        }
        else
        {
            tempstring = "select (WarehouseMaster.Name+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join WarehouseMaster on WarehouseMaster.WarehouseID=ProjectMaster.Whid where  ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and ProjectMaster.BusinessID='0' and ProjectMaster.DeptID='0' and ProjectMaster.EmployeeID='0' and ProjectMaster.Status='Pending' Order by WarehouseMaster.Name,ProjectMaster.ProjectName";
        }

        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(tempstring, con);
        da12.Fill(ds12);
        if (ds12.Rows.Count > 0)
        {

            DropDownList2.DataSource = ds12;
            DropDownList2.DataTextField = "ProjectName";
            DropDownList2.DataValueField = "ProjectId";
            DropDownList2.DataBind();
        }
        DropDownList2.Items.Insert(0, "-Select-");
        DropDownList2.Items[0].Value = "0";
    }

    protected void projetbusiness()
    {
        DropDownList2.Items.Clear();

        string tempstring = "select (EmployeeMaster.EmployeeName+':'+Left(ProjectMaster.ProjectName,40)) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=ProjectMaster.EmployeeID where  ProjectMaster.Whid='" + ddlStore.SelectedValue + "' and Status='Pending' ";

        string order = " Order by EmployeeMaster.EmployeeName,ProjectMaster.ProjectName ";
        string str12 = tempstring + order;
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        if (ds12.Rows.Count > 0)
        {

            DropDownList2.DataSource = ds12;
            DropDownList2.DataTextField = "ProjectName";
            DropDownList2.DataValueField = "ProjectId";
            DropDownList2.DataBind();
        }
        DropDownList2.Items.Insert(0, "-Select-");
        DropDownList2.Items[0].Value = "0";
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            weeklygoal();
            projet();
            Panel5.Visible = true;
        }
        else
        {
            Panel5.Visible = false;
        }
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            weeklygoal();
            projet();
            Panel6.Visible = true;
        }
        else
        {
            Panel6.Visible = false;
        }
    }
    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox3.Checked == true)
        {
            weeklygoal();
            projet();
            Panel7.Visible = true;
        }
        else
        {
            Panel7.Visible = false;
        }
    }
}
