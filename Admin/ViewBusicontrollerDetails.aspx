<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ViewBusicontrollerDetails.aspx.cs" Inherits="Admin_ViewBusicontrollerDetails" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
      
    <table id="pagetbl" cellpadding="0" cellspacing="0">
         <tr>
            <td colspan="3"  class="hdng">
                <strong>Manage My BusiController.</strong>
            </td>
        </tr>
        <tr>
            <td colspan="3" >
                &nbsp;          </td>
        </tr>
        <tr>
            <td colspan="3"><asp:Label ID="lbl1" runat="server" Font-Bold="True" Font-Names="Action Is,Diagonal JL"    Font-Size="12"  Text="You will need to downoad our application BusiController to get our services for your products."></asp:Label>
               </td>
        </tr>
      
        <%-- <tr>
            <td colspan="3" >
                &nbsp;          </td>
        </tr>--%>
        <tr>
            <td colspan="2">
                <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="True" Font-Names="Action Is,Diagonal JL"
                    Font-Size="12" NavigateUrl="~/PreReqFiles/StepOfinstalltion.doc" Target="_self"
                   >BusiController Application can be downloaded from here.</asp:HyperLink></td>
       <td><asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Names="Action Is,Diagonal JL"
                    Font-Size="Medium" NavigateUrl="~/PreReqFiles/StepOfinstalltion.doc" Target="_self">Download</asp:HyperLink></td>
        </tr>
         <tr>
            <td colspan="3"><asp:Label ID="Labe1" runat="server" Font-Bold="True" Font-Names="Action Is,Diagonal JL"    Font-Size="12"  Text="(The download package will have instruction sheet to explain how to install the application)."></asp:Label>
               </td>
        </tr>
          <%--<tr>
            <td colspan="3" >
                &nbsp;          </td>
        </tr>--%>
         
        <tr>
            <td colspan="3"><asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Action Is,Diagonal JL"    Font-Size="12"  Text="You can Manage the BusiController from here"></asp:Label>
               </td>
        </tr>
       <%--  <tr>
            <td colspan="3" >
                &nbsp;          </td>
        </tr>--%>
        </table>
         
       			<table id="pagetbl" >
       			<tr>
            <td class="column1" >
              
              Product/Version Name :</td>
            <td class="column2" >
                             <asp:DropDownList ID="Ddlpv" runat="server" Height="20px" Width="159px"></asp:DropDownList> 
               </td>
                <td class="column1" >
              Company Name :</td>
            <td class="column2" >
             <asp:DropDownList ID="ddlcomname" runat="server"  Height="20px" Width="159px"></asp:DropDownList>  </td>
               </tr>
       			<tr>
            <td class="column1" >
              
              BusiController Application URL :</td>
            <td class="column2" >
                <asp:TextBox ID="txtbasicurl" runat="server" Width="155px"></asp:TextBox><asp:RequiredFieldValidator ID="rqapp" runat="server" ControlToValidate="txtbasicurl" ErrorMessage="*"></asp:RequiredFieldValidator>
               </td>
                <td class="column1" >
              Client Name :</td>
            <td class="column2" >
             <asp:DropDownList ID="ddlcname" runat="server" Height="20px" Width="159px"></asp:DropDownList>  </td>
               </tr>
       		<tr>
            <td class="column1" >
                
                 Database ServerName/IP :</td>
            <td class="column2" >
                <asp:TextBox ID="txtdatabaseserverip" runat="server" Width="155px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdatabaseserverip" ErrorMessage="*"></asp:RequiredFieldValidator>
              </td>
            <td class="column1" >
              Port :</td>
            <td class="column2" >
                <asp:TextBox ID="txtPort" runat="server" Width="155px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="column1" >
               
               UserID :</td>
            <td class="column2" >
                <asp:TextBox ID="txtUserID" runat="server" Width="155px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUserID" ErrorMessage="*"></asp:RequiredFieldValidator>
              </td>
            <td class="column1" style="width: 200px" >
                Password :</td>
           <td class="column2" >
            <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" Width="155px" />
              
             </td>
             
        </tr>
        <tr>
            <td class="column1" >
               
               Database Name :</td>
            <td class="column2" >
                <asp:TextBox ID="txtdtname" runat="server" Width="155px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdtname" ErrorMessage="*"></asp:RequiredFieldValidator>
              </td>
            <td class="column1" >
                Activate :</td>
           <td class="column2" >
           <asp:CheckBox ID="chkactive" runat="server" />
              
             </td>
    <%--    <tr>
            <td colspan="3" >
                &nbsp;          </td>
        </tr>--%>
                      
         <tr><td></td><td  align="center">
           &nbsp;&nbsp;<asp:Button ID="btnsubmit" runat="server" Text="Submit" 
                 onclick="btnsubmit_Click" /> </td> <td></td>        </tr>
                      
        <tr>
            <td class="column1" style="width: 450px">
            </td>
            <td class="column2" style="width: 200px" align="center">
                &nbsp;</td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
           
            <td  class="column1"  colspan="5"><asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
               
    <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
          PageSize="30"   CssClass="GridBack1"  
            Width="645px" AllowPaging="True" onrowcommand="GridView1_RowCommand">
            <Columns>
             <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Edit" ImageUrl="~/images/edit.gif"
                    Text="Button"  />
                   <asp:TemplateField HeaderText="ID" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                         
                                        </ItemTemplate>
                                        
                                        <HeaderStyle Width="40px"   HorizontalAlign="Center" VerticalAlign="Middle"/>
                                    </asp:TemplateField>  
                <asp:TemplateField HeaderText="ClientID" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblcid" runat="server" Text='<%# Bind("ClinetMasterID") %>'></asp:Label>
                                         
                                        </ItemTemplate>
                                        
                                        <HeaderStyle Width="50px"   HorizontalAlign="Center" VerticalAlign="Middle"/>
                                    </asp:TemplateField>
                                 
                   <asp:TemplateField HeaderText="Client Name" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblcname" runat="server" Text='<%# Bind("ClientName") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                           <HeaderStyle Width="400px" HorizontalAlign="Center" />
               </asp:TemplateField>                     
                <asp:TemplateField HeaderText="BusiController Application URL" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblbsa" runat="server" 
                                                    Text='<%# Bind("BusiControllerApplicationURL") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                           <HeaderStyle Width="400px" HorizontalAlign="Center" />
               </asp:TemplateField>
                 <asp:TemplateField HeaderText="Database ServerName/IP" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblserverip" runat="server" 
                                                    Text='<%# Bind("DatabaseServerNameOrIp") %>'></asp:Label>
                                            </ItemTemplate>
                                           <HeaderStyle Width="300px" HorizontalAlign="Center" />
               </asp:TemplateField>
                   <asp:TemplateField HeaderText="Database Name" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbldtnames" runat="server" Text='<%# Bind("DatabaseName") %>'></asp:Label>
                                            </ItemTemplate>
                                           <HeaderStyle Width="300px" HorizontalAlign="Center" />
               </asp:TemplateField>
              <asp:TemplateField HeaderText="Port">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPort" runat="server" Text='<%# Bind("Port") %>' ></asp:Label>
                                            </ItemTemplate>
                                           
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
              <asp:TemplateField HeaderText="UserID" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                                            </ItemTemplate>
                                              <HeaderStyle Width="200px" HorizontalAlign="Center" />
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Password" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblPassword" runat="server" Text='<%# Bind("Password") %>'></asp:Label>
                                             
                                            </ItemTemplate>
                                             <HeaderStyle Width="100px" HorizontalAlign="Center" />
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Active" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblActive" runat="server" Text='<%# Bind("Active") %>'></asp:Label>
                                             
                                            </ItemTemplate>
                                             <HeaderStyle Width="100px" HorizontalAlign="Center" />
               </asp:TemplateField>
               </Columns>
            <PagerStyle CssClass="GridPager" />
            <HeaderStyle CssClass="GridHeader" ForeColor="White" />
            <AlternatingRowStyle CssClass="GridAlternateRow" />
            <RowStyle CssClass="GridRowStyle" />
         <FooterStyle CssClass="GridFooter" ForeColor="white" BackColor="White"/>
        </asp:GridView>
               
            </td>
           
        </tr> 
    
       
    </table>
    <asp:Panel ID="Panel1" runat="server" Width="645px"  Height="350px" ScrollBars="Auto">
    
            </asp:Panel>
    <input runat="server" id="hdnProductDetailId" type="hidden" name="hdnProductDetailId"
        style="width: 3px" />
 <input  id="hdnsortExp" type="hidden" name="hdnsortExp"  style="width: 3px" runat="Server" />
       <input  id="hdnsortDir" type="hidden" name="hdnsortDir"  style="width: 3px" runat="Server" />
    <input runat="server" id="hdnProductId" type="hidden" name="hdnProductId" style="width: 3px" />

</asp:Content>





