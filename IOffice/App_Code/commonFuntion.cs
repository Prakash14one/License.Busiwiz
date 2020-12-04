using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using DS.Win.BO;
using System.Xml;
using System.IO;

/// <summary>
/// Summary description for commonFuntion
/// </summary>
/// 

namespace CommonClass
{
    public class commonFuntion
    {



        public DataSet CreateDataSet<T>(List<T> list)
        {
            //list is nothing or has nothing, return nothing (or add exception handling)
            if (list == null || list.Count == 0) { return null; }

            //get the type of the first obj in the list
            var obj = list[0].GetType();

            //now grab all properties
            var properties = obj.GetProperties();

            //make sure the obj has properties, return nothing (or add exception handling)
            if (properties.Length == 0) { return null; }

            //it does so create the dataset and table
            var dataSet = new DataSet();
            var dataTable = new DataTable();

            //now build the columns from the properties
            var columns = new DataColumn[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                columns[i] = new DataColumn(properties[i].Name, properties[i].PropertyType);
            }

            //add columns to table
            dataTable.Columns.AddRange(columns);

            //now add the list values to the table
            foreach (var item in list)
            {
                //create a new row from table
                var dataRow = dataTable.NewRow();

                //now we have to iterate thru each property of the item and retrieve it's value for the corresponding row's cell
                var itemProperties = item.GetType().GetProperties();

                for (int i = 0; i < itemProperties.Length; i++)
                {
                    dataRow[i] = itemProperties[i].GetValue(item, null);
                }

                //now add the populated row to the table
                dataTable.Rows.Add(dataRow);
            }

            //add table to dataset
            dataSet.Tables.Add(dataTable);

            //return dataset
            return dataSet;
        }

        public commonFuntion()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool CheckImageFormat(string str_Extention)
        {
            string ImageExt = str_Extention.ToLower();
            if (ImageExt == ".jpg" || ImageExt == ".jpeg" || ImageExt == ".bmp" || ImageExt == ".png" || ImageExt == ".gif" || ImageExt == ".tiff")
            {
                ImageExt = "";
                return true;
            }
            else
            {
                ImageExt = "";
                return false;
            }
        }

        public static void ElseData(Image img, string strExtention)
        {
            if (strExtention.Equals(".txt"))
            {
                img.ImageUrl = CommonClass.Admin.ImagePath.FileNotepad;
            }
            else if (strExtention.Equals(".xls") || strExtention.Equals(".xla") || strExtention.Equals(".xlb") || strExtention.Equals(".xlc") || strExtention.Equals(".xld") || strExtention.Equals(".xlk") || strExtention.Equals(".xll") || strExtention.Equals(".xlm") || strExtention.Equals(".xlt") || strExtention.Equals(".xlv") || strExtention.Equals(".xlw") || strExtention.Equals(".xlsx") || strExtention.Equals(".xlsm") || strExtention.Equals(".xlsb") || strExtention.Equals(".xltm") || strExtention.Equals(".xlam"))
            {
                img.ImageUrl = CommonClass.Admin.ImagePath.FileExcel;
            }
            else if (strExtention.Equals(".doc") || strExtention.Equals(".docx") || strExtention.Equals(".docm") || strExtention.Equals(".dotm"))
            {
                img.ImageUrl = CommonClass.Admin.ImagePath.Fileword;
            }
            else if (strExtention.Equals(".pdf"))
            {
                img.ImageUrl = CommonClass.Admin.ImagePath.Filepdf;
            }
            else if (strExtention.Equals(".ppt") || strExtention.Equals(".pptx") || strExtention.Equals(".pptm") || strExtention.Equals(".potx") || strExtention.Equals(".potm") || strExtention.Equals(".ppam") || strExtention.Equals(".ppsx") || strExtention.Equals(".ppsm") || strExtention.Equals(".sldx") || strExtention.Equals(".sldm") || strExtention.Equals(".thmx"))
            {
                img.ImageUrl = CommonClass.Admin.ImagePath.Fileppt;
            }
            else if (strExtention.Equals(".zip") || strExtention.Equals(".rar"))
            {
                img.ImageUrl = CommonClass.Admin.ImagePath.FileZip;
            }
            else
            {
                img.ImageUrl = CommonClass.Admin.ImagePath.FileCOMPANYPROFILEacharyaassociates;
            }
        }

