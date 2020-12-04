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
using System.Text;
using System.Net;
using System.Net.Mail;


public partial class Add_Party_Master : System.Web.UI.Page
{
    SqlConnection con;
    string strconn;
    String qryStr;
    int groupid = 0;
    string accid = "";
    int classid = 0;
    string compid1;
    string wh;
 //   DocumentCls1 clsDocument = new DocumentCls1();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        PageConn pgcon = new PageConn();
        strconn = pgcon.dynconn.ConnectionString;
        con = pgcon.dynconn;
        vi();
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        compid1 = Session["Comid"].ToString();

        Page.Title = pg.getPageTitle(page);
        lblmsg.Text = "";
        string pass1 = tbPassword.Text;
        tbPassword.Attributes.Add("Value", pass1);
        string pass2 = tbConPassword.Text;
        tbConPassword.Attributes.Add("Value", pass2);

        ModalPopupExtender145.Hide();
        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";

            vi();
            string str = "Select Id, Name From PartyMasterCategory where Name in('Vendor') order by Name ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable datat = new DataTable();
            adp.Fill(datat);
            if (datat.Rows.Count > 0)
            {
                ddlpartycate.DataSource = datat;
                ddlpartycate.DataTextField = "Name";
                ddlpartycate.DataValueField = "Id";
                ddlpartycate.DataBind();

                ddlpartycategory.DataSource = datat;
                ddlpartycategory.DataTextField = "Name";
                ddlpartycategory.DataValueField = "Id";
                ddlpartycategory.DataBind();
               
            }
            DataTable dswh = ClsStore.SelectStorename();
            ddwarehouse.DataSource = dswh;
            ddwarehouse.DataTextField = "Name";
            ddwarehouse.DataValueField = "WareHouseId";

            ddwarehouse.DataBind();

            ddshorting.DataSource = dswh;
            ddshorting.DataTextField = "Name";
            ddshorting.DataValueField = "WareHouseId";

