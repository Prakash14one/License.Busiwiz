using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DS.Win.BO;

/// <summary>
/// Summary description for clsGeneral
/// </summary>
public class clsGeneral
{
    //static public bool IsTrial = false;
    //public static long RetCustID = 0;
    //public static int DEH = 0;
    //public static int ChkMin = 0;
    //public static float SerTax = 0;
    //public static string RoomNo = "";
    //public static bool IsFromDashBoard = false;
    //public static long SelID = 0;

    //public static long FCID = 0;
    //public static int CustID = 0;

    //public static bool IsFromRoomBook = false;
    //public static bool IsFromList = false;

    //public static string Mode = "";
    //public static string gridAmountFormat = "N2";
    ////DataView oDVButton;
    //public static clsFormList FormList = new clsFormList();
    //static public string college = "";

    public enum enumDropDownType
    {
        UserList = 1,
        AllCompany = 2,
        CompanyList = 3,
        RoleList = 4,
        CategoryList = 5,
        KeywordList = 6,
        CityList = 7,
        AreaList = 8,
        PackList
    }
    public static void BindDropDown(DropDownList ddl, enumDropDownType DDLType, string SelectString, string WhereClause)
    {
        DataSet oDS;
        oDS = GetBindDropDownDataSet(DDLType.ToString(), SelectString, WhereClause);
        PopulateDropDownList(ddl, oDS.Tables[0]);
    }
    public static DataSet GetBindDropDownDataSet(string DDLTypeName, string SelectString, string WhereClause)
    {
        DataSet oDS;
        string OrderBy = "";

        if (SelectString == null || SelectString == "")
        {
            SelectString = "-----Select-----";
        }
        if (WhereClause.IndexOf("-999999") > 0)
        {
            oDS = new DataSet();
            oDS.Tables.Add(new DataTable());
            if (SelectString != null && SelectString != "")
            {
                oDS.Tables[0].Columns.Add("TextField", Type.GetType("System.String"));
                oDS.Tables[0].Columns.Add("ValueField", Type.GetType("System.String"));
                DataRow dr = oDS.Tables[0].NewRow();
                dr["TextField"] = SelectString;
                dr["ValueField"] = "-999999";
                oDS.Tables[0].Rows.InsertAt(dr, 0);
            }
            return oDS;
        }

        string SQL = "";
        DDLTypeName = DDLTypeName.ToUpper();

        if (DDLTypeName == enumDropDownType.UserList.ToString().ToUpper())
        {
            //OrderBy = "order by TextField";
            if (WhereClause == "")
            { SQL = "Select UserName as TextField, UserID as ValueField from UserMast "; }
            else
            { SQL = "Select UserName as TextField, UserID as ValueField from UserMast Where " + WhereClause + " "; }
            SQL += " order by TextField";
        }
        else if (DDLTypeName == enumDropDownType.AllCompany.ToString().ToUpper())
        {
            SQL = "Select CmpName as TextField, CmpID as ValueField from cmpmast order by CmpName";
        }
        else if (DDLTypeName == enumDropDownType.CompanyList.ToString().ToUpper())
        {
            OrderBy = "order by cm.CmpName";
            if (WhereClause == "")
            {
                SQL = "Select Distinct CmpName as TextField, cm.CmpID as ValueField from cmpmast cm inner join userrights ur ";
                SQL += "on cm.CmpID = ur.cmpid ";
            }
            else
            {
                SQL = "Select Distinct CmpName as TextField, cm.CmpID as ValueField from cmpmast cm inner join userrights ur ";
                SQL += "on cm.CmpID = ur.cmpid Where " + WhereClause + " ";
            }
            SQL += " order by TextField";
        }
        else if (DDLTypeName == enumDropDownType.RoleList.ToString().ToUpper())
        {
            OrderBy = "order by RoleName";
            if (WhereClause == "")
            { SQL = "Select RoleName as TextField, RoleID as ValueField from RoleMst "; }
            else
            { SQL = "Select RoleName as TextField, RoleID as ValueField from RoleMst Where " + WhereClause + " "; }
            SQL += " order by TextField";
        }
        else if (DDLTypeName == enumDropDownType.CategoryList.ToString().ToUpper())
        {
            OrderBy = "order by CategoryName";
            if (WhereClause == "")
            { SQL = "Select CategoryName as TextField, CategoryID as ValueField from tblCategory Where Flag=0 or Flag=null"; }
            else
            { SQL = "Select CategoryName as TextField, CategoryID as ValueField from tblCategory Where (Flag=0 or Flag=null) and " + WhereClause + " "; }
            SQL += " order by TextField";
        }
        else if (DDLTypeName == enumDropDownType.KeywordList.ToString().ToUpper())
        {
            OrderBy = "order by KeywordName";
            if (WhereClause == "")
            { SQL = "Select KeywordName as TextField, KeywordID as ValueField from tblKeyword Where Flag=0 or Flag=null "; }
            else
            { SQL = "Select KeywordName as TextField, KeywordID as ValueField from tblKeyword Where (Flag=0 or Flag=null) and " + WhereClause + " "; }
            SQL += " order by TextField";
        }
        else if (DDLTypeName == enumDropDownType.CityList.ToString().ToUpper())
        {
            OrderBy = "order by CityName";
            if (WhereClause == "")
            { SQL = "Select CityName as TextField, CityID as ValueField from tblCity Where Flag=0 or Flag=null "; }
            else
            { SQL = "Select CityName as TextField, CityID as ValueField from tblCity Where (Flag=0 or Flag=null) and " + WhereClause + " "; }
            SQL += " order by TextField";
        }
        else if (DDLTypeName == enumDropDownType.AreaList.ToString().ToUpper())
        {
            OrderBy = "order by AreaName";
            if (WhereClause == "")
            { SQL = "Select AreaName as TextField, AreaID as ValueField from tblArea Where Flag=0 or Flag=null "; }
            else
            { SQL = "Select AreaName as TextField, AreaID as ValueField from tblArea Where (Flag=0 or Flag=null) and " + WhereClause + " "; }
            SQL += " order by TextField";
        }
        else if (DDLTypeName == enumDropDownType.PackList.ToString().ToUpper())
        {
            OrderBy = "order by PackName";
            if (WhereClause == "")
            { SQL = "Select PackName as TextField, PackID as ValueField from tblPackage Where Flag=0 or Flag=null "; }
            else
            { SQL = "Select PackName as TextField, PackID as ValueField from tblPackage Where (Flag=0 or Flag=null) and " + WhereClause + " "; }
            SQL += " order by TextField";
        }

        #region DataSet Function
        oDS = BOGeneral.GetDataset(SQL);
        if (oDS != null)
        {
            if (oDS.Tables.Count > 0)
            {
                if (SelectString != null && SelectString != "")
                {
                    DataRow dr = oDS.Tables[0].NewRow();
                    dr["TextField"] = SelectString;
                    dr["ValueField"] = "-999999";
                    oDS.Tables[0].Rows.InsertAt(dr, 0);
                }
            }
        }
        return oDS;
        #endregion
    }
    public static void PopulateDropDownList(DropDownList ddl, DataTable DataSource)
    {
        ddl.DataSource = DataSource.DefaultView;
        ddl.DataTextField = "TextField";
        ddl.DataValueField = "ValueField";
    }

