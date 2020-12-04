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

public partial class ShoppingCart_Admin_JobManage : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            fillstore();
            //FillParty();
            fillstatus();
            filljob();
            fillgrid();
            Fillitem();
            fillwarehouse();
            datefill();
            fillfilterstatus();
            FillfilterParty();




        }
    }


    protected void fillstore()
    {
        string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "'and [WareHouseMaster].Status='1' order by Name";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlStoreName.DataSource = ds;
        ddlStoreName.DataTextField = "Name";
        ddlStoreName.DataValueField = "WareHouseId";
        ddlStoreName.DataBind();
        ddlStoreName.SelectedIndex = ddlStoreName.Items.IndexOf(ddlStoreName.Items.FindByValue(""));
    }

    protected void Fillitem()
    {
        string str1234 = "SELECT   InventoryWarehouseMasterTbl.InventoryWarehouseMasterId ,  LEFT(InventoryCategoryMaster.InventoryCatName, 8) + ' : ' + LEFT(InventorySubCategoryMaster.InventorySubCatName, 8) " +
                     "     + ' : ' + LEFT(InventoruSubSubCategory.InventorySubSubName, 8) + ' : ' + InventoryMaster.Name AS Name, InventoryMaster.InventoryMasterId " +
                  "  FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
                   "      InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId LEFT OUTER JOIN " +
                   "      InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
                   "      InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId inner join InventoryWarehouseMasterTbl  on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId " +
                 " WHERE  (InventoryWarehouseMasterTbl.WareHouseId='" + ddlStoreName.SelectedValue + "') and (InventoryCategoryMaster.CatType IS NULL) and  (InventoryMaster.MasterActiveStatus = 1) and ( InventoryCategoryMaster.compid='" + Session["comid"] + "') order by InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName";
        SqlCommand cmd = new SqlCommand(str1234, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);


        ddlmainitem.DataSource = ds;
        ddlmainitem.DataTextField = "Name";
        ddlmainitem.DataValueField = "InventoryWarehouseMasterId";
        ddlmainitem.DataBind();
        ddlmainitem.Items.Insert(0, "-Select-");

    }
    protected void filljob()
    {

        //string str = " select JobMaster.*,Party_master.Compname,StatusMaster.StatusName from JobMaster inner join [Party_master]  on [Party_master].PartyID=JobMaster.PartyId  inner join StatusMaster on [StatusMaster].[StatusId]=[JobMaster].StatusId  WHERE JobMaster.Whid='" + ddlStoreName.SelectedValue + "' order by StatusId";
        string str = " select * from JobMaster where Whid='" + ddlStoreName.SelectedValue + "' order by StatusId ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddljobname.DataSource = ds;
        ddljobname.DataTextField = "JobName";
        ddljobname.DataValueField = "Id";
        ddljobname.DataBind();

       

    }
    protected void fillstatus()
    {

        string str = " select * from StatusMaster where StatusCategoryMasterId='165' order by StatusId";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlStatus.DataSource = ds;
        ddlStatus.DataTextField = "StatusName";
        ddlStatus.DataValueField = "StatusId";
        ddlStatus.DataBind();

    }

    protected void fillgrid()
    {
        //string str = " Select JobMaster.* , StatusMaster.StatusId , StatusMaster.StatusName ,Party_master.Compname from JobMaster inner join Party_master on Party_master.PartyID=JobMaster.PartyId inner join StatusMaster on StatusMaster.StatusId=JobMaster.StatusId WHERE compid='" + Session["Comid"].ToString() + "'";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        //GridView1.DataSource = ds;

        //GridView1.DataBind();

    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
           
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = GridView1.SelectedDataKey.Value;
            Panel1.Visible = true;

            string materialin = "";
            double Qty = 0;
            double Rate = 0;
            double TotalCost = 0;
            double FinalCost = 0;

            //material issue id
            string avgmaterial = "select * from MaterialIssueMasterTbl where JobMasterId='" + ViewState["Id"] + "'";
            SqlCommand cmdavgmat = new SqlCommand(avgmaterial, con);
            SqlDataAdapter adpavgmat = new SqlDataAdapter(cmdavgmat);
            DataTable dsavgma = new DataTable();
            adpavgmat.Fill(dsavgma);
            if (dsavgma.Rows.Count > 0)
            {

                string strId = "";
                string strInvAllIds = "";
                string strtemp = "";
                foreach (DataRow dtrrr in dsavgma.Rows)
                {
                    strId = dtrrr["Id"].ToString();
                    strInvAllIds = strId + "," + strInvAllIds;
                    strtemp = strInvAllIds.Substring(0, (strInvAllIds.Length - 1));
                }

                materialin = " InventoryWarehouseMasterAvgCostTbl.MaterialIssueMasterTblId In (" + strtemp + ") and";

            }
            //direct material

            string st123 = "select * from InventoryWarehouseMasterAvgCostTbl where " + materialin + "  Qty Is Not Null ";
            SqlCommand cmd123 = new SqlCommand(st123, con);
            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
            DataTable dt123 = new DataTable();
            adp123.Fill(dt123);
            if (dt123.Rows.Count > 0)
            {
                foreach (DataRow dtp in dt123.Rows)
                {
                    Qty = Convert.ToDouble(dtp["Qty"].ToString());
                    if (Qty > 0)
                    {
                        Rate = Convert.ToDouble(dtp["Rate"].ToString());
                        TotalCost = Qty * Rate;
                        FinalCost += TotalCost;
                    }
                }
            }
            lblTotMaterialCost.Text = FinalCost.ToString();

            double LabourCost = 0;
            double FinalLabourCost = 0;
            double LabourCost1 = 0;
            double FinalLabourCost1 = 0;

           // string st454 = " select * from JobEmployeeDailyTaskTbl inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobEmployeeDailyTaskTbl.Id where JobEmployeeDailyTaskDetail.JobMasterId='" + ViewState["Id"] + "' ";
            string st454 = "select JobEmployeeDailyTaskDetail.* from JobEmployeeDailyTaskDetail where JobMasterId='" + ViewState["Id"] + "' ";
            SqlCommand cmd454 = new SqlCommand(st454, con);
            SqlDataAdapter adp454 = new SqlDataAdapter(cmd454);
            DataTable dt454 = new DataTable();
            adp454.Fill(dt454);
            if (dt454.Rows.Count > 0)
            {
                foreach (DataRow dt4 in dt454.Rows)
                {
                    LabourCost1 = Convert.ToDouble(dt4["Cost"].ToString());

                    FinalLabourCost1 += LabourCost1;
                }


            }
            lbltotallabour.Text = FinalLabourCost1.ToString();

           

            double OhAllocationTotal = 0;
            double OhAllocationTotalFinal = 0;




            string st456 = " select * from OverHeadAllocationJobDetail where JobMasterId='" + ViewState["Id"] + "'  ";
            SqlCommand cmd456 = new SqlCommand(st456, con);
            SqlDataAdapter adp456 = new SqlDataAdapter(cmd456);
            DataTable dt456 = new DataTable();
            adp456.Fill(dt456);
            if (dt456.Rows.Count > 0)
            {
                foreach (DataRow dt15636 in dt456.Rows)
                {
                    OhAllocationTotal = Convert.ToDouble(dt15636["OhAllocationtotal"].ToString());

                    OhAllocationTotalFinal += OhAllocationTotal;
                    
                    
                }
 
            }

            lbltotaloverhead.Text = OhAllocationTotalFinal.ToString();

            




            edit();
            fillgrdoutput();


        }
    }


   
    protected void edit()
    {
        Panel2.Visible = true;
        string str1 = "  Select JobMaster.* , StatusMaster.StatusId , StatusMaster.StatusName ,Party_master.Contactperson from JobMaster inner join Party_master on Party_master.PartyID=JobMaster.PartyId inner join StatusMaster on StatusMaster.StatusId=JobMaster.StatusId where  JobMaster.Id='" + ViewState["Id"] + "'";

        SqlCommand cmd = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        adp.Fill(dt);

        ddlStoreName.SelectedValue = dt.Rows[0]["Whid"].ToString();
        filljob();

        ddljobname.SelectedIndex = ddljobname.Items.IndexOf(ddljobname.Items.FindByValue(dt.Rows[0]["Id"].ToString()));
        

        lblJobNo.Text = dt.Rows[0]["JobNumber"].ToString();
        lblReferenceNo.Text = dt.Rows[0]["JobReferenceNo"].ToString();
        lblPartyName.Text = dt.Rows[0]["Contactperson"].ToString();

        ddlStatus.SelectedValue = dt.Rows[0]["StatusId"].ToString();
        lblJobStart.Text = dt.Rows[0]["JobStartDate"].ToString();
        lblEndDate.Text = dt.Rows[0]["JobEndDate"].ToString();
        
    }

    
    protected void btnadd_Click(object sender, EventArgs e)
    {
        String str = "Insert Into JobOutputTbl (JobMasterTblId,InvWMasterId,BatchNo,ActualQty,ReferenceNumber,Note,Date,ExpiryDate)values('" + ddljobname.SelectedValue + "','" + ddlmainitem.SelectedValue + "','" + txtbatchno.Text + "','" + TextBox17.Text + "','" + TextBox18.Text + "','" + TextBox19.Text + "','" + txtissuedate.Text + "','" + txtexpiry.Text + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        fillgrdoutput();
        Button1.Visible = true;
        Button2.Visible = true;
        

        foreach (GridViewRow gdr in grdjob.Rows)
        {
            double mcallocationamount = 0;
            double lcallocationamount = 0;
            double ohallocationamount = 0;
            double TotalCostofoutputtemp = 0;
            double TotalProductionCostperunit = 0;
            

            TextBox txtactual123 = (TextBox)gdr.FindControl("txtactual123");

            TextBox txtmaterialcostallocation = (TextBox)gdr.FindControl("txtmaterialcostallocation");
            TextBox txtmcamount = (TextBox)gdr.FindControl("txtmcamount");

            TextBox txtlabourcostallocation = (TextBox)gdr.FindControl("txtlabourcostallocation");
            TextBox txtlabourcostamount = (TextBox)gdr.FindControl("txtlabourcostamount");

            TextBox txtoaallocationpercent = (TextBox)gdr.FindControl("txtoaallocationpercent");
            TextBox txtoaamount = (TextBox)gdr.FindControl("txtoaamount");

            TextBox txttotalcost123 = (TextBox)gdr.FindControl("txttotalcost123");
            TextBox txttotalcostperunit123 = (TextBox)gdr.FindControl("txttotalcostperunit123");

            
                


            string str1 = "select count(Id) as Id from  JobOutputTbl where JobMasterTblId='" + ddljobname.SelectedValue + "'";
            SqlCommand cmd12 = new SqlCommand(str1, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd12);

            DataTable dt = new DataTable();
            adp.Fill(dt);

            double percentallocation = 0;
            if (dt.Rows.Count > 0)
            {
                double countrow = Convert.ToDouble(dt.Rows[0]["Id"].ToString());
                percentallocation = (100 / countrow);

                txtmaterialcostallocation.Text = percentallocation.ToString();
                txtlabourcostallocation.Text = percentallocation.ToString();
                txtoaallocationpercent.Text = percentallocation.ToString();

                mcallocationamount = ((Convert.ToDouble(lblTotMaterialCost.Text) * Convert.ToDouble(txtmaterialcostallocation.Text)) / 100);
                lcallocationamount = ((Convert.ToDouble(lbltotallabour.Text) * Convert.ToDouble(txtlabourcostallocation.Text)) / 100);
                ohallocationamount = ((Convert.ToDouble(lbltotaloverhead.Text) * Convert.ToDouble(txtoaallocationpercent.Text)) / 100);
                
                txtmcamount.Text = mcallocationamount.ToString();
                txtlabourcostamount.Text = lcallocationamount.ToString();
                txtoaamount.Text = ohallocationamount.ToString();

                TotalCostofoutputtemp = mcallocationamount + lcallocationamount + ohallocationamount;
                txttotalcost123.Text = TotalCostofoutputtemp.ToString();

                

                TotalProductionCostperunit = ((TotalCostofoutputtemp) / (Convert.ToDouble(txtactual123.Text)));
                txttotalcostperunit123.Text = TotalProductionCostperunit.ToString();
            }

        }

    }

    protected void fillgrdoutput()
    {

        string str1 = " select JobOutputTbl.* ,InventoryMaster.Name from JobOutputTbl inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=JobOutputTbl.InvWMasterId inner join InventoryMaster on InventoryMaster.InventoryMasterId= InventoryWarehouseMasterTbl.InventoryMasterId where JobOutputTbl.JobMasterTblId='" + ddljobname.SelectedValue + "'  ";

        SqlCommand cmd = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        
        DataTable dt = new DataTable();
        adp.Fill(dt);

        grdjob.DataSource = dt;
        grdjob.DataBind();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        double mcallocation = 0;
        double lcallocation = 0;

        foreach (GridViewRow gdr in grdjob.Rows)

        {
            double mcallocationamount=0;
            double lcallocationamount = 0;


            Label lblitemname123 = (Label)gdr.FindControl("lblitemname123");

            Label lblstartdate123 = (Label)gdr.FindControl("lblstartdate123");

             TextBox txtexpect123 = (TextBox)gdr.FindControl("txtexpect123");
             TextBox txtactual123 = (TextBox)gdr.FindControl("txtactual123");
             TextBox txtreferenceno123 = (TextBox)gdr.FindControl("txtreferenceno123");
             TextBox txtnote123 = (TextBox)gdr.FindControl("txtnote123");

             TextBox txtmaterialcostallocation = (TextBox)gdr.FindControl("txtmaterialcostallocation");
             TextBox txtmcamount = (TextBox)gdr.FindControl("txtmcamount");

             TextBox txtlabourcostallocation = (TextBox)gdr.FindControl("txtlabourcostallocation");
             TextBox txtlabourcostamount = (TextBox)gdr.FindControl("txtlabourcostamount");

             TextBox txtoaallocationpercent = (TextBox)gdr.FindControl("txtoaallocationpercent");
             TextBox txtoaamount = (TextBox)gdr.FindControl("txtoaamount");

             TextBox txttotalcost123 = (TextBox)gdr.FindControl("txttotalcost123");

             

             string str1 = "select count(Id) as Id from  JobOutputTbl where JobMasterTblId='" + ddljobname.SelectedValue + "'";
             SqlCommand cmd = new SqlCommand(str1, con);
             SqlDataAdapter adp = new SqlDataAdapter(cmd);
        
             DataTable dt = new DataTable();
             adp.Fill(dt);
             double percentallocation = 0;
             if(dt.Rows.Count>0)
             {
                double countrow= Convert.ToDouble(dt.Rows[0]["Id"].ToString());
                percentallocation = (100 / countrow);

             }
             

           

             


             //double mc1 = Convert.ToDouble(txtmaterialcostallocation.Text);
             //double lc1 = Convert.ToDouble(txtlabourcostallocation.Text);

             //mcallocation += mc1;
             //lcallocation += lc1;




             //if (mcallocation > 100 || lcallocation > 100)
             //{
             //    lblpercentmasg.Text = "Allocation Percent Cannot be greator then 100%";

             // }
             //else
             //{

                 //mcallocationamount = ((Convert.ToDouble(lblTotMaterialCost.Text) * Convert.ToDouble(txtmaterialcostallocation.Text)) / 100);
                 txtmcamount.Text = percentallocation.ToString();

                // lcallocationamount = ((Convert.ToDouble(lbltotallabour.Text) * Convert.ToDouble(txtlabourcostallocation.Text)) / 100);
                 txtlabourcostamount.Text = percentallocation.ToString();
             //}
            
 
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string materialin = "";
        double Qty = 0;
        double Rate = 0;
        double TotalCost = 0;
        double FinalCost = 0;

        double TotalQty = 0;
        double MatAvgRate = 0;
        
        foreach (GridViewRow gdr in grdjob.Rows)
        {
            



            Label lblitemmasterid = (Label)gdr.FindControl("lblitemmasterid");
            Label lblmasterid = (Label)gdr.FindControl("lblmasterid");
            Label lblitemname123 = (Label)gdr.FindControl("lblitemname123");

            Label lblstartdate123 = (Label)gdr.FindControl("lblstartdate123");

            TextBox txtbatchno123 = (TextBox)gdr.FindControl("txtbatchno123");
            TextBox txtactual123 = (TextBox)gdr.FindControl("txtactual123");
            TextBox txtreferenceno123 = (TextBox)gdr.FindControl("txtreferenceno123");
            TextBox txtnote123 = (TextBox)gdr.FindControl("txtnote123");
            TextBox txtexpirydate123 = (TextBox)gdr.FindControl("txtexpirydate123");
            

            TextBox txtmaterialcostallocation = (TextBox)gdr.FindControl("txtmaterialcostallocation");
            TextBox txtmcamount = (TextBox)gdr.FindControl("txtmcamount");

            TextBox txtlabourcostallocation = (TextBox)gdr.FindControl("txtlabourcostallocation");
            TextBox txtlabourcostamount = (TextBox)gdr.FindControl("txtlabourcostamount");

            TextBox txtoaallocationpercent = (TextBox)gdr.FindControl("txtoaallocationpercent");
            TextBox txtoaamount = (TextBox)gdr.FindControl("txtoaamount");

            TextBox txttotalcost123 = (TextBox)gdr.FindControl("txttotalcost123");
            TextBox txttotalcostperunit123 = (TextBox)gdr.FindControl("txttotalcostperunit123");



            String str = "Update  JobOutputTbl  set BatchNo='" + txtbatchno123.Text + "',ActualQty='" + txtactual123.Text + "',ReferenceNumber='" + txtreferenceno123.Text + "',Note='" + txtnote123.Text + "',Date='" + lblstartdate123.Text + "',ExpiryDate='" + txtexpirydate123.Text + "',MaterialCostAllocationPercent='" + txtmaterialcostallocation.Text + "',MaterialCostAmount='" + txtmcamount.Text + "',LabourCostAllocationPercent='" + txtlabourcostallocation.Text + "',LabourCostAmount='" + txtlabourcostamount.Text + "',OverHeadAllocationPercent='" + txtoaallocationpercent.Text + "',OverHeadAllocationAmount='" + txtoaamount.Text + "',TotalProductionCost='" + txttotalcost123.Text + "',ProductionCostPerUnit='" + txttotalcostperunit123.Text + "' where Id='" + lblmasterid.Text + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            String avginsert = "Insert Into InventoryWarehouseMasterAvgCostTbl (InvWMasterId,AvgCost,DateUpdated,Qty,Rate,JobMasterId) values ('"+lblitemmasterid.Text+"','"+0+"','"+lblstartdate123.Text+"','"+txtactual123.Text+"','"+txttotalcostperunit123.Text+"','"+ddljobname.SelectedValue+"')";
            SqlCommand cmdavg = new SqlCommand(avginsert, con);
            con.Open();
            cmdavg.ExecuteNonQuery();
            con.Close();

            
           
            String updatestatus = "Update  JobMaster set StatusId='" + ddlStatus.SelectedValue + "'";
            SqlCommand cmdstatus = new SqlCommand(updatestatus, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();





        }

        string avgmaterial = "select * from MaterialIssueMasterTbl where JobMasterId='" + ViewState["Id"] + "'";
        SqlCommand cmdavgmat = new SqlCommand(avgmaterial, con);
        SqlDataAdapter adpavgmat = new SqlDataAdapter(cmdavgmat);
        DataTable dsavgma = new DataTable();
        adpavgmat.Fill(dsavgma);
        if (dsavgma.Rows.Count > 0)
        {

            string strId = "";
            string strInvAllIds = "";
            string strtemp = "";
            foreach (DataRow dtrrr in dsavgma.Rows)
            {
                strId = dtrrr["Id"].ToString();
                strInvAllIds = strId + "," + strInvAllIds;
                strtemp = strInvAllIds.Substring(0, (strInvAllIds.Length - 1));
            }

            materialin = " InventoryWarehouseMasterAvgCostTbl.MaterialIssueMasterTblId In (" + strtemp + ") and";

        }
        //direct material

        string st123 = "select * from InventoryWarehouseMasterAvgCostTbl where " + materialin + "  Qty Is Not Null ";
        SqlCommand cmd123 = new SqlCommand(st123, con);
        SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
        DataTable dt123 = new DataTable();
        adp123.Fill(dt123);


        if (dt123.Rows.Count > 0)
        {
            foreach (DataRow dtp in dt123.Rows)
            {
                Qty = Convert.ToDouble(dtp["Qty"].ToString());
                if (Qty > 0)
                {
                    Rate = Convert.ToDouble(dtp["Rate"].ToString());

                    TotalQty += Qty;

                    TotalCost = Qty * Rate;


                    FinalCost += TotalCost;

                }
            }
            double tq = -(TotalQty);

            MatAvgRate = FinalCost / TotalQty;

            string strjobinv = " select * from JobMaster where Id='" + ddljobname.SelectedValue + "'";
            SqlCommand cmdjobinv = new SqlCommand(strjobinv, con);
            SqlDataAdapter adpjob = new SqlDataAdapter(cmdjobinv);
            DataTable dtjob = new DataTable();
            adpjob.Fill(dtjob);


            String avgmatinsert = "Insert Into InventoryWarehouseMasterAvgCostTbl (InvWMasterId,AvgCost,DateUpdated,Qty,Rate,JobMasterId) values ('" + dtjob.Rows[0]["InvWMasterId"].ToString() + "','" + 0 + "','" + System.DateTime.Now.ToShortDateString() + "','" + tq + "','" + MatAvgRate + "','" + ddljobname.SelectedValue + "')";
            SqlCommand cmdmatins = new SqlCommand(avgmatinsert, con);
            con.Open();
            cmdmatins.ExecuteNonQuery();
            con.Close();

        }
        lblpercentmasg.Visible = true;
        lblpercentmasg.Text = "Record Inserted Successfully";

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (ddlfilterparty.SelectedIndex > 0)
        {
            string str = " Select JobMaster.* , StatusMaster.StatusId , StatusMaster.StatusName ,Party_master.Compname from JobMaster inner join Party_master on Party_master.PartyID=JobMaster.PartyId inner join StatusMaster on StatusMaster.StatusId=JobMaster.StatusId WHERE JobMaster.compid='" + Session["Comid"].ToString() + "' and JobMaster.StatusId='" + ddlfilterstatus.SelectedValue + "' and JobMaster.JobStartDate between '" + txtstartdate.Text + "' and '" + txtenddate.Text + "'  and JobMaster.Whid='" + ddlwarehouse.SelectedValue + "' and JobMaster.PartyId='" + ddlfilterparty.SelectedValue + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            GridView1.DataSource = ds;

            GridView1.DataBind();

        }
        else
        {
            string str = " Select JobMaster.* , StatusMaster.StatusId , StatusMaster.StatusName ,Party_master.Compname from JobMaster inner join Party_master on Party_master.PartyID=JobMaster.PartyId inner join StatusMaster on StatusMaster.StatusId=JobMaster.StatusId WHERE JobMaster.compid='" + Session["Comid"].ToString() + "' and JobMaster.StatusId='" + ddlfilterstatus.SelectedValue + "' and JobMaster.JobStartDate between '" + txtstartdate.Text + "' and '" + txtenddate.Text + "'  and JobMaster.Whid='" + ddlwarehouse.SelectedValue + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            GridView1.DataSource = ds;

            GridView1.DataBind();
 
        }
    }
    protected void datefill()
    {
        string openingdate = "select StartDate,EndDate from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Whid='" + Convert.ToInt32(ddlwarehouse.SelectedValue) + "' and Active='1'";
        SqlCommand cmd22221 = new SqlCommand(openingdate, con);
        SqlDataAdapter adp22221 = new SqlDataAdapter(cmd22221);
        DataTable ds112221 = new DataTable();
        adp22221.Fill(ds112221);

        if (ds112221.Rows.Count > 0)
        {
            DateTime t1;
            DateTime t2;

            t1 = Convert.ToDateTime(ds112221.Rows[0]["StartDate"].ToString());
            t2 = Convert.ToDateTime(ds112221.Rows[0]["EndDate"].ToString());

            txtstartdate.Text = t1.ToShortDateString();
            txtenddate.Text = t2.ToShortDateString();

        }

    }

    protected void fillwarehouse()
    {

        string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "'and [WareHouseMaster].Status='1' order by Name";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlwarehouse.DataSource = ds;
        ddlwarehouse.DataTextField = "Name";
        ddlwarehouse.DataValueField = "WareHouseId";
        ddlwarehouse.DataBind();

    }

    protected void fillfilterstatus()
    {

        string str = " select * from StatusMaster where StatusCategoryMasterId='165' order by StatusId";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlfilterstatus.DataSource = ds;
        ddlfilterstatus.DataTextField = "StatusName";
        ddlfilterstatus.DataValueField = "StatusId";
        ddlfilterstatus.DataBind();

    }
    protected void FillfilterParty()
    {
        string str = " SELECT     Party_master.PartyID, Party_master.Account, Party_master.Compname, " +
            " Party_master.Address, Party_master.City, Party_master.State, Party_master.Country,  " +
               "       User_master.Active, Party_master.PartyTypeId " +
              " FROM         Party_master LEFT OUTER JOIN " +
             "         User_master ON Party_master.PartyID = User_master.PartyID " +
           " WHERE     (User_master.Active = 1) and Party_master.Whid='" + ddlStoreName.SelectedValue + "' " +
            " ORDER BY Party_master.Compname ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlfilterparty.DataSource = ds;
        ddlfilterparty.DataTextField = "Compname";
        ddlfilterparty.DataValueField = "PartyID";
        ddlfilterparty.DataBind();
        ddlfilterparty.Items.Insert(0, "-Select-");

        ddlfilterparty.Items[0].Value = "0";

    }

    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillfilterParty();
    }
    protected void link1_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;


        Label lblsalesitemid = (Label)grdjob.Rows[rinrow].FindControl("lblsalesitemid");
        Label JobMasterTblId123 = (Label)grdjob.Rows[rinrow].FindControl("JobMasterTblId123");
        Label lblmasterid = (Label)grdjob.Rows[rinrow].FindControl("lblmasterid");
        Label lblitemname123 = (Label)grdjob.Rows[rinrow].FindControl("lblitemname123");
        //Label JobMasterTblId123 = (Label)grdjob.FindControl("JobMasterTblId123");
        //Label lblmasterid = (Label)grdjob.FindControl("lblmasterid");
       // Session["MasterId"] = lblmasterid.Text;
        string temp1 = lblmasterid.Text;
        string temp2=lblmasterid.Text;
        Session["MasterId"] = temp1;
       // Session["InvMid"] = lblsalesitemid.Text;
        Session["InvMid"] = temp2;


        string strjobmaster = "select * from JobMaster where Id='" + JobMasterTblId123.Text + "'";
        SqlCommand cmd123 = new SqlCommand(strjobmaster, con);
        SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
        DataTable dt123 = new DataTable();
        adp123.Fill(dt123);

        string tempdate = "";
        if (dt123.Rows.Count > 0)
        {
            tempdate = dt123.Rows[0]["JobStartDate"].ToString();
 
        }


        string str132 = " select SalesOrderDetail.*,TransactionMasterMoreInfo.*,SalesOrderMaster.*,TranctionMaster.EntryNumber,TranctionMaster.Tranction_Master_Id,Party_master.Compname,Party_master.PartyID from SalesOrderDetail inner join " +
                     " SalesOrderMaster on SalesOrderMaster.SalesOrderId=SalesOrderDetail.SalesOrderMasterId " +
                     " inner join TransactionMasterMoreInfo on TransactionMasterMoreInfo.SalesOrderId=SalesOrderMaster.SalesOrderId " +
                     " inner join Party_master on Party_master.PartyID=SalesOrderMaster.PartyId "+
                     " LEFT OUTER JOIN " +
                     " EntryTypeMaster " +
                     " RIGHT OUTER JOIN TranctionMaster ON EntryTypeMaster.Entry_Type_Id = TranctionMaster.EntryTypeId " +
                     " ON TransactionMasterMoreInfo.Tranction_Master_Id = TranctionMaster.Tranction_Master_Id " +
                  //   " where   InventoryWHM_Id='" + lblsalesitemid .Text+ "' and SalesOrderMaster.SalesOrderDate>'"+tempdate+"' ";
        " where   InventoryWHM_Id='" + 5823 + "'  ";

        SqlCommand cmd = new SqlCommand(str132, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        grdsalespopup.DataSource = dt;
        grdsalespopup.DataBind();
        lblitemname123.Text = lblitemname123.Text;
       
        foreach (GridViewRow gdr in grdsalespopup.Rows)
        {
            double tempsum = 0;
            
            Label lbltranmid123 = (Label)gdr.FindControl("lbltranmid123");
            Label lblsalesdate = (Label)gdr.FindControl("lblsalesdate");
            Label lbldate123 = (Label)gdr.FindControl("lbldate123");
            TextBox txtsalesqty = (TextBox)gdr.FindControl("txtsalesqty");
            TextBox txtqtysold = (TextBox)gdr.FindControl("txtqtysold");
            Label lblrate123 = (Label)gdr.FindControl("lblrate123");

            string str123456 = "select * from OutPutSalesorderMaster where Transaction_Master_Id='" + lbltranmid123.Text + "'";
            SqlCommand cmd132456 = new SqlCommand(str123456, con);
            SqlDataAdapter adp1332 = new SqlDataAdapter(cmd132456);
            DataTable dt13212 = new DataTable();
            adp1332.Fill(dt13212);
            if (dt13212.Rows.Count > 0)
            {
                double temp1123=0;
                foreach (DataRow dtr in dt13212.Rows)
                {
                    temp1123 = Convert.ToDouble(dt13212.Rows[0]["Qty"].ToString());
                    tempsum += temp1123;
                    txtsalesqty.Text = tempsum.ToString();
                }
                
                
 
            }

        }




      //  ModalPopupExtender2.Show();

        filloutgrid();
        

        
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
       // ModalPopupExtender2.Hide();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow gdr in grdsalespopup.Rows)
        {
 
             Label lbltranmid123 = (Label)gdr.FindControl("lbltranmid123");
             Label lblsalesdate = (Label)gdr.FindControl("lblsalesdate");
             Label lblqty123 = (Label)gdr.FindControl("lblqty123");
             TextBox txtsalesqty = (TextBox)gdr.FindControl("txtsalesqty");
             TextBox txtqtysold = (TextBox)gdr.FindControl("txtqtysold");
             Label lblrate123 = (Label)gdr.FindControl("lblrate123");
             Label lblpartymasterid = (Label)gdr.FindControl("lblpartymasterid");
            

            

             if (txtqtysold.Text == "")
             {


             }
             else
             {

                 String avgmatinsert = "Insert Into OutPutSalesorderMaster (JobOutPuttblId,Transaction_Master_Id,Qty,Inwh_Mid,Party_Id,ItemSold) values ('" + Session["MasterId"] + "','" + lbltranmid123.Text + "','" + txtqtysold.Text + "','" + Session["InvMid"] + "','" + lblpartymasterid.Text + "','" + lblqty123 .Text+ "')";
                 SqlCommand cmdmatins = new SqlCommand(avgmatinsert, con);
                 con.Open();
                 cmdmatins.ExecuteNonQuery();
                 con.Close();
 
             }

             


        }
        filloutgrid();
        lblmsg1.Visible = true;
        lblmsg1.Text = "Record Inserted Succesfully";


    }
    protected void filloutgrid()
    {
        string str132 = "select OutPutSalesorderMaster.*,Party_master.Compname,Party_master.PartyID from OutPutSalesorderMaster inner join Party_master on Party_master.PartyID=OutPutSalesorderMaster.Party_Id  where Inwh_Mid='" + Session["InvMid"] + "' ";

        SqlCommand cmd = new SqlCommand(str132, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        GridView2.DataSource = dt;
        GridView2.DataBind();
 
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st2 = "Delete from OutPutSalesorderMaster where Id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, con);

        con.Open();
        cmd2.ExecuteNonQuery();
        con.Close();
        GridView1.EditIndex = -1;
        filloutgrid();
        //lblmsg.Visible = true;
       // lblmsg.Text = "Record Succesfully Deleted";
    }
}