        public static string SetWebSiteURL(string str_WebSiteURL)
        {
            string strWebSiteURLCheck = string.Empty;
            strWebSiteURLCheck = str_WebSiteURL.ToLower();
            if (!str_WebSiteURL.StartsWith("http:/") && !str_WebSiteURL.StartsWith("https:/"))
            {
                return "http://" + strWebSiteURLCheck;
            }
            else
            {
                return strWebSiteURLCheck;
            }

        }

        public void MessageBoxShowWithURL(Page page, string message, string PassURL)
        {
            Literal ltr = new Literal();
            //"<script LANGUAGE='JavaScript' >alert('"+strMessageSuccess+"');document.location='" + ResolveClientUrl("~/admin/ManageZone.aspx") + "';</script>"
            ltr.Text = "<script type='text/javascript'> alert('" + message + "')==true?document.location='" + page.ResolveClientUrl(PassURL) + "':document.location='" + page.ResolveClientUrl(PassURL) + "' </script>";
            page.Controls.Add(ltr);
        }

        public void MessageBoxShow(Page page, string message)
        {
            Literal ltr = new Literal();
            ltr.Text = @"<script type='text/javascript'> alert('" + message + "') </script>";
            page.Controls.Add(ltr);
        }

        public void MessageBoxShowConfirm(Page page, string message, string OkURL, string CancelURL)
        {
            Literal ltr = new Literal();
            page.Form.Attributes.Add("class", "show-50");
            ltr.Text = @"<script type='text/javascript'> confirm('" + message + "')==true? document.location='" + page.ResolveClientUrl(OkURL) + "':document.location='" + page.ResolveClientUrl(CancelURL) + "' </script>";
            page.Controls.Add(ltr);
        }

        #region Dropdown

      

        private static DropDownList FillDropdownList(string flb_tableName, string flb_DataTextField, string flb_DataValueField, string flb_whereCauseWithAndwithoutflag, DropDownList flb_dropdownList, string SelectFieldText, bool SelectFieldTextVisible, string flb_orderby, string ExtraJoin)
        {
            string sQuery = string.Empty;

            if (flb_dropdownList != null)
            {
                flb_dropdownList.Items.Clear();
                if (!string.IsNullOrEmpty(flb_tableName) && !string.IsNullOrEmpty(flb_DataTextField) && !string.IsNullOrEmpty(flb_DataValueField))
                {
                    if (!string.IsNullOrEmpty(flb_whereCauseWithAndwithoutflag))
                    {
                        if (!flb_whereCauseWithAndwithoutflag.Trim().StartsWith("and"))
                        {
                            flb_whereCauseWithAndwithoutflag =  " where "  +flb_whereCauseWithAndwithoutflag;
                        }
                    }
                    sQuery = "Select " + flb_DataValueField + "," + flb_DataTextField + " from " + flb_tableName + " " + ExtraJoin + " "+  flb_whereCauseWithAndwithoutflag + " order By " + flb_orderby;
                    DataSet dsDropdownList = BOGeneral.GetDataset(sQuery);
                    if (dsDropdownList != null)
                    {
                        if (dsDropdownList.Tables.Count > 0)
                        {
                            DataTable dtDropdownList = dsDropdownList.Tables[0];
                            if (dtDropdownList.Rows.Count > 0)
                            {

                                flb_dropdownList.DataSource = dtDropdownList;
                                flb_dropdownList.DataTextField = flb_DataTextField;
                                flb_dropdownList.DataValueField = flb_DataValueField;
                                flb_dropdownList.DataBind();
                            }
                        }
                    }
                }
                if (SelectFieldTextVisible == true)
                {
                    flb_dropdownList.Items.Insert(0, new ListItem(" " + SelectFieldText + " ", "0"));
                }
            }
            return flb_dropdownList;
        }

