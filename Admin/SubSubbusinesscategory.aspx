<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="SubSubbusinesscategory.aspx.cs" Inherits="Admin_SubSubbusinesscategory" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" style="width: 625px">
        <tr>
            <td class="hdng">
                Add/Manage Business Sub Sub Category<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" style="width: 642px">
                  <tbody>
                       <tr>
                            <td style="height: 16px; width: 207px;">
                               Sub Business Category Name :</td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                
                                <asp:DropDownList ID="ddldivicetype" runat="server"  Width="325px" 
                                    Height="18px">
                                                                  
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator  ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddldivicetype" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                        <tr>
                            <td class="column1" style="padding-top: 10px; width: 207px;">
                                Sub&nbsp;
                                Sub Business Category Name :&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:TextBox ID="Txtaddname" runat="server" Width="247px"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txtaddname" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                        <tr>
                            <td class="column1" style="padding-top: 10px; width: 207px;">
                                 Sub Sub Business Category Desc&nbsp;:
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:TextBox ID="txtdesc" runat="server" TextMode="MultiLine" Width="267px" Height="82px"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdesc" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                       <tr>
                            <td class="column1" style="width: 207px">
                            </td>
                            <td class="column2" align="center">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnadd" runat="server" Text="Submit" onclick="btnadd_Click" />
                                    
                            </td>
                            <td class="column2">
                            </td>
                            <td class="column2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblmsg" runat="server" Width="297px" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                            <td class="column2" colspan="1">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
  
    <table id="Table2" cellpadding="0" cellspacing="0">
       
        
        <tr>
            <td class="column2" colspan="4">
                <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Auto" Width="645px">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridBack" DataKeyNames="Id" EmptyDataText="There is no data."
                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                        PageSize="5" onrowdeleting="GridView1_RowDeleting">
                        <Columns>
                            <asp:ButtonField ButtonType="Image" ItemStyle-Width="60px" CommandName="edit1" HeaderText="Edit" ImageUrl="~/images/edit.gif"
                                Text="Button" />
                             <asp:BoundField HeaderText="Sub Business category Name" DataField="subcategoryname" />
                            <asp:BoundField HeaderText="Sub Sub Business  category Name" DataField="subsubcategoryname" />
                            <asp:BoundField HeaderText="Sub Sub Business category Desc" DataField="subsubcategorydesc"  />
                        <asp:ButtonField ButtonType="Button" CommandName="Delete"  ItemStyle-Width="50px"  HeaderText="DELETE" 
                                Text="DELETE" />
                        </Columns>
                        <PagerStyle CssClass="GridPager" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                        <RowStyle CssClass="GridRowStyle" />
                        <FooterStyle CssClass="GridFooter" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="column2" colspan="4">
            </td>
        </tr>
    </table>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div style="border-right: #946702 1px solid; border-top: #946702 1px solid; left: 45%;
                visibility: visible; vertical-align: middle; border-left: #946702 1px solid;
                width: 196px; border-bottom: #946702 1px solid; position: absolute; top: 65%;
                height: 51px; background-color: #ffe29f" id="IMGDIV" align="center" runat="server"
                valign="middle">
                <table width="645px">
                    <tbody>
                        <tr>
                            <td>
                                <asp:Image ID="Image11" runat="server" ImageUrl="~/images/preloader.gif"></asp:Image>
                            </td>
                            <td>
                                <asp:Label ID="lblprogress" runat="server" ForeColor="#946702" Text="Please Wait"
                                    Font-Bold="True" Font-Size="16px" Font-Names="Arial"></asp:Label><br />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>


