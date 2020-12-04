//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

///// <summary>
///// Summary description for MessageBox1
///// </summary>
//public class MessageBox1
//{
//    public MessageBox1()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

//public class js
//{
//    public object js()
//    {
//    }
//}



public class MessageBox : WebControl
{


    public MessageBox()
        //: base("div")
    {
    }

    private string strLeft;
    private string strTop;
    private int intButton;
    private string strMessage;
    private string strTitle;
    private string strImage;
    private string strCss;
    private string strCssTitle;
    private string strCssMessage;
    private string strButtonYes;
    private string strButtonNo;
    private string strButtonCancel;
    private int strButtonWidth;
    private string strMessageBoxIDYes;
    private string strMessageBoxIDNo;
    private string strMessageBoxIDCancel;
    private int intMessageBoxWidth;
    private int intMessageBoxHeight;
    private int intMessageBoxImageWidth;
    private int intMessageBoxImageHeight;
    string homedirectory;
    // Message box left position
    public string MessageBoxLeft
    {
        get { return strLeft; }
        set { strLeft = value; }
    }
    // Message box top position
    public string MessageBoxTop
    {
        get { return strTop; }
        set { strTop = value; }
    }
    // Number of buttons you want to display in the message box
    public int MessageBoxButton
    {
        get { return intButton; }


        set { intButton = value; }
    }
    // Customize message you want to display in the message box
    public string MessageBoxMessage
    {
        get { return strMessage; }
        set { strMessage = value; }
    }
    // Title you want to display in the message box
    public string MessageBoxTitle
    {
        get { return strTitle; }
        set { strTitle = value; }
    }
    // Image you want to display in the message box (like information / warning)
    public string MessageBoxImage
    {
        get { return strImage; }
        set { strImage = value; }
    }
    // Message box ID for Yes button
    public string MessageBoxIDYes
    {
        get { return strMessageBoxIDYes; }
        set { strMessageBoxIDYes = value; }
    }
    // Message box ID for No button
    public string MessageBoxIDNo
    {
        get { return strMessageBoxIDNo; }
        set { strMessageBoxIDNo = value; }
    }
    // Message box ID for Cancel button
    public string MessageBoxIDCancel
    {
        get { return strMessageBoxIDCancel; }


        set { strMessageBoxIDCancel = value; }
    }
    // Style you want to incorporate for message box
    public string MessageBoxCss
    {
        get { return strCss; }
        set { strCss = value; }
    }
    public string MessageBoxCssTitle
    {
        get { return strCssTitle; }
        set { strCssTitle = value; }
    }
    public string MessageBoxCssMessage
    {
        get { return strCssMessage; }
        set { strCssMessage = value; }
    }
    // Message box Text for Yes button
    public string MessageBoxButtonYesText
    {
        get { return strButtonYes; }
        set { strButtonYes = value; }
    }
    // Message box Text for No button
    public string MessageBoxButtonNoText
    {
        get { return strButtonNo; }
        set { strButtonNo = value; }
    }
    // Message box Text for Cancel button
    public string MessageBoxButtonCancelText
    {
        get { return strButtonCancel; }
        set { strButtonCancel = value; }
    }


    // Message box buttons width
    public int MessageBoxButtonWidth
    {
        get { return strButtonWidth; }
        set { strButtonWidth = value; }
    }
    // Message box width
    public int MessageBoxWidth
    {
        get { return intMessageBoxWidth; }
        set { intMessageBoxWidth = value; }
    }
    // Message box height
    public int MessageBoxHeight
    {
        get { return intMessageBoxHeight; }
        set { intMessageBoxHeight = value; }
    }
    // Message box image width
    public int MessageBoxImageWidth
    {
        get { return intMessageBoxImageWidth; }
        set { intMessageBoxImageWidth = value; }
    }
    // Message box image height
    public int MessageBoxImageHeight
    {
        get { return intMessageBoxImageHeight; }
        set { intMessageBoxImageHeight = value; }
    }

    protected internal HtmlGenericControl layer;
    protected internal HtmlGenericControl ilayer;
    protected internal HtmlGenericControl img;
    protected internal HtmlGenericControl div;
    protected internal Button ButtonOK;
    protected internal Button ButtonYes;
    protected internal Button ButtonNo;
    protected internal Button ButtonCancel;


