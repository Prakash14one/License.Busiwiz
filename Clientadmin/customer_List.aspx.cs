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

public partial class CustomerList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblVersion.Text = "This PageVersion Is V3  Develop By priya on 3-5-2016";
        if (!IsPostBack)
        {
           
            Fillddlcountry();
            CheckBox1.Checked = true;   
            FillProduct();
            
            
            ViewState["sortOrder"] = "";
            DateTime dto = DateTime.Now;
            string s_today = dto.ToString("MM/dd/yyyy"); // As String
            txtenddate.Text = Convert.ToString(s_today);
            DateTime monthStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            string s_month = monthStartDate.ToString("MM/dd/yyyy");
            txtstartdate.Text = Convert.ToString(s_month);
           // txtenddate.Text = System.DateTime.Now.ToString();
            fillnewgrid();
            GridView1.Columns[5].Visible = false;
            GridView1.Columns[6].Visible = false;
            GridView1.Columns[7].Visible = false;
            GridView1.Columns[8].Visible = false;
            GridView1.Columns[9].Visible = false;
            GridView1.Columns[10].Visible = false;
            GridView1.Columns[11].Visible = false;
            GridView1.Columns[12].Visible = false;
            GridView1.Columns[18].Visible = false;
            GridView1.Columns[17].Visible = false;
            GridView1.Columns[19].Visible = false;

            chkproduct.Checked = true;
           // chkportal.Checked = true;
            chkprice.Checked = true;
            chklinse.Checked = true;
            chkcontry.Checked = true;
            chkstate.Checked = true;
            chkcity.Checked = true;
            chlicensedate.Checked = true;
            chlicensedatend.Checked = true;
            chkliduration.Checked = true;
            
           // chkserver.Checked = true;
                          

        }
    }

    protected void ddlProductname1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillPortal();
        fillnewgrid();
        //rdmultip_SelectedIndexChanged(sender, e);
        //lblprp1_TextChanged(sender, e);
       
    }
    protected void FillProduct()
    {
        //string strcln = " SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1' and ProductDetail.Active ='True' order  by productversion";
        if (CheckBox1.Checked == true)
        {
            string strcln = " SELECT distinct ProductName, ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1' and VersionInfoMaster.Active='1' order  by ProductName";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                ddlProductname1.DataSource = dtcln;

                ddlProductname1.DataValueField = "ProductId";
                ddlProductname1.DataTextField = "productversion";
                ddlProductname1.DataBind();
                ddlProductname1.Items.Insert(0, "-Select-");
                ddlProductname1.Items[0].Value = "0";
            }
            else
            {
            }
        }
        else
        {
            string strcln1 = " SELECT distinct ProductName, ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + "  order  by ProductName";
            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);
            if (dtcln1.Rows.Count > 0)
            {
                ddlProductname1.DataSource = dtcln1;

                ddlProductname1.DataValueField = "ProductId";
                ddlProductname1.DataTextField = "productversion";
                ddlProductname1.DataBind();
                ddlProductname1.Items.Insert(0, "-Select-");
                ddlProductname1.Items[0].Value = "0";
            }
            else
            {
            }
        }
      

    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Panel4.Visible = false; 
    }
    public void fillnewgrid()
    {
        string hostname = "";

        string active = "";

        
        string strdate = "";
        if (txtenddate.Text == "" || txtstartdate.Text == "")
        {
        }
        else
        {
            //   strdate += " and CompanyMaster.date >='" + Convert.ToDateTime(txtstartdate.Text) + "' and CompanyMaster.date <='" + Convert.ToDateTime(txtenddate.Text) + "'";
            strdate += "  and LicenseMaster.LicenseDate between '" + txtstartdate.Text + "' and '" + txtenddate.Text + "'";
        }
        if (ddlProductname1.SelectedIndex > 0)
        {
            strdate += "   and ProductMaster.ProductId ='" + ddlProductname1.SelectedValue.ToString() + "' ";
        }
        if (ddlsortDomain.SelectedIndex > 0)
        {
            strdate += "   and PortalMasterTbl.Id ='" + ddlsortDomain.SelectedValue.ToString() + "' ";
        }
        if (ddlsortPlan.SelectedIndex > 0)
        {
            strdate += "  and PricePlanMaster.PricePlanId='" + ddlsortPlan.SelectedValue + "'";
        }

        if (ddlfillservernm.SelectedValue == "User Server")
        {
            hostname = "True";
            strdate += "  and  CompanyMaster.HostId='True'";
        }
        else
        {
            hostname = "false";
            strdate += " and  CompanyMaster.HostId='false'";
        }

        if (ddlActive.SelectedValue == "1")
        {
            active = "True";

            strdate += " and CompanyMaster.active='True'";
        }
        else if (ddlActive.SelectedValue == "0")
        {
            active = "false";
            strdate += " and CompanyMaster.active='false'";
        }
        if (ddlcountry.SelectedIndex > 0)
        {
            strdate += "  and  dbo.CompanyMaster.countryId='"+ ddlcountry.SelectedValue  +"'";
        }
        if (ddlstate.SelectedIndex > 0)
        {
            strdate += "  and   dbo.CompanyMaster.StateId='" + ddlstate.SelectedValue + "'";
        }
        if (txtcity.Text != "")
        {
            strdate += "and dbo.CompanyMaster.city like '%" + txtcity.Text.Replace("'", "''") + "%'";
        }
        //if (ddlsortDomain.SelectedValue.ToString() != "---Select All---")
        //{
        //    if (ddlsortPlan.SelectedValue.ToString() != "---Select All---")
        //    {
        //        str = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='" + ddlActive.SelectedValue + "' and PricePlanMaster.active='1' and  CompanyMaster.HostId='" + hostname.ToString() + "' and PortalMasterTbl.Id ='" + ddlsortDomain.SelectedValue.ToString() + "' and PricePlanMaster.PricePlanId='" + ddlsortPlan.SelectedValue + "' " + strdate + " Order by CompanyMaster.date DESC";
        //    }
        //    else
        //    {
        //        str = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='" + ddlActive.SelectedValue + "' and PricePlanMaster.active='1' and  CompanyMaster.HostId='" + hostname.ToString() + "' and PortalMasterTbl.Id ='" + ddlsortDomain.SelectedValue.ToString() + "' " + strdate + " Order by CompanyMaster.date DESC";
        //    }
        //}
        //else
        //{

        //    str = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='" + ddlActive.SelectedValue + "' and PricePlanMaster.active='1' and  CompanyMaster.HostId='" + hostname.ToString() + "' " + strdate + "  Order by CompanyMaster.date DESC ";

        //}
        if (ddlfillservernm.SelectedValue == "User Server")
        {
            // string str = "select * from CompanyMaster Where HostId='1'";
            strdate += " and CompanyMaster.HostId='1' ";
        }
        else if (ddlfillservernm.SelectedValue == "busiwiz Server")
        {
            // string str = "select * from CompanyMaster Where HostId='0'";
            strdate += " and CompanyMaster.HostId='0' ";

        }



        if (txtsortsearch.Text != "")
        {
            strdate += "and (dbo.CompanyMaster.CompanyName like '%" + txtsortsearch.Text.Replace("'", "''") + "%' OR   dbo.ProductMaster.ProductName like '%" + txtsortsearch.Text.Replace("'", "''") + "%' OR dbo.PortalMasterTbl.PortalName like '%" + txtsortsearch.Text.Replace("'", "''") + "%' OR  dbo.PricePlanMaster.PricePlanName like '%" + txtsortsearch.Text.Replace("'", "''") + "%' )";
        }


        //string str = "select DISTINCT PricePlanMaster.StartDate,CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + " ,dbo.CountryMaster.CountryName, dbo.StateMasterTbl.StateName , dbo.ProductMaster.ProductName ,   dbo.PricePlanMaster.PricePlanAmount from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.PortalMasterId1 =PortalMasterTbl.Id inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId   INNER JOIN   dbo.CountryMaster ON dbo.CompanyMaster.countryId = dbo.CountryMaster.CountryId INNER JOIN    dbo.StateMasterTbl ON dbo.CompanyMaster.StateId = dbo.StateMasterTbl.StateId Where  PricePlanMaster.active='1' " + strdate + " Order by PricePlanMaster.StartDate DESC";
        //1052017 String str = " SELECT DISTINCT LicenseMaster.LicenseDate,LicenseMaster.LicensePeriod,CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey,CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName ,CompanyMaster.CompanyId,CountryMaster.CountryName,StateMasterTbl.StateName ,ProductMaster.ProductName , PricePlanMaster.PricePlanAmount from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId =CompanyMaster.PricePlanId inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMaster.CompanyId=CompanyMaster.CompanyId INNER JOIN CountryMaster ON CompanyMaster.countryId = CountryMaster.CountryId INNER JOIN StateMasterTbl ON CompanyMaster.StateId =StateMasterTbl.StateId where PricePlanMaster.active='1' " + strdate + " Order by CompanyMaster.CompanyId DESC"; 
        String str = " SELECT DISTINCT LicenseMaster.LicenseDate,LicenseMaster.LicensePeriod,CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey,CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName ,CompanyMaster.CompanyId,CountryMaster.CountryName,StateMasterTbl.StateName ,ProductMaster.ProductName , PricePlanMaster.PricePlanAmount  FROM dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.ProductMaster ON dbo.ProductMaster.ProductId = dbo.CompanyMaster.ProductId INNER JOIN dbo.OrderMaster ON dbo.OrderMaster.OrderId = dbo.CompanyMaster.OrderId AND dbo.PricePlanMaster.PricePlanId = dbo.OrderMaster.PlanId INNER JOIN dbo.LicenseMaster ON dbo.LicenseMaster.CompanyId = dbo.CompanyMaster.CompanyId INNER JOIN dbo.CountryMaster ON dbo.CompanyMaster.countryId = dbo.CountryMaster.CountryId INNER JOIN dbo.StateMasterTbl ON dbo.CompanyMaster.StateId = dbo.StateMasterTbl.StateId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id where PricePlanMaster.active='1' " + strdate + " Order by CompanyMaster.CompanyId DESC"; 
        
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            DataTable dt_s = new DataTable();
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                string str1 = ds.Rows[i]["LicensePeriod"].ToString();
                int TT = Convert.ToInt32(str1.ToString());
                DateTime target = Convert.ToDateTime(ds.Rows[i]["LicenseDate"].ToString());
                string startdate = target.ToString("MM/dd/yyyy");
                DateTime hh1 = target.AddDays(TT);
               string lastDay1 = hh1.ToString("MM/dd/yyyy");
                if (dt_s.Rows.Count < 1)
                {


                    dt_s.Columns.Add("LicenseDate");
                    dt_s.Columns.Add("LicensePeriod");
                    dt_s.Columns.Add("CompanyName");
                    dt_s.Columns.Add("HostName");
                    dt_s.Columns.Add("phone");
                    dt_s.Columns.Add("PortalName");
                    dt_s.Columns.Add("active");
                    dt_s.Columns.Add("PricePlanName");
                    dt_s.Columns.Add("PricePlanAmount");
                    dt_s.Columns.Add("LicenseKey");
                    dt_s.Columns.Add("CompanyLoginId");
                    dt_s.Columns.Add("city");
                    dt_s.Columns.Add("StateName");
                    dt_s.Columns.Add("CountryName");
                    dt_s.Columns.Add("ProductName");
                    dt_s.Columns.Add("Email");
                    dt_s.Columns.Add("OrderId");
                    dt_s.Columns.Add("LicenseEnddate");
                    dt_s.Columns.Add("CompanyId");
                    dt_s.Columns.Add("ServerId");
                }
                DataRow dr = dt_s.NewRow();
                dr["LicenseDate"] = startdate.ToString();
                dr["LicensePeriod"] = ds.Rows[i]["LicensePeriod"].ToString();
                dr["CompanyName"] = ds.Rows[i]["CompanyName"].ToString();
                dr["HostName"] = ds.Rows[i]["HostName"].ToString();
                dr["phone"] = ds.Rows[i]["phone"].ToString();
                dr["PortalName"] = ds.Rows[i]["PortalName"].ToString();
                dr["active"] = ds.Rows[i]["active"].ToString();
                dr["PricePlanName"] = ds.Rows[i]["PricePlanName"].ToString();
                dr["PricePlanAmount"] = ds.Rows[i]["PricePlanAmount"].ToString();
                dr["LicenseKey"] = ds.Rows[i]["LicenseKey"].ToString();

                dr["CompanyLoginId"] = ds.Rows[i]["CompanyLoginId"].ToString();
                dr["City"] = ds.Rows[i]["city"].ToString();
                dr["StateName"] = ds.Rows[i]["StateName"].ToString();
                dr["CountryName"] = ds.Rows[i]["CountryName"].ToString();
                dr["ProductName"] = ds.Rows[i]["ProductName"].ToString();
                dr["Email"] = ds.Rows[i]["Email"].ToString();
                dr["OrderId"] = ds.Rows[i]["OrderId"].ToString();
                dr["CompanyId"] = ds.Rows[i]["CompanyId"].ToString();
                dr["ServerId"] = ds.Rows[i]["ServerId"].ToString();
                dr["LicenseEnddate"] =lastDay1.ToString();
                dt_s.Rows.Add(dr);


            }


            
            //dr["LicenseDate"] = ds.Rows[i]["LicenseDate"].ToString();
            //dt_s.Rows.Add(dr);
            //}
            GridView1.DataSource = dt_s;
            GridView1.DataBind();

            foreach (GridViewRow ggg in GridView1.Rows)
            {
                ImageButton labelapplied = (ImageButton)ggg.FindControl("ImageButton2");
                Label LabelLogin = (Label)ggg.FindControl("Label24");
                ImageButton labelapply = (ImageButton)ggg.FindControl("ImageButton3");

                string stdd = "select distinct * from CompanyMaster where CompanyLoginId='" + LabelLogin.Text + "'and CompanyActiveByAdmin='1' ";
                SqlDataAdapter dafff = new SqlDataAdapter(stdd, con);
                DataTable dtfff = new DataTable();
                dafff.Fill(dtfff);

                if (dtfff.Rows.Count > 0)
                {
                    labelapplied.Visible = true;
                    labelapply.Visible = false;
                }
                else
                {
                    labelapplied.Visible = false;
                    labelapply.Visible = true;
                }
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
     
    }
    public void fillPricePlan()
    {   //inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId 

        if (CheckBox1.Checked == true)
        {
            //pnlhide.Visible = true;  

            string str = "select PricePlanId,  PricePlanName +' : '+ PricePlanAmount as PricePlanName  from PricePlanMaster inner join PortalMasterTbl on PortalMasterTbl.Id=PricePlanMaster.PortalMasterId1  Where PortalMasterTbl.Id ='" + ddlsortDomain.SelectedValue + "' order by PricePlanName";
            string str22 = "select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PricePlanMaster.PricePlanName AS planname,PricePlanMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='1' and PricePlanMaster.active='1' and  PortalMasterTbl.Id ='" + ddlsortDomain.SelectedValue.ToString() + "'";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                Hashtable hTable = new Hashtable();
                ArrayList duplicateList = new ArrayList();
                foreach (DataRow dtRow in ds.Rows)
                {
                    if (hTable.Contains(dtRow["PricePlanName"]))
                        duplicateList.Add(dtRow);
                    else
                        hTable.Add(dtRow["PricePlanName"], string.Empty);
                }
                foreach (DataRow dtRow in duplicateList)
                    ds.Rows.Remove(dtRow);
                if (ds.Rows.Count > 0)
                {
                    ddlsortPlan.DataSource = ds;
                    ddlsortPlan.DataTextField = "PricePlanName";
                    ddlsortPlan.DataValueField = "PricePlanId";
                    ddlsortPlan.DataBind();
                    ddlsortPlan.Items.Insert(0, "---Select All---");
                }
            }
        }

        else
        {
            // pnlhide.Visible = false;

            string str1 = "select PricePlanId,  PricePlanName +' : '+ PricePlanAmount as PricePlanName  from PricePlanMaster inner join PortalMasterTbl on PortalMasterTbl.Id= PortalMasterId1  Where PortalMasterTbl.Id ='" + ddlsortDomain.SelectedValue + "'order by PricePlanName ";
            string str22 = "select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PricePlanMaster.PricePlanName AS planname,PricePlanMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='1' and PricePlanMaster.active='1' and  PortalMasterTbl.Id ='" + ddlsortDomain.SelectedValue+ "'";

            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            adp1.Fill(ds1);
            if (ds1.Rows.Count > 0)
            {
                Hashtable hTable = new Hashtable();
                ArrayList duplicateList = new ArrayList();
                foreach (DataRow dtRow in ds1.Rows)
                {
                    if (hTable.Contains(dtRow["PricePlanName"]))
                        duplicateList.Add(dtRow);
                    else
                        hTable.Add(dtRow["PricePlanName"], string.Empty);
                }
                foreach (DataRow dtRow in duplicateList)
                    ds1.Rows.Remove(dtRow);
                if (ds1.Rows.Count > 0)
                {
                    ddlsortPlan.DataSource = ds1;
                    ddlsortPlan.DataTextField = "PricePlanName";
                    ddlsortPlan.DataValueField = "PricePlanId";
                    ddlsortPlan.DataBind();
                    ddlsortPlan.Items.Insert(0, "---Select All---");
                }
            }
        }
    }
    public void fillPortal()
    {

        //string str = " Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + ddlProductname1.SelectedValue + "' ) order by PortalName ";
        if (CheckBox1.Checked == true)
        {
            string str = "select * from PortalMasterTbl  inner join ProductMaster on ProductMaster.ProductId=PortalMasterTbl.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ProductMaster.ProductId= '" + ddlProductname1.SelectedValue + "' and ProductDetail.Active='1' and VersionInfoMaster.Active='1' order by PortalName";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                Hashtable hTable = new Hashtable();
                ArrayList duplicateList = new ArrayList();
                foreach (DataRow dtRow in ds.Rows)
                {
                    if (hTable.Contains(dtRow["PortalName"]))
                        duplicateList.Add(dtRow);
                    else
                        hTable.Add(dtRow["PortalName"], string.Empty);
                }
                foreach (DataRow dtRow in duplicateList)
                    ds.Rows.Remove(dtRow);


                if (ds.Rows.Count > 0)
                {
                    ddlsortDomain.DataSource = ds;
                    ddlsortDomain.DataTextField = "PortalName";
                    ddlsortDomain.DataValueField = "Id";
                    ddlsortDomain.DataBind();
                    ddlsortDomain.Items.Insert(0, "---Select All---");
                }
            }
        }
        else
        {
            string str1 = "select * from PortalMasterTbl  inner join ProductMaster on ProductMaster.ProductId=PortalMasterTbl.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ProductMaster.ProductId= '" + ddlProductname1.SelectedValue + "' order by PortalName";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            adp1.Fill(ds1);
            if (ds1.Rows.Count > 0)
            {
                Hashtable hTable = new Hashtable();
                ArrayList duplicateList = new ArrayList();
                foreach (DataRow dtRow in ds1.Rows)
                {
                    if (hTable.Contains(dtRow["PortalName"]))
                        duplicateList.Add(dtRow);
                    else
                        hTable.Add(dtRow["PortalName"], string.Empty);
                }
                foreach (DataRow dtRow in duplicateList)
                    ds1.Rows.Remove(dtRow);


                if (ds1.Rows.Count > 0)
                {
                    ddlsortDomain.DataSource = ds1;
                    ddlsortDomain.DataTextField = "PortalName";
                    ddlsortDomain.DataValueField = "Id";
                    ddlsortDomain.DataBind();
                    ddlsortDomain.Items.Insert(0, "---Select All---");
                }
            }
        }
    }

    //protected void btngo_Clickcheck(object sender, EventArgs e)
    //{
    //    if (chkactive.Checked == true || chkportal.Checked == true || chkcompany.Checked == true)
    //    {
    //        pnl_all.Visible = true;
    //    }
    //    else
    //    {
    //        pnl_all.Visible = false;
    //    }

    //    if (chkactive.Checked == true)
    //    {
    //        pnl_date.Visible = true;

    //    }
    //    else
    //    {
    //        pnl_date.Visible = false;
    //    }

    //    if (chkcompany.Checked == true)
    //    {
    //        pnl_company.Visible = true;
    //    }
    //    else
    //    {
    //        pnl_company.Visible = false;
    //    }

    //    if (chkportal.Checked == true)
    //    {
    //        pnl_portal.Visible = true;
    //    }
    //    else
    //    {
    //        pnl_portal.Visible = false;
    //    }
    //}
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.SelectedDataKey.Value);

            Label Orderid = (Label)GridView1.Rows[GridView1.SelectedIndex].FindControl("lblOrderId");
            Session["Orderid"] = Orderid.Text;
            Response.Redirect("~/CustomerEditNew.aspx?CompanyId=" + PageMgmt.Encrypted(i.ToString()) + "");
        }
        if (e.CommandName == "Next")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.SelectedDataKey.Value);

            Label portal = (Label)GridView1.Rows[GridView1.SelectedIndex].FindControl("lblportal");
            Label Orderid = (Label)GridView1.Rows[GridView1.SelectedIndex].FindControl("lblOrderId");
            Label Serverid = (Label)GridView1.Rows[GridView1.SelectedIndex].FindControl("lblserverId");
            Session["Orderid"] = Orderid.Text;
            Session["Portal"] = portal.Text;
            Response.Redirect("~/ManageCustomerServerMasterandDatabases.aspx?CompanyId=" + i + "&SerId="+Serverid.Text+"");
        }
        if (e.CommandName == "View1")
        {
            string hh = Convert.ToString(e.CommandArgument);
            string gg = hh.ToString();

            string te = "http://members.busiwiz.com/Companymoreinfo.aspx?comid=" + PageMgmt.Encrypted(gg.ToString());
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");
            string str = "select DISTINCT PricePlanMaster.StartDate,CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + " ,dbo.CountryMaster.CountryName, dbo.StateMasterTbl.StateName , dbo.ProductMaster.ProductName ,   dbo.PricePlanMaster.PricePlanAmount from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId   INNER JOIN   dbo.CountryMaster ON dbo.CompanyMaster.countryId = dbo.CountryMaster.CountryId INNER JOIN    dbo.StateMasterTbl ON dbo.CompanyMaster.StateId = dbo.StateMasterTbl.StateId Where  PricePlanMaster.active='1'  Order by PricePlanMaster.StartDate DESC";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count > 0)
            {

                GridView1.DataSource = ds;
                GridView1.DataBind();

                foreach (GridViewRow ggg in GridView1.Rows)
                {
                    Label labelapplied = (Label)ggg.FindControl("Label7");
                    ImageButton llapply = (ImageButton)ggg.FindControl("ImageButton2");
                    ImageButton llapplyq = (ImageButton)ggg.FindControl("ImageButton3");
                    Label LabelLogin = (Label)ggg.FindControl("Label24");
                    Label labelapply = (Label)ggg.FindControl("Label5");

                    string stdd = "select distinct * from CompanyMaster where CompanyLoginId='" + LabelLogin.Text + "'and CompanyActiveByAdmin='1' ";
                    SqlDataAdapter dafff = new SqlDataAdapter(stdd, con);
                    DataTable dtfff = new DataTable();
                    dafff.Fill(dtfff);

                    if (dtfff.Rows.Count > 0)
                    {
                        labelapplied.Visible = true;
                        labelapply.Visible = false;
                        llapply.Visible = false;
                        llapplyq.Visible = false;
                    }
                    else
                    {
                        labelapplied.Visible = false;
                        labelapply.Visible = true;
                        llapply.Visible = false;
                        llapplyq.Visible = false;
                    }
                }
            }
            Button1.Text = "Hide Printable Version";
            Button5.Visible = true;
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[9].Visible = false;
            }
            GridView1.Columns[17].Visible = false;
            btngodate.Visible = false;
           // GridView1.Columns[19].Visible = false;

            
     
        }
        else
        {
            Button1.Text = "Printable Version";
            Button5.Visible = false;
            btngodate.Visible = true;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
             string str1 = "select DISTINCT PricePlanMaster.StartDate,CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + " ,dbo.CountryMaster.CountryName, dbo.StateMasterTbl.StateName , dbo.ProductMaster.ProductName ,   dbo.PricePlanMaster.PricePlanAmount from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId   INNER JOIN   dbo.CountryMaster ON dbo.CompanyMaster.countryId = dbo.CountryMaster.CountryId INNER JOIN    dbo.StateMasterTbl ON dbo.CompanyMaster.StateId = dbo.StateMasterTbl.StateId Where  PricePlanMaster.active='1'  Order by PricePlanMaster.StartDate DESC";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            adp1.Fill(ds1);
            if (ds1.Rows.Count > 0)
            {

                GridView1.DataSource = ds1;
                GridView1.DataBind();

                foreach (GridViewRow ggg in GridView1.Rows)
                {
                    Label labelapplied = (Label)ggg.FindControl("Label7");
                    ImageButton llapply = (ImageButton)ggg.FindControl("ImageButton2");
                    ImageButton llapplyq = (ImageButton)ggg.FindControl("ImageButton3");
                    Label LabelLogin = (Label)ggg.FindControl("Label24");
                    Label labelapply = (Label)ggg.FindControl("Label5");

                    string stdd1 = "select distinct * from CompanyMaster where CompanyLoginId='" + LabelLogin.Text + "'and CompanyActiveByAdmin='1' ";
                    SqlDataAdapter dafff1 = new SqlDataAdapter(stdd1, con);
                    DataTable dtfff1 = new DataTable();
                    dafff1.Fill(dtfff1);

                    if (dtfff1.Rows.Count > 0)
                    {
                        labelapplied.Visible = false;
                        labelapply.Visible = false;
                        llapply.Visible = true;
                        llapplyq.Visible = false;
                    }
                    else
                    {
                        labelapplied.Visible = false;
                        labelapply.Visible = false;
                        llapply.Visible = false;
                        llapplyq.Visible = true;
                    }
                }
            }

            //if (ViewState["deleHide"] != null)
            //{
            //    GridView1.Columns[6].Visible = true;
            //}
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        // fillgrid();
        fillnewgrid();
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


    public void fillServerName()
    {
        string str = "select * from CompanyMaster";

        // string str = "select * from CompanyMaster Case CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName where ClientId='" + Session["ClientId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlfillservernm.DataSource = ds;
        ddlfillservernm.DataTextField = "HostId";
        ddlfillservernm.DataValueField = "CompanyId";
        ddlfillservernm.DataBind();
        ddlfillservernm.Items.Insert(0, "---Select All---");

    }

    protected void ddlfillservernm_SelectedIndexChanged(object sender, EventArgs e)
    {
      
            fillnewgrid();
      
    }
    protected void ddlActive_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillnewgrid();
    }
    protected void ddlsortfilter_SelectedIndexChanged(object sender, EventArgs e)
    {

        fillPricePlan();
        fillnewgrid();

        //if (ddlsortDomain.SelectedItem.ToString() == "---Select All---")
        //{
        //    fillnewgrid();
        //}
        //else
        //{
        //    string str = "select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='1' and PricePlanMaster.active='1' and  PortalMasterTbl.Id ='" + ddlsortDomain.SelectedValue.ToString() + "'";
        //    SqlCommand cmd = new SqlCommand(str, con);
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    adp.Fill(ds);
        //    GridView1.DataSource = ds;
        //    GridView1.DataBind();
        //    

        //}

    }
    //protected void ddlsortPlan_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (ddlsortPlan.SelectedItem.ToString() == "---Select All---")
    //    {
    //        fillnewgrid();
    //    }
    //    else if (ddlsortPlan.SelectedItem.ToString() != "---Select All---")
    //    {
    //        string str = "select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='1' and PricePlanMaster.active='1' and PricePlanMaster.PricePlanId='" + ddlsortPlan.SelectedValue.ToString() + "'";
    //        // string strdata = "SELECT * From"


    //        SqlCommand cmd = new SqlCommand(str, con);
    //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        adp.Fill(ds);
    //        GridView1.DataSource = ds;
    //        GridView1.DataBind();
    //    }
    //}


    protected void txtsortsearch_TextChanged(object sender, EventArgs e)
    {
        //if (txtsortsearch.Text == "")
        //{
        //    fillnewgrid();
        //}
        //else
        //{
        //    searchcompname();
        //}
    }

    public void searchcompname()
    {
        string hostname = "";

        string active = "";
        string strdate = "";
        if (txtenddate.Text == "" || txtstartdate.Text == "")
        {
        }
        else
        {
            //   strdate += " and CompanyMaster.date >='" + Convert.ToDateTime(txtstartdate.Text) + "' and CompanyMaster.date <='" + Convert.ToDateTime(txtenddate.Text) + "'";
            strdate += "  and CompanyMaster.date between '" + txtstartdate.Text + "' and '" + txtenddate.Text + "'";
        }
        if (ddlsortDomain.SelectedValue.ToString() != "---Select All---")
        {
            strdate += "   and PortalMasterTbl.Id ='" + ddlsortDomain.SelectedValue.ToString() + "' ";
        }
        if (ddlsortPlan.SelectedIndex > 0)
        {
            strdate += "  and PricePlanMaster.PricePlanId='" + ddlsortPlan.SelectedValue + "'";
        }

        if (ddlfillservernm.SelectedValue == "User Server")
        {
            hostname = "True";
            strdate += "  and  CompanyMaster.HostId='True'";
        }
        else
        {
            hostname = "false";
            strdate += " and  CompanyMaster.HostId='false'";
        }

        if (ddlActive.SelectedValue == "1")
        {
            active = "True";

            strdate += " and CompanyMaster.active='True'";
        }
        else if (ddlActive.SelectedValue == "0")
        {
            active = "false";
            strdate += " and CompanyMaster.active='false'";
        }
        //if (ddlsortDomain.SelectedValue.ToString() != "---Select All---")
        //{
        //    if (ddlsortPlan.SelectedValue.ToString() != "---Select All---")
        //    {
        //        str = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='" + ddlActive.SelectedValue + "' and PricePlanMaster.active='1' and  CompanyMaster.HostId='" + hostname.ToString() + "' and PortalMasterTbl.Id ='" + ddlsortDomain.SelectedValue.ToString() + "' and PricePlanMaster.PricePlanId='" + ddlsortPlan.SelectedValue + "' " + strdate + " Order by CompanyMaster.date DESC";
        //    }
        //    else
        //    {
        //        str = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='" + ddlActive.SelectedValue + "' and PricePlanMaster.active='1' and  CompanyMaster.HostId='" + hostname.ToString() + "' and PortalMasterTbl.Id ='" + ddlsortDomain.SelectedValue.ToString() + "' " + strdate + " Order by CompanyMaster.date DESC";
        //    }
        //}
        //else
        //{

        //    str = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='" + ddlActive.SelectedValue + "' and PricePlanMaster.active='1' and  CompanyMaster.HostId='" + hostname.ToString() + "' " + strdate + "  Order by CompanyMaster.date DESC ";

        //}
        if (txtsortsearch.Text != "")
        {
            strdate += "and dbo.CompanyMaster.CompanyName like '%" + txtsortsearch.Text.Replace("'", "''") + "%'";
        }


        string str = "select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='1' and PricePlanMaster.active='1' and CompanyMaster.CompanyName LIKE '%" + txtsortsearch.Text + "%' Order by CompanyMaster.date DESC";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        fillnewgrid();
    }
    protected void GridView1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillnewgrid();
    }

    public void feelpage()
    {
        if (txtstartdate.Text != "" && txtenddate.Text != "")
        {
            date();
        }
        else if (ddlfillservernm.SelectedValue == "---Select All---" && ddlsortDomain.SelectedValue.ToString() == "---Select All---" && ddlActive.SelectedValue.ToString() == "---Select All---" && txtsortsearch.Text != "")
        {
            searchcompname();
        }
        else
        {
            string hostname = "";
            if (ddlfillservernm.SelectedValue == "User Server")
            {
                hostname = "True";
            }
            else
            {
                hostname = "false";
            }
            string active = "";
            if (ddlActive.SelectedValue.ToString() == "True")
            {
                active = "True";
            }
            else
            {
                active = "false";
            }
            string str = "";
            if (ddlsortDomain.SelectedValue.ToString() != "---Select All---")
            {
                if (ddlsortPlan.SelectedValue.ToString() != "---Select All---")
                {
                    str = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='" + ddlActive.SelectedValue + "' and PricePlanMaster.active='1' and  CompanyMaster.HostId='" + hostname.ToString() + "' and PortalMasterTbl.Id ='" + ddlsortDomain.SelectedValue.ToString() + "' and PricePlanMaster.PricePlanId='" + ddlsortPlan.SelectedValue + "' Order by CompanyMaster.date DESC";
                }
                else
                {
                    str = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='" + ddlActive.SelectedValue + "' and PricePlanMaster.active='1' and  CompanyMaster.HostId='" + hostname.ToString() + "' and PortalMasterTbl.Id ='" + ddlsortDomain.SelectedValue.ToString() + "' Order by CompanyMaster.date DESC";

                }
            }
            else
            {
                str = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='" + active + "' and PricePlanMaster.active='1' and  CompanyMaster.HostId='" + hostname.ToString() + "' Order by CompanyMaster.date DESC";

            }

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }
    }
    protected void btngodate_Clickcccccc(object sender, EventArgs e)
    {
        Panel4.Visible = false;
        if (chkproduct.Checked == true)
        {
            GridView1.Columns[12].Visible = false;
            fillnewgrid();
        }
        else
        {
            GridView1.Columns[12].Visible = true;
            fillnewgrid();
        }

        //if (chkportal.Checked == true)
        //{
        //    GridView1.Columns[6].Visible = false;
        //    fillnewgrid();
        //}
        //else
        //{
        //    GridView1.Columns[6].Visible = true;
        //    fillnewgrid();
        //}
        if (chkprice.Checked == true)
        {
            GridView1.Columns[5].Visible = false;
            fillnewgrid();
        }
        else
        {
            GridView1.Columns[5].Visible = true;
            fillnewgrid();
        }
        if (chklinse.Checked == true)
        {
            GridView1.Columns[7].Visible = false;
            fillnewgrid();
        }
        else
        {
            GridView1.Columns[7].Visible = true;
            fillnewgrid();
        }

        if (chkcontry.Checked == true)
        {
            GridView1.Columns[11].Visible = false;
            fillnewgrid();
        }
        else
        {
            GridView1.Columns[11].Visible = true;
            fillnewgrid();
        }
        if (chkstate.Checked == true)
        {
            GridView1.Columns[10].Visible = false;
            fillnewgrid();
        }
        else
        {
            GridView1.Columns[10].Visible = true;
            fillnewgrid();
        }
        if (chkcity.Checked == true)
        {
            GridView1.Columns[9].Visible = false;
            fillnewgrid();
        }
        else
        {
            GridView1.Columns[9].Visible = true;
            fillnewgrid();
        }
        if (chlicensedate.Checked == true)
        {
            GridView1.Columns[17].Visible = false;
            fillnewgrid();
        }
        else
        {
            GridView1.Columns[17].Visible = true;
            fillnewgrid();
        }
        if (chkliduration.Checked == true)
        {
            GridView1.Columns[18].Visible = false;
            fillnewgrid();
        }
        else
        {
            GridView1.Columns[18].Visible = true;
            fillnewgrid();
        }
        if (chlicensedatend.Checked == true)
        {
            GridView1.Columns[19].Visible = false;
            fillnewgrid();
        }
        else
        {
            GridView1.Columns[19].Visible = true;
            fillnewgrid();
        }
        //if (chkserver.Checked == true)
        //{
        //    GridView1.Columns[12].Visible = false;
        //    fillnewgrid();
        //}
        //else
        //{
        //    GridView1.Columns[12].Visible = true;
        //    fillnewgrid();
        //}

    }
    protected void btngodate_Clickopenpnl(object sender, EventArgs e)
    {

     Panel4.Visible = true; 
    }

    protected void btngodate_Click(object sender, EventArgs e)
    {
      //  date();
        fillnewgrid();
    }
    public void date()
    {
        string str = "select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='1' and PricePlanMaster.active='1' and CompanyMaster.date >='" + Convert.ToDateTime(txtstartdate.Text) + "' and CompanyMaster.date <='" + Convert.ToDateTime(txtenddate.Text) + "' Order by CompanyMaster.date DESC";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }


  
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
  
        Fillddlstatelist();
    }
    protected void Fillddlcountry()
    {
        dt = new DataTable();
        dt = SelectCountryMaster();
        ddlcountry.DataSource = dt;
        ddlcountry.DataBind();
        ddlcountry.Items.Insert(0, "-Select-");
        ddlcountry.SelectedIndex = 0;
        ddlcountry.SelectedItem.Value = "0";
    }
    protected DataTable SelectCountryMaster()
    {
        SqlCommand cmd = new SqlCommand();
        dt = new DataTable();
        cmd.Connection = con;
        cmd.CommandText = "SelectCountryMaster";
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        return dt;
    }
    protected void Fillddlstatelist()
    {

        SqlCommand cmd = new SqlCommand();
        if (ddlcountry.SelectedIndex > 0)
        {
            cmd.CommandText = "SELECT StateMasterTbl.StateId, StateMasterTbl.StateName, StateMasterTbl.State_Code, CountryMaster.CountryId AS CountryId, CountryMaster.CountryName, CountryMaster.Country_Code FROM StateMasterTbl LEFT OUTER JOIN  CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId WHERE (CountryMaster.CountryId =" + ddlcountry.SelectedValue + ") order by StateMasterTbl.StateName ";

        }
        else
        {
            cmd.CommandText = "SELECT StateMasterTbl.StateId, StateMasterTbl.StateName, StateMasterTbl.State_Code, CountryMaster.CountryId AS CountryId, CountryMaster.CountryName, CountryMaster.Country_Code FROM StateMasterTbl LEFT OUTER JOIN  CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId order by StateMasterTbl.StateName";

        }
        cmd.CommandType = CommandType.Text;
        DataTable dt = new DataTable();
        cmd.Connection = con;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        ddlstate.DataSource = dt;
        ddlstate.DataValueField = "StateId";
        ddlstate.DataTextField = "StateName";
        ddlstate.DataBind();
        ddlstate.Items.Insert(0, "-Select-");

        ddlstate.Items[0].Value = "0";
    }
    protected DataTable SelectStateMasterWithCountry(Int32 CountryId)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT StateMasterTbl.StateId, StateMasterTbl.StateName, StateMasterTbl.State_Code, CountryMaster.CountryId AS CountryId, CountryMaster.CountryName, CountryMaster.Country_Code FROM         StateMasterTbl LEFT OUTER JOIN  CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId WHERE (CountryMaster.CountryId =" + CountryId + ")";
        cmd.CommandType = CommandType.Text;
        DataTable dt = new DataTable();
        cmd.Connection = con;
        //  cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.Int));
        //  cmd.Parameters["@CountryId"].Value = CountryId;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        //   dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            FillProduct();
        }
        else
        {
            FillProduct();
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        //string id = GridView6.Rows[j].Cells[0].Text;

        Label companyid = (Label)GridView1.Rows[j].FindControl("Label24");
        string str = "select * from CompanyActivationRequestTbl where  emailgeneratecompanyid='"+companyid.Text+"' and companyactive='0'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adpt.Fill(ds);
       
           ViewState["activationid"]=ds.Rows[0]["Id"].ToString();
          Response.Redirect("http://license.busiwiz.com/companyapproval.aspx?comid=" + PageMgmt.Encrypted(companyid.Text)+"&id="+ViewState["activationid"].ToString()+"");
        //Response.Redirect("http://license.busiwiz.com/companyapproval.aspx");
    }
    protected void ddlsortPlan_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillnewgrid();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}
