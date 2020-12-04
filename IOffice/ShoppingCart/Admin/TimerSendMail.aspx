<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="TimerSendMail.aspx.cs" Inherits="Manage_Ip_Address_Allowed" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    ]<style type="text/css">
        .open
        {
	        display:block;
        }
        .closed
        {
	        display:none;
        }
    </style><asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>                    
                     <div style="clear: both;">

                       <asp:Timer ID="Timer1" runat="server" ontick="Timer1_Tick" Interval="2000">
                     </asp:Timer>
             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                     <ContentTemplate>
                         <asp:Label ID="Label1" runat="server" Text="Label" 
                             style="font-weight: 700; font-size: xx-large"></asp:Label>

                          <asp:Label ID="Label2" runat="server" Text="Label" 
                             style="font-weight: 700; font-size: xx-large"></asp:Label>
                   </ContentTemplate>
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
             </asp:UpdatePanel>
                    </div>
        
    </ContentTemplate>
    </asp:UpdatePanel>          
</asp:Content>