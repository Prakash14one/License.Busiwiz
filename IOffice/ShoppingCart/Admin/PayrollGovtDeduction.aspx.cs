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

public partial class PayrollGovtDeduction : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);
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


        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {

            ViewState["sortOrder"] = "";
            Pagecontrol.dypcontrol(Page, page);

            Fillwarehouse();
            ddlfilterbus_SelectedIndexChanged(sender, e);
        }

    }

    protected void Fillwarehouse()
    {


        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {

            ddlstore.DataSource = ds;
            ddlstore.DataValueField = "WareHouseId";
            ddlstore.DataTextField = "Name";
            ddlstore.DataBind();

            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {

                ddlstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);


            }
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            //if (GridView1.Columns[7].Visible == true)
            //{
            //    ViewState["edith"] = "tt";
            //    GridView1.Columns[7].Visible = false;
            //}
            //if (GridView1.Columns[8].Visible == true)
            //{
            //    ViewState["deleteh"] = "tt";
            //    GridView1.Columns[8].Visible = false;
            //}

        }
        else
        {



            Button1.Text = "Printable Version";
            Button7.Visible = false;
            //if (ViewState["edith"] != null)
            //{
            //    GridView1.Columns[7].Visible = true;
            //}
            //if (ViewState["deleteh"] != null)
            //{
            //    GridView1.Columns[8].Visible = true;
            //}

        }
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

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }

    protected void ddlfilterbus_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnsave.Visible = false;
        string strrrr = "select top(1) StateMasterTbl.Statename,CountryMaster.CountryName,CompanyWebsiteAddressMasterId,CompanyWebsiteAddressMaster.State,CompanyWebsiteAddressMaster.Country from CompanyWebsiteAddressMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join AddressTypeMaster on AddressTypeMaster.AddressTypeMasterId=CompanyWebsiteAddressMaster.AddressTypeMasterId inner join CountryMaster on " +
                   "CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country inner join StateMasterTbl on " +
                   "StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State  where WareHouseMaster.WareHouseId='" + ddlstore.SelectedValue + "' and AddressTypeMaster.Name='Business Address' order by CompanyWebsiteAddressMasterId Desc ";
        DataTable drg = select(strrrr);
        if (drg.Rows.Count > 0)
        {
            lblcountry.Text = Convert.ToString(drg.Rows[0]["CountryName"]);
            lblstate.Text = Convert.ToString(drg.Rows[0]["Statename"]);
            string stren = "select Distinct MatchingEmployee, tax_name+ ' : '+Sortname as Dedname,Payrolltax_id,CrAccId,DrAccId,CrAccumulateLiab,PayrollGovtDeductionTbl.Id as GovId from PayrolltaxMaster Left join PayrollGovtDeductionTbl on PayrollGovtDeductionTbl.PayrolltaxMasterId=PayrolltaxMaster.Payrolltax_id and PayrollGovtDeductionTbl.Whid='" + ddlstore.SelectedValue + "'  where  PayrolltaxMaster.Country_id='" + drg.Rows[0]["Country"] + "' and  (Sameruleforcountryallstates='1'  or PayrolltaxMaster.Payrolltax_id " +
                "in(select Distinct PayrolltaxMaster.Payrolltax_id from PayrolltaxMaster inner join PayRollTaxDetail on PayRollTaxDetail.Payrolltaxmasterid= " +
                "PayrolltaxMaster.Payrolltax_id inner join Payrolltaxdetailwithstate on " +
                "Payrolltaxdetailwithstate.PayrolltaxdetailId=PayRollTaxDetail.Id and Payrolltaxdetailwithstate.StateId='" + drg.Rows[0]["State"] + "'" +
                "and PayrolltaxMaster.Country_id='" + drg.Rows[0]["Country"] + "' and PayrolltaxMaster.Status='1' and  PayRollTaxDetail.Status='1') )";
            DataTable dtc = select(stren);

            lblCompany.Text = "Business : " + ddlstore.SelectedItem.Text;
            lblcst.Text = "Country : " + lblcountry.Text + ", State : " + lblstate.Text;
            if (dtc.Rows.Count > 0)
            {

                GridView1.DataSource = dtc;

                //DataView myDataView = new DataView();
                //myDataView = dtc.DefaultView;
                //if (hdnsortExp.Value != string.Empty)
                //{
                //    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                //}


                GridView1.DataBind();
                fillgstep();
               
            }

        }

    }
    protected void fillgstep()
    {
        DataTable dtall = select("SELECT Distinct (AccountMaster.AccountName) +':'+(GroupCompanyMaster.groupdisplayname) as Classgroup,AccountMaster.AccountId  FROM ClassTypeCompanyMaster inner join  ClassCompanyMaster on ClassCompanyMaster.classtypecompanymasterid=ClassTypeCompanyMaster.Id  inner join GroupCompanyMaster on GroupCompanyMaster.classcompanymasterid=ClassCompanyMaster.Id  inner join AccountMaster  on AccountMaster.GroupId=GroupCompanyMaster.GroupId  where ClassCompanyMaster.ClassId='5' and  AccountMaster.Whid='" + ddlstore.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlstore.SelectedValue + "' order by Classgroup");

        DataTable dtex = select("SELECT Distinct (AccountMaster.AccountName) +':'+(GroupCompanyMaster.groupdisplayname) as Classgroup,AccountMaster.AccountId  FROM ClassTypeCompanyMaster inner join  ClassCompanyMaster on ClassCompanyMaster.classtypecompanymasterid=ClassTypeCompanyMaster.Id  inner join GroupCompanyMaster on GroupCompanyMaster.classcompanymasterid=ClassCompanyMaster.Id  inner join AccountMaster  on AccountMaster.GroupId=GroupCompanyMaster.GroupId  where ClassTypeCompanyMaster.classtypeid='15' and AccountMaster.Whid='" + ddlstore.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlstore.SelectedValue + "' order by Classgroup");

        DataTable dtliab = select("SELECT Distinct (AccountMaster.AccountName) +':'+(GroupCompanyMaster.groupdisplayname) as Classgroup,AccountMaster.AccountId  FROM ClassTypeCompanyMaster inner join  ClassCompanyMaster on ClassCompanyMaster.classtypecompanymasterid=ClassTypeCompanyMaster.Id  inner join GroupCompanyMaster on GroupCompanyMaster.classcompanymasterid=ClassCompanyMaster.Id  inner join AccountMaster  on AccountMaster.GroupId=GroupCompanyMaster.GroupId  where  ClassCompanyMaster.ClassId='5' and AccountMaster.Whid='" + ddlstore.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlstore.SelectedValue + "' order by Classgroup");

        int btnvew = 0;
        int matchcont = 0;
        GridView1.Columns[2].Visible = true;
        GridView1.Columns[3].Visible = true;
        foreach (GridViewRow item in GridView1.Rows)
        {
            DropDownList ddlreditac = (DropDownList)item.FindControl("ddlreditac");
            DropDownList ddldebitac = (DropDownList)item.FindControl("ddldebitac");
            DropDownList ddlaccumulateliab = (DropDownList)item.FindControl("ddlaccumulateliab");
            Label lblgovId = (Label)item.FindControl("lblgovId");
            Label lbldbacc = (Label)item.FindControl("lbldbacc");
            Label lblcracc = (Label)item.FindControl("lblcracc");
            Label lblcraccum = (Label)item.FindControl("lblcraccum");
            Label lblempcont = (Label)item.FindControl("lblempcont");
            if (dtall.Rows.Count > 0)
            {
                ddlreditac.DataSource = dtall;
                ddlreditac.DataTextField = "Classgroup";
                ddlreditac.DataValueField = "AccountId";
                ddlreditac.DataBind();
                if (Convert.ToString(lblcracc.Text) != "")
                {
                    btnvew = 1;
                    ddlreditac.SelectedIndex = ddlreditac.Items.IndexOf(ddlreditac.Items.FindByValue(Convert.ToString(lblcracc.Text)));
                }

            }
            if (Convert.ToString(lblempcont.Text) == "True")
            {
                matchcont = 1;
                ddldebitac.Enabled = true;
                ddlaccumulateliab.Enabled = true;
                if (dtex.Rows.Count > 0)
                {
                    ddldebitac.DataSource = dtex;
                    ddldebitac.DataTextField = "Classgroup";
                    ddldebitac.DataValueField = "AccountId";
                    ddldebitac.DataBind();
                    if (Convert.ToString(lbldbacc.Text) != "")
                    {
                        ddldebitac.SelectedIndex = ddldebitac.Items.IndexOf(ddldebitac.Items.FindByValue(Convert.ToString(lbldbacc.Text)));
                    }
                }
                if (dtliab.Rows.Count > 0)
                {

                    ddlaccumulateliab.DataSource = dtliab;
                    ddlaccumulateliab.DataTextField = "Classgroup";
                    ddlaccumulateliab.DataValueField = "AccountId";
                    ddlaccumulateliab.DataBind();
                    if (Convert.ToString(lblcraccum.Text) != "")
                    {
                        ddlaccumulateliab.SelectedIndex = ddlaccumulateliab.Items.IndexOf(ddlaccumulateliab.Items.FindByValue(Convert.ToString(lblcraccum.Text)));
                    }
                }
            }
            else
            {
                ddldebitac.Items.Insert(0, "Not Required");
                ddldebitac.Items[0].Value = "0";
                ddldebitac.Enabled = false;
                ddlaccumulateliab.Items.Insert(0, "Not Required");
                ddlaccumulateliab.Items[0].Value = "0";
                ddlaccumulateliab.Enabled = false;
            }
        }

        if (matchcont == 0)
        {
            GridView1.Columns[2].Visible = false;
            GridView1.Columns[3].Visible = false;
        }
        if (btnvew == 1)
        {
            btnEdit.Visible = true;
            btnsave.Visible = false;
            pnledit.Enabled = false;
        }
        else
        {
            btnsave.Visible = true;
            btnEdit.Visible = false;
            pnledit.Enabled = true;
        }

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        int recsave = 0;
        foreach (GridViewRow item in GridView1.Rows)
        {
            recsave = 1;
            string payroltaxid = GridView1.DataKeys[item.RowIndex].Value.ToString();
            DropDownList ddlreditac = (DropDownList)item.FindControl("ddlreditac");
            DropDownList ddldebitac = (DropDownList)item.FindControl("ddldebitac");
            DropDownList ddlaccumulateliab = (DropDownList)item.FindControl("ddlaccumulateliab");
            Label lblgovId = (Label)item.FindControl("lblgovId");
            if (Convert.ToString(lblgovId.Text) == "")
            {
                string strd = "insert into PayrollGovtDeductionTbl(CrAccId,DrAccId,CrAccumulateLiab,Whid,PayrolltaxMasterId) values ('" + ddlreditac.SelectedValue + "','" + ddldebitac.SelectedValue + "','" + ddlaccumulateliab.SelectedValue + "','" + ddlstore.SelectedValue + "','" + payroltaxid + "') ";

                SqlCommand cmd1d = new SqlCommand(strd, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1d.ExecuteNonQuery();
            }
            else
            {
                string strd = "Update PayrollGovtDeductionTbl set CrAccId='" + ddlreditac.SelectedValue + "',DrAccId='" + ddldebitac.SelectedValue + "',CrAccumulateLiab='" + ddlaccumulateliab.SelectedValue + "' where Id='" + lblgovId.Text + "' ";

                SqlCommand cmd1d = new SqlCommand(strd, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1d.ExecuteNonQuery();
            }

        }
        ddlfilterbus_SelectedIndexChanged(sender, e);
        if (recsave == 1)
        {
            lblmsg.Visible = true;

            lblmsg.Text = "Record save successfully";
        }

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        pnledit.Enabled = true;
        btnEdit.Visible = false;
        btnsave.Visible = true;
    }
    protected void imgAdd2_Click(object sender, ImageClickEventArgs e)
    {
        string te = "AccountMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void imgRefresh2_Click(object sender, ImageClickEventArgs e)
    {
        ddlfilterbus_SelectedIndexChanged(sender, e);
    }
}