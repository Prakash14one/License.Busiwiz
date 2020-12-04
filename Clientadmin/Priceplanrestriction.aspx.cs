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

public partial class Priceplanrestriction : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["ClientId"] = "35";
        
        if (!IsPostBack)
        {
            lblmsg.Text = "";
            ViewState["sortOrder"] = "";
         
            FillProduct();
            ddlpversion_SelectedIndexChanged1(sender, e);

        }
    }
    protected void FillProduct()
    {

        string strcln = " SELECT distinct ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlproduct.DataSource = dtcln;

        ddlproduct.DataValueField = "VersionInfoId";
        ddlproduct.DataTextField = "productversion";
        ddlproduct.DataBind();
        ddlproduct.Items.Insert(0, "-Select-");
        ddlproduct.Items[0].Value = "0";

        ddltable.Items.Insert(0, "-Select-");
        ddltable.Items[0].Value = "0";
        ddlfeildrest.Items.Insert(0, "-Select-");
        ddlfeildrest.Items[0].Value = "0";
        ddlportal.Items.Insert(0, "-Select-");
        ddlportal.Items[0].Value = "0";
        //ddlpriceplan.Items.Insert(0, "-Select-");
        //ddlpriceplan.Items[0].Value = "0";

        ddlpversion.DataSource = dtcln;
        ddlpversion.DataValueField = "VersionInfoId";
        ddlpversion.DataTextField = "productversion";
        ddlpversion.DataBind();


        //ddlpversion.Items.Insert(0, "All");
        //ddlpversion.Items[0].Value = "0";
        ddlfilterbyportal.Items.Insert(0, "All");
        ddlfilterbyportal.Items[0].Value = "0";
        //ddlfltprice.Items.Insert(0, "All");
        //ddlfltprice.Items[0].Value = "0";
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnsubmit.Text == "Update")
            {
                DataTable dtc = select("Select * from PriceplanrestrictionTbl where PortalId='" + ddlportal.SelectedValue + "' and ProductversionId='" + ddlproduct.SelectedValue + "' and TableId='" + ddltable.SelectedValue + "' and NameofRest='" + txtnameofrest.Text + "' and TextofQueinSelection='" + txttxtquestpps.Text + "' and Id<>'" + ViewState["pfid"] + "'");
                if (dtc.Rows.Count == 0)
                {
                    SqlCommand cmd = new SqlCommand("Update PriceplanrestrictionTbl set RestGroupname='"+ txtrestgroupname.Text+"', PortalId='" + ddlportal.SelectedValue + "',FieldrestrictionSet='" + ddlfeildrest.SelectedItem.Text + "',RestrictfieldId='" + txtfieldIdRest.Text + "', Restingroup='" + txtrestgroup.Text + "',Priceaddingroup='" + txtaddprice.Text + "', ProductversionId='" + ddlproduct.SelectedValue + "',TableId='" + ddltable.SelectedValue + "', NameofRest='" + txtnameofrest.Text + "', TextofQueinSelection='" + txttxtquestpps.Text + "' where Id='" + ViewState["pfid"] + "' ", con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }

                    int result = cmd.ExecuteNonQuery();

                    con.Close();
                    if (result > 0)
                    {
                        lblmsg.Visible = true;
                        btnsubmit.Text = "Submit";
                        lblmsg.Text = "Record updated successfully. ";
                        FillGrid();
                        txttxtquestpps.Text = "";
                        txtnameofrest.Text = "";
                        txtaddprice.Text = "";
                        txtrestgroup.Text = "";
                        ddltable.SelectedIndex = 0;
                        ddlportal.SelectedValue = "0";
                        ddlfeildrest.SelectedIndex = 0;
                        txtfieldIdRest.Text = "";
                        txtrestgroupname.Text = "";
                        addnewpanel.Visible = true;
                        pnladdnew.Visible = false;
                        Label2.Text = "";
                        ModernpopSync.Show();
                    }
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record is already existed.";
                }
            }
            else
            {
                DataTable dtc = select("Select * from PriceplanrestrictionTbl where PortalId='" + ddlportal.SelectedValue + "' and ProductversionId='" + ddlproduct.SelectedValue + "' and TableId='" + ddltable.SelectedValue + "' and NameofRest='" + txtnameofrest.Text + "' and TextofQueinSelection='" + txttxtquestpps.Text + "'");
                if (dtc.Rows.Count == 0)
                {
                    SqlCommand cmd = new SqlCommand("Insert into PriceplanrestrictionTbl(ProductversionId,TableId,NameofRest,TextofQueinSelection,Restingroup,Priceaddingroup,PortalId,FieldrestrictionSet,RestrictfieldId,RestGroupname)Values('" + ddlproduct.SelectedValue + "','" + ddltable.SelectedValue + "','" + txtnameofrest.Text + "','" + txttxtquestpps.Text + "','" + txtrestgroup.Text + "','" + txtaddprice.Text + "','" + ddlportal.SelectedValue + "','" + ddlfeildrest.SelectedItem.Text + "','" + txtfieldIdRest.Text + "','"+txtrestgroupname.Text+"')", con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    Int32 result = cmd.ExecuteNonQuery();


                    con.Close();
                    if (result > 0)
                    {
                        Label2.Text = "";
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully.";
                        FillGrid();
                        txttxtquestpps.Text = "";
                        txtnameofrest.Text = "";
                        txtaddprice.Text = "";
                        txtrestgroup.Text = "";
                        ddltable.SelectedIndex = 0;
                        ddlportal.SelectedValue = "0";
                        ddlfeildrest.SelectedIndex = 0;
                        txtfieldIdRest.Text = "";
                        txtrestgroupname.Text = "";
                        addnewpanel.Visible = true;
                        pnladdnew.Visible = false;
                        ModernpopSync.Show();
                    }

                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record not added.";
                    }
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record is already existed.";
                }

            }


        }
        catch
        {
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
    protected void FillGrid()
    {


        string filte = "";
        if (ddlpversion.SelectedIndex > -1)
        {
            filte = " and ClientProductTableMaster.VersionInfoId='" + ddlpversion.SelectedValue + "'";
        } if (ddlfilterbyportal.SelectedIndex > 0)
        {
            filte = filte + " and PriceplanrestrictionTbl.PortalId='" + ddlfilterbyportal.SelectedValue + "'";
        }
        DataTable dtcln = select("Select PriceplanrestrictionTbl.Id, ProductMaster.ProductName+':'+VersionInfoMaster.VersionInfoName as productversion,TableName,NameofRest,TextofQueinSelection,Priceaddingroup,Restingroup from ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=  ProductMaster.ProductId  inner join ClientProductTableMaster on ClientProductTableMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join PriceplanrestrictionTbl on PriceplanrestrictionTbl.TableId=ClientProductTableMaster.Id where ClientMasterId='" + Session["ClientId"] + "' " + filte);
       
        GridView1.DataSource = dtcln;
        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
            int Id = Convert.ToInt32(e.CommandArgument);
            int i = Id;
            ViewState["pfid"] = i.ToString();


            DataTable dtcln = select("Select * from PriceplanrestrictionTbl where Id='" + ViewState["pfid"] + "'");
        
            if (dtcln.Rows.Count > 0)
            {
                Label2.Text = "Edit Price Plan Restriction";
                addnewpanel.Visible = false;
                pnladdnew.Visible = true;
                txtnameofrest.Text = Convert.ToString(dtcln.Rows[0]["NameofRest"]);
                txttxtquestpps.Text = Convert.ToString(dtcln.Rows[0]["TextofQueinSelection"]);
                ddlproduct.SelectedValue = Convert.ToString(dtcln.Rows[0]["ProductversionId"]);
                ddlproduct_SelectedIndexChanged(sender, e);
                ddltable.SelectedValue = Convert.ToString(dtcln.Rows[0]["TableId"]);
                ddltable_SelectedIndexChanged(sender, e);
                txtaddprice.Text = Convert.ToString(dtcln.Rows[0]["Priceaddingroup"]);
                txtrestgroupname.Text = Convert.ToString(dtcln.Rows[0]["RestGroupname"]);
                txtrestgroup.Text = Convert.ToString(dtcln.Rows[0]["Restingroup"]);
               ddlportal.SelectedIndex=ddlportal.Items.IndexOf(ddlportal.Items.FindByValue(Convert.ToString(dtcln.Rows[0]["PortalId"])));
               ddlfeildrest.SelectedIndex = ddlfeildrest.Items.IndexOf(ddlfeildrest.Items.FindByText(Convert.ToString(dtcln.Rows[0]["FieldrestrictionSet"])));
               txtfieldIdRest.Text = Convert.ToString(dtcln.Rows[0]["RestrictfieldId"]);
                btnsubmit.Text = "Update";
            }
        }
        else if (e.CommandName == "delete")
        {
            int i = Convert.ToInt32(e.CommandArgument);
         
                SqlCommand cd4 = new SqlCommand("Delete from  PriceplanrestrictionTbl where Id='" + i + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cd4.ExecuteNonQuery();
                con.Close();
                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully.";
                FillGrid();
           
           

        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGrid();
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
    protected void btnedit_Click(object sender, EventArgs e)
    {
        Label2.Text = "";
        btnsubmit.Text = "Submit";
        ddlproduct.SelectedIndex=0;
        ddlproduct_SelectedIndexChanged(sender, e);
        txttxtquestpps.Text = "";
        txtnameofrest.Text = "";
        ddltable.SelectedIndex = 0;
        ddlportal.SelectedValue = "0";
        ddlfeildrest.SelectedIndex = 0;
        txtfieldIdRest.Text = "";
        lblmsg.Text = "";
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        txtrestgroupname.Text = "";

    }
    protected void ddlpversion_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlportal.Items.Clear();
        ddltable.Items.Clear();
        //ddlpriceplan.Items.Clear();
       if (ddlproduct.SelectedIndex > 0)
        {

            string strcln22 = "Select Distinct * from ClientProductTableMaster inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId= ClientProductTableMaster.VersionInfoId inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId   where ClientProductTableMaster.VersionInfoId = '" + ddlproduct.SelectedValue + "' order by TableName";
            SqlCommand cmdcln22 = new SqlCommand(strcln22, con);
            DataTable dtcln22 = new DataTable();
            SqlDataAdapter adpcln22 = new SqlDataAdapter(cmdcln22);
            adpcln22.Fill(dtcln22);
            ddltable.DataSource = dtcln22;

            ddltable.DataValueField = "Id";
            ddltable.DataTextField = "TableName";
            ddltable.DataBind();

            string strcln22v = "Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + ddlproduct.SelectedValue + "' ) order by PortalName";
            SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
            DataTable dtcln22v = new DataTable();
            SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
            adpcln22v.Fill(dtcln22v);
            ddlportal.DataSource = dtcln22v;

            ddlportal.DataValueField = "Id";
            ddlportal.DataTextField = "PortalName";
            ddlportal.DataBind();

        }
       ddltable.Items.Insert(0, "-Select-");
       ddltable.Items[0].Value = "0";
       ddlportal.Items.Insert(0, "-Select-");
       ddlportal.Items[0].Value = "0";
      
        //ddlpriceplan.Items.Insert(0, "-Select-");
        //ddlpriceplan.Items[0].Value = "0";
    }
    protected void ddlpversion_SelectedIndexChanged1(object sender, EventArgs e)
    {
        
        fillporfil();
        ddlfilterbyportal_SelectedIndexChanged(sender, e);
      
        
    }
    protected void fillporfil()
    {
            ddlfilterbyportal.Items.Clear();
            string strcln22v = "Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + ddlpversion.SelectedValue + "' ) order by PortalName";
            SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
            DataTable dtcln22v = new DataTable();
            SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
            adpcln22v.Fill(dtcln22v);
            ddlfilterbyportal.DataSource = dtcln22v;

            ddlfilterbyportal.DataValueField = "Id";
            ddlfilterbyportal.DataTextField = "PortalName";
            ddlfilterbyportal.DataBind();


            ddlfilterbyportal.Items.Insert(0, "All");
            ddlfilterbyportal.Items[0].Value = "0";
    }



    protected void ddlfltprice_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }


    protected void ddltable_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlfeildrest.Items.Clear();
        string strcln22 = "Select  Id, feildname from tablefielddetail  where tableId = '" + ddltable.SelectedValue + "' order by feildname";
        SqlCommand cmdcln22 = new SqlCommand(strcln22, con);
        DataTable dtcln22 = new DataTable();
        SqlDataAdapter adpcln22 = new SqlDataAdapter(cmdcln22);
        adpcln22.Fill(dtcln22);
        ddlfeildrest.DataSource = dtcln22;

        ddlfeildrest.DataValueField = "Id";
        ddlfeildrest.DataTextField = "feildname";
        ddlfeildrest.DataBind();

        ddlfeildrest.Items.Insert(0, "-Select-");
        ddlfeildrest.Items[0].Value = "0";
        
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {
        string te = "ClientProductTableDetails.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        ddltable.Items.Clear();
        string strcln22 = "Select * from ClientProductTableMaster inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId= ClientProductTableMaster.VersionInfoId inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId   where ProductMaster.ProductId = '" + ddlproduct.SelectedValue + "' order by TableName";
        SqlCommand cmdcln22 = new SqlCommand(strcln22, con);
        DataTable dtcln22 = new DataTable();
        SqlDataAdapter adpcln22 = new SqlDataAdapter(cmdcln22);
        adpcln22.Fill(dtcln22);
        ddltable.DataSource = dtcln22;

        ddltable.DataValueField = "Id";
        ddltable.DataTextField = "TableName";
        ddltable.DataBind();
    }
    protected void ddlfilterbyportal_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void btndosyncro_Click(object sender, EventArgs e)
    {
        if (rdsync.SelectedValue == "1")
        {
            int transf = 0;
            DataTable dt1 = select("SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ClientProductTableMaster.TableName='PriceplanrestrictionTbl' ");//and ClientProductTableMaster.VersionInfoId='" + ddlproduct.SelectedValue + "' and SatelliteSyncronisationrequiringTablesMaster.ProductVersionID='" + ddlproduct.SelectedValue + "'
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string datetim = DateTime.Now.ToString();
                    string arqid = dt1.Rows[i]["Id"].ToString();

                    string str22 = "Insert Into SyncronisationrequiredTbl(SatelliteSyncronisationrequiringTablesMasterID,DateandTime)Values('" + arqid + "','" + Convert.ToDateTime(datetim) + "')";
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmn = new SqlCommand(str22, con);
                    cmn.ExecuteNonQuery();
                    con.Close();

                    DataTable dt121 = select("SELECT Max(ID) as ID from SyncronisationrequiredTbl where SatelliteSyncronisationrequiringTablesMasterID='" + arqid + "'");

                    if (Convert.ToString(dt121.Rows[0]["ID"]) != "")
                    {
                        DataTable dtcln = select("SELECT Distinct ServerMasterTbl.Id FROM ServerMasterTbl inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId    where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' ");//and  PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "'

                        for (int j = 0; j < dtcln.Rows.Count; j++)
                        {
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            transf = Convert.ToInt32(ddlproduct.SelectedValue);
                            string str223 = "Insert Into SateliteServerRequiringSynchronisationMasterTbl(SyncronisationrequiredTBlID,[servermasterID],[SynchronisationSuccessful],[SynchronisationSuccessfulDatetime])Values('" + dt121.Rows[0]["ID"] + "','" + dtcln.Rows[j]["Id"] + "','0','" + DateTime.Now.ToString() + "')";
                            SqlCommand cmn3 = new SqlCommand(str223, con);
                            cmn3.ExecuteNonQuery();
                            con.Close();
                            transf = Convert.ToInt32(rdsync.SelectedValue);
                        }
                    }


                }

            }


            if (transf > 0)
            {
                string te = "SyncData.aspx?verId=" + transf;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

            }
        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        Label2.Text = "Add Price Plan Restriction";
        lblmsg.Text = "";
    }
    protected void btndosyncro_Clickpop(object sender, EventArgs e)
    {
        ModernpopSync.Show();
    }
}

