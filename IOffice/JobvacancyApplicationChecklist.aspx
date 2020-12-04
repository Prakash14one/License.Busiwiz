<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" AutoEventWireup="true" CodeFile="JobvacancyApplicationChecklist.aspx.cs" Inherits="ShoppingCart_Admin_JobvacancyApplicationChecklist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        
    
        <table style="width: 100%">
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
                   <asp:Panel ID="Panel5" runat="server" 
                        
                        GroupingText="Set Screening Questions" 
                        Visible="False">
            <table style="width: 100%">
                <tr>
                    <td class="cssLabelCompany_Information" style="width: 319px">
                        &nbsp;</td>
                    <td style="width: 24%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="cssLabelCompany_Information" style="width: 319px">
                        <asp:Label ID="Label6" runat="server" Text="Vacancy Title"></asp:Label>
                    </td>
                    <td style="width: 24%">
                        <asp:Label ID="Label7" runat="server"></asp:Label>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="cssLabelCompany_Information" style="width: 319px">
                        <asp:Label ID="Label8" runat="server" Text="Question Number"></asp:Label>
                    </td>
                    <td style="width: 24%">
                        <asp:Label ID="Label9" runat="server"></asp:Label>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="cssLabelCompany_Information" style="width: 319px">
                        <asp:Label ID="Label10" runat="server" Text="Question"></asp:Label>
                    </td>
                    <td style="width: 24%">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="cssLabelCompany_Information" style="width: 319px">
                        <asp:Label ID="Label11" runat="server" Text="Desired Answer"></asp:Label>
                    </td>
                    <td style="width: 24%">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="cssLabelCompany_Information" style="width: 319px">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td style="width: 24%">
                        <asp:Button ID="Button8" runat="server" onclick="Button8_Click" Text="Submit" />
                        <asp:Button ID="Button11" runat="server" onclick="Button11_Click" Text="Update" 
                            Visible="False" />
                        <asp:Button ID="Button10" runat="server" onclick="Button10_Click" 
                            Text="Cancel" />
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="cssLabelCompany_Information" style="width: 319px">
                        &nbsp;</td>
                    <td style="width: 24%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
            </table>
        </asp:Panel></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Panel ID="Panel6" runat="server" 
                        GroupingText="List of Screening Questions">
                        <table style="width: 100%">
                            <tr>
                                <td width="25%">
                                    <asp:Label ID="Label12" runat="server" Text="Select Vacancy Title"></asp:Label>
                                </td>
                                <td width="25%">
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="DropDownList1_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%" align="right">
                                    <asp:Button ID="Button9" runat="server" onclick="Button9_Click" 
                                        Text="Set Question" Visible="true" />
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td align="right" width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                                    CssClass="mGrid" onrowcommand="GridView2_RowCommand" 
                                                    onrowdatabound="GridView2_RowDataBound" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="QuestionNumber" HeaderText="Question Number">
                                                        <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="QuestionText" HeaderText="Question">
                                                        <ItemStyle Width="40%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CorrectAnswer" HeaderText="Desired Answer">
                                                        <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" runat="server" 
                                                                    ImageUrl="~/Account/images/edit.gif" onclick="ImageButton48_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton49" runat="server" 
                                                                    ImageUrl="~/Account/images/delete.gif" onclick="ImageButton49_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 29px">
                                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="CheckBox1_CheckedChanged" Text="Show All" Visible="False" />
                                                </td>
                                            <td style="height: 29px">
                                                </td>
                                            <td style="height: 29px">
                                                </td>
                                        </tr>
                                        <tr>
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
                        </table>
                    </asp:Panel>
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
        </table>
    
        
    
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

