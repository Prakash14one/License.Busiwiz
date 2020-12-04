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
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class Account_DocumentAccesRightdupli : System.Web.UI.Page
{
    DocumentCls1 clsDocument = new DocumentCls1();
    MasterCls1 clsMaster;
    EmployeeCls clsEmployee = new EmployeeCls();


    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        DataSet ds = new DataSet();

        //compid = Session["comid"].ToString();

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);




        MasterCls1 clsMaster = new MasterCls1();
        DataTable dt = new DataTable();
        DocumentCls1 clsDocument = new DocumentCls1();
        EmployeeCls clsEmployee = new EmployeeCls();
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            lblcomname.Text = Session["comid"].ToString();
            lblBusiness.Text = "ALL";
            //lblCabinet.Text = "ALL";
            //lblDrawer.Text = "ALL";
            //lblFolder.Text = "ALL";
            lblDepartment.Text = "ALL";
            //ViewState["p1"] = Request.UrlReferrer.ToString();

        }

        if (Session["CompanyName"] != null)
        {
            this.Title = Session["CompanyName"] + " IFileCabinet.com - Document Access Rights ";
        }

        Session["PageName"] = "DocumentAccesRightdupli.aspx";


        if (!IsPostBack)
        {
            string str = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' and EmployeeWarehouseRights.AccessAllowed='True' order by name";
            ViewState["sortOrder"] = "";
            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dtb = new DataTable();
            da.Fill(dtb);

            ddlbusiness.DataSource = dtb;
            ddlbusiness.DataTextField = "Name";
            ddlbusiness.DataValueField = "WareHouseId";
            ddlbusiness.DataBind();

            ddlbussele.DataSource = dtb;
            ddlbussele.DataTextField = "Name";
            ddlbussele.DataValueField = "WareHouseId";
            ddlbussele.DataBind();

         
            string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
            SqlCommand cmdeeed = new SqlCommand(eeed, con);
            SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            DataTable dteeed = new DataTable();
            adpeeed.Fill(dteeed);
            if (dteeed.Rows.Count > 0)
            {
                ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
                ddlbussele.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
            ddlbusiness_SelectedIndexChanged(sender, e);
          
            btnedit_Click(sender, e);
        }




    }



    protected void FillDepartMent()
    {
        DataTable dt;
        dt = new DataTable();
        clsMaster = new MasterCls1();
        dt = select("select DepartmentmasterMNC.Departmentname+':'+DesignationName as Departmentname,DesignationMasterId  from DepartmentmasterMNC inner join DesignationMaster on DesignationMaster.DeptID=DepartmentmasterMNC.Id where DepartmentmasterMNC.Whid='" + ddlbusiness.SelectedValue + "' Order by Departmentname");
        //dt = clsMaster.SelectDepartmentMaster(ddlbusiness.SelectedValue);

        ddldeptname.DataTextField = "Departmentname";
        ddldeptname.DataValueField = "DesignationMasterId";
        ddldeptname.DataSource = dt;
        ddldeptname.DataBind();

        ddldeptname.Items.Insert(0, "-Select-");
        ddldeptname.SelectedItem.Value = "0";



    }
    protected void ddldeptname_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        DataTable dtr = select("select DesignationName from DesignationMaster where DesignationMasterId='" + ddldeptname.SelectedValue + "'");
        if (dtr.Rows.Count > 0)
        {

            lbldeshead.Text = " " + dtr.Rows[0]["DesignationName"] + " ";
        }
        //FillGridSelection();
        imgbtnsubmit.Visible = false;
        if (ddldeptname.SelectedIndex > 0)
        {
           
            pnlrule2.Visible = true;
            pnl1dis.Visible = true;
            imgbtnsubmit.Visible = true;
        }
        else
        {
            pnl1dis.Visible = false;
            pnlrule2.Visible = false;
           
        }
       // rdbus_SelectedIndexChanged(sender, e);
       // ddlbussele_SelectedIndexChanged(sender, e);
        dataUP();
    }
    protected void dataUP()
    {
        lblserd.Text = "Extent of Access Rights for Entire Filing System of ";
        lblserd.Text = lblserd.Text + ddlbussele.SelectedItem.Text + "  ( All existing and Future Filing Cabinets)";
        Label24.Visible = true;
        Label3.Visible = true;
        Label21.Visible = true;
        Label22.Visible = true;
        Label23.Visible = true;
        Label25.Visible = true;
        lblserd.Visible = true;
        EventArgs e = new EventArgs();
        object sender = new object();
        DataTable dtmain = select("SELECT * from DocumentAccessRighallBus where DesignationId='" + ddldeptname.SelectedValue + "' and  CID='" + Session["Comid"] + "' and AllbusAccess='1'");
        if (dtmain.Rows.Count > 0)
        {
            rdbus.SelectedValue = "1";
            rdbus_SelectedIndexChanged(sender, e);
            chkviewabus.Checked = Convert.ToBoolean(dtmain.Rows[0]["ViewAccess"]);
            chkdeleteabus.Checked = Convert.ToBoolean(dtmain.Rows[0]["DeleteAccess"]);
            chkeditabus.Checked = Convert.ToBoolean(dtmain.Rows[0]["EditAccess"]);
            chksaveabus.Checked = Convert.ToBoolean(dtmain.Rows[0]["SaveAccess"]);
            chkemailabus.Checked = Convert.ToBoolean(dtmain.Rows[0]["EmailAccess"]);
            chkMessageabus.Checked = Convert.ToBoolean(dtmain.Rows[0]["MessageAccess"]);

            imgbtnreset.Visible = true;
            imgbtnsubmit.Visible = false;
            pnlrule2.Enabled = false;
            //  btnup.Visible = false;

            rdbus.Visible = false;
            lbldocset1.Text = "The Selected designation has full access rights for ";
            lblallcab.Text = " ALL the cabinets ";
            lblfor.Text = "of ";
            lblallbus.Text = " ALL the businesses ";
            lblsel.Visible = true;
            lblseldes.Text = lbldeshead.Text;
            lblr1edit.Text = "Level of Access rights for all the documents in all the cabinets of all businesses";
        }
        else
        {
            lbldocset1.Text = "Do you wish to give Full Access rights for ";
            lblallcab.Text = " ALL cabinets ";
            lblfor.Text = "for ";
            lblallbus.Text = " ALL businesses ";
            lblsel.Visible = true;
            lblseldes.Text = lbldeshead.Text;
            rdbus.Visible = true;
            lblr1edit.Text = "Select the level of access rights for all the documents in all cabinets of all businesses";
            rdbus.SelectedValue = "0";
            rdbus_SelectedIndexChanged(sender, e);
            editdata();
        }
    }
    protected void editdata()
    {
        EventArgs e = new EventArgs();
        object sender = new object();
        DataTable dtmain = select("SELECT * from DocumentAccessRightforbusallCabinet where DesignationId='" + ddldeptname.SelectedValue + "' and  Whid='" + ddlbussele.SelectedValue + "' and CabinetAccess='1'");
        if (dtmain.Rows.Count > 0)
        {
            rdsiglebus.SelectedValue = "1";
           
            chkviewcab.Checked = Convert.ToBoolean(dtmain.Rows[0]["ViewAccess"]);
            chkdeletecab.Checked = Convert.ToBoolean(dtmain.Rows[0]["DeleteAccess"]);
            chkeditcab.Checked = Convert.ToBoolean(dtmain.Rows[0]["EditAccess"]);
            chksavecab.Checked = Convert.ToBoolean(dtmain.Rows[0]["SaveAccess"]);
            chkmailcab.Checked = Convert.ToBoolean(dtmain.Rows[0]["EmailAccess"]);
            chkmessagecab.Checked = Convert.ToBoolean(dtmain.Rows[0]["MessageAccess"]);
          
            imgbtnreset.Visible = true;
            imgbtnsubmit.Visible = false;
           
           // btnup.Visible = false;
            rdallcab.Enabled = false;
            rdsiglebus.Visible = false;
            Label3.Text = "The Selected designation has full access rights for ";
            Label21.Text = " ALL the cabinets ";
            Label22.Text = "of ";
            Label23.Text =" "+ddlbussele.SelectedItem.Text+ " ";
           // Label24.Visible = false;
            Label25.Text = lbldeshead.Text;
            pnlallcab.Enabled = false;
            pnlallcab.Visible = true;
            pnlrightd.Visible = false;
        }
        else
        {
            Label3.Text = "1) Do you wish to give Full Access rights for ";
            Label21.Text = " ALL cabinets ";
            Label22.Text = "for ";
            Label23.Text = " " + ddlbussele.SelectedItem.Text + " ";
            Label25.Text = lbldeshead.Text;
            rdsiglebus.Visible = true;
            rdsiglebus.SelectedValue = "0";
            rdallcab.SelectedValue = "2";
            rdsiglebus_SelectedIndexChanged(sender, e);
           
          
            grddata();
          
        }
    }
    protected void grddata()
    {
        int lk = 0;
        DataTable dtd = select("select DocumentMainTypeId,+'Cabinet - '+DocumentMainType as DocumentMainType from DocumentMainType where Whid='" + ddlbussele.SelectedValue + "'");
        grdfillcab.DataSource = dtd;
        grdfillcab.DataBind();
        if (grdfillcab.Rows.Count > 0)
        {
            pnlfilec.Visible = true;
          DropDownList rdcab = new DropDownList();
            CheckBox chkview = new CheckBox();
            CheckBox chkdelete = new CheckBox();
            CheckBox chksave = new CheckBox();
            CheckBox chkedit = new CheckBox();
            CheckBox chkemail = new CheckBox();
            CheckBox chkMessage = new CheckBox();
            DataTable drm = new DataTable();
            foreach (GridViewRow item in grdfillcab.Rows)
            {
                string  cabinetId = grdfillcab.DataKeys[item.RowIndex].Value.ToString();
                 chkview = (CheckBox)(item.FindControl("chkview"));
                 chkdelete = (CheckBox)(item.FindControl("chkdelete"));
                 chksave = (CheckBox)(item.FindControl("chksave"));
                 chkedit = (CheckBox)(item.FindControl("chkedit"));
                 chkemail = (CheckBox)(item.FindControl("chkemail"));
                 chkMessage = (CheckBox)(item.FindControl("chkMessage"));
                 rdcab = (DropDownList)(item.FindControl("rdcab"));
                Label lblviw = (Label)(item.FindControl("lblviw"));

                Label lbldel = (Label)(item.FindControl("lbldel"));
                Label lblsave = (Label)(item.FindControl("lblsave"));
                Label lbledit = (Label)(item.FindControl("lbledit"));
                Label lblema = (Label)(item.FindControl("lblema"));
                Label lblmess = (Label)(item.FindControl("lblmess"));
                string cabid = grdfillcab.DataKeys[item.RowIndex].Value.ToString();
                GridView grddrower = (GridView)item.FindControl("grddrower");
                drm = select("select * from CabinetAccessRightsMaster  where CabinetId='" + cabinetId + "' and DesignationId='" + ddldeptname.SelectedValue + "'");
                if (drm.Rows.Count > 0)
                {
                    lk = 1;
                    rdcab.SelectedValue = "1";
                    chkview.Checked = Convert.ToBoolean(drm.Rows[0]["ViewAccess"]);
                    chkdelete.Checked = Convert.ToBoolean(drm.Rows[0]["DeleteAccess"]);
                    chksave.Checked = Convert.ToBoolean(drm.Rows[0]["SaveAccess"]);
                    chkedit.Checked = Convert.ToBoolean(drm.Rows[0]["EditAccess"]);
                    chkemail.Checked = Convert.ToBoolean(drm.Rows[0]["EmailAccess"]);
                    chkMessage.Checked = Convert.ToBoolean(drm.Rows[0]["MessageAccess"]);
                    lblviw.Visible = true;
                    lblsave.Visible = true;
                    lbldel.Visible = true;
                    lbledit.Visible = true;
                    lblema.Visible = true;
                    lblmess.Visible = true;
                    chkview.Visible = true;
                    chkdelete.Visible = true;
                    chksave.Visible = true;
                    chkedit.Visible = true;
                    chkemail.Visible = true;
                    chkMessage.Visible = true;
                }
                else
                {
                    lblviw.Visible = false;
                    lblsave.Visible = false;
                    lbldel.Visible = false;
                    lbledit.Visible = false;
                    lblema.Visible = false;
                    lblmess.Visible = false;
                    chkview.Visible = false;
                    chkdelete.Visible = false;
                    chksave.Visible = false;
                    chkedit.Visible = false;
                    chkemail.Visible = false;
                    chkMessage.Visible = false;
                    
                    rdcab.SelectedValue = "0";
                    DataTable dtx = new DataTable();
                    dtx = select(" select DrawerAccessRightsMaster.* from DrawerAccessRightsMaster inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DrawerAccessRightsMaster.DrawerId  where DocumentMainTypeId='" + cabinetId + "' and DesignationId='" + ddldeptname.SelectedValue + "'");
                     if (dtx.Rows.Count == 0)
                     {
                         dtx = select("select DocumentAccessRightMaster.* from DocumentAccessRightMaster inner join DocumentType on DocumentType.DocumentTypeId=DocumentAccessRightMaster.DocumentTypeId  inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId where DocumentSubType.DocumentMainTypeId='" + cabinetId + "' and DesignationId='" + ddldeptname.SelectedValue + "'");
                     }
                     if (dtx.Rows.Count > 0)
                     {
                         lk = 1;
                         DataTable dtde = select("select DocumentSubTypeId,'Drawer - '+DocumentSubType as DocumentSubType from DocumentSubType where DocumentMainTypeId='" + cabinetId + "'");
                         grddrower.DataSource = dtde;
                         grddrower.DataBind();

                         foreach (GridViewRow itdr in grddrower.Rows)
                         {
                             string drowId = grddrower.DataKeys[itdr.RowIndex].Value.ToString();
                             DropDownList rdcabA = (DropDownList)(itdr.FindControl("rddrow"));
                             chkview = (CheckBox)(itdr.FindControl("chkview"));
                             chkdelete = (CheckBox)(itdr.FindControl("chkdelete"));
                             chksave = (CheckBox)(itdr.FindControl("chksave"));
                             chkedit = (CheckBox)(itdr.FindControl("chkedit"));
                             chkemail = (CheckBox)(itdr.FindControl("chkemail"));
                             chkMessage = (CheckBox)(itdr.FindControl("chkMessage"));
                             GridView grddfolder = (GridView)itdr.FindControl("grddfolder");

                             Label lblviw1 = (Label)(itdr.FindControl("lblviw"));

                             Label lbldel1 = (Label)(itdr.FindControl("lbldel"));
                             Label lblsave1 = (Label)(itdr.FindControl("lblsave"));
                             Label lbledit1 = (Label)(itdr.FindControl("lbledit"));
                             Label lblema1 = (Label)(itdr.FindControl("lblema"));
                             Label lblmess1 = (Label)(itdr.FindControl("lblmess"));
                             drm = select("select * from DrawerAccessRightsMaster where DrawerId='" + drowId + "' and DesignationId='" + ddldeptname.SelectedValue + "'");
                             if (drm.Rows.Count > 0)
                             {
                                 rdcabA.SelectedValue = "1";
                                 rdcab.SelectedValue = "2";
                                 chkview.Checked = Convert.ToBoolean(drm.Rows[0]["ViewAccess"]);
                                 chkdelete.Checked = Convert.ToBoolean(drm.Rows[0]["DeleteAccess"]);
                                 chksave.Checked = Convert.ToBoolean(drm.Rows[0]["SaveAccess"]);
                                 chkedit.Checked = Convert.ToBoolean(drm.Rows[0]["EditAccess"]);
                                 chkemail.Checked = Convert.ToBoolean(drm.Rows[0]["EmailAccess"]);
                                 chkMessage.Checked = Convert.ToBoolean(drm.Rows[0]["MessageAccess"]);
                                 chkview.Visible = true;
                                 chkdelete.Visible = true;
                                 chksave.Visible = true;
                                 chkedit.Visible = true;
                                 chkemail.Visible = true;
                                 chkMessage.Visible = true;
                                 lblviw1.Visible = true;
                                 lbldel1.Visible = true;
                                 lblsave1.Visible = true;
                                 lbledit1.Visible = true;
                                 lblema1.Visible = true;
                                 lblmess1.Visible = true;
                             }
                             else
                             {
                                 rdcabA.SelectedValue = "0";
                                 chkview.Visible = false;
                                 chkdelete.Visible = false;
                                 chksave.Visible = false;
                                 chkedit.Visible = false;
                                 chkemail.Visible = false;
                                 chkMessage.Visible = false;
                                 lblviw1.Visible = false;
                                 lbldel1.Visible = false;
                                 lblsave1.Visible = false;
                                 lbledit1.Visible = false;
                                 lblema1.Visible = false;
                                 lblmess1.Visible = false;
                                 DataTable dtxw = new DataTable();

                                 dtxw = select("select DocumentAccessRightMaster.* from DocumentAccessRightMaster inner join DocumentType on DocumentType.DocumentTypeId=DocumentAccessRightMaster.DocumentTypeId  inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId where DocumentSubType.DocumentSubTypeId='" + drowId + "' and DesignationId='" + ddldeptname.SelectedValue + "'");

                                 if (dtxw.Rows.Count > 0)
                                 {
                                     DataTable dtde1 = select("select DocumentTypeId,'Folder - '+DocumentType as DocumentType from DocumentType where DocumentSubTypeId='" + drowId + "'");
                                     grddfolder.DataSource = dtde1;
                                     grddfolder.DataBind();
                                     foreach (GridViewRow itfolder in grddfolder.Rows)
                                     {
                                         string FolderId = grddfolder.DataKeys[itfolder.RowIndex].Value.ToString();
                                         DropDownList rdcabB = (DropDownList)(itfolder.FindControl("rdfolder"));
                                         chkview = (CheckBox)(itfolder.FindControl("chkview"));
                                         chkdelete = (CheckBox)(itfolder.FindControl("chkdelete"));
                                         chksave = (CheckBox)(itfolder.FindControl("chksave"));
                                         chkedit = (CheckBox)(itfolder.FindControl("chkedit"));
                                         chkemail = (CheckBox)(itfolder.FindControl("chkemail"));
                                         chkMessage = (CheckBox)(itfolder.FindControl("chkMessage"));


                                         Label lblviw12 = (Label)(itfolder.FindControl("lblviw"));

                                         Label lbldel12 = (Label)(itfolder.FindControl("lbldel"));
                                         Label lblsave12 = (Label)(itfolder.FindControl("lblsave"));
                                         Label lbledit12 = (Label)(itfolder.FindControl("lbledit"));
                                         Label lblema12 = (Label)(itfolder.FindControl("lblema"));
                                         Label lblmess12 = (Label)(itfolder.FindControl("lblmess"));
                                         drm = select("select * from DocumentAccessRightMaster where DocumentTypeId='" + FolderId + "' and DesignationId='" + ddldeptname.SelectedValue + "'");
                                         if (drm.Rows.Count > 0)
                                         {
                                             rdcabA.SelectedValue = "2";
                                             rdcabB.SelectedValue = "1";
                                             chkview.Checked = Convert.ToBoolean(drm.Rows[0]["ViewAccess"]);
                                             chkdelete.Checked = Convert.ToBoolean(drm.Rows[0]["DeleteAccess"]);
                                             chksave.Checked = Convert.ToBoolean(drm.Rows[0]["SaveAccess"]);
                                             chkedit.Checked = Convert.ToBoolean(drm.Rows[0]["EditAccess"]);
                                             chkemail.Checked = Convert.ToBoolean(drm.Rows[0]["EmailAccess"]);
                                             chkMessage.Checked = Convert.ToBoolean(drm.Rows[0]["MessageAccess"]);
                                             chkview.Visible = true;
                                             chkdelete.Visible = true;
                                             chksave.Visible = true;
                                             chkedit.Visible = true;
                                             chkemail.Visible = true;
                                             chkMessage.Visible = true;
                                             lblviw12.Visible = true;
                                             lbldel12.Visible = true;
                                             lblsave12.Visible = true;
                                             lbledit12.Visible = true;
                                             lblema12.Visible = true;
                                             lblmess12.Visible = true;

                                         }
                                         else
                                         {
                                             rdcabB.SelectedValue = "0";

                                             chkview.Visible = false;
                                             chkdelete.Visible = false;
                                             chksave.Visible = false;
                                             chkedit.Visible = false;
                                             chkemail.Visible = false;
                                             chkMessage.Visible = false;
                                             lblviw12.Visible = false;
                                             lbldel12.Visible = false;
                                             lblsave12.Visible = false;
                                             lbledit12.Visible = false;
                                             lblema12.Visible = false;
                                             lblmess12.Visible = false;

                                             //grddfolder.DataSource = null;
                                             //grddfolder.DataBind();
                                         }
                                     }
                                 }
                             }


                         }
                     }
                    
                     
                }
            }
            if (lk == 1)
            {
                pnlrightd.Enabled = false;
                rdsiglebus.Visible = false;

                Label3.Text = "The Selected designation has full access rights for ";
                Label21.Text = " ALL the cabinets ";
                Label22.Text = "of ";
                Label23.Text = " " + ddlbussele.SelectedItem.Text + " ";
                Label25.Text = lbldeshead.Text;
                Label3.Visible=false;
                Label21.Visible=false;
                Label22.Visible=false;
                Label23.Visible = false;
                     Label25.Visible = false;
                     Label24.Visible = false;
                imgbtnreset.Visible = true;
                imgbtnsubmit.Visible = false;
                lblserd.Visible = false;
               
            }
            else
            {
                Label3.Text = "1) Do you wish to give Full Access rights for ";
                Label21.Text = " ALL cabinets ";
                Label22.Text = "for ";
                Label23.Text = " " + ddlbussele.SelectedItem.Text + " ";
                Label25.Text = lbldeshead.Text;
                Label3.Visible = true;
                Label21.Visible = true;
                Label22.Visible = true;
                Label23.Visible = true;
                Label25.Visible = true;
                Label24.Visible = true;
                lblserd.Visible = true;
                rdsiglebus.Visible = true;
                imgbtnreset.Visible = false;
                imgbtnsubmit.Visible = true;
            }
        }
    }
    protected void imgbtnreset_Click(object sender, EventArgs e)
    {
        imgbtnsubmit.Visible = true;
        if (rdbus.SelectedValue == "1")
        {
            pnlrule2.Visible = true;
            lbldocset1.Text = "Do you wish to give Full Access rights for ";
            lblallcab.Text = " ALL cabinets ";
            lblfor.Text = "for ";
            lblallbus.Text = " ALL businesses ";
            lblsel.Visible = true;
            lblseldes.Text = lbldeshead.Text;
            rdbus.Visible = true;
            pnlallbus.Enabled = true;
            pnlrule2.Enabled = true;
            lblr1edit.Text = "Select the level of access rights for all the documents in all cabinets of all businesses";
        }
        else
        {
            pnl1dis.Visible = true;

            if (rdsiglebus.SelectedValue == "1")
            {
                Label3.Text = "1) Do you wish to give Full Access rights for ";
                Label21.Text = " ALL cabinets ";
                Label22.Text = "for ";
                Label23.Text = " " + ddlbussele.SelectedItem.Text + " ";
                Label25.Text = lbldeshead.Text;
                rdsiglebus.Visible = true;
                pnlrightd.Enabled = true;
                pnlallcab.Enabled = true;
                rdsiglebus.Enabled = true;
                rdallcab.Enabled = true;
            }
            else
            {
                Label3.Text = "1) Do you wish to give Full Access rights for ";
                Label21.Text = " ALL cabinets ";
                Label22.Text = "for ";
                Label23.Text = " " + ddlbussele.SelectedItem.Text + " ";
                Label25.Text = lbldeshead.Text;
                Label3.Visible = true;
                Label21.Visible = true;
                Label22.Visible = true;
                Label23.Visible = true;
                Label25.Visible = true;
                Label24.Visible = true;
                lblserd.Visible = true;
                rdsiglebus.Visible = true;
                pnlrightd.Enabled = true;
            }
        
        }

        imgbtnreset.Visible = false;
    }
    protected void Clear()
    {


       
    }

    protected DataTable select(String str)
    {
        DataTable dt = new DataTable();
        SqlCommand cmdeeed = new SqlCommand(str, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        return dteeed;
    }
    protected void strdeletegriadall()
    {
        string stdde = "Delete from DocumentAccessRightMaster where DesignationId='" + ddldeptname.SelectedValue + "'  " +
                           "  and  DocumentTypeId In(Select Distinct DocumentTypeId  from WarehouseMaster inner join DocumentMainType on DocumentMainType.Whid=WarehouseMaster.WarehouseId Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId " +
                            "   where WareHouseMaster.comid='" + Session["Comid"] + "' and DocumentMainType.Whid='"+ddlbussele.SelectedValue+"')";
        SqlCommand cmdde = new SqlCommand(stdde, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int retde = Convert.ToInt32(cmdde.ExecuteNonQuery());
        con.Close();

        string stddedr = "Delete from DrawerAccessRightsMaster where DesignationId='" + ddldeptname.SelectedValue + "'  " +
                          "  and  DrawerId In(Select Distinct DocumentSubTypeId from WarehouseMaster inner join DocumentMainType on DocumentMainType.Whid=WarehouseMaster.WarehouseId Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId  " +
                           "   where WareHouseMaster.comid='" + Session["Comid"] + "' and DocumentMainType.Whid='" + ddlbussele.SelectedValue + "')";
        SqlCommand cmddedr = new SqlCommand(stddedr, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int retdedr = Convert.ToInt32(cmddedr.ExecuteNonQuery());
        con.Close();

        string stddecab = "Delete from CabinetAccessRightsMaster where DesignationId='" + ddldeptname.SelectedValue + "'  " +
                         "  and  CabinetId In(Select DocumentMainTypeId Distinct  from WarehouseMaster inner join DocumentMainType on DocumentMainType.Whid=WarehouseMaster.WarehouseId " +
                          "   where WareHouseMaster.comid='" + Session["Comid"] + "' and DocumentMainType.Whid='" + ddlbussele.SelectedValue + "')";
        SqlCommand cmddecab= new SqlCommand(stddecab, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int retdecab = Convert.ToInt32(cmddecab.ExecuteNonQuery());
        con.Close();
    }
    protected void imgbtnsubmit_Click(object sender, EventArgs e)
    {

        int intApp = 0;

        if (rdbus.SelectedValue == "1" || rdsiglebus.SelectedValue=="1")
        {
            Insertdata();
        }
        else
        {

            DataTable dtmain = new DataTable();
            //CheckBox chkfax = new CheckBox();
            DropDownList rdcab = new DropDownList();
            CheckBox chkview = new CheckBox();
            CheckBox chkdelete = new CheckBox();
            CheckBox chksave = new CheckBox();
            CheckBox chkedit = new CheckBox();
            CheckBox chkemail = new CheckBox();
            CheckBox chkMessage = new CheckBox();
            string dtyid = "";

            string strallbusdet = "Update DocumentAccessRighallBus set AllbusAccess='0' where DesignationId='" + ddldeptname.SelectedValue + "' and CID='" + Session["Comid"] + "'";
            SqlCommand cmdallbusdel = new SqlCommand(strallbusdet, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }

            int retdebA = Convert.ToInt32(cmdallbusdel.ExecuteNonQuery());
            con.Close();
            string strallbusdetbus = "Update DocumentAccessRightforbusallCabinet set CabinetAccess='0' where DesignationId='" + ddldeptname.SelectedValue + "' and Whid='" + ddlbussele.SelectedValue + "'";
            SqlCommand cmdallbusdelbus = new SqlCommand(strallbusdetbus, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            int retdeb = Convert.ToInt32(cmdallbusdelbus.ExecuteNonQuery());
            con.Close();
            if (rdallcab.SelectedValue == "0")
            {
                strdeletegriadall();
               
            
                ddldeptname_SelectedIndexChanged(sender, e);
                lblmsg.Text = "Record saved successfully";
            }
            else
            {
               
                foreach (GridViewRow item in grdfillcab.Rows)
                {
                    string cabinetId = "";
                    cabinetId = grdfillcab.DataKeys[item.RowIndex].Value.ToString();
                    //chkfax = (CheckBox)(item.FindControl("chkfax"));
                    rdcab = (DropDownList)(item.FindControl("rdcab"));
                    chkview = (CheckBox)(item.FindControl("chkview"));
                    chkdelete = (CheckBox)(item.FindControl("chkdelete"));
                    chksave = (CheckBox)(item.FindControl("chksave"));
                    chkedit = (CheckBox)(item.FindControl("chkedit"));
                    chkemail = (CheckBox)(item.FindControl("chkemail"));
                    chkMessage = (CheckBox)(item.FindControl("chkMessage"));

                    string cabid = grdfillcab.DataKeys[item.RowIndex].Value.ToString();
                    GridView grddrower = (GridView)item.FindControl("grddrower");
                    int rst11 = InsertCabinetAccessRightMaster(Convert.ToInt32(cabinetId), Convert.ToInt32(ddldeptname.SelectedValue), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked, rdcab.SelectedValue);

                    if (rdcab.SelectedValue == "1")
                    {
                        
                        dtmain = select("SELECT Distinct DocumentType.DocumentTypeId from  DocumentMainType Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where  DocumentMainType.DocumentMainTypeId In(" + cabid + ")");
                        for (int i = 0; i < dtmain.Rows.Count; i++)
                        {
                            if (dtyid == "")
                            {
                                dtyid = dtyid + "'" + dtmain.Rows[i]["DocumentTypeId"] + "'";
                            }
                            else
                            {
                                dtyid = dtyid + ",'" + dtmain.Rows[i]["DocumentTypeId"] + "'";
                            }
                            int rst1 = clsDocument.InsertDocumentAccessRightMaster(Convert.ToInt32(dtmain.Rows[i]["DocumentTypeId"]), Convert.ToInt32(ddldeptname.SelectedValue), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked);
                        }
                    }
                    else if (rdcab.SelectedValue == "2")
                    {
                        foreach (GridViewRow itm1 in grddrower.Rows)
                        {
                            string drawerId = "";
                            drawerId = grddrower.DataKeys[itm1.RowIndex].Value.ToString();

                            rdcab = (DropDownList)(itm1.FindControl("rddrow"));
                            chkview = (CheckBox)(itm1.FindControl("chkview"));
                            chkdelete = (CheckBox)(itm1.FindControl("chkdelete"));
                            chksave = (CheckBox)(itm1.FindControl("chksave"));
                            chkedit = (CheckBox)(itm1.FindControl("chkedit"));
                            chkemail = (CheckBox)(itm1.FindControl("chkemail"));
                            chkMessage = (CheckBox)(itm1.FindControl("chkMessage"));
                            string droId = grddrower.DataKeys[itm1.RowIndex].Value.ToString();
                            GridView grddfolder = (GridView)itm1.FindControl("grddfolder");

                            int rst1v = InsertDrawerAccessRightMaster(Convert.ToInt32(drawerId), Convert.ToInt32(ddldeptname.SelectedValue), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked, rdcab.SelectedValue);

                             if (rdcab.SelectedValue == "1")
                             {
                                
          
                                dtmain = select("SELECT Distinct DocumentType.DocumentTypeId from  DocumentMainType Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentSubType.DocumentSubTypeId In(" + droId + ")");
                                for (int i = 0; i < dtmain.Rows.Count; i++)
                                {
                                    if (dtyid == "")
                                    {
                                        dtyid = dtyid + "'" + dtmain.Rows[i]["DocumentTypeId"] + "'";

                                    }
                                    else
                                    {
                                        dtyid = dtyid + ",'" + dtmain.Rows[i]["DocumentTypeId"] + "'";


                                    }
                                    int rst1 = clsDocument.InsertDocumentAccessRightMaster(Convert.ToInt32(dtmain.Rows[i]["DocumentTypeId"]), Convert.ToInt32(ddldeptname.SelectedValue), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked);

                                }
                            }
                            else if (rdcab.SelectedValue == "2")
                            {
                                foreach (GridViewRow imf in grddfolder.Rows)
                                {
                                    rdcab = (DropDownList)(imf.FindControl("rdfolder"));
                                    chkview = (CheckBox)(imf.FindControl("chkview"));
                                    chkdelete = (CheckBox)(imf.FindControl("chkdelete"));
                                    chksave = (CheckBox)(imf.FindControl("chksave"));
                                    chkedit = (CheckBox)(imf.FindControl("chkedit"));
                                    chkemail = (CheckBox)(imf.FindControl("chkemail"));
                                    chkMessage = (CheckBox)(imf.FindControl("chkMessage"));
                                    string folderid = grddfolder.DataKeys[imf.RowIndex].Value.ToString();
                                    if (rdcab.SelectedValue == "1")
                                    {
                                        if (dtyid == "")
                                        {
                                            dtyid = dtyid + "'" + folderid + "'";

                                        }
                                        else
                                        {
                                            dtyid = dtyid + ",'" + folderid + "'";


                                        }
                                        int rst1c = clsDocument.InsertDocumentAccessRightMaster(Convert.ToInt32(folderid), Convert.ToInt32(ddldeptname.SelectedValue), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked);

                                    }
                                }
                            }
                            else
                            {
                            }
                        }
                    }
                    else
                    {
                    }



                }
                if (dtyid != "")
                {
                    string stdde = "Delete from DocumentAccessRightMaster where DesignationId='" + ddldeptname.SelectedValue + "' and DocumentTypeId Not in (" + dtyid + ") " +
                      "  and  DocumentTypeId In(Select DocumentTypeId  from DocumentMainType Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId " +
                       "   where DocumentMainType.Whid='" + ddlbussele.SelectedValue + "')";
                    SqlCommand cmd = new SqlCommand(stdde, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                //btnedit.Text = "View";
              //  btnedit_Click(sender, e);
                ddldeptname_SelectedIndexChanged(sender, e);
                lblmsg.Text = "Record saved successfully";
            }
          
        }
    }
    public Int32 InsertDrawerAccessRightMaster(Int32 DrawerId, Int32 DesignationId, bool PrintAccess, bool ViewAccess,
     bool DeleteAccess, bool SaveAccess, bool EditAccess, bool EmailAccess, bool FaxAccess, bool MessageAccess, string draty)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "InsertDrawerAccessRightMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@DrawerId", SqlDbType.Int));
        cmd.Parameters["@DrawerId"].Value = DrawerId;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@PrintAccess", SqlDbType.Bit));
        cmd.Parameters["@PrintAccess"].Value = PrintAccess;
        cmd.Parameters.Add(new SqlParameter("@ViewAccess", SqlDbType.Bit));
        cmd.Parameters["@ViewAccess"].Value = ViewAccess;
        cmd.Parameters.Add(new SqlParameter("@DeleteAccess", SqlDbType.Bit));
        cmd.Parameters["@DeleteAccess"].Value = DeleteAccess;
        cmd.Parameters.Add(new SqlParameter("@SaveAccess", SqlDbType.Bit));
        cmd.Parameters["@SaveAccess"].Value = SaveAccess;
        cmd.Parameters.Add(new SqlParameter("@EditAccess", SqlDbType.Bit));
        cmd.Parameters["@EditAccess"].Value = EditAccess;
        cmd.Parameters.Add(new SqlParameter("@EmailAccess", SqlDbType.Bit));
        cmd.Parameters["@EmailAccess"].Value = EmailAccess;
        cmd.Parameters.Add(new SqlParameter("@FaxAccess", SqlDbType.Bit));
        cmd.Parameters["@FaxAccess"].Value = FaxAccess;
        cmd.Parameters.Add(new SqlParameter("@MessageAccess", SqlDbType.Bit));
        cmd.Parameters["@MessageAccess"].Value = MessageAccess;

        cmd.Parameters.Add(new SqlParameter("@draty", SqlDbType.NVarChar));
        cmd.Parameters["@draty"].Value = draty;
       
      
        //cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        //result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    
    public Int32 InsertCabinetAccessRightMaster(Int32 CabinetId, Int32 DesignationId, bool PrintAccess, bool ViewAccess,
       bool DeleteAccess, bool SaveAccess, bool EditAccess, bool EmailAccess, bool FaxAccess, bool MessageAccess, string Cabty)
    {

       SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "InsertCabinetAccessRightMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@CabinetId", SqlDbType.Int));
        cmd.Parameters["@CabinetId"].Value = CabinetId;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@PrintAccess", SqlDbType.Bit));
        cmd.Parameters["@PrintAccess"].Value = PrintAccess;
        cmd.Parameters.Add(new SqlParameter("@ViewAccess", SqlDbType.Bit));
        cmd.Parameters["@ViewAccess"].Value = ViewAccess;
        cmd.Parameters.Add(new SqlParameter("@DeleteAccess", SqlDbType.Bit));
        cmd.Parameters["@DeleteAccess"].Value = DeleteAccess;
        cmd.Parameters.Add(new SqlParameter("@SaveAccess", SqlDbType.Bit));
        cmd.Parameters["@SaveAccess"].Value = SaveAccess;
        cmd.Parameters.Add(new SqlParameter("@EditAccess", SqlDbType.Bit));
        cmd.Parameters["@EditAccess"].Value = EditAccess;
        cmd.Parameters.Add(new SqlParameter("@EmailAccess", SqlDbType.Bit));
        cmd.Parameters["@EmailAccess"].Value = EmailAccess;
        cmd.Parameters.Add(new SqlParameter("@FaxAccess", SqlDbType.Bit));
        cmd.Parameters["@FaxAccess"].Value = FaxAccess;
        cmd.Parameters.Add(new SqlParameter("@MessageAccess", SqlDbType.Bit));
        cmd.Parameters["@MessageAccess"].Value = MessageAccess;

        cmd.Parameters.Add(new SqlParameter("@Cabty", SqlDbType.NVarChar));
        cmd.Parameters["@Cabty"].Value = Cabty;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        //cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        //cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
       // result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    
    protected void Insertdata()
    {  string strdelpera="";
        string stddeadd = "";
        if (rdbus.SelectedValue == "1")
        {          
            DataTable dts = select("Select * from DocumentAccessRighallBus where DesignationId='"+ddldeptname.SelectedValue+"' and CID='"+Session["Comid"]+"'");
            if (dts.Rows.Count == 0)
            {
                 stddeadd = " Insert into DocumentAccessRighallBus  ( DesignationId,ViewAccess,DeleteAccess,SaveAccess,EditAccess,EmailAccess,FaxAccess,PrintAccess,MessageAccess,CID,AllbusAccess)Values " +
                    " ( '" + ddldeptname.SelectedValue + "','" + chkviewabus.Checked + "','" + chkdeleteabus.Checked + "','" + chksaveabus.Checked + "','" + chkeditabus.Checked + "','" + chkemailabus.Checked + "','1','1','" + chkMessageabus.Checked + "','" + Session["Comid"] + "','1') ";
            }
            else
            {
                stddeadd = "Update DocumentAccessRighallBus set ViewAccess='" + chkviewabus.Checked + "',DeleteAccess='" + chkdeleteabus.Checked + "',SaveAccess='" + chksaveabus.Checked + "',EditAccess='" + chkeditabus.Checked + "',EmailAccess='" + chkemailabus.Checked + "',MessageAccess='" + chkMessageabus.Checked + "',AllbusAccess='1' where Id='" + dts .Rows[0]["Id"]+ "'";
                 
            }
            if (stddeadd.Length > 0)
            {
                SqlCommand cmd = new SqlCommand(stddeadd, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        else if (rdsiglebus.SelectedValue == "1")
        {
            strdelpera = "and DocumentMainType.Whid='" + ddlbussele.SelectedValue + "'";
            DataTable dts = select("Select * from DocumentAccessRightforbusallCabinet where DesignationId='" + ddldeptname.SelectedValue + "' and Whid='" + ddlbussele.SelectedValue + "'");
            if (dts.Rows.Count == 0)
            {
                stddeadd = "Insert into DocumentAccessRightforbusallCabinet(DesignationId,ViewAccess,DeleteAccess,SaveAccess,EditAccess,EmailAccess,FaxAccess,PrintAccess,MessageAccess,Whid,CabinetAccess)Values " +
                  "('" + ddldeptname.SelectedValue + "','" + chkviewcab.Checked + "','" + chkdeletecab.Checked + "','" + chksavecab.Checked + "','" + chkeditcab.Checked + "','" + chkmailcab.Checked + "','1','1','" + chkmessagecab.Checked + "','" + ddlbussele.SelectedValue + "','1') ";
            }
            else
            {
                stddeadd = "Update DocumentAccessRightforbusallCabinet set ViewAccess='" + chkviewcab.Checked + "',DeleteAccess='" + chkdeletecab.Checked + "',SaveAccess='" + chksavecab.Checked + "',EditAccess='" + chkeditcab.Checked + "',EmailAccess='" + chkmailcab.Checked + "',MessageAccess='" + chkmessagecab.Checked + "',CabinetAccess='1' where Id='" + dts.Rows[0]["Id"] + "'";


            }
            if (stddeadd.Length > 0)
            {
                SqlCommand cmd = new SqlCommand(stddeadd, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                string strallbusdet = "Update DocumentAccessRighallBus set AllbusAccess='0' where DesignationId='" + ddldeptname.SelectedValue + "' and CID='" + Session["Comid"] + "'";
                SqlCommand cmdallbusdel = new SqlCommand(strallbusdet, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                int retdeb = Convert.ToInt32(cmdallbusdel.ExecuteNonQuery());
                con.Close();
            }
        }

            int rst1 = 0;

          
            
                DataTable dtmain = select("SELECT Distinct DocumentType.DocumentTypeId from WarehouseMaster inner join DocumentMainType on DocumentMainType.Whid=WarehouseMaster.WarehouseId Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where WareHouseMaster.comid='" + Session["Comid"] + "'" + strdelpera);
                string dtyid = "";
                int i = 0;
                foreach (DataRow item in dtmain.Rows)
                {
                    if (dtyid == "")
                    {
                        dtyid = dtyid + "'" + dtmain.Rows[i]["DocumentTypeId"] + "'";

                    }
                    else
                    {
                        dtyid = dtyid + ",'" + dtmain.Rows[i]["DocumentTypeId"] + "'";


                    }
   
                    rst1 = clsDocument.InsertDocumentAccessRightMaster(Convert.ToInt32(item["DocumentTypeId"]), Convert.ToInt32(ddldeptname.SelectedValue), Convert.ToBoolean(1), chkviewabus.Checked, chkdeleteabus.Checked, chksaveabus.Checked, chkeditabus.Checked, chkemailabus.Checked, Convert.ToBoolean(0), chkMessageabus.Checked);
                    i += 1;
                }
               string pess="";
                if (dtyid.Length > 0)
                {
                    pess = "  and DocumentTypeId Not in (" + dtyid + ")";
                }
        
                string stdde = "Delete from DocumentAccessRightMaster where DesignationId='" + ddldeptname.SelectedValue + "'  " +pess+
                         "  and  DocumentTypeId In(Select distinct DocumentTypeId  from WarehouseMaster inner join DocumentMainType on DocumentMainType.Whid=WarehouseMaster.WarehouseId Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId " +
                          "   where WareHouseMaster.comid='" + Session["Comid"] + "'" + strdelpera + ")";
                SqlCommand cmdde = new SqlCommand(stdde, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                int retde = Convert.ToInt32(cmdde.ExecuteNonQuery());
                con.Close();
            if (rst1 >0)
            {
               
                EventArgs e=new EventArgs();
                object sender=new object();
                ddldeptname_SelectedIndexChanged(sender, e);
                lblmsg.Text = "Record saved successfully";
            }
            else
            {
                lblmsg.Text = "Record not saved successfully";
            }
        
    }
    protected void chkAll2_chachedChanged(object sender, EventArgs e)
    {

       

            GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

            int rinrow = row.RowIndex;
            CheckBox chkprocess = (CheckBox)sender;
            GridView grddrower = (GridView)chkprocess.NamingContainer.NamingContainer;
            //string i = grdfillcab.DataKeys[it.RowIndex].Value.ToString();

            string i = "0";
            //GridView grddrower = (GridView)grdfillcab.Rows[Convert.ToInt32(i)].FindControl("grddrower");
            GridView grddfolder = (GridView)grddrower.Rows[rinrow].FindControl("grddfolder");


            string cabid = grddrower.DataKeys[rinrow].Value.ToString();

            CheckBox chkfax = (CheckBox)(grddrower.Rows[rinrow].FindControl("chkfax"));
            CheckBox chkview = (CheckBox)(grddrower.Rows[rinrow].FindControl("chkview"));
            CheckBox chkdelete = (CheckBox)(grddrower.Rows[rinrow].FindControl("chkdelete"));
            CheckBox chksave = (CheckBox)(grddrower.Rows[rinrow].FindControl("chksave"));
            CheckBox chkedit = (CheckBox)(grddrower.Rows[rinrow].FindControl("chkedit"));
            CheckBox chkemail = (CheckBox)(grddrower.Rows[rinrow].FindControl("chkemail"));
            CheckBox chkMessage = (CheckBox)(grddrower.Rows[rinrow].FindControl("chkMessage"));


            Label lblviw1 = (Label)(grddrower.Rows[rinrow].FindControl("lblviw"));

            Label lbldel1 = (Label)(grddrower.Rows[rinrow].FindControl("lbldel"));
            Label lblsave1 = (Label)(grddrower.Rows[rinrow].FindControl("lblsave"));
            Label lbledit1 = (Label)(grddrower.Rows[rinrow].FindControl("lbledit"));
            Label lblema1 = (Label)(grddrower.Rows[rinrow].FindControl("lblema"));
            Label lblmess1 = (Label)(grddrower.Rows[rinrow].FindControl("lblmess"));
            if (chkfax.Checked == false)
            {
                chkview.Visible = false;
                chkdelete.Visible = false;
                chksave.Visible = false;
                chkedit.Visible = false;
                chkemail.Visible = false;
                chkMessage.Visible = false;
                lblviw1.Visible = false;
                lbldel1.Visible = false;
                lblsave1.Visible = false;
                lbledit1.Visible = false;
                lblema1.Visible = false;
                lblmess1.Visible = false;
                DataTable dtde1 = select("select * from DocumentType where DocumentSubTypeId='" + cabid + "'");
                grddfolder.DataSource = dtde1;
                grddfolder.DataBind();
            }

            else
            {

                chkview.Visible = true;
                chkdelete.Visible = true;
                chksave.Visible = true;
                chkedit.Visible = true;
                chkemail.Visible = true;
                chkMessage.Visible = true;
                lblviw1.Visible = true;
                lbldel1.Visible = true;
                lblsave1.Visible = true;
                lbledit1.Visible = true;
                lblema1.Visible = true;
                lblmess1.Visible = true;
                chkview.Checked = true;
                chkdelete.Checked = true;
                chksave.Checked = true;
                chkedit.Checked = true;
                chkemail.Checked = true;
                chkMessage.Checked = true;
                grddfolder.DataSource = null;
                grddfolder.DataBind();
            }
        
    }
    protected void chkAll1_chachedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;



        string cabid = grdfillcab.DataKeys[rinrow].Value.ToString();
        GridView grddrower = (GridView)grdfillcab.Rows[rinrow].FindControl("grddrower");

        CheckBox chkfax = (CheckBox)(grdfillcab.Rows[rinrow].FindControl("chkfax"));
        CheckBox chkview = (CheckBox)(grdfillcab.Rows[rinrow].FindControl("chkview"));
        CheckBox chkdelete = (CheckBox)(grdfillcab.Rows[rinrow].FindControl("chkdelete"));
        CheckBox chksave = (CheckBox)(grdfillcab.Rows[rinrow].FindControl("chksave"));
        CheckBox chkedit = (CheckBox)(grdfillcab.Rows[rinrow].FindControl("chkedit"));
        CheckBox chkemail = (CheckBox)(grdfillcab.Rows[rinrow].FindControl("chkemail"));
        CheckBox chkMessage = (CheckBox)(grdfillcab.Rows[rinrow].FindControl("chkMessage"));


        Label lblviw1 = (Label)(grdfillcab.Rows[rinrow].FindControl("lblviw"));

        Label lbldel1 = (Label)(grdfillcab.Rows[rinrow].FindControl("lbldel"));
        Label lblsave1 = (Label)(grdfillcab.Rows[rinrow].FindControl("lblsave"));
        Label lbledit1 = (Label)(grdfillcab.Rows[rinrow].FindControl("lbledit"));
        Label lblema1 = (Label)(grdfillcab.Rows[rinrow].FindControl("lblema"));
        Label lblmess1 = (Label)(grdfillcab.Rows[rinrow].FindControl("lblmess"));
        if (chkfax.Checked == false)
        {
            chkview.Visible = false;
            chkdelete.Visible = false;
            chksave.Visible = false;
            chkedit.Visible = false;
            chkemail.Visible = false;
            chkMessage.Visible = false;
            lblviw1.Visible = false;
            lbldel1.Visible = false;
            lblsave1.Visible = false;
            lbledit1.Visible = false;
            lblema1.Visible = false;
            lblmess1.Visible = false;
            DataTable dtde = select("select * from DocumentSubType where DocumentMainTypeId='" + cabid + "'");
            grddrower.DataSource = dtde;
            grddrower.DataBind();
        }

        else
        {

            chkview.Visible = true;
            chkdelete.Visible = true;
            chksave.Visible = true;
            chkedit.Visible = true;
            chkemail.Visible = true;
            chkMessage.Visible = true;
            lblviw1.Visible = true;
            lbldel1.Visible = true;
            lblsave1.Visible = true;
            lbledit1.Visible = true;
            lblema1.Visible = true;
            lblmess1.Visible = true;
            chkview.Checked = true;
            chkdelete.Checked = true;
            chksave.Checked = true;
            chkedit.Checked = true;
            chkemail.Checked = true;
            chkMessage.Checked = true;
            grddrower.DataSource = null;
            grddrower.DataBind();
        }

    }
   
    protected void chkAll3_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdfillcab.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chksave"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void chkAll4_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdfillcab.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chkedit"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void chkAll5_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdfillcab.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chkemail"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void chkAll6_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdfillcab.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chkfax"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void chkAll7_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdfillcab.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chkMessage"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void chkAll8_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdfillcab.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chkview"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void ImageButton47_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewState["p1"].ToString());
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblBusiness.Text = ddlbusiness.SelectedItem.Text;

        FillDepartMent();

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
   
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button3.Text = "Hide Printable Version";
            Button1.Visible = true;
        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(200);

            Button3.Text = "Printable Version";
            Button1.Visible = false;
        }
    }
    protected void rdbus_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkdeleteabus.Checked = false;
        chksaveabus.Checked = false;
        chkviewabus.Checked = true;
        chkeditabus.Checked = false;
        chkemailabus.Checked = false;
        chkMessageabus.Checked = false;
        rdsiglebus_SelectedIndexChanged(sender, e);


    }




  
    protected void ddlbussele_SelectedIndexChanged(object sender, EventArgs e)
    {

       
          filldata();
          //editdata();
          dataUP();
        lblserd.Text = "Extent of Access Rights for Entire Filing System of ";
        lblserd.Text = lblserd.Text + ddlbussele.SelectedItem.Text+"  ( All existing and Future Filing Cabinets)";
        lblDepartment.Text = ddldeptname.SelectedItem.Text;
        lblappbus.Text = ddlbussele.SelectedItem.Text;
    }
    protected void filldata()
    {
        imgbtnsubmit.Visible = false;
        imgbtnreset.Visible = false;
        if (rdsiglebus.SelectedValue == "1")
        {
            imgbtnsubmit.Visible = true;
            imgbtnreset.Visible = true;
            EventArgs e=new EventArgs();
            object sender=new object();
            rdallcab_SelectedIndexChanged(sender, e);
        }
        else
        {
            DataTable dtd = select("select DocumentMainTypeId,+'Cabinet - '+DocumentMainType as DocumentMainType from DocumentMainType where Whid='" + ddlbussele.SelectedValue + "'");
            grdfillcab.DataSource = dtd;
            grdfillcab.DataBind();
            if (grdfillcab.Rows.Count > 0)
            {
                pnlfilec.Visible = true;
                if (pnlfilec.Visible == true)
                {
                    imgbtnsubmit.Visible = true;
                    //  imgbtnreset.Visible = true;
                }
                //grdfillcab.Columns[8].Visible = false;
                foreach (GridViewRow item in grdfillcab.Rows)
                {

                    //CheckBox chkfax = (CheckBox)(item.FindControl("chkfax"));


                    CheckBox chkview = (CheckBox)(item.FindControl("chkview"));
                    CheckBox chkdelete = (CheckBox)(item.FindControl("chkdelete"));
                    CheckBox chksave = (CheckBox)(item.FindControl("chksave"));
                    CheckBox chkedit = (CheckBox)(item.FindControl("chkedit"));
                    CheckBox chkemail = (CheckBox)(item.FindControl("chkemail"));
                    CheckBox chkMessage = (CheckBox)(item.FindControl("chkMessage"));

                    Label lblviw = (Label)(item.FindControl("lblviw"));

                    Label lbldel = (Label)(item.FindControl("lbldel"));
                    Label lblsave = (Label)(item.FindControl("lblsave"));
                    Label lbledit = (Label)(item.FindControl("lbledit"));
                    Label lblema = (Label)(item.FindControl("lblema"));
                    Label lblmess = (Label)(item.FindControl("lblmess"));
                    string cabid = grdfillcab.DataKeys[item.RowIndex].Value.ToString();
                    GridView grddrower = (GridView)item.FindControl("grddrower");

                }
            }
        }
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        //if (btnedit.Text == "Edit")
        //{
        //    btnedit.Text = "View";
        //  //  Label9.Visible = true;
        //   // ddlselection.Visible = true;
        //    pnlfilec.Visible = true;
         
        //    filldata();
        //}
        //else
        //{
        //    btnedit.Text = "Edit";
        //   // Label9.Visible = false;
        //   // ddlselection.Visible = false;
          
        //    pnlfilec.Visible = false;
       
           
        //}
    }

 
   
    protected void rdallcab_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkdeletecab.Checked = false;
        chksavecab.Checked = false;
        chkviewcab.Checked = true;
        chkeditcab.Checked = false;
        chkmailcab.Checked = false;
        chkmessagecab.Checked = false;
        //if (rdsiglebus.SelectedValue == "1")
        //{
        //    pnlgrid.Visible = false;
        //    pnlallcab.Visible = true;
           

        //}
        //else
        if(rdallcab.SelectedValue == "0")
        {
            pnlgrid.Visible = false;
          
        }
        else if (rdallcab.SelectedValue == "2")
        {
            pnlgrid.Visible = true;
          

        }

    }
    protected void rdcab_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;



        string cabid = grdfillcab.DataKeys[rinrow].Value.ToString();
        GridView grddrower = (GridView)grdfillcab.Rows[rinrow].FindControl("grddrower");

        DropDownList rdcab = (DropDownList)(grdfillcab.Rows[rinrow].FindControl("rdcab"));
        CheckBox chkview = (CheckBox)(grdfillcab.Rows[rinrow].FindControl("chkview"));
        CheckBox chkdelete = (CheckBox)(grdfillcab.Rows[rinrow].FindControl("chkdelete"));
        CheckBox chksave = (CheckBox)(grdfillcab.Rows[rinrow].FindControl("chksave"));
        CheckBox chkedit = (CheckBox)(grdfillcab.Rows[rinrow].FindControl("chkedit"));
        CheckBox chkemail = (CheckBox)(grdfillcab.Rows[rinrow].FindControl("chkemail"));
        CheckBox chkMessage = (CheckBox)(grdfillcab.Rows[rinrow].FindControl("chkMessage"));


        Label lblviw1 = (Label)(grdfillcab.Rows[rinrow].FindControl("lblviw"));

        Label lbldel1 = (Label)(grdfillcab.Rows[rinrow].FindControl("lbldel"));
        Label lblsave1 = (Label)(grdfillcab.Rows[rinrow].FindControl("lblsave"));
        Label lbledit1 = (Label)(grdfillcab.Rows[rinrow].FindControl("lbledit"));
        Label lblema1 = (Label)(grdfillcab.Rows[rinrow].FindControl("lblema"));
        Label lblmess1 = (Label)(grdfillcab.Rows[rinrow].FindControl("lblmess"));
        if (rdcab.SelectedValue == "2" || rdcab.SelectedValue == "0")
        {
            chkview.Visible = false;
            chkdelete.Visible = false;
            chksave.Visible = false;
            chkedit.Visible = false;
            chkemail.Visible = false;
            chkMessage.Visible = false;
            lblviw1.Visible = false;
            lbldel1.Visible = false;
            lblsave1.Visible = false;
            lbledit1.Visible = false;
            lblema1.Visible = false;
            lblmess1.Visible = false;
            if (rdcab.SelectedValue == "2")
            {
                DataTable dtde = select("select DocumentSubTypeId,'Drawer - '+DocumentSubType as DocumentSubType from DocumentSubType where DocumentMainTypeId='" + cabid + "'");
                grddrower.DataSource = dtde;
                grddrower.DataBind();
            }
            else
            {
                grddrower.DataSource = null;
                grddrower.DataBind();
            }
        }

        else
        {

            chkview.Visible = true;
            chkdelete.Visible = true;
            chksave.Visible = true;
            chkedit.Visible = true;
            chkemail.Visible = true;
            chkMessage.Visible = true;
            lblviw1.Visible = true;
            lbldel1.Visible = true;
            lblsave1.Visible = true;
            lbledit1.Visible = true;
            lblema1.Visible = true;
            lblmess1.Visible = true;
            chkview.Checked = true;
            chkdelete.Checked = false;
            chksave.Checked = false;
            chkedit.Checked = false;
            chkemail.Checked = false;
            chkMessage.Checked = false;
            grddrower.DataSource = null;
            grddrower.DataBind();
        }

    }
    protected void rddrow_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;


        DropDownList chkprocess = (DropDownList)sender;
        GridView grddrower = (GridView)chkprocess.NamingContainer.NamingContainer;
        //string i = grdfillcab.DataKeys[it.RowIndex].Value.ToString();

      
       string cabid = grddrower.DataKeys[rinrow].Value.ToString();

       DropDownList rdcab = (DropDownList)(grddrower.Rows[rinrow].FindControl("rddrow"));
        GridView grddfolder = (GridView)grddrower.Rows[rinrow].FindControl("grddfolder");


      

        CheckBox chkfax = (CheckBox)(grddrower.Rows[rinrow].FindControl("chkfax"));
        CheckBox chkview = (CheckBox)(grddrower.Rows[rinrow].FindControl("chkview"));
        CheckBox chkdelete = (CheckBox)(grddrower.Rows[rinrow].FindControl("chkdelete"));
        CheckBox chksave = (CheckBox)(grddrower.Rows[rinrow].FindControl("chksave"));
        CheckBox chkedit = (CheckBox)(grddrower.Rows[rinrow].FindControl("chkedit"));
        CheckBox chkemail = (CheckBox)(grddrower.Rows[rinrow].FindControl("chkemail"));
        CheckBox chkMessage = (CheckBox)(grddrower.Rows[rinrow].FindControl("chkMessage"));


        Label lblviw1 = (Label)(grddrower.Rows[rinrow].FindControl("lblviw"));

        Label lbldel1 = (Label)(grddrower.Rows[rinrow].FindControl("lbldel"));
        Label lblsave1 = (Label)(grddrower.Rows[rinrow].FindControl("lblsave"));
        Label lbledit1 = (Label)(grddrower.Rows[rinrow].FindControl("lbledit"));
        Label lblema1 = (Label)(grddrower.Rows[rinrow].FindControl("lblema"));
        Label lblmess1 = (Label)(grddrower.Rows[rinrow].FindControl("lblmess"));
        
          
           
        if (rdcab.SelectedValue == "2" || rdcab.SelectedValue == "0")
        {
            chkview.Visible = false;
            chkdelete.Visible = false;
            chksave.Visible = false;
            chkedit.Visible = false;
            chkemail.Visible = false;
            chkMessage.Visible = false;
            lblviw1.Visible = false;
            lbldel1.Visible = false;
            lblsave1.Visible = false;
            lbledit1.Visible = false;
            lblema1.Visible = false;
            lblmess1.Visible = false;
            if (rdcab.SelectedValue == "2")
            {
                DataTable dtde1 = select("select DocumentTypeId,'Folder - '+DocumentType as DocumentType from DocumentType where DocumentSubTypeId='" + cabid + "'");
            grddfolder.DataSource = dtde1;
            grddfolder.DataBind();
            }
            else
            {
                grddfolder.DataSource = null;
                grddfolder.DataBind();
            }
        }

        else
        {

            chkview.Visible = true;
            chkdelete.Visible = true;
            chksave.Visible = true;
            chkedit.Visible = true;
            chkemail.Visible = true;
            chkMessage.Visible = true;
            lblviw1.Visible = true;
            lbldel1.Visible = true;
            lblsave1.Visible = true;
            lbledit1.Visible = true;
            lblema1.Visible = true;
            lblmess1.Visible = true;
            chkview.Checked = true;
            chkdelete.Checked = false;
            chksave.Checked = false;
            chkedit.Checked = false;
            chkemail.Checked = false;
            chkMessage.Checked = false;
            grddfolder.DataSource = null;
            grddfolder.DataBind();
        }

    }

    protected void rdfolder_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;


        DropDownList chkprocess = (DropDownList)sender;
        GridView grddfolder = (GridView)chkprocess.NamingContainer.NamingContainer;
        //string i = grdfillcab.DataKeys[it.RowIndex].Value.ToString();


        string cabid = grddfolder.DataKeys[rinrow].Value.ToString();

        DropDownList rdcab = (DropDownList)(grddfolder.Rows[rinrow].FindControl("rdfolder"));
       
        CheckBox chkview = (CheckBox)(grddfolder.Rows[rinrow].FindControl("chkview"));
        CheckBox chkdelete = (CheckBox)(grddfolder.Rows[rinrow].FindControl("chkdelete"));
        CheckBox chksave = (CheckBox)(grddfolder.Rows[rinrow].FindControl("chksave"));
        CheckBox chkedit = (CheckBox)(grddfolder.Rows[rinrow].FindControl("chkedit"));
        CheckBox chkemail = (CheckBox)(grddfolder.Rows[rinrow].FindControl("chkemail"));
        CheckBox chkMessage = (CheckBox)(grddfolder.Rows[rinrow].FindControl("chkMessage"));


        Label lblviw1 = (Label)(grddfolder.Rows[rinrow].FindControl("lblviw"));

        Label lbldel1 = (Label)(grddfolder.Rows[rinrow].FindControl("lbldel"));
        Label lblsave1 = (Label)(grddfolder.Rows[rinrow].FindControl("lblsave"));
        Label lbledit1 = (Label)(grddfolder.Rows[rinrow].FindControl("lbledit"));
        Label lblema1 = (Label)(grddfolder.Rows[rinrow].FindControl("lblema"));
        Label lblmess1 = (Label)(grddfolder.Rows[rinrow].FindControl("lblmess"));



        if ( rdcab.SelectedValue == "0")
        {
            chkview.Visible = false;
            chkdelete.Visible = false;
            chksave.Visible = false;
            chkedit.Visible = false;
            chkemail.Visible = false;
            chkMessage.Visible = false;
            lblviw1.Visible = false;
            lbldel1.Visible = false;
            lblsave1.Visible = false;
            lbledit1.Visible = false;
            lblema1.Visible = false;
            lblmess1.Visible = false;
           
        }

        else
        {

            chkview.Visible = true;
            chkdelete.Visible = true;
            chksave.Visible = true;
            chkedit.Visible = true;
            chkemail.Visible = true;
            chkMessage.Visible = true;
            lblviw1.Visible = true;
            lbldel1.Visible = true;
            lblsave1.Visible = true;
            lbledit1.Visible = true;
            lblema1.Visible = true;
            lblmess1.Visible = true;
            chkview.Checked = true;
            chkdelete.Checked = false;
            chksave.Checked = false;
            chkedit.Checked = false;
            chkemail.Checked = false;
            chkMessage.Checked = false;
            
        }

    }

    protected void rdsiglebus_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlbussele_SelectedIndexChanged(sender, e);
        if (rdsiglebus.SelectedValue == "1")
        {
            pnlrightd.Visible = false;
            pnlallcab.Visible = true;
            pnlgrid.Visible = false;
        }
        else
        {
            if (grdfillcab.Rows.Count == 0)
            {
                filldata();
            }
            pnlrightd.Visible = true;
            pnlallcab.Visible = false;
            pnlgrid.Visible = true;
            //ddlbussele_SelectedIndexChanged(sender, e);
        }
        if (rdbus.SelectedValue == "0")
        {
            pnlallbus.Visible = false;
            pnl1dis.Visible = true;
            //   ddlbussele_SelectedIndexChanged(sender, e);
        }
        else
        {

            pnlallbus.Visible = true;
            pnl1dis.Visible = false;
        }
    }
   
}
