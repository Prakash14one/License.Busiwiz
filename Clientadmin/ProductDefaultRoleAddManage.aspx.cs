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

public partial class UserRoleforePriceplan : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
   
    SqlConnection conn;
    public SqlConnection connweb;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            FillProduct();
            //ddlProductname_SelectedIndexChanged(sender, e);
            //fillgd();
        }

    }
    protected void FillProduct()
    {        
        //string strcln = " SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' order  by productversion";
        //SqlCommand cmdcln = new SqlCommand(strcln, con);
        //DataTable dtcln = new DataTable();
        //SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        //adpcln.Fill(dtcln);
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM dbo.ClientProductTableMaster INNER JOIN dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductDetail.VersionNo = dbo.VersionInfoMaster.VersionInfoName ON dbo.ClientProductTableMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.ClientProductTableMaster.Databaseid = dbo.ProductCodeDetailTbl.Id where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active =1 order  by productversion ");       
        ddlProductname.DataSource = dtcln;
        ddlProductname.DataValueField = "VersionInfoId";
        ddlProductname.DataTextField = "productversion";
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "-Select-");

    }
  
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button1.Visible = false;
        Button2.Visible = true;
        Button3.Visible = false;
        Panel3.Enabled = false;
        fillgdDept();
    }
    protected void fillgdDept()
    {
        DataTable dtTemp = new DataTable();
        dtTemp = CreateDatatableDept();
        string selepagea1 = " select Distinct DeptId,DeptName from ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId Left Join DefaultDeptIdforPriceplan on DefaultDeptIdforPriceplan.PricePlanId=PricePlanMaster.PricePlanId   inner join DefaultDept  on DefaultDept.DeptId= DefaultDeptIdforPriceplan.DefaultDeptId where PricePlanMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "'  order by DefaultDept.DeptId";
        DataTable dtsel1 = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.DefaultDept.DeptId, dbo.DefaultDept.DeptName FROM dbo.DefaultDept INNER JOIN dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId ON dbo.DefaultDept.VersionId = dbo.VersionInfoMaster.VersionInfoId where VersionInfoId='" + ddlProductname.SelectedValue + "'  order by DefaultDept.DeptId ");
        for (int i = 1; i <= dtsel1.Rows.Count; i++)
        {
            DataRow dtadd = dtTemp.NewRow();
            dtadd["Id"] = i.ToString();
            dtadd["DefaultDeptId"] = Convert.ToString(dtsel1.Rows[i - 1]["DeptId"]);
            dtadd["DefaultDeptName"] = Convert.ToString(dtsel1.Rows[i - 1]["DeptName"]);
            string seld = " select Distinct DefaultDesignationTbl.* from DefaultDesignationTbl inner join DefaultRole on DefaultRole.RoleId=DefaultDesignationTbl.RoleId where  DefaultDesignationTbl.DeptId='" + dtadd["DefaultDeptId"] + "'  order by DefaultDesignationTbl.Id";
            SqlDataAdapter addes = new SqlDataAdapter(seld, con);

            DataTable dtdes = new DataTable();
            addes.Fill(dtdes);
            for (int y = 1; y <= dtdes.Rows.Count; y++)
            {
                if (y == 1)
                {
                    dtadd["DefaultDesitId"] = Convert.ToString(dtdes.Rows[y - 1]["Id"]);
                    dtadd["DefaultDesiName"] = Convert.ToString(dtdes.Rows[y - 1]["DesignationName"]);
                    dtadd["DefaultRoleId"] = Convert.ToString(dtdes.Rows[y - 1]["RoleId"]);
                }
                if (y == 2)
                {
                    dtadd["DefaultDesitId2"] = Convert.ToString(dtdes.Rows[y - 1]["Id"]);
                    dtadd["DefaultDesiName2"] = Convert.ToString(dtdes.Rows[y - 1]["DesignationName"]);
                    dtadd["DefaultRoleId2"] = Convert.ToString(dtdes.Rows[y - 1]["RoleId"]);
                }

                if (y == 3)
                {
                    dtadd["DefaultDesitId3"] = Convert.ToString(dtdes.Rows[y - 1]["Id"]);
                    dtadd["DefaultDesiName3"] = Convert.ToString(dtdes.Rows[y - 1]["DesignationName"]);
                    dtadd["DefaultRoleId3"] = Convert.ToString(dtdes.Rows[y - 1]["RoleId"]);
                }
                if (y == 4)
                {
                    dtadd["DefaultDesitId4"] = Convert.ToString(dtdes.Rows[y - 1]["Id"]);
                    dtadd["DefaultDesiName4"] = Convert.ToString(dtdes.Rows[y - 1]["DesignationName"]);
                    dtadd["DefaultRoleId4"] = Convert.ToString(dtdes.Rows[y - 1]["RoleId"]);
                }
                if (y == 5)
                {
                    dtadd["DefaultDesitId5"] = Convert.ToString(dtdes.Rows[y - 1]["Id"]);
                    dtadd["DefaultDesiName5"] = Convert.ToString(dtdes.Rows[y - 1]["DesignationName"]);
                    dtadd["DefaultRoleId5"] = Convert.ToString(dtdes.Rows[y - 1]["RoleId"]);
                }
                if (y == 6)
                {
                    dtadd["DefaultDesitId6"] = Convert.ToString(dtdes.Rows[y - 1]["Id"]);
                    dtadd["DefaultDesiName6"] = Convert.ToString(dtdes.Rows[y - 1]["DesignationName"]);
                    dtadd["DefaultRoleId6"] = Convert.ToString(dtdes.Rows[y - 1]["RoleId"]);
                }
            }
            dtTemp.Rows.Add(dtadd);
        }

        for (int i = (dtsel1.Rows.Count + 1); i <= 15; i++)
        {
            DataRow dtadd = dtTemp.NewRow();
            dtadd["Id"] = i.ToString();
            dtadd["DefaultDeptId"] = "";
            dtadd["DefaultDeptName"] = "";

            dtadd["DefaultDesitId"] = "";
            dtadd["DefaultDesiName"] = "";
            dtadd["DefaultRoleId"] = "";


            dtadd["DefaultDesitId2"] = "";
            dtadd["DefaultDesiName2"] = "";
            dtadd["DefaultRoleId2"] = "";

            dtadd["DefaultDesitId3"] = "";
            dtadd["DefaultDesiName3"] = "";
            dtadd["DefaultRoleId3"] = "";

            dtadd["DefaultDesitId4"] = "";
            dtadd["DefaultDesiName4"] = "";
            dtadd["DefaultRoleId4"] = "";

            dtadd["DefaultDesitId5"] = "";
            dtadd["DefaultDesiName5"] = "";
            dtadd["DefaultRoleId5"] = "";
            dtadd["DefaultDesitId6"] = "";
            dtadd["DefaultDesiName6"] = "";
            dtadd["DefaultRoleId6"] = "";
            dtTemp.Rows.Add(dtadd);
        }
        GridView3.DataSource = dtTemp;
        GridView3.DataBind();
    }
    public DataTable CreateDatatableDept()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Id";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd11 = new DataColumn();
        prd11.ColumnName = "DefaultDeptId";
        prd11.DataType = System.Type.GetType("System.String");
        prd11.AllowDBNull = true;
        dtTemp.Columns.Add(prd11);

        DataColumn prd111 = new DataColumn();
        prd111.ColumnName = "DefaultDeptName";
        prd111.DataType = System.Type.GetType("System.String");
        prd111.AllowDBNull = true;
        dtTemp.Columns.Add(prd111);
        /////1
        DataColumn prd1f = new DataColumn();
        prd1f.ColumnName = "DefaultDesitId";
        prd1f.DataType = System.Type.GetType("System.String");
        prd1f.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f);

        DataColumn ptrc = new DataColumn();
        ptrc.ColumnName = "DefaultDesiName";
        ptrc.DataType = System.Type.GetType("System.String");
        ptrc.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc);

        DataColumn prd1c = new DataColumn();
        prd1c.ColumnName = "DefaultRoleId";
        prd1c.DataType = System.Type.GetType("System.String");
        prd1c.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c);

        /////2
        DataColumn prd1f2 = new DataColumn();
        prd1f2.ColumnName = "DefaultDesitId2";
        prd1f2.DataType = System.Type.GetType("System.String");
        prd1f2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f2);

        DataColumn ptrc2 = new DataColumn();
        ptrc2.ColumnName = "DefaultDesiName2";
        ptrc2.DataType = System.Type.GetType("System.String");
        ptrc2.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc2);

        DataColumn prd1c2 = new DataColumn();
        prd1c2.ColumnName = "DefaultRoleId2";
        prd1c2.DataType = System.Type.GetType("System.String");
        prd1c2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c2);

        /////3
        DataColumn prd1f3 = new DataColumn();
        prd1f3.ColumnName = "DefaultDesitId3";
        prd1f3.DataType = System.Type.GetType("System.String");
        prd1f3.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f3);

        DataColumn ptrc3 = new DataColumn();
        ptrc3.ColumnName = "DefaultDesiName3";
        ptrc3.DataType = System.Type.GetType("System.String");
        ptrc3.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc3);

        DataColumn prd1c3 = new DataColumn();
        prd1c3.ColumnName = "DefaultRoleId3";
        prd1c3.DataType = System.Type.GetType("System.String");
        prd1c3.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c3);

        /////4
        DataColumn prd1f4 = new DataColumn();
        prd1f4.ColumnName = "DefaultDesitId4";
        prd1f4.DataType = System.Type.GetType("System.String");
        prd1f4.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f4);

        DataColumn ptrc4 = new DataColumn();
        ptrc4.ColumnName = "DefaultDesiName4";
        ptrc4.DataType = System.Type.GetType("System.String");
        ptrc4.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc4);

        DataColumn prd1c4 = new DataColumn();
        prd1c4.ColumnName = "DefaultRoleId4";
        prd1c4.DataType = System.Type.GetType("System.String");
        prd1c4.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c4);

        /////5
        DataColumn prd1f5 = new DataColumn();
        prd1f5.ColumnName = "DefaultDesitId5";
        prd1f5.DataType = System.Type.GetType("System.String");
        prd1f5.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f5);

        DataColumn ptrc5 = new DataColumn();
        ptrc5.ColumnName = "DefaultDesiName5";
        ptrc5.DataType = System.Type.GetType("System.String");
        ptrc5.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc5);

        DataColumn prd1c5 = new DataColumn();
        prd1c5.ColumnName = "DefaultRoleId5";
        prd1c5.DataType = System.Type.GetType("System.String");
        prd1c5.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c5);

        /////6
        DataColumn prd1f6 = new DataColumn();
        prd1f6.ColumnName = "DefaultDesitId6";
        prd1f6.DataType = System.Type.GetType("System.String");
        prd1f6.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f6);

        DataColumn ptrc6 = new DataColumn();
        ptrc6.ColumnName = "DefaultDesiName6";
        ptrc6.DataType = System.Type.GetType("System.String");
        ptrc6.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc6);

        DataColumn prd1c6 = new DataColumn();
        prd1c6.ColumnName = "DefaultRoleId6";
        prd1c6.DataType = System.Type.GetType("System.String");
        prd1c6.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c6);

        return dtTemp;
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





   
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {       
            foreach (GridViewRow den in GridView3.Rows)
            {
                TextBox lbldeptname = (TextBox)(den.FindControl("lbldeptname"));
                TextBox lbldesigname = (TextBox)(den.FindControl("lbldesigname"));
                TextBox lbldesigname2 = (TextBox)(den.FindControl("lbldesigname2"));
                TextBox lbldesigname3 = (TextBox)(den.FindControl("lbldesigname3"));
                TextBox lbldesigname4 = (TextBox)(den.FindControl("lbldesigname4"));
                TextBox lbldesigname5 = (TextBox)(den.FindControl("lbldesigname5"));
                TextBox lbldesigname6 = (TextBox)(den.FindControl("lbldesigname6"));
                Label txtdesigid = (Label)(den.FindControl("txtdesigid"));
                Label txtdesigid2 = (Label)(den.FindControl("txtdesigid2"));
                Label txtdesigid3 = (Label)(den.FindControl("txtdesigid3"));
                Label txtdesigid4 = (Label)(den.FindControl("txtdesigid4"));
                Label txtdesigid5 = (Label)(den.FindControl("txtdesigid5"));
                Label txtdesigid6 = (Label)(den.FindControl("txtdesigid6"));
                Label txtroleid = (Label)(den.FindControl("txtroleid"));
                Label txtroleid2 = (Label)(den.FindControl("txtroleid2"));
                Label txtroleid3 = (Label)(den.FindControl("txtroleid3"));
                Label txtroleid4 = (Label)(den.FindControl("txtroleid4"));
                Label txtroleid5 = (Label)(den.FindControl("txtroleid5"));
                Label txtroleid6 = (Label)(den.FindControl("txtroleid6"));
                if (lbldeptname.Text.Length > 0)
                {
                    string spt = "insert into DefaultDept(DeptName,VersionId)values('" + lbldeptname.Text + "','" + ddlProductname.SelectedValue + "')";
                    SqlCommand cmd1 = new SqlCommand(spt, con);
                    con.Open();
                    Int32 avb = Convert.ToInt32(cmd1.ExecuteNonQuery());
                    con.Close();
                    string selepagea1 = "select Max(DeptId) as Id from  DefaultDept where VersionId='" + ddlProductname.SelectedValue + "'";
                    SqlDataAdapter adsel1 = new SqlDataAdapter(selepagea1, con);
                    DataTable dtsel1 = new DataTable();
                    adsel1.Fill(dtsel1);
                    if (dtsel1.Rows.Count > 0)
                    {                       
                        if (lbldesigname.Text.Length > 0)
                        {
                            insdesig(lbldesigname, txtdesigid, txtroleid, dtsel1.Rows[0]["Id"].ToString(), lbldeptname);
                        }
                        if (lbldesigname2.Text.Length > 0)
                        {
                            insdesig(lbldesigname2, txtdesigid2, txtroleid2, dtsel1.Rows[0]["Id"].ToString(), lbldeptname);
                        }
                        if (lbldesigname3.Text.Length > 0)
                        {
                            insdesig(lbldesigname3, txtdesigid3, txtroleid3, dtsel1.Rows[0]["Id"].ToString(), lbldeptname);
                        }
                        if (lbldesigname4.Text.Length > 0)
                        {
                            insdesig(lbldesigname4, txtdesigid4, txtroleid4, dtsel1.Rows[0]["Id"].ToString(), lbldeptname);
                        }
                        if (lbldesigname5.Text.Length > 0)
                        {
                            insdesig(lbldesigname5, txtdesigid5, txtroleid5, dtsel1.Rows[0]["Id"].ToString(), lbldeptname);
                        }
                        if (lbldesigname6.Text.Length > 0)
                        {
                            insdesig(lbldesigname6, txtdesigid6, txtroleid6, dtsel1.Rows[0]["Id"].ToString(), lbldeptname);
                        }
                    }
                }
            }
            ddlProductname_SelectedIndexChanged(sender, e);
            Label1.Text = "Record inserted successfully";      
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
       
       
            foreach (GridViewRow gg1 in GridView3.Rows)
            {
                TextBox lbldeptname = (TextBox)(gg1.FindControl("lbldeptname"));
                Label txtdeptid = (Label)(gg1.FindControl("txtdeptid"));
                string deptid = txtdeptid.Text;
                TextBox lbldesigname = (TextBox)(gg1.FindControl("lbldesigname"));
                TextBox lbldesigname2 = (TextBox)(gg1.FindControl("lbldesigname2"));
                TextBox lbldesigname3 = (TextBox)(gg1.FindControl("lbldesigname3"));
                TextBox lbldesigname4 = (TextBox)(gg1.FindControl("lbldesigname4"));
                TextBox lbldesigname5 = (TextBox)(gg1.FindControl("lbldesigname5"));
                TextBox lbldesigname6 = (TextBox)(gg1.FindControl("lbldesigname6"));
                Label txtdesigid = (Label)(gg1.FindControl("txtdesigid"));
                Label txtdesigid2 = (Label)(gg1.FindControl("txtdesigid2"));
                Label txtdesigid3 = (Label)(gg1.FindControl("txtdesigid3"));
                Label txtdesigid4 = (Label)(gg1.FindControl("txtdesigid4"));
                Label txtdesigid5 = (Label)(gg1.FindControl("txtdesigid5"));
                Label txtdesigid6 = (Label)(gg1.FindControl("txtdesigid6"));

                Label txtroleid = (Label)(gg1.FindControl("txtroleid"));
                Label txtroleid2 = (Label)(gg1.FindControl("txtroleid2"));
                Label txtroleid3 = (Label)(gg1.FindControl("txtroleid3"));
                Label txtroleid4 = (Label)(gg1.FindControl("txtroleid4"));
                Label txtroleid5 = (Label)(gg1.FindControl("txtroleid5"));
                Label txtroleid6 = (Label)(gg1.FindControl("txtroleid6"));
                if (lbldeptname.Text.Length > 0)
                {
                    if (txtdeptid.Text.Length > 0)
                    {
                        string update = "update  DefaultDept set DeptName='" + lbldeptname.Text + "',VersionId='" + ddlProductname.SelectedValue + "' where  DeptId='" + txtdeptid.Text + "'  ";
                        SqlCommand ccm = new SqlCommand(update, con);
                        con.Open();
                        ccm.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        string spt = "insert into DefaultDept(DeptName,VersionId)values('" + lbldeptname.Text + "','" + ddlProductname.SelectedValue + "')";
                        SqlCommand cmd1 = new SqlCommand(spt, con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else
                {
                    if (txtdeptid.Text.Length > 0)
                    {
                        string sptsw = "Delete from DefaultDept where DeptId='" + txtdeptid.Text + "'";
                        SqlCommand cmd1sw = new SqlCommand(sptsw, con);
                        con.Open();
                        cmd1sw.ExecuteNonQuery();
                        con.Close();                        
                    }
                }
                if (lbldesigname.Text.Length > 0 || txtdesigid.Text.Length > 0)
                {
                    insdesig(lbldesigname, txtdesigid, txtroleid, deptid, lbldeptname);
                }
                if (lbldesigname2.Text.Length > 0 || txtdesigid2.Text.Length > 0)
                {
                    insdesig(lbldesigname2, txtdesigid2, txtroleid2, deptid, lbldeptname);
                }
                if (lbldesigname3.Text.Length > 0 || txtdesigid3.Text.Length > 0)
                {
                    insdesig(lbldesigname3, txtdesigid3, txtroleid3, deptid, lbldeptname);
                }
                if (lbldesigname4.Text.Length > 0 || txtdesigid4.Text.Length > 0)
                {
                    insdesig(lbldesigname4, txtdesigid4, txtroleid4, deptid, lbldeptname);
                }
                if (lbldesigname5.Text.Length > 0 || txtdesigid5.Text.Length > 0)
                {
                    insdesig(lbldesigname5, txtdesigid5, txtroleid5, deptid, lbldeptname);
                }
                if (lbldesigname6.Text.Length > 0 || txtdesigid6.Text.Length > 0)
                {
                    insdesig(lbldesigname6, txtdesigid6, txtroleid6, deptid, lbldeptname);
                }
            }
            ddlProductname_SelectedIndexChanged(sender, e);
            Label1.Text = "Record updated successfully";
       
    }



    protected void insdesig(TextBox lbldesigname, Label txtdesigid, Label txtroleid, string deptid, TextBox lbldeptname)
    {
        string rolid = txtroleid.Text;
        if (lbldeptname.Text.Length > 0)
        {
            if (Convert.ToString(txtdesigid.Text) == "" && Convert.ToString(txtroleid.Text) == "")
            {
                if (lbldesigname.Text.Length > 0)
                {
                    string sptr = "insert into DefaultRole(RoleName,VersionId)values('" + lbldesigname.Text + "','" + ddlProductname.SelectedValue + "')";
                    SqlCommand cmdr = new SqlCommand(sptr, con);
                    con.Open();
                    cmdr.ExecuteNonQuery();
                    con.Close();
                     string selrolm = "select Max(RoleId) as Id from  DefaultRole where VersionId='" + ddlProductname.SelectedValue + "'";
                    SqlDataAdapter admaxr = new SqlDataAdapter(selrolm, con);
                    DataTable dtmaxr = new DataTable();
                    admaxr.Fill(dtmaxr);
                    if (dtmaxr.Rows.Count > 0)
                    {
                        rolid = dtmaxr.Rows[0]["Id"].ToString();
                        string spts = "insert into DefaultDesignationTbl(DesignationName,DeptId,RoleId)values('" + lbldesigname.Text + "','" + deptid + "','" + rolid + "')";
                        SqlCommand cmd1s = new SqlCommand(spts, con);
                        con.Open();
                        cmd1s.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            else
            {
                if (lbldesigname.Text.Length > 0 && txtroleid.Text.Length > 0)
                {
                    string updateR = "update  DefaultRole set RoleName='" + lbldesigname.Text + "',VersionId='" + ddlProductname.SelectedValue + "' where  RoleId='" + txtroleid.Text + "'  ";
                    SqlCommand ccmR = new SqlCommand(updateR, con);
                    con.Open();
                    ccmR.ExecuteNonQuery();
                    con.Close();
                    string update = "update  DefaultDesignationTbl set DesignationName='" + lbldesigname.Text + "',DeptId='" + deptid + "',RoleId='" + txtroleid.Text + "' where  Id='" + txtdesigid.Text + "'  ";
                    SqlCommand ccm = new SqlCommand(update, con);
                    con.Open();
                    ccm.ExecuteNonQuery();
                    con.Close();                   
                }
                else
                {  
                    string update = "Delete from  DefaultDesignationTbl  where  Id='" + txtdesigid.Text + "'  ";
                    SqlCommand ccm = new SqlCommand(update, con);
                    con.Open();
                    ccm.ExecuteNonQuery();
                    con.Close();
                    string updatezx = "Delete from  DefaultRole  where  RoleId='" + txtroleid.Text + "'   ";
                    SqlCommand ccmzx = new SqlCommand(updatezx, con);
                    con.Open();
                    ccmzx.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        else
        {  
            string update = "Delete from  DefaultDesignationTbl  where  Id='" + txtdesigid.Text + "'  ";
            SqlCommand ccm = new SqlCommand(update, con);
            con.Open();
            ccm.ExecuteNonQuery();
            con.Close();
            string updatezx = "Delete from  DefaultRole  where  RoleId='" + txtroleid.Text + "'   ";
            SqlCommand ccmzx = new SqlCommand(updatezx, con);
            con.Open();
            ccmzx.ExecuteNonQuery();
            con.Close();
        }

    }



    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Button3.Visible = true;
        Button2.Visible = false;
        Panel3.Enabled = true;
    }
}