        public static DropDownList dropdownServices(DropDownList ddl_ServicesdropdownList, bool ddl_ServicesSelectFieldTextVisible)
        {
            return FillDropdownList("tblProducts", "ProductName", "ProductID", "", ddl_ServicesdropdownList, "Select Services", ddl_ServicesSelectFieldTextVisible, "ProductName", "");
        }

        public static DropDownList dropdownTitles(DropDownList ddl_TitlesdropdownList, bool ddl_TitlesSelectFieldTextVisible)
        {
            return FillDropdownList("tblDescriptionTitle", "Title", "TitleID", "", ddl_TitlesdropdownList, "Select Tilte", ddl_TitlesSelectFieldTextVisible, "Title", "");
        }

        //public static DropDownList dropdownFamily(DropDownList ddl_FamilydropdownList, bool ddl_FamilySelectFieldTextVisible, string ddl_ZoneID, bool SearchByText)
        //{
        //    string WhereCauseZone = string.Empty;
        //    string InnerJoinZone = string.Empty;
        //    if (!string.IsNullOrEmpty(ddl_ZoneID))
        //    {

        //        InnerJoinZone = " INNER JOIN tblZone ON tblFamily.ZoneID_FK=tblZone.ZoneID ";
        //        if (SearchByText)
        //        {
        //            WhereCauseZone = " and tblZone.ExtraFlag!=1 and tblZone.ZoneName Like " + BOGeneral.MakeInsertable(ddl_ZoneID, false);
        //        }
        //        else
        //        {
        //            if (Microsoft.VisualBasic.Information.IsNumeric(ddl_ZoneID))
        //            {
        //                if (Convert.ToInt16(ddl_ZoneID) > 0)
        //                {
        //                    WhereCauseZone = " and tblZone.Flag!=1 and tblFamily.ZoneID_FK=" + BOGeneral.MakeInsertable(ddl_ZoneID, false);
        //                }
        //            }
        //        }

        //    }
        //    return FillDropdownList("tblFamily", "FamilyCode", "FamilyID", WhereCauseZone, ddl_FamilydropdownList, "Select Family", ddl_FamilySelectFieldTextVisible, "FamilyCode", InnerJoinZone);
        //}

       
        //public static DropDownList dropdownCategory(DropDownList ddl_CategorydropdownList, bool ddl_CategorySelectFieldTextVisible)
        //{
        //    return FillDropdownList("tblCategory", "CategoryName", "CategoryID", "", ddl_CategorydropdownList, "Select Category", ddl_CategorySelectFieldTextVisible, "CategoryName", "");
        //}

        //public static DropDownList dropdownCountry(DropDownList ddl_CountrydropdownList, bool ddl_CountrySelectFieldTextVisible)
        //{
        //    return FillDropdownList("tblCountry", "CountryName", "CountryID", "", ddl_CountrydropdownList, "Select Country", ddl_CountrySelectFieldTextVisible, "CountryName", "");
        //}

        //public static DropDownList dropdownMfgCategory(DropDownList ddl_MfgCategorydropdownList, bool ddl_MfgCategorySelectFieldTextVisible)
        //{
        //    return FillDropdownList("tblMfgCat", "MfgCatName", "MfgCatID", "", ddl_MfgCategorydropdownList, "Select Mfg Category", ddl_MfgCategorySelectFieldTextVisible, "MfgCatName", "");
        //}


        #endregion

        #region CheckDate

