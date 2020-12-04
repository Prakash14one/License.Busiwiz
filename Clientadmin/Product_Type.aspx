<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="Product_Type.aspx.cs" Inherits="ProductType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table style="width: 100%">
        <tr>
            <td>
            
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" ></asp:Label>
               
            </td>
        </tr>
        <tr>
            <td>
             <fieldset>
     
        <legend>
            <asp:Label ID="Label19" runat="server" Text="Product Type Add   Manage"></asp:Label>
        </legend>
        <div style="float: right;">
            <asp:Button ID="addnewpanel" runat="server" Text="Product Type Add " CssClass="btnSubmit" onclick="addnewpanel_Click" />
        </div>
        <div style="clear: both;">
        </div>
                <asp:Panel ID="pnladdnew" runat="server"  Visible="false">
                    <table style="width: 100%">
                    <tr>
                            <td width="20%">
                            <label>
                                <asp:Label ID="Label1" runat="server" Text="Sales Type: "></asp:Label>
                                 <asp:Label ID="Label6" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsalestype"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="ddlsalestype" runat="server" >
                                </asp:DropDownList>
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                            <label>
                                <asp:Label ID="Label11" runat="server" Text="Product Type: "></asp:Label>
                                 <asp:Label ID="Label63" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator178" runat="server" ControlToValidate="txtproductType"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td width="20%">
                           
                                <asp:TextBox ID="txtproductType" runat="server"></asp:TextBox>
                                
                            </td>
                            <td align="right" colspan="3" style="width: 40%">
                                &nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td width="20%">
                            <label>
                                <asp:Label ID="Label12" runat="server" Text="Active: "></asp:Label>
                                </label>
                            </td>
                            <td width="20%">
                                <asp:CheckBox ID="chkboxActiveDeactive" runat="server" Text="Active" />
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                
                                <asp:Image ID="Image1" runat="server" Style="height: 75px;  width: 99px;" />
                                <asp:Label ID="Label18" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                               
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                            <label>
                                <asp:Label ID="Label14" runat="server" Text="Image: "></asp:Label>
                                </label>
                            </td>
                            <td width="20%">
                                
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="190px" />
                                <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Upload" />
                            </td>
                            <td width="20%" colspan="2" style="width: 40%">
                                &nbsp;</td>
                            <td width="20%">
                               
                            </td>
                        </tr>
                         <tr>
                            <td width="20%">
                            <label>
                                <asp:Label ID="Label13" runat="server" Text=" "></asp:Label>
                                </label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="txtEndDate" runat="server" Visible="false"> </asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtEndDate" TargetControlID="txtEndDate">
                                </cc1:CalendarExtender>
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                <asp:Button ID="Btnsave" runat="server" ForeColor="Black"  ValidationGroup="1" onclick="Btnsave_Click" Text="Save" />
                                <asp:Button ID="Btnupdate" runat="server" ForeColor="Black" Text="Update" ValidationGroup="1" onclick="Btnupdate_Click" />
                                <asp:Button ID="Btncancel" runat="server" ForeColor="Black"  onclick="Btncancel_Click" Text="Cancel" />
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>

                 </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel2" runat="server" GroupingText="List Of Product Type">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="2">
                                         <label style="width:100px;">
                                                <asp:Label ID="Label2" runat="server" Text="Sales Type: "></asp:Label>
                                        </label>
                                        <label>
                                        <asp:DropDownList ID="ddlsalestypeFiltr" runat="server" >
                                        </asp:DropDownList>
                                        </label> 
                                         <label style="width:100px;">
                                                          <asp:Label ID="Label3" runat="server" Text="Active: "></asp:Label>
                                     </label>
                                     <label>
                                                <asp:DropDownList ID="ddlstatus" runat="server"  Width="181px" >
                                                  <asp:ListItem  Text="All" Selected="True"></asp:ListItem>
                                                         <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                                 <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                                </asp:DropDownList>
                                 </label>
                                  <label>
                                            <asp:Button ID="BtnGo" CssClass="btnSubmit" runat="server" Text="Go" OnClick="BtnGo_Click"  />
                                </label> 
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                                CssClass="mGrid" PagerStyle-CssClass="pgr" Width="100%" 
                                             EmptyDataText="No Record Found."    onpageindexchanging="GridView1_PageIndexChanging" PageSize="20" AllowPaging="True">
                                                <Columns>
                                                    <asp:BoundField DataField="ProductTypeName" HeaderText="Product Type Name" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Name" HeaderText="Sales Type" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Active" HeaderText="Active" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="InactiveDate" HeaderText="Inactive Date" HeaderStyle-HorizontalAlign="Left" Visible="false" 
                                                        DataFormatString="{0:MM/dd/yyy}" />
                                                    <asp:BoundField DataField="ImageFileName" HeaderText="Image File Name" Visible="false"  />
                                                    <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" 
                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Edit" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton3" runat="server" 
                                                              
                                                                ImageUrl="~/Account/images/edit.gif" onclick="ImageButton3_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" HeaderText="Delete"  ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                         <asp:Label ID="Label18" runat="server"  Text='<%#Eval("ID") %>' Visible="False"></asp:Label>
                                                            <asp:ImageButton ID="imgdelete" runat="server" ImageUrl="~/Account/images/delete.gif" ToolTip="Delete" onclick="imgdelete_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            </td>
                                        <td>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </ContentTemplate>
   


     <Triggers>
 <asp:PostBackTrigger ControlID="Button2" />
 </Triggers>
    </asp:UpdatePanel>
   

</asp:Content>

