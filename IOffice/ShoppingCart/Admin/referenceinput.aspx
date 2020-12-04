<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" AutoEventWireup="true" CodeFile="referenceinput.aspx.cs" Inherits="ShoppingCart_Admin_referenceinput" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>


    
     
        <table style="width: 100%">
            <tr>
                <td>
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" ></asp:Label>
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel3" runat="server" 
            GroupingText="Candidate has requested only a general reference" Visible="False">
     <table style="width:100%">
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label2" runat="server" 
                        Text="The following Candidate has asked for you to provide her/him a reference"></asp:Label>
                </td>
                </tr>
                <tr>
                <td colspan="5">
        <asp:Panel ID="Panel1" runat="server" GroupingText="Candidate Information">
        <table style="width:100%">
            
        <tr>
        <td width="20%" rowspan="5" align="right">
         <asp:Image ID="photo" runat="server" Height="130px" Width="130px" />
           </td>
        <td width="20%">
            <asp:Label ID="Label4" runat="server" Text="Candidate Name "></asp:Label>
        </td>
         <td width="20%">
             :<asp:Label ID="candidatename" runat="server"></asp:Label>
         </td>
          <td width="20%">
              &nbsp;</td> 
          <td width="20%">
          </td> 
        </tr>
        <tr>
        <td width="20%">
            <asp:Label ID="Label5" runat="server" Text="Mobile Number"></asp:Label>
        </td>
         <td width="20%">
             :<asp:Label ID="candphone" runat="server"></asp:Label>
         </td>
          <td width="20%">
              &nbsp;</td> 
          <td width="20%">
          </td> 
        </tr>
        <tr>
        <td width="20%">
            <asp:Label ID="Label1" runat="server" Text="Email ID"></asp:Label>
        </td>
         <td width="20%">
             :<asp:Label ID="candemail" runat="server"></asp:Label>
         </td>
          <td width="20%">
          </td> 
          <td width="20%">
          </td>  
        </tr>
            <tr>
                <td width="20%">
                   
                    <asp:Label ID="Label9" runat="server" Text="City"></asp:Label>
                    ,
                    <asp:Label ID="Label8" runat="server" Text="State"></asp:Label>
                    ,
                    <asp:Label ID="Label7" runat="server" Text="Country"></asp:Label>
                </td>
                <td width="20%">
                    :<asp:Label ID="city" runat="server"></asp:Label>
                    ,
                    <asp:Label ID="state" runat="server"></asp:Label>
                    ,
                    <asp:Label ID="country" runat="server"></asp:Label>
                </td>
                <td width="20%">
                </td>
                <td width="20%">
                </td>
            </tr>
        <tr>
        <td width="20%">
            &nbsp;</td>
         <td width="20%">
             &nbsp;</td>
          <td width="20%">
          </td> 
          <td width="20%">
          </td>  
        </tr>
        </table>
        </asp:Panel>
       </td>
        </tr>
        <tr>
        <td  colspan="5">
       <asp:Panel ID="Panel2" runat="server" GroupingText="General Reference">
       <table style="width:100%">
            <tr>
            <td width="20%">
              <asp:Label ID="Label13" runat="server" Text="Punctuality"></asp:Label>
            </td>
            <td width="20%">
               <asp:DropDownList ID="ddlpunct" runat="server" AutoPostBack="True" 
                                        Width="162px">
                  <asp:ListItem Value="0">NA</asp:ListItem>
                  <asp:ListItem Value="5">Excellent</asp:ListItem>
                  <asp:ListItem Value="4">Very Good</asp:ListItem>
                  <asp:ListItem Value="3">Good</asp:ListItem>
                   <asp:ListItem Value="2">Bad</asp:ListItem>
                   <asp:ListItem Value="1">Not Bad</asp:ListItem>
                  </asp:DropDownList>
            </td>
            <td width="20%">
            </td>
            <td width="20%">
            </td>
            <td width="20%">
            </td>
            </tr>
             <tr>
            <td width="20%">
              <asp:Label ID="Label14" runat="server" Text="Dependability"></asp:Label>
            </td>
            <td width="20%">
             <asp:DropDownList ID="ddldepend" runat="server" AutoPostBack="True" 
                                        Height="19px" Width="162px">
            <asp:ListItem Value="0">NA</asp:ListItem>
                  <asp:ListItem Value="5">Excellent</asp:ListItem>
                  <asp:ListItem Value="4">Very Good</asp:ListItem>
                  <asp:ListItem Value="3">Good</asp:ListItem>
                   <asp:ListItem Value="2">Bad</asp:ListItem>
                   <asp:ListItem Value="1">Not Bad</asp:ListItem>
             </asp:DropDownList>
            </td>
            <td width="20%">
            </td>
            <td width="20%">
            </td>
            <td width="20%">
            </td>
            </tr>
             <tr>
            <td width="20%">
            <asp:Label ID="Label15" runat="server" Text="Integrity"></asp:Label>
            </td>
            <td width="20%">
            <asp:DropDownList ID="ddlinter" runat="server" AutoPostBack="True" 
                                        Width="162px">
            <asp:ListItem Value="0">NA</asp:ListItem>
                  <asp:ListItem Value="5">Excellent</asp:ListItem>
                  <asp:ListItem Value="4">Very Good</asp:ListItem>
                  <asp:ListItem Value="3">Good</asp:ListItem>
                   <asp:ListItem Value="2">Bad</asp:ListItem>
                   <asp:ListItem Value="1">Not Bad</asp:ListItem>
             </asp:DropDownList>
            </td>
            <td width="20%">
            </td>
            <td width="20%">
            </td>
            <td width="20%">
            </td>
            </tr>
            <tr>
                <td width="20%">
                 <asp:Label ID="Label16" runat="server" Text="Work Ethic"></asp:Label>
                </td>
                <td width="20%">
                  <asp:DropDownList ID="ddlwork" runat="server" AutoPostBack="True" Width="162px">
                  <asp:ListItem Value="0">NA</asp:ListItem>
                  <asp:ListItem Value="5">Excellent</asp:ListItem>
                  <asp:ListItem Value="4">Very Good</asp:ListItem>
                  <asp:ListItem Value="3">Good</asp:ListItem>
                   <asp:ListItem Value="2">Bad</asp:ListItem>
                   <asp:ListItem Value="1">Not Bad</asp:ListItem>
                  </asp:DropDownList>
                </td>
                <td width="20%">
                </td>
                <td width="20%">
                </td>
                <td width="20%">
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="Label17" runat="server" Text="Remark"></asp:Label>
                </td>
                <td width="20%">
                    <asp:TextBox ID="TextBox1" runat="server" Height="70px" TextMode="MultiLine" 
                        Width="250px"></asp:TextBox>
                </td>
                <td width="20%">
                </td>
                <td width="20%">
                </td>
                <td width="20%">
                </td>
            </tr>
             <tr>
            <td width="20%">
            </td>
            <td width="20%">
                <asp:Button ID="Button8" runat="server" Text="Submit" onclick="Button8_Click" />
                <asp:Button ID="Button9" runat="server" Text="Clear" onclick="Button9_Click" />
            </td>
            <td width="20%">
            </td>
            <td width="20%">
            </td>
            <td width="20%">
            </td>
            </tr>
            </table>
       </asp:Panel>
       </td>
       </tr>
       </table>
       </asp:Panel>
      </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel4" runat="server" 
                        GroupingText="Candidate has requested a reference for a specific position(s)" 
                        Visible="False">
                        <table style="width:100%">
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="Label3" runat="server" 
                                        Text="The following Candidate has asked for you to provide her/him a reference"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:Panel ID="Panel5" runat="server" GroupingText="Candidate Information">
                                        <table style="width:100%">
                                            <tr>
                                                <td align="right" rowspan="6" width="20%">
                                                    <asp:Image ID="Image1" runat="server" Height="130px" Width="130px" />
                                                </td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <asp:Label ID="Label6" runat="server" Text="Candidate Name "></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td width="20%">
                                                    :<asp:Label ID="candidatename1" runat="server"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <asp:Label ID="Label11" runat="server" Text="Mobile Number"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td width="20%">
                                                    :<asp:Label ID="candphone1" runat="server"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <asp:Label ID="Label18" runat="server" Text="Email ID"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td width="20%">
                                                    :<asp:Label ID="candemail1" runat="server"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                </td>
                                                <td width="20%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <asp:Label ID="Label20" runat="server" Text="City"></asp:Label>
                                                    ,
                                                    <asp:Label ID="Label21" runat="server" Text="State"></asp:Label>
                                                    ,
                                                    <asp:Label ID="Label22" runat="server" Text="Country"></asp:Label>
                                                    :
                                                </td>
                                                <td width="20%">
                                                    :<asp:Label ID="city1" runat="server"></asp:Label>
                                                    ,
                                                    <asp:Label ID="state1" runat="server"></asp:Label>
                                                    ,
                                                    <asp:Label ID="country1" runat="server"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                </td>
                                                <td width="20%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                </td>
                                                <td width="20%">
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:Panel ID="Panel6" runat="server" GroupingText="General Reference">
                                        <table style="width:100%">
                                            <tr>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <asp:Label ID="Label10" runat="server" Text="Punctuality"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:DropDownList ID="ddlpunct1" runat="server" AutoPostBack="True" 
                                                        Width="162px">
                                                        <asp:ListItem Value="0">NA</asp:ListItem>
                                                        <asp:ListItem Value="5">Excellent</asp:ListItem>
                                                        <asp:ListItem Value="4">Very Good</asp:ListItem>
                                                        <asp:ListItem Value="3">Good</asp:ListItem>
                                                        <asp:ListItem Value="2">Bad</asp:ListItem>
                                                        <asp:ListItem Value="1">Not Bad</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="20%">
                                                </td>
                                                <td width="20%">
                                                </td>
                                                <td width="20%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <asp:Label ID="Label12" runat="server" Text="Dependability"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:DropDownList ID="ddldepend1" runat="server" AutoPostBack="True" 
                                                        Height="19px" Width="162px">
                                                        <asp:ListItem Value="0">NA</asp:ListItem>
                                                        <asp:ListItem Value="5">Excellent</asp:ListItem>
                                                        <asp:ListItem Value="4">Very Good</asp:ListItem>
                                                        <asp:ListItem Value="3">Good</asp:ListItem>
                                                        <asp:ListItem Value="2">Bad</asp:ListItem>
                                                        <asp:ListItem Value="1">Not Bad</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="20%">
                                                </td>
                                                <td width="20%">
                                                </td>
                                                <td width="20%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <asp:Label ID="Label19" runat="server" Text="Integrity"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:DropDownList ID="ddlinter1" runat="server" AutoPostBack="True" 
                                                        Width="162px">
                                                        <asp:ListItem Value="0">NA</asp:ListItem>
                                                        <asp:ListItem Value="5">Excellent</asp:ListItem>
                                                        <asp:ListItem Value="4">Very Good</asp:ListItem>
                                                        <asp:ListItem Value="3">Good</asp:ListItem>
                                                        <asp:ListItem Value="2">Bad</asp:ListItem>
                                                        <asp:ListItem Value="1">Not Bad</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="20%">
                                                </td>
                                                <td width="20%">
                                                </td>
                                                <td width="20%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <asp:Label ID="Label23" runat="server" Text="Work Ethic"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:DropDownList ID="ddlwork1" runat="server" AutoPostBack="True" 
                                                        Width="162px">
                                                        <asp:ListItem Value="0">NA</asp:ListItem>
                                                        <asp:ListItem Value="5">Excellent</asp:ListItem>
                                                        <asp:ListItem Value="4">Very Good</asp:ListItem>
                                                        <asp:ListItem Value="3">Good</asp:ListItem>
                                                        <asp:ListItem Value="2">Bad</asp:ListItem>
                                                        <asp:ListItem Value="1">Not Bad</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="20%">
                                                </td>
                                                <td width="20%">
                                                </td>
                                                <td width="20%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                                <td width="20%">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:Panel ID="Panel7" runat="server" 
                                        GroupingText="Candidate's suitability for a specific position">
                                        <table style="width:100%">
                                            <tr>
                                                <td align="left" width="100%">
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                                        CssClass="mGrid" onrowdatabound="GridView1_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Vacancy ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label123" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Name" HeaderText="Position Type" />
                                                            <asp:BoundField DataField="VacancyPositionTitle" HeaderText="Position Title" />
                                                            <asp:TemplateField HeaderText="Checkbox">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="Button11" runat="server" onclick="Button11_Click" 
                                        Text="Submit" />
                                    <asp:Button ID="Button12" runat="server" onclick="Button12_Click" 
                                        Text="Clear" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Panel ID="Panel21" runat="server" BackColor="White" BorderColor="#999999" 
                                        BorderStyle="Solid" BorderWidth="10px" Height="270px" Width="620px">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <table bgcolor="#CCCCCC" 
                                                        style="width: 100%; font-weight: bold; color: #000000;">
                                                        <tr>
                                                            <td>
                                                                Remark For Candidate Preferred Position From Referencer</td>
                                                            <td align="right">
                                                                <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" 
                                                                    ImageUrl="~/images/closeicon.jpeg" Width="15px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnlof" runat="server" ScrollBars="Vertical">
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="50%">
                                                                    <asp:Label ID="Label24" runat="server" 
                                                                        Text="Rate the candidate for this Position"></asp:Label>
                                                                </td>
                                                                <td width="50%">
                                                                    <asp:DropDownList ID="ddlposition" runat="server" Height="22px" Width="161px">
                                                                        <asp:ListItem Value="0">NA</asp:ListItem>
                                                                        <asp:ListItem Value="1">Not Suitable</asp:ListItem>
                                                                        <asp:ListItem Value="2">Suitable</asp:ListItem>
                                                                        <asp:ListItem Value="3">More Suitable</asp:ListItem>
                                                                        <asp:ListItem Value="4">Execellent</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="50%">
                                                                    <asp:Label ID="Label25" runat="server" Text="Remark"></asp:Label>
                                                                </td>
                                                                <td width="50%">
                                                                    <asp:TextBox ID="TextBox2" runat="server" Height="70px" TextMode="MultiLine" 
                                                                        Width="250px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2">
                                                                    <asp:Button ID="Button10" runat="server" onclick="Button10_Click" 
                                                                        Text="Submit" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtenderAddnew" runat="server" 
                                        BackgroundCssClass="modalBackground" Enabled="True" PopupControlID="Panel21" 
                                        TargetControlID="HiddenButton222">
                                    </cc1:ModalPopupExtender>
                                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>


    
     
    </ContentTemplate>
    </asp:UpdatePanel>
           
</asp:Content>

