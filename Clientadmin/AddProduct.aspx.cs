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

public partial class AddProduct : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
       // chkboxActiveDeactive.Checked=true;
       
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            if (Request.QueryString["PId"] != null || Request.QueryString["prd"] != null)
            {
                if (Request.QueryString["PId"] != null)
                {

                    hdnProductDetailId.Value = Request.QueryString["PId"].ToString();
                }
                else if (Request.QueryString["prd"] != null)
                {
                    hdnProductDetailId.Value = Request.QueryString["Prd"].ToString();
                }
                //
                string strcln = " SELECT     ProductMaster.ProductId, ProductMaster.ClientMasterId, ProductMaster.ProductName, ProductMaster.ProductURL, ProductMaster.PricePlanURL, " +
                     " ProductDetail.ProductDetailId, ProductDetail.ProductId AS Expr1, ProductDetail.VersionNo, ProductDetail.Active, ProductDetail.Startdate, ProductDetail.EndDate, " +
                     " ProductDetail.ProductSetup, ProductDetail.ProductDb, ProductDetail.ProductExtra " +
                     " FROM         ProductDetail RIGHT OUTER JOIN ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId" +
                     " where   ProductDetail.ProductDetailId= " + hdnProductDetailId.Value.ToString();
             //   hdnProductDetailId.Value = i.ToString();

                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);
                if (dtcln.Rows.Count > 0)
                {
                    hdnProductId.Value = dtcln.Rows[0]["ProductId"].ToString();
                    txtPricePlanURL.Text = dtcln.Rows[0]["PricePlanURL"].ToString();
                    txtProductName.Text = dtcln.Rows[0]["ProductName"].ToString();
                    txtUrl.Text = dtcln.Rows[0]["ProductURL"].ToString();
                    txtStartdate.Text = Convert.ToDateTime(dtcln.Rows[0]["Startdate"].ToString()).ToShortDateString();
                    txtEndDate.Text = Convert.ToDateTime(dtcln.Rows[0]["EndDate"].ToString()).ToShortDateString();
                    txtVersionNo.Text = dtcln.Rows[0]["VersionNo"].ToString();
                    txtUrl.Enabled = true;
                    txtProductName.Enabled = true;
                    txtPricePlanURL.Enabled = true;
                    btnSubmit.Text = "Update";
                }
                //
            }
            if (Request.QueryString["prd"] != null)
            {
                pnlgrid.Visible = false;
                btnSubmit.Visible = false;
                RadioButtonList1.Visible = false;
            }
            FillgridFTP();
            FillGrid();

            if (Session["Login"] != null)
            {
                if (Session["Login"].ToString() == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            FillProduct();
            FillddlClientname();
            FillProductfilter();
            
        }
    }
    protected void FillgridFTP()
    {
        string finalstr = " SELECT  dbo.ClientMaster.FTP, dbo.ClientMaster.FTPUserName, dbo.ClientMaster.FTPPassword, dbo.ClientMaster.FTPport,dbo.ServerMasterTbl.serverdefaultpathforiis, dbo.ServerMasterTbl.ServerName, dbo.ServerMasterTbl.serverloction, dbo.ServerMasterTbl.serverdetail, dbo.ServerMasterTbl.Ipaddress, dbo.ServerMasterTbl.PublicIp, dbo.ClientMaster.ServerId, dbo.ClientMaster.ClientMasterId,dbo.ServerMasterTbl.folderpathformastercode FROM dbo.ClientMaster INNER JOIN dbo.ServerMasterTbl ON dbo.ClientMaster.ServerId = dbo.ServerMasterTbl.Id Where ClientMasterId='" + Session["ClientId"] + "'";
        SqlCommand cmd = new SqlCommand(finalstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            txt_servername.Text = ds.Rows[0]["ServerName"].ToString();
            txt_serverlocation.Text = ds.Rows[0]["serverloction"].ToString();
            txt_publicip.Text = ds.Rows[0]["PublicIp"].ToString();
            txt_privateip.Text = ds.Rows[0]["Ipaddress"].ToString();          

            string ftp = ds.Rows[0]["FTP"].ToString();
            string user = ds.Rows[0]["FTPUserName"].ToString();
            //lbl_password.Text = ds.Rows[0]["FTPPassword"].ToString();
            //lbl_port.Text = ds.Rows[0]["FTPPort"].ToString();
            txtMasterIISWebsite.Text = ds.Rows[0]["serverdefaultpathforiis"].ToString() + "\\" + ds.Rows[0]["ClientMasterId"].ToString() + "\\";
            txt_mastercodepath.Text = ds.Rows[0]["folderpathformastercode"].ToString() + "\\" + ds.Rows[0]["ClientMasterId"].ToString() + "\\";
        }
        else
        {
        }
    }
    protected void FillProduct()
    {
        string active = "";
        if (ddlstatus.SelectedItem.Text == "Active")
        {
            active = " and ProductDetail.Active='True'";            
        }
        if (ddlstatus.SelectedItem.Text == "Inactive")
        {
            active = " and ProductDetail.Active='False'"; 
        }
        string strcln = " SELECT * from   dbo.ProductMaster INNER JOIN dbo.ProductDetail ON dbo.ProductMaster.ProductId = dbo.ProductDetail.ProductId where ClientMasterId= " + Session["ClientId"].ToString() +active;
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductList.DataSource  = dtcln;
        ddlProductList.DataBind();
        ddlProductList.Items.Insert(0, "-Select-");
        ddlProductList.Items[0].Value = "0";
        ddlProductList.SelectedIndex = 0;
    }
    protected void FillProductfilter()
    {

        string strcln = " SELECT * from  ProductMaster where ClientMasterId= " + Session["ClientId"].ToString()+" Order By ProductName ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddproductfilter.DataSource = dtcln;
        ddproductfilter.DataBind();
        ddproductfilter.Items.Insert(0, "-Select-");
        ddproductfilter.Items[0].Value = "0";
        ddproductfilter.SelectedIndex = 0;
    }
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void FillGrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        string active;
        string deactive;

        string strcln = " SELECT ProductMaster.*, ProductDetail.* ,dbo.VersionInfoMaster.VersionInfoId as vid,dbo.VersionInfoMaster.Active as VActive ,dbo.VersionInfoMaster.VersionInfoName " +
                        " FROM  dbo.ProductMaster INNER JOIN dbo.ProductDetail ON dbo.ProductMaster.ProductId = dbo.ProductDetail.ProductId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductDetail.VersionNo = dbo.VersionInfoMaster.VersionInfoName AND dbo.ProductDetail.ProductId = dbo.VersionInfoMaster.ProductId " +
                        " where ClientMasterId= " + Session["ClientId"].ToString();        
        
        if (ddlstatus.SelectedItem.Text == "Active")
        {
            active = " and ProductDetail.Active='True'";
            strcln += active;
        }
        else if (ddlstatus.SelectedItem.Text == "Inactive")
        {
            deactive = " and ProductDetail.Active='False'";
            strcln += deactive;
        }
        if (ddproductfilter.SelectedIndex > 0)
        {
            strcln += " and ProductMaster.ProductId=" + ddproductfilter.SelectedValue + "";
        }
        if (txt_search.Text != "")
        {
            strcln += "  and (ProductMaster.ProductName Like '%" + txt_search.Text + "%' OR dbo.VersionInfoMaster.VersionInfoName Like '%" + txt_search.Text + "%' OR dbo.ProductMaster.Description Like '%" + txt_search.Text + "%' ) ";
        }
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GridView1.DataSource = dtcln;
        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataBind();
    }
   
    protected void FillddlClientname()
    {
        string strcln = "SELECT ClientMasterId,CompanyName FROM ClientMaster where ClientMasterId='"+Session["ClientId"]+"' ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        
        //ddlClientname.DataSource = dtcln;
        //ddlClientname.DataTextField = "CompanyName";
        //ddlClientname.DataValueField = "ClientMasterId";
        //ddlClientname.DataBind();

        //ddlClientname.Items.Insert(0, "-Select-");
        //ddlClientname.Items[0].Value = "0";

 
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string fileup = "";
        string fileuptop = "";
        string fileupbottom = "";
        string fileupleft = "";
        string fileupright = "";
        string fileupfront = "";
        string fileupback = "";

        string MasterCodeVersionPath = txt_mastercodepath.Text + "\\" + txt_MaterCodeProductVersionFolder.Text;
        string MasterIISWebsite = txtMasterIISWebsite.Text + "\\" + txt_IISProductWebsiteFolder.Text; 

        try
        {
            if (btnSubmit.Text == "Update")
            {
              
                SqlCommand cmd;
                string str;

                string strcln11 = "";
                strcln11 = "SELECT * FROM   dbo.ProductMaster INNER JOIN dbo.ProductDetail ON dbo.ProductDetail.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId where ProductMaster.ProductName='" + txtProductName.Text + "' and ProductDetail.VersionNo='" + txtVersionNo.Text + "' and ClientMasterId='" + Session["ClientId"].ToString() + "' and ProductDetail.ProductDetailId<>'" + ViewState["ProductDetailId"] + "' and  dbo.VersionInfoMaster.VersionInfoId <> " + ViewState["VerID"] + "";
                SqlCommand cmdcln11 = new SqlCommand(strcln11, con);
                DataTable dtcln11 = new DataTable();
                SqlDataAdapter adpcln11 = new SqlDataAdapter(cmdcln11);
                adpcln11.Fill(dtcln11);
                if (dtcln11.Rows.Count > 0)
                {
                    lblmsg.Text = "Product with this Product Name and Version is already Exists";
                }
                else
                {
                    string upimage = "SELECT ProductMaster.* FROM ProductMaster  where  ProductMaster.ProductId='" + ViewState["PID"] + "'";
                    SqlCommand cmdupi = new SqlCommand(upimage, con);
                    DataTable dtcupi = new DataTable();
                    SqlDataAdapter adpupi = new SqlDataAdapter(cmdupi);
                    adpupi.Fill(dtcupi);
                    if (dtcupi.Rows.Count > 0)
                    {
                        if (filemain.HasFile == true)
                        {
                            if (Convert.ToString(dtcupi.Rows[0]["Pimage"]) != "")
                            {
                                System.IO.File.Delete(Server.MapPath("~\\Productimage\\") + Convert.ToString(dtcupi.Rows[0]["Pimage"]));
                            }
                            fileup = ",Pimage='" + Convert.ToString(filetop.FileName) + "'";
                            filemain.SaveAs(Server.MapPath("~\\Productimage\\") + filemain.FileName);
                           
                        }
                        if (chkimg.Checked == true)
                        {

                            if (filetop.HasFile == true)
                            {
                                if (Convert.ToString(dtcupi.Rows[0]["Topimg"]) != "")
                                {
                                    System.IO.File.Delete(Server.MapPath("~\\Productimage\\") + Convert.ToString(dtcupi.Rows[0]["Topimg"]));
                                }
                                fileuptop = ",Topimg='" + Convert.ToString(filetop.FileName) + "'";
                                filetop.SaveAs(Server.MapPath("~\\Productimage\\") + filetop.FileName);
                               
                            }
                            if (filebottom.HasFile == true)
                            {
                                if (Convert.ToString(dtcupi.Rows[0]["Bottomimage"]) != "")
                                {
                                    System.IO.File.Delete(Server.MapPath("~\\Productimage\\") + Convert.ToString(dtcupi.Rows[0]["Bottomimage"]));
                                }
                                fileupbottom = ",Bottomimage='" + Convert.ToString(filebottom.FileName) + "'";
                                filebottom.SaveAs(Server.MapPath("~\\Productimage\\") + filebottom.FileName);
                                
                            }
                            if (fileright.HasFile == true)
                            {
                                if (Convert.ToString(dtcupi.Rows[0]["Rightimage"]) != "")
                                {
                                    System.IO.File.Delete(Server.MapPath("~\\Productimage\\") + Convert.ToString(dtcupi.Rows[0]["Rightimage"]));
                                }
                                fileupright = ",Rightimage='" + Convert.ToString(fileright.FileName) + "'";
                                fileright.SaveAs(Server.MapPath("~\\Productimage\\") + fileright.FileName);
                              
                            }
                            if (fileleft.HasFile == true)
                            {
                                if (Convert.ToString(dtcupi.Rows[0]["Leftimage"]) != "")
                                {
                                    System.IO.File.Delete(Server.MapPath("~\\Productimage\\") + Convert.ToString(dtcupi.Rows[0]["Leftimage"]));
                                }
                                fileupleft = ",Leftimage='" + Convert.ToString(fileleft.FileName) + "'";
                                fileleft.SaveAs(Server.MapPath("~\\Productimage\\") + fileleft.FileName);
                                
                            }
                            if (filefront.HasFile == true)
                            {
                                if (Convert.ToString(dtcupi.Rows[0]["Frontimage"]) != "")
                                {
                                    System.IO.File.Delete(Server.MapPath("~\\Productimage\\") + Convert.ToString(dtcupi.Rows[0]["Frontimage"]));
                                }
                                fileupfront = " ,Frontimage='" + Convert.ToString(filefront.FileName) + "'";
                                filefront.SaveAs(Server.MapPath("~\\Productimage\\") + filefront.FileName);
                              
                            }
                            if (fileback.HasFile == true)
                            {
                                if (Convert.ToString(dtcupi.Rows[0]["Backimage"]) != "")
                                {
                                    System.IO.File.Delete(Server.MapPath("~\\Productimage\\") + Convert.ToString(dtcupi.Rows[0]["Backimage"]));
                                }
                                fileupback = " ,Backimage='" + Convert.ToString(fileback.FileName) + "'";
                                fileback.SaveAs(Server.MapPath("~\\Productimage\\") + fileback.FileName);
                             
                            }
                        }
                    }
                    str = "update   ProductMaster set ProductName='" + txtProductName.Text + "',Description='" + txtdescription.Text.ToString() + "',ProductURL='" + txtUrl.Text + "', PricePlanURL='" + txtPricePlanURL.Text + "' ,ClientMasterId =" + Session["ClientId"].ToString() + ",Download='" + chkboxActiveDeactive0.Checked + "',otheimage='" + chkimg.Checked + "'" + fileup + fileuptop + fileupbottom + fileupright + fileupleft + fileupfront + fileupback + "   where ProductId=" + ViewState["PID"];
                    cmd = new SqlCommand(str, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                    str = "update   ProductDetail set  VersionNo ='" + txtVersionNo.Text + "', Active= '" + chkboxActiveDeactive.Checked + "' ,startDate='" + txtStartdate.Text + "', EndDate='" + txtEndDate.Text + "' where ProductDetailId=" + ViewState["ProductDetailId"];
                    cmd = new SqlCommand(str, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string strnew;
                    strnew = "update   VersionInfoMaster set  VersionInfoname ='" + txtVersionNo.Text + "' , Active= '" + chbversionactive.Checked + "' , ProductURL = '" + txtUrl.Text + "' , PricePlanURL ='" + txtPricePlanURL.Text + "', MasterCodeSourcePath ='" + MasterCodeVersionPath + "', TemporaryPublishPath = '" + txttemppath.Text + "', DestinationPath = '" + txtoutputsourcepath.Text + "',MasterCodeLatestVersionFolderFullPath='" + MasterCodeVersionPath + "',IISProductWebsiteFolder='" + txt_IISProductWebsiteFolder.Text + "',MaterCodeProductVersionFolder='" + txt_MaterCodeProductVersionFolder.Text + "',ServerMasterCodeSourceIISWebsitePath='" + txtMasterIISWebsite.Text + "',ServerMasterCodeVersionPath='" + txt_mastercodepath.Text + "' where VersionInfoMaster.Versioninfoid=" + ViewState["VerID"] + " ";
                    cmd = new SqlCommand(strnew, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //--------------------------


                    int i = 0;
                    string str11 = "select * from VersionInfoMasterDeleteFolder where VersionInfoId='" + ViewState["VerID"] + "' ";
                    SqlCommand cmd11 = new SqlCommand(str11, con);
                    SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
                    DataTable dt11 = new DataTable();
                    da11.Fill(dt11);
                    while (i < ListBox1.Items.Count)
                    {
                        if (i < dt11.Rows.Count)
                        {
                            if (dt11.Rows[i]["Id"] != "0" || dt11.Rows[i]["Id"] != null)
                            {
                                str = "Update VersionInfoMasterDeleteFolder Set VersionInfoId='" + ViewState["VerID"] + "',VersionDeleteFolderPath='" + ListBox1.Items[i].Text + "' where VersionInfoId='" + dt11.Rows[i]["Id"] + "'";
                                SqlCommand cmd112 = new SqlCommand(str, con);
                                con.Open();
                                cmd112.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        else
                        {
                            str = "Insert Into VersionInfoMasterDeleteFolder (VersionInfoId,VersionDeleteFolderPath)values('" + ViewState["VerID"] + "','" + ListBox1.Items[i].Text + "')";
                            SqlCommand cmd112 = new SqlCommand(str, con);
                            con.Open();
                            cmd112.ExecuteNonQuery();
                            con.Close();
                        }
                        i++;
                    }
                    //-----------------------------
                    string otherup = " Delete ProductMasterSubProduct Where  VersionInfoId='" + ViewState["VerID"] + "' ";
                    SqlCommand cmdotherup = new SqlCommand(otherup, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdotherup.ExecuteNonQuery();
                    con.Close();
                    
                    //---------------------------
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record updated successfully";
                    chkboxActiveDeactive.Checked = false;
                    chbversionactive.Checked = false;
                    chkboxActiveDeactive0.Checked = false;
                    pnlProduct.Visible = false;
                    txtProductName.Text = "";
                    txtPricePlanURL.Text = "";
                    txtStartdate.Text = "";
                    txtVersionNo.Text = "";
                    txtUrl.Text = "";
                    txtEndDate.Text = "";
                    btnSubmit.Text = "Submit";
                    txtdescription.Text = "";
                    chkimg.Checked = false;
                    RadioButtonList1.SelectedIndex = 0;
                    FillGrid();
                    addnewpanel.Visible = true;
                    pnladdnew.Visible = false;
                    Label19.Text = "";

                    //string thisDir = "";
                    //string thisDirMainsetup = "";
                    ////  thisDir = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + ""; // Server.MapPath(".");
                    //thisDir = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + ""; // Server.MapPath(".");
                    //System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(thisDir);
                    //if (!di.Exists)
                    //{
                    //    di.Create();
                    //}
                    //thisDirMainsetup = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup"; // Server.MapPath(".");
                    //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(thisDirMainsetup);
                    //if (!dir.Exists)
                    //{
                    //    dir.Create();
                    //}
                    //productSetup = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fuploadProjectSetup.FileName;
                    //     productdb = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + FuploadDbFile.FileName;
                    //if (FUploadExtra.HasFile == true)
                    //{
                    //    productextra = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + FUploadExtra.FileName;
                    //}
                    //String Dest = "";
                    //thisDir = "";
                    //
                    
                }

                
            }
            else
            {
                SqlCommand cmdcln;
                DataTable dtcln;
                SqlDataAdapter adpcln;
                string strcln;
                SqlCommand cmd;
                string str;

                fileuptop = Convert.ToString(filetop.FileName);
                fileupbottom = Convert.ToString(filebottom.FileName);
                fileupleft = Convert.ToString(fileleft.FileName);
                fileupright = Convert.ToString(fileright.FileName);
                fileupfront = Convert.ToString(filefront.FileName);
                fileupback = Convert.ToString(fileback.FileName);
                if (pnlProduct.Visible == true)
                {
                    
                    strcln = "SELECT * FROM ProductMaster where ProductId = " + ddlProductList.SelectedItem.Value.ToString();
                    cmdcln = new SqlCommand(strcln, con);
                    dtcln = new DataTable();
                    adpcln = new SqlDataAdapter(cmdcln);
                    adpcln.Fill(dtcln);

                    string strcln11 = "";
                    strcln11 = "SELECT * FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId where ProductMaster.ProductId='" + ddlProductList.SelectedValue + "' and ProductDetail.VersionNo='" + txtVersionNo.Text + "' and ClientMasterId='" + Session["ClientId"].ToString() + "'";
                    SqlCommand cmdcln11 = new SqlCommand(strcln11, con);
                    DataTable dtcln11 = new DataTable();
                    SqlDataAdapter adpcln11 = new SqlDataAdapter(cmdcln11);
                    adpcln11.Fill(dtcln11);
                    if (dtcln11.Rows.Count > 0)
                    {
                        lblmsg.Text = "Product with this Product Name and Version is already Exists... ";
                    }
                    else
                    {   
                        lblmsg.Visible = true;
                        //strcln = "SELECT * FROM ProductMaster order by productId desc";
                        strcln = "SELECT * FROM ProductMaster where ProductId = " + ddlProductList.SelectedItem.Value.ToString();                  
                        cmdcln = new SqlCommand(strcln, con);
                        dtcln = new DataTable();
                        adpcln = new SqlDataAdapter(cmdcln);
                        adpcln.Fill(dtcln);
                        if (dtcln.Rows.Count > 0)
                        {
                            str = " INSERT INTO ProductDetail(ProductId,VersionNo, Active ,startDate, EndDate) " +
                                  " VALUES('" + dtcln.Rows[0]["productId"].ToString() + "', '" + txtVersionNo.Text + "' ,  '" + chkboxActiveDeactive.Checked + "'  ,'" + txtStartdate.Text + "','" + txtEndDate.Text + "')";
                            cmd = new SqlCommand(str, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            str = "INSERT INTO VersionInfoMaster(VersionInfoname,productid, Active,ProductURL,PricePlanURL,MasterCodeSourcePath,TemporaryPublishPath,DestinationPath," +
                                  " MasterCodeLatestVersionFolderFullPath,IISProductWebsiteFolder,MaterCodeProductVersionFolder,ServerMasterCodeSourceIISWebsitePath  ,ServerMasterCodeVersionPath) " +
                                  " VALUES('" + txtVersionNo.Text + "','" + dtcln.Rows[0]["productId"].ToString() + "' ,  '" + chbversionactive.Checked + "'  ,'" + txtUrl.Text + "','" + txtPricePlanURL.Text + "', '" + MasterCodeVersionPath + "','" + txttemppath.Text + "', '" + txtoutputsourcepath.Text + "',"+
                                   " '" + MasterCodeVersionPath + "','" + txt_IISProductWebsiteFolder.Text + "','" + txt_MaterCodeProductVersionFolder.Text + "','" + txtMasterIISWebsite.Text + "','" + txt_mastercodepath.Text + "')";
                            cmd = new SqlCommand(str, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            lblmsg.Visible = true;
                            lblmsg.Text = "Record inserted successfully </br>.Please put link  <b> http://license.busiwiz.com/viewpriceplan.aspx?id=" + dtcln.Rows[0]["productId"].ToString() + " </b> to your site " + txtUrl.Text + " at order now link.";
                        }
                    }
                }
                else
                {
                    string strcln11 = "";
                    strcln11 = "SELECT * FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId where ProductMaster.ProductName='" + txtProductName.Text + "' and ProductDetail.VersionNo='" + txtVersionNo.Text + "' and ClientMasterId='" + Session["ClientId"].ToString() + "'";
                    SqlCommand cmdcln11 = new SqlCommand(strcln11, con);
                    DataTable dtcln11 = new DataTable();
                    SqlDataAdapter adpcln11 = new SqlDataAdapter(cmdcln11);
                    adpcln11.Fill(dtcln11);
                    if (dtcln11.Rows.Count > 0)
                    {
                        lblmsg.Text = "Product with this Product Name and Version is already Exists... ";
                    }
                    else
                    {
                        str = "INSERT INTO ProductMaster(ProductName,ProductURL, PricePlanURL ,ClientMasterId,loginurlforuser,Download,Pimage,otheimage,Topimg,Bottomimage,Leftimage,Rightimage,Frontimage,Backimage,Description) " +
                                   "VALUES('" + txtProductName.Text + "','" + txtUrl.Text + "','" + txtPricePlanURL.Text + "'," + Session["ClientId"].ToString() + ",'" + chkboxActiveDeactive.Checked + "','" + chkboxActiveDeactive0.Checked + "','" + fileup + "','" + chkimg.Checked + "','" + fileuptop + "','" + fileupbottom + "','" + fileupleft + "','" + fileupright + "','" + fileupfront + "','" + fileupback + "','" + txtdescription.Text.ToString() + "')";
                        cmd = new SqlCommand(str, con);
                        con.Close();
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        if (filemain.HasFile == true)
                        {
                            filemain.SaveAs(Server.MapPath("~\\Productimage\\") + filemain.FileName);
                        }
                        if (chkimg.Checked == true)
                        {
                            if (filetop.HasFile == true)
                            {
                                filetop.SaveAs(Server.MapPath("~\\Productimage\\") + filetop.FileName);
                            }
                            if (filebottom.HasFile == true)
                            {
                                filebottom.SaveAs(Server.MapPath("~\\Productimage\\") + filebottom.FileName);
                            }
                            if (fileright.HasFile == true)
                            {
                                fileright.SaveAs(Server.MapPath("~\\Productimage\\") + fileright.FileName);
                            }
                            if (fileleft.HasFile == true)
                            {
                                fileleft.SaveAs(Server.MapPath("~\\Productimage\\") + fileleft.FileName);
                            }
                            if (filefront.HasFile == true)
                            {
                                filefront.SaveAs(Server.MapPath("~\\Productimage\\") + filefront.FileName);
                            }
                            if (fileback.HasFile == true)
                            {
                                fileback.SaveAs(Server.MapPath("~\\Productimage\\") + fileback.FileName);
                            }
                        }
                      
                        strcln = "SELECT * FROM ProductMaster order by productId desc";
                        cmdcln = new SqlCommand(strcln, con);
                        dtcln = new DataTable();
                        adpcln = new SqlDataAdapter(cmdcln);
                        adpcln.Fill(dtcln);
                        if (dtcln.Rows.Count > 0)
                        {

                            str = "INSERT INTO ProductDetail(ProductId,VersionNo, Active ,startDate, EndDate) " +
                                  " VALUES('" + dtcln.Rows[0]["productId"].ToString() + "','" + txtVersionNo.Text + "' ,  '" + chkboxActiveDeactive.Checked + "' ,'" + txtStartdate.Text + "','" + txtEndDate.Text + "')";
                            cmd = new SqlCommand(str, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            //str = " INSERT INTO VersionInfoMaster(VersionInfoname,productid, Active,ProductURL,PricePlanURL,MasterCodeSourcePath,TemporaryPublishPath,DestinationPath,MasterIISWebsitePath) " +
                            //      " VALUES('" + txtVersionNo.Text + "','" + dtcln.Rows[0]["productId"].ToString() + "' ,  '" + chbversionactive.Checked + "'  ,'" + txtUrl.Text + "','" + txtPricePlanURL.Text + "','" + MasterCodeVersionPath + "','" + txttemppath.Text + "','" + txtoutputsourcepath.Text + "','" + MasterIISWebsite + "')";

                            str = "INSERT INTO VersionInfoMaster(VersionInfoname,productid, Active,ProductURL,PricePlanURL,MasterCodeSourcePath,TemporaryPublishPath,DestinationPath," +
                                " MasterCodeLatestVersionFolderFullPath,IISProductWebsiteFolder,MaterCodeProductVersionFolder,ServerMasterCodeSourceIISWebsitePath  ,ServerMasterCodeVersionPath) " +
                                " VALUES('" + txtVersionNo.Text + "','" + dtcln.Rows[0]["productId"].ToString() + "' ,  '" + chbversionactive.Checked + "'  ,'" + txtUrl.Text + "','" + txtPricePlanURL.Text + "', '" + MasterCodeVersionPath + "','" + txttemppath.Text + "', '" + txtoutputsourcepath.Text + "'," +
                                 " '" + MasterCodeVersionPath + "','" + txt_IISProductWebsiteFolder.Text + "','" + txt_MaterCodeProductVersionFolder.Text + "','" + txtMasterIISWebsite.Text + "','" + txt_mastercodepath.Text + "')";
                      

                            cmd = new SqlCommand(str, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            //--------------------------------------
                            //-------------------------------------
                            string str2 = "select MAX(VersionInfoId) as VersionInfoId from VersionInfoMaster ";
                            SqlCommand cmd2 = new SqlCommand(str2, con);
                            SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                            DataSet ds2 = new DataSet();
                            adp2.Fill(ds2);
                            int i = 0;
                            while (i < ListBox1.Items.Count)
                            {
                                string strr = "Insert Into VersionInfoMasterDeleteFolder (VersionInfoId, VersionDeleteFolderPath)values('" + ds2.Tables[0].Rows[0]["VersionInfoMaster"] + "','" + ListBox1.Items[i].Text + "')";
                                SqlCommand cmd11 = new SqlCommand(strr, con);
                                con.Open();
                                cmd11.ExecuteNonQuery();
                                con.Close();
                                i++;
                            }
                           
                            //-------------------------------------
                            lblmsg.Visible = true;
                            lblmsg.Text = "Record inserted successfully </br>.Please put link  <b> http://license.busiwiz.com/viewpriceplan.aspx?id=" + dtcln.Rows[0]["productId"].ToString() + " </b> to your site " + txtUrl.Text + " at order now link.";
                        }
                    }
                }
                chk_productdesc.Checked = false;
                txtdescription.Visible = false;
                txtPricePlanURL.Enabled = true;
                txtProductName.Enabled = true;
                txtUrl.Enabled = true;
                txtVersionNo.Text = "";
                txtPricePlanURL.Text = "";
                hdnProductDetailId.Value = "";
                hdnProductId.Value = "";
                txtEndDate.Text = "";
                txtStartdate.Text = "";
                txtProductName.Text = "";
                txtUrl.Text = "";
                txtdescription.Text = "";
                btnSubmit.Text = "Submit";
                chkboxActiveDeactive.Checked = false;
                chbversionactive.Checked = false;  
                chkboxActiveDeactive0.Checked = false;
                chkimg.Checked = false;
                FillGrid();
                addnewpanel.Visible = true;
                pnladdnew.Visible = false;
                Label19.Text = "";
            }
        }
        catch (Exception eerr)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error : " + eerr.Message;

        }



    }
    protected void link2_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        Label19.Text = "Edit Product or Version";
        lblmsg.Text = "";
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;
        Label lblVid = (Label)GridView1.Rows[rinrow].FindControl("lblVid");
        Label lblpid = (Label)GridView1.Rows[rinrow].FindControl("lblpid");
        Label lblProductDetailId = (Label)GridView1.Rows[rinrow].FindControl("lblProductDetailId");
        hdnProductDetailId.Value = lblProductDetailId.ToString();

        string strcln = " SELECT   ProductMaster.Description,ProductMaster.otheimage,  ProductMaster.ProductId,ProductMaster.Download, ProductMaster.ClientMasterId, ProductMaster.ProductName, ProductMaster.ProductURL, ProductMaster.PricePlanURL, " +
                        " ProductDetail.ProductDetailId, ProductDetail.ProductId AS Expr1, ProductDetail.VersionNo, ProductDetail.Active, ProductDetail.Startdate, ProductDetail.EndDate, " +
                        " ProductDetail.ProductSetup, ProductDetail.ProductDb, ProductDetail.ProductExtra ,dbo.VersionInfoMaster.MasterCodeSourcePath,dbo.VersionInfoMaster.TemporaryPublishPath, dbo.VersionInfoMaster.DestinationPath  " +
                        " ,dbo.VersionInfoMaster.Versioninfoid,VersionInfoMaster.ClientFTPRootPath ,VersionInfoMaster.Active as versionActive,dbo.VersionInfoMaster.MasterCodeLatestVersionFolderFullPath, dbo.VersionInfoMaster.IISProductWebsiteFolder, dbo.VersionInfoMaster.MaterCodeProductVersionFolder, dbo.VersionInfoMaster.ServerMasterCodeSourceIISWebsitePath, dbo.VersionInfoMaster.ServerMasterCodeVersionPath FROM            dbo.VersionInfoMaster INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId LEFT OUTER JOIN " +
                        " dbo.ProductDetail ON dbo.ProductMaster.ProductId = dbo.ProductDetail.ProductId " +
                        " where   ProductDetail.ProductDetailId= " + lblProductDetailId.Text + " and  dbo.VersionInfoMaster.VersionInfoId=" + lblVid.Text + "";
      

        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        pnlProduct.Visible = false;
        if (dtcln.Rows.Count > 0)
        {
            ViewState["VerID"] = dtcln.Rows[0]["Versioninfoid"].ToString();
            ViewState["ProductDetailId"] = dtcln.Rows[0]["ProductDetailId"].ToString();
            hdnProductId.Value = dtcln.Rows[0]["ProductId"].ToString();
            ViewState["PID"] = hdnProductId.Value;
            txtPricePlanURL.Text = dtcln.Rows[0]["PricePlanURL"].ToString();
            txtProductName.Text = dtcln.Rows[0]["ProductName"].ToString();
            txtUrl.Text = dtcln.Rows[0]["ProductURL"].ToString();
            txtStartdate.Text = Convert.ToDateTime(dtcln.Rows[0]["Startdate"].ToString()).ToShortDateString();
            txtEndDate.Text = Convert.ToDateTime(dtcln.Rows[0]["EndDate"].ToString()).ToShortDateString();
            txtVersionNo.Text = dtcln.Rows[0]["VersionNo"].ToString();



            txt_IISProductWebsiteFolder.Text = dtcln.Rows[0]["IISProductWebsiteFolder"].ToString();
            txt_MaterCodeProductVersionFolder.Text = dtcln.Rows[0]["MaterCodeProductVersionFolder"].ToString();

            //txtMasterIISWebsite.Text = dtcln.Rows[0]["MasterCodeSourcePath"].ToString();
            //txt_MasterIISWebsitePathProductPath.Text = dtcln.Rows[0]["IISWebsiteFolder"].ToString();            
            FillgridFTP();

            txttemppath.Text = dtcln.Rows[0]["TemporaryPublishPath"].ToString();
            txtoutputsourcepath.Text = dtcln.Rows[0]["DestinationPath"].ToString();
           // txtMasterFTPPath.Text = dtcln.Rows[0]["ClientFTPRootPath"].ToString();

            FillgridFTP();
            txtUrl.Enabled = true;
            chkboxActiveDeactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
            try
            {
                chbversionactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["versionActive"].ToString());

                chkboxActiveDeactive0.Checked = Convert.ToBoolean(dtcln.Rows[0]["Download"].ToString());
            }
            catch (Exception ex)
            {
            }
            
            if (Convert.ToString(dtcln.Rows[0]["otheimage"]) != "")
            {
                chkimg.Checked = Convert.ToBoolean(dtcln.Rows[0]["otheimage"].ToString());
            }
            txtdescription.Text = Convert.ToString(dtcln.Rows[0]["Description"]);
            //------------------------
            string str122 = "select * from VersionInfoMasterDeleteFolder where VersionInfoId='" + ViewState["VerID"] + "'";
            SqlCommand cmd12 = new SqlCommand(str122, con);
            SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
            DataTable ds12 = new DataTable();
            adp12.Fill(ds12);
            int ii = 0;           
            while (ii < ds12.Rows.Count)
            {
                ListBox1.Items.Add(ds12.Rows[ii]["VersionDeleteFolderPath"].ToString());               
                ii++;
            }
            //---------------------------
           
           

            //------------------------------
            txtProductName.Enabled = true;
            txtPricePlanURL.Enabled = true;
            btnSubmit.Text = "Update";
            chkimg_CheckedChanged(sender, e);
        }
    }
    protected void l_btn_delete_Click(object sender, EventArgs e)
    {
       
        lblmsg.Text = "";
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;
        Label lblVid = (Label)GridView1.Rows[rinrow].FindControl("lblVid");
        Label lblpid = (Label)GridView1.Rows[rinrow].FindControl("lblpid");
        Label lblProductDetailId = (Label)GridView1.Rows[rinrow].FindControl("lblProductDetailId");
        hdnProductDetailId.Value = lblProductDetailId.ToString();
        try
        {
            string str = " SELECT ID, VersionInfoId FROM dbo.WebsiteMaster where VersionInfoId='" + lblVid.Text + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";
            }
            else
            {
                str = " SELECT ID, ProductVersionId FROM dbo.CodeTypeTbl where ProductVersionId='" + lblVid.Text + "'";
                cmd = new SqlCommand(str, con);
                adpt = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adpt.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";
                }
                else
                {

                    str = " SELECT ProductId as ProductId FROM VersionInfoMaster where VersionInfoId='" + lblVid.Text + "'";
                    cmd = new SqlCommand(str, con);
                    adpt = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adpt.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        SqlCommand mycmd2 = new SqlCommand("ProductMaster_AddDelUpdtSelect", con);
                        mycmd2.CommandType = CommandType.StoredProcedure;
                        mycmd2.Parameters.AddWithValue("@StatementType", "Delete");
                        mycmd2.Parameters.AddWithValue("@ProductId", dt.Rows[0]["ProductId"].ToString());
                        con.Open();
                        mycmd2.ExecuteNonQuery();
                        con.Close();

                        SqlCommand mycmd3 = new SqlCommand("VersionInfoMaster_AddDelUpdtSelect", con);
                        mycmd3.CommandType = CommandType.StoredProcedure;
                        mycmd3.Parameters.AddWithValue("@StatementType", "Delete_WithProductId");
                        mycmd3.Parameters.AddWithValue("@ProductId", dt.Rows[0]["ProductId"].ToString());
                        con.Open();
                        mycmd3.ExecuteNonQuery();
                        con.Close();

                        SqlCommand mycmd4 = new SqlCommand("ProductDetail_AddDelUpdtSelect", con);
                        mycmd4.CommandType = CommandType.StoredProcedure;
                        mycmd4.Parameters.AddWithValue("@StatementType", "Delete_WithProductId");
                        mycmd4.Parameters.AddWithValue("@ProductId", dt.Rows[0]["ProductId"].ToString());
                        con.Open();
                        mycmd4.ExecuteNonQuery();
                        con.Close();                            
                    }

                    FillGrid();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record deleted successfully";
                }
            }
        }
        catch (Exception ex1)
        {
            lblmsg.Text = ex1.ToString();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
           
        }
        if (e.CommandName == "Delete")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            try
            {
                string str = " SELECT ID, VersionInfoId FROM dbo.WebsiteMaster where VersionInfoId='" + id + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";
                }
                else
                {
                    str = " SELECT ID, ProductVersionId FROM dbo.CodeTypeTbl where ProductVersionId='" + id + "'";
                     cmd = new SqlCommand(str, con);
                     adpt = new SqlDataAdapter(cmd);
                     dt = new DataTable();
                    adpt.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";
                    }
                    else
                    {
                        
                        str = " SELECT ProductId as ProductId FROM VersionInfoMaster where VersionInfoId='" + id + "'";
                        cmd = new SqlCommand(str, con);
                        adpt = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        adpt.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            SqlCommand mycmd2 = new SqlCommand("ProductMaster_AddDelUpdtSelect", con);
                            mycmd2.CommandType = CommandType.StoredProcedure;
                            mycmd2.Parameters.AddWithValue("@StatementType", "Delete");
                            mycmd2.Parameters.AddWithValue("@ProductId", dt.Rows[0]["ProductId"].ToString());
                            con.Open();
                            mycmd2.ExecuteNonQuery();
                            con.Close();

                           //SqlCommand mycmd3 = new SqlCommand("VersionInfoMaster_AddDelUpdtSelect", con);
                           //mycmd3.CommandType = CommandType.StoredProcedure;
                           //mycmd3.Parameters.AddWithValue("@StatementType", "Delete_WithProductId");
                           // mycmd3.Parameters.AddWithValue("@ProductId", dt.Rows[0]["ProductId"].ToString());
                           // con.Open();
                           // mycmd3.ExecuteNonQuery();
                           // con.Close();

                           // SqlCommand mycmd4 = new SqlCommand("ProductDetail_AddDelUpdtSelect", con);
                           // mycmd4.CommandType = CommandType.StoredProcedure;
                           // mycmd4.Parameters.AddWithValue("@StatementType", "Delete_WithProductId");
                           // mycmd4.Parameters.AddWithValue("@ProductId", dt.Rows[0]["ProductId"].ToString());
                           // con.Open();
                           // mycmd4.ExecuteNonQuery();
                           // con.Close();                            
                        }
                      
                        FillGrid();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record deleted successfully";
                    }                    
                }
            }
            catch (Exception ex1)
            {
                lblmsg.Text = ex1.ToString();
            }
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
               pnlProduct.Visible = false;
               btnSubmit.Text = "Submit";
               txtUrl.Enabled = true;
               RequiredFieldValidator2.Enabled = true;
               Label51.Visible = true;
               txtProductName.Enabled = true;
               txtPricePlanURL.Enabled = true;
              // lblpro.Visible = true;
               //lblurl.Visible = true;
               pnldown.Visible = true;
               pnlhideimg.Visible = true;
               pnldesc.Visible = true;
        }
        else
        {  
            FillProduct();
               pnlProduct.Visible = true;
               btnSubmit.Text = "Add";
               txtUrl.Enabled = true;
               RequiredFieldValidator2.Enabled = false;
               Label51.Visible = false;
               txtProductName.Enabled = false;
               txtPricePlanURL.Enabled = true;
               //lblpro.Visible = false;
              // lblurl.Visible = false;
               pnldown.Visible = false;
               pnlhideimg.Visible = false;
               pnldesc.Visible = false;
        }
    }
    protected void ddlProductList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductList.SelectedIndex > 0)
        {
            btnSubmit.Text = "Add";
            string strcln = "SELECT     ProductMaster.ProductId, ProductMaster.ClientMasterId, ProductMaster.ProductName, ProductMaster.ProductURL, ProductMaster.PricePlanURL, " +
                      " ProductDetail.ProductDetailId, ProductDetail.ProductId AS Expr1, ProductDetail.VersionNo, ProductDetail.Active, ProductDetail.Startdate, ProductDetail.EndDate, " +
                      " ProductDetail.ProductSetup, ProductDetail.ProductDb, ProductDetail.ProductExtra " +
                      " FROM         ProductDetail RIGHT OUTER JOIN ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId" +
                      " where   ProductMaster.ProductId= " + ddlProductList.SelectedItem.Value.ToString(); SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {                
                txtUrl.Enabled = false;
                txtProductName.Enabled = false;
                txtPricePlanURL.Enabled = false;
                txtPricePlanURL.Text = dtcln.Rows[0]["PricePlanURL"].ToString();
                txtProductName.Text = dtcln.Rows[0]["ProductName"].ToString();
                txtUrl.Text = dtcln.Rows[0]["ProductURL"].ToString();
            }
        }
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;
        Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink");
      // Response.Write("<script>Window.open(" + ctrl.Text + ",'_blank');</script>");
       // Response.Redirect("http://" + "../../" + "'"+ctrl.Text+"'");
        ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>OpenNewWin('../"+"http://"+"" + ctrl.Text + "')</script>");
         //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + ctrl.Text + "');", true);
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
      
        int rinrow = row.RowIndex;
        Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('page master.aspx?');", true);
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + ctrl.Text + "'); ",  true);
       // Response.Write("<script>Window.open('" + ctrl.Text + "','_blank');</script>");
        //Response.Write("<script>Window.open('" + ctrl.Text + "','_blank');</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>OpenNewWin('../"+"http://"+ "" + ctrl.Text + "')</script>");
        //"http://safestmall.indiaauthentic.com/ShoppingCart/Default.aspx?Cid=111&Wid=4" target="_blank" >
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
        hdnsortDir.Value = sortOrder;
        FillGrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        btnSubmit.Text = "Submit";
        pnlProduct.Visible = false;
        txtProductName.Text = "";
        txtPricePlanURL.Text = "";
        txtStartdate.Text = "";
        txtVersionNo.Text = "";
        txtUrl.Text = "";
        txtEndDate.Text = "";
        RadioButtonList1.SelectedIndex = 0;
        chkimg.Checked = false;
        chkboxActiveDeactive0.Checked = false;
        chbversionactive.Checked = false;

        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        Label19.Text = "";


    }
    protected void chkboxActiveDeactive0_CheckedChanged(object sender, EventArgs e)
    {
       
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void chkimg_CheckedChanged(object sender, EventArgs e)
    {
        if (chkimg.Checked == true)
        {
            pnlimage.Visible = true;
        }
        else
        {
            pnlimage.Visible = false;
        }
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[7].Visible = false;
            }
            //if (GridView1.Columns[6].Visible == true)
            //{
            //    ViewState["deleHide"] = "tt";
            //    GridView1.Columns[6].Visible = false;
            //}
        }
        else
        {



            Button3.Text = "Printable Version";
            Button4.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }
            //if (ViewState["deleHide"] != null)
            //{
            //    GridView1.Columns[6].Visible = true;
            //}
        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        pnladdnew.Visible = true;
        addnewpanel.Visible = false;
        lblmsg.Text = "";
        Label19.Text = "Add New Product or Version";
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        ListBox1.Items.Add(TextBox1.Text);
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {


        if (btnEdit.Text == "Edit")
        {
            TextBox1.Text = ListBox1.SelectedItem.Text;

        }
        if (btnEdit.Text == "Update")
        {
            ListBox1.SelectedItem.Text = TextBox1.Text;
            //btnEdit.Text = "Edit";
        }

        if (btnEdit.Text == "Update")
        {
            btnEdit.Text = "Edit";
        }
        else
        {
            btnEdit.Text = "Update";
        }

    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        ListBox1.Items.Remove(ListBox1.SelectedItem.Text);
        ListBox1.SelectedIndex = ListBox1.Items.Count - 1;
    }

    //----------
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
   
  
    //-------
    protected void chk_productdesc_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_productdesc.Checked == true)
        {
            txtdescription.Visible = true;
        }
        else
        {
            txtdescription.Visible = false;
        }
    }
}
