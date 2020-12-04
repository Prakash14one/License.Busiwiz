<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Productcodedetail.aspx.cs" Inherits="CodeType" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
           
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }


        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59) {


                alert("You have entered an invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered an invalid character");
            }

            counter = document.getElementById(id);

            if (txt1.value.length <= max_len) {
                remaining_characters = max_len - txt1.value.length;
                counter.innerHTML = remaining_characters;
            }
        }

        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        } 

    </script>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="margin-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="true"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Code Type Name"></asp:Label>
                    </legend>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Select Products Version"></asp:Label>
                                        <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlproductversion"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlproductversion" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlproductversion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <%--<tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Category Type "></asp:Label>
                                        <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlproductversion"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlcodetypecategory" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="Default Category Type"></asp:Label>
                                        <asp:Label ID="Label7" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList1"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>--%>
                            
                            
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Code Type Name"></asp:Label>
                                        <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlproductversion"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtcodetypename" runat="server"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>

                             <asp:Panel ID="Panel1" runat="server" Width="100%" Visible="false">
                            <tr>
                            <td colspan="2">
                                        <table>
                                        <tr>
                                        <td colspan="2">
                                         <label style="width:500px;">
                                            <asp:Label ID="Label3" runat="server" Text="List out Folder name that you want to backup"  />
                                         </label>
                                        </td>
                                        </tr>
                                <tr>
                        
                               <td colspan="2">                               
                        
                         <label style="width:200px;">
                          <asp:Label ID="Label2" runat="server" Text="Main Folder"  />
                               <asp:DropDownList ID="ddl_MainFolder" runat="server" ValidationGroup="1" Width="180px" AutoPostBack="true" OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChangedMainFolder" >
                                                        </asp:DropDownList>
                         </label> 
                        <label style="width:200px;">
                                 <asp:Label ID="Label42" runat="server" Text="Sub Folder"></asp:Label>
                                    <asp:DropDownList ID="ddl_subfolder" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChangedsubFolder" Width="180px">
                                 </asp:DropDownList>
                         </label>
                        <label style="width:200px;">
                                 <asp:Label ID="Label9" runat="server" Text="Sub Sub Folder"></asp:Label>
                                    <asp:DropDownList ID="ddl_SubSubfolder" runat="server" AutoPostBack="false"  Width="180px">
                                 </asp:DropDownList>
                         </label>
                            
                         <label style="width:50px;"><br />
                           <asp:Button ID="Button2N" CssClass="btnSubmit" runat="server" Text="Add" OnClick="Button2_Click"   />                           
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

                                </table>
                            </td>
                            </tr>
                            </asp:Panel>

                              <tr>
                                <td style="width: 30%">
                                   
                                </td>
                                <td style="width: 70%">
                                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit"
                                        ValidationGroup="1" Text="Submit" onclick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                 <fieldset>
                    <legend>
                        <asp:Label ID="Label10" runat="server" Text="List Of code Type Name"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right">
                              <%--  <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                    OnClick="Button1_Click1" />
                                <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    style="width: 51px;" type="button" value="Print" visible="false" />--%>
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
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" Text="List of Code Type" ForeColor="Black"
                                                                    Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>
                                                  <label style="width:180px;">                                   
                                                  <asp:Label ID="Label8" runat="server" Text="Filter by Product Name"></asp:Label> </label>
                                                 </label>
                                    
                                                 <label>
                                                  <asp:DropDownList ID="ddlProductname" runat="server" DataTextField="ProductName" DataValueField="ProductId" AutoPostBack="True" OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                 </label> 
                                
                                                 <label  style="width:220px;">
                                                  <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Checked="True" oncheckedchanged="CheckBox1_CheckedChanged" Text="Show only active " style="width:100%;"  />                                       
                                                </label>
                                                  <label style="width:100px;">
                                               
                                        </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                              OnRowEditing="GridView1_RowEditing1"   OnPageIndexChanging="GridView1_PageIndexChanging"   PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="Id"
                                                OnRowDeleting="GridView1_RowDeleting"   OnRowCommand="GridView1_RowCommand"   EmptyDataText="No Record Found." AllowSorting="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="CodeTypeName" SortExpression="CodeTypeName"  HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpversion" runat="server" Text='<%#Bind("CodeTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="AdditionalPageInserted" SortExpression="AdditionalPageInserted" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="20%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcodetypecategory" runat="server" Text='<%#Bind("AdditionalPageInserted") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                         <asp:TemplateField HeaderText="BusiwizSynchronization" SortExpression="BusiwizSynchronization" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="20%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldefaultcodename" runat="server" Text='<%#Bind("BusiwizSynchronization") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="CompanyDefaultData" SortExpression="CompanyDefaultData" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="30%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcodetype" runat="server" Text='<%#Bind("CompanyDefaultData") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnedit" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                        ToolTip="Edit" CommandName="edit" ImageUrl="~/Account/images/edit.gif" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="0.5%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <%--     <asp:ButtonField CommandName="delete" Text="Delete" HeaderText="Delete" HeaderStyle-HorizontalAlign="Left"
                                        HeaderImageUrl="~/ShoppingCart/images/trash.jpg" ImageUrl="~/images/delete.gif"
                                        ButtonType="Image" ItemStyle-Width="3%">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="3%" />
                                    </asp:ButtonField>--%>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtndelete" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                        ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="0.5%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
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