    public static bool IsDataExists(string SQL, string Message)
    {
        bool retValue = false;
        if (SQL == "" || SQL == null)
        {
            return retValue;
        }

        if (Message == "" || Message == null)
        {
            Message = "Data can't be deleted";
        }
        DataSet oDS = BOGeneral.GetDataset(SQL);
        foreach (DataTable oDT in oDS.Tables)
        {
            if (oDT.Rows.Count > 0)
            {
                retValue = true;
                //DSExceptions.Messages.Add("", Message, ExceptionType.Information);
                break;
            }
        }
        oDS.Dispose();
        return retValue;
    }

    //void btnClose_Click(object sender, EventArgs e)
    //{
    //    Form oForm = ((Form)((Button)(sender)).Parent);
    //    oForm.Close();
    //}
    //void btnMin_Click(object sender, EventArgs e)
    //{
    //    Form oForm = ((Form)((Button)(sender)).Parent);
    //    oForm.WindowState = FormWindowState.Minimized;
    //}
    //void SetControlStyle(Form oForm)
    //{ //int intWidth, intHeight;
    //    //intWidth = oForm.Width;
    //    //intHeight = oForm.Height;
    //    //Bitmap bmForm;
    //    //#region Set Background Image
    //    //if (intWidth > 900)
    //    //{
    //    //  if (intHeight > 575)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_950_625;
    //    //  }
    //    //  else if (intHeight > 475)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_950_525;
    //    //  }
    //    //  else if (intHeight > 375)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_950_425;
    //    //  }
    //    //  else
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_950_325;
    //    //  }
    //    //}
    //    //else if (intWidth > 800)
    //    //{
    //    //  if (intHeight > 575)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_850_625;
    //    //  }
    //    //  else if (intHeight > 475)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_850_525;
    //    //  }
    //    //  else if (intHeight > 375)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_850_425;
    //    //  }
    //    //  else
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_850_325;
    //    //  }
    //    //}
    //    //else if (intWidth > 600)
    //    //{
    //    //  if (intHeight > 575)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.From_750_625;
    //    //  }
    //    //  else if (intHeight > 475)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.From_750_525;
    //    //  }
    //    //  else if (intHeight > 375)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.From_750_425;
    //    //  }
    //    //  else
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.From_750_325;
    //    //  }
    //    //}
    //    //else if (intWidth > 500)
    //    //{
    //    //  if (intHeight > 575)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_650_625;
    //    //  }
    //    //  else if (intHeight > 475)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_650_525;
    //    //  }
    //    //  else if (intHeight > 375)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_650_425;
    //    //  }
    //    //  else
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_650_325;
    //    //  }
    //    //}
    //    //else if (intWidth > 400)
    //    //{
    //    //  if (intHeight > 575)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.From_550_625;
    //    //  }
    //    //  else if (intHeight > 475)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.From_550_525;
    //    //  }
    //    //  else if (intHeight > 375)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.From_550_425;
    //    //  }
    //    //  else
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.From_550_325;
    //    //  }
    //    //}
    //    //else
    //    //{
    //    //  if (intHeight > 575)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_450_625;
    //    //  }
    //    //  else if (intHeight > 475)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_450_525;
    //    //  }
    //    //  else if (intHeight > 375)
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_450_425;
    //    //  }
    //    //  else
    //    //  {
    //    //    bmForm = global::DSWinApp.Properties.Resources.Form_450_325;
    //    //  }
    //    //}
    //    //#endregion

