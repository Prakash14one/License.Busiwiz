<%@ Page Title="" Language="C#" MasterPageFile="~/afterloginMasterPage.master" AutoEventWireup="true" CodeFile="ProductProfile.aspx.cs" Inherits="ProductProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor" TagPrefix="cc2" %>
<%@ Register TagPrefix="obshow" Namespace="OboutInc.Show" Assembly="obout_Show_Net" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table id="tblterms" cellpadding="0" cellspacing="3" style="font-size: 12Px" width="100%">
    <tr>
        <td colspan="4" align="center" style="font-size: 14px; font-weight: normal;">
            
        </td>
    </tr>
    <tr>
        <td colspan="4" style="font-weight: bold; color: #3366CC; font-size: 14px;" align="center">
            Product Profile
        </td>        
    </tr>
    <tr>
        <td colspan="4" 
            style="font-weight: bold; color: #3366CC; font-size: 14px; height: 21px;">          
        </td>
    </tr>
    <tr>
        <td style="width:25%; height: 18px;"></td>
        <td class="col1" style="width:25%; height: 18px;">Product Logo :</td>
        <td class="column2" style="width:25%; height: 18px;">
        
           <%-- <asp:Label ID="lblprofile" runat="server" Text="Label">
            </asp:Label>--%>
            
            <asp:ImageButton ID="ImageButton1" runat="server" Width="100px" Height="50px" />
            
        </td>
        <td style="width:25%; " rowspan="4" align="center">
            <%--<asp:Image ID="Image7" runat="server" Height="100px" Width="100px" ImageUrl="~/images/20080707.jpg"/>--%>
             <obshow:Show id="Show1" runat="server" 
                                                       
                     ScrollDirection="top" ShowType="show" TransitionType="Fading" height="200"
                     StopScrolling="True"  FixedScrolling="True" width="200" >
                                                            
                  <Changer ArrowType="Side1" HorizontalAlign="Center" Position="Bottom" Type="Arrow"
                       VerticalAlign="Middle">
                        </Changer>
             </obshow:Show>      
        </td>
    </tr>
    <tr>
        <td style="width:25%;"></td>
        <td class="col1" style="width:25%;">Product Name :</td>
        <td style="width:25%;">
            <asp:Label ID="lblproduct" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width:25%;"></td>
        <td style="width:25%;">Product Description :</td>
        <td style="width:25%;">
            <asp:Label ID="lblproductdescription123" runat="server"></asp:Label>
        </td>
    </tr>    
    <tr>
        <td style="width:25%; height: 21px;"></td>
        <td class="col1" style="width:25%; height: 21px;" valign="top">
        List of Product Feature </td>
        <td class="column2" style="width:25%; height: 21px;">
            </td>
    </tr>
    <tr>
        <td style="width:25%;"></td>
        <td class="col1" valign="top" colspan="2" rowspan="3">
            <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="GridBack"
                AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="Feature">
                    <ItemTemplate>
                        <asp:Label ID="lblfeature" runat="server" Text='<%#bind("productfeature") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Feature Description">
                    <ItemTemplate>
                        <asp:Label ID="lblfeaturedesc" runat="server" Text='<%#bind("feturedisc") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
             <PagerStyle CssClass="GridPager" />
               <HeaderStyle CssClass="GridHeader" />
               <AlternatingRowStyle CssClass="GridAlternateRow" />
               <FooterStyle CssClass="GridFooter" />
               <RowStyle CssClass="GridRowStyle" />
            </asp:GridView>
        </td>
        <td style="width:25%;">
            <table width="100%">
                <tr align="center">
                    <td>
                        <asp:Image ID="Image1" runat="server"  Width="22" Height="22"/>
                        </td>
                    <td><asp:Image ID="Image2" runat="server"  Width="22" Height="22"/></td>
                    <td><asp:Image ID="Image3" runat="server"  Width="22" Height="22"/></td>
                </tr>
                <tr align="center">
                    <td><asp:Image ID="Image4" runat="server"  Width="22" Height="22"/></td>
                    <td><asp:Image ID="Image5" runat="server"  Width="22" Height="22"/></td>
                    <td><asp:Image ID="Image6" runat="server"  Width="22" Height="22"/></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width:25%;"></td>
        <td style="width:25%;"></td>
    </tr>
    <tr>        
        <td style="width:25%;"></td>
        <td style="width:25%;"></td>   
    </tr>        
    <tr>        
        <td colspan="4"></td>        
    </tr>
    <tr>       
        <td colspan="4">
                    
        </td>       
    </tr>
</table>

</asp:Content>

