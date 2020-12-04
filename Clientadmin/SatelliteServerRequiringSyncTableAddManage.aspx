<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="SatelliteServerRequiringSyncTableAddManage.aspx.cs" Inherits="UserRoleforePriceplan" Title="Satellite Server's Requiring Sync with Master Server" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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


    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend>
                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                </legend>
                <div style="clear: both;">
                </div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <label>
                                 <asp:Label ID="Label2" runat="server" Text="Here you can define which tables of satellite server product needs to be synchronized with what tables of license"></asp:Label>                                
                            </label> 
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="2">
                                <label style="width:100%;">
                            Product which is used as master satellite server product and which is installed as satellite server on client servers / PC
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProductname" ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlProductname" runat="server"  AutoPostBack="True" Width="250px"  OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged" Enabled="false">
                                </asp:DropDownList>
                                </label>
                        </td>
                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <label style="width:430px;">
                             Master satellite database whose table required to be synchronized 
                                <asp:DropDownList ID="ddlcodetype" runat="server" AutoPostBack="True" Width="250px" onselectedindexchanged="ddlcodetype_SelectedIndexChanged" Enabled="false">
                                        </asp:DropDownList>
                            </label>
                        </td>                     
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk_active" runat="server" Text="Show checked only" AutoPostBack="true" oncheckedchanged="chk_active_CheckedChanged" checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width:100%">
                                            <asp:Panel ID="pnlpr" runat="server" ScrollBars="Horizontal" Width="100%"  Height="600px" Enabled="false">
                                                <asp:GridView ID="GV_ServerTable" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                                                    OnRowCommand="GV_ServerTable_RowCommand" EmptyDataText="There is no data." AllowSorting="True"
                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    Width="100%" OnPageIndexChanging="GV_ServerTable_PageIndexChanging" OnSorting="GV_ServerTable_Sorting"
                                                  OnRowDataBound="GV_ServerTable_RowDataBound"   OnSelectedIndexChanged="GV_ServerTable_SelectedIndexChanged">
                                                    <Columns>
                                                      <asp:TemplateField HeaderStyle-Width="8%">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="cbHeader" runat="server" OnCheckedChanged="ch1_chachedChanged" AutoPostBack="true" />
                                                                <asp:Label ID="check" runat="server" ForeColor="White" Text="Synchronise Required"   />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbItem"  runat="server" />                                                               
                                                                <asp:CheckBox ID="chkdef"  runat="server" Visible="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Database Name" SortExpression="CodeTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="16%" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_database" runat="server" Text='<%# Bind("CodeTypeName") %>'></asp:Label>                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Satellite Server Table Name" SortExpression="TableName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="16%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Tablename" runat="server" Text='<%# Bind("TableName") %>'></asp:Label>
                                                                <asp:Label ID="lbl_TableID" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderStyle-Width="14%">
                                                                 <HeaderTemplate>
                                                                     <asp:Label ID="lblheader" runat="server" ForeColor="White" Text="Mapped Tables of "  />   
                                                                     <asp:Label ID="lbllicenseproduct" runat="server" ForeColor="White" Text="License.Busiwiz.com"  />   
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="DDLLiceTableName" runat="server" Width="100%" AutoPostBack="false" onselectedindexchanged="DDLgvFKTableName_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                      <asp:Label ID="lbl_LBDAtabaseName" runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="lnl_LBInstance" runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_serverid" runat="server" Visible="false"></asp:Label>
                                                                    <%--AutoPostBack="True" onselectedindexchanged="DDLLiceTableName_SelectedIndexChanged"--%>
                                                                </ItemTemplate>
                                                        </asp:TemplateField>     
                                                         <asp:TemplateField HeaderStyle-Width="8%">
                                                                 <HeaderTemplate>
                                                                     <asp:Label ID="lblheaderpkid" runat="server" ForeColor="White" Text="Select Primary Key Field Name"  />   
                                                                     
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                   <%-- <asp:DropDownList ID="DDLLiceTablepkname" runat="server" Visible="false">
                                                                    </asp:DropDownList> --%>        
                                                                    
                                                                    <asp:TextBox ID="txt_fkname" runat="server" Text=""  Width="100%"></asp:TextBox>                                                          
                                                                </ItemTemplate>
                                                        </asp:TemplateField>                                                   
                                                        <asp:TemplateField HeaderText="Add Query For select this table when syncronice record at sattellite Server" SortExpression="TableName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="45%">
                                                            <ItemTemplate>
                                                               <asp:TextBox ID="txt_addquery" runat="server" Text=""  Width="100%" Height="25px"   TextMode="MultiLine"></asp:TextBox>   
                                                              
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                      
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                             <input runat="server" id="hdnProductDetailId" type="hidden" name="hdnProductDetailId" style="width: 3px" />
                                                <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                                <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                                <input runat="server" id="hdnProductId" type="hidden" name="hdnProductId" style="width: 3px" />
                                        </td>                                                    
                           
                       
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                                        <asp:Button ID="btn_submit" runat="server" OnClick="btnSubmit_Click" Text="Edit" CssClass="btnSubmit"  ValidationGroup="1"  />                                   
                        </td>
                        
                    </tr>
                   
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