    protected System.Web.UI.WebControls.Image Alertimage;
   
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        // Default properties settings for message box control
        if (strLeft == null)
        {
            strLeft = "250";
        }

        if (strTop == null)
        {
            strTop = "250";
        }

        if (strTitle == null)
        {
            strTitle = "MessageBox";
        }
        if (intButton < 0)
        {
            intButton = 1;
        }
        if (strMessageBoxIDYes == null)
        {
            strMessageBoxIDYes = "MessageBoxIDYes";
        }
        if (strMessageBoxIDNo == null)
        {
            strMessageBoxIDNo = "MessageBoxIDNo";
        }
        if (strMessageBoxIDCancel == null)
        {
            strMessageBoxIDCancel = "MessageBoxIDCancel";
        }
        if (strCss == null)
        {
            strCss = "";
        }

        if (strCssMessage == null)
        {
            strCssMessage = "";
        }

        if (strCssTitle == null)
        {
            strCssTitle = "";
        }

        if (strMessage == null)
        {
            strMessage = "No message to display here.";
        }

        if (intButton == 1 | intButton > 3 | intButton < 1)
        {
            if (strButtonYes == null)
            {
                strButtonYes = "OK";
            }
        }
        else if (intButton > 1 & intButton < 4)
        {
            if (strButtonYes == null)
            {
                strButtonYes = "Approve";
            }
            if (strButtonNo == null)
            {


                strButtonNo = "Cancel";
            }
            if (strButtonCancel == null)
            {
                strButtonCancel = "Ignore";
            }
        }
        if (strButtonWidth < 5)
        {
            strButtonWidth = 70;
        }

        if (intMessageBoxWidth < 10)
        {
            intMessageBoxWidth = 250;
        }

        if (intMessageBoxHeight < 1)
        {
            intMessageBoxHeight = 8;
        }

        if (intMessageBoxImageWidth < 5)
        {
            intMessageBoxImageWidth = 36;
        }

        if (intMessageBoxImageHeight < 5)
        {
            intMessageBoxImageHeight = 36;
        }

