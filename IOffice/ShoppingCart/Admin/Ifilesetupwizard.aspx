<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Ifilesetupwizard.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <asp:Panel ID="panelfilcabinet" runat="server" Width="100%">
                    <fieldset>
                        <table width="100%" cellspacing="0" cellpadding="2">
                            <tr>
                                <td style="width: 85%" colspan="2">
                                    <asp:Label ID="Label55" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Online Filling Cabinet"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="Label56" runat="server" Font-Bold="true" Text="Would you like to go through the Online Filing Cabinet Setup Wizard?"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 5%">
                                    <label>
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel98" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel99" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image28" runat="server" Width="40px" Height="20px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel100" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image29" runat="server" Width="40px" Height="20px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel101" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton31" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label57" runat="server" Font-Bold="true" Text="Part A"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label58" runat="server" Font-Bold="true" Text="Filing Cabinet Setup"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                <label>
                                                    <label>
                                                        Would you like to add a Cabinet to your virtual filing cabinet?
                                                    </label>
                                                    <asp:CheckBox ID="CheckBox8" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox8_CheckedChanged" />
                                                    <br /><br />
                                                    <label>
                                                        Would you like to add a Drawer within your virtual filing cabinet?
                                                    </label>
                                                    <asp:CheckBox ID="CheckBox9" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox9_CheckedChanged" />
                                                    <br /><br />
                                                    <label>
                                                        Would you like to add a Folder within drawers of your virtual filing cabinet?
                                                    </label>
                                                    <asp:CheckBox ID="CheckBox11" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox11_CheckedChanged" />
                                                    <br /><br /><br />
                                                    <label>
                                                        Existing Cabinets,Drawers and Folders :
                                                        <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Black" OnClick="LinkButton3_Click">View</asp:LinkButton>
                                                    </label>
                                                </label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel102" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel103" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image30" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel104" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image31" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel105" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton32" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label59" runat="server" Font-Bold="true" Text="Part B"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label60" runat="server" Font-Bold="true" Text="Auto Retrieval of documents from email/ FTP/ server folder Setup"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                <label>
                                                    <label>
                                                        The online filing cabinet allows you to import documents directly from Email,FTP,
                                                        Server folder. The documents will be added directly to the general cabinet, general
                                                        drawers, general folders. You can also set the fixed and dynamic rule for importing
                                                        of such documents.
                                                     </label><br /><br />
                                                        <label>
                                                            Would you like to setup the auto retrieval rule for the Email now?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox10" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox10_CheckedChanged" />
                                                        <br /><br /><br />
                                                        <label>
                                                            Would you like to setup the auto retrieval rule for the FTP now?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox5" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox5_CheckedChanged" />
                                                        <br /><br /><br />
                                                        <label>
                                                            Would you like to setup the auto retrieval rule for the Folder now?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox7" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox7_CheckedChanged" />
                                                 </label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel106" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel107" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image32" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel108" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image33" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel109" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton33" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label61" runat="server" Font-Bold="true" Text="Part C"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label62" runat="server" Font-Bold="true" Text="Storage Option Setup"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        <label>
                                                        Your online filing cabinet allows only certain amount of storage depending on your
                                                        price plan. Any excess files can be stored on your own FTP server so the files which
                                                        are recently used are kept on our server for your quick and easy view. Older files
                                                        are automatically transfered to your FTP location.
                                                        </label>
                                                        <br />
                                                        <label>
                                                            Would you like to set up storage options for your files and documents?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox6" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox6_CheckedChanged" />
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel110" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel111" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image34" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel112" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image35" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel113" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton34" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label63" runat="server" Font-Bold="true" Text="Part D"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label64" runat="server" Font-Bold="true" Text="Allocation and Document Flow Setup"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                    <label>
                                                        This will ensure that all the newly arrived docuemnts are properly allocated to
                                                        the correct cabinet, drawer and folders and given proper titles. This will ensure
                                                        that the document access rights that you have set for different designation for
                                                        different cabinet, drawer and folders would work correctly. So no sensitive docuement
                                                        confidentiality  is breached.                                                                                                           
                                                    </label>
                                                    <br /> 
                                                    <label>
                                                        Would you like to set up the auto allocation of documents for filing desk departments
                                                        now?
                                                    </label>
                                                    <asp:CheckBox ID="CheckBox4" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox4_CheckedChanged" />
                                                   <br /><br /><br /><br />
                                                    <label>
                                                        Would you like to have all your newly enter documents in the system be properly
                                                        processed by <br />
                                                        your filing desk department before they are made availble for all the users of your
                                                        documents?
                                                    </label>   
                                                    <label>                                               
                                                        <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox3_CheckedChanged" />
                                                    
                                                      </label>
                                                    </label>
                                                    
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel114" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel115" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image36" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel116" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image37" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel117" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton35" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label65" runat="server" Font-Bold="true" Text="Part E"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label66" runat="server" Font-Bold="true" Text="Document Flow Setup"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        You can set up the rules for the flow of documents in the organization for view
                                                        and approval by the selected designation/ users. For example you can have your invoice&#39;s
                                                        sent to the department responsible for paying bills, then sent to the designation
                                                        with signing authority, and finally sent to the accounting department for processing.
                                                        <br />
                                                        <label>
                                                            Would you like to set up document flow now?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox2_CheckedChanged" />
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel118" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel119" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image38" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel120" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image39" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel121" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton36" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label67" runat="server" Font-Bold="true" Text="Part F"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label68" runat="server" Font-Bold="true" Text="Document Access Rights Setup"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        You can set up restriction as to which users have access to which cabinet/ drawer/
                                                        folders of your documents. You can further set access rights to view, to download,
                                                        and the right to delete, etc. Even for the users for whom you have allowed access
                                                        to the documents, you can limit that access with this feature.
                                                        <br />
                                                        <label>
                                                            Would you like to set up the document access rights now?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Yes" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
            </div>                                                               
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
