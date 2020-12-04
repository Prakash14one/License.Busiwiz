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

public partial class Account_UserControl_ExternalInternalMessageacc : System.Web.UI.UserControl
{
    protected static string empeml = "";
    MasterCls clsMaster = new MasterCls();
    protected void Page_Load(object sender, EventArgs e)
    {

        ddlempemail.Visible = true;
        if (!IsPostBack)
        {
            fillddl();
            
        }
        if (ddlempemail.SelectedIndex == 0)
        {
            HttpContext.Current.Session["slctype"] = "employ";
        }
       
    }
    protected void rbtnlistsetrules_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnlistsetrules.SelectedIndex > 0)
        {
            /*if (rbtnlistsetrules.SelectedItem.Text == "External Message")
            {
                ddlempemail.Visible = true;
                fillddl();
                Session["msgtype"] = "Ext";
                Response.Redirect("~/Account/MessageInbox.aspx");
                //Response.Redirect("~/Account/MessageInboxExt.aspx");
            }
            else if (rbtnlistsetrules.SelectedItem.Text == "Internal Message")
            {
                ddlempemail.Visible = false;
                Session["msgtype"] = "Int";
                Response.Redirect("~/Account/MessageInbox.aspx");

            }*/
            if (rbtnlistsetrules.SelectedIndex==1)
            {
                //ddlempemail.Visible = true;
                //fillddl();
                //Session["msgtype"] = "Ext";
                //rbtnlistsetrules.SelectedItem.Text = "External Message";
                //Response.Redirect("~/Account/MessageInbox.aspx");
                Response.Redirect("~/ShoppingCart/Admin/MessageInboxExt.aspx");
                
            }
            else if (rbtnlistsetrules.SelectedIndex==0)
            {
                
                //Session["msgtype"] = "Int";
                //rbtnlistsetrules.SelectedItem.Text = "Internal Message";
                Response.Redirect("~/ShoppingCart/Admin/MessageInbox.aspx");
                
            }
        }
        
    }
    protected void fillddl()
    {
        DataTable dtcomemail = new DataTable();
        DataTable dtempemail = new DataTable();
        
        
        dtempemail = clsMaster.SelectEmpEmail(Convert.ToInt32(Session["EmployeeId"]));
        if (dtempemail.Rows.Count > 0)
        {
            if (dtempemail.Rows[0]["Email"] != System.DBNull.Value)
            {
                empeml = dtempemail.Rows[0]["Email"].ToString();
            }
        }
        dtcomemail = clsMaster.SelectCompanyEmailForEmp(Convert.ToInt32(Session["EmployeeId"]));
        ddlempemail.DataSource = dtcomemail;
        ddlempemail.DataBind();
        ddlempemail.Items.Insert(0, "-Select-");
        ddlempemail.SelectedItem.Value = "0";
        if (empeml != "")
        {
            ddlempemail.SelectedItem.Value = "1";
            ddlempemail.Items.Insert(1, empeml);                       
        }
        
    }
    protected void rbtnlistsetrules_TextChanged(object sender, EventArgs e)
    {
        if (rbtnlistsetrules.SelectedIndex == 1)
        {
            
            //fillddl();
            //Session["msgtype"] = "Ext";
            //rbtnlistsetrules.SelectedItem.Text = "External Message";
            //Response.Redirect("~/Account/MessageInbox.aspx");
            Response.Redirect("~/ShoppingCart/Admin/MessageInboxExt.aspx");
            
        }
        else if (rbtnlistsetrules.SelectedIndex == 0)
        {
            
            //Session["msgtype"] = "Int";
            //rbtnlistsetrules.SelectedItem.Text = "Internal Message";
            Response.Redirect("~/ShoppingCart/Admin/MessageInbox.aspx");

        }
    }
    //protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    //{
       
    //    //Session["msgtype"] = "Int";
    //    //rbtnlistsetrules.SelectedItem.Text = "Internal Message";
    //    RadioButton1.Checked = true;
        
    //    Response.Redirect("~/ShoppingCart/Admin/MessageInbox.aspx");

    //}

    protected void ddlempemail_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (empeml != "")
        //{
            //if (ddlempemail.SelectedItem.Value == empeml)
            if (ddlempemail.SelectedIndex==1)
            {
                HttpContext.Current.Session["slctype"] = "employ";
                //HttpContext.Current.Session["Company"] = "2";
                HttpContext.Current.Session["PartyId"] = Session["PartyId1"];
            }
            else if(ddlempemail.SelectedIndex>1)
            {
                HttpContext.Current.Session["slctype"] = "cmpny";
                HttpContext.Current.Session["orgemlid"] = Convert.ToString(ddlempemail.SelectedValue);
                DataTable dtorgeml = new DataTable();
                dtorgeml = clsMaster.SelectPartyIdfromCompanyEmail(Convert.ToInt32(ddlempemail.SelectedValue));
                //HttpContext.Current.Session["Company"] = "1";
                if (dtorgeml.Rows.Count > 0)
                {
                    if (dtorgeml.Rows[0][1] != System.DBNull.Value)
                    {
                        HttpContext.Current.Session["PartyId"] = dtorgeml.Rows[0][1].ToString();
                    }
                }
                //int i = Convert.ToInt32(ddlempemail.SelectedItem.Value);
                //HttpContext.Current.Session["PartyId"] = i.ToString();                
            }
        //}
        //else
        //{

                //HttpContext.Current.Session["Company"] = "1";
                //int i = Convert.ToInt32(ddlempemail.SelectedItem.Value);
                //HttpContext.Current.Session["PartyId"] = i.ToString();
               ;
            
        //}
        
    }
    protected void btnexternal_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/MessageInbox.aspx");
    }
}
