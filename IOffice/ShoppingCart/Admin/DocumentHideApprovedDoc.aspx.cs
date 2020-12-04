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
using System.Data.SqlClient;
public partial class DocumentHideApprovedDoc : System.Web.UI.Page
{
    SqlConnection con;
    DataTable dt;
    DocumentCls1 clsDocument = new DocumentCls1();
    MasterCls clsMaster = new MasterCls();
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
			Session["PageUrl"]=strData;
            Session["PageName"] = page;
            Page.Title = pg.getPageTitle(page);
            

            pnlmsg.Visible = false;
            if (!Page.IsPostBack)
            {

                Pagecontrol.dypcontrol(Page, page);
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
                pnlFromDate.Visible = false;
                pnlFromId.Visible = false;
                string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
                SqlCommand cmdeeed = new SqlCommand(eeed, con);
                SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                DataTable dteeed = new DataTable();
                adpeeed.Fill(dteeed);
                if (dteeed.Rows.Count > 0)
                {
                    ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

                }
                ddlbusiness_SelectedIndexChanged(sender, e);
                txtfrom.Text = System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString();
                txtto.Text = System.DateTime.Now.ToShortDateString();

                
            }
            
    }
    protected void ClearAll()
    {
        txtfrom.Text = "";
        txtto.Text = "";
        txtFromId.Text = "";
        txtToId.Text = "";
    }
    protected void Rbtnoptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        if (Rbtnoptions.SelectedValue.ToString() == "1")
        {
            pnlFromDate.Visible = false;
            pnlFromId.Visible = false;
        }
        if (Rbtnoptions.SelectedValue.ToString() == "2")
        {          
            pnlFromDate.Visible = true;
            pnlFromId.Visible = false;
        }
        if (Rbtnoptions.SelectedValue.ToString() == "3")
        {
            pnlFromDate.Visible = false;
            pnlFromId.Visible = true;
        }  
    }
    protected void ImgBtnSubmit_Click(object sender, EventArgs e)
    {
        DateTime fromdate, todate ;
        if (Rbtnoptions.SelectedValue.ToString() == "2")
        {
            fromdate = Convert.ToDateTime(txtfrom.Text);
            todate = Convert.ToDateTime(txtto.Text);
        }
        else
        {
            fromdate = Convert.ToDateTime("01/01/1900");
            todate = Convert.ToDateTime("01/01/1900");
        }
        
        Int32  FromId, ToId ;
        if (Rbtnoptions.SelectedValue.ToString() == "3")
        {
            FromId = Convert.ToInt32(txtFromId.Text.ToString());
            ToId = Convert.ToInt32(txtToId.Text.ToString());
        }
        else
        {
            FromId = 0;
            ToId = 0;
        }
        bool deleteforAll = clsDocument.DeleteDocumentViewRuleMasterDesIdWise(1,ddlbusiness.SelectedValue);
         
        Int32 ins = clsDocument.InsertDocumentViewRuleMaster(Convert.ToInt32(rbtnDesignationOpt.SelectedValue.ToString()), Convert.ToInt32 (Rbtnoptions.SelectedValue.ToString()), fromdate, todate, FromId, ToId,ddlbusiness.SelectedValue);
        if (ins > 0)
        {
            pnlmsg.Visible = true;
            lblmsg.Text = "Rule is set successfully.";
            if (rbtnDesignationOpt.SelectedIndex >= 0)
            {
                FillGrid();

            }
            ClearAll();
        }
    }
    void All(bool status)
    {
        pnlAll.Visible = status;
        
    }
    protected void rbtnDesignationOpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnDesignationOpt.SelectedIndex >= 0)
        {
            FillGrid();

        }
    }
    protected void FillGrid()
    {
        lblbusiness1.Text = ddlbusiness.SelectedItem.Text;
        lblbusiness2.Text = ddlbusiness.SelectedItem.Text;
        lblbusiness3.Text = ddlbusiness.SelectedItem.Text;
        lblbusiness4.Text = ddlbusiness.SelectedItem.Text;

        dt = new DataTable();
        if (rbtnDesignationOpt.SelectedIndex >= 0)
        {
            dt = clsDocument.SelectDocumentViewRuleMasterDesIdWise(Convert.ToInt32(rbtnDesignationOpt.SelectedValue.ToString()),ddlbusiness.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                Int32 RuleSelectId = Convert.ToInt32(dt.Rows[0]["RuleSelectId"].ToString());
                Int32 DesId = Convert.ToInt32(dt.Rows[0]["DesId"].ToString());
                if (rbtnDesignationOpt.SelectedValue.ToString() == "1")
                {
                    pnlAdmin.Visible = false;
                    pnlAll.Visible = true;
                    pnlSuper.Visible = false;
                    pnlDataEO.Visible = false;

                    if (RuleSelectId.ToString() == "1")
                    {
                        lblSelectionFor.Text = "All";
                         
                        lblFromData.Text = "";
                        lblTo.Text = "";
                        lblFromDatamain.Text = "";
                        lblToData.Text = "";
                        lblToDataMain.Text = "";
                        lblFrom.Text = "";
                    }
                    if (RuleSelectId.ToString() == "2")
                    {
                        
                        lblSelectionFor.Text = "";
                        lblFrom.Text = "From Date";
                        lblFromDatamain.Text = dt.Rows[0]["FromDate"].ToString();
                        lblTo.Text = "To Date";
                        lblToDataMain.Text = dt.Rows[0]["ToDate"].ToString();
                        lblFromData.Text = "";
                        lblToData.Text = "";
                       
                    }
                    if (RuleSelectId.ToString() == "3")
                    {
                       
                        lblSelectionFor.Text = "";
                        lblFrom.Text = "From Document ID";
                        lblFromDatamain.Text = dt.Rows[0]["FromId"].ToString();
                        lblTo.Text = "To Documnet ID";
                        lblToDataMain.Text = dt.Rows[0]["ToId"].ToString();
                        lblFromData.Text = "";
                        lblToData.Text = "";
                    }

                }
                if (rbtnDesignationOpt.SelectedValue.ToString() == "2")
                {
                    pnlAdmin.Visible = true;
                    pnlAll.Visible = false;
                    pnlSuper.Visible = false;
                    pnlDataEO.Visible = false;

                    if (RuleSelectId.ToString() == "1")
                    {
                        lblSelectionForAdmin.Text = "All";
                        lblFromAdmin.Text = "";
                        lblFromDataAdmin.Text = "";
                        lblFromDataAdminMain.Text = "";                        
                        lblToDataAdmin.Text = "";
                        lblToDataAdminMain.Text = "";
                        lblToAdmin.Text = "";
                        lblFromAdmin.Text = "";
                        lblFromDataAdmin.Text = "";

                    }
                    if (RuleSelectId.ToString() == "2")
                    {
                       
                        lblSelectionForAdmin.Text = "";
                        lblFromAdmin.Text = "From Date";
                        lblFromDataAdminMain.Text = dt.Rows[0]["FromDate"].ToString();
                        lblToAdmin.Text = "To Date";
                        lblToDataAdminMain.Text = dt.Rows[0]["ToDate"].ToString();
                        lblToDataAdmin.Text = "";
                        lblFromDataAdmin.Text = "";

                    }
                    if (RuleSelectId.ToString() == "3")
                    {
                       

                        lblSelectionForAdmin.Text = "";
                        lblFromAdmin.Text = "From Document ID";
                        lblFromDataAdminMain.Text = dt.Rows[0]["FromId"].ToString();
                        lblToAdmin.Text = "To Documnet ID";
                        lblToDataAdminMain.Text = dt.Rows[0]["ToId"].ToString();
                        lblToDataAdmin.Text = "";
                        lblFromDataAdmin.Text = "";
                    }
                }
                if (rbtnDesignationOpt.SelectedValue.ToString() == "3")
                {

                    pnlAdmin.Visible = false;
                    pnlAll.Visible = false;
                    pnlSuper.Visible = true;
                    pnlDataEO.Visible = false;
                    if (RuleSelectId.ToString() == "1")
                    {
                        lblselectionforS.Text = "All";
                        lblFromS.Text = "";
                        lblFromDataS.Text = "";
                        lblFromDataSmain.Text = "";
                        lblToDataS.Text = "";
                        lblToDataSMain.Text = "";
                        lblToDataAdmin.Text = "";
                        lblToS.Text = "";
                        lblFromS.Text = "";
                    }
                    if (RuleSelectId.ToString() == "2")
                    {

                       
                        lblselectionforS.Text = "";
                        lblFromS.Text = "From Date";
                        lblFromDataSmain.Text = dt.Rows[0]["FromDate"].ToString();

                        lblToS.Text = "To Date";
                        lblToDataSMain.Text = dt.Rows[0]["ToDate"].ToString();

                        lblFromDataS.Text = "";
                        lblToDataS.Text = "";


                    }
                    if (RuleSelectId.ToString() == "3")
                    {
                        lblselectionforS.Text = "";
                        lblFromS.Text = "From Document ID";
                        lblFromDataSmain.Text = dt.Rows[0]["FromId"].ToString();
                        lblToS.Text = "To Documnet ID";
                        lblToDataSMain.Text = dt.Rows[0]["ToId"].ToString();
                        lblFromDataS.Text = "";
                        lblToDataS.Text = "";
                    }
                }
                if (rbtnDesignationOpt.SelectedValue.ToString() == "4")
                {
                    pnlAdmin.Visible = false;
                    pnlAll.Visible = false;
                    pnlSuper.Visible = false;
                    pnlDataEO.Visible = true;
                    if (RuleSelectId.ToString() == "1")
                    {
                     lblselectionforD.Text = "All";
                     lblfromD.Text   = "";
                     lblfromDataD.Text  = "";
                     lblfromDataDMain.Text   = "";
                     lblToDataD.Text   = "";
                     lblTodataDMain.Text   = "";
                     lblToD.Text = "";
                         
                    }
                    if (RuleSelectId.ToString() == "2")
                    {

                        lblselectionforD.Text = "";
                        lblfromD.Text = "From Date";
                        lblfromDataDMain.Text = dt.Rows[0]["FromDate"].ToString();
                        lblToD.Text = "To Date";
                        lblTodataDMain.Text = dt.Rows[0]["ToDate"].ToString();
                        lblfromDataD.Text = "";
                        lblToDataD.Text = "";


                    }
                    if (RuleSelectId.ToString() == "3")
                    {
                        lblselectionforD.Text = "";
                        lblfromD.Text = "From Document ID";
                        lblfromDataDMain.Text = dt.Rows[0]["FromId"].ToString();
                        lblToD.Text = "To Documnet ID";
                        lblTodataDMain.Text = dt.Rows[0]["ToId"].ToString();
                        lblfromDataD.Text = "";
                        lblToDataD.Text = "";
                    }
                }
            }
            else
            {
                pnlAdmin.Visible = false;
                pnlAll.Visible = false;
                pnlSuper.Visible = false;
                pnlDataEO.Visible = false;
            }
        }
        else
        {
            dt = clsDocument.SelectDocumentViewRuleMaster(ddlbusiness.SelectedValue );
             if (dt.Rows.Count > 0)
            {
                Int32 RuleSelectId = Convert.ToInt32( dt.Rows[0]["RuleSelectId"].ToString());
                Int32 DesId = Convert.ToInt32(dt.Rows[0]["DesId"].ToString());
                if (rbtnDesignationOpt.SelectedValue.ToString() == "1")
                {
                    pnlAdmin.Visible = false;
                    pnlAll.Visible = true;
                    pnlSuper.Visible = false;
                    pnlDataEO.Visible = false;                   
                      
                    if (RuleSelectId.ToString() == "1")
                    {
                      lblSelectionFor.Text =  "All";
                       
                      lblFromData.Text = "";
                      lblFromDatamain.Text = "";
                      lblToData.Text = "";
                      lblToDataMain.Text = "";
                      lblTo.Text = "To ";
                      lblFrom.Text = "";
                    }
                    if (RuleSelectId.ToString() == "2")
                    {
                       
                        lblSelectionFor.Text = "";
                        lblFrom.Text = "From Date";
                        lblFromDatamain.Text = dt.Rows[0]["FromDate"].ToString();
                        lblTo.Text = "To Date";
                        lblToDataMain.Text = dt.Rows[0]["ToDate"].ToString();
                        lblFromData.Text = "";
                        lblToData.Text = "";
                    }
                    if (RuleSelectId.ToString() == "3")
                    {
                        

                        lblSelectionFor.Text = "";
                        lblFrom.Text = "From Document ID";
                        lblFromDatamain.Text = dt.Rows[0]["FromId"].ToString();
                        lblTo.Text = "To Documnet ID";
                        lblToDataMain.Text = dt.Rows[0]["ToId"].ToString();
                        lblFromData.Text = "";
                        lblToData.Text = "";
                    }

                }
                if (rbtnDesignationOpt.SelectedValue.ToString() == "2")
                {
                    pnlAdmin.Visible = true;
                    pnlAll.Visible = false;
                    pnlSuper.Visible = false;
                    pnlDataEO.Visible = false;
                   
                    if (RuleSelectId.ToString() == "1")
                    {
                        lblSelectionForAdmin.Text  = "All";
                        lblFromAdmin.Text = "";
                        lblFromDataAdmin.Text = "";
                        lblFromDataAdminMain.Text = "";                      
                        lblToDataAdmin.Text = "";
                        lblToDataAdminMain.Text = "";
                        lblFromAdmin.Text = "";
                        lblToAdmin.Text = "";
                    }
                    if (RuleSelectId.ToString() == "2")
                    {
                     

                        lblSelectionForAdmin.Text = "";
                        lblFromAdmin.Text = "From Date";
                        lblFromDataAdminMain.Text = dt.Rows[0]["FromDate"].ToString();
                        lblToAdmin.Text = "To Date";
                        lblToDataAdminMain.Text = dt.Rows[0]["ToDate"].ToString();
                        lblToDataAdmin.Text = "";
                        lblFromDataAdmin.Text = "";
                    }
                    if (RuleSelectId.ToString() == "3")
                    {
                       

                        lblSelectionForAdmin.Text = "";
                        lblFromAdmin.Text = "From Document ID";
                        lblFromDataAdminMain.Text = dt.Rows[0]["FromId"].ToString();
                        lblToAdmin.Text = "To Documnet ID";
                        lblToDataAdminMain.Text = dt.Rows[0]["ToId"].ToString();
                        lblToDataAdmin.Text = "";
                        lblFromDataAdmin.Text = "";
                    }
                }
                if (rbtnDesignationOpt.SelectedValue.ToString() == "3")
                {                  
                    pnlAdmin.Visible = false;
                    pnlAll.Visible = false;
                    pnlSuper.Visible = true;
                    pnlDataEO.Visible = false;
                    if (RuleSelectId.ToString() == "1")
                    {
                       lblselectionforS.Text  = "All";
                       lblFromS.Text = "";
                       lblFromDataS.Text = "";
                       lblFromDataSmain.Text = "";
                       lblToDataS.Text = "";
                       lblToDataSMain.Text = "";
                       lblToDataAdmin.Text = "";
                       lblToS.Text = "";
                       lblFromS.Text = "";
                    }
                    if (RuleSelectId.ToString() == "2")
                    {                         
                        
                        lblselectionforS.Text = "";
                        lblFromS.Text = "From Date";
                        lblFromDataSmain.Text = dt.Rows[0]["FromDate"].ToString();

                        lblToS.Text = "To Date";
                        lblToDataSMain.Text = dt.Rows[0]["ToDate"].ToString();

                        lblFromDataS.Text = "";
                        lblToDataS.Text = "";

                    }
                    if (RuleSelectId.ToString() == "3")
                    {
                        
                        lblselectionforS.Text = "";
                        lblFromS.Text = "From Document ID";
                        lblFromDataSmain.Text = dt.Rows[0]["FromId"].ToString();
                        lblToS.Text = "To Documnet ID";
                        lblToDataSMain.Text = dt.Rows[0]["ToId"].ToString();
                        lblFromDataS.Text = "";
                        lblToDataS.Text = "";
                    }
                }
                if (rbtnDesignationOpt.SelectedValue.ToString() == "4")
                {
                    pnlAdmin.Visible = false;
                    pnlAll.Visible = false;
                    pnlSuper.Visible = false;
                    pnlDataEO.Visible = true;
                    if (RuleSelectId.ToString() == "1")
                    {
                        lblselectionforD.Text = "All";
                        lblfromD.Text = "";
                        lblfromDataD.Text = "";
                        lblfromDataDMain.Text = "";
                        lblToDataD.Text = "";
                        lblTodataDMain.Text = "";
                        lblToD.Text = "";
                    }
                    if (RuleSelectId.ToString() == "2")
                    {
                        lblselectionforD.Text = "";
                        lblfromD.Text = "From Date";
                        lblfromDataDMain.Text = dt.Rows[0]["FromDate"].ToString();
                        lblToD.Text = "To Date";
                        lblTodataDMain.Text = dt.Rows[0]["ToDate"].ToString();
                        lblfromDataD.Text = "";
                        lblToDataD.Text = "";
                    }
                    if (RuleSelectId.ToString() == "3")
                    {
                        lblselectionforD.Text = "";
                        lblfromD.Text = "From Document ID";
                        lblfromDataDMain.Text = dt.Rows[0]["FromId"].ToString();
                        lblToD.Text = "To Documnet ID";
                        lblTodataDMain.Text = dt.Rows[0]["ToId"].ToString();
                        lblfromDataD.Text = "";
                        lblToDataD.Text = "";
                    }
                }
            }
            else
            {
                pnlAdmin.Visible = false;
                pnlAll.Visible = false;
                pnlSuper.Visible = false;
                pnlDataEO.Visible = false;
            }
        }
    
    }
    protected void FillGridAll()
    {
        lblbusiness1.Text = ddlbusiness.SelectedItem.Text;
        lblbusiness2.Text = ddlbusiness.SelectedItem.Text;
        lblbusiness3.Text = ddlbusiness.SelectedItem.Text;
        lblbusiness4.Text = ddlbusiness.SelectedItem.Text;

        dt = new DataTable();
        if (rbtnDesignationOpt.SelectedIndex >= 0)
        {
            pnlAdmin.Visible = false;
            pnlAll.Visible = false;
            pnlSuper.Visible = false;
            pnlDataEO.Visible = false;
            dt = clsDocument.SelectDocumentViewRuleMaster(ddlbusiness.SelectedValue); //.SelectDocumentViewRuleMasterDesIdWise(Convert.ToInt32(rbtnDesignationOpt.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {              
                foreach (DataRow dr in dt.Rows)
                {
                    Int32 RuleSelectId = Convert.ToInt32(dr["RuleSelectId"].ToString());
                    Int32 DesId = Convert.ToInt32(dr["DesId"].ToString());
                    if (DesId.ToString() == "1")
                    {
                        pnlAdmin.Visible = false;
                        pnlAll.Visible = true;
                        pnlSuper.Visible = false;
                        pnlDataEO.Visible = false;
                        if (RuleSelectId.ToString() == "1")
                        {
                            lblSelectionFor.Text = "All";

                            lblFromData.Text = "";
                            lblTo.Text = "";
                            lblFromDatamain.Text = "";
                            lblToData.Text = "";
                            lblToDataMain.Text = "";
                            lblFrom.Text = "";
                        }
                        if (RuleSelectId.ToString() == "2")
                        {
                            lblSelectionFor.Text = "";
                            lblFrom.Text = "From Date";
                            lblFromDatamain.Text = dr["FromDate"].ToString();
                            lblTo.Text = "To Date";
                            lblToDataMain.Text = dr["ToDate"].ToString();
                            lblFromData.Text = "";
                            lblToData.Text = "";
                           
                        }
                        if (RuleSelectId.ToString() == "3")
                        {
                            lblSelectionFor.Text = "";
                            lblFrom.Text = "From Document ID";
                            lblFromDatamain.Text = dr["FromId"].ToString();
                            lblTo.Text = "To Documnet ID";
                            lblToDataMain.Text = dr["ToId"].ToString();
                            lblFromData.Text = "";
                            lblToData.Text = "";
                           
                        }

                    }
                    if (DesId.ToString() == "2")
                    {
                        pnlAdmin.Visible = true;
                       

                        if (RuleSelectId.ToString() == "1")
                        {
                            lblSelectionForAdmin.Text = "All";
                            lblFromAdmin.Text = "";
                            lblFromDataAdmin.Text = "";
                            lblFromDataAdminMain.Text = "";
                            lblToDataAdmin.Text = "";
                            lblToDataAdminMain.Text = "";
                            lblToAdmin.Text = "";
                            lblFromAdmin.Text = "";

                        }
                        if (RuleSelectId.ToString() == "2")
                        {
                            lblSelectionForAdmin.Text = "";
                            lblFromAdmin.Text = "From Date";
                            lblFromDataAdminMain.Text = dr["FromDate"].ToString();
                            lblToAdmin.Text = "To Date";
                            lblToDataAdminMain.Text = dr["ToDate"].ToString();
                            lblToDataAdmin.Text = "";
                            lblFromDataAdmin.Text = "";

                           

                        }
                        if (RuleSelectId.ToString() == "3")
                        {
                          

                            lblSelectionForAdmin.Text = "";
                            lblFromAdmin.Text = "From Document ID";
                            lblFromDataAdminMain.Text = dr["FromId"].ToString();
                            lblToAdmin.Text = "To Documnet ID";
                            lblToDataAdminMain.Text = dr["ToId"].ToString();
                            lblToDataAdmin.Text = "";
                            lblFromDataAdmin.Text = "";
                        }
                    }
                    if (DesId.ToString() == "3")
                    {

                      
                        pnlSuper.Visible = true;
                        
                        if (RuleSelectId.ToString() == "1")
                        {
                            lblselectionforS.Text = "All";
                            lblFromS.Text = "";
                            lblFromDataS.Text = "";
                            lblFromDataSmain.Text = "";
                            lblToDataS.Text = "";
                            lblToDataSMain.Text = "";
                            lblToDataAdmin.Text = "";
                            lblToS.Text = "";
                            lblFromS.Text = "";
                        }
                        if (RuleSelectId.ToString() == "2")
                        {

                            lblselectionforS.Text = "";
                            lblFromS.Text = "From Date";
                            lblFromDataSmain.Text = dr["FromDate"].ToString();
                            lblToS.Text = "To Date";
                            lblToDataSMain.Text = dr["ToDate"].ToString();
                            lblFromDataS.Text = "";
                            lblToDataS.Text = "";

                        }
                        if (RuleSelectId.ToString() == "3")
                        {
                           
                            lblselectionforS.Text = "";
                            lblFromS.Text = "From Document ID";
                            lblFromDataSmain.Text = dr["FromId"].ToString();
                            lblToS.Text = "To Documnet ID";
                            lblToDataSMain.Text = dr["ToId"].ToString();
                            lblFromDataS.Text = "";
                            lblToDataS.Text = "";

                        }
                    }
                    if (DesId.ToString() == "4")
                    {
                      
                        pnlDataEO.Visible = true;
                        if (RuleSelectId.ToString() == "1")
                        {
                            lblselectionforD.Text = "All";
                            lblfromD.Text = "";
                            lblfromDataD.Text = "";
                            lblfromDataDMain.Text = "";
                            lblToDataD.Text = "";
                            lblTodataDMain.Text = "";
                            lblToD.Text = "";

                        }
                        if (RuleSelectId.ToString() == "2")
                        {

                            lblselectionforD.Text = "";
                            lblfromD.Text = "From Date";
                            lblfromDataDMain.Text = dr["FromDate"].ToString();
                            lblToD.Text = "To Date";
                            lblTodataDMain.Text = dr["ToDate"].ToString();
                            lblfromDataD.Text = "";
                            lblToDataD.Text = "";
                            


                        }
                        if (RuleSelectId.ToString() == "3")
                        {
                          
                            lblselectionforD.Text = "";
                            lblfromD.Text = "From Document ID";
                            lblfromDataDMain.Text = dr["FromDate"].ToString();
                            lblToD.Text = "To Documnet ID";
                            lblTodataDMain.Text = dr["ToDate"].ToString();
                            lblfromDataD.Text = "";
                            lblToDataD.Text = "";
                        }
                    }
                }

            }
            else
            {
                pnlAdmin.Visible = false;
                pnlAll.Visible = false;
                pnlSuper.Visible = false;
                pnlDataEO.Visible = false;
            }
        }
        else
        {
            dt = clsDocument.SelectDocumentViewRuleMaster(ddlbusiness.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                Int32 RuleSelectId = Convert.ToInt32(dt.Rows[0]["RuleSelectId"].ToString());
                Int32 DesId = Convert.ToInt32(dt.Rows[0]["DesId"].ToString());
                if (rbtnDesignationOpt.SelectedValue.ToString() == "1")
                {
                    pnlAdmin.Visible = false;
                    pnlAll.Visible = true;
                    pnlSuper.Visible = false;
                    pnlDataEO.Visible = false;

                    if (RuleSelectId.ToString() == "1")
                    {
                        lblSelectionFor.Text = "All";

                        lblFromData.Text = "";
                        lblFromDatamain.Text = "";
                        lblToData.Text = "";
                        lblToDataMain.Text = "";
                        lblTo.Text = "To ";
                        lblFrom.Text = "";
                    }
                    if (RuleSelectId.ToString() == "2")
                    {
                       
                        lblSelectionFor.Text = "";
                        lblFrom.Text = "From Date";
                        lblFromDatamain.Text = dt.Rows[0]["FromDate"].ToString();
                        lblTo.Text = "To Date";
                        lblToDataMain.Text = dt.Rows[0]["ToDate"].ToString();
                        lblFromData.Text = "";
                        lblToData.Text = "";
                    }
                    if (RuleSelectId.ToString() == "3")
                    {
                       
                        lblSelectionFor.Text = "";
                        lblFrom.Text = "From Document ID";
                        lblFromDatamain.Text = dt.Rows[0]["FromId"].ToString();
                        lblTo.Text = "To Documnet ID";
                        lblToDataMain.Text = dt.Rows[0]["ToId"].ToString();
                        lblFromData.Text = "";
                        lblToData.Text = "";
                    }

                }
                if (rbtnDesignationOpt.SelectedValue.ToString() == "2")
                {
                    pnlAdmin.Visible = true;
                    pnlAll.Visible = false;
                    pnlSuper.Visible = false;
                    pnlDataEO.Visible = false;

                    if (RuleSelectId.ToString() == "1")
                    {
                        lblSelectionForAdmin.Text = "All";
                        lblFromAdmin.Text = "";
                        lblFromDataAdmin.Text = "";
                        lblFromDataAdminMain.Text = "";
                        lblToDataAdmin.Text = "";
                        lblToDataAdminMain.Text = "";
                        lblFromAdmin.Text = "";
                        lblToAdmin.Text = "";
                    }
                    if (RuleSelectId.ToString() == "2")
                    {
                       
                        lblSelectionForAdmin.Text = "";
                        lblFromAdmin.Text = "From Date";
                        lblFromDataAdminMain.Text = dt.Rows[0]["FromDate"].ToString();
                        lblToAdmin.Text = "To Date";
                        lblToDataAdminMain.Text = dt.Rows[0]["ToDate"].ToString();
                        lblToDataAdmin.Text = "";
                        lblFromDataAdmin.Text = "";
                    }
                    if (RuleSelectId.ToString() == "3")
                    {
                       
                        lblSelectionForAdmin.Text = "";
                        lblFromAdmin.Text = "From Document ID";
                        lblFromDataAdminMain.Text = dt.Rows[0]["FromId"].ToString();
                        lblToAdmin.Text = "To Documnet ID";
                        lblToDataAdminMain.Text = dt.Rows[0]["ToId"].ToString();
                        lblToDataAdmin.Text = "";
                        lblFromDataAdmin.Text = "";
                    }
                }
                if (rbtnDesignationOpt.SelectedValue.ToString() == "3")
                {
                    pnlAdmin.Visible = false;
                    pnlAll.Visible = false;
                    pnlSuper.Visible = true;
                    pnlDataEO.Visible = false;
                    if (RuleSelectId.ToString() == "1")
                    {
                        lblselectionforS.Text = "All";
                        lblFromS.Text = "";
                        lblFromDataS.Text = "";
                        lblFromDataSmain.Text = "";
                        lblToDataS.Text = "";
                        lblToDataSMain.Text = "";
                        lblToDataAdmin.Text = "";
                        lblToS.Text = "";
                        lblFromS.Text = "";
                    }
                    if (RuleSelectId.ToString() == "2")
                    {
                        
                        lblselectionforS.Text = "";
                        lblFromS.Text = "From Date";
                        lblFromDataSmain.Text = dt.Rows[0]["FromDate"].ToString();

                        lblToS.Text = "To Date";
                        lblToDataSMain.Text = dt.Rows[0]["ToDate"].ToString();

                        lblFromDataS.Text = "";
                        lblToDataS.Text = "";

                    }
                    if (RuleSelectId.ToString() == "3")
                    {
                       
                        lblselectionforS.Text = "";
                        lblFromS.Text = "From Document ID";
                        lblFromDataSmain.Text = dt.Rows[0]["FromId"].ToString();
                        lblToS.Text = "To Documnet ID";
                        lblToDataSMain.Text = dt.Rows[0]["ToId"].ToString();
                        lblFromDataS.Text = "";
                        lblToDataS.Text = "";
                    }
                }
                if (rbtnDesignationOpt.SelectedValue.ToString() == "4")
                {
                    pnlAdmin.Visible = false;
                    pnlAll.Visible = false;
                    pnlSuper.Visible = false;
                    pnlDataEO.Visible = true;
                    if (RuleSelectId.ToString() == "1")
                    {
                        lblselectionforD.Text = "All";
                        lblfromD.Text = "";
                        lblfromDataD.Text = "";
                        lblfromDataDMain.Text = "";
                        lblToDataD.Text = "";
                        lblTodataDMain.Text = "";
                        lblToD.Text = "";
                    }
                    if (RuleSelectId.ToString() == "2")
                    {
                       
                        lblselectionforD.Text = "";
                        lblfromD.Text = "From Date";
                        lblfromDataDMain.Text = dt.Rows[0]["FromDate"].ToString();
                        lblToD.Text = "To Date";
                        lblTodataDMain.Text = dt.Rows[0]["ToDate"].ToString();
                        lblfromDataD.Text = "";
                        lblToDataD.Text = "";
                    }
                    if (RuleSelectId.ToString() == "3")
                    {
                       
                        lblselectionforD.Text = "";
                        lblfromD.Text = "From Document ID";
                        lblfromDataDMain.Text = dt.Rows[0]["FromId"].ToString();
                        lblToD.Text = "To Documnet ID";
                        lblTodataDMain.Text = dt.Rows[0]["ToId"].ToString();
                        lblfromDataD.Text = "";
                        lblToDataD.Text = "";
                    }
                }
            }
            else
            {
                pnlAdmin.Visible = false;
                pnlAll.Visible = false;
                pnlSuper.Visible = false;
                pnlDataEO.Visible = false;
            }
        }
       
    }

    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        FillGridAll();
        //FillGrid();
       
    }
}
