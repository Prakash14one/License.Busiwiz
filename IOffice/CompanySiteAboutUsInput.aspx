<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" CodeFile="CompanySiteAboutUsInput.aspx.cs" Inherits="ShoppingCart_Admin_CompanySiteAboutUsInput" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="~/ShoppingCart/Admin/css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 540px;
        }
        .style3
        {
            width: 100%;
        }
        .style7
        {
        }
        .style9
        {
        }
        .style17
        {
            width: 40%;
            height: 68px;
        }
        .style18
        {
            height: 75px;
        }
        </style>
    <script type="text/javascript"  language = "Javascript">
                                      function tbLimit() {

                                          var tbObj = event.srcElement;
                                          if (tbObj.value.length == tbObj.maxLength * 1) return false;

                                      }

                                      function tbCount(visCnt) {
                                          var tbObj = event.srcElement;

                                          if (tbObj.value.length > tbObj.maxLength * 1) tbObj.value = tbObj.value.substring(0, tbObj.maxLength * 1);
                                          if (visCnt) visCnt.innerText = tbObj.maxLength - tbObj.value.length;

                                      }

</script>



     <div style="margin-top: 6px; margin-left: 97px;width: 960px;    min-height:438px;border: 2px solid #1991A9;">
   <%-- <div class="container_middle_main_content_bg">
    <div class= "middle_content_container_1_columns">
    <div class="columns_contents_Inner"> --%>
    <div>
       
        
        <table >
            <tr>
                <td width="50%" >
                        <asp:Panel ID="Panel5" runat="server" Height="417px">
                            <table class="style3">
                                <tr>
                                    <td class="style7" height="100px">
                                        <asp:Panel ID="Panel11" runat="server">
                                            <table style="width: 95%">
                                                <tr>
                                                    <td class="style9">
                                                        <asp:Panel ID="Panel2" runat="server" >
                                                            <table width="100%" style="height: 661px">
                                                                <tr>
                                                                    <td class="style17"  >
                                                                        <table class="style3">
                                                                            <tr>
                                                                                <td  colspan="4">
                                                                                    <asp:Label ID="Label17" runat="server" Text="Label" Visible="False" 
                                                                                        ForeColor="#FF3300"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style18" width="25%">
                                                                                    <asp:Button ID="Button18" runat="server" onclick="Button7_Click" 
                                                                                        Text="Edit Image" />
                                                                                    <br />
                                                                                    <asp:FileUpload ID="FileUpload1" runat="server" Visible="false" />
                                                                                </td>
                                                                                <td class="style18" width="25%">
                                                                                    <br />
                                                                                    <asp:Button ID="Button20" runat="server" onclick="Button8_Click" Text="Upload" 
                                                                                        Visible="false" Width="62px" />
                                                                                </td>
                                                                                <td class="style18" width="25%">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    <asp:Button ID="Button19" runat="server" Text="View Live" Width="100px" 
                                                                                        onclick="Button19_Click" />
                                                                                    <br />
                                                                                </td>
                                                                                <td class="style18" width="25%">
                                                                                    <asp:Button ID="Button16" runat="server" onclick="Button6_Click" Text="Edit" 
                                                                                        Width="100px" />
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="h30" colspan="4">
                                                                                    <asp:Panel ID="Panel12" runat="server" BorderStyle="Ridge">
                                                                                        <table class="style3">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table class="style3">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                <asp:Label ID="Label25" runat="server" Font-Bold="True" Text="Image"></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="Label22" runat="server" Font-Bold="True" Text="About Us"></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                &nbsp;</td>
                                                                                                            <td>
                                                                                                                &nbsp;</td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table class="style3">
                                                                                                        <tr>
                                                                                                            <td height="5cm" width="35%">
                                                                                                                <asp:Image ID="cphMain_ucAboutUs_imgITimeKeeperLogo" runat="server" />
                                                                                                            </td>
                                                                                                            <td height="5cm">
                                                                                                                <br />
                                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" BackColor="White" 
                                                                                                                    BorderColor="White" BorderStyle="Groove" Height="136px" MaxLength="1000" 
                                                                                                                    onkeyup="Count()" ontextchanged="TextBox1_TextChanged" 
                                                                                                                    placeholder="Do not include email, phone number or other contact information" 
                                                                                                                    style="margin-left: 0px" TextMode="MultiLine" Visible="False" Width="665px" 
                                                                                                                   ></asp:TextBox> 
                                                                                                                <asp:Panel ID="Panel13" runat="server" Width="120px" Visible="False">
                                                                                                                    <asp:Label ID="Label1" runat="server">500</asp:Label>
                                                                                                                    <asp:Label ID="Label28" runat="server" Text="of 500"></asp:Label>
                                                                                                                </asp:Panel>
                                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                <asp:Button ID="Button4" runat="server" Height="30px" onclick="submit" 
                                                                                                                    Text="Submit" Visible="false" Width="75px" />
                                                                                                                <%--  </asp:Panel>--%>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="25%">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    </td>
                                                                                <td width="25%">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    </td>
                                                                                <td width="25%" colspan="2" style="width: 70%">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    <asp:Label ID="Label4" runat="server" 
                                                                                        Text="19/2/2016 version2 By db(DineshBabu) "></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <table class="style3">
                                                                                        <tr>
                                                                                            <td height="5cm" width="35%">
                                                                                                &nbsp;</td>
                                                                                            <td height="5cm">
                                                                                                <%-- <asp:Panel ID="Panel16" runat="server" BackColor="White" BorderStyle="None" 
                                                                                                    GroupingText="About Company" Height="234px" style="margin-left: 0px" 
                                                                                                    Width="700px">--%>
                                                                                                &nbsp;</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                &nbsp;</td>
                                                                                            <td>
                                                                                                &nbsp;</td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </asp:Panel>
                </td>
            </tr>
            </table>
   </div>
    </div></div>
    <%--<div> </div></div></div>--%>
   
   </asp:Content>