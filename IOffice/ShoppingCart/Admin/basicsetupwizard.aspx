<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="basicsetupwizard.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <asp:Panel ID="pnlbasicsetup" runat="server" Width="100%">
                    <fieldset>
                          <table width="100%" cellspacing="0" cellpadding="2">
                            <tr>
                                <td style="width: 85%" colspan="2">
                                    <asp:Label ID="Label37" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Basic"></asp:Label>
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
                                    <asp:Label ID="Label38" runat="server" Font-Bold="true" Text="Would you like to go through the Basic Setup Wizard?"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 5%">
                                    <label>
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel79" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel37" runat="server" Width="100%">
                                            <table>
                                                <tr>
                                                    <td style="width: 10%">
                                                        <asp:Panel ID="Panel1" runat="server" Width="100%" Visible="false">
                                                            <asp:Image ID="Image1" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel2" Visible="false" runat="server" Width="100%">
                                                            <asp:Image ID="Image2" runat="server" Width="40px" Height="25px" ImageUrl="~/Account/images/delete.gif" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel3" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Part A"></asp:Label>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Business Setup"></asp:Label>
                                                    </td>
                                                    <td style="width: 15%">
                                                    </td>
                                                    
                                                </tr>                                         
                                                 <tr >
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                        <td>
                                                         <label>
                                                            A business has already been setup by the name
                                                            <asp:Label ID="lblbusiness" runat="server" Text="" ForeColor="Black"></asp:Label>                                                            
                                                            </label>
                                                         </td>
                                                 </tr>
                                                 <tr >
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                             <td>
                                                            <label>
                                                                Would you like to add another business now or make any changes to the business
                                                                <asp:Label ID="lblbusiness1" runat="server" Text="" ForeColor="Black"></asp:Label>
                                                                ?
                                                                </label>                                                         
                                                            <asp:CheckBox ID="CheckBox123" runat="server" Text="yes" AutoPostBack="true" OnCheckedChanged="CheckBox123_CheckedChanged" />
                                                            <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Black" Font-Bold="true"
                                                                OnClick="LinkButton3_Click" Visible="false">  Yes</asp:LinkButton>                                                           
                                                             </td>                                                 
                                                 </tr>                                                 
                                                 <tr>
                                                  <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                            <td>
                                                             <label>
                                                                From this page you can also set up your email functionality. Please note that this
                                                                is required if you wish to send reports and approvals, as well as communicating
                                                                with employees regarding the set up of their user ID and password.
                                                            </label>
                                                             </td>
                                                 </tr>                                                     
                                                </tr>                                               
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel38" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 10%">
                                                        <asp:Panel ID="Panel4" runat="server" Width="100%" Visible="false">
                                                            <asp:Image ID="Image3" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel5" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel6" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Part B"></asp:Label>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Department Setup"></asp:Label>
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
                                                            The default departments have been created for your business,would you like <br />
                                                           to add any more departments?                                                          
                                                        </label>
                                                        <label>                                                      
                                                        <asp:CheckBox ID="CheckBox3a" runat="server" Text="yes" AutoPostBack="true" OnCheckedChanged="CheckBox3a_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton4_Click" Visible="false"> Yes</asp:LinkButton>
                                                            </label>  
                                                     </td>
                                                  </tr>
                                                  
                                                   <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>                                                       
                                                        <label>
                                                            Existing Departments:
                                                            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" OnClick="LinkButton1_Click">View</asp:LinkButton>
                                                        </label>
                                                    </td>
                                                 </tr>                                                    
                                                      
                                                </tr>                                                
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel39" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 10%">
                                                        <asp:Panel ID="Panel7" Visible="false" runat="server" Width="100%">
                                                            <asp:Image ID="Image4" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel8" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel9" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Part C"></asp:Label>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:Label ID="Label8" runat="server" Font-Bold="true" Text="Designation Setup"></asp:Label>
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
                                                            The default designations have been created for your business, would you like <br />
                                                            to add any designations?
                                                        </label>
                                                        <label>
                                                          <asp:CheckBox ID="CheckBox8" runat="server" Text="yes" AutoPostBack="true" OnCheckedChanged="CheckBox8_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton5_Click" Visible="false"> Yes</asp:LinkButton>
                                                         </label>
                                                      </td>
                                                   <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>                                                      
                                                      <td>
                                                        <label>
                                                            Existing Designations:
                                                            <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Black" OnClick="LinkButton2_Click">View</asp:LinkButton>
                                                        </label>
                                                     </td>                                         
                                                  </tr>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel40" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 10%">
                                                        <asp:Panel ID="Panel10" Visible="false" runat="server" Width="100%">
                                                            <asp:Image ID="Image5" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel11" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel12" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Part D"></asp:Label>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:Label ID="Label10" runat="server" Font-Bold="true" Text="Batch Time Zone Setup"></asp:Label>
                                                    </td>
                                                    <td style="width: 15%">
                                                    </td>                                                   
                                                </tr>                                               
                                                
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td valign="bottom">
                                                        <label>
                                                            The default time zone for your business is
                                                            <asp:Label ID="Label17" runat="server" Text="" ForeColor="Black"></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>  
                                                    <td>                                                        
                                                            <label>
                                                                Would you like to change your time zone?
                                                            </label>
                                                            <asp:CheckBox ID="CheckBox7" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox7_CheckedChanged" />
                                                            <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="Black" Font-Bold="true"
                                                                Visible="false" OnClick="LinkButton6_Click"> Yes</asp:LinkButton>                                                            
                                                        
                                                    </td>
                                                  </tr>                                                  
                                                </tr>
                                                
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel41" runat="server" Width="100%">
                                            <table>
                                                <tr>
                                                    <td style="width: 10%">
                                                        <asp:Panel ID="Panel13" Visible="false" runat="server" Width="100%">
                                                            <asp:Image ID="Image6" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel14" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel15" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="Part E"></asp:Label>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="Batch Timing Setup"></asp:Label>
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
                                                            You can create your employee schedule batch names for your business here. You can
                                                            select a batch to be a default batch for the entire business, a particular department,
                                                            or for a particular designation. You can view the list of batches created by default
                                                            from
                                                            <asp:LinkButton ID="LinkButton7" runat="server" ForeColor="Black" Font-Bold="true"
                                                                OnClick="LinkButton7_Click"> here</asp:LinkButton>
                                                      </td>
                                                   </tr>
                                                  <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>       
                                                                <label>                                                                                                                 <label>
                                                                Would you like to set up an additional batch?
                                                            </label>
                                                            <asp:CheckBox ID="CheckBox6" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox6_CheckedChanged" />
                                                            <asp:LinkButton ID="LinkButton8" runat="server" ForeColor="Black" Font-Bold="true"
                                                                Visible="false" OnClick="LinkButton8_Click"> Yes</asp:LinkButton>
                                                        
                                                    </td>
                                                   
                                                  </tr>
                                                </tr>                                               
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel42" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 10%">
                                                        <asp:Panel ID="Panel16" Visible="false" runat="server" Width="100%">
                                                            <asp:Image ID="Image7" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel17" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel18" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Part F"></asp:Label>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Text="Batch Working Days"></asp:Label>
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
                                                            You can view default working days per week for your business from here
                                                            <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="Black" Font-Bold="true"
                                                                OnClick="LinkButton9_Click"> here</asp:LinkButton>.
                                                         </label>
                                                      </td>
                                                  </tr>
                                                   <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                            <label>
                                                                Would you like to add additional batch working days or modify the default settings?
                                                            </label>
                                                            <asp:CheckBox ID="CheckBox5" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox5_CheckedChanged" />
                                                            <asp:LinkButton ID="LinkButton10" runat="server" ForeColor="Black" Font-Bold="true"
                                                                Visible="false" OnClick="LinkButton10_Click"> Yes</asp:LinkButton>
                                                       
                                                    </td>                                                                                                    
                                                </tr>                                                
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel43" runat="server" Width="100%">
                                            <table>
                                                <tr>
                                                    <td style="width: 10%">
                                                        <asp:Panel ID="Panel19" Visible="false" runat="server" Width="100%">
                                                            <asp:Image ID="Image8" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel20" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel21" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:Label ID="Label15" runat="server" Font-Bold="true" Text="Part G"></asp:Label>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:Label ID="Label16" runat="server" Font-Bold="true" Text="Website Access Rights Setup"></asp:Label>
                                                    </td>
                                                    <td style="width: 15%">
                                                    </td>
                                                    <%-- <td style="width: 5%">
                                                    </td>--%>
                                                </tr>
                                             
                                              <tr> 
                                                  <td>
                                                  </td>
                                                  <td>
                                                  </td>
                                                  <td>
                                                      <label>
                                                      You can set specific website access rights for specific role names. For example, 
                                                      a business manager designation would require access to more pages (such as 
                                                      sensative pages such as payroll, accounting, and personal employee information) 
                                                      within Online Accounts then the receptionist designation due to the requirements 
                                                      of the different jobs.
                                                      </label>
                                                  </td>
                                             </tr>    
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <label>
                                                        Would you like to setup your businesses access rights?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox4" runat="server" AutoPostBack="true" 
                                                            OnCheckedChanged="CheckBox4_CheckedChanged" Text="Yes" />
                                                        <asp:LinkButton ID="LinkButton11" runat="server" Font-Bold="true" 
                                                            ForeColor="Black" OnClick="LinkButton11_Click" Visible="false"> Yes</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel45" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 10%">
                                                        <asp:Panel ID="Panel25" Visible="false" runat="server" Width="100%">
                                                            <asp:Image ID="Image10" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel26" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel27" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:Label ID="Label19" runat="server" Font-Bold="true" Text="Part H"></asp:Label>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:Label ID="Label20" runat="server" Font-Bold="true" Text="Employee Setup"></asp:Label>
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
                                                            The default employee
                                                            <asp:LinkButton ID="LinkButton13" runat="server" ForeColor="Black" Font-Bold="true"> 
                                                        Admin</asp:LinkButton>
                                                            has been created to your business.
                                                   </td>
                                                </tr>
                                                     <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                            <label>
                                                                Do you want to add employees to your business now?
                                                            </label>
                                                            <asp:CheckBox ID="CheckBox33" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox33_CheckedChanged" />
                                                            <asp:LinkButton ID="LinkButton14" runat="server" ForeColor="Black" Font-Bold="true"
                                                                OnClick="LinkButton14_Click" Visible="false"> Yes</asp:LinkButton>
                                                       
                                                    </td>                                                                                                   
                                                </tr>                                                
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel46" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 10%">
                                                        <asp:Panel ID="Panel28" Visible="false" runat="server" Width="100%">
                                                            <asp:Image ID="Image11" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel29" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel30" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:Label ID="Label21" runat="server" Font-Bold="true" Text="Part I"></asp:Label>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:Label ID="Label22" runat="server" Font-Bold="True" Text="Company Holiday Setup"></asp:Label>
                                                    </td>
                                                    <td style="width: 15%">
                                                    </td>                                                   
                                                </tr>
                                              
                                                 <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td style="width: 75%" >
                                                          
                                                        <label>
                                                            This will be required for attendance and payroll management. Do you want <br />
                                                             to add company holidays now?
                                                       </label>   
                                                       <label>                                                                                             
                                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton12" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton12_Click" Visible="false"> Yes</asp:LinkButton>                                                        
                                                         </label>  
                                                    </td>                                                                                                     
                                                </tr>
                                               
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel47" runat="server" Width="100%">
                                            <table>
                                                <tr>
                                                    <td style="width: 10%">
                                                        <asp:Panel ID="Panel31" Visible="false" runat="server" Width="100%">
                                                            <asp:Image ID="Image12" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel32" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel33" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:Label ID="Label23" runat="server" Font-Bold="true" Text="Part J"></asp:Label>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:Label ID="Label24" runat="server" Font-Bold="true" Text="Company sick leave and vacation setup"></asp:Label>
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
                                                      This will be required for attendance and payroll management, ensuring the right 
                                                      supervisors know how many sick leaves are allowed, and attendance managers know 
                                                      how many vacation days are allotted to you.
                                                      </label>
                                                  </td>
                                              </tr>  
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <label>
                                                        Do you want to set up sick leave and vacation policies for employees now?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                                                            OnCheckedChanged="CheckBox1_CheckedChanged" Text="Yes" />
                                                        <asp:LinkButton ID="LinkButton15" runat="server" Font-Bold="true" 
                                                            ForeColor="Black" OnClick="LinkButton15_Click" Visible="false"> Yes</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel48" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 10%">
                                                        <asp:Panel ID="Panel34" Visible="false" runat="server" Width="100%">
                                                            <asp:Image ID="Image13" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel35" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel36" Visible="false" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="width: 5%" valign="bottom">
                                                        <asp:Label ID="Label25" runat="server" Font-Bold="true" Text="Part K"></asp:Label>
                                                    </td>
                                                    <td style="width: 75%" valign="top">
                                                        <label>
                                                            <asp:Label ID="Label26" runat="server" Font-Bold="true" Text="Would you like to add contacts (customers, vendors, candidates, others, etc.)?"></asp:Label>
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="true" Text="Yes" OnCheckedChanged="CheckBox3_CheckedChanged" />
                                                    </td>
                                                    <td style="width: 15%">
                                                    </td>
                                                    <%-- <td style="width: 5%">
                                                    </td>--%>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset></asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
