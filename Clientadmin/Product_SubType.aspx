<%@ Page Title="" Language="C#"MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="Product_SubType.aspx.cs" Inherits="ProductType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lblmsg" runat="server" ForeColor="#CC3300" Visible="False" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
             <fieldset>
             <legend>
            <asp:Label ID="Label19" runat="server" Text="Product Sub Type Add / Manage"></asp:Label>
        </legend>
        <div style="float: right;">
            <asp:Button ID="Button1" runat="server"  ForeColor="Black" onclick="Button1_Click" Text="Add New Sub Type" />
        </div>
        <div style="clear: both;">
        </div>
            
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    <table style="width: 100%">
                         <tr>
                            <td width="20%">
                            <label>
                                <asp:Label ID="Label1" runat="server" Text="Product Type: "></asp:Label>
                                 <asp:Label ID="Label6" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Pdtypeddl"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </label> 
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="Pdtypeddl" runat="server" Height="16px">
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
                                <asp:Label ID="Label11" runat="server" Text="Sub Type Name: "></asp:Label>
                                  <asp:Label ID="Label63" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator178" runat="server" ControlToValidate="TextBox1"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                </label> 
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
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
                            <label>
                                <asp:Label ID="Label13" runat="server" Text="Inactive Date: "></asp:Label>
                                </label> 
                            </td>
                            <td width="20%">
                              <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                                    PopupButtonID="txtEndDate" TargetControlID="txtEndDate">
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
                            <label>
                                <asp:Label ID="Label14" runat="server" Text="Image: "></asp:Label>
                                </label> 
                            </td>
                            <td width="20%">
                                <asp:Image ID="Image1" runat="server" Style="height: 75px;
    width: 99px;" Visible="False" />
     <asp:Label ID="Label15" runat="server" Visible="False" ></asp:Label>
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
                                &nbsp;</td>
                            <td width="20%">
                                <asp:Button ID="Btnsave" runat="server" ForeColor="Black" 
                                   ValidationGroup="1"   onclick="Btnsave_Click" Text="Save" />
                                <asp:Button ID="Btnupdate" runat="server" ForeColor="Black" Text="Update" 
                                  ValidationGroup="1"    onclick="Btnupdate_Click" Visible="False" />
                                <asp:Button ID="Btncancel" runat="server" ForeColor="Black" 
                                    onclick="Btncancel_Click" Text="Cancel" />
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
                <asp:Panel ID="Panel2" runat="server" GroupingText="List Of Product Sub Type">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    
                                    <tr>
                                    <td colspan="2">
                                         <label style="width:150px;">
                                                <asp:Label ID="Label2" runat="server" Text="Product Type: "></asp:Label>
                                        </label>
                                        <label>
                                        <asp:DropDownList ID="ddlproducttypeFiltr" runat="server" >
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
                                                CssClass="mGrid" PagerStyle-CssClass="pgr" Width="100%" AllowPaging="True" 
                                               EmptyDataText="No Record Found."     onpageindexchanging="GridView1_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="Sub Type Name" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="ProductTypeName" HeaderText="Product Type Name" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Active" HeaderText="Active" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="InactiveDate" HeaderText="Inactive Date" DataFormatString="{0:MM/dd/yyy}" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="ImageFileName" HeaderText="Image File Name" Visible="false"  />
                                                    <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" HeaderText="Edit" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton3" runat="server" 
                                                              
                                                                ImageUrl="~/Account/images/edit.gif" onclick="ImageButton3_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif" 
                                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" HeaderText="Delete" 
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                         <asp:Label ID="Label15" runat="server"  Text='<%#Eval("ID") %>' Visible="False"></asp:Label>
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
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
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

