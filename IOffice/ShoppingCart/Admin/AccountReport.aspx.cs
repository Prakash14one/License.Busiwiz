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

public partial class ShoppingCart_Admin_AccountReport : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            fillstore();
            fillclasstype();
            fillclassname();
            fillgroupname();
            fillgrid();
            lblcomname.Text = Session["Cname"].ToString();

        }

    }
    protected void fillstore()
    {
        DataTable ds1 = ClsStore.SelectStorename();
        if (ds1.Rows.Count > 0)
        {
            ddlsearchByStore.DataSource = ds1;
            ddlsearchByStore.DataTextField = "Name";
            ddlsearchByStore.DataValueField = "WarehouseId";
            ddlsearchByStore.DataBind();
            ddlsearchByStore.Items.Insert(0, "All");
            ddlsearchByStore.Items[0].Value = "0";
        }
    }
    protected void fillgrid()
    {
        string str1 = "";
       // lblBusiness.Text = "ALL";

            lblBusiness.Text=ddlsearchByStore.SelectedItem.Text;
            lblclasstypeprint.Text=ddlSearchByClassType.SelectedItem.Text;
            lblclassnameprint.Text=ddlSearchByClass.SelectedItem.Text;
            lblgroupnameprint.Text=ddlSearchByGroup.SelectedItem.Text;
            lblstatusprint.Text = ddlstatus.SelectedItem.Text;

            if (ddlsearchByStore.SelectedIndex > 0)
            {
                //   lblBusiness.Text = ddlsearchByStore.SelectedItem.Text;
                str1 = "select distinct ClassCompanyMaster.displayname, warehousemaster.Name, GroupCompanyMaster.Groupid, GroupCompanyMaster.groupdisplayname,AccountMaster.id,AccountMaster.AccountId,AccountMaster.AccountName,AccountMaster.Balance,AccountMaster.Date,AccountMaster.Status,case when (AccountMaster.Status='1') then 'Active' else 'Inactive' End as Statuslabel,ClassTypeCompanyMaster.displayname as Classtypename from warehousemaster inner join AccountMaster on AccountMaster.Whid=warehousemaster.warehouseId inner join GroupCompanyMaster on AccountMaster.GroupId=GroupCompanyMaster.groupid inner join ClassCompanyMaster on ClassCompanyMaster.Id=GroupCompanyMaster.classcompanymasterid inner join ClassTypeCompanyMaster on ClassTypeCompanyMaster.id=ClassCompanyMaster.classtypecompanymasterid where GroupCompanyMaster.[Whid] = '" + ddlsearchByStore.SelectedValue + "' and ClassCompanyMaster.[Whid] = '" + ddlsearchByStore.SelectedValue + "' and AccountMaster.[Whid] = '" + ddlsearchByStore.SelectedValue + "' and AccountMaster.compid='" + Session["comid"] + "' and GroupCompanyMaster.cid='" + Session["comid"] + "' and GroupCompanyMaster.Whid= '" + ddlsearchByStore.SelectedValue + "'and warehousemaster.Status='" + 1 + "' ";
            }
            else
            {
                str1 = "select distinct ClassCompanyMaster.displayname, warehousemaster.Name, GroupCompanyMaster.Groupid, GroupCompanyMaster.groupdisplayname,AccountMaster.id,AccountMaster.AccountId,AccountMaster.AccountName,AccountMaster.Balance,AccountMaster.Date,AccountMaster.Status,case when (AccountMaster.Status='1') then 'Active' else 'Inactive' End as Statuslabel,ClassTypeCompanyMaster.displayname as Classtypename from warehousemaster inner join AccountMaster on AccountMaster.Whid=warehousemaster.warehouseId inner join GroupCompanyMaster on AccountMaster.GroupId=GroupCompanyMaster.groupid inner join ClassCompanyMaster on ClassCompanyMaster.Id=GroupCompanyMaster.classcompanymasterid inner join ClassTypeCompanyMaster on ClassTypeCompanyMaster.id=ClassCompanyMaster.classtypecompanymasterid where  ClassCompanyMaster.cid='" + Session["comid"] + "' and AccountMaster.compid='" + Session["comid"] + "' and GroupCompanyMaster.cid='" + Session["comid"] + "'and warehousemaster.Status='" + 1 + "' ";
            }
            //if (ddlsearchByStore.SelectedIndex > 0)
            //{
            //    //   lblBusiness.Text = ddlsearchByStore.SelectedItem.Text;
            //    str1 = "select distinct ClassCompanyMaster.displayname, warehousemaster.Name, GroupCompanyMaster.Groupid, GroupCompanyMaster.groupdisplayname,AccountMaster.id,AccountMaster.AccountId,AccountMaster.AccountName,AccountMaster.Balance,AccountMaster.Date,AccountMaster.Status,case when (AccountMaster.Status='1') then 'Active' else 'Inactive' End as Statuslabel,ClassTypeCompanyMaster.displayname as Classtypename from warehousemaster inner join AccountMaster on AccountMaster.Whid=warehousemaster.warehouseId inner join GroupCompanyMaster on AccountMaster.GroupId=GroupCompanyMaster.groupid inner join ClassCompanyMaster on ClassCompanyMaster.Id=GroupCompanyMaster.classcompanymasterid inner join ClassTypeCompanyMaster on ClassTypeCompanyMaster.id=ClassCompanyMaster.classtypecompanymasterid where GroupCompanyMaster.[Whid] = '" + ddlsearchByStore.SelectedValue + "' and ClassCompanyMaster.[Whid] = '" + ddlsearchByStore.SelectedValue + "' and AccountMaster.[Whid] = '" + ddlsearchByStore.SelectedValue + "' and AccountMaster.compid='" + Session["comid"] + "' and GroupCompanyMaster.cid='" + Session["comid"] + "' and GroupCompanyMaster.Whid= '" + ddlsearchByStore.SelectedValue + "'and warehousemaster.Status='" + 1 + "' ";
            //}
            //else
            //{
            //    str1 = "select distinct ClassCompanyMaster.displayname, warehousemaster.Name, GroupCompanyMaster.Groupid, GroupCompanyMaster.groupdisplayname,AccountMaster.id,AccountMaster.AccountId,AccountMaster.AccountName,AccountMaster.Balance,AccountMaster.Date,AccountMaster.Status,case when (AccountMaster.Status='1') then 'Active' else 'Inactive' End as Statuslabel,ClassTypeCompanyMaster.displayname as Classtypename from warehousemaster inner join AccountMaster on AccountMaster.Whid=warehousemaster.warehouseId inner join GroupCompanyMaster on AccountMaster.GroupId=GroupCompanyMaster.groupid inner join ClassCompanyMaster on ClassCompanyMaster.Id=GroupCompanyMaster.classcompanymasterid inner join ClassTypeCompanyMaster on ClassTypeCompanyMaster.id=ClassCompanyMaster.classtypecompanymasterid where  ClassCompanyMaster.cid='" + Session["comid"] + "' and AccountMaster.compid='" + Session["comid"] + "' and GroupCompanyMaster.cid='" + Session["comid"] + "'and warehousemaster.Status='" + 1 + "' ";
            //}

        if (ddlSearchByClassType.SelectedIndex > 0)
        {
            str1 += " and ClassTypeCompanyMaster.id='" + ddlSearchByClassType.SelectedValue + "'";
 
        }
        if (ddlSearchByClass.SelectedIndex > 0)
        {
            str1 += "and ClassCompanyMaster.id='" + ddlSearchByClass.SelectedValue + "'";

        }
        if (ddlSearchByGroup.SelectedIndex > 0)
        {
            str1 += "and GroupCompanyMaster.id='" + ddlSearchByGroup.SelectedValue + "'";

        }
        if (ddlstatus.SelectedIndex > 0)
        {
            str1 += "and AccountMaster.Status='" + ddlstatus.SelectedValue + "'";

        }



        string str2 = "order by warehousemaster.Name,ClassTypeCompanyMaster.displayname, ClassCompanyMaster.displayname,GroupCompanyMaster.groupdisplayname ,AccountMaster.Id,AccountMaster.AccountName Desc";

        string finalstr = str1 + str2;

        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(finalstr, con);
        da.Fill(ds1);

        //if (ds1.Rows.Count > 0)
        //{
            GridView1.DataSource = ds1;


            DataView myDataView = new DataView();
            myDataView = ds1.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataBind();
       // }
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void ddlsearchByStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillclasstype();
        fillclassname();
        fillgroupname();
        fillgrid();
    }
    protected void fillclasstype()
    {
        ddlSearchByClassType.Items.Clear();
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            string str22 = "select id,displayname from ClassTypeCompanyMaster where Whid='" + ddlsearchByStore.SelectedValue + "' order by displayname ";
            SqlCommand cmd = new SqlCommand(str22, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            ddlSearchByClassType.DataSource = dt;
            ddlSearchByClassType.DataTextField = "displayname";
            ddlSearchByClassType.DataValueField = "id";
            ddlSearchByClassType.DataBind();
            
        }
        ddlSearchByClassType.Items.Insert(0, "All");
        ddlSearchByClassType.Items[0].Value = "0";
       
      

    }
    protected void fillclassname()
    {
        ddlSearchByClass.Items.Clear();
        if (ddlSearchByClassType.SelectedIndex > 0)
        {
            string str11 = "SELECT  Distinct ClassCompanyMaster.id,ClassCompanyMaster.Displayname AS ClassType1 " +
                           "FROM   ClassCompanyMaster INNER JOIN " +
                           "ClassTypeCompanyMaster ON ClassCompanyMaster.classtypecompanymasterid = ClassTypeCompanyMaster.id " +
                           "WHERE     (ClassTypeCompanyMaster.id = '" + ddlSearchByClassType.SelectedValue + "')  order by ClassCompanyMaster.Displayname  ";

            SqlCommand cmd = new SqlCommand(str11, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            ddlSearchByClass.DataSource = dt;
            ddlSearchByClass.DataTextField = "ClassType1";
            ddlSearchByClass.DataValueField = "id";
            ddlSearchByClass.DataBind();

        }
        ddlSearchByClass.Items.Insert(0, "All");
        ddlSearchByClass.Items[0].Value = "0";
 
    }
    protected void fillgroupname()
    {
        ddlSearchByGroup.Items.Clear();
        if (ddlSearchByClass.SelectedIndex > 0)
        {

            string str13 = "SELECT Distinct GroupCompanyMaster.id,  GroupCompanyMaster.GroupDisplayName AS ClassType1 " +
                           "FROM         ClassTypeCompanyMaster INNER JOIN " +
                           "ClassCompanyMaster ON ClassTypeCompanyMaster.id = ClassCompanyMaster.classtypecompanymasterid RIGHT OUTER JOIN " +
                           "GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid " +
                           "WHERE     ( ClassCompanyMaster.id= " + ddlSearchByClass.SelectedValue + ") and GroupCompanyMaster.Whid='" + ddlsearchByStore.SelectedValue + "' order by GroupCompanyMaster.GroupDisplayName ";

            SqlCommand cmd = new SqlCommand(str13, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            ddlSearchByGroup.DataSource = dt;
            ddlSearchByGroup.DataTextField = "ClassType1";
            ddlSearchByGroup.DataValueField = "id";
            ddlSearchByGroup.DataBind();

        }
        ddlSearchByGroup.Items.Insert(0, "All");
        ddlSearchByGroup.Items[0].Value = "0";

    }

    protected void ddlSearchByClassType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillclassname();
        fillgroupname();
        fillgrid();
    }
    protected void ddlSearchByClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgroupname();
        fillgrid();
    }

    protected void ddlSearchByGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;

        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
}