            ddshorting.DataBind();
            ddshorting.Items.Insert(0, "All");
            ddshorting.Items[0].Value = "0";
            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }

            if (Request.QueryString["id"] != null)
            {
                if (Request.QueryString["id"].ToString() != null)
                {

                    //****  Radhika Chnages
                    //qryStr = "select * from PartytTypeMaster order by PartType";//<> 'employee'
                    //******************
                    qryStr = "select * from PartytTypeMaster order by PartType where PartyCategoryId='" + ddlpartycate.SelectedValue + "' and  compid='" + compid1 + "'";
                    ddlPartyType.DataSource = (DataSet)fillddl(qryStr);
                    fillddlOther(ddlPartyType, "PartType", "PartyTypeId");
                    ddlPartyType.Items.Insert(0, "-Select-");
                    ddlPartyType.Items[0].Value = "0";
                    //------------------------------------------------------------

                   
                    //-------------------------------------------------------------
                    qryStr = "select CountryId,CountryName from CountryMaster order by CountryName";
                    ddlCountry.DataSource = (DataSet)fillddl(qryStr);
                    fillddlOther(ddlCountry, "CountryName", "CountryId");
                    ddlCountry.Items.Insert(0, "-Select-");
                    ddlCountry.Items[0].Value = "0";
                   
                    ddlState.Items.Insert(0, "-Select-");
                    ddlState.Items[0].Value = "0";
                    ddlCity.Items.Insert(0, "-Select-");
                    ddlCity.Items[0].Value = "0";

                    qryStr = "select BalanceLimitTypeId,BalanceLimitType from BalanceLimitType where compid= '" + Session["comid"] + "' order by BalanceLimitType";
                    dddlbalance.DataSource = (DataSet)fillddl(qryStr);
                    fillddlOther(dddlbalance, "BalanceLimitType", "BalanceLimitTypeId");

                    dddlbalance.Items.Insert(0, "-Select-");
                    dddlbalance.Items[0].Value = "0";
                    dddlbalance.SelectedIndex = dddlbalance.Items.IndexOf(dddlbalance.Items.FindByValue("2"));

                    qryStr = "select GroupId,GroupName from GroupMaster where GroupId in('2','5','15','20') order by GroupName";
                    ddlGroup.DataSource = (DataSet)fillddl(qryStr);
                    fillddlOther(ddlGroup, "GroupName", "GroupId");


                    ddldesignation.Items.Insert(0, "-Select-");
                    ddldesignation.Items[0].Value = "0";
                    qryStr = " SELECT  [StatusMaster].StatusId as StatusId,[StatusCategory].StatusCategory +' : '+ [StatusMaster].StatusName as StatusName, [StatusMaster].StatusCategoryMasterId,[StatusCategory].[StatusCategoryMasterId] FROM StatusMaster " +
                                  "  inner join [StatusCategory] on StatusMaster.StatusCategoryMasterId=[StatusCategory].StatusCategoryMasterId WHERE ([StatusCategory].compid = '" + Session["Comid"] + "') order by [StatusCategory].StatusCategory ";
                    ddlstatus.DataSource = (DataSet)fillddl(qryStr);
                    fillddlOther(ddlstatus, "StatusName", "StatusId");
                    ddlstatus.Items.Insert(0, "-Select-");
                    ddlstatus.Items[0].Value = "0";

                    ViewState["partyid"] = Request.QueryString["id"].ToString();

                    DataTable dtaccsel = new DataTable();

                    //*********  Radhika Solaniki
                    // dtaccsel = (DataTable)select("Select Account from Party_master where PartyID='" + ViewState["partyid"] + "'");
                    dtaccsel = (DataTable)select("Select Account from Party_master,PartytTypeMaster where PartyID='" + ViewState["partyid"] + "' and Party_master.PartyTypeId=PartytTypeMaster.PartyTypeId and PartytTypeMaster.compid='" + compid1 + "' ");
                    if (dtaccsel.Rows.Count > 0)
                    {
                        ViewState["accoutid"] = dtaccsel.Rows[0]["Account"].ToString();
                    }
                    DataTable dtsel = new DataTable();
                    dtsel = (DataTable)select("Select * from Party_master where PartyID='" + ViewState["partyid"] + "'");
                    if (dtsel.Rows.Count > 0)
                    {
                        imgbtnedit.Visible = true;
                        imgbtnupdate.Visible = false;
                        btnSubmit.Visible = false;
                        controlenable(false);
                        btnCancel.Visible = true;
                        btnCancel.Enabled = false;
                        //imgbtndelete.Visible = true;
                        ddlPartyType.SelectedIndex = ddlPartyType.Items.IndexOf(ddlPartyType.Items.FindByValue(dtsel.Rows[0]["PartyTypeId"].ToString()));
                        object ob = new object();
                        EventArgs evt = new EventArgs();
                        ddlPartyType_SelectedIndexChanged(ob, evt);

                        ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dtsel.Rows[0]["Country"].ToString()));
                        object obs = new object();
                        EventArgs evts = new EventArgs();
                        ddlCountry_SelectedIndexChanged(obs, evts);

                        ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dtsel.Rows[0]["State"].ToString()));
                        object obc = new object();
                        EventArgs evtc = new EventArgs();
                        ddlState_SelectedIndexChanged(obc, evtc);

                        ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(dtsel.Rows[0]["City"].ToString()));
                        object o = new object();
                        EventArgs ev = new EventArgs();
                        ddlCity_SelectedIndexChanged(o, ev);

                        ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue(dtsel.Rows[0]["StatusMasterId"].ToString()));
                        tbWebsite.Text = dtsel.Rows[0]["Website"].ToString();
                        tbFax.Text = dtsel.Rows[0]["Fax"].ToString();
                        tbEmail.Text = dtsel.Rows[0]["Email"].ToString();
                        tbGSTNumber.Text = dtsel.Rows[0]["GSTno"].ToString();
                        tbITNumber.Text = dtsel.Rows[0]["Incometaxno"].ToString();
                        tbName.Text = dtsel.Rows[0]["Contactperson"].ToString();
                        tbCompanyName.Text = dtsel.Rows[0]["Compname"].ToString();
                        tbPhone.Text = dtsel.Rows[0]["Phoneno"].ToString();
                        tbAddress.Text = dtsel.Rows[0]["Address"].ToString();

                        DataTable dtselbal = new DataTable();
                        dtselbal = (DataTable)select("Select * from AccountBalanceLimit where AccountId='" + ViewState["accoutid"] + "'");

                        if (dtselbal.Rows.Count > 0)
                        {
                            dddlbalance.SelectedIndex = dddlbalance.Items.IndexOf(dddlbalance.Items.FindByValue(dtselbal.Rows[0]["BalanceLimitTypeId"].ToString()));
                            txtbal.Text = dtselbal.Rows[0]["BalancelimitAmount"].ToString();
                            txtdate.Text = dtselbal.Rows[0]["DateTime"].ToString();
                        }

                        DataTable dtseluser = new DataTable();
                        dtseluser = (DataTable)select("Select * from User_master where PartyID='" + ViewState["partyid"] + "'");
                        if (dtseluser.Rows.Count > 0)
                        {
                            tbUserName.Text = dtseluser.Rows[0]["Username"].ToString();
                            ddldept.SelectedIndex = ddldept.Items.IndexOf(ddldept.Items.FindByValue(dtseluser.Rows[0]["Department"].ToString()));

                            object obu = new object();
                            EventArgs evtu = new EventArgs();
                            ddldept_SelectedIndexChanged(obu, evtu);

                            ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(dtseluser.Rows[0]["DesigantionMasterId"].ToString()));
                            tbExtension.Text = dtseluser.Rows[0]["Extention"].ToString();
                            tbZipCode.Text = dtseluser.Rows[0]["zipcode"].ToString();
                            ddlActive.SelectedIndex = ddlActive.Items.IndexOf(ddlActive.Items.FindByValue(dtseluser.Rows[0]["Active"].ToString()));

                            DataTable dtpass = new DataTable();
                            dtpass = (DataTable)select("Select password from Login_Master where UserID ='" + dtseluser.Rows[0]["UserID"].ToString() + "'");
                            if (dtpass.Rows.Count > 0)
                            {
                                tbPassword.Text = ClsEncDesc.Decrypted(dtpass.Rows[0]["password"].ToString());
                                tbPassword.Enabled = false;
                            }

                        }

                        DataTable dtgro = new DataTable();
                        dtgro = (DataTable)select("Select GroupId from AccountMaster where AccountId='" + ViewState["accoutid"] + "'");
                        if (dtgro.Rows.Count > 0)
                        {
                            ddlGroup.SelectedIndex = ddlGroup.Items.IndexOf(ddlGroup.Items.FindByValue(dtgro.Rows[0]["GroupId"].ToString()));
                        }


                    }
                }
            }
            else
            {
                tbUserName.Text = "Default";

                tbPassword.Attributes.Add("Value", "Default");
                tbConPassword.Attributes.Add("Value", "Default");
                ddlpartycate_SelectedIndexChanged(sender, e);
                //qryStr = "select * from PartytTypeMaster where compid='" + compid1 + "' and PartType not in('Employee') order by PartType";//<> 'employee'
                //ddlPartyType.DataSource = (DataSet)fillddl(qryStr);
                //fillddlOther(ddlPartyType, "PartType", "PartyTypeId");
                //ddlPartyType.Items.Insert(0, "Select");
                ddlPartyType.SelectedIndex = ddlPartyType.Items.IndexOf(ddlPartyType.Items.FindByText("Vendor")); ;
                //ddlPartyType_SelectedIndexChanged(sender, e);

                fillpartytype();
                //ddlfilterbypartytype.DataSource = (DataSet)fillddl(qryStr);
                // fillddlOther(ddlfilterbypartytype, "PartType", "PartyTypeId");
                // ddlfilterbypartytype.Items.Insert(0, "All");
                // ddlfilterbypartytype.Items[0].Value = "0";
                ddlPartyType.SelectedIndex = 3;
                ddlPartyType_SelectedIndexChanged(sender, e);
                qryStr = "select CountryId,CountryName from CountryMaster order by CountryName";
                ddlCountry.DataSource = (DataSet)fillddl(qryStr);
                fillddlOther(ddlCountry, "CountryName", "CountryId");
                ddlCountry.Items.Insert(0, "-Select-");
                ddlCountry.Items[0].Value = "0";

                ddlState.Items.Insert(0, "-Select-");
                ddlState.Items[0].Value = "0";
                ddlCity.Items.Insert(0, "-Select-");
                ddlCity.Items[0].Value = "0";

                qryStr = "select BalanceLimitTypeId,BalanceLimitType from BalanceLimitType where compid='" + Session["comid"] + "' order by BalanceLimitType ";
                dddlbalance.DataSource = (DataSet)fillddl(qryStr);
                fillddlOther(dddlbalance, "BalanceLimitType", "BalanceLimitTypeId");


                dddlbalance.SelectedIndex = dddlbalance.Items.IndexOf(dddlbalance.Items.FindByValue("2"));

                qryStr = "select GroupId,GroupName from GroupMaster where GroupId in('2','5','15','20') order by GroupName";
                ddlGroup.DataSource = (DataSet)fillddl(qryStr);
                fillddlOther(ddlGroup, "GroupName", "GroupId");

                //qryStr = " SELECT  [StatusMaster].StatusId as StatusId,[StatusCategory].StatusCategory +' : '+ [StatusMaster].StatusName as StatusName, [StatusMaster].StatusCategoryMasterId,[StatusCategory].[StatusCategoryMasterId] FROM StatusMaster " +
                //       "  inner join [StatusCategory] on StatusMaster.StatusCategoryMasterId=[StatusCategory].StatusCategoryMasterId WHERE ([StatusMaster].StatusCategoryMasterId = 19) order by [StatusCategory].StatusCategory ";
                qryStr = " SELECT  [StatusMaster].StatusId as StatusId,[StatusCategory].StatusCategory +' : '+ [StatusMaster].StatusName as StatusName, [StatusMaster].StatusCategoryMasterId,[StatusCategory].[StatusCategoryMasterId] FROM StatusMaster " +
                                   "  inner join [StatusCategory] on StatusMaster.StatusCategoryMasterId=[StatusCategory].StatusCategoryMasterId WHERE ([StatusCategory].compid = '" + Session["Comid"] + "') order by [StatusCategory].StatusCategory ";

                ddlstatus.DataSource = (DataSet)fillddl(qryStr);
                fillddlOther(ddlstatus, "StatusName", "StatusId");
                ddlfilterstatuscategory.DataSource = (DataSet)fillddl(qryStr);
                fillddlOther(ddlfilterstatuscategory, "StatusName", "StatusId");
                ddlfilterstatuscategory.Items.Insert(0, "All");
                ddlfilterstatuscategory.Items[0].Value = "0";
                // ddlstatus.Items.Insert(0, "Select");
                //ddlstatus.Items[0].Value = "0";
                ddlstatus.SelectedIndex = 0;
                ddwarehouse_SelectedIndexChanged(sender, e);
                fillgriddata();

                txtdate.Text = System.DateTime.Now.ToShortDateString();
            }


        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlState.Items.Clear();
        ddlCity.Items.Clear();
        if (ddlCountry.SelectedIndex > 0)
        {
            //ddlState.Items.Clear();
            //ddlCity.Items.Clear();
            qryStr = "select StateId,StateName from StateMasterTbl where CountryId=" + ddlCountry.SelectedValue + " order by StateName";
            ddlState.DataSource = (DataSet)fillddl(qryStr);
            fillddlOther(ddlState, "StateName", "StateId");
            ddlState.Items.Insert(0, "-Select-");
            ddlState.Items[0].Value = "0";
            ddlCity.Items.Insert(0, "-Select-");
            ddlCity.Items[0].Value = "0";
            //ddlState_SelectedIndexChanged(sender, e);
        }
        else
        {
            //qryStr = "";//select StateId,StateName from StateMasterTbl  order by StateName";
            ddlState.Items.Insert(0, "-Select-");
            ddlState.Items[0].Value = "0";
            ddlCity.Items.Insert(0, "-Select-");
            ddlCity.Items[0].Value = "0";
            //ddlCity_SelectedIndexChanged(sender, e);
        }


    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCity.Items.Clear();
        if (ddlState.SelectedIndex > 0)
        {

            qryStr = "select CityId,CityName from CityMasterTbl where StateId=" + ddlState.SelectedValue + " order by CityName";
            ddlCity.DataSource = (DataSet)fillddl(qryStr);
            fillddlOther(ddlCity, "CityName", "CityId");
            ddlCity.Items.Insert(0, "-Select-");
            ddlCity.Items[0].Value = "0";

            //ddlCity_SelectedIndexChanged(sender, e);
        }
        else
        {
            ddlCity.Items.Insert(0, "-Select-");
            ddlCity.Items[0].Value = "0";
        }

        //ddlCity.Items[0].Value = "0";

    }
    protected void fillpartytype()
    {
        ddlfilterbypartytype.Items.Clear();
        
            qryStr = "select * from PartytTypeMaster  where PartyCategoryId='" + ddlpartycategory.SelectedValue + "' and  compid='" + compid1 + "' and PartType not in('Employee') order by PartType";
            ddlfilterbypartytype.DataSource = (DataSet)fillddl(qryStr);
            fillddlOther(ddlfilterbypartytype, "PartType", "PartyTypeId");
           
      
    }
    public void fillddlOther(DropDownList ddl, String dtf, String dvf)
    {
        ddl.Items.Clear();
        ddl.DataTextField = dtf;
        ddl.DataValueField = dvf;
        ddl.DataBind();
        //ddl.Items.Insert(0, "Select");
        //ddl.Items[0].Value = "0";
    }
    public DataSet fillddl(String qry)
    {
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
    protected void fillgriddata()
    {

        lblCompany.Text = Session["Cname"].ToString();
        lblBusiness.Text = ddshorting.SelectedItem.Text;
        lblptype.Text = ddlfilterbypartytype.SelectedItem.Text;
        lblstcategory.Text = ddlpartycategory.SelectedItem.Text;
        lblactive.Text = ddlfilterbyactive.SelectedItem.Text;
        if (ddlfilterbypartytype.SelectedItem.Text == "Customer")
        {
            lblterc.Visible = true;
            lblfilterc.Visible = true;
            if (ddlfiltercusdis.SelectedIndex > -1)
            {
                lblfilterc.Text = ddlfiltercusdis.SelectedItem.Text;
            }
        }
        else
        {
            lblterc.Visible = false;
            lblfilterc.Visible = false;
        }
        string pera = "";

        GridView1.DataSource = null;
        GridView1.DataBind();

        if (ddshorting.SelectedIndex > 0)
        {
            pera = " and Party_master.Whid=" + ddshorting.SelectedValue;
        }
       
            pera += " and PartytTypeMaster.PartyTypeId=" + ddlfilterbypartytype.SelectedValue;
        
        if (ddlpartycategory.SelectedIndex > 0)
        {
            //pera += " and PartyMasterCategory.Name=" + ddlpartycategory.SelectedValue;
        }
        if (ddlfilterbyactive.SelectedIndex > 0)
        {
            pera += " and Party_master.StatusMasterId=" + ddlfilterbyactive.SelectedValue;
        }
        if (ddlfiltercusdis.SelectedIndex > 0)
        {
            pera += " and PartyTypeDetailTbl.PartyTypeCategoryMasterId=" + ddlfiltercusdis.SelectedValue;
        }

        string filter = "";
        if (txtsearch.Text.Length > 0)
        {
            filter = "and (Party_master.PartyID like '%" + txtsearch.Text + "%' " +
                   "or PartytTypeMaster.PartType like '%" + txtsearch.Text + "%' " +
                   "or Party_master.Compname like '%" + txtsearch.Text + "%' " +
                   "or Party_master.Contactperson like '%" + txtsearch.Text + "%' " +
                   "or Party_master.Email like '%" + txtsearch.Text.Trim() + "%' " +
                   "or Party_master.Phoneno like '%" + txtsearch.Text + "%' " +
                   "or Party_master.StatusMasterId like '%" + txtsearch.Text + "%' " +
                   "or Party_master.City like '%" + txtsearch.Text + "%' " +
                   "or Party_master.State like '%" + txtsearch.Text + "%' " +
                   "or Party_master.Country like '%" + txtsearch.Text + "%' " +
                   "or Party_master.Website like '%" + txtsearch.Text + "%')";
        }

        string pera12 = " CityMasterTbl.CityName  +':'+StateMasterTbl.StateName +':'+CountryMaster.CountryName+':'+ Case when( Party_master.zipcode Is NUll) then '-' else Party_master.zipcode End as country1, Party_master.PartyID, Party_master.Account, Party_master.Compname, Party_master.Contactperson, Party_master.Address, Party_master.City,  Party_master.State, Party_master.Country, Party_master.Website, Party_master.GSTno, Party_master.Incometaxno, Party_master.Email,  Party_master.Phoneno, Party_master.DataopID, Party_master.AssignedAccountManagerId, Party_master.AssignedRecevingDepartmentInchargeId,  Party_master.AssignedPurchaseDepartmentInchargeId, Party_master.AssignedShippingDepartmentInchargeId,  Party_master.AssignedSalesDepartmentIncharge, Case when(Party_master.StatusMasterId IS NULL or Party_master.StatusMasterId='0') then 'Inactive' else 'Active' End as Statusname, Party_master.Fax, Party_master.AccountnameID,Party_master.Id,  Party_master.AccountBalanceLimitId, PartytTypeMaster.PartType, PartytTypeMaster.PartyTypeId  FROM PartytTypeMaster " +
             " inner join Party_master  ON Party_master.PartyTypeId=PartytTypeMaster.PartyTypeId" +
             " inner join User_Master on User_Master.PartyId=Party_Master.PartyId  left join CityMasterTbl on CityMasterTbl.CityId=Party_master.City " +
             "  Left join StateMasterTbl on StateMasterTbl.StateId=Party_master.State left join CountryMaster on CountryMaster.CountryId=Party_master.Country left join " +
             " StatusControl on StatusControl.UserMasterId=User_Master.UserId left join  StatusMaster " +
             "  on StatusMaster.StatusId=StatusControl.StatusMasterId left join  StatusCategory on StatusCategory.StatusCategoryMasterId= StatusMaster.StatusCategoryMasterId " +
             "  left join PartyTypeDetailTbl on PartyTypeDetailTbl.PartyID = Party_Master.PartyId where Party_master.id='" + Session["comid"] + "' " + pera + " " + filter + " ";

        string str2 = "select count(Party_master.PartyID) as ci FROM PartytTypeMaster " +
             " inner join Party_master  ON Party_master.PartyTypeId=PartytTypeMaster.PartyTypeId" +
             " inner join User_Master on User_Master.PartyId=Party_Master.PartyId  left join CityMasterTbl on CityMasterTbl.CityId=Party_master.City " +
             "  Left join StateMasterTbl on StateMasterTbl.StateId=Party_master.State left join CountryMaster on CountryMaster.CountryId=Party_master.Country left join " +
             " StatusControl on StatusControl.UserMasterId=User_Master.UserId left join  StatusMaster " +
             "  on StatusMaster.StatusId=StatusControl.StatusMasterId left join  StatusCategory on StatusCategory.StatusCategoryMasterId= StatusMaster.StatusCategoryMasterId " +
             "  left join PartyTypeDetailTbl on PartyTypeDetailTbl.PartyID = Party_Master.PartyId where Party_master.id='" + Session["comid"] + "' " + pera + " " + filter + " ";

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " Party_master.PartyID desc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable ds = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, pera12);

            if (ds.Rows.Count > 0)
            {
                DataView myDataView = new DataView();
                myDataView = ds.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }

                GridView1.DataSource = myDataView;
                GridView1.DataBind();
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        //pera12 = pera12 + pera + filter + " order by Party_master.PartyID desc";

        //SqlDataAdapter adc = new SqlDataAdapter(pera12, con);
        //DataTable ds = new DataTable();
        //adc.Fill(ds);       
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
    //protected DataTable select(string qu)
    //{
    //    SqlCommand cmd = new SqlCommand(qu, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //    return dt;
    //}

    protected void acccc(string accgenid)
    {
        int act = Convert.ToInt32(accgenid) + 1;
        DataTable dtrs = select("select AccountId from AccountMaster where AccountId='" + act + "' and Whid='" + ddwarehouse.SelectedValue + "'");
        if (dtrs.Rows.Count > 0)
        {
            accid += 1;
            acccc(accid);
        }

    }

    protected void groupclass()
    {
        if (ddlPartyType.SelectedItem.Text == "Vendor")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                accid = dtt.Rows[0]["aid"].ToString();
                acccc(dtt.Rows[0]["aid"].ToString());
                int gid = Convert.ToInt32(accid) + 1;
                accid = gid.ToString();
              

            }
            else
            {
                accid = Convert.ToString(30000);
                acccc(accid);
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Customer")
        {
            groupid = 2;
            DataTable dtt = new DataTable();

            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='2' and Whid='" + ddwarehouse.SelectedValue + "'");

            if (dtt.Rows.Count > 0)
            {
                accid = dtt.Rows[0]["aid"].ToString();
                acccc(dtt.Rows[0]["aid"].ToString());
                int gid = Convert.ToInt32(accid) + 1;
                accid = gid.ToString();


            }
            else
            {
                accid = Convert.ToString(10000);
                acccc(accid);
            }
            
          
        }
        else if (ddlPartyType.SelectedItem.Text == "Employee")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                accid = dtt.Rows[0]["aid"].ToString();
                acccc(dtt.Rows[0]["aid"].ToString());
                int gid = Convert.ToInt32(accid) + 1;
                accid = gid.ToString();


            }
            else
            {
                accid = Convert.ToString(30000);
                acccc(accid);
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Other")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                accid = dtt.Rows[0]["aid"].ToString();
                acccc(dtt.Rows[0]["aid"].ToString());
                int gid = Convert.ToInt32(accid) + 1;
                accid = gid.ToString();


            }
            else
            {
                accid = Convert.ToString(30000);
                acccc(accid);
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Admin")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                accid = dtt.Rows[0]["aid"].ToString();
                acccc(dtt.Rows[0]["aid"].ToString());
                int gid = Convert.ToInt32(accid) + 1;
                accid = gid.ToString();


            }
            else
            {
                accid = Convert.ToString(30000);
                acccc(accid);
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Credit Card Company")
        {
            groupid = 20;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='20' and Whid='" + ddwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {
                    if (Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) >= 3999)
                    {
                        accid = Convert.ToString(33000);
                    }
                    else
                    {
                        int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                        accid = Convert.ToString(gid);
                    }
                }
                else
                {
                    accid = Convert.ToString(3300);
                }
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Employee - CustomerSupport")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {
                    //if(Convert.ToInt32(dtt.Rows[0]["aid"].ToString() >  )
                    //{

                    //}
                    int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                    accid = Convert.ToString(gid);
                }
                else
                {
                    accid = Convert.ToString(30000);
                }
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Employee - Sale")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {
                    //if(Convert.ToInt32(dtt.Rows[0]["aid"].ToString() >  )
                    //{

                    //}
                    int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                    accid = Convert.ToString(gid);
                }
                else
                {
                    accid = Convert.ToString(30000);
                }
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Employee - OnlineManagement")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {
                    //if(Convert.ToInt32(dtt.Rows[0]["aid"].ToString() >  )
                    //{

                    //}
                    int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                    accid = Convert.ToString(gid);
                }
                else
                {
                    accid = Convert.ToString(30000);
                }
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Employee - OnlinetechSupport")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {
                    //if(Convert.ToInt32(dtt.Rows[0]["aid"].ToString() >  )
                    //{

                    //}
                    int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                    accid = Convert.ToString(gid);
                }
                else
                {
                    accid = Convert.ToString(30000);
                }
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Employee - Warehouse")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {
                    //if(Convert.ToInt32(dtt.Rows[0]["aid"].ToString() >  )
                    //{

                    //}
                    int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                    accid = Convert.ToString(gid);
                }
                else
                {
                    accid = Convert.ToString(30000);
                }
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Credit Card Company")
        {
            groupid = 5;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='5' and Whid='" + ddwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {
                    if (Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) >= 1999)
                    {
                        accid = Convert.ToString(17000);
                    }
                    else
                    {
                        int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                        accid = Convert.ToString(gid);
                    }
                }
                else
                {
                    accid = Convert.ToString(1700);
                }
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "client")
        {
            groupid = 2;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='2' and Whid='" + ddwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {
                    if (Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) >= 29999)
                    {
                        accid = Convert.ToString(100000);
                    }
                    else
                    {
                        int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                        accid = Convert.ToString(gid);
                    }
                }
                else
                {
                    accid = Convert.ToString(10000);
                }
            }
        }
        //}


        if (groupid == 15)
        {
            classid = 5;
        }
        else if (groupid == 2)
        {
            classid = 1;
        }
        else if (groupid == 5)
        {
            classid = 1;
        }
        else if (groupid == 20)
        {
            classid = 5;
        }
    }


    public void sendmail(string To)
    {

        string ADDRESSEX = "SELECT distinct CompanyMaster.CompanyLogo, CompanyMaster.CompanyName,CompanyWebsitMaster.Sitename,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.EmailMasterLoginPassword,CompanyWebsitMaster.OutGoingMailServer, CompanyWebsitMaster.EmailSentDisplayName,CompanyWebsitMaster.SiteUrl,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1, CompanyWebsiteAddressMaster.Phone2, CompanyWebsiteAddressMaster.TollFree1, CompanyWebsiteAddressMaster.Fax,CompanyWebsiteAddressMaster.Email,CompanyMaster.CompanyId,CompanyWebsitMaster.WHid FROM  CompanyMaster LEFT OUTER JOIN AddressTypeMaster RIGHT OUTER JOIN CompanyWebsiteAddressMaster ON AddressTypeMaster.AddressTypeMasterId = CompanyWebsiteAddressMaster.AddressTypeMasterId RIGHT OUTER JOIN CompanyWebsitMaster ON CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = CompanyWebsitMaster.CompanyWebsiteMasterId ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId where CompanyMaster.Compid='" + Session["comid"] + "' and WHId='" + ddwarehouse.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(ADDRESSEX, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        StringBuilder HeadingTable = new StringBuilder();
        HeadingTable.Append("<table width=\"100%\"> ");

        SqlDataAdapter dalogo = new SqlDataAdapter("select logourl from CompanyWebsitMaster where whid='" + ddwarehouse.SelectedValue + "'", con);
        DataTable dtlogo = new DataTable();
        dalogo.Fill(dtlogo);

        HeadingTable.Append("<tr><td width=\"50%\" style=\"padding-left:10px\" align=\"left\" > <img src=\"~/images/" + dtlogo.Rows[0]["logourl"].ToString() + "\" border=\"0\" width=\"176px\" height=\"106px\" / > </td><td width=\"50%\" align=\"left\"><table><tr><td colspan=\"2\"><b><span style=\"color: #996600\">" + ds.Rows[0]["CompanyName"].ToString() + "</span></b></td></tr><tr><td colspan=\"2\">" + ds.Rows[0]["Address1"].ToString() + "</td></tr><tr><td colspan=\"2\">" + ds.Rows[0]["Address2"].ToString() + "</td></tr><tr><td><b>Toll Free : </b></td><td>" + ds.Rows[0]["TollFree1"].ToString() + "</td></tr><tr><td><b>Phone : </b></td><td>" + ds.Rows[0]["Phone1"].ToString() + "</td></tr><tr><td><b>Fax : </b></td><td>" + ds.Rows[0]["Fax"].ToString() + "</td></tr><tr><td><b>Email : </b></td><td>" + ds.Rows[0]["Email"].ToString() + "</td></tr><tr><td><b>Website : </b></td><td>" + ds.Rows[0]["SiteUrl"].ToString() + " </td></tr></table></td></tr>  ");
        HeadingTable.Append("</table> ");



        string welcometext = getWelcometext();
        string AccountInfo = " Your new account has been successfully created and following is your temporary account login information. <br><br><b>Temporary Login Information: </b><table width=\"100%\"><tr><td width=\"25%\">Login URL: </td><td width=\"75%\">http://" + Request.Url.Host.ToString() + "</td></tr><tr><td>Company ID: </td><td>" + ds.Rows[0]["CompanyName"].ToString() + "</td></tr><tr><td>Temporary User ID: </td><td>" + Session["PartyUser"] + "</td></tr><tr><td>Temporary Password: </td><td>" + Session["PartyPassword"] + "</td></tr></table>";
        string Accountdetail = "";




        string accountdetailofparty = "select Party_master.*,CountryMaster.CountryName,StateMasterTbl.StateName,CityMasterTbl.CityName,User_master.zipcode from Party_master  left outer join CountryMaster on CountryMaster.CountryId=Party_master.Country left outer join StateMasterTbl on StateMasterTbl.StateId=Party_master.State left outer join CityMasterTbl on CityMasterTbl.CityId=Party_master.City inner join User_master on User_master.PartyID=Party_master.PartyID where Party_master.id='" + Session["comid"] + "' and Party_master.PartyID='" + ViewState["PartyMasterId"] + "' ";
        SqlCommand cmdpartydetail = new SqlCommand(accountdetailofparty, con);
        SqlDataAdapter adppartydetail = new SqlDataAdapter(cmdpartydetail);
        DataTable dspartydetail = new DataTable();
        adppartydetail.Fill(dspartydetail);
        string Accountdetail12 = "";
        if (dspartydetail.Rows.Count > 0)
        {
            Accountdetail12 = "<br><br><b>Account Information: </b><br><br><table width=\"100%\"><tr><td width=\"20%\">Party Name: </td><td width=\"80%\">" + dspartydetail.Rows[0]["Compname"].ToString() + "</td></tr><tr><td>Contact Person: </td><td>" + dspartydetail.Rows[0]["Contactperson"].ToString() + " </td></tr><tr><td>Address: </td><td>" + dspartydetail.Rows[0]["Address"].ToString() + " </td></tr><tr><td>Country: </td><td>" + dspartydetail.Rows[0]["CountryName"].ToString() + "</td></tr><tr><td>State: </td><td>" + dspartydetail.Rows[0]["StateName"].ToString() + "</td></tr><tr><td>City: </td><td>" + dspartydetail.Rows[0]["CityName"].ToString() + "</td></tr><tr><td>Phone: </td><td>" + dspartydetail.Rows[0]["Phoneno"].ToString() + "</td></tr><tr><td>Fax: </td><td>" + dspartydetail.Rows[0]["Fax"].ToString() + "</td></tr><tr><td>Email: </td><td>" + dspartydetail.Rows[0]["Email"].ToString() + "</td></tr><tr><td>Zip Code: </td><td>" + dspartydetail.Rows[0]["zipcode"].ToString() + "</td></tr></table><br><br>Please ensure that you change your user ID and password as soon as possible for your account security. Please click <a href=http://onlineaccounts.net/shoppingcart/admin/resetpassworduser.aspx target=_blank > here</a>to change your account information, if the the link does not open please copy and paste the following link:<br><br> http://onlineaccounts.net/shoppingcart/admin/resetpassworduser.aspx <br><br>To edit your contact details, login and make changes from the <strong><span style=\"color: #996600\"> \"Modify Account Detail\"</span></strong> page.";
        }


        //string body = "" + HeadingTable + "<br><br> Dear " + txtlastname.Text + "&nbsp;" + txtfirstname.Text + " ,<br><br>" + welcometext.ToString() + " <br>" + AccountInfo.ToString() + "<br><br><strong><span style=\"color: #996600\"> " + ViewState["sitename"] + " Team</span></strong>";

        //string body = "<b><centre>" + ds.Rows[0]["EmailSentDisplayName"] + "</centre></b><br>" + HeadingTable + "<br><br> Dear " + tbName.Text + " ,<br><br>" + welcometext.ToString() + " <br>" + AccountInfo.ToString() + "<br> " + Accountdetail.ToString() + " <br>"+Accountdetail12+" <br><br> <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"><br>Thanking you <br>Sincerely</span><br><strong><span style=\"color: #996600\"> " + ViewState["sitename"] + "Admin Team<br>" + ds.Rows[0]["CompanyName"].ToString() + "-" + ds.Rows[0]["Sitename"].ToString() + "</span></strong>";
        string body = "<br>" + HeadingTable + "<br><br> Dear <strong><span style=\"color: #996600\"> " + tbName.Text + " </span></strong>,<br><br>" + welcometext.ToString() + " <br>" + AccountInfo.ToString() + "<br> " + Accountdetail.ToString() + " <br>" + Accountdetail12 + " <br><br> <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"><br>Thank you,<br><br><strong><span style=\"color: #996600\"><br>Admin Team<br>" + ds.Rows[0]["CompanyName"].ToString() + "<br>Address: " + ds.Rows[0]["Address1"].ToString() + "<br> " + ds.Rows[0]["Address2"].ToString() + "<br>Toll Free: " + ds.Rows[0]["TollFree1"].ToString() + "<br>Phone: " + ds.Rows[0]["Phone1"].ToString() + "<br>Fax:" + ds.Rows[0]["Fax"].ToString() + "<br>Email:" + ds.Rows[0]["Email"].ToString() + "<br>Website:" + ds.Rows[0]["SiteUrl"].ToString() + "</span></strong>";


        if (ds.Rows[0]["MasterEmailId"].ToString() != "" && ds.Rows[0]["EmailSentDisplayName"].ToString() != "")
        {
            MailAddress to = new MailAddress(To);
            MailAddress from = new MailAddress("" + ds.Rows[0]["MasterEmailId"] + "", "" + ds.Rows[0]["EmailSentDisplayName"] + "");

            //MailAddress from = new MailAddress("" + ds.Rows[0]["MasterEmailId"] + "");

            MailMessage objEmail = new MailMessage(from, to);

            SqlDataAdapter dabus = new SqlDataAdapter("select Name from Warehousemaster where Warehouseid='" + ddwarehouse.SelectedValue + "'", con);
            DataTable dtbus = new DataTable();
            dabus.Fill(dtbus);


            //objEmail.Subject = "Welcome to " + ViewState["sitename"] + " - Registration";
            objEmail.Subject = "New User Creation - " + dtbus.Rows[0]["Name"].ToString() + "";
            // objEmail.Sender.DisplayName.ToString() = " "+ds.Rows[0]["EmailSentDisplayName"].ToString() + "";

            objEmail.Body = body.ToString();
            objEmail.IsBodyHtml = true;


            objEmail.Priority = MailPriority.Normal;

            //Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyID"])
            SmtpClient client = new SmtpClient();

            client.Credentials = new NetworkCredential("" + ds.Rows[0]["MasterEmailId"] + "", "" + ds.Rows[0]["EmailMasterLoginPassword"] + "");
            client.Host = ds.Rows[0]["OutGoingMailServer"].ToString();


            client.Send(objEmail);
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please, First Set Master Email Server";
        }
    }
    public StringBuilder getSiteAddress()
    {
        SqlConnection conn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand("Sp_select_Siteaddress", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        StringBuilder strAddress = new StringBuilder();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {


            strAddress.Append("<table width=\"100%\"> ");

            strAddress.Append("<tr><td> <img src=\"http://www.indianmall.com/ShoppingCart/images/logo.gif\" \"border=\"0\"  /> </td><td align=\"center\"><b><span style=\"color: #996600\">" + ds.Rows[0]["Sitename"].ToString() + "</span></b><Br><b>" + ds.Rows[0]["CompanyName"].ToString() + "</b><Br>" + ds.Rows[0]["Address1"].ToString() + "<Br><b>TollFree:</b>" + ds.Rows[0]["TollFree1"].ToString() + "<Br><b>Phone:</b>" + ds.Rows[0]["Phone1"].ToString() + "<Br><b>Fax:</b>" + ds.Rows[0]["Fax"].ToString() + "<Br>" + ds.Rows[0]["LiveChatUrl"].ToString() + "<br><b>Email:</b>" + ds.Rows[0]["Email"].ToString() + "<Br><b>Website:</b>" + ds.Rows[0]["SiteUrl"].ToString() + " </td></tr>  ");


            strAddress.Append("</table> ");
            ViewState["sitename"] = ds.Rows[0]["Sitename"].ToString();
        }
        return strAddress;

    }
    //this is just copy past method i have not make any changes
    public string getWelcometext()
    {
        SqlConnection conn = new SqlConnection(strconn);

        string str = "SELECT EmailContentMaster.EmailContent, EmailContentMaster.EntryDate, CompanyWebsitMaster.SiteUrl, EmailTypeMaster.EmailTypeId " +
                    " FROM CompanyWebsitMaster INNER JOIN " +
                      " EmailContentMaster ON CompanyWebsitMaster.CompanyWebsiteMasterId = EmailContentMaster.CompanyWebsiteMasterId INNER JOIN " +
                      " EmailTypeMaster ON EmailContentMaster.EmailTypeId = EmailTypeMaster.EmailTypeId " +
                    " WHERE     (EmailTypeMaster.EmailTypeId = 1) AND (CompanyWebsitMaster.SiteUrl = 'www.IndianMall.com')  and (EmailTypeMaster.Compid='" + compid1 + "')" +
                    " ORDER BY EmailContentMaster.EntryDate DESC ";
        SqlCommand cmd = new SqlCommand(str, conn);
        //cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        string welcometext = "";
        if (ds.Rows.Count > 0)
        {
            welcometext = ds.Rows[0]["EmailContent"].ToString();

        } return welcometext;

    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlPartyType.SelectedItem.Text == "Employee")
        if (ddlCity.SelectedIndex > 0)
        {
            if (ddlState.SelectedIndex > 0)
            {
                if (ddlCountry.SelectedIndex > 0)
                {
                    string str = "SELECT     AssignedAccountManager, AssignedRecievingDept, AssignedPurchseDept, AssignedSalesDept, AssignedShippingDept " +
                         " FROM         PartyAutoAllocationManager " +
                         "where ((Country=" + ddlCountry.SelectedValue + ") And (State=" + ddlState.SelectedValue + ") And (City=" + ddlCity.SelectedValue + ") and (compid='" + compid1 + "')) ";
                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        //ddlAssAccManagerId.SelectedIndex = ddlAssAccManagerId.Items.IndexOf(ddlAssAccManagerId.Items.FindByValue(dt.Rows[0]["AssignedAccountManager"].ToString()));
                        ddlAssPurDeptId.SelectedIndex = ddlAssPurDeptId.Items.IndexOf(ddlAssPurDeptId.Items.FindByValue(dt.Rows[0]["AssignedPurchseDept"].ToString()));
                        ddlAssRecieveDeptId.SelectedIndex = ddlAssRecieveDeptId.Items.IndexOf(ddlAssRecieveDeptId.Items.FindByValue(dt.Rows[0]["AssignedRecievingDept"].ToString()));
                        ddlAssSalDeptId.SelectedIndex = ddlAssSalDeptId.Items.IndexOf(ddlAssSalDeptId.Items.FindByValue(dt.Rows[0]["AssignedSalesDept"].ToString()));
                        ddlAssShipDeptId.SelectedIndex = ddlAssShipDeptId.Items.IndexOf(ddlAssShipDeptId.Items.FindByValue(dt.Rows[0]["AssignedShippingDept"].ToString()));

                    }
                    else
                    {
                        string str1 = "SELECT     AssignedAccountManager, AssignedRecievingDept, AssignedPurchseDept, AssignedSalesDept, AssignedShippingDept " +
                         " FROM         PartyAutoAllocationManager " +
                         "where ((Country=" + ddlCountry.SelectedValue + ") And (State=" + ddlState.SelectedValue + ") and (compid='" + compid1 + "')) ";
                        SqlCommand cmd1 = new SqlCommand(str1, con);
                        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            //ddlAssAccManagerId.SelectedIndex = ddlAssAccManagerId.Items.IndexOf(ddlAssAccManagerId.Items.FindByValue(dt1.Rows[0]["AssignedAccountManager"].ToString()));
                            ddlAssPurDeptId.SelectedIndex = ddlAssPurDeptId.Items.IndexOf(ddlAssPurDeptId.Items.FindByValue(dt1.Rows[0]["AssignedPurchseDept"].ToString()));
                            ddlAssRecieveDeptId.SelectedIndex = ddlAssRecieveDeptId.Items.IndexOf(ddlAssRecieveDeptId.Items.FindByValue(dt1.Rows[0]["AssignedRecievingDept"].ToString()));
                            ddlAssSalDeptId.SelectedIndex = ddlAssSalDeptId.Items.IndexOf(ddlAssSalDeptId.Items.FindByValue(dt1.Rows[0]["AssignedSalesDept"].ToString()));
                            ddlAssShipDeptId.SelectedIndex = ddlAssShipDeptId.Items.IndexOf(ddlAssShipDeptId.Items.FindByValue(dt1.Rows[0]["AssignedShippingDept"].ToString()));

                        }
                        else
                        {
                            string strcou = "SELECT     AssignedAccountManager, AssignedRecievingDept, AssignedPurchseDept, AssignedSalesDept, AssignedShippingDept " +
                         " FROM         PartyAutoAllocationManager " +
                         "where (Country=" + ddlCountry.SelectedValue + ") and (compid='" + compid1 + "')";
                            SqlCommand cmdco = new SqlCommand(strcou, con);
                            SqlDataAdapter adpco = new SqlDataAdapter(cmdco);
                            DataTable dtco = new DataTable();
                            adpco.Fill(dtco);
                            if (dtco.Rows.Count > 0)
                            {
                                //ddlAssAccManagerId.SelectedIndex = ddlAssAccManagerId.Items.IndexOf(ddlAssAccManagerId.Items.FindByValue(dtco.Rows[0]["AssignedAccountManager"].ToString()));
                                ddlAssPurDeptId.SelectedIndex = ddlAssPurDeptId.Items.IndexOf(ddlAssPurDeptId.Items.FindByValue(dtco.Rows[0]["AssignedPurchseDept"].ToString()));
                                ddlAssRecieveDeptId.SelectedIndex = ddlAssRecieveDeptId.Items.IndexOf(ddlAssRecieveDeptId.Items.FindByValue(dtco.Rows[0]["AssignedRecievingDept"].ToString()));
                                ddlAssSalDeptId.SelectedIndex = ddlAssSalDeptId.Items.IndexOf(ddlAssSalDeptId.Items.FindByValue(dtco.Rows[0]["AssignedSalesDept"].ToString()));
                                ddlAssShipDeptId.SelectedIndex = ddlAssShipDeptId.Items.IndexOf(ddlAssShipDeptId.Items.FindByValue(dtco.Rows[0]["AssignedShippingDept"].ToString()));

                            }
                            else
                            {
                                string str12 = "SELECT     AssignedAccountManager, AssignedRecievingDept, AssignedPurchseDept, AssignedSalesDept, AssignedShippingDept " +
                         " FROM         PartyAutoAllocationManager " +
                         " where [All]='1' and (compid='" + compid1 + "')";
                                SqlCommand cmd12 = new SqlCommand(str12, con);
                                SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
                                DataTable dt12 = new DataTable();
                                adp12.Fill(dt12);
                                if (dt12.Rows.Count > 0)
                                {
                                    //ddlAssAccManagerId.SelectedIndex = ddlAssAccManagerId.Items.IndexOf(ddlAssAccManagerId.Items.FindByValue(dt12.Rows[0]["AssignedAccountManager"].ToString()));
                                    ddlAssPurDeptId.SelectedIndex = ddlAssPurDeptId.Items.IndexOf(ddlAssPurDeptId.Items.FindByValue(dt12.Rows[0]["AssignedPurchseDept"].ToString()));
                                    ddlAssRecieveDeptId.SelectedIndex = ddlAssRecieveDeptId.Items.IndexOf(ddlAssRecieveDeptId.Items.FindByValue(dt12.Rows[0]["AssignedRecievingDept"].ToString()));
                                    ddlAssSalDeptId.SelectedIndex = ddlAssSalDeptId.Items.IndexOf(ddlAssSalDeptId.Items.FindByValue(dt12.Rows[0]["AssignedSalesDept"].ToString()));
                                    ddlAssShipDeptId.SelectedIndex = ddlAssShipDeptId.Items.IndexOf(ddlAssShipDeptId.Items.FindByValue(dt12.Rows[0]["AssignedShippingDept"].ToString()));
                                }
                            }
                        }
                    }
                }
            }
        }


    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editview" || e.CommandName == "View")
        {
            btnshowparty.Visible = false;
            pnlparty.Visible = true;

            ddwarehouse.Enabled = false;
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["partyid"] = GridView1.SelectedIndex;

            lblusername123.Visible = true;
            lblpassword123.Visible = true;
            tbUserName.Visible = true;
            tbPassword.Visible = true;

            //Label lblaccid
            //ViewState["accoutid"] = GridView1.Rows[GridView1.SelectedRow.Cells[1]].FindControl("lblaccid").ToString();
            //ViewState["accoutid"] = lblaccid.Text;
            DataTable dtaccsel = new DataTable();
            //********   Radhika Solanki

            //dtaccsel = (DataTable)select("Select Account from Party_master where PartyID='" + ViewState["partyid"] + "' ");
            dtaccsel = (DataTable)select("Select Account from Party_master,PartytTypeMaster where PartyID='" + ViewState["partyid"] + "'  AND   PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId AND PartytTypeMaster.compid='" + compid1 + "'");

            if (dtaccsel.Rows.Count > 0)
            {
                ViewState["accoutid"] = dtaccsel.Rows[0]["Account"].ToString();
            }
            DataTable dtsel = new DataTable();
            dtsel = (DataTable)select("Select * from Party_master,PartytTypeMaster where PartyID='" + ViewState["partyid"] + "' and PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId AND PartytTypeMaster.compid='" + compid1 + "' ");
            if (dtsel.Rows.Count > 0)
            {
                ViewState["PartyMasterId"] = ViewState["partyid"];
                ViewState["wh"] = dtsel.Rows[0]["Whid"].ToString();
                ddwarehouse.SelectedIndex = ddwarehouse.Items.IndexOf(ddwarehouse.Items.FindByValue(dtsel.Rows[0]["Whid"].ToString()));

                btnCancel.Enabled = false;
                //imgbtnedit.Visible = true;
                //imgbtnupdate.Visible = false;
                btnSubmit.Visible = false;
                //controlenable(false);
                string str = "Select Id From PartyMasterCategory where PartyMasterCategoryNo='" + dtsel.Rows[0]["PartyTypeCategoryNo"].ToString() + "' order by Name ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable datat = new DataTable();
                adp.Fill(datat);
                if (datat.Rows.Count > 0)
                {
                    ddlpartycate.SelectedIndex = ddlpartycate.Items.IndexOf(ddlpartycate.Items.FindByValue(datat.Rows[0]["Id"].ToString()));
                }
                ddlpartycate_SelectedIndexChanged(sender, e);
                ddlPartyType.SelectedIndex = ddlPartyType.Items.IndexOf(ddlPartyType.Items.FindByValue(dtsel.Rows[0]["PartyTypeId"].ToString()));
                object ob = new object();
                EventArgs evt = new EventArgs();
                ddlPartyType_SelectedIndexChanged(ob, evt);
                ViewState["PartyTypeDetailId"] = "";

                if (ddlPartyType.SelectedItem.Text == "Customer")
                {
                    lblcustdisc.Visible = true;
                    ddlcustomerdis.Visible = true;
                    DataTable dtselc = new DataTable();
                    dtselc = (DataTable)select("Select * from PartyTypeDetailTbl where PartyID='" + ViewState["partyid"] + "'");
                    if (dtselc.Rows.Count > 0)
                    {
                        ViewState["PartyTypeDetailId"] = dtselc.Rows[0]["PartyTypeDetailId"].ToString();
                        ddlcustomerdis.SelectedIndex = ddlcustomerdis.Items.IndexOf(ddlcustomerdis.Items.FindByValue(dtselc.Rows[0]["PartyTypeCategoryMasterId"].ToString()));
                    }
                }
                else
                {
                    lblcustdisc.Visible = false;
                    ddlcustomerdis.Visible = false;
                }
                ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dtsel.Rows[0]["Country"].ToString()));
                object obs = new object();
                EventArgs evts = new EventArgs();
                ddlCountry_SelectedIndexChanged(obs, evts);

                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dtsel.Rows[0]["State"].ToString()));
                object obc = new object();
                EventArgs evtc = new EventArgs();
                ddlState_SelectedIndexChanged(obc, evtc);

                ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(dtsel.Rows[0]["City"].ToString()));
                object o = new object();
                EventArgs ev = new EventArgs();
                ddlCity_SelectedIndexChanged(o, ev);

                //qryStr = " SELECT  [StatusMaster].StatusId as StatusId,[StatusCategory].StatusCategory +' : '+ [StatusMaster].StatusName as StatusName, [StatusMaster].StatusCategoryMasterId,[StatusCategory].[StatusCategoryMasterId] FROM StatusMaster " +
                //        "  inner join [StatusCategory] on StatusMaster.StatusCategoryMasterId=[StatusCategory].StatusCategoryMasterId WHERE ([StatusMaster].StatusCategoryMasterId = 19) order by [StatusCategory].StatusCategory ";

                // ddlstatus.DataSource = (DataSet)fillddl(qryStr);
                // fillddlOther(ddlstatus, "StatusName", "StatusId");

                ddlActive.SelectedIndex = ddlActive.Items.IndexOf(ddlActive.Items.FindByValue(dtsel.Rows[0]["StatusMasterId"].ToString()));

                tbWebsite.Text = dtsel.Rows[0]["Website"].ToString();
                tbFax.Text = dtsel.Rows[0]["Fax"].ToString();
                tbEmail.Text = dtsel.Rows[0]["Email"].ToString();
                tbGSTNumber.Text = dtsel.Rows[0]["GSTno"].ToString();
                tbITNumber.Text = dtsel.Rows[0]["Incometaxno"].ToString();
                tbName.Text = dtsel.Rows[0]["Contactperson"].ToString();
                tbCompanyName.Text = dtsel.Rows[0]["Compname"].ToString();
                tbPhone.Text = dtsel.Rows[0]["Phoneno"].ToString();
                tbAddress.Text = dtsel.Rows[0]["Address"].ToString();

                DataTable dtselbal = new DataTable();
                dtselbal = (DataTable)select("Select * from AccountBalanceLimit where AccountId='" + ViewState["accoutid"] + "' and Whid='" + dtsel.Rows[0]["Whid"] + "'");

                if (dtselbal.Rows.Count > 0)
                {

                    listip.Items.Clear();
                    if (dtselbal.Rows.Count >= 2)
                    {

                        for (int i = 0; i < dtselbal.Rows.Count; i++)
                        {

                            dddlbalance.SelectedIndex = dddlbalance.Items.IndexOf(dddlbalance.Items.FindByValue(dtselbal.Rows[i]["BalanceLimitTypeId"].ToString()));
                            listip.Items.Add(dddlbalance.SelectedItem.Text + " : " + dtselbal.Rows[i]["BalancelimitAmount"].ToString());
                            listip.Visible = true;

                        }

                    }
                    else
                    {
                        dddlbalance.SelectedIndex = dddlbalance.Items.IndexOf(dddlbalance.Items.FindByValue(dtselbal.Rows[0]["BalanceLimitTypeId"].ToString()));
                        txtbal.Text = dtselbal.Rows[0]["BalancelimitAmount"].ToString();

                    }
                    txtdate.Text = dtselbal.Rows[0]["DateTime"].ToString();
                }

                DataTable dtseluser = new DataTable();
                dtseluser = (DataTable)select(" Select Login_master.password, User_master.* from User_master inner join Login_master on  Login_master.UserID=User_master.UserID where PartyID='" + ViewState["partyid"] + "'");
                if (dtseluser.Rows.Count > 0)
                {
                    tbUserName.Text = dtseluser.Rows[0]["Username"].ToString();
                    string strqa = ClsEncDesc.Decrypted(dtseluser.Rows[0]["password"].ToString());

                    tbPassword.Attributes.Add("Value", strqa);
                    tbConPassword.Attributes.Add("Value", strqa);
                    tbPassword.Enabled = false;
                    ddldept.SelectedIndex = ddldept.Items.IndexOf(ddldept.Items.FindByValue(dtseluser.Rows[0]["Department"].ToString()));

                    object obu = new object();
                    EventArgs evtu = new EventArgs();
                    ddldept_SelectedIndexChanged(obu, evtu);

                    ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(dtseluser.Rows[0]["DesigantionMasterId"].ToString()));
                    tbExtension.Text = dtseluser.Rows[0]["Extention"].ToString();
                    tbZipCode.Text = dtseluser.Rows[0]["zipcode"].ToString();
                    //ddlActive.SelectedIndex = ddlActive.Items.IndexOf(ddlActive.Items.FindByValue(Convert.ToBoolean(dtseluser.Rows[0]["Active"]).ToString()));

                    //DataTable dtpass = new DataTable();
                    //dtpass = (DataTable)select("Select password from Login_Master where UserID ='" + dtseluser.Rows[0]["UserID"].ToString() + "' ");
                    //if (dtpass.Rows.Count > 0)
                    //{
                    //    tbPassword.Text =ClsEncDesc.Decrypted( dtpass.Rows[0]["password"].ToString());
                    //    tbPassword.Attributes.Add("Value", ClsEncDesc.Decrypted(dtpass.Rows[0]["password"].ToString()));
                    //    tbConPassword.Attributes.Add("Value", dtpass.Rows[0]["password"].ToString());
                    //    tbPassword.Enabled = false;

                    //}
                }

                DataTable dtgro = new DataTable();
                dtgro = (DataTable)select("Select GroupId from AccountMaster where AccountId='" + ViewState["accoutid"] + "'");
                if (dtgro.Rows.Count > 0)
                {
                    ddlGroup.SelectedIndex = ddlGroup.Items.IndexOf(ddlGroup.Items.FindByValue(dtgro.Rows[0]["GroupId"].ToString()));
                }


                string strole = "select UserID from User_master where PartyID='" + ViewState["partyid"] + "'";
                SqlCommand cmrl = new SqlCommand(strole, con);
                SqlDataAdapter dauprole = new SqlDataAdapter(cmrl);
                DataSet dtrl = new DataSet();
                dauprole.Fill(dtrl);

                string srl = Convert.ToInt32(dtrl.Tables[0].Rows[0]["UserID"]).ToString();

                string ststatus = "select StatusMasterId from StatusControl where UserMasterId='" + Convert.ToInt32(dtrl.Tables[0].Rows[0]["UserID"]).ToString() + "' and TranctionMasterId IS NULL and SalesOrderId IS NULL and SalesChallanMasterId IS NULL";
                SqlCommand cmstatus = new SqlCommand(ststatus, con);
                SqlDataAdapter dastatus = new SqlDataAdapter(cmstatus);
                DataTable dtstatus = new DataTable();
                dastatus.Fill(dtstatus);

                foreach (DataRow dr in dtstatus.Rows)
                {
                    if (ddlstatus.Items.Count > 0)
                    {

                        ListItem match = listb.Items.FindByValue(dr["StatusMasterId"].ToString());
                        if (match == null)
                        {
                            ddlstatus.SelectedIndex = (ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue(dr["StatusMasterId"].ToString())));
                            listb.Items.Insert(listb.Items.Count, ddlstatus.SelectedItem.Text);
                            listb.Items[listb.Items.Count - 1].Value = ddlstatus.SelectedValue;
                            listb.SelectedIndex = 0;
                        }
                    }
                }


                string strole1 = "select Role_id from User_Role where User_id='" + Convert.ToInt32(dtrl.Tables[0].Rows[0]["UserID"]).ToString() + "'";
                SqlCommand cmrl1 = new SqlCommand(strole1, con);
                SqlDataAdapter dauprole1 = new SqlDataAdapter(cmrl1);
                DataTable dtrl1 = new DataTable();
                dauprole1.Fill(dtrl1);
                fillrole();
                if (dtrl1.Rows.Count > 0)
                {
                    ddlemprole.SelectedIndex = ddlemprole.Items.IndexOf(ddlemprole.Items.FindByValue(dtrl1.Rows[0]["Role_id"].ToString()));
                }
                ddlemprole.Enabled = false;

                SqlCommand cmdedit = new SqlCommand("SELECT     AccountMasterId, EditAllowed FROM Fixeddata where AccountMasterId='" + ViewState["accoutid"].ToString() + "'", con);
                SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
                DataTable dtedit = new DataTable();
                dtpedit.Fill(dtedit);
                if (dtedit.Rows.Count > 0)
                {
                    ////if (dtedit.Rows[0]["EditAllowed"].ToString() == "0")
                    ////{
                    //RequiredFieldValidator5.ValidationGroup = "2";
                    imgbtnedit.Visible = false;
                    controlenable(true);
                    btnupdate.Visible = true;
                    //}
                    //else
                    //{
                    //    ModalPopupExtender3.Show();
                    //////}
                }
                else
                {
                    //RequiredFieldValidator5.ValidationGroup = "2";
                    //imgbtnedit.Visible = false;
                    //btnCancel.Enabled = true;
                    //imgbtnupdate.Visible = true;
                    //controlenable(true);
                    // btnupdate.Visible = false;
                }
                if (e.CommandName == "editview")
                {
                    lbladd.Text = "Edit User Category";
                    btnCancel.Enabled = true;
                    imgbtnupdate.Visible = true;
                    btnCancel.Visible = true;
                    controlenable(true);

                    CheckBox2.Enabled = true;
                }
                else if (e.CommandName == "View")
                {
                    lbladd.Text = "View User Category";
                    CheckBox2.Enabled = false;
                    imgbtnupdate.Visible = false;
                    btnCancel.Visible = true;
                    btnCancel.Enabled = true;
                    controlenable(false);
                }
                else
                {
                    CheckBox2.Checked = false;
                }
            }



        }
        else if (e.CommandName == "del")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["partyid"] = GridView1.SelectedIndex;
            DataTable dtaccid = new DataTable();

            //   Radhika Solanki
            //dtaccid = (DataTable)select("Select Account,PartyID from  Party_master where PartyID = '" + ViewState["partyid"].ToString() + "'");

            dtaccid = (DataTable)select("Select Account,PartyID from  Party_master where PartyID = '" + ViewState["partyid"].ToString() + "' and id='" + compid1 + "' ");
            if (dtaccid.Rows.Count > 0)
            {
                SqlCommand cmddel1 = new SqlCommand("SELECT PartyID, DeleteAllowed FROM Fixeddata where PartyID = '" + ViewState["partyid"].ToString() + "'", con);
                SqlDataAdapter dtpdel1 = new SqlDataAdapter(cmddel1);
                DataTable dtdel1 = new DataTable();
                dtpdel1.Fill(dtdel1);
                if (dtdel1.Rows.Count > 0)
                {
                    if (dtdel1.Rows[0]["DeleteAllowed"].ToString() == "0")
                    {
                        SqlCommand cmddel = new SqlCommand("SELECT AccountMasterId, DeleteAllowed FROM Fixeddata where AccountMasterId='" + dtaccid.Rows[0]["Account"].ToString() + "'", con);
                        SqlDataAdapter dtpdel = new SqlDataAdapter(cmddel);
                        DataTable dtdel = new DataTable();
                        dtpdel.Fill(dtdel);
                        if (dtdel.Rows.Count > 0)
                        {
                            if (dtdel.Rows[0]["DeleteAllowed"].ToString() == "0")
                            {
                                DataTable dtopbal = new DataTable();
                                dtopbal = (DataTable)select("select BalanceOfLastYear from AccountMaster where AccountId = '" + dtaccid.Rows[0]["Account"].ToString() + "'");
                                if (dtopbal.Rows.Count > 0)
                                {
                                    if (dtopbal.Rows[0]["BalanceOfLastYear"].ToString() != "")
                                    {
                                        if (Convert.ToDouble(dtopbal.Rows[0]["BalanceOfLastYear"].ToString()) > 0 || Convert.ToDouble(dtopbal.Rows[0]["BalanceOfLastYear"].ToString()) < 0)
                                        {
                                            lblmsg.Text = "You cannot delete this user record because it currently has as open balance. Please close the balance to delete.";
                                        }
                                        else
                                        {
                                            ModalPopupExtender145.Show();
                                        }
                                    }
                                }
                                else
                                {
                                    ModalPopupExtender145.Show();
                                }
                            }
                            else
                            {
                                ModalPopupExtender2.Show();
                            }
                        }
                        else
                        {
                            DataTable dtopbal = new DataTable();
                            dtopbal = (DataTable)select("select BalanceOfLastYear from AccountMaster where AccountId = '" + dtaccid.Rows[0]["Account"].ToString() + "'");
                            if (dtopbal.Rows.Count > 0)
                            {
                                if (dtopbal.Rows[0]["BalanceOfLastYear"].ToString() != "")
                                {
                                    if (Convert.ToDouble(dtopbal.Rows[0]["BalanceOfLastYear"].ToString()) > 0 || Convert.ToDouble(dtopbal.Rows[0]["BalanceOfLastYear"].ToString()) < 0)
                                    {
                                        lblmsg.Text = "You cannot delete this user record because it currently has as open balance. Please close the balance to delete.";
                                    }
                                    else
                                    {
                                        ModalPopupExtender145.Show();
                                    }
                                }
                            }
                            else
                            {
                                ModalPopupExtender145.Show();
                            }
                        }

                    }
                    else
                    {
                        ModalPopupExtender112121.Show();
                    }
                }
                else
                {
                    SqlCommand cmddel = new SqlCommand("SELECT AccountMasterId, DeleteAllowed FROM Fixeddata where AccountMasterId='" + dtaccid.Rows[0]["Account"].ToString() + "'", con);
                    SqlDataAdapter dtpdel = new SqlDataAdapter(cmddel);
                    DataTable dtdel = new DataTable();
                    dtpdel.Fill(dtdel);
                    if (dtdel.Rows.Count > 0)
                    {
                        if (dtdel.Rows[0]["DeleteAllowed"].ToString() == "0")
                        {
                            DataTable dtopbal = new DataTable();
                            dtopbal = (DataTable)select("select BalanceOfLastYear from AccountMaster where AccountId = '" + dtaccid.Rows[0]["Account"].ToString() + "'");
                            if (dtopbal.Rows.Count > 0)
                            {
                                if (dtopbal.Rows[0]["BalanceOfLastYear"].ToString() != "")
                                {
                                    if (Convert.ToDouble(dtopbal.Rows[0]["BalanceOfLastYear"].ToString()) > 0 || Convert.ToDouble(dtopbal.Rows[0]["BalanceOfLastYear"].ToString()) < 0)
                                    {
                                        lblmsg.Text = "You cannot delete this user record because it currently has as open balance. Please close the balance to delete.";
                                    }
                                    else
                                    {
                                        ModalPopupExtender145.Show();
                                    }
                                }
                            }
                            else
                            {
                                ModalPopupExtender145.Show();
                            }
                        }
                        else
                        {
                            ModalPopupExtender2.Show();
                        }
                    }
                    else
                    {
                        DataTable dtopbal = new DataTable();
                        dtopbal = (DataTable)select("select BalanceOfLastYear from AccountMaster where AccountId = '" + dtaccid.Rows[0]["Account"].ToString() + "'");
                        if (dtopbal.Rows.Count > 0)
                        {
                            if (dtopbal.Rows[0]["BalanceOfLastYear"].ToString() != "")
                            {
                                if (Convert.ToDouble(dtopbal.Rows[0]["BalanceOfLastYear"].ToString()) > 0 || Convert.ToDouble(dtopbal.Rows[0]["BalanceOfLastYear"].ToString()) < 0)
                                {
                                    lblmsg.Text = "You cannot delete this user record because it currently has as open balance. Please close the balance to delete.";
                                }
                                else
                                {
                                    ModalPopupExtender145.Show();
                                }
                            }
                        }
                        else
                        {
                            ModalPopupExtender145.Show();
                        }
                    }

                }
            }

        }

    }
    protected void Delete(string query)
    {
        SqlCommand cmd = new SqlCommand(query, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void controlenable(bool t)
    {
        ddlpartycate.Enabled = t;
        ddlPartyType.Enabled = t;
        tbUserName.Enabled = t;
        tbPassword.Enabled = t;
        tbAddress.Enabled = t;
        tbCompanyName.Enabled = t;
        tbConPassword.Enabled = t;
        tbEmail.Enabled = t;
        tbExtension.Enabled = t;
        tbFax.Enabled = t;
        tbGSTNumber.Enabled = t;
        tbITNumber.Enabled = t;
        tbName.Enabled = t;
        tbPhone.Enabled = t;
        tbWebsite.Enabled = t;
        tbZipCode.Enabled = t;
        dddlbalance.Enabled = t;
        ddlActive.Enabled = t;
        // ddlAssAccManagerId.Enabled = t;
        ddlAssPurDeptId.Enabled = t;
        ddlAssRecieveDeptId.Enabled = t;
        ddlAssSalDeptId.Enabled = t;
        ddlAssShipDeptId.Enabled = t;
        ddlCity.Enabled = t;
        // ddlContectPerson.SelectedIndex = 0;
        ddlCountry.Enabled = t;
        ddlGroup.Enabled = t;
        ddlState.Enabled = t;
        listip.Enabled = t;
        listb.Enabled = t;
        ddlstatus.Enabled = t;
        ddldept.Enabled = t;
        ddldesignation.Enabled = t;
        btnaddstatus.Enabled = t;
        btnremove.Enabled = t;
        ddlemprole.Enabled = t;
        txtdate.Enabled = t;
        //btnupdate.Enabled = t;
        btnadd.Enabled = t;
        ddlcustomerdis.Enabled = t;
        txtbal.Enabled = t;
        CheckBox2.Enabled = t;
        ddwarehouse.Enabled = t;
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddldesignation.Items.Clear();
        if (ddldept.SelectedIndex > 0)
        {
            string str = "SELECT DesignationMasterId,DesignationName,DeptID " +
" FROM         DesignationMaster " +
                     "where DeptID=" + ddldept.SelectedValue + "";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ddldesignation.DataSource = dt;
                ddldesignation.DataTextField = "DesignationName";
                ddldesignation.DataValueField = "DesignationMasterId";
                ddldesignation.DataBind();
                ddldesignation.Items.Insert(0, "-Select-");
                ddldesignation.Items[0].Value = "0";
            }


        }
        else
        {
            ddldesignation.Items.Insert(0, "-Select-");
            ddldesignation.Items[0].Value = "0";
        }
    }
    protected void em()
    {
        //ddlAssAccManagerId.Enabled = false;
        ddlAssPurDeptId.Enabled = false;
        ddlAssRecieveDeptId.Enabled = false;
        ddlAssSalDeptId.Enabled = false;
        ddlAssShipDeptId.Enabled = false;
    }
    protected void vi()
    {
        //ddlAssAccManagerId.Visible = false;
        ddlAssPurDeptId.Visible = false;
        ddlAssRecieveDeptId.Visible = false;
        ddlAssSalDeptId.Visible = false;
        ddlAssShipDeptId.Visible = false;
        //Label112.Visible = false;
        Label113.Visible = false;
        Label114.Visible = false;
        Label115.Visible = false;
        Label116.Visible = false;

    }
    protected void ddlPartyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPartyType.SelectedItem.Text == "Employee" || ddlPartyType.SelectedItem.Text == "Admin")
        {
            lbldept.Visible = true;
            ddldept.Visible = true;
            lbldes.Visible = true;
            ddldesignation.Visible = true;
        }
        else
        {
            lbldept.Visible = false;
            ddldept.Visible = false;
            lbldes.Visible = false;
            ddldept.SelectedIndex = 0;
            ddldesignation.SelectedIndex = 0;

            ddldesignation.Visible = false;
        }

        if (ddlPartyType.SelectedItem.Text == "Employee")
        {
            em(); vi();

        }
        else
            if (ddlPartyType.SelectedItem.Text == "Employee - OnlineManagement")
            {
                em(); vi();
            }
            else
                if (ddlPartyType.SelectedItem.Text == "Employee - Warehouse")
                {
                    em(); vi();
                }
                else
                    if (ddlPartyType.SelectedItem.Text == "Employee - OnlinetechSupport")
                    {
                        em(); vi();
                    }
                    else
                        if (ddlPartyType.SelectedItem.Text == "Employee - OnlineManagement")
                        {
                            em(); vi();
                        }
                        else
                            if (ddlPartyType.SelectedItem.Text == "Employee - Sale")
                            {
                                em(); vi();
                            }
                            else if (ddlPartyType.SelectedItem.Text == "Employee - CustomerSupport")
                            {
                                em(); vi();
                            }
                            else
                                if (ddlPartyType.SelectedItem.Text == "Vendor")
                                {
                                    //ddldept.Enabled = false;
                                    //ddldesignation.Enabled = false;
                                    //Label112.Visible = true;
                                    //Label113.Visible = true;
                                    //Label114.Visible = true;
                                    //Label115.Visible = true;
                                    //Label116.Visible = true;
                                    ////ddlAssAccManagerId.Enabled = true;
                                    //ddlAssPurDeptId.Enabled = true;
                                    //ddlAssRecieveDeptId.Enabled = true;
                                    //ddlAssSalDeptId.Enabled = true;
                                    //ddlAssShipDeptId.Enabled = true;
                                    ////ddlAssAccManagerId.Visible = true;
                                    //ddlAssPurDeptId.Visible = true;
                                    //ddlAssRecieveDeptId.Visible = true;
                                    //ddlAssSalDeptId.Visible = true;
                                    //ddlAssShipDeptId.Visible = true;


                                    qryStr = "SELECT     EmployeeMaster.EmployeeMasterID, EmployeeMaster.EmployeeName, EmployeeMaster.PartyID, User_master.Active " +
          " FROM         EmployeeMaster LEFT OUTER JOIN " +
                            " User_master ON EmployeeMaster.PartyID = User_master.PartyID LEFT OUTER JOIN DepartmentmasterMNC on   " +
                                "    DepartmentmasterMNC.Departmentid=User_master.Department " +
                                  "  where User_master.Active = 1 and  DepartmentmasterMNC.Companyid ='" + compid1 + "' order by EmployeeMaster.EmployeeName";


                                    //ddlAssAccManagerId.DataSource = (DataSet)fillddl(qryStr);
                                    //fillddlOther(ddlAssAccManagerId, "EmployeeName", "EmployeeMasterID");
                                    //ddlAssAccManagerId.Items.Insert(0, "Select");
                                    //ddlAssAccManagerId.Items[0].Value = "0";

                                    ddlAssPurDeptId.DataSource = (DataSet)fillddl(qryStr);
                                    fillddlOther(ddlAssPurDeptId, "EmployeeName", "EmployeeMasterID");
                                    ddlAssPurDeptId.Items.Insert(0, "Select");
                                    ddlAssPurDeptId.Items[0].Value = "0";

                                    ddlAssRecieveDeptId.DataSource = (DataSet)fillddl(qryStr);
                                    fillddlOther(ddlAssRecieveDeptId, "EmployeeName", "EmployeeMasterID");
                                    ddlAssRecieveDeptId.Items.Insert(0, "Select");
                                    ddlAssRecieveDeptId.Items[0].Value = "0";

                                    ddlAssSalDeptId.DataSource = (DataSet)fillddl(qryStr);
                                    fillddlOther(ddlAssSalDeptId, "EmployeeName", "EmployeeMasterID");
                                    ddlAssSalDeptId.Items.Insert(0, "Select");
                                    ddlAssSalDeptId.Items[0].Value = "0";

                                    ddlAssShipDeptId.DataSource = (DataSet)fillddl(qryStr);
                                    fillddlOther(ddlAssShipDeptId, "EmployeeName", "EmployeeMasterID");
                                    ddlAssShipDeptId.Items.Insert(0, "Select");
                                    ddlAssShipDeptId.Items[0].Value = "0";
                                    //ModalPopupExtender142422.Show();
                                    // }
                                }
                                else
                                {

                                    //ddldept.Enabled = true;
                                    //ddldesignation.Enabled = true;
                                    //Label112.Visible = true;
                                    Label113.Visible = true;
                                    Label114.Visible = true;
                                    Label115.Visible = true;
                                    Label116.Visible = true;
                                    //ddlAssAccManagerId.Enabled = true;
                                    //ddlAssPurDeptId.Enabled = true;
                                    //ddlAssRecieveDeptId.Enabled = true;
                                    //ddlAssSalDeptId.Enabled = true;
                                    //ddlAssShipDeptId.Enabled = true;
                                    ////ddlAssAccManagerId.Visible = true;
                                    //ddlAssPurDeptId.Visible = true;
                                    //ddlAssRecieveDeptId.Visible = true;
                                    //ddlAssSalDeptId.Visible = true;
                                    //ddlAssShipDeptId.Visible = true;
                                    Label113.Visible = false;
                                    Label115.Visible = false;
                                    Label116.Visible = false;
                                    Label114.Visible = false;


                                    qryStr = "SELECT     EmployeeMaster.EmployeeMasterID, EmployeeMaster.EmployeeName, EmployeeMaster.PartyID, User_master.Active " +
          " FROM         EmployeeMaster LEFT OUTER JOIN " +
                            " User_master ON EmployeeMaster.PartyID = User_master.PartyID LEFT OUTER JOIN DepartmentmasterMNC on   " +
                                "    DepartmentmasterMNC.Departmentid=User_master.Department " +
                                  "  where User_master.Active = 1 and  DepartmentmasterMNC.Companyid ='" + compid1 + "' order by EmployeeMaster.EmployeeName";



                                    //ddlAssAccManagerId.DataSource = (DataSet)fillddl(qryStr);
                                    //fillddlOther(ddlAssAccManagerId, "EmployeeName", "EmployeeMasterID");
                                    //ddlAssAccManagerId.Items.Insert(0, "Select");
                                    //ddlAssAccManagerId.Items[0].Value = "0";

                                    ddlAssPurDeptId.DataSource = (DataSet)fillddl(qryStr);
                                    fillddlOther(ddlAssPurDeptId, "EmployeeName", "EmployeeMasterID");
                                    ddlAssPurDeptId.Items.Insert(0, "Select");
                                    ddlAssPurDeptId.Items[0].Value = "0";

                                    ddlAssRecieveDeptId.DataSource = (DataSet)fillddl(qryStr);
                                    fillddlOther(ddlAssRecieveDeptId, "EmployeeName", "EmployeeMasterID");
                                    ddlAssRecieveDeptId.Items.Insert(0, "Select");
                                    ddlAssRecieveDeptId.Items[0].Value = "0";

                                    ddlAssSalDeptId.DataSource = (DataSet)fillddl(qryStr);
                                    fillddlOther(ddlAssSalDeptId, "EmployeeName", "EmployeeMasterID");
                                    ddlAssSalDeptId.Items.Insert(0, "Select");
                                    ddlAssSalDeptId.Items[0].Value = "0";

                                    ddlAssShipDeptId.DataSource = (DataSet)fillddl(qryStr);
                                    fillddlOther(ddlAssShipDeptId, "EmployeeName", "EmployeeMasterID");
                                    ddlAssShipDeptId.Items.Insert(0, "Select");
                                    ddlAssShipDeptId.Items[0].Value = "0";
                                    //ModalPopupExtender142422.Show();
                                    // }
                                }

        fillrole();
        CUSTDIS();
    }
    protected void CUSTDfilter()
    {
        ddlfiltercusdis.Items.Clear();
        if (ddlfilterbypartytype.SelectedItem.Text == "Customer")
        {
            lblfiltercustdiscategory.Visible = true;
            ddlfiltercusdis.Visible = true;
            string strpartytype = "";
            if (ddshorting.SelectedIndex > 0)
            {
                strpartytype = " SELECT     PartyTypeCategoryMasterId,WarehouseMaster.Name +':'+ PartyCategoryName as PartyCategoryName, PartyCategoryDiscount, CONVERT(nvarchar(10), EntryDate, 101) AS EntryDate, " +
                                            " CASE WHEN (IsPercentage = 1) THEN 'IsPer' ELSE 'Amt' END AS per " +
                                            " FROM PartyTypeCategoryMasterTbl inner join WarehouseMaster on WarehouseMaster.WarehouseId=PartyTypeCategoryMasterTbl.Whid  " +

                    " where PartyTypeCategoryMasterTbl.Whid='" + ddshorting.SelectedValue + "' and (PartyTypeCategoryMasterTbl.Active = 1)  order by PartyCategoryName";
            }
            else
            {
                strpartytype = " SELECT     PartyTypeCategoryMasterId,WarehouseMaster.Name +':'+ PartyCategoryName as PartyCategoryName, PartyCategoryDiscount, CONVERT(nvarchar(10), EntryDate, 101) AS EntryDate, " +
                                          " CASE WHEN (IsPercentage = 1) THEN 'IsPer' ELSE 'Amt' END AS per " +
                                          " FROM PartyTypeCategoryMasterTbl inner join WarehouseMaster on WarehouseMaster.WarehouseId=PartyTypeCategoryMasterTbl.Whid  " +

                  " where PartyTypeCategoryMasterTbl.compid='" + Session["Comid"] + "' and (PartyTypeCategoryMasterTbl.Active = 1)  order by PartyCategoryName";

            }
            SqlCommand cmdpartytype = new SqlCommand(strpartytype, con);
            SqlDataAdapter adppartytype = new SqlDataAdapter(cmdpartytype);
            DataTable dtpartytype = new DataTable();
            adppartytype.Fill(dtpartytype);
            if (dtpartytype.Rows.Count > 0)
            {
                ddlfiltercusdis.DataSource = dtpartytype;
                ddlfiltercusdis.DataTextField = "PartyCategoryName";
                ddlfiltercusdis.DataValueField = "PartyTypeCategoryMasterId";
                ddlfiltercusdis.DataBind();

            }
            ddlfiltercusdis.Items.Insert(0, "All");
            ddlfiltercusdis.Items[0].Value = "0";

        }
        else
        {
            lblfiltercustdiscategory.Visible = false;
            ddlfiltercusdis.Visible = false;
        }
    }

    protected void CUSTDIS()
    {
        ddlcustomerdis.Items.Clear();
        if (ddlPartyType.SelectedItem.Text == "Customer")
        {
            lblcustdisc.Visible = true;
            ddlcustomerdis.Visible = true;
            string strpartytype = " SELECT   WarehouseMaster.Name,  PartyTypeCategoryMasterId, PartyCategoryName, PartyCategoryDiscount, CONVERT(nvarchar(10), EntryDate, 101) AS EntryDate, " +
                                       " CASE WHEN (IsPercentage = 1) THEN 'IsPer' ELSE 'Amt' END AS per " +
                                       " FROM PartyTypeCategoryMasterTbl inner join WarehouseMaster on WarehouseMaster.WarehouseId=PartyTypeCategoryMasterTbl.Whid  " +

               " where PartyTypeCategoryMasterTbl.Whid='" + ddwarehouse.SelectedValue + "' and (PartyTypeCategoryMasterTbl.Active = 1)  order by PartyCategoryName";

            SqlCommand cmdpartytype = new SqlCommand(strpartytype, con);
            SqlDataAdapter adppartytype = new SqlDataAdapter(cmdpartytype);
            DataTable dtpartytype = new DataTable();
            adppartytype.Fill(dtpartytype);
            if (dtpartytype.Rows.Count > 0)
            {
                ddlcustomerdis.DataSource = dtpartytype;
                ddlcustomerdis.DataTextField = "PartyCategoryName";
                ddlcustomerdis.DataValueField = "PartyTypeCategoryMasterId";
                ddlcustomerdis.DataBind();

            }
            ddlcustomerdis.Items.Insert(0, "-Select-");
            ddlcustomerdis.Items[0].Value = "0";
            //ModalPopupExtender142422.Show();
        }
        else
        {
            lblcustdisc.Visible = false;
            ddlcustomerdis.Visible = false;
        }
    }

    protected void dddlbalance_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ImageButton61_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Shoppingcart/Admin/wzDesignationmaster.aspx");
    }
    protected void ImageButton71_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Shoppingcart/Admin/wzEmployeeMaster.aspx");
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgriddata();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        fillgriddata();
    }
    protected void GridView1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        fillgriddata();
    }
    protected void dddlbalance_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void ddldesignation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        String qryStr1 = "Select id,Departmentname from DepartmentmasterMNC  where Companyid='" + compid1 + "' and DepartmentmasterMNC.Whid='" + ddwarehouse.SelectedValue + "'  order by Departmentname";
        ddldept.DataSource = (DataSet)fillddl(qryStr1);
        fillddlOther(ddldept, "Departmentname", "id");
        ddldept.Items.Insert(0, "-Select-");
        ddldept.Items[0].Value = "0";

        ddldesignation.Items.Insert(0, "-Select-");
        ddldesignation.Items[0].Value = "0";
        CUSTDIS();

        string strgrd1 = "select CompanyWebsiteAddressMaster.*,CompanyWebsitMaster.CompanyWebsiteMasterId from CompanyWebsiteAddressMaster inner join CompanyWebsitMaster on  CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = CompanyWebsitMaster.CompanyWebsiteMasterId  where CompanyWebsitMaster.WHId='" + ddwarehouse.SelectedValue + "' ";
        SqlDataAdapter adpt = new SqlDataAdapter(strgrd1, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dt.Rows[0]["Country"].ToString()));
            //ddlCountry_SelectedIndexChanged(sender, e);
            //ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dt.Rows[0]["State"].ToString()));
            //ddlState_SelectedIndexChanged(sender, e);
            //ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(dt.Rows[0]["City"].ToString()));
        }
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddshorting_SelectedIndexChanged(object sender, EventArgs e)
    {
        CUSTDfilter();


    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        int flag = 1;
        if (listip.Items.Count > 0)
        {
            for (int i = 0; i < listip.Items.Count; i++)
            {
                string[] separator1 = new string[] { " : " };
                string[] strSplitArr1 = listip.Items[i].Text.ToString().Split(separator1, StringSplitOptions.RemoveEmptyEntries);
                string cridit = strSplitArr1[0].ToString();
                if (cridit == dddlbalance.SelectedItem.Text.ToString())
                {
                    flag += 1;
                    lblmsg.Text = "You have already used this balance limit type";
                }

            }
        }
        else
        {
            flag += 1;
            listip.Visible = true;
            listip.Items.Add(dddlbalance.SelectedItem.Text + " : " + txtbal.Text);
            lblmsg.Text = "";
        }
        if (flag.ToString() == "1")
        {
            listip.Visible = true;
            listip.Items.Add(dddlbalance.SelectedItem.Text + " : " + txtbal.Text);
            lblmsg.Text = "";
        }
        if (listip.Items.Count > 0)
        {
            btnupdate.Visible = true;
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(listip.SelectedItem) != "")
        {
            string[] separator1 = new string[] { " : " };
            string[] strSplitArr1 = listip.SelectedItem.Text.ToString().Split(separator1, StringSplitOptions.RemoveEmptyEntries);

            string cridit = strSplitArr1[0].ToString();
            string amo = strSplitArr1[1].ToString();
            dddlbalance.SelectedIndex = dddlbalance.Items.IndexOf(dddlbalance.Items.FindByText(cridit));
            txtbal.Text = amo.ToString();
            listip.Items.Remove(listip.SelectedItem.Text);
        }

    }
    protected void fillrole()
    {
        //string emprole1 = " select Party_master.Account from User_master inner join Party_master on Party_master.PartyID=User_master.PartyID where Party_master.id='" + compid1 + "'  and User_master.UserID='" + Session["userid"] + "' ";
        //SqlCommand cmdrole1 = new SqlCommand(emprole1, con);
        //SqlDataAdapter darole1 = new SqlDataAdapter(cmdrole1);
        //DataTable dtrole1 = new DataTable();
        //darole1.Fill(dtrole1);
        //int aid = Convert.ToInt32(dtrole1.Rows[0]["Account"].ToString());


        //if (aid == 30000)
        //{
        //    string emprole = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + compid1 + "' order by Role_name";
        //    SqlCommand cmdrole = new SqlCommand(emprole, con);
        //    SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        //    DataTable dtrole = new DataTable();

        //    darole.Fill(dtrole);
        //    ddlemprole.DataSource = dtrole;
        //    ddlemprole.DataTextField = "Role_name";
        //    ddlemprole.DataValueField = "Role_id";
        //    ddlemprole.DataBind();
        //}
        //else
        //{
        //    string emprole = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + compid1 + "' and Role_name<>'Admin' order by Role_name";
        //    SqlCommand cmdrole = new SqlCommand(emprole, con);
        //    SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        //    DataTable dtrole = new DataTable();

        //    darole.Fill(dtrole);
        //    ddlemprole.DataSource = dtrole;
        //    ddlemprole.DataTextField = "Role_name";
        //    ddlemprole.DataValueField = "Role_id";
        //    ddlemprole.DataBind();

        //}
        string emprole = "";
        if (ddlPartyType.SelectedItem.Text == "Vendor" || ddlPartyType.SelectedItem.Text == "Customer")
        {
            emprole = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + compid1 + "' and Role_name='" + ddlPartyType.SelectedItem.Text + "' order by Role_name";
            ddlemprole.Enabled = false;
        }
        else
        {
            emprole = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + compid1 + "' order by Role_name";
            ddlemprole.Enabled = true;
        }
        SqlCommand cmdrole = new SqlCommand(emprole, con);
        SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        DataTable dtrole = new DataTable();

        darole.Fill(dtrole);
        ddlemprole.DataSource = dtrole;
        ddlemprole.DataTextField = "Role_name";
        ddlemprole.DataValueField = "Role_id";
        ddlemprole.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int acce = 0;
        if (CheckBox2.Checked == true && tbEmail.Text == "")
        {

            lblmsg.Text = "Please fill email address";
            tbEmail.Focus();


        }
        else
        {
            SqlCommand cmdec = new SqlCommand("Select Email from Party_master where Email='" + tbEmail.Text + "'", con);
            SqlDataAdapter adpec = new SqlDataAdapter(cmdec);
            DataTable dsec = new DataTable();
            adpec.Fill(dsec);

            if (dsec.Rows.Count == 0)
            {


                acce = 0;
            }
            else
            {
                if (tbEmail.Text.Length > 0)
                {
                    acce = 2;
                    tbEmail.Focus();
                }
                else
                {
                    acce = 0;
                }
            }
            if (acce == 0)
            {

                lblmsg.Text = "";
                string date = "select Convert(nvarchar,StartDate,101) as StartDate,Convert(nvarchar,EndDate,101) EndDate from [ReportPeriod] where Compid = '" + compid1 + "' and Whid='" + ddwarehouse.SelectedValue + "' and Active='1'";
                SqlCommand cmd1111111 = new SqlCommand(date, con);
                SqlDataAdapter adp1111 = new SqlDataAdapter(cmd1111111);
                DataTable dt1111 = new DataTable();
                adp1111.Fill(dt1111);
                if (dt1111.Rows.Count > 0)
                {
                    if ((Convert.ToDateTime(txtdate.Text) < Convert.ToDateTime(dt1111.Rows[0][0].ToString())))
                    {
                        lblstartdate.Text = dt1111.Rows[0][0].ToString();
                        ModalPopupExtender1.Show();

                    }

                    else
                    {
                        groupclass();

                        SqlCommand cmddate = new SqlCommand("select StartDateOfAccountYear from CompanyMaster where Compid='" + compid1 + "'", con);
                        SqlDataAdapter dtpdate = new SqlDataAdapter(cmddate);
                        DataTable dtdate = new DataTable();
                        dtpdate.Fill(dtdate);

                        if (dtdate.Rows.Count > 0)
                        {
                            if (dtdate.Rows[0]["StartDateOfAccountYear"].ToString() != "")
                            {

                                if (Convert.ToDateTime(txtdate.Text) < Convert.ToDateTime(dtdate.Rows[0]["StartDateOfAccountYear"].ToString()))
                                {
                                    lblmsg.Text = "";
                                    lblmsg.Text = "Start date can not be earlier then the " + Convert.ToDateTime(dtdate.Rows[0]["StartDateOfAccountYear"].ToString()).ToShortDateString() + "";

                                }
                                else
                                {
                                    bool access = UserAccess.Usercon("Party_Master", lblpno.Text, "PartyId", "", "", "id", "Party_Master");
                                    if (access == true)
                                    {
                                        qryStr = " insert into AccountMaster(ClassId,AccountId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) " +
                                                 " values ('" + classid + "','" + accid + "','" + groupid + "','" + tbName.Text + "','New Party','0'," + System.DateTime.Now.ToShortDateString() + ",'0','0','" + System.DateTime.Now.ToShortDateString() + "','" + ddlActive.SelectedValue + "','" + compid1 + "','" + ddwarehouse.SelectedValue.ToString() + "')";
                                        SqlCommand cm = new SqlCommand(qryStr, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();

                                        }

                                        cm.ExecuteNonQuery();
                                        con.Close();

                                        string str1113 = "select max(Id) as Aid from AccountMaster";
                                        SqlCommand cmd1113 = new SqlCommand(str1113, con);
                                        SqlDataAdapter adp1113 = new SqlDataAdapter(cmd1113);
                                        DataTable ds1113 = new DataTable();
                                        adp1113.Fill(ds1113);
                                        Session["maxaid"] = ds1113.Rows[0]["Aid"].ToString();



                                        string st153 = "select Report_Period_Id,Cast(EndDate as Date)  as EndDate  from ReportPeriod where Compid='" + compid1 + "' and Whid='" + ddwarehouse.SelectedValue + "' and Active='1'";
                                        SqlCommand cmd153 = new SqlCommand(st153, con);
                                        SqlDataAdapter adp153 = new SqlDataAdapter(cmd153);
                                        DataTable ds153 = new DataTable();
                                        adp153.Fill(ds153);
                                        Session["reportid"] = ds153.Rows[0]["Report_Period_Id"].ToString();


                                        string st1531 = "select Report_Period_Id,Cast(EndDate as Date)  as EndDate from [ReportPeriod] where EndDate<'" + ds153.Rows[0]["EndDate"] + "' and  Whid='" + ddwarehouse.SelectedValue + "'  order by EndDate Desc";
                                        SqlCommand cmd1531 = new SqlCommand(st1531, con);
                                        SqlDataAdapter adp1531 = new SqlDataAdapter(cmd1531);
                                        DataTable ds1531 = new DataTable();
                                        adp1531.Fill(ds1531);
                                        Session["reportid1"] = ds1531.Rows[0]["Report_Period_Id"].ToString();

                                        string str4562 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid1"] + "')";
                                        SqlCommand cmd4562 = new SqlCommand(str4562, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmd4562.ExecuteNonQuery();
                                        con.Close();



                                        string str456 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid"].ToString() + "')";
                                        SqlCommand cmd456 = new SqlCommand(str456, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmd456.ExecuteNonQuery();
                                        con.Close();



                                        if (listip.Items.Count > 0)
                                        {
                                            for (int i = 0; i < listip.Items.Count; i++)
                                            {
                                                string[] separator1 = new string[] { " : " };
                                                string[] strSplitArr1 = listip.Items[i].Text.ToString().Split(separator1, StringSplitOptions.RemoveEmptyEntries);

                                                string cridit = strSplitArr1[0].ToString();
                                                string amo = strSplitArr1[1].ToString();
                                                dddlbalance.SelectedIndex = dddlbalance.Items.IndexOf(dddlbalance.Items.FindByText(cridit));
                                                string str111 = "insert into AccountBalanceLimit(AccountId,BalanceLimitTypeId,BalancelimitAmount,DateTime,Whid) " +
                                                  " values('" + accid + "','" + dddlbalance.SelectedValue + "','" + amo + "','" + txtdate.Text + "','" + ddwarehouse.SelectedValue + "')";
                                                SqlCommand cmd111 = new SqlCommand(str111, con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmd111.ExecuteNonQuery();
                                                con.Close();


                                            }
                                        }
                                        else
                                        {
                                            string str111 = "insert into AccountBalanceLimit(AccountId,BalanceLimitTypeId,BalancelimitAmount,DateTime,Whid) " +
                                                  " values('" + accid + "','" + dddlbalance.SelectedValue + "','" + txtbal.Text + "','" + txtdate.Text + "','" + ddwarehouse.SelectedValue + "')";
                                            SqlCommand cmd111 = new SqlCommand(str111, con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmd111.ExecuteNonQuery();
                                            con.Close();
                                        }


                                        SqlDataAdapter ad = new SqlDataAdapter("select max(AccountBalanceLimitId) as balid from AccountBalanceLimit where Whid='" + ddwarehouse.SelectedValue + "'", con);
                                        DataSet ds112 = new DataSet();
                                        ad.Fill(ds112);
                                        if (ds112.Tables[0].Rows.Count > 0)
                                        {
                                            ViewState["balid"] = ds112.Tables[0].Rows[0]["balid"].ToString();
                                        }


                                        SqlConnection conn3 = new SqlConnection(strconn);
                                        string ins1 = "insert into Party_master(Account,Compname,Contactperson,Address,City,State,Country,Website,GSTno,Incometaxno,Email,Phoneno,DataopID, " +
                                        " PartyTypeId,AssignedAccountManagerId,AssignedRecevingDepartmentInchargeId,AssignedPurchaseDepartmentInchargeId,AssignedShippingDepartmentInchargeId, " +
                                        " AssignedSalesDepartmentIncharge,StatusMasterId,Fax,AccountnameID,AccountBalanceLimitId,id,Whid,Zipcode,PartyTypeCategoryNo) " +
                                        " values ( '" + accid + "','" + tbCompanyName.Text + "','" + tbName.Text + "', " +
                                        "'" + tbAddress.Text + "','" + ddlCity.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCountry.SelectedValue + "','" + tbWebsite.Text + "', " +
                                        " '" + tbGSTNumber.Text + "' ,'" + tbITNumber.Text + "','" + tbEmail.Text + "','" + tbPhone.Text + "','1', '" + ddlPartyType.SelectedValue + "' ,'" + "' ,'" + ddlAssPurDeptId.SelectedValue + "', " +
                                        " '" + ddlAssRecieveDeptId.SelectedValue + "' , '" + ddlAssSalDeptId.SelectedValue + "' , '" + ddlAssShipDeptId.SelectedValue + "' ,'" + ddlActive.SelectedValue + "' , '" + tbFax.Text + "' ,'1','" + ViewState["balid"] + "','" + Session["comid"] + "','" + ddwarehouse.SelectedValue.ToString() + "','" + tbZipCode.Text + "','" + lblpno.Text + "')";
                                        SqlCommand cmd3 = new SqlCommand(ins1);
                                        cmd3.Connection = conn3;
                                        conn3.Open();
                                        cmd3.ExecuteNonQuery();

                                        SqlConnection conn5 = new SqlConnection(strconn);
                                        string sel = "select max(PartyID) as PartyID from Party_master";
                                        SqlCommand cmd5 = new SqlCommand(sel);
                                        cmd5.Connection = conn5;
                                        SqlDataAdapter da5 = new SqlDataAdapter();
                                        da5.SelectCommand = cmd5;
                                        DataSet ds5 = new DataSet();
                                        da5.Fill(ds5, "Party_master");
                                        ViewState["PartyMasterId"] = Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyID"]);

                                        string strgetusername = "select * from Party_master where PartyID='" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyID"]) + "'";
                                        SqlCommand cmdusername = new SqlCommand(strgetusername, con);
                                        SqlDataAdapter adpusername = new SqlDataAdapter(cmdusername);
                                        DataTable dsusername = new DataTable();
                                        adpusername.Fill(dsusername);

                                        string yearactive = "select LEFT(Name,4) as Name  from ReportPeriod where Compid='" + compid1 + "' and Active='1' and Whid='" + ddwarehouse.SelectedValue + "'";
                                        SqlCommand cmdactive = new SqlCommand(yearactive, con);
                                        SqlDataAdapter adpactive = new SqlDataAdapter(cmdactive);
                                        DataTable dsactive = new DataTable();
                                        adpactive.Fill(dsactive);


                                        string username = tbCompanyName.Text + dsusername.Rows[0]["PartyID"].ToString();
                                        string Password = "Party" + dsactive.Rows[0]["Name"].ToString() + "++";

                                        Session["PartyUser"] = username.ToString();
                                        Session["PartyPassword"] = ClsEncDesc.Decrypted(Password.ToString());




                                        SqlConnection conn6 = new SqlConnection(strconn);
                                        string ins6 = "insert into User_master(Name,Address ,City,State,Country,Phoneno,EmailID ,Username,Department,Accesslevel,PartyID,DesigantionMasterId,Photo,Active,Extention,zipcode)" +
                                                                      "values ('" + tbName.Text + "','" + tbAddress.Text + "','" + ddlCity.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCountry.SelectedValue + "','" + tbPhone.Text + "','" + tbEmail.Text + "','" + username + "','" + ddldept.SelectedValue + "','1','" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"]) + "','" + ddldesignation.SelectedValue + "','' ,'" + ddlActive.SelectedValue + "','" + tbExtension.Text + "','" + tbZipCode.Text + "')";
                                        SqlCommand cmd6 = new SqlCommand(ins6);
                                        cmd6.Connection = conn6;
                                        conn6.Open();
                                        cmd6.ExecuteNonQuery();


                                        SqlConnection conn10 = new SqlConnection(strconn);
                                        string sel11 = "select max(UserID) as UserID from User_master";
                                        SqlCommand cmd10 = new SqlCommand(sel11);
                                        cmd10.Connection = conn10;
                                        SqlDataAdapter da10 = new SqlDataAdapter();
                                        da10.SelectCommand = cmd10;
                                        DataSet ds10 = new DataSet();
                                        da10.Fill(ds10, "User_master");


                                        //SqlConnection conn9 = new SqlConnection(strconn);
                                        //string ins7 = "insert into Login_master(UserID,username,password,department,accesslevel,deptid,accessid) values ('" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "','" + username + "','" + ClsEncDesc.Encrypted(Password) + "','" + ddldept.SelectedValue + "','1','" + ddldesignation.SelectedValue + "','1')";
                                        //SqlCommand cmd9 = new SqlCommand(ins7);
                                        //cmd9.Connection = conn9;
                                        //conn9.Open();
                                        //cmd9.ExecuteNonQuery();


                                        //sendmail(tbUserName.Text);
                                        if (listb.Items.Count > 0)
                                        {
                                            for (int ik = 0; ik < listb.Items.Count; ik++)
                                            {
                                                string inststatuscontrol = "insert into StatusControl(StatusMasterId,Datetime,UserMasterId) values ('" + listb.Items[ik].Value + "','" + DateTime.Now.ToShortDateString() + "','" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "')";
                                                SqlCommand cmdstaus = new SqlCommand(inststatuscontrol, con);
                                                cmdstaus.Connection = con;
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmdstaus.ExecuteNonQuery();
                                                con.Close();
                                            }
                                        }
                                        string instrole = "insert into User_Role(User_id,Role_id,ActiveDeactive) values ('" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "','" + ddlemprole.SelectedValue + "','1')";
                                        SqlCommand cmdid = new SqlCommand(instrole, con);
                                        cmdid.Connection = con;
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdid.ExecuteNonQuery();
                                        con.Close();

                                        SqlConnection conn = new SqlConnection(strconn);

                                        string str11 = "select max(UserID) as UserID from User_master";
                                        SqlCommand cmd11 = new SqlCommand(str11, conn);
                                        SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
                                        DataTable ds11 = new DataTable();
                                        adp11.Fill(ds11);


                                        Session["userid"] = ds11.Rows[0]["UserID"].ToString();
                                        Session["username"] = tbUserName.Text;

                                        if (ddlPartyType.SelectedItem.Text == "Customer")
                                        {
                                            if (ddlcustomerdis.SelectedIndex > 0)
                                            {
                                                SqlCommand cmdins = new SqlCommand("Insert into PartyTypeDetailTbl(PartyTypeCategoryMasterId,PartyID) values('" + ddlcustomerdis.SelectedValue + "','" + ds5.Tables[0].Rows[0]["PartyID"] + "')", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmdins.ExecuteNonQuery();
                                                con.Close();
                                            }
                                        }





                                        fillgriddata();

                                        if (CheckBox2.Checked == true && tbEmail.Text != "")
                                        {
                                            sendmail(tbEmail.Text);
                                        }

                                        btnCancel_Click(sender, e);
                                        lblmsg.Text = "";
                                        lblmsg.Text = "Record inserted successfully";

                                        pnlparty.Visible = false;
                                        btnshowparty.Visible = true;
                                        lbladd.Text = "";

                                        dddlbalance.SelectedIndex = dddlbalance.Items.IndexOf(dddlbalance.Items.FindByValue("2"));
                                    }
                                    else
                                    {
                                        lblmsg.Text = "";
                                        lblmsg.Text = "Sorry, you don't permitted greater record to priceplan";
                                    }

                                }
                            }

                        }
                        else
                        {
                            lblmsg.Text = "";
                            lblmsg.Text = "Record can not be inserted";
                        }



                    }
                }

            }
            else if (acce == 2)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "This email ID is already in use.";
            }
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        tbUserName.Text = "";

        tbAddress.Text = "";
        tbCompanyName.Text = "";
        tbPassword.Attributes.Clear();
        tbConPassword.Attributes.Clear();
        tbEmail.Text = "";
        tbExtension.Text = "";
        tbFax.Text = "";
        tbGSTNumber.Text = "";
        tbITNumber.Text = "";
        tbName.Text = "";
        tbPhone.Text = "";
        tbWebsite.Text = "";
        tbZipCode.Text = "";
        dddlbalance.SelectedIndex = -1;
        ddlActive.SelectedIndex = -1;
        ddlpartycate.SelectedIndex = -1;
        ddlpartycate_SelectedIndexChanged(sender, e);
        ddlPartyType.SelectedIndex = -1;
        if (ddlPartyType.SelectedIndex > 0)
        {
            //ddlAssAccManagerId.SelectedIndex = -1;
            ddlAssPurDeptId.SelectedIndex = -1;
            ddlAssRecieveDeptId.SelectedIndex = -1;
            ddlAssSalDeptId.SelectedIndex = -1;
            ddlAssShipDeptId.SelectedIndex = -1;
        }
        lbldept.Visible = false;
        ddldept.Visible = false;
        lbldes.Visible = false;
        ddldesignation.Visible = false;
        listb.Items.Clear();
        listb.Visible = false;
        btnremove.Visible = false;

        ddldept.SelectedIndex = -1;
        //ddlContectPerson.SelectedIndex = 0;
        listip.Items.Clear();
        listip.Visible = false;

        ddlGroup.SelectedIndex = -1;

        ddlstatus.SelectedIndex = -1;
        txtdate.Text = "";
        txtdate.Text = System.DateTime.Now.ToShortDateString();
        txtbal.Text = "0";
        imgbtnupdate.Visible = false;
        imgbtnedit.Visible = false;
        ddldesignation.SelectedIndex = -1;
        btnSubmit.Visible = true;
        ddldept.Enabled = true;
        ddldesignation.Enabled = true;

        btnupdate.Visible = false;
        listb.Items.Clear();
        lblcustdisc.Visible = false;
        ddlcustomerdis.Visible = false;
        controlenable(true);
        lblusername123.Visible = false;
        lblpassword123.Visible = false;
        tbUserName.Visible = false;
        tbPassword.Visible = false;
        dddlbalance.SelectedIndex = dddlbalance.Items.IndexOf(dddlbalance.Items.FindByValue("2"));

        //if (CheckBox2.Visible == false)
        //{
        //    CheckBox2.Visible = true;

        //}
        CheckBox2.Enabled = true;
        btnshowparty.Visible = true;
        CheckBox2.Checked = false;
        pnlparty.Visible = false;
        lbladd.Text = "";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();
        if (dteeed.Rows.Count > 0)
        {
            ddwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
        ddwarehouse_SelectedIndexChanged(sender, e);
    }
    protected void imgbtnedit_Click(object sender, EventArgs e)
    {
        SqlCommand cmdedit = new SqlCommand("SELECT     AccountMasterId, EditAllowed FROM Fixeddata where AccountMasterId='" + ViewState["accoutid"].ToString() + "'", con);
        SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
        DataTable dtedit = new DataTable();
        dtpedit.Fill(dtedit);
        if (dtedit.Rows.Count > 0)
        {
            ////if (dtedit.Rows[0]["EditAllowed"].ToString() == "0")
            ////{
            //RequiredFieldValidator5.ValidationGroup = "2";
            imgbtnedit.Visible = false;
            btnCancel.Enabled = true;
            imgbtnupdate.Visible = true;
            controlenable(true);
            btnupdate.Visible = true;
            //}
            //else
            //{
            //    ModalPopupExtender3.Show();
            //////}
        }
        else
        {
            //RequiredFieldValidator5.ValidationGroup = "2";
            imgbtnedit.Visible = false;
            btnCancel.Enabled = true;
            imgbtnupdate.Visible = true;
            controlenable(true);
            // btnupdate.Visible = false;
        }

    }
    protected void imgbtnupdate_Click(object sender, EventArgs e)
    {
        int acce = 0;
        if (CheckBox2.Checked == true && tbEmail.Text == "")
        {

            lblmsg.Text = "Please fill email address";
            tbEmail.Focus();


        }
        else
        {
            int fla = 0;
            int flap = 0;
            DataTable dtdeleteall = (DataTable)select(" Select TOP(1) Party_master.*,PartytTypeMaster.PartType from Party_master inner join PartytTypeMaster ON Party_master.PartyTypeId=PartytTypeMaster.PartyTypeId where Whid ='" + ddwarehouse.SelectedValue + "'order by Party_master.PartyId ASC");

            if (dtdeleteall.Rows.Count > 0)
            {
                if (Convert.ToString(dtdeleteall.Rows[0]["PartyId"]) == Convert.ToString(ViewState["partyid"]))
                {
                    fla = 1;
                }
                if (fla == 1)
                {
                    if (Convert.ToString(dtdeleteall.Rows[0]["PartyTypeId"]) != Convert.ToString(ddlPartyType.SelectedValue))
                    {
                        flap = 1;
                    }
                    else if ((Convert.ToString(dtdeleteall.Rows[0]["PartType"]) != Convert.ToString(ddlemprole.SelectedItem.Text)))
                    {
                        flap = 2;
                    }

                }
            }
            if (flap == 1)
            {
                lblmsg.Text = "";
                lblmsg.Text = "Sorry,you are not permitted to change party type for this record.";

            }
            else if (flap == 2)
            {
                lblmsg.Text = "";
                lblmsg.Text = "Sorry,you are not permitted to change party role in this record.";

            }
            else
            {
                SqlCommand cmdec = new SqlCommand("Select Email from Party_master where Email='" + tbEmail.Text + "' and PartyID<>'" + ViewState["partyid"] + "' ", con);
                SqlDataAdapter adpec = new SqlDataAdapter(cmdec);
                DataTable dsec = new DataTable();
                adpec.Fill(dsec);


                if (dsec.Rows.Count == 0)
                {

                    acce = 0;
                }
                else
                {
                    if (tbEmail.Text.Length > 0)
                    {
                        acce = 2;
                        tbEmail.Focus();
                    }
                    else
                    {
                        acce = 0;
                    }
                }
                if (acce == 0)
                {
                    //SqlCommand cmd = new SqlCommand("SELECT Party_master.* FROM  Party_master inner join User_master on User_master.PartyID=Party_master.PartyID " +
                    //            "where   Whid='" + ViewState["wh"] + "' and Username='" + tbUserName.Text + "' and   Party_master.PartyID<>'" + ViewState["partyid"] + "' ", con);
                    SqlCommand cmd = new SqlCommand("SELECT Party_master.* FROM  Party_master inner join User_master on User_master.PartyID=Party_master.PartyID " +
                               "where    Username='" + tbUserName.Text + "' and   Party_master.PartyID<>'" + ViewState["partyid"] + "' ", con);

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable ds = new DataTable();
                    adp.Fill(ds);

                    if (ds.Rows.Count > 0)
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Sorry, user name already exists.";
                        tbUserName.Focus();
                    }
                    else
                    {

                        string date = "select Convert(nvarchar,StartDate,101) as StartDate,Convert(nvarchar,EndDate,101) EndDate from [ReportPeriod] where Compid = '" + compid1 + "' and Whid='" + ddwarehouse.SelectedValue + "' and Active='1'";
                        SqlCommand cmd1111111 = new SqlCommand(date, con);
                        SqlDataAdapter adp1111 = new SqlDataAdapter(cmd1111111);
                        DataTable dt1111 = new DataTable();
                        adp1111.Fill(dt1111);
                        if (dt1111.Rows.Count > 0)
                        {
                            if ((Convert.ToDateTime(txtdate.Text) < Convert.ToDateTime(dt1111.Rows[0][0].ToString())))
                            {
                                lblstartdate.Text = dt1111.Rows[0][0].ToString();
                                ModalPopupExtender1.Show();

                            }

                            else
                            {
                                bool access = UserAccess.Usercon("Party_Master", lblpno.Text, "PartyId", "", "", "id", "Party_Master");
                                if (access == true)
                                {
                                    lblmsg.Text = "";
                                    qryStr = "Update AccountMaster set AccountName='" + tbName.Text + "',Status='" + ddlActive.SelectedValue + "' where AccountId='" + ViewState["accoutid"] + "' and Whid='" + ViewState["wh"] + "' ";
                                    //ClassId='" + classid + "',AccountId='" + accid + "',GroupId='" + groupid + "',
                                    SqlCommand cm = new SqlCommand(qryStr, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cm.ExecuteNonQuery();
                                    con.Close();
                                    if (listip.Items.Count > 0)
                                    {
                                        for (int i = 0; i < listip.Items.Count; i++)
                                        {
                                            string[] separator1 = new string[] { " : " };
                                            string[] strSplitArr1 = listip.Items[i].Text.ToString().Split(separator1, StringSplitOptions.RemoveEmptyEntries);

                                            string cridit = strSplitArr1[0].ToString();
                                            string amo = strSplitArr1[1].ToString();
                                            dddlbalance.SelectedIndex = dddlbalance.Items.IndexOf(dddlbalance.Items.FindByText(cridit));
                                            string str111 = "Update AccountBalanceLimit set BalancelimitAmount='" + amo + "',DateTime='" + txtdate.Text + "'  " +
                                        " where AccountId='" + ViewState["accoutid"] + "'  and Whid='" + ViewState["wh"] + "' and BalanceLimitTypeId='" + dddlbalance.SelectedValue + "'";
                                            SqlCommand cmd111 = new SqlCommand(str111, con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmd111.ExecuteNonQuery();
                                            con.Close();


                                        }
                                    }
                                    else
                                    {
                                        string str111 = "Update AccountBalanceLimit set BalanceLimitTypeId='" + dddlbalance.SelectedValue + "',BalancelimitAmount='" + txtbal.Text + "',DateTime='" + txtdate.Text + "'  " +
                                       " where AccountId='" + ViewState["accoutid"] + "'  and Whid='" + ViewState["wh"] + "'";
                                        SqlCommand cmd111 = new SqlCommand(str111, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmd111.ExecuteNonQuery();
                                        con.Close();



                                    }


                                    SqlConnection conn3 = new SqlConnection(strconn);

                                    string up = "Update Party_master set Compname='" + tbCompanyName.Text + "',Contactperson='" + tbName.Text + "',Address='" + tbAddress.Text + "', " +
                                        " City='" + ddlCity.SelectedValue + "',State='" + ddlState.SelectedValue + "',Country='" + ddlCountry.SelectedValue + "',Website='" + tbWebsite.Text + "',GSTno='" + tbGSTNumber.Text + "', " +
                                        " Incometaxno='" + tbITNumber.Text + "',Email='" + tbEmail.Text + "',Phoneno='" + tbPhone.Text + "',PartyTypeId='" + ddlPartyType.SelectedValue + "', " +
                                        " AssignedAccountManagerId='" + "',AssignedRecevingDepartmentInchargeId='" + ddlAssRecieveDeptId.SelectedValue + "' ,AssignedPurchaseDepartmentInchargeId='" + ddlAssPurDeptId.SelectedValue + "', " +
                                        " AssignedSalesDepartmentIncharge='" + ddlAssSalDeptId.SelectedValue + "' ,AssignedShippingDepartmentInchargeId='" + ddlAssShipDeptId.SelectedValue + "',StatusMasterId='" + ddlActive.SelectedValue + "' ,Fax='" + tbFax.Text + "',Zipcode='" + tbZipCode.Text + "',PartyTypeCategoryNo='" + lblpno.Text + "' where PartyID='" + ViewState["partyid"] + "' ";
                                    SqlCommand cmd3 = new SqlCommand(up);
                                    cmd3.Connection = con;
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd3.ExecuteNonQuery();


                                    SqlConnection conn6 = new SqlConnection(strconn);

                                    string up6 = "Update User_master set Name='" + tbName.Text + "',Address='" + tbAddress.Text + "',City='" + ddlCity.SelectedValue + "', " +
                                        " State='" + ddlState.SelectedValue + "',Country='" + ddlCountry.SelectedValue + "',Phoneno='" + tbPhone.Text + "',Active='" + ddlActive.SelectedValue + "', " +
                                        " EmailID='" + tbEmail.Text + "',Username='" + tbUserName.Text + "',Department='" + ddldept.SelectedValue + "',DesigantionMasterId='" + ddldesignation.SelectedValue + "',Extention='" + tbExtension.Text + "',zipcode='" + tbZipCode.Text + "' where PartyID='" + ViewState["partyid"] + "'";
                                    SqlCommand cmd6 = new SqlCommand(up6);
                                    cmd6.Connection = conn6;
                                    conn6.Open();
                                    cmd6.ExecuteNonQuery();


                                    SqlConnection conn10 = new SqlConnection(strconn);
                                    string sel11 = "SELECT  UserID FROM User_master where PartyID='" + ViewState["partyid"] + "'";
                                    SqlCommand cmd10 = new SqlCommand(sel11);
                                    cmd10.Connection = conn10;
                                    SqlDataAdapter da10 = new SqlDataAdapter();
                                    da10.SelectCommand = cmd10;
                                    DataSet ds10 = new DataSet();
                                    da10.Fill(ds10, "User_master");


                                    SqlConnection conn9 = new SqlConnection(strconn);
                                    //string ins7 = "insert into Login_master(UserID,username,password,department,accesslevel,deptid,accessid) values ('" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "','" + tbUserName.Text + "','" + tbPassword.Text + "','" + ddldept.SelectedValue + "','1','" + ddldesignation.SelectedValue + "','1')";
                                    string up7 = "Update Login_master set password='" + ClsEncDesc.Encrypted(tbPassword.Text) + "', username='" + tbUserName.Text + "',department='" + ddldept.SelectedValue + "',deptid='" + ddldesignation.SelectedValue + "' where UserID='" + ds10.Tables[0].Rows[0]["UserID"].ToString() + "'";
                                    SqlCommand cmd9 = new SqlCommand(up7);
                                    cmd9.Connection = conn9;
                                    conn9.Open();
                                    cmd9.ExecuteNonQuery();
                                    con.Close();


                                    string ststatus = "select StatusControlId, StatusMasterId from StatusControl where UserMasterId='" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"].ToString()) + "' and TranctionMasterId IS NULL and SalesOrderId IS NULL and SalesChallanMasterId IS NULL";
                                    SqlCommand cmstatus = new SqlCommand(ststatus, con);
                                    SqlDataAdapter dastatus = new SqlDataAdapter(cmstatus);
                                    DataTable dtstatus = new DataTable();
                                    dastatus.Fill(dtstatus);

                                    foreach (DataRow dr in dtstatus.Rows)
                                    {

                                        ListItem match = listb.Items.FindByValue(dr["StatusMasterId"].ToString());
                                        if (match == null)
                                        {
                                            string deletefrn = "delete from StatusControl where StatusControlId='" + dr["StatusControlId"].ToString() + "'";
                                            SqlCommand cmddel = new SqlCommand(deletefrn, con);
                                            cmddel.Connection = con;
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddel.ExecuteNonQuery();
                                            con.Close();
                                        }

                                        if (listb.Items.Count > 0)
                                        {
                                            for (int ik = 0; ik < listb.Items.Count; ik++)
                                            {
                                                string ststat = "select StatusControlId, StatusMasterId from StatusControl where UserMasterId='" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"].ToString()) + "' and StatusMasterId='" + listb.Items[ik].Value + "' and TranctionMasterId IS NULL and SalesOrderId IS NULL and SalesChallanMasterId IS NULL";
                                                SqlCommand cmstat = new SqlCommand(ststat, con);
                                                SqlDataAdapter dastat = new SqlDataAdapter(cmstat);
                                                DataTable dtstat = new DataTable();
                                                dastat.Fill(dtstat);
                                                if (dtstat.Rows.Count == 0)
                                                {
                                                    string inststatuscontrol = "insert into StatusControl(StatusMasterId,Datetime,UserMasterId) values ('" + listb.Items[ik].Value + "','" + DateTime.Now.ToShortDateString() + "','" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "')";
                                                    SqlCommand cmdstaus = new SqlCommand(inststatuscontrol, con);
                                                    cmdstaus.Connection = con;
                                                    if (con.State.ToString() != "Open")
                                                    {
                                                        con.Open();
                                                    }
                                                    cmdstaus.ExecuteNonQuery();
                                                    con.Close();
                                                }
                                            }
                                        }


                                    }

                                    string uprole = "Update User_Role set Role_id='" + ddlemprole.SelectedValue + "' where User_id='" + ds10.Tables[0].Rows[0]["UserID"].ToString() + "'";
                                    SqlCommand cmduprl = new SqlCommand(uprole, con);
                                    cmduprl.Connection = con;
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmduprl.ExecuteNonQuery();
                                    con.Close();
                                    if (Convert.ToString(ViewState["PartyTypeDetailId"]) == "")
                                    {
                                        string strpartytype1 = " SELECT   * from PartyTypeDetailTbl WHERE PartyTypeCategoryMasterId='" + ddlcustomerdis.SelectedValue + "' and PartyID='" + ViewState["partyid"] + "'";
                                        SqlCommand cmdpartytype1 = new SqlCommand(strpartytype1, con);
                                        SqlDataAdapter adppartytype1 = new SqlDataAdapter(cmdpartytype1);
                                        DataTable dtpartytype1 = new DataTable();
                                        adppartytype1.Fill(dtpartytype1);
                                        if (dtpartytype1.Rows.Count == 0)
                                        {
                                            SqlCommand cmdins = new SqlCommand("Insert into PartyTypeDetailTbl(PartyTypeCategoryMasterId,PartyID) values('" + ddlcustomerdis.SelectedValue + "','" + ViewState["partyid"] + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdins.ExecuteNonQuery();
                                            con.Close();

                                        }
                                    }
                                    else
                                    {
                                        string str219 = "";
                                        if (ddlPartyType.SelectedItem.Text == "Customer")
                                        {
                                            str219 = " update PartyTypeDetailTbl set PartyTypeCategoryMasterId='" + ddlcustomerdis.SelectedValue + "' where PartyTypeDetailId='" + ViewState["PartyTypeDetailId"] + "'";
                                        }
                                        else
                                        {
                                            str219 = " Delete from PartyTypeDetailTbl  where PartyTypeDetailId='" + ViewState["PartyTypeDetailId"] + "'";
                                        }

                                        SqlCommand cmd219 = new SqlCommand(str219, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmd219.ExecuteNonQuery();
                                        con.Close();
                                    }

                                    //if (CheckBox2.Checked == true && tbEmail.Text != "")
                                    //{
                                    //    sendmail(tbEmail.Text);
                                    //}


                                    lblusername123.Visible = false;
                                    lblpassword123.Visible = false;
                                    tbUserName.Visible = false;
                                    tbPassword.Visible = false;
                                    btnCancel_Click(sender, e);
                                    lblmsg.Text = "";
                                    lblmsg.Text = "Record updated successfully";
                                    imgbtnedit.Visible = false;
                                    btnCancel.Enabled = true;
                                    imgbtnupdate.Visible = false;

                                    fillgriddata();
                                    pnlparty.Visible = false;
                                    btnshowparty.Visible = true;
                                    lbladd.Text = "";
                                }
                                else
                                {
                                    lblmsg.Text = "";
                                    lblmsg.Text = "Sorry, you don't permitted greter record to priceplan.";
                                }
                            }
                        }
                    }
                }
                else if (acce == 2)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "This email ID is already in use.";
                }

            }
        }

    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        DataTable dtdeleteall = (DataTable)select(" Select TOP(1)* from  Party_master where Whid ='" + ddwarehouse.SelectedValue + "'order by PartyId ASC");

        if (dtdeleteall.Rows.Count > 0)
        {
            if (Convert.ToString(dtdeleteall.Rows[0]["PartyId"]) == Convert.ToString(ViewState["partyid"]))
            {
                lblmsg.Text = "";
                lblmsg.Text = "Sorry,you are not permitted to delete this record.";


            }
            else
            {

                try
                {

                    DataTable dtable = new DataTable();
                    dtable = (DataTable)select("select UserID from User_master where PartyID='" + ViewState["partyid"] + "'");

                    if (dtable.Rows.Count > 0)
                    {
                        Delete("Delete from Login_master Where UserID='" + dtable.Rows[0]["UserID"].ToString() + "'");
                        Delete("delete from User_master where PartyID = '" + ViewState["partyid"] + "'");
                        Delete("Delete From SalesChallanMaster where PartyID='" + ViewState["partyid"] + "'");
                        DataTable dtsalodsup = new DataTable();
                        dtsalodsup = (DataTable)select("select SalesOrderId from SalesOrderMaster where PartyId='" + ViewState["partyid"] + "'");
                        if (dtsalodsup.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtsalodsup.Rows)
                            {
                                Delete("delete from SalesOrderSuppliment where SalesOrderMasterId = '" + dr["SalesOrderId"].ToString() + "'");
                            }
                        }

                        Delete("delete from SalesOrderMaster where PartyId = '" + ViewState["partyid"] + "'");
                        Delete("Delete from EmployeeMaster where PartyID='" + ViewState["partyid"] + "'");
                        DataTable dttid = new DataTable();
                        dttid = (DataTable)select("Select Account,Whid from Party_master where PartyID='" + ViewState["partyid"] + "'");
                        Delete("delete from Party_master where PartyID = '" + ViewState["partyid"] + "'");

                        SqlCommand cmdins = new SqlCommand("Delete  from PartyTypeDetailTbl where PartyID='" + ViewState["partyid"] + "'", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }

                        cmdins.ExecuteNonQuery();
                        con.Close();
                        string inststatuscontrol = "Delete from  StatusControl where UserMasterId in (Select User_master.userId from User_master inner join Party_master on Party_master.PartyId=User_master.PartyId where User_master.PartyId='" + ViewState["partyid"] + "'";
                        SqlCommand cmdstaus = new SqlCommand(inststatuscontrol, con);
                        cmdstaus.Connection = con;
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }



                        if (dttid.Rows.Count > 0)
                        {
                            Delete("Delete from AccountBalanceLimit where AccountId = '" + dttid.Rows[0]["Account"].ToString() + "' and  Whid='" + dttid.Rows[0]["Whid"].ToString() + "'");
                            Delete("Delete from AccountMaster where AccountId='" + dttid.Rows[0]["Account"].ToString() + "' and  Whid='" + dttid.Rows[0]["Whid"].ToString() + "'");
                            DataTable dtt = new DataTable();
                            dtt = (DataTable)select("Select TranctionMaster.Tranction_Master_Id from TranctionMaster  inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id where AccountDebit='" + dttid.Rows[0]["Account"].ToString() + "' and TranctionMaster.Whid='" + dttid.Rows[0]["Whid"].ToString() + "'");
                            if (dtt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtt.Rows)
                                {
                                    Delete("Delete from Tranction_Details where Tranction_Master_Id = '" + dr["Tranction_Master_Id"].ToString() + "'");
                                    Delete("Delete from TranctionMaster where Tranction_Master_Id = '" + dr["Tranction_Master_Id"].ToString() + "'");
                                    Delete("Delete from TranctionMasterSuppliment where Tranction_Master_Id = '" + dr["Tranction_Master_Id"].ToString() + "'");
                                    Delete("Delete From PaymentApplicationTbl where TranMIdPayReceived = '" + dr["Tranction_Master_Id"].ToString() + "'");
                                    Delete("Delete From PaymentApplicationTbl where TranMIdAmtApplied = '" + dr["Tranction_Master_Id"].ToString() + "'");
                                }

                            }
                            DataTable dtt1 = new DataTable();
                            dtt1 = (DataTable)select("Select TranctionMaster.Tranction_Master_Id from TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id where AccountCredit='" + dttid.Rows[0]["Account"].ToString() + "' and TranctionMaster.Whid='" + dttid.Rows[0]["Whid"].ToString() + "'");
                            if (dtt1.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtt1.Rows)
                                {
                                    Delete("Delete from Tranction_Details where Tranction_Master_Id = '" + dr["Tranction_Master_Id"].ToString() + "'");
                                    Delete("Delete from TranctionMaster where Tranction_Master_Id = '" + dr["Tranction_Master_Id"].ToString() + "'");
                                    Delete("Delete from TranctionMasterSuppliment where Tranction_Master_Id = '" + dr["Tranction_Master_Id"].ToString() + "'");
                                    Delete("Delete From PaymentApplicationTbl where TranMIdPayReceived = '" + dr["Tranction_Master_Id"].ToString() + "'");
                                    Delete("Delete From PaymentApplicationTbl where TranMIdAmtApplied = '" + dr["Tranction_Master_Id"].ToString() + "'");
                                }

                            }
                        }

                    }
                }

                catch
                {
                    throw;
                }
                finally
                {
                    //ModalPopupExtender1xz.Show();
                    ModalPopupExtender145.Hide();
                    lblmsg.Text = "";
                    lblmsg.Text = "Record deleted successfully";


                    fillgriddata();
                    //controlenable(true);
                    btnCancel.Enabled = true;
                }
            }
        }
    }
    protected void ImageButton5_Click(object sender, EventArgs e)
    {
        ModalPopupExtender145.Hide();
    }
    protected void ImageButton10_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Hide();
    }
    protected void ImageButton8_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {

        ModalPopupExtender4.Hide();
    }
    protected void Panel1_DataBinding(object sender, EventArgs e)
    {

    }
    protected void btnaddstatus_Click(object sender, EventArgs e)
    {
        ListItem match = listb.Items.FindByValue(ddlstatus.SelectedValue);
        if (match == null)
        {
            listb.Items.Insert(listb.Items.Count, ddlstatus.SelectedItem.Text);
            listb.Items[listb.Items.Count - 1].Value = ddlstatus.SelectedValue;
            listb.SelectedIndex = 0;
            listb.Visible = true;
        }
        if (listb.Items.Count > 0)
        {
            btnremove.Visible = true;
        }

    }
    protected void btnremove_Click(object sender, EventArgs e)
    {
        listb.Items.RemoveAt(listb.SelectedIndex);
        if (listb.Items.Count > 0)
        {
            listb.SelectedIndex = 0;
        }

    }
    protected void ddlfilterbypartytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        CUSTDfilter();

    }
    protected void ddlfilterstatuscategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }


    protected void btnshowparty_Click(object sender, EventArgs e)
    {
        if (pnlparty.Visible == false)
        {
            pnlparty.Visible = true;
        }
        else if (pnlparty.Visible == true)
        {
            pnlparty.Visible = false;
        }
        btnshowparty.Visible = false;
        lblmsg.Text = "";
        lbladd.Text = "Add New User";
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        if (Button8.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            GridView1.AllowPaging = false;
            GridView1.PageSize = 10000;
            fillgriddata();

            Button8.Text = "Hide Printable Version";
            Button5.Visible = true;
            if (GridView1.Columns[8].Visible == true)
            {
                ViewState["edithide"] = "tt";
                GridView1.Columns[8].Visible = false;
            }
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["delhide"] = "tt";
                GridView1.Columns[9].Visible = false;
            }
            if (GridView1.Columns[10].Visible == true)
            {
                ViewState["viewprofile"] = "tt";
                GridView1.Columns[10].Visible = false;
            }
        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(200);

            GridView1.AllowPaging = true;
            GridView1.PageSize = 30;
            fillgriddata();

            Button8.Text = "Printable Version";
            Button5.Visible = false;
            if (ViewState["edithide"] != null)
            {
                GridView1.Columns[8].Visible = true;
            }
            if (ViewState["delhide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
            if (ViewState["viewprofile"] != null)
            {
                GridView1.Columns[10].Visible = true;
            }
        }
    }


    protected void ddlpartycate_SelectedIndexChanged(object sender, EventArgs e)
    {
        qryStr = "select * from PartytTypeMaster  where PartyCategoryId='" + ddlpartycate.SelectedValue + "' and  compid='" + compid1 + "' and PartType not in('Employee') order by PartType";
        ddlPartyType.DataSource = (DataSet)fillddl(qryStr);
        fillddlOther(ddlPartyType, "PartType", "PartyTypeId");
        ddlPartyType.Items.Insert(0, "-Select-");
        ddlPartyType.Items[0].Value = "0";


        string str = "Select Id, PartyMasterCategoryNo From PartyMasterCategory where Id='" + ddlpartycate.SelectedValue + "' order by Name ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable datat = new DataTable();
        adp.Fill(datat);
        if (datat.Rows.Count > 0)
        {
            lblpno.Text = Convert.ToString(datat.Rows[0]["PartyMasterCategoryNo"]);
        }
        fillrole();
    }

    protected void ddlpartycategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpartytype();

    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        fillgriddata();
    }
}
