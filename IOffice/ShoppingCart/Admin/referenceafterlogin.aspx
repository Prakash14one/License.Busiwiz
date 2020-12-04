<%@ Page Language="C#" AutoEventWireup="true" CodeFile="referenceafterlogin.aspx.cs" Inherits="WebSite1_referenceafterlogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
          
            .container
            {
                width:100%;
                height:100px;
                   background-color: #416271;}
                .left
                {float:left;
                    width:19%;
                    height:1200px;
                  
                    margin-top:15px;}
                    
                    .right
                    {
                        float: left;
width: 60%;
height: 50px;

margin-top: 72px;
margin-left: 60px;}
                    .clear
                    {
                        clear:both;}
            .style1
            {
                width: 100%;
            }
            .style4
            {
                width: 142px;
            }
                                      
        .butten{
           width: 58%;
           height:50px;
         
          }
          
          .clear
          {
              clear:both;}
                .mGrid
{
	width: 100%;
	background-color: #fff;
	margin: 5px 0 10px 0;
	border: solid 1px #525252;
	border-collapse: collapse;
	font-size: 13px !important;

}
.mGrid a
{
	font-size: 15px !important;
	color:White;
}
.mGrid a:hover
{
	font-size: 15px !important;
	color:White;
	text-decoration:underline;
}
.mGrid td
{
	padding: 2px;
	border: solid 1px #c1c1c1;
	color: #717171;
}
.mGrid th
{
	padding: 4px 2px;
	color: #fff;
	background-color: #416271;
	border-left: solid 1px #525252;
	font-size: 14px !important;
}
.mGrid .alt
{
	background: #fcfcfc url(grd_alt.png) repeat-x top;
	background-color: #CCCCCC;

}
.mGrid .pgr
{
	background-color: #416271;
}
.mGrid .ftr
{
	background-color: #416271;
	font-size: 15px !important;
	color:White;
	border: solid 1px #525252;
}
.mGrid .pgr table
{
	margin: 5px 0;
}
.mGrid .pgr td
{
	border-width: 0;
	padding: 0 6px;
	border-left: solid 1px #666;
	font-weight: bold;
	color: #fff;
	line-height: 12px;
}
.mGrid .pgr a
{
	color: Gray;
	text-decoration: none;
}
.mGrid .pgr a:hover
{
	color: #000;
	text-decoration: none;
}
.mGrid input[type="checkbox"]
{
	margin-top: 5px !important;
	width: 10px !important;
	float: left !important;
}
.mGrid input[type="radio"]
{
	/*margin-top: 10px !important;*/
	
	float: left !important;
        }
        .style8
        {
            width: 25%;
        }
        .style9
        {
            height: 26px;
        }
        .style10
        {
            width: 142px;
            height: 26px;
        }
        .style11
        {
            height: 23px;
        }
        </style>
</head>
<body>
    
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <div>
    <div class="container">
    </div>
        <div class="left">
        <div class="butten">
            <asp:LinkButton ID="LinkButton1" runat="server" Text="Reference Requests" 
                onclick="LinkButton1_Click"></asp:LinkButton>
            <p class="clear"></p>
            <asp:LinkButton ID="LinkButton2" runat="server" Text="My Active References" 
                onclick="LinkButton2_Click"></asp:LinkButton>
             <p class="clear"></p>
                <asp:LinkButton ID="LinkButton4" runat="server" Text="My Profile " 
                onclick="LinkButton4_Click"></asp:LinkButton>
                 <p class="clear"></p>
                <asp:LinkButton ID="LinkButton3" runat="server" Text="Login Information" 
                onclick="LinkButton3_Click"></asp:LinkButton>
</div>
</div>
        <div>  <asp:Label ID="Label16" runat="server" ForeColor="Red" ></asp:Label>
        </div>            
         <asp:Panel ID="Panel1" runat="server" 
            GroupingText="List of Reference Requests" Visible="False">
           <%-- <div class="right">
            </div>--%>
             <table class="style1">
                 <tr>
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
                     <td colspan="4">
                         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                             CssClass="mGrid" EmptyDataText="No Record Found" 
                             onrowdatabound="GridView1_RowDataBound" 
                             onrowcommand="GridView1_RowCommand">
                             <Columns>
                                 <asp:TemplateField HeaderText="Candidate Picture">
                                     <ItemTemplate>
                                         <asp:ImageButton ID="ImageButton5" runat="server" Height="35px" Width="35px"  ImageUrl='<%#Eval("CandidatePhotoPath") %>'/>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Candidate Last Name">
                                     <ItemTemplate>
                                        
                                         
                                         <asp:Label ID="Label22" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Candidate First Name">
                                     <ItemTemplate>
                                         
                                         <asp:Label ID="Label23" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Give Reference">
                                     <ItemTemplate>
                                         <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="Black" 
                                             onclick="LinkButton6_Click">Give Reference</asp:LinkButton>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Ignore">
                                     <ItemTemplate>
                                         <asp:ImageButton ID="ImageButton6" runat="server" 
                                             ImageUrl="~/Account/images/delete.gif" onclick="ImageButton6_Click" />
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:BoundField DataField="refernceid" HeaderText="refernceid" />
                             </Columns>
                         </asp:GridView>
                     </td>
                 </tr>
                 <tr>
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
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" 
            GroupingText="My Profile " Visible="False">
                <table class="style1">
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:TextBox ID="txtname" runat="server" Width="160px"></asp:TextBox>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label2" runat="server" Text="Designation"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:TextBox ID="txtdesig" runat="server" Width="160px"></asp:TextBox>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label3" runat="server" Text="Company"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:TextBox ID="txtcom" runat="server" Width="160px"></asp:TextBox>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label4" runat="server" Text="Contact Phone Number"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:TextBox ID="txtph" runat="server" Width="160px"></asp:TextBox>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label5" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:TextBox ID="txtemail" runat="server" Width="160px"></asp:TextBox>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label6" runat="server" Text="Address"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" Width="160px"></asp:TextBox>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label9" runat="server" Text="Country"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="ddlcountry" runat="server" Width="160px" 
                                AutoPostBack="True" DataTextField="CountryName" DataValueField="CountryId" 
                                onselectedindexchanged="ddlcountry_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="25%">
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label8" runat="server" Text="State"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="ddlstate" runat="server" Width="160px" 
                                AutoPostBack="True" DataTextField="StateName" DataValueField="StateId" 
                                onselectedindexchanged="ddlstate_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label7" runat="server" Text="City"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="ddlcity" runat="server" Width="160px" 
                                DataTextField="CityName" DataValueField="CityId">
                            </asp:DropDownList>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            &nbsp;</td>
                        <td class="style8">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            &nbsp;</td>
                        <td class="style8">
                            <asp:Button ID="btnupdate" runat="server" onclick="btnupdate_Click" 
                                Text="Update" />
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                   </tr>
                    <tr>
                        <td width="25%">
                            &nbsp;</td>
                        <td class="style8">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

             <asp:Panel ID="Panel4" runat="server" 
            GroupingText="List of References Provided" Visible="False">
                 <table class="style1">
                     <tr>
                         <td width="25%">
                             <asp:Label ID="Label17" runat="server" Text="Filter by from date"></asp:Label>
                         </td>
                         <td width="25%">
                             <asp:TextBox ID="txtfromdate" runat="server" Width="160px"></asp:TextBox>
                              <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                       Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                         </td>
                         <td width="25%">
                             <asp:Label ID="Label18" runat="server" Text="To date"></asp:Label>
                         </td>
                         <td width="25%">
                             <asp:TextBox ID="txttodate" runat="server" Width="160px" AutoPostBack="True" 
                                 ontextchanged="txttodate_TextChanged"></asp:TextBox>
                              <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                                       Enabled="True" Format="yyyy-MM-dd" TargetControlID="txttodate"></cc1:CalendarExtender>
                         </td>
                     </tr>
                     <tr>
                         <td width="25%">
                             <asp:Label ID="Label19" runat="server" Text="Search"></asp:Label>
                         </td>
                         <td width="25%">
                             <asp:TextBox ID="TextBox10" runat="server" Width="160px" AutoPostBack="True" 
                                 ontextchanged="TextBox10_TextChanged" ToolTip="Enter the first name"></asp:TextBox>
                         </td>
                         <td width="25%">
                             &nbsp;</td>
                         <td width="25%">
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td width="25%">
                         </td>
                         <td width="25%">
                         </td>
                         <td width="25%">
                         </td>
                         <td width="25%">
                         </td>
                     </tr>
                     <tr>
                         <td colspan="4">
                             <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                 CssClass="mGrid" onrowdatabound="GridView2_RowDataBound" 
                                 EmptyDataText="No Record Found">
                                 <Columns>
                                     <asp:TemplateField HeaderText="Candidate Photo" >
                                         <ItemTemplate>
                                             <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%#Eval("CandidatePhotoPath") %>' Height="35px" Width="35px" />
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="First Name">
                                         <ItemTemplate>
                                             <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="Black" 
                                                 Text='<%#Eval("FirstName") %>' onclick="LinkButton5_Click"></asp:LinkButton>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Last Name">
                                         <ItemTemplate>
                                              <asp:LinkButton ID="LinkButton7" runat="server" ForeColor="Black" 
                                                  Text='<%#Eval("LastName") %>' onclick="LinkButton7_Click"></asp:LinkButton>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:BoundField HeaderText="City" DataField="CityName" />
                                     <asp:BoundField HeaderText="Country" DataField="CountryName" />
                                     <asp:BoundField HeaderText="Email" DataField="Email" />
                                     <asp:BoundField HeaderText="Phone No" DataField="MobileNo" />
                                     <asp:BoundField HeaderText="Date Reference Provided" DataField="Dateandtime" />
                                     <asp:TemplateField HeaderText="View Reference">
                                         <ItemTemplate>
                                             <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="Black" 
                                                 Text="View Reference" onclick="LinkButton5_Click1"></asp:LinkButton>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Delete">
                                         <ItemTemplate>
                                             <asp:ImageButton ID="ImageButton4" runat="server" 
                                                 ImageUrl="~/Account/images/delete.gif" onclick="ImageButton4_Click" />
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:BoundField DataField="refernceid" HeaderText="referenceid" />
                                     <asp:BoundField DataField="CandidateId" HeaderText="candidateid" />
                                 </Columns>
                             </asp:GridView>
                         </td>
                     </tr>
                 </table>
                 

            </asp:Panel>

             <asp:Panel ID="Panel3" runat="server" 
            GroupingText="Login Information " Visible="False">
                 <table class="style1">
                     <tr>
                         <td width="25%">
                             &nbsp;</td>
                         <td class="style4" width="25%">
                             <asp:Label ID="Label10" runat="server" Text="User ID"></asp:Label>
                         </td>
                         <td width="25%">
                             <asp:TextBox ID="TextBox12" runat="server" Enabled="False"></asp:TextBox>
                         </td>
                         <td width="25%">
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td width="25%" class="style9">
                             </td>
                         <td class="style10" width="25%">
                             <asp:Label ID="Label11" runat="server" Text="Password"></asp:Label>
                         </td>
                         <td width="25%" class="style9">
                             <asp:TextBox ID="TextBox11" runat="server" Enabled="False"></asp:TextBox>
                         </td>
                         <td width="25%" class="style9">
                             </td>
                     </tr>
                     <tr>
                         <td width="25%">
                             &nbsp;</td>
                         <td class="style4" width="25%">
                             &nbsp;</td>
                         <td width="25%">
                             <asp:Button ID="Button1" runat="server" Text="Change" onclick="Button1_Click" />
                         </td>
                         <td width="25%">
                             &nbsp;</td>
                     </tr>
                 </table>
            </asp:Panel>
            <asp:Panel ID="panel23" runat="server">
                <table class="style1" width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="This is Version 1 Updated on 10/28/2015 by Nithya"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="style11">
                            </td>
                    </tr>
                </table>
            </asp:Panel>
    </div>
    </form>
</body>
</html>
