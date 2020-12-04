<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" AutoEventWireup="true" CodeFile="Communicationreport.aspx.cs" Inherits="Admin_Communicationreport" Title="Untitled Page" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="javascript" type="text/javascript">
function test()
{
    window.print();
}
function test2()
{
    var oContent = document.getElementById("testDiv").innerHTML;
    var a;
    a="<html><head><title>Report</title></head><body>";
    a= a + oContent;
    a= a+"</body></html> ";
}
function test3()
{
 var printContent = document.getElementById("testDiv");
 var windowUrl = 'about:blank';
 var uniqueName = new Date();
 var windowName = 'Print' + uniqueName.getTime();
 var printWindow = window.open(windowUrl, windowName,'left=0000,top=0000,width=0,height=0');
//'left=50000,top=50000,width=0,height=0'
 printWindow.document.write("<html><head><title>Report</title></head><body style='font-size: 12px;'>");
 printWindow.document.write(printContent.innerHTML);
 printWindow.document.write("</body></html> ");
 printWindow.document.close();
 printWindow.focus();
 printWindow.print();
 printWindow.close();
}
</script>
    <p>
    <br />    
    <table style="width: 100%">
        <tr>
            <td colspan="2" 
                style="height: 25px; background-color: #ccccff; color: black; text-align: center;">
                <b style="color: #000000">Communication Report</b></td>
        </tr>
        <tr>
            <td style="height: 37px; width: 946px;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButtonList ID="rdcrselection" runat="server" Font-Bold="True" 
                    RepeatDirection="Horizontal" AutoPostBack="True" 
                    OnSelectedIndexChanged="rdcrselection_SelectedIndexChanged" Width="875px">
                    <asp:ListItem>Date Wise</asp:ListItem>
                    <asp:ListItem>Reminder Date Wise</asp:ListItem>
                    <asp:ListItem>Party Type /Party Name Wise</asp:ListItem>
                    <asp:ListItem>Period Wise</asp:ListItem>
                </asp:RadioButtonList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td style="height: 37px">
                </td>
        </tr>
        <tr>
            <td style="height: 19px; width: 946px;" align="center">

                <asp:Panel ID="paneldate" runat="server" Font-Bold="True" Height="163px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="lblcommtype" runat="server" Font-Bold="True" 
                        Text="Communication Type:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlcommtype" runat="server" Width="200px" 
                        Font-Bold="True">
                        <asp:ListItem>-Select-</asp:ListItem>
                        <asp:ListItem>Incoming/Outgoing Phone</asp:ListItem>
                        <asp:ListItem>Incoming/Outgoing Visit</asp:ListItem>
                        <asp:ListItem>Incoming/Outgoing Fax</asp:ListItem>
                        <asp:ListItem>Incoming/Outgoing Email</asp:ListItem>
                        <asp:ListItem>Incoming/Outgoing Postmail</asp:ListItem>
                        <asp:ListItem>Message</asp:ListItem>
                        <asp:ListItem>Test</asp:ListItem>
                        <asp:ListItem>All</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                    <asp:Label ID="lbldatewise" runat="server" Font-Bold="True" Text="Date Wise:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="From :" Font-Bold="True" 
                        Font-Size="10pt"></asp:Label>
                    <asp:TextBox ID="txtFromdate" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server" 
                        PopupButtonID="txtfromdate" TargetControlID="txtFromdate">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="txtFromdate_MaskedEditExtender" runat="server" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromdate">
                    </cc1:MaskedEditExtender>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label2" runat="server" Text="To :" Font-Bold="True" 
                        Font-Size="10pt"></asp:Label>
                    <asp:TextBox ID="txtTodate" runat="server" Height="19px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server" 
                        PopupButtonID="txtTodate" TargetControlID="txtTodate">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="txtTodate_MaskedEditExtender" runat="server" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtTodate">
                    </cc1:MaskedEditExtender>
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnviewdate" runat="server" Height="20px" 
                        OnClick="btnviewdate_Click" Text="View" Width="50px" />
                    <asp:Button ID="btndatewiseprint" runat="server" Text="Print" 
                        onclientclick="test3()" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;<br />
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                </asp:Panel>

                </td>
            <td style="height: 19px">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
        </tr>
        
        <tr>
            <td style="height: 19px; width: 946px;" align="center">

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
                <br />

                <br />

                <asp:Panel ID="panelperiod" runat="server">
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                    <asp:Label ID="lblperiod" runat="server" Font-Bold="True" Font-Size="10pt" 
                        Text="Period :"></asp:Label>
                    &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:DropDownList ID="ddlperiodwise" runat="server" 
                        DataTextField="Period_name" DataValueField="Periodmaster_id" Height="24px" 
                        Width="145px">
                        <asp:ListItem>-Select-</asp:ListItem>
                        <asp:ListItem>Today</asp:ListItem>
                        <asp:ListItem>Yesterday</asp:ListItem>
                        <asp:ListItem>This Week</asp:ListItem>
                        <asp:ListItem>This Month</asp:ListItem>
                        <asp:ListItem>This Quarter</asp:ListItem>
                        <asp:ListItem>This Year</asp:ListItem>
                        <asp:ListItem>Last Week</asp:ListItem>
                        <asp:ListItem>Last Month</asp:ListItem>
                        <asp:ListItem>Last Quarter</asp:ListItem>
                        <asp:ListItem>Last Year</asp:ListItem>
                        <asp:ListItem>All</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
                        ID="btnviewperiod" runat="server" Height="20px" OnClick="btnviewperiod_Click" 
                        Text="View" Width="50px" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btndatewiseprint0" runat="server" Text="Print" 
                        onclientclick="test3()" />
                    <br />
                    &nbsp;<br />
                </asp:Panel>

                </td>
            <td style="height: 19px">
                </td>
        </tr>
        
        <tr>
            <td style="height: 62px; width: 946px;" align="center">

                <asp:Panel ID="panelparty" runat="server" Height="402px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblcommtype0" runat="server" 
                        Font-Bold="True" Text="Communication Type:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlcommtype1" runat="server" Font-Bold="True" 
                        Width="215px" AutoPostBack="True">
                        <asp:ListItem>-Select-</asp:ListItem>
                        <asp:ListItem>Incoming/Outgoing Fax</asp:ListItem>
                        <asp:ListItem>Incoming/Outgoing Email</asp:ListItem>
                        <asp:ListItem>Incoming/Outgoing Post mail</asp:ListItem>
                        <asp:ListItem>Incoming/Outgoing Phone</asp:ListItem>
                        <asp:ListItem>All</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblparty0" runat="server" Font-Bold="True" Font-Size="10pt" 
                        Text="Party Type:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlpartytype" runat="server" AutoPostBack="True" 
                        DataTextField="PartyTypeName" DataValueField="PartyTypeId" Font-Bold="True" 
                        onselectedindexchanged="ddlpartytype_SelectedIndexChanged" Width="150px">
                        <asp:ListItem>-Select-</asp:ListItem>
                        <asp:ListItem>All</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblparty" runat="server" Font-Bold="True" Font-Size="10pt" 
                        Text="Party Name :"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlpartyname" runat="server" EnableTheming="True" 
                        Width="400px" Font-Bold="True">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButtonList ID="rdcrselection1" runat="server" AutoPostBack="True" 
                        Font-Bold="True" OnSelectedIndexChanged="rdcrselection1_SelectedIndexChanged1" 
                        RepeatDirection="Horizontal" Width="400px">
                        <asp:ListItem>Date Wise</asp:ListItem>
                        <asp:ListItem>Remainder Date Wise</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Panel ID="Panel1" runat="server" Height="36px">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lbldatewise2" runat="server" Font-Bold="True" Text="Date Wise:"></asp:Label>
                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="10pt" 
                            Text="From :"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtFromdate4" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFromdate4_CalendarExtender" runat="server" 
                            PopupButtonID="txtfromdate" TargetControlID="txtFromdate4">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="txtFromdate4_MaskedEditExtender" runat="server" 
                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromdate4">
                        </cc1:MaskedEditExtender>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="10pt" 
                            Text="To :"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtTodate4" runat="server" Height="19px"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtTodate4_CalendarExtender" runat="server" 
                            PopupButtonID="txtTodate" TargetControlID="txtTodate4">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="txtTodate4_MaskedEditExtender" runat="server" 
                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtTodate4">
                        </cc1:MaskedEditExtender>
                        <br />
                        <br />
                    </asp:Panel>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; 
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Panel ID="Panel2" runat="server">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lbldatewise3" runat="server" Font-Bold="True" 
                            Text="Remainder Date Wise:"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="10pt" 
                            Text="From :"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtFromdate5" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFromdate5_CalendarExtender" runat="server" 
                            PopupButtonID="txtfromdate" TargetControlID="txtFromdate5">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="txtFromdate5_MaskedEditExtender" runat="server" 
                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromdate5">
                        </cc1:MaskedEditExtender>
                        <cc1:CalendarExtender ID="txtFromdate5_CalendarExtender2" runat="server" 
                            PopupButtonID="txtfromdate" TargetControlID="txtFromdate5">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="txtFromdate5_MaskedEditExtender2" runat="server" 
                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromdate5">
                        </cc1:MaskedEditExtender>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="10pt" 
                            Text="To :"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtTodate5" runat="server" Height="19px"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtTodate5_CalendarExtender" runat="server" 
                            PopupButtonID="txtTodate" TargetControlID="txtTodate5">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="txtTodate5_MaskedEditExtender" runat="server" 
                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtTodate5">
                        </cc1:MaskedEditExtender>
                        <cc1:CalendarExtender ID="txtTodate5_CalendarExtender2" runat="server" 
                            PopupButtonID="txtTodate" TargetControlID="txtTodate5">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="txtTodate5_MaskedEditExtender2" runat="server" 
                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtTodate5">
                        </cc1:MaskedEditExtender>
                    </asp:Panel>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnviewpartyr" runat="server" Height="20px" 
                       Text="View" Width="50px" onclick="btnviewpartyr_Click" />
                    &nbsp;<asp:Button ID="btnrdatewiseprint0" runat="server" Text="Print" 
                        onclientclick="test3()" />
                </asp:Panel>

                </td>
            <td style="height: 62px">
                </td>
        </tr>
        
         <tr>
            <td style="text-align: center; height: 141px;" colspan="2">

                <asp:Panel ID="panelremainderdate" runat="server" Font-Bold="True" 
                    Height="121px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                    <br />
                    <asp:Label ID="lbldatewise0" runat="server" Text="Remainder Date Wise:" 
                        Font-Bold="True"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="10pt" 
                        Text="From :"></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="txtFromdate1" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtFromdate1_CalendarExtender" runat="server" 
                        PopupButtonID="txtfromdate" TargetControlID="txtFromdate1">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="txtFromdate1_MaskedEditExtender" runat="server" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromdate1">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="txtFromdate1_CalendarExtender0" runat="server" 
                        PopupButtonID="txtfromdate" TargetControlID="txtFromdate1">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="txtFromdate1_MaskedEditExtender0" runat="server" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromdate1">
                    </cc1:MaskedEditExtender>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label6" runat="server" Text="To :" Font-Bold="True" 
                        Font-Size="10pt"></asp:Label>
                    <asp:TextBox ID="txtTodate1" runat="server" Height="19px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtTodate1_CalendarExtender" runat="server" 
                        PopupButtonID="txtTodate" TargetControlID="txtTodate1">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="txtTodate1_MaskedEditExtender" runat="server" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtTodate1">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="txtTodate1_CalendarExtender0" runat="server" 
                        PopupButtonID="txtTodate" TargetControlID="txtTodate1">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="txtTodate1_MaskedEditExtender0" runat="server" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtTodate1">
                    </cc1:MaskedEditExtender>
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnviewremainder" runat="server" Height="20px" 
                        OnClick="btnviewremainder_Click" Text="View" Width="50px" />
                    &nbsp;<asp:Button ID="btnrdatewiseprint" runat="server" Text="Print" 
                        onclientclick="test3()" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </asp:Panel>

                </td>
        </tr>
        
         <tr>
            <td style="text-align: center; height: 141px;" colspan="2">

                &nbsp;</td>
        </tr>
        </table>
        
        <table style="width:50%">
        <tr>
        <td>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                AutoDataBind="true" ReportSourceID="CrystalReportSource1"          />
            
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="reports\CommunicationReport.rpt">
            
            </Report>
            </CR:CrystalReportSource>
        
        </td>
        </tr>
        
        
       </table>
</p>
<p>
    &nbsp;</p>
<p>
    &nbsp;</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
</asp:Content>

