<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Defaultafterloginbyrole.aspx.cs" Inherits="UserRoleforePriceplan" Title="UserRole for Priceplan" %>

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
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend>
                    <asp:Label ID="Label4" runat="server" Text="Set Default creation of Deaprtment/ Desgination for price plan"></asp:Label>
                </legend>
                <div style="clear: both;">
                </div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            <label>
                                Select Product/Version :<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server" ControlToValidate="ddlProductname" ErrorMessage="*" InitialValue="0"
                                    ValidationGroup="1"></asp:RequiredFieldValidator></label>
                            <label>
                                <asp:DropDownList ID="ddlProductname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                        <td>
                        </td>
                    </tr>

                     <tr>
                        <td>
                            <label>
                               </label>
                            <label>
                                <asp:DropDownList ID="ddl_priceplan" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged11" Visible="false">
                                </asp:DropDownList>
                            </label>
                        </td>
                        <td>
                        </td>
                    </tr>


                    <tr>
                        <td>
                       
                               <%-- <asp:CheckBox ID="chkallprole" runat="server" OnCheckedChanged="chkallprole_CheckedChanged"
                                    AutoPostBack="True" />--%>
                           
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <input runat="server" id="hdnProductDetailId" type="hidden" name="hdnProductDetailId"
                                style="width: 3px" />
                            <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                            <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                            <input runat="server" id="hdnProductId" type="hidden" name="hdnProductId" style="width: 3px" />
                        </td>
                    </tr>
                  
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel3" runat="server" Width="100%" ScrollBars="Auto">
                                


                                <asp:GridView ID="GridView3" runat="server" Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" EmptyDataText=""
                                                                OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                                                OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                                                                OnRowCancelingEdit="GridView1_RowCancelingEdit1"  >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Department Names" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                    <asp:Label ID="lbl_DeptName" runat="server" Text='<%# Bind("DeptName") %>' ></asp:Label>
                                                <asp:Label ID="txtdeptid" runat="server" Text='<%# Bind("degid") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Designation/Role" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                            <asp:Label ID="lbl_DesignationName" runat="server"  Text='<%# Bind("DesignationName") %>'  ></asp:Label>
                                                            <asp:Label ID="txtdesigid" runat="server" Visible="false" Text='<%# Bind("DeptId") %>'  ></asp:Label>
                                         </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                       
                                      

                                         <asp:TemplateField HeaderText="AfterLogin Page" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="17%">
                                              <ItemTemplate>
                                                     <asp:Label ID="Label5" runat="server" Text='<%# Eval("pagename") %>'></asp:Label>
                                                     <asp:Label ID="txtdesigid2" runat="server" Text='<%# Bind("RoleId") %>' Visible="false"></asp:Label>
                                                  
                                              </ItemTemplate>
                                             <EditItemTemplate>
                                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                                    </asp:DropDownList>
                                             </EditItemTemplate>
                                             <HeaderStyle HorizontalAlign="Left" Width="17%" />
                                           </asp:TemplateField>
                                          <asp:CommandField ShowEditButton="True" HeaderText="Edit" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left" ButtonType="Image" HeaderImageUrl="~/Account/images/edit.gif"
                                                                        EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/UpdateGrid.JPG"
                                                                        CancelImageUrl="~/images/delete.gif">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                          </asp:CommandField>
                                    </Columns>
                                </asp:GridView>

                            </asp:Panel>
                        
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                   
                 
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0" id="Table1" style="width: 100%">
                                <tr>
                                    <td align="center">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                   
                </table>
            </fieldset>


             <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" Text="V2" ForeColor="#416271" Font-Size="10px"></asp:Label>
              </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
