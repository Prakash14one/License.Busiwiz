<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Pagesearch.aspx.cs" Inherits="MainMenuMaster" Title="Product Main Menu-Add,Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        .tooltip {
                    position: relative;
                    display: inline-block;
                    border-bottom: 1px dotted black; /* If you want dots under the hoverable text */
                }

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
                <div style="margin-left:1%">
                     
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
             <fieldset>
              <%--  <legend>
                    <asp:Label ID="Label12" runat="server" Text="Pages Search"></asp:Label>
                </legend>--%>
                <table width="100%">
                    <tr>
                        <td align="left">
                          <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                     <asp:Panel ID="Panel3" runat="server" Visible="false">
                        <tr>
                   <td>
                    <label style="width:600px">
                                <asp:Label ID="Label13" runat="server" Text="Filter by Product: "></asp:Label>
                                 <asp:DropDownList ID="FilterProduct" Width="600px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FilterProduct_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                            <label>
                            <asp:CheckBox ID="chk_activefilter" runat="server" AutoPostBack="false" Text="Show Only Active Filters" OnCheckedChanged="chkupload_CheckedChanged1" Checked="true">
                                </asp:CheckBox>
                            </label> 
                   </td>
                   </tr>                     
                        <tr>
                        <td>
                           
                            <label style="width:200px;">
                                <asp:Label ID="Label38" runat="server" Text="Menu Category: "></asp:Label>
                                  <asp:DropDownList ID="DDLCategoryS" Width="200px"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="FilterCategorysearch_SelectedIndexChanged">
                                        </asp:DropDownList>
                            </label>  
                             <label style="width:200px;">
                                <asp:Label ID="Label14" runat="server" Text="Main Menu: "></asp:Label>
                           
                                <asp:DropDownList ID="FilterMenu" runat="server" Width="200px" AutoPostBack="true" CausesValidation="True" OnSelectedIndexChanged="FilterMenu_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label> 
                              <label style="width:200px;">
                                <asp:Label ID="Label15" runat="server" Text="Sub Menu: "></asp:Label>
                                  <asp:DropDownList ID="FilterSubMenu" runat="server" Width="200px" CausesValidation="True">
                                </asp:DropDownList>
                            </label> 
                             <label style="width:200px;">
                            <asp:Label ID="Label13Fun" runat="server" Text="Functionality Title: "></asp:Label>
                             <asp:DropDownList ID="ddlfuncti" runat="server" AutoPostBack="True"  Width="200px" onselectedindexchanged="ddlfuncti_SelectedIndexChanged">
                            </asp:DropDownList>
                             </label>   
                             <label style="width:80px;">
                                <asp:Label ID="Label16" runat="server" Text="Active: "></asp:Label>
                                <asp:DropDownList ID="ddlAct" runat="server" Width="80px" CausesValidation="True">
                                    <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </label>  
                            
                        </td>                      
                    </tr>     
                        <tr>
                    <td>
                          <label style="width:80px;">
                                <asp:Label ID="Label3" runat="server" Text="Search: "></asp:Label>
                            </label>
                           <label style="width:350px;">
                              <asp:TextBox ID="TextBox5" runat="server"   placeholder=" Search here"  Font-Bold="true"   Width="350px" Height="20px" onKeyDown="submitButton(event)"  ></asp:TextBox>
                              </label>
                            <label>
                             <asp:Button ID="BtnGo" CssClass="btnSubmit" runat="server" Text="Go" OnClick="BtnGo_Click"  />
                            </label> 
                    </td>
                   
                    </tr>
                    </asp:Panel>
                    
                    <tr>
                        <td style="width:100%">
                            <asp:Panel ID="pnlgrid" runat="server" ScrollBars="Vertical">
                                <table width="100%">                                    
                                    <tr>
                                        <td align="left">
                                            <asp:Panel ID="Panel2" runat="server" >
                                                
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    
                </table>
            </fieldset>
            <input id="PageId" name="PageId" runat="server" type="hidden" style="width: 1px" />
            <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
            <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Width="90%" BackColor="White"  Height="450px" ScrollBars="None" BorderStyle="Solid" BorderColor="Gray">
                <table style="width: 100%">
                        <tr>
                              <td style="padding-left:16px; color:#416271; font-weight: bold; font-size: 18px;" align="center">
                                <span lang="en-us">
                                  <asp:Label ID="lbltext" runat="server" Text="Page Search Result "></asp:Label>
                                </span>
                            </td>
                            <td align="right" >
                               
                                <asp:ImageButton ID="ImageButton5" ImageUrl="~/images/closeicon.jpeg" runat="server" Width="16px" OnClick="ImageButton2_Click"  />
                               
                            </td>
                        </tr>
                       
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel4" runat="server"   Height="400px" ScrollBars="Auto" BorderStyle="Solid">
                                <asp:GridView ID="GridView1" Width="100%" runat="server" DataKeyNames="PageId"  AutoGenerateColumns="False" EmptyDataText="There is no data."   CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                                                PageSize="30" AllowPaging="true" AllowSorting="True"
                                               OnPageIndexChanging="GridView1_PageIndexChanging"
                                                   OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting"  OnRowDeleting="GridView1_RowDeleting"  OnRowCommand="GridView1_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="MainMenuCatName" HeaderStyle-HorizontalAlign="Left" HeaderText="Category"  SortExpression="MainMenuCatName" >
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>                                                        
                                                        <asp:BoundField DataField="MainMenuName" HeaderStyle-HorizontalAlign="Left" HeaderText="Main Menu"  SortExpression="MainMenuName">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SubMenuName" HeaderText="Sub Menu" HeaderStyle-HorizontalAlign="Left" SortExpression="SubMenuName">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>  
                                                        <asp:TemplateField HeaderText="Page Name And Title">
                                                                <ItemTemplate>
                                                                      <asp:LinkButton ID="LinkButton2" Style="color: #717171;" runat="server"  OnClick="link2_Click"> <asp:Label ID="lbl_pagename_Grid1" runat="server" Text='<%#Bind("PageName") %>'  ToolTip='<%#Bind("PageDescription") %>'></asp:Label><asp:Label ID="lbl_foldername" runat="server" Text='<%#Bind("FolderName") %>' Visible="false"></asp:Label><asp:Label ID="lblpageid" runat="server" Text='<%#Bind("PageId") %>' Visible="false"></asp:Label><asp:Label ID="lbl_MainMenucatId" runat="server" Text='<%#Bind("MainMenucatId") %>' Visible="false"></asp:Label></asp:LinkButton>   
                                                                      <br />(<asp:Label ID="lblpagetitle" runat="server" Text='<%#Bind("PageTitle") %>' Font-Size="Small" ToolTip='<%#Bind("PageDescription") %>' ></asp:Label>)                                                                  
                                                                 </ItemTemplate>
                                                        </asp:TemplateField>
                                                         
                                                          <asp:BoundField DataField="Active" HeaderText="Active" HeaderStyle-HorizontalAlign="Left" SortExpression="Active">
                                                            
                                                             <HeaderStyle HorizontalAlign="Left" Width="3%"  />
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:BoundField>      
                                                           <asp:BoundField DataField="ManuAccess" HeaderText="Manu Access" HeaderStyle-HorizontalAlign="Left" SortExpression="ManuAccess">
                                                             <HeaderStyle HorizontalAlign="Left" Width="3%"  />
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:BoundField>      
                                                        <asp:TemplateField HeaderText="Do I Have Rights?"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                                <ItemTemplate>                                                               
                                                                    <asp:ImageButton ID="imgaccess" runat="server" Height="25px"  CommandArgument='<%# Eval("PageId") %>' ToolTip="Manual backup" CommandName="backup" ImageUrl="~/images/Right.jpg" />                                                     
                                                                    <asp:ImageButton ID="imgnotaccess" Enabled="false"  runat="server" Height="25px"  ToolTip="Cant Backup"  ImageUrl="~/images/closeicon.png" Visible="false" />                                                    
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                                <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Which designation has rights to view ?"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                                <ItemTemplate>                                                               
                                                                   <asp:Label ID="lblDesi_right" runat="server" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateField>
                                                                                                                                                                
                                                        <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Edit" ImageUrl="~/Account/images/edit.gif"   Text="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" Visible="false" > 
                                                        <HeaderStyle HorizontalAlign="Left"   />
                                                        </asp:ButtonField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="0%" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinkbb" ToolTip="Delete" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" CommandArgument='<%# Eval("PageId") %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                                </asp:Panel>
                            </td>
                        </tr>
                        </table>
                
        </asp:Panel>
         <asp:Button ID="Button8" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"  PopupControlID="Panel1" TargetControlID="Button8" >
                                </cc1:ModalPopupExtender>
                   
                    
                </fieldset>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
