<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="MyWork.aspx.cs" Inherits="MyWork" Title="Develope Pages Upload On Version" %>
 

    <%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
        .btnFillGreen
        {
            background-color:Green;  
        }
        .btnFillGrey
        {
            background-color:Gray;  
        }
        .btnFillRed
        {
            background-color:Red;  
        }
          .btnFillBlue
        {
            background-color:#0099FF;  
        }
    </style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="products_box">
       <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text=""></asp:Label>
                    </legend>
    <table id="pagetbl" style="width: 100%;">
        <tr>
            <td colspan="4">
               
                <asp:Label ID="lblMsg1" ForeColor="Red" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td>
            <lable style="font-size: 14px; color: #416271; padding-left: 20px;">
            Employee Name
            </lable>
            </td>
            <td style="width: 75%" colspan="3">
                <asp:DropDownList ID="ddlemployee" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
      
        <tr>
            <td style="font-size: 14px; color: #416271; padding-left: 20px;">
              <lable>Type Of Work</lable> 
            </td>
            <td style="width: 75%" colspan="3">
                <asp:DropDownList ID="ddltypeofwork" runat="server">
                 <asp:ListItem Value="1">Developer</asp:ListItem>
                  <asp:ListItem Value="2">Tester</asp:ListItem>
                   <asp:ListItem Value="3">Supervisor</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        
        
     
        
        <tr>
            <td>
               <lable style="font-size: 14px; color: #416271; padding-left: 20px;"> From Date:</lable>
            </td>
            <td colspan="3">
            <table>
            <tr>
            <td>
              <label style="width:85px;">
                 <asp:TextBox ID="TextBox1" runat="server" Width="80px" ></asp:TextBox>
                 </label>
            </td>
            <td>
              <lable style="width:30px;">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/cal_actbtn.jpg"  />
                     <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox1"
                    PopupButtonID="ImageButton2">
                </cc1:CalendarExtender>
                    </lable>
            </td>
            <td>
             <label style="width:30px;font-size: 14px; color: #416271; padding-left: 20px;"> Date:</label>
            </td>
            <td>
              <lable style="width:100px;"><asp:TextBox ID="TextBox2" runat="server" Width="80px"></asp:TextBox></lable>
            </td>
            <td>
              <lable style="width:30px;"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cal_actbtn.jpg" /></lable>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox2"
                        PopupButtonID="ImageButton1">
                    </cc1:CalendarExtender>
            </td>
            
            </tr>
            </table>
          
               
              
               
                 
         
              
                  
                   
                  
               
               
            </td>
        </tr>
     
        <tr>
            <td style="font-size: 14px; color: #416271; padding-left: 20px;">
               Status
            </td>
            <td style="width: 75%" colspan="3">
                <asp:DropDownList ID="ddlstatus" runat="server">
                    <asp:ListItem Value="0">Pending</asp:ListItem>
                    <asp:ListItem Value="1">Completed</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
       
       
       <tr>
            <td style="font-size: 14px; color: #416271; padding-left: 20px;">
              <lable> Select Website</lable>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlwebsite" runat="server">
                <asp:ListItem>All</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
                     <td style="font-size: 14px; color: #416271; padding-left: 20px;">
                Search Page
                <asp:Label ID="Label16N" runat="server" Text="" Visible="false"></asp:Label>
            </td>
                    <td colspan="3">
                    <asp:TextBox ID="TextBox6" runat="server"   placeholder="Search"  Font-Bold="true"  Width="250px" Height="20px"></asp:TextBox>
                    </td>
                    </tr>
         <%--<tr>
            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                align="left">
                <span lang="en-us">Select Product&nbsp; :&nbsp; </span>
            </td>
            <td style="width: 75%" colspan="3">
                 <asp:DropDownList ID="ddl_product" runat="server"  OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged1" AutoPostBack="true" Width="250px">
                </asp:DropDownList>
                <span lang="en-us">&nbsp;</span>
                <span lang="en-us" style="width: 30%; font-weight: bold; font-size: 14px; color: #416271;
                    padding-left: 20px;">&nbsp;Select Version Name:
                      <asp:DropDownList ID="ddl_version" runat="server"  OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged" AutoPostBack="true" Width="250px"  >
                </asp:DropDownList>
                </span>

            </td>
        </tr>
          

        <tr>
            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                align="left">
                <span lang="en-us">Select Website&nbsp; :&nbsp; </span>
            </td>
            <td style="width: 75%" colspan="3">
                <asp:DropDownList ID="ddlwebsite" runat="server" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>--%>

        <tr>
            <td colspan="4">
                <br />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="GO" OnClick="Button1_Click" ValidationGroup="1" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="GridView2" AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging"
                    Width="100%" runat="server" AutoGenerateColumns="False" EmptyDataText="No Records Found." PageSize="15" 
                    DataKeyNames="Id,SupervisorOK,PageId,DeveloperOK,TesterOk, ReturntoDevelopers" AllowSorting="True" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                    PagerStyle-CssClass="prg" OnRowDataBound="GridView2_RowDataBound" 
                    onrowcommand="GridView2_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblmasterId" runat="server" Text='<%#Bind("Id") %>'></asp:Label>
                                 <asp:Label ID="Label3PageId" runat="server" Text='<%#Bind("PageId") %>'></asp:Label>
                                <asp:Label ID="lblempiddevloper" runat="server" Text='<%#Bind("EpmloyeeID_AssignedDeveloper") %>'></asp:Label>
                                <asp:Label ID="lblempidtester" runat="server" Text='<%#Bind("EpmloyeeID_AssignedTester") %>'></asp:Label>
                                <asp:Label ID="lblempidsupervisor" runat="server" Text='<%#Bind("EpmloyeeID_AssignedSupervisor") %>'></asp:Label>
                                <asp:Label ID="lbl_page" runat="server" Text='<%#Bind("PageName") %>'></asp:Label>
                                 <asp:Label ID="lblVirsionurl" runat="server" Text='<%#Bind("VersionFolderUrl") %>'></asp:Label>
                                 <asp:Label ID="lblReturn" runat="server" Text='<%#Bind("ReturntoDevelopers") %>'></asp:Label>
                                 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Website Name">
                            <ItemTemplate>
                                <asp:Label ID="lblwebsitename12345" runat="server" Text='<%#Bind("WebsiteName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Page Title">
                            <ItemTemplate>
                                <asp:Label ID="lblpagename12345" runat="server" Text='<%#Bind("PageTitle") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ver.No">
                            <ItemTemplate>
                                <asp:Label ID="lblworkdonereport45" runat="server" Text='<%#Bind("VersionName") %>'></asp:Label>
                                  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Work Title">
                            <ItemTemplate>
                                <asp:Label ID="lblworktitle12345" runat="server" Text='<%#Bind("WorkRequirementTitle") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Targate Date">
                            <ItemTemplate>
                                <asp:Label ID="lbldate12345" runat="server"></asp:Label>
                                <%-- <asp:Label ID="Label1" runat="server" Text='<%#Bind("Date","{0:MM/dd/yyyy}") %>'></asp:Label>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bud.Hr">
                            <ItemTemplate>
                                <asp:Label ID="lblbudgetd132" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Act.Hr">
                            <ItemTemplate>
                                <asp:Label ID="lblactualhour" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="File Uploaded">
                                            <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton3" style="color:#717171;" runat="server" Text="FileUpload" OnClick="link312_Click">
                                            </asp:LinkButton>
                                                <asp:Label ID="lblfileupload" runat="server" ></asp:Label>
                                            
                                           
                                            </ItemTemplate>
                                            </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Fill Report">
                            <ItemTemplate>
                            
                                <asp:LinkButton ID="LinkButton1"     runat="server" CommandName="popup"  CommandArgument='<%# Eval("Pageid") %>' Text="FillReport" Style="color: #000;"  ></asp:LinkButton>
                                
                            <%--    <asp:LinkButton ID="LinkButton3" CommandName="popup" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Style="color: #000;" runat="server" Text="FillReport"></asp:LinkButton>--%>                            
                                  <%--  <asp:Button ID="Button15" runat="server" Text="Fill Report"  CommandName="popup"   CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  --%>
                                <asp:Label ID="lblfillreport" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View Detail">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" Style="color: #717171;" runat="server" Text="ViewDetail"  OnClick="link2_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ShowPage">
                            <ItemTemplate>
                               <asp:LinkButton ID="LinkButton11" CommandName="popup1" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Style="color: #717171;" runat="server" Text="ShowPage"></asp:LinkButton>
                                  
                                <asp:Label ID="lblfillreport1" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField CommandName="Delete1" ItemStyle-ForeColor="#717171" Text="Delete" Visible="false">
                            <ItemStyle ForeColor="#717171"></ItemStyle>
                        </asp:ButtonField>
                    </Columns>
                    <PagerStyle CssClass="GridPager" ForeColor="#717171" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="GridRowStyle" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4">
              <asp:Label ID="lblvername" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="Gray" Width="800px" Height="600px"  ScrollBars="Auto" BorderStyle="Solid" BorderWidth="5px">
                    <table style="width: 100%">
                        <tr>
                            <td colspan="2" align="right" >
                                <div style="padding-left:550px">
                                <asp:ImageButton ID="ImageButton5" ImageUrl="~/images/closeicon.jpeg" runat="server" Width="16px" OnClick="ImageButton2_Click"  />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <span lang="en-us">Page Work Detail</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lblpaged" runat="server" ForeColor="Red" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Website Name<span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblwebsitenamedetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Page Title<span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblpagenamedetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Version no<span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblnewpageversion" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                File Name<span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblnewpagedetaildetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Work Title<span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblworktitledetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000; height: 22px;">
                                Work Description<span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000; height: 22px;">
                                <asp:Label ID="lblworkdescriptiondetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Target Date <span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lbltargatedatedetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Budgedet Hour:
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblbudgetedhourdetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Actual Hour:
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblactualhourdetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Intruction File :
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; font-size: 12px; color: #000000;" colspan="2" align="center">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel3" runat="server" ScrollBars="Both" HorizontalAlign="Center"
                                                Height="130px">
                                                <asp:GridView ID="GridView1" runat="server" EmptyDataText="No file found." AutoGenerateColumns="False"
                                                    DataKeyNames="Id" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="prg"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Title">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltitle" runat="server" Text='<%#Bind("FileTitle") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PDF URL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpdfurl" runat="server" Text='<%#Bind("WorkRequirementPdfFilename") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Audio URL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblaudiourl" runat="server" Text='<%#Bind("WorkRequirementAudioFileName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldate123" runat="server" Text='<%#Bind("Date" ,"{0:MM/dd/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkdow1" Style="color: #717171;" runat="server" Text="Download" OnClick="linkdow1_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="GridPager" />
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                                                    <RowStyle CssClass="GridRowStyle" />
                                                    <FooterStyle CssClass="GridFooter" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Source Code Files :
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; font-size: 12px; color: #000000;" align="center" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel4" runat="server" ScrollBars="Both" HorizontalAlign="Center"
                                                Height="130px">
                                                <asp:GridView ID="grdsourcefile" runat="server" EmptyDataText="No file found." AutoGenerateColumns="False"
                                                    DataKeyNames="Id" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="prg"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Page Title">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblspagetitle" runat="server" Text='<%#Bind("PageTitle") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Version No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblviesionno" runat="server" Text='<%#Bind("VersionNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FileName" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfilename" runat="server" Text='<%#Bind("PageName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FileName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfilenam" runat="server" Text='<%#Bind("PName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="250px" />
                                                            <HeaderStyle Width="250px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldates" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkdow" Style="color: #717171;" runat="server" Text="Download"
                                                                    OnClick="linkdow_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="GridPager" />
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                                                    <RowStyle CssClass="GridRowStyle" />
                                                    <FooterStyle CssClass="GridFooter" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                &nbsp;
                            </td>
                        </tr>
                        <%--<tr>
                <td style="width:30%; font-size: 12px; color: #000000;">
                    &nbsp;</td>
                <td style="width:70%; font-size: 12px; color: #000000;">
                    &nbsp;</td>
                </tr>
                <tr>
                <td style="width:30%; font-size: 12px; color: #000000;">
                </td>
                <td style="width:70%; font-size: 12px; color: #000000;">
                    &nbsp;</td>
                </tr>
                <tr>
                <td style="width:30%; font-size: 12px; color: #000000;">
                </td>
                <td style="width:70%; font-size: 12px; color: #000000;">
                    &nbsp;</td>
                </tr>--%>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button2" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="Panel1" TargetControlID="Button2">
                </cc1:ModalPopupExtender>
            </td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="Gray" Width="600"
                    BorderStyle="Solid" BorderWidth="5px">
                    <table style="width: 100%">
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                    Width="16px" OnClick="ImageButton1_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-left:20px; color:#416271; font-weight: bold; font-size: 18px; ">
                                <span lang="en-us">My Daily Work Report</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #717171;">
                                &nbsp;
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #717171;">
                                <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #717171;">
                                &nbsp;
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #717171;">
                                <asp:Label ID="lblpageworkId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                align="left">
                                WorkTitle <span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: ##717171;">
                                <asp:Label ID="lblworltitleatreport" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                align="left">
                                <span lang="en-us">Date :</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #717171;">
                                <asp:TextBox ID="TextBox3" runat="server" Width="110px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBox3"
                                    PopupButtonID="ImageButton3">
                                </cc1:CalendarExtender>
                                
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox3"
                                    ErrorMessage="*" ValidationGroup="6"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                         <tr>
                            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                align="left">
                                <span lang="en-us">Budgeted Hour:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #717171;">
                                <asp:Label ID="lblnewbujtedhr" runat="server"></asp:Label>
                            </td>

                       </tr>
                        <tr>
                            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                align="left">
                                <span lang="en-us">Hour Spent :</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #717171; height: 22px;">
                                <asp:TextBox ID="TextBox4" runat="server" Width="110px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox4"
                                    ErrorMessage="*" ValidationGroup="6"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox4"
                                    ErrorMessage="*" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                    ValidationGroup="6"></asp:RegularExpressionValidator>
                            </td>
                            
                        </tr>
                       
                        <tr>
                            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                align="left">
                                <span lang="en-us">Work Done Report :</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #717171;">
                                <asp:TextBox ID="TextBox5" runat="server" Width="410px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox5"
                                    ErrorMessage="*" ValidationGroup="6"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"align="left">
                                <span lang="en-us">Work Done :</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #717171;">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" 
                                    RepeatDirection="Horizontal" 
                                    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                <asp:CheckBox ID="CheckBox1" runat="server" 
                                    oncheckedchanged="CheckBox1_CheckedChanged" AutoPostBack="true" Visible="false"/>
                            </td>
                            <td style="width: 0%; font-size: 12px; color: #717171;">
                                
                            </td>
                        </tr>
                        <tr>
                                            
                                            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"align="left">
                                <span id="lblactulhr" runat="server" visible="false" lang="en-us">Actual hour required to finish today's task :</span>
                            </td>
                                            <td width="70%">
                                                <label>
                                                    <asp:TextBox ID="txtactualhourrequired" runat="server" Width="110px" Visible="false">00:00</asp:TextBox>
                                                    
                                                </label>
                                            </td>
                                        </tr>
                                        <tr id="trtbl" runat="server" visible="false">
                                        <td colspan="2">
                                        <table  border="3" width="100%">
                                          <tr>
                            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                align="left">
                                <span id="lblupldcode" runat="server" lang="en-us" visible="false"> Upload completed code file   to Replace main code file when certified   by supervisor :</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #717171;">
                                <asp:CheckBox ID="chkupld" runat="server" OnCheckedChanged="chkupld_CheckedChanged"
                                    AutoPostBack="true" Visible="false" />
                            </td>
                        </tr>
                        <asp:Panel ID="Panel5" runat="server" Visible="false" >
                        
                        <tr>      
                        <td style="width:30%; font-size: 12px; color: #717171; height: 22px;">
                    &nbsp;</td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblftpurl123" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblftpport123" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblftpuserid" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblftppassword123" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:30%; font-size: 12px; color: #000000; height: 22px;">
                    &nbsp;</td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblpageworkmasterId" runat="server" Visible="False" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                       
                       <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000; ">
                                &nbsp;
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #717171; ">
                                <asp:Label ID="Label2" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                         </asp:Panel>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                <span id="lblfnm" runat="server" lang="en-us" style="display: none">File Name</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:FileUpload ID="fileuploadadattachment" runat="server" Visible="false" />
                                <span lang="en-us">&nbsp;&nbsp; </span>
                                <asp:Button ID="Button7" runat="server" Text="Add" Visible="false" OnClick="Button9_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                &nbsp;
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:GridView ID="gridFileAttach" runat="server" AutoGenerateColumns="False" EmptyDataText="There is no data." Visible="false" Width="100%" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                    PagerStyle-CssClass="prg" >
                                    
                                    
                                 
                                    <Columns>
                                        <asp:TemplateField HeaderText="File Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpdfurl" runat="server" Text='<%#Bind("PDFURL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="GridPager" />
                                    <HeaderStyle CssClass="GridHeader" />
                                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                                    <FooterStyle CssClass="GridFooter" />
                                    <RowStyle CssClass="GridRowStyle" />
                                </asp:GridView>
                            </td>
                        </tr>
                         <tr>
                            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                align="left">
                                <span id="lblcopycode" runat="server" lang="en-us" visible="false"> Copy Paste Code For Documentation :</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #717171;">
                                <cc2:HtmlEditor ID="txtcopycode" Width="450px" runat="server"></cc2:HtmlEditor>
                            </td>
                        </tr>
                                        </table>
                                        </td>
                                        </tr>
                      
                        <tr>
                            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                align="left">
                                <span id="lbluplddoc" runat="server" lang="en-us" visible="false">Upload Documentation file :</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:CheckBox ID="Chkmultiupld" runat="server" AutoPostBack="true" OnCheckedChanged="Chkmultiupld_CheckedChanged" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                <span id="lbldocname" runat="server" lang="en-us" style="display: none">Document Name</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:FileUpload ID="Fileupldmulti" runat="server" Visible="false" />
                                <span lang="en-us">&nbsp;&nbsp; </span>
                                <asp:Button ID="Button8" runat="server" Text="Add" Visible="false" OnClick="Button8_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                &nbsp;
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:GridView ID="Griddoc" Width="100%" runat="server" AutoGenerateColumns="False"
                                    EmptyDataText="There is no data." Visible="false" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                    PagerStyle-CssClass="prg">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Document Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldocname" runat="server" Text='<%#Bind("DocumentTitle") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="GridPager" />
                                    <HeaderStyle CssClass="GridHeader" />
                                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                                    <FooterStyle CssClass="GridFooter" />
                                    <RowStyle CssClass="GridRowStyle" />
                                </asp:GridView>
                            </td>
                        </tr>
                         <tr>
                            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                align="left">
                                <span id="lblcerti" runat="server" lang="en-us" visible="false" > Certify :</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:CheckBox ID="Chkcertify" runat="server" AutoPostBack="true" Visible="false" 
                                    oncheckedchanged="Chkcertify_CheckedChanged1"/>
                                    
                            </td>
                        </tr>
                        <tr>      
                        <td style="width:30%; font-size: 12px; color: #717171; height: 22px;">
                    &nbsp;</td>
                            <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271;">
                                <asp:Label ID="lbldeveloper" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lbltester" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblsupervior" runat="server" Visible="False"></asp:Label>
                                
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                &nbsp;
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Button ID="Button4" runat="server" Text="Submit" OnClick="Button4_Click" ValidationGroup="6" Visible="false"/>
                                <span lang="en-us">&nbsp; </span>
                                <asp:Button ID="Button5" runat="server" Text="Cancel" onclick="Button5_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                &nbsp;
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                &nbsp;
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button3" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel2" TargetControlID="Button3">
                </cc1:ModalPopupExtender>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnl3master" runat="server" BackColor="White" BorderColor="Gray" Width="600"
                    BorderStyle="Solid" BorderWidth="5px">
                    <table style="width: 100%">
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:ImageButton ID="ImageButton6" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                    Width="16px" OnClick="ImageButton2_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <span lang="en-us">Upload A File</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000; height: 22px;">
                                &nbsp;
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000; height: 22px;">
                                <asp:Label ID="lblftpurl1233" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblftpport1233" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblftpuseridd" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblftppassword1233" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000; height: 22px;">
                                &nbsp;
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000; height: 22px;">
                                <asp:Label ID="lblpageworkmasterIdd" runat="server" Visible="False" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000; height: 22px;">
                                &nbsp;
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000; height: 22px;">
                                <asp:Label ID="Label22" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                <span lang="en-us">File Name</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:FileUpload ID="fileuploadadattachmentff" runat="server" />
                                <span lang="en-us">&nbsp;&nbsp; </span>
                                <asp:Button ID="Button9" runat="server" Text="Add" OnClick="Button9_Click" />
                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                        ErrorMessage="Fiill More" ControlToValidate="fileuploadadattachment" 
                        SetFocusOnError="true" 
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.txt|.TXT)$" ></asp:RegularExpressionValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                &nbsp;
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:GridView ID="gridFileAttachh" Width="100%" runat="server" AutoGenerateColumns="False"
                                    EmptyDataText="There is no data." CssClass="GridBack">
                                    <Columns>
                                        <asp:TemplateField HeaderText="File Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpdfurl" runat="server" Text='<%#Bind("PDFURL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="GridPager" />
                                    <HeaderStyle CssClass="GridHeader" />
                                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                                    <FooterStyle CssClass="GridFooter" />
                                    <RowStyle CssClass="GridRowStyle" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000; height: 22px;">
                                &nbsp;
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000; height: 22px;">
                                <asp:Button ID="Button10" runat="server" OnClick="Button10_Click" Text="Submit" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button6" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="pnl3master" TargetControlID="Button6">
                </cc1:ModalPopupExtender>
            </td>
        </tr>
    </table>
    </fieldset>


    </div>
      </ContentTemplate>
      </asp:UpdatePanel>
       <asp:Label ID="lbl_versionuser" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lbl_versionpass" runat="server" Visible="False"></asp:Label>

                                 <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" Text="V5" ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>
</asp:Content>
