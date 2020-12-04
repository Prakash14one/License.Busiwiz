<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master"
    AutoEventWireup="true" CodeFile="Page_Role_MultiAccess.aspx.cs" Inherits="Shoppingcart_Admin_Page_role_Access"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>--%>

    <script language="javascript" type="text/javascript">

        function SelectAllCheckboxes(spanChk) {


            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {

                if (elm[i].checked != xState)
                    elm[i].click();

            }
        }
    </script>

   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
            <fieldset>
                <legend>
                    <asp:Label ID="Label6" runat="server" Text="Role Access Management"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <%-- <label>
                                <asp:Label ID="Label2" runat="server" Text="Select role to set its access rights"></asp:Label>
                                  <asp:Label ID="Label53" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dpdrolename"
                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </label>
                            <label>
                                <asp:DropDownList ID="dpdrolename" runat="server" AutoPostBack="True" Width="200px"
                                    OnSelectedIndexChanged="dpdrolename_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>--%>
                        </td>
                       
                    </tr>
                    <tr>
                    <td>
                     <asp:Panel ID="pnlpr" runat="server" ScrollBars="Horizontal" Height="200px" Width="500px">
                                                <asp:GridView ID="GvRoleName" runat="server" DataKeyNames="Role_id" AutoGenerateColumns="False"
                                                    EmptyDataText="There is no data." AllowSorting="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    Width="450px" OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Role Name" SortExpression="Role_id" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="300px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblp" runat="server" Text='<%# Bind("Role_name") %>'></asp:Label>
                                                                <asp:Label ID="lblroleid" runat="server" Text='<%# Bind("Role_id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="cbHeader" runat="server" OnCheckedChanged="ch1_chachedChanged" AutoPostBack="true" />
                                                                <asp:Label ID="check" runat="server" ForeColor="White" Text="Role Access" HeaderStyle-Width="100px" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbItem"  runat="server" />
                                                                <asp:CheckBox ID="chkdef"  runat="server" Visible="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                    </td>
                    </tr>
                    <tr>
                        <td style="height: 32px">
                        <label>
                            <asp:RadioButtonList ID="Rlist" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="Rlist_SelectedIndexChanged"
                                AutoPostBack="True" Width="940px">
                                <asp:ListItem Value="0">Do you wish to set rights at Menu Level ?</asp:ListItem>
                                <asp:ListItem Value="1">Do you wish to set rights at Sub Menu Level ?</asp:ListItem>
                                <asp:ListItem Value="2" Selected="True">Do you wish to set rights at Page level ?</asp:ListItem>
                            </asp:RadioButtonList>
                        </label>    
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input id="hdnFileName" runat="server" name="hdnFileName" style="width: 1px" type="hidden" />
                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Panel ID="pnlmain" runat="server" Width="940px" Visible="False">
                                <asp:GridView ID="grdmain" runat="server" AutoGenerateColumns="False" CssClass="mGrid"  PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="MainMenuId" Width="940px"  AllowSorting="True"   onsorting="grdmain_Sorting">
                                    <%--OnRowCommand="grdmain_RowCommand"--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Menu Name" HeaderStyle-HorizontalAlign="Left" SortExpression="MainMenuName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblma" runat="server" Text='<%#Bind("MainMenuName") %>'></asp:Label>
                                                <asp:Label ID="lblmainmanu" runat="server" Visible="false" Text='<%#Bind("MainMenuId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblInw" runat="server" Text="Menu Level Access"></asp:Label>
                                                <asp:RadioButtonList ID="rlheader" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rlheader_SelectedIndexChanged" RepeatDirection="Horizontal">                                                   
                                                    <asp:ListItem Value="0">Denied</asp:ListItem>
                                                    <asp:ListItem Value="1">Full</asp:ListItem>
                                                    <asp:ListItem Value="2">Limited</asp:ListItem>
                                                     <asp:ListItem Value="3" Selected="True">None</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="RadioButtonListgrdmain_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Denied</asp:ListItem>
                                                    <asp:ListItem Value="1">Full</asp:ListItem>
                                                    <asp:ListItem Value="2">Limited</asp:ListItem>
                                                    <asp:ListItem Value="3" Selected="True">None</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Edit_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblIn" runat="server" Text="Edit"></asp:Label>
                                                <asp:CheckBox ID="chkedit" runat="server" AutoPostBack="true" OnCheckedChanged="chkedit_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxEdit1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Delete_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDELETE" runat="server" Text="Delete"></asp:Label>
                                                <asp:CheckBox ID="chkdelete" runat="server" AutoPostBack="true" OnCheckedChanged="chkdelete_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDelete1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Download_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDownload" runat="server" Text="Download"></asp:Label>
                                                <asp:CheckBox ID="chkDownload" runat="server" AutoPostBack="true" OnCheckedChanged="chkDownload_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDownload1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Insert_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblInsert" runat="server" Text="Insert"></asp:Label>
                                                <asp:CheckBox ID="chkInsert" runat="server" AutoPostBack="true" OnCheckedChanged="chkInsert_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxInsert1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Update_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblUpdate" runat="server" Text="Update"></asp:Label>
                                                <asp:CheckBox ID="chkUpdate" runat="server" AutoPostBack="true" OnCheckedChanged="chkUpdate_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxUpdate1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="View_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblView" runat="server" Text="View"></asp:Label>
                                                <asp:CheckBox ID="chkView" runat="server" AutoPostBack="true" OnCheckedChanged="chkView_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxView1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Go_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblGo" runat="server" Text="Go"></asp:Label>
                                                <asp:CheckBox ID="chkGo" runat="server" AutoPostBack="true" OnCheckedChanged="chkGo_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxGo1" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Send Mail" SortExpression="SendMail_Right" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSendMail" runat="server" Text="Send Mail"></asp:Label>
                                                <asp:CheckBox ID="chkSendMail" runat="server" AutoPostBack="true" OnCheckedChanged="chkSendMail_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxSendMail1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Panel ID="pnlsubmanu" runat="server" Width="940px" Visible="False">
                                <div style="clear: both;">
                                </div>
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Filter by Main Manu"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlmainfilter" runat="server" AutoPostBack="True" Width="200px"
                                        OnSelectedIndexChanged="ddlmainfilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <div style="clear: both;">
                                </div>
                                <asp:GridView ID="Grdsub" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="SubMenuId"
                                    Width="940px" AllowSorting="True" onsorting="Grdsub_Sorting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Menu Name" HeaderStyle-HorizontalAlign="Left" SortExpression="MainMenuName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblptypename" runat="server" Text='<%#Bind("MainMenuName") %>'></asp:Label>
                                                <asp:Label ID="lblpid" runat="server" Visible="false" Text='<%#Bind("MainMenuId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Menu Name" HeaderStyle-HorizontalAlign="Left"
                                            SortExpression="SubMenuName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsubmanuname" runat="server" Text='<%#Bind("SubMenuName") %>'></asp:Label>
                                                <asp:Label ID="lblsubmanuid" runat="server" Visible="false" Text='<%#Bind("SubMenuId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblInw" runat="server" Text="Sub Menu Level Access"></asp:Label>
                                                <asp:RadioButtonList ID="rlheader" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rlheader_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                   
                                                    <asp:ListItem Value="0">Denied</asp:ListItem>
                                                    <asp:ListItem Value="1">Full</asp:ListItem>
                                                    <asp:ListItem Value="2">Limited</asp:ListItem>
                                                     <asp:ListItem Value="4">None</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" RepeatDirection="Horizontal">                                                   
                                                    <asp:ListItem Value="0">Denied</asp:ListItem>
                                                    <asp:ListItem Value="1">Full</asp:ListItem>
                                                    <asp:ListItem Value="2">Limited</asp:ListItem>
                                                     <asp:ListItem Value="4" Selected="True">None</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Edit_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblIn" runat="server" Text="Edit"></asp:Label>
                                                <asp:CheckBox ID="chkedit" runat="server" AutoPostBack="true" OnCheckedChanged="chkedit_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxEdit1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Delete_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDELETE" runat="server" Text="Delete"></asp:Label>
                                                <asp:CheckBox ID="chkdelete" runat="server" AutoPostBack="true" OnCheckedChanged="chkdelete_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDelete1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Download_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDownload" runat="server" Text="Download"></asp:Label>
                                                <asp:CheckBox ID="chkDownload" runat="server" AutoPostBack="true" OnCheckedChanged="chkDownload_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDownload1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Insert_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblInsert" runat="server" Text="Insert"></asp:Label>
                                                <asp:CheckBox ID="chkInsert" runat="server" AutoPostBack="true" OnCheckedChanged="chkInsert_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxInsert1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Update_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblUpdate" runat="server" Text="Update"></asp:Label>
                                                <asp:CheckBox ID="chkUpdate" runat="server" AutoPostBack="true" OnCheckedChanged="chkUpdate_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxUpdate1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="View_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblView" runat="server" Text="View"></asp:Label>
                                                <asp:CheckBox ID="chkView" runat="server" AutoPostBack="true" OnCheckedChanged="chkView_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxView1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Go_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblGo" runat="server" Text="Go"></asp:Label>
                                                <asp:CheckBox ID="chkGo" runat="server" AutoPostBack="true" OnCheckedChanged="chkGo_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxGo1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Send Mail" HeaderStyle-HorizontalAlign="Left" SortExpression="SendMail_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSendMail" runat="server" Text="Send Mail"></asp:Label>
                                                <asp:CheckBox ID="chkSendMail" runat="server" AutoPostBack="true" OnCheckedChanged="chkSendMail_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxSendMail1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
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
                   
                   </td>
                   </tr>
                    <tr>
                        <td align="left">
                            <asp:Panel ID="pnlpage" runat="server" Visible="False" ScrollBars="Horizontal">
                                <div style="clear: both;">
                                </div>
                                <label  style="width:170px;">
                                 <asp:Label ID="Label4" runat="server" Text="Filter by Main Manu"></asp:Label>
                                    <asp:DropDownList ID="ddlmailpagefilter" runat="server" AutoPostBack="True" Width="170px" OnSelectedIndexChanged="ddlmailpagefilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                               
                                <label  style="width:170px;">
                                  <asp:Label ID="Label5" runat="server" Text="Filter by Sub Menu"></asp:Label>
                                    <asp:DropDownList ID="ddlsubpagefilter" runat="server" AutoPostBack="True" Width="170px"  OnSelectedIndexChanged="ddlsubpagefilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                               
                                
                                <label style="width:170px;">
                                <asp:Label ID="Label2" runat="server" Text="Functionality Title"></asp:Label>
                                <asp:DropDownList ID="ddlfuncti" runat="server" AutoPostBack="True"  Width="170px" onselectedindexchanged="ddlsubpagefilter_SelectedIndexChanged">
                                </asp:DropDownList>
                                </label> 
                                <label style="width:250px;">
                                <asp:Label ID="Label7" runat="server" Text="Search"></asp:Label>
                                <asp:TextBox ID="TextBox7" runat="server"   placeholder="Search"  Font-Bold="true"  Width="250px"></asp:TextBox>
                                </label> 
                                 <label  style="width:60px;">
                                 <br />
                                  <asp:Button ID="Button7" CssClass="btnSubmit" runat="server" Text="Search" OnClick="ddlsubpagefilter_SelectedIndexChanged"  />
                                </label> 
                                <div style="clear: both;">
                                </div>
                                <asp:Panel ID="Panel1" runat="server"  ScrollBars="Horizontal"  Width="940px" Height="900px">
                                <asp:GridView ID="grdpage" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="MainMenuId"
                                    Width="940px" AllowSorting="True" onsorting="grdpage_Sorting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Menu Name" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" SortExpression="MainMenuName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblptypename" runat="server" Text='<%#Bind("MainMenuName") %>'></asp:Label>
                                                <asp:Label ID="lblsubmanu" runat="server" Visible="false" Text='<%#Bind("MainMenuId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Menu Name" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Left"
                                            SortExpression="SubMenuName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsubmanuname" runat="server" Text='<%#Bind("SubMenuName") %>'></asp:Label>
                                                 
                                                <asp:Label ID="lblsubmanuid" runat="server" Visible="false" Text='<%#Bind("SubMenuId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Page Name" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" SortExpression="PageName and (Title)">
                                            <ItemTemplate>
                                               <asp:Label ID="lblpagenamee" runat="server" Font-Underline="true" Text='<%#Bind("Pagename") %>' ForeColor="Black"  ></asp:Label>                                                
                                                    <br />(<asp:Label ID="lblpagename" runat="server" Font-Underline="true" Text='<%#Bind("PageTitle") %>' ForeColor="Black" Font-Size="9px"></asp:Label>)
                                                
                                                
                                                <asp:Label ID="lblpageid" runat="server" Visible="false" Text='<%#Bind("PageId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblInw" runat="server" Text="Page Level Access"></asp:Label>
                                                <asp:RadioButtonList ID="rlheader" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rlheader_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="4">Don't make changes</asp:ListItem>
                                                    <asp:ListItem Value="0">Denied Access</asp:ListItem>
                                                    <asp:ListItem Value="1">Give Access</asp:ListItem>
                                                    <%--<asp:ListItem Value="2">Limited</asp:ListItem>--%>
                                                </asp:RadioButtonList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="false"  OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                   <asp:ListItem Value="4" Selected="True">Don't make changes</asp:ListItem>
                                                    <asp:ListItem Value="0">Denied Access</asp:ListItem>
                                                    <asp:ListItem Value="1">Give Access</asp:ListItem>
                                                    <%--<asp:ListItem Value="2" >Limited</asp:ListItem>--%>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Edit_Right" ItemStyle-Width="3%" Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblIn" runat="server" Text="Edit"></asp:Label>
                                                <asp:CheckBox ID="chkedit" runat="server" AutoPostBack="true" OnCheckedChanged="chkedit_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxEdit1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Delete_Right" ItemStyle-Width="5%" Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDELETE" runat="server" Text="Delete"></asp:Label>
                                                <asp:CheckBox ID="chkdelete" runat="server" AutoPostBack="true" OnCheckedChanged="chkdelete_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDelete1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Download_Right" ItemStyle-Width="10%" Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDownload" runat="server" Text="Download"></asp:Label>
                                                <asp:CheckBox ID="chkDownload" runat="server" AutoPostBack="true" OnCheckedChanged="chkDownload_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDownload1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Insert_Right" ItemStyle-Width="5%" Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblInsert" runat="server" Text="Insert"></asp:Label>
                                                <asp:CheckBox ID="chkInsert" runat="server" AutoPostBack="true" OnCheckedChanged="chkInsert_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxInsert1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Update_Right" ItemStyle-Width="7%" Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblUpdate" runat="server" Text="Update"></asp:Label>
                                                <asp:CheckBox ID="chkUpdate" runat="server" AutoPostBack="true" OnCheckedChanged="chkUpdate_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxUpdate1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="View_Right" ItemStyle-Width="5%" Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblView" runat="server" Text="View"></asp:Label>
                                                <asp:CheckBox ID="chkView" runat="server" AutoPostBack="true" OnCheckedChanged="chkView_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxView1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Go_Right" ItemStyle-Width="3%" Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblGo" runat="server" Text="Go"></asp:Label>
                                                <asp:CheckBox ID="chkGo" runat="server" AutoPostBack="true" OnCheckedChanged="chkGo_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxGo1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Send Mail" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%" SortExpression="SendMail_Right" Visible="false" >
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSendMail" runat="server" Text="Send Mail"></asp:Label>
                                                <asp:CheckBox ID="chkSendMail" runat="server" AutoPostBack="true" OnCheckedChanged="chkSendMail_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxSendMail1" runat="server"  />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                                </asp:Panel>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="Button1_Click" />
                        </td>                       
                    </tr>
                </table>
            </fieldset>
            </div>
        </ContentTemplate>
    <%--</asp:UpdatePanel>--%>
</asp:Content>
