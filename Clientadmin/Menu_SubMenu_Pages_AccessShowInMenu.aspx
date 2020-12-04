<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Menu_SubMenu_Pages_AccessShowInMenu.aspx.cs" Inherits="PageAccessUser" Title="Page Access PlanUser" %>

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
        function ChangeCheckBoxState(id, checkstate) {

            var chb = document.getElementById(id);
            if (chb != null)
                chb.checked = checkstate;
        }

        function ChangeAllCheckBoxStates(checkstate) {

            if (CheckBoxIDs != null) {
                for (var i = 0; i < CheckBoxIDs.length; i++)
                    ChangeCheckBoxState(CheckBoxIDs[i], checkstate);
            }
        }

        function ChangeHeaderState() {

            if (CheckBoxIDs != null) {
                for (var i = 1; i < CheckBoxIDs.length; i++) {
                    var chb = document.getElementById(CheckBoxIDs[i]);
                    if (!chb.checked) {
                        ChangeCheckBoxState(CheckBoxIDs[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDs[0], true);
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
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div style="float: left;">
        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div style="clear: both;">
    </div>
    <div class="products_box">
        <fieldset>
            <legend>
                <asp:Label ID="lbllgnd" runat="server" Text="Selecting pages accessible by Plan users "></asp:Label>
            </legend>
            <%--<asp:Label ID="CheckBoxIDsArray" runat="server" Text=""></asp:Label>--%>
            <table width="100%">
                <tr>
                    <td width="30%">
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Product Name/Version"></asp:Label>
                            <asp:Label ID="Label7" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProductname" ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td width="70%">
                        <label>
                            <asp:DropDownList ID="ddlProductname" runat="server" Width="250px" AutoPostBack="True"  OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                            <label>
                                 <asp:Label ID="Label8" runat="server" Text="Select Website"></asp:Label>
                                <asp:Label ID="Label9" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DDLWebsiteC" ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </label> 
                    </td>
                    <td>
                        <label>
                            <asp:DropDownList ID="DDLWebsiteC" runat="server" Width="200px" AutoPostBack="True" onselectedindexchanged="DDLWebsiteC_SelectedIndexChanged">
                                              </asp:DropDownList>
                        </label> 
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Portal Name<asp:RequiredFieldValidator ID="reqportal" runat="server" ErrorMessage="*"
                                SetFocusOnError="true" InitialValue="0" ControlToValidate="ddlportal" ValidationGroup="1"> </asp:RequiredFieldValidator></label>
                    </td>
                    <td>
                        <label>
                            <asp:DropDownList ID="ddlportal" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlportal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        <label>
                            <asp:Label ID="Label48" runat="server" Text="Priceplan Category"></asp:Label>
                            <asp:Label ID="Label49" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlpriceplancatagory"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td width="80%">
                        <label>
                            <asp:DropDownList ID="ddlpriceplancatagory" runat="server" Width="200px" OnSelectedIndexChanged="ddlpriceplancatagory_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                    <label>
                        <asp:Label ID="Label2" runat="server" Text="Role"></asp:Label>
                     </label> 
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlrolemode" runat="server" AutoPostBack="true"  Width="250px" OnSelectedIndexChanged="ddlrolemode_SelectedIndexChanged">
                         </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Menu Category
                        </label> 
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlcategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMaster_SelectedIndexChanged" Height="27px"  >
                            </asp:DropDownList>
                    </td>
                </tr>

                <asp:Panel ID="paneldfd" runat="server" Visible="false">
                    <tr>
                        <td width="30%">
                            <label>
                                <asp:Label ID="Label5" runat="server" Text="Price Plan"></asp:Label>
                                <asp:Label ID="Label6" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlpriceplan"
                                    runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </label>
                        </td>
                        <td width="70%">
                            <label>
                                <asp:DropDownList ID="ddlpriceplan" runat="server" Width="250px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlpriceplan_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <%--<asp:Label ID="lbllgnggngg" runat="server" Text="List of"></asp:Label>--%>
            </legend>
            <asp:Panel ID="panelem" runat="server" Visible="false">
                <table width="100%">
                    <tr>
                        <td>
                         <label>
                          <asp:CheckBox ID="chk_activelistonly" runat="server" Text="Show Active Filter Only" Checked="true" AutoPostBack="true" OnCheckedChanged="DropDownList2_SelectedIndexChanged" />      
                        </label>  
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                        
                            <label>
                                   Master Page 
                                   <asp:DropDownList ID="DDLmasterpageL" runat="server" AutoPostBack="True" Width="200px" OnSelectedIndexChanged="FilterMaster_SelectedIndexChanged">
                                        </asp:DropDownList>
                            </label>
                            <label>
                                Menu Category
                                <asp:DropDownList ID="DDLCategoryS" runat="server" AutoPostBack="True"  Width="200px" OnSelectedIndexChanged="FilterCategory_SelectedIndexChanged" >
                                        </asp:DropDownList>
                            </label> 
                            <label>
                                    Main Menu
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="200px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                            <label>
                                Sub Menu
                                  <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Width="200px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>    
                    <tr>
                        <td>
                            <label style="width:400px;" >
                             <label>  Pages</label> 
                                <label>
                                   <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Width="100px">
                                                <asp:ListItem Text="ALL" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Active" Selected="True" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            </label> 
                                            <label>
                                             <asp:DropDownList ID="DDL_Accesspages" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Width="200px" >
                                                <asp:ListItem Text="ALL" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Show Pages To Given Access" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Show Pagess To Not Given Access" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            </label> 

                            </label> 
                                
                        </td>
                    </tr>               
                </table>
            </asp:Panel>
            <table width="100%">
                <tr>
                    <td colspan="2">

                            <asp:Menu ID="Menu1" runat="server" Font-Bold="true" Orientation="Horizontal" CssClass="Menu" 
                                    Font-Size="12px" Font-Names="Verdana" ForeColor="White" Width="100%" OnMenuItemClick="Menu1_MenuItemClick1">
                                    <StaticMenuItemStyle Font-Size="12px" Height="35px" />
                                    <DynamicMenuItemStyle Font-Size="12px" CssClass="DynamicMenu" Height="40px" HorizontalPadding="10px" />
                                    <DynamicHoverStyle Font-Size="12px" CssClass="menuhover" />
                                    <StaticHoverStyle Font-Size="12px" CssClass="menuhover" />
                                    <StaticSelectedStyle CssClass="menuSelected" />
                            </asp:Menu>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <caption>
                    <tr>
                        <td align="center">
                            <asp:Button ID="Button1" runat="server" OnClick="btnSubmit_Click" Text="Apply"  ValidationGroup="1" Visible="False" />
                        </td>
                    </tr>
                   

                     
                </caption>
            </table>
        </fieldset>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
