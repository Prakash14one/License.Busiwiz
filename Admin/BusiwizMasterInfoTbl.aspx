<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="BusiwizMasterInfoTbl.aspx.cs" Inherits="Admin_BusiwizMasterInfoTbl" Title="BusiwizMaster Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<head >
    <title>BusiwizMaster Information</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 148px;
        }
        .style3
        {
            width: 79px;
        }
        .style4
        {
            width: 79px;
            height: 33px;
        }
        .style5
        {
            width: 148px;
            height: 33px;
        }
        .style6
        {
            height: 33px;
        }
        .style7
        {
            width: 79px;
            height: 37px;
        }
        .style8
        {
            width: 148px;
            height: 37px;
        }
        .style9
        {
            height: 37px;
        }
        .style10
        {
            width: 79px;
            height: 26px;
        }
        .style11
        {
            width: 148px;
            height: 26px;
        }
        .style12
        {
            height: 26px;
        }
        .style13
        {
            width: 79px;
            height: 31px;
        }
        .style14
        {
            width: 148px;
            height: 31px;
        }
        .style15
        {
            height: 31px;
        }
        .style16
        {
            width: 79px;
            height: 30px;
        }
        .style17
        {
            width: 148px;
            height: 30px;
        }
        .style18
        {
            height: 30px;
        }
        .style19
        {
            width: 236px;
        }
    </style>
</head>
<body>
    
    <div>
    
        <table class="style1">
            <tr>
                <td class="style16">
                </td>
                <td class="style17">
                </td>
                <td class="style18">
                </td>
            </tr>
            <tr>
                <td class="style16">
                </td>
                <td class="style17">
                    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:48pt" width="64">
                        <colgroup>
                            <col style="width:48pt" width="64" />
                        </colgroup>
                        <tr height="20" style="height:15.0pt">
                            <td height="20" width="64">
                                Name</td>
                        </tr>
                    </table>
                </td>
                <td class="style18">
                    <asp:TextBox ID="txtname" runat="server" Width="166px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style13">
                </td>
                <td class="style14">
                    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:48pt" width="64">
                        <colgroup>
                            <col style="width:48pt" width="64" />
                        </colgroup>
                        <tr height="20" style="height:15.0pt">
                            <td height="20" width="64">
                                LogoURL</td>
                        </tr>
                    </table>
                </td>
                <td class="style15">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="164px" /><asp:Button 
                                                                                ID="Butsubmit" runat="server" Text="Submit" width="65px" 
                                                                                onclick="Butsubmit_Click" />
                </td>
            </tr>
            <tr>
                <td class="style10">
                </td>
                <td class="style11">
                    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:48pt" width="64">
                        <colgroup>
                            <col style="width:48pt" width="64" />
                        </colgroup>
                        <tr height="20" style="height:15.0pt">
                            <td height="20" width="64">
                                PaypalID</td>
                        </tr>
                    </table>
                </td>
                <td class="style12">
                    <asp:TextBox ID="txtpaypalid" runat="server" Width="166px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:96pt" width="128">
                        <colgroup>
                            <col span="2" style="width:48pt" width="64" />
                        </colgroup>
                        <tr height="20" style="height:15.0pt">
                            <td colspan="2" height="20" width="128">
                                PaypalNotifyURL</td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:TextBox ID="txtpaypalnotifyurl" runat="server" Width="166px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style4">
                </td>
                <td class="style5">
                    PaypalCancelURL</td>
                <td class="style6">
                    <asp:TextBox ID="txtpaypalcancelurl" Width="166px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style7">
                </td>
                <td class="style8">
                    PaypalReturnURL</td>
                <td class="style9">
                    <asp:TextBox ID="txtpaypalreturnurl" runat="server" Width="166px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style4">
                </td>
                <td class="style5">
                    PaymentNotifyURL</td>
                <td class="style6">
                    <asp:TextBox ID="txtpaypalpaymenturl" Width="166px" runat="server" ></asp:TextBox>
                </td>
            </tr>
        </table>
    
    </div>
    <table class="style1">
        <tr>
            <td>
                &nbsp;</td>
            <td class="style19">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
   
</body>
</asp:Content>

