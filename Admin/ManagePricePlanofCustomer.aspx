<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ManagePricePlanofCustomer.aspx.cs" Inherits="ManagePricePlanofCustomer" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table id="pagetbl" cellpadding="0" cellspacing="0">
<tr><td class="hdng">
    Client List : 
    </td></tr>
    <tr>
    <td>
    <asp:GridView id="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="False" 
    DataKeyNames="ClientMasterId" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CssClass="GridBack">
    <Columns>
      <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Edit" ImageUrl="~/images/edit.gif"
                            Text="Button" >
          <HeaderStyle CssClass="theHeader" />
          <ItemStyle CssClass="theHeader" />
      </asp:ButtonField>
    <asp:BoundField  DataField="CompanyName" HeaderText="Company Name" >
        <HeaderStyle CssClass="wideColumn" />
        <ItemStyle CssClass="wideColumn" />
    </asp:BoundField>
        <asp:BoundField  DataField="ContactPersonName" HeaderText="Contact Person Name" >
            <HeaderStyle CssClass="wideColumn" />
            <ItemStyle CssClass="wideColumn" />
        </asp:BoundField>
    <asp:BoundField  DataField="Address1" HeaderText="Address" >
        <HeaderStyle CssClass="wideColumn" />
        <ItemStyle CssClass="wideColumn" />
    </asp:BoundField>
    <asp:BoundField  DataField="Phone1" HeaderText="Phone 1" ></asp:BoundField>
    <asp:BoundField  DataField="Phone2" HeaderText="Phone 2" ></asp:BoundField>

    <asp:BoundField  DataField="Fax1" HeaderText="Fax 1"   ></asp:BoundField>
    <asp:BoundField  DataField="Fax2" HeaderText="Fax 2"   ></asp:BoundField>
    
     <asp:BoundField  DataField="Email1" HeaderText="Email 1"   ></asp:BoundField>
  
    </Columns>
    <PagerStyle CssClass="GridPager" />
    <HeaderStyle CssClass="GridHeader" />
    <AlternatingRowStyle CssClass="GridAlternateRow" />
    <RowStyle CssClass="GridRowStyle" />
    <FooterStyle CssClass="GridFooter" />
    </asp:GridView>
    </td>
    </tr></table>


</asp:Content>

