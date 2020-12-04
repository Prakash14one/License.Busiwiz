<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="ProductMaster.aspx.cs" Inherits="ProductMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" ForeColor="#CC3300" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" GroupingText="Product Type Add/Manage" Visible="False" 
                    >
                    <table style="width: 100%">
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label11" runat="server" Text="Product Name"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 20%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="TextBox1" ErrorMessage="*" Enabled="False"></asp:RequiredFieldValidator>
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
                                <asp:Label ID="Label12" runat="server" Text="Status"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="180px">
                                    <asp:ListItem  Value="0" Text="Active"></asp:ListItem>
                                    <asp:ListItem  Value="1" Text="Inactive"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label17" runat="server" Text="Start Date"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="TextBox4_CalendarExtender" runat="server" 
                                    PopupButtonID="TextBox4" TargetControlID="TextBox4">
                                </cc1:CalendarExtender>
                            </td>
                            <td style="width: 20%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="TextBox3" ErrorMessage="*" Enabled="False"></asp:RequiredFieldValidator>
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
                                <asp:Label ID="Label13" runat="server" Text="End Date"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                                    PopupButtonID="TextBox3" TargetControlID="TextBox3">
                                </cc1:CalendarExtender>
                            </td>
                            <td style="width: 20%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="TextBox4" ErrorMessage="*" Enabled="False"></asp:RequiredFieldValidator>
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
                                <asp:Label ID="Label14" runat="server" Text="Product Type"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                                    Width="180px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%">
                                &nbsp;</td>
                            <td width="20%" colspan="2" style="width: 40%">
                                &nbsp;</td>
                            <td width="20%">
                               
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label19" runat="server" Text="Sales Type"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                                    Width="180px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%">
                                &nbsp;</td>
                            <td colspan="2" style="width: 40%" width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label20" runat="server" Text="Sub Type"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" 
                                    Width="180px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%">
                                &nbsp;</td>
                            <td colspan="2" style="width: 40%" width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label21" runat="server" Text="Sub sub Type"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" 
                                    Width="180px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%">
                                &nbsp;</td>
                            <td colspan="2" style="width: 40%" width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%" style="height: 28px">
                                <asp:Label ID="Label18" runat="server" Text="Stock Quantity"></asp:Label>
                            </td>
                            <td width="20%" style="height: 28px">
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                
                                
                            </td>
                            <td style="width: 20%; height: 28px;">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                    ValidationExpression="((\d+)((\.\d{1,2})?))$" runat="server" 
                                    ErrorMessage="Numbers Only" ControlToValidate="TextBox5"></asp:RegularExpressionValidator>
                            </td>
                            <td colspan="2" style="width: 40%; height: 28px;" width="20%">
                                </td>
                            <td width="20%" style="height: 28px">
                                </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label24" runat="server" Text="Picture Title"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                
                            </td>
                            <td style="width: 20%">
                                &nbsp;</td>
                            <td colspan="2" style="width: 40%" width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 28px" width="20%">
                                <asp:Label ID="Label27" runat="server" Text="List Price"></asp:Label>
                            </td>
                            <td style="height: 28px" width="20%">
                                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                
                            </td>
                            <td style="height: 28px; width: 20%;">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                    ControlToValidate="TextBox7" ErrorMessage="Numbers Only" 
                                    ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ControlToValidate="TextBox6" Enabled="False" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td colspan="2" style="width: 40%; height: 28px;" width="20%">
                            </td>
                            <td style="height: 28px" width="20%">
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label28" runat="server" Text="Sales Price"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                
                            </td>
                            <td style="width: 20%">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                    ControlToValidate="TextBox8" ErrorMessage="Numbers Only" 
                                    ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            </td>
                            <td colspan="2" style="width: 40%" width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label29" runat="server" Text="Effective Date"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="TextBox9_CalendarExtender" runat="server" 
                                    PopupButtonID="TextBox9" TargetControlID="TextBox9">
                                </cc1:CalendarExtender>
                            </td>
                            <td style="width: 20%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                    ControlToValidate="TextBox9" ErrorMessage="*" Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                            <td colspan="2" style="width: 40%" width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label22" runat="server" Text="Small Image"></asp:Label>
                            </td>
                            <td width="20%">
                        
                                <asp:Panel ID="Panel3" runat="server">  
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                </asp:Panel>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="Label26" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td colspan="2" style="width: 40%" width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                <asp:Image ID="Image11235" runat="server" Height="63px" Width="124px" 
                                    Visible="False" />
                            </td>
                            <td style="width: 20%">
                                &nbsp;</td>
                            <td colspan="2" style="width: 40%" width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label23" runat="server" Text="Large Image"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:Panel ID="Panel4" runat="server">
                                    <asp:FileUpload ID="FileUpload2" runat="server" />
                                </asp:Panel>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="Label25" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td colspan="2" style="width: 40%" width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                <asp:Image ID="Image11236" runat="server" Height="63px" Visible="False" 
                                    Width="123px" />
                            </td>
                            <td style="width: 20%">
                                &nbsp;</td>
                            <td colspan="2" style="width: 40%" width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%" style="height: 28px">
                                </td>
                            <td width="20%" style="height: 28px">
                            </td>
                            <td style="height: 28px; width: 20%;">
                                </td>
                            <td colspan="2" style="width: 40%; height: 28px;" width="20%">
                                </td>
                            <td width="20%" style="height: 28px">
                                </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                <asp:Panel ID="Panel5" runat="server">
                                    <asp:Button ID="Button2" runat="server" onclick="Button2_Click1" 
                                        Text="Upload  Image" />
                                </asp:Panel>
                            </td>
                            <td style="width: 20%">
                                &nbsp;</td>
                            <td colspan="2" style="width: 40%" width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td style="width: 20%">
                                &nbsp;</td>
                            <td colspan="2" style="width: 40%" width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                <asp:Button ID="Btnsave" runat="server" ForeColor="Black" 
                                    onclick="Btnsave_Click" Text="Save" style="height: 26px" />
                                <asp:Button ID="Btnupdate" runat="server" ForeColor="Black" Text="Update" 
                                    onclick="Btnupdate_Click" Visible="False" style="height: 26px" />
                                <asp:Button ID="Btncancel" runat="server" ForeColor="Black" 
                                    onclick="Btncancel_Click" Text="Cancel" />
                            </td>
                            <td style="width: 20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
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
                                        <td>
                                            &nbsp;</td>
                                        <td align="right">
                                            <asp:Button ID="Button1" runat="server" Font-Bold="True" ForeColor="Black" 
                                                onclick="Button1_Click" Text="ADD NEW" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                                CssClass="mGrid" PagerStyle-CssClass="pgr" Width="100%" 
                                                onpageindexchanging="GridView1_PageIndexChanging" 
                                                ondatabound="GridView1_DataBound" onrowdatabound="GridView1_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ProductName" HeaderText="Product  Name" />
                                                    <asp:TemplateField HeaderText="ProductStatus">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ProductStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <%--<EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ProductStatus") %>'></asp:TextBox>
                                                        </EditItemTemplate>--%>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProductStartDate" HeaderText="Start Date" />
                                                    <asp:BoundField DataField="ProductRetiredate" HeaderText="End Date" />
                                                    <asp:BoundField DataField="ProductTypeID" HeaderText="Product Type" 
                                                        Visible="False" />
                                                    <asp:BoundField DataField="SalesTypeID" HeaderText="Sales Type" 
                                                        Visible="False" />
                                                    <asp:BoundField DataField="ProductSubTypeID" HeaderText="Sub Type" 
                                                        Visible="False" />
                                                    <asp:BoundField DataField="ProductSubSubTypeID" HeaderText="Sub-Sub type" 
                                                        Visible="False" />
                                                    <asp:BoundField DataField="ProductStockQty" HeaderText="Quantity" />
                                                    <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" 
                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Edit" ItemStyle-Width="3%">
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
                                                         <asp:Label ID="Label15" runat="server"  Text='<%#Eval("ProductMasterID") %>' Visible="False"></asp:Label>
                                                            <asp:ImageButton ID="imgdelete" runat="server" 
                                                              
                                                                ImageUrl="~/Account/images/delete.gif" ToolTip="Delete" 
                                                                onclick="imgdelete_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
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

