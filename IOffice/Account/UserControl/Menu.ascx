<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Menu" %>
<link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />
<%--<link href="../../css/format.css" rel="stylesheet" type="text/css" />--%>
<table cellpadding="3"
    cellspacing="2" id="uctbl">
    <tr>
        <td class="uctopcolumn" style="width: 134px; height: 20px;">
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Account/TemplateStep1.aspx"
              >Template Setup</asp:HyperLink></td>
        <td class="uctopcolumn" style="width: 218px; height: 20px;">
            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Account/Template.aspx" Width="159px"
            >Create Document</asp:HyperLink></td>
                
                <%-- <td class="uctopcolumn" style="width: 113px">
            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Account/TemplateCreate.aspx"
             >Create Template</asp:HyperLink></td>
        <td class="uctopcolumn">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Account/Template.aspx" Width="139px"
               >Select Dynamic Value</asp:HyperLink></td>--%>
                
                 <td class="uctopcolumn" style="width: 200px; height: 20px;">
            <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Account/TemplateStationary.aspx" Width="155px"
                >Select Stationary</asp:HyperLink></td>

        <td class="uctopcolumn" style="height: 20px">
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Account/UseTemplate.aspx" Width="95px"
          >Merge and Send</asp:HyperLink></td>
    </tr>
    <tr>
        <td class="ucbtmcolumn" style="width: 134px">
            Step : 1</td>
        <td class="ucbtmcolumn" style="width: 218px">
            Step : 2</td>
        <%--<td class="ucbtmcolumn" style="width: 113px">
            Step : 3</td>
        <td class="ucbtmcolumn">
            Step : 4</td>--%>
        <td class="ucbtmcolumn" style="width: 200px">
            Step : 3</td>
             <td class="ucbtmcolumn">
            Step : 4</td>
    </tr>
</table>
