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

public partial class ShoppingCart_Admin_JobMaster : System.Web.UI.Page
{
    string compid;

    SqlConnection con;
    //= new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

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
        compid = Session["comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        if (!Page.IsPostBack)
        {
            Label1.Text = "";
            Pagecontrol.dypcontrol(Page, page);
            txtstartdate.Text = System.DateTime.Now.ToShortDateString();
            txtenddate.Text = System.DateTime.Now.ToShortDateString();
            TargetDate.Text = System.DateTime.Now.ToShortDateString();
            lblCompany.Text = Session["Cname"].ToString();
            lblBusiness.Text = "All";
            ViewState["sortOrder"] = "";
            fillstore();
            FillParty();
            fillstatus();
            fillgrid();
            filljobno();

        }

    }

    protected void fillstore()
    {
        string wh = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' and EmployeeWarehouseRights.AccessAllowed='True' order by name";

        SqlCommand cmd = new SqlCommand(wh, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlStoreName.DataSource = ds;
        ddlStoreName.DataTextField = "Name";
        ddlStoreName.DataValueField = "WareHouseId";
        ddlStoreName.DataBind();
        string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        if (dteeed.Rows.Count > 0)
        {
            ddlStoreName.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

        }
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");
    }



    protected void FillParty()
    {
        string str = " SELECT     Party_master.PartyID, Party_master.Account, Party_master.Compname, " +
            " Party_master.Address,Party_master.Contactperson,Party_master.City, Party_master.State, Party_master.Country,  " +
               "       User_master.Active, Party_master.PartyTypeId " +
              " FROM         Party_master LEFT OUTER JOIN " +
             "         User_master ON Party_master.PartyID = User_master.PartyID inner join [PartytTypeMaster] on Party_master.partytypeId = PartytTypeMaster.[PartyTypeId] " +
           " WHERE     (User_master.Active = 1) and Party_master.Whid='" + ddlStoreName.SelectedValue + "' and [PartytTypeMaster].[PartType] = 'Customer'  " +
            " ORDER BY Party_master.Compname ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlPartyName.DataSource = ds;
        ddlPartyName.DataTextField = "Compname";
        ddlPartyName.DataValueField = "PartyID";
        ddlPartyName.DataBind();

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

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        string str = "select * from JobMaster where JobName='" + txtjobname.Text + "' and JobReferenceNo='" + txtRefernceNo.Text + "'  and Whid ='" + ddlStoreName.SelectedValue + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
        }
        else
        {
            if (Convert.ToDateTime(txtstartdate.Text) >= System.DateTime.Now.Date)
            {
                if (Convert.ToDateTime(TargetDate.Text) >= Convert.ToDateTime(txtstartdate.Text))
                {
                    if (Convert.ToDateTime(txtenddate.Text) >= Convert.ToDateTime(txtstartdate.Text))
                    {
                        lblmain.Text = "Work In Process";

                        string sgw = "select InventoryCatName,InventeroyCatId from InventoryCategoryMaster where " +
                            " InventoryCatName='" + lblmain.Text + "' and compid='" + Session["comid"] + "' and InventoryCategoryMaster.CatType IS NULL ";
                        SqlCommand cgw = new SqlCommand(sgw, con);
                        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
                        DataTable dtgw = new DataTable();
                        adgw.Fill(dtgw);

                        if (dtgw.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            try
                            {
                                string catname = "insert into InventoryCategoryMaster(InventoryCatName,Activestatus,compid) values('" + lblmain.Text + "','" + 1 + "','" + Session["comid"] + "')";
                                SqlCommand mycmd = new SqlCommand(catname, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                mycmd.ExecuteNonQuery();
                                con.Close();
                            }
                            catch (Exception ererer)
                            {
                            }
                        }



                        double mainid = 0;

                        if (dtgw.Rows.Count > 0)
                        {
                            mainid = Convert.ToDouble(dtgw.Rows[0]["InventeroyCatId"].ToString());

                        }
                        else
                        {
                            string str123 = "select Max(InventeroyCatId) as InventeroyCatId from InventoryCategoryMaster";
                            SqlCommand cmd123 = new SqlCommand(str123, con);
                            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
                            DataTable dt123 = new DataTable();
                            adp123.Fill(dt123);
                            mainid = Convert.ToDouble(dt123.Rows[0]["InventeroyCatId"].ToString());
                        }

                        //lblsubcat.Text = "WIP" + ddlStoreName.Text;
                        lblsubcat.Text = "Work In Process";

                        string sgw1 = "select InventorySubCatName,InventorySubCatId from InventorySubCategoryMaster where  " +
                            " InventorySubCatName='" + lblsubcat.Text + "' and InventoryCategoryMasterId='" + mainid + "' ";
                        SqlCommand cgw1 = new SqlCommand(sgw1, con);
                        SqlDataAdapter adgw1 = new SqlDataAdapter(cgw1);
                        DataTable dtgw1 = new DataTable();
                        adgw1.Fill(dtgw1);
                        if (dtgw1.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            string queryinsert = "insert into  InventorySubCategoryMaster(InventorySubCatName,InventoryCategoryMasterId,Activestatus)  values('" + lblsubcat.Text + "'," + mainid + ",'" + 1 + "')";
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
                            string str123 = "select Max(InventorySubCatId) as InventorySubCatId from InventorySubCategoryMaster";
                            SqlCommand cmd123 = new SqlCommand(str123, con);
                            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
                            DataTable dt123 = new DataTable();
                            adp123.Fill(dt123);
                            subcatid = Convert.ToDouble(dt123.Rows[0]["InventorySubCatId"].ToString());
                        }

                        //subsub
                        //lblsubsub.Text = lblsubcat.Text + txtJobno.Text;
                        lblsubsub.Text = "Work In Process";

                        string sgw12 = "select InventorySubSubName,InventorySubSubId from InventoruSubSubCategory where " +
                           " InventorySubSubName='" + lblsubsub.Text + "' and InventorySubCatID='" + subcatid + "'";

                        SqlCommand cgw12 = new SqlCommand(sgw12, con);
                        SqlDataAdapter adgw12 = new SqlDataAdapter(cgw12);
                        DataTable dtgw12 = new DataTable();
                        adgw12.Fill(dtgw12);
                        if (dtgw12.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            string qurty = "insert into InventoruSubSubCategory(InventorySubSubName,InventorySubCatID,Activestatus)values('" + lblsubsub.Text + "'," + subcatid + ",'" + 1 + "')";
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
                            string str123 = "select Max(InventorySubSubId) as InventorySubSubId from InventoruSubSubCategory";
                            SqlCommand cmd123 = new SqlCommand(str123, con);
                            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
                            DataTable dt123 = new DataTable();
                            adp123.Fill(dt123);
                            subsubcatid = Convert.ToDouble(dt123.Rows[0]["InventorySubSubId"].ToString());
                        }

                        lblinvname.Text = txtjobname.Text + txtJobno.Text + "WIP" + System.DateTime.Now.Year;

                        string insrDetails = "INSERT INTO InventoryDetails (DateStarted,QtyTypeMasterId,UnitTypeId,Weight) " +
                                        " VALUES ('" + Convert.ToDateTime(txtstartdate.Text).ToShortDateString() + "',1,'" + 1 + "','" + 1 + "') ";
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
                                        " VALUES ('" + lblinvname.Text + "'," + Convert.ToInt32(ViewState["InvDId"]) + ",  " +
                                         " " + subsubcatid + ",'" + 1 + "','" + txtRefernceNo.Text + "') ";
                        SqlCommand cmdMasters = new SqlCommand(insrMasters, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdMasters.ExecuteNonQuery();
                        con.Close();

                        string st2 = "Select Max(InventoryMasterId)from InventoryMaster";
                        SqlCommand cmd6 = new SqlCommand(st2, con);
                        SqlDataAdapter adp6 = new SqlDataAdapter(cmd6);
                        DataSet ds6 = new DataSet();
                        adp6.Fill(ds6);
                        ViewState["InvMId"] = ds6.Tables[0].Rows[0][0].ToString();


                        string s23New = "INSERT INTO InventoryMeasurementUnit(InventoryMasterId,Unit,UnitType) " +
                                             " VALUES   ('" + Convert.ToInt32(ViewState["InvMId"].ToString()) + "' ,'" + 1 + "','" + 1 + "')";


                        SqlCommand cmd3New = new SqlCommand(s23New, con);

                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd3New.ExecuteNonQuery();
                        con.Close();

                        string insertBarcode = "Insert Into InventoryBarcodeMaster (InventoryMaster_id)values(" + Convert.ToInt32(ViewState["InvMId"].ToString()) + ")";
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
                        string insertdetail = "INSERT INTO InventoryWarehouseMasterTbl(InventoryWarehouseMasterId,InventoryMasterId ,InventoryDetailsId,WareHouseId,Active, ReorderQuantiy,NormalOrderQuantity,ReorderLevel ,QtyOnDateStarted ,QtyOnHand,Rate,OpeningQty,OpeningRate,Total,Weight,UnitTypeId)  VALUES ('" + k + "'," + Convert.ToInt32(ViewState["InvMId"].ToString()) + "," + Convert.ToInt32(ViewState["InvDId"]) + ", ' " + Convert.ToInt32(ddlStoreName.SelectedValue) + "','" + 1 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + txtstartdate.Text + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','0','1')";


                        SqlCommand insertDetailss = new SqlCommand(insertdetail, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        insertDetailss.ExecuteNonQuery();

                        string strmax = "select Max(InventoryWarehouseMasterId) as InventoryWarehouseMasterId from InventoryWarehouseMasterTbl where  WareHouseId='" + ddlStoreName.SelectedValue + "'";
                        SqlCommand cmdmax = new SqlCommand(strmax, con);
                        SqlDataAdapter adpmax = new SqlDataAdapter(cmdmax);
                        DataTable dsmax = new DataTable();
                        adpmax.Fill(dsmax);

                        //SqlCommand cmdmax = new SqlCommand("select Max(InventoryWarehouseMasterId) as InventoryWarehouseMasterId from InventoryWarehouseMasterTbl where  WareHouseId='" + ddlStoreName.SelectedValue + "'", con);
                        //SqlDataAdapter dtpmax = new SqlDataAdapter(cmdmax);
                        //DataTable dtmax = new DataTable();
                        //dtpmax.Fill(dtmax);
                        if (dsmax.Rows.Count > 0)
                        {
                            double inwmasterid = Convert.ToDouble(dsmax.Rows[0]["InventoryWarehouseMasterId"].ToString());
                            string MasterIns = "Insert Into JobMaster(JobNumber,JobName,JobReferenceNo,Note,JobStartDate,TargetDate,JobEndDate,StatusId,PartyId,Whid,compid,InvWMasterId) Values('" + txtJobno.Text + "','" + txtjobname.Text + "','" + txtRefernceNo.Text + "','" + txtnote.Text + "','" + txtstartdate.Text + "','" + TargetDate.Text + "','" + txtenddate.Text + "','" + ddlStatus.SelectedValue + "','" + ddlPartyName.SelectedValue + "','" + ddlStoreName.SelectedValue + "','" + Session["Comid"].ToString() + "','" + inwmasterid + "') ";
                            SqlCommand cmdmaster = new SqlCommand(MasterIns, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdmaster.ExecuteNonQuery();
                            con.Close();

                            //SqlDataAdapter dassss = new SqlDataAdapter("select max(id) as ID from JobMaster");
                            //DataTable dtssss = new DataTable();
                            //dassss.Fill(dtssss);

                            //if (dtssss.Rows.Count > 0)
                            //{
                            //    ViewState["rid"] = Convert.ToString(dtssss.Rows[0]["ID"]);
                            //}



                            string projectst = "";

                            if (ddlStatus.SelectedValue == "186")
                            {
                                projectst = "Completed";
                            }
                            else
                            {
                                projectst = "Pending";
                            }

                            //   int success = ClsProject.SpProjectMasterAddData(Convert.ToString("0"), txtjobname.Text, projectst, txtstartdate.Text, txtenddate.Text, Convert.ToString("0"), Convert.ToString("0"), txtnote.Text, "0", Convert.ToString("0"), "0", ddlStoreName.SelectedValue, Convert.ToBoolean(1), ddlPartyName.SelectedValue);

                            //string stvf = "select Max(id) as jobid from JobMaster where  Whid='" + ddlStoreName.SelectedValue + "'";
                            //SqlCommand cmdmaxcd = new SqlCommand(stvf, con);
                            //SqlDataAdapter asdf = new SqlDataAdapter(cmdmaxcd);
                            //DataTable aer = new DataTable();
                            //asdf.Fill(aer);
                            //if (Convert.ToString(aer.Rows[0]["jobid"]) != "")
                            //{
                            //    string strcf = "insert into JobProjectMasterTbl(JobId,ProjectId)Values('" + aer.Rows[0]["jobid"] + "','" + success + "')";
                            //    SqlCommand cmdmastera = new SqlCommand(strcf, con);
                            //    if (con.State.ToString() != "Open")
                            //    {
                            //        con.Open();
                            //    }
                            //    cmdmastera.ExecuteNonQuery();
                            //    con.Close();
                            //}
                            clearall();
                            Label1.Visible = true;
                            Label1.Text = "Record inserted successfully";

                            addinventoryroom.Visible = false;
                            btnaddroom.Visible = true;
                            lbladd.Text = "";
                            filljobno();
                        }
                        fillgrid();
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "The end date must be the current date, or greater than the current date";
                    }
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "The target date must be the start date, or greater than the start date";
                }
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "The start date must be the current date, or greater than the current date";
            }

        }
    }
    protected void ddlStoreName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillParty();
        filljobno();
    }
    protected void filljobno()
    {
        string str = "Select Max(JobNumber) as JobNumber from JobMaster where Whid='" + ddlStoreName.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["JobNumber"].ToString() != "" && dt.Rows[0]["JobNumber"].ToString() != null)
            {
                int jobno = Convert.ToInt32(dt.Rows[0]["JobNumber"].ToString()) + 1;
                txtJobno.Text = jobno.ToString();
            }
            else
            {
                int jobno = 1;
                txtJobno.Text = jobno.ToString();
            }
        }
        else
        {
            int jobno = 1;
            txtJobno.Text = jobno.ToString();
        }
    }

    protected void fillgrid()
    {
        string str = "";
        string str2 = "";

        if (DropDownList1.SelectedIndex > 0)
        {
            str = "  JobMaster.Id,JobMaster.JobNumber ,[JobName],Convert(nvarchar,TargetDate,101) as  TargetDate,WarehouseMaster.Name,[JobReferenceNo],Party_master.Compname as Compname,Convert(nvarchar,JobStartDate,101) as  JobStartDate,Convert(nvarchar,[JobEndDate],101) as JobEndDate ,[StatusMaster].[StatusName],JobMaster.StatusId ,JobMaster.[PartyId],JobMaster.[Whid] ,JobMaster.[compid] ,JobMaster.[InvWMasterId],JobMaster.JobReferenceNo as cost from JobMaster left join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId = [JobMaster].[Id] inner join [Party_master]  on [Party_master].PartyID=JobMaster.PartyId  inner join StatusMaster on [StatusMaster].[StatusId]=[JobMaster].StatusId inner join WarehouseMaster on WarehouseMaster.WarehouseId=JobMaster.Whid WHERE compid='" + Session["Comid"].ToString() + "' and JobMaster.Whid='" + DropDownList1.SelectedValue + "'";
            //order by Name, Compname ";

            str2 = " select count(JobMaster.Id) as ci from JobMaster left join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId = [JobMaster].[Id] inner join [Party_master]  on [Party_master].PartyID=JobMaster.PartyId  inner join StatusMaster on [StatusMaster].[StatusId]=[JobMaster].StatusId inner join WarehouseMaster on WarehouseMaster.WarehouseId=JobMaster.Whid WHERE compid='" + Session["Comid"].ToString() + "' and JobMaster.Whid='" + DropDownList1.SelectedValue + "'";
        }
        else
        {
            str = "  JobMaster.Id,JobMaster.JobNumber ,[JobName] ,Convert(nvarchar,TargetDate,101) as  TargetDate,WarehouseMaster.Name,[JobReferenceNo],Party_master.Compname as Compname,Convert(nvarchar,JobStartDate,101) as  JobStartDate,Convert(nvarchar,[JobEndDate],101) as JobEndDate ,[StatusMaster].[StatusName],JobMaster.StatusId ,JobMaster.[PartyId],JobMaster.[Whid] ,JobMaster.[compid] ,JobMaster.[InvWMasterId],JobMaster.JobReferenceNo as cost from JobMaster left join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId = [JobMaster].[Id] inner join [Party_master]  on [Party_master].PartyID=JobMaster.PartyId  inner join StatusMaster on [StatusMaster].[StatusId]=[JobMaster].StatusId inner join WarehouseMaster on WarehouseMaster.WarehouseId=JobMaster.Whid WHERE compid='" + Session["Comid"].ToString() + "'";

            str2 = " select count(JobMaster.Id) as ci from JobMaster left join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId = [JobMaster].[Id] inner join [Party_master]  on [Party_master].PartyID=JobMaster.PartyId  inner join StatusMaster on [StatusMaster].[StatusId]=[JobMaster].StatusId inner join WarehouseMaster on WarehouseMaster.WarehouseId=JobMaster.Whid WHERE compid='" + Session["Comid"].ToString() + "'";
        }
        lblBusiness.Text = DropDownList1.SelectedItem.Text;

     

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,Party_master.Compname asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable ds = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, str);

            DataView myDataView = new DataView();
            myDataView = ds.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            for (int rowindex = 0; rowindex < ds.Rows.Count; rowindex++)
            {
                string t11 = Convert.ToString(ds.Rows[rowindex]["Id"]);
                ViewState["rid"] = t11;

                fillmaterialissue();
                overheadallocation();
                fillgridtemp();

                Double Workordercost = (Convert.ToDouble(lblTotalSum.Text) + Convert.ToDouble(lbltotaloverheadbyall.Text) + Convert.ToDouble(lbldailyworktotal.Text));
                lblMyfinal.Text = Workordercost.ToString();

                ds.Rows[rowindex]["cost"] = lblMyfinal.Text;
            }

            GridView1.DataSource = myDataView;
            GridView1.DataBind();
        }
    }

    private int GetRowCount(string str)
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

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression, string query)
    {
        DataTable dt = select(string.Format("SELECT * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));

        dt.Columns.Remove("ROW_NUM");

        return dt;

        ViewState["dt"] = dt;
    }

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }


    protected void LinkButton4_Click(object sender, ImageClickEventArgs e)
    {
        lbladd.Text = "Edit Work Order";
        Label1.Text = "";
        ImageButton lk = (ImageButton)sender;
        int j = Convert.ToInt32(lk.CommandArgument);
        ViewState["Id"] = j;
        Session["TimeId"] = j;

        string str = "select * from JobMaster where Id='" + j + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        adp.Fill(dt);

        fillstore();
        ddlStoreName.SelectedIndex = ddlStoreName.Items.IndexOf(ddlStoreName.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));

        FillParty();

        ddlPartyName.SelectedIndex = ddlPartyName.Items.IndexOf(ddlPartyName.Items.FindByValue(dt.Rows[0]["PartyId"].ToString()));

        fillstatus();

        ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(dt.Rows[0]["StatusId"].ToString()));

        txtJobno.Text = dt.Rows[0]["JobNumber"].ToString();
        txtRefernceNo.Text = dt.Rows[0]["JobReferenceNo"].ToString();
        txtjobname.Text = dt.Rows[0]["JobName"].ToString();

        txtstartdate.Enabled = false;
        ImageButton2.Enabled = false;
        DateTime JobStartDate;
        JobStartDate = Convert.ToDateTime(dt.Rows[0]["JobStartDate"].ToString());
        txtstartdate.Text = JobStartDate.ToShortDateString();

        DateTime target = Convert.ToDateTime(dt.Rows[0]["TargetDate"].ToString());
        TargetDate.Text = target.ToShortDateString();

        DateTime JobEndDate;
        JobEndDate = Convert.ToDateTime(dt.Rows[0]["JobEndDate"].ToString());
        txtenddate.Text = JobEndDate.ToShortDateString();



        txtnote.Text = dt.Rows[0]["Note"].ToString();



        Button7.Visible = true;
        Button3.Visible = false;

        addinventoryroom.Visible = true;
        btnaddroom.Visible = false;





    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        string str = "select * from JobMaster where JobName='" + txtjobname.Text + "' and JobReferenceNo='" + txtRefernceNo.Text + "'  and Whid ='" + ddlStoreName.SelectedValue + "' and Id!='" + ViewState["Id"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
        }
        else
        {

            if (Convert.ToDateTime(TargetDate.Text) >= Convert.ToDateTime(txtstartdate.Text))
            {
                if (Convert.ToDateTime(txtenddate.Text) >= Convert.ToDateTime(txtstartdate.Text))
                {
                    string Update = "Update JobMaster set JobNumber='" + txtJobno.Text + "',JobName='" + txtjobname.Text + "',JobReferenceNo='" + txtRefernceNo.Text + "',Note='" + txtnote.Text + "',JobStartDate='" + txtstartdate.Text + "',TargetDate='" + TargetDate.Text + "',JobEndDate='" + txtenddate.Text + "',StatusId='" + ddlStatus.SelectedValue + "',PartyId='" + ddlPartyName.SelectedValue + "',Whid='" + ddlStoreName.SelectedValue + "' where Id='" + ViewState["Id"] + "'";

                    SqlCommand cmdupdate = new SqlCommand(Update, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupdate.ExecuteNonQuery();
                    con.Close();

                    //SqlDataAdapter dajoje = new SqlDataAdapter("select projectid from JobProjectMasterTbl where jobid='" + ViewState["Id"] + "'", con);
                    //DataTable dtjoje = new DataTable();
                    //dajoje.Fill(dtjoje);

                    //if (dtjoje.Rows.Count > 0)
                    //{
                    //    SqlDataAdapter dass = new SqlDataAdapter("select * from ProjectMaster where ProjectId='" + Convert.ToString(dtjoje.Rows[0]["projectid"]) + "'", con);
                    //    DataTable dtss = new DataTable();
                    //    dass.Fill(dtss);

                    //    // int success = ClsProject.SpProjectMasterAddData(Convert.ToString("0"), Convert.ToString("0"), txtnote.Text, "0", Convert.ToString("0"), "0", ddlStoreName.SelectedValue, Convert.ToBoolean(1), ddlPartyName.SelectedValue);

                    //    string MasterIns = "Update ProjectMaster set businessid='" + Convert.ToString(dtss.Rows[0]["BusinessId"]) + "',projectname='" + txtjobname.Text + "',status='" + Convert.ToString(dtss.Rows[0]["status"]) + "',estartdate='" + txtstartdate.Text + "',eenddate='" + txtenddate.Text + "',percentage='" + Convert.ToString(dtss.Rows[0]["Percentage"]) + "',ltgmasterid='" + Convert.ToString(dtss.Rows[0]["LTGMasterId"]) + "',stgmasterid='" + Convert.ToString(dtss.Rows[0]["STGMasterId"]) + "',ygmasterid='" + Convert.ToString(dtss.Rows[0]["YGMasterId"]) + "',mgmasterid='" + Convert.ToString(dtss.Rows[0]["MGMasterId"]) + "',wtmasterid='" + Convert.ToString(dtss.Rows[0]["WTMasterId"]) + "',strategyid='" + Convert.ToString(dtss.Rows[0]["StrategyId"]) + "',tacticid='" + Convert.ToString(dtss.Rows[0]["TacticId"]) + "',description='" + txtnote.Text + "',budgetedamount='" + Convert.ToString(dtss.Rows[0]["BudgetedAmount"]) + "',EmployeeID='" + Convert.ToString(dtss.Rows[0]["EmployeeID"]) + "',DeptId='" + Convert.ToString(dtss.Rows[0]["DeptId"]) + "',Whid='" + ddlStoreName.SelectedValue + "',Addjob='" + Convert.ToBoolean(1) + "',PartyId='" + ddlPartyName.SelectedValue + "' where ProjectId='" + dtss.Rows[0]["ProjectId"].ToString() + "'";
                    //    SqlCommand cmdmaster = new SqlCommand(MasterIns, con);
                    //    if (con.State.ToString() != "Open")
                    //    {
                    //        con.Open();
                    //    }
                    //    cmdmaster.ExecuteNonQuery();
                    //    con.Close();
                    //}

                    clearall();
                    Label1.Visible = true;
                    fillgrid();
                    Label1.Text = "Record updated successfully";
                    addinventoryroom.Visible = false;
                    btnaddroom.Visible = true;
                    lbladd.Text = "";
                    filljobno();
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "The end date must be the start date, or greater than the start date";
                }
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "The target date must be the start date, or greater than the start date";
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();

        string st2 = "Delete from JobMaster where Id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();

        SqlDataAdapter dajoje = new SqlDataAdapter("select projectid from JobProjectMasterTbl where jobid='" + id + "'", con);
        DataTable dtjoje = new DataTable();
        dajoje.Fill(dtjoje);

        if (dtjoje.Rows.Count > 0)
        {
            string st211 = "Delete from ProjectMaster where projectid='" + dtjoje.Rows[0]["projectid"].ToString() + "' ";
            SqlCommand cmd2111 = new SqlCommand(st211, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd2111.ExecuteNonQuery();
            con.Close();
        }

        //GridView1.EditIndex = -1;
        fillgrid();
        filljobno();
        Label1.Visible = true;
        Label1.Text = "Record deleted successfully";
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        ddlStoreName.SelectedIndex = 0;
        ddlPartyName.SelectedIndex = 0;
        txtjobname.Text = "";

        txtnote.Text = "";
        txtRefernceNo.Text = "";
        txtstartdate.Text = System.DateTime.Now.ToShortDateString();
        txtenddate.Text = System.DateTime.Now.ToShortDateString();
        TargetDate.Text = System.DateTime.Now.ToShortDateString();
        ddlStatus.SelectedIndex = 0;
        Label1.Text = "";
        Button7.Visible = false; ;
        Button3.Visible = true;
        addinventoryroom.Visible = false;
        btnaddroom.Visible = true;
        lbladd.Text = "";
        lblmain.Text = "";
        lblsubcat.Text = "";
        lblsubsub.Text = "";
        lblinvname.Text = "";
        txtstartdate.Enabled = true;
        ImageButton2.Enabled = true;

    }
    protected void LinkButton97666667_Click(object sender, EventArgs e)
    {
        if (ddlStoreName.SelectedIndex >= 0)
        {
            FillParty();
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
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
    protected void clearall()
    {
        ddlStoreName.SelectedIndex = 0;
        ddlPartyName.SelectedIndex = 0;
        txtjobname.Text = "";
        txtJobno.Text = "";
        txtnote.Text = "";
        txtRefernceNo.Text = "";
        txtstartdate.Text = System.DateTime.Now.ToShortDateString();
        txtenddate.Text = System.DateTime.Now.ToShortDateString();
        TargetDate.Text = System.DateTime.Now.ToShortDateString();
        ddlStatus.SelectedIndex = 0;
        Label1.Text = "";
        Button7.Visible = false; ;
        Button3.Visible = true;
        txtstartdate.Enabled = true;
        ImageButton2.Enabled = true;
        lblmain.Text = "";
        lblsubcat.Text = "";
        lblsubsub.Text = "";
        lblinvname.Text = "";

    }
    protected void imgAdd2_Click(object sender, ImageClickEventArgs e)
    {
        string te = "customerspartyregister.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;

            GridView1.AllowPaging = false;
            GridView1.PageSize = 10000;
            fillgrid();

            if (GridView1.Columns[10].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[10].Visible = false;
            }
            if (GridView1.Columns[11].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[11].Visible = false;
            }
        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = 25;
            fillgrid();

            Button1.Text = "Printable Version";
            Button2.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[10].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[11].Visible = true;
            }
        }
    }
    protected void btnaddroom_Click(object sender, EventArgs e)
    {
        if (addinventoryroom.Visible == false)
        {
            addinventoryroom.Visible = true;
            lbladd.Text = "Add Work Order";
        }
        else if (addinventoryroom.Visible == true)
        {
            addinventoryroom.Visible = false;
            lbladd.Text = "";

        }
        Label1.Text = "";
        btnaddroom.Visible = false;

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    protected void fillmaterialissue()
    {
        string str123t = "select distinct MaterialIssueMasterTbl.Id, MaterialIssueDetail.InvWMasterId,MaterialIssueDetail.Rate,MaterialIssueDetail.Qty,InventoryMaster.Name,MaterialIssueMasterTbl.JobMasterId  from MaterialIssueMasterTbl inner join MaterialIssueDetail on MaterialIssueDetail.MaterialMasterId=MaterialIssueMasterTbl.Id " +
                        " inner join  InventoryWarehouseMasterTbl on  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=MaterialIssueDetail.InvWMasterId " +
                        " inner join InventoryMaster on InventoryMaster.InventoryMasterId=InventoryWarehouseMasterTbl.InventoryMasterId " +
                        " where MaterialIssueMasterTbl.JobMasterId='" + ViewState["rid"] + "' ";

        SqlCommand cmd = new SqlCommand(str123t, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        grdMaterialIssue.DataSource = dt;
        grdMaterialIssue.DataBind();

        double totalmaterialcost = 0;
        if (grdMaterialIssue.Rows.Count > 0)
        {
            foreach (GridViewRow gdr in grdMaterialIssue.Rows)
            {

                double materialcost = 0;

                Label lblmaterialmasterid = (Label)gdr.FindControl("lblmaterialmasterid");
                Label InvWMasterId = (Label)gdr.FindControl("lblitemnameid");
                Label lbldate124 = (Label)gdr.FindControl("lbldate124");
                Label lbltotalcost = (Label)gdr.FindControl("lblCost");
                Label lblRate = (Label)gdr.FindControl("lblRate");
                Label lblqty = (Label)gdr.FindControl("lblqty");

                string str123tmaterial = "select * from InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + InvWMasterId.Text + "' and MaterialIssueMasterTblId='" + lblmaterialmasterid.Text + "' ";
                SqlCommand cmdmaterial = new SqlCommand(str123tmaterial, con);
                SqlDataAdapter adpmaterial = new SqlDataAdapter(cmdmaterial);
                DataTable dtmaterial = new DataTable();
                adpmaterial.Fill(dtmaterial);

                if (dtmaterial.Rows.Count > 0)
                {
                    lbldate124.Text = Convert.ToDateTime(Convert.ToString(dtmaterial.Rows[0]["DateUpdated"].ToString())).ToShortDateString();

                    double qty = 0;
                    double rate = 0;
                    double totalcost = 0;
                    if (lblqty.Text != "")
                    {
                        qty = Convert.ToDouble(lblqty.Text);
                    }
                    if (lblRate.Text != "")
                    {
                        rate = Convert.ToDouble(lblRate.Text);
                    }
                    totalcost = qty * rate;

                    lbltotalcost.Text = totalcost.ToString();
                    materialcost += totalcost;

                    totalmaterialcost += materialcost;
                    lblTotalSum.Text = totalmaterialcost.ToString();
                }
            }
        }
        else
        {
            lblTotalSum.Text = "0";
        }
    }

    public void overheadallocation()
    {
        string st178 = " select distinct OverHeadAllocationMaster.* from OverHeadAllocationMaster inner join OverHeadAllocationJobDetail on OverHeadAllocationJobDetail.OverHeadMasterId=OverHeadAllocationMaster.Id where OverHeadAllocationJobDetail.JobMasterId='" + ViewState["rid"] + "'";
        SqlCommand cmd178 = new SqlCommand(st178, con);
        SqlDataAdapter adp178 = new SqlDataAdapter(cmd178);
        DataTable dt178 = new DataTable();
        adp178.Fill(dt178);

        grdoverhead.DataSource = dt178;
        grdoverhead.DataBind();

        double totaloverheadcosting = 0;

        if (grdoverhead.Rows.Count > 0)
        {
            foreach (GridViewRow gdr in grdoverhead.Rows)
            {

                Label lbloverheadmasterid = (Label)gdr.FindControl("lbloverheadmasterid");
                Label lblstartdate789 = (Label)gdr.FindControl("lblstartdate789");
                Label lblenddate789 = (Label)gdr.FindControl("lblenddate789");
                Label lblohbymaterial789 = (Label)gdr.FindControl("lblohbymaterial789");

                Label lbldirectlabour789 = (Label)gdr.FindControl("lbldirectlabour789");
                Label lblnoofdays789 = (Label)gdr.FindControl("lblnoofdays789");
                Label ohbyequal789 = (Label)gdr.FindControl("ohbyequal789");
                Label lblOhAllocationtotal789 = (Label)gdr.FindControl("lblOhAllocationtotal789");

                string Avgcost = "select OverHeadAllocationJobDetail.* from OverHeadAllocationJobDetail   where  OverHeadMasterId='" + lbloverheadmasterid.Text + "' and Active='1' ";
                SqlCommand cmd1451 = new SqlCommand(Avgcost, con);
                SqlDataAdapter adp1451 = new SqlDataAdapter(cmd1451);
                DataTable ds1451 = new DataTable();
                adp1451.Fill(ds1451);

                double overheadbymate = 0;
                double overheadbymalabour = 0;
                double overheadbymapd = 0;
                double overheadbymaequal = 0;
                double totaloverhead = 0;

                double overheadbymate1 = 0;
                double overheadbymalabour1 = 0;
                double overheadbymapd1 = 0;
                double overheadbymaequal1 = 0;
                double totaloverhead1 = 0;

                if (dt178.Rows.Count > 0)
                {

                    foreach (DataRow dtr132 in ds1451.Rows)
                    {
                        overheadbymate = Convert.ToDouble(dtr132["OhByMaterial"].ToString());
                        overheadbymalabour = Convert.ToDouble(dtr132["OhByLabour"].ToString());
                        overheadbymapd = Convert.ToDouble(dtr132["OhByDays"].ToString());
                        overheadbymaequal = Convert.ToDouble(dtr132["Ohbyequal"].ToString());
                        totaloverhead = Convert.ToDouble(dtr132["OhAllocationtotal"].ToString());

                        overheadbymate1 += overheadbymate;
                        lblohbymaterial789.Text = overheadbymate1.ToString();

                        overheadbymalabour1 += overheadbymalabour;
                        lbldirectlabour789.Text = overheadbymalabour1.ToString();

                        overheadbymapd1 += overheadbymapd;
                        lblnoofdays789.Text = overheadbymapd1.ToString();

                        overheadbymaequal1 += overheadbymaequal;
                        ohbyequal789.Text = overheadbymaequal1.ToString();

                        totaloverhead1 += totaloverhead;
                        lblOhAllocationtotal789.Text = totaloverhead1.ToString();

                        totaloverheadcosting += totaloverhead1;
                        lbltotaloverheadbyall.Text = totaloverheadcosting.ToString();

                    }
                }
            }
        }
        else
        {
            lbltotaloverheadbyall.Text = "0";
        }
    }

    protected void fillgridtemp()
    {
        string st132 = "select distinct JobEmployeeDailyTaskTbl.*,EmployeeMaster.EmployeeName,EmployeeMaster.EmployeeMasterID,JobEmployeeDailyTaskDetail.JobMasterId from JobEmployeeDailyTaskTbl inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobDailyTaskMasterId=JobEmployeeDailyTaskTbl.Id inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=JobEmployeeDailyTaskTbl.EmployeeId  where JobEmployeeDailyTaskDetail.JobMasterId='" + ViewState["rid"] + "' ";
        SqlCommand cmd = new SqlCommand(st132, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        grddailywork.DataSource = ds;
        grddailywork.DataBind();

        double TotalCost = 0;

        if (grddailywork.Rows.Count > 0)
        {
            foreach (GridViewRow gdr in grddailywork.Rows)
            {
                Label lblEmployee = (Label)gdr.FindControl("lblEmployee");
                Label lblmasterid = (Label)gdr.FindControl("lblmasterid");
                Label lblhours = (Label)gdr.FindControl("lblhours");
                Label lblcost = (Label)gdr.FindControl("lblcost");

                string Avgcost = "Select sum(datepart(hour,convert(datetime,Hrs)))  AS TotalHours,sum(datepart(minute,convert(datetime,Hrs))) AS TotalMinutes,SUM(cast(Cost as decimal(18,2))) as Cost from JobEmployeeDailyTaskDetail    where  JobEmployeeDailyTaskDetail.JobDailyTaskMasterId='" + lblmasterid.Text + "' ";

                SqlCommand cmd1451 = new SqlCommand(Avgcost, con);
                SqlDataAdapter adp1451 = new SqlDataAdapter(cmd1451);
                DataTable ds1451 = new DataTable();
                adp1451.Fill(ds1451);

                if (ds1451.Rows.Count > 0)
                {
                    string FinalTime = "";

                    string TotalHour = ds1451.Rows[0]["TotalHours"].ToString();
                    string TotalMinutes = ds1451.Rows[0]["TotalMinutes"].ToString();

                    Int32 in1 = Convert.ToInt32(TotalHour.ToString());
                    Int32 HourtoMinute1 = in1 * 60;
                    Int32 Minute1 = Convert.ToInt32(TotalMinutes.ToString());

                    Int32 TotalMinutes132 = (HourtoMinute1) + (Minute1);

                    Int32 FinalHours = (TotalMinutes132 / 60);
                    Int32 FinalMinute = (TotalMinutes132 % 60);

                    FinalTime = FinalHours + ":" + FinalMinute;
                    lblhours.Text = FinalTime.ToString();

                    double Cost = Convert.ToDouble(ds1451.Rows[0]["Cost"].ToString());
                    lblcost.Text = Cost.ToString();

                    TotalCost += Cost;
                    lbldailyworktotal.Text = TotalCost.ToString();
                }
            }
        }
        else
        {
            lbldailyworktotal.Text = "0";
        }
    }
    protected void btnjv_Click(object sender, EventArgs e)
    {
        DataTable dts = select("select distinct JobMaster.Id as jid,InvWMasterId,JobEmployeeDailyTaskTbl.Date, JobEmployeeDailyTaskTbl.EmployeeId,JobStartDate,JobEndDate,JobEmployeeDailyTaskTbl.Id,SUM(cast(Cost as decimal(18,2))) as Cost from JobEmployeeDailyTaskTbl inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskTbl.Id= JobEmployeeDailyTaskDetail.JobDailyTaskMasterId inner join JobMaster on JobMaster.Id=JobEmployeeDailyTaskDetail.JobMasterId  where JobEmployeeDailyTaskTbl.Id NOT IN " +
       "(Select Distinct JobcompleteCOGS.JobempdailytaskId from JobcompleteCOGS inner join JobMaster on JobMaster.Id=JobcompleteCOGS.JobId Inner join JobEmployeeDailyTaskDetail on  JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id where  JobMaster.Whid='" + ddlStoreName.SelectedValue + "'  ) and JobMaster.Whid='" + ddlStoreName.SelectedValue + "' group by InvWMasterId,JobMaster.Id,JobEmployeeDailyTaskTbl.EmployeeId,JobStartDate,JobEndDate,JobEmployeeDailyTaskTbl.Id,JobEmployeeDailyTaskTbl.Date Order by JobMaster.Id,JobEmployeeDailyTaskTbl.Id");
        foreach (DataRow itm in dts.Rows)
        {

            if (Convert.ToString(itm["Cost"]) != "")
            {
                if (Convert.ToDecimal(itm["Cost"]) > 0)
                {
                    int ety = 1;
                    int Id1 = 0;
                    DataTable ds131b = select(" SELECT     EntryTypeId, Max(EntryNumber) as Number FROM  TranctionMaster " +
                                        " Where EntryTypeId = 3 and Whid='" + ddlStoreName.SelectedValue + "' Group by EntryTypeId ");

                    if (ds131b.Rows.Count > 0)
                    {
                        if (Convert.ToString(ds131b.Rows[0]["Number"]) != "")
                        {
                            ety = Convert.ToInt32(ds131b.Rows[0]["Number"]) + 1;

                        }
                    }
                    SqlCommand cd3 = new SqlCommand("Sp_Insert_TranctionMasterRetIdentity", con);

                    cd3.CommandType = CommandType.StoredProcedure;
                    cd3.Parameters.AddWithValue("@Date", Convert.ToDateTime(itm["Date"]).ToShortDateString());
                    cd3.Parameters.AddWithValue("@EntryNumber", ety);
                    cd3.Parameters.AddWithValue("@EntryTypeId", "3");
                    cd3.Parameters.AddWithValue("@UserId", Session["UserId"]);
                    cd3.Parameters.AddWithValue("@Tranction_Amount", Convert.ToDecimal(itm["Cost"]));
                    cd3.Parameters.AddWithValue("@whid", ddlStoreName.SelectedValue);

                    cd3.Parameters.AddWithValue("@compid", HttpContext.Current.Session["Comid"]);


                    cd3.Parameters.Add(new SqlParameter("@Tranction_Master_Id", SqlDbType.Int));
                    cd3.Parameters["@Tranction_Master_Id"].Direction = ParameterDirection.Output;
                    cd3.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                    cd3.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    Id1 = (int)cd3.ExecuteNonQuery();
                    Id1 = Convert.ToInt32(cd3.Parameters["@Tranction_Master_Id"].Value);
                    con.Close();
                    decimal OLDavgcost = 0;
                    decimal oLDqtyONHAND = 0;
                    DataTable dt123 = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl " +
                           "  where InvWMasterId='" + itm["InvWMasterId"] + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc");
                    string dateor = "";
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
                        dateor = Convert.ToDateTime(dt123.Rows[0]["DateUpdated"]).ToShortDateString();
                        if (oLDqtyONHAND == 0)
                        {
                            oLDqtyONHAND = 1;
                        }
                    }
                    else
                    {
                        dateor = Convert.ToDateTime(itm["Date"]).ToShortDateString();
                    }
                    if (oLDqtyONHAND == 0)
                    {
                        oLDqtyONHAND = 1;
                    }

                    decimal invtranamt = Convert.ToDecimal(itm["Cost"]);
                    if (oLDqtyONHAND == 0)
                    {

                        oLDqtyONHAND = 1;
                    }
                    decimal avxc = (OLDavgcost * oLDqtyONHAND) + (invtranamt) / oLDqtyONHAND;

                    string ABCD = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,Rate,DateUpdated,AvgCost,QtyonHand)values('" + itm["InvWMasterId"] + "','" + Id1 + "','" + oLDqtyONHAND + "','" + Math.Round(avxc, 2) + "','" + dateor + "','" + Math.Round(avxc, 2) + "','" + oLDqtyONHAND + "')";
                    SqlCommand cmdadd = new SqlCommand(ABCD, con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdadd.ExecuteNonQuery();
                    con.Close();

                    string a6 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                        " ,DateTimeOfTransaction,compid,whid,DiscEarn,Memo)" +
                        " VALUES('8000','" + invtranamt + "'" +
                        " ,'" + Id1 + "','" + Convert.ToDateTime(itm["Date"]).ToShortDateString() + "','" + HttpContext.Current.Session["Comid"] + "','" + ddlStoreName.SelectedValue + "','','Employee cost based on All the tasks done and reported in Daily worksheet of all emloyees for different workorders /projects for Date " + Convert.ToDateTime(itm["Date"]).ToShortDateString() + " allocated to respective work orders')";
                    SqlCommand cmdtrd = new SqlCommand(a6, con);


                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdtrd.ExecuteNonQuery();
                    string accd = "";
                    DataTable dtr = select("Select DepartmentmasterMNC.Departmentname from EmployeeMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=EmployeeMaster.DeptID where EmployeeMaster.EmployeeMasterID='" + itm["EmployeeId"] + "' and DepartmentmasterMNC.Departmentname='Work Staff'");
                    if (dtr.Rows.Count > 0)
                    {
                        accd = "8009";
                    }
                    else
                    {
                        accd = "7002";
                    }
                    string a61 = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                          " ,DateTimeOfTransaction,compid,whid,DiscEarn,Memo)" +
                          " VALUES('" + accd + "','" + invtranamt + "'" +
                          " ,'" + Id1 + "','" + Convert.ToDateTime(itm["Date"]).ToShortDateString() + "','" + HttpContext.Current.Session["Comid"] + "','" + ddlStoreName.SelectedValue + "','','Employee cost based on All the tasks done and reported in Daily worksheet of all emloyees for different workorders /projects for Date " + Convert.ToDateTime(itm["Date"]).ToShortDateString() + " allocated to respective work orders')";

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }

                    SqlCommand cd41 = new SqlCommand(a61, con);

                    cd41.ExecuteNonQuery();
                    con.Close();


                    string Abrd = "Insert into JobcompleteCOGS(JobId,JobempdailytaskId,Date,TrnansId)values('" + itm["jid"] + "' ,'" + itm["Id"] + "','" + Convert.ToDateTime(itm["Date"]).ToShortDateString() + "','" + Id1 + "')";
                    SqlCommand sxd = new SqlCommand(Abrd, con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    sxd.ExecuteNonQuery();
                    con.Close();

                }
            }

        }

    }
    //protected DataTable select(string str)
    //{
    //    DataTable ds = new DataTable();

    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);

    //    adp.Fill(ds);
    //    return ds;
    //}
}