    //    //oForm.BackColor = Color.White;
    //    //oForm.FormBorderStyle = FormBorderStyle.None;
    //    //oForm.BackgroundImage = bmForm;
    //    //oForm.BackgroundImageLayout = ImageLayout.Stretch;
    //    //#region Set Control Style
    //    //foreach (Control ctrl in oForm.Controls)
    //    //{
    //    //  switch (ctrl.GetType().ToString())
    //    //  {
    //    //    case "System.Windows.Forms.Button":
    //    //      ctrl.BackgroundImage = global::DSWinApp.Properties.Resources.button2;
    //    //      ctrl.BackgroundImageLayout = ImageLayout.Stretch;
    //    //      ctrl.Size = new Size(82, 29);
    //    //      ctrl.ForeColor = Color.White;
    //    //      ctrl.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

    //    //      for (int i = 0; i < oDVButton.Count; i++)
    //    //      {
    //    //        if (ctrl.Name == oDVButton[i]["CtrlName"].ToString())
    //    //        {
    //    //          ctrl.Enabled = false;
    //    //        }
    //    //      }

    //    //      break;
    //    //    case "System.Windows.Forms.Label":
    //    //      //ctrl.Text = "N1";
    //    //      break;
    //    //    case "System.Windows.Forms.DataGridView":
    //    //      DataGridView gdv = (DataGridView)ctrl;
    //    //      gdv.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
    //    //      //gdv.AlternatingRowsDefaultCellStyle.Font; 
    //    //      gdv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
    //    //      gdv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(168, 207, 237);
    //    //      gdv.EnableHeadersVisualStyles = false;
    //    //      gdv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(8, 89, 151);
    //    //      gdv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
    //    //      gdv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(8, 89, 151);
    //    //      gdv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
    //    //      gdv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.NotSet;
    //    //      Padding p = new Padding(0, 5, 0, 5);
    //    //      gdv.ColumnHeadersDefaultCellStyle.Padding = p;
    //    //      gdv.DefaultCellStyle.BackColor = Color.White;
    //    //      gdv.DefaultCellStyle.ForeColor = Color.Black;
    //    //      gdv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(168, 207, 237);
    //    //      gdv.DefaultCellStyle.SelectionForeColor = Color.Black;
    //    //      break;
    //    //  }
    //    //}
    //    //#endregion

