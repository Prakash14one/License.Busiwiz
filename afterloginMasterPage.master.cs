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
using System.IO;
using System.Text;
using System.Net;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Diagnostics;

public partial class MasterPage : System.Web.UI.MasterPage
{
    string ClientId1;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        //      ClientId1 = Session["ClientId1"].ToString();

        if (Session["ClientName"] != null)
        {
            lblclientname.Text = Session["ClientName"].ToString();
        }
        if (!IsPostBack)
        {
            PopulateMenu();
        }

    }


    protected void Lnkbtn1_Click(object sender, EventArgs e)
    {
        System.Diagnostics.ProcessStartInfo pinfo = new System.Diagnostics.ProcessStartInfo("cmd.exe");
        pinfo.RedirectStandardError = true;
        pinfo.RedirectStandardInput = true;
        pinfo.UseShellExecute = false;
        pinfo.RedirectStandardOutput = true;

        pinfo.FileName = "C:\\windows\\system32\\cmd.exe";
        System.Diagnostics.Process console = System.Diagnostics.Process.Start(pinfo);

        console.WaitForExit();
        // console.StandardInput.WriteLine(@"/C net start ""MonitorWebsite"" ");

        //  Process p = new Process();
        //  //string result = "";
        //  p.StartInfo.UseShellExecute = false;
        //  p.StartInfo.CreateNoWindow = true;
        //  p.StartInfo.RedirectStandardInput = true;
        //  p.StartInfo.RedirectStandardOutput = true;
        // // p.StartInfo.UserName = @"ashimo01";
        ////  p.StartInfo.Password = "nnn";
        //  p.StartInfo.FileName = @"C:\WINDOWS\system32\cmd.exe";
        // // p.StartInfo.Arguments = @"-u enterprise\ashimo01 -p Password1 \\nawrkxp613 cmd.exe/C ipconfig";
        //  p.Start();
        //  StreamReader stdout = p.StandardOutput;
        //  p.WaitForExit();
    }
    private void PopulateMenu()
    {
        DataSet ds = GetDataSetForMenu();
        //Menu menu = new Menu();

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables["parent"].Rows.Count > 2)
            {
                foreach (DataRow parentItem in ds.Tables["parent"].Rows)
                {
                    int f1 = 0;

                    int f3 = 0;
                    MenuItem categoryItem = new MenuItem((string)parentItem["MainMenuName"]);
                    //Menu1.Items.Add(categoryItem);
                    if (categoryItem.Text != "0")
                    {
                        foreach (DataRow childItem in parentItem.GetChildRows("Children"))
                        {
                            int f2 = 0;
                            if (f1 == 0)
                            {
                                Menu1.Items.Add(categoryItem);
                                f3 += 1;
                                f1 += 1;
                            }
                            MenuItem childrenItem = new MenuItem((string)childItem["SubMenuName"]);


                            foreach (DataRow subchildItem in childItem.GetChildRows("Children2"))
                            {

                                if (f2 == 0)
                                {
                                    categoryItem.ChildItems.Add(childrenItem);

                                    f2 += 1;
                                }
                                MenuItem childrenItem111 = new MenuItem((string)subchildItem["PageId"].ToString());
                                MenuItem childrenItem11 = new MenuItem((string)subchildItem["PageTitle"]);
                                MenuItem childrenItem112 = new MenuItem((string)subchildItem["PageName"]);
                                childrenItem11.NavigateUrl = "~/" + childrenItem112.Text;

                                childrenItem.ChildItems.Add(childrenItem11);

                            }

                        }
                        foreach (DataRow childItem in parentItem.GetChildRows("Children3"))
                        {
                            if (f3 == 0)
                            {
                                Menu1.Items.Add(categoryItem);
                                f3 += 1;
                            }
                            MenuItem childrenItem111 = new MenuItem((string)childItem["PageId"].ToString());
                            MenuItem childrenItem11 = new MenuItem((string)childItem["PageTitle"]);
                            MenuItem childrenItem112 = new MenuItem((string)childItem["PageName"]);
                            childrenItem11.NavigateUrl = "~/" + childrenItem112.Text;

                            categoryItem.ChildItems.Add(childrenItem11);

                        }
                    }

                }
            }
            else
            {
                int bb = 0;
                foreach (DataRow parentItem in ds.Tables["parent"].Rows)
                {

                    MenuItem categoryItem = new MenuItem((string)parentItem["MainMenuName"]);

                    //Menu1.Items.Add(categoryItem);
                    if (categoryItem.Text != "0")
                    {

                        foreach (DataRow childItem in parentItem.GetChildRows("Children"))
                        {

                            MenuItem childrenItem = new MenuItem((string)childItem["SubMenuName"]);

                            Menu1.Items.Add(childrenItem);
                            foreach (DataRow subchildItem in childItem.GetChildRows("Children2"))
                            {





                                MenuItem childrenItem111 = new MenuItem((string)subchildItem["PageId"].ToString());
                                MenuItem childrenItem11 = new MenuItem((string)subchildItem["PageTitle"]);
                                MenuItem childrenItem112 = new MenuItem((string)subchildItem["PageName"]);
                                childrenItem11.NavigateUrl = "~/" + childrenItem112.Text;

                                childrenItem.ChildItems.Add(childrenItem11);

                            }
                            if (bb == 0)
                            {
                                foreach (DataRow childItem1 in parentItem.GetChildRows("Children3"))
                                {

                                    MenuItem childrenItem111 = new MenuItem((string)childItem1["PageId"].ToString());
                                    MenuItem childrenItem11 = new MenuItem((string)childItem1["PageTitle"]);
                                    MenuItem childrenItem112 = new MenuItem((string)childItem1["PageName"]);
                                    childrenItem11.NavigateUrl = "~/" + childrenItem112.Text;

                                    childrenItem.ChildItems.Add(childrenItem11);

                                }
                                bb += 1;
                            }

                        }


                    }

                }
            }

        }
    }
    private DataSet GetDataSetForMenu()
    {
        string main1 = "";
        string main2 = "";
        string main3 = "";
        string manuid = "";
        string rsubmanu = "";
        string rpagemanu = "";
        string submanuid = "";

        int mcount = 0;
        int scount = 0;
        string userid = "";
        if (Convert.ToString(Session["EmpId"]) != "")
        {
            userid = " and User_Role.User_id=" + Session["EmpId"];
        }

        string str121f = "SELECT Distinct RoleMenuAccessRightTbl.MenuId FROM RoleMenuAccessRightTbl INNER JOIN User_Role ON RoleMenuAccessRightTbl.RoleId = User_Role.Role_id INNER JOIN EmployeeMaster ON User_Role.User_id = EmployeeMaster.Id WHERE  EmployeeMaster.ClientId ='" + Session["ClientId"] + "'" + userid + " and RoleMenuAccessRightTbl.AccessRight<>'0' ";
        SqlDataAdapter da121f = new SqlDataAdapter(str121f, con);
        DataTable dt121f = new DataTable();
        da121f.Fill(dt121f);
        foreach (DataRow dts in dt121f.Rows)
        {
            main1 += "'" + dts["MenuId"] + "',";
            manuid += "'" + dts["MenuId"] + "',";
        }
        if (main1.Length > 0)
        {
            main1 = main1.Remove(main1.Length - 1);

        }

        string str1211f1 = "SELECT Distinct PageMaster.MainMenuId as MenuId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId,PageMaster.PageId FROM PageMaster inner join RolePageAccessRightTbl on RolePageAccessRightTbl.PageId=PageMaster.PageId INNER JOIN User_Role ON RolePageAccessRightTbl.RoleId = User_Role.Role_id INNER JOIN EmployeeMaster ON User_Role.User_id = EmployeeMaster.Id WHERE   EmployeeMaster.ClientId ='" + Session["ClientId"] + "'" + userid + " and AccessRight<>'0' " + userid;
        SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, con);
        DataTable dt121f1 = new DataTable();
        da121f1.Fill(dt121f1);
        foreach (DataRow dts in dt121f1.Rows)
        {
            if (mcount != Convert.ToInt32(dts["MenuId"]))
            {
                mcount = Convert.ToInt32(dts["MenuId"]);
                main2 += "'" + dts["MenuId"] + "',";
                manuid += "'" + dts["MenuId"] + "',";

            }
            if (scount != Convert.ToInt32(dts["SubMenuId"]))
            {
                scount = Convert.ToInt32(dts["SubMenuId"]);

                rsubmanu += "'" + dts["SubMenuId"] + "',";
            }
            rpagemanu += "'" + dts["PageId"] + "',";

        }
        if (main2.Length > 0)
        {

            main2 = main2.Remove(main2.Length - 1);

        }


        string str1211f11 = " SELECT Distinct SubMenuMaster.MainMenuId as MenuId,SubMenuMaster.SubMenuId FROM SubMenuMaster inner join RoleSubMenuAccessRightTbl on RoleSubMenuAccessRightTbl.SubMenuId=SubMenuMaster.SubMenuId INNER JOIN User_Role ON RoleSubMenuAccessRightTbl.RoleId = User_Role.Role_id INNER JOIN  EmployeeMaster ON User_Role.User_id = EmployeeMaster.Id WHERE   EmployeeMaster.ClientId ='" + Session["ClientId"] + "'" + userid + "  and AccessRight<>'0'" + userid; //Add as MenuId at 11/28/2014
        SqlDataAdapter da121f11 = new SqlDataAdapter(str1211f11, con);
        DataTable dt121f11 = new DataTable();
        da121f11.Fill(dt121f11);
        foreach (DataRow dts in dt121f11.Rows)
        {
            main3 += "'" + dts["MenuId"] + "',";
            manuid += "'" + dts["MenuId"] + "',";
            rsubmanu += "'" + dts["SubMenuId"] + "',";

        }
        if (main3.Length > 0)
        {
            main3 = main3.Remove(main3.Length - 1);

        }
        if (manuid.Length > 0)
        {
            manuid = manuid.Remove(manuid.Length - 1);

        }
        if (rsubmanu.Length > 0)
        {
            rsubmanu = rsubmanu.Remove(rsubmanu.Length - 1);

        }
        DataSet ds = new DataSet();
        if (manuid.Length > 0)
        {
            string str = "Select Distinct MainMenuMaster.* from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join PageMaster  on PageMaster.MainMenuId = MainMenuMaster.MainMenuId where MainMenuMaster.MainMenuId in(" + manuid + ") and WebsiteMaster.compid='" + Session["ClientId"] + "' Order by MainMenuIndex";

            // string str = "SELECT distinct * FROM MainMenuMaster where MainMenuId in(" + manuid + ") Order by MainMenuIndex";

            SqlDataAdapter adpcat = new SqlDataAdapter(str, con);

            adpcat.Fill(ds, "parent");


            if (main1.Length == 0)
            {
                if (manuid.Length > 0)
                {
                    main1 = manuid;
                }
            }
            else
            {
                string ax = "Select Distinct MainMenuMaster.*,PageId from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join PageMaster  on PageMaster.MainMenuId = MainMenuMaster.MainMenuId where MainMenuMaster.MainMenuId in(" + manuid + ") and WebsiteMaster.compid='" + Session["ClientId"] + "' Order by MainMenuIndex";

                DataTable drtc = new DataTable();
                SqlDataAdapter asc = new SqlDataAdapter(ax, con);
                asc.Fill(drtc);
                foreach (DataRow dts in drtc.Rows)
                {

                    rpagemanu += "'" + dts["PageId"] + "',";
                }
            }
            string str11 = "";
            string cdrt = "";
            //if (rpagemanu.Length != 0)
            //{
            //    cdrt = " or PageMaster.PageId in(" + rpagemanu + ")";
            //}
            if (rpagemanu.Length > 0)
            {

                rpagemanu = rpagemanu.Remove(rpagemanu.Length - 1);

            }
            if (rsubmanu.Length != 0)
            {
                if (main1.Length != 0)
                {
                    str11 = " Select Distinct SubMenuMaster.* from PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId  inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  MainMenuMaster.MainMenuId In( " + main1 + ") ";


                }
                else
                {
                    str11 = " Select Distinct SubMenuMaster.* from PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where   SubMenuMaster.SubMenuId in(" + rsubmanu + ")";

                }
            }
            else
            {
                if (main1.Length != 0)
                {
                    str11 = " Select Distinct SubMenuMaster.* from PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId  inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  MainMenuMaster.MainMenuId In( " + main1 + ")" + cdrt;
                }
                else if (manuid.Length != 0)
                {
                    str11 = " Select Distinct SubMenuMaster.* from PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId  inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  MainMenuMaster.MainMenuId In( " + manuid + ")" + cdrt;
                }
            }


            SqlDataAdapter adpProduct = new SqlDataAdapter(str11, con);
            adpProduct.Fill(ds, "child");
            foreach (DataRow dts in ds.Tables["child"].Rows)
            {
                submanuid += "'" + dts["SubMenuId"] + "',";

            }
            if (submanuid.Length != 0)
            {
                submanuid = submanuid.Remove(submanuid.Length - 1);

            }

            string str15 = "";
            if (manuid.Length != 0 && rpagemanu.Length != 0)
            {
                str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster   where  (SubMenuId='0' or SubMenuId IS NULL) and (MainMenuId In( " + manuid + ")) and PageMaster.PageId in(" + rpagemanu + ") order by PageIndex ASC";
            }
            else if (main1.Length != 0)
            {

                str15 = " SELECT  distinct PageIndex, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster  where (SubMenuId='0' or SubMenuId IS NULL)  and (MainMenuId In( " + main1 + "))order by PageIndex ASC";

            }

            else if (manuid.Length != 0)
            {
                str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster where (SubMenuId='0' or SubMenuId IS NULL) and (MainMenuId In( " + manuid + ")) order by PageIndex ASC";
            }
            // string str15 = " SELECT PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where (SubMenuId='0' or SubMenuId IS NULL) and (MainMenuId In( " + main1 + "))";
            SqlDataAdapter adp115 = new SqlDataAdapter(str15, con);
            DataSet ds125 = new DataSet();
            adp115.Fill(ds, "leafchild1");
            string str1 = "";

            if (rpagemanu.Length > 0)
            {

                if (submanuid.Length > 0)
                {
                    str1 = " SELECT  distinct PageIndex, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster  where (SubMenuId in(" + submanuid + ")) and(PageMaster.PageId in(" + rpagemanu + "))order by PageIndex ASC ";

                }
                else
                {
                    str1 = " SELECT  distinct PageIndex, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster  where (PageMaster.PageId in(" + rpagemanu + "))order by PageIndex ASC ";

                }
                SqlDataAdapter adp11 = new SqlDataAdapter(str1, con);
                DataSet ds12 = new DataSet();
                adp11.Fill(ds, "leafchild");
            }
            else
            {
                if (submanuid.Length > 0)
                {
                    str1 = " SELECT   distinct PageIndex,PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster   where  (SubMenuId in(" + submanuid + "))order by PageIndex ASC";
                    SqlDataAdapter adp11 = new SqlDataAdapter(str1, con);
                    DataSet ds12 = new DataSet();
                    adp11.Fill(ds, "leafchild");
                }
                else
                {
                    str1 = " SELECT  distinct PageIndex, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster   where  (SubMenuId in(" + 00 + ")) and (PageMaster.PageId =0)order by PageIndex ASC";
                    SqlDataAdapter adp11 = new SqlDataAdapter(str1, con);
                    DataSet ds12 = new DataSet();
                    adp11.Fill(ds, "leafchild");
                    ds.Tables["leafchild"].Rows.Add(0, 0, 0, 0, 0);
                }
            }

            ds.Tables["parent"].Rows.Add(0, 0, 0, 0, 0);
            ds.Tables["child"].Rows.Add(0, 0, 0, 0, 0);

            ds.Relations.Add("Children", ds.Tables["parent"].Columns["MainMenuId"], ds.Tables["child"].Columns["MainMenuId"]);
            ds.Relations.Add("Children2", ds.Tables["child"].Columns["SubMenuId"], ds.Tables["leafchild"].Columns["SubMenuId"]);
            ds.Relations.Add("Children3", ds.Tables["parent"].Columns["MainMenuId"], ds.Tables["leafchild1"].Columns["MainMenuId"]);
        }
        return ds;
    }



    //    private DataSet GetDataSetForMenu()
    //    {

    //        string str = "Select MainMenuMaster.* from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId where WebsiteMaster.compid='"+Session["ClientId"]+"'";

    //        SqlDataAdapter adpcat = new SqlDataAdapter(str, con);
    //        DataSet ds = new DataSet();
    //        adpcat.Fill(ds, "parent");
    //        string str11 = "";


    //        str11 = " Select SubMenuMaster.* from SubMenuMaster inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  MainMenuMaster.MainMenuId In(  Select distinct MainMenuMaster.MainMenuId from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId where  WebsiteMaster.compid='"+Session["ClientId"]+"')";


    //        SqlDataAdapter adpProduct = new SqlDataAdapter(str11, con);
    //        adpProduct.Fill(ds, "child");


    //        string str15 = "SELECT PageMaster.* FROM PageMaster inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId where (SubMenuId='0' or SubMenuId IS NULL) and (PageMaster.MainMenuId In(  Select distinct MainMenuMaster.MainMenuId from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId where  WebsiteMaster.compid='"+Session["ClientId"]+"'))";
    //        SqlDataAdapter adp115 = new SqlDataAdapter(str15, con);
    //        DataSet ds125 = new DataSet();
    //        adp115.Fill(ds, "leafchild1");
    //        string str1 = "";

    //        str1 = " SELECT PageMaster.* FROM PageMaster  inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId where (SubMenuId IS NOT NULL  or SubMenuId<>'') and (PageMaster.MainMenuId In(  Select distinct MainMenuMaster.MainMenuId from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId where WebsiteMaster.compid='"+Session["ClientId"]+"'))";


    //        SqlDataAdapter adp11 = new SqlDataAdapter(str1, con);
    //        DataSet ds12 = new DataSet();
    //        adp11.Fill(ds, "leafchild");



    //        ds.Relations.Add("Children", ds.Tables["parent"].Columns["MainMenuId"], ds.Tables["child"].Columns["MainMenuId"]);
    //        ds.Relations.Add("Children2", ds.Tables["child"].Columns["SubMenuId"], ds.Tables["leafchild"].Columns["SubMenuId"]);
    //        ds.Relations.Add("Children3", ds.Tables["parent"].Columns["MainMenuId"], ds.Tables["leafchild1"].Columns["MainMenuId"]);

    //        return ds;
    //    }
   
}