        if (homedirectory == null)
        {
            homedirectory = this.Page.Request.PhysicalApplicationPath;
        }
    }

    //protected override void createChildControls()
    //{
    //    // Creating message box
    //    TableRow myRow = default(TableRow);
    //    TableCell myCell = default(TableCell);

    //    Table myTable = new Table();
    //    myTable.BorderWidth = new Unit(0);
    //    myTable.CellSpacing = 0;
    //    myTable.Width = new Unit(intMessageBoxWidth);
    //    myTable.Height = new Unit(intMessageBoxHeight);
    //    Controls.Add(myTable);

    //    myRow = new TableRow();
    //    myRow.BorderWidth = new Unit(0);
    //    myTable.Rows.Add(myRow);

    //    myCell = new TableCell();
    //    Label NewLabel = new Label();
    //    NewLabel.Text = strTitle;
    //    NewLabel.CssClass = strCssTitle;
    //    myCell.Controls.Add(NewLabel);

    //    myCell.ID = "dragbar";
    //    myCell.ColumnSpan = 5;


    //    myCell.CssClass = strCssTitle;
    //    if (string.IsNullOrEmpty(strCssTitle))
    //    {
    //        myCell.ForeColor = System.Drawing.Color.White;
    //        myCell.BackColor = System.Drawing.Color.DarkBlue;
    //        myCell.Font.Name = "Verdana";
    //        myCell.Font.Bold = true;
    //        myCell.Font.Size = new FontUnit(8);
    //        myCell.Style.Add("CURSOR", "hand");
    //    }

    //    myRow.Cells.Add(myCell);

    //    myRow = new TableRow();
    //    myRow.BorderWidth = new Unit(0);
    //    myTable.Rows.Add(myRow);

    //    myCell = new TableCell();
    //    myCell.ColumnSpan = 5;
    //    myCell.CssClass = strCssMessage;
    //    if (string.IsNullOrEmpty(strCssMessage))
    //    {
    //        myCell.BackColor = System.Drawing.Color.LightGray;
    //    }
    //    myRow.Cells.Add(myCell);

    //    TableRow myRow1 = default(TableRow);
    //    TableCell myCell1 = default(TableCell);

    //    Table myTable1 = new Table();
    //    myTable1.BorderWidth = new Unit(0);
    //    myTable1.CellSpacing = 0;

    //    myCell.Controls.Add(myTable1);
    //    myRow1 = new TableRow();
    //    myRow1.BorderWidth = new Unit(0);
    //    myTable1.Rows.Add(myRow1);

    //    myCell1 = new TableCell();
    //    myCell1.CssClass = strCssMessage;
    //    myCell1.BorderWidth = new Unit(0);
    //    myCell1.Width = new Unit(36);

    //    System.Web.UI.WebControls.Image Alertimage = new System.Web.UI.WebControls.Image();
    //    Alertimage.Height = new Unit(intMessageBoxImageHeight);
    //    Alertimage.Width = new Unit(intMessageBoxImageWidth);
    //    Alertimage.BorderWidth = new Unit(0);
    //    Alertimage.ImageUrl = strImage;
    //    myCell1.Controls.Add(Alertimage);


    //    myRow1.Cells.Add(myCell1);
    //    myCell1 = new TableCell();
    //    myCell1.CssClass = strCssMessage;
    //    myCell1.BorderWidth = new Unit(0);



    //    myCell1.CssClass = strCssMessage;
    //    if (string.IsNullOrEmpty(strCssMessage))
    //    {
    //        myCell1.HorizontalAlign = HorizontalAlign.Center;
    //        myCell1.ForeColor = System.Drawing.Color.Black;
    //        myCell1.BackColor = System.Drawing.Color.LightGray;
    //        myCell1.BorderColor = System.Drawing.Color.LightGray;
    //        myCell1.Font.Name = "Verdana";
    //        myCell1.Font.Bold = true;
    //        myCell1.Font.Size = new FontUnit(8);
    //    }

    //    Label NewLabel1 = new Label();
    //    NewLabel1.Text = strMessage;
    //    myCell1.Controls.Add(NewLabel1);

    //    myRow1.Cells.Add(myCell1);

    //    myRow = new TableRow();
    //    myRow.BorderWidth = new Unit(0);
    //    myTable.Rows.Add(myRow);
    //    if (intButton == 1 | intButton > 3 | intButton < 1)
    //    {
    //        myCell = new TableCell();
    //        myCell.ColumnSpan = 5;
    //        myCell.BorderWidth = new Unit(0);
    //        myCell.CssClass = strCssMessage;
    //        myCell.HorizontalAlign = HorizontalAlign.Center;

    //        if (string.IsNullOrEmpty(strCssMessage))
    //        {
    //            myCell.ForeColor = System.Drawing.Color.Black;
    //            myCell.BackColor = System.Drawing.Color.LightGray;
    //            myCell.Font.Name = "Verdana";
    //            myCell.Font.Bold = true;
    //            myCell.Font.Size = new FontUnit(8);
    //        }

    //        ButtonOK = new Button();
    //        ButtonOK.ID = strMessageBoxIDYes;
    //        ButtonOK.Text = strButtonYes;
    //        ButtonOK.Width = new Unit(strButtonWidth);
    //        ButtonOK.Style.Add("CURSOR", "hand");
    //        myCell.Controls.Add(ButtonOK);

    //        myRow.Cells.Add(myCell);
    //    }

    //    if (intButton > 1 & intButton < 4)
    //    {
    //        myCell = new TableCell();
    //        myCell.CssClass = strCssMessage;
    //        myCell.BorderWidth = new Unit(0);
    //        myCell.HorizontalAlign = HorizontalAlign.Right;

    //        if (string.IsNullOrEmpty(strCssMessage))
    //        {
    //            myCell.ForeColor = System.Drawing.Color.Black;
    //            myCell.BackColor = System.Drawing.Color.LightGray;
    //            myCell.Font.Name = "Verdana";


    //            myCell.Font.Bold = true;
    //            myCell.Font.Size = new FontUnit(8);
    //        }

    //        ButtonYes = new Button();
    //        ButtonYes.ID = strMessageBoxIDYes;
    //        ButtonYes.Text = strButtonYes;
    //        ButtonYes.Width = new Unit(strButtonWidth);
    //        ButtonYes.Style.Add("CURSOR", "hand");
    //        myCell.Controls.Add(ButtonYes);

    //        myRow.Cells.Add(myCell);
    //        myCell = new TableCell();
    //        myCell.Width = new Unit(20);
    //        myCell.BorderWidth = new Unit(0);
    //        myCell.CssClass = strCssMessage;

    //        if (string.IsNullOrEmpty(strCssMessage))
    //        {
    //            myCell.BackColor = System.Drawing.Color.LightGray;
    //        }

    //        myRow.Cells.Add(myCell);
    //        myCell = new TableCell();
    //        myCell.CssClass = strCssMessage;
    //        myCell.BorderWidth = new Unit(0);

    //        if (string.IsNullOrEmpty(strCssMessage))
    //        {
    //            myCell.ForeColor = System.Drawing.Color.Black;
    //            myCell.BackColor = System.Drawing.Color.LightGray;
    //            myCell.Font.Name = "Verdana";
    //            myCell.Font.Bold = true;
    //            myCell.Font.Size = new FontUnit(8);
    //        }


    //        if (intButton == 2)
    //        {
    //            myCell.HorizontalAlign = HorizontalAlign.Left;
    //        }
    //        else if (intButton == 3)
    //        {
    //            myCell.HorizontalAlign = HorizontalAlign.Center;
    //        }

    //        ButtonNo = new Button();
    //        ButtonNo.ID = strMessageBoxIDNo;
    //        ButtonNo.Text = strButtonNo;
    //        ButtonNo.Width = new Unit(strButtonWidth);
    //        ButtonNo.Attributes("WIDTH") = strButtonWidth.ToString();
    //        ButtonNo.Attributes("HEIGHT") = strButtonWidth.ToString();
    //        ButtonNo.Style.Add("CURSOR", "hand");
    //        myCell.Controls.Add(ButtonNo);

    //        myRow.Cells.Add(myCell);

    //        if (intButton == 3)
    //        {
    //            myCell = new TableCell();
    //            myCell.Width = new Unit(10);


    //            myCell.BorderWidth = new Unit(0);
    //            myCell.CssClass = strCssMessage;

    //            if (string.IsNullOrEmpty(strCssMessage))
    //            {
    //                myCell.BackColor = System.Drawing.Color.LightGray;
    //            }

    //            myRow.Cells.Add(myCell);
    //            myCell = new TableCell();
    //            myCell.CssClass = strCssMessage;
    //            myCell.BorderWidth = new Unit(0);
    //            myCell.HorizontalAlign = HorizontalAlign.Left;

    //            if (string.IsNullOrEmpty(strCssMessage))
    //            {
    //                myCell.ForeColor = System.Drawing.Color.Black;
    //                myCell.BackColor = System.Drawing.Color.LightGray;
    //                myCell.Font.Name = "Verdana";
    //                myCell.Font.Bold = true;
    //                myCell.Font.Size = new FontUnit(8);
    //            }

    //            ButtonCancel = new Button();
    //            ButtonCancel.ID = strMessageBoxIDCancel;
    //            ButtonCancel.Text = strButtonCancel;
    //            ButtonCancel.Width = new Unit(strButtonWidth);
    //            ButtonCancel.Style.Add("CURSOR", "hand");
    //            myCell.Controls.Add(ButtonCancel);


    //            myRow.Cells.Add(myCell);
    //        }
    //    }
    //}
    protected override void AddAttributesToRender(HtmlTextWriter writer)
    {
        // Rendering message box control to the browser
        base.AddAttributesToRender(writer);
        writer.AddAttribute(HtmlTextWriterAttribute.Id, "showimage");
        writer.AddAttribute(HtmlTextWriterAttribute.Style, "Z-INDEX: 9999; LEFT:" + strLeft + "px; WIDTH:" + strLeft + "px; POSITION: absolute; TOP: " + strTop + "px; filter:progid:DXImageTransform.Microsoft.Shadow(color='dimgray', direction=");
        //135;
    }
}
