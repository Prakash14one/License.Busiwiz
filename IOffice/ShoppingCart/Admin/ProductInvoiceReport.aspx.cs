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
using System.Text;
using System.Net;

public partial class ProductInvoiceReport : System.Web.UI.Page
{

    SqlConnection con=new SqlConnection(PageConn.connnn);

    protected void Page_Load(object sender, EventArgs e)
    {
      

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
            Pagecontrol.dypcontrol(Page, page);
            lblDate.Text = DateTime.Now.ToShortDateString();
            if (Request.QueryString["Tid"] != null )
            {
                Button1.Visible = true;
                Button2.Visible = true;
                Button3.Visible = true;
               ViewState["TID"]=ClsEncDesc.Decrypted(Request.QueryString["Tid"].Replace("@","+"));
               DataTable dtorder = select("Select Distinct JobReferenceNo,JobName,JobMaster.PartyId, Compname,JobMaster.compid,Temp_ProjectInvoiceType.Whid from [TranctionMaster_Temp] inner join Temp_ProjectInvoiceType on Temp_ProjectInvoiceType.TransactionId=TranctionMaster_Temp.Tranction_Master_Temp_Id inner join JobMaster on JobMaster.Id=Temp_ProjectInvoiceType.JobId inner join Party_master on Party_master.PartyID=JobMaster.PartyId where Tranction_Master_Temp_Id= '" + ViewState["TID"].ToString() + "'");
              if(dtorder.Rows.Count>0)
              {
                  lblclientname.Text = Convert.ToString(dtorder.Rows[0]["Compname"]);
                  lblproject.Text = Convert.ToString(dtorder.Rows[0]["JobName"]);
                  lblrefno.Text = Convert.ToString(dtorder.Rows[0]["JobReferenceNo"]);
                   ViewState["Whid"] = dtorder.Rows[0]["Whid"];
                   ViewState["CID"] = dtorder.Rows[0]["compid"];
                   ViewState["PartyId"] = dtorder.Rows[0]["PartyId"];
               }   
            }
            else if( Request.QueryString["Tapid"]!=null)
            {
                Button1.Visible = false;
                Button2.Visible = false;
                Button3.Visible = false;
                 ViewState["TID"]=ClsEncDesc.Decrypted(Request.QueryString["Tapid"]);
                 DataTable dtorder = select("Select Distinct JobMaster.PartyId,  JobReferenceNo,JobName, Compname, JobMaster.compid,ProjectInvoiceType.Whid from [TranctionMaster] inner join ProjectInvoiceType on ProjectInvoiceType.TransactionId=TranctionMaster.Tranction_Master_Id inner join JobMaster on JobMaster.Id=ProjectInvoiceType.JobId inner join Party_master on Party_master.PartyID=JobMaster.PartyId where Tranction_Master_Id= '" + ViewState["TID"].ToString() + "'");
                 if (dtorder.Rows.Count > 0)
                 {
                     lblclientname.Text = Convert.ToString(dtorder.Rows[0]["Compname"]);
                     lblproject.Text = Convert.ToString(dtorder.Rows[0]["JobName"]);
                     lblrefno.Text = Convert.ToString(dtorder.Rows[0]["JobReferenceNo"]);
                     ViewState["Whid"] = dtorder.Rows[0]["Whid"];
                     ViewState["CID"] = dtorder.Rows[0]["compid"];
                     ViewState["PartyId"] = dtorder.Rows[0]["PartyId"];
                 }
            }
            if (Convert.ToString(ViewState["TID"]) != "")
            {
                if (Request.QueryString["Tid"] != null)
                {

                    DataTable dtma = select("SELECT  OutGoingMailServer,WebMasterEmail,MasterEmailId, EmailMasterLoginPassword, AdminEmail, WHId " +
                               " FROM  CompanyWebsitMaster WHERE     (WHId = " + Convert.ToInt32(ViewState["Whid"]) + ") ");

                    if (dtma.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtma.Rows[0]["MasterEmailId"]) != "" && Convert.ToString(dtma.Rows[0]["OutGoingMailServer"]) != "")
                        {
                            CheckBox1.Enabled = true;
                            CheckBox1.ToolTip = "";

                            CheckBox1.Visible = true;
                        }
                        else
                        {

                            CheckBox1.Visible = true;
                            CheckBox1.Enabled = false;
                            CheckBox1.ToolTip = "There is no email set for this customer.";
                        }
                    }
                    else
                    {
                        CheckBox1.Visible = true;
                        CheckBox1.Enabled = false;
                        CheckBox1.ToolTip = "There is no email set for this customer.";
                    }

                }
                else
                {
                    CheckBox1.Visible = false;
                }

                BillAdd();
                fillPromaterial();
                fillOthercharges();
                fillgridlabour();
              
