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
using System.Data;
using System.Data.SqlClient;

public partial class ProjectInvoice : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            ViewState["data"] = "";
            ViewState["sortOrder"] = "";
            Pagecontrol.dypcontrol(Page, page);
            ddlcash.Visible = false;
            lblcashtax.Visible = false;
            fillstore();
            if (Request.QueryString["Tid"] != null)
            {

                ViewState["TID"] = ClsEncDesc.Decrypted(Request.QueryString["Tid"].Replace("@","+"));
                DataTable dtorder = select("Select Distinct StatusId,JobMaster.PartyId, JobMaster.Id as jid,InvoiceAccount,InvoiceType, JobReferenceNo,JobName, Compname,JobMaster.compid,Temp_ProjectInvoiceType.Whid from [TranctionMaster_Temp] inner join Temp_ProjectInvoiceType on Temp_ProjectInvoiceType.TransactionId=TranctionMaster_Temp.Tranction_Master_Temp_Id inner join JobMaster on JobMaster.Id=Temp_ProjectInvoiceType.JobId inner join Party_master on Party_master.PartyID=JobMaster.PartyId where Tranction_Master_Temp_Id= '" + ViewState["TID"].ToString() + "'");
                if (dtorder.Rows.Count > 0)
                {
                    ddlwarehouse.SelectedValue = Convert.ToString(dtorder.Rows[0]["Whid"]);
                    //ddlwarehouse_SelectedIndexChanged(sender, e);
                    fillparty();
                    fillstatus();
                    ddlclient.SelectedValue = Convert.ToString(dtorder.Rows[0]["PartyId"]);
                    ddlinvtype.SelectedValue =  Convert.ToString(dtorder.Rows[0]["InvoiceType"]);
                    if (Convert.ToString(dtorder.Rows[0]["InvoiceType"]) == "2")
                    {
                         
                    }
                    else
                    {
                        ddlcash.Visible = true;
                        lblcashtax.Visible = true;
                        fillCashAccount();
                        ddlcash.SelectedValue = Convert.ToString(dtorder.Rows[0]["InvoiceAccount"]); ;
                    }
                    ddlStatus.SelectedValue = Convert.ToString(dtorder.Rows[0]["StatusId"]);
                    filljob();
                    ddlproject.SelectedValue = Convert.ToString(dtorder.Rows[0]["jid"]);
                   
                   DataTable dtsub=select("Select * from Temp_FGMasterTbl where TranasId='"+ViewState["TID"]+"'");
                   if (dtsub.Rows.Count > 0)
                   {
                       chkworder.Checked = true;
                       chkworder_CheckedChanged(sender, e);
                       fillsub();
                       ddlcatesub.SelectedIndex = ddlcatesub.Items.IndexOf(ddlcatesub.Items.FindByValue(dtsub.Rows[0]["SubSubId"].ToString()));
                       ddlcatesub_SelectedIndexChanged(sender, e);
                       ddlinvname.SelectedIndex = ddlinvname.Items.IndexOf(ddlinvname.Items.FindByValue(dtsub.Rows[0]["InvWMasterId"].ToString()));
                       lblunits.Text = Convert.ToString(dtsub.Rows[0]["Qtyonhand"]);
                   }
                   else
                   {
                       chkworder.Checked = false;
                       chkworder_CheckedChanged(sender, e);
                   }
                    fillCategory();
                    fillservices();
                    fillPromaterial();
                    fillOthercharges();
                    fillgridlabour();
                    FillTotalInvamt();
                   
                }
            }
            else
            {
                fillservices();
                fillCategory();
                fillparty();
                fillstatus();
                filljob();
                fillPromaterial();
                fillOthercharges();
                fillgridlabour();
               
            }

        }
    }
    protected void reffilldata()
    {
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlTransaction transaction = con.BeginTransaction();
        try
        {
            string strdelePurD = " delete TranctionMaster_Temp where Tranction_Master_Temp_Id = '" + Convert.ToString(ViewState["TID"]) + "' ";
            string strdelePurM = " delete Temp_ProjectInvoiceType where TransactionId='" + Convert.ToString(ViewState["TID"]) + "' ";
            string stsaleothe = " delete Temp_salesothechargestbillbl where TranId = '" + Convert.ToString(ViewState["TID"]) + "' ";
            string materi = " delete Temp_ProductMaterialBillTbl where TransactionId='" + Convert.ToString(ViewState["TID"]) + "' ";
            string labour = " delete Temp_LabourMaterialBillTbl where TransactionId = '" + Convert.ToString(ViewState["TID"]) + "' ";
            string invFG = " delete Temp_FGMasterTbl where TranasId = '" + Convert.ToString(ViewState["TID"]) + "' ";

            SqlCommand cmddeltrans = new SqlCommand(strdelePurD, con);
            cmddeltrans.CommandType = CommandType.Text;
            cmddeltrans.Transaction = transaction;
            cmddeltrans.ExecuteNonQuery();
            SqlCommand cmddelpinv = new SqlCommand(strdelePurM, con);
            cmddelpinv.CommandType = CommandType.Text;
            cmddelpinv.Transaction = transaction;
            cmddelpinv.ExecuteNonQuery();
            SqlCommand cmdother = new SqlCommand(stsaleothe, con);
            cmdother.CommandType = CommandType.Text;
            cmdother.Transaction = transaction;
            cmdother.ExecuteNonQuery();
            SqlCommand cmdmatr = new SqlCommand(materi, con);
            cmdmatr.CommandType = CommandType.Text;
            cmdmatr.Transaction = transaction;
            cmdmatr.ExecuteNonQuery();
            SqlCommand cmdlabou = new SqlCommand(labour, con);
            cmdlabou.CommandType = CommandType.Text;
            cmdlabou.Transaction = transaction;
            cmdlabou.ExecuteNonQuery();

            SqlCommand cminFG = new SqlCommand(invFG, con);
            cminFG.CommandType = CommandType.Text;
            cminFG.Transaction = transaction;
            cminFG.ExecuteNonQuery();

            transaction.Commit();
        }

        catch (Exception ex)
        {
            transaction.Rollback();
          
        }
        finally
        {
            con.Close();
        }
    }
    public void fillCashAccount()
    {

        SqlCommand cmd = new SqlCommand("SELECT AccountName, AccountId, GroupId FROM  AccountMaster   WHERE  GroupId=1 and compid='" + Session["comid"] + "' and Whid='" + ddlwarehouse.SelectedValue + "' order by AccountName asc", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        ddlcash.DataSource = ds;
        ddlcash.DataTextField = "AccountName";
        ddlcash.DataValueField = "AccountId";
        ddlcash.DataBind();
    }
    protected void fillstatus()
    {

        string str = " select * from StatusMaster where StatusCategoryMasterId='165' order by StatusName";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlStatus.DataSource = ds;
        ddlStatus.DataTextField = "StatusName";
        ddlStatus.DataValueField = "StatusId";
        ddlStatus.DataBind();
        ddlStatus.Items.Insert(0, "All");
        ddlStatus.Items[0].Value = "0";

    }
    protected void fillstore()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlwarehouse.DataSource = ds;
        ddlwarehouse.DataTextField = "Name";
        ddlwarehouse.DataValueField = "WareHouseId";
        ddlwarehouse.DataBind();
        //ddlstore.Items.Insert(0, "Select");

        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["data"] = null;
        GridView3.DataSource = null;
        GridView3.DataBind();
        fillparty();
        //filljob();
        //fillPromaterial();
        //fillgridlabour();
        //fillOthercharges();
        FillTotalInvamt();
        fillCategory();
        fillservices();



       
    }
    protected void fillCategory()
    {
        DataTable ds =select("SELECT distinct    left(InventoryCategoryMaster.InventoryCatName,8) + '-' + left(InventorySubCategoryMaster.InventorySubCatName,8) + '-' + left(InventoruSubSubCategory.InventorySubSubName,8) " +
                       " AS category, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubSubId " +
                        " FROM         InventorySubCategoryMaster INNER JOIN " +
                      " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId INNER JOIN " +
                      " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where InventoryCategoryMaster.compid= '" + Session["Comid"] + "' " +
                      " and InventoryWarehouseMasterTbl.WareHouseId='" + ddlwarehouse.SelectedValue + "' and InventoryMaster.CatType='0'");
        if (ds.Rows.Count > 0)
        {
            ddlCategory.DataSource = ds;
            ddlCategory.DataTextField = "category";
            ddlCategory.DataValueField = "InventorySubSubId";
            ddlCategory.DataBind();
          
        }
    }
    protected void fillservices()
    {
        DataTable dinv = select("SELECT distinct    InventoryMaster.Name AS category, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                       " FROM         InventorySubCategoryMaster INNER JOIN " +
                     " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId INNER JOIN " +
                     " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where  InventoryMaster.InventorySubSubId = '" + ddlCategory.SelectedValue + "' and "+
                     "  InventoryWarehouseMasterTbl.WareHouseId='" + ddlwarehouse.SelectedValue + "' and InventoryMaster.CatType='0'");
        ddlservices.DataSource = dinv;
        ddlservices.DataTextField = "category";
        ddlservices.DataValueField = "InventoryWarehouseMasterId";
        ddlservices.DataBind();
        EventArgs e=new EventArgs();
        object sender=new object();
        ddlservices_SelectedIndexChanged(sender, e);
    }
    protected void fillparty()
    {
        ddlclient.Items.Clear();
        string party = "";
        party = "Select Party_master.PartyId,PartytTypeMaster.PartType+':'+Party_master.Compname as Partyname from Party_master inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId";

        party = party + " Where Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and PartytTypeMaster.PartType='Customer' order by Partyname";


        SqlDataAdapter adp = new SqlDataAdapter(party, con);
        DataTable dts = new DataTable();
        adp.Fill(dts);
        if (dts.Rows.Count > 0)
        {
            ddlclient.DataSource = dts;
            ddlclient.DataTextField = "Partyname";
            ddlclient.DataValueField = "PartyId";
            ddlclient.DataBind();

        }

        EventArgs e = new EventArgs();
        object sender = new object();
        ddlclient_SelectedIndexChanged(sender, e);


    }
    protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
    {
        filljob();
        fillPromaterial();
        fillgridlabour();
        ViewState["data"] = "";
        fillOthercharges();
        FillTotalInvamt();
    }
    protected void filljob()
    {
        string str = "";

        str = "select JobMaster.Id,JobName from JobMaster  where JobMaster.Whid='" + ddlwarehouse.SelectedValue + "' and PartyId='" + ddlclient.SelectedValue + "'  order by JobMaster.JobName ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlproject.DataSource = ds;
        ddlproject.DataTextField = "JobName";
        ddlproject.DataValueField = "Id";
        ddlproject.DataBind();
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string te = "Materialissueform.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ddlproject_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (chkworder.Checked == true)
        {
            
            fillinv();
        }
        fillPromaterial();
        fillgridlabour();
        ViewState["data"] = "";
        fillOthercharges();
     
        FillTotalInvamt();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillPromaterial();

    }
    protected void fillPromaterial()
    {
        string pera = "";
        if (ddlStatus.SelectedIndex > 0)
        {
            pera += " and JobMaster.StatusId='" + ddlStatus.SelectedValue + "'";
        }
        if (ddlproject.SelectedIndex != -1)
        {
            pera += " and MaterialIssueMasterTbl.JobMasterId='" + ddlproject.SelectedValue + "'";
        }
        if (ddlclient.SelectedIndex != -1)
        {
            pera += " and JobMaster.PartyId='" + ddlclient.SelectedValue + "'";
        }
        if (DropDownList1.SelectedValue == "2")
        {
            pera += " and (ProductMaterialBillTbl.Id='' or ProductMaterialBillTbl.Id IS NULL)";
        }
        //if (DropDownList1.SelectedValue == "1")
        //{
        //    pera += " and ProductMaterialBillTbl.Id<>'' and ProductMaterialBillTbl.Id IS NOT NULL";
        //}
        DataTable dt;
        dt = select("SELECT Distinct  MaterialIssueDetail.Id as Mid,ProductMaterialBillTbl.Id as pcid, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId , LEFT(InventoryCategoryMaster.InventoryCatName, 15) as InventoryCatName, LEFT(InventorySubCategoryMaster.InventorySubCatName, 15) as InventorySubCatName, " +
              "  case when(ProductMaterialBillTbl.Id IS NULL) then 'Unbilled Material' else 'Already billed' end bilst,  LEFT(InventoruSubSubCategory.InventorySubSubName, 15)as InventorySubSubName, InventoryMaster.Name AS Name,case when(ProductMaterialBillTbl.SalesRate IS NULL) then InventoryWarehouseMasterTbl.Rate else ProductMaterialBillTbl.SalesRate end  as SalesRate, MaterialIssueDetail.Rate, MaterialIssueDetail.Qty,  InventoryMaster.InventoryMasterId " +
           "  FROM     InventoryCategoryMaster Inner join " +
            "      InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId inner JOIN " +
            "      InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner JOIN " +
            "      InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId inner join InventoryWarehouseMasterTbl  on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join MaterialIssueDetail on MaterialIssueDetail.InvWMasterId =InventoryWarehouseMasterTbl.InventoryWarehouseMasterId Left join ProductMaterialBillTbl on ProductMaterialBillTbl.MaterialIssueDetailid=MaterialIssueDetail.Id inner join MaterialIssueMasterTbl on MaterialIssueMasterTbl.Id=MaterialIssueDetail.MaterialMasterId inner join JobMaster on JobMaster.Id= MaterialIssueMasterTbl.JobMasterId  " +
          " WHERE  (InventoryWarehouseMasterTbl.WareHouseId='" + ddlwarehouse.SelectedValue + "') and (MaterialIssueMasterTbl.Whid='" + ddlwarehouse.SelectedValue + "')   and  (InventoryMaster.MasterActiveStatus = 1) " + pera +
          "   order by MaterialIssueDetail.Id Desc");

        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }


        GridView1.DataSource = myDataView;

        GridView1.DataBind();
        decimal totcost = 0;
        decimal totalcosttobe = 0;
        if (GridView1.Rows.Count > 0)
        {
            foreach (GridViewRow item in GridView1.Rows)
            {
                Label lblunit = (Label)item.FindControl("lblunit");
                Label lblcost = (Label)item.FindControl("lblcost");
                TextBox lblsalesrate = (TextBox)item.FindControl("lblsalesrate");
                Label lblcharges = (Label)item.FindControl("lblcharges");
                Label lblpcid = (Label)item.FindControl("lblpcid");
                Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
                Label lblmargin = (Label)item.FindControl("lblmargin");
                Label lblmarginper = (Label)item.FindControl("lblmarginper");
                CheckBox chkc = (CheckBox)item.FindControl("chkc");
                lblcharges.Text = Math.Round(Convert.ToDecimal(lblunit.Text) * Convert.ToDecimal(lblcost.Text), 2).ToString();
                lblchargestobe.Text = Math.Round(Convert.ToDecimal(lblunit.Text) * Convert.ToDecimal(lblsalesrate.Text), 2).ToString();

                lblmargin.Text = Math.Round(Convert.ToDecimal(lblchargestobe.Text) - Convert.ToDecimal(lblcharges.Text), 2).ToString();
              
               decimal mper = 0;
                if (Convert.ToDecimal(lblchargestobe.Text) > 0)
                {
                     mper = Convert.ToDecimal(lblmargin.Text) * 100 / Convert.ToDecimal(lblchargestobe.Text);
                }
                else
                {
                    mper = Convert.ToDecimal(lblmargin.Text) * 100;
                }          
                //decimal mper = Convert.ToDecimal(lblmargin.Text) * Convert.ToDecimal(lblsalesrate.Text) / 100;
                lblmarginper.Text = Convert.ToString(Math.Round(mper, 2));

                if (Convert.ToString(lblpcid.Text) == "")
                {
                    chkc.Enabled = true;
                    lblsalesrate.Enabled = true;
                    totcost += Convert.ToDecimal(lblcharges.Text);
                    totalcosttobe += Convert.ToDecimal(lblchargestobe.Text);
                    chkc.Checked = true;
                }
                else
                {
                    lblsalesrate.Enabled = false;
                    chkc.Enabled = false;
                }
            }
            GridViewRow ft = (GridViewRow)GridView1.FooterRow;
            Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
            lbltotalfooter.Text = totcost.ToString();
            lbltotaltobefooter.Text = totalcosttobe.ToString();
        }
    }
    protected DataTable select(string str)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        return dt;
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillPromaterial();
        fillgridlabour();
      
        FillTotalInvamt();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgridlabour();
    }
    protected void fillgridlabour()
    {

        string trc = "";

        if (ddlclient.SelectedIndex > 0)
        {

            trc += " and JobMaster.PartyId='" + ddlclient.SelectedValue + "'";
        }



        if (ddlproject.SelectedIndex > 0)
        {

            trc += " and JobEmployeeDailyTaskDetail.JobMasterId='" + ddlproject.SelectedValue + "'";
        }


        if (DropDownList2.SelectedValue == "2")
        {
            trc += " and (LabourMaterialBillTbl.Id='' or LabourMaterialBillTbl.Id IS NULL)";
        }
        string str = "select   distinct JobEmployeeDailyTaskTbl.EmployeeId, case when(LabourMaterialBillTbl.Id IS NULL) then 'Unbilled Labour' else 'Already billed' end bilst, LabourMaterialBillTbl.Id as labourId, Case when(SalesRate='' or Rate IS NULL) then '0' else SalesRate end as  SalesRate, EmployeeMaster.EmployeeName as Employee, Case when(Cost='') then '0' else Cost end as  Cost,Case when(Rate='' or Rate IS NULL) then '0' else Rate end as  Rate, FromToTime,Hrs,JobEmployeeDailyTaskTbl.EmployeeId, Convert(nvarchar, JobEmployeeDailyTaskTbl.Enddate,101) as Enddate, Convert(nvarchar, JobEmployeeDailyTaskTbl.Date,101) as Date,JobEmployeeDailyTaskDetail.FromTime, JobEmployeeDailyTaskDetail.Id,JobName,Party_Master.Compname from Party_Master inner join JobMaster on JobMaster.PartyId=Party_Master.PartyId inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id Left join LabourMaterialBillTbl on LabourMaterialBillTbl.JobEmpDailyTaskDetailId=JobEmployeeDailyTaskDetail.Id inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=JobEmployeeDailyTaskTbl.EmployeeId  where JobEmployeeDailyTaskTbl.Whid='" + ddlwarehouse.SelectedValue + "' " + trc + "   and (FromToTime IS NOT NULL and FromToTime<>'') order by JobEmployeeDailyTaskDetail.Id Desc";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);


        DataView myDataView = new DataView();
        myDataView = ds.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView2.DataSource = ds;

        GridView2.DataSource = myDataView;

        GridView2.DataBind();

        Decimal totalcosttobe = 0;
        Decimal totcost = 0;

        if (GridView2.Rows.Count > 0)
        {
            DataTable dinv = select("SELECT distinct   InventoryMaster.Name AS category,InventoryWarehouseMasterTbl.Rate, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                        " FROM         InventorySubCategoryMaster INNER JOIN " +
                      " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId INNER JOIN " +
                      " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where InventoryCategoryMaster.compid= '" + Session["Comid"] + "' " +
                      " and InventoryWarehouseMasterTbl.WareHouseId='" + ddlwarehouse.SelectedValue + "' and InventoryMaster.CatType='0'");

            foreach (GridViewRow item in GridView2.Rows)
            {
                if (dinv.Rows.Count > 0)
                {
                    string id = GridView2.DataKeys[item.RowIndex].Value.ToString();
                    Label lblhor = (Label)item.FindControl("lblhor");
                    Label lblempid = (Label)item.FindControl("lblempid");

                    Label lblemprare = (Label)item.FindControl("lblemprare");
                    TextBox lblsalesrate = (TextBox)item.FindControl("lblsalesrate");
                    Label lblsalesrateor = (Label)item.FindControl("lblsalesrateor");
                    Label lblcharges = (Label)item.FindControl("lblcharges");
                    Label labourId = (Label)item.FindControl("labourId");
                    Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
                    Label lblmargin = (Label)item.FindControl("lblmargin");
                    Label lblmarginper = (Label)item.FindControl("lblmarginper");
                    CheckBox chkc = (CheckBox)item.FindControl("chkc");
                    DropDownList ddlservice = (DropDownList)item.FindControl("ddlservice");
                    ddlservice.DataSource = dinv;
                    ddlservice.DataTextField = "category";
                    ddlservice.DataValueField = "InventoryWarehouseMasterId";
                    ddlservice.DataBind();
                    if (Convert.ToString(labourId.Text) == "")
                    {
                        lblsalesrate.Text = Convert.ToString(dinv.Rows[0]["Rate"]);
                        lblsalesrateor.Text = Convert.ToString(dinv.Rows[0]["Rate"]);
                    }

                    //decimal salerate = 0;
                    //if (Convert.ToString(labourId.Text) == "")
                    //{
                    //    DataTable df;
                    //    df = select("select distinct LabourMaterialBillTbl.SalesRate, LabourMaterialBillTbl.Id  from Party_Master inner join JobMaster on JobMaster.PartyId=Party_Master.PartyId inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id Inner join LabourMaterialBillTbl on LabourMaterialBillTbl.JobEmpDailyTaskDetailId=JobEmployeeDailyTaskDetail.Id inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId where JobEmployeeDailyTaskTbl.Whid='" + ddlwarehouse.SelectedValue + "' and JobMaster.PartyId='" + ddlclient.SelectedValue + "' and JobEmployeeDailyTaskTbl.EmployeeId='" + lblempid.Text + "' order by LabourMaterialBillTbl.Id Desc");
                    //    if (df.Rows.Count > 0)
                    //    {
                    //        salerate = Convert.ToDecimal(df.Rows[0]["SalesRate"]);
                    //    }
                    //    else if (df.Rows.Count < 0)
                    //    {
                    //        df = select("select distinct LabourMaterialBillTbl.SalesRate, LabourMaterialBillTbl.Id  from Party_Master inner join JobMaster on JobMaster.PartyId=Party_Master.PartyId inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id Inner join LabourMaterialBillTbl on LabourMaterialBillTbl.JobEmpDailyTaskDetailId=JobEmployeeDailyTaskDetail.Id inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId where JobEmployeeDailyTaskTbl.Whid='" + ddlwarehouse.SelectedValue + "'  and JobEmployeeDailyTaskTbl.EmployeeId='" + lblempid.Text + "' order by LabourMaterialBillTbl.Id Desc");
                    //        if (df.Rows.Count > 0)
                    //        {
                    //            salerate = Convert.ToDecimal(df.Rows[0]["SalesRate"]);
                    //        }
                    //    }
                    //    else if (df.Rows.Count < 0)
                    //    {
                    //        df = select("select distinct LabourMaterialBillTbl.SalesRate, LabourMaterialBillTbl.Id  from Party_Master inner join JobMaster on JobMaster.PartyId=Party_Master.PartyId inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id Inner join LabourMaterialBillTbl on LabourMaterialBillTbl.JobEmpDailyTaskDetailId=JobEmployeeDailyTaskDetail.Id inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId where JobEmployeeDailyTaskTbl.Whid='" + ddlwarehouse.SelectedValue + "'  and JobMaster.PartyId='" + ddlclient.SelectedValue + "' order by LabourMaterialBillTbl.Id Desc");
                    //        if (df.Rows.Count > 0)
                    //        {
                    //            salerate = Convert.ToDecimal(df.Rows[0]["SalesRate"]);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        salerate = Convert.ToDecimal(lblemprare.Text);
                    //    }

                    //}
                    //lblsalesrate.Text = salerate.ToString();
                    string[] separbm = new string[] { ":" };
                    string[] strSplitArrbm = lblhor.Text.Split(separbm, StringSplitOptions.RemoveEmptyEntries);


                    decimal bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));



                    lblchargestobe.Text = Math.Round(bhour * Convert.ToDecimal(lblsalesrate.Text), 2).ToString();

                    lblmargin.Text = Math.Round(Convert.ToDecimal(lblchargestobe.Text) - Convert.ToDecimal(lblcharges.Text), 2).ToString();
                    //decimal mper = Convert.ToDecimal(lblmargin.Text) * Convert.ToDecimal(lblsalesrate.Text) / 100;
                    decimal mper = 0;
                    if (Convert.ToDecimal(lblchargestobe.Text) > 0)
                    {
                        mper = Convert.ToDecimal(lblmargin.Text) * 100 / Convert.ToDecimal(lblchargestobe.Text);
                    }
                    else
                    {
                        mper = Convert.ToDecimal(lblmargin.Text) * 100;
                    }
                    lblmarginper.Text = Convert.ToString(Math.Round(mper, 2));

                    if (Convert.ToString(labourId.Text) == "")
                    {
                        chkc.Enabled = true;
                        lblsalesrate.Enabled = true;
                        totcost += Convert.ToDecimal(lblcharges.Text);
                        totalcosttobe += Convert.ToDecimal(lblchargestobe.Text);
                        chkc.Checked = true;
                    }
                    else
                    {
                        lblsalesrate.Enabled = false;
                        chkc.Enabled = false;
                    }
                }
            }
            GridViewRow ft = (GridViewRow)GridView2.FooterRow;
            Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
            lbltotalfooter.Text = totcost.ToString();
            lbltotaltobefooter.Text = totalcosttobe.ToString();
        }
        FillTotalInvamt();
    }
    protected void ddlitem123_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;

        Decimal totalcosttobe = 0;
        Decimal totcost = 0;
        DropDownList ddlservice = (DropDownList)GridView2.Rows[rinrow].FindControl("ddlservice");
        TextBox lblsalesratea = (TextBox)GridView2.Rows[rinrow].FindControl("lblsalesrate");
        Label lblsalesrateor = (Label)GridView2.Rows[rinrow].FindControl("lblsalesrateor");
        DataTable dinv = select("SELECT distinct InventoryWarehouseMasterTbl.Rate from InventoryWarehouseMasterTbl where InventoryWarehouseMasterId='" + ddlservice.SelectedValue + "' ");
        if (dinv.Rows.Count > 0)
        {
            lblsalesratea.Text = Convert.ToString(dinv.Rows[0]["Rate"]);
            lblsalesrateor.Text = Convert.ToString(dinv.Rows[0]["Rate"]);

        }
        foreach (GridViewRow item in GridView2.Rows)
        {
            string id = GridView2.DataKeys[item.RowIndex].Value.ToString();
            Label lblhor = (Label)item.FindControl("lblhor");
            Label lblempid = (Label)item.FindControl("lblempid");
         
            Label lblemprare = (Label)item.FindControl("lblemprare");
            TextBox lblsalesrate = (TextBox)item.FindControl("lblsalesrate");
            Label lblcharges = (Label)item.FindControl("lblcharges");
            Label labourId = (Label)item.FindControl("labourId");
            Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
            Label lblmargin = (Label)item.FindControl("lblmargin");
            Label lblmarginper = (Label)item.FindControl("lblmarginper");
            CheckBox chkc = (CheckBox)item.FindControl("chkc");
        
         
            
            
            
            string[] separbm = new string[] { ":" };
            string[] strSplitArrbm = lblhor.Text.Split(separbm, StringSplitOptions.RemoveEmptyEntries);


            decimal bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));


           
            if (lblsalesrate.Text == "")
            {
                lblsalesrate.Text = "0";
            }
            lblchargestobe.Text = Math.Round(bhour * Convert.ToDecimal(lblsalesrate.Text), 2).ToString();

            lblmargin.Text = Math.Round(Convert.ToDecimal(lblchargestobe.Text) - Convert.ToDecimal(lblcharges.Text), 2).ToString();
            //decimal mper = Convert.ToDecimal(lblmargin.Text) * Convert.ToDecimal(lblsalesrate.Text) / 100;
          decimal mper = 0;
                if (Convert.ToDecimal(lblchargestobe.Text) > 0)
                {
                     mper = Convert.ToDecimal(lblmargin.Text) * 100 / Convert.ToDecimal(lblchargestobe.Text);
                }
                else
                {
                    mper = Convert.ToDecimal(lblmargin.Text) * 100;
                }
            lblmarginper.Text = Convert.ToString(Math.Round(mper, 2));

            if (Convert.ToString(labourId.Text) == "")
            {
                chkc.Enabled = true;
                lblsalesrate.Enabled = true;
                if (chkc.Checked == true)
                {
                    totcost += Convert.ToDecimal(lblcharges.Text);
                    totalcosttobe += Convert.ToDecimal(lblchargestobe.Text);
                }
            }
            else
            {
                lblsalesrate.Enabled = false;
                chkc.Enabled = false;
            }
        }
        GridViewRow ft = (GridViewRow)GridView2.FooterRow;
        Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
        Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
        lbltotalfooter.Text = totcost.ToString();
        lbltotaltobefooter.Text = totalcosttobe.ToString();
        FillTotalInvamt();
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        fillOthercharges();
        txtnote.Text = "";
        txtqty.Text = "";
        txtrate.Text = "";
       
    }

    
    protected void fillOthercharges()
    {
        DataTable dtTemp = new DataTable();
        if (Convert.ToString(ViewState["data"]) == "")
        {
            dtTemp = createOthechargestbl();
            DataTable drt = new DataTable();
            int kk = 0;

            DataTable drt1 = select("select distinct salesothechargestbillbl.*,OriginalRate as Rateor   from salesothechargestbillbl where jobid='" + ddlproject.SelectedValue + "' order by Id");
          
            if (drt1.Rows.Count > 0)
            {
                 foreach (DataRow item in drt1.Rows)
                {
                  
                    DataRow dtr2 = dtTemp.NewRow();
                    dtr2["Id"] = item["Id"];
                    dtr2["Text"] = item["Note"];
                    dtr2["Qty"] = item["Qty"];
                    dtr2["Rate"] = item["Qty"];
                    dtr2["Rateor"] = item["Rateor"];
                    dtr2["Total"] = item["Total"];
                  
                    dtr2["chk"] = Convert.ToBoolean(0);
                   
                    dtr2["Service"] = item["InvwId"];

                    DataTable dinv = select("SELECT distinct InventoryMaster.Name AS category, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                       " FROM         InventorySubCategoryMaster INNER JOIN " +
                     " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId INNER JOIN " +
                     " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where  " +
                     "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId='" + item["InvwId"] + "'");
                    if (dinv.Rows.Count > 0)
                    {
                        dtr2["Service"] = Convert.ToString(dinv.Rows[0]["category"]);
                        dtr2["ServiceId"] = Convert.ToString(dinv.Rows[0]["InventoryWarehouseMasterId"]);
                        dtTemp.Rows.Add(dtr2);
                    }
                 }
            }
            if (Request.QueryString["Tid"] != null)
            {
                drt = select("select distinct Case when(Id IS NOT NULL) then '' else Id end as Id,Note,Qty,Rate,Total,OriginalRate as Rateor,InvwId from Temp_salesothechargestbillbl where TranId='" + ViewState["TID"] + "' and  jobid='" + ddlproject.SelectedValue + "' order by Id");
                foreach (DataRow item in drt.Rows)
                {
                    kk = 1;
                    DataRow dtr2 = dtTemp.NewRow();
                    dtr2["Id"] = item["Id"];
                    dtr2["Text"] = item["Note"];
                    dtr2["Qty"] = item["Qty"];
                    dtr2["Rate"] = item["Qty"];
                    dtr2["Rateor"] = item["Rateor"];
                    dtr2["Total"] = item["Total"];
                    if (kk == 0)
                    {
                        dtr2["chk"] = Convert.ToBoolean(0);
                    }
                    else
                    {
                        dtr2["chk"] = Convert.ToBoolean(1);
                    }
                    dtr2["Service"] = item["InvwId"];

                    DataTable dinv = select("SELECT distinct InventoryMaster.Name AS category, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                       " FROM         InventorySubCategoryMaster INNER JOIN " +
                     " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId INNER JOIN " +
                     " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where  " +
                     "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId='" + item["InvwId"] + "'");

                    dtr2["Service"] = Convert.ToString(dinv.Rows[0]["category"]);
                    dtr2["ServiceId"] = Convert.ToString(dinv.Rows[0]["InventoryWarehouseMasterId"]);
                    dtTemp.Rows.Add(dtr2);
                }


            }

            ViewState["data"] = dtTemp;
            GridView3.DataSource = dtTemp;
            GridView3.DataBind();
           
        }
        else
        {
            dtTemp = (DataTable)ViewState["data"];
        }
        if (panel3.Visible == true)
        {
            DataRow dtr = dtTemp.NewRow();
            if (panel3.Visible == true)
            {
                if (txtqty.Text != "" && txtrate.Text != "")
                {
                    dtr["Id"] = "";
                    dtr["Text"] = txtnote.Text;
                    dtr["Qty"] = txtqty.Text;
                    dtr["Rate"] = txtrate.Text;
                    dtr["Rateor"] = lbllistsalrate.Text;
                    dtr["Total"] = Convert.ToDecimal(txtqty.Text) * Convert.ToDecimal(txtrate.Text);
                    dtr["chk"] = Convert.ToBoolean(1);
                    dtr["Service"] = ddlservices.SelectedItem.Text;
                    dtr["ServiceId"] = ddlservices.SelectedValue;
                    dtTemp.Rows.Add(dtr);
                }
            }
            
            ViewState["data"] = dtTemp;
            GridView3.DataSource = dtTemp;
            GridView3.DataBind();
        }
        //else if (Request.QueryString["Tid"] != null)
        //{
        //    DataTable df;



        //    df = select("select distinct *  from salesothechargestbillbl where TranId='" +  ViewState["TID"] + "' order by Id");
        //    if (df.Rows.Count > 0)
        //    {
        //        GridView3.DataSource = df;
        //        GridView3.DataBind();
        //    }
        //}
        Decimal totf0 = 0;
        if (GridView3.Rows.Count > 0)
        {
            foreach (GridViewRow item in GridView3.Rows)
            {
                CheckBox chkc = (CheckBox)item.FindControl("chkc");
                Label lblgtotal = (Label)item.FindControl("lblgtotal");
                Label lblId = (Label)item.FindControl("lblId");
                if (Convert.ToString(lblId.Text) == "" || Convert.ToString(lblId.Text) == "0")
                { 
                    totf0 += Convert.ToDecimal(lblgtotal.Text);
                    chkc.Enabled = true;
                    chkc.Checked = true;
                }
                else
                {
                    chkc.Enabled = false;
                }
            }

            GridViewRow ft = (GridViewRow)GridView3.FooterRow;
            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
            lbltotaltobefooter.Text = totf0.ToString();
        }
        FillTotalInvamt();

    }
    public DataTable createOthechargestbl()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Id";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd11 = new DataColumn();
        prd11.ColumnName = "Text";
        prd11.DataType = System.Type.GetType("System.String");
        prd11.AllowDBNull = true;
        dtTemp.Columns.Add(prd11);

        DataColumn prd111 = new DataColumn();
        prd111.ColumnName = "Qty";
        prd111.DataType = System.Type.GetType("System.String");
        prd111.AllowDBNull = true;
        dtTemp.Columns.Add(prd111);

        DataColumn prd111r = new DataColumn();
        prd111r.ColumnName = "chk";
        prd111r.DataType = System.Type.GetType("System.Boolean");
        prd111r.AllowDBNull = true;
        dtTemp.Columns.Add(prd111r);



        DataColumn prd111rs = new DataColumn();
        prd111rs.ColumnName = "Service";
        prd111rs.DataType = System.Type.GetType("System.String");
        prd111rs.AllowDBNull = true;
        dtTemp.Columns.Add(prd111rs);

        DataColumn prd111rsi = new DataColumn();
        prd111rsi.ColumnName = "ServiceId";
        prd111rsi.DataType = System.Type.GetType("System.String");
        prd111rsi.AllowDBNull = true;
        dtTemp.Columns.Add(prd111rsi);


        DataColumn prd111t = new DataColumn();
        prd111t.ColumnName = "Total";
        prd111t.DataType = System.Type.GetType("System.String");
        prd111t.AllowDBNull = true;
        dtTemp.Columns.Add(prd111t);
        DataColumn prd111ta = new DataColumn();
        prd111ta.ColumnName = "Rate";
        prd111ta.DataType = System.Type.GetType("System.String");
        prd111ta.AllowDBNull = true;
        dtTemp.Columns.Add(prd111ta);
        DataColumn prd111taz = new DataColumn();
        prd111taz.ColumnName = "Rateor";
        prd111taz.DataType = System.Type.GetType("System.String");
        prd111taz.AllowDBNull = true;
        dtTemp.Columns.Add(prd111taz);
        return dtTemp;
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        panel3.Visible = true;
    }
    protected void Button7_Click(object sender, EventArgs e)
    {

        txtnote.Text = "";
        txtqty.Text = "";
        txtrate.Text = "";
      
        panel3.Visible = false;
    }
    protected void otherchk_chachedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        decimal totf0 = 0;
        foreach (GridViewRow item in GridView3.Rows)
        {
            CheckBox chkc = (CheckBox)item.FindControl("chkc");
            Label lblgtotal = (Label)item.FindControl("lblgtotal");
            Label lblId = (Label)item.FindControl("lblId");
            if (Convert.ToString(lblId.Text) == "" || Convert.ToString(lblId.Text) == "0")
            {
                if (chkc.Checked == true)
                {
                    totf0 += Convert.ToDecimal(lblgtotal.Text);
                }
                chkc.Enabled = true;
            }
            else
            {
                chkc.Enabled = false;
            }
        }
        GridViewRow ft = (GridViewRow)GridView3.FooterRow;
        Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
        lbltotaltobefooter.Text = totf0.ToString();
        FillTotalInvamt();
    }
    protected void otherlabour_chachedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        Decimal totalcosttobe = 0;
        Decimal totcost = 0;
        foreach (GridViewRow item in GridView2.Rows)
        {
            string id = GridView2.DataKeys[item.RowIndex].Value.ToString();
            Label lblhor = (Label)item.FindControl("lblhor");
            Label lblempid = (Label)item.FindControl("lblempid");

            Label lblemprare = (Label)item.FindControl("lblemprare");
            TextBox lblsalesrate = (TextBox)item.FindControl("lblsalesrate");
            Label lblcharges = (Label)item.FindControl("lblcharges");
            Label labourId = (Label)item.FindControl("labourId");
            Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
            Label lblmargin = (Label)item.FindControl("lblmargin");
            Label lblmarginper = (Label)item.FindControl("lblmarginper");
            CheckBox chkc = (CheckBox)item.FindControl("chkc");

            if (Convert.ToString(labourId.Text) == "")
            {
                chkc.Enabled = true;
                lblsalesrate.Enabled = true;
                if (chkc.Checked == true)
                {
                    totcost += Convert.ToDecimal(lblcharges.Text);
                    totalcosttobe += Convert.ToDecimal(lblchargestobe.Text);
                }
            }
            else
            {
                lblsalesrate.Enabled = false;
                chkc.Enabled = false;
            }
        }
        GridViewRow ft = (GridViewRow)GridView2.FooterRow;
        Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
        Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
        lbltotalfooter.Text = totcost.ToString();
        lbltotaltobefooter.Text = totalcosttobe.ToString();
        FillTotalInvamt();
    }
    protected void otherMaterial_chachedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        Decimal totalcosttobe = 0;
        Decimal totcost = 0;
        foreach (GridViewRow item in GridView1.Rows)
        {
            string id = GridView1.DataKeys[item.RowIndex].Value.ToString();



            Label lblcharges = (Label)item.FindControl("lblcharges");
            Label lblpcid = (Label)item.FindControl("lblpcid");
            Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
            Label lblmargin = (Label)item.FindControl("lblmargin");
            Label lblmarginper = (Label)item.FindControl("lblmarginper");
            CheckBox chkc = (CheckBox)item.FindControl("chkc");

            if (Convert.ToString(lblpcid.Text) == "")
            {
                if (chkc.Checked == true)
                {
                    totcost += Convert.ToDecimal(lblcharges.Text);
                    totalcosttobe += Convert.ToDecimal(lblchargestobe.Text);
                }
            }

        }

        GridViewRow ft = (GridViewRow)GridView1.FooterRow;
        Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
        Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
        lbltotalfooter.Text = totcost.ToString();
        lbltotaltobefooter.Text = totalcosttobe.ToString();
        FillTotalInvamt();
    }
    protected void txtlabour_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((TextBox)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        //string val = ((CheckBox)sender).Text;
        Decimal totalcosttobe = 0;
        Decimal totcost = 0;

        foreach (GridViewRow item in GridView2.Rows)
        {
            string id = GridView2.DataKeys[item.RowIndex].Value.ToString();
            Label lblhor = (Label)item.FindControl("lblhor");
            Label lblempid = (Label)item.FindControl("lblempid");
            //Label lblunit = (Label)item.FindControl("lblunit");
            //Label lblcost = (Label)item.FindControl("lblcost");
            Label lblemprare = (Label)item.FindControl("lblemprare");
            TextBox lblsalesrate = (TextBox)item.FindControl("lblsalesrate");
            Label lblcharges = (Label)item.FindControl("lblcharges");
            Label labourId = (Label)item.FindControl("labourId");
            Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
            Label lblmargin = (Label)item.FindControl("lblmargin");
            Label lblmarginper = (Label)item.FindControl("lblmarginper");
            CheckBox chkc = (CheckBox)item.FindControl("chkc");

            string[] separbm = new string[] { ":" };
            string[] strSplitArrbm = lblhor.Text.Split(separbm, StringSplitOptions.RemoveEmptyEntries);


            decimal bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));


            //lblcharges.Text = Math.Round(Convert.ToDecimal(lblsalesrate.Text) * bhour, 2).ToString();
            if (lblsalesrate.Text == "")
            {
                lblsalesrate.Text = "0";
            }
            lblchargestobe.Text = Math.Round(bhour * Convert.ToDecimal(lblsalesrate.Text), 2).ToString();

            lblmargin.Text = Math.Round(Convert.ToDecimal(lblchargestobe.Text) - Convert.ToDecimal(lblcharges.Text), 2).ToString();
            //decimal mper = Convert.ToDecimal(lblmargin.Text) * Convert.ToDecimal(lblsalesrate.Text) / 100;
          decimal mper = 0;
                if (Convert.ToDecimal(lblchargestobe.Text) > 0)
                {
                     mper = Convert.ToDecimal(lblmargin.Text) * 100 / Convert.ToDecimal(lblchargestobe.Text);
                }
                else
                {
                    mper = Convert.ToDecimal(lblmargin.Text) * 100;
                }            lblmarginper.Text = Convert.ToString(Math.Round(mper, 2));

            if (Convert.ToString(labourId.Text) == "")
            {
                chkc.Enabled = true;
                lblsalesrate.Enabled = true;
                if (chkc.Checked == true)
                {
                    totcost += Convert.ToDecimal(lblcharges.Text);
                    totalcosttobe += Convert.ToDecimal(lblchargestobe.Text);
                }
            }
            else
            {
                lblsalesrate.Enabled = false;
                chkc.Enabled = false;
            }
        }
        GridViewRow ft = (GridViewRow)GridView2.FooterRow;
        Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
        Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
        lbltotalfooter.Text = totcost.ToString();
        lbltotaltobefooter.Text = totalcosttobe.ToString();
        FillTotalInvamt();
    }
    protected void txtMaterial_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((TextBox)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        decimal totcost = 0;
        decimal totalcosttobe = 0;
        if (GridView1.Rows.Count > 0)
        {
            foreach (GridViewRow item in GridView1.Rows)
            {
                Label lblunit = (Label)item.FindControl("lblunit");
                Label lblcost = (Label)item.FindControl("lblcost");
                TextBox lblsalesrate = (TextBox)item.FindControl("lblsalesrate");
                Label lblcharges = (Label)item.FindControl("lblcharges");
                Label lblpcid = (Label)item.FindControl("lblpcid");
                Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
                Label lblmargin = (Label)item.FindControl("lblmargin");
                Label lblmarginper = (Label)item.FindControl("lblmarginper");
                CheckBox chkc = (CheckBox)item.FindControl("chkc");
                //lblcharges.Text = Math.Round(Convert.ToDecimal(lblunit.Text) * Convert.ToDecimal(lblcost.Text), 2).ToString();
                if (lblsalesrate.Text == "")
                {
                    lblsalesrate.Text = "0";
                }
                lblchargestobe.Text = Math.Round(Convert.ToDecimal(lblunit.Text) * Convert.ToDecimal(lblsalesrate.Text), 2).ToString();

                lblmargin.Text = Math.Round(Convert.ToDecimal(lblchargestobe.Text) - Convert.ToDecimal(lblcharges.Text), 2).ToString();
                //decimal mper = Convert.ToDecimal(lblmargin.Text) * Convert.ToDecimal(lblsalesrate.Text) / 100;
                decimal mper = 0;
                if (Convert.ToDecimal(lblchargestobe.Text) > 0)
                {
                     mper = Convert.ToDecimal(lblmargin.Text) * 100 / Convert.ToDecimal(lblchargestobe.Text);
                }
                else
                {
                    mper = Convert.ToDecimal(lblmargin.Text) * 100;
                }                lblmarginper.Text = Convert.ToString(Math.Round(mper, 2));

                if (Convert.ToString(lblpcid.Text) == "")
                {
                    chkc.Enabled = true;
                    lblsalesrate.Enabled = true;
                    if (chkc.Checked == true)
                    {
                        totcost += Convert.ToDecimal(lblcharges.Text);
                        totalcosttobe += Convert.ToDecimal(lblchargestobe.Text);
                    }
                }
                else
                {
                    lblsalesrate.Enabled = false;
                    chkc.Enabled = false;
                }
            }
            GridViewRow ft = (GridViewRow)GridView1.FooterRow;
            Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
            lbltotalfooter.Text = totcost.ToString();
            lbltotaltobefooter.Text = totalcosttobe.ToString();
            FillTotalInvamt();
        }

    }
    protected void FillTotalInvamt()
    {
        decimal g1 = 0;
        decimal g2 = 0;
        decimal g3 = 0;
        if (GridView1.Rows.Count > 0)
        {
            GridViewRow ft = (GridViewRow)GridView1.FooterRow;

            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
            g1 = Convert.ToDecimal(lbltotaltobefooter.Text);
        }
        if (GridView2.Rows.Count > 0)
        {
            GridViewRow ft = (GridViewRow)GridView2.FooterRow;

            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
            g2 = Convert.ToDecimal(lbltotaltobefooter.Text);
        }
        if (GridView3.Rows.Count > 0)
        {
            GridViewRow ft = (GridViewRow)GridView3.FooterRow;

            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
            g3 = Convert.ToDecimal(lbltotaltobefooter.Text);
        }
        lblTotalnetamt.Text = Convert.ToString(g1 + g2 + g3);
        fillviewmar();
    }
    protected void h1_chachedChanged(object sender, EventArgs e)
    {
        //GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        //int rinrow = row.RowIndex;
        Decimal totalcosttobe = 0;
        Decimal totcost = 0;
        foreach (GridViewRow item in GridView1.Rows)
        {
            CheckBox chkc = (CheckBox)item.FindControl("chkc");
            string id = GridView1.DataKeys[item.RowIndex].Value.ToString();
            Label lblcharges = (Label)item.FindControl("lblcharges");
            Label lblpcid = (Label)item.FindControl("lblpcid");
            Label lblchargestobe = (Label)item.FindControl("lblchargestobe");

            if (chkc.Enabled == true)
            {
                chkc.Checked = ((CheckBox)sender).Checked;
            }
            if (Convert.ToString(lblpcid.Text) == "")
            {
                if (chkc.Checked == true)
                {
                    totcost += Convert.ToDecimal(lblcharges.Text);
                    totalcosttobe += Convert.ToDecimal(lblchargestobe.Text);
                }
            }
            GridViewRow ft = (GridViewRow)GridView1.FooterRow;
            Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
            lbltotalfooter.Text = totcost.ToString();
            lbltotaltobefooter.Text = totalcosttobe.ToString();
        }
        FillTotalInvamt();
    }
    protected void h2_chachedChanged(object sender, EventArgs e)
    {
        //GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        //int rinrow = row.RowIndex;
        Decimal totalcosttobe = 0;
        Decimal totcost = 0;

        foreach (GridViewRow item in GridView2.Rows)
        {
            CheckBox chkc = (CheckBox)item.FindControl("chkc");
            string id = GridView2.DataKeys[item.RowIndex].Value.ToString();
            Label lblcharges = (Label)item.FindControl("lblcharges");
            Label labourId = (Label)item.FindControl("labourId");
            Label lblchargestobe = (Label)item.FindControl("lblchargestobe");

            if (chkc.Enabled == true)
            {
                chkc.Checked = ((CheckBox)sender).Checked;
            }
            if (Convert.ToString(labourId.Text) == "")
            {
                if (chkc.Checked == true)
                {
                    totcost += Convert.ToDecimal(lblcharges.Text);
                    totalcosttobe += Convert.ToDecimal(lblchargestobe.Text);
                }
            }
            GridViewRow ft = (GridViewRow)GridView2.FooterRow;
            Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
            lbltotalfooter.Text = totcost.ToString();
            lbltotaltobefooter.Text = totalcosttobe.ToString();
        }
        FillTotalInvamt();
    }
    protected void h3_chachedChanged(object sender, EventArgs e)
    {
        //GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;
        decimal totf0 = 0;
        //int rinrow = row.RowIndex;
        foreach (GridViewRow item in GridView3.Rows)
        {
            CheckBox chkc = (CheckBox)item.FindControl("chkc");

            if (chkc.Enabled == true)
            {
                chkc.Checked = ((CheckBox)sender).Checked;
            }
            Label lblgtotal = (Label)item.FindControl("lblgtotal");
            Label lblId = (Label)item.FindControl("lblId");
            if (Convert.ToString(lblId.Text) == "" || Convert.ToString(lblId.Text) == "0")
            {
                if (chkc.Checked == true)
                {
                    totf0 += Convert.ToDecimal(lblgtotal.Text);
                }
                chkc.Enabled = true;
            }
            else
            {
                chkc.Enabled = false;
            }

            GridViewRow ft = (GridViewRow)GridView3.FooterRow;
            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
            lbltotaltobefooter.Text = totf0.ToString();
        }
        FillTotalInvamt();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Tid"] != null)
        {
            reffilldata();
        }
        int Id1 = 0;
        bool transuc = false;
        string en = "";
        SqlCommand cm131 = new SqlCommand(" SELECT     EntryTypeId, Max(EntryNumber) as Number FROM TranctionMaster_Temp Where EntryTypeId = '34'  Group by EntryTypeId", con);
        SqlDataAdapter da131 = new SqlDataAdapter(cm131);
        DataTable ds131 = new DataTable();
        da131.Fill(ds131);

        if (ds131.Rows.Count > 0)
        {
            if (ds131.Rows[0]["Number"].ToString() != "")
            {
                int q = Convert.ToInt32(ds131.Rows[0]["Number"]) + 1;
                en = q.ToString();
            }
            else
            {
                en = "1";
            }
        }
        else
        {
            en = "1";
        }
        string acc = "";
        if (ddlinvtype.SelectedValue == "2")
        {
            DataTable dtsr = select("select Account from Party_master where PartyID='" + ddlclient.SelectedValue + "'");
            if (dtsr.Rows.Count > 0)
            {
                acc = dtsr.Rows[0]["Account"].ToString(); ;
            }
        }
        else
        {
            acc = ddlcash.SelectedValue;

        }
      
        decimal avxc = 0;
        if (chkworder.Checked == true)
        {
            decimal OLDavgcost = 0;
            decimal oLDqtyONHAND = 0;
            DataTable Dataacces = select("Select InvWMasterId  from JobMaster where Id='" + ddlproject.SelectedValue + "'");

            if (Dataacces.Rows.Count > 0)
            {
                DataTable dt123 = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl " +
                " where InvWMasterId='" + Dataacces.Rows[0]["InvWMasterId"] + "' and DateUpdated<='" + DateTime.Now.ToShortDateString() + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc");

                if (dt123.Rows.Count > 0)
                {

                    if (Convert.ToString(dt123.Rows[0]["AvgCost"]) != "")
                    {
                        OLDavgcost = Convert.ToDecimal(dt123.Rows[0]["AvgCost"]);
                    }
                    if (Convert.ToString(dt123.Rows[0]["QtyonHand"]) != "")
                    {
                        oLDqtyONHAND = Convert.ToDecimal(dt123.Rows[0]["QtyonHand"]);
                    }
                }
                if (oLDqtyONHAND == 0)
                {
                    oLDqtyONHAND = 1;
                }

                 avxc = (OLDavgcost * oLDqtyONHAND) / Convert.ToDecimal(lblunits.Text);

                
            }
        }
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlTransaction transaction = con.BeginTransaction();
        try
        {

            SqlCommand cd3 = new SqlCommand("Temp_TranctionMasterRetIdentity", con);


            cd3.CommandType = CommandType.StoredProcedure;
            cd3.Parameters.AddWithValue("@Date", DateTime.Now.ToShortDateString());
            cd3.Parameters.AddWithValue("@EntryNumber", Convert.ToInt32(en));
            cd3.Parameters.AddWithValue("@EntryTypeId", "34");
            cd3.Parameters.AddWithValue("@UserId", Session["userid"].ToString());
            cd3.Parameters.AddWithValue("@Tranction_Amount", Convert.ToDecimal(lblTotalnetamt.Text));
            //cd3.Parameters.AddWithValue("@whid", ddlWarehouse.SelectedValue);

            cd3.Parameters.AddWithValue("@salesorderid", '0');


            cd3.Parameters.Add(new SqlParameter("@Tranction_Master_Id", SqlDbType.Int));
            cd3.Parameters["@Tranction_Master_Id"].Direction = ParameterDirection.Output;
            cd3.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
            cd3.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

            cd3.Transaction = transaction;
             Id1 = (int)cd3.ExecuteNonQuery();
            Id1 = Convert.ToInt32(cd3.Parameters["@Tranction_Master_Id"].Value);


            if (chkworder.Checked == true)
            {
                string invsubm = "INSERT INTO Temp_FGMasterTbl(InvWMasterId,AvgCost,QtyonHand,SubSubId,TranasId" +
                              ")Values('" + ddlinvname.SelectedValue + "','" + avxc + "','" +lblunits.Text + "','" + ddlcatesub.SelectedValue + "','" + Id1 + "')";
                SqlCommand sqlsub = new SqlCommand(invsubm, con);
                sqlsub.CommandType = CommandType.Text;
                sqlsub.Transaction = transaction;
                sqlsub.ExecuteNonQuery();
            }

            string invty = "INSERT INTO Temp_ProjectInvoiceType(InvoiceType,InvoiceAccount,TransactionId" +
                            ",Whid,Jobid,Date)Values('" + ddlinvtype.SelectedValue + "','" + acc + "','" + Id1 + "','" + ddlwarehouse.SelectedValue + "','" + ddlproject.SelectedValue + "','" + DateTime.Now.ToShortDateString() + "')";
            SqlCommand sqlcty = new SqlCommand(invty, con);
            sqlcty.CommandType = CommandType.Text;
            sqlcty.Transaction = transaction;
            sqlcty.ExecuteNonQuery();


            string Temp1val = "";
            string Temp1 = "INSERT INTO Temp_ProductMaterialBillTbl(MaterialIssueDetailid,Billingstatus,SalesRate" +
                             ",Totalcost,Totaltobechange,TransactionId,Margin,Marginper,OriginalRate)Values";
            foreach (GridViewRow item in GridView1.Rows)
            {
                string mid = GridView1.DataKeys[item.RowIndex].Value.ToString();
                Label lblunit = (Label)item.FindControl("lblunit");
                Label lblcost = (Label)item.FindControl("lblcost");
                TextBox lblsalesrate = (TextBox)item.FindControl("lblsalesrate");
                Label lblcharges = (Label)item.FindControl("lblcharges");
                Label lblpcid = (Label)item.FindControl("lblpcid");
                Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
                Label lblmargin = (Label)item.FindControl("lblmargin");
                Label lblmarginper = (Label)item.FindControl("lblmarginper");
                Label lblsalesrateor = (Label)item.FindControl("lblsalesrateor");
                CheckBox chkc = (CheckBox)item.FindControl("chkc");
                if (chkc.Checked == true)
                {
                    if (Temp1val.Length > 0)
                    {
                        Temp1val += ",";
                    }
                    Temp1val += "('" + mid + "','1','" + lblsalesrate.Text + "','" + lblcharges.Text + "','" + lblchargestobe.Text + "','" + Id1 + "','" + lblmargin.Text + "','" + lblmarginper.Text + "','" + lblsalesrateor.Text+ "')";
                }
            }
            if (Temp1val.Length > 0)
            {
                Temp1 += Temp1val;
                SqlCommand cd4 = new SqlCommand(Temp1, con);
                cd4.CommandType = CommandType.Text;
                cd4.Transaction = transaction;
                cd4.ExecuteNonQuery();
            }

            string Temp2val = "";
            string Temp2 = "INSERT INTO Temp_LabourMaterialBillTbl(JobEmpDailyTaskDetailId,Billingstatus,SalesRate" +
                             ",Totalcost,Totaltobechange,TransactionId,Margin,Marginper,OriginalRate,InvwId)Values";
            foreach (GridViewRow item in GridView2.Rows)
            {
                string mid = GridView2.DataKeys[item.RowIndex].Value.ToString();

                Label lblhor = (Label)item.FindControl("lblhor");
                Label lblempid = (Label)item.FindControl("lblempid");

                Label lblemprare = (Label)item.FindControl("lblemprare");
                TextBox lblsalesrate = (TextBox)item.FindControl("lblsalesrate");
                Label lblcharges = (Label)item.FindControl("lblcharges");
                Label labourId = (Label)item.FindControl("labourId");
                Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
                Label lblmargin = (Label)item.FindControl("lblmargin");
                Label lblmarginper = (Label)item.FindControl("lblmarginper");
                Label lblsalesrateor = (Label)item.FindControl("lblsalesrateor");
                DropDownList ddlservice = (DropDownList)item.FindControl("ddlservice");
                CheckBox chkc = (CheckBox)item.FindControl("chkc");
                if (chkc.Checked == true)
                {
                    if (Temp2val.Length > 0)
                    {
                        Temp2val += ",";
                    }
                    Temp2val += "('" + mid + "','1','" + lblsalesrate.Text + "','" + lblcharges.Text + "','" + lblchargestobe.Text + "','" + Id1 + "','" + lblmargin.Text + "','" + lblmarginper.Text + "','" +lblsalesrateor.Text + "','"+ddlservice.SelectedValue+"')";

                }
            }
            if (Temp2val.Length > 0)
            {
                Temp2 += Temp2val;
                SqlCommand cd4 = new SqlCommand(Temp2, con);
                cd4.CommandType = CommandType.Text;
                cd4.Transaction = transaction;
                cd4.ExecuteNonQuery();
            }

            string Temp3val = "";
            string Temp3 = "INSERT INTO Temp_salesothechargestbillbl(CID,Whid,Note" +
                             ",Qty,Rate,Total,TranId,jobid,OriginalRate,InvwId)Values";
            foreach (GridViewRow item in GridView3.Rows)
            {
                string mid = GridView3.DataKeys[item.RowIndex].Value.ToString();

                Label lblgnote = (Label)item.FindControl("lblgnote");
                Label lblgqty = (Label)item.FindControl("lblgqty");

                Label lblgrate = (Label)item.FindControl("lblgrate");


                Label lblgtotal = (Label)item.FindControl("lblgtotal");
                CheckBox chkc = (CheckBox)item.FindControl("chkc");
                Label lblsalesrateor = (Label)item.FindControl("lblsalesrateor");
                Label lblserviceId = (Label)item.FindControl("lblserviceId");
                if (chkc.Checked == true)
                {
                    if (Temp3val.Length > 0)
                    {
                        Temp3val += ",";
                    }
                    Temp3val += "('" + Session["Comid"] + "','" + ddlwarehouse.SelectedValue + "','" + lblgnote.Text + "','" + lblgqty.Text + "','" + lblgrate.Text + "','" + lblgtotal.Text + "','" + Id1 + "','" + ddlproject.SelectedValue + "','" + lblsalesrateor.Text + "','" + lblserviceId.Text + "')";

                }
            }
            if (Temp3val.Length > 0)
            {
                Temp3 += Temp3val;
                SqlCommand cd4 = new SqlCommand(Temp3, con);
                cd4.CommandType = CommandType.Text;
                cd4.Transaction = transaction;
                cd4.ExecuteNonQuery();
            }
           
                       
            transaction.Commit();
            transuc = true;
            //Response.Redirect("ProductInvoiceReport");
        }

        catch (Exception ex)
        {
            transaction.Rollback();
            transuc = false;
            //Label8.Visible = true;
            //Label8.Text = "Error :" + ex.Message;
        }
        finally
        {
            con.Close();
        }
        if (transuc == true)
        {
            string val=ClsEncDesc.Encrypted(Id1.ToString());
            Response.Redirect("ProductInvoiceReport.aspx?Tid=" + val.Replace("+", "@"));
            //string te = "ProductInvoiceReport.aspx?Tid=" + ClsEncDesc.Encrypted(Id1.ToString());
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
    protected void ddlinvtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlinvtype.SelectedValue == "1")
        {
            ddlcash.Visible = true;
            lblcashtax.Visible = true;
            fillCashAccount();
        }
        else
        {
            ddlcash.Visible = false;
            lblcashtax.Visible = false;
        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        fillPromaterial();
        fillOthercharges();
        fillgridlabour();
        FillTotalInvamt();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string te = "Dailyworksheet.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ddlservices_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dinv = select("SELECT Rate FROM  InventoryWarehouseMasterTbl where InventoryWarehouseMasterId='" + ddlservices.SelectedValue + "'");
        if (dinv.Rows.Count > 0)
        {
            txtrate.Text = Convert.ToString(dinv.Rows[0]["Rate"]);
            lbllistsalrate.Text = Convert.ToString(dinv.Rows[0]["Rate"]);
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillservices();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        fillviewmar();
        ModalPopupExtender4.Show();
    }
    protected void fillviewmar()
    {
        lblmaterialoldbill.Text = "0.00";
        lbllabouroldbill.Text = "0.00";
        lblotheroldbill.Text = "0.00";
        lblalreadytotalbill.Text = "0.00";

        lblmaterialNowbill.Text = "0.00";
        lbllabourNowbill.Text = "0.00";
        lblotherNowbill.Text = "0.00";
        lblnewtotalbill.Text = "0.00";
        DataTable oldmarcost = select("  Select Sum(Cast(Totalcost as Decimal(18,2))) as Totalcost from ProductMaterialBillTbl inner join ProjectInvoiceType on ProjectInvoiceType.TransactionId=ProductMaterialBillTbl.TransactionId where ProjectInvoiceType.Jobid='" + ddlproject.SelectedValue + "'");
        if (oldmarcost.Rows.Count > 0)
        {
            if (Convert.ToString(oldmarcost.Rows[0]["Totalcost"]) != "")
            {
                lblmaterialoldbill.Text = String.Format("{0:n}", Convert.ToDecimal(oldmarcost.Rows[0]["Totalcost"]));
            }

        }
        DataTable oldlabcost = select("  Select Sum(Cast(Totalcost as Decimal(18,2))) as Totalcost from LabourMaterialBillTbl inner join ProjectInvoiceType on ProjectInvoiceType.TransactionId=LabourMaterialBillTbl.TransactionId where ProjectInvoiceType.Jobid='" + ddlproject.SelectedValue + "'");
        if (oldlabcost.Rows.Count > 0)
        {
            if (Convert.ToString(oldlabcost.Rows[0]["Totalcost"]) != "")
            {
                lbllabouroldbill.Text = String.Format("{0:n}", Convert.ToDecimal(oldlabcost.Rows[0]["Totalcost"]));
            }

        }
        DataTable dt123other = select("select Sum(Cast(AmountApplied as Decimal(18,2))) as AmountApplied from AllocationMethod inner join OverHeadAllocationAccountOverHeadDetail on OverHeadAllocationAccountOverHeadDetail.AllocationMethod=AllocationMethod.Id  where OverHeadAllocationAccountOverHeadDetail.OverHeadMasterId in (select OverHeadMasterId  from  OverHeadAllocationJobDetail where  OverHeadAllocationJobDetail.JobMasterId='" + ddlproject.SelectedValue + "')");
        if (dt123other.Rows.Count > 0)
        {
            if (Convert.ToString(dt123other.Rows[0]["AmountApplied"]) != "")
            {
                lblotheroldbill.Text = String.Format("{0:n}", Convert.ToDecimal(dt123other.Rows[0]["AmountApplied"]));

            }
        }
        decimal totalcost = Convert.ToDecimal(lblmaterialoldbill.Text) + Convert.ToDecimal(lbllabouroldbill.Text) + Convert.ToDecimal(lblotheroldbill.Text);
        lblalreadytotalbill.Text = String.Format("{0:n}", totalcost);


        decimal totalnewcosttobe = 0;
        if (GridView1.Rows.Count > 0)
        {
            GridViewRow ft = (GridViewRow)GridView1.FooterRow;
            Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));

            lblmaterialNowbill.Text = String.Format("{0:n}", Convert.ToDecimal(lbltotalfooter.Text));
            totalnewcosttobe += Convert.ToDecimal(lbltotaltobefooter.Text);
        }
        if (GridView2.Rows.Count > 0)
        {
            GridViewRow ft = (GridViewRow)GridView2.FooterRow;
            Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));

            lbllabourNowbill.Text = String.Format("{0:n}", Convert.ToDecimal(lbltotalfooter.Text));
            totalnewcosttobe += Convert.ToDecimal(lbltotaltobefooter.Text);
        }
        if (GridView3.Rows.Count > 0)
        {
            GridViewRow ft = (GridViewRow)GridView3.FooterRow;
            //Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));

            totalnewcosttobe += Convert.ToDecimal(lbltotaltobefooter.Text);
        }

        decimal totalnowcost = Convert.ToDecimal(lblmaterialNowbill.Text) + Convert.ToDecimal(lbllabourNowbill.Text) + Convert.ToDecimal(lblotherNowbill.Text);
        totalnowcost = Math.Round(totalnowcost, 2);
        lblnewtotalbill.Text = String.Format("{0:n}", totalnowcost);
        lbltotalcost.Text = String.Format("{0:n}", (totalcost + totalnowcost));

        decimal productsale = 0;
        decimal laboursale = 0;
        decimal othersale = 0;
        DataTable oldsale = select("  Select Sum(Cast(Totaltobechange as Decimal(18,2))) as Totalcost from ProductMaterialBillTbl inner join ProjectInvoiceType on ProjectInvoiceType.TransactionId=ProductMaterialBillTbl.TransactionId where ProjectInvoiceType.Jobid='" + ddlproject.SelectedValue + "'");
        if (oldsale.Rows.Count > 0)
        {
            if (Convert.ToString(oldsale.Rows[0]["Totalcost"]) != "")
            {
                productsale = Convert.ToDecimal(oldsale.Rows[0]["Totalcost"]);
            }

        }
        DataTable oldsallab = select("  Select Sum(Cast(Totaltobechange as Decimal(18,2))) as Totalcost from LabourMaterialBillTbl inner join ProjectInvoiceType on ProjectInvoiceType.TransactionId=LabourMaterialBillTbl.TransactionId where ProjectInvoiceType.Jobid='" + ddlproject.SelectedValue + "'");
        if (oldsallab.Rows.Count > 0)
        {
            if (Convert.ToString(oldsallab.Rows[0]["Totalcost"]) != "")
            {
                laboursale = Convert.ToDecimal(oldsallab.Rows[0]["Totalcost"]);
            }

        }
        DataTable oldothercost = select("  Select Sum(Cast(Total as Decimal(18,2))) as Totalcost from salesothechargestbillbl inner join ProjectInvoiceType on ProjectInvoiceType.TransactionId=salesothechargestbillbl.TranId where ProjectInvoiceType.Jobid='" + ddlproject.SelectedValue + "'");
        if (oldothercost.Rows.Count > 0)
        {
            if (Convert.ToString(oldothercost.Rows[0]["Totalcost"]) != "")
            {
                othersale = Convert.ToDecimal(oldothercost.Rows[0]["Totalcost"]);
                //lblotheroldbill.Text = String.Format("{0:n}", Convert.ToDecimal(oldothercost.Rows[0]["Totalcost"]));
            }

        }
        decimal totalsale = productsale + laboursale + othersale;
        lblalreadytotalsale.Text = String.Format("{0:n}", totalsale);
        lblnewsale.Text = String.Format("{0:n}", totalnewcosttobe);
        lbltotsales.Text = String.Format("{0:n}", totalsale + totalnewcosttobe);


        decimal margin = Convert.ToDecimal(lbltotsales.Text) - Convert.ToDecimal(lbltotalcost.Text);
        margin = Math.Round(margin, 2);
        lblnewmargin.Text = String.Format("{0:n}", margin);
        decimal mper = 0;
        if (Convert.ToDecimal(lbltotsales.Text) > 0)
        {
            mper = Math.Round((margin * 100 / Convert.ToDecimal(lbltotsales.Text)), 2);
        }
        else
        {
            mper = Math.Round((margin * 100), 2);
        }
        lblnewmarginper.Text = String.Format("{0:n}", mper);
        lblmarginamt.Text = lblnewmargin.Text;
        lblMarginpercen.Text = lblnewmarginper.Text;


    }
    protected void btnover_Click(object sender, EventArgs e)
    {


        DataTable dt123 = select("select distinct AllocationMethod.Name as AllName,  OverHeadAllocationAccountOverHeadDetail.Id as OId,OverHeadAllocationAccountOverHeadDetail.AmountForPeriod,OverHeadAllocationAccountOverHeadDetail.AmountApplied,OverHeadAllocationAccountOverHeadDetail.Active,AccountMaster.Id  ,GroupCompanyMaster.groupid,GroupCompanyMaster.groupdisplayname,AccountMaster.AccountName as AccountName,AccountMaster.AccountId   from AllocationMethod inner join OverHeadAllocationAccountOverHeadDetail on OverHeadAllocationAccountOverHeadDetail.AllocationMethod=AllocationMethod.Id inner join AccountMaster on AccountMaster.Id=OverHeadAllocationAccountOverHeadDetail.AccountMasterId inner join GroupCompanyMaster on  GroupCompanyMaster.groupid=AccountMaster.GroupId where OverHeadAllocationAccountOverHeadDetail.OverHeadMasterId in (select OverHeadMasterId  from  OverHeadAllocationJobDetail where  OverHeadAllocationJobDetail.JobMasterId='" + ddlproject.SelectedValue + "')");
     
        grdaccount.DataSource = dt123;
        grdaccount.DataBind();
        foreach (GridViewRow gdr in grdaccount.Rows)
        {



          
            Label lbloid123 = (Label)gdr.FindControl("lbloid");
            Label lblamount123 = (Label)gdr.FindControl("lblamount");
            Label txtamountallocate = (Label)gdr.FindControl("txtamountallocate");
            lblamount123.Text = Math.Round(Convert.ToDecimal(lblamount123.Text), 2).ToString("###,###.##");
            if (lblamount123.Text.Length <= 0)
            {
                lblamount123.Text = "0.00";
            }
            else
            {
                lblamount123.Text = String.Format("{0:n}", Convert.ToDecimal(lblamount123.Text));
            }
            txtamountallocate.Text = String.Format("{0:n}", Convert.ToDecimal(txtamountallocate.Text));

          
           
        }


        ModalPopupExtender1.Show();
    }
    protected void chkworder_CheckedChanged(object sender, EventArgs e)
    {
       
        if (chkworder.Checked == true)
        {
            pnlworkorderinv.Visible = true;
            fillinv();
        }
        else
        {
            pnlworkorderinv.Visible = false;
        }
    }
    
    protected void fillinv()
    {
        DataTable dsinvmaster = new DataTable();
        DataTable dtmaster = select("select JobNumber,JobName from JobMaster  where JobMaster.Id='" + ddlproject.SelectedValue + "'");
        string FGinv = "FG_" + dtmaster.Rows[0]["JobNumber"] + "_" + dtmaster.Rows[0]["JobName"];
        DataTable dsfindpron = select("Select InventoryMaster.* from  InventoryMaster INNER JOIN " +
                        "  InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                        "  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN " +
                        "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where InventoryMaster.Name='" + FGinv + "' and InventoryCategoryMaster.compid='" + Session["comid"] + "'");

        if (dsfindpron.Rows.Count > 0)
        {
            dsinvmaster = select("select InventoryWarehouseMasterTbl.InventoryWarehouseMasterId  from InventoryWarehouseMasterTbl where InventoryMasterId='" + dsfindpron.Rows[0]["InventoryMasterId"] + "' and WareHouseId='" + ddlwarehouse.SelectedValue + "'");
            if (dsinvmaster.Rows.Count == 0)
            {
                int kkf = 0;
                SqlCommand cmd1 = new SqlCommand("select Max(InventoryWarehouseMasterId) as finaltotal from     InventoryWarehouseMasterTbl", con);
                con.Open();
                kkf = Convert.ToInt32(cmd1.ExecuteScalar());
                kkf = kkf + 1;
                con.Close();
                string insertdetail = "INSERT INTO InventoryWarehouseMasterTbl(InventoryWarehouseMasterId,InventoryMasterId ,InventoryDetailsId,WareHouseId,Active, ReorderQuantiy,NormalOrderQuantity,ReorderLevel ,QtyOnDateStarted ,QtyOnHand,Rate,OpeningQty,OpeningRate,Total,Weight,UnitTypeId)  VALUES ('" + kkf + "'," + Convert.ToInt32(dsfindpron.Rows[0]["InventoryMasterId"]) + "," + Convert.ToInt32(dsfindpron.Rows[0]["InventoryDetailsId"]) + ", ' " + Convert.ToInt32(ddlwarehouse.SelectedValue) + "','" + 1 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + DateTime.Now.ToShortDateString() + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','0','1')";

                SqlCommand insertDetailss = new SqlCommand(insertdetail, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                insertDetailss.ExecuteNonQuery();

            }
            else
            {
                fillsub();
            }
        }
        else
        {



            DataTable dtgw = select("select InventoryCatName,InventeroyCatId from InventoryCategoryMaster where " +
                 " InventoryCatName='FG' and compid='" + Session["comid"] + "' and InventoryCategoryMaster.CatType IS NULL");
            if (dtgw.Rows.Count > 0)
            {

            }
            else
            {
                string catname = "insert into InventoryCategoryMaster(InventoryCatName,Activestatus,compid) values('FG','" + 1 + "','" + Session["comid"] + "')";
                SqlCommand mycmd = new SqlCommand(catname, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                mycmd.ExecuteNonQuery();
                con.Close();

            }

            double mainid = 0;

            if (dtgw.Rows.Count > 0)
            {
                mainid = Convert.ToDouble(dtgw.Rows[0]["InventeroyCatId"].ToString());

            }
            else
            {
                DataTable dt123 = select("select Max(InventeroyCatId) as InventeroyCatId from InventoryCategoryMaster where compid='" + Session["comid"] + "'");

                mainid = Convert.ToDouble(dt123.Rows[0]["InventeroyCatId"].ToString());
            }

            DataTable dtgw1 = select("select InventorySubCatName,InventorySubCatId from InventorySubCategoryMaster where  " +
                 " InventorySubCatName='FG' and InventoryCategoryMasterId='" + mainid + "' ");

            if (dtgw1.Rows.Count > 0)
            {

            }
            else
            {
                string queryinsert = "insert into  InventorySubCategoryMaster(InventorySubCatName,InventoryCategoryMasterId,Activestatus)  values('FG'," + mainid + ",'" + 1 + "')";
                SqlCommand mycmd = new SqlCommand(queryinsert, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                mycmd.ExecuteNonQuery();
                con.Close();
            }

            double subcatid = 0;

            if (dtgw1.Rows.Count > 0)
            {
                subcatid = Convert.ToDouble(dtgw1.Rows[0]["InventorySubCatId"].ToString());

            }
            else
            {
                DataTable dt123 = select("select Max(InventorySubCatId) as InventorySubCatId from InventorySubCategoryMaster");
                subcatid = Convert.ToDouble(dt123.Rows[0]["InventorySubCatId"].ToString());
            }

            DataTable dtgw12 = select("select InventorySubSubName,InventorySubSubId from InventoruSubSubCategory where " +
              " InventorySubSubName='FG' and InventorySubCatID='" + subcatid + "'");

            if (dtgw12.Rows.Count > 0)
            {

            }
            else
            {
                string qurty = "insert into InventoruSubSubCategory(InventorySubSubName,InventorySubCatID,Activestatus)values('FG'," + subcatid + ",'" + 1 + "')";
                SqlCommand mycmd = new SqlCommand(qurty, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                mycmd.ExecuteNonQuery();
                con.Close();
            }
            double subsubcatid = 0;

            if (dtgw12.Rows.Count > 0)
            {
                subsubcatid = Convert.ToDouble(dtgw12.Rows[0]["InventorySubSubId"].ToString());
            }
            else
            {
                DataTable dt123 = select("select Max(InventorySubSubId) as InventorySubSubId from InventoruSubSubCategory");

                subsubcatid = Convert.ToDouble(dt123.Rows[0]["InventorySubSubId"].ToString());
            }


            string insrDetails = "INSERT INTO InventoryDetails (DateStarted,QtyTypeMasterId,UnitTypeId,Weight) " +
                            " VALUES ('" + DateTime.Now.ToShortDateString() + "',1,'" + 1 + "','" + 1 + "') ";
            SqlCommand cmdDetails = new SqlCommand(insrDetails, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdDetails.ExecuteNonQuery();
            con.Close();

            SqlCommand mycmd2 = new SqlCommand("select max(Inventory_Details_Id) as Inventory_Details_Id from InventoryDetails", con);
            mycmd2.CommandType = CommandType.Text;

            SqlDataAdapter adp2 = new SqlDataAdapter(mycmd2);
            DataSet ds2 = new DataSet();
            adp2.Fill(ds2);
            ViewState["InvDId"] = ds2.Tables[0].Rows[0][0].ToString();

            string insrMasters = "INSERT INTO InventoryMaster    (Name,InventoryDetailsId,InventorySubSubId,MasterActiveStatus,ProductNo) " +
                            " VALUES ('" + FGinv + "'," + Convert.ToInt32(ViewState["InvDId"]) + ",  " +
                             " " + subsubcatid + ",'" + 1 + "','768678') ";
            SqlCommand cmdMasters = new SqlCommand(insrMasters, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdMasters.ExecuteNonQuery();
            con.Close();

            DataTable dts = select("Select Max(InventoryMasterId)from InventoryMaster");
            ViewState["InvMId"] = dts.Rows[0][0].ToString();
            string insertBarcode = "Insert Into InventoryBarcodeMaster (InventoryMaster_id,Barcode)values(" + Convert.ToInt32(ViewState["InvMId"].ToString()) + ",'" + (subsubcatid+"GBar") + "')";
            SqlCommand cmdBarcode = new SqlCommand(insertBarcode, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdBarcode.ExecuteNonQuery();
            con.Close();

            SqlCommand cmd1 = new SqlCommand("select Max(InventoryWarehouseMasterId) as finaltotal from     InventoryWarehouseMasterTbl", con);
            con.Open();
            int k = Convert.ToInt32(cmd1.ExecuteScalar());
            k = k + 1;
            con.Close();
            string insertdetail = "INSERT INTO InventoryWarehouseMasterTbl(InventoryWarehouseMasterId,InventoryMasterId ,InventoryDetailsId,WareHouseId,Active, ReorderQuantiy,NormalOrderQuantity,ReorderLevel ,QtyOnDateStarted ,QtyOnHand,Rate,OpeningQty,OpeningRate,Total,Weight,UnitTypeId)  VALUES ('" + k + "'," + Convert.ToInt32(ViewState["InvMId"].ToString()) + "," + Convert.ToInt32(ViewState["InvDId"]) + ", ' " + Convert.ToInt32(ddlwarehouse.SelectedValue) + "','" + 1 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + DateTime.Now.ToShortDateString() + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','0','1')";

            SqlCommand insertDetailss = new SqlCommand(insertdetail, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            insertDetailss.ExecuteNonQuery();
            fillsub();
            ddlinvname.SelectedIndex = ddlinvname.Items.IndexOf(ddlinvname.Items.FindByText(FGinv));
        }
       
    }
    protected void fillsub()
    {
        DataTable dset = select("Select InventoryCatName+':'+InventorySubCatName+':'+InventorySubSubName as iName,InventorySubSubId from InventoruSubSubCategory INNER JOIN " +
                        "  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN " +
                        "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where InventoryCategoryMaster.compid='" + Session["comid"] + "' order by iName");
        ddlcatesub.DataSource = dset;
        ddlcatesub.DataTextField = "iName";
        ddlcatesub.DataValueField = "InventorySubSubId";
        ddlcatesub.DataBind();
        ddlcatesub.SelectedIndex = ddlcatesub.Items.IndexOf(ddlcatesub.Items.FindByText("FG:FG:FG"));
        EventArgs e=new EventArgs();
        object sender=new object();
        ddlcatesub_SelectedIndexChanged(sender, e);
    }

    protected void ddlcatesub_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dset = select("select InventoryMaster.Name,InventoryWarehouseMasterTbl.InventoryWarehouseMasterId  from InventoryWarehouseMasterTbl inner join "+
  " InventoryMaster on InventoryMaster.InventoryMasterId=InventoryWarehouseMasterTbl.InventoryMasterId inner join InventoruSubSubCategory  on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId where InventoruSubSubCategory.InventorySubSubId='" + ddlcatesub.SelectedValue + "' and WareHouseId='" + ddlwarehouse.SelectedValue + "' order by InventoryMaster.Name ");
        ddlinvname.DataSource = dset;
        ddlinvname.DataTextField = "Name";
        ddlinvname.DataValueField = "InventoryWarehouseMasterId";
        ddlinvname.DataBind();
      
        
    }
    protected void imgadddivision_Click(object sender, ImageClickEventArgs e)
    {

        string te = "InventoruSubSubCategory.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {fillsub();

    }
    protected void LinkButton3_Click1(object sender, ImageClickEventArgs e)
    {
        string te = "InventoryMasterAdd.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ddlcatesub_SelectedIndexChanged(sender, e);
    }
}
