<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="MainFolderMaster.aspx.cs" Inherits="MainFolderMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- <div id="right_content">
                <div id="ctl00_ucTitle_PNLTITLE">
                    <div class="divHeaderLeft">
                        <div style="float: left; width: 50%;">
                            <h2>
                                <span id="ctl00_ucTitle_lbltitle">Add Monthly Goal</span>
                            </h2>
                        </div>
                        <div style="float: right; width: 50%">
                            <div id="ctl00_ucTitle_pnlshow">
                            </div>
                        </div>
                    </div>
                </div>
                <div style="clear: both;">
                </div>
                <div id="ctl00_ucTitle_pnlhelp" style="border: solid 1px black">
                    <h3>
                        <span id="ctl00_ucTitle_lblDetail" style="font-family: Tahoma; font-size: 7pt; font-weight: bold;">
                            Add Monthly Goal</span>
                    </h3>
                </div>
            </div>--%>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend id="5">
                        <asp:Label ID="Label19" runat="server" Text=""></asp:Label>Add/Manage Main Folder</legend>
                   <div style="float: right;">
                        <asp:Button ID="addnewpanel" runat="server" Text="Add Folder" CssClass="btnSubmit"
                            OnClick="addnewpanel_Click" Height="25px" Width="100px" />
                        <br />
                        <br />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlmonthgoal" runat="server" Width="100%" Visible="false">
                        <table id="pagetbl" width="100%">
                            <tr>
                                <td width="25%">
                                   
                                    Main Folder
                                    
                                </td>
                                <td width="25%">
                                  
                                        

                                              

<asp:TextBox ID="txtmainfolder" runat="server" ontextchanged="txtmainfolder_TextChanged" Width="150px" 
                                        Height="25px"></asp:TextBox>

                                   
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    
                                       Product/Version
                                   
                                </td>
                                <td width="25%">
                                    <asp:Label ID="Labelproductversion" runat="server" Text=""></asp:Label>
<asp:DropDownList ID="Drproductversion" runat="server" Width="150px" Height="25px"></asp:DropDownList>


                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    
                                    Status
                                    
                                </td>
                                <td colspan="3" width="25%">
                                    <asp:Label ID="Labelststus1" runat="server" Text=""></asp:Label>
                                    <asp:DropDownList ID="ddlstatus" runat="server" Width="150px" Height="25px">
                                            <asp:ListItem>Active</asp:ListItem>
                                            <asp:ListItem>Inactive</asp:ListItem>
                                        </asp:DropDownList>

                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <%--  <tr>
                                <td colspan="6">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="If you Add Monthly Goal Description select Chechbox ?">
                                        </asp:Label>
                                    </label>
                                   <asp:CheckBox ID="ChkDesc"  AutoPostBack="true" runat="server" 
                                        oncheckedchanged="ChkDesc_CheckedChanged" />
                                   </td>
                            </tr>
                            <tr>
                            <td></td>
                              <td colspan="6">
                                <label>
                                     <asp:TextBox ID="txtDescription" runat="server" Height="75px" TextMode="MultiLine"
                                            Visible="false" Width="369"></asp:TextBox>
                                    </label>
                                    </td>
                            </tr>--%>
                            <tr>
                                <td width="25%">
                                    
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                </td>
                                <td width="25%">

                                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Submit " 
                                        Height="25px" Width="60px" />
                                                                        <asp:Button ID="Button5" runat="server" 
    CssClass="btnSubmit" Text="Update" Visible="false"
                                        ValidationGroup="1" 
    OnClick="Button5_Click" Height="25px" Width="60px" />
                                    

                                    <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btnSubmit" 
                                        OnClick="Button2_Click" Height="25px" Width="60px" />
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label30" runat="server" Text="List of Main Folders"></asp:Label>
                    </legend>


                    &nbsp;<div style="clear: both;">
                    </div>
                 
                    <div style="clear: both;">
                    </div>
                    <label>
                         <asp:Label ID="Label3" runat="server" Text="Filter by Product/Version"></asp:Label>
                      <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                                    Height="25px" onselectedindexchanged="DropDownList2_SelectedIndexChanged" 
                                    Width="150px">
                                </asp:DropDownList>

                    </label>
                    <label>
                  <asp:Label ID="Label4" runat="server" Text="Filter by Status"></asp:Label>
                   

                    <asp:DropDownList ID="Drepaqctive" runat="server" AutoPostBack="True" 
                                    Height="25px" onselectedindexchanged="Drepaqctive_SelectedIndexChanged" 
                                    Width="150px">
                                    <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                </asp:DropDownList>

                    </label>
                    <label>
                        
                     
                    </label>
                    <div style="clear: both;">
                    </div>





                            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <%--    <div id="mydiv" class="closed">
                                                    <table width="850Px">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                               
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of MonthlyGoal"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvEmployeeDetails" runat="server" AutoGenerateColumns="False" 
                                                CssClass="mGrid" DataKeyNames="FolderMasterId" 
                                                onrowcancelingedit="gvEmployeeDetails_RowCancelingEdit" 
                                                onrowcommand="gvEmployeeDetails_RowCommand" 
                                                onrowdatabound="gvEmployeeDetails_RowDataBound" 
                                                onrowediting="gvEmployeeDetails_RowEditing" 
                                                onrowupdating="gvEmployeeDetails_RowUpdating" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label31" runat="server" Text='<%#Eval("FolderMasterId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpID" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container.DataItem, " FolderMasterId") %>' 
                                                                Visible="False"></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblEditEmpID" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "FolderMasterId") %>' 
                                                                Visible="False"></asp:Label>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product/Version">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesignation" runat="server" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "productversion") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Main Folder Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "FolderCatName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCity" runat="server" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "Activestatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnEdit" runat="server" 
                                                                CommandArgument='<%# Eval("FolderMasterId") %>' CommandName="Edit" 
                                                                Height="20px" ImageUrl="~/Images/edit.gif" onclick="imgbtnEdit_Click" 
                                                                Width="20px" />
                                                            <asp:ImageButton ID="imgbtnDelete" runat="server" 
                                                                CommandArgument='<%# Eval("FolderMasterId") %>' CommandName="Delete" 
                                                                Height="20px" ImageUrl="~/images/delete.gif" onclick="imgbtnDelete_Click" 
                                                                Width="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    </tr>
                                </table>
                    </asp:Panel>
                    </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