                FillTotalInvamt();
                StringBuilder HeadingTable = new StringBuilder();
                HeadingTable = (StringBuilder)getSiteAddress();
                lblHeading.Text = HeadingTable.ToString();
                lblHeading.Visible = true;
            }
        }
    }
    protected void BillAdd()
    {
        DataTable ds = select(" SELECT     User_master.UserID, User_master.Name, User_master.Address, User_master.City as city, User_master.Country as countryid, User_master.State as stateid, User_master.Phoneno as phone, " +
                         " User_master.EmailID as email, User_master.Active, StateMasterTbl.StateName as state ,CountryMaster.CountryName as country, User_master.zipcode as zip " +
                       "    FROM         " +
                       "   StateMasterTbl INNER JOIN " +
                         " User_master INNER JOIN " +
                         " CountryMaster ON User_master.Country = CountryMaster.CountryId ON StateMasterTbl.StateId = User_master.State  " +
                           " WHERE  (User_master.PartyId = '" + ViewState["PartyId"] + "')");
        if (ds.Rows.Count > 0)
        {

            string billaddress = "Name:" + ds.Rows[0]["Name"].ToString() + "<br>Address:" + ds.Rows[0]["Address"].ToString() + "<br>Mail:" + ds.Rows[0]["email"].ToString() + "<br>City:" + ds.Rows[0]["City"].ToString() + "<br>State:" + ds.Rows[0]["State"].ToString() + "<br>Country:" + ds.Rows[0]["Country"].ToString() + "<br>Phone:" + ds.Rows[0]["phone"].ToString() + "<br>Zip:" + ds.Rows[0]["zip"].ToString() + "<br>";
            lblBillAddress.Text = billaddress;
            lblShipAddress.Text = billaddress;

        }


    }
    public StringBuilder getSiteAddress()
    {
        StringBuilder strAddress = new StringBuilder();
        if (Convert.ToString(ViewState["Whid"]) != "")
        {
            string warename = ViewState["Whid"].ToString();
            string ADDRESSEX = "select  distinct CompanyWebsitMaster.Sitename,CompanyWebsitMaster.SiteUrl,CompanyMaster.AdminName,CompanyMaster.CompanyLogo, CompanyMaster.CompanyName,CompanyMaster.CompanyId,  CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Phone1,  CompanyWebsiteAddressMaster.Phone2,CompanyWebsiteAddressMaster.Fax,CompanyWebsiteAddressMaster.Email,  CompanyWebsiteAddressMaster.Zip,CompanyWebsiteAddressMaster.TollFree1,CompanyWebsiteAddressMaster.TollFree2,  CompanyWebsiteAddressMaster.State,CompanyWebsiteAddressMaster.City,CompanyWebsiteAddressMaster.Country ,  CountryMaster.CountryName,CityMasterTbl.CityName,StateMasterTbl.StateName,CityMasterTbl.StateId,StateMasterTbl.CountryId  from   CompanyWebsitMaster , CompanyMaster,CompanyWebsiteAddressMaster,StateMasterTbl, CityMasterTbl,CountryMaster  WHERE  CompanyWebsitMaster.CompanyId=CompanyMaster.CompanyId  AND   CompanyWebsiteAddressMaster.CompanyWebsiteMasterId=CompanyWebsitMaster.CompanyWebsiteMasterId  and   CompanyWebsiteAddressMaster.City= CityMasterTbl.CityId and CityMasterTbl.StateId=StateMasterTbl.StateId  and StateMasterTbl.CountryId=CountryMaster.CountryId and   CompanyWebsitMaster.WHId='" + warename + "'";
            SqlCommand cmd = new SqlCommand(ADDRESSEX, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);
           
            if (ds.Rows.Count > 0)
            {

                strAddress.Append("<table width=\"100%\"> ");

                strAddress.Append("<tr> <td width=\"70%\" style=\"padding-left:10px\" align=\"left\" > <img src=\"../images/" + ds.Rows[0]["CompanyLogo"].ToString() + "\" \"border=\"0\" Width=\"200px\" Height=\"125px\" / > </td><td style=\"padding-left:100px\" width=\"50%\" align=\"left\"><b><span style=\"color: #996600\">" + ds.Rows[0]["CompanyName"].ToString() + "</span></b><Br><b>" + ds.Rows[0]["Sitename"].ToString() + "</b><Br>" + ds.Rows[0]["Address1"].ToString() + "");



                if (ds.Rows[0]["Zip"].ToString() != "")
                {
                    strAddress.Append("<BR>" + ds.Rows[0]["CityName"].ToString() + "," + ds.Rows[0]["StateName"].ToString() + "," + ds.Rows[0]["CountryName"].ToString() + "," + ds.Rows[0]["Zip"].ToString() + "<Br>");
                }
                else
                {
                    strAddress.Append("<BR>" + ds.Rows[0]["CityName"].ToString() + "," + ds.Rows[0]["StateName"].ToString() + "," + ds.Rows[0]["CountryName"].ToString() + "<Br>");
                }

                    strAddress.Append( Convert.ToString( ds.Rows[0]["Phone1"]) + "<br>");
               

                if (ds.Rows[0]["Email"].ToString() != "")
                {
                    strAddress.Append( ds.Rows[0]["Email"].ToString() + "<Br>");
                }
                else
                {
                }
                if (ds.Rows[0]["SiteUrl"].ToString() != "")
                {
                    strAddress.Append(ds.Rows[0]["SiteUrl"].ToString() + " </td></tr>  ");
                }
                else
                {
                }

                strAddress.Append("</table> ");

                ViewState["sitename"] = ds.Rows[0]["Sitename"].ToString();
            }
        }
        return strAddress;

    }
   
    protected void fillPromaterial()
    {
       
        DataTable dt=new DataTable();
         if (Request.QueryString["Tid"] != null )
            {
                dt = select("SELECT Distinct ProductNo,OriginalRate,InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, Totalcost,Totaltobechange,Margin,Marginper,  MaterialIssueDetail.Id as Mid,Temp_ProductMaterialBillTbl.Id as pcid, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId , LEFT(InventoryCategoryMaster.InventoryCatName, 15) as InventoryCatName, LEFT(InventorySubCategoryMaster.InventorySubCatName, 15) as InventorySubCatName, " +
              "  case when(Temp_ProductMaterialBillTbl.Id IS NULL) then 'Unbilled Material' else 'Already billed' end bilst,  LEFT(InventoruSubSubCategory.InventorySubSubName, 15)as InventorySubSubName, InventoryMaster.Name AS Name,case when(Temp_ProductMaterialBillTbl.SalesRate IS NULL) then InventoryWarehouseMasterTbl.Rate else Temp_ProductMaterialBillTbl.SalesRate end  as SalesRate, MaterialIssueDetail.Rate, MaterialIssueDetail.Qty,  InventoryMaster.InventoryMasterId " +
           "  FROM     InventoryCategoryMaster Inner join " +
            "      InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId inner JOIN " +
            "      InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner JOIN " +
            "      InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId inner join InventoryWarehouseMasterTbl  on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join MaterialIssueDetail on MaterialIssueDetail.InvWMasterId =InventoryWarehouseMasterTbl.InventoryWarehouseMasterId inner join Temp_ProductMaterialBillTbl on Temp_ProductMaterialBillTbl.MaterialIssueDetailid=MaterialIssueDetail.Id inner join "+
            " TranctionMaster_Temp on TranctionMaster_Temp.Tranction_Master_Temp_Id=Temp_ProductMaterialBillTbl.TransactionId inner join MaterialIssueMasterTbl on MaterialIssueMasterTbl.Id=MaterialIssueDetail.MaterialMasterId inner join JobMaster on JobMaster.Id= MaterialIssueMasterTbl.JobMasterId  " +
          " WHERE Temp_ProductMaterialBillTbl.TransactionId='" + Convert.ToString( ViewState["TID"]) + "'  and  (InventoryMaster.MasterActiveStatus = 1) " +
          "   order by MaterialIssueDetail.Id Desc");
            
            }
         else if (Request.QueryString["Tapid"] != null)
         {
             dt = select("SELECT Distinct ProductNo,OriginalRate,InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, Totalcost,Totaltobechange,Margin,Marginper,  MaterialIssueDetail.Id as Mid,ProductMaterialBillTbl.Id as pcid, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId , LEFT(InventoryCategoryMaster.InventoryCatName, 15) as InventoryCatName, LEFT(InventorySubCategoryMaster.InventorySubCatName, 15) as InventorySubCatName, " +
             "  case when(ProductMaterialBillTbl.Id IS NULL) then 'Unbilled Material' else 'Already billed' end bilst,  LEFT(InventoruSubSubCategory.InventorySubSubName, 15)as InventorySubSubName, InventoryMaster.Name AS Name,case when(ProductMaterialBillTbl.SalesRate IS NULL) then InventoryWarehouseMasterTbl.Rate else ProductMaterialBillTbl.SalesRate end  as SalesRate, MaterialIssueDetail.Rate, MaterialIssueDetail.Qty,  InventoryMaster.InventoryMasterId " +
          "  FROM     InventoryCategoryMaster Inner join " +
           "      InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId inner JOIN " +
           "      InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner JOIN " +
           "      InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId inner join InventoryWarehouseMasterTbl  on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join MaterialIssueDetail on MaterialIssueDetail.InvWMasterId =InventoryWarehouseMasterTbl.InventoryWarehouseMasterId inner join ProductMaterialBillTbl on ProductMaterialBillTbl.MaterialIssueDetailid=MaterialIssueDetail.Id inner join " +
           " TranctionMaster on TranctionMaster.Tranction_Master_Id=ProductMaterialBillTbl.TransactionId inner join MaterialIssueMasterTbl on MaterialIssueMasterTbl.Id=MaterialIssueDetail.MaterialMasterId inner join JobMaster on JobMaster.Id= MaterialIssueMasterTbl.JobMasterId  " +
         " WHERE ProductMaterialBillTbl.TransactionId='" + Convert.ToString(ViewState["TID"]) + "'  and  (InventoryMaster.MasterActiveStatus = 1) " +
         "   order by MaterialIssueDetail.Id Desc");

         }
       
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
                //Label lblunit = (Label)item.FindControl("lblunit");
                //Label lblcost = (Label)item.FindControl("lblcost");
                Label lblsalesrate = (Label)item.FindControl("lblsalesrate");
                Label lblcharges = (Label)item.FindControl("lblcharges");
                Label lblpcid = (Label)item.FindControl("lblpcid");
                Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
                Label lblmargin = (Label)item.FindControl("lblmargin");
                Label lblmarginper = (Label)item.FindControl("lblmarginper");
                Label lblserviceId = (Label)item.FindControl("lblserviceId");
                //CheckBox chkc = (CheckBox)item.FindControl("chkc");
                //lblcharges.Text = Math.Round(Convert.ToDecimal(lblunit.Text) * Convert.ToDecimal(lblcost.Text), 2).ToString();
                //lblchargestobe.Text = Math.Round(Convert.ToDecimal(lblunit.Text) * Convert.ToDecimal(lblsalesrate.Text), 2).ToString();

                //lblmargin.Text = Math.Round(Convert.ToDecimal(lblchargestobe.Text) - Convert.ToDecimal(lblcharges.Text), 2).ToString();
                //decimal mper = Convert.ToDecimal(lblmargin.Text) * Convert.ToDecimal(lblsalesrate.Text) / 100;
                //lblmarginper.Text = Convert.ToString(Math.Round(mper, 2));


                totcost += Convert.ToDecimal(lblcharges.Text);
                totalcosttobe += Convert.ToDecimal(lblchargestobe.Text);


            }
            GridViewRow ft = (GridViewRow)GridView1.FooterRow;
            Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
            lbltotalfooter.Text = totcost.ToString();
            lbltotaltobefooter.Text = totalcosttobe.ToString();
            pnlmaterial.Visible = true;
        }
        else
        {
            pnlmaterial.Visible = false;
        }
    }
    protected void fillgridlabour()
    {

        string trc = "";
        string str = "";
        if (Request.QueryString["Tid"] != null)
        {
            str = "select   distinct JobEmployeeDailyTaskDetail.Note, Totalcost,Totaltobechange,Margin,Marginper,OriginalRate, JobEmployeeDailyTaskTbl.EmployeeId, case when(Temp_LabourMaterialBillTbl.Id IS NULL) then 'Unbilled Labour' else 'Already billed' end bilst, Temp_LabourMaterialBillTbl.Id as labourId," +
                " Case when(SalesRate='' or Rate IS NULL) then '0' else SalesRate end as  SalesRate, EmployeeMaster.EmployeeName as Employee, " +
                " Case when(Cost='') then '0' else Cost end as  Cost,Case when(Rate='' or Rate IS NULL) then '0' else Rate end as  Rate, " +
              "  FromToTime,Hrs,JobEmployeeDailyTaskTbl.EmployeeId, Convert(nvarchar, JobEmployeeDailyTaskTbl.Enddate,101) as Enddate, " +
              " Convert(nvarchar, JobEmployeeDailyTaskTbl.Date,101) as Date,JobEmployeeDailyTaskDetail.FromTime, JobEmployeeDailyTaskDetail.Id," +
              " JobName,Party_Master.Compname from Party_Master inner join JobMaster on JobMaster.PartyId=Party_Master.PartyId inner join " +
              " JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id inner join Temp_LabourMaterialBillTbl on " +
              " Temp_LabourMaterialBillTbl.JobEmpDailyTaskDetailId=JobEmployeeDailyTaskDetail.Id inner join TranctionMaster_Temp on " +
              " TranctionMaster_Temp.Tranction_Master_Temp_Id=Temp_LabourMaterialBillTbl.TransactionId inner join JobEmployeeDailyTaskTbl on " +
              "JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId inner join EmployeeMaster on " +
              "EmployeeMaster.EmployeeMasterID=JobEmployeeDailyTaskTbl.EmployeeId  where  Temp_LabourMaterialBillTbl.TransactionId='" + Convert.ToString(ViewState["TID"]) + "'   and (FromToTime IS NOT NULL and FromToTime<>'') order by JobEmployeeDailyTaskDetail.Id Desc";
        }
        else if(Request.QueryString["Tapid"] != null )
            {
                str = "select   distinct JobEmployeeDailyTaskDetail.Note, Totalcost,Totaltobechange,Margin,Marginper,OriginalRate, JobEmployeeDailyTaskTbl.EmployeeId, case when(LabourMaterialBillTbl.Id IS NULL) then 'Unbilled Labour' else 'Already billed' end bilst, LabourMaterialBillTbl.Id as labourId," +
                  " Case when(SalesRate='' or Rate IS NULL) then '0' else SalesRate end as  SalesRate, EmployeeMaster.EmployeeName as Employee, " +
                  " Case when(Cost='') then '0' else Cost end as  Cost,Case when(Rate='' or Rate IS NULL) then '0' else Rate end as  Rate, " +
                "  FromToTime,Hrs,JobEmployeeDailyTaskTbl.EmployeeId, Convert(nvarchar, JobEmployeeDailyTaskTbl.Enddate,101) as Enddate, " +
                " Convert(nvarchar, JobEmployeeDailyTaskTbl.Date,101) as Date,JobEmployeeDailyTaskDetail.FromTime, JobEmployeeDailyTaskDetail.Id," +
                " JobName,Party_Master.Compname from Party_Master inner join JobMaster on JobMaster.PartyId=Party_Master.PartyId inner join " +
                " JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id inner join LabourMaterialBillTbl on " +
                " LabourMaterialBillTbl.JobEmpDailyTaskDetailId=JobEmployeeDailyTaskDetail.Id inner join TranctionMaster on " +
                " TranctionMaster.Tranction_Master_Id=LabourMaterialBillTbl.TransactionId inner join JobEmployeeDailyTaskTbl on " +
                "JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId inner join EmployeeMaster on " +
                "EmployeeMaster.EmployeeMasterID=JobEmployeeDailyTaskTbl.EmployeeId  where  LabourMaterialBillTbl.TransactionId='" + Convert.ToString(ViewState["TID"]) + "'   and (FromToTime IS NOT NULL and FromToTime<>'') order by JobEmployeeDailyTaskDetail.Id Desc";
     
        }
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
            foreach (GridViewRow item in GridView2.Rows)
            {
                string id = GridView2.DataKeys[item.RowIndex].Value.ToString();
                //Label lblhor = (Label)item.FindControl("lblhor");
                //Label lblempid = (Label)item.FindControl("lblempid");

                //Label lblemprare = (Label)item.FindControl("lblemprare");
                //TextBox lblsalesrate = (TextBox)item.FindControl("lblsalesrate");
                Label lblcharges = (Label)item.FindControl("lblcharges");
                Label labourId = (Label)item.FindControl("labourId");
                Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
                Label lblmargin = (Label)item.FindControl("lblmargin");
                Label lblmarginper = (Label)item.FindControl("lblmarginper");
                Label lblservice = (Label)item.FindControl("lblservice");
                Label lblserviceId = (Label)item.FindControl("lblserviceId");
                totcost += Convert.ToDecimal(lblcharges.Text);
                totalcosttobe += Convert.ToDecimal(lblchargestobe.Text);
                DataTable dinv = new DataTable();
                if (Request.QueryString["Tid"] != null)
                {
                    dinv = select("SELECT distinct   InventoryMaster.Name AS category,InventoryWarehouseMasterTbl.Rate, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                           " FROM         InventorySubCategoryMaster INNER JOIN " +
                         " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId INNER JOIN " +
                         " Temp_LabourMaterialBillTbl ON Temp_LabourMaterialBillTbl.InvwId = InventoryWarehouseMasterTbl.InventoryWarehouseMasterId where Temp_LabourMaterialBillTbl.Id='" + labourId.Text + "' " +
                         " and InventoryMaster.CatType='0'");


                }
                else if (Request.QueryString["Tapid"] != null)
                {
                    dinv = select("SELECT distinct   InventoryMaster.Name AS category,InventoryWarehouseMasterTbl.Rate, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                         " FROM         InventorySubCategoryMaster INNER JOIN " +
                       " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId INNER JOIN " +
                       " LabourMaterialBillTbl ON LabourMaterialBillTbl.InvwId = InventoryWarehouseMasterTbl.InventoryWarehouseMasterId where LabourMaterialBillTbl.Id='" + labourId.Text + "' " +
                       " and InventoryMaster.CatType='0'");

                }
                if (dinv.Rows.Count > 0)
                {
                    lblservice.Text = Convert.ToString(dinv.Rows[0]["category"]);
                    lblserviceId.Text = Convert.ToString(dinv.Rows[0]["InventoryWarehouseMasterId"]);
                }
            }
            GridViewRow ft = (GridViewRow)GridView2.FooterRow;
            Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
            lbltotalfooter.Text = totcost.ToString();
            lbltotaltobefooter.Text = totalcosttobe.ToString();
            pnllabour.Visible = true;
        }
        else
        {
            pnllabour.Visible = false;
        }
      
    }
    protected void fillOthercharges()
    {
        DataTable dtTemp = new DataTable();
           
      
            DataTable df=new DataTable();
            if (Request.QueryString["Tid"] != null)
            {
                df = select("select distinct Temp_salesothechargestbillbl.*,InventoryMaster.Name AS category,InventoryWarehouseMasterTbl.Rate, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId   from  InventorySubCategoryMaster INNER JOIN " +
                       " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join "+
                       " InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join "+
                       " InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId "+
                       " INNER JOIN   Temp_salesothechargestbillbl ON Temp_salesothechargestbillbl.InvwId = InventoryWarehouseMasterTbl.InventoryWarehouseMasterId where TranId='" + Convert.ToString(ViewState["TID"]) + "' order by Id");
            }
            else if (Request.QueryString["Tapid"] != null)
            {
                df = select("select distinct salesothechargestbillbl.*,InventoryMaster.Name AS category,InventoryWarehouseMasterTbl.Rate, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId   from  InventorySubCategoryMaster INNER JOIN " +
                       " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join " +
                       " InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join " +
                       " InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId " +
                       " INNER JOIN   salesothechargestbillbl ON salesothechargestbillbl.InvwId = InventoryWarehouseMasterTbl.InventoryWarehouseMasterId where TranId='" + Convert.ToString(ViewState["TID"]) + "' order by Id");
      
            }
            if (df.Rows.Count > 0)
            {
                GridView3.DataSource = df;
                GridView3.DataBind();
                pnlothecharge.Visible = true;
            }
            else
            {
                pnlothecharge.Visible = false;
            }
        
        Decimal totf0 = 0;
        if (GridView3.Rows.Count > 0)
        {
            foreach (GridViewRow item in GridView3.Rows)
            { 
                Label lblgtotal = (Label)item.FindControl("lblgtotal");
                Label lblId = (Label)item.FindControl("lblId");
               
                    totf0 += Convert.ToDecimal(lblgtotal.Text);
                 
            }

            GridViewRow ft = (GridViewRow)GridView3.FooterRow;
            Label lbltotaltobefooter = (Label)(ft.FindControl("lbltotaltobefooter"));
            lbltotaltobefooter.Text = totf0.ToString();
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
    protected void FillTotalInvamt()
    {
        decimal g1 = 0;
        decimal g2 = 0;
        decimal g3 = 0;
        Decimal tax1amt = 0;
        Decimal tax2amt = 0;
        Decimal tax3amt = 0;
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
        lblGross.Text = Convert.ToString(g1 + g2 + g3);
        lblGross.Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(lblGross.Text), 2));
        DataTable dtwhid = select("select Top(3) Taxshortname,Name,Percentage,Amount,PurchaseTaxAccountMasterID,TaxAccountMasterID from [TaxTypeMaster]  where Active='1' and [Whid]='" + ViewState["Whid"] + "'   order by TaxTypeMaster.TaxTypeMasterId Desc");
      
        pnltax1.Visible = false;
        pnltax2.Visible = false;
        pnltax2.Visible = false;
        lbltax1amt.Text = "0";
        lbltax2amt.Text = "0";
        lbltax3amt.Text = "0";
         lblt3acc.Text="";
        lblt2acc.Text="";
         lblt1acc.Text="";
        if (dtwhid.Rows.Count > 0)
        {
            if (dtwhid.Rows.Count >= 1)
            {lblt1acc.Text=Convert.ToString (dtwhid.Rows[0]["TaxAccountMasterID"]);
                if (Convert.ToDecimal(dtwhid.Rows[0]["Percentage"]) > 0)
                {
                    tax1amt = Convert.ToDecimal(lblGross.Text) * Convert.ToDecimal(dtwhid.Rows[0]["Percentage"]) / 100;
                    tax1amt = Math.Round(tax1amt, 2);
                }
                else
                {
                    tax1amt = Convert.ToDecimal(dtwhid.Rows[0]["Amount"]);
               
                }
                lbltax1text.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                lbltax1amt.Text = String.Format("{0:n}", Math.Round(tax1amt, 2));
           
                pnltax1.Visible = true;
            }
            if (dtwhid.Rows.Count >= 2)
            {lblt2acc.Text=Convert.ToString (dtwhid.Rows[1]["TaxAccountMasterID"]);
                if (Convert.ToDecimal(dtwhid.Rows[1]["Percentage"]) > 0)
                {
                    tax2amt = Convert.ToDecimal(lblGross.Text) * Convert.ToDecimal(dtwhid.Rows[1]["Percentage"]) / 100;
                    tax2amt = Math.Round(tax2amt, 2);
                }
                else
                {
                    tax2amt = Convert.ToDecimal(dtwhid.Rows[1]["Amount"]);
               
                }
                lbltax2text.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]);
               
                lbltax2amt.Text = String.Format("{0:n}",  Math.Round(tax2amt, 2));
                pnltax2.Visible = true;
            }
            if (dtwhid.Rows.Count >= 3)
            {    lblt3acc.Text=Convert.ToString (dtwhid.Rows[2]["TaxAccountMasterID"]);
                if (Convert.ToDecimal(dtwhid.Rows[2]["Percentage"]) > 0)
                {
                    tax3amt = Convert.ToDecimal(lblGross.Text) * Convert.ToDecimal(dtwhid.Rows[2]["Percentage"]) / 100;
                    tax3amt = Math.Round(tax3amt, 2);
                }
                else
                {
                    tax3amt = Convert.ToDecimal(dtwhid.Rows[2]["Amount"]);

                }
                lbltax3text.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]);
                lbltax3amt.Text = String.Format("{0:n}", Math.Round(tax3amt, 2));
            
                pnltax3.Visible = true;
            }
        }
        ordervaldis();
        CountCustomerDisc();
        lbltotaltaxamt.Text = Convert.ToString(tax1amt + tax2amt + tax3amt);
        lbltotaltaxamt.Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(lbltotaltaxamt.Text), 2));
        lblnetamt.Text = Convert.ToString(Convert.ToDecimal(lblGross.Text) + tax1amt + tax2amt + tax3amt - Convert.ToDecimal(lblCustDisc.Text) - Convert.ToDecimal(lblOrderDisc.Text));
        lblnetamt.Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(lblnetamt.Text), 2));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        lblnetamt.Text = lblnetamt.Text.Replace(",", "");
        int Id1 = 0;
        bool transuc = false;
        string en = "";
        SqlConnection cn = new SqlConnection(PageConn.connnn);


        SqlCommand cmet = new SqlCommand(" SELECT * FROM Temp_ProjectInvoiceType Where TransactionId='" + Convert.ToString(ViewState["TID"]) + "'", cn);
        SqlDataAdapter adsn = new SqlDataAdapter(cmet);
        DataTable dtsin = new DataTable();
        adsn.Fill(dtsin);
        if (dtsin.Rows.Count > 0)
        {

            SqlCommand cm131 = new SqlCommand(" SELECT  EntryTypeId, Max(EntryNumber) as Number FROM TranctionMaster Where EntryTypeId = '34' and Whid='" + dtsin.Rows[0]["Whid"]+ "'  Group by EntryTypeId", con);
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
            DataTable Dataacces = select("Select InvWMasterId  from JobMaster where Id='" + dtsin.Rows[0]["Jobid"] + "'");

               DataTable dtsub=select("Select * from Temp_FGMasterTbl where TranasId='"+ViewState["TID"]+"'");
              
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                ViewState["Wared"] = dtsin.Rows[0]["Whid"];
                SqlCommand cd3 = new SqlCommand("Sp_Insert_TranctionMasterRetIdentity", con);


                cd3.CommandType = CommandType.StoredProcedure;
                cd3.Parameters.AddWithValue("@Date",lblDate.Text);
                cd3.Parameters.AddWithValue("@EntryNumber", Convert.ToInt32(en));
                cd3.Parameters.AddWithValue("@EntryTypeId", "34");
                cd3.Parameters.AddWithValue("@UserId", Session["userid"].ToString());
                cd3.Parameters.AddWithValue("@Tranction_Amount", Convert.ToDecimal(lblnetamt.Text));
                cd3.Parameters.AddWithValue("@whid", dtsin.Rows[0]["Whid"]);

                cd3.Parameters.AddWithValue("@compid", ViewState["CID"]);


                cd3.Parameters.Add(new SqlParameter("@Tranction_Master_Id", SqlDbType.Int));
                cd3.Parameters["@Tranction_Master_Id"].Direction = ParameterDirection.Output;
                cd3.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                cd3.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

                cd3.Transaction = transaction;
                Id1 = (int)cd3.ExecuteNonQuery();
                Id1 = Convert.ToInt32(cd3.Parameters["@Tranction_Master_Id"].Value);
                if (dtsub.Rows.Count > 0)
                {

                    string ABCD = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,Rate,DateUpdated,AvgCost,QtyonHand)values('" + dtsub.Rows[0]["InvWMasterId"] + "','" + Id1 + "','" + dtsub.Rows[0]["QtyonHand"] + "','" + dtsub.Rows[0]["AvgCost"] + "','" + lblDate.Text + "','" + dtsub.Rows[0]["AvgCost"] + "','" + dtsub.Rows[0]["QtyonHand"] + "')";
                        SqlCommand cfidn = new SqlCommand(ABCD, con);
                        cfidn.CommandType = CommandType.Text;
                        cfidn.Transaction = transaction;
                        cfidn.ExecuteNonQuery();

                        if (Dataacces.Rows.Count > 0)
                        {
                            string ABCDEFR = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,Rate,DateUpdated,AvgCost,QtyonHand)values('" + Dataacces.Rows[0]["InvWMasterId"] + "','" + Id1 + "','0','0','" + lblDate.Text + "','0','0')";
                            SqlCommand cfidnrec = new SqlCommand(ABCDEFR, con);
                            cfidnrec.CommandType = CommandType.Text;
                            cfidnrec.Transaction = transaction;
                            cfidnrec.ExecuteNonQuery();
                        }
                  
                }
                string antr1 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                          " ,DateTimeOfTransaction,compid,whid)" +
                          " VALUES('" + dtsin.Rows[0]["InvoiceAccount"] + "','" + Convert.ToDecimal(lblGross.Text) + "'" +
                          " ,'" + Id1 + "','" + lblDate.Text + "','" + ViewState["CID"].ToString() + "','" + dtsin.Rows[0]["Whid"] + "')";

                SqlCommand cdmasd1 = new SqlCommand(antr1, con);
                cdmasd1.CommandType = CommandType.Text;
                cdmasd1.Transaction = transaction;
                cdmasd1.ExecuteNonQuery();

                if (lblt1acc.Text.Length > 0)
                {
                    string antr1t1 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                      " ,DateTimeOfTransaction,compid,whid)" +
                      " VALUES('" + lblt1acc.Text + "','" + Convert.ToDecimal(lbltax1amt.Text) + "'" +
                      " ,'" + Id1 + "','" + lblDate.Text + "','" + ViewState["CID"].ToString() + "','" + dtsin.Rows[0]["Whid"] + "')";

                    SqlCommand cdmasdt1 = new SqlCommand(antr1t1, con);
                    cdmasdt1.CommandType = CommandType.Text;
                    cdmasdt1.Transaction = transaction;
                    cdmasdt1.ExecuteNonQuery();


                }

                if (lblt2acc.Text.Length > 0)
                {
                    string antr1t2 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                      " ,DateTimeOfTransaction,compid,whid)" +
                      " VALUES('" + lblt2acc.Text + "','" + Convert.ToDecimal(lbltax2amt.Text) + "'" +
                      " ,'" + Id1 + "','" + lblDate.Text + "','" + ViewState["CID"].ToString() + "','" + dtsin.Rows[0]["Whid"] + "')";

                    SqlCommand cdmasdt2 = new SqlCommand(antr1t2, con);
                    cdmasdt2.CommandType = CommandType.Text;
                    cdmasdt2.Transaction = transaction;
                    cdmasdt2.ExecuteNonQuery();


                }
                if (lblt3acc.Text.Length > 0)
                {
                    string antr1t3 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                      " ,DateTimeOfTransaction,compid,whid)" +
                      " VALUES('" + lblt3acc.Text + "','" + Convert.ToDecimal(lbltax3amt.Text) + "'" +
                      " ,'" + Id1 + "','" + lblDate.Text + "','" + ViewState["CID"].ToString() + "','" + dtsin.Rows[0]["Whid"] + "')";

                    SqlCommand cdmasdt3 = new SqlCommand(antr1t3, con);
                    cdmasdt3.CommandType = CommandType.Text;
                    cdmasdt3.Transaction = transaction;
                    cdmasdt3.ExecuteNonQuery();


                }

                string antr = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                       " ,DateTimeOfTransaction,compid,whid)" +
                       " VALUES('5000','" + Convert.ToDecimal(lblnetamt.Text) + "'" +
                       " ,'" + Id1 + "','" + lblDate.Text + "','" + ViewState["CID"].ToString() + "','" + dtsin.Rows[0]["Whid"] + "')";


                SqlCommand cdmasd = new SqlCommand(antr, con);
                cdmasd.CommandType = CommandType.Text;
                cdmasd.Transaction = transaction;
                cdmasd.ExecuteNonQuery();

                string invty = "INSERT INTO ProjectInvoiceType(InvoiceType,InvoiceAccount,TransactionId" +
                                ",Whid,Jobid,Date)Values('" + dtsin.Rows[0]["InvoiceType"] + "','" + dtsin.Rows[0]["InvoiceAccount"] + "','" + Id1 + "','" + dtsin.Rows[0]["Whid"] + "','" + dtsin.Rows[0]["Jobid"] + "','" + dtsin.Rows[0]["Date"] + "')";
                SqlCommand sqlcty = new SqlCommand(invty, con);
                sqlcty.CommandType = CommandType.Text;
                sqlcty.Transaction = transaction;
                sqlcty.ExecuteNonQuery();


                string Temp1val = "";
                string Temp1 = "INSERT INTO ProductMaterialBillTbl(MaterialIssueDetailid,Billingstatus,SalesRate" +
                                 ",Totalcost,Totaltobechange,TransactionId,Margin,Marginper,OriginalRate)Values";
                foreach (GridViewRow item in GridView1.Rows)
                {
                    string mid = GridView1.DataKeys[item.RowIndex].Value.ToString();
                    Label lblunit = (Label)item.FindControl("lblunit");
                    Label lblcost = (Label)item.FindControl("lblcost");
                    Label lblsalesrate = (Label)item.FindControl("lblsalesrate");
                    Label lblcharges = (Label)item.FindControl("lblcharges");
                    Label lblpcid = (Label)item.FindControl("lblpcid");
                    Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
                    Label lblmargin = (Label)item.FindControl("lblmargin");
                    Label lblmarginper = (Label)item.FindControl("lblmarginper");
                    Label lblsalesrateor = (Label)item.FindControl("lblsalesrateor");

                    if (Temp1val.Length > 0)
                    {
                        Temp1val += ",";
                    }
                    Temp1val += "('" + mid + "','1','" + lblsalesrate.Text + "','" + lblcharges.Text + "','" + lblchargestobe.Text + "','" + Id1 + "','" + lblmargin.Text + "','" + lblmarginper.Text + "','" + lblsalesrateor.Text + "')";

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
                string Temp2 = "INSERT INTO LabourMaterialBillTbl(JobEmpDailyTaskDetailId,Billingstatus,SalesRate" +
                                 ",Totalcost,Totaltobechange,TransactionId,Margin,Marginper,OriginalRate,InvwId)Values";
                foreach (GridViewRow item in GridView2.Rows)
                {
                    string mid = GridView2.DataKeys[item.RowIndex].Value.ToString();

                    Label lblhor = (Label)item.FindControl("lblhor");
                    Label lblempid = (Label)item.FindControl("lblempid");

                    Label lblemprare = (Label)item.FindControl("lblemprare");
                    Label lblsalesrate = (Label)item.FindControl("lblsalesrate");
                    Label lblcharges = (Label)item.FindControl("lblcharges");
                    Label labourId = (Label)item.FindControl("labourId");
                    Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
                    Label lblmargin = (Label)item.FindControl("lblmargin");
                    Label lblmarginper = (Label)item.FindControl("lblmarginper");
                    Label lblsalesrateor = (Label)item.FindControl("lblsalesrateor");
                    Label lblserviceId = (Label)item.FindControl("lblserviceId");
                    if (Temp2val.Length > 0)
                    {
                        Temp2val += ",";
                    }

                    Temp2val += "('" + mid + "','1','" + lblsalesrate.Text + "','" + lblcharges.Text + "','" + lblchargestobe.Text + "','" + Id1 + "','" + lblmargin.Text + "','" + lblmarginper.Text + "','" + lblsalesrateor.Text + "','" + lblserviceId.Text + "')";
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
                string Temp3 = "INSERT INTO salesothechargestbillbl(CID,Whid,Note" +
                                 ",Qty,Rate,Total,TranId,jobid,OriginalRate,InvwId)Values";
                foreach (GridViewRow item in GridView3.Rows)
                {
                    string mid = GridView3.DataKeys[item.RowIndex].Value.ToString();

                    Label lblgnote = (Label)item.FindControl("lblgnote");
                    Label lblgqty = (Label)item.FindControl("lblgqty");
                    Label lbljid = (Label)item.FindControl("lbljid");
                    Label lblgrate = (Label)item.FindControl("lblgrate");
                    Label lblsalesrateor = (Label)item.FindControl("lblsalesrateor");
                    Label lblserviceId = (Label)item.FindControl("lblserviceId");

                    Label lblgtotal = (Label)item.FindControl("lblgtotal");

                    if (Temp3val.Length > 0)
                    {
                        Temp3val += ",";
                    }

                    Temp3val += "('" + ViewState["CID"] + "','" + dtsin.Rows[0]["Whid"] + "','" + lblgnote.Text + "','" + lblgqty.Text + "','" + lblgrate.Text + "','" + lblgtotal.Text + "','" + Id1 + "','" + lbljid.Text + "','" + lblsalesrateor.Text + "','" + lblserviceId.Text + "')";
                }
                if (Temp3val.Length > 0)
                {
                    Temp3 += Temp3val;
                    SqlCommand cd4 = new SqlCommand(Temp3, con);
                    cd4.CommandType = CommandType.Text;
                    cd4.Transaction = transaction;
                    cd4.ExecuteNonQuery();
                }
               
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
                transuc = true;

                try
                {
                    DataTable dsEmp1 = select("SELECT  Max(Cast (SalesOrderNo as int)) as SalesOrderNo  from SalesOrderMaster inner join Party_master on Party_master.PartyID=SalesOrderMaster.PartyId where Party_master.Whid='" + ViewState["Whid"] + "'");

                    int salno = 0;
                    if (dsEmp1.Rows[0]["SalesOrderNo"].ToString() != "")
                    {
                        salno = Convert.ToInt32(dsEmp1.Rows[0]["SalesOrderNo"]) + 1;
                    }
                    else
                    {
                        salno = 1;
                    }
                    string paytype = "";
                    if (Convert.ToString(dtsin.Rows[0]["InvoiceType"]) == "1")
                    {
                        paytype = "7";
                    }
                    else
                    {
                        paytype = "4";
                    }
                    string str2 = "insert into SalesOrderMaster(SalesOrderNo,SalesManId,PartyId,SalesOrderDate,BuyersPOno," +
                  " ShippersId,ExpextedDeliveryDate, " +
                 " PaymentsTerms,OtherTerms,ShippingCharges, " +
                 " HandlingCharges,OtherCharges,Discounts,GrossAmount,Tax1Id,Tax1Amt,Tax2Id,Tax2Amt,Tax3Id,Tax3Amt,TaxOption) " +
                 " values('" + salno + "'," + Session["UserId"] + ",'" + Convert.ToInt32(ViewState["PartyId"]) + "', " +
                   " '" +lblDate.Text + "','0','0','" + System.DateTime.Now.AddDays(4).ToString("MM/dd/yyyy") + "', " +
                  " '" + paytype + "','0','0','0','" + isdecimalornot(lbltotaltaxamt.Text) + "', " +
                   " '" + 0 + "','" + isdecimalornot(lblnetamt.Text) + "','" + Convert.ToString(lblt1acc.Text) + "','" + lbltax1amt.Text + "','" + Convert.ToString(lblt2acc.Text) + "','" + lbltax2amt.Text + "','" + Convert.ToString(lblt3acc.Text) + "','" + lbltax3amt.Text + "','" + 1 + "')";

                    SqlCommand cmd2 = new SqlCommand(str2, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd2.ExecuteNonQuery();
                    con.Close();

                    DataTable ds3 = select("SELECT  MAX(SalesOrderId) AS SalesOrderId,GrossAmount FROM   SalesOrderMaster  " +
                    " group by  SalesOrderId,GrossAmount order by SalesOrderId desc");

                    ViewState["ordernonew"] = Convert.ToInt32(ds3.Rows[0]["SalesOrderId"]);


                    DataTable dssaleno = select("select MAX(SalesNo) as SalesNo from SalesChallanMaster inner join Party_master on Party_master.PartyID=SalesChallanMaster.PartyID where Party_master.Whid='" + ViewState["Whid"] + "' ");

                    int saleno = 1;
                    if (dssaleno.Rows.Count > 0)
                    {
                        if (dssaleno.Rows[0]["SalesNo"].ToString() != "")
                        {
                            if (dssaleno.Rows[0]["SalesNo"].ToString() != "--")
                            {
                                saleno = Convert.ToInt32(dssaleno.Rows[0]["SalesNo"]);
                                saleno = saleno + 1;
                            }
                        }
                    }

                    string str = "INSERT INTO SalesChallanMaster " +
                                        " (SalesNo,RefSalesOrderId, PartyID, SalesChallanDatetime, ShipToAddress, BillToAddress, EntryTypeId) " +
                                      " VALUES     ('" + saleno + "','" + ViewState["ordernonew"] + "','" + ViewState["PartyId"] + "','" +lblDate.Text + "' ,'"+lblShipAddress.Text+"','"+lblBillAddress.Text+"',34)";
                    SqlCommand cmd = new SqlCommand(str, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                    SqlDataAdapter adp = new SqlDataAdapter("select max(SalesChallanMasterId) as DelNo from SalesChallanMaster", con);
                    DataTable ds = new DataTable();
                    adp.Fill(ds);
                    int ik = 1;
                    if (ds.Rows.Count > 0)
                    {
                        if (ds.Rows[0]["DelNo"].ToString() != "")
                        {
                            ik = Convert.ToInt32(ds.Rows[0]["DelNo"]);

                        }
                    }

                    ViewState["DcNo1"] = ik;
                    ViewState["DId"] = ik;
                    string str2q = "INSERT INTO SalesChallanDetail(SalesChallanMasterId, Notes) " +
                                " VALUES     ('" + Convert.ToInt32(ds.Rows[0]["DelNo"]) + "','')";
                    SqlCommand cmd2q = new SqlCommand(str2q, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd2q.ExecuteNonQuery();
                    con.Close();

                    //Session["userid"] = "1";
                    string str3q = "INSERT INTO SalesChallanMoreInfo " +
                              " (SalesChallanMasterId, ShippersId, ShippersShipOptionMasterId, ShippersTrackingNo, ShippingCost, ShippingPersonId, UserId,PurchaseOrder,Terms) " +
                               " VALUES     ('" + ViewState["DId"] + "','0','0','0','0','" + Session["UserId"] + "','" + Session["UserId"] + "','','')";
                    SqlCommand cmd3q = new SqlCommand(str3q, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd3q.ExecuteNonQuery();
                    con.Close();

                    string str2h = "insert into SalesOrderSuppliment(SalesOrderMasterId,AmountDue)" +
                 "values(" + Convert.ToInt32(ViewState["ordernonew"]) + ",'0')";
                    SqlCommand cmd2h = new SqlCommand(str2h, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd2h.ExecuteNonQuery();
                    con.Close();

                    SqlCommand cmd19 = new SqlCommand("insert into SalesOrderMasterTamp(SalesOrderTempId,subTotal,CustDisc,OrderDisc,Tax,HandlingCharg,ShippingCharge,Total,userid,CustDiscId,OrderDiscId)  " +
               " values('" + Convert.ToInt32(ViewState["ordernonew"]) + "','" + isdecimalornot(lblGross.Text) + "','" +lblCustDisc.Text + "','" + lblOrderDisc.Text + "','" + isdecimalornot(lbltotaltaxamt.Text) + "','0','0','" + isdecimalornot(lblnetamt.Text) + "','" + Session["Userid"] + "','" + Convert.ToString(ViewState["Pcid"]) + "','" + Convert.ToString(ViewState["OrderDesc"]) + "')", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd19.ExecuteNonQuery();
                    con.Close();

                    foreach (GridViewRow item in GridView1.Rows)
                    {

                        Label lblsalesrate = (Label)item.FindControl("lblsalesrate");
                        Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
                        //Label lblchargestobe = (Label)item.FindControl("lblchargestobe");

                        Label lblpname = (Label)item.FindControl("lblpname");
                        Label lblserviceId = (Label)item.FindControl("lblserviceId");
                        Label lblunit = (Label)item.FindControl("lblunit");

                        DataTable dinvb = select("SELECT distinct  InventoruSubSubCategory.InventorySubSubId,InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                          " FROM         InventorySubCategoryMaster INNER JOIN " +
                        " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId INNER JOIN " +
                        " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where " +
                        "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId='" + lblserviceId.Text + "' ");
                        if (dinvb.Rows.Count > 0)
                        {
                            string str50 = "insert into SalesOrderDetail(SalesOrderMasterId,categorySubSubId, " +
                             " InventoryWHM_Id,Qty,Rate,Amount,Quality,Notes) " +
                             " values('" + ViewState["ordernonew"] + "', " +
                             " '" + Convert.ToInt32(dinvb.Rows[0]["InventorySubSubId"]) + "', " +
                             " '" + Convert.ToInt32(dinvb.Rows[0]["InventoryWarehouseMasterId"]) + "', " +
                             " '" + lblunit.Text + "','" + lblsalesrate.Text + "', " +
                             " '" + lblchargestobe.Text + "','1','Project Inv')";


                            SqlCommand cmd50 = new SqlCommand(str50, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd50.ExecuteNonQuery();
                            con.Close();

                            string str1 = "INSERT INTO SalesChallanTransaction " +
                        " (SalesChallanMasterId, inventoryWHM_Id, Quantity, Note) " +
                      " VALUES('" + ViewState["DId"] + "','" + Convert.ToInt32(lblserviceId.Text) + "','" + Convert.ToDecimal(lblunit.Text) + "','Project Inv')";
                            SqlCommand cmd1 = new SqlCommand(str1, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd22 = new SqlCommand("insert into SalesOrderDetailTemp(SalesOrderTempId,InventoryWHM_Id, " +
                             " item,Price,Qty,PromDisc,VolDisc,HandlingCharg,Total,Promorate,Bulkrate)" +
                             " values('" + ViewState["ordernonew"] + "','" + Convert.ToInt32(lblserviceId.Text) + "','" + Convert.ToString(lblpname.Text) + "','" + lblsalesrate.Text + "', " +
                             " '" + Convert.ToDecimal(lblunit.Text) + "','0','0','0','" + lblchargestobe.Text + "','0','0')", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd22.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    foreach (GridViewRow item in GridView2.Rows)
                    {

                        Label lblhor = (Label)item.FindControl("lblhor");

                        Label lblemprare = (Label)item.FindControl("lblemprare");
                        Label lblsalesrate = (Label)item.FindControl("lblsalesrate");
                        Label lblcharges = (Label)item.FindControl("lblcharges");

                        Label lblchargestobe = (Label)item.FindControl("lblchargestobe");
                        Label lblserviceId = (Label)item.FindControl("lblserviceId");
                        Label lblservice = (Label)item.FindControl("lblservice");
                        DataTable dinvb = select("SELECT distinct  InventoruSubSubCategory.InventorySubSubId,InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                          " FROM         InventorySubCategoryMaster INNER JOIN " +
                        " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId INNER JOIN " +
                        " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where  " +
                        "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId='" + lblserviceId.Text + "' ");
                        if (dinvb.Rows.Count > 0)
                        {
                            decimal bhour = 0;
                            string[] separbm = new string[] { ":" };
                            string[] strSplitArrbm = lblhor.Text.Split(separbm, StringSplitOptions.RemoveEmptyEntries);
                            bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));



                            string str50 = "insert into SalesOrderDetail(SalesOrderMasterId,categorySubSubId, " +
                            " InventoryWHM_Id,Qty,Rate,Amount,Quality,Notes) " +
                            " values('" + ViewState["ordernonew"] + "', " +
                            " '" + Convert.ToInt32(dinvb.Rows[0]["InventorySubSubId"]) + "', " +
                            " '" + Convert.ToInt32(dinvb.Rows[0]["InventoryWarehouseMasterId"]) + "', " +
                            " '" + bhour + "','" + lblsalesrate.Text + "', " +
                            " '" + lblchargestobe.Text + "','1','Labour Service')";


                            SqlCommand cmd50 = new SqlCommand(str50, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd50.ExecuteNonQuery();
                            con.Close();
                            string str1 = "INSERT INTO SalesChallanTransaction " +
                         " (SalesChallanMasterId, inventoryWHM_Id, Quantity, Note) " +
                       " VALUES('" + ViewState["DId"] + "','" + Convert.ToInt32(lblserviceId.Text) + "','" + Convert.ToDecimal(bhour) + "','Project Inv')";
                            SqlCommand cmd1 = new SqlCommand(str1, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd22 = new SqlCommand("insert into SalesOrderDetailTemp(SalesOrderTempId,InventoryWHM_Id, " +
                             " item,Price,Qty,PromDisc,VolDisc,HandlingCharg,Total,Promorate,Bulkrate)" +
                             " values('" + ViewState["ordernonew"] + "','" + Convert.ToInt32(lblserviceId.Text) + "','" + lblservice.Text + "','" + lblsalesrate.Text + "', " +
                             " '" + Convert.ToDecimal(bhour) + "','0','0','0','" + lblchargestobe.Text + "','0','0')", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd22.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    foreach (GridViewRow item in GridView3.Rows)
                    {

                        Label lblgnote = (Label)item.FindControl("lblgnote");
                        Label lblgqty = (Label)item.FindControl("lblgqty");
                        Label lbljid = (Label)item.FindControl("lbljid");
                        Label lblgrate = (Label)item.FindControl("lblgrate");
                        Label lblsalesrateor = (Label)item.FindControl("lblsalesrateor");
                        Label lblserviceId = (Label)item.FindControl("lblserviceId");
                        Label lblservice = (Label)item.FindControl("lblservice");
                        Label lblgtotal = (Label)item.FindControl("lblgtotal");
                        DataTable dinvb = select("SELECT distinct  InventoruSubSubCategory.InventorySubSubId,InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                          " FROM         InventorySubCategoryMaster INNER JOIN " +
                        " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId INNER JOIN " +
                        " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where  " +
                        "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId='" + lblserviceId.Text + "' ");
                        if (dinvb.Rows.Count > 0)
                        {
                            string str50 = "insert into SalesOrderDetail(SalesOrderMasterId,categorySubSubId, " +
                            " InventoryWHM_Id,Qty,Rate,Amount,Quality,Notes) " +
                            " values('" + ViewState["ordernonew"] + "', " +
                            " '" + Convert.ToInt32(dinvb.Rows[0]["InventorySubSubId"]) + "', " +
                            " '" + Convert.ToInt32(dinvb.Rows[0]["InventoryWarehouseMasterId"]) + "', " +
                            " '" + lblgqty.Text + "','" + lblgrate.Text + "', " +
                            " '" + lblgtotal.Text + "','1','Other Service')";


                            SqlCommand cmd50 = new SqlCommand(str50, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd50.ExecuteNonQuery();
                            con.Close();
                            string str1 = "INSERT INTO SalesChallanTransaction " +
                        " (SalesChallanMasterId, inventoryWHM_Id, Quantity, Note) " +
                      " VALUES('" + ViewState["DId"] + "','" + Convert.ToInt32(lblserviceId.Text) + "','" + Convert.ToDecimal(lblgqty.Text) + "','Project Inv')";
                            SqlCommand cmd1 = new SqlCommand(str1, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd22 = new SqlCommand("insert into SalesOrderDetailTemp(SalesOrderTempId,InventoryWHM_Id, " +
                             " item,Price,Qty,PromDisc,VolDisc,HandlingCharg,Total,Promorate,Bulkrate)" +
                             " values('" + ViewState["ordernonew"] + "','" + Convert.ToInt32(lblserviceId.Text) + "','" + Convert.ToString(lblservice.Text) + "','" + lblgrate.Text + "', " +
                             " '" + Convert.ToDecimal(lblgqty.Text) + "','0','0','0','" + lblgtotal.Text + "','0','0')", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd22.ExecuteNonQuery();
                            con.Close();
                        }
                    }


                    string strNewDCndInvRelation = "INSERT INTO TransactionMasterSalesChallanTbl  (TransactionMasterId, SalesChallanMasterId, " +
                     " SalesOrderMasterId) VALUES     " +
                     " ('" + Id1 + "', '" + Convert.ToInt32(ViewState["DcNo1"]) + "', '" + Convert.ToInt32(ViewState["ordernonew"]) + "')";
                    SqlCommand cmMore1 = new SqlCommand(strNewDCndInvRelation, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmMore1.ExecuteNonQuery();
                    con.Close();
                    SqlCommand cmMore = new SqlCommand("insert into TransactionMasterMoreInfo(SalesOrderId,Tranction_Master_Id) values('" + Convert.ToInt32(ViewState["ordernonew"]) + "','" + Id1 + "')", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmMore.ExecuteNonQuery();
                    con.Close();

                    if (paytype == "4")
                    {
                        string str345 = "INSERT INTO TranctionMasterSuppliment " +
                                   " (Tranction_Master_Id,AmountDue ,Party_MasterId,GrnMaster_Id)" +
                                    "VALUES           ('" + Id1 + "','" + lblnetamt.Text + "' ,'" + ViewState["PartyId"] + "','" +lblDate.Text + "' ) ";
                        SqlCommand cmMore34 = new SqlCommand(str345, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmMore34.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {

                        string str345 = "INSERT INTO TranctionMasterSuppliment " +
                                   " (Tranction_Master_Id,AmountDue ,Party_MasterId,GrnMaster_Id)" +
                                    "VALUES           ('" + Id1 + "','0' ,'" + ViewState["PartyId"] + "','" +lblDate.Text + "' ) ";
                        SqlCommand cmMore34 = new SqlCommand(str345, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmMore34.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmdinspay = new SqlCommand("Insert into PaymentApplicationTbl values('" + Id1 + "','" + Id1 + "','" + lblnetamt.Text.ToString() + "','" + lblDate.Text + "','0','" + ViewState["ordernonew"] + "')", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdinspay.ExecuteNonQuery();
                        con.Close();
                   
                    }

                    DataTable dt9982 = select("Select * from PaymentApplicationTbl where SalesOrderId='" + Convert.ToInt32(ViewState["ordernonew"])+"'");

                    if (dt9982.Rows.Count > 0)
                    {


                        SqlCommand cmm09 = new SqlCommand("INSERT INTO StatusControl(SalesOrderId,  Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                                                " VALUES('" + ViewState["ordernonew"] + "','" + System.DateTime.Now + "','30','Project invoice Entry',  '" + Id1 + "') ", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmm09.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        //SqlCommand cmm09x = new SqlCommand("INSERT INTO StatusControl(SalesOrderId,  Datetime, StatusMasterId,  note,TranctionMasterId) " +
                        //                                       " VALUES('" + ViewState["ordernonew"] + "','" + System.DateTime.Now + "','21','crd up',  '" + Id1 + "') ", con);
                        //if (con.State.ToString() != "Open")
                        //{
                        //    con.Open();
                        //}
                        //cmm09x.ExecuteNonQuery();
                        //con.Close();
                        SqlCommand cmm09 = new SqlCommand("INSERT INTO StatusControl(SalesOrderId,  Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                                              " VALUES('" + ViewState["ordernonew"] + "','" + System.DateTime.Now + "','28','Project invoice Entry',  '" + Id1 + "') ", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmm09.ExecuteNonQuery();
                        con.Close();
                    }
                       

                    //string statusmid = "";
                    //DataTable ds587 = select("SELECT    StatusId, StatusName, StatusCategoryMasterId FROM StatusMaster " +
                    //                " WHERE     (StatusName = 'Fully Shipped')");

                    //if (ds587.Rows.Count > 0)
                    //{
                    //    statusmid = ds587.Rows[0]["StatusId"].ToString();
                    //}

                    //string str95 = "    INSERT INTO StatusControl (StatusMasterId,Datetime  ,UserMasterId ,SalesOrderId, note) " +
                    //                " VALUES ('" + statusmid + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "','" + Session["UserId"] + "','" + ViewState["ordernonew"] + "','Project Order Master Entry')";
                    //SqlCommand cmd95 = new SqlCommand(str95, con);
                    //if (con.State.ToString() != "Open")
                    //{
                    //    con.Open();
                    //}
                    //cmd95.ExecuteNonQuery();
                    //con.Close();
                    SqlCommand cmd23 = new SqlCommand("insert into SalesOrderPaymentOption(SalesOrderId,PaymentMethodID) values('" + ViewState["ordernonew"] + "','" + paytype + "')", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd23.ExecuteNonQuery();
                    con.Close();
                    SqlCommand cmm = new SqlCommand("INSERT INTO StatusControl(SalesOrderId, SalesChallanMasterId, Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                       " VALUES('" + Convert.ToInt32(ViewState["ordernonew"]) + "','" + Convert.ToInt32(ViewState["DcNo1"]) + "' ,'" + lblDate.Text + "','18','Project invoice','" + Id1.ToString() + "') ", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmm.ExecuteNonQuery();
                    con.Close();

                }
                catch (Exception exe)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Error :" + exe.Message;
                }

                double amtavgcost = 0;
                foreach (GridViewRow item in GridView1.Rows)
                {
                    Label lblserviceId = (Label)item.FindControl("lblserviceId");
                    Label lblunit = (Label)item.FindControl("lblunit");
                    double FinalQtySub = 0;
                    double FinalQty = 0;
                   DataTable drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + lblserviceId.Text + "' and DateUpdated<='" + lblDate.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
                   if (drtinvdata.Rows.Count > 0)
                   {

                       FinalQtySub = Convert.ToDouble(lblunit.Text);
                       FinalQty = -(FinalQtySub);
                       decimal Totalavgcost = Convert.ToDecimal(drtinvdata.Rows[0]["AvgCost"]);
                       decimal Newqtyonhand = Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]) - Convert.ToDecimal(FinalQtySub);
                       amtavgcost += Convert.ToDouble(drtinvdata.Rows[0]["AvgCost"]) * FinalQtySub;
                       string insAvgCost = "INSERT INTO InventoryWarehouseMasterAvgCostTbl(InvWMasterId, AvgCost,DateUpdated,Qty,Tranction_Master_Id,QtyonHand) VALUES ('" + Convert.ToInt32(lblserviceId.Text) + "','" + Totalavgcost + "','" + lblDate.Text + "','" + FinalQty + "','" + Id1.ToString() + "','" + Newqtyonhand + "')";

                       SqlCommand cmd123123 = new SqlCommand(insAvgCost, con);
                       if (con.State.ToString() != "Open")
                       {
                           con.Open();
                       }
                       cmd123123.ExecuteNonQuery();
                       con.Close();

                       DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + lblserviceId.Text + "' and DateUpdated>'" + lblDate.Text + "' order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
                       decimal changeTotalavgcost = Totalavgcost;
                       decimal changeTotalonhand = Newqtyonhand;
                       decimal Finalqtyhand = Newqtyonhand;
                       foreach (DataRow itm in Dataupval.Rows)
                       {
                           string gupd = "";
                           string gupd1 = "";
                           string manul = "";
                           if (Convert.ToString(itm["Rate"]) == "" && Convert.ToDecimal(itm["Qty"]) < 0)
                           {

                               decimal newamt = 0;
                               DataTable drsv = select("Select AmountDebit from Tranction_Details where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "' ");
                               if (drsv.Rows.Count > 0)
                               {
                                   newamt = Convert.ToDecimal(drsv.Rows[0]["AmountDebit"]);
                               }

                               changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                               changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                               Finalqtyhand = changeTotalonhand;
                               decimal appn = (changeTotalavgcost) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                               decimal appold = Convert.ToDecimal(itm["AvgCost"]) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                               newamt = newamt + appn - (appold);
                               newamt = Math.Round(newamt, 2);
                               gupd = "Update Tranction_Details Set AmountDebit='" + newamt + "' where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                               gupd1 = "Update Tranction_Details Set AmountCredit='" + newamt + "' where AccountCredit='8000' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                               manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";
                               SqlCommand cmdupcugs = new SqlCommand(gupd, con);
                               if (con.State.ToString() != "Open")
                               {
                                   con.Open();
                               }
                               cmdupcugs.ExecuteNonQuery();
                               con.Close();
                               SqlCommand cmdupcugsin = new SqlCommand(gupd1, con);
                               if (con.State.ToString() != "Open")
                               {
                                   con.Open();
                               }
                               cmdupcugsin.ExecuteNonQuery();
                               con.Close();

                           }
                           else if (Convert.ToString(itm["Tranction_Master_Id"]) != "")
                           {
                               Finalqtyhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                               if (Finalqtyhand > 0)
                               {
                                   changeTotalavgcost = ((changeTotalavgcost * changeTotalonhand) + (Convert.ToDecimal(itm["Qty"]) * Convert.ToDecimal(itm["Rate"]))) / Finalqtyhand;
                               }
                               changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                               changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                               manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";

                           }
                           if (manul.Length > 0)
                           {
                               SqlCommand cmdupinv = new SqlCommand(manul, con);
                               if (con.State.ToString() != "Open")
                               {
                                   con.Open();
                               }
                               cmdupinv.ExecuteNonQuery();
                               con.Close();
                           }

                       }
                   }
                }
                amtavgcost = Math.Round(amtavgcost, 2);
                string accdebiinv = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                              " ,DateTimeOfTransaction,compid,whid )" +
                              " VALUES('8003','" + amtavgcost + "'" +
                              "  , '" + Id1.ToString() + "','" + lblDate.Text + "','" + Session["comid"].ToString() + "','" + ViewState["Wared"] + "')";
                SqlCommand cdinvdeb = new SqlCommand(accdebiinv, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }

                cdinvdeb.ExecuteNonQuery();
                con.Close();
                string costgood = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                          " ,DateTimeOfTransaction,compid,whid)" +
                          " VALUES('8000','" + amtavgcost + "'" +
                          " ,'" + Id1.ToString() + "','" + lblDate.Text + "','" + Session["comid"].ToString() + "','" + ViewState["Wared"] + "')";

                SqlCommand cdcostgood = new SqlCommand(costgood, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cdcostgood.ExecuteNonQuery();
                con.Close();
            }

            catch (Exception ex)
            {
                transaction.Rollback();
                transuc = false;
                lblmsg.Visible = true;
                lblmsg.Text = "Error :" + ex.Message;
            }
            finally
            {
                con.Close();
            }
            if (transuc == true)
            {
                //Response.Redirect("ProductInvoiceReport.aspx?Tid=" + Id1);
                Button1.Enabled = false;
                if (CheckBox1.Checked == true)
                {
                    sendmail(Id1.ToString());
                }
                lblmsg.Text = "Record Apply Successfully";
                lblnetamt.Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(lblnetamt.Text), 2));
            }
        }
    }

    protected decimal isdecimalornot(string ck)
    {
        decimal ik = 0;
        try
        {
            ik = Convert.ToDecimal(ck);
            return ik;
        }
        catch
        {
            return ik;

        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Button1.Enabled = true;
        Button2.Enabled = false;
        Button3.Enabled = false;
    }
    public void sendmail(string tid)
    {
        try
        {


            DataTable dtorder = select("Select Distinct Party_master.Email, ProjectInvoiceType.Whid, JobMaster.JobName,JobMaster.Id,JobReferenceNo,Party_master.Contactperson,EntryNumber,Tranction_Amount,Convert(nvarchar,TranctionMaster.Date,101) as Date from [TranctionMaster] inner join ProjectInvoiceType on ProjectInvoiceType.TransactionId=TranctionMaster.Tranction_Master_Id inner join JobMaster on JobMaster.Id=ProjectInvoiceType.JobId inner join Party_master on Party_master.PartyID=JobMaster.PartyId where Tranction_Master_Id= '" + tid + "'");

                StringBuilder strorder = new StringBuilder();
                strorder.Append("<br><table width=\"100%\"> ");
                strorder.Append("<tr><td align=\"left\">Dear " + dtorder.Rows[0]["Contactperson"].ToString() + ",<br></td></tr>");

                if (dtorder.Rows.Count > 0)
                {

                    strorder.Append("<tr><td align=\"left\">We are sending you an invoice for the project that is listed below. <br></td></tr>");

                }
                strorder.Append("</table> ");
                string body = "<b>Project Name: </b>" + dtorder.Rows[0]["JobName"].ToString() + "<br>";
                body += "<b>Project Reference Number </b>" + dtorder.Rows[0]["JobReferenceNo"].ToString() + "<br>";
                body += "<b>Invoice Number </b>" + dtorder.Rows[0]["EntryNumber"].ToString() + "<br>";
                body += "<b>Invoice Date </b>" + dtorder.Rows[0]["Date"].ToString() + "<br>";

                body += "<b>Invoice Amount </b>" + dtorder.Rows[0]["Tranction_Amount"].ToString() + "<br><br>";

                string bodytext1 = "<a href=http://" + Request.Url.Host + "/Shoppingcart/Admin/ProductInvoiceReport.aspx?Tapid=" + ClsEncDesc.Encrypted(tid) + " target=_blank >Please click this link, for a detailed invoice</a>";
                string que = "<br>Or, copy and paste this URL into your internet browser to view the invoice.<br><br>";
                que += "http://" + Request.Url.Host + "/Shoppingcart/Admin/ProductInvoiceReport.aspx?Tapid=" + ClsEncDesc.Encrypted(tid);

                    string axx = "<br><br>If you have any questions, please contact us.";
                    axx += "<br><br>Thank you for your business.";
                    axx += "<br><br>Sincerely, < br > <br>";

                    string subti = "Your invoice for ";
                    DataTable dre = select("select  distinct CompanyWebsitMaster.Sitename,ContactPersonName,CompanyWebsitMaster.SiteUrl,CompanyWebsiteAddressMaster.Phone1, CompanyWebsiteAddressMaster.Email  from  CompanyWebsitMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.CompanyWebsiteMasterId=CompanyWebsitMaster.CompanyWebsiteMasterId where   CompanyWebsitMaster.WHId='" + dtorder.Rows[0]["Whid"].ToString() + "'");
                    if (dre.Rows.Count > 0)
                    {
                        subti +="("+ Convert.ToString(dre.Rows[0]["Sitename"])+")";
                        axx += Convert.ToString(dre.Rows[0]["Sitename"])+"<br>";
                        axx += Convert.ToString(dre.Rows[0]["ContactPersonName"]) + "<br>";
                        axx += Convert.ToString(dre.Rows[0]["Phone1"]) + "<br>";
                        axx += Convert.ToString(dre.Rows[0]["Email"]) + "<br>";
                    }
                    subti += "-(" + dtorder.Rows[0]["JobName"].ToString() + ")";
                    subti += "-(" + dtorder.Rows[0]["JobReferenceNo"].ToString() + ")";
                    subti += "-(" + dtorder.Rows[0]["Date"].ToString() + ")";
                    string strmal = "SELECT  OutGoingMailServer,WebMasterEmail,MasterEmailId, EmailMasterLoginPassword, AdminEmail, WHId " +
                                   " FROM  CompanyWebsitMaster WHERE     (WHId = " + Convert.ToInt32(dtorder.Rows[0]["Whid"].ToString()) + ") ";
                    SqlCommand cmdma = new SqlCommand(strmal, con);
                    SqlDataAdapter adpma = new SqlDataAdapter(cmdma);
                    DataTable dtma = new DataTable();
                    adpma.Fill(dtma);
                    if (dtma.Rows.Count > 0)
                    {

                        string AdminEmail = dtma.Rows[0]["MasterEmailId"].ToString();// TextAdminEmail.Text;

                        String Password = dtma.Rows[0]["EmailMasterLoginPassword"].ToString();// TextEmailMasterLoginPassword.Text;
                        System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress(AdminEmail);

                        System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(Convert.ToString(dtorder.Rows[0]["Email"]));
                        System.Net.Mail.MailMessage objEmail = new System.Net.Mail.MailMessage(from, to);
                        //emn = "<span style=\"color: #996600\">You are receiving this email as you are on the send list: Regarding lateness at work </span><b>" + empname + "</b><br>";



                        string addd = strorder + body + bodytext1 + que + axx;
                        objEmail.Subject = subti;
                        objEmail.Body = addd;
                        objEmail.IsBodyHtml = true;


                        objEmail.Priority = System.Net.Mail.MailPriority.High;
                        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

                        client.Credentials = new NetworkCredential(AdminEmail, Password);
                        client.Host = dtma.Rows[0]["OutGoingMailServer"].ToString(); //TextOutGoingMailServer.Text;

                        client.Send(objEmail);
                    }
                
            
        }
        catch (Exception e)
        {

        }
    }


    public void CountCustomerDisc()
    {
        Session["custdis"] = "0.00";
          lblcusdisname.Text = "";
          DataTable ds =select( "SELECT     PartyTypeCategoryMasterTbl.IsPercentage,PartyCategoryName, PartyTypeCategoryMasterTbl.PartyCategoryDiscount, PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId " +
                        " FROM         Party_master INNER JOIN " +
                         " PartyTypeDetailTbl ON Party_master.PartyID = PartyTypeDetailTbl.PartyID INNER JOIN " +
                          " User_master ON Party_master.PartyID = User_master.PartyID INNER JOIN " +
                         " PartyTypeCategoryMasterTbl ON PartyTypeDetailTbl.PartyTypeCategoryMasterId = PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId " +
                " WHERE     (User_master.PartyID = " + ViewState["PartyId"] + ") AND (PartyTypeCategoryMasterTbl.Active = 1) " +
                " ORDER BY PartyTypeCategoryMasterTbl.EntryDate DESC ");
          
           

            if (ds.Rows.Count > 0)
            {
                ViewState["Pcid"] = ds.Rows[0]["PartyTypeCategoryMasterId"].ToString();
                int i = Convert.ToInt32(ds.Rows[0]["IsPercentage"]);

                if (i == 1)
                {
                    lblcusdisname.Text = Convert.ToString(ds.Rows[0]["PartyCategoryName"]) + "    " + ds.Rows[0]["PartyCategoryDiscount"] + "%";

                    double first = Convert.ToDouble(lblGross.Text);
                    double CustDis = (first * Convert.ToDouble(ds.Rows[0]["PartyCategoryDiscount"])) / 100;

                
                   
                    Session["custdis"] = Math.Round(CustDis, 2);

                }
                else
                {

                    double CustDis = Convert.ToDouble(ds.Rows[0]["PartyCategoryDiscount"]);
                    lblcusdisname.Text = Convert.ToString(ds.Rows[0]["PartyCategoryName"]) + "   $" + CustDis;
                

                    Session["custdis"] = Math.Round(CustDis, 2); 
                   
                }

               
            }
         
            lblCustDisc.Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(Session["custdis"]), 2));
    }
    protected double isdoubleornot(string ck)
    {
        double ick = 0;
        try
        {
            ick = Convert.ToDouble(ck);
            return ick;
        }
        catch
        {

        }
        return ick;

    }
    protected void ordervaldis()
    {
        lblorderdiscname.Text = "";
        DataTable ds1 = select("select * from OrderValueDiscountMaster where StartDate<='" +lblDate.Text + "' and EndDate>='" +lblDate.Text + "' and Active=1 and Whid='" + ViewState["Whid"] + "' and ApplyRetailSales='1' order by OrderValueDiscountMasterId Desc");
        if (ds1.Rows.Count > 0)
        {
            ViewState["OrderDesc"] = ds1.Rows[0]["OrderValueDiscountMasterId"].ToString();
            double Gtotal = Convert.ToDouble(lblGross.Text);
            double minvalu = Convert.ToDouble(ds1.Rows[0]["MinValue"].ToString());
            double maxvalu = Convert.ToDouble(ds1.Rows[0]["MaxValue"].ToString());
            double discount = Convert.ToDouble(ds1.Rows[0]["ValueDiscount"].ToString());
            string checkper = Convert.ToString(ds1.Rows[0]["IsPercentage"].ToString());
            if (Gtotal >= minvalu && Gtotal <= maxvalu)
            {
                if (checkper == "False")
                {
                    lblOrderDisc.Text = Convert.ToString( Math.Round(discount, 2) );
                    lblorderdiscname.Text = Convert.ToString(ds1.Rows[0]["SchemeName"]) + "    $" + discount;
                }
                else
                {
                    lblOrderDisc.Text = Convert.ToString(((Gtotal) * (discount)) / 100);
                    lblOrderDisc.Text = Math.Round(Convert.ToDecimal(lblOrderDisc.Text), 2).ToString();
                    lblorderdiscname.Text = Convert.ToString(ds1.Rows[0]["SchemeName"]) + "     " + discount + "%";
                }
            }
            else
            {
                lblOrderDisc.Text = "0.00";
            }

        }
        else
        {
            lblOrderDisc.Text = "0.00";
        }
        lblOrderDisc.Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(lblOrderDisc.Text), 2));
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectInvoice.aspx?Tid=" + ClsEncDesc.Encrypted(ViewState["TID"].ToString()).Replace("+","@"));
    }
}
