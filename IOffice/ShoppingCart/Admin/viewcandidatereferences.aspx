<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" AutoEventWireup="true" CodeFile="viewcandidatereferences.aspx.cs" Inherits="ShoppingCart_Admin_viewcandidatereferences" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--   <script language="javascript" type="text/javascript">
  function ChangeCheckBoxState(refernceid, checkstate)
         {

             var chb = document.getElementById(refernceid);
            if (chb != null)
                chb.checked = checkstate;
        }

        function ChangeAllCheckBoxStates(checkstate) 
        {

            if (CheckBoxIDs != null) 
            {
                for (var i = 0; i < CheckBoxIDs.length; i++)
                    ChangeCheckBoxState(CheckBoxIDs[i], checkstate);
            }
        }
        </script>--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <table style="width: 100%">
            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Panel ID="Panel5" runat="server" GroupingText="View Candidate References">
                        <table style="width: 100%">
                            <tr>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <asp:Label ID="Label27" runat="server" Font-Bold="True" 
                                        Text="Candidate Information"></asp:Label>
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <asp:Label ID="Label6" runat="server" Text="Candidate Name"></asp:Label>
                                </td>
                                <td width="25%">
                                    :<asp:Label ID="Label10" runat="server"></asp:Label>
                                </td>
                                <td rowspan="4" width="25%">
                                    <asp:ImageButton ID="ImageButton48" runat="server" Height="120px" 
                                        Width="120px" />
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <asp:Label ID="Label7" runat="server" Text="Candidate Phone No."></asp:Label>
                                </td>
                                <td width="25%">
                                    :<asp:Label ID="Label11" runat="server"></asp:Label>
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <asp:Label ID="Label8" runat="server" Text="Candidate Email Address"></asp:Label>
                                </td>
                                <td width="25%">
                                    :<asp:Label ID="Label12" runat="server"></asp:Label>
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <asp:Label ID="Label9" runat="server" Text="Candidate's Preferred Position(s)"></asp:Label>
                                </td>
                                <td width="25%">
                                    :<asp:Label ID="Label13" runat="server"></asp:Label>
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <asp:Label ID="Label14" runat="server" ForeColor="Black" Text="Average Ranking" Font-Bold="True" ></asp:Label>
                                    :</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <asp:Label ID="Label28" runat="server" Text="Punctuality"></asp:Label>
                                </td>
                                <td width="25%">
                                    :<asp:Label ID="Label15" runat="server"></asp:Label>
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <asp:Label ID="Label29" runat="server" Text="Dependability"></asp:Label>
                                </td>
                                <td width="25%">
                                    :<asp:Label ID="Label16" runat="server"></asp:Label>
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <asp:Label ID="Label30" runat="server" Text="Integrity"></asp:Label>
                                </td>
                                <td width="25%">
                                    :<asp:Label ID="Label17" runat="server"></asp:Label>
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <asp:Label ID="Label31" runat="server" Text="Work Ethic "></asp:Label>
                                </td>
                                <td width="25%">
                                    :<asp:Label ID="Label18" runat="server"></asp:Label>
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%" colspan="2" style="width: 50%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 50%" width="25%">
                                    <asp:Label ID="Label20" runat="server" Font-Bold="True" 
                                        Text="Select Reference Provider For Detailed Evaluation"></asp:Label>
                                    &nbsp;:</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 50%" width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                        CssClass="mGrid" onrowdatabound="GridView1_RowDataBound" PageSize="3" 
                                        Width="80%">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="RadioButton1_CheckedChanged" />
                                                    <%-- <asp:CheckBox ID="chkSelect" runat="server" Checked="true" 
                                                            oncheckedchanged="chkSelect_CheckedChanged"   />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="refernceid" HeaderText="refernceid" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:BoundField DataField="CityName" HeaderText="City Name" />
                                            <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                <asp:Label ID="Label19" runat="server" 
                                        Text=" Note:" Font-Bold="True"></asp:Label> 
                                   <asp:Label ID="Label5" runat="server" 
                                        Text=" Please Select Any Of The Radio Button For Viewing the Reference's Opinion And Ranking" ></asp:Label>  </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Panel ID="Panel6" runat="server" GroupingText="Referencer Opininon" 
                                        Visible="False">
                                        <table style="width: 100%">
                                            <tr>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    <asp:Label ID="Label21" runat="server" Font-Bold="True" 
                                                        Text="Reference's Opinion:"></asp:Label>
                                                </td>
                                                <td width="25%">
                                                    <asp:TextBox ID="TextBox2" runat="server" Height="90px" TextMode="MultiLine" 
                                                        Width="400px"></asp:TextBox>
                                                </td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    <asp:Label ID="Label22" runat="server" Font-Bold="True" ForeColor="Black" 
                                                        Text="Reference's Ranking"></asp:Label>
                                                </td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    <asp:Label ID="Label1" runat="server" Text="Punctuality"></asp:Label> </td>
                                                <td width="25%">
                                                    :<asp:Label ID="Label23" runat="server"></asp:Label>
                                                </td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    <asp:Label ID="Label2" runat="server" Text="Dependability"></asp:Label> 
                                                   </td>
                                                <td width="25%">
                                                    :<asp:Label ID="Label24" runat="server"></asp:Label>
                                                </td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                     <asp:Label ID="Label3" runat="server" Text="Integrity"></asp:Label> </td>
                                                <td width="25%">
                                                    :<asp:Label ID="Label25" runat="server"></asp:Label>
                                                </td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                     <asp:Label ID="Label4" runat="server" Text="Work Ethic "></asp:Label> </td>
                                                <td width="25%">
                                                    :<asp:Label ID="Label26" runat="server"></asp:Label>
                                                </td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
        </table>

    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

