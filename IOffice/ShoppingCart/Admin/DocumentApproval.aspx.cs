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
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using ForAspNet.POP3;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Media;
using AjaxControlToolkit;

public partial class Account_DocumentApproval : System.Web.UI.Page
{
    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    MasterCls clsMaster = new MasterCls();
    Companycls ClsCompany = new Companycls();
    protected int DesignationId;
    EmployeeCls clsEmployee = new EmployeeCls();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
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
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);


        if (!Page.IsPostBack)
        {

            Pagecontrol.dypcontrol(Page, page);

            txtfrom.Text = System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString();
            txtto.Text = System.DateTime.Now.ToShortDateString();

            TxtDocDate.Text = System.DateTime.Now.ToShortDateString();

            lblCompany.Text = Session["Cname"].ToString();

            string str = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' and EmployeeWarehouseRights.AccessAllowed='True' order by name";
            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlbusiness.DataSource = dt;
            ddlbusiness.DataTextField = "Name";
            ddlbusiness.DataValueField = "WareHouseId";
            ddlbusiness.DataBind();

            string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
            SqlCommand cmdeeed = new SqlCommand(eeed, con);
            SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            DataTable dteeed = new DataTable();
            adpeeed.Fill(dteeed);
            //  lblBusiness.Text = ddlbusiness.SelectedItem.Text;
            if (dteeed.Rows.Count > 0)
            {
                ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
                //ddlbusiness.Enabled = false;
            }
            filldatebyperiod();
            fillemployee();
            filldesignation();
            fillemployeeofficeclerk();
            fillemployeesupervisor();
            fillgrid();
            ViewState["sortOrder"] = "";
            filldll();
            FillParty();
        }
    }
    protected void fillemployee()
    {
        string eeed = "Select distinct EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName from EmployeeMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=EmployeeMaster.DeptID inner join  DesignationMaster on DesignationMaster.DeptId=DepartmentmasterMNC.Id where DepartmentmasterMNC.Departmentname='Filling Desk' and  DesignationMaster.Designationname in('Manager','Office Clerk','Supervisor') and  EmployeeMaster.Whid ='" + ddlbusiness.SelectedValue + "'";
        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        if (dteeed.Rows.Count > 0)
        {
            ddlemp.DataSource = dteeed;
            ddlemp.DataTextField = "EmployeeName";
            ddlemp.DataValueField = "EmployeeMasterID";
            ddlemp.DataBind();
            ddlemp.Items.Insert(0, "Select");
            ddlemp.Items[0].Value = "0";
            ddlemp.SelectedValue = Session["EmployeeID"].ToString();
            ddlemp.Enabled = false;
        }
        else
        {
            ddlemp.DataSource = null;
            ddlemp.DataTextField = "EmployeeName";
            ddlemp.DataValueField = "EmployeeMasterID";
            ddlemp.DataBind();
            ddlemp.Items.Insert(0, "Select");
            ddlemp.Items[0].Value = "0";
        }

    }    
    protected void filldesignation()
    {
        string strdesig = " select DesignationMaster.* from EmployeeMaster inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID where EmployeeMaster.EmployeeMasterID='" + Session["EmployeeID"].ToString() + "'  ";
        SqlCommand cmdeeed = new SqlCommand(strdesig, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        if (dteeed.Rows.Count > 0)
        {
            ViewState["DesignationName"] = dteeed.Rows[0]["DesignationName"].ToString();
            lbldesignation.Text = dteeed.Rows[0]["DesignationName"].ToString();
        }
        if (lbldesignation.Text == "Supervisor")
        {
            ddlofficestatus.SelectedValue = "3";
            Label21.Visible = false;
            ddlsupervisorfilter.Visible = false;
            ddlsupervisorstatus.Visible = false;  
        }
        if (lbldesignation.Text == "Manager")
        {
            ddlsupervisorstatus.SelectedValue = "3";
            ddlofficestatus.SelectedValue = "3";
        }
    }
    protected void fillemployeeofficeclerk()
    {
        string eeed = "SELECT EmployeeMaster.EmployeeMasterID ,DesignationMaster.DesignationName+' : '+EmployeeMaster.EmployeeName as EmployeeName FROM         DesignationMaster inner join DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id inner join EmployeeMaster ON EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId WHERE      DepartmentmasterMNC.Companyid='" + Session["Comid"] + "' and DepartmentmasterMNC.Whid='" + ddlbusiness.SelectedValue + "' and (DepartmentmasterMNC.DepartmentName='Filling Desk') and  EmployeeMaster.Whid ='" + ddlbusiness.SelectedValue + "' and (DesignationMaster.DesignationName ='Office Clerk' ) order by EmployeeName ";
        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        if (dteeed.Rows.Count > 0)
        {
            ddlfilterofficeclerk.DataSource = dteeed;
            ddlfilterofficeclerk.DataTextField = "EmployeeName";
            ddlfilterofficeclerk.DataValueField = "EmployeeMasterID";
            ddlfilterofficeclerk.DataBind();
            ddlfilterofficeclerk.Items.Insert(0, "All");
            ddlfilterofficeclerk.Items[0].Value = "0";
        }
        else
        {
            ddlfilterofficeclerk.DataSource = null;
            ddlfilterofficeclerk.DataTextField = "EmployeeName";
            ddlfilterofficeclerk.DataValueField = "EmployeeMasterID";
            ddlfilterofficeclerk.DataBind();
            ddlfilterofficeclerk.Items.Insert(0, "All");
            ddlfilterofficeclerk.Items[0].Value = "0";
        }

    }
    protected void fillemployeesupervisor()
    {
        //string eeed = "Select distinct EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName from EmployeeMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=EmployeeMaster.DeptID inner join  DesignationMaster on DesignationMaster.DeptId=DepartmentmasterMNC.Id where DepartmentmasterMNC.Departmentname='Filling Desk' and  DesignationMaster.Designationname ='Supervisor' and  EmployeeMaster.Whid ='" + ddlbusiness.SelectedValue + "' order by EmployeeName";
        string eeed = "SELECT EmployeeMaster.EmployeeMasterID ,DesignationMaster.DesignationName+' : '+EmployeeMaster.EmployeeName as EmployeeName FROM         DesignationMaster inner join DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id inner join EmployeeMaster ON EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId WHERE      DepartmentmasterMNC.Companyid='" + Session["Comid"] + "' and DepartmentmasterMNC.Whid='" + ddlbusiness.SelectedValue + "' and (DepartmentmasterMNC.DepartmentName='Filling Desk') and  EmployeeMaster.Whid ='" + ddlbusiness.SelectedValue + "' and (DesignationMaster.DesignationName ='Supervisor' ) order by EmployeeName ";
        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        if (dteeed.Rows.Count > 0)
        {
            ddlsupervisorfilter.DataSource = dteeed;
            ddlsupervisorfilter.DataTextField = "EmployeeName";
            ddlsupervisorfilter.DataValueField = "EmployeeMasterID";
            ddlsupervisorfilter.DataBind();
            ddlsupervisorfilter.Items.Insert(0, "All");
            ddlsupervisorfilter.Items[0].Value = "0";
        }
        else
        {
            ddlsupervisorfilter.DataSource = null;
            ddlsupervisorfilter.DataTextField = "EmployeeName";
            ddlsupervisorfilter.DataValueField = "EmployeeMasterID";
            ddlsupervisorfilter.DataBind();
            ddlsupervisorfilter.Items.Insert(0, "All");
            ddlsupervisorfilter.Items[0].Value = "0";
        }

    }



    protected void SelectDocumentforApproval()
    {


        if (ddlemp.SelectedIndex > 0)
        {
            lblStatus.Text = ddlemp.SelectedItem.Text;



            string str = "SELECT     DocumentMaster.DocumentId, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadTypeId, DocumentMaster.DocumentUploadDate,DocumentMaster.DocumentDate , " +
                     " DocumentMaster.DocumentName, DocumentMaster.DocumentTitle, DocumentMaster.Description, DocumentMaster.PartyId,  " +
                     "  DocumentMaster.DocumentRefNo, DocumentMaster.DocumentAmount, DocumentType.DocumentType, Party_Master.Compname as PartyName,  " +
                      " EmployeeMaster.EmployeeName, DocumentProcessing_1.ProcessingId as DocumentProcessingId, DocumentProcessing_1.DocAllocateDate,   " +
                     "  DocumentProcessing_1.ApproveDate, DocumentProcessing_1.Approve, DocumentProcessing_1.Note, DesignationMaster.DesignationName,    " +
                     "  DesignationMaster.DesignationMasterID as DesignationID , EmployeeMaster.EmployeeMasterID as EmployeeID    " +
                     "  FROM         DocumentProcessing AS DocumentProcessing_1 RIGHT OUTER JOIN    " +
                     "  DocumentMaster ON DocumentProcessing_1.DocumentId = DocumentMaster.DocumentId LEFT OUTER JOIN    " +
                     "  EmployeeMaster ON DocumentProcessing_1.EmployeeId = EmployeeMaster.EmployeeMasterID LEFT OUTER JOIN    " +
                     "  DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID LEFT OUTER JOIN   " +
                     "  DocumentType ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId LEFT OUTER JOIN   " +
                     "  Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId    " +
                    "  WHERE     (DocumentMaster.DocumentId IN    " +
                    "  (SELECT     DocumentId    " +
                     "  FROM          DocumentProcessing AS DocumentProcessing)) AND (DocumentProcessing_1.EmployeeId = '" + ddlemp.SelectedValue + "')  And (DocumentProcessing_1.CID='" + Session["Comid"].ToString() + "')     ";

            string status = "";
            if (ddlapproval.SelectedValue == "None")
            {
                status = " AND   (DocumentProcessing_1.Approve IS NULL) ";
            }
            if (ddlapproval.SelectedValue == "True")
            {
                status = " AND   (DocumentProcessing_1.Approve='1') ";

            }
            if (ddlapproval.SelectedValue == "False")
            {
                status = " AND  (DocumentProcessing_1.Approve='0') ";

            }
            string orderby = " ORDER BY DocumentProcessing_1.ProcessingId desc ";

            string finalstr = str + status + orderby;

            SqlCommand cmd = new SqlCommand(finalstr, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable Dt = new DataTable();
            adp.Fill(Dt);

            if (Dt.Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                DataColumn dtcom1 = new DataColumn();
                dtcom1.DataType = System.Type.GetType("System.String");
                dtcom1.ColumnName = "DocumentId";
                dtcom1.ReadOnly = false;
                dtcom1.Unique = false;
                dtcom1.AllowDBNull = true;
                dt1.Columns.Add(dtcom1);

                DataColumn dtcom2 = new DataColumn();
                dtcom2.DataType = System.Type.GetType("System.String");
                dtcom2.ColumnName = "DocumentTypeId";
                dtcom2.ReadOnly = false;
                dtcom2.Unique = false;
                dtcom2.AllowDBNull = true;
                dt1.Columns.Add(dtcom2);

                DataColumn dtcom3 = new DataColumn();
                dtcom3.DataType = System.Type.GetType("System.String");
                dtcom3.ColumnName = "DocumentUploadTypeId";
                dtcom3.ReadOnly = false;
                dtcom3.Unique = false;
                dtcom3.AllowDBNull = true;
                dt1.Columns.Add(dtcom3);

                DataColumn dtcom4 = new DataColumn();
                dtcom4.DataType = System.Type.GetType("System.String");
                dtcom4.ColumnName = "DocumentUploadDate";
                dtcom4.ReadOnly = false;
                dtcom4.Unique = false;
                dtcom4.AllowDBNull = true;
                dt1.Columns.Add(dtcom4);

                DataColumn dtcomdate = new DataColumn();
                dtcomdate.DataType = System.Type.GetType("System.String");
                dtcomdate.ColumnName = "DocumentDate";
                dtcomdate.ReadOnly = false;
                dtcomdate.Unique = false;
                dtcomdate.AllowDBNull = true;
                dt1.Columns.Add(dtcomdate);


                DataColumn dtcom5 = new DataColumn();
                dtcom5.DataType = System.Type.GetType("System.String");
                dtcom5.ColumnName = "DocumentName";
                dtcom5.ReadOnly = false;
                dtcom5.Unique = false;
                dtcom5.AllowDBNull = true;
                dt1.Columns.Add(dtcom5);

                DataColumn dtcom6 = new DataColumn();
                dtcom6.DataType = System.Type.GetType("System.String");
                dtcom6.ColumnName = "DocumentTitle";
                dtcom6.ReadOnly = false;
                dtcom6.Unique = false;
                dtcom6.AllowDBNull = true;
                dt1.Columns.Add(dtcom6);

                DataColumn dtcom7 = new DataColumn();
                dtcom7.DataType = System.Type.GetType("System.String");
                dtcom7.ColumnName = "Description";
                dtcom7.ReadOnly = false;
                dtcom7.Unique = false;
                dtcom7.AllowDBNull = true;
                dt1.Columns.Add(dtcom7);

                DataColumn dtcom8 = new DataColumn();
                dtcom8.DataType = System.Type.GetType("System.String");
                dtcom8.ColumnName = "PartyId";
                dtcom8.ReadOnly = false;
                dtcom8.Unique = false;
                dtcom8.AllowDBNull = true;
                dt1.Columns.Add(dtcom8);

                DataColumn dtcom9 = new DataColumn();
                dtcom9.DataType = System.Type.GetType("System.String");
                dtcom9.ColumnName = "DocumentRefNo";
                dtcom9.ReadOnly = false;
                dtcom9.Unique = false;
                dtcom9.AllowDBNull = true;
                dt1.Columns.Add(dtcom9);

                DataColumn dtcom10 = new DataColumn();
                dtcom10.DataType = System.Type.GetType("System.String");
                dtcom10.ColumnName = "DocumentAmount";
                dtcom10.ReadOnly = false;
                dtcom10.Unique = false;
                dtcom10.AllowDBNull = true;
                dt1.Columns.Add(dtcom10);

                DataColumn dtcom11 = new DataColumn();
                dtcom11.DataType = System.Type.GetType("System.String");
                dtcom11.ColumnName = "DocumentType";
                dtcom11.ReadOnly = false;
                dtcom11.Unique = false;
                dtcom11.AllowDBNull = true;
                dt1.Columns.Add(dtcom11);

                DataColumn dtcom12 = new DataColumn();
                dtcom12.DataType = System.Type.GetType("System.String");
                dtcom12.ColumnName = "PartyName";
                dtcom12.ReadOnly = false;
                dtcom12.Unique = false;
                dtcom12.AllowDBNull = true;
                dt1.Columns.Add(dtcom12);

                DataColumn dtcom13 = new DataColumn();
                dtcom13.DataType = System.Type.GetType("System.String");
                dtcom13.ColumnName = "EmployeeName";
                dtcom13.ReadOnly = false;
                dtcom13.Unique = false;
                dtcom13.AllowDBNull = true;
                dt1.Columns.Add(dtcom13);

                DataColumn dtcom14 = new DataColumn();
                dtcom14.DataType = System.Type.GetType("System.String");
                dtcom14.ColumnName = "DocumentProcessingId";
                dtcom14.ReadOnly = false;
                dtcom14.Unique = false;
                dtcom14.AllowDBNull = true;
                dt1.Columns.Add(dtcom14);

                DataColumn dtcom15 = new DataColumn();
                dtcom15.DataType = System.Type.GetType("System.String");
                dtcom15.ColumnName = "DocAllocateDate";
                dtcom15.ReadOnly = false;
                dtcom15.Unique = false;
                dtcom15.AllowDBNull = true;
                dt1.Columns.Add(dtcom15);

                DataColumn dtcom16 = new DataColumn();
                dtcom16.DataType = System.Type.GetType("System.String");
                dtcom16.ColumnName = "ApproveDate";
                dtcom16.ReadOnly = false;
                dtcom16.Unique = false;
                dtcom16.AllowDBNull = true;
                dt1.Columns.Add(dtcom16);

                DataColumn dtcom17 = new DataColumn();
                dtcom17.DataType = System.Type.GetType("System.String");
                dtcom17.ColumnName = "Approve";
                dtcom17.ReadOnly = false;
                dtcom17.Unique = false;
                dtcom17.AllowDBNull = true;
                dt1.Columns.Add(dtcom17);

                DataColumn dtcom18 = new DataColumn();
                dtcom18.DataType = System.Type.GetType("System.String");
                dtcom18.ColumnName = "Note";
                dtcom18.ReadOnly = false;
                dtcom18.Unique = false;
                dtcom18.AllowDBNull = true;
                dt1.Columns.Add(dtcom18);

                DataColumn dtcom19 = new DataColumn();
                dtcom19.DataType = System.Type.GetType("System.String");
                dtcom19.ColumnName = "DesignationName";
                dtcom19.ReadOnly = false;
                dtcom19.Unique = false;
                dtcom19.AllowDBNull = true;
                dt1.Columns.Add(dtcom19);

                DataColumn dtcom20 = new DataColumn();
                dtcom20.DataType = System.Type.GetType("System.String");
                dtcom20.ColumnName = "DesignationID";
                dtcom20.ReadOnly = false;
                dtcom20.Unique = false;
                dtcom20.AllowDBNull = true;
                dt1.Columns.Add(dtcom20);

                DataColumn dtcom21 = new DataColumn();
                dtcom21.DataType = System.Type.GetType("System.String");
                dtcom21.ColumnName = "EmployeeID";
                dtcom21.ReadOnly = false;
                dtcom21.Unique = false;
                dtcom21.AllowDBNull = true;
                dt1.Columns.Add(dtcom21);

                foreach (DataRow dr in Dt.Rows)
                {
                    if (Convert.ToString(dr["Approve"]) == "")
                    {
                        gridocapproval.Columns[6].Visible = true;


                        // imgbtnSubmit.Visible = true;

                        DataTable dsag = new DataTable();
                        dsag = clsDocument.SelectDocumentforApprovalLessProcessId(Convert.ToInt32(dr["DocumentProcessingId"].ToString()), Convert.ToInt32(dr["DocumentId"].ToString()));

                        if (dsag.Rows.Count > 0)
                        {
                            if (dsag.Rows[0]["Approve"] == "1")
                            {
                                DataRow drow = dt1.NewRow();
                                drow["DocumentId"] = dr["DocumentId"].ToString();
                                drow["DocumentTypeId"] = dr["DocumentTypeId"].ToString();
                                drow["DocumentUploadTypeId"] = dr["DocumentUploadTypeId"].ToString();
                                drow["DocumentUploadDate"] = dr["DocumentUploadDate"].ToString();
                                drow["DocumentDate"] = dr["DocumentDate"].ToString();
                                drow["DocumentName"] = dr["DocumentName"].ToString();
                                drow["DocumentTitle"] = dr["DocumentTitle"].ToString();

                                drow["Description"] = dr["Description"].ToString();
                                drow["PartyId"] = dr["PartyId"].ToString();
                                drow["DocumentRefNo"] = dr["DocumentRefNo"].ToString();
                                drow["DocumentAmount"] = dr["DocumentAmount"].ToString();
                                drow["DocumentType"] = dr["DocumentType"].ToString();
                                drow["PartyName"] = dr["PartyName"].ToString();
                                drow["EmployeeName"] = dr["EmployeeName"].ToString();
                                drow["DocumentProcessingId"] = dr["DocumentProcessingId"].ToString();
                                drow["DocAllocateDate"] = dr["DocAllocateDate"].ToString();
                                drow["ApproveDate"] = dr["ApproveDate"].ToString();
                                drow["Approve"] = dr["Approve"].ToString();
                                drow["Note"] = dr["Note"].ToString();
                                drow["DesignationName"] = dr["DesignationName"].ToString();
                                drow["DesignationID"] = dr["DesignationID"].ToString();
                                drow["EmployeeID"] = dr["EmployeeID"].ToString();
                                dt1.Rows.Add(drow);
                            }
                            else
                            {

                            }
                        }
                        else if (ddlapproval.SelectedItem.Text == "None")
                        {
                            DataRow drow = dt1.NewRow();
                            drow["DocumentId"] = dr["DocumentId"].ToString();
                            drow["DocumentTypeId"] = dr["DocumentTypeId"].ToString();
                            drow["DocumentUploadTypeId"] = dr["DocumentUploadTypeId"].ToString();
                            drow["DocumentUploadDate"] = dr["DocumentUploadDate"].ToString();
                            drow["DocumentDate"] = dr["DocumentDate"].ToString();
                            drow["DocumentName"] = dr["DocumentName"].ToString();
                            drow["DocumentTitle"] = dr["DocumentTitle"].ToString();
                            drow["Description"] = dr["Description"].ToString();
                            drow["PartyId"] = dr["PartyId"].ToString();
                            drow["DocumentRefNo"] = dr["DocumentRefNo"].ToString();
                            drow["DocumentAmount"] = dr["DocumentAmount"].ToString();
                            drow["DocumentType"] = dr["DocumentType"].ToString();
                            drow["PartyName"] = dr["PartyName"].ToString();
                            drow["EmployeeName"] = dr["EmployeeName"].ToString();
                            drow["DocumentProcessingId"] = dr["DocumentProcessingId"].ToString();
                            drow["DocAllocateDate"] = dr["DocAllocateDate"].ToString();
                            drow["ApproveDate"] = dr["ApproveDate"].ToString();
                            drow["Approve"] = dr["Approve"].ToString();
                            drow["Note"] = dr["Note"].ToString();
                            drow["DesignationName"] = dr["DesignationName"].ToString();
                            drow["DesignationID"] = dr["DesignationID"].ToString();
                            drow["EmployeeID"] = dr["EmployeeID"].ToString();
                            dt1.Rows.Add(drow);

                        }


                    }
                    else
                    {
                        gridocapproval.Columns[6].Visible = false;
                        // imgbtnSubmit.Visible = false;
                        DataRow drow = dt1.NewRow();
                        drow["DocumentId"] = dr["DocumentId"].ToString();
                        drow["DocumentTypeId"] = dr["DocumentTypeId"].ToString();
                        drow["DocumentUploadTypeId"] = dr["DocumentUploadTypeId"].ToString();
                        drow["DocumentUploadDate"] = dr["DocumentUploadDate"].ToString();
                        drow["DocumentDate"] = dr["DocumentDate"].ToString();
                        drow["DocumentName"] = dr["DocumentName"].ToString();
                        drow["DocumentTitle"] = dr["DocumentTitle"].ToString();
                        drow["Description"] = dr["Description"].ToString();
                        drow["PartyId"] = dr["PartyId"].ToString();
                        drow["DocumentRefNo"] = dr["DocumentRefNo"].ToString();
                        drow["DocumentAmount"] = dr["DocumentAmount"].ToString();
                        drow["DocumentType"] = dr["DocumentType"].ToString();
                        drow["PartyName"] = dr["PartyName"].ToString();
                        drow["EmployeeName"] = dr["EmployeeName"].ToString();
                        drow["DocumentProcessingId"] = dr["DocumentProcessingId"].ToString();
                        drow["DocAllocateDate"] = dr["DocAllocateDate"].ToString();
                        drow["ApproveDate"] = dr["ApproveDate"].ToString();
                        drow["Approve"] = dr["Approve"].ToString();
                        drow["Note"] = dr["Note"].ToString();
                        drow["DesignationName"] = dr["DesignationName"].ToString();
                        drow["DesignationID"] = dr["DesignationID"].ToString();
                        drow["EmployeeID"] = dr["EmployeeID"].ToString();
                        dt1.Rows.Add(drow);
                    }
                }
                DataView myDataView = new DataView();
                myDataView = dt1.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }
                gridocapproval.DataSource = dt1;
                gridocapproval.DataBind();




            }


            else
            {
                gridocapproval.DataSource = null;
                gridocapproval.DataBind();
            }
        }
        else
        {
            gridocapproval.DataSource = null;
            gridocapproval.DataBind();
        }

        if (gridocapproval.Rows.Count > 0)
        {
            if (ddlapproval.SelectedValue == "None")
            {
                imgbtnSubmit.Visible = true;
            }

            if (ddlapproval.SelectedValue == "True")
            {
                imgbtnSubmit.Visible = false;
            }
            if (ddlapproval.SelectedValue == "False")
            {
                imgbtnSubmit.Visible = false;
            }




        }
        else
        {
            imgbtnSubmit.Visible = false;
        }

        foreach (GridViewRow grd in gridocapproval.Rows)
        {
            TextBox txtNote = (TextBox)grd.FindControl("txtNote");
            Label lbltxtnote = (Label)grd.FindControl("lbltxtnote");

            if (ddlapproval.SelectedValue == "None")
            {
                lbltxtnote.Visible = false;
                txtNote.Visible = true;
            }

            if (ddlapproval.SelectedValue == "True")
            {
                lbltxtnote.Visible = true;
                txtNote.Visible = false;

            }
            if (ddlapproval.SelectedValue == "False")
            {
                lbltxtnote.Visible = true;
                txtNote.Visible = false;
            }


        }

    }

    protected void imgbtnSubmit_Click(object sender, EventArgs e)
    {


        foreach (GridViewRow grd in gridocapproval.Rows)
        {
            Label lbllevelofaccess = (Label)grd.FindControl("lbllevelofaccess");
            Label lblemployeeid = (Label)grd.FindControl("lblemployeeid");
            Label lblstatusid = (Label)grd.FindControl("lblstatusid");
            Label lblapprovalstatus = (Label)grd.FindControl("lblapprovalstatus");
            Label lbldocumentid = (Label)grd.FindControl("lbldocumentid");
            TextBox txtNote = (TextBox)grd.FindControl("txtNote");
            DropDownList rbtnAcceptReject = (DropDownList)grd.FindControl("rbtnAcceptReject");
            Label lblmasterid = (Label)grd.FindControl("lblmasterid");



            if (rbtnAcceptReject.SelectedValue != "0" && rbtnAcceptReject.Enabled == true)
            {




                if (lbllevelofaccess.Text == "1")
                {

                    string strofficeclerk = " select * from  DocumentProcessing where DocumentId='" + lbldocumentid.Text + "' and Levelofaccess='1'  and ProcessingId<>'" + lblmasterid.Text + "'   ";
                    SqlCommand cmdofficeclerk = new SqlCommand(strofficeclerk, con);
                    cmdofficeclerk.CommandType = CommandType.Text;
                    SqlDataAdapter daofficeclerk = new SqlDataAdapter(cmdofficeclerk);
                    DataTable dtofficeclerk = new DataTable();
                    daofficeclerk.Fill(dtofficeclerk);

                    if (dtofficeclerk.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtofficeclerk.Rows)
                        {
                            string status = dr["StatusId"].ToString();
                            string masterid = dr["ProcessingId"].ToString();

                            if (rbtnAcceptReject.SelectedValue == "3")
                            {
                                if (status != "3")
                                {
                                    string str123office = " delete from  DocumentProcessing  where ProcessingId='" + masterid.ToString() + "'  ";
                                    SqlCommand cmd1123office = new SqlCommand(str123office, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd1123office.ExecuteNonQuery();
                                    con.Close();
                                }

                            }


                        }


                    }



                    int approve = 0;
                    if (rbtnAcceptReject.SelectedValue == "3")
                    {
                        approve = 1;
                    }
                    else
                    {
                        approve = 0;
                    }

                    string str1 = " update  DocumentProcessing set ApproveDate='" + System.DateTime.Now.ToShortDateString() + "',Note ='" + txtNote.Text + "',StatusId='" + rbtnAcceptReject.SelectedValue + "' , Approve='" + approve + "' where ProcessingId='" + lblmasterid.Text + "'  ";
                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();

                }
                if (lbllevelofaccess.Text == "2")
                {

                    string strt = " select * from  DocumentProcessing where DocumentId='" + lbldocumentid.Text + "' and (Levelofaccess='1' or Levelofaccess='2') and ProcessingId<>'" + lblmasterid.Text + "'   ";
                    SqlCommand cmd1t = new SqlCommand(strt, con);
                    cmd1t.CommandType = CommandType.Text;
                    SqlDataAdapter dat = new SqlDataAdapter(cmd1t);
                    DataTable dtt = new DataTable();
                    dat.Fill(dtt);

                    if (dtt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtt.Rows)
                        {
                            string status = dr["StatusId"].ToString();
                            string masterid = dr["ProcessingId"].ToString();

                            if (rbtnAcceptReject.SelectedValue == "3")
                            {
                                if (status != "3")
                                {
                                    string str123 = " delete from  DocumentProcessing  where ProcessingId='" + masterid.ToString() + "'  ";
                                    SqlCommand cmd1123 = new SqlCommand(str123, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd1123.ExecuteNonQuery();
                                    con.Close();
                                }

                            }
                            else if (rbtnAcceptReject.SelectedValue == "2")
                            {


                                string str12345 = " Update  DocumentProcessing set  StatusId='1' ,Approve='0' where ProcessingId='" + masterid.ToString() + "' and Levelofaccess<>'2' ";
                                SqlCommand cmd12345 = new SqlCommand(str12345, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmd12345.ExecuteNonQuery();
                                con.Close();

                            }

                        }


                    }
                    int approve = 0;
                    if (rbtnAcceptReject.SelectedValue == "3")
                    {
                        approve = 1;
                    }
                    else
                    {
                        approve = 0;
                    }
                    string str167 = " update  DocumentProcessing set ApproveDate='" + System.DateTime.Now.ToShortDateString() + "',Note ='" + txtNote.Text + "',StatusId='" + rbtnAcceptReject.SelectedValue + "' , Approve='" + approve + "' where ProcessingId='" + lblmasterid.Text + "'  ";
                    SqlCommand cmd167 = new SqlCommand(str167, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd167.ExecuteNonQuery();
                    con.Close();

                }

                if (lbllevelofaccess.Text == "3")
                {

                    string strt4565 = " select * from  DocumentProcessing where DocumentId='" + lbldocumentid.Text + "' and ( Levelofaccess='1' or Levelofaccess='2' or Levelofaccess='3')   and ProcessingId<>'" + lblmasterid.Text + "'   ";
                    SqlCommand cmd1t4565 = new SqlCommand(strt4565, con);
                    cmd1t4565.CommandType = CommandType.Text;
                    SqlDataAdapter dat = new SqlDataAdapter(cmd1t4565);
                    DataTable dtt = new DataTable();
                    dat.Fill(dtt);

                    if (dtt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtt.Rows)
                        {
                            string status = dr["StatusId"].ToString();
                            string masterid = dr["ProcessingId"].ToString();

                            if (rbtnAcceptReject.SelectedValue == "3")
                            {
                                if (status != "3")
                                {
                                    string str12586 = " delete from  DocumentProcessing  where ProcessingId='" + masterid.ToString() + "'  ";
                                    SqlCommand cmd12586 = new SqlCommand(str12586, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd12586.ExecuteNonQuery();
                                    con.Close();
                                }

                            }
                            else if (rbtnAcceptReject.SelectedValue == "2")
                            {


                                string str1789 = " Update  DocumentProcessing set  StatusId='1' ,Approve='0' where ProcessingId='" + masterid.ToString() + "' and Levelofaccess<>'3' ";
                                SqlCommand cmd1789 = new SqlCommand(str1789, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmd1789.ExecuteNonQuery();
                                con.Close();

                            }

                        }


                    }
                    int approve = 0;
                    if (rbtnAcceptReject.SelectedValue == "3")
                    {
                        approve = 1;
                    }
                    else
                    {
                        approve = 0;
                    }
                    string str1010 = " update  DocumentProcessing set ApproveDate='" + System.DateTime.Now.ToShortDateString() + "',Note ='" + txtNote.Text + "',StatusId='" + rbtnAcceptReject.SelectedValue + "' , Approve='" + approve + "' where ProcessingId='" + lblmasterid.Text + "'  ";
                    SqlCommand cmd1010 = new SqlCommand(str1010, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1010.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        lblmsg.Visible = true;
        lblmsg.Text = "Document approved successfully.";
        fillgrid();


    }
    public void setGridisize()
    {
        // doc grid
        // doc grid
        if (gridocapproval.Rows.Count == 0)
        {
            Panel2.CssClass = "GridPanel20";
        }
        else if (gridocapproval.Rows.Count == 1)
        {
            Panel2.CssClass = "GridPanel250";
        }
        else if (gridocapproval.Rows.Count == 2)
        {
            Panel2.CssClass = "GridPanel300";
        }
        else if (gridocapproval.Rows.Count == 3)
        {
            Panel2.CssClass = "GridPanel350";
        }
        else if (gridocapproval.Rows.Count == 4)
        {
            Panel2.CssClass = "GridPanel400";
        }
        else if (gridocapproval.Rows.Count == 5)
        {
            Panel2.CssClass = "GridPanel450";
        }
        else
        {
            Panel2.CssClass = "GridPanel475";
        }
    }
    protected void gridocapproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridocapproval.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void gridocapproval_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit1")
        {
            lblmsg.Text = "";
            int dk1 = Convert.ToInt32(e.CommandArgument);
            ViewState["MasterId"] = dk1.ToString();

            SqlCommand cmdedit = new SqlCommand("Select * from DocumentMaster where DocumentId='" + dk1 + "'", con);
            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);

            if (dtedit.Rows.Count > 0)
            {


                lbldocidmaster.Text = dtedit.Rows[0]["DocumentId"].ToString();

                filldll();
                ddlDocType.SelectedIndex = ddlDocType.Items.IndexOf(ddlDocType.Items.FindByValue(dtedit.Rows[0]["DocumentTypeId"].ToString()));
                FillParty();
                if (dtedit.Rows[0]["PartyId"].ToString() != null)
                {

                    ddlpartyname.SelectedIndex = ddlpartyname.Items.IndexOf(ddlpartyname.Items.FindByValue(dtedit.Rows[0]["PartyId"].ToString()));
                }
                txtdoctitle.Text = dtedit.Rows[0]["DocumentTitle"].ToString();

                txtdocrefnmbr.Text = dtedit.Rows[0]["DocumentRefNo"].ToString();
                txtnetamount.Text = dtedit.Rows[0]["DocumentAmount"].ToString();


                if (dtedit.Rows[0]["DocumentDate"].ToString() != "")
                {
                    TxtDocDate.Text = Convert.ToDateTime(dtedit.Rows[0]["DocumentDate"].ToString()).ToShortDateString();
                }
                else
                {
                    TxtDocDate.Text = "";
                }
                pnlupdatedoc.Visible = true;

            }
        }
    }
    protected void gridocapproval_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();
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
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {

        //filldesignation();
        //fillemployee();
        fillemployeeofficeclerk();
        fillemployeesupervisor();
       // fillgrid();


    }
   

    protected void ddlemp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillgrid();
    }
    protected void ddlapproval_SelectedIndexChanged(object sender, EventArgs e)
    {

       // fillgrid();


    }
    protected void gridocapproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbllevelofaccess = (Label)e.Row.FindControl("lbllevelofaccess");
            Label lblofficeclarkapproval = (Label)e.Row.FindControl("lblofficeclarkapproval");
            Label lblsupervisorapproval = (Label)e.Row.FindControl("lblsupervisorapproval");
            Label lblstatusid = (Label)e.Row.FindControl("lblstatusid");
            Label lblapprovalstatus = (Label)e.Row.FindControl("lblapprovalstatus");
            Label lbldocumentid = (Label)e.Row.FindControl("lbldocumentid");
            TextBox txtNote = (TextBox)e.Row.FindControl("txtNote");
            DropDownList rbtnAcceptReject = (DropDownList)e.Row.FindControl("rbtnAcceptReject");
            Label lblmasterid = (Label)e.Row.FindControl("lblmasterid");
            Image Image2 = (Image)e.Row.FindControl("Image2");
            ImageButton llinedit = (ImageButton)e.Row.FindControl("llinedit");
            ImageButton ImageButton2 = (ImageButton)e.Row.FindControl("ImageButton2");
            Image Image1 = (Image)e.Row.FindControl("Image1");
            ImageButton ImageButton3 = (ImageButton)e.Row.FindControl("ImageButton3");


            if (lbllevelofaccess.Text == "1")
            {
                if (lblstatusid.Text != "")
                {
                    rbtnAcceptReject.SelectedValue = lblstatusid.Text;
                }

                if (lblstatusid.Text == "3" || lblstatusid.Text == "2")
                {
                    rbtnAcceptReject.Enabled = false;

                    llinedit.Visible = false;
                    ImageButton2.Visible = true;

                    Image1.Visible = false;
                    ImageButton3.Visible = true;
                }
                else
                {
                    rbtnAcceptReject.Enabled = true;
                    llinedit.Enabled = true;
                    ImageButton2.Visible = false;

                    Image1.Visible = true;
                    ImageButton3.Visible = false;
                }
            }

            if (lbllevelofaccess.Text == "2")
            {
                if (lblstatusid.Text != "")
                {
                    rbtnAcceptReject.SelectedValue = lblstatusid.Text;
                }

                string strt = " select * from  DocumentProcessing where DocumentId='" + lbldocumentid.Text + "' and  Levelofaccess='3' ";
                SqlCommand cmd1t = new SqlCommand(strt, con);
                cmd1t.CommandType = CommandType.Text;
                SqlDataAdapter dat = new SqlDataAdapter(cmd1t);
                DataTable dtt = new DataTable();
                dat.Fill(dtt);

                if (dtt.Rows.Count > 0)
                {

                    int flag = 0;
                    foreach (DataRow dr in dtt.Rows)
                    {
                        string status = dr["StatusId"].ToString();

                        if (status == "3")
                        {
                            flag = 1;

                        }


                    }
                    if (flag == 1)
                    {
                        rbtnAcceptReject.Enabled = false;

                        llinedit.Visible = false;
                        ImageButton2.Visible = true;

                        Image1.Visible = false;
                        ImageButton3.Visible = true;
                    }
                    else
                    {
                        rbtnAcceptReject.Enabled = true;
                        llinedit.Enabled = true;
                        ImageButton2.Visible = false;

                        Image1.Visible = true;
                        ImageButton3.Visible = false;
                    }

                }
                else
                {
                    rbtnAcceptReject.Enabled = true;
                    llinedit.Enabled = true;
                    ImageButton2.Visible = false;

                    Image1.Visible = true;
                    ImageButton3.Visible = false;
                }

            }
            if (lbllevelofaccess.Text == "3")
            {
                if (lblstatusid.Text != "")
                {
                    rbtnAcceptReject.SelectedValue = lblstatusid.Text;
                }
            }



            string strofficeclerk = " select DocumentProcessing.*,EmployeeMaster.EmployeeName,case when (DocumentProcessing.StatusId='0') then 'Pending-New' else  (case when (DocumentProcessing.StatusId='1') then 'Pending-Returned' else  (case when (DocumentProcessing.StatusId='2') then 'Rejected'  else (case when (DocumentProcessing.StatusId='3') then 'Approved'  else  '' End ) End   ) End )  End  as Statuslabel from  DocumentProcessing inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=DocumentProcessing.EmployeeId where DocumentId='" + lbldocumentid.Text + "' and  Levelofaccess='1'  ";
            SqlCommand cmdofficeclerk = new SqlCommand(strofficeclerk, con);
            cmdofficeclerk.CommandType = CommandType.Text;
            SqlDataAdapter adpofficeclerk = new SqlDataAdapter(cmdofficeclerk);
            DataTable dtofficeclerk = new DataTable();
            adpofficeclerk.Fill(dtofficeclerk);

            if (dtofficeclerk.Rows.Count > 0)
            {
                string strId = "";
                string strInvAllIds = "";
                string strtemp = "";

                foreach (DataRow dtrrr in dtofficeclerk.Rows)
                {
                    strId = dtrrr["EmployeeName"].ToString() + "-" + dtrrr["Statuslabel"].ToString();
                    strInvAllIds = strId + " <br/>" + strInvAllIds;
                    strtemp = strInvAllIds.Substring(0, (strInvAllIds.Length - 1));
                }
                lblofficeclarkapproval.Text = strtemp.ToString();
            }
            else
            {
                lblofficeclarkapproval.Text = "";
            }

            string strsupervisorapprove = " select DocumentProcessing.*,EmployeeMaster.EmployeeName,case when (DocumentProcessing.StatusId='0') then 'Pending-New' else  (case when (DocumentProcessing.StatusId='1') then 'Pending-Returned' else  (case when (DocumentProcessing.StatusId='2') then 'Rejected'  else (case when (DocumentProcessing.StatusId='3') then 'Approved'  else  '' End ) End   ) End )  End  as Statuslabel from  DocumentProcessing inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=DocumentProcessing.EmployeeId where DocumentId='" + lbldocumentid.Text + "' and  Levelofaccess='2'  ";
            SqlCommand cmdsupervisorapprove = new SqlCommand(strsupervisorapprove, con);
            cmdsupervisorapprove.CommandType = CommandType.Text;
            SqlDataAdapter adpsupervisorapprove = new SqlDataAdapter(cmdsupervisorapprove);
            DataTable dtsupervisorapprove = new DataTable();
            adpsupervisorapprove.Fill(dtsupervisorapprove);

            if (dtsupervisorapprove.Rows.Count > 0)
            {
                string strId1 = "";
                string strInvAllIds1 = "";
                string strtemp1 = "";

                foreach (DataRow dtrrr in dtsupervisorapprove.Rows)
                {
                    strId1 = dtrrr["EmployeeName"].ToString() + "-" + dtrrr["Statuslabel"].ToString();

                    strInvAllIds1 = strId1 + " <br/>" + strInvAllIds1;
                    strtemp1 = strInvAllIds1.Substring(0, (strInvAllIds1.Length - 1));
                }
                lblsupervisorapproval.Text = strtemp1.ToString();
            }
            else
            {
                lblsupervisorapproval.Text = "";
            }

            DataTable dtmastr = LoadPdf(Convert.ToInt32(lbldocumentid.Text));
            if (dtmastr.Rows.Count > 0)
            {

                Image2.ImageUrl = dtmastr.Rows[0]["image"].ToString();
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnl_grid_priceplan.ScrollBars = ScrollBars.None;
            pnl_grid_priceplan.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (gridocapproval.Columns[14].Visible == true)
            {
                ViewState["editHide"] = "tt";
                gridocapproval.Columns[14].Visible = false;
            }
            if (gridocapproval.Columns[15].Visible == true)
            {
                ViewState["VieweditHide"] = "tt";
                gridocapproval.Columns[15].Visible = false;
            }

        }
        else
        {

            pnl_grid_priceplan.ScrollBars = ScrollBars.Vertical;
            pnl_grid_priceplan.Height = new Unit(250);

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                gridocapproval.Columns[14].Visible = true;
            }
            if (ViewState["VieweditHide"] != null)
            {
                gridocapproval.Columns[15].Visible = true;
            }

        }
    }
    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }
    protected void fillgrid()
    {
        lblBusiness.Text = ddlbusiness.SelectedItem.Text;

        columndisplay();
        string straprange = "";

        int flag = 1;
        if (ddlapproval.SelectedValue == "2" || ddlapproval.SelectedValue == "3")
        {
            flag = 0;
            string strdestype = "";
            DataTable dts = select("Select DesignationName from DesignationMaster where DesignationMasterId='"+Session["DesignationId"]+"'");
            if (dts.Rows.Count > 0)
            {
                if (Convert.ToString(dts.Rows[0]["DesignationName"]) == "Office Clerk")
                {
                    strdestype="'1','4'";
                }
                else if (Convert.ToString(dts.Rows[0]["DesignationName"]) == "Supervisor")
                {
                    strdestype = "'1','3'";
                }
                else if (Convert.ToString(dts.Rows[0]["DesignationName"]) == "Manager")
                {
                    strdestype = "'1','2'";
                }
                if (strdestype != "")
                {
                    flag = 1;
                    DataTable dtac = select("Select top(1) * from DocumentViewRuleMaster where Whid='" + ddlbusiness.SelectedValue + "' and DesId in(" + strdestype + ") order by DocViewRuleId Desc");
                    if (dtac.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtac.Rows[0]["RuleSelectId"]) == "1")
                        {
                            straprange = "";
                        }
                        else if (Convert.ToString(dtac.Rows[0]["RuleSelectId"]) == "2")
                        {
                            straprange = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + dtac.Rows[0]["FromDate"] + "' and '" + dtac.Rows[0]["ToDate"] + "'";

                        }
                        else if (Convert.ToString(dtac.Rows[0]["RuleSelectId"]) == "3")
                        {
                            straprange = " and DocumentMaster.DocumentId  between '" + dtac.Rows[0]["FromId"] + "' and '" + dtac.Rows[0]["ToId"] + "'";
                        }
                    }
                }
            }

        }
        if (flag == 1)
        {
            string strmaster = "select   DocumentMaster.*, DocumentType.DocumentType, Party_Master.Compname as PartyName,EmployeeMaster.EmployeeName, DocumentProcessing.ProcessingId as DocumentProcessingId,DocumentProcessing.ProcessingId ,DocumentProcessing.DocAllocateDate,DocumentProcessing.ApproveDate, DocumentProcessing.Approve, DocumentProcessing.Note, DocumentProcessing.StatusId,DocumentProcessing.Levelofaccess,DesignationMaster.DesignationName,DesignationMaster.DesignationMasterID as DesignationID , EmployeeMaster.EmployeeMasterID as EmployeeIDNew   from DocumentMainType inner join     DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster on  DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId inner join DocumentProcessing on  DocumentProcessing.DocumentId= DocumentMaster.DocumentId inner join  EmployeeMaster ON DocumentProcessing.EmployeeId = EmployeeMaster.EmployeeMasterID inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID left outer join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId where DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and DocumentProcessing.EmployeeId = '" + ddlemp.SelectedValue + "' and DocumentProcessing.CID='" + Session["Comid"].ToString() + "'  ";

            string status = "";
            string strbyperiod = "";
            string strbydate = "";
            string strsearch = "";
            string strfilterbyofficeclerk = "";
            string strfilterbysupervisor = "";

            if (ddlapproval.SelectedValue == "5")
            {
                status = " AND (DocumentProcessing.StatusId='0' or DocumentProcessing.StatusId='1') ";

            }
            else
            {
                status = " AND DocumentProcessing.StatusId='" + ddlapproval.SelectedValue + "' ";
            }

            if (RadioButtonList1.SelectedValue == "0")
            {

                if (ViewState["periodstartdate"] != null && ViewState["periodenddate"] != null)
                {
                    strbyperiod = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + ViewState["periodstartdate"].ToString() + "' and '" + ViewState["periodenddate"].ToString() + "'";
                }
            }
            if (RadioButtonList1.SelectedValue == "1")
            {
                if (txtfrom.Text != "" && txtto.Text != "")
                {
                    strbydate = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + txtfrom.Text + "' and '" + txtto.Text + "'";
                }
            }
            if (txtsearch.Text != "")
            {
                strsearch = " and DocumentMaster.DocumentTitle Like '%" + txtsearch.Text.Replace("'", "''") + "%' ";
            }


            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue == "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex == 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }


            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue == "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex == 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }


            string finalstr = strmaster + status + strsearch + strbyperiod + strbydate + strfilterbyofficeclerk + strfilterbysupervisor + straprange;

            SqlCommand cmdeeed = new SqlCommand(finalstr, con);
            SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            DataTable dteeed = new DataTable();
            adpeeed.Fill(dteeed);


            DataView myDataView = new DataView();
            myDataView = dteeed.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            gridocapproval.DataSource = dteeed;
            gridocapproval.DataBind();

        }
        else
        {
            gridocapproval.DataSource = null;
            gridocapproval.DataBind();

        }


        if (gridocapproval.Rows.Count > 0)
        {
           
            imgbtnSubmit.Visible = true;
        }
        else
        {
            imgbtnSubmit.Visible = false;
        }



    }
   
    protected DataTable LoadPdf(int Docid)
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDoucmentMasterByID(Docid);
        string docname = dt.Rows[0]["DocumentName"].ToString();
        ViewState["decname"] = docname.ToString();

        string filepath = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + docname);

        string strft = "Select FileStorage.* from FileStorage Where B='" + encryptstrring(Session["comid"].ToString()) + "' and H='" + encryptstrring("True") + "'";

        SqlCommand cmdft = new SqlCommand(strft, con);
        SqlDataAdapter adpft = new SqlDataAdapter(cmdft);
        DataTable dtft = new DataTable();
        adpft.Fill(dtft);

        if (dtft.Rows.Count > 0)
        {
            FileInfo filec = new FileInfo(filepath);
            if (!filec.Exists)
            {
                datatransftp(docname, filepath);
                System.Threading.Thread.Sleep(1000);
                FileInfo filecup = new FileInfo(filepath);
                if (filecup.Exists)
                {

                    string filepathu = Server.MapPath("~//Account//pdftoimage.exe");
                    System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepathu);

                    pti.UseShellExecute = false;
                    pti.Arguments = filepathu + " -i UploadedDocuments//" + docname + " " + "-o" + " " + "DocumentImage//";//+ " " + "-r" + "VNKSURDLWQOVHPGH";


                    pti.RedirectStandardOutput = true;
                    pti.RedirectStandardInput = true;
                    pti.RedirectStandardError = true;

                    pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");
                    System.Diagnostics.Process ps = Process.Start(pti);
                    System.Threading.Thread.Sleep(1000);

                }
            }

        }
        int length = docname.Length;
        string docnameIn = docname.Substring(0, length - 4);


        ViewState["path"] = filepath.ToString();
        DataTable dt1 = new DataTable();
        DataColumn dcom = new DataColumn();
        dcom.ColumnName = "image";
        dcom.DataType = System.Type.GetType("System.String");
        dt1.Columns.Add(dcom);
        string Location = Server.MapPath("~//Account//" + Session["comid"] + "//DocumentImage//");
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);

        foreach (System.IO.FileInfo f in dir.GetFiles(docnameIn.ToString() + "*.*"))
        {

            DataRow drow = dt1.NewRow();
            drow["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + f.Name.ToString();
            dt1.Rows.Add(drow);


        }

        return dt1;
    }
    public string encryptstrring(string strText)
    {
        return Encrypt(strText, "&%#@?,:*");
    }
    protected void datatransftp(string doc, string filepath)
    {


        string str1 = "Select FileStorage.* from FileStorage Where B='" + encryptstrring(Session["comid"].ToString()) + "' and H='" + encryptstrring("True") + "'";

        SqlCommand cmd = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {

            string ftpur = decryptstring(dt.Rows[0]["C"].ToString());
            string ftpport = decryptstring(dt.Rows[0]["D"].ToString());

            string ftpuser = decryptstring(dt.Rows[0]["E"].ToString());
            string ftppass = decryptstring(dt.Rows[0]["F"].ToString());
            string ftpcond = decryptstring(dt.Rows[0]["G"].ToString());
            string ftpallowed = decryptstring(dt.Rows[0]["H"].ToString());
            string[] separator1 = new string[] { "/" };
            string[] strSplitArr1 = ftpur.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

            String productno = strSplitArr1[0].ToString();
            string ftpurl = "";

            if (productno == "FTP:" || productno == "ftp:")
            {
                if (strSplitArr1.Length >= 3)
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + ftpport;
                    for (int i = 2; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + ftpport;

                }
            }
            else
            {
                if (strSplitArr1.Length >= 2)
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + ftpport;
                    for (int i = 1; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + ftpport;

                }

            }
            if (ftpur.Length > 0)
            {

                string ftphost = ftpurl + "/";
                string fnname = doc;

                GetFile(ftphost, fnname, filepath, ftpuser, ftppass);

            }
        }
    }
    public string decryptstring(string str)
    {
        return Decrypt(str, "&%#@?,:*");
    }
    private string Decrypt(string strText, string strEncrypt)
    {
        byte[] bKey = new byte[20];
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private string Encrypt(string strtxt, string strtoencrypt)
    {
        byte[] bykey = new byte[20];
        byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bykey = System.Text.Encoding.UTF8.GetBytes(strtoencrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(strtxt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, dv), CryptoStreamMode.Write);
            cs.Write(inputArray, 0, inputArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public bool GetFile(string ftp, string filename, string Destpath, string username, string password)
    {
        try
        {
            FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.
               Create(ftp.ToString() + filename.ToString());

            oFTP.Credentials = new NetworkCredential(username.ToString(), password.ToString());
            oFTP.UseBinary = false;
            oFTP.UsePassive = true;
            oFTP.Method = WebRequestMethods.Ftp.DownloadFile;


            FtpWebResponse response =
               (FtpWebResponse)oFTP.GetResponse();
            Stream responseStream = response.GetResponseStream();

            FileStream fs = new FileStream(Destpath, FileMode.CreateNew);
            Byte[] buffer = new Byte[2047];
            int read = 1;
            while (read != 0)
            {
                read = responseStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, read);
            }

            responseStream.Close();
            fs.Flush();
            fs.Close();
            responseStream.Close();
            response.Close();

            oFTP = null;

        }
        catch (Exception ecx)
        {
        }
        return true;
    }

    public void filldll()
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocTypeAll(ddlbusiness.SelectedValue);
        ddlDocType.DataSource = dt;
        ddlDocType.DataTextField = "doctype";
        ddlDocType.DataValueField = "DocumentTypeId";
        ddlDocType.DataBind();
        ddlDocType.SelectedIndex = ddlDocType.Items.IndexOf(ddlDocType.Items.FindByText("GENERAL - GENERAL - GENERAL"));

    }
    protected void FillParty()
    {
        DataTable dt = new DataTable();
        dt = clsDocument.selectparty(ddlbusiness.SelectedValue);
        ddlpartyname.DataSource = dt;
        ddlpartyname.DataTextField = "PartyName";
        ddlpartyname.DataValueField = "PartyId";
        ddlpartyname.DataBind();
        ddlpartyname.Items.Insert(0, "-Select-");
        ddlpartyname.Items[0].Value = "0";


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (ViewState["MasterId"].ToString() != null)
        {

            string str1010 = " update  DocumentMaster set DocumentTypeId='" + ddlDocType.SelectedValue + "',DocumentTitle='" + txtdoctitle.Text + "',PartyId='" + ddlpartyname.SelectedValue + "' ,DocumentDate='" + TxtDocDate.Text + "',DocumentRefNo='" + txtdocrefnmbr.Text + "' ,DocumentAmount='" + txtnetamount.Text + "' where DocumentId='" + ViewState["MasterId"].ToString() + "' ";

            SqlCommand cmd1010 = new SqlCommand(str1010, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1010.ExecuteNonQuery();
            con.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully.";
            fillgrid();

            pnlupdatedoc.Visible = false;
            txtdoctitle.Text = "";
            ddlpartyname.SelectedIndex = -1;
            ddlDocType.SelectedIndex = -1;

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        pnlupdatedoc.Visible = false;
        txtdoctitle.Text = "";
        ddlpartyname.SelectedIndex = -1;
        ddlDocType.SelectedIndex = -1;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Button4.Text == "Select Display Columns")
        {
            Button4.Text = "Hide Display Columns";
            Panel6.Visible = true;
        }
        else
        {
            Button4.Text = "Select Display Columns";
            Panel6.Visible = false;

        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void columndisplay()
    {
        if (chkidcolumn.Checked == true)
        {
            gridocapproval.Columns[0].Visible = true;
        }
        else
        {
            gridocapproval.Columns[0].Visible = false;
        }
        if (chktitlecolumn.Checked == true)
        {
            gridocapproval.Columns[1].Visible = true;
        }
        else
        {
            gridocapproval.Columns[1].Visible = false;

        }
        if (chkfileextsion.Checked == true)
        {
            gridocapproval.Columns[2].Visible = true;
        }
        else
        {
            gridocapproval.Columns[2].Visible = false;
        }
        if (chkfoldername.Checked == true)
        {
            gridocapproval.Columns[3].Visible = true;
        }
        else
        {
            gridocapproval.Columns[3].Visible = false;
        }

        if (chkpartycolumn.Checked == true)
        {
            gridocapproval.Columns[4].Visible = true;
        }
        else
        {
            gridocapproval.Columns[4].Visible = false;
        }


        if (chkdocumentdate.Checked == true)
        {

            gridocapproval.Columns[5].Visible = true;
        }
        else
        {
            gridocapproval.Columns[5].Visible = false;
        }

        if (chkrefno.Checked == true)
        {

            gridocapproval.Columns[6].Visible = true;
        }
        else
        {
            gridocapproval.Columns[6].Visible = false;
        }


        if (chkdocamount.Checked == true)
        {

            gridocapproval.Columns[7].Visible = true;
        }
        else
        {
            gridocapproval.Columns[7].Visible = false;
        }




        if (chkuploaddate.Checked == true)
        {

            gridocapproval.Columns[8].Visible = true;
        }
        else
        {
            gridocapproval.Columns[8].Visible = false;
        }

        if (chkmyfoldercolumn.Checked == true)
        {
            gridocapproval.Columns[9].Visible = true;
        }
        else
        {
            gridocapproval.Columns[9].Visible = false;
        }

        if (chkaddtomyfoldercolumn.Checked == true)
        {
            gridocapproval.Columns[10].Visible = true;

        }
        else
        {
            gridocapproval.Columns[10].Visible = false;
        }

        if (chkaccountentrycolumn.Checked == true)
        {
            gridocapproval.Columns[11].Visible = true;
        }
        else
        {
            gridocapproval.Columns[11].Visible = false;
        }

        if (chksendmessagecolumn.Checked == true)
        {
            gridocapproval.Columns[12].Visible = true;
        }
        else
        {
            gridocapproval.Columns[12].Visible = false;
        }


    }


    protected void ddlDuration_SelectedIndexChanged(object sender, EventArgs e)
    {

        filldatebyperiod();
        //fillgrid();

    }

    protected void filldatebyperiod()
    {
        //date between you should use  date first and earlier date lateafter
        string Today, Yesterday, ThisYear;
        Today = Convert.ToString(System.DateTime.Today.ToShortDateString());
        Yesterday = Convert.ToString(System.DateTime.Today.AddDays(-1).ToShortDateString());
        ThisYear = Convert.ToString(System.DateTime.Today.Year.ToString());


        //-------------------this week start...............
        DateTime d1, d2, d3, d4, d5, d6, d7;
        DateTime weekstart, weekend;
        d1 = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());
        d2 = Convert.ToDateTime(System.DateTime.Today.AddDays(-1).ToShortDateString());
        d3 = Convert.ToDateTime(System.DateTime.Today.AddDays(-2).ToShortDateString());
        d4 = Convert.ToDateTime(System.DateTime.Today.AddDays(-3).ToShortDateString());
        d5 = Convert.ToDateTime(System.DateTime.Today.AddDays(-4).ToShortDateString());
        d6 = Convert.ToDateTime(System.DateTime.Today.AddDays(-5).ToShortDateString());
        d7 = Convert.ToDateTime(System.DateTime.Today.AddDays(-6).ToShortDateString());
        string ThisWeek = (System.DateTime.Today.DayOfWeek.ToString());
        if (ThisWeek.ToString() == "Monday")
        {
            weekstart = d1;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(ThisWeek) == "Tuesday")
        {
            weekstart = d2;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Wednesday")
        {
            weekstart = d3;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Thursday")
        {
            weekstart = d4;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Friday")
        {
            weekstart = d5;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Saturday")
        {
            weekstart = d6;
            weekend = weekstart.Date.AddDays(+6);

        }
        else
        {
            weekstart = d7;
            weekend = weekstart.Date.AddDays(+6);
        }
        string thisweekstart = weekstart.ToShortDateString();
        string thisweekend = weekend.ToShortDateString();

        //.................this week duration end.....................

        ///--------------------last week duration ....

        DateTime d17, d8, d9, d10, d11, d12, d13;
        DateTime lastweekstart, lastweekend;
        d17 = Convert.ToDateTime(System.DateTime.Today.AddDays(-7).ToShortDateString());
        d8 = Convert.ToDateTime(System.DateTime.Today.AddDays(-8).ToShortDateString());
        d9 = Convert.ToDateTime(System.DateTime.Today.AddDays(-9).ToShortDateString());
        d10 = Convert.ToDateTime(System.DateTime.Today.AddDays(-10).ToShortDateString());
        d11 = Convert.ToDateTime(System.DateTime.Today.AddDays(-11).ToShortDateString());
        d12 = Convert.ToDateTime(System.DateTime.Today.AddDays(-12).ToShortDateString());
        d13 = Convert.ToDateTime(System.DateTime.Today.AddDays(-13).ToShortDateString());
        string thisday = (System.DateTime.Today.DayOfWeek.ToString());
        if (thisday.ToString() == "Monday")
        {
            lastweekstart = d17;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(thisday) == "Tuesday")
        {
            lastweekstart = d8;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Wednesday")
        {
            lastweekstart = d9;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Thursday")
        {
            lastweekstart = d10;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Friday")
        {
            lastweekstart = d11;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Saturday")
        {
            lastweekstart = d12;
            lastweekend = lastweekstart.Date.AddDays(+6);

        }
        else
        {
            lastweekstart = d13;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        string lastweekstartdate = lastweekstart.ToShortDateString();
        string lastweekenddate = lastweekend.ToShortDateString();
        //---------------lastweek duration end.................

        //        Today
        //2	Yesterday
        //3	ThisWeek
        //4	LastWeek
        //5	ThisMonth
        //6	LastMonth
        //7	ThisQuarter
        //8	LastQuarter
        //9	ThisYear
        //10Last Year
        //------------------this month period-----------------

        DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
        string thismonthstartdate = thismonthstart.ToShortDateString();
        string thismonthenddate = Today.ToString();
        //------------------this month period end................



        //-----------------last month period start ---------------
        int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
        if (lastmonthno == 0)
        {
            lastmonthno = 12;
            Int32 ThisYearstr = Convert.ToInt32(ThisYear) - 1;
            ThisYear = Convert.ToString(ThisYearstr);
        }
       
        string lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        DateTime lastmonth = Convert.ToDateTime(lastmonthNumber.ToString() + "/1/" + ThisYear.ToString());
        string lastmonthstart = lastmonth.ToShortDateString();
        string lastmonthend = "";

        if (lastmonthNumber == "1" || lastmonthNumber == "3" || lastmonthNumber == "5" || lastmonthNumber == "7" || lastmonthNumber == "8" || lastmonthNumber == "10" || lastmonthNumber == "12")
        {
            lastmonthend = lastmonthNumber + "/31/" + ThisYear.ToString();
        }
        else if (lastmonthNumber == "4" || lastmonthNumber == "6" || lastmonthNumber == "9" || lastmonthNumber == "11")
        {
            lastmonthend = lastmonthNumber + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                lastmonthend = lastmonthNumber + "/29/" + ThisYear.ToString();
            }
            else
            {
                lastmonthend = lastmonthNumber + "/28/" + ThisYear.ToString();
            }
        }

        string lastmonthstartdate = lastmonthstart.ToString();
        string lastmonthenddate = lastmonthend.ToString();


        //-----------------last month period end -----------------------

        //--------------------this quater period start ----------------


        int thisqtr = 0;
        string thisqtrNumber = "";
        int mon = Convert.ToInt32(DateTime.Now.Month.ToString());
        if (mon >= 1 && mon <= 3)
        {
            thisqtr = 1;
            thisqtrNumber = "3";

        }
        else if (mon >= 4 && mon <= 6)
        {
            thisqtr = 4;
            thisqtrNumber = "6";
        }
        else if (mon >= 7 && mon <= 9)
        {
            thisqtr = 7;
            thisqtrNumber = "9";
        }
        else if (mon >= 10 && mon <= 12)
        {
            thisqtr = 10;
            thisqtrNumber = "12";
        }


        DateTime thisquater = Convert.ToDateTime(thisqtr.ToString() + "/1/" + ThisYear.ToString());
        string thisquaterstart = thisquater.ToShortDateString();

        string thisquaterend = "";

        if (thisqtrNumber == "1" || thisqtrNumber == "3" || thisqtrNumber == "5" || thisqtrNumber == "7" || thisqtrNumber == "8" || thisqtrNumber == "10" || thisqtrNumber == "12")
        {
            thisquaterend = thisqtrNumber + "/31/" + ThisYear.ToString();
        }
        else if (thisqtrNumber == "4" || thisqtrNumber == "6" || thisqtrNumber == "9" || thisqtrNumber == "11")
        {
            thisquaterend = thisqtrNumber + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                thisquaterend = thisqtrNumber + "/29/" + ThisYear.ToString();
            }
            else
            {
                thisquaterend = thisqtrNumber + "/28/" + ThisYear.ToString();
            }
        }

        string thisquaterstartdate = thisquaterstart.ToString();
        string thisquaterenddate = thisquaterend.ToString();


        // --------------this quater period end ------------------------

        // --------------last quater period start----------------------

        //int lastqtr = Convert.ToInt32(thismonthstart.AddMonths(-5).Month.ToString());// -5;
        //string lastqtrNumber = Convert.ToString(lastqtr.ToString());
        //int lastqater3 = Convert.ToInt32(thismonthstart.AddMonths(-3).Month.ToString());
        int lastqtr = Convert.ToInt32(Convert.ToDateTime(thisquaterstartdate).AddMonths(-3).Month.ToString());// -5;
        string lastqtrNumber = Convert.ToString(lastqtr.ToString());
        int lastqater3 = Convert.ToInt32(Convert.ToDateTime(thisquaterenddate).AddMonths(-3).Month.ToString());
        //DateTime lastqater3 = Convert.ToDateTime(System.DateTime.Now.AddMonths(-3).Month.ToString());
        string lasterquater3 = lastqater3.ToString();
        DateTime lastquater = Convert.ToDateTime(lastqtrNumber.ToString() + "/1/" + ThisYear.ToString());
        string lastquaterstart = lastquater.ToShortDateString();
        string lastquaterend = "";

        if (lasterquater3 == "1" || lasterquater3 == "3" || lasterquater3 == "5" || lasterquater3 == "7" || lasterquater3 == "8" || lasterquater3 == "10" || lasterquater3 == "12")
        {
            lastquaterend = lasterquater3 + "/31/" + ThisYear.ToString();
        }
        else if (lasterquater3 == "4" || lasterquater3 == "6" || lasterquater3 == "9" || lasterquater3 == "11")
        {
            lastquaterend = lasterquater3 + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                lastquaterend = lasterquater3 + "/29/" + ThisYear.ToString();
            }
            else
            {
                lastquaterend = lasterquater3 + "/28/" + ThisYear.ToString();
            }
        }

        string lastquaterstartdate = lastquaterstart.ToString();
        string lastquaterenddate = lastquaterend.ToString();

        //--------------last quater period end-------------------------

        //--------------this year period start----------------------
        DateTime thisyearstart = Convert.ToDateTime("1/1/" + ThisYear.ToString());
        DateTime thisyearend = Convert.ToDateTime("12/31/" + ThisYear.ToString());

        string thisyearstartdate = thisyearstart.ToShortDateString();
        string thisyearenddate = thisyearend.ToShortDateString();

        //---------------this year period end-------------------
        //--------------this year period start----------------------
        DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());

        string lastyearstartdate = lastyearstart.ToShortDateString();
        string lastyearenddate = lastyearend.ToShortDateString();



        //---------------this year period end-------------------


        string periodstartdate = "";
        string periodenddate = "";

        if (ddlDuration.SelectedItem.Text == "Today")
        {
            periodstartdate = Today.ToString();
            periodenddate = Today.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "Yesterday")
        {
            periodstartdate = Yesterday.ToString();
            periodenddate = Yesterday.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "This Week")
        {
            periodstartdate = thisweekstart.ToString();
            periodenddate = thisweekend.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "Last Week")
        {

            periodstartdate = lastweekstartdate.ToString();
            periodenddate = lastweekenddate.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "This Month")
        {

            periodstartdate = thismonthstart.ToShortDateString();
            periodenddate = Today.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "Last Month")
        {

            periodstartdate = lastmonthstartdate.ToString();
            periodenddate = lastmonthenddate.ToString();


        }
        else if (ddlDuration.SelectedItem.Text == "This Quarter")
        {

            periodstartdate = thisquaterstartdate.ToString();
            periodenddate = thisquaterenddate.ToString();


        }
        else if (ddlDuration.SelectedItem.Text == "Last Quarter")
        {

            periodstartdate = lastquaterstartdate.ToString();
            periodenddate = lastquaterenddate.ToString();


        }

        else if (ddlDuration.SelectedItem.Text == "This Year")
        {

            periodstartdate = thisyearstartdate.ToString();
            periodenddate = thisyearenddate.ToString();


        }
        else if (ddlDuration.SelectedItem.Text == "Last Year")
        {

            periodstartdate = lastyearstartdate.ToString();
            periodenddate = lastyearenddate.ToString();
        }
        else
        {
            periodstartdate = Today.ToString();
            periodenddate = Today.ToString();
        }
        if (periodstartdate.Length > 0)
        {
            txtfrom.Text = periodstartdate;
            txtto.Text = periodenddate;

        }
        ViewState["periodstartdate"] = periodstartdate.ToString();
        ViewState["periodenddate"] = periodenddate.ToString();
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (RadioButtonList1.SelectedValue == "0")
        {
            pnlperiod.Visible = true;
            pnldate.Visible = false;
        }
        else
        {
            pnlperiod.Visible = false;
            pnldate.Visible = true;
        }
        filldatebyperiod();
        fillgrid();

    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
   

    protected void linkdow1_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;
        Label lbldocumentid = (Label)gridocapproval.Rows[rinrow].FindControl("lbldocumentid");

        DataTable dtmastr = LoadPdf(Convert.ToInt32(lbldocumentid.Text));

        if (dtmastr.Rows.Count > 0)
        {
            DataList1.DataSource = dtmastr;
            DataList1.DataBind();
            //Image2DocView.ImageUrl = dtmastr.Rows[0]["image"].ToString();
        }
        ModalPopupExtender3.Show();

    }




    
}
