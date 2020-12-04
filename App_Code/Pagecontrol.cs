using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Common;

/// <summary>
/// Summary description for Pagecontrol
/// </summary>
public class Pagecontrol
{
    private static Control myC;
	public Pagecontrol()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void GetControls(Control c, string FindControl)
    {
       
         
        foreach (Control cc in c.Controls)
        {
           
            if (cc.ID == FindControl)
            {
                myC = cc;
                
                break;

            }

            if (cc.Controls.Count > 0)
                GetControls(cc, FindControl);


        }

    }


    public static void dypcontrol(Page conpage, string pagen)
    {

        string str1211 = "SELECT   distinct   Control_type_Master.Type_name,PageControlMaster.ControlName,PageControlPricePlanCoordination.ControlLabel FROM DynamicPageControlScheme inner join PageControlPricePlanCoordination on PageControlPricePlanCoordination.DynamicPageControlMasterSchemeID = DynamicPageControlScheme.ID inner join   PageControlMaster  on PageControlMaster.PageControl_id=PageControlPricePlanCoordination.PageControlID inner join PageMaster on PageControlMaster.Page_id=PageMaster.PageId INNER JOIN Control_type_Master ON PageControlMaster.ControlType_id = Control_type_Master.Type_id where DynamicPageControlScheme.PricePlanID='" + HttpContext.Current.Session["PriceId"] + "' and PageMaster.VersionInfoMasterId='" + (HttpContext.Current.Session["VerId"]) + "' and  PageMaster.PageName='" + pagen + "'";
        SqlDataAdapter da1211 = new SqlDataAdapter(str1211, PageConn.busclient());
        DataTable dt1211 = new DataTable();
        da1211.Fill(dt1211);
        foreach (DataRow dsc in dt1211.Rows)
        {
            String StrVal = Convert.ToString(dsc["Type_Name"]);
            String StrName = Convert.ToString(dsc["ControlName"]);
            String ControlLabel = Convert.ToString(dsc["ControlLabel"]);
            GetControls(conpage, StrName);
        
            if (myC is Button)
            {
                Button myLabel = (Button)Convert.ChangeType(myC, typeof(Button));
                if (myLabel.ID == Convert.ToString(StrName))
                {
                    myLabel.Text = ControlLabel;
                }
            }
            else if (myC is  GridView)
            {
                GridView myLabel = (GridView)Convert.ChangeType(myC, typeof(GridView));
                if (myLabel.ID == Convert.ToString(StrName))
                {
                    for (int hr = 0; hr < myLabel.Columns.Count; hr++)
                    {
                        if (myLabel.Columns[hr].HeaderText.ToString() ==Convert.ToString( dsc["Type_Name"]))
                        {

                            myLabel.Columns[hr].Visible = false;
                        }
                    }

                }
            }
            else if (myC is LinkButton)
            {
                LinkButton myLabel = (LinkButton)Convert.ChangeType(myC, typeof(LinkButton));
                if (myLabel.ID == Convert.ToString(StrName))
                {
                    myLabel.Text = ControlLabel;
                }
            }
            else if (myC is LinkButton)
            {
                LinkButton myLabel = (LinkButton)Convert.ChangeType(myC, typeof(LinkButton));
                if (myLabel.ID == Convert.ToString(StrName))
                {
                    myLabel.Text = ControlLabel;
                }
            }
            else if (myC is ImageButton)
            {
                ImageButton myLabel = (ImageButton)Convert.ChangeType(myC, typeof(ImageButton));
                if (myLabel.ID == Convert.ToString(StrName))
                {
                    myLabel.AlternateText = ControlLabel;
                }
            }
            else if (myC is TextBox)
            {
                TextBox myLabel = (TextBox)Convert.ChangeType(myC, typeof(TextBox));
                if (myLabel.ID == Convert.ToString(StrName))
                {
                    myLabel.Text = ControlLabel;
                }
            }
            else if (myC is Label)
            {
                Label myLabel = (Label)Convert.ChangeType(myC, typeof(Label));
                if (myLabel.ID == Convert.ToString(StrName))
                {
                    myLabel.Text = ControlLabel;
                }
            }
            else if (myC is CheckBox)
            {
                CheckBox myLabel = (CheckBox)Convert.ChangeType(myC, typeof(CheckBox));
                if (myLabel.ID == Convert.ToString(StrName))
                {
                    myLabel.Text = ControlLabel;
                }
            }
            else if (myC is CheckBoxList)
            {
                CheckBoxList myLabel = (CheckBoxList)Convert.ChangeType(myC, typeof(CheckBoxList));
                if (myLabel.ID == Convert.ToString(StrName))
                {
                    myLabel.Text = ControlLabel;
                }
            }
            else if (myC is RadioButton)
            {
                RadioButton myLabel = (RadioButton)Convert.ChangeType(myC, typeof(RadioButton));
                if (myLabel.ID == Convert.ToString(StrName))
                {
                    myLabel.Text = ControlLabel;
                }
            }
            else if (myC is RadioButtonList)
            {
                RadioButtonList myLabel = (RadioButtonList)Convert.ChangeType(myC, typeof(RadioButtonList));
                if (myLabel.ID == Convert.ToString(StrName))
                {
                    myLabel.Text = ControlLabel;
                }
            }
        }

    }
       
}
