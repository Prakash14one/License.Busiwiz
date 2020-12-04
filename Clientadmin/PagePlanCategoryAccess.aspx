<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="PagePlanCategoryAccess.aspx.cs" Inherits="PageAccessUser" Title="Page Access PlanUser" %>

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
                        <td>
                            <label>
                            1 ) Select the product and websites whose page access rights are to be set for designations /roles
                            </label> 
                        </td>
                    </tr>
                <tr>
                    <td width="100%">
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Product Name/Version"></asp:Label>
                            <asp:Label ID="Label7" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProductname" ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                             <asp:DropDownList ID="ddlProductname" runat="server" Width="250px" AutoPostBack="True"  OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                         <label>
                                 <asp:Label ID="Label8" runat="server" Text="Select Website"></asp:Label>
                                <asp:Label ID="Label9" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DDLWebsiteC" ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                 <asp:DropDownList ID="DDLWebsiteC" runat="server" Width="200px" AutoPostBack="True" onselectedindexchanged="DDLWebsiteC_SelectedIndexChanged">
                                              </asp:DropDownList>
                            </label> 
                    </td>
                  
                </tr>
                  <tr>
                        <td>
                               <label> 2)	Select th portal and price plan category for which you wish to set the page access rights						</label>

                        </td>
                    </tr>               
                <tr>
                    <td>
                        <label>
                            Portal Name
                            <asp:RequiredFieldValidator ID="reqportal" runat="server" ErrorMessage="*" SetFocusOnError="true" InitialValue="0" ControlToValidate="ddlportal" ValidationGroup="1"> </asp:RequiredFieldValidator>
                              <asp:DropDownList ID="ddlportal" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlportal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                          <label>
                            <asp:Label ID="Label48" runat="server" Text="Priceplan Category"></asp:Label>
                            <asp:Label ID="Label49" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlpriceplancatagory" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                              <asp:DropDownList ID="ddlpriceplancatagory" runat="server" Width="200px" OnSelectedIndexChanged="ddlpriceplancatagory_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </label>
                    </td>                   
                </tr>
                           
                <tr>                    
                    <td>
                             <table>
                                    <tr>
                                        <td colspan="2">
                                             <asp:Label ID="lbl_CopyAccess" runat="server" Text=" Would you like to copy rights you already set for other priceplan category to this Price plan category for which you are trying to create access rights?" Visible="false" ></asp:Label>                                                                              
                                        </td>
                                    </tr>
                                    <tr valign="middle">
                                        <td style="width:45%"><br />
                                             <asp:RadioButtonList ID="Rbtn_CopyAccess" runat="server" AutoPostBack="true" BackColor="White" RepeatDirection="Vertical" OnSelectedIndexChanged="Rbtn_CopyAccess_SelectedIndexChanged" Visible="false">                                                          
                                                            <asp:ListItem Value="1" Text="Yes Copy page access from others priceplan category access rights"></asp:ListItem>                                                            
                                                             <asp:ListItem Value="0" Text=" No  I would like to set the rights by selecting pages"></asp:ListItem>                                                         
                                                        </asp:RadioButtonList>
                                        </td>   
                                        <td>
                                              <asp:DropDownList ID="ddlpriceplancatagoryCopyFrom" runat="server" Width="200px" OnSelectedIndexChanged="ddlpriceplancatagoryCopyFrom_SelectedIndexChanged" AutoPostBack="True" Visible="false">
                                            </asp:DropDownList>
                                        </td>
                                        </tr>

                                        </table>
                       

                                        
                                          
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
                            <label> 3)	Set rights</label>
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
                              <label>
                          <asp:CheckBox ID="chk_activelistonly" runat="server" Text="Show Active Filter Only" Checked="true" AutoPostBack="true" OnCheckedChanged="DropDownList2_SelectedIndexChanged" />      
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
                        <asp:Panel ID="pnlgrid" runat="server" Width="100%" ScrollBars="Vertical">
                            <table width="100%">
                                <tr align="center">
                                    <td>
                                        <div id="mydiv" class="closed">
                                            <table width="100%">
                                                <tr align="center">
                                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                        <%--<asp:Label ID="Label19" runat="server" Text="List of Pagewise Controls" Font-Italic="True"></asp:Label>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                     <asp:Panel ID="Panel1" runat="server" Height="300px" Width="100%" ScrollBars="Vertical">
                                        <asp:GridView ID="GridView1" runat="server" DataKeyNames="PageId" AutoGenerateColumns="False"
                                         OnRowDataBound="GridView1_RowDataBound"   OnRowCommand="GridView1_RowCommand" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                            AlternatingRowStyle-CssClass="alt" AllowSorting="true" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging"
                                            OnSorting="GridView1_Sorting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                            <Columns>
                                                <asp:BoundField DataField="PageId" HeaderStyle-HorizontalAlign="Left" Visible="true" HeaderText="Id" SortExpression="PageId" HeaderStyle-Width="5%">
                                                </asp:BoundField> 
                                                 <asp:BoundField DataField="MasterPageName" HeaderStyle-HorizontalAlign="Left" Visible="true" HeaderText="MasterPageName" SortExpression="MasterPageName" HeaderStyle-Width="10%">
                                                </asp:BoundField> 
                                                 <asp:BoundField DataField="MainMenuName" HeaderStyle-HorizontalAlign="Left" Visible="true" HeaderText="MainMenuName" SortExpression="MainMenuName" HeaderStyle-Width="15%">
                                                </asp:BoundField> 
                                                <asp:BoundField DataField="SubMenuName" HeaderStyle-HorizontalAlign="Left" Visible="true" HeaderText="SubMenuName" SortExpression="SubMenuName" HeaderStyle-Width="15%">
                                                </asp:BoundField> 
                                                <asp:BoundField DataField="PageName" HeaderStyle-HorizontalAlign="Left" Visible="true" HeaderText="PageName" SortExpression="PageName" HeaderStyle-Width="25%">
                                                </asp:BoundField> 
                                                <asp:BoundField DataField="PageTitle" HeaderStyle-HorizontalAlign="Left" Visible="true" HeaderText="PageTitle" SortExpression="PageTitle" HeaderStyle-Width="35%">
                                                </asp:BoundField> 
                                                
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="cbHeader" runat="server" OnCheckedChanged="ch1_chachedChanged" AutoPostBack="true" />
                                                        <asp:Label ID="check" runat="server" ForeColor="White" Text=" Access Right" />                                                        
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbItem" runat="server" Checked='<%#bind("pag") %>' /></ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="0%">
                                                            <ItemTemplate>
                                                               <asp:Label ID="lblpageid" runat="server" Visible="false"  Text='<%# Eval("PageId") %>'> </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <input runat="server" id="hdnProductDetailId" type="hidden" name="hdnProductDetailId"
                            style="width: 3px" />
                        <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                        <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                        <input runat="server" id="hdnProductId" type="hidden" name="hdnProductId" style="width: 3px" />
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
