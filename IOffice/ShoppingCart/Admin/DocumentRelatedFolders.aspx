<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentRelatedFolders.aspx.cs" Inherits="Account_DocumentRelatedFolders"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upn" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Panel ID="pnlmsg" runat="server" Width="100%" Visible="False">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                </asp:Panel>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <table width="100%">
                    <tr>
                        <td colspan="4">
                            <label>
                                <asp:Label ID="Label1" runat="server" Text="Folder Name"></asp:Label>
                            </label>
                            <label>
                                <asp:DropDownList ID="ddlfolder" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfolder_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                            <input runat="Server" id="hdnsortExp" name="hdnsortExp" type="hidden" style="width: 1px" />
                            <input runat="Server" name="hdnsortDir" id="hdnsortDir" type="hidden" style="width: 1px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:DataList ID="DataList1" runat="server" OnItemCommand="DataList1_ItemCommand"
                                DataKeyField="FolderID" Width="100%">
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Account/images/closeFolder.jpg" />
                                                
                                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("FolderName") %>'
                                                    Font-Bold="True" ForeColor="#996600"></asp:LinkButton>
                                                    
                                                &nbsp; <strong><span style="color: Olive">Created By:</span></strong>
                                                <asp:Label ID="lbluser"
                                                    runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                                    
                                                &nbsp; <strong><span style="color: Olive">Date:</span></strong><asp:Label ID="lblDate"
                                                    runat="server" Text='<%# Eval("CreatedDate","{0:MM/dd/yyyy-HH:mm}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="Gridreqinfo" runat="server" AllowPaging="True" AllowSorting="false"
                                                    AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    DataKeyNames="DocumentTypeId" OnPageIndexChanging="Gridreqinfo_PageIndexChanging"
                                                    OnRowCommand="Gridreqinfo_RowCommand" PageSize="20" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="DocumentId" HeaderText="Document ID" ItemStyle-Width="10%"
                                                             HeaderStyle-HorizontalAlign="Left">
                                                            </asp:BoundField>
                                                            
                                                        <asp:BoundField DataField="DocumentUploadDate" ItemStyle-Width="10%" DataFormatString="{0:dd-MM-yyyy}"
                                                            HeaderText="Date" SortExpression="DocumentUploadDate" HeaderStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                        
                                                        <asp:BoundField DataField="DocumentType" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Folder Name" SortExpression="DocumentType"></asp:BoundField>
                                                            
                                                        <asp:BoundField DataField="DocumentTitle" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Document Title" SortExpression="DocumentTitle"></asp:BoundField>
                                                        <asp:BoundField DataField="PartyName" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="User Name" SortExpression="PartyName"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="View" ShowHeader="False" HeaderImageUrl="~/images/view.png"
                                                            ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <a href="javascript:void(0)" style="color: Black" onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&Did=<%= DesignationId %>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')">
                                                                    <asp:ImageButton ID="ImageButton221" ToolTip="View Document" runat="server" ImageUrl="~/images/view.png" />
                                                                </a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Accept/Reject" Visible="False">
                                                            <ItemTemplate>
                                                                &nbsp;<asp:RadioButtonList ID="rbtnAcceptReject" runat="server" RepeatDirection="Horizontal"
                                                                    Width="75px">
                                                                    <asp:ListItem Selected="True" Value="None">None</asp:ListItem>
                                                                    <asp:ListItem Value="True">Accept</asp:ListItem>
                                                                    <asp:ListItem Value="False">Reject</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approve Type" Visible="False">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                                                    SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="TextBox1"
                                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlApprovetype" runat="server">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Note" Visible="False">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="REG2" runat="server" ErrorMessage="*" Display="Dynamic"
                                                                    SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="TextBox2"
                                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="REG3" runat="server" ErrorMessage="*" Display="Dynamic"
                                                                    SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="TextBox3"
                                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete From Folder" ItemStyle-Width="2%" ShowHeader="False"
                                                            HeaderImageUrl="~/ShoppingCart/images/trash.jpg" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="Delete From Folder" CausesValidation="false"
                                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem, "RelationID")%>' CommandName="delete1"
                                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DataList1" EventName="ItemCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
