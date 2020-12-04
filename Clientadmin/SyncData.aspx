<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="SyncData.aspx.cs" Inherits="SyncData" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                        <asp:Label ID="lbllegend" runat="server" Text="Pending synchronisation with client databases."></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:100px;"><label> <asp:Label ID="Label13" runat="server" Text="Filters"></asp:Label></label> 
                                        </td>
                                        <td style="width:100px;">
                                        <asp:CheckBox ID="chkall" runat="server" oncheckedchanged="chkall_CheckedChanged" AutoPostBack="true" />
                                        </td>
                                        <td align="right">
                                        <asp:Button ID="Button2" runat="server" Text="Sync with all servers now" CssClass="btnSubmit" OnClick="btnsync_Click" />
                                 
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlsync" runat="server" Visible="false" >
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label2" runat="server" Text="Product Name/Version"></asp:Label>
                                                    <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProductname"
                                                        ErrorMessage="*" InitialValue="0" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlProductname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label4" runat="server" Text=" Portal Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlportal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpriceplan_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="lblcid" runat="server" Text="Priceplan Category"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlpriceplancatagory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpriceplancatagory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label5" runat="server" Text="Server Name"></asp:Label>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:DropDownList ID="ddlserver" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                        </tr>
                                          <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label14" runat="server" Text="Sync requiring table name"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlsynctable" runat="server" >
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label15" runat="server" Text="From date"></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="txtfdate" runat="server" Width="80px"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="imgfd" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                            </label>
                                                            <label>
                                                                <cc1:CalendarExtender ID="cd1" runat="server" PopupButtonID="imgfd"
                                                                    TargetControlID="txtfdate">
                                                                </cc1:CalendarExtender>
                                                            </label>
                                                        </td>
                                                        <td>
                                                              <label>
                                                                <asp:Label ID="Label16" runat="server" Text="To date"></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="txttodate" runat="server" Width="80px"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="imgtodate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                            </label>
                                                            <label>
                                                                <cc1:CalendarExtender ID="cedate" runat="server" PopupButtonID="imgtodate"
                                                                    TargetControlID="txttodate">
                                                                </cc1:CalendarExtender>
                                                            </label>
                                                           
                                                        </td>
                                                   <%-- </tr>
                                        <tr>
                                            <td>
                                            </td>--%>
                                            <td>
                                                <input id="hdnsortExp1" type="hidden" name="hdnsortExp1" style="width: 3px" runat="Server" />
                                                <input id="hdnsortDir1" type="hidden" name="hdnsortDir1" style="width: 3px" runat="Server" />
                                                <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                                <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                                <input id="hdnPricePlanId" name="hdnPricePlanId" runat="server" type="hidden" style="width: 1px" />
                                                <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" OnClick="btnSubmit_Click"
                                                    Text=" Go " ValidationGroup="1" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnltransst" runat="server" Visible="false">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="lbllegendd" runat="server" Text="List of Server Syncronisation Required"></asp:Label>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grdserver" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                                                        EmptyDataText="No data required to be synchronise right now" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" Width="100%" PageSize="15" AllowPaging="True"
                                                        OnPageIndexChanging="grdserver_PageIndexChanging"  AllowSorting="True" OnSorting="grdserver_Sorting">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Satellite Server Name" SortExpression="ServerName"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("ServerName") %>'></asp:Label>
                                                                    
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Server Location" SortExpression="serverloction" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblserverloction" runat="server" Text='<%# Eval("serverloction") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sync requiring table name" SortExpression="tabdesname"  HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltabname" runat="server" Text='<%# Eval("TableName") %>' Visible="true"></asp:Label>
                                                                    <asp:Label ID="lblsyncreq" runat="server" Text='<%# Eval("tabdesname") %>' Visible="false" ></asp:Label>
                                                                    <asp:Label ID="lblseid" runat="server" Text='<%# Eval("servermasterID") %>' Visible="false"></asp:Label>
                                                                    <%--  <asp:Label ID="lblpoid" runat="server" Text='<%# Eval("PortalId") %>' Visible="false"></asp:Label>--%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sync requested date" SortExpression="DateandTime"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldt" runat="server" Text='<%# Eval("DateandTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Failed Attempt" SortExpression="DateandTime"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmsgatt" runat="server" Text='<%# Eval("Msg") %>'></asp:Label>
                                                                     <asp:Label ID="lblattempt" runat="server" Text='<%# Eval("Attempt") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="cbHeader" runat="server" OnCheckedChanged="ch1_chachedChanged"
                                                                        AutoPostBack="true" Checked="true" />
                                                                    <asp:Label ID="check" runat="server" ForeColor="White" Text="Sync All" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbItem" runat="server" Checked="true" /></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnsync" runat="server" Text="Syncronise" CssClass="btnSubmit" OnClick="btnsync_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlhist" runat="server" Visible="true">
                                <fieldset>
                                    <legend>
                                        <asp:Label ID="Label6" runat="server" Text="Syncronisation History"></asp:Label>
                                    </legend>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label7" runat="server" Text="Filter by Product/Version"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlproductfilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlproductfilter_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label8" runat="server" Text="Filter by Portal"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlportalfilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlportalfilter_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label9" runat="server" Text="Filter by Price Plan Category"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlcategoryfilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcategoryfilter_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label10" runat="server" Text="Filter by Server"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlserverfilter" runat="server" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label12" runat="server" Text="Sync requiring table name"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlsyncreqtbl" runat="server" OnSelectedIndexChanged="ddlsyncreqtbl_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label11" runat="server" Text="Filter by Date"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtStartdate" runat="server" Width="80px"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                            </label>
                                                            <label>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtnEndDate"
                                                                    TargetControlID="txtStartdate">
                                                                </cc1:CalendarExtender>
                                                            </label>
                                                            <label>
                                                                <asp:Button ID="btnfilgo" runat="server" CssClass="btnSubmit" Text=" Go " ValidationGroup="2"
                                                                    OnClick="btnfilgo_Click" /></label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdhistory" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                                                    EmptyDataText="No Record Found." CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                    AlternatingRowStyle-CssClass="alt" Width="100%" AllowSorting="True" AllowPaging="True"
                                                    PageSize="15" OnPageIndexChanging="grdhistory_PageIndexChanging" OnSorting="grdhistory_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Portal" SortExpression="PortalName" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("PortalName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Priceplan Category" SortExpression="CategoryName"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblppcat" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label></ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Satellite Server Name" SortExpression="ServerName"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("ServerName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Server Location" SortExpression="serverloction" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblserverloction" runat="server" Text='<%# Eval("serverloction") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sync. RequiredName" SortExpression="tabdesname" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsyncreq" runat="server" Text='<%# Eval("tabdesname") %>'></asp:Label></ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Requested Date" SortExpression="DateandTime" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldt" runat="server" Text='<%# Eval("DateandTime") %>'></asp:Label></ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actual Date" SortExpression="SynchronisationAttemptDatetime"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldt" runat="server" Text='<%# Eval("SynchronisationAttemptDatetime") %>'></asp:Label></ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" Visible="false" SortExpression="SynchronisationSuccessful"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldt" runat="server" Text='<%# Eval("SynchronisationSuccessful") %>'></asp:Label></ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsyncmsg" runat="server" Text='<%# Eval("SynchronisationAttemptErromMessage") %>'></asp:Label></ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset></asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
