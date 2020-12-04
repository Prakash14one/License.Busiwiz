<%@ Page Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true"
    CodeFile="InsertPermenatalybanSilent.aspx.cs" Inherits="AccessRight" Title="Page Access Right" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
<style type="text/css">


.Labelcss {
    background-color: #f1f1f1;
    font-family: Times New Roman; 
     color: #666; 
     font-size:18px; 
     letter-spacing:inherit; 
}

h1 { color: #ff4411; font-size: 18px; font-family: 'Signika', sans-serif; padding-bottom: 10px; }



</style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box" style="min-height:400px;">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td align="center" >
                                <h2 style="text-align: center">
                                    <asp:Label ID="Label1a" runat="server" Text="Notice"></asp:Label>
                                </h2>
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                            </td>
                        </tr>
                        <tr>
                        <td><br />
                                <asp:Label ID="Label1" runat="server" Text="" 
                                style="font-size:14px"></asp:Label>
                        </td>
                        </tr>
                        <tr>
                        <td>
                               <asp:Panel runat="server" ID="Pnl1" Width="100%" Visible="false">
                                <table>
                                <tr>
                                <td valign="top">
                                    <label>
                                    Reason For Globally ban IP/Mac Address
                                    </label> 
                                </td>
                                <td>
                                
                                </td>
                                <td align="left" valign="top">
                                <label>
                                        <asp:TextBox ID="txtdescription" runat="server" Height="60px" Width="450px" TextMode="MultiLine"></asp:TextBox>
                                </label> 
                                </td>
                                </tr>
                                <tr>
                                <td align="right">
                                <asp:Button ID="Button1" runat="server" Text="Send" CssClass="btnSubmit" OnClick="Button1_Click" />
                                </td>
                                <td></td>
                                <td></td>
                                </tr>
                                </table> 
                                </asp:Panel>
                        </td>
                        </tr>
                    </table>                   
                   <table>
                   <tr>
                   <td>
                   
                   </td>
                   </tr>
                   </table> 
                    
                        
                   
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
