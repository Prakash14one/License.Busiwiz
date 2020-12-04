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
public partial class ShoppingCart_Admin_DesignationRequirment : System.Web.UI.Page
{

    SqlConnection con;
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


        Page.Title = pg.getPageTitle(page);


        statuslable.Visible = false;
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                Pagecontrol.dypcontrol(Page, page);

                Page.Form.Attributes.Add("enctype", "multipart/form-data");

                ViewState["sortOrder"] = "";

                fillstore();
                fillareaofstudy();
                fillpasinggre();

                filleddeg();

                ddlwarehouse_SelectedIndexChanged(sender, e);


            }
        }
    }

    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
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
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;

    }



    protected void btngo_Click(object sender, EventArgs e)
    {
        statuslable.Text = "";
    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        statuslable.Text = "";
        FillDept();
        fillspecialisesub();
        fillskilltype();
        ddldesi_SelectedIndexChanged(sender, e);

    }
    protected void enableview(Boolean t)
    {
        //ddlwarehouse.Enabled = t;
        //ddldesi.Enabled = t;

        ddlsex.Enabled = t;
        btnaddnew.Enabled = t;
        Gridreqinfo.Enabled = t;
        txtexpreq.Enabled = t;
        txtfromage.Enabled = t;
        txtothenote.Enabled = t;
        txttoage.Enabled = t;
        chkyes.Enabled = t;
        datalistskilltype.Enabled = t;
        GridView1.Enabled = t;
        btnadd.Enabled = t;
    }
    protected void fillstore()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {
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
    }

    protected void FillDept()
    {


        DataTable ds = select("Select DesignationMaster.DesignationMasterId,DepartmentmasterMNC.Departmentname+':'+DesignationMaster.DesignationName as desname from DesignationMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=DesignationMaster.DeptId where DepartmentmasterMNC.Whid='" + ddlwarehouse.SelectedValue + "' order by desname");
        if (ds.Rows.Count > 0)
        {
            ddldesi.DataSource = ds;
            ddldesi.DataTextField = "desname";
            ddldesi.DataValueField = "DesignationMasterId";
            ddldesi.DataBind();
        }


    }
    protected void fillareaofstudy()
    {
        DataTable ds = select("Select Name,Id  from AreaofStudiesTbl where Active='1' order by Name");
        if (ds.Rows.Count > 0)
        {
            ddlareaofstudy.DataSource = ds;
            ddlareaofstudy.DataTextField = "Name";
            ddlareaofstudy.DataValueField = "Id";
            ddlareaofstudy.DataBind();
        }
    }
    protected void fillpasinggre()
    {
        DataTable ds = select("Select Name,Id  from [PassingGrade] where Active='1' order by Name");
        if (ds.Rows.Count > 0)
        {
            ddlpassinggrade.DataSource = ds;
            ddlpassinggrade.DataTextField = "Name";
            ddlpassinggrade.DataValueField = "Id";
            ddlpassinggrade.DataBind();
        }
        ddlpassinggrade.Items.Insert(0, "Any");
        ddlpassinggrade.Items[0].Value = "0";
    }
    protected void ddlareaofstudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        filleddeg();
        fillspecialisesub();
    }
    protected void filleddeg()
    {
        ddledudeg.Items.Clear();
        DataTable ds = select("Select EducationDegrees.Id,EducationDegrees.DegreeName from EducationDegrees where AreaofStudyID='" + ddlareaofstudy.SelectedValue + "' and EducationDegrees.Active='1' order by DegreeName");
        if (ds.Rows.Count > 0)
        {
            ddledudeg.DataSource = ds;
            ddledudeg.DataTextField = "DegreeName";
            ddledudeg.DataValueField = "Id";
            ddledudeg.DataBind();
        }
        ddledudeg.Items.Insert(0, "Any");
        ddledudeg.Items[0].Value = "0";


    }
    protected void fillspecialisesub()
    {
        ddlspecialisub.Items.Clear();
        DataTable ds = select("Select Id,SubjectName from [SpecialisedSubjectTBL]  where AreaofStudiesId='" + ddlareaofstudy.SelectedValue + "' and compid='" + Session["Comid"] + "' and Status='1' order by SubjectName");
        if (ds.Rows.Count > 0)
        {
            ddlspecialisub.DataSource = ds;
            ddlspecialisub.DataTextField = "SubjectName";
            ddlspecialisub.DataValueField = "Id";
            ddlspecialisub.DataBind();
        }
        ddlspecialisub.Items.Insert(0, "Any");
        ddlspecialisub.Items[0].Value = "0";
    }
    protected void fillskilltype()
    {
        datalistskilltype.DataSource = null;
        datalistskilltype.DataBind();
        DataTable ds = select("Select Id,Name from [SkillType]  where [BusinessID]='" + ddlwarehouse.SelectedValue + "' order by Name");
        if (ds.Rows.Count > 0)
        {
            datalistskilltype.DataSource = ds;
            datalistskilltype.DataBind();
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (btnupdate.Visible == true)
        {

        }
        else
        {
            btnsubmit.Visible = true;
        }
        //Label1.Visible = true;

        string all = "";
        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["data"]) == "")
        {
            dt = CreateDatatable();
        }
        else
        {
            dt = (DataTable)ViewState["data"];
        }
        Label lblareaId = new Label();
        foreach (GridViewRow sdr in Gridreqinfo.Rows)
        {
            lblareaId = (Label)sdr.FindControl("lblareaId");
            if (Convert.ToString(lblareaId.Text) == ddlareaofstudy.SelectedValue.ToString())
            {
                all = "ab";
                statuslable.Text = "Record already exist";
                statuslable.Visible = true;
                break;
            }

        }
        if (all != "ab")
        {
            DataRow Drow = dt.NewRow();
            Drow["AreaStudy"] = ddlareaofstudy.SelectedItem.Text;
            Drow["edudegree"] = ddledudeg.SelectedItem.Text;
            Drow["passGrade"] = ddlpassinggrade.SelectedItem.Text;

            Drow["spsubject"] = ddlspecialisub.SelectedItem.Text;
            Drow["Whid"] = ddlwarehouse.SelectedValue;
            Drow["areaid"] = ddlareaofstudy.SelectedValue;

            Drow["edudegreeid"] = ddledudeg.SelectedValue;
            Drow["passgradeid"] = ddlpassinggrade.SelectedValue;
            Drow["spsubid"] = ddlspecialisub.SelectedValue;
            Drow["Id"] = "0";
            all = "";
            dt.Rows.Add(Drow);

            ViewState["data"] = dt;
            Gridreqinfo.DataSource = dt;
            Gridreqinfo.DataBind();
            pnledu.Visible = false;

            btnaddnew.Visible = true;
        }
    }
    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "AreaStudy";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "edudegree";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "passGrade";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "spsubject";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "Whid";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;


        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "areaid";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;


        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "edudegreeid";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;

        DataColumn Dcom7 = new DataColumn();
        Dcom7.DataType = System.Type.GetType("System.String");
        Dcom7.ColumnName = "passgradeid";
        Dcom7.AllowDBNull = true;
        Dcom7.Unique = false;
        Dcom7.ReadOnly = false;

        DataColumn Dcom8 = new DataColumn();
        Dcom8.DataType = System.Type.GetType("System.String");
        Dcom8.ColumnName = "spsubid";
        Dcom8.AllowDBNull = true;
        Dcom8.Unique = false;
        Dcom8.ReadOnly = false;


        DataColumn Dcom9 = new DataColumn();
        Dcom9.DataType = System.Type.GetType("System.String");
        Dcom9.ColumnName = "Id";
        Dcom9.AllowDBNull = true;
        Dcom9.Unique = false;
        Dcom9.ReadOnly = false;


        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);
        dt.Columns.Add(Dcom7);
        dt.Columns.Add(Dcom8);
        dt.Columns.Add(Dcom9);
        return dt;
    }

    public DataTable itemdy()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "ID";
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

    protected void Gridreqinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            Gridreqinfo.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DeleteFromGrid(Convert.ToInt32(Gridreqinfo.SelectedIndex.ToString()));

        }
    }
    protected void DeleteFromGrid(int rowindex)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["data"];
        dt.Rows[rowindex].Delete();
        dt.AcceptChanges();
        Gridreqinfo.DataSource = dt;
        Gridreqinfo.DataBind();
        ViewState["data"] = dt;

        statuslable.Text = "Record Deleted successfully.";
        statuslable.Visible = true;
    }
    protected void chkyes_CheckedChanged(object sender, EventArgs e)
    {
        if (chkyes.Checked == true)
        {
            btnsubmit.Visible = true;
            fillskilltype();
            lblis.Visible = true;
            pnljobid.Visible = true;
            GridView1.Visible = true;
        }
        else
        {
            lblis.Visible = false;
            pnljobid.Visible = false;
            GridView1.Visible = false;
            datalistskilltype.DataSource = null;
            datalistskilltype.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        String str = "insert into DesignationQualificationRequiredMasterTbl([BusinessId],[DesignationId],[ExperienceYrs],[Othenotes] ,[Preferredsex],[Preferredagemin] ,[Preferredagemax])values" +
           " ('" + ddlwarehouse.SelectedValue + "','" + ddldesi.SelectedValue + "','" + txtexpreq.Text + "','" + txtothenote.Text + "','" + ddlsex.SelectedValue + "','" + txtfromage.Text + "','" + txttoage.Text + "')";
        if (con.State.ToString() != "Open")
        {
            con.Open();

        }
        SqlCommand cmd = new SqlCommand(str, con);
        cmd.ExecuteNonQuery();
        con.Close();
        DataTable ds = select("Select Max(Id) as Id from DesignationQualificationRequiredMasterTbl  where DesignationId='" + ddldesi.SelectedValue + "'");
        if (ds.Rows.Count > 0)
        {
            foreach (GridViewRow drs in Gridreqinfo.Rows)
            {
                string i = Gridreqinfo.DataKeys[drs.RowIndex].Value.ToString();

                Label lblwhid = (Label)drs.FindControl("lblwhid");
                Label lblareaId = (Label)drs.FindControl("lblareaId");
                Label lbledudegreeId = (Label)drs.FindControl("lbledudegreeId");
                Label lblpassgradeid = (Label)drs.FindControl("lblpassgradeid");
                Label lblspsubid = (Label)drs.FindControl("lblspsubid");

                String str1 = "insert into [DesignationQualificationsRequiredDetailTbl]([DesignationQualificationsrequiredmastertblId],[AreaofstudyId],[EducationDegreeId],[SpecialisationsubjectId] ,[PassinggreadId])values" +
            " ('" + ds.Rows[0]["Id"] + "','" + lblareaId.Text + "','" + lbledudegreeId.Text + "','" + lblspsubid.Text + "','" + lblpassgradeid.Text + "')";
                if (con.State.ToString() != "Open")
                {
                    con.Open();

                }
                SqlCommand cmd1 = new SqlCommand(str1, con);
                cmd1.ExecuteNonQuery();
                con.Close();
            }

            foreach (GridViewRow drs in GridView1.Rows)
            {
                string i = GridView1.DataKeys[drs.RowIndex].Value.ToString();

                DataList datatlistskillname = (DataList)drs.FindControl("datatlistskillname");
                foreach (DataListItem dti in datatlistskillname.Items)
                {
                    string skillId = datatlistskillname.DataKeys[dti.ItemIndex].ToString();

                    CheckBox chkprocess = (CheckBox)dti.FindControl("chksp");
                    if (chkprocess.Checked == true)
                    {
                        String str1 = "insert into DesignationQualificationskillsTbl(DesignationQualificationsrequiredmastertblId,[SkillnameId])values" +
                    " ('" + ds.Rows[0]["Id"] + "','" + skillId + "')";
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();

                        }
                        SqlCommand cmd1 = new SqlCommand(str1, con);
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }

                }
            }
        }
        statuslable.Text = "Record inserted successfully";
        statuslable.Visible = true;
        enableview(false);
        ddldesi_SelectedIndexChanged(sender, e);
    }


    protected void chksp_CheckedChanged(object sender, EventArgs e)
    {
        int indexer = 0;
        // CheckBox chkprocess = (CheckBox)sender;
        // DataListItem items = (DataListItem)chkprocess.NamingContainer;
        //string i = datalistskilltype.DataKeys[items.ItemIndex].ToString();
        ViewState["dy"] = null;
        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["dy"]) == "")
        {
            dt = itemdy();
        }
        else
        {
            dt = (DataTable)ViewState["dy"];
        }


        foreach (DataListItem dti in datalistskilltype.Items)
        {
            string i = datalistskilltype.DataKeys[indexer].ToString();
            //  string i = datalistskilltype.DataKeys[dti.ItemIndex].ToString();
            CheckBox chkprocess = (CheckBox)datalistskilltype.Items[indexer].FindControl("chksp");
            indexer += 1;
            // DataList datatlistskillname = (DataList)datalistskilltype.Items[dti.ItemIndex].FindControl("datatlistskillname");
            if (chkprocess.Checked == true)
            {

                DataTable dtp = select("Select Id,Name  from SkillType where Id='" + Convert.ToInt32(i) + "'");

                if (dtp.Rows.Count > 0)
                {


                    DataRow Drow = dt.NewRow();
                    Drow["Id"] = Convert.ToString(dtp.Rows[0]["Id"]);
                    Drow["Name"] = Convert.ToString(dtp.Rows[0]["Name"]);
                    //Drow["passGrade"] = ddlpassinggrade.SelectedItem.Text;

                    //Drow["spsubject"] = ddlspecialisub.SelectedItem.Text;


                    dt.Rows.Add(Drow);

                    ViewState["dy"] = dt;

                }
            }
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();
        foreach (GridViewRow grd in GridView1.Rows)
        {
            string i = GridView1.DataKeys[grd.RowIndex].Value.ToString();

            DataList datatlistskillname = (DataList)grd.FindControl("datatlistskillname");
            DataTable dtpr = select("Select Id AS SkillId,SkillName  from SkillName where [SkillTypeID]='" + Convert.ToInt32(i) + "'");
            if (dtpr.Rows.Count > 0)
            {
                datatlistskillname.DataSource = dtpr;
                datatlistskillname.DataBind();
                datatlistskillname.Enabled = true;
            }
        }    
    }

    protected void ddldesi_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["data"] = null;
        DataTable ds = select("Select Distinct DesignationQualificationRequiredMasterTbl.* from DesignationQualificationRequiredMasterTbl where DesignationId='" + ddldesi.SelectedValue + "' and [BusinessId]='" + ddlwarehouse.SelectedValue + "'");
        if (ds.Rows.Count > 0)
        {
            ViewState["did"] = Convert.ToString(ds.Rows[0]["Id"]);
            ddlsex.SelectedIndex = ddlsex.Items.IndexOf(ddlsex.Items.FindByValue(Convert.ToString(ds.Rows[0]["Preferredsex"])));
            txttoage.Text = Convert.ToString(ds.Rows[0]["Preferredagemax"]);
            txtfromage.Text = Convert.ToString(ds.Rows[0]["Preferredagemin"]);
            txtothenote.Text = Convert.ToString(ds.Rows[0]["Othenotes"]);
            txtexpreq.Text = Convert.ToString(ds.Rows[0]["ExperienceYrs"]);
            //ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(Convert.ToString(ds.Rows[0]["BusinessId"])));
            //ddlwarehouse_SelectedIndexChanged(sender, e);
            DataTable dsqu = select("Select Distinct  AreaofStudiesTbl.Name,DesignationQualificationsRequiredDetailTbl.Id, [DesignationQualificationsRequiredDetailTbl].AreaofstudyId,DesignationQualificationsRequiredDetailTbl.[EducationDegreeId],DesignationQualificationsRequiredDetailTbl.[SpecialisationsubjectId],DesignationQualificationsRequiredDetailTbl.[PassinggreadId]," +
                "Case When(DegreeName IS NULL) then 'Any' else DegreeName End as edudegree ,Case When(PassingGrade.Name IS NULL) then 'Any' else PassingGrade.Name End as passGrade,Case When(SubjectName IS NULL) then 'Any' else SubjectName End as spsubject from DesignationQualificationsRequiredDetailTbl inner join AreaofStudiesTbl on AreaofStudiesTbl.Id=DesignationQualificationsRequiredDetailTbl.AreaofstudyId Left join " +
           "[EducationDegrees] on [EducationDegrees].Id =DesignationQualificationsRequiredDetailTbl.EducationDegreeId left join PassingGrade on PassingGrade.Id=DesignationQualificationsRequiredDetailTbl.PassinggreadId left join SpecialisedSubjectTBL on SpecialisedSubjectTBL.Id=DesignationQualificationsRequiredDetailTbl.SpecialisationsubjectId " +
            " where DesignationQualificationsrequiredmastertblId='" + ds.Rows[0]["Id"] + "'");
            DataTable dt = new DataTable();
            if (Convert.ToString(ViewState["data"]) == "")
            {
                dt = CreateDatatable();
            }
            else
            {
                dt = (DataTable)ViewState["data"];
            }
            foreach (DataRow drs in dsqu.Rows)
            {


                DataRow Drow = dt.NewRow();
                Drow["AreaStudy"] = Convert.ToString(drs["Name"]);
                Drow["edudegree"] = Convert.ToString(drs["edudegree"]);
                Drow["passGrade"] = Convert.ToString(drs["passGrade"]);

                Drow["spsubject"] = Convert.ToString(drs["spsubject"]);
                Drow["Whid"] = ddlwarehouse.SelectedValue;
                Drow["areaid"] = Convert.ToString(drs["AreaofstudyId"]);

                Drow["edudegreeid"] = Convert.ToString(drs["EducationDegreeId"]);
                Drow["passgradeid"] = Convert.ToString(drs["PassinggreadId"]);
                Drow["spsubid"] = Convert.ToString(drs["SpecialisationsubjectId"]);
                Drow["Id"] = Convert.ToString(drs["Id"]);

                dt.Rows.Add(Drow);

                ViewState["data"] = dt;
                Gridreqinfo.DataSource = dt;
                Gridreqinfo.DataBind();
                //ddlareaofstudy.SelectedIndex = ddlareaofstudy.Items.IndexOf(ddlareaofstudy.Items.FindByValue(Convert.ToString(ds.Rows[0]["AreaofstudyId"])));
                //ddlareaofstudy_SelectedIndexChanged(sender, e);
                //ddledudeg.SelectedIndex = ddledudeg.Items.IndexOf(ddledudeg.Items.FindByValue(Convert.ToString(ds.Rows[0]["EducationDegreeId"])));
                //ddlpassinggrade.SelectedIndex = ddlpassinggrade.Items.IndexOf(ddlpassinggrade.Items.FindByValue(Convert.ToString(ds.Rows[0]["PassinggreadId"])));

                //ddlspecialisub.SelectedIndex = ddlspecialisub.Items.IndexOf(ddlspecialisub.Items.FindByValue(Convert.ToString(ds.Rows[0]["SpecialisationsubjectId"])));
                //ddlsex.SelectedIndex = ddlsex.Items.IndexOf(ddlsex.Items.FindByValue(Convert.ToString(ds.Rows[0]["Preferredsex"])));
            }


            DataTable dsqusk = select("Select Distinct  SkillType.Id from  DesignationQualificationskillsTbl inner join SkillName on SkillName.Id=DesignationQualificationskillsTbl.SkillnameId inner join [SkillType] on [SkillType].Id=[SkillName].[SkillTypeID]  where DesignationQualificationsrequiredmastertblId='" + ds.Rows[0]["Id"] + "'");
            if (dsqusk.Rows.Count > 0)
            {
                chkyes.Checked = true;
                chkyes_CheckedChanged(sender, e);
                foreach (DataRow drs in dsqusk.Rows)
                {
                    foreach (DataListItem dti in datalistskilltype.Items)
                    {
                        string i = datalistskilltype.DataKeys[dti.ItemIndex].ToString();

                        CheckBox chkprocess = (CheckBox)dti.FindControl("chksp");
                        if (Convert.ToString(drs["Id"]) == i)
                        {
                            chkprocess.Checked = true;
                        }

                    }


                }
                chksp_CheckedChanged(sender, e);
            }
            else
            {
                pnljobid.Visible = true;
                fillskilltype();
            }
            DataTable dsqusk1 = select("Select Distinct DesignationQualificationskillsTbl.Id as did,  SkillName.Id as SkillId,SkillType.Id from  DesignationQualificationskillsTbl inner join SkillName on SkillName.Id=DesignationQualificationskillsTbl.SkillnameId inner join [SkillType] on [SkillType].Id=[SkillName].[SkillTypeID]  where DesignationQualificationsrequiredmastertblId='" + ds.Rows[0]["Id"] + "'");
            if (dsqusk1.Rows.Count > 0)
            {
                //chkyes.Checked = true;
                //chkyes_CheckedChanged(sender, e);
                foreach (DataRow drs in dsqusk1.Rows)
                {
                    foreach (GridViewRow dti in GridView1.Rows)
                    {
                        string i = GridView1.DataKeys[dti.RowIndex].Value.ToString();

                        if (Convert.ToString(drs["Id"]) == i)
                        {
                            DataList datatlistskillname = (DataList)dti.FindControl("datatlistskillname");
                            foreach (DataListItem dee in datatlistskillname.Items)
                            {
                                string key = datatlistskillname.DataKeys[dee.ItemIndex].ToString();
                                Label lblid = (Label)dee.FindControl("lblid");
                                CheckBox chkprocess = (CheckBox)dee.FindControl("chksp");
                                if (Convert.ToString(drs["SkillId"]) == key)
                                {
                                    lblid.Text = (Convert.ToString(drs["did"]));
                                    chkprocess.Checked = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }           
            enableview(false);
            btnsubmit.Visible = false;
            btnedit.Visible = true;
            btnupdate.Visible = false;
            //CheckBox1.Checked = true;
            CheckBox1.Enabled = false;
            //CheckBox1_CheckedChanged(sender, e);
            //CheckBox2.Checked = true;
            CheckBox2.Enabled = false;
            //CheckBox2_CheckedChanged(sender, e);
            //CheckBox3.Checked = true;
            CheckBox3.Enabled = false;
            //CheckBox3_CheckedChanged(sender, e);
            panelexpr.Visible = true;
            panelother.Visible = true;
            paneleduca.Visible = true;
            CheckBox1.Visible = false;
            CheckBox2.Visible = false;
            CheckBox3.Visible = false;
            chkyes.Visible = false;
            lblis.Visible = true;
        }
        else
        {
            CheckBox1.Visible = true;
            CheckBox2.Visible = true;
            CheckBox3.Visible = true;
            CheckBox3.Checked = true;
            CheckBox3_CheckedChanged(sender, e);
            chkyes.Visible = true;
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            panelexpr.Visible = false;
            panelother.Visible = false;
            CheckBox1.Enabled = true;
            CheckBox2.Enabled = true;
            CheckBox3.Enabled = true;
            enableview(true);
            btnsubmit.Visible = false;
            btnedit.Visible = false;
            btnupdate.Visible = false;
            Gridreqinfo.DataSource = null;
            Gridreqinfo.DataBind();
            datalistskilltype.DataSource = null;
            datalistskilltype.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
            txtexpreq.Text = "";
            txtfromage.Text = "";
            txtothenote.Text = "";
            txttoage.Text = "";
            chkyes.Checked = false;
            chkyes_CheckedChanged(sender, e);
        }

    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        enableview(true);
        btnedit.Visible = false;
        btnupdate.Visible = true;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string updetail = "";
        string updetail1 = "";
        String str = "Update  DesignationQualificationRequiredMasterTbl Set [BusinessId]='" + ddlwarehouse.SelectedValue + "',[DesignationId]='" + ddldesi.SelectedValue + "',[ExperienceYrs]='" + txtexpreq.Text + "',[Othenotes]='" + txtothenote.Text + "' ,[Preferredsex]='" + ddlsex.SelectedValue + "',[Preferredagemin]='" + txtfromage.Text + "' ,[Preferredagemax]='" + txttoage.Text + "' where Id='" + ViewState["did"] + "'";

        if (con.State.ToString() != "Open")
        {
            con.Open();

        }
        SqlCommand cmd = new SqlCommand(str, con);
        cmd.ExecuteNonQuery();
        con.Close();

        foreach (GridViewRow drs in Gridreqinfo.Rows)
        {
            String str1 = "";
            string i = Gridreqinfo.DataKeys[drs.RowIndex].Value.ToString();

            Label lblwhid = (Label)drs.FindControl("lblwhid");
            Label lblareaId = (Label)drs.FindControl("lblareaId");
            Label lbledudegreeId = (Label)drs.FindControl("lbledudegreeId");
            Label lblpassgradeid = (Label)drs.FindControl("lblpassgradeid");
            Label lblspsubid = (Label)drs.FindControl("lblspsubid");
            if (i != "0")
            {
                if (updetail.ToString() != "")
                {
                    updetail = updetail + ",";
                }

                updetail = updetail + i;

                str1 = "Update [DesignationQualificationsRequiredDetailTbl] Set [AreaofstudyId]='" + lblareaId.Text + "',[EducationDegreeId]='" + lbledudegreeId.Text + "',[SpecialisationsubjectId]='" + lblspsubid.Text + "' ,[PassinggreadId]='" + lblpassgradeid.Text + "' where [DesignationQualificationsrequiredmastertblId]='" + ViewState["did"] + "' and Id='" + i + "'";

                if (updetail.ToString() != "")
                {
                    //updetail = updetail.Remove(updetail.Length - 1, 1);
                    string delTrD = "delete DesignationQualificationsRequiredDetailTbl where Id not in(" + updetail + ") and  DesignationQualificationsrequiredmastertblId='" + Convert.ToInt32(ViewState["did"]) + "'";
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();

                    }
                    SqlCommand cmd11 = new SqlCommand(delTrD, con);
                    cmd11.ExecuteNonQuery();
                    con.Close();
                }
            }
            else
            {
                str1 = "insert into [DesignationQualificationsRequiredDetailTbl]([DesignationQualificationsrequiredmastertblId],[AreaofstudyId],[EducationDegreeId],[SpecialisationsubjectId] ,[PassinggreadId])values" +
           " ('" + ViewState["did"] + "','" + lblareaId.Text + "','" + lbledudegreeId.Text + "','" + lblspsubid.Text + "','" + lblpassgradeid.Text + "')";

            }
            if (con.State.ToString() != "Open")
            {
                con.Open();

            }
            SqlCommand cmd1 = new SqlCommand(str1, con);
            cmd1.ExecuteNonQuery();
            con.Close();
        }
       
        foreach (GridViewRow drs in GridView1.Rows)
        {
            string i = GridView1.DataKeys[drs.RowIndex].Value.ToString();

            DataList datatlistskillname = (DataList)drs.FindControl("datatlistskillname");
            foreach (DataListItem dti in datatlistskillname.Items)
            {
                String str1 = "";
                string skillId = datatlistskillname.DataKeys[dti.ItemIndex].ToString();
                string key = ((Label)dti.FindControl("lblid")).Text;
                CheckBox chkprocess = (CheckBox)dti.FindControl("chksp");
                if (key != "")
                {
                    if (updetail1.ToString() != "")
                    {
                        updetail1 = updetail1 + ",";
                    }

                    updetail1 = updetail1 + key;
                }
                if (chkprocess.Checked == true)
                {
                    if (key != "")
                    {
                        str1 = "Update  DesignationQualificationskillsTbl Set [SkillnameId]='" + skillId + "' where  [DesignationQualificationsrequiredmastertblId]='" + ViewState["did"] + "' and Id='" + key + "'";
                        if (updetail1.ToString() != "")
                        {

                            string delTrD = "delete DesignationQualificationskillsTbl where Id not in(" + updetail1 + ") and  DesignationQualificationsrequiredmastertblId='" + Convert.ToInt32(ViewState["did"]) + "'";
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();

                            }
                            SqlCommand cmd11 = new SqlCommand(delTrD, con);
                            cmd11.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else
                    {
                        str1 = "insert into DesignationQualificationskillsTbl(DesignationQualificationsrequiredmastertblId,[SkillnameId])values" +
                     " ('" + ViewState["did"] + "','" + skillId + "')";
                    }
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();

                    }
                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }

            }
        }
       
        statuslable.Text = "Record updated successfully";
        statuslable.Visible = true;

        enableview(false);
        btnedit.Visible = true;
        btnupdate.Visible = false;
    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        pnledu.Visible = true;
        btnaddnew.Visible = false;
        //Label1.Visible = false;
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            btnsubmit.Visible = true;
            panelexpr.Visible = true;
        }
        else
        {
            panelexpr.Visible = false;
        }
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            btnsubmit.Visible = true;
            panelother.Visible = true;
        }
        else
        {
            panelother.Visible = false;
        }
    }
    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox3.Checked == true)
        {
            paneleduca.Visible = true;
        }
        else
        {
            paneleduca.Visible = false;
        }
    }
}