    //    //#region Close and Minimise button and Form Header Label
    //    //Button btnClose = new Button();
    //    //btnClose.BackgroundImageLayout = ImageLayout.Stretch;
    //    //btnClose.Image = DSWinApp.Properties.Resources.close2;
    //    //btnClose.Size = new System.Drawing.Size(25, 20);
    //    //btnClose.UseVisualStyleBackColor = true;
    //    //btnClose.Location = new Point(oForm.Width - 45, 1);
    //    //btnClose.Click += new System.EventHandler(btnClose_Click);
    //    //oForm.Controls.Add(btnClose);

    //    //Button btnMin = new Button();
    //    //btnMin.BackgroundImageLayout = ImageLayout.Stretch;
    //    //btnMin.Image = DSWinApp.Properties.Resources.mnemice2;
    //    //btnMin.Size = new System.Drawing.Size(25, 20);
    //    //btnMin.UseVisualStyleBackColor = true;
    //    //btnMin.Location = new Point(oForm.Width - 70, 1);
    //    //btnMin.Click += new System.EventHandler(btnMin_Click);
    //    //oForm.Controls.Add(btnMin);

    //    //Label lblTitle = new Label();
    //    //lblTitle.Text = oForm.Text; //"Page Header";
    //    //lblTitle.AutoSize = true;
    //    //int lblWidth = (oForm.Width);
    //    //lblTitle.BackColor = Color.Transparent;
    //    //lblTitle.FlatStyle = FlatStyle.Popup;
    //    //lblTitle.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //    //lblTitle.ForeColor = Color.White;
    //    //oForm.Controls.Add(lblTitle);
    //    //lblTitle.Left = (lblWidth - lblTitle.Width) / 2;
    //    //lblTitle.Top = 1;
    //    //#endregion
    //}

    //void oForm_KeyDown(object sender, KeyEventArgs e)
    //{
    //    if (e.KeyValue.ToString() == "27")
    //    {
    //        if (MessageBox.Show("Are you sure You want to close form?", "Confirm Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
    //        {
    //            Form oForm = ((Form)(sender));
    //            oForm.Close();
    //        }
    //    }
    //    else if (e.KeyCode == Keys.Enter)
    //    {
    //        Form oForm = ((Form)(sender));
    //        if (oForm.ActiveControl.GetType().ToString() == "System.Windows.Forms.TextBox")
    //        {
    //            TextBox txt = (TextBox)oForm.ActiveControl;
    //            if (txt.Multiline == true)
    //            {

    //            }
    //            else
    //            {
    //                SendKeys.Send("{TAB}");
    //            }
    //        }
    //        else
    //        {
    //            SendKeys.Send("{TAB}");
    //        }

