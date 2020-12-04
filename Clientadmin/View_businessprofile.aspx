<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="View_businessprofile.aspx.cs" Inherits="BusinessProfile" %>

<%@ Register TagPrefix="cc" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <style>
   .cc
   {
      margin-left:1px;
      padding-left:1px;
   }
   </style>
 
        
                <table style="width: 95% " cellspacing="15px">
                    <tr>
                        <td style="font-size: large; font-weight: bold; text-decoration: underline; font-style: normal; color: #000000">
                            BUSINESS PROFILE</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="business_name" runat="server" Text="Business Name"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                        <td align="left">
                            <asp:Label ID="name" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="business_category" runat="server" Text="Business Category"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                        <td>
                            <asp:Label ID="category" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="country" runat="server" Text="Country"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="countrydata" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="state" runat="server" Text="State"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="statedata" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="city" runat="server" Text="City"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="citydata" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="business_phone" runat="server" Text="Business Phone Number"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="business_phone_number" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="business_mobile" runat="server" Text="Mobile No."></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="business_mobile_phone" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 16px">
                            <asp:Label ID="emailid" runat="server" Text="Email Id"></asp:Label>
                        </td>
                        <td style="height: 16px">
                            <asp:Label ID="email_id" runat="server"></asp:Label>
                        </td>
                        <td style="height: 16px">
                        </td>
                        <td style="height: 16px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="business_detail" runat="server" Text="Business Detail"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="business_details" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
               
         
                   <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" Text="V5" ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>  
</asp:Content>

