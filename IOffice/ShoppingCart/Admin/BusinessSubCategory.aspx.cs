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

public partial class BusinessSubCategory : System.Web.UI.Page
{
    // SqlConnection con = new SqlConnection(@"Data Source = tcp:192.168.5.221,2810; Initial Catalog = 1133.onlineaccounts.net; Integrated Security = true");

    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    SqlConnection con;

    public static string logoname1;
    public static string logoname;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;



        // Session["Comid"] = "1133";

        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        if (!IsPostBack)
        {
            fillddlCat();
            filtercategory();
            fillGvSC();
        }

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        SqlCommand cmdstr = new SqlCommand("select B_SubCategory from BusinessSubCat where B_SubCategory='" + txtSubCat.Text + "'", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmdstr);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            lblmsg.Text = "Record already exist.";
        }
        else
        {
            Insert();
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";
            clear();
        }
        fillGvSC();
    }

    protected void Insert()
    {
        SqlCommand cmd = new SqlCommand("Insert Into BusinessSubCat(B_SubCategory,B_CatID,Active,Image) values('" + txtSubCat.Text + "','" + ddlCat.SelectedValue + "','" + cbActive.Checked + "','" + logoname + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        fillGvSC();
    }
    protected void fillddlCat()
    {
        ddlCat.Items.Clear();

        string strc = "SELECT B_Category, B_CatID From BusinessCategory where Active='1'";
        SqlCommand cmdddlc = new SqlCommand(strc, con);
        SqlDataAdapter dac = new SqlDataAdapter(cmdddlc);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);

        if (dtc.Rows.Count > 0)
        {
            ddlCat.DataSource = dtc;
            ddlCat.DataTextField = "B_Category";
            ddlCat.DataValueField = "B_CatID";
            ddlCat.DataBind();
        }
        ddlCat.Items.Insert(0, "-Select-");
        ddlCat.Items[0].Value = "0";
    }
    protected void fillGvSC()
    {
        string st1 = "";

        lblstat.Text = "All";
        lblcategory.Text = "All";

        if (ddlcategory.SelectedIndex > 0)
        {
            lblcategory.Text = ddlcategory.SelectedItem.Text;
            st1 += " and BusinessSubCat.B_CatID='" + ddlcategory.SelectedValue + "'";
        }

        if (ddlstatus_search.SelectedIndex > 0)
        {
            lblstat.Text = ddlstatus_search.SelectedItem.Text;
            st1 += " and BusinessSubCat.Active='" + ddlstatus_search.SelectedValue + "'";
        }

        SqlCommand cmdsc = new SqlCommand("SELECT BusinessSubCat.B_SubCatID,case when(BusinessSubCat.Active = '1') then 'Active' else 'Inactive' end as Active,BusinessSubCat.B_SubCategory,BusinessCategory.B_Category FROM BusinessSubCat INNER JOIN BusinessCategory ON BusinessSubCat.B_CatID = BusinessCategory.B_CatID where BusinessCategory.Active='1' " + st1 + "", con);
        SqlDataAdapter dasc = new SqlDataAdapter(cmdsc);
        DataTable dtsc = new DataTable();
        dasc.Fill(dtsc);

        if (dtsc.Rows.Count > 0)
        {
            GVSC.DataSource = dtsc;
            GVSC.DataBind();
        }
        else
        {
            GVSC.DataSource = null;
            GVSC.DataBind();
        }
    }
    protected void GVSC_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GVSC_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GVSC_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GVSC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVSC.PageIndex = e.NewPageIndex;
        fillGvSC();
    }
    protected void clear()
    {
        logoname = "";
        ddlCat.SelectedValue = "0";
        txtSubCat.Text = "";
        cbActive.Checked = false;
        lbllegend.Text = "";
        btnsave.Visible = true;
        Button3.Visible = false;
        btnadddd.Visible = true;
        pnladdd.Visible = false;
        imgLogo.ImageUrl = "";
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        lblmsg.Text = "";
    }
    protected void GVSC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            btnsave.Visible = false;
            Button3.Visible = true;
            pnladdd.Visible = true;
            btnadddd.Visible = false;
            lbllegend.Text = "Edit Business Sub Category";
            lblmsg.Text = "";

            int mm = Convert.ToInt32(e.CommandArgument);

            ViewState["ID"] = mm;

            SqlDataAdapter da = new SqlDataAdapter("select * from BusinessSubCat where B_SubCatID='" + mm + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            fillddlCat();
            ddlCat.SelectedIndex = ddlCat.Items.IndexOf(ddlCat.Items.FindByValue(dt.Rows[0]["B_CatID"].ToString()));

            txtSubCat.Text = dt.Rows[0]["B_SubCategory"].ToString();
            cbActive.Checked = Convert.ToBoolean(dt.Rows[0]["Active"].ToString());

            logoname1 = dt.Rows[0]["Image"].ToString();
            imgLogo.ImageUrl = "~/ShoppingCart/images/" + logoname1;
            imgLogo.Visible = true;
        }

        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            SqlDataAdapter daf = new SqlDataAdapter("select * from BusinessSubSubCat where B_SubCatID='" + mm1 + "'", con);
            DataTable dtf = new DataTable();
            daf.Fill(dtf);

            if (dtf.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, you are not able to delete this record as child record exist using this record.";
            }
            else
            {
                string str1 = "Delete From BusinessSubCat  where B_SubCatID=" + mm1 + " ";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();

                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully.";
            }
            fillGvSC();
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        if (logoname1 == "")
        {
            logoname1 = Session["phofile"].ToString();
        }
        else
        {
            if (logoname == null)
            {
                logoname1 = logoname1;
            }
            else
            {
                logoname1 = logoname;
            }
        }
        string str = "update BusinessSubCat set B_SubCategory='" + txtSubCat.Text + "',Active='" + cbActive.Checked + "',B_CatID='" + ddlCat.SelectedValue + "',Image='" + logoname1 + "' where B_SubCatID='" + ViewState["ID"] + "'";

        SqlCommand cmd1 = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd1.ExecuteNonQuery();
        con.Close();

        lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully.";
        fillGvSC();
        clear();

        logoname = null;
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;

            if (GVSC.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GVSC.Columns[3].Visible = false;
            }
            if (GVSC.Columns[4].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GVSC.Columns[4].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GVSC.Columns[3].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GVSC.Columns[4].Visible = true;
            }
        }
    }

    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGvSC();
    }
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGvSC();
    }
    protected void btnadddd_Click(object sender, EventArgs e)
    {
        lbllegend.Text = "Add New Business Sub Category";
        lblmsg.Text = "";
        pnladdd.Visible = true;
        btnadddd.Visible = false;
    }

    protected void filtercategory()
    {
        ddlcategory.Items.Clear();

        string strc = "SELECT B_Category, B_CatID From BusinessCategory where Active='1'";
        SqlCommand cmdddlc = new SqlCommand(strc, con);
        SqlDataAdapter dac = new SqlDataAdapter(cmdddlc);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);

        if (dtc.Rows.Count > 0)
        {
            ddlcategory.DataSource = dtc;
            ddlcategory.DataTextField = "B_Category";
            ddlcategory.DataValueField = "B_CatID";
            ddlcategory.DataBind();
        }
        ddlcategory.Items.Insert(0, "All");
        ddlcategory.Items[0].Value = "0";
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {
        string te = "BusinessCategory.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        fillddlCat();
    }
    protected void imgBtnImageUpdate_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            bool valid = ext(FileUpload1.FileName);

            if (valid == true)
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\ShoppingCart\\images\\") + FileUpload1.FileName);
                logoname = FileUpload1.FileName.ToString();

                imgLogo.Visible = true;
                imgLogo.ImageUrl = "~/ShoppingCart/images/" + FileUpload1.FileName.ToString();

                Session["phofile"] = FileUpload1.FileName.ToString();
            }
            else
            {
                lblmsg.Text = "Invalid File Type. Please upload an image file in one of the following formats: bmp, gif, png, jpg, jpeg, ipg.";
            }

        }
    }

    public bool ext(string filename)
    {
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "ipg" };

        string ext = System.IO.Path.GetExtension(filename);

        bool isValidFile = false;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + validFileTypes[i])
            {

                isValidFile = true;

                break;

            }

        }
        return isValidFile;
    }
}