    //    }
    //    else if (e.KeyCode == Keys.F10)
    //    {
    //        Form oForm = ((Form)(sender));
    //        Control[] ctrl = oForm.Controls.Find("btnSave", false);
    //        if (ctrl.Length == 1)
    //        {
    //            Button btn = (Button)ctrl[0];
    //            //btn.Click += new EventHandler(btnSave_Click1);
    //            btn.Focus();
    //            SendKeys.Send("{ENTER}");
    //        }
    //    }
    //}

    //void btn_Enter(object sender, EventArgs e)
    //{
    //    Button btn = ((Button)(sender));
    //    btn.BackColor = Color.Maroon;
    //    btn.ForeColor = Color.White;
    //}
    //void btn_Leave(object sender, EventArgs e)
    //{
    //    Button btn = ((Button)(sender));
    //    btn.BackColor = Color.Snow;
    //    btn.ForeColor = Color.Black;
    //}

   
    //public void formFeatures(Form oForm)
    //{
    //    oForm.KeyDown += new System.Windows.Forms.KeyEventHandler(oForm_KeyDown);
    //    oForm.KeyPreview = true;
    //    oForm.AutoScroll = true;
    //    //oForm.Font= new Font("Microsoft Sans Serif",12, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //    oForm.BackColor = Color.FromArgb(255, 255, 227);
    //    oForm.FormBorderStyle = FormBorderStyle.FixedSingle;
    //    oForm.Icon = ((System.Drawing.Icon)(global::DSWinApp.Properties.Resources.ds));
    //    oForm.MaximizeBox = false;
    //    oForm.ShowIcon = true;
    //    foreach (Control ctrl in oForm.Controls)
    //    {
    //        ctrl.ForeColor = Color.Black;
    //        switch (ctrl.GetType().ToString())
    //        {
    //            //case "System.Windows.Forms.Label":
    //            //  ctrl.Font = new Font("Microsoft Sans Serif", ctrl.Font.Size, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //            //  break;

    //            case "System.Windows.Forms.Button":
    //                if (ctrl.Name.StartsWith("btn"))
    //                {
    //                    ctrl.BackColor = Color.Snow;
    //                    ctrl.ForeColor = Color.Black;
    //                    //ctrl.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //                    ctrl.Enter += new System.EventHandler(btn_Enter);
    //                    ctrl.Leave += new System.EventHandler(btn_Leave);
    //                    ctrl.MouseEnter += new System.EventHandler(btn_Enter);
    //                    ctrl.MouseLeave += new System.EventHandler(btn_Leave);
    //                }
    //                break;
    //            case "System.Windows.Forms.TextBox":
    //                ctrl.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //                break;
    //            case "System.Windows.Forms.ComboBox":
    //                ctrl.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //                break;
    //            case "System.Windows.Forms.FlowLayoutPanel":
    //                FlowLayoutPanel pnl = (FlowLayoutPanel)ctrl;
    //                pnl.BackColor = Color.Transparent;
    //                foreach (Control CTRL in pnl.Controls)
    //                {
    //                    switch (CTRL.GetType().ToString())
    //                    {
    //                        case "System.Windows.Forms.Button":
    //                            if (CTRL.Name.StartsWith("btn"))
    //                            {
    //                                CTRL.BackColor = Color.Snow;
    //                                CTRL.ForeColor = Color.Black;
    //                                //CTRL.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //                                CTRL.Enter += new System.EventHandler(btn_Enter);
    //                                CTRL.Leave += new System.EventHandler(btn_Leave);
    //                                CTRL.MouseEnter += new System.EventHandler(btn_Enter);
    //                                CTRL.MouseLeave += new System.EventHandler(btn_Leave);
    //                            }
    //                            break;
    //                        //case "System.Windows.Forms.Label":
    //                        //  CTRL.Font = new Font("Microsoft Sans Serif", ctrl.Font.Size, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //                        //  break;
    //                    }
    //                }
    //                break;
    //            case "System.Windows.Forms.TableLayoutPanel":
    //                FlowLayoutPanel Tpnl = (FlowLayoutPanel)ctrl;
    //                Tpnl.BackColor = Color.Transparent;
    //                foreach (Control TCTRL in Tpnl.Controls)
    //                {
    //                    switch (TCTRL.GetType().ToString())
    //                    {
    //                        case "System.Windows.Forms.Button":
    //                            if (TCTRL.Name.StartsWith("btn"))
    //                            {
    //                                TCTRL.BackColor = Color.Snow;
    //                                TCTRL.ForeColor = Color.Black;
    //                                //TCTRL.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //                                TCTRL.Enter += new System.EventHandler(btn_Enter);
    //                                TCTRL.Leave += new System.EventHandler(btn_Leave);
    //                                TCTRL.MouseEnter += new System.EventHandler(btn_Enter);
    //                                TCTRL.MouseLeave += new System.EventHandler(btn_Leave);
    //                            }
    //                            break;
    //                        //case "System.Windows.Forms.Label":
    //                        //  TCTRL.Font = new Font("Microsoft Sans Serif", ctrl.Font.Size, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //                        //  break;
    //                    }
    //                }
    //                break;

