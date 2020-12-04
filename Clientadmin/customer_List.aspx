<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="customer_List.aspx.cs" Inherits="CustomerList" Title="Customer List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
    </script>
    
      
  <style type="text/css">
         .open {
            display: block;
        }

        .closed {
            display: none;
        }

        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=50);
            opacity: 1.2;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 22px;
            padding-left: 0px;
            width: 250px;
            height: 380px;
            top:250px;
        }
        .lblManan
        {
           width: 80px;
	display: block;
	color: #333;
    font-family: Georgia,"Times New Roman",Times,serif;
    font-weight: normal

        }
    </style>
    <fieldset>
        <legend>
            <asp:Label ID="Label1" runat="server" Text="Customer List"></asp:Label>
        </legend>
        <table width="100%">
        <tr>
        <td width="100%">
       
        </td>
        </tr>
        </table>
        
        <table width="100%">
       
        <asp:Panel ID="pnl_company" Visible="true" runat="server">
          </asp:Panel>
           <asp:Panel ID="pnl_portal" Visible="true" runat="server">
           </asp:Panel>
           <asp:Panel ID="pnl_date" Visible="true" runat="server">
             </asp:Panel>
              <asp:Panel ID="pnl_all" Visible="false" runat="server">
                </asp:Panel>
              <tr>
              <td colspan="4" align="right">
            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version" 
                OnClick="Button1_Click1" />
                <input id="Button5" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                                    style="width: 51px;" type="button" value="Print" visible="false" />
          
                </td>
       </tr> 
            <tr>
                <td width="25%">
                  <label>
                  <asp:Label ID="Label2" runat="server" Text="Filter by Status"></asp:Label>
                 
                        <asp:DropDownList ID="ddlActive" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlActive_SelectedIndexChanged">
                            <asp:ListItem Value="-1">Null</asp:ListItem>
                            <asp:ListItem Value="1">Active</asp:ListItem>
                            <asp:ListItem Value="0" >Deactive</asp:ListItem>
                        </asp:DropDownList>
                    </label>  
                    
                </td>
               <td width="25%">
                    <label>
                      <asp:Label ID="Label8" runat="server" Text="Filter By  Product"></asp:Label>
                                <asp:DropDownList ID="ddlProductname1" runat="server" AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddlProductname1_SelectedIndexChanged">
                                </asp:DropDownList>
                           </label>
                           </td>
                           <td width="25%">
                           <label> 
                 
                        <asp:Label ID="Label3" runat="server" Text="Filter By  Portal"></asp:Label>
                        <asp:DropDownList ID="ddlsortDomain" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsortfilter_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    
                    <%-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                </td>
                 <td width="25%">
                  <label>
                      <asp:Label ID="Label4" runat="server" Text="  Filter By Plan"></asp:Label>
                        <asp:DropDownList ID="ddlsortPlan" runat="server" AutoPostBack="True" 
                         onselectedindexchanged="ddlsortPlan_SelectedIndexChanged">
                        </asp:DropDownList>
                       
                    </label>
                 </td>
            </tr>
            <tr>
            <td >
             <label>
                        <asp:Label ID="Label6" runat="server" Text="Filter By Server Name"></asp:Label>
                  
                        <asp:DropDownList ID="ddlfillservernm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfillservernm_SelectedIndexChanged">
                            <asp:ListItem>---Select All---</asp:ListItem>
                            <asp:ListItem>User Server</asp:ListItem>
                            <asp:ListItem>busiwiz Server</asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    </td>
                    <td>
                       <label>
                                Country
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlcountry"
                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="True" DataTextField="CountryName"
                                    DataValueField="CountryId" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                    </td>
                     <td>
                             <label for="state1">
                                State
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlstate"
                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1">
                                </asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlstate" runat="server">
                                </asp:DropDownList>
                            </label>
                            </td>
                            <td>
                             <label>
                                City
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtcity"
                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtcity" MaxLength="100" runat="server" Width="200px"> 
                                </asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                    TargetControlID="txtcity" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                </cc1:FilteredTextBoxExtender>

                                
                            </label>
                                      
            </td>
            </tr>
            <tr>
            <td>
            <label>
            License Start Date 
             <asp:TextBox ID="txtstartdate" runat="server" Width="200px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                        PopupButtonID="txtstartdate" TargetControlID="txtstartdate">
                    </cc1:CalendarExtender>
            </label>
            </td>
            <td>
            <label>
              License End Date 
             <asp:TextBox ID="txtenddate" runat="server" Width="200px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                        PopupButtonID="txtenddate" TargetControlID="txtenddate">
                    </cc1:CalendarExtender>
                    </label>
            </td>
            <td>
             <label>
                    Search
                        <asp:TextBox ID="txtsortsearch" runat="server" width="200px" ></asp:TextBox>
                        </label>
            </td>
            <td>
            
                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="CheckBox1_CheckedChanged" 
                    Text="Show Active Records in Dropdown" />
            
            </td>
            </tr>
            
             
            <tr>
               
            <td>
               
                        <asp:Button ID="btnsubmit" runat="server" CssClass="btnSubmit" Text="Go"   OnClick="btngodate_Click"  Height="30px"    />
            </td>
            </tr> 
       
           
           
        </table>
        <asp:Panel ID="pnlgrid" runat="server" Width="100%">
            <table width="100%">
                <tr>
                    <td align="right">
                        <div id="mydiv" class="closed">
                            <table width="850Px">
                                <tr align="center">
                                    <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                        <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                    </td>
                                </tr>
                                <%--<tr align="center">
                                    <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                        <asp:Label ID="Label67" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                        <asp:Label ID="lblBusiness" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr align="center">
                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                        <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Customers"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Button ID="btngodate" runat="server" CssClass="btnSubmit" Text="Show/Hide Columns" OnClick="btngodate_Clickopenpnl" Visible="true" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="left">
                        <asp:Panel ID="Panel1" runat="server" Width="100%">
                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                                AutoGenerateColumns="False" DataKeyNames="CompanyId" 
                                OnRowCommand="GridView1_RowCommand" EmptyDataText="No Record Found." 
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                Width="100%" OnSorting="GridView1_Sorting" AllowPaging="True" PageSize="10" 
                                OnPageIndexChanging="GridView1_PageIndexChanging1" 
                                onselectedindexchanged="GridView1_SelectedIndexChanged1">      
                                 <AlternatingRowStyle CssClass="alt" />
                                 <Columns>
                                     <%--<asp:BoundField DataField="CompanyName" SortExpression="CompanyName" HeaderStyle-HorizontalAlign="Left" HeaderText="Company Name" />  --%>
                                     <asp:TemplateField HeaderText="Company Name">
                                         <ItemTemplate>
                                             <asp:LinkButton ID="LinkButton1"  CommandArgument='<%# Eval("CompanyLoginId") %>' CommandName="View1" ForeColor="Black" 
                                                    Text='<%# Eval("CompanyName") %>' ToolTip="View profile"  runat="server">LinkButton</asp:LinkButton>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:BoundField DataField="CompanyLoginId" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="Company Login Id" SortExpression="CompanyLoginId">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="phone" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="Phone" SortExpression="phone">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="PortalName" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="Portal Name/Domain " SortExpression="PortalName">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                     <%-- <asp:BoundField DataField="ProductURL" SortExpression="ProductURL" HeaderStyle-HorizontalAlign="Left" HeaderText="Domain" />--%>
                                     <asp:BoundField DataField="active" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="Active" SortExpression="active">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="PricePlanName" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="Plan" SortExpression="PricePlanName">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="PricePlanAmount" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="Price/Month" SortExpression="PricePlanAmount">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="LicenseKey" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="License Key" SortExpression="LicenseKey">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                     <%--  <asp:BoundField DataField="Expr1" SortExpression="Expr1" HeaderStyle-HorizontalAlign="Left" HeaderText="Price/Month" />--%>
                                     <asp:BoundField DataField="CompanyLoginId" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="Company ID" SortExpression="CompanyLoginId">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="city" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="City" SortExpression="city">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="StateName" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="State" SortExpression="StateName">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="CountryName" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="Country" SortExpression="CountryName">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="ProductName" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="Product Name" SortExpression="ProductName">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                         <ItemTemplate>
                                             <asp:Label ID="lblportal" runat="server" Text='<%#Bind("PortalName")%>'></asp:Label>
                                             <asp:Label ID="lblOrderId" runat="server" Text='<%#Bind("OrderId")%>'></asp:Label>
                                             <asp:Label ID="lblserverId13" runat="server" Text='<%#Bind("ServerId")%>'></asp:Label>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:BoundField DataField="Email" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="Email ID " SortExpression="Email" Visible="false">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Location 15 " 
                                         Visible="false">
                                         <ItemTemplate>
                                             <asp:Label ID="lblcity" runat="server" Text='<%#Bind("city")%>'></asp:Label>
                                             ,
                                             <asp:Label ID="lblStateName" runat="server" Text='<%#Bind("StateName")%>'></asp:Label>
                                             ,
                                             <asp:Label ID="lblCountryName" runat="server" Text='<%#Bind("CountryName")%>'></asp:Label>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                  <asp:BoundField DataField="OrderId" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="Order Id" SortExpression="OrderId" Visible="false">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                       
                                       <asp:TemplateField HeaderText="License Start Date"  >
                                           <ItemTemplate >
                                                                <asp:Label ID="Label122" runat="server" Text='<%# Eval("LicenseDate","{0:MM/dd/yyyy}") %>' ></asp:Label>
                                                                 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                      <asp:BoundField DataField="LicensePeriod" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="License Duration" SortExpression="LicensePeriod">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                       <asp:TemplateField HeaderText="License End date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label125" runat="server" Text='<%# Eval("LicenseEnddate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                     <asp:ButtonField ButtonType="Image" CommandName="edit1" 
                                         HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" 
                                         HeaderText="Edit" ImageUrl="~/Account/images/edit.gif" Text="Button">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     </asp:ButtonField>
                                    
                                     <asp:ButtonField ButtonType="Button" CommandName="Next" 
                                         HeaderStyle-HorizontalAlign="Left" HeaderText="More Info" 
                                         ItemStyle-Height="30px" Text="More Info" Visible="false">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <ItemStyle Height="30px" />
                                     </asp:ButtonField>
                                    
                                      <asp:TemplateField HeaderText="Approve By Admin">
                                          <ItemTemplate>
                                              <asp:ImageButton ID="ImageButton3" runat="server" 
                                                  ImageUrl="~/Account/images/approve.png" Width="50px" Height="30px" 
                                                  onclick="ImageButton1_Click" ToolTip="Approve" />
                                               <asp:Label ID="Label24" runat="server"  Text='<%# Eval("CompanyLoginId") %>' Visible="false" ></asp:Label>
                                              <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Account/images/approved.png" Width="30px" Height="30px" />
                                              <asp:Label ID="Label5" runat="server"  Text=" Need to  Approve" Visible="false" ToolTip="Approved"></asp:Label>
                                              <asp:Label ID="Label7" runat="server"  Text="Approved" Visible="false" ></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="10px" />
                                     </asp:TemplateField>
                                  
                                     
                                     <%--<asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/img/edit.gif" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:LinkButton ID="ImageButton3" runat="server" ImageUrl="~/img/edit.gif" CommandName="Next"--%>
                                     <%-- CommandArgument='<%# Eval("VacancyMst_Id") %>' />--%>
                                     <%--<asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/img/edit.gif" CommandName="Edit"
                                CommandArgument='<%# Eval("VacancyMst_Id") %>' HeaderImageUrl="~/Image/nxt.jpg"/ImageUrl="~/Image/nxt.jpg">--%>
                                     <%--</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="10%" />
                    </asp:TemplateField>--%>
                                 </Columns>
                                 <PagerStyle CssClass="pgr" />
                            </asp:GridView>
                        </asp:Panel>
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                    </td>
                </tr>
            </table>

                <asp:Panel ID="Panel4"  runat="server"  width="450px" Visible="false" >
                  <div style="position:absolute;     margin: -645px 0px 0px 322px; width:482px; background-color: #dddddd;" >
               <fieldset>
           <legend>
            <asp:Label ID="Label119" runat="server" Text="Show/Hide Columns"></asp:Label>
           </legend>
           
               <asp:Panel ID="Panel2" runat="server" width="450px"  >
                <table width="80%">
                <tr>
                 <td  width="40%" >
                 </td>
                <td  width="40%" >
                &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;
                                <asp:ImageButton ID="ImageButton5" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                  Width="16px" OnClick="ImageButton2_Click" />
                                  </td>  
               </tr>
                               <tr>
                               <td width="40%">
                                 <asp:CheckBox ID="chkproduct" runat="server"  AutoPostBack="true"  Checked="false" Text="Product"  />
                                </td> 
                              <%-- <td width="40%">
                               <asp:CheckBox ID="chkportal" runat="server"  AutoPostBack="true"  Checked="false" Text="Portal"  />
                               </td> --%>
                             
                              
                               <td  width="40%">
                                <asp:CheckBox ID="chkprice" runat="server"  AutoPostBack="true"  Checked="false" Text="Price Plan"  />                               
                               </td> 
                               </tr>
                               <tr> 
                               <td  width="40%">             
                           <asp:CheckBox ID="chklinse" runat="server"  AutoPostBack="true"  Checked="false" Text="License Key"  />
                              </td> 
                             
                             
                               <td width="40%">
                               <asp:CheckBox ID="chkliduration" runat="server"  AutoPostBack="true"  Checked="false" Text="License Duration"  />
                              
                              
                               </td> 
                             </tr>
                              <tr>
                               <td width="40%">     
                                  <asp:CheckBox ID="chlicensedate" runat="server"  AutoPostBack="true"  Checked="false" Text="License Start Date"  />      
                        
                               </td> 

                               <td width="40%"> 
                                <asp:CheckBox ID="chlicensedatend" runat="server"  AutoPostBack="true"  Checked="false" Text="License End Date"  />            
                         
                            </td> 
                            </tr>
                             <tr>
                               <td width="40%">     
                                      
                         <asp:CheckBox ID="chkcontry" runat="server"  AutoPostBack="true"  Checked="false" Text="Country"  />            
                               </td> 

                               <td width="40%"> 
                               
                         <asp:CheckBox ID="chkstate" runat="server"  AutoPostBack="true"  Checked="false" Text="State "  /> 
                            </td> 
                            </tr>
                            
                           <%-- <td width="40%">
                           <asp:CheckBox ID="chkserver" runat="server"  AutoPostBack="true"  Checked="false" Text="Server Name "  />
                          </td> --%>
                        <tr>
                     
                         <td width="40%">
                           <asp:CheckBox ID="chkcity" runat="server" AutoPostBack="true"  Checked="false" Text="City "  />

                        </td>
                        </tr>
                          <tr>
                          <td colspan="2" align="center">
                              <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Go"   OnClick="btngodate_Clickcccccc"  Height="30px"    />
                          </td>
                          </tr>
                </table>

                    
                </asp:Panel>
                </fieldset>
           </div>
                </asp:Panel>
            <table>
            <tr>
            <td>
            
            </td>
            </tr>
            </table> 
        </asp:Panel>
          <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" Text="V2" ForeColor="#416271" Font-Size="10px"></asp:Label>
              </div>
    </fieldset>
</asp:Content>
