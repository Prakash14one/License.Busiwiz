<%@ Page Language="C#" MasterPageFile="~/Admin/LoginMaster.master"
    AutoEventWireup="true" CodeFile="Page_role_AccessShow.aspx.cs" Inherits="Shoppingcart_Admin_Page_role_Access"
    Title="Show Page Access" %>

<asp:Content ID="Content1" ContentPlaceHolderID="admin" runat="Server">
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
                            <label>
                                <asp:Label ID="Label2" runat="server" Text="Select role to set its access rights"></asp:Label>
                                  <asp:Label ID="Label53" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dpdrolename"
                                    ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </label>
                            <label>
                                <asp:DropDownList ID="dpdrolename" runat="server" AutoPostBack="True" Width="200px"
                                    OnSelectedIndexChanged="dpdrolename_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                       
                    </tr>
                    <tr>
                        <td>
                        <label>
                            <asp:RadioButtonList ID="Rlist" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="Rlist_SelectedIndexChanged"
                                AutoPostBack="True" Width="940px">
                                <asp:ListItem Value="0" Selected="True">Show Menu Access</asp:ListItem>
                                <asp:ListItem Value="1">Show Sub Menu Access</asp:ListItem>
                                <asp:ListItem Value="2" >Show Page Access</asp:ListItem>
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
                                <asp:GridView ID="grdmain" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                 Enabled="false"   PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="MainMenuId"
                                    Width="940px" OnRowCommand="grdmain_RowCommand" AllowSorting="True" 
                                    onsorting="grdmain_Sorting">
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
                                                <asp:RadioButtonList ID="rlheader" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rlheader_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Denied</asp:ListItem>
                                                    <asp:ListItem Value="1">Full</asp:ListItem>
                                                    <asp:ListItem Value="2">Limited</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" SelectedValue='<%#Bind("AccessRight") %>'
                                                    OnSelectedIndexChanged="RadioButtonListgrdmain_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Denied</asp:ListItem>
                                                    <asp:ListItem Value="1">Full</asp:ListItem>
                                                    <asp:ListItem Value="2">Limited</asp:ListItem>
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
                                                <asp:CheckBox ID="CheckBoxEdit1" runat="server" Checked='<%#Bind("Edit_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Delete_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDELETE" runat="server" Text="Delete"></asp:Label>
                                                <asp:CheckBox ID="chkdelete" runat="server" AutoPostBack="true" OnCheckedChanged="chkdelete_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDelete1" runat="server" Checked='<%#Bind("Delete_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Download_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDownload" runat="server" Text="Download"></asp:Label>
                                                <asp:CheckBox ID="chkDownload" runat="server" AutoPostBack="true" OnCheckedChanged="chkDownload_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDownload1" runat="server" Checked='<%#Bind("Download_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Insert_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblInsert" runat="server" Text="Insert"></asp:Label>
                                                <asp:CheckBox ID="chkInsert" runat="server" AutoPostBack="true" OnCheckedChanged="chkInsert_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxInsert1" runat="server" Checked='<%#Bind("Insert_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Update_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblUpdate" runat="server" Text="Update"></asp:Label>
                                                <asp:CheckBox ID="chkUpdate" runat="server" AutoPostBack="true" OnCheckedChanged="chkUpdate_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxUpdate1" runat="server" Checked='<%#Bind("Update_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="View_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblView" runat="server" Text="View"></asp:Label>
                                                <asp:CheckBox ID="chkView" runat="server" AutoPostBack="true" OnCheckedChanged="chkView_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxView1" runat="server" Checked='<%#Bind("View_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Go_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblGo" runat="server" Text="Go"></asp:Label>
                                                <asp:CheckBox ID="chkGo" runat="server" AutoPostBack="true" OnCheckedChanged="chkGo_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxGo1" runat="server" Checked='<%#Bind("Go_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Send Mail" SortExpression="SendMail_Right" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSendMail" runat="server" Text="Send Mail"></asp:Label>
                                                <asp:CheckBox ID="chkSendMail" runat="server" AutoPostBack="true" OnCheckedChanged="chkSendMail_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxSendMail1" runat="server" Checked='<%#Bind("SendMail_Right") %>' />
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
                                  Enabled="false"  PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="SubMenuId"
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
                                                <asp:RadioButtonList ID="rlheader" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rlheader_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Denied</asp:ListItem>
                                                    <asp:ListItem Value="1">Full</asp:ListItem>
                                                    <asp:ListItem Value="2">Limited</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" SelectedValue='<%#Bind("AccessRight") %>'
                                                    OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Denied</asp:ListItem>
                                                    <asp:ListItem Value="1">Full</asp:ListItem>
                                                    <asp:ListItem Value="2" Selected="True">Limited</asp:ListItem>
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
                                                <asp:CheckBox ID="CheckBoxEdit1" runat="server" Checked='<%#Bind("Edit_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Delete_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDELETE" runat="server" Text="Delete"></asp:Label>
                                                <asp:CheckBox ID="chkdelete" runat="server" AutoPostBack="true" OnCheckedChanged="chkdelete_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDelete1" runat="server" Checked='<%#Bind("Delete_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Download_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDownload" runat="server" Text="Download"></asp:Label>
                                                <asp:CheckBox ID="chkDownload" runat="server" AutoPostBack="true" OnCheckedChanged="chkDownload_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDownload1" runat="server" Checked='<%#Bind("Download_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Insert_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblInsert" runat="server" Text="Insert"></asp:Label>
                                                <asp:CheckBox ID="chkInsert" runat="server" AutoPostBack="true" OnCheckedChanged="chkInsert_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxInsert1" runat="server" Checked='<%#Bind("Insert_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Update_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblUpdate" runat="server" Text="Update"></asp:Label>
                                                <asp:CheckBox ID="chkUpdate" runat="server" AutoPostBack="true" OnCheckedChanged="chkUpdate_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxUpdate1" runat="server" Checked='<%#Bind("Update_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="View_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblView" runat="server" Text="View"></asp:Label>
                                                <asp:CheckBox ID="chkView" runat="server" AutoPostBack="true" OnCheckedChanged="chkView_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxView1" runat="server" Checked='<%#Bind("View_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Go_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblGo" runat="server" Text="Go"></asp:Label>
                                                <asp:CheckBox ID="chkGo" runat="server" AutoPostBack="true" OnCheckedChanged="chkGo_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxGo1" runat="server" Checked='<%#Bind("Go_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Send Mail" HeaderStyle-HorizontalAlign="Left" SortExpression="SendMail_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSendMail" runat="server" Text="Send Mail"></asp:Label>
                                                <asp:CheckBox ID="chkSendMail" runat="server" AutoPostBack="true" OnCheckedChanged="chkSendMail_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxSendMail1" runat="server" Checked='<%#Bind("SendMail_Right") %>' />
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
                            <asp:Panel ID="pnlpage" runat="server" Width="940px" Visible="False" ScrollBars="Horizontal">
                                <div style="clear: both;">
                                </div>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Filter by Main Manu"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlmailpagefilter" runat="server" AutoPostBack="True" Width="200px"
                                        OnSelectedIndexChanged="ddlmailpagefilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Filter by Sub Manu"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlsubpagefilter" runat="server" AutoPostBack="True" Width="200px"
                                        OnSelectedIndexChanged="ddlsubpagefilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <div style="clear: both;">
                                </div>
                                <asp:GridView ID="grdpage" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                  Enabled="false"    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="MainMenuId"
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
                                        <asp:TemplateField HeaderText="Page Name" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" SortExpression="PageName">
                                            <ItemTemplate>
                                                <a href="<%#Eval("PageName") %>" target="_blank">
                                                    <asp:Label ID="lblpagename" runat="server" Font-Underline="true" Text='<%#Bind("PageTitle") %>' ForeColor="Black"  ></asp:Label>
                                                </a>
                                                <%--<asp:Label ID="lblpagename" runat="server" Text='<%#Bind("PageName") %>'></asp:Label>--%>
                                                <asp:Label ID="lblpageid" runat="server" Visible="false" Text='<%#Bind("PageId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblInw" runat="server" Text="Page Level Access"></asp:Label>
                                                <asp:RadioButtonList ID="rlheader" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rlheader_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Denied</asp:ListItem>
                                                    <asp:ListItem Value="1">Full</asp:ListItem>
                                                    <asp:ListItem Value="2">Limited</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" SelectedValue='<%#Bind("AccessRight") %>'
                                                    OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Denied</asp:ListItem>
                                                    <asp:ListItem Value="1">Full</asp:ListItem>
                                                    <asp:ListItem Value="2" Selected="True">Limited</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Edit_Right" ItemStyle-Width="3%">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblIn" runat="server" Text="Edit"></asp:Label>
                                                <asp:CheckBox ID="chkedit" runat="server" AutoPostBack="true" OnCheckedChanged="chkedit_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxEdit1" runat="server" Checked='<%#Bind("Edit_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Delete_Right" ItemStyle-Width="5%">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDELETE" runat="server" Text="Delete"></asp:Label>
                                                <asp:CheckBox ID="chkdelete" runat="server" AutoPostBack="true" OnCheckedChanged="chkdelete_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDelete1" runat="server" Checked='<%#Bind("Delete_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Download_Right" ItemStyle-Width="10%">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDownload" runat="server" Text="Download"></asp:Label>
                                                <asp:CheckBox ID="chkDownload" runat="server" AutoPostBack="true" OnCheckedChanged="chkDownload_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDownload1" runat="server" Checked='<%#Bind("Download_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Insert_Right" ItemStyle-Width="5%">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblInsert" runat="server" Text="Insert"></asp:Label>
                                                <asp:CheckBox ID="chkInsert" runat="server" AutoPostBack="true" OnCheckedChanged="chkInsert_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxInsert1" runat="server" Checked='<%#Bind("Insert_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Update_Right" ItemStyle-Width="7%">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblUpdate" runat="server" Text="Update"></asp:Label>
                                                <asp:CheckBox ID="chkUpdate" runat="server" AutoPostBack="true" OnCheckedChanged="chkUpdate_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxUpdate1" runat="server" Checked='<%#Bind("Update_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="View_Right" ItemStyle-Width="5%">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblView" runat="server" Text="View"></asp:Label>
                                                <asp:CheckBox ID="chkView" runat="server" AutoPostBack="true" OnCheckedChanged="chkView_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxView1" runat="server" Checked='<%#Bind("View_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Go_Right" ItemStyle-Width="3%">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblGo" runat="server" Text="Go"></asp:Label>
                                                <asp:CheckBox ID="chkGo" runat="server" AutoPostBack="true" OnCheckedChanged="chkGo_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxGo1" runat="server" Checked='<%#Bind("Go_Right") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Send Mail" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%" SortExpression="SendMail_Right">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSendMail" runat="server" Text="Send Mail"></asp:Label>
                                                <asp:CheckBox ID="chkSendMail" runat="server" AutoPostBack="true" OnCheckedChanged="chkSendMail_chachedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxSendMail1" runat="server" Checked='<%#Bind("SendMail_Right") %>' />
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
                        <td align="center">
                            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="Button1_Click" Visible="false"  />
                        </td>                       
                    </tr>
                </table>
            </fieldset>
            </div>
        </ContentTemplate>
    <%--</asp:UpdatePanel>--%>
</asp:Content>
