<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" AutoEventWireup="true" CodeFile="giftcertdetail.aspx.cs" Inherits="ShoppingCart_Admin_giftcertdetail" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    &nbsp;
    
<style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
    </style>
    
    
    <script>
    
     function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        </script>
        
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div style="float: left;">
     <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
     </div>
                    <div style="clear: both;">
                        </div> 
                        
                         <div class="products_box">
                          <fieldset>
                <legend>
             
                <asp:Label runat="server" ID="title" Text="List of Gift Cards Issued"></asp:Label>
               </legend> 
               <div style="float: right;">
                     
                            <asp:Button ID="btnaddcard" runat="server" CssClass="btnSubmit" 
                                Text="Add New Gift Card" onclick="btnaddcard_Click"
                                 />
                       
                    </div>
               <table width="100%">
                <tr>
               <td colspan="2">
               <div style="clear: both;">
                    </div> 
               </td>
               </tr>
               
               <tr>
               <td>
               <label>
               <asp:Label runat="server" ID="lblbname" Text="Filter by Business"></asp:Label>
               </label>
               </td>
               <td>
               <label>
               <asp:DropDownList  runat="server" ID="ddlfilterbusiness" AutoPostBack="true" 
                       onselectedindexchanged="ddlfilterbusiness_SelectedIndexChanged"></asp:DropDownList>
               </label>
               </td>
                </tr>
               <tr>
               <td>
               <label>
               <asp:Label ID="lbluser" runat="server" Text="Filter Bought by User"></asp:Label>
               </label>
               </td>
               <td>
               <label>
               <asp:DropDownList runat="server" ID="ddlboughtuserid" 
                      > </asp:DropDownList>
               </label>
               </td>
              
               
               </tr>
               <tr>
               <td>
               <label>
               <asp:Label runat="server" ID="lblfilter" Text="Filter"></asp:Label>
               </label>
               </td>
               <td>
               <label>
                <asp:RadioButton ID="rdperiod" AutoPostBack="true" Checked="true" GroupName="1" Text="Period" 
                       runat="server" oncheckedchanged="rdperiod_CheckedChanged"></asp:RadioButton>
               </label>
               <label>
               <asp:RadioButton ID="rddate" AutoPostBack="true" OnCheckedChanged="rdchanged" Text="Date" runat="server" GroupName="1"></asp:RadioButton>
               </label>
               </td>
               </tr>
               <tr>
               <td>
                
               </td>
              <td>
             <asp:Panel ID="pnlperiod" runat="server">
                                            <table id="Table7">
                                                <tr>
                                                    <td style="width: 25%">
                                                        <label>
                                                            <asp:Label ID="Label17" runat="server" Text="Period"></asp:Label>
                                                       
                                                            <asp:DropDownList ID="ddlperiod" runat="server">
                                                            </asp:DropDownList>
                                                      
                                                        </label>
                                                    </td>
                                                    <td style="width: 25%">
                                                        
                                                    </td>
                                                    <td style="width: 25%">
                                                        <label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 25%">
                                                        <label>
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
              <asp:Panel ID="pnldate" runat="server" Visible="false">
                <label>
                <asp:Label runat="server" ID="lblfrom" Text="From"></asp:Label>
                <asp:TextBox ID="txtfrom" runat="server"></asp:TextBox>
                  <cc1:CalendarExtender  ID="CalendarExtender1" runat="server"
                                TargetControlID="txtfrom">
                            </cc1:CalendarExtender>
                             <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfrom" />
                </label>
                <label>
                <asp:Label runat="server" ID="lblto" Text="To"></asp:Label>
                <asp:TextBox ID="txttodate" runat="server" s
                     ></asp:TextBox>
                  <cc1:CalendarExtender  ID="txtdate_CalendarExtender" runat="server"
                                TargetControlID="txttodate">
                            </cc1:CalendarExtender>
                             <cc1:MaskedEditExtender ID="TextBox1_MaskedEditExtender" runat="server" CultureName="en-AU"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txttodate" />
                </label>
                </asp:Panel>
               </td>
               <tr >
               <td ></td>
               <td>
               <asp:Button ID="btngo" runat="server" CssClass="btnSubmit" Text="Go" onclick="btngo_Click"
                                  />
                    
               </td>
               </tr>
               <tr>
                  
               <td colspan="2">
                <div style="float: right;">
                    
                            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click" CausesValidation="False" />
                            <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                      type="button" value="Print" visible="false" />
                      
                     </div>  
                  
                    
               </td>
               
               </tr>
               </tr>
               
               <tr>
               <td colspan="2">
               
                 <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                <asp:Label runat="server" ID="name" Font-Italic="true" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label7" runat="server" Font-Italic="True" Text="List of Gift Cards Issued"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <asp:GridView runat="server" ID="grdviewlist" Width="100%" CssClass="mGrid" 
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                                        AutoGenerateColumns="False" EmptyDataRowStyle-HorizontalAlign="Center" 
                                        EmptyDataText="No Record Found." onsorting="grdviewlist_Sorting" 
                                        onrowcommand="grdviewlist_RowCommand" onrowdeleting="grdviewlist_RowDeleting" 
                                        onrowupdating="grdviewlist_RowUpdating">
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("DateandTime") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Gift Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" 
                                                    Text='<%# Eval("Giftcertificatemasterid") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="User ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbluid" runat="server" Text='<%# Eval("UserID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Max Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamnt" runat="server" Text='<%# Eval("MaxAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Available Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblavailamnt" runat="server" Text='<%# Eval("BalanceAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderStyle-HorizontalAlign="Left" HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif" HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ImageUrl="~/Account/images/edit.gif" CommandName="Edit" ID="Image48" 
                                                    runat="server" CommandArgument='<%# Eval("GiftCertificatemasterId") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/trash.jpg" HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ImageUrl="~/Account/images/delete.gif" CommandName="Delete" OnClientClick="return confirm('Do you wish to delete this record?');" ID="ImageButton48" 
                                                    runat="server" CommandArgument='<%# Eval("GiftCertificatemasterId") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                
               
                                  </td>
                                  </tr>
                                  </table>
                                  </asp:Panel>
                                 
                                      
                 
                 </td>
               
               </tr>
               
               </table>
                     <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
                        Width="300px">
                        <table cellpadding="0" cellspacing="0" style="width: 290px">
                            <tr>
                                <td>
                                    Confirm Delete
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" ForeColor="Black">You Sure , You Want to 
                                    Delete !</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="ImageButton2" runat="server" AlternateText="submit" Text="Yes" OnClick="ImageButton2_Click" />
                                    <asp:Button ID="ImageButton5" runat="server" Text="No" AlternateText="cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                  <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel3" TargetControlID="HiddenButton222">
                    </cc1:ModalPopupExtender>
               
               </fieldset>
               </div>
               </ContentTemplate>
               </asp:UpdatePanel>
</asp:Content>