        public bool CheckDate(string strDate)
        {
            //string[] strMonths = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };


            //Check if date is valid

            bool dateIsValid = true;

            //Prompt user for input
            //Console.WriteLine("Please enter date...");
            string inputDate = Convert.ToString(strDate);
            string[] dateParts = inputDate.Split('/');

            int iDay = int.Parse(dateParts[0]);
            int iMonth = int.Parse(dateParts[1]);
            int iYear = int.Parse(dateParts[2]);

            //Check date validity and leap year
            if ((iYear % 4 == 0 && iYear % 100 != 0) || (iYear % 400 == 0))
            {
                if (iMonth == 2 && iDay > 29)
                {
                    dateIsValid = false;
                }
            }
            else if ((iDay > 31 || iDay < 1) || (iMonth == 2 && iDay > 28))
            {
                dateIsValid = false;
            }
            else if ((iMonth > 12) || (iMonth < 1))
            {
                dateIsValid = false;
            }
            if ((iMonth == 4 || iMonth == 6 || iMonth == 9 || iMonth == 11) && iDay > 30)
            {
                dateIsValid = false;
            }
            //dateIsValid = true;

            //If Date is Valid
            if (dateIsValid)
            {
                //string Output = iDay + " " + strMonths[iMonth - 1] + ", " + iYear;
                //Console.WriteLine("\n" + Output + "\n");
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region DatetimeformatChange

        // mm/dd/yyyy to dd/mm/yyyy
        public string MMDDYYYYTODDMMYYYY(string sDate)
        {

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            DateTime dtProjectStartDate = Convert.ToDateTime(sDate);

            //char[] myChars = { '/' };
            //string sdate = sDate;
            //string sday = sdate.Substring(0, sdate.IndexOfAny(myChars));
            //sdate = sdate.Replace(sday + "/", "");
            //string sMonth = sdate.Substring(0, sdate.IndexOfAny(myChars));
            //sdate = sdate.Replace(sMonth + "/", "");
            //string sYear = sdate.Substring(0);
            //string strdatetime = sMonth + "/" + sday + "/" + sYear;
            return Convert.ToString(dtProjectStartDate.ToShortDateString());
        }

        // mm/dd/yyyy to dd/mm/yyyy
        public string DDMMYYYYTOMMDDYYYY(string sDate1)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            DateTime dtProjectStartDate = Convert.ToDateTime(sDate1);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime dtProjectStartDate1 = Convert.ToDateTime(dtProjectStartDate);
            //char[] myChars = { '/' };
            //string sdate = Convert.ToDateTime(sDate).ToShortDateString().ToString();
            //string sMonth = sdate.Substring(0, sdate.IndexOfAny(myChars));
            //sdate = sdate.Replace(sMonth + "/", "");
            //string sday = sdate.Substring(0, sdate.IndexOfAny(myChars));
            //sdate = sdate.Replace(sday + "/", "");
            //string sYear = sdate.Substring(0);
            //string strdatetime = sday + "/" + sMonth + "/" + sYear;
            return Convert.ToString(dtProjectStartDate1.ToShortDateString());
        }

        #endregion


        public bool ExistsQuery(string CheckID, string CheckName, string tableName, string attributeId1, string attributeName2, string attributeName3, string CheckParentID)
        {
            string sQuery = string.Empty;
            if (!string.IsNullOrEmpty(CheckID))
            {
                sQuery = BOGeneral.MakeQueryForDataExistChecking(tableName, attributeName2 + "=" + BOGeneral.MakeInsertable(CheckName, true) + "", attributeId1 + "<>" + CheckID + "", attributeName3 + "=" + CheckParentID);
            }
            else
            {
                sQuery = BOGeneral.MakeQueryForDataExistChecking(tableName, attributeName2 + "=" + BOGeneral.MakeInsertable(CheckName, true) + "",  attributeName3 + "=" + CheckParentID);
            }
            if (clsGeneral.IsDataExists(sQuery, "Exists"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}