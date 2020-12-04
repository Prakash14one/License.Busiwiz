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
using AjaxControlToolkit;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Common;
using System.Xml;
using System.IO;
using Microsoft.SqlServer.Management.Smo;
using System.Security.Cryptography;

public partial class MasterPage : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ToString());
  // SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
   SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString1"].ToString());
    private Control myC;
    protected void Page_Load(object sender, EventArgs e)
    {
       
         ModalPopupExtender1.Hide();
        ModalPopupExtender1222.Hide();
        string strData = Request.Url.LocalPath.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Session["pagename"] = page.ToString();

       


        //RestrictPriceplan(page);
        //RestrictionRole(page);

        
        if (!IsPostBack)
        {
            //if (ViewState["PanIndex"] != null)
            //{
            //    Accordion1.SelectedIndex = Convert.ToInt16(ViewState["PanIndex"].ToString());
            //}
            //else
            //{
            //    Accordion1.SelectedIndex = 0;
            //}
            if (Request.UrlReferrer != null)
            {

               // Accordion1.SelectedIndex =0 ;
                ViewState["p1"] = Request.UrlReferrer.ToString();
                FillLogos();

                //if (Session.Count > 0 || Session["userid"] != null)
                //{
                //    string str = "SELECT  PartytTypeMaster.PartType " +
                //       "  FROM         PartytTypeMaster RIGHT OUTER JOIN " +
                //       "         Party_master ON PartytTypeMaster.PartyTypeId = Party_master.PartyTypeId RIGHT OUTER JOIN " +
                //       "         User_master ON Party_master.PartyID = User_master.PartyID RIGHT OUTER JOIN " +
                //       "         Login_master ON User_master.UserID = Login_master.UserID " +
                //       "  WHERE        (Login_master.UserID ='" + Session["userid"].ToString() + "')";

                //    SqlDataAdapter da = new SqlDataAdapter(str, con);
                //    DataTable dt = new DataTable();
                //    da.Fill(dt);
                //    if (dt.Rows.Count > 0)
                //    {
                //        if (dt.Rows[0][0].ToString() == "ADMIN")
                //        {
                //            //AccordionPane10.Visible = true;
                //        }
                //        else
                //        {
                //           // AccordionPane10.Visible = false;
                //        }
                //    }
                //}
                //else
                //{
                //    Response.Redirect("ShoppingCartLogin.aspx");
                //}
                //Session["pnl1"] = "8";

                //if (Session["userid"] != null)
                //{

                //    string str = "SELECT     PageName, PageTypeId, PageId, PageTitle, PageDescription, PageIndex " +
                //                " FROM         PageMaster " +
                //            " WHERE     (PageName like '%" + page.ToString() + "%')";
                //    SqlCommand cmd = new SqlCommand(str, con);
                //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //    DataTable dt = new DataTable();
                //    adp.Fill(dt);
                //    Accordion accordioMenu = (Accordion)this.FindControl("Accordion1");
                //    if (dt.Rows.Count > 0)
                //    {

                //        accordioMenu.SelectedIndex = Convert.ToInt32(dt.Rows[0]["PageIndex"]);
                //    }
                //    else
                //    {
                //        accordioMenu.SelectedIndex = 0;

                //    }
                //    //AccordionPane aaa = new AccordionPane();



                //}
                //else
                //{
                //    Response.Redirect("ShoppingCartLogin.aspx");
                //}
            }
            else
            {
                Response.Redirect("ShoppingCartLogin.aspx");
            }
        }
    }
    protected void lnk5_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/ShoppingCart/Default.aspx");
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //Session["ctrl"] = Panel123;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

    }
    protected void FillLogos()
    {
        string strMainRedirect = " SELECT     CompanyWebsitMaster.Sitename, CompanyAddressMaster.WebSite, CompanyMaster.CompanyId, CompanyMaster.CompanyLogo, " +
                     "  CompanyWebsiteAddressMaster.LiveChatUrl,CompanyMaster.CompanyName " +
                     " FROM         CompanyWebsitMaster LEFT OUTER JOIN " +
                     " CompanyWebsiteAddressMaster ON  " +
                     " CompanyWebsitMaster.CompanyWebsiteMasterId = CompanyWebsiteAddressMaster.CompanyWebsiteMasterId RIGHT OUTER JOIN " +
                     " CompanyMaster ON CompanyWebsitMaster.CompanyId = CompanyMaster.CompanyId LEFT OUTER JOIN " +
                     " CompanyAddressMaster ON CompanyMaster.CompanyId = CompanyAddressMaster.CompanyMasterId ";
        SqlCommand cmdRedirect = new SqlCommand(strMainRedirect, con);
        SqlDataAdapter adpRedirect = new SqlDataAdapter(cmdRedirect);
        DataTable dtRedirect = new DataTable();
        adpRedirect.Fill(dtRedirect);
        if (dtRedirect.Rows.Count > 0)
        {

            mainloginlogo.ImageUrl = "~/ShoppingCart/images/" + dtRedirect.Rows[0]["CompanyLogo"].ToString();
            // RightImage.ImageUrl = "~/ShoppingCart/images/"+dtRedirect.Rows[0]["LiveChatUrl"].ToString();

        }
        else
        {

            mainloginlogo.ImageUrl = "#";
            // RightImage.ImageUrl = "#";

        }

    }

    public void GetControls(Control c, string FindControl)
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
    private string Decrypt(string strText, string strEncrypt)
    {
        byte[] bKey = new byte[20];
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string decryptstring(string str)
    {
        return Decrypt(str, "&%#@?,:*");
    }
    public void RestrictionRole(string pg)
    {
        //PricePlanMasterID;
        if (!IsPostBack)
        {
            if (Session.Count > 0 || Session["userid"] != "")
            {

                //if (Request.UrlReferrer.ToString() != "")
                //{
                if (Request.UrlReferrer != null)
                {
                    ViewState["p1"] = Request.UrlReferrer.ToString();

                    string str = "SELECT  LUPD,MP, CID, PID, V FROM Lmaster";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(str, con);
                    da.Fill(dt);

                    int PPlan;
                    //PPlan = Convert.ToInt32(dt.Rows[0][0].ToString());
                   PPlan = Convert.ToInt32(decryptstring(dt.Rows[0][0].ToString()));

                    string str1 = "SELECT     PricePlanName, PricePlanId FROM PricePlanMaster where PricePlanId='" + PPlan + "'";
                    SqlDataAdapter da1 = new SqlDataAdapter(str1, con1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    int priceplanid;
                    priceplanid = Convert.ToInt32(dt1.Rows[0][1].ToString());
                    //int LMastlimit = Convert.ToInt32(decryptstring(dt.Rows[0][0].ToString()));


                    //PPlan = Convert.ToInt32(dt.Rows[0][0].ToString());

                    if (PPlan == priceplanid)
                    {


                        string str11 = "Select PageId from PageMaster where PageName='" + pg + "'";
                        SqlCommand cmd = new SqlCommand(str11, con);
                        SqlDataAdapter da11 = new SqlDataAdapter(cmd);
                        DataTable dt11 = new DataTable();
                        da11.Fill(dt11);
                        if (dt11.Rows.Count > 0)
                        {
                            if (Session.Count > 0 )
                            {

                                string userroledeactive = "SELECT     ActiveDeactive, Role_id, User_id, User_Role_id FROM User_Role WHERE User_id = " + Session["userid"] + "";
                                SqlCommand cmd1 = new SqlCommand(userroledeactive, con);
                                SqlDataAdapter da111 = new SqlDataAdapter(cmd1);
                                DataTable dt111 = new DataTable();
                                da111.Fill(dt111);
                                if (dt111.Rows.Count > 0)
                                {
                                    if (dt111.Rows[0][0].ToString() == "True")
                                    {


                                        if (dt11.Rows.Count > 0)
                                        {


                                            string str121 = "SELECT     Role_Page_Access.Page_id, Role_Page_Access.ActiveDeactive, Role_Page_Access.Role_id, User_master.UserID " +
                                                            "FROM         Role_Page_Access INNER JOIN " +
                                                            "User_Role ON Role_Page_Access.Role_id = User_Role.Role_id INNER JOIN " +
                                                            "User_master ON User_Role.User_id = User_master.UserID " +
                                                            "WHERE     Role_Page_Access.Page_id = " + dt11.Rows[0][0] + " AND (User_master.UserID =" + Session["userid"] + ")";
                                            SqlDataAdapter da121 = new SqlDataAdapter(str121, con);
                                            DataTable dt121 = new DataTable();
                                            da121.Fill(dt121);



                                            int i;
                                            for (i = 0; i <= dt121.Rows.Count - 1; i++)
                                            {
                                                if (dt121.Rows[i]["ActiveDeactive"].ToString() == "False")
                                                {
                                                    //Response.Write("<script>alert('This Page Can Not Be Accessed')</script>");
                                                    Panel2.BackColor = System.Drawing.Color.Lavender;
                                                    // Panel2.BorderStyle = System.
                                                    Label4.ForeColor = System.Drawing.Color.Black;
                                                    Label1.ForeColor = System.Drawing.Color.Black;
                                                    LinkButton15.BackColor = System.Drawing.Color.Blue;
                                                    ImageButton3.Visible = true;
                                                    ModalPopupExtender1.Show();
                                                }
                                                else
                                                {
                                                    string str1211 = "SELECT     Role_Page_Contreol_Access.Role_id, Role_Page_Contreol_Access.Page_Control_id, Role_Page_Contreol_Access.Page_id,  " +
                                                                     "Role_Page_Contreol_Access.ActiveDeactive, Role_Page_Contreol_Access.id, Control_type_Master.Type_id, Control_type_Master.Type_name,PageControlMaster.ControlName " +
                                                                     "FROM         PageControlMaster INNER JOIN " +
                                                                      "Control_type_Master ON PageControlMaster.ControlType_id = Control_type_Master.Type_id INNER JOIN " +
                                                                      "Role_Page_Contreol_Access ON PageControlMaster.PageControl_id = Role_Page_Contreol_Access.Page_Control_id where  Role_Page_Contreol_Access.Page_id=" + dt11.Rows[0][0];
                                                    SqlDataAdapter da1211 = new SqlDataAdapter(str1211, con);
                                                    DataTable dt1211 = new DataTable();
                                                    da1211.Fill(dt1211);




                                                    int i1;
                                                    for (i1 = 0; i1 <= dt1211.Rows.Count - 1; i1++)
                                                    {


                                                        if (dt1211.Rows[i1][3].ToString() == "False")
                                                        {
                                                            string str1311 = "Select Type_Name from Control_type_Master where Type_id=" + dt1211.Rows[i1][5];
                                                            SqlDataAdapter da1311 = new SqlDataAdapter(str1311, con);
                                                            DataTable dt1311 = new DataTable();
                                                            da1311.Fill(dt1311);

                                                            String StrVal = dt1311.Rows[0][0].ToString();
                                                            String StrName = dt1211.Rows[i1][7].ToString();
                                                            GetControls(this, StrName);
                                                            if (StrVal == "RadioButtonList")
                                                            {
                                                                RadioButtonList myLabel = (RadioButtonList)Convert.ChangeType(myC, typeof(RadioButtonList));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "ListBox")
                                                            {
                                                                ListBox myLabel = (ListBox)Convert.ChangeType(myC, typeof(ListBox));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "HyperLink")
                                                            {
                                                                HyperLink myLabel = (HyperLink)Convert.ChangeType(myC, typeof(HyperLink));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "LinkButton")
                                                            {
                                                                LinkButton myLabel = (LinkButton)Convert.ChangeType(myC, typeof(LinkButton));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "ImageButton")
                                                            {
                                                                ImageButton myLabel = (ImageButton)Convert.ChangeType(myC, typeof(ImageButton));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "TextBox")
                                                            {
                                                                TextBox myLabel = (TextBox)Convert.ChangeType(myC, typeof(TextBox));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "Button")
                                                            {
                                                                Button myLabel = (Button)Convert.ChangeType(myC, typeof(Button));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "Panel")
                                                            {
                                                                Panel myLabel = (Panel)Convert.ChangeType(myC, typeof(Panel));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "CheckBox")
                                                            {
                                                                CheckBox myLabel = (CheckBox)Convert.ChangeType(myC, typeof(CheckBox));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "CheckBoxList")
                                                            {
                                                                CheckBoxList myLabel = (CheckBoxList)Convert.ChangeType(myC, typeof(CheckBoxList));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "Label")
                                                            {
                                                                Label myLabel = (Label)Convert.ChangeType(myC, typeof(Label));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "Image")
                                                            {
                                                                Image myLabel = (Image)Convert.ChangeType(myC, typeof(Image));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "ImageMap")
                                                            {
                                                                ImageMap myLabel = (ImageMap)Convert.ChangeType(myC, typeof(ImageMap));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "RadioButton")
                                                            {
                                                                RadioButton myLabel = (RadioButton)Convert.ChangeType(myC, typeof(RadioButton));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "DropDownList")
                                                            {
                                                                DropDownList myLabel = (DropDownList)Convert.ChangeType(myC, typeof(DropDownList));
                                                                myLabel.Enabled = false;

                                                            }
                                                            else if (StrVal == "FileUpload")
                                                            {
                                                                FileUpload myLabel = (FileUpload)Convert.ChangeType(myC, typeof(FileUpload));
                                                                myLabel.Enabled = false;

                                                            }
                                                            else if (StrVal == "PlaceHolder")
                                                            {
                                                                PlaceHolder myLabel = (PlaceHolder)Convert.ChangeType(myC, typeof(PlaceHolder));
                                                                myLabel.Visible = false;

                                                            }
                                                            else if (StrVal == "GridView")
                                                            {
                                                                GridView myLabel = (GridView)Convert.ChangeType(myC, typeof(GridView));
                                                                myLabel.Enabled = false;

                                                            }

                                                            else if (StrVal == "DataList")
                                                            {
                                                                DataList myLabel = (DataList)Convert.ChangeType(myC, typeof(DataList));
                                                                myLabel.Enabled = false;

                                                            }
                                                            else if (StrVal == "DetailsView")
                                                            {
                                                                DetailsView myLabel = (DetailsView)Convert.ChangeType(myC, typeof(DetailsView));
                                                                myLabel.Enabled = false;

                                                            }
                                                            else if (StrVal == "FormView")
                                                            {
                                                                FormView myLabel = (FormView)Convert.ChangeType(myC, typeof(FormView));
                                                                myLabel.Enabled = false;

                                                            }
                                                            else if (StrVal == "Repeater")
                                                            {
                                                                Repeater myLabel = (Repeater)Convert.ChangeType(myC, typeof(Repeater));
                                                                myLabel.Visible = false;

                                                            }
                                                            else if (StrVal == "ListView")
                                                            {
                                                                ListView myLabel = (ListView)Convert.ChangeType(myC, typeof(ListView));
                                                                myLabel.Enabled = false;

                                                            }
                                                            else if (StrVal == "DataPager")
                                                            {
                                                                DataPager myLabel = (DataPager)Convert.ChangeType(myC, typeof(DataPager));
                                                                myLabel.Visible = false;

                                                            }
                                                            else if (StrVal == "LinqDataSource")
                                                            {
                                                                LinqDataSource myLabel = (LinqDataSource)Convert.ChangeType(myC, typeof(LinqDataSource));
                                                                myLabel.Visible = false;

                                                            }
                                                            else if (StrVal == "RangeValidator")
                                                            {
                                                                RangeValidator myLabel = (RangeValidator)Convert.ChangeType(myC, typeof(RangeValidator));
                                                                myLabel.Enabled = false;

                                                            }
                                                            else if (StrVal == "RangeValidator")
                                                            {
                                                                RangeValidator myLabel = (RangeValidator)Convert.ChangeType(myC, typeof(RangeValidator));
                                                                myLabel.Enabled = false;

                                                            }
                                                            else if (StrVal == "RegularExpressionValidator")
                                                            {
                                                                RegularExpressionValidator myLabel = (RegularExpressionValidator)Convert.ChangeType(myC, typeof(RegularExpressionValidator));
                                                                myLabel.Enabled = false;

                                                            }
                                                            else if (StrVal == "RequiredFieldValidator")
                                                            {
                                                                RequiredFieldValidator myLabel = (RequiredFieldValidator)Convert.ChangeType(myC, typeof(RequiredFieldValidator));
                                                                myLabel.Enabled = false;

                                                            }
                                                            else if (StrVal == "CompareValidator")
                                                            {
                                                                CompareValidator myLabel = (CompareValidator)Convert.ChangeType(myC, typeof(CompareValidator));
                                                                myLabel.Enabled = false;

                                                            }
                                                            else if (StrVal == "CustomValidator")
                                                            {
                                                                CustomValidator myLabel = (CustomValidator)Convert.ChangeType(myC, typeof(CustomValidator));
                                                                myLabel.Enabled = false;

                                                            }
                                                            else if (StrVal == "ValidationSummary")
                                                            {
                                                                ValidationSummary myLabel = (ValidationSummary)Convert.ChangeType(myC, typeof(ValidationSummary));
                                                                myLabel.Enabled = false;

                                                            }
                                                            else if (StrVal == "SiteMapPath")
                                                            {
                                                                SiteMapPath myLabel = (SiteMapPath)Convert.ChangeType(myC, typeof(SiteMapPath));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "Menu")
                                                            {
                                                                Menu myLabel = (Menu)Convert.ChangeType(myC, typeof(Menu));
                                                                myLabel.Visible = false;
                                                            }
                                                            else if (StrVal == "TreeView")
                                                            {
                                                                TreeView myLabel = (TreeView)Convert.ChangeType(myC, typeof(TreeView));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "WebPart")
                                                            {
                                                                WebPart myLabel = (WebPart)Convert.ChangeType(myC, typeof(WebPart));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "ProxyWebPart")
                                                            {
                                                                ProxyWebPart myLabel = (ProxyWebPart)Convert.ChangeType(myC, typeof(ProxyWebPart));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "WebPartZone")
                                                            {
                                                                WebPartZone myLabel = (WebPartZone)Convert.ChangeType(myC, typeof(WebPartZone));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "CatalogPart")
                                                            {
                                                                CatalogPart myLabel = (CatalogPart)Convert.ChangeType(myC, typeof(CatalogPart));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "CatalogZone")
                                                            {
                                                                CatalogZone myLabel = (CatalogZone)Convert.ChangeType(myC, typeof(CatalogZone));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "DeclarativeCatalogPart")
                                                            {
                                                                DeclarativeCatalogPart myLabel = (DeclarativeCatalogPart)Convert.ChangeType(myC, typeof(DeclarativeCatalogPart));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "PageCatalogPart")
                                                            {
                                                                PageCatalogPart myLabel = (PageCatalogPart)Convert.ChangeType(myC, typeof(PageCatalogPart));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "ImportCatalogPart")
                                                            {
                                                                ImportCatalogPart myLabel = (ImportCatalogPart)Convert.ChangeType(myC, typeof(ImportCatalogPart));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "EditorZone")
                                                            {
                                                                EditorZone myLabel = (EditorZone)Convert.ChangeType(myC, typeof(EditorZone));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "AppearanceEditorPart")
                                                            {
                                                                AppearanceEditorPart myLabel = (AppearanceEditorPart)Convert.ChangeType(myC, typeof(AppearanceEditorPart));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "BehaviorEditorPart")
                                                            {
                                                                BehaviorEditorPart myLabel = (BehaviorEditorPart)Convert.ChangeType(myC, typeof(BehaviorEditorPart));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "LayoutEditorPart")
                                                            {
                                                                LayoutEditorPart myLabel = (LayoutEditorPart)Convert.ChangeType(myC, typeof(LayoutEditorPart));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "PropertyGridEditorPart")
                                                            {
                                                                PropertyGridEditorPart myLabel = (PropertyGridEditorPart)Convert.ChangeType(myC, typeof(PropertyGridEditorPart));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "ConnectionsZone")
                                                            {
                                                                ConnectionsZone myLabel = (ConnectionsZone)Convert.ChangeType(myC, typeof(ConnectionsZone));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "UpdateProgress")
                                                            {
                                                                UpdateProgress myLabel = (UpdateProgress)Convert.ChangeType(myC, typeof(UpdateProgress));
                                                                myLabel.Visible = false;
                                                            }
                                                            else if (StrVal == "Timer")
                                                            {
                                                                Timer myLabel = (Timer)Convert.ChangeType(myC, typeof(Timer));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "UpdatePanel")
                                                            {
                                                                UpdatePanel myLabel = (UpdatePanel)Convert.ChangeType(myC, typeof(UpdatePanel));
                                                                myLabel.Visible = false;
                                                            }
                                                            else if (StrVal == "Accordion")
                                                            {
                                                                Accordion myLabel = (Accordion)Convert.ChangeType(myC, typeof(Accordion));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "UserControl")
                                                            {
                                                                UserControl myLabel = (UserControl)Convert.ChangeType(myC, typeof(UserControl));
                                                                myLabel.Visible = false;
                                                            }
                                                            else if (StrVal == "AccordionPane")
                                                            {
                                                                AccordionPane myLabel = (AccordionPane)Convert.ChangeType(myC, typeof(AccordionPane));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "AlwaysVisibleControlExtender")
                                                            {
                                                                AlwaysVisibleControlExtender myLabel = (AlwaysVisibleControlExtender)Convert.ChangeType(myC, typeof(AlwaysVisibleControlExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "AnimationExtender")
                                                            {
                                                                AnimationExtender myLabel = (AnimationExtender)Convert.ChangeType(myC, typeof(AnimationExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "AutoCompleteExtender")
                                                            {
                                                                AutoCompleteExtender myLabel = (AutoCompleteExtender)Convert.ChangeType(myC, typeof(AutoCompleteExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "CalendarExtender")
                                                            {
                                                                CalendarExtender myLabel = (CalendarExtender)Convert.ChangeType(myC, typeof(CalendarExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "CascadingDropDown")
                                                            {
                                                                CascadingDropDown myLabel = (CascadingDropDown)Convert.ChangeType(myC, typeof(CascadingDropDown));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "CollapsiblePanelExtender")
                                                            {
                                                                CollapsiblePanelExtender myLabel = (CollapsiblePanelExtender)Convert.ChangeType(myC, typeof(CollapsiblePanelExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "ConfirmButtonExtender")
                                                            {
                                                                ConfirmButtonExtender myLabel = (ConfirmButtonExtender)Convert.ChangeType(myC, typeof(ConfirmButtonExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "DragPanelExtender")
                                                            {
                                                                DragPanelExtender myLabel = (DragPanelExtender)Convert.ChangeType(myC, typeof(DragPanelExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "DropDownExtender")
                                                            {
                                                                DropDownExtender myLabel = (DropDownExtender)Convert.ChangeType(myC, typeof(DropDownExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "DropShadowExtender")
                                                            {
                                                                DropShadowExtender myLabel = (DropShadowExtender)Convert.ChangeType(myC, typeof(DropShadowExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "DynamicPopulateExtender")
                                                            {
                                                                DynamicPopulateExtender myLabel = (DynamicPopulateExtender)Convert.ChangeType(myC, typeof(DynamicPopulateExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "FilteredTextBoxExtender")
                                                            {
                                                                FilteredTextBoxExtender myLabel = (FilteredTextBoxExtender)Convert.ChangeType(myC, typeof(FilteredTextBoxExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "HoverExtender")
                                                            {
                                                                HoverExtender myLabel = (HoverExtender)Convert.ChangeType(myC, typeof(HoverExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "HoverMenuExtender")
                                                            {
                                                                HoverMenuExtender myLabel = (HoverMenuExtender)Convert.ChangeType(myC, typeof(HoverMenuExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "ListSearchExtender")
                                                            {
                                                                ListSearchExtender myLabel = (ListSearchExtender)Convert.ChangeType(myC, typeof(ListSearchExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "MaskedEditExtender")
                                                            {
                                                                MaskedEditExtender myLabel = (MaskedEditExtender)Convert.ChangeType(myC, typeof(MaskedEditExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "MaskedEditValidator")
                                                            {
                                                                MaskedEditValidator myLabel = (MaskedEditValidator)Convert.ChangeType(myC, typeof(MaskedEditValidator));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "ModalPopupExtender")
                                                            {
                                                                ModalPopupExtender myLabel = (ModalPopupExtender)Convert.ChangeType(myC, typeof(ModalPopupExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "MutuallyExclusiveCheckBoxExtender")
                                                            {
                                                                MutuallyExclusiveCheckBoxExtender myLabel = (MutuallyExclusiveCheckBoxExtender)Convert.ChangeType(myC, typeof(MutuallyExclusiveCheckBoxExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "NoBot")
                                                            {
                                                                NoBot myLabel = (NoBot)Convert.ChangeType(myC, typeof(NoBot));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "NumericUpDownExtender")
                                                            {
                                                                NumericUpDownExtender myLabel = (NumericUpDownExtender)Convert.ChangeType(myC, typeof(NumericUpDownExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "PagingBulletedListExtender")
                                                            {
                                                                PagingBulletedListExtender myLabel = (PagingBulletedListExtender)Convert.ChangeType(myC, typeof(PagingBulletedListExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "PasswordStrength")
                                                            {
                                                                PasswordStrength myLabel = (PasswordStrength)Convert.ChangeType(myC, typeof(PasswordStrength));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "PopupControlExtender")
                                                            {
                                                                PopupControlExtender myLabel = (PopupControlExtender)Convert.ChangeType(myC, typeof(PopupControlExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "Rating")
                                                            {
                                                                Rating myLabel = (Rating)Convert.ChangeType(myC, typeof(Rating));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "ReorderList")
                                                            {
                                                                ReorderList myLabel = (ReorderList)Convert.ChangeType(myC, typeof(ReorderList));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "ResizableControlExtender")
                                                            {
                                                                ResizableControlExtender myLabel = (ResizableControlExtender)Convert.ChangeType(myC, typeof(ResizableControlExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "RoundedCornersExtender")
                                                            {
                                                                RoundedCornersExtender myLabel = (RoundedCornersExtender)Convert.ChangeType(myC, typeof(RoundedCornersExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "SliderExtender")
                                                            {
                                                                SliderExtender myLabel = (SliderExtender)Convert.ChangeType(myC, typeof(SliderExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "SlideShowExtender")
                                                            {
                                                                SlideShowExtender myLabel = (SlideShowExtender)Convert.ChangeType(myC, typeof(SlideShowExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "TabContainer")
                                                            {
                                                                TabContainer myLabel = (TabContainer)Convert.ChangeType(myC, typeof(TabContainer));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "TextBoxWatermarkExtender")
                                                            {
                                                                TextBoxWatermarkExtender myLabel = (TextBoxWatermarkExtender)Convert.ChangeType(myC, typeof(TextBoxWatermarkExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "ToggleButtonExtender")
                                                            {
                                                                ToggleButtonExtender myLabel = (ToggleButtonExtender)Convert.ChangeType(myC, typeof(ToggleButtonExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "UpdatePanelAnimationExtender")
                                                            {
                                                                UpdatePanelAnimationExtender myLabel = (UpdatePanelAnimationExtender)Convert.ChangeType(myC, typeof(UpdatePanelAnimationExtender));
                                                                myLabel.Enabled = false;
                                                            }
                                                            else if (StrVal == "ValidatorCalloutExtender")
                                                            {
                                                                ValidatorCalloutExtender myLabel = (ValidatorCalloutExtender)Convert.ChangeType(myC, typeof(ValidatorCalloutExtender));
                                                                myLabel.Enabled = false;
                                                            }



                                                        }

                                                    }



                                                }
                                            }

                                        }



                                        else
                                        {
                                            Panel2.BackColor = System.Drawing.Color.Lavender;
                                            // Panel2.BorderStyle = System.
                                            Label1.ForeColor = System.Drawing.Color.Black;
                                            Label4.ForeColor = System.Drawing.Color.Black;
                                            LinkButton15.BackColor = System.Drawing.Color.Blue;
                                            ImageButton3.Visible = true;
                                            ModalPopupExtender1.Show();
                                        }
                                    }

                                    else
                                    {
                                        Response.Redirect("ShoppingCartLogin.aspx");
                                    }

                                }
                                else
                                {
                                    Response.Redirect("ShoppingCartLogin.aspx");
                                }
                            }

                            else
                            {
                                Response.Redirect("ShoppingCartLogin.aspx");
                            }
                        }
                        else
                        {
                            Panel2.BackColor = System.Drawing.Color.Lavender;
                            // Panel2.BorderStyle = System.
                            Label4.ForeColor = System.Drawing.Color.Black;
                            Label1.ForeColor = System.Drawing.Color.Black;
                            LinkButton15.BackColor = System.Drawing.Color.Blue;
                            ImageButton3.Visible = true;
                            ModalPopupExtender1.Show();
                        }


                    }

                }

                else
                {
                    Session.Remove("userid");
                    Session["userid"] = "";
                    Session.Clear();
                    Session.Abandon();
                    Session.RemoveAll();
                    Response.Redirect("ShoppingCartLogin.aspx");


                }
            }
            else
            {
                Response.Redirect("ShoppingCartLogin.aspx");
            }
            
        }


    }
    
    public void RestrictPriceplan(string pg)
    {
        //PricePlanMasterID;
        if (!IsPostBack)
        {
            if (Session.Count > 0 || Session["userid"] != "")
            {
                //if (Request.UrlReferrer.Port > 0)
                //{

                if (Request.UrlReferrer != null)
                {
                    ViewState["p1"] = Request.UrlReferrer.ToString();
                    string str = "SELECT  LUPD,MP, CID, PID, V FROM Lmaster";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(str, con);
                    da.Fill(dt);
                   
                    int PPlan;
                //  PPlan = Convert.ToInt32(dt.Rows[0][0].ToString());
                  PPlan = Convert.ToInt32(decryptstring(dt.Rows[0][0].ToString()));
                    string str1 = "SELECT     PricePlanName, PricePlanId FROM PricePlanMaster where PricePlanId=" + PPlan;
                    //string str1 = "SELECT     PricePlanName, PricePlanId FROM PricePlanMaster";
                    SqlDataAdapter da1 = new SqlDataAdapter(str1, con1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    int priceplanid;
                    priceplanid = Convert.ToInt32(dt1.Rows[0][1].ToString());


                    // PPlan = Convert.ToInt32(dt.Rows[0][0].ToString());

                    if (PPlan == priceplanid)
                    {
                        string str11 = "Select PageId from PageMaster where PageName='" + pg + "'";
                        SqlCommand cmd = new SqlCommand(str11, con);
                        SqlDataAdapter da11 = new SqlDataAdapter(cmd);
                        DataTable dt11 = new DataTable();
                        da11.Fill(dt11);

                        if (dt11.Rows.Count > 0)
                        {
                            if (Session.Count > 0 || Session["userid"] != "")
                            {

                                string str121 = "Select Page_id,ActiveDeactive from Plan_page_Access where Plan_id=" + PPlan + " and Page_id=" + dt11.Rows[0][0].ToString();
                                SqlDataAdapter da121 = new SqlDataAdapter(str121, con1);
                                DataTable dt121 = new DataTable();
                                da121.Fill(dt121);



                                int i;
                                for (i = 0; i <= dt121.Rows.Count - 1; i++)
                                {
                                    if (dt121.Rows[i][1].ToString() == "False")
                                    {
                                        //Response.Write("<script>alert('This Page Can Not Be Accessed')</script>");
                                        Panel3.BackColor = System.Drawing.Color.Lavender;
                                        Label3.ForeColor = System.Drawing.Color.Black;
                                        Label5.ForeColor = System.Drawing.Color.Black;
                                        Lnkbtn1.ForeColor = System.Drawing.Color.Blue;
                                        ImageButton2.Visible = true;
                                        ModalPopupExtender1222.Show();
                                    }
                                    else
                                    {
                                        string str1211 = "Select PageControl_id,ControlName,ActiveDeactive,ControlType_id from PageControlMaster where  Page_id=" + dt11.Rows[0][0];
                                        SqlDataAdapter da1211 = new SqlDataAdapter(str1211, con);
                                        DataTable dt1211 = new DataTable();
                                        da1211.Fill(dt1211);




                                        int i1;
                                        for (i1 = 0; i1 <= dt1211.Rows.Count - 1; i1++)
                                        {


                                            if (dt1211.Rows[i1][2].ToString() == "False")
                                            {
                                                string str1311 = "Select Type_Name from Control_type_Master where Type_id=" + dt1211.Rows[i1][3];
                                                SqlDataAdapter da1311 = new SqlDataAdapter(str1311, con);
                                                DataTable dt1311 = new DataTable();
                                                da1311.Fill(dt1311);

                                                String StrVal = dt1311.Rows[0][0].ToString();
                                                String StrName = dt1211.Rows[i1][1].ToString();
                                                GetControls(this, StrName);
                                                if (StrVal == "RadioButtonList")
                                                {
                                                    RadioButtonList myLabel = (RadioButtonList)Convert.ChangeType(myC, typeof(RadioButtonList));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ListBox")
                                                {
                                                    ListBox myLabel = (ListBox)Convert.ChangeType(myC, typeof(ListBox));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "HyperLink")
                                                {
                                                    HyperLink myLabel = (HyperLink)Convert.ChangeType(myC, typeof(HyperLink));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "LinkButton")
                                                {
                                                    LinkButton myLabel = (LinkButton)Convert.ChangeType(myC, typeof(LinkButton));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ImageButton")
                                                {
                                                    ImageButton myLabel = (ImageButton)Convert.ChangeType(myC, typeof(ImageButton));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "TextBox")
                                                {
                                                    TextBox myLabel = (TextBox)Convert.ChangeType(myC, typeof(TextBox));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "Button")
                                                {
                                                    Button myLabel = (Button)Convert.ChangeType(myC, typeof(Button));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "Panel")
                                                {
                                                    Panel myLabel = (Panel)Convert.ChangeType(myC, typeof(Panel));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CheckBox")
                                                {
                                                    CheckBox myLabel = (CheckBox)Convert.ChangeType(myC, typeof(CheckBox));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CheckBoxList")
                                                {
                                                    CheckBoxList myLabel = (CheckBoxList)Convert.ChangeType(myC, typeof(CheckBoxList));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "Label")
                                                {
                                                    Label myLabel = (Label)Convert.ChangeType(myC, typeof(Label));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "Image")
                                                {
                                                    Image myLabel = (Image)Convert.ChangeType(myC, typeof(Image));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ImageMap")
                                                {
                                                    ImageMap myLabel = (ImageMap)Convert.ChangeType(myC, typeof(ImageMap));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "RadioButton")
                                                {
                                                    RadioButton myLabel = (RadioButton)Convert.ChangeType(myC, typeof(RadioButton));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "DropDownList")
                                                {
                                                    DropDownList myLabel = (DropDownList)Convert.ChangeType(myC, typeof(DropDownList));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "FileUpload")
                                                {
                                                    FileUpload myLabel = (FileUpload)Convert.ChangeType(myC, typeof(FileUpload));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "PlaceHolder")
                                                {
                                                    PlaceHolder myLabel = (PlaceHolder)Convert.ChangeType(myC, typeof(PlaceHolder));
                                                    myLabel.Visible = false;

                                                }
                                                else if (StrVal == "GridView")
                                                {
                                                    GridView myLabel = (GridView)Convert.ChangeType(myC, typeof(GridView));
                                                    myLabel.Enabled = false;

                                                }

                                                else if (StrVal == "DataList")
                                                {
                                                    DataList myLabel = (DataList)Convert.ChangeType(myC, typeof(DataList));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "DetailsView")
                                                {
                                                    DetailsView myLabel = (DetailsView)Convert.ChangeType(myC, typeof(DetailsView));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "FormView")
                                                {
                                                    FormView myLabel = (FormView)Convert.ChangeType(myC, typeof(FormView));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "Repeater")
                                                {
                                                    Repeater myLabel = (Repeater)Convert.ChangeType(myC, typeof(Repeater));
                                                    myLabel.Visible = false;

                                                }
                                                else if (StrVal == "ListView")
                                                {
                                                    ListView myLabel = (ListView)Convert.ChangeType(myC, typeof(ListView));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "DataPager")
                                                {
                                                    DataPager myLabel = (DataPager)Convert.ChangeType(myC, typeof(DataPager));
                                                    myLabel.Visible = false;

                                                }
                                                else if (StrVal == "LinqDataSource")
                                                {
                                                    LinqDataSource myLabel = (LinqDataSource)Convert.ChangeType(myC, typeof(LinqDataSource));
                                                    myLabel.Visible = false;

                                                }
                                                else if (StrVal == "RangeValidator")
                                                {
                                                    RangeValidator myLabel = (RangeValidator)Convert.ChangeType(myC, typeof(RangeValidator));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "RangeValidator")
                                                {
                                                    RangeValidator myLabel = (RangeValidator)Convert.ChangeType(myC, typeof(RangeValidator));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "RegularExpressionValidator")
                                                {
                                                    RegularExpressionValidator myLabel = (RegularExpressionValidator)Convert.ChangeType(myC, typeof(RegularExpressionValidator));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "RequiredFieldValidator")
                                                {
                                                    RequiredFieldValidator myLabel = (RequiredFieldValidator)Convert.ChangeType(myC, typeof(RequiredFieldValidator));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "CompareValidator")
                                                {
                                                    CompareValidator myLabel = (CompareValidator)Convert.ChangeType(myC, typeof(CompareValidator));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "CustomValidator")
                                                {
                                                    CustomValidator myLabel = (CustomValidator)Convert.ChangeType(myC, typeof(CustomValidator));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "ValidationSummary")
                                                {
                                                    ValidationSummary myLabel = (ValidationSummary)Convert.ChangeType(myC, typeof(ValidationSummary));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "SiteMapPath")
                                                {
                                                    SiteMapPath myLabel = (SiteMapPath)Convert.ChangeType(myC, typeof(SiteMapPath));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "Menu")
                                                {
                                                    Menu myLabel = (Menu)Convert.ChangeType(myC, typeof(Menu));
                                                    myLabel.Visible = false;
                                                }
                                                else if (StrVal == "TreeView")
                                                {
                                                    TreeView myLabel = (TreeView)Convert.ChangeType(myC, typeof(TreeView));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "WebPart")
                                                {
                                                    WebPart myLabel = (WebPart)Convert.ChangeType(myC, typeof(WebPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ProxyWebPart")
                                                {
                                                    ProxyWebPart myLabel = (ProxyWebPart)Convert.ChangeType(myC, typeof(ProxyWebPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "WebPartZone")
                                                {
                                                    WebPartZone myLabel = (WebPartZone)Convert.ChangeType(myC, typeof(WebPartZone));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CatalogPart")
                                                {
                                                    CatalogPart myLabel = (CatalogPart)Convert.ChangeType(myC, typeof(CatalogPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CatalogZone")
                                                {
                                                    CatalogZone myLabel = (CatalogZone)Convert.ChangeType(myC, typeof(CatalogZone));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "DeclarativeCatalogPart")
                                                {
                                                    DeclarativeCatalogPart myLabel = (DeclarativeCatalogPart)Convert.ChangeType(myC, typeof(DeclarativeCatalogPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "PageCatalogPart")
                                                {
                                                    PageCatalogPart myLabel = (PageCatalogPart)Convert.ChangeType(myC, typeof(PageCatalogPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ImportCatalogPart")
                                                {
                                                    ImportCatalogPart myLabel = (ImportCatalogPart)Convert.ChangeType(myC, typeof(ImportCatalogPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "EditorZone")
                                                {
                                                    EditorZone myLabel = (EditorZone)Convert.ChangeType(myC, typeof(EditorZone));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "AppearanceEditorPart")
                                                {
                                                    AppearanceEditorPart myLabel = (AppearanceEditorPart)Convert.ChangeType(myC, typeof(AppearanceEditorPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "BehaviorEditorPart")
                                                {
                                                    BehaviorEditorPart myLabel = (BehaviorEditorPart)Convert.ChangeType(myC, typeof(BehaviorEditorPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "LayoutEditorPart")
                                                {
                                                    LayoutEditorPart myLabel = (LayoutEditorPart)Convert.ChangeType(myC, typeof(LayoutEditorPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "PropertyGridEditorPart")
                                                {
                                                    PropertyGridEditorPart myLabel = (PropertyGridEditorPart)Convert.ChangeType(myC, typeof(PropertyGridEditorPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ConnectionsZone")
                                                {
                                                    ConnectionsZone myLabel = (ConnectionsZone)Convert.ChangeType(myC, typeof(ConnectionsZone));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "UpdateProgress")
                                                {
                                                    UpdateProgress myLabel = (UpdateProgress)Convert.ChangeType(myC, typeof(UpdateProgress));
                                                    myLabel.Visible = false;
                                                }
                                                else if (StrVal == "Timer")
                                                {
                                                    Timer myLabel = (Timer)Convert.ChangeType(myC, typeof(Timer));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "UpdatePanel")
                                                {
                                                    UpdatePanel myLabel = (UpdatePanel)Convert.ChangeType(myC, typeof(UpdatePanel));
                                                    myLabel.Visible = false;
                                                }
                                                else if (StrVal == "Accordion")
                                                {
                                                    Accordion myLabel = (Accordion)Convert.ChangeType(myC, typeof(Accordion));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "UserControl")
                                                {
                                                    UserControl myLabel = (UserControl)Convert.ChangeType(myC, typeof(UserControl));
                                                    myLabel.Visible = false;
                                                }
                                                else if (StrVal == "AccordionPane")
                                                {
                                                    AccordionPane myLabel = (AccordionPane)Convert.ChangeType(myC, typeof(AccordionPane));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "AlwaysVisibleControlExtender")
                                                {
                                                    AlwaysVisibleControlExtender myLabel = (AlwaysVisibleControlExtender)Convert.ChangeType(myC, typeof(AlwaysVisibleControlExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "AnimationExtender")
                                                {
                                                    AnimationExtender myLabel = (AnimationExtender)Convert.ChangeType(myC, typeof(AnimationExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "AutoCompleteExtender")
                                                {
                                                    AutoCompleteExtender myLabel = (AutoCompleteExtender)Convert.ChangeType(myC, typeof(AutoCompleteExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CalendarExtender")
                                                {
                                                    CalendarExtender myLabel = (CalendarExtender)Convert.ChangeType(myC, typeof(CalendarExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CascadingDropDown")
                                                {
                                                    CascadingDropDown myLabel = (CascadingDropDown)Convert.ChangeType(myC, typeof(CascadingDropDown));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CollapsiblePanelExtender")
                                                {
                                                    CollapsiblePanelExtender myLabel = (CollapsiblePanelExtender)Convert.ChangeType(myC, typeof(CollapsiblePanelExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ConfirmButtonExtender")
                                                {
                                                    ConfirmButtonExtender myLabel = (ConfirmButtonExtender)Convert.ChangeType(myC, typeof(ConfirmButtonExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "DragPanelExtender")
                                                {
                                                    DragPanelExtender myLabel = (DragPanelExtender)Convert.ChangeType(myC, typeof(DragPanelExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "DropDownExtender")
                                                {
                                                    DropDownExtender myLabel = (DropDownExtender)Convert.ChangeType(myC, typeof(DropDownExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "DropShadowExtender")
                                                {
                                                    DropShadowExtender myLabel = (DropShadowExtender)Convert.ChangeType(myC, typeof(DropShadowExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "DynamicPopulateExtender")
                                                {
                                                    DynamicPopulateExtender myLabel = (DynamicPopulateExtender)Convert.ChangeType(myC, typeof(DynamicPopulateExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "FilteredTextBoxExtender")
                                                {
                                                    FilteredTextBoxExtender myLabel = (FilteredTextBoxExtender)Convert.ChangeType(myC, typeof(FilteredTextBoxExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "HoverExtender")
                                                {
                                                    HoverExtender myLabel = (HoverExtender)Convert.ChangeType(myC, typeof(HoverExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "HoverMenuExtender")
                                                {
                                                    HoverMenuExtender myLabel = (HoverMenuExtender)Convert.ChangeType(myC, typeof(HoverMenuExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ListSearchExtender")
                                                {
                                                    ListSearchExtender myLabel = (ListSearchExtender)Convert.ChangeType(myC, typeof(ListSearchExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "MaskedEditExtender")
                                                {
                                                    MaskedEditExtender myLabel = (MaskedEditExtender)Convert.ChangeType(myC, typeof(MaskedEditExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "MaskedEditValidator")
                                                {
                                                    MaskedEditValidator myLabel = (MaskedEditValidator)Convert.ChangeType(myC, typeof(MaskedEditValidator));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ModalPopupExtender")
                                                {
                                                    ModalPopupExtender myLabel = (ModalPopupExtender)Convert.ChangeType(myC, typeof(ModalPopupExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "MutuallyExclusiveCheckBoxExtender")
                                                {
                                                    MutuallyExclusiveCheckBoxExtender myLabel = (MutuallyExclusiveCheckBoxExtender)Convert.ChangeType(myC, typeof(MutuallyExclusiveCheckBoxExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "NoBot")
                                                {
                                                    NoBot myLabel = (NoBot)Convert.ChangeType(myC, typeof(NoBot));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "NumericUpDownExtender")
                                                {
                                                    NumericUpDownExtender myLabel = (NumericUpDownExtender)Convert.ChangeType(myC, typeof(NumericUpDownExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "PagingBulletedListExtender")
                                                {
                                                    PagingBulletedListExtender myLabel = (PagingBulletedListExtender)Convert.ChangeType(myC, typeof(PagingBulletedListExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "PasswordStrength")
                                                {
                                                    PasswordStrength myLabel = (PasswordStrength)Convert.ChangeType(myC, typeof(PasswordStrength));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "PopupControlExtender")
                                                {
                                                    PopupControlExtender myLabel = (PopupControlExtender)Convert.ChangeType(myC, typeof(PopupControlExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "Rating")
                                                {
                                                    Rating myLabel = (Rating)Convert.ChangeType(myC, typeof(Rating));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ReorderList")
                                                {
                                                    ReorderList myLabel = (ReorderList)Convert.ChangeType(myC, typeof(ReorderList));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ResizableControlExtender")
                                                {
                                                    ResizableControlExtender myLabel = (ResizableControlExtender)Convert.ChangeType(myC, typeof(ResizableControlExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "RoundedCornersExtender")
                                                {
                                                    RoundedCornersExtender myLabel = (RoundedCornersExtender)Convert.ChangeType(myC, typeof(RoundedCornersExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "SliderExtender")
                                                {
                                                    SliderExtender myLabel = (SliderExtender)Convert.ChangeType(myC, typeof(SliderExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "SlideShowExtender")
                                                {
                                                    SlideShowExtender myLabel = (SlideShowExtender)Convert.ChangeType(myC, typeof(SlideShowExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "TabContainer")
                                                {
                                                    TabContainer myLabel = (TabContainer)Convert.ChangeType(myC, typeof(TabContainer));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "TextBoxWatermarkExtender")
                                                {
                                                    TextBoxWatermarkExtender myLabel = (TextBoxWatermarkExtender)Convert.ChangeType(myC, typeof(TextBoxWatermarkExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ToggleButtonExtender")
                                                {
                                                    ToggleButtonExtender myLabel = (ToggleButtonExtender)Convert.ChangeType(myC, typeof(ToggleButtonExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "UpdatePanelAnimationExtender")
                                                {
                                                    UpdatePanelAnimationExtender myLabel = (UpdatePanelAnimationExtender)Convert.ChangeType(myC, typeof(UpdatePanelAnimationExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ValidatorCalloutExtender")
                                                {
                                                    ValidatorCalloutExtender myLabel = (ValidatorCalloutExtender)Convert.ChangeType(myC, typeof(ValidatorCalloutExtender));
                                                    myLabel.Enabled = false;
                                                }


                                            }

                                        }



                                    }
                                }


                            }
                            else
                            {
                                Response.Redirect("ShoppingCartLogin.aspx");
                            }
                        }



                    //}
                        else
                        {
                            Panel3.BackColor = System.Drawing.Color.Lavender;
                            Label3.ForeColor = System.Drawing.Color.Black;
                            Label5.ForeColor = System.Drawing.Color.Black;
                            Lnkbtn1.ForeColor = System.Drawing.Color.Blue;
                            ImageButton2.Visible = true;
                            ModalPopupExtender1222.Show();
                        }
                    }
                }
                else
                {
                    Session.Remove("userid");
                    Session["userid"] = "";
                    Session.Clear();
                    Session.Abandon();
                    Session.RemoveAll();
                    Response.Redirect("ShoppingCartLogin.aspx");
                }

            }
            else
            {
                Response.Redirect("ShoppingCartLogin.aspx");
            }
        }
    }

    protected void ImageButton51_Click(object sender, ImageClickEventArgs e)
    {
        object p11 = ViewState["p1"].ToString();
        if (p11 != null)
        {
            Response.Redirect((string)p11);
        }
        ModalPopupExtender1222.Hide();
        //pnlMain.Enabled = false;


    }
    
    protected void ImageButton511_Click(object sender, ImageClickEventArgs e)
    {
        object p11 = ViewState["p1"].ToString();
        if (p11 != null)
        {
            Response.Redirect((string)p11);
        }
        ModalPopupExtender1.Hide();
    }

    //protected void LinkButton89_Click(object sender, EventArgs e)
    //{
    //    ViewState["PanIndex"] = Accordion1.SelectedIndex;
    //    Response.Write(ViewState["PanIndex"].ToString());
    //}
}