    //            case "System.Windows.Forms.DataGridView":
    //                DataGridView gdv = (DataGridView)ctrl;
    //                gdv.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //                gdv.AlternatingRowsDefaultCellStyle.BackColor = Color.Ivory;
    //                //gdv.AlternatingRowsDefaultCellStyle.Font; 
    //                gdv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
    //                gdv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(110, 110, 110);
    //                gdv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 160);
    //                gdv.EnableHeadersVisualStyles = false;
    //                gdv.GridColor = Color.Black;
    //                gdv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
    //                gdv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 160);
    //                gdv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 64, 64);
    //                gdv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 160);
    //                //gdv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.NotSet;
    //                gdv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
    //                Padding p = new Padding(0, 5, 0, 5);
    //                gdv.ColumnHeadersDefaultCellStyle.Padding = p;
    //                gdv.DefaultCellStyle.BackColor = Color.White;
    //                gdv.DefaultCellStyle.ForeColor = Color.Black;
    //                gdv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(110, 110, 110);
    //                gdv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 160);
    //                break;

    //            case "System.Windows.Forms.GroupBox":
    //                GroupBox Gbox = (GroupBox)ctrl;
    //                Gbox.BackColor = Color.Transparent;
    //                foreach (Control GCTRL in Gbox.Controls)
    //                {
    //                    switch (GCTRL.GetType().ToString())
    //                    {
    //                        case "System.Windows.Forms.Button":
    //                            if (GCTRL.Name.StartsWith("btn"))
    //                            {
    //                                GCTRL.BackColor = Color.Snow;
    //                                GCTRL.ForeColor = Color.Black;
    //                                //GCTRL.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //                                GCTRL.Enter += new System.EventHandler(btn_Enter);
    //                                GCTRL.Leave += new System.EventHandler(btn_Leave);
    //                                GCTRL.MouseEnter += new System.EventHandler(btn_Enter);
    //                                GCTRL.MouseLeave += new System.EventHandler(btn_Leave);
    //                            }
    //                            break;
    //                        case "System.Windows.Forms.DataGridView":
    //                            DataGridView gBoxdv = (DataGridView)GCTRL;
    //                            gBoxdv.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //                            gBoxdv.AlternatingRowsDefaultCellStyle.BackColor = Color.Ivory;
    //                            //gBoxdv.AlternatingRowsDefaultCellStyle.Font; 
    //                            gBoxdv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
    //                            gBoxdv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(110, 110, 110);
    //                            gBoxdv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 160);
    //                            gBoxdv.EnableHeadersVisualStyles = false;
    //                            gBoxdv.GridColor = Color.Black;
    //                            gBoxdv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
    //                            gBoxdv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 160);
    //                            gBoxdv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 64, 64);
    //                            gBoxdv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 160);
    //                            //gBoxdv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.NotSet;
    //                            gBoxdv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
    //                            Padding pGbox = new Padding(0, 5, 0, 5);
    //                            gBoxdv.ColumnHeadersDefaultCellStyle.Padding = pGbox;
    //                            gBoxdv.DefaultCellStyle.BackColor = Color.White;
    //                            gBoxdv.DefaultCellStyle.ForeColor = Color.Black;
    //                            gBoxdv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(110, 110, 110);
    //                            gBoxdv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 160);
    //                            break;
    //                        //case "System.Windows.Forms.Label":
    //                        //  GCTRL.Font = new Font("Microsoft Sans Serif", ctrl.Font.Size, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    //                        //  break;
    //                    }
    //                }
    //                break;
    //            default:
    //                break;


