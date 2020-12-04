<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="tablefielddetail.aspx.cs" Inherits="tablefielddetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="pnl" runat="server">
        <ContentTemplate>

            <table style="width: 100%">
                <tr>
                    <td width="25%" colspan="2" style="width: 50%">
                        <asp:Label ID="Label360" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Panel ID="Panel9" runat="server"  GroupingText="Table Field List" Visible="false">
                                            <table class="style1" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel10" runat="server" Visible="false" 
                                                            GroupingText="Add Table Field Name">

                                                            

                                                            <table class="style1" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="gvaddnew" runat="server" AutoGenerateColumns="False" 
                                                                CssClass="mGrid" Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Field Name">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TextBox2" runat="server" Width="60px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlfiledtype" runat="server" Width="100px">
                                                                                <asp:ListItem Selected="True" Text="int" Value="int"></asp:ListItem>
                                                                                <asp:ListItem Text="bigint" Value="bigint"></asp:ListItem>
                                                                                <asp:ListItem Text="nvarchar" Value="nvarchar"></asp:ListItem>
                                                                                <asp:ListItem Text="binary" Value="binary"></asp:ListItem>
                                                                                <asp:ListItem Text="bit" Value="bit"></asp:ListItem>
                                                                                <asp:ListItem Text="char" Value="char"></asp:ListItem>
                                                                                <asp:ListItem Text="date" Value="date"></asp:ListItem>
                                                                                <asp:ListItem Text="datetime" Value="datetime"></asp:ListItem>
                                                                                <asp:ListItem Text="datetime2(7)" Value="datetime2(7)"></asp:ListItem>
                                                                                <asp:ListItem Text="datetimeoffset(7)" Value="datetimeoffset(7)"></asp:ListItem>
                                                                                <asp:ListItem Text="decimal(18, 0)" Value="decimal(18, 0)"></asp:ListItem>
                                                                                <asp:ListItem Text="float" Value="float"></asp:ListItem>
                                                                                <asp:ListItem Text="geography" Value="geography"></asp:ListItem>
                                                                                <asp:ListItem Text="geometry" Value="geometry"></asp:ListItem>
                                                                                <asp:ListItem Text="float" Value="float"></asp:ListItem>
                                                                                <asp:ListItem Text="image" Value="image"></asp:ListItem>
                                                                                <asp:ListItem Text="numeric(18, 0)" Value="numeric(18, 0)"></asp:ListItem>
                                                                                <asp:ListItem Text="real" Value="real"></asp:ListItem>
                                                                                <asp:ListItem Text="time(7)" Value="time(7)"></asp:ListItem>
                                                                                <asp:ListItem Text="real" Value="real"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Size">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TextBox21" runat="server" Width="60px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Primary Key">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CheckBox4" runat="server" 
                                                                                oncheckedchanged="CheckBox4_CheckedChanged" AutoPostBack="True" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Foreign Key">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CheckBox5" runat="server" 
                                                                                oncheckedchanged="CheckBox5_CheckedChanged" AutoPostBack="True" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText=" Foreign Key Table Name">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="DropDownList8" runat="server" Width="100px" 
                                                                                AutoPostBack="True" onselectedindexchanged="DropDownList8_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Foreign key Field Name">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="DropDownList9" runat="server" Width="100px">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Null Value  Allowed">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CheckBox6" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="Button2" runat="server" Text="Add" onclick="Button2_Click" /></td>
                                                                </tr>
                                                            </table>

                                                            

                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Button ID="Button16" runat="server" Text="Add New" 
                                                            onclick="Button16_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" 
                                                            CssClass="mGrid" onrowcommand="GridView8_RowCommand" Width="100%" 
                                                            onrowdeleting="GridView8_RowDeleting" onrowediting="GridView8_RowEditing" 
                                                            onrowupdating="GridView8_RowUpdating">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Field  Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label25" runat="server" Text='<%#Eval("feildname")%>'></asp:Label> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label27" runat="server" Text='<%#Eval("type")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Size">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label28" runat="server" Text='<%#Eval("size")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Primary Key">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label29" runat="server" Text='<%#Eval("primarykey")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Foreign Key">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label31" runat="server" Text='<%#Eval("foreignkey")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText=" Foreign Key Table Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label32" runat="server" Text='<%#Eval("keytable")%>'></asp:Label>
                                                                       <asp:Label ID="Label351" runat="server" Text='<%#Eval("keytableid")%>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Foreign key Field Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label33" runat="server" Text='<%#Eval("keyfeild")%>'></asp:Label>
                                                                         <asp:Label ID="Label354" runat="server" Text='<%#Eval("keyfeildid")%>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Null Value Allowed">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label34" runat="server" Text='<%#Eval("nullvalue")%>'></asp:Label> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField ButtonType="Image" CommandName="del" 
                                                                                                            
                                                                    HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left" 
                                                                                                            HeaderText="Delete" ImageUrl="~/Account/images/delete.gif" 
                                                                                                            ItemStyle-HorizontalAlign="Left" 
                                                                    ItemStyle-Width="2%" >
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                </asp:ButtonField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="Button17" runat="server" onclick="Button17_Click" 
                                                            Text="Submit" Height="26px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Panel ID="Panel2" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="Button18" runat="server" onclick="Button18_Click" 
                                            Text="Synchronise Now" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                        Filter By Product/Version
                                        </label>
                                        <label>
                                        <asp:DropDownList ID="ddlpversion" runat="server" AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddlpversion_SelectedIndexChanged1">
                                        </asp:DropDownList>
                                        </label>
                                        <label>
                                        Search
                                        </label>
                                        <label>
                                        <asp:TextBox ID="TextBox1" runat="server" Width="180px"></asp:TextBox>
                                        </label>
                                        <label>
                                        <asp:Button ID="Button15" runat="server" onclick="Button15_Click" Text="Go" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" Width="100%">
                                        
                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid" 
                                            DataKeyNames="Id" EmptyDataText="No Record Found." 
                                            EnableSortingAndPagingCallbacks="True" 
                                            OnPageIndexChanging="GridView1_PageIndexChanging" 
                                            
                                            PagerStyle-CssClass="pgr" Width="100%" 
                                            onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
                                            onrowupdating="GridView1_RowUpdating" onrowcommand="GridView1_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Table Name" 
                                                    ItemStyle-Width="15%" SortExpression="Table Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltname" runat="server" Text='<%# Bind("TableName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="15%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Table Title" 
                                                    ItemStyle-Width="15%" SortExpression="TableTitle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemaildisplaynameName" runat="server" 
                                                            Text='<%# Bind("TableTitle")%>'></asp:Label>
                                                             <asp:Label ID="Label11" runat="server" 
                                                            Text='<%# Bind("Id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="15%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Field  Name" 
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label355" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" 
                                                    HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" HeaderText="Edit" 
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnedit" runat="server" 
                                                            CommandArgument='<%# Eval("Id") %>' CommandName="Edit" 
                                                            ImageUrl="~/Account/images/edit.gif" ToolTip="Edit" 
                                                            onclick="imgbtnedit_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderImageUrl="~/Account/images/trash.jpg" 
                                                    HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" HeaderText="Delete" 
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtndelete" runat="server" 
                                                            CommandArgument='<%# Eval("Id") %>' CommandName="Delete" 
                                                            ImageUrl="~/Account/images/delete.gif" 
                                                            OnClientClick="return confirm('Do you wish to delete this record?');" 
                                                            ToolTip="Delete" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                            <asp:Panel ID="Panel3" runat="server" Width="65%" CssClass="modalPopup" Height="500px" ScrollBars="Vertical">
                                    <fieldset>
                                        <legend>
                                           
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td width="50%">
                                                </td>
                                                <td align="right" width="50%">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" colspan="2" style="width: 100%" >
                                                    <asp:Label ID="Label356" runat="server" ForeColor="Black" 
                                                        
                                                        Text="The Fields  details do not match  with the actual Fields in the database "></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td width="50%" >
                                                    <asp:Label ID="Label357" runat="server" Font-Bold="True" ForeColor="Black" 
                                                        Text="Actual database details"></asp:Label>
                                                </td>
                                                    <td width="50%" >
                                                        &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="width: 100%" >
                                                    <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" 
                                                        CssClass="mGrid">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Field Name"  DataField="COLUMN_NAME"/>
                                                            <asp:BoundField HeaderText="Data Type" DataField="DATA_TYPE" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td width="50%" >
                                                    <asp:Label ID="Label358" runat="server" Font-Bold="True" ForeColor="Black" 
                                                        Text="Proposed database details"></asp:Label>
                                                </td>
                                                    <td width="50%" >
                                                        &nbsp;</td>
                                            </tr>
                                            <tr>
                                               <td  colspan="2" style="width: 100%" >
                                                    <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" 
                                                        CssClass="mGrid">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Field Name" DataField="feildname"/>
                                                            <asp:BoundField HeaderText="Data Type" DataField="fieldtype" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100%" colspan="2">
                                                    <asp:Label ID="Label359" runat="server" ForeColor="Black" 
                                                        Text="Would you like to update the actual table ?"></asp:Label>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" align="right" >
                                                    <asp:Button ID="Button1" runat="server" Text="Yes" onclick="Button1_Click" /></td>
                                                    <td width="50%" align="left" >
                                                    <asp:Button ID="Button3" runat="server" Text="No" /></td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel3" TargetControlID="Button5" CancelControlID="ImageButton1">
                                </cc1:ModalPopupExtender></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Panel ID="Paneldoc" runat="server" Width="55%" CssClass="modalPopup">
                                <fieldset>
                                    <legend>
                                        <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                                    </legend>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 95%;">
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="16px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    <asp:Label ID="Label20" runat="server" Text="Was this the last record you are going to add right now to this table?"></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButtonList ID="rdsync" runat="server">
                                                                <asp:ListItem Value="1" Text="Yes, this is the last record in the series of records I am inserting/editing to this table right now"></asp:ListItem>
                                                                <asp:ListItem Value="0" Text="No, I am still going to add/edit records to this table right now"
                                                                    Selected="True"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnok" runat="server" CssClass="btnSubmit" Text="OK" OnClick="btndosyncro_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset></asp:Panel>
                            <asp:Button ID="btnreh" runat="server" Style="display: none" />
                            <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground"
                                PopupControlID="Paneldoc" TargetControlID="btnreh" CancelControlID="ImageButton10">
                            </cc1:ModalPopupExtender></td>
                </tr>
                <tr>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
            </table>

        </ContentTemplate></asp:UpdatePanel>
</asp:Content>

