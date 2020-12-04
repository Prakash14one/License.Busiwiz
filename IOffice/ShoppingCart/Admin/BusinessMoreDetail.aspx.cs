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

public partial class BusinessDetails : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";

            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            fillstore();
            fillddlCat();
            fillddlSubCat();
            fillddlSubSubCat();

            fillcategor();

            fillGVC();
        }
    }

    protected void fillstore()
    {
        ddlStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlStore.DataSource = ds;
        ddlStore.DataTextField = "Name";
        ddlStore.DataValueField = "WareHouseId";
        ddlStore.DataBind();
        ddlStore.Items.Insert(0, "-Select-");
        ddlStore.Items[0].Value = "0";

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }

    protected void fillddlCat()
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
        ddlcategory.Items.Insert(0, "-Select-");
        ddlcategory.Items[0].Value = "0";
    }

    protected void fillddlSubCat()
    {
        ddlsubcategory.Items.Clear();

        if (ddlcategory.SelectedIndex > 0)
        {
            string strsc = "SELECT B_SubCategory, B_SubCatID From BusinessSubCat Where B_CatID=" + ddlcategory.SelectedValue + " and Active='1'";
            SqlCommand cmdddlsc = new SqlCommand(strsc, con);
            SqlDataAdapter dasc = new SqlDataAdapter(cmdddlsc);
            DataTable dtsc = new DataTable();
            dasc.Fill(dtsc);

            if (dtsc.Rows.Count > 0)
            {
                ddlsubcategory.DataSource = dtsc;
                ddlsubcategory.DataTextField = "B_SubCategory";
                ddlsubcategory.DataValueField = "B_SubCatID";
                ddlsubcategory.DataBind();
            }
        }
        ddlsubcategory.Items.Insert(0, "-Select-");
        ddlsubcategory.Items[0].Value = "0";
    }

    protected void fillddlSubSubCat()
    {
        ddlsubsubcategory.Items.Clear();

        if (ddlsubcategory.SelectedIndex > 0)
        {
            string strsc = "SELECT B_SubSubCategory, B_SubSubCatID From BusinessSubSubCat Where B_SubCatID=" + ddlsubcategory.SelectedValue + " and Active='1'";
            SqlCommand cmdddlsc = new SqlCommand(strsc, con);
            SqlDataAdapter dasc = new SqlDataAdapter(cmdddlsc);
            DataTable dtsc = new DataTable();
            dasc.Fill(dtsc);

            if (dtsc.Rows.Count > 0)
            {
                ddlsubsubcategory.DataSource = dtsc;
                ddlsubsubcategory.DataTextField = "B_SubSubCategory";
                ddlsubsubcategory.DataValueField = "B_SubSubCatID";
                ddlsubsubcategory.DataBind();
            }
        }
        ddlsubsubcategory.Items.Insert(0, "-Select-");
        ddlsubsubcategory.Items[0].Value = "0";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (txtmission.Text.Length > 200)
        {
            Label67.Text = "Please enter maximum 200 chars";
            Label7.Text = "";
            Label9.Text = "";
            Label11.Text = "";
            txtmission.Focus();
        }
        else if (txthighlights.Text.Length > 1000)
        {
            Label67.Text = "";
            Label7.Text = "Please enter maximum 1000 chars";
            Label9.Text = "";
            Label11.Text = "";
            txthighlights.Focus();
        }
        else if (txtcorporate.Text.Length > 1000)
        {
            Label67.Text = "";
            Label7.Text = "";
            Label9.Text = "Please enter maximum 1000 chars";
            Label11.Text = "";
            txtcorporate.Focus();
        }
        else if (txtfacts.Text.Length > 1000)
        {
            Label67.Text = "";
            Label7.Text = "";
            Label9.Text = "";
            Label11.Text = "Please enter maximum 1000 chars";
            txtfacts.Focus();
        }
        else
        {
            SqlCommand cmdstr = new SqlCommand("select * from BusinessDetails where BusinessID='" + ddlStore.SelectedValue + "'", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmdstr);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                lblmsg.Text = "Record already exist.";
            }
            else
            {
                insert();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully.";
                clear();
            }
            fillGVC();
        }
    }

    protected void clear()
    {
        ddlStore.SelectedIndex = 0;
        ddlcategory.SelectedIndex = 0;
        txtnoofhours.Text = "";

        btnadddd.Visible = true;
        pnladdd.Visible = false;
        lbllegend.Text = "";
        btnsave.Visible = true;
        Button3.Visible = false;

        txtcorporate.Text = "";
        txtfacts.Text = "";
        txthighlights.Text = "";
        txtmission.Text = "";
        txtnoemp.Text = "";

        GridView5.DataSource = null;
        panel1.Visible = false;
    }

    protected void insert()
    {
        if (GridView5.Rows.Count > 0)
        {
            SqlCommand cmd = new SqlCommand("Insert Into BusinessDetails(BusinessID,BusinessCategory,year,Title,kee,Corporate,Facts,noof) values('" + ddlStore.SelectedValue + "','" + ddlsubsubcategory.SelectedValue + "','" + txtnoofhours.Text + "','" + txtmission.Text + "','" + txthighlights.Text + "','" + txtcorporate.Text + "','" + txtfacts.Text + "','" + txtnoemp.Text + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            foreach (GridViewRow gfhh in GridView5.Rows)
            {
                Image Image1 = (Image)gfhh.FindControl("Image1");

                DataTable dtmax = select("select max(ID) as ID from BusinessDetails");

                SqlCommand cmd1 = new SqlCommand("Insert Into BusinessDetails2(detailid,image) values('" + dtmax.Rows[0]["ID"].ToString() + "','" + Image1.ImageUrl + "')", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();
            }
        }
        else
        {
            SqlCommand cmd = new SqlCommand("Insert Into BusinessDetails(BusinessID,BusinessCategory,year,Title,kee,Corporate,Facts,noof) values('" + ddlStore.SelectedValue + "','" + ddlsubsubcategory.SelectedValue + "','" + txtnoofhours.Text + "','" + txtmission.Text + "','" + txthighlights.Text + "','" + txtcorporate.Text + "','" + txtfacts.Text + "','" + txtnoemp.Text + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            DataTable dtmax = select("select max(ID) as ID from BusinessDetails");

            SqlCommand cmd1 = new SqlCommand("Insert Into BusinessDetails2(detailid,image) values('" + dtmax.Rows[0]["ID"].ToString() + "','')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();
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

    public void fillcategor()
    {
        ddlstatus_search.Items.Clear();

        string strc = "SELECT B_Category, B_CatID From BusinessCategory where Active='1'";
        SqlCommand cmdddlc = new SqlCommand(strc, con);
        SqlDataAdapter dac = new SqlDataAdapter(cmdddlc);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);

        if (dtc.Rows.Count > 0)
        {
            ddlstatus_search.DataSource = dtc;
            ddlstatus_search.DataTextField = "B_Category";
            ddlstatus_search.DataValueField = "B_CatID";
            ddlstatus_search.DataBind();
        }
        ddlstatus_search.Items.Insert(0, "-Select-");
        ddlstatus_search.Items[0].Value = "0";
    }

    protected void fillGVC()
    {
        string st1 = "";

        //lblstat.Text = "All";

        if (ddlstatus_search.SelectedIndex > 0)
        {
            //lblstat.Text = ddlstatus_search.SelectedItem.Text;

            st1 += " where BusinessDetails.BusinessCategory='" + ddlstatus_search.SelectedValue + "'";
        }

        string str = "select distinct BusinessDetails.ID,WareHouseMaster.Name as Wname,BusinessSubSubCat.B_SubSubCategory as Category,BusinessDetails.year,LEFT(BusinessDetails.Title,40) as Title,BusinessDetails.Title as Title1,LEFT(BusinessDetails.kee,40) as kee,BusinessDetails.kee as kee1,LEFT(BusinessDetails.Corporate,40) as Corporate,BusinessDetails.Corporate as Corporate1,LEFT(BusinessDetails.Facts,40) as Facts,BusinessDetails.Facts as Facts1 from BusinessDetails inner join WareHouseMaster on WareHouseMaster.WareHouseId=BusinessDetails.BusinessID left join BusinessSubSubCat on BusinessSubSubCat.B_SubSubCatID=BusinessDetails.BusinessCategory " + st1 + " order by WareHouseMaster.Name asc";
        SqlDataAdapter dac = new SqlDataAdapter(str, con);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);

        DataView myDataView = new DataView();
        myDataView = dtc.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        if (dtc.Rows.Count > 0)
        {
            GVC.DataSource = myDataView;
            GVC.DataBind();
        }
        else
        {
            GVC.DataSource = null;
            GVC.DataBind();
        }       
    }

    protected void GVC_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GVC_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GVC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVC.PageIndex = e.NewPageIndex;
        fillGVC();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        lblmsg.Text = "";
    }
    protected void btnadddd_Click(object sender, EventArgs e)
    {
        pnladdd.Visible = true;
        btnadddd.Visible = false;
        lbllegend.Text = "Add New Business Profile";
        lblmsg.Text = "";
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;

            if (GVC.Columns[7].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GVC.Columns[7].Visible = false;
            }
            if (GVC.Columns[8].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GVC.Columns[8].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GVC.Columns[7].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GVC.Columns[8].Visible = true;
            }
        }
    }

    protected void GVC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            btnsave.Visible = false;
            Button3.Visible = true;
            pnladdd.Visible = true;
            btnadddd.Visible = false;

            lbllegend.Text = "Edit Business Profile";
            lblmsg.Text = "";

            int mm = Convert.ToInt32(e.CommandArgument);

            ViewState["ID"] = mm;

            SqlDataAdapter da = new SqlDataAdapter("select * from BusinessDetails where ID='" + mm + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dt.Rows[0]["BusinessID"].ToString()));

                if (Convert.ToString(dt.Rows[0]["BusinessCategory"]) != "0")
                {
                    DataTable dtsubcat = select("select B_SubCatID from BusinessSubSubCat where B_SubSubCatID='" + dt.Rows[0]["BusinessCategory"].ToString() + "'");

                    DataTable dtcat = select("select B_CatID from BusinessSubCat where B_SubCatID='" + dtsubcat.Rows[0]["B_SubCatID"].ToString() + "'");

                    fillddlCat();
                    ddlcategory.SelectedIndex = ddlcategory.Items.IndexOf(ddlcategory.Items.FindByValue(dtcat.Rows[0]["B_CatID"].ToString()));

                    fillddlSubCat();
                    ddlsubcategory.SelectedIndex = ddlsubcategory.Items.IndexOf(ddlsubcategory.Items.FindByValue(dtsubcat.Rows[0]["B_SubCatID"].ToString()));

                    fillddlSubSubCat();
                    ddlsubsubcategory.SelectedIndex = ddlsubsubcategory.Items.IndexOf(ddlsubsubcategory.Items.FindByValue(dt.Rows[0]["BusinessCategory"].ToString()));
                }

                txtnoofhours.Text = dt.Rows[0]["year"].ToString();
                txtnoemp.Text = dt.Rows[0]["noof"].ToString();

                txtmission.Text = dt.Rows[0]["Title"].ToString();
                txthighlights.Text = dt.Rows[0]["kee"].ToString();
                txtcorporate.Text = dt.Rows[0]["Corporate"].ToString();
                txtfacts.Text = dt.Rows[0]["Facts"].ToString();

                DataTable dtdeta = select("select image as image1 from BusinessDetails2 where detailid='" + ViewState["ID"] + "'");

                if (dtdeta.Rows.Count > 0)
                {
                    if (Convert.ToString(dtdeta.Rows[0]["image1"]) != "")
                    {
                        panel1.Visible = true;

                        ViewState["datavac"] = dtdeta;

                        GridView5.DataSource = dtdeta;
                        GridView5.DataBind();
                    }
                }
            }

        }

        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            string str1 = "Delete  From BusinessDetails Where ID= " + mm1 + " ";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully.";

            fillGVC();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (txtmission.Text.Length > 200)
        {
            Label67.Text = "Please enter maximum 200 chars";
            Label7.Text = "";
            Label9.Text = "";
            Label11.Text = "";
            txtmission.Focus();
        }
        else if (txthighlights.Text.Length > 1000)
        {
            Label67.Text = "";
            Label7.Text = "Please enter maximum 1000 chars";
            Label9.Text = "";
            Label11.Text = "";
            txthighlights.Focus();
        }
        else if (txtcorporate.Text.Length > 1000)
        {
            Label67.Text = "";
            Label7.Text = "";
            Label9.Text = "Please enter maximum 1000 chars";
            Label11.Text = "";
            txtcorporate.Focus();
        }
        else if (txtfacts.Text.Length > 1000)
        {
            Label67.Text = "";
            Label7.Text = "";
            Label9.Text = "";
            Label11.Text = "Please enter maximum 1000 chars";
            txtfacts.Focus();
        }
        else
        {

            SqlCommand cmdstr = new SqlCommand("select * from BusinessDetails where BusinessID='" + ddlStore.SelectedValue + "' and ID <> '" + ViewState["ID"] + "'", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmdstr);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                lblmsg.Text = "Record already exist.";
            }
            else
            {
                string str = "update BusinessDetails set BusinessID='" + ddlStore.SelectedValue + "',BusinessCategory='" + ddlsubsubcategory.SelectedValue + "',year='" + txtnoofhours.Text + "',Title='" + txtmission.Text + "',kee='" + txthighlights.Text + "',Corporate='" + txtcorporate.Text + "',Facts='" + txtfacts.Text + "',noof='" + txtnoemp.Text + "' where ID='" + ViewState["ID"] + "'";
                SqlCommand cmd1 = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();

                if (GridView5.Rows.Count > 0)
                {
                    string str11 = "delete from BusinessDetails2 where detailid='" + ViewState["ID"] + "'";
                    SqlCommand cmd11 = new SqlCommand(str11, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd11.ExecuteNonQuery();
                    con.Close();

                    foreach (GridViewRow gfhh in GridView5.Rows)
                    {
                        Image Image1 = (Image)gfhh.FindControl("Image1");

                        SqlCommand cmd12 = new SqlCommand("Insert Into BusinessDetails2(detailid,image) values('" + ViewState["ID"] + "','" + Image1.ImageUrl + "')", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd12.ExecuteNonQuery();
                        con.Close();
                    }
                }

                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully.";
                clear();
            }

            fillGVC();
        }
    }

    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGVC();
    }
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlSubCat();
    }
    protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlSubSubCat();
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

            lblmsg.Text = "Record deleted successfully.";
        }
    }

    protected void imgBtnImageUpdate_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            bool valid = ext(FileUpload1.FileName);

            if (valid == true)
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\ShoppingCart\\images\\") + FileUpload1.FileName);

                //logoname = FileUpload1.FileName.ToString();

                //imgLogo.Visible = true;
                //imgLogo.ImageUrl = "~/ShoppingCart/images/" + FileUpload1.FileName.ToString();

                Session["phofile"] = FileUpload1.FileName.ToString();

                DataTable dtvac = new DataTable();

                if (Convert.ToString(ViewState["datavac"]) == "")
                {
                    DataRow Drow = dtvac.NewRow();

                    DataColumn Dcom = new DataColumn();

                    Dcom.DataType = System.Type.GetType("System.String");
                    Dcom.ColumnName = "Image1";
                    Dcom.AllowDBNull = true;
                    Dcom.Unique = false;
                    Dcom.ReadOnly = false;

                    dtvac.Columns.Add(Dcom);

                    Drow["Image1"] = "~/ShoppingCart/images/" + FileUpload1.FileName.ToString();

                    dtvac.Rows.Add(Drow);

                    ViewState["datavac"] = dtvac;

                    panel1.Visible = true;
                }
                else
                {
                    dtvac = (DataTable)ViewState["datavac"];

                    int flag = 0;

                    foreach (DataRow dr in dtvac.Rows)
                    {
                        string image = dr["Image1"].ToString();

                        if (image == FileUpload1.FileName.ToString())
                        {
                            lblmsg.Text = "Record already exist";
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        DataRow Drow = dtvac.NewRow();

                        Drow["Image1"] = "~/ShoppingCart/images/" + FileUpload1.FileName.ToString();

                        dtvac.Rows.Add(Drow);
                        ViewState["datavac"] = dtvac;
                    }
                }

                GridView5.DataSource = ViewState["datavac"];
                GridView5.DataBind();
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
    protected void GVC_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillGVC();
    }
}