    //        }
    //    }
    //    if (oForm.IsMdiChild)
    //    {
    //        oForm.Left = (oForm.MdiParent.Width - 13 - oForm.Width) / 2;
    //        oForm.Top = (oForm.MdiParent.Height - 98 - oForm.Height) / 2;
    //    }
    //    else
    //    {
    //        oForm.Left = (Screen.GetBounds(oForm).Width - oForm.Width) / 2;
    //        oForm.Top = (Screen.GetBounds(oForm).Height - oForm.Height) / 2;
    //    }
    //}
    //public static bool isNumericOnly(object Value)
    //{
    //    bool b = false;
    //    long a;
    //    b = long.TryParse((string)Value, out a);
    //    return b;
    //}
    //public void Center(Form oForm, Form MDI)
    //{
    //    oForm.Left = (MDI.Width - 14 - oForm.Width) / 2;
    //    oForm.Top = (MDI.Height - 88 - oForm.Height) / 2;
    //    //oForm.KeyPress += new KeyPressEventHandler(Form_KeyPress);
    //}

    //# region License
    //public string GetHDDSerialNumber()
    //{
    //    string drive = "";
    //    string sr = "QHD35RN0";
    //    try
    //    {
    //        //check to see if the user provided a drive letter
    //        //if not default it to "C"
    //        if (drive == "" || drive == null)
    //        {
    //            drive = "C";
    //        }
    //        //create our ManagementObject, passing it the drive letter to the
    //        //DevideID using WQL
    //        ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");

    //        disk.Get();
    //        //return the serial number
    //        sr = disk["VolumeSerialNumber"].ToString();
    //    }
    //    catch (Exception)
    //    { }

    //    sr = sr.Trim();
    //    if (sr.Length < 8)
    //    {
    //        sr = sr + "QHD35RN0Y";
    //    }

    //    sr = sr.Substring(0, 8);
    //    sr = sr.ToUpper();
    //    return sr;
    //}

    //public string getProductNumber()
    //{
    //    return GetHDDSerialNumber();
    //}

    //public string getColgKeyCode()
    //{
    //    string ColgKeyCode = Convert.ToString(GetColgCode() + getSrOfBaseBoard());

    //    ColgKeyCode = ColgKeyCode.Trim();
    //    if (ColgKeyCode.Length < 8)
    //    {
    //        ColgKeyCode = ColgKeyCode + "C@lgC0deK";
    //    }

    //    ColgKeyCode = ColgKeyCode.Substring(0, 8);
    //    ColgKeyCode = ColgKeyCode.ToUpper();
    //    return ColgKeyCode;
    //}

    //public string GetColgCode()
    //{
    //    string ColgCode = "";
    //    string COlgName = clsGeneral.college;
    //    if (COlgName.Length < 12)
    //    {
    //        COlgName = COlgName + "CGFDJDS@GNAM3YRPY";
    //    }
    //    ColgCode = ColgCode + COlgName.Substring(11, 1);
    //    ColgCode = ColgCode + COlgName.Substring(3, 1);
    //    ColgCode = ColgCode + COlgName.Substring(0, 1);
    //    ColgCode = ColgCode + COlgName.Substring(7, 1);

    //    ColgCode = ColgCode.Trim();
    //    if (ColgCode.Length < 4)
    //    {
    //        ColgCode = ColgCode + "$YODS";
    //    }
    //    ColgCode = ColgCode.Substring(0, 4);
    //    ColgCode = ColgCode.ToUpper();
    //    return ColgCode;
    //}

