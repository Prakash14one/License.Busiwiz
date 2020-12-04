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

public partial class BusinessCategory : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (!IsPostBack)
        {
            DataTable datcand = select("select candidatemaster.candidateid from candidatemaster where partyid='" + Convert.ToInt32(Session["PartyId"].ToString()) + "'");

            Session["CandidateID"] = datcand.Rows[0]["candidateid"].ToString();

            //Session["Comid"] = "jobcenter";

            //Session["CandidateID"] = "67";

            //Session["PartyId"] = "4237";

            CheckBox6_CheckedChanged(sender, e);

            DataTable dtaaigyo = select("select * from candidatetimings where candidateid='" + Session["CandidateID"] + "'");

            if (dtaaigyo.Rows.Count > 0)
            {

                RadioButtonList3.Items[0].Enabled = true;
            }
            else
            {

                RadioButtonList3.Items[0].Enabled = false;
            }

            // DataTable dtbuss = select("select Whid from candidatemaster where candidateid='" + Session["CandidateID"] + "'");

            // ViewState["Whid"] = dtbuss.Rows[0]["Whid"].ToString();

            filldatalist1();
            fillcity();

            fillvacancy();
            fillcurrency();
            findava();
            Fillddltimezone();
        }
    }

    protected void findava()
    {
        DataTable DTGG = select("select * from SMSCarrierTesting inner join candidatemaster on candidatemaster.candidateid=SMSCarrierTesting.candidateid where candidatemaster.partyid='" + Convert.ToInt32(Session["PartyId"].ToString()) + "'");

        if (DTGG.Rows.Count > 0)
        {
            CheckBox4.Enabled = true;
        }
        else
        {
            CheckBox4.Enabled = false;
        }
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            panel1.Visible = false;
        }
        else
        {
            panel1.Visible = true;
        }
    }

    protected void filldatalist1()
    {
        DataTable dt = CreateDatatable();

        for (int i = 1; i <= 6; i++)
        {
            DataRow Drow = dt.NewRow();

            Drow["Id"] = i;

            if (i == 1)
            {
                Drow["Name"] = "All working days this week";
            }
            if (i == 2)
            {
                Drow["Name"] = "All working days next week";
            }
            if (i == 3)
            {
                Drow["Name"] = "This week Saturday";
            }
            if (i == 4)
            {
                Drow["Name"] = "This week Sunday";
            }
            if (i == 5)
            {
                Drow["Name"] = "Next week Saturday";
            }
            if (i == 6)
            {
                Drow["Name"] = "Next week Sunday";
            }
            dt.Rows.Add(Drow);
        }

        DataList1.DataSource = dt;
        DataList1.DataBind();
    }

    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "Id";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "Name";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);

        return dt;
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "0")
        {
            panelarea.Visible = false;
        }
        else
        {
            panelarea.Visible = true;
        }
    }

    protected void fillcity()
    {
        DataTable datcand = select("select candidatemaster.CountryId,candidatemaster.StateId from candidatemaster where partyid='" + Convert.ToInt32(Session["PartyId"].ToString()) + "'");

        if (datcand.Rows.Count > 0)
        {
            DataTable dtcity = select("select CityMasterTbl.CityId,CityMasterTbl.CityName from CityMasterTbl inner join StateMasterTbl on StateMasterTbl.StateId=CityMasterTbl.StateId inner join CountryMaster on CountryMaster.CountryId=StateMasterTbl.CountryId where CityMasterTbl.StateId='" + datcand.Rows[0]["StateId"] + "' and StateMasterTbl.CountryId='" + datcand.Rows[0]["CountryId"] + "'");

            if (dtcity.Rows.Count > 0)
            {
                ddlcity.DataSource = dtcity;
                ddlcity.DataTextField = "CityName";
                ddlcity.DataValueField = "CityId";
                ddlcity.DataBind();
            }
        }
        ddlcity.Items.Insert(0, "-Select-");
        ddlcity.Items[0].Value = "0";
    }

    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;
    }

    protected void fillvacancy()
    {
        ddlvacancytype.Items.Clear();

        SqlDataAdapter dav = new SqlDataAdapter("select ID,Name from VacancyTypeMaster", con);
        DataTable dtv = new DataTable();
        dav.Fill(dtv);

        if (dtv.Rows.Count > 0)
        {
            ddlvacancytype.DataSource = dtv;
            ddlvacancytype.DataTextField = "Name";
            ddlvacancytype.DataValueField = "ID";
            ddlvacancytype.DataBind();
        }
        ddlvacancytype.Items.Insert(0, "-Select-");
        ddlvacancytype.Items[0].Value = "0";

        //ddlvacancytype.SelectedIndex = ddlvacancytype.Items.IndexOf(ddlvacancytype.Items.FindByText("Permanent"));
    }
    protected void ddlvacancytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlvacancytype.SelectedIndex > 0)
        {
            DataTable dtff = select("select ID,VacancyPositionTitle as vtitle from VacancyPositionTitleMaster where VacancyPositionTypeID='" + ddlvacancytype.SelectedValue + "' and Active=1");

            panel2.Visible = true;
            Button7.Visible = true;
            Label50.Visible = true;

            datalist2.DataSource = dtff;
            datalist2.DataBind();
        }
        else
        {
            panel2.Visible = false;
            Button7.Visible = false;
            Label50.Visible = false;

            datalist2.DataSource = null;
            datalist2.DataBind();
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        int ii = 0;

        foreach (DataListItem gr in datalist2.Items)
        {
            CheckBox chkMsg11 = (CheckBox)gr.FindControl("chkMsg11");

            Label lblvtitle = (Label)gr.FindControl("lblvtitle");

            Label Label51 = (Label)gr.FindControl("Label51");

            if (chkMsg11.Checked == true)
            {
                ii = ii + 1;

                ViewState["chkMsg11"] = "1";
                Label400.Text = "";

                DataTable dtvac = new DataTable();

                string sds = Convert.ToString(ViewState["datavac"]);

                if (Convert.ToString(ViewState["datavac"]) == "")
                {
                    DataRow Drow = dtvac.NewRow();

                    DataColumn Dcom = new DataColumn();

                    Dcom.DataType = System.Type.GetType("System.String");
                    Dcom.ColumnName = "VacancyType";
                    Dcom.AllowDBNull = true;
                    Dcom.Unique = false;
                    Dcom.ReadOnly = false;

                    DataColumn Dcom1 = new DataColumn();
                    Dcom1.DataType = System.Type.GetType("System.String");
                    Dcom1.ColumnName = "VacancyTitle";
                    Dcom1.AllowDBNull = true;
                    Dcom1.Unique = false;
                    Dcom1.ReadOnly = false;

                    DataColumn Dcom2 = new DataColumn();
                    Dcom2.DataType = System.Type.GetType("System.String");
                    Dcom2.ColumnName = "VacancyTypeID";
                    Dcom2.AllowDBNull = true;
                    Dcom2.Unique = false;
                    Dcom2.ReadOnly = false;

                    DataColumn Dcom3 = new DataColumn();
                    Dcom3.DataType = System.Type.GetType("System.String");
                    Dcom3.ColumnName = "VacancyTitleID";
                    Dcom3.AllowDBNull = true;
                    Dcom3.Unique = false;
                    Dcom3.ReadOnly = false;

                    dtvac.Columns.Add(Dcom);
                    dtvac.Columns.Add(Dcom1);
                    dtvac.Columns.Add(Dcom2);
                    dtvac.Columns.Add(Dcom3);

                    Drow["VacancyType"] = ddlvacancytype.SelectedItem.Text;
                    Drow["VacancyTitle"] = lblvtitle.Text;

                    Drow["VacancyTypeID"] = Label51.Text;
                    Drow["VacancyTitleID"] = ddlvacancytype.SelectedValue;

                    dtvac.Rows.Add(Drow);
                    ViewState["datavac"] = dtvac;
                }
                else
                {
                    dtvac = (DataTable)ViewState["datavac"];

                    int flag = 0;

                    if (dtvac.Rows.Count < 5)
                    {
                        foreach (DataRow dr in dtvac.Rows)
                        {
                            string vtype = dr["VacancyType"].ToString();
                            string vtitle = dr["VacancyTitle"].ToString();

                            if (vtype == ddlvacancytype.SelectedItem.Text && vtitle == lblvtitle.Text)
                            {
                                Label400.Text = "Record already exist";
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 0)
                        {
                            DataRow Drow = dtvac.NewRow();

                            Drow["VacancyType"] = ddlvacancytype.SelectedItem.Text;
                            Drow["VacancyTitle"] = lblvtitle.Text;

                            Drow["VacancyTypeID"] = Label51.Text;
                            Drow["VacancyTitleID"] = ddlvacancytype.SelectedValue;

                            dtvac.Rows.Add(Drow);
                            ViewState["datavac"] = dtvac;
                        }
                    }
                    else
                    {
                        Label400.Text = "Sorry, you are not allowed to add more job title please upgrade your priceplan.";
                    }
                }


            }
            panel3.Visible = true;

            GridView5.DataSource = ViewState["datavac"];
            GridView5.DataBind();
        }
        if (ViewState["chkMsg11"] == "1")
        {
            if (ii > 5)
            {
                Label400.Text = "Sorry, you are not allowed to add more job title please upgrade your priceplan.";
            }
            else
            {

            }
        }
        else
        {
            Label400.Text = "Please select atleast one Vacancy title to add.";
        }

        foreach (DataListItem grdd in datalist2.Items)
        {
            CheckBox chkMsg11 = (CheckBox)grdd.FindControl("chkMsg11");

            chkMsg11.Checked = false;
        }
    }

    protected void fillcurrency()
    {
        DataTable dtcurrency = select("select CurrencyId,CurrencyName from CurrencyMaster");

        DropDownList4.DataSource = dtcurrency;
        DropDownList4.DataTextField = "CurrencyName";
        DropDownList4.DataValueField = "CurrencyId";
        DropDownList4.DataBind();

        DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByText("USD"));
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label42.Text = DropDownList4.SelectedItem.Text;
        Label43.Text = DropDownList4.SelectedItem.Text;
        Label44.Text = DropDownList4.SelectedItem.Text;
        Label45.Text = DropDownList4.SelectedItem.Text;
        Label46.Text = DropDownList4.SelectedItem.Text;
        Label47.Text = DropDownList4.SelectedItem.Text;
    }

    protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GridView5.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["datavac"];

            dt.Rows[Convert.ToInt32(GridView5.SelectedIndex.ToString())].Delete();

            dt.AcceptChanges();

            GridView5.DataSource = dt;
            GridView5.DataBind();

            ViewState["datavac"] = dt;

            Label400.Text = "Record deleted successfully.";
        }
    }
    //protected void Button1_Click(object sender, ImageClickEventArgs e)
    //{

    //}

    protected void clear()
    {
        TextBox1.Text = "09:00";
        TextBox2.Text = "05:00";

        DropDownList1.SelectedValue = "0";
        DropDownList2.SelectedValue = "1";

        ddltimezone.SelectedIndex = ddltimezone.Items.IndexOf(ddltimezone.Items.FindByText("Eastern Time (US & Canada):Estm:-05:00"));

        RadioButtonList1.SelectedValue = "0";

        if (RadioButtonList1.SelectedValue == "0")
        {
            panel1.Visible = false;
        }
        else
        {
            panel1.Visible = true;
        }

        panel3.Visible = false;

        GridView5.DataSource = null;
        GridView5.DataBind();

        txtdescription.Text = "";
        ddlcity.SelectedIndex = 0;
        RadioButtonList2.SelectedValue = "0";

        if (RadioButtonList2.SelectedValue == "0")
        {
            panelarea.Visible = false;
        }
        else
        {
            panelarea.Visible = true;
        }
        TextBox3.Text = "";

        ddlvacancytype.SelectedIndex = 0;

        panel2.Visible = false;
        Button7.Visible = false;
        Label50.Visible = false;

        datalist2.DataSource = null;
        datalist2.DataBind();

        DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByText("USD"));

        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";

        Label42.Text = DropDownList4.SelectedItem.Text;
        Label43.Text = DropDownList4.SelectedItem.Text;
        Label44.Text = DropDownList4.SelectedItem.Text;
        Label45.Text = DropDownList4.SelectedItem.Text;
        Label46.Text = DropDownList4.SelectedItem.Text;
        Label47.Text = DropDownList4.SelectedItem.Text;

        CheckBox2.Checked = false;

        CheckBox1.Checked = true;
        CheckBox3.Checked = true;
    }

    protected void Fillddltimezone()
    {
        lblmsg.Text = "";

        string str = "select TimeZoneMaster.ID,TimeZoneMaster.Name+':'+TimeZoneMaster.ShortName+':'+TimeZoneMaster.gmt AS BatchTimeZone from TimeZoneMaster where status='1'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            ddltimezone.DataSource = ds;
            ddltimezone.DataTextField = "BatchTimeZone";
            ddltimezone.DataValueField = "ID";
            ddltimezone.DataBind();
        }

        ddltimezone.SelectedIndex = ddltimezone.Items.IndexOf(ddltimezone.Items.FindByText("Eastern Time (US & Canada):Estm:-05:00"));
    }

    protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox5.Checked == true)
        {
            DataTable dt11 = select("select * from candidatemyinfo where candidateid='" + Session["CandidateID"] + "'");

            if (dt11.Rows.Count > 0)
            {
                CheckBox4.Checked = Convert.ToBoolean(dt11.Rows[0]["sms"]);
                CheckBox3.Checked = Convert.ToBoolean(dt11.Rows[0]["email"]);
                CheckBox1.Checked = Convert.ToBoolean(dt11.Rows[0]["phone"]);
            }

            DataTable dt22 = select("select * from CandidateJobTitles2 where candidateid='" + Session["CandidateID"] + "'");

            if (dt22.Rows.Count > 0)
            {
                //GridView5.DataSource = dt22;
                //GridView5.DataBind();
            }

            DataTable dt33 = select("select * from candidateremuneration2 where candidateid='" + Session["CandidateID"] + "'");

            if (dt33.Rows.Count > 0)
            {
                DropDownList4.SelectedValue = dt33.Rows[0]["currencyid"].ToString();

                TextBox6.Text = dt33.Rows[0]["amt1"].ToString();
                TextBox7.Text = dt33.Rows[0]["amt2"].ToString();
                TextBox8.Text = dt33.Rows[0]["amt3"].ToString();
                TextBox9.Text = dt33.Rows[0]["amt4"].ToString();
                TextBox10.Text = dt33.Rows[0]["amt5"].ToString();
                TextBox11.Text = dt33.Rows[0]["amt6"].ToString();

                Label42.Text = DropDownList4.SelectedItem.Text;
                Label43.Text = DropDownList4.SelectedItem.Text;
                Label44.Text = DropDownList4.SelectedItem.Text;
                Label45.Text = DropDownList4.SelectedItem.Text;
                Label46.Text = DropDownList4.SelectedItem.Text;
                Label47.Text = DropDownList4.SelectedItem.Text;

                CheckBox2.Checked = Convert.ToBoolean(dt33.Rows[0]["Unspecified"]);
            }

            DataTable dt44 = select("select * from candidatelocation3 where candidateid='" + Session["CandidateID"] + "'");

            if (dt44.Rows.Count > 0)
            {
                ddlcity.SelectedValue = dt44.Rows[0]["cityid"].ToString();

                if (dt44.Rows[0]["areaid"].ToString() == "False")
                {
                    RadioButtonList2.SelectedValue = "0";
                }
                else
                {
                    RadioButtonList2.SelectedValue = "1";
                }

                RadioButtonList2_SelectedIndexChanged(sender, e);

                TextBox3.Text = dt44.Rows[0]["area"].ToString();
            }

            string s11 = "";
            string s22 = "";
            string s33 = "";
            string s44 = "";
            string s55 = "";
            string s66 = "";

            DataTable dt55 = select("select * from candidateavailable where candidateid='" + Session["CandidateID"] + "'");

            if (dt55.Rows.Count > 0)
            {
                if (dt55.Rows[0]["dayss"].ToString() == "False")
                {
                    RadioButtonList1.SelectedValue = "0";
                    RadioButtonList1_SelectedIndexChanged(sender, e);
                }
                else
                {
                    RadioButtonList1.SelectedValue = "1";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    for (int ii = 0; ii < dt55.Rows.Count; ii++)
                    {
                        if (Convert.ToString(dt55.Rows[ii]["alldays"]) != "")
                        {
                            if (dt55.Rows[ii]["alldays"].ToString() == "1")
                            {
                                s11 = "1";
                            }
                            if (dt55.Rows[ii]["alldays"].ToString() == "2")
                            {
                                s22 = "2";
                            }
                            if (dt55.Rows[ii]["alldays"].ToString() == "3")
                            {
                                s33 = "3";
                            }
                            if (dt55.Rows[ii]["alldays"].ToString() == "4")
                            {
                                s44 = "4";
                            }
                            if (dt55.Rows[ii]["alldays"].ToString() == "5")
                            {
                                s55 = "5";
                            }
                            if (dt55.Rows[ii]["alldays"].ToString() == "6")
                            {
                                s66 = "6";
                            }
                        }
                    }
                }
            }

            //CheckBox chkMsg11 = (CheckBox)DataList1.FindControl("chkMsg11");

            foreach (DataListItem dtg in DataList1.Items)
            {
                CheckBox chkMsg11 = (CheckBox)dtg.FindControl("chkMsg11");
                Label Label2 = (Label)dtg.FindControl("Label2");

                if (s11 != "")
                {
                    if (s11 == Label2.Text)
                    {
                        chkMsg11.Checked = true;
                    }
                }
                if (s22 != "")
                {
                    if (s22 == Label2.Text)
                    {
                        chkMsg11.Checked = true;
                    }
                }
                if (s33 != "")
                {
                    if (s33 == Label2.Text)
                    {
                        chkMsg11.Checked = true;
                    }
                }
                if (s44 != "")
                {
                    if (s44 == Label2.Text)
                    {
                        chkMsg11.Checked = true;
                    }
                }
                if (s55 != "")
                {
                    if (s55 == Label2.Text)
                    {
                        chkMsg11.Checked = true;
                    }
                }
                if (s66 != "")
                {
                    if (s66 == Label2.Text)
                    {
                        chkMsg11.Checked = true;
                    }
                }
            }

            DataTable dt66 = select("select * from candidatetimings where candidateid='" + Session["CandidateID"] + "'");

            if (dt66.Rows.Count > 0)
            {
                TextBox1.Text = dt66.Rows[0]["fromt"].ToString();
                TextBox2.Text = dt66.Rows[0]["tot"].ToString();

                chkMsg11.Checked = true;

                panelimpnotes.Visible = true;

                txtdescription.Text = dt66.Rows[0]["notes"].ToString();

                DropDownList1.SelectedValue = dt66.Rows[0]["fromtAMPM"].ToString();
                DropDownList2.SelectedValue = dt66.Rows[0]["totAMPM"].ToString();

                if (Convert.ToString(dt66.Rows[0]["timezone"]) != "")
                {
                    ddltimezone.SelectedValue = dt66.Rows[0]["timezone"].ToString();
                }
            }
        }
        else
        {
            clear();
        }
    }
    protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel6.Visible = true;
        lblmsg.Text = "";

        if (RadioButtonList3.SelectedValue == "0")
        {
            DataTable dt11 = select("select * from candidatemyinfo where candidateid='" + Session["CandidateID"] + "'");

            if (dt11.Rows.Count > 0)
            {
                CheckBox4.Checked = Convert.ToBoolean(dt11.Rows[0]["sms"]);
                CheckBox3.Checked = Convert.ToBoolean(dt11.Rows[0]["email"]);
                CheckBox1.Checked = Convert.ToBoolean(dt11.Rows[0]["phone"]);
            }

            DataTable dt22 = select("select * from CandidateJobTitles2 where candidateid='" + Session["CandidateID"] + "'");

            if (dt22.Rows.Count > 0)
            {
                //GridView5.DataSource = dt22;
                //GridView5.DataBind();
            }

            DataTable dt33 = select("select * from candidateremuneration2 where candidateid='" + Session["CandidateID"] + "'");

            if (dt33.Rows.Count > 0)
            {
                DropDownList4.SelectedValue = dt33.Rows[0]["currencyid"].ToString();

                TextBox6.Text = dt33.Rows[0]["amt1"].ToString();
                TextBox7.Text = dt33.Rows[0]["amt2"].ToString();
                TextBox8.Text = dt33.Rows[0]["amt3"].ToString();
                TextBox9.Text = dt33.Rows[0]["amt4"].ToString();
                TextBox10.Text = dt33.Rows[0]["amt5"].ToString();
                TextBox11.Text = dt33.Rows[0]["amt6"].ToString();

                Label42.Text = DropDownList4.SelectedItem.Text;
                Label43.Text = DropDownList4.SelectedItem.Text;
                Label44.Text = DropDownList4.SelectedItem.Text;
                Label45.Text = DropDownList4.SelectedItem.Text;
                Label46.Text = DropDownList4.SelectedItem.Text;
                Label47.Text = DropDownList4.SelectedItem.Text;

                CheckBox2.Checked = Convert.ToBoolean(dt33.Rows[0]["Unspecified"]);
            }

            DataTable dt44 = select("select * from candidatelocation3 where candidateid='" + Session["CandidateID"] + "'");

            if (dt44.Rows.Count > 0)
            {
                ddlcity.SelectedValue = dt44.Rows[0]["cityid"].ToString();

                if (dt44.Rows[0]["areaid"].ToString() == "False")
                {
                    RadioButtonList2.SelectedValue = "0";
                }
                else
                {
                    RadioButtonList2.SelectedValue = "1";
                }

                RadioButtonList2_SelectedIndexChanged(sender, e);

                TextBox3.Text = dt44.Rows[0]["area"].ToString();
            }

            string s11 = "";
            string s22 = "";
            string s33 = "";
            string s44 = "";
            string s55 = "";
            string s66 = "";

            DataTable dt55 = select("select * from candidateavailable where candidateid='" + Session["CandidateID"] + "'");

            if (dt55.Rows.Count > 0)
            {
                if (dt55.Rows[0]["dayss"].ToString() == "False")
                {
                    RadioButtonList1.SelectedValue = "0";
                    RadioButtonList1_SelectedIndexChanged(sender, e);
                }
                else
                {
                    RadioButtonList1.SelectedValue = "1";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    for (int ii = 0; ii < dt55.Rows.Count; ii++)
                    {
                        if (Convert.ToString(dt55.Rows[ii]["alldays"]) != "")
                        {
                            if (dt55.Rows[ii]["alldays"].ToString() == "1")
                            {
                                s11 = "1";
                            }
                            if (dt55.Rows[ii]["alldays"].ToString() == "2")
                            {
                                s22 = "2";
                            }
                            if (dt55.Rows[ii]["alldays"].ToString() == "3")
                            {
                                s33 = "3";
                            }
                            if (dt55.Rows[ii]["alldays"].ToString() == "4")
                            {
                                s44 = "4";
                            }
                            if (dt55.Rows[ii]["alldays"].ToString() == "5")
                            {
                                s55 = "5";
                            }
                            if (dt55.Rows[ii]["alldays"].ToString() == "6")
                            {
                                s66 = "6";
                            }
                        }
                    }
                }
            }

            //CheckBox chkMsg11 = (CheckBox)DataList1.FindControl("chkMsg11");

            foreach (DataListItem dtg in DataList1.Items)
            {
                CheckBox chkMsg11 = (CheckBox)dtg.FindControl("chkMsg11");
                Label Label2 = (Label)dtg.FindControl("Label2");

                if (s11 != "")
                {
                    if (s11 == Label2.Text)
                    {
                        chkMsg11.Checked = true;
                    }
                }
                if (s22 != "")
                {
                    if (s22 == Label2.Text)
                    {
                        chkMsg11.Checked = true;
                    }
                }
                if (s33 != "")
                {
                    if (s33 == Label2.Text)
                    {
                        chkMsg11.Checked = true;
                    }
                }
                if (s44 != "")
                {
                    if (s44 == Label2.Text)
                    {
                        chkMsg11.Checked = true;
                    }
                }
                if (s55 != "")
                {
                    if (s55 == Label2.Text)
                    {
                        chkMsg11.Checked = true;
                    }
                }
                if (s66 != "")
                {
                    if (s66 == Label2.Text)
                    {
                        chkMsg11.Checked = true;
                    }
                }
            }

            DataTable dt66 = select("select * from candidatetimings where candidateid='" + Session["CandidateID"] + "'");

            if (dt66.Rows.Count > 0)
            {
                TextBox1.Text = dt66.Rows[0]["fromt"].ToString();
                TextBox2.Text = dt66.Rows[0]["tot"].ToString();

                chkMsg11.Checked = true;

                panelimpnotes.Visible = true;

                txtdescription.Text = dt66.Rows[0]["notes"].ToString();

                if (dt66.Rows[0]["fromtAMPM"].ToString() == "False")
                {
                    DropDownList1.SelectedValue = "0";
                }
                else
                {
                    DropDownList1.SelectedValue = "1";
                }

                if (dt66.Rows[0]["totAMPM"].ToString() == "False")
                {
                    DropDownList2.SelectedValue = "0";
                }
                else
                {
                    DropDownList2.SelectedValue = "1";
                }

                //DropDownList2.SelectedValue = dt66.Rows[0]["totAMPM"].ToString();

                if (Convert.ToString(dt66.Rows[0]["timezone"]) != "")
                {
                    ddltimezone.SelectedValue = dt66.Rows[0]["timezone"].ToString();
                }
            }
        }
        if (RadioButtonList3.SelectedValue == "1")
        {
            clear();
        }
    }
    protected void CheckBox6_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox6.Checked == true)
        {
            Panel4.Visible = true;
        }
        else
        {
            Panel4.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dttimings = select("select * from candidatetimings where candidateid='" + Session["CandidateID"] + "'");

        if (dttimings.Rows.Count > 0)
        {
            string st1 = "update candidatetimings set fromt='" + TextBox1.Text + "',fromtAMPM='" + DropDownList1.SelectedValue + "',tot='" + TextBox2.Text + "',totAMPM='" + DropDownList2.SelectedValue + "',notes='" + txtdescription.Text + "',datetime='" + DateTime.Now.ToString() + "',timezone='" + ddltimezone.SelectedValue + "' where candidateid='" + Session["CandidateID"].ToString() + "'";

            SqlCommand cmd1 = new SqlCommand(st1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            string st1 = "insert into candidatetimings values('" + Session["CandidateID"].ToString() + "','" + TextBox1.Text + "','" + DropDownList1.SelectedValue + "','" + TextBox2.Text + "','" + DropDownList2.SelectedValue + "','" + txtdescription.Text + "','" + DateTime.Now.ToString() + "','" + ddltimezone.SelectedValue + "')";

            SqlCommand cmd1 = new SqlCommand(st1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();
        }

        DataTable dtavailable = select("select * from candidateavailable where candidateid='" + Session["CandidateID"].ToString() + "'");

        if (dtavailable.Rows.Count > 0)
        {
            SqlCommand cmddel = new SqlCommand("delete from candidateavailable where candidateid='" + Session["CandidateID"].ToString() + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmddel.ExecuteNonQuery();
            con.Close();

            if (RadioButtonList1.SelectedValue == "0")
            {
                string st2 = "insert into candidateavailable(candidateid,dayss,datetime) values('" + Session["CandidateID"].ToString() + "','" + RadioButtonList1.SelectedValue + "','" + DateTime.Now.ToString() + "')";

                SqlCommand cmd2 = new SqlCommand(st2, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd2.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                foreach (DataListItem ddl in DataList1.Items)
                {
                    CheckBox chkMsg11 = (CheckBox)ddl.FindControl("chkMsg11");
                    Label Label2 = (Label)ddl.FindControl("Label2");

                    if (chkMsg11.Checked == true)
                    {
                        string st2 = "insert into candidateavailable(candidateid,dayss,alldays,datetime) values('" + Session["CandidateID"].ToString() + "','" + RadioButtonList1.SelectedValue + "','" + Label2.Text + "','" + DateTime.Now.ToString() + "')";

                        SqlCommand cmd2 = new SqlCommand(st2, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }
        else
        {
            if (RadioButtonList1.SelectedValue == "0")
            {
                string st2 = "insert into candidateavailable(candidateid,dayss,datetime) values('" + Session["CandidateID"].ToString() + "','" + RadioButtonList1.SelectedValue + "','" + DateTime.Now.ToString() + "')";

                SqlCommand cmd2 = new SqlCommand(st2, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd2.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                foreach (DataListItem ddl in DataList1.Items)
                {
                    CheckBox chkMsg11 = (CheckBox)ddl.FindControl("chkMsg11");
                    Label Label2 = (Label)ddl.FindControl("Label2");

                    if (chkMsg11.Checked == true)
                    {
                        string st2 = "insert into candidateavailable(candidateid,dayss,alldays,datetime) values('" + Session["CandidateID"].ToString() + "','" + RadioButtonList1.SelectedValue + "','" + Label2.Text + "','" + DateTime.Now.ToString() + "')";

                        SqlCommand cmd2 = new SqlCommand(st2, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }

        DataTable dtlocation3 = select("select * from candidatelocation3 where candidateid='" + Session["CandidateID"] + "'");

        if (dtlocation3.Rows.Count > 0)
        {
            string st3 = "update candidatelocation3 set cityid='" + ddlcity.SelectedValue + "',areaid='" + RadioButtonList2.SelectedValue + "',area='" + TextBox3.Text + "',datetime='" + DateTime.Now.ToString() + "' where candidateid='" + Session["CandidateID"].ToString() + "'";

            SqlCommand cmd3 = new SqlCommand(st3, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd3.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            string st3 = "insert into candidatelocation3 values('" + Session["CandidateID"].ToString() + "','" + ddlcity.SelectedValue + "','" + RadioButtonList2.SelectedValue + "','" + TextBox3.Text + "','" + DateTime.Now.ToString() + "')";

            SqlCommand cmd3 = new SqlCommand(st3, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd3.ExecuteNonQuery();
            con.Close();
        }

        string remHour = "";
        string remDay = "";
        string remWeek = "";
        string remMonth = "";
        string remYear = "";
        string remProject = "";

        if (TextBox6.Text == "")
        {
            remHour = "0";
        }
        else
        {
            remHour = "1";
        }
        if (TextBox7.Text == "")
        {
            remDay = "0";
        }
        else
        {
            remDay = "1";
        }
        if (TextBox8.Text == "")
        {
            remWeek = "0";
        }
        else
        {
            remWeek = "1";
        }
        if (TextBox9.Text == "")
        {
            remMonth = "0";
        }
        else
        {
            remMonth = "1";
        }
        if (TextBox10.Text == "")
        {
            remYear = "0";
        }
        else
        {
            remYear = "1";
        }
        if (TextBox11.Text == "")
        {
            remProject = "0";
        }
        else
        {
            remProject = "1";
        }

        DataTable dtremuneration = select("select * from candidateremuneration2 where candidateid='" + Session["CandidateID"] + "'");

        if (dtremuneration.Rows.Count > 0)
        {
            SqlCommand cmdremun = new SqlCommand("update candidateremuneration2 set amt1='" + TextBox6.Text + "',Hour='" + remHour + "',amt2='" + TextBox7.Text + "',Day='" + remDay + "',amt3='" + TextBox8.Text + "',Week='" + remWeek + "',amt4='" + TextBox9.Text + "',Month='" + remMonth + "',amt5='" + TextBox10.Text + "',Year='" + remYear + "',amt6='" + TextBox11.Text + "',Project='" + remProject + "',currencyid='" + DropDownList4.SelectedValue + "',Unspecified='" + CheckBox2.Checked + "',datetime='" + DateTime.Now.ToString() + "' where candidateid='" + Session["CandidateID"].ToString() + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdremun.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            SqlCommand cmdremun = new SqlCommand("insert into candidateremuneration2 values('" + Session["CandidateID"].ToString() + "','" + TextBox6.Text + "','" + remHour + "','" + TextBox7.Text + "','" + remDay + "','" + TextBox8.Text + "','" + remWeek + "','" + TextBox9.Text + "','" + remMonth + "','" + TextBox10.Text + "','" + remYear + "','" + TextBox11.Text + "','" + remProject + "','" + DropDownList4.SelectedValue + "','" + CheckBox2.Checked + "','" + DateTime.Now.ToString() + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdremun.ExecuteNonQuery();
            con.Close();
        }

        DataTable dtjobtitle2 = select("select * from CandidateJobTitles2 where candidateid='" + Session["CandidateID"].ToString() + "'");

        if (dtjobtitle2.Rows.Count > 0)
        {
            SqlCommand cmddel = new SqlCommand("delete from CandidateJobTitles2 where candidateid='" + Session["CandidateID"].ToString() + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmddel.ExecuteNonQuery();
            con.Close();

            foreach (GridViewRow gr5 in GridView5.Rows)
            {
                Label Label511 = (Label)gr5.FindControl("Label511");
                Label Label521 = (Label)gr5.FindControl("Label521");

                SqlCommand cmdgr5 = new SqlCommand("insert into CandidateJobTitles2(candidateid,typeid,titleid,datetime) values('" + Session["CandidateID"].ToString() + "','" + Label511.Text + "','" + Label521.Text + "','" + DateTime.Now.ToString() + "')", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdgr5.ExecuteNonQuery();
                con.Close();
            }
        }
        else
        {
            foreach (GridViewRow gr5 in GridView5.Rows)
            {
                Label Label511 = (Label)gr5.FindControl("Label511");
                Label Label521 = (Label)gr5.FindControl("Label521");

                SqlCommand cmdgr5 = new SqlCommand("insert into CandidateJobTitles2(candidateid,typeid,titleid,datetime) values('" + Session["CandidateID"].ToString() + "','" + Label511.Text + "','" + Label521.Text + "','" + DateTime.Now.ToString() + "')", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdgr5.ExecuteNonQuery();
                con.Close();
            }
        }

        DataTable dtmyinfo = select("select * from candidatemyinfo where candidateid='" + Session["CandidateID"] + "'");

        if (dtmyinfo.Rows.Count > 0)
        {
            SqlCommand cmdmyinfo = new SqlCommand("update candidatemyinfo set sms='" + CheckBox4.Checked + "',email='" + CheckBox3.Checked + "',phone='" + CheckBox1.Checked + "',datetime='" + DateTime.Now.ToString() + "' where candidateid='" + Session["CandidateID"].ToString() + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdmyinfo.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            SqlCommand cmdmyinfo = new SqlCommand("insert into candidatemyinfo values('" + Session["CandidateID"].ToString() + "','" + CheckBox4.Checked + "','" + CheckBox3.Checked + "','" + CheckBox1.Checked + "','" + DateTime.Now.ToString() + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdmyinfo.ExecuteNonQuery();
            con.Close();
        }

        clear();

        Panel6.Visible = false;

        CheckBox6_CheckedChanged(sender, e);

        DataTable dtaaigyo = select("select * from candidatetimings where candidateid='" + Session["CandidateID"] + "'");

        if (dtaaigyo.Rows.Count > 0)
        {

            RadioButtonList3.Items[0].Enabled = true;
        }
        else
        {

            RadioButtonList3.Items[0].Enabled = false;
        }

        lblmsg.Text = "Record inserted successfully.";
    }
    protected void chkMsg11_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMsg11.Checked == true)
        {
            panelimpnotes.Visible = true;
        }
        else
        {
            panelimpnotes.Visible = false;
        }
    }
}
