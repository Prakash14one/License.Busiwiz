<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" AutoEventWireup="true" CodeFile="myreference.aspx.cs" Inherits="ShoppingCart_Admin_myreferences" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
  <ContentTemplate>

      <table style="width: 100%">
          <tr>
              <td>
                  <asp:Label ID="Label17" runat="server" ForeColor="Red"></asp:Label>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td colspan="4">
                  <asp:Panel ID="pnladd" runat="server" GroupingText="Add New Reference" 
                      Visible="False">
                      <table style="width: 100%">
                          <tr>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <asp:Label ID="Label6" runat="server" Text="Name"></asp:Label>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                      ControlToValidate="txtname" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                      ControlToValidate="txtname" ErrorMessage="Invalid" 
                                      ValidationExpression="^[A-z]+$" ValidationGroup="1"></asp:RegularExpressionValidator>
                              </td>
                              <td width="25%">
                                  <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                              </td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <asp:Label ID="Label7" runat="server" Text="Designation"></asp:Label>
                              </td>
                              <td width="25%">
                                  <asp:TextBox ID="txtdesig" runat="server"></asp:TextBox>
                              </td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <asp:Label ID="Label8" runat="server" Text="Company"></asp:Label>
                              </td>
                              <td width="25%">
                                  <asp:TextBox ID="txtcompany" runat="server"></asp:TextBox>
                              </td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <asp:Label ID="Label9" runat="server" Text="Contact No"></asp:Label>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                      ControlToValidate="txtphone" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                      ControlToValidate="txtphone" ErrorMessage="Invalid" 
                                      ValidationExpression="^[0-9]+$" ValidationGroup="1"></asp:RegularExpressionValidator>
                              </td>
                              <td width="25%">
                                  <asp:TextBox ID="txtphone" runat="server"></asp:TextBox>
                              </td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <asp:Label ID="Label10" runat="server" Text="Email"></asp:Label>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                      ControlToValidate="txtemail" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                      ControlToValidate="txtemail" ErrorMessage="Invalid Email ID" 
                                      ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                      ValidationGroup="1"></asp:RegularExpressionValidator>
                              </td>
                              <td width="25%">
                                  <label><asp:TextBox ID="txtemail" runat="server" AutoPostBack="True" 
                                      ontextchanged="txtemail_TextChanged"></asp:TextBox></label>
                                      <label> <asp:Label ID="Label16" runat="server"></asp:Label></label>
                              </td>
                              <td width="25%">
                                 
                              </td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <asp:Label ID="Label11" runat="server" Text="Address"></asp:Label>
                              </td>
                              <td width="25%">
                                  <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                              </td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%" style="height: 28px">
                                  <asp:Label ID="Label12" runat="server" Text="Country"></asp:Label>
                              </td>
                              <td width="25%" style="height: 28px">
                                  <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="True" 
                                      onselectedindexchanged="ddlcountry_SelectedIndexChanged" 
                                      DataTextField="CountryName" DataValueField="CountryId">
                                  </asp:DropDownList>
                              </td>
                              <td width="25%" style="height: 28px">
                                  </td>
                              <td width="25%" style="height: 28px">
                                  </td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <asp:Label ID="Label13" runat="server" Text="State"></asp:Label>
                              </td>
                              <td width="25%">
                                  <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="True" 
                                      onselectedindexchanged="ddlstate_SelectedIndexChanged" 
                                      DataTextField="StateName" DataValueField="StateId">
                                  </asp:DropDownList>
                              </td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <asp:Label ID="Label14" runat="server" Text="City"></asp:Label>
                              </td>
                              <td width="25%">
                                  <asp:DropDownList ID="ddlcity" runat="server" DataTextField="CityName" 
                                      DataValueField="CityId">
                                  </asp:DropDownList>
                              </td>
                              
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%" colspan="2" style="height: 19px; width: 50%">
                                  <asp:Label ID="Label18" runat="server" 
                                      Text="Which kind of reference would you like to request?"></asp:Label>
                              </td>
                              <td width="25%" style="height: 19px">
                                  </td>
                              <td width="25%" style="height: 19px">
                                  </td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  &nbsp;</td>
                              <td colspan="2" style="width: 50%" width="25%">
                                  <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
                                      onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                                      <asp:ListItem Value="1"> A General/character reference</asp:ListItem>
                                      <asp:ListItem Value="2"> A Reference about your suitability for a specific position</asp:ListItem>
                                  </asp:RadioButtonList>
                              </td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td align="center" colspan="3" style="width: 50%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td colspan="3" style="width: 50%">
                                  <asp:Panel ID="Panel6" runat="server" Visible="False">
                                      <table style="width: 100%">
                                          <tr>
                                              <td>
                                                  <asp:Label ID="Label19" runat="server" 
                                                      Text="Select from your list of Preferred Vacancies the specific position for which you would like to request a reference for:"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td>
                                                  <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                                                      CssClass="mGrid" onrowdatabound="GridView3_RowDataBound">
                                                      <Columns>
                                                          <asp:TemplateField>
                                                              <HeaderTemplate>
                                                                  <asp:Button ID="Button8" runat="server" onclick="Button8_Click" Text="Select" />
                                                              </HeaderTemplate>
                                                              <ItemTemplate>
                                                                  <asp:CheckBox ID="CheckBox2" runat="server" />
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                          <asp:BoundField DataField="Name" HeaderText="Vacancy Type" />
                                                          <asp:BoundField DataField="VacancyPositionTitle" HeaderText="Vacancy Title" />
                                                          <asp:TemplateField HeaderText="ID" >
                                                              <ItemTemplate>
                                                                  <asp:Label ID="Label20" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Add">
                                                              <ItemTemplate>
                                                                  <asp:ImageButton ID="ImageButton50" runat="server" Height="20px" 
                                                                      ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg" onclick="ImageButton50_Click" 
                                                                      Width="20px" />
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Refresh">
                                                              <ItemTemplate>
                                                                  <asp:ImageButton ID="ImageButton51" runat="server" Height="20px" 
                                                                      ImageAlign="Left" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" 
                                                                      onclick="ImageButton51_Click" Width="20px" />
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                      </Columns>
                                                  </asp:GridView>
                                              </td>
                                          </tr>
                                      </table>
                                  </asp:Panel>
                              </td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td colspan="4">
                                  <asp:Label ID="Label37" runat="server" 
                                      Text="Note:Click on Select button before clicking on Submit"></asp:Label>
                              </td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  <asp:Button ID="btnsubmit" runat="server" Text="Submit" 
                                      onclick="btnsubmit_Click" ValidationGroup="1" />
                                  &nbsp;&nbsp;
                                  <asp:Button ID="btncancel" runat="server" onclick="btncancel_Click" 
                                      Text="Cancel" />
                              </td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                      </table>
                  </asp:Panel>
              </td>
          </tr>
          <tr>
              <td colspan="4">
                  <asp:Panel ID="pnlgrid" runat="server" GroupingText="List Of Reference">
                      <table style="width: 100%">
                          <tr>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%" align="right">
                                  <asp:Button ID="btnadd" runat="server" Text="Add New Reference" 
                                      onclick="btnadd_Click" />
                              </td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <asp:Label ID="Label15" runat="server" Text="Status" Visible="False"></asp:Label>
                              </td>
                              <td width="25%">
                                  <asp:DropDownList ID="ddlstatus" runat="server" Visible="False">
                                      <asp:ListItem Value="0">All</asp:ListItem>
                                      <asp:ListItem Value="1">Active</asp:ListItem>
                                      <asp:ListItem Value="2">Inactive</asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td colspan="4">
                                  <asp:Panel ID="Panel5" runat="server">
                                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                          CssClass="mGrid" onrowcommand="GridView1_RowCommand" 
                                          onrowdatabound="GridView1_RowDataBound">
                                          <Columns>
                                              <asp:BoundField DataField="refernceid" HeaderText="ID" />
                                              <asp:BoundField DataField="name" HeaderText="Name" />
                                              <asp:BoundField DataField="designation" HeaderText="Designation" />
                                              <asp:BoundField DataField="contactno" HeaderText="Phone" />
                                              <asp:BoundField DataField="email" HeaderText="Email" />
                                             <%-- <asp:BoundField DataField="Status" HeaderText="Status" />--%>
                                              <asp:TemplateField HeaderText="Response  of Reference Received">
                                                  <ItemTemplate>
                                                      <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("Response") %>' 
                                                          ForeColor="Black" onclick="LinkButton1_Click"></asp:LinkButton>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Delete">
                                                  <ItemTemplate>
                                                      <asp:ImageButton ID="ImageButton49" runat="server" 
                                                          ImageUrl="~/Account/images/delete.gif" onclick="ImageButton49_Click" />
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                          </Columns>
                                      </asp:GridView>
                                  </asp:Panel>
                              </td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                          </tr>
                      </table>
                  </asp:Panel>
              </td>
          </tr>
          <tr>
              <td>
                  <asp:Panel ID="Panel2" runat="server" BackColor="#FFFFCC" BorderColor="#999999" 
                      BorderStyle="Solid" BorderWidth="10px" Height="200px" Width="300px">
                      <table cellpadding="0" cellspacing="0" style="width: 100%">
                          <tr>
                              <td>
                                  <table bgcolor="#CCCCCC" 
                                      style="width: 100%; font-weight: bold; color: #000000;">
                                      <tr>
                                          <td>
                                              &nbsp;</td>
                                          <td align="right">
                                              <asp:ImageButton ID="ImageButton2" runat="server" Height="15px" 
                                                  ImageUrl="~/images/closeicon.jpeg" Width="15px" 
                                                  onclick="ImageButton2_Click" />
                                          </td>
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr>
                              <td>
                                  <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical">
                                      <table width="100%">
                                          <tr>
                                              <td align="left" colspan="2">
                                              </td>
                                          </tr>
                                          <tr>
                                              <td align="center" colspan="2">
                                                  <asp:Label ID="Label36" runat="server" Font-Bold="True" ForeColor="Black" 
                                                      Text="Do You Want Continue?"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td align="center" width="50%">
                                                  &nbsp;</td>
                                              <td align="center" width="50%">
                                                  &nbsp;</td>
                                          </tr>
                                          <tr>
                                              <td align="center" width="50%">
                                                  <asp:Button ID="btnyes" runat="server" Text="Yes" onclick="btnyes_Click" />
                                              </td>
                                              <td align="center" width="50%">
                                                  <asp:Button ID="btnno" runat="server" Text="No" onclick="btnno_Click" />
                                              </td>
                                          </tr>
                                          <tr>
                                              <td align="left" colspan="2">
                                              </td>
                                          </tr>
                                      </table>
                                  </asp:Panel>
                                   <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                    Enabled="True" PopupControlID="Panel2" TargetControlID="Button2">
                                </cc1:ModalPopupExtender>
                                <asp:Button ID="Button2" runat="server" Style="display: none" />
                              </td>
                          </tr>
                      </table>
                  </asp:Panel>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <span style="color: rgb(102, 102, 102); font-family: Calibri, Times, serif; font-size: 16px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: 0.8px; line-height: 24px; orphans: auto; text-align: justify; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">
                  This is Version 1 Updated on 10/28/2015 by Ni</span>thya</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
      </table>

  </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

