<%@ Page Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="CompanyProfileForEdit.aspx.cs" Inherits="CompanyProfileForEdit" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
 <style type="text/css">
.Grid {background-color: #fff; margin: 5px 0 10px 0; border: solid 1px #525252; border-collapse:collapse; font-family:Calibri; color: #474747;}
.Grid td {
      padding: 2px; 
      border: solid 1px #c1c1c1; }
.Grid th  {
      padding : 4px 2px; 
      color: #fff; 
      background: #363670 url(Images/grid-header.png) repeat-x top; 
      border-left: solid 1px #525252; 
      font-size: 0.9em; }
.Grid .alt {
      background: #fcfcfc url(Images/grid-alt.png) repeat-x top; }
.Grid .pgr {background: #363670 url(Images/grid-pgr.png) repeat-x top; }
.Grid .pgr table { margin: 3px 0; }
.Grid .pgr td { border-width: 0; padding: 0 6px; border-left: solid 1px #666; font-weight: bold; color: #fff; line-height: 12px; }   
.Grid .pgr a { color: Gray; text-decoration: none; }
.Grid .pgr a:hover { color: #000; text-decoration: none; }
h1 { color: #ff4411; font-size: 48px; font-family: 'Signika', sans-serif; padding-bottom: 10px; }



.button {
    background-color: #4CAF50; /* Green */
    border: none;
    color: white;
    padding: 15px 32px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
    margin: 4px 2px;
    cursor: pointer;
}

.button1 {
    background-color: white;
    color: black;
    border: 2px solid #4CAF50;
}

.button2 {
    background-color: white;
    color: black;
    border: 2px solid #008CBA;
}

.button3 {
    background-color: white;
    color: black;
    border: 2px solid #f44336;
}

.button4 {
    background-color: white;
    color: black;
    border: 2px solid #e7e7e7;
}

.button5 {
    background-color: white;
    color: black;
    border: 2px solid #555555;
}
a:link, a:visited {
    background-color: #363670;
    color: white;
    padding: 14px 25px;
    text-align: center; 
    text-decoration: none;
    display: inline-block;
}

a:hover, a:active {
    background-color: #f44336;
}
</style>
 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
    <div class="products_box">
   
       <fieldset style="margin: 0">
            <legend>
                <asp:Label ID="Label5" runat="server" Text=" "></asp:Label>
            </legend>
            <table style="width: 100%">
                <asp:Panel ID="Panel1" runat="server" Visible="false" Width="100%">
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button1" runat="server" Text="Edit Profile" OnClick="Button1_Click" />
                         <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label6" runat="server" Text="Company Information" 
                            Font-Bold="True"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        
                            <asp:Label ID="Label11" runat="server" Text="Company Name:"></asp:Label>
                            <asp:Label ID="txtcompanyname" runat="server"></asp:Label>
                       
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 42px">
                       
                            <asp:Label ID="Label12" runat="server" Text="Contact Person Name: "></asp:Label>
                            <asp:Label ID="txtcontactprsn" runat="server"></asp:Label>
                        
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 28px">
                      
                            <asp:Label ID="Label13" runat="server" Text="Contact Person Designation:"></asp:Label>
&nbsp;<asp:Label ID="txtcontactprsndsg" runat="server"></asp:Label>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label10" runat="server" Font-Bold="False" Text="Address"></asp:Label>
                        
                        :&nbsp;<asp:Label ID="txtadd" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 5%">
                                    &nbsp;</td>
                                <td style="width: 57%">
                                    <asp:Label ID="ddlcountry" runat="server"></asp:Label>
                      
                                    ,
                     
                                    <asp:Label ID="ddlstate" runat="server"></asp:Label>
                                    ,
                      
                            <asp:Label ID="ddlcity" runat="server"></asp:Label>
                      
                                    </td>
                                <td style="width: 17%">
                                    &nbsp;&nbsp;
                                    </td>
                                <td style="width: 12%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 62px">
                                    &nbsp;&nbsp;</td>
                                <td>
                                    <asp:Label ID="lblzipcode" runat="server"></asp:Label>
                      
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 34px">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 706px" >
                                    <asp:Label ID="Label18" runat="server" Text="Phone No.:"></asp:Label>
                                &nbsp;<asp:Label ID="txtphn" runat="server"></asp:Label>
                                </td>
                                <td>
                      
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 20%">
                                    <asp:Label ID="Label19" runat="server" Text="Fax  No.:"></asp:Label>
                                &nbsp;<asp:Label ID="txtfax" runat="server"></asp:Label>
                    
                                </td>
                                <td>
                     
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    &nbsp;</td>
                                <td>
                     
                            <asp:Label ID="txtemail" runat="server" Visible="False"></asp:Label>
                     
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                      
                        <asp:Label ID="Label7" runat="server" Text=" Website Login Information" 
                            Font-Bold="True"></asp:Label>  
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                      
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="Label20" runat="server" Text="Website Login URL"></asp:Label>
                                    :<asp:Label ID="lblweburl" runat="server"></asp:Label>
                     
                                </td>
                                <td width="10%">
                    
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="Label21" runat="server" Text="Company ID:"></asp:Label>
                  
                                <asp:Label ID="lblcidfff" runat="server"></asp:Label>
                     
                                </td>
                                <td width="10%">
                  
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="Label22" runat="server" Text="Username:"></asp:Label>
                        
                            <asp:Label ID="lbluserid" runat="server"></asp:Label>
                                </td>
                                <td width="10%">
                        
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    &nbsp;</td>
                                <td width="10%">
                                    <asp:Label ID="lblpassw" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                       
                            <asp:Label ID="Label9" runat="server" Font-Bold="True" 
                            Text="My Subscribed Plans"></asp:Label>
                         <asp:Label ID="Label23" runat="server" Text="Plan Name:"></asp:Label>
                                    &nbsp;<asp:Label ID="lblplanname" runat="server"></asp:Label>

                                    <asp:Label ID="Label24" runat="server" Text="Subscription Date:	"></asp:Label>
                     
                            <asp:Label ID="lblsubdate" runat="server"></asp:Label>
                 <asp:Label ID="Label25" runat="server" Text="Valid Date: "></asp:Label>                      
                                    <asp:Label ID="lblvaliddate" runat="server"></asp:Label>
                                    <asp:Label ID="Label26" runat="server" Text="	Payper Order Plan Balance: "></asp:Label>
                                    <asp:Label ID="lblppob" runat="server"></asp:Label>
                                    <asp:Label ID="Label27" runat="server" Text="Existing License Key:" ForeColor="#445553"></asp:Label>
                                    <asp:Label ID="lbllicense" runat="server"></asp:Label>
                    &nbsp;</td>
                </tr>
               
                </asp:Panel>
                </table>
                <table>
                <asp:Panel ID="Panel3" runat="server" Visible="false" Width="100%">
                <tr>
                    <td>
                       
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 32%; height: 36px;">
                                  
                    
                                    </td>
                                    <td width="10%" style="height: 36px">
                      
                            <asp:Button ID="btnplanrenue" runat="server" Text="Renew Plan " OnClick="btnplanchange_ClickCurrent" />
                            <asp:Button ID="Button2" runat="server" Text="Renew Plan Manuually" OnClick="btnplanrenue_Click" Visible="false" />
                                    </td>
                                    <td style="width: 1%; height: 36px;">
                                        </td>
                                    <td width="10%" style="height: 36px">
                     
                      <%--      <asp:Button ID="Button2" runat="server" Text="Renew Plan" OnClick="btnplanrenue_Click" />--%>
                      
                            <asp:Button ID="btnplanchange" runat="server" OnClick="btnplanchange_Clickall" Text="Change/Upgrade Plan" />
                                            </td>
                                    <td width="10%" style="height: 36px">
                      
                            <asp:Button ID="btnplanchange0" runat="server" OnClick="btnplanchange_Click" Text="Switch to Other Portal" />
                                        </td>
                                    <td width="10%" style="height: 36px">
                                    <asp:Label ID="Label8" runat="server" Visible="false">Select Portal</asp:Label>
                                       
                                        </td>
                                    <td width="10%" style="height: 36px">
                                    <asp:DropDownList ID="ddlportal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlportal_SelectedIndexChanged" Visible="false">
                                       </asp:DropDownList>
                                        </td>
                                    <td width="10%" style="height: 36px">
                                    <asp:Button ID="btngo" runat="server" OnClick="btnplanchange_ClickGo" Text="Go" Visible="false" />
                                        </td>
                                    <td width="10%" style="height: 36px">
                                        </td>
                                    <td style="height: 36px">
                                        </td>
                                </tr>
                                <tr>
                                    <td style="width: 32%">
                                    
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td style="width: 1%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 32%">
                                  
                   
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td style="width: 1%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 32%">
                                     
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td style="width: 1%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 32%">
                                    

                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td style="width: 1%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 32%">  &nbsp;</td>
                                    <td width="10%">&nbsp;</td>
                                    <td style="width: 1%">&nbsp;</td>
                                    <td width="10%">&nbsp;</td>
                                    <td width="10%">&nbsp;</td>
                                    <td width="10%">&nbsp;</td><td width="10%">&nbsp;</td><td width="10%">&nbsp;</td>     <td width="10%">&nbsp;</td>                                    <td>
                                        &nbsp;</td>
                                </tr>
                                </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnldownload" runat="server" Visible="false" Width="100%">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            Download Details
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;" align="right">
                                        <label>
                                            Product Setup</label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Names="Action Is,Diagonal JL"
                                                Target="_self">Download</asp:HyperLink>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Product DB
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="True" Font-Names="Action Is,Diagonal JL"
                                                Target="_self">Download</asp:HyperLink>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Busicontroler Setup
                                        </label>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink3" runat="server" Font-Bold="True" Font-Names="Action Is,Diagonal JL"
                                            Target="_self">Download</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Busicontroler DB
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:HyperLink ID="HyperLink4" runat="server" Font-Bold="True" Font-Names="Action Is,Diagonal JL"
                                                Target="_self">Download</asp:HyperLink>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Extra Files
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:HyperLink ID="HyperLink5" runat="server" Font-Bold="True" Font-Names="Action Is,Diagonal JL"
                                                Target="_self">Download</asp:HyperLink>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                </asp:Panel>
                </table>
                <table width="900px" >
                <tr>
                    <td>
                        <asp:Panel ID="pnl3master" runat="server" BackColor="White" BorderColor="Gray" Width="900px"  BorderStyle="Solid" BorderWidth="5px" >
                          
                            <table style="width:900px ">
                                <tr>
                                    <td align="right">
                                      
                                        <label>
                                            <asp:Label ID="lblexistpamt" runat="server" Text="Label" Visible="False"></asp:Label>
                                        </label>
                                        <asp:ImageButton ID="ImageButton5" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                            Width="0px" OnClick="ImageButton5_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                          
                                        </label>
                                   
                                        <label>
                                            <asp:Label ID="lblplid" runat="server" Text="Label" Visible="False"></asp:Label>
                                        </label>
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="lblpalnex" runat="server" Text="My Existing plan "></asp:Label>
                                        </label>
                                   
                                        <label>
                                            <asp:Label ID="lblexistplanname" runat="server" Text=""></asp:Label>
                                             <asp:Label ID="Llbproductid" runat="server" Text="" Visible="false"></asp:Label>
                                             <asp:Label ID="lblportal" runat="server" Text="" Visible="false"></asp:Label>
                                        </label>
                                  
                                        <label>
                                            <asp:Label ID="Label1" runat="server" Text="Subscription Date "></asp:Label>
                                        </label>
                                  
                                        <label>
                                            <asp:Label ID="lblsubcrubdate" runat="server" Text=""></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label3" runat="server" Text="Expiry Date "></asp:Label></label>
                                   
                                        <label>
                                            <asp:Label ID="lblexdate" runat="server" Text=""></asp:Label></label>
                                   
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="Validity Period(Days)"></asp:Label></label>
                                   
                                        <label>
                                            <asp:Label ID="lblvalidityperiod" runat="server" Text=""></asp:Label></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label2" runat="server" Text="Description "></asp:Label>
                                        </label>
                                  
                                        <label>
                                            <asp:TextBox ID="lbldesctext" TextMode="MultiLine" Height="40px" runat="server" Text=""
                                                Width="400px"></asp:TextBox>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server" Width="900px" ScrollBars="Auto" Height="350px">
                                            <asp:GridView ID="GridView1" runat="server" DataKeyNames="PricePlanId" AutoGenerateColumns="False" CssClass="mGrid"
                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="850px" EmptyDataText="No Record Found."
                                                AllowSorting="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="PricePlanId" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpid" runat="server" Text='<%# Bind("PricePlanId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Price PlanName" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpname" runat="server" Text='<%# Bind("PricePlanName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PricePlan Description" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                                        <ItemTemplate>
                                                           <%-- <asp:TextBox ID="lblpdisc" runat="server" Width="400px" TextMode="MultiLine" Height="40px"
                                                                Text='<%# Bind("PricePlanDesc") %>'></asp:TextBox>--%>
                                                                <asp:Label ID="lblpdisc" runat="server" Text='<%# Bind("PricePlanDesc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" Visible="true" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpamt" runat="server" Text='<%# Bind("PricePlanAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DurationMonth" HeaderStyle-HorizontalAlign="Left"  HeaderText="Validity Period(Days)">
                                                       
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Change Plan" HeaderStyle-HorizontalAlign="Left" Visible="true" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnpchange" runat="server" OnClick="btnpchange_Click" Text="Change" />
                                                        </ItemTemplate>
                                                      
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                    <td align="center">
                        <asp:Button ID="btnmoneytobalane" runat="server" Text="Add Money To Balance" OnClick="btnmoneytobalane_Click" Visible="false" Width="197px" />
                        <br />
                        <br />
                        <hr style="width: 614px" align="left" />
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
                <table>
                <tr>
                <td>
                <asp:Panel ID="pnlpopup" runat="server" ScrollBars="Horizontal"  Visible="false"  background="rgba(0, 0, 0, 0.7)" transition="opacity 500000ms">
                                       <div style="position: absolute; margin:-10px 0px 0px 0px; height:auto; width: auto;  background: #fff; border-style: solid; transition: opacity 500ms;" class="Box">   
                                         <div align="right" style="font-family:Arial;font-size:15px;font-weight:bold;">
                                   
                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;      
                                                                    
                                     <table> 
                                     
                                     <tr>
                                     <td align="center" valign="middle">
                                      <asp:GridView ID="gvallrest" runat="server" ShowHeader="false" 
                                      CssClass="Grid" AlternatingRowStyle-CssClass="alt"  PagerStyle-CssClass="pgr"
                                             OnRowDataBound="gvallrest_RowCommand" AutoGenerateColumns="false" DataKeyNames="ID"  >
                                                            <Columns>                                                           
                                                          
                                                          
                                                                 <asp:TemplateField>                                                               
                                                                <ItemTemplate>
                                                                  <table width="980px" >
                                                                  <tr>
                                                                  <td style="width:250px;">
                                                                     <asp:ImageButton ID="img_logo" runat="server"  Width="250px" ImageUrl='<%# Eval("LogoPath") %>' ToolTip='<%# Eval("PortalName") %>'    >
                                                                     </asp:ImageButton>
                                                                     
                                                                         <asp:Label ID="lbllogo" runat="server" Text='<%# Bind("LogoPath") %>' Visible="false"></asp:Label>
                                                                  </td>
                                                                  <td style="width:700px;" valign="top">
                                                                    <h1>    <asp:Label ID="lblportal" runat="server" Text='<%# Bind("PortalName") %>'></asp:Label></h1>
                                                                        <%--  <asp:Label ID="lblpid" runat="server" Text='<%# Bind("pid") %>' Visible="false"></asp:Label>--%>
                                                                         <asp:Label ID="lblpid" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>                                                                         
                                                                  </td>
                                                                  <td valign="top">
                                                                  
                                                                  <a id="HR11" runat="server" >
                                                                    <asp:Label ID="lbla11" runat="server" Text="Select" ForeColor="Black" ></asp:Label>
                                                                    </a> 
                                                                  </td>
                                                                  </tr>
                                                                  <tr>
                                                                  <td colspan="3">
                                                                             <div id="div<%# Eval("id") %>" style="overflow: auto; position: relative; overflow: auto"  >
                                                        <asp:GridView ID="gvOrdersPRICEPLN" runat="server" ShowHeader="true" CssClass="mGridcss"  PagerStyle-CssClass="pgr" 
                                                         AutoGenerateColumns="false" DataKeyNames="Id">
                                                            <Columns>
                                                            
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  HeaderText="Portal Features">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfeut" runat="server" Text='<%# Bind("productfeature") %>' Font-Bold="true"  ></asp:Label><br /><br />
                                                                        <asp:Label ID="lblpid" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblcidpo" runat="server" Text='<%# Bind("portalid") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblfcxe1" runat="server" Text='<%# Bind("feturedisc") %>'></asp:Label>
                                                                       <%-- <%# HttpUtility.HtmlDecode(Eval("feturedisc").ToString())%>--%>
                                                                  
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        
                                                    </div>
                                                                  </td>

                                                                  </tr>
                                                                  </table> 
                                                                  
                                                                 
                                                                </ItemTemplate>
                                                                </asp:TemplateField>
                                                               
                                                            </Columns>
                                        </asp:GridView>

                                     </td>
                                     </tr>
                                     </table> 
                                     

                                  </div>
                               </asp:Panel>
                </td>
                </tr>
            </table>
        </fieldset>
   
   </div>
  <div style="clear: both;">
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            <asp:Label ID="lblproductid" runat="server" ForeColor="Red"></asp:Label>
            <input id="hdnPricePlanId" name="hdnPricePlanId" runat="server" type="hidden" style="width: 1px" />
        </div>
        
           </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>

