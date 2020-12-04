<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Login.master" AutoEventWireup="true" CodeFile="ResetPasswordUser.aspx.cs" Inherits="ShoppingCart_Admin_ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <!-- Codrops top bar -->
        <div class="codrops-top">
        </div>
        <!--/ Codrops top bar -->
        <div id="container_demo">
            <a class="hiddenanchor" id="toregister">
            </a>
            <a class="hiddenanchor" id="tologin">
            </a>
            <div id="wrapper">
                <div id="login" >
                    <form runat="server" id="fmLogin" autocomplete="on">
                    <%--<asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="false"></asp:Label>--%>
                    <h1>
                        Change Password</h1>
                        <p>
                        <label for="username" class="uname">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                           <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                        </label>
                                          </p>
                                          
                    <asp:Panel ID="Panel2" runat="server">
                         <table width="100%" cellspacing="3">
                           <tr >
                            <td align="right" width="28%">
                                <asp:Label ID="Label1" runat="server" Text="Company Id "></asp:Label>
                                 
                                </td>
                            <td align="left">
                                <asp:TextBox ID="txtcompanyid" MaxLength="40" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcompanyid"  ErrorMessage="*" ValidationGroup="1" Display="Dynamic"   >
                                   </asp:RequiredFieldValidator>  
                                   
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                    ErrorMessage="*" Display="Dynamic"
                    SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                    ControlToValidate="txtcompanyid" ValidationGroup="1"></asp:RegularExpressionValidator>
                              
                            </td>
                        </tr>               
                        <tr >
                            <td align="right" width="28%">
                                <asp:Label ID="Label2" runat="server" Text="User Id  "></asp:Label>
                                 
                                </td>
                            <td align="left">
                                <asp:TextBox ID="txtuname" MaxLength="30" runat="server" Width="200px"></asp:TextBox>
                                
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtuname"  ErrorMessage="*" ValidationGroup="1" Display="Dynamic"   >
                                   </asp:RequiredFieldValidator>                          
                                                           
                                                          
                                                           
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="*" Display="Dynamic"
                    SetFocusOnError="True" ValidationExpression="^([@._a-zA-Z0-9\s]*)"
                    ControlToValidate="txtuname" ValidationGroup="1"></asp:RegularExpressionValidator>
                              
                            </td>
                        </tr>
                        <tr >
                            <td align="right" width="28%">
                                <asp:Label ID="Label3" runat="server" Text="Password"></asp:Label>
                                
                                 
                                </td>
                            <td align="left">
                                <asp:TextBox ID="txtpass" MaxLength="30" runat="server" Width="200px" 
                                    TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpass"  ErrorMessage="*" ValidationGroup="1" Display="Dynamic"   >
                                   </asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ErrorMessage="*" Display="Dynamic"
                    SetFocusOnError="True" ValidationExpression="^([@._a-zA-Z0-9\s]*)"
                    ControlToValidate="txtpass" ValidationGroup="1">
                    </asp:RegularExpressionValidator>
                              
                            </td>
                        </tr>
                      
                     
                        <tr>
                           
                            <td colspan="2" align="center" >
                               
                                <asp:Button ID="Button1" runat="server" 
                                    ValidationGroup="1" Width="100px"  Text="Confirm" 
                                    onclick="Button1_Click"/>
                                
                                <asp:Image ID="img1" runat="server" Height="13" Width="13" ImageUrl="~/Account/images/true.gif" Visible="false" ImageAlign="Middle" />    
                               
                            </td>
                        </tr>
                        </table>
                    </asp:Panel>  
                   
                    <asp:Panel Width="100%" Visible="false" ID="pnlsecurityquestion" runat="server">
                    
                    <table width="100%" >
                    <tr>
                     <td align="left" colspan="2">
                     Please Set your login information
                     </td>   
                              
                     </tr>
                    <tr>
                     <td align="right" width="28%">
                     New User Id
                     </td>   
                     <td align="left">     
                     <asp:TextBox ID="txtnewuserid" runat="server"></asp:TextBox>               
                     </td>         
                     </tr>
                      <tr>
                     <td align="right" width="28%">
                      Confirm User Id
                     </td>   
                     <td align="left">   
                      <asp:TextBox ID="txtconfirmuserid" runat="server"></asp:TextBox>   
                       
                      <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="txtnewuserid"
                                                            ControlToValidate="txtconfirmuserid" ErrorMessage="*">
                                                            </asp:CompareValidator>     
                                                                  
                     </td>         
                     </tr>
                      <tr>
                     <td align="right" width="28%">
                     New Password
                     </td>   
                     <td align="left">   
                      <asp:TextBox ID="txtnewpassword" runat="server" TextMode="Password"></asp:TextBox>           
                     </td>         
                     </tr>
                      <tr>
                     <td align="right" width="28%">
                      Confirm Password
                     </td>   
                     <td align="left">  
                     <asp:TextBox ID="txtconfirmpassword" runat="server" TextMode="Password"></asp:TextBox>    
                     <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtnewpassword"
                                                            ControlToValidate="txtconfirmpassword" ErrorMessage="*"></asp:CompareValidator>      
                     </td>         
                     </tr>
                      <tr>
                     <td align="right" width="28%">
                     New Employee Code
                     </td>   
                     <td align="left">  
                     <asp:TextBox ID="txtempcode" runat="server"></asp:TextBox>     
                      
                     </td>         
                     </tr>
                      <tr>
                     <td align="right" width="28%">
                     Confirm Employee Code
                     </td>   
                     <td align="left">   
                         <asp:TextBox ID="txtconfirmempcode" runat="server"></asp:TextBox>  
                          <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtempcode"
                                                            ControlToValidate="txtconfirmempcode" ErrorMessage="*"></asp:CompareValidator> 
                     </td>         
                     </tr>
                     <tr>
                     <td colspan="2">
                     <br />
                     </td>
                     </tr>
                      <tr>
                                    <td colspan="2" align="left">
                                       Set Your Security question
                                    </td>
                                </tr>
                     
                     <tr>
                       <td align="right" style="width:28%" >
                              Question 1 :         
                       </td>             
                       <td align="left" > 
                          <asp:DropDownList ID="ddlquestion1" runat="server">
                           </asp:DropDownList>      
                         </td> 
                         </tr>  
                         <tr>
                       <td align="right" style="width:28%" >
                              Answer 1 :         
                       </td>             
                       <td align="left" > 
                           <asp:TextBox ID="txtanswer1" runat="server"></asp:TextBox>  
                         </td> 
                         </tr>  
                         
                         
                         
                          <tr>
                       <td align="right" style="width:28%" >
                              Question 2 :         
                       </td>             
                       <td align="left" > 
                          <asp:DropDownList ID="ddlquestion2" runat="server">
                           </asp:DropDownList>      
                         </td> 
                         </tr>    
                         <tr>
                       <td align="right" style="width:28%" >
                              Answer 2 :         
                       </td>             
                       <td align="left" > 
                           <asp:TextBox ID="txtanswer2" runat="server"></asp:TextBox>  
                         </td> 
                         </tr>  
                         
                          <tr>
                       <td align="right" style="width:28%" >
                              Question 3 :         
                       </td>             
                       <td align="left" > 
                          <asp:DropDownList ID="ddlquestion3" runat="server">
                           </asp:DropDownList>      
                         </td> 
                         </tr>   
                         <tr>
                       <td align="right" style="width:28%" >
                              Answer 3 :         
                       </td>             
                       <td align="left" > 
                           <asp:TextBox ID="txtanswer3" runat="server"></asp:TextBox>  
                         </td> 
                         </tr>    
                         
                          <tr>
                       <td  colspan="2" align="center">
                           <asp:Button ID="btnsubmit" runat="server" Text="Submit" 
                               onclick="btnsubmit_Click" />
                       </td>             
                      
                         </tr>                    
                        </table>                                        
                  </asp:Panel>                                                                                      
                               
                        
                                
                              
                           
                  
                    

                                
                 
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
