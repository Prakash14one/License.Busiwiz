<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="BackupRule.aspx.cs" Inherits="BackupRule" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <script language="javascript" type="text/javascript">

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
            <div class="products_box">
                <div style="margin-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label10" runat="server" Text="Portal Default Client Data Backup Rule"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    Please set the rule for backing up of client data of your portal.
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Select Product"></asp:Label>
                                    
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlProductname1" Width="300px" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlProductname1_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Select Portal"></asp:Label>
                                    &nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1" ControlToValidate="ddlportal" ErrorMessage="*" InitialValue="0" ></asp:RequiredFieldValidator>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlportal" runat="server">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    A )
                                </label>
                            </td>
                            <td>
                                <label>
                                    How many copies of backup would be maintained ?
                                </label>
                                <label>
                                    <asp:DropDownList ID="DropDownList1" Width="50px" runat="server">
                                        <asp:ListItem Selected="True" Value="1" Text="1"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="(When the number of copies of backup exceeds this limit the oldest backup copy will be deleted and the latest copy would be inserted)"
                                        Font-Size="10px"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    B )
                                </label>
                            </td>
                            <td>
                                <label>
                                    How often you wish to take a backup ?
                                </label>
                                <label>
                                    <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="1" Text="More than once a day"></asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True" Text="Once a day"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Weekly"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Monthly"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <asp:Panel ID="Panel2" Visible="false" runat="server">
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <label>
                                        Select Backup Time for a Day
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="DropDownList3" Width="100px" runat="server">
                                            <asp:ListItem Value="1" Text="1:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="11" Text="11:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="12" Text="12:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="13" Text="1:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="14" Text="2:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="3:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="16" Selected="True" Text="4:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="17" Text="5:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="18" Text="6:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="19" Text="7:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="20" Text="8:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="21" Text="9:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="22" Text="10:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="23" Text="11:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="23:59" Text="12:00 PM"></asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Button1_Click" />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:GridView ID="GridView2" runat="server" AllowPaging="false" AllowSorting="false"
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid"
                                        EmptyDataText="No Record Found." GridLines="Both" PagerStyle-CssClass="pgr" Width="30%"
                                        OnRowCommand="GridView2_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Scheduled Time"
                                                ItemStyle-Width="98%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltime" runat="server" Visible="false" Text='<%#Eval("Time") %>'></asp:Label>
                                                    <asp:Label ID="lbldisplaytime" runat="server" Text='<%#Eval("TimeDisplay") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField ButtonType="Image" CommandName="del" HeaderImageUrl="~/Account/images/delete.gif"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Delete" ImageUrl="~/Account/images/delete.gif"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                <ItemStyle Width="50px" />
                                            </asp:ButtonField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="Panel3" Visible="false" runat="server">
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <label>
                                        Select Scheduled Backup Time
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="DropDownList4" Width="100px" runat="server">
                                            <asp:ListItem Value="1" Text="1:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="11" Text="11:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="12" Text="12:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="13" Text="1:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="14" Text="2:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="3:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="16" Selected="True" Text="4:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="17" Text="5:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="18" Text="6:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="19" Text="7:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="20" Text="8:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="21" Text="9:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="22" Text="10:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="23" Text="11:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="23:59" Text="12:00 PM"></asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="Panel4" Visible="false" runat="server">
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <label>
                                        Select Backup Scheduled Day & Time
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="DropDownList6" Width="120px" runat="server">
                                            <asp:ListItem Selected="True" Value="Monday" Text="Monday"></asp:ListItem>
                                            <asp:ListItem Value="Tuesday" Text="Tuesday"></asp:ListItem>
                                            <asp:ListItem Value="Wednesday" Text="Wednesday"></asp:ListItem>
                                            <asp:ListItem Value="Thursday" Text="Thursday"></asp:ListItem>
                                            <asp:ListItem Value="Friday" Text="Friday"></asp:ListItem>
                                            <asp:ListItem Value="Seturday" Text="Seturday"></asp:ListItem>
                                            <asp:ListItem Value="Sunday" Text="Sunday"></asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="DropDownList5" Width="100px" runat="server">
                                            <asp:ListItem Value="1" Text="1:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="11" Text="11:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="12" Text="12:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="13" Text="1:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="14" Text="2:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="3:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="16" Selected="True" Text="4:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="17" Text="5:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="18" Text="6:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="19" Text="7:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="20" Text="8:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="21" Text="9:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="22" Text="10:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="23" Text="11:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="23:59" Text="12:00 PM"></asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="Panel5" Visible="false" runat="server">
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <label>
                                        Select Backup Scheduled Date & Time
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="DropDownList7" Width="100px" runat="server">
                                            <asp:ListItem Selected="True" Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="DropDownList8" Width="100px" runat="server">
                                            <asp:ListItem Value="1" Text="1:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="11" Text="11:00 AM"></asp:ListItem>
                                            <asp:ListItem Value="12" Text="12:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="13" Text="1:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="14" Text="2:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="3:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="16" Selected="True" Text="4:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="17" Text="5:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="18" Text="6:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="19" Text="7:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="20" Text="8:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="21" Text="9:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="22" Text="10:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="23" Text="11:00 PM"></asp:ListItem>
                                            <asp:ListItem Value="23:59" Text="12:00 PM"></asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                                <label>
                                    C )
                                </label>
                            </td>
                            <td>
                                <label>
                                    Notify Company Admin when backup is not successful
                                </label>
                                <asp:CheckBox ID="CheckBox1" AutoPostBack="true" Checked="true" Text="" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            </td>
                        </tr>

                          
                        <asp:Panel ID="Panel1" runat="server" Visible="false" Width="100%">
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox2" Enabled="false" Checked="true" runat="server" />
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text=" Admin"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                        </asp:Panel>

                        <tr>
                            <td colspan="2">
                                        <table>
                                        <tr>
                                        <td>
                                         <label>
                                            <asp:Label ID="Label8" runat="server" Text="D )"  />
                                         </label>
                                         <label style="width:700px;">
                                         <asp:RadioButtonList ID="rbfolder" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"  OnSelectedIndexChanged="rbfolder_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Selected="True"> Backup all website code folders</asp:ListItem>
                                            <asp:ListItem Value="1"> Backup only the selected folders of the website code</asp:ListItem>
                                        </asp:RadioButtonList>
                                         </label> 

                                        </td>
                        </tr>
                <asp:Panel ID="pnlatach" runat="server" Width="100%" Visible="false">
                        <tr>                        
                               <td colspan="2">                               
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="Default Category Type"></asp:Label>
                                        <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList1"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlcodecate" runat="server" >
                                        </asp:DropDownList>
                                    </label>
                                
                                
                         <label style="width:200px;">
                          <asp:Label ID="Label9" runat="server" Text="Main Folder"  />
                               <asp:DropDownList ID="ddl_MainFolder" runat="server" ValidationGroup="1" Width="180px" AutoPostBack="true"  OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChangedMainFolder" >
                                                        </asp:DropDownList>
                         </label> 
                        <label style="width:200px;">
                                 <asp:Label ID="Label42" runat="server" Text="Sub Folder"></asp:Label>
                                    <asp:DropDownList ID="ddl_subfolder" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChangedsubFolder" Width="180px">
                                 </asp:DropDownList>
                         </label>
                        <label style="width:200px;">
                                 <asp:Label ID="Label11" runat="server" Text="Sub Sub Folder"></asp:Label>
                                    <asp:DropDownList ID="ddl_SubSubfolder" runat="server" AutoPostBack="false"  Width="180px">
                                 </asp:DropDownList>
                         </label>
                            
                         <label style="width:50px;"><br />
                           <asp:Button ID="Button2N" CssClass="btnSubmit" runat="server" Text="Add" OnClick="Button2_ClickAdd"   />                           
                          </label> 
                          <label>
                          <asp:Label Visible="false" class="lblInfoMsg"  ID="lblempmsg" runat="server" Text="Folder alredy Added "></asp:Label>
                          </label> 
                         </td>
                        </tr>
                        <tr>
                         <td colspan="2">                   
                              <asp:GridView ID="gridFileAttach" runat="server"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid"  GridLines="Both" OnRowCommand="gridFileAttach_RowCommand" 
                           PagerStyle-CssClass="pgr" Width="100%">
                           <Columns>                              
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30%"  HeaderText="Code Category">
                                   <ItemTemplate>
                                       <asp:Label ID="lblProductCodeDetailid" runat="server" Text='<%#Bind("ProductCodeDetailid") %>' Visible="false" ></asp:Label>
                                       <asp:Label ID="lblCodeTypeName" runat="server" Text='<%#Bind("CodeTypeName") %>'  ></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30%"  HeaderText="FolderCatName">
                                   <ItemTemplate>
                                       <asp:Label ID="lblFolderID" runat="server" Text='<%#Bind("FolderID") %>' Visible="false" ></asp:Label>
                                       <asp:Label ID="lblFolderCatName" runat="server" Text='<%#Bind("FolderCatName") %>'  ></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30%"  HeaderText="FolderSubName">
                                   <ItemTemplate>
                                       <asp:Label ID="lblSubfolderid" runat="server" Text='<%#Bind("Subfolderid") %>' Visible="false" ></asp:Label>
                                       <asp:Label ID="lblFolderSubName" runat="server" Text='<%#Bind("FolderSubName") %>'></asp:Label>                                       
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30%"  HeaderText="FolderSubName">
                                   <ItemTemplate>
                                       <asp:Label ID="lblSubsubfolderid" runat="server" Text='<%#Bind("Subsubfolderid") %>' Visible="false" ></asp:Label>
                                       <asp:Label ID="lblFolderSubSubName" runat="server" Text='<%#Bind("FolderSubSubName") %>'></asp:Label>
                                        <asp:Label ID="lbllastfolder" runat="server" Text='<%#Bind("FolderName") %>' Visible="false" ></asp:Label>
                                       
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:ButtonField CommandName="Delete1" HeaderStyle-HorizontalAlign="Left" 
                                   HeaderText="Remove" ImageUrl="~/Account/images/delete.gif" 
                                   ItemStyle-ForeColor="Black" Text="Remove" />
                           </Columns>
                       </asp:GridView>                   
                         </td>
                      </tr>
                </asp:Panel>
                    </table>
                            </td>
                            </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="Submit" ValidationGroup="1" OnClick="Button2_Click" />
                                <asp:Button ID="Button5" Visible="false" runat="server" Text="Update" ValidationGroup="1"
                                    OnClick="Button5_Click" />
                                <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label5" runat="server" Text="List of Portal Default Client Data Backup Rules"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                    OnClick="Button3_Click" />
                                <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    style="width: 51px;" type="button" value="Print" visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Portal Default Client Data Backup Rules"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="Id"
                                                    EmptyDataText="No Record Found." AllowSorting="false" OnRowCommand="GridView1_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblproductname" runat="server" Text='<%#Bind("ProductName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Portal Name" SortExpression="PortalName" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblportalname" runat="server" Text='<%#Bind("PortalName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No of Copies" SortExpression="NoofCoppies" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnoofcopies" runat="server" Text='<%#Bind("NoofCoppies") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Frequecy Type of Backup" SortExpression="FrequecyTypeForBackup"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfrequencytype" runat="server" Visible="false" Text='<%#Bind("FrequecyTypeForBackup") %>'></asp:Label>
                                                                <asp:Label ID="lblfrequencytypedisplay" runat="server" Text='<%#Bind("FrequecyTypeForBackupDisplay") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Notification Email" SortExpression="NotificationEmail"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnotificationemail" Visible="false" runat="server" Text='<%#Bind("NotificationEmail") %>'></asp:Label>
                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("NotificationEmaildisplay") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Edit" ImageUrl="~/Account/images/edit.gif"
                                                            Text="Button" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:ButtonField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinkbb" ToolTip="Delete" runat="server" CommandName="del1"
                                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                    CommandArgument='<%# Eval("Id") %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </td>
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
