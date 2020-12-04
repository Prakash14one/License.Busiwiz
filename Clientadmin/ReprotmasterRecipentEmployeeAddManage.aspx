<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="ReprotmasterRecipentEmployeeAddManage.aspx.cs" Inherits="Page_Master" Title="ReprotmasterRrecipentEmployeeTbl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

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

       
        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
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

//        function button_click(objTextBox, objBtnID) {
//            if (window.event.keyCode == 13) {
//                document.getElementById(objBtnID).focus();
//                document.getElementById(objBtnID).click();
//            }
        //        }

        function submitButton(event) {
            if (event.which == 13) {
                $('#BtnGo').trigger('click');
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin-left:1%">
                <asp:Label ID="lblmsg" runat="server" Text="1212" ForeColor="Red"></asp:Label>
                 <asp:Label ID="lblpagename" runat="server"  Visible="false"></asp:Label>
            </div>
            <div style="clear: both;">

              
           


            </div>
          
            <fieldset>
                <legend>
                    <asp:Label ID="lbllegend" runat="server" Text="Add Rules for sending Reprots "></asp:Label>
                </legend>
               <div style="float: right;">
              <asp:Button ID="addnewpanel" runat="server" Text="Add Rules " CssClass="btnSubmit" OnClick="addnewpanel_Click" />
              </div>
                <asp:Panel ID="pnladdnew" runat="server" Visible="false" >
                <table width="100%">
                    <tr>
                        <td colspan="2" align="right">
                            
                        </td>
                    </tr>
                   
                    <tr>
                        <td style="width: 35%">
                            <label>
                                <asp:Label ID="Label13" runat="server" Text="Filter by Product"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="FilterProduct" Width="600px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FilterProduct_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>                 
                    <tr>
                    <td>
                    <label style="width:300px;">
                        Select the  Page for sending report about
                    </label> 
                    </td>
                    <td>
                   <label style="width:300px;">
                                <asp:DropDownList ID="ddlpagename" runat="server" Width="300px" AutoPostBack="True" onselectedindexchanged="ddlpagename_SelectedIndexChanged">
                                </asp:DropDownList>
                                </label> 
                                <label style="width:30px;">
                                <asp:ImageButton ID="imgadd" runat="server" Height="20px" ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" OnClick="imgadd_Click" ToolTip="Add New" Width="20px" />
                                </label> 
                                <label style="width:30px;">
                                <asp:ImageButton ID="imgrefrole" runat="server"  Height="20px" ImageAlign="Bottom" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px" OnClick="imgrefrole_Click" />
                                </label>
                                <label style="width:40px;">
                          <asp:Button ID="btnsearch" CssClass="btnSubmit" runat="server" Text="Search" OnClick="btnsearch_Click"   />
                          </label>
                    </td>
                    </tr>
                    <asp:Panel ID="pnl_search" runat="server" Visible="false">
                    <tr>
                    <td>
                    
                    </td>
                    <td>
                   <label style="width:50px;">
                                <asp:Label ID="Label16N" runat="server" Text="Search"></asp:Label>
                            </label>
                   <label style="width:200px;">
                      <asp:TextBox ID="TextBox5" runat="server"   placeholder="Search"  Font-Bold="true" Width="200px" Height="20px" onKeyDown="submitButton(event)"  ></asp:TextBox>
                    </label>  
                    <label style="width:40px;">
                          <asp:Button ID="BtnGo" CssClass="btnSubmit" runat="server" Text="Go" OnClick="BtnGo_Click" Width="40px" />
                          </label>
                    
                    </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                                <asp:Panel ID="Panel2" runat="server" Height="250px" ScrollBars="Vertical">
                                                <asp:GridView ID="GV_pagelist" Width="100%" runat="server" DataKeyNames="PageId" OnRowCommand="GV_pagelist_RowCommand"
                                                    AutoGenerateColumns="False" EmptyDataText="There is no data." 
                                                    AllowSorting="false"  CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                    AlternatingRowStyle-CssClass="alt">
                                                    <Columns>
                                                       <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="PageTitle" HeaderText="Page Title" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="PageTitle">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="MainMenuName" HeaderStyle-HorizontalAlign="Left" HeaderText="Main Menu"  SortExpression="MainMenuName">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SubMenuName" HeaderText="Sub Menu" HeaderStyle-HorizontalAlign="Left"  SortExpression="SubMenuName">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>                                                     
                                                       
                                                        <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Select" ImageUrl="~/images/Selectimg.png"  Text="Edit" HeaderImageUrl="~/images/Selectimg.png" HeaderStyle-HorizontalAlign="Left" >
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:ButtonField>
                                                   
                                                     
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </asp:Panel>
                        </td>
                    </tr>
                    </asp:Panel>
                    <tr>
                        <td>
                            <label>
                                Page Name
                            </label> 
                        </td>
                        <td>
                            <label>
                              <asp:Label ID="lbl_pagename" runat="server" Text=""></asp:Label>
                            </label> 
                        </td>
                    </tr>
                      <tr>
                        <td>
                            <label>
                                Page Title
                            </label> 
                        </td>
                        <td>
                            <label>
                              <asp:Label ID="lbl_titel" runat="server" Text=""></asp:Label>
                            </label> 
                        </td>
                    </tr>
                    <tr>
                    <td colspan="2" >
                        <label style="width:600px;">
                            Select Employee to receive autogenerated report
                        </label> 
                        
                    </td>
                    
                    </tr>
                   
                  
                        <tr>
                        
                               <td colspan="2">                               
                          <label style="width:200px;">
                                  <asp:Label ID="Label40" runat="server" Text="Business Name"></asp:Label>
                                  <asp:DropDownList ID="ddlfilterstore" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfilterstore_SelectedIndexChanged" Width="180px" >
                                  </asp:DropDownList>
                         </label>
                         <label style="width:200px;">
                          <asp:Label ID="Label3" runat="server" Text="Time Zone" />
                               <asp:DropDownList ID="ddlwztimezone" runat="server" ValidationGroup="1" Width="180px">
                                                        </asp:DropDownList>
                         </label> 
                        <label style="width:200px;">
                                 <asp:Label ID="Label42" runat="server" Text="Employee Name"></asp:Label>
                                    <asp:DropDownList ID="ddlfilteremployee" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilteremployee_SelectedIndexChanged" Width="180px">
                                 </asp:DropDownList>
                         </label>
                        <label style="width:100px;">
                         <asp:Label ID="Label2" runat="server" Text="Set Time"></asp:Label>
                                        <asp:TextBox ID="txtstarttime" runat="server" Width="40px" MaxLength="5"></asp:TextBox>
                                       
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtstarttime"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtstarttime"
                                            ErrorMessage="*" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                         </label>
                            <label style="width:130px;">
                                 <br />
                                       <span class="lblInfoMsg">(24Hour(HH:MM))</span>  
                           </label>
                         <label style="width:50px;"><br />
                           <asp:Button ID="Button2N" CssClass="btnSubmit" runat="server" ValidationGroup="1" Text="Add" OnClick="Button2_Click"   />                           
                          </label> 
                          <label>
                          <asp:Label Visible="false" class="lblInfoMsg"  ID="lblempmsg" runat="server" Text="Employee alredy Added "></asp:Label>
                          </label> 
                         </td>
                        </tr>
                   <tr>
                   <td colspan="2">
                   
                       <asp:GridView ID="gridFileAttach" runat="server" 
                           AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid" 
                           GridLines="Both" OnRowCommand="gridFileAttach_RowCommand" 
                           PagerStyle-CssClass="pgr" Width="100%">
                           <Columns>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50%" 
                                   HeaderText="Employee Name">
                                   <ItemTemplate>
                                       <asp:Label ID="Label1" runat="server" Text='<%#Bind("employeename") %>'></asp:Label>
                                       <asp:Label ID="lblempid" runat="server" Text='<%#Bind("employee") %>' 
                                           Visible="false"></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" 
                                   HeaderText="Time">
                                   <ItemTemplate>
                                       <asp:Label ID="lbltime" runat="server" Text='<%#Bind("time") %>'></asp:Label>
                                       <asp:Label ID="lbltimezoneid" runat="server" Text='<%#Bind("timezoneid") %>' 
                                           Visible="false"></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40%" 
                                   HeaderText="Timezone">
                                   <ItemTemplate>
                                       <asp:Label ID="lbltimezone" runat="server" Text='<%#Bind("timezone") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:ButtonField CommandName="Delete1" HeaderStyle-HorizontalAlign="Left" 
                                   HeaderText="Remove" ImageUrl="~/Account/images/delete.gif" 
                                   ItemStyle-ForeColor="Black" Text="Remove" />
                           </Columns>
                       </asp:GridView>                   
                   </td>
                   </tr>
                    <tr>
                        <td>
                           
                        </td>
                        <td>
                        </td>
                    </tr>
                     <tr>
                            <td>
                              
                                
                            </td>
                           <td>
                            <asp:Button ID="Button1" CssClass="btnSubmit" runat="server" Text="Submit" OnClick="Button1_Click"   />
                            <asp:Button ID="btnupdate" Visible="false"  CssClass="btnSubmit" runat="server" Text="Update" OnClick="Btnupdate_Click"   />
                           </td>
                            </tr>
                </table>
                </asp:Panel>
            </fieldset>  
            <fieldset>
                <legend>
                    <asp:Label ID="Label12" runat="server" Text="Rules for sending Reprots"></asp:Label>
                </legend>
                <table width="100%">
                <tr>
                <td>
  <input id="PageId" name="PageId" runat="server" type="hidden" style="width: 1px" />
          
                </td>
                <td>
                </td>
                </tr>
                <tr>
                <td colspan="2"> 
                                            <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                            <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                            <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                            <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                </td>
                </tr>
                <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" OnRowDeleting="GridView1_RowDeleting"
                                                    DataKeyNames="id" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                                                    OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand" EmptyDataText="No Record Found."
                                                    AllowSorting="True" OnSorting="GridView1_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Page Name" SortExpression="PageName" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="27%" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmainmenuName" runat="server" Text='<%#Bind("PageName") %>'></asp:Label>
                                                            </ItemTemplate>                                                         
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="27%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Page Title" SortExpression="BackColour" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="75%" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBackColor" runat="server" Text='<%#Bind("PageTitle") %>' ></asp:Label>
                                                            </ItemTemplate>                                                          
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="75%" />
                                                        </asp:TemplateField>                                                     
                                                        <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Account/images/edit.gif"  CommandName="Edit" CommandArgument='<%# Eval("id") %>' ToolTip="Edit" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" CommandArgument='<%# Eval("id") %>' ToolTip="Delete"></asp:ImageButton>
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
            </fieldset>           
        </ContentTemplate>         
    </asp:UpdatePanel>
     <div style="position: fixed;bottom: 0; right:20px;">
              <asp:Label ID="lblVersion" runat="server" Text="V5" ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>
</asp:Content>
  

