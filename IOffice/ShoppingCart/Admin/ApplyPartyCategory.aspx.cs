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
public partial class PartyTypeDetail : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con;
   
    string compid;
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

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page); 
       
        Label1.Visible = false;
     
        if (!IsPostBack)
        {
            lblCompany.Text = Session["Cname"].ToString();

            lblBusiness.Text = "All";
            fillstore();
            fillstorefilter();
            
            

            ViewState["sortOrder"] = "";


           

            fillpartydiscountname();
            fillpartydiscountnamefilter();
            fillpartyddl();
            fillpartyddlfilter();

           
         

            fillgridmaster();
           
        }
    }
    protected DataTable wzfill()
    {
        string wh = "select Name,WareHouseID from WareHouseMaster where comid='" + Session["comid"] + "' and WareHouseMaster.Status='" + 1 + "'";

        SqlCommand cmdwh = new SqlCommand(wh, con);
        SqlDataAdapter daeh = new SqlDataAdapter(cmdwh);
        DataTable dswh = new DataTable();
        daeh.Fill(dswh);
        return dswh;

    }
    protected void FillGrid()
    {
        string strr = "";
        if (ddlwarehouse.SelectedIndex == 0)
        {
            if (ddlcustomercat.SelectedIndex > 0)
            {
              //  lblPartyCat.Text = ddlcustomercat.SelectedItem.Text;
                if (txtsearch.Text != "")
                {
                    strr = " Where PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId='" + ddlcustomercat.SelectedValue + "' and Party_master.Compname='" + txtsearch.Text + "' or Party_master.Contactperson='" + txtsearch.Text + "' or Party_master.Phoneno='" + txtsearch.Text + "'";
                }
                else
                {
                    strr = " Where PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId='" + ddlcustomercat.SelectedValue + "'";
                }
            }
            else if (txtsearch.Text != "")
            {
                strr = " Where Party_master.Compname='" + txtsearch.Text + "' or Party_master.Contactperson='" + txtsearch.Text + "' or Party_master.Phoneno='" + txtsearch.Text + "' and Party_master.id='" + Session["comid"] + "'";
            }
            else
            {
                strr = "where Party_master.id='" + Session["comid"] + "'";
            }
        }
        else
        {

            if (ddlcustomercat.SelectedIndex > 0)
            {
               // lblPartyCat.Text = ddlcustomercat.SelectedItem.Text;
                if (txtsearch.Text != "")
                {
                    strr = " And PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId='" + ddlcustomercat.SelectedValue + "' and PartyTypeCategoryMasterTbl.Whid='" + ddlwarehouse.SelectedValue + "' and Party_master.Compname='" + txtsearch.Text + "' or Party_master.Contactperson='" + txtsearch.Text + "' or Party_master.Phoneno='" + txtsearch.Text + "'";
                }
                else
                {
                    strr = " And PartyTypeCategoryMasterTbl.Whid='" + ddlwarehouse.SelectedValue + "' and PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId='" + ddlcustomercat.SelectedValue + "'";
                }
            }
            else if (txtsearch.Text != "")
            {
                strr = " and PartyTypeCategoryMasterTbl.Whid='" + ddlwarehouse.SelectedValue + "' And Party_master.Compname='" + txtsearch.Text + "' or Party_master.Contactperson='" + txtsearch.Text + "' or Party_master.Phoneno='" + txtsearch.Text + "' and Party_master.id='" + Session["comid"] + "'";
            }
            else
            {
                strr = "And PartyTypeCategoryMasterTbl.Whid='" + ddlwarehouse.SelectedValue + "' and Party_master.id='" + Session["comid"] + "'";
            }
        
        }
        string strgrd = "";
        if (ddlwarehouse.SelectedIndex == 0)
        {

            lblBusiness.Text = ddlwarehouse.SelectedItem.Text;
            strgrd = " SELECT distinct    PartyTypeDetailTbl.PartyTypeCategoryMasterId as partytypecatid, Party_master.PartyID, Party_master.Compname, Party_master.Contactperson, Party_master.City, " +
                            " Party_master.State, Party_master.Country, Party_master.Phoneno, PartyTypeDetailTbl.PartyTypeDetailId, " +
                            " PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId,PartyTypeCategoryMasterTbl.PartyCategoryName, PartyTypeCategoryMasterTbl.PartyCategoryDiscount, PartyTypeCategoryMasterTbl.IsPercentage, " +
                            " convert(nvarchar(10),PartyTypeCategoryMasterTbl.EntryDate,101)as EntryDate, CityMasterTbl.CityName, StateMasterTbl.StateName, CountryMaster.CountryName,Party_master.Compname + '-' + Party_master.Contactperson + '-' + CityMasterTbl.CityName + '-' + StateMasterTbl.StateName + '-' + CountryMaster.CountryName + '-' + Party_master.Phoneno as Partyname,CASE WHEN (IsPercentage = 1) THEN 'IsPer' ELSE 'Amt' END AS per " +
                            " FROM  CityMasterTbl RIGHT OUTER JOIN " +
                            " CountryMaster RIGHT OUTER JOIN " +
                            " Party_master ON CountryMaster.CountryId = Party_master.Country LEFT OUTER JOIN " +
                            " StateMasterTbl ON Party_master.State = StateMasterTbl.StateId RIGHT OUTER JOIN " +
                            " PartyTypeCategoryMasterTbl RIGHT OUTER JOIN " +
                            " PartyTypeDetailTbl ON PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId = PartyTypeDetailTbl.PartyTypeCategoryMasterId ON " +
                            " Party_master.PartyID = PartyTypeDetailTbl.PartyID ON CityMasterTbl.CityId = Party_master.City inner join WarehouseMaster on WarehouseMaster.WarehouseId=Party_master.Whid " + strr + " and (PartyTypeCategoryMasterTbl.Active = 1)";
        }
        else
        {
            strgrd = " SELECT distinct    PartyTypeDetailTbl.PartyTypeCategoryMasterId as partytypecatid, Party_master.PartyID, Party_master.Compname, Party_master.Contactperson, Party_master.City, " +
                            " Party_master.State, Party_master.Country, Party_master.Phoneno, PartyTypeDetailTbl.PartyTypeDetailId, " +
                            " PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId,PartyTypeCategoryMasterTbl.PartyCategoryName, PartyTypeCategoryMasterTbl.PartyCategoryDiscount, PartyTypeCategoryMasterTbl.IsPercentage, " +
                            " convert(nvarchar(10),PartyTypeCategoryMasterTbl.EntryDate,101)as EntryDate, CityMasterTbl.CityName, StateMasterTbl.StateName, CountryMaster.CountryName,Party_master.Compname + '-' + Party_master.Contactperson + '-' + CityMasterTbl.CityName + '-' + StateMasterTbl.StateName + '-' + CountryMaster.CountryName + '-' + Party_master.Phoneno as Partyname,CASE WHEN (IsPercentage = 1) THEN 'IsPer' ELSE 'Amt' END AS per " +
                            " FROM  CityMasterTbl RIGHT OUTER JOIN " +
                            " CountryMaster RIGHT OUTER JOIN " +
                            " Party_master ON CountryMaster.CountryId = Party_master.Country LEFT OUTER JOIN " +
                            " StateMasterTbl ON Party_master.State = StateMasterTbl.StateId RIGHT OUTER JOIN " +
                            " PartyTypeCategoryMasterTbl RIGHT OUTER JOIN " +
                            " PartyTypeDetailTbl ON PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId = PartyTypeDetailTbl.PartyTypeCategoryMasterId ON " +
                            " Party_master.PartyID = PartyTypeDetailTbl.PartyID ON CityMasterTbl.CityId = Party_master.City inner join WarehouseMaster on WarehouseMaster.WarehouseId=Party_master.Whid where PartyTypeCategoryMasterTbl.Whid='" + ddlwarehouse.SelectedValue + "' " + strr + " and(PartyTypeCategoryMasterTbl.Active = 1)"; 
   
        }
        
        SqlCommand cmdgrd = new SqlCommand(strgrd, con);
        SqlDataAdapter adpgrd = new SqlDataAdapter(cmdgrd);
        DataTable dtgrd1 = new DataTable();
        adpgrd.Fill(dtgrd1);

        DataTable dtgrd = new DataTable();

        DataColumn dtco1 = new DataColumn();
        dtco1.DataType = System.Type.GetType("System.String");
        dtco1.ColumnName = "PartyTypeCategoryMasterId";
        dtco1.ReadOnly = false;
        dtco1.Unique = false;
        dtco1.AllowDBNull = false;

        dtgrd.Columns.Add(dtco1);
        
        DataColumn dtco2 = new DataColumn();
        dtco2.DataType = System.Type.GetType("System.String");
        dtco2.ColumnName = "Mer";
        dtco2.ReadOnly = false;
        dtco2.Unique = false;
        dtco2.AllowDBNull = false;

        dtgrd.Columns.Add(dtco2);

        DataColumn dtco3 = new DataColumn();
        dtco3.DataType = System.Type.GetType("System.String");
        dtco3.ColumnName = "PartyID";
        dtco3.ReadOnly = false;
        dtco3.Unique = false;
        dtco3.AllowDBNull = false;

        dtgrd.Columns.Add(dtco3);

        DataColumn dtco4 = new DataColumn();
        dtco4.DataType = System.Type.GetType("System.String");
        dtco4.ColumnName = "ParMer";
        dtco4.ReadOnly = false;
        dtco4.Unique = false;
        dtco4.AllowDBNull = false;

        dtgrd.Columns.Add(dtco4);

        DataColumn dtco5 = new DataColumn();
        dtco5.DataType = System.Type.GetType("System.String");
        dtco5.ColumnName = "PartyTypeCategoryMasterId1";
        dtco5.ReadOnly = false;
        dtco5.Unique = false;
        dtco5.AllowDBNull = false;

        dtgrd.Columns.Add(dtco5);

        DataColumn dtco6 = new DataColumn();
        dtco6.DataType = System.Type.GetType("System.String");
        dtco6.ColumnName = "PartyTypeDetailId";
        dtco6.ReadOnly = false;
        dtco6.Unique = false;
        dtco6.AllowDBNull = false;

        dtgrd.Columns.Add(dtco6);

        if (dtgrd1.Rows.Count > 0)
        {
            foreach (DataRow dr in dtgrd1.Rows)
            {
                DataRow drtemp = dtgrd.NewRow();
                drtemp["PartyTypeCategoryMasterId"] = dr["partytypecatid"].ToString();
                drtemp["Mer"] = dr["PartyCategoryName"].ToString() + '-' + dr["PartyCategoryDiscount"].ToString() + '-' + dr["per"].ToString();//+ '-' + dr["EntryDate"].ToString()
                drtemp["PartyID"] = dr["PartyID"].ToString();
                drtemp["ParMer"] = dr["PartyID"].ToString() + '-' + dr["Partyname"].ToString();
                drtemp["PartyTypeCategoryMasterId1"] = dr["PartyTypeCategoryMasterId"].ToString();
                drtemp["PartyTypeDetailId"] = dr["PartyTypeDetailId"].ToString();
                dtgrd.Rows.Add(drtemp);
            }
        }
        //if (dtgrd.Rows.Count > 0)
        //{
          //  GridView1.DataSource = dtgrd;
            DataView myDataView = new DataView();
            myDataView = dtgrd.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
         
        //}
        GridView1.DataSource = myDataView;
        GridView1.DataBind();
    }
    protected void FillddlPartyType()
    {
        string strpartytype = "";
        
        if (ddlwarehouse.SelectedIndex == 0)
        {
            strpartytype = " SELECT   WarehouseMaster.Name,  PartyTypeCategoryMasterId, PartyCategoryName, PartyCategoryDiscount, CONVERT(nvarchar(10), EntryDate, 101) AS EntryDate, " +
                                    " CASE WHEN (IsPercentage = 1) THEN 'IsPer' ELSE 'Amt' END AS per " +
                                    " FROM PartyTypeCategoryMasterTbl inner join WarehouseMaster on WarehouseMaster.WarehouseId=PartyTypeCategoryMasterTbl.Whid where (PartyTypeCategoryMasterTbl.Active = 1) " +

            " and compid='" + compid + "' order by PartyCategoryName";
        }
        else
        {
            strpartytype = " SELECT   WarehouseMaster.Name,  PartyTypeCategoryMasterId, PartyCategoryName, PartyCategoryDiscount, CONVERT(nvarchar(10), EntryDate, 101) AS EntryDate, " +
                                    " CASE WHEN (IsPercentage = 1) THEN 'IsPer' ELSE 'Amt' END AS per " +
                                    " FROM PartyTypeCategoryMasterTbl inner join WarehouseMaster on WarehouseMaster.WarehouseId=PartyTypeCategoryMasterTbl.Whid  " +

            " where PartyTypeCategoryMasterTbl.Whid='" + ddlwarehouse.SelectedValue + "' and (PartyTypeCategoryMasterTbl.Active = 1)  order by PartyCategoryName";
        }
        SqlCommand cmdpartytype = new SqlCommand(strpartytype,con);
        SqlDataAdapter adppartytype = new SqlDataAdapter(cmdpartytype);
        DataTable dtpartytype = new DataTable();
        adppartytype.Fill(dtpartytype);

        DataTable dttemp = new DataTable();

        DataColumn dtcol1 = new DataColumn();
        dtcol1.DataType = System.Type.GetType("System.String");
        dtcol1.ColumnName = "PartyTypeCategoryMasterId";
        dtcol1.ReadOnly = false;
        dtcol1.Unique = false;
        dtcol1.AllowDBNull = false;

        dttemp.Columns.Add(dtcol1);

        DataColumn dtcol2 = new DataColumn();
        dtcol2.DataType = System.Type.GetType("System.String");
        dtcol2.ColumnName = "Mer";
        dtcol2.ReadOnly = false;
        dtcol2.Unique = false;
        dtcol2.AllowDBNull = false;

        dttemp.Columns.Add(dtcol2);

        if (dtpartytype.Rows.Count > 0)
        {
            ddlPartyType.Items.Clear();
            ddlcustomercat.Items.Clear();
            foreach (DataRow dr in dtpartytype.Rows)
            {
                DataRow dtrow = dttemp.NewRow();

                dtrow["PartyTypeCategoryMasterId"] = dr["PartyTypeCategoryMasterId"].ToString();
                dtrow["Mer"] =dr["PartyCategoryName"].ToString() + '-' + dr["PartyCategoryDiscount"].ToString() + '-' + dr["per"].ToString();// + '-' + dr["EntryDate"].ToString()
                dttemp.Rows.Add(dtrow);
            }
            ddlPartyType.DataSource = dttemp;
            //ddlPartyType.DataTextField="PartType";
            //ddlPartyType.DataValueField="PartyTypeId";
            if (ddlwarehouse.SelectedIndex != 0)
            {
                ddlPartyType.DataTextField = "Mer";
                ddlPartyType.DataValueField = "PartyTypeCategoryMasterId";
                ddlPartyType.DataBind();
                ddlPartyType.Items.Insert(0, "--Select--");
                ddlPartyType.Items[0].Value = "0";
            }
            ddlcustomercat.DataSource = dttemp;
            ddlcustomercat.DataTextField = "Mer";
            ddlcustomercat.DataValueField = "PartyTypeCategoryMasterId";
            ddlcustomercat.DataBind();
            ddlcustomercat.Items.Insert(0, "All");
            ddlcustomercat.Items[0].Value = "0";
        }
        else
        {
            ddlPartyType.Items.Insert(0, "--Select--");
            ddlPartyType.Items[0].Value = "0";

            //ddlcustomercat.Items.Insert(0, "--Select--");
            //ddlcustomercat.Items[0].Value = "0";
        }
    }
    //protected void FillddlParty()
    //{
    //    if (ddlwarehouse.SelectedIndex >= 0)
    //    {
    //        string strparty = "SELECT PartyID,Compname ,Contactperson, Compname+' '+Contactperson as PartyName FROM Party_master left outer join PartytTypeMaster on Party_master.PartyTypeId=PartytTypeMaster.PartyTypeId where Party_master.Whid='" + ddlwarehouse.SelectedValue + "'  order by Compname";
    //        SqlCommand cmdparty = new SqlCommand(strparty, con);
    //        SqlDataAdapter adpparty = new SqlDataAdapter(cmdparty);
    //        DataTable dtparty = new DataTable();
    //        adpparty.Fill(dtparty);

    //        if (dtparty.Rows.Count > 0)
    //        {
    //            ddlParty.DataSource = dtparty;
    //            ddlParty.DataTextField = "PartyName";
    //            ddlParty.DataValueField = "PartyID";
    //            ddlParty.DataBind();
    //            ddlParty.Items.Insert(0, "All");
    //            ddlParty.Items[0].Value = "0";

    //            ddlpartyname.DataSource = dtparty;
    //            ddlpartyname.DataTextField = "PartyName";
    //            ddlpartyname.DataValueField = "PartyID";
    //            ddlpartyname.DataBind();
    //            ddlpartyname.Items.Insert(0, "--Select--");
    //            ddlpartyname.Items[0].Value = "0";
    //        }
    //    }
    //    else
    //    {
    //        ddlParty.Items.Insert(0, "All");
    //        ddlParty.Items[0].Value = "0";
    //        ddlpartyname.Items.Insert(0, "--Select--");
    //        ddlpartyname.Items[0].Value = "0";
    //    }
    //}

    protected void Button4_Click(object sender, EventArgs e)
    {
       // Panel3.Visible = true;
       //// Panel1.Visible = false;
        
       // SqlCommand cmd = new SqlCommand("update PartyTypeDetailTbl set PartyTypeCategoryMasterId="+DropDownList3.SelectedValue+""+
       //    "  where PartyTypeDetailId= "+Session["id"]+" ", con);
       // con.Open();
       // cmd.ExecuteNonQuery();
       // con.Close();
       // SqlCommand cmd1 = new SqlCommand("Sp_Select_PartyTypeCategoryDetails", con);
       // cmd1.CommandType = CommandType.StoredProcedure;
       // SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
       // DataSet ds1 = new DataSet();
       // adp1.Fill(ds1);

       // GridView1.DataSource = ds1;
       // GridView1.DataBind();
       //// Button4.Visible = false;
       // Button3.Visible = true;
       // Label3.Visible = true;
       // Label3.Text = "Record Update Successfully";
    }
    protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    {
        //GridView1.DataSource = (DataSet) fillgridmaster();
        //GridView1.DataBind();
    //    Panel3.Visible = true;

    }
    protected void RadioButton4_CheckedChanged(object sender, EventArgs e)
    {
       // Panel3.Visible = false;
        //Panel3.Visible = false;
    }
   
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      
    }
   
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView2.EditIndex = e.NewEditIndex;

        //DataSet ds44 = new DataSet();
        //SqlDataAdapter da44 = new SqlDataAdapter("select * from PartyTypeCategoryMasterTbl ", con);
        //da44.Fill(ds44);
        //GridView2.DataSource = ds44;
        //GridView2.DataBind();

        //ModalPopupExtenderAddnew.Show();
    }
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //TextBox txtName = (TextBox)GridView2.Rows[e.RowIndex].FindControl("TextBox1");
        //TextBox txtPartyCatDis = (TextBox)GridView2.Rows[e.RowIndex].FindControl("TextBox2");
        //TextBox txtEntryDate = (TextBox)GridView2.Rows[e.RowIndex].FindControl("TextBox3");
        //CheckBox ch1 = (CheckBox)GridView2.Rows[e.RowIndex].FindControl("CheckBox1");
        //// (TextBox) tn1=(TextBox)GridView2.Rows[e.RowIndex].Cells[][]

        //int id = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value);

        //SqlCommand cmd = new SqlCommand("update PartyTypeCategoryMasterTbl set PartyCategoryName='" + txtName.Text + "',PartyCategoryDiscount='" + txtPartyCatDis.Text + "',IsPercentage='" + ch1.Checked + "'  from PartyTypeCategoryMasterTbl where PartyTypeCategoryMasterId=" + id + "", con);
        //con.Open();
        //cmd.ExecuteNonQuery();
        //con.Close();

        //DataSet ds4 = new DataSet();
        //SqlDataAdapter da4 = new SqlDataAdapter("select * from PartyTypeCategoryMasterTbl ", con);
        //da4.Fill(ds4);
        //GridView2.DataSource = ds4;
        //GridView2.DataBind();
        //ModalPopupExtenderAddnew.Show();
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //int id = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value);
        //SqlCommand cmd = new SqlCommand("delete from PartyTypeCategoryMasterTbl where PartyTypeCategoryMasterId=" + id + "", con);
        //con.Open();
        //cmd.ExecuteNonQuery();
        //con.Close();

        //DataSet ds4 = new DataSet();
        //SqlDataAdapter da4 = new SqlDataAdapter("select * from PartyTypeCategoryMasterTbl ", con);
        //da4.Fill(ds4);
        //GridView2.DataSource = ds4;
        //GridView2.DataBind();      

       // ModalPopupExtenderAddnew.Show();
    }
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
       // ModalPopupExtenderAddnew.Show();
    }
    protected void Button3_Click1(object sender, EventArgs e)
    {
        txtentrydate.Text = "";
        txtpartycategory.Text = "";
        txtpartycategorydiscount.Text = "";
        chkIsper.Checked = false;
        chkActive.Checked = false;
       // ModalPopupExtenderAddnew.Show();
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
       // ModalPopupExtenderAddnew.Hide();
    }
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //GridView2.EditIndex = -1;
        //ModalPopupExtenderAddnew.Show();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {

    }
    protected void imgBtnAddNewCat_Click(object sender, EventArgs e)
    {
        ddlware.DataSource = (DataTable)wzfill();
        ddlware.DataTextField = "Name";
        ddlware.DataValueField = "WareHouseId";

        ddlware.DataBind();
        ddlware.Items.Insert(0, "--Select--");
        ddlware.Items[0].Value = "0";
      //  ModalPopupExtenderAddnew.Show();
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        try
        {
           
            string isper = "";
            string act = "";
            if (chkIsper.Checked == true)
            {
                isper = "1";
            }
            else
            {
                isper = "0";
            }
            if (chkActive.Checked == true)
            {
                act = "1";
            }
            else
            {
                act = "0";
            }
            SqlCommand cmd1 = new SqlCommand("SELECT * from PartyTypeCategoryMasterTbl where Whid='" + ddlware.SelectedValue + "' and PartyCategoryName='" + txtpartycategory.Text + "' ", con);
           
            SqlDataAdapter adp = new SqlDataAdapter(cmd1);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            if (ds.Rows.Count > 0)
            {
                lblm.Text = "Record Already used";
              
            }
            else
            {
                string strPartyTypeInsert = "Insert into PartyTypeCategoryMasterTbl(PartyCategoryName,PartyCategoryDiscount,IsPercentage,Active,EntryDate,compid,Whid) values " +
                " ('" + txtpartycategory.Text + "','" + txtpartycategorydiscount.Text + "','" + isper + "','" + act + "','" + System.DateTime.Now.ToShortDateString() + "','" + compid + "','" + ddlware.SelectedValue + "')";
                SqlCommand cmdpartytypeinsert = new SqlCommand(strPartyTypeInsert, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdpartytypeinsert.ExecuteNonQuery();
                con.Close();

                FillddlPartyType();
                txtpartycategory.Text = "";
                txtpartycategorydiscount.Text = "";
                chkIsper.Checked = false;
                chkActive.Checked = false;
                lblm.Text = "Record Already used";
            }
            // // FillGrid();
            //string strPartyTypeMax = " select Max(PartyTypeId) as id1 from PartytTypeMaster";
            //SqlCommand cmd1 = new SqlCommand(strPartyTypeMax, con);
            //SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            //DataTable dt1 = new DataTable();
            //adp1.Fill(dt1);
            //if (dt1.Rows.Count > 0)
            //{
            //}
            //          SELECT TOP 1000 [PartyTypeCategoryMasterId]
            //    ,[PartyCategoryName]
            //    ,[PartyCategoryDiscount]
            //    ,[IsPercentage]
            //    ,[Active]
            //    ,[EntryDate]
            //FROM [TestIwebshop(24-Nov-09)].[dbo].[PartyTypeCategoryMasterTbl]
            //string str2 = "";
            //if (txtpartycategorydiscount.Text == "")
            //{
            //    str2 = "insert into PartyTypeCategoryMasterTbl (PartyCategoryName,PartyCategoryDiscount,IsPercentage,Active,EntryDate) " +
            //                          " Values ( '" + txtpartycategory.Text + "',0, " +
            //                          "  '" + chkIsper.Checked + "','" + chkActive.Checked + "','" + System.DateTime.Now.Date.ToShortDateString() + "') ";

            //}
            //else
            //{
            //    str2 = "insert into PartyTypeCategoryMasterTbl (PartyCategoryName,PartyCategoryDiscount,IsPercentage,Active,EntryDate) " +
            //                         " Values ( '" + txtpartycategory.Text + "'," + Convert.ToDecimal(txtpartycategorydiscount.Text) + ", " +
            //                         "  '" + chkIsper.Checked + "','" + chkActive.Checked + "','" + System.DateTime.Now.Date.ToShortDateString() + "') ";

            //}
            //SqlCommand cmd2 = new SqlCommand(str2, con);
            //con.Open();
            //cmd2.ExecuteNonQuery();
            //con.Close();

            //string str3 = " select Max(PartyTypeCategoryMasterId) as id2 from PartyTypeCategoryMasterTbl";
            //SqlCommand cmd3 = new SqlCommand(str3, con);
            //SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);
            //DataTable dt3 = new DataTable();
            //adp3.Fill(dt3);
            //          SELECT PartyTypeDetailId
            //    ,[PartyTypeCategoryMasterId]
            //    ,[PartyID]
            //FROM [TestIwebshop(24-Nov-09)].[dbo].[PartyTypeDetailTbl]

            //string str21 = " insert into PartyTypeDetailTbl (PartyTypeCategoryMasterId,PartyID) " +
            //                         " Values ( '" + dt1.Rows[0]["id1"].ToString() + "','" + ddlpartyWithTypeDetail.SelectedValue + "') ";
            //SqlCommand cmd21 = new SqlCommand(str21, con);
            //con.Open();
            //cmd21.ExecuteNonQuery();
            //con.Close();
            //FillGrid();
        }
        catch (Exception erere)
        {
            Label1.Text = "Error :" + erere.Message;
            Label1.Visible = true;
        }
    }
    protected void ddlPartyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillGrid();
    }
    protected void ddlParty_SelectedIndexChanged(object sender, EventArgs e)
    {
         fillgridmaster();
    }
   
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
        int dk1 = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        ViewState["Id"] = dk1;

        string eeed = " select PartyTypeDetailTbl.*,Party_master.Whid from PartyTypeDetailTbl inner join Party_master on Party_master.PartyID=PartyTypeDetailTbl.PartyID  where PartyTypeDetailTbl.PartyTypeDetailId='" + dk1 + "'  ";
        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);

        fillstore();
        ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["Whid"]).ToString()));

        fillpartydiscountname();
        ddlPartyType.SelectedIndex = ddlPartyType.Items.IndexOf(ddlPartyType.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["PartyTypeCategoryMasterId"]).ToString()));

        fillpartyddl();
        ddlpartyname.SelectedIndex = ddlpartyname.Items.IndexOf(ddlpartyname.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["PartyID"]).ToString()));
        
        pnladd.Visible = true;

        lbllegend.Visible = true;
        btnadd.Visible = false;
        lbllegend.Text = "Edit Customer Discount Category";
        Button5.Visible = true;
        imgbtnapp.Visible = false;

    }
   
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }
        if (e.CommandName == "Edit")
        {
           

        }
        else if (e.CommandName == "Delete")
        {
            int m = Convert.ToInt32(e.CommandArgument);

            if (m.ToString() != "")
            {
                SqlCommand cmddel = new SqlCommand("Delete from PartyTypeDetailTbl where PartyTypeDetailId="+m, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmddel.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record deleted succesfully";
                fillgridmaster();
            }

           
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgridmaster();
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
    protected void imgbtnapp_Click(object sender, EventArgs e)
    {
        if (ddlPartyType.SelectedIndex > -1 && ddlpartyname.SelectedIndex >-1)
        {
            string strpartytype1 = " SELECT   * from PartyTypeDetailTbl WHERE PartyTypeCategoryMasterId='" + ddlPartyType.SelectedValue + "' and PartyID='" + ddlpartyname.SelectedValue + "' ";
            SqlCommand cmdpartytype1 = new SqlCommand(strpartytype1, con);
            SqlDataAdapter adppartytype1 = new SqlDataAdapter(cmdpartytype1);
            DataTable dtpartytype1 = new DataTable();
            adppartytype1.Fill(dtpartytype1);
            if (dtpartytype1.Rows.Count > 0)
            {
                Label1.Visible = true;
                Label1.Text = "Discount already applied";
            }
            else
            {
                SqlCommand cmdins = new SqlCommand("Insert into PartyTypeDetailTbl(PartyTypeCategoryMasterId,PartyID) values('" + ddlPartyType.SelectedValue + "','" + ddlpartyname.SelectedValue + "')", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdins.ExecuteNonQuery();
                con.Close();

                Label1.Visible = true;
                Label1.Text = "";
                Label1.Text = "Discount applied sucessfully";
                fillgridmaster();
                ddlpartyname.SelectedIndex = 0;
                ddlPartyType.SelectedIndex = 0;

                pnladd.Visible = false;
                lbllegend.Visible = false;
                btnadd.Visible = true;
                lbllegend.Text = "Add New Customer Discount Category";


            }
        }
    }
    protected void imgbtngo_Click(object sender, EventArgs e)
    {
        fillgridmaster();
    }
    protected void Imagee_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1222.Hide();
    }
    protected void Imabtn_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["tdid"].ToString() != "")
        {
            SqlCommand cmddel = new SqlCommand("Delete from PartyTypeDetailTbl where PartyTypeDetailId='" + ViewState["tbid"] + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmddel.ExecuteNonQuery();
            con.Close();
            Label1.Visible = true;
            Label1.Text = "Record Deleted Succesfully";
            fillgridmaster();
        }
    }

    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
  
       

        fillpartydiscountname();
        fillpartyddl();
    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        if (ddlwarehouse.SelectedIndex > 0 )

        {

           // FillddlParty();
        }
    }

    protected void Button1_Click2(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button4.Visible = true;
            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[3].Visible = false;
            }
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
        }
        else
        {



            Button1.Text = "Printable Version";
            Button4.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[3].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
        }
    }

    protected void fillpartydiscountname()
    {
         


                string strpartytype = "  SELECT   WarehouseMaster.Name,PartyCategoryName,  PartyTypeCategoryMasterId,   CONVERT(nvarchar(10), EntryDate, 101) AS EntryDate,PartyCategoryName+':'+ CONVERT(nvarchar(50),PartyCategoryDiscount)+':'+(CASE WHEN (IsPercentage = 1) THEN 'IsPer' ELSE 'Amount' END) AS Discountmastername  FROM PartyTypeCategoryMasterTbl inner join WarehouseMaster on WarehouseMaster.WarehouseId=PartyTypeCategoryMasterTbl.Whid where PartyTypeCategoryMasterTbl.Whid='" + ddlwarehouse.SelectedValue + "' and (PartyTypeCategoryMasterTbl.Active = 1)  order by PartyCategoryName";
        SqlCommand cmdpartytype = new SqlCommand(strpartytype, con);
        SqlDataAdapter adppartytype = new SqlDataAdapter(cmdpartytype);
        DataTable dtpartytype = new DataTable();
        adppartytype.Fill(dtpartytype);

        ddlPartyType.DataSource = dtpartytype;
        ddlPartyType.DataTextField = "Discountmastername";
        ddlPartyType.DataValueField = "PartyTypeCategoryMasterId";
        ddlPartyType.DataBind();
    }
    protected void fillpartydiscountnamefilter()
    {



        string strpartytype = "  SELECT   WarehouseMaster.Name,PartyCategoryName,  PartyTypeCategoryMasterId,   CONVERT(nvarchar(10), EntryDate, 101) AS EntryDate,PartyCategoryName+':'+ CONVERT(nvarchar(50),PartyCategoryDiscount)+':'+(CASE WHEN (IsPercentage = 1) THEN 'IsPer' ELSE 'Amount' END) AS Discountmastername  FROM PartyTypeCategoryMasterTbl inner join WarehouseMaster on WarehouseMaster.WarehouseId=PartyTypeCategoryMasterTbl.Whid where PartyTypeCategoryMasterTbl.Whid='" + ddlfilterbusiness.SelectedValue + "' and (PartyTypeCategoryMasterTbl.Active = 1)  order by PartyCategoryName";
        SqlCommand cmdpartytype = new SqlCommand(strpartytype, con);
        SqlDataAdapter adppartytype = new SqlDataAdapter(cmdpartytype);
        DataTable dtpartytype = new DataTable();
        adppartytype.Fill(dtpartytype);

        if (dtpartytype.Rows.Count > 0)
        {

            ddlcustomercat.DataSource = dtpartytype;
            ddlcustomercat.DataTextField = "Discountmastername";
            ddlcustomercat.DataValueField = "PartyTypeCategoryMasterId";
            ddlcustomercat.DataBind();

            ddlcustomercat.Items.Insert(0, "All");
            ddlcustomercat.Items[0].Value = "0";
        }
        else
        {
            ddlcustomercat.Items.Insert(0, "All");
            ddlcustomercat.Items[0].Value = "0";
        }
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {

        string te = "CustomersPartyRegister.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }

    protected void fillpartyddl()
    {
        string strparty = "SELECT PartyID,Compname ,Contactperson, Compname+' '+Contactperson as PartyName FROM Party_master left outer join PartytTypeMaster on Party_master.PartyTypeId=PartytTypeMaster.PartyTypeId where Party_master.Whid='" + ddlwarehouse.SelectedValue + "'  order by Compname";
        SqlCommand cmdparty = new SqlCommand(strparty, con);
        SqlDataAdapter adpparty = new SqlDataAdapter(cmdparty);
        DataTable dtparty = new DataTable();
        adpparty.Fill(dtparty);

        ddlpartyname.DataSource = dtparty;
        ddlpartyname.DataTextField = "PartyName";
        ddlpartyname.DataValueField = "PartyID";
        ddlpartyname.DataBind();
    }
    protected void fillpartyddlfilter()
    {
        string strparty = "SELECT PartyID,Compname ,Contactperson, Compname+' '+Contactperson as PartyName FROM Party_master left outer join PartytTypeMaster on Party_master.PartyTypeId=PartytTypeMaster.PartyTypeId where Party_master.Whid='" + ddlfilterbusiness.SelectedValue + "'  order by Compname";
        SqlCommand cmdparty = new SqlCommand(strparty, con);
        SqlDataAdapter adpparty = new SqlDataAdapter(cmdparty);
        DataTable dtparty = new DataTable();
        adpparty.Fill(dtparty);
        if (dtparty.Rows.Count > 0)
        {
            ddlParty.DataSource = dtparty;
            ddlParty.DataTextField = "PartyName";
            ddlParty.DataValueField = "PartyID";
            ddlParty.DataBind();

            ddlParty.Items.Insert(0, "All");
            ddlParty.Items[0].Value = "0";
        }
        else
        {
        

            ddlParty.Items.Insert(0, "All");
            ddlParty.Items[0].Value = "0";
        }
    }
    protected void fillstore()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
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
    protected void fillstorefilter()
    {
        ddlfilterbusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlfilterbusiness.DataSource = ds;
        ddlfilterbusiness.DataTextField = "Name";
        ddlfilterbusiness.DataValueField = "WareHouseId";
        ddlfilterbusiness.DataBind();


        ddlfilterbusiness.Items.Insert(0, "All");
        ddlfilterbusiness.Items[0].Value = "0";
        


    }
    protected void ddlfilterbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        fillpartydiscountnamefilter();
        fillpartyddlfilter();
        fillgridmaster();
    }
    protected void ddlcustomercat_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgridmaster();
    }

    protected void fillgridmaster()
    {
        string st1="";
        string st2 = "";
        string strgrd = "";
        lblBusiness.Text = ddlfilterbusiness.SelectedItem.Text;
        lblcutomercategoryprint.Text = ddlcustomercat.SelectedItem.Text;
        lblpartynameprint.Text = ddlParty.SelectedItem.Text;

         strgrd = "SELECT distinct PartyTypeDetailTbl.PartyTypeDetailId, WarehouseMaster.Name,  PartyTypeDetailTbl.PartyTypeCategoryMasterId as partytypecatid, Party_master.PartyID, Party_master.Compname, Party_master.Contactperson, Party_master.City,  Party_master.State, Party_master.Country, Party_master.Phoneno, PartyTypeDetailTbl.PartyTypeDetailId,  PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId,PartyTypeCategoryMasterTbl.PartyCategoryName + '-' + convert(nvarchar(50),PartyTypeCategoryMasterTbl.PartyCategoryDiscount)+ '-' + CASE WHEN (PartyTypeCategoryMasterTbl.IsPercentage = 1) THEN 'IsPer' ELSE 'Amount' END AS per, PartyTypeCategoryMasterTbl.IsPercentage,  convert(nvarchar(10),PartyTypeCategoryMasterTbl.EntryDate,101)as EntryDate, CityMasterTbl.CityName, StateMasterTbl.StateName, CountryMaster.CountryName, Party_master.Contactperson + '-' + CityMasterTbl.CityName + '-' + StateMasterTbl.StateName + '-' + CountryMaster.CountryName + '-' + Party_master.Phoneno as Partyname  FROM  CityMasterTbl RIGHT OUTER JOIN  CountryMaster RIGHT OUTER JOIN  Party_master ON CountryMaster.CountryId = Party_master.Country LEFT OUTER JOIN  StateMasterTbl ON Party_master.State = StateMasterTbl.StateId RIGHT OUTER JOIN  PartyTypeCategoryMasterTbl RIGHT OUTER JOIN  PartyTypeDetailTbl ON PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId = PartyTypeDetailTbl.PartyTypeCategoryMasterId ON  Party_master.PartyID = PartyTypeDetailTbl.PartyID ON CityMasterTbl.CityId = Party_master.City inner join WarehouseMaster on WarehouseMaster.WarehouseId=Party_master.Whid  where Party_master.id='" + Session["comid"] + "'";

        if (ddlfilterbusiness.SelectedIndex>0)
        {
            st1 = "and WarehouseMaster.WarehouseId='" + ddlfilterbusiness.SelectedValue + "'";
        }
        if (ddlParty.SelectedIndex > 0)
        {
            st2 = "and PartyTypeDetailTbl.PartyID='" + ddlParty.SelectedValue + "'";
        }

        string strfinal = strgrd + st1 + st2 ;

        SqlCommand cmdgrd = new SqlCommand(strfinal, con);
        SqlDataAdapter adpgrd = new SqlDataAdapter(cmdgrd);
        DataTable dtgrd1 = new DataTable();
        adpgrd.Fill(dtgrd1);

        if (dtgrd1.Rows.Count > 0)
        {

            GridView1.DataSource = dtgrd1;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        SqlCommand cmdchk = new SqlCommand("select * from PartyTypeDetailTbl where PartyTypeCategoryMasterId='" + ddlPartyType.SelectedValue + "' and PartyID='" + ddlpartyname.SelectedValue + " ' and   PartyTypeDetailId<>'" + ViewState["Id"] + "' ", con);
        SqlDataAdapter dtpchk = new SqlDataAdapter(cmdchk);
        DataSet dschk = new DataSet();
        dtpchk.Fill(dschk);

        if (dschk.Tables[0].Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exist";
        }
        else
        {
            string strup = " update PartyTypeDetailTbl set  " +
                   " PartyTypeCategoryMasterId='" + ddlPartyType.SelectedValue + "',PartyID='" + ddlpartyname.SelectedValue + " ' where PartyTypeDetailId='" + ViewState["Id"] + "' ";
            
            SqlCommand cmdup = new SqlCommand(strup, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdup.ExecuteNonQuery();
            con.Close();

            Label1.Visible = true;
            Label1.Text = "Discount updated successfully";

            pnladd.Visible = false;
            lbllegend.Visible = false;
            btnadd.Visible = true;
            lbllegend.Text = "Add New Customer Discount Category";


            GridView1.EditIndex = -1;
            fillgridmaster();
            Button5.Visible = false;
            imgbtnapp.Visible = true;
        }
        //ImgBtnInsert.Visible = true;
        //btnupdate.Visible = false;

        //btncalculate.Visible = false;
        //clear();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            lbllegend.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
            lbllegend.Visible = false;
        }
        btnadd.Visible = false;
        Label1.Text = "";
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        lbllegend.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "Add New Customer Discount Category";
        if (ddlPartyType.SelectedIndex > -1)
        {
            ddlPartyType.SelectedIndex = 0;
        }
        if (ddlpartyname.SelectedIndex > -1)
        {
            ddlpartyname.SelectedIndex = 0;
        }
        Label1.Text = "";
        Button5.Visible = false;
        imgbtnapp.Visible = true;
    }
}