    //public string getSrOfBaseBoard()
    //{
    //    string sr = "G7H2B3D65F68";
    //    try
    //    {
    //        ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_BaseBoard");
    //        foreach (ManagementObject share in searcher.Get())
    //        {
    //            foreach (PropertyData PC in share.Properties)
    //            {
    //                if (PC.Name.ToLower() == "serialnumber")
    //                {
    //                    sr = Convert.ToString(PC.Value);
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    { }

    //    sr = sr.Trim();
    //    if (sr.Length < 12)
    //    {
    //        sr = sr + "G7H2B3D65F68Y";
    //    }
    //    sr = sr.Substring(0, 12);
    //    sr = sr.ToUpper();
    //    return sr;
    //}

    //public string GetLicenseKey(string ProductNumber, string ColgKeyCode)
    //{
    //    string LicenseKey = "";

    //    string plainText = ColgKeyCode;
    //    string passPhrase = ProductNumber;
    //    string saltValue = ProductNumber.Substring(2, 1) + ProductNumber;
    //    if (saltValue.Length < 9)
    //    {
    //        saltValue = saltValue + "s@lTV@LUE";
    //    }
    //    saltValue = saltValue.Substring(0, 9);

    //    string hashAlgorithm = "SHA1";
    //    int passwordIterations = 2;
    //    string initVector = ProductNumber + ColgKeyCode;
    //    if (initVector.Length < 16)
    //    {
    //        initVector = initVector + "1B2c3D4e5F6g7H8";
    //    }
    //    initVector = initVector.Substring(0, 16);

    //    int keySize = 256;


    //    LicenseKey = Encrypt(plainText,
    //                                            passPhrase,
    //                                            saltValue,
    //                                            hashAlgorithm,
    //                                            passwordIterations,
    //                                            initVector,
    //                                            keySize);

    //    LicenseKey = LicenseKey.Trim();
    //    if (LicenseKey.Length < 25)
    //    {
    //        LicenseKey = LicenseKey + "4@S5PR@SES@1TVALUE@1B2C3DY";
    //    }
    //    LicenseKey = LicenseKey.Substring(0, 25);
    //    LicenseKey = LicenseKey.ToUpper();
    //    return LicenseKey;
    //}

    //public static string Encrypt(string plainText,
    //                   string passPhrase,
    //                   string saltValue,
    //                   string hashAlgorithm,
    //                   int passwordIterations,
    //                   string initVector,
    //                   int keySize)
    //{

    //    byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
    //    byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);


    //    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

    //    PasswordDeriveBytes password = new PasswordDeriveBytes(
    //                                                    passPhrase,
    //                                                    saltValueBytes,
    //                                                    hashAlgorithm,
    //                                                    passwordIterations);

    //    byte[] keyBytes = password.GetBytes(keySize / 8);

    //    RijndaelManaged symmetricKey = new RijndaelManaged();

    //    symmetricKey.Mode = CipherMode.CBC;

    //    ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
    //                                                     keyBytes,
    //                                                     initVectorBytes);

    //    MemoryStream memoryStream = new MemoryStream();

    //    CryptoStream cryptoStream = new CryptoStream(memoryStream,
    //                                                 encryptor,
    //                                                 CryptoStreamMode.Write);
    //    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

    //    cryptoStream.FlushFinalBlock();

    //    byte[] cipherTextBytes = memoryStream.ToArray();

    //    memoryStream.Close();
    //    cryptoStream.Close();

    //    string cipherText = Convert.ToBase64String(cipherTextBytes);

    //    return cipherText;
    //}




    //public bool IsHaveLicense(string EnteredKey)
    //{
    //    string dbkey = Convert.ToString(BOGeneral.GetScalar("select Value from setting where SetName='" + getProductNumber() + "'"));
    //    if (EnteredKey == "")
    //    {
    //        EnteredKey = GetLicenseKey(getProductNumber(), getColgKeyCode());
    //    }
    //    else
    //    {
    //        dbkey = GetLicenseKey(getProductNumber(), getColgKeyCode());
    //    }

    //    if (EnteredKey == dbkey)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //#endregion
}