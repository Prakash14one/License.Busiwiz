<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="ViewEmployeeProjectStatusLB.aspx.cs" Inherits="ProjectViewStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="right_content">
        <div id="ctl00_ucTitle_PNLTITLE">
            <div class="divHeaderLeft">
                <div style="float: left; width: 50%;">
                    <h2>
                        <span id="ctl00_ucTitle_lbltitle">Project Profile </span>
                    </h2>
                </div>
                <div style="float: right; width: 50%">
                    <div id="ctl00_ucTitle_pnlshow">
                    </div>
                </div>
            </div>
        </div>
        <div style="clear: both;">
        </div>
        <div id="ctl00_ucTitle_pnlhelp" style="border: solid 1px black">
            <h3>
                <span id="ctl00_ucTitle_lblDetail" style="font-family: Tahoma; font-size: 7pt; font-weight: bold;">
                    Project Profile </span>
            </h3>
        </div>
    </div>
    <div style="margin-left: 1%">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div style="clear: both;">
    </div>
    <fieldset>
        <legend id="5">
            <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
        </legend>
        <div style="float: right;">
            <%--     <asp:Button ID="addnewpanel" runat="server" Text="Add Project Status" CssClass="btnSubmit" />--%>
             <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version" Visible="false"  />
                                <input id="Button4" runat="server" class="btnSubmit" Visible="false" onclick="document.getElementById('mydiv').className = 'open'; javascript: CallPrint('divPrint'); document.getElementById('mydiv').className = 'closed';" style="width: 51px;" type="button" value="Print"  />
        </div>
       
      <br />  
    <asp:Panel ID="pnlmonthgoal" runat="server" Width="90%">
            <table id="pagetbl" width="80%">
                <tr>
                    <td colspan="5" >
                        <label>
                            Department Name:
                        </label>
                      
                        <label>
                            <asp:TextBox ID="ddlDeptName" runat="server" Width="200px" Enabled="false">
                            </asp:TextBox>
                        </label>
                      
                      
                          <label>
                            Employee Name:
                        </label>
                      
                  
                         <label>
                            <asp:TextBox ID="ddlemployee" runat="server" Width="200px" Enabled="false">
                            </asp:TextBox>
                        </label>
                        </td>
                   
                </tr>
                <tr>
                    <td colspan="5">
                        <label>
                            Project Name:
                        </label>
                  
                        <label style="width:800px;">
                         <asp:TextBox ID="lblproname" runat="server" Width="720px" Enabled="false">
                            </asp:TextBox>
                          
                        </label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <label>
                            Start date:
                        </label>
                      
                        <label>
                            <asp:TextBox ID="txtstartdate" runat="server" Width="200px" Enabled="false">
                            </asp:TextBox>
                        </label>
                        
                       
                        <label>
                            End Date:
                        </label>
                       
                             <label>
                            <asp:TextBox ID="txtenddate" runat="server" Width="200px" Enabled="false">
                            </asp:TextBox>
                        </label>
                        </td>
                       
                </tr>
                <tr>
                    <td colspan="5">
                        <label>
                            Target End Date:
                        </label>
                        
                         <label>
                            <asp:TextBox ID="txttargetenddate" runat="server" Width="200px" Enabled="false">
                            </asp:TextBox>
                        </label>
                         <label>
                Reminder Date:
                </label> 
                <label>
                  <%--<asp:Label ID="lbl_reminder" runat="server" Text=""></asp:Label>--%>
                    <asp:TextBox ID="lbl_reminder" runat="server" Width="200px" Enabled="false">
                            </asp:TextBox>
                </label> 
                        </td>
                </tr>
                <tr>
                <td colspan="5">
                <label>
                Project Type:
                </label> 
                    <label>
                        <%-- <asp:Label ID="lbl_type" runat="server" Text=""></asp:Label>--%>
                          <asp:TextBox ID="lbl_type" runat="server" Enabled="false" Width="200px">
                            </asp:TextBox>
                    </label> 
                                     <label>
                            Project Status:
                        </label>
                 
                        <label>
                            <asp:TextBox ID="txtstatus" runat="server" Enabled="false" Width="200px">
                            </asp:TextBox>
                        </label>
              
                </td>
                </tr>
                
               
                
                 <tr>
                    <td>     
                    
                        <label>
                            Project Description:
                            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                        </label>
                  
                       
                       
                            <%--<asp:TextBox ID="txtprodescription" runat="server" Width="600px" Enabled="false"
                                Height="175px" TextMode="MultiLine"></asp:TextBox>--%>
                        
                    </td>
                     <td valign="top" align="left"   colspan="4">     
                       <asp:Label ID="lblprodescription" runat="server" Text="" Width="1000px"></asp:Label>
                     </td>
                </tr>
             
                
               
            </table>
        </asp:Panel>
    </fieldset>
     <div id="mydiv" >
    <fieldset>
        <legend>
            <asp:Label ID="Label1" runat="server" Text="List of Project Instructions File"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid" width="100%"
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnRowCommand="gridFileAttach_RowCommand">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>                        
                                                                    
                       
                                            
                                            <asp:TemplateField HeaderText="Instruction File" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" >
                                             <ItemTemplate>
                                                 <asp:LinkButton ID="LinkButton1" runat="server" Enabled="false"  ForeColor="Gray" Text='<%# Eval("DocumentTitle") %>' ToolTip="Download"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>   
                                            
                                            <asp:TemplateField HeaderText="Audio Instruction File"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" >
                                             <ItemTemplate>
                                              <asp:LinkButton ID="LinkButton9" runat="server" CommandArgument='<%# Eval("DocumentFileName") %>' CommandName="view111" ForeColor="Gray" Text='<%# Eval("DocumentFileName") %>' ToolTip="Download"></asp:LinkButton>
                                               </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>   
                                                  <asp:TemplateField HeaderText="Instruction File"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" >
                                                    <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton95" runat="server" CommandArgument='<%# Eval("Doc") %>' CommandName="Download" ForeColor="Gray" Text='<%# Eval("Doc") %>' ToolTip="Download"></asp:LinkButton>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                
                                                 
                        </Columns>
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                </td>
                <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>
            <asp:Label ID="Label30" runat="server" Text="Project Progress Reports"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                            <td colspan="3" align="right">
                               
                            </td>
                        </tr>
                <td>
                    <asp:GridView ID="grdprostatus" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                        AllowSorting="True" onrowcommand="grdprostatus_RowCommand">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:BoundField DataField="ProjectStatus_Date" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="Status Date" ItemStyle-Width="12%" SortExpression="ProjectStatus_Date"
                                DataFormatString="{0:dd.MM.yyyy}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="12%" />
                            </asp:BoundField>
                             <asp:BoundField DataField="ProjectStatus_Status_Description" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="Project Status Notes" ItemStyle-Width="55%" SortExpression="ProjectStatus_Status_Description">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="55%" />
                            </asp:BoundField>
<%--                            <asp:BoundField DataField="ProjectStatus_Status" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="12%" HeaderText="Project Status" SortExpression="ProjectStatus_Status">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="12%" />
                            </asp:BoundField>--%>

                            
                         <%-- <asp:TemplateField HeaderText="Download" HeaderImageUrl="~/Account/images/download.jpg"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" >
                                                    <ItemTemplate>
                                                         <asp:ImageButton ID="imgdelete" ToolTip="Do" runat="server" ImageUrl="~/Account/images/download.jpg"
                                                            CommandName="Download" CommandArgument='<%# Eval("DocumentFileName") %>'></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField> --%>

                                                 
                           
                        </Columns>
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                </td>
                <tr>
                <td>
                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                </td>
            </tr>
            <tr>
            <td colspan="4">
              <asp:Label ID="lblvername" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>
            <asp:Label ID="Label2" runat="server" Text="List of  Supervisor Status Notes"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                            <td colspan="3" align="right">
                               
                            </td>
                        </tr>
                <td>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                        AllowSorting="True" onrowcommand="GridView2_RowCommand">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:BoundField HeaderText="Status Date" DataField="ProjectStatus_Date" HeaderStyle-HorizontalAlign="Left"
                             ItemStyle-Width="12%" SortExpression="ProjectStatus_Date" DataFormatString="{0:dd.MM.yyyy}" />
                            <asp:BoundField HeaderText="Supervisors Staus Notes" 
                                DataField="Status_Description" />
                           <%-- <asp:TemplateField HeaderText="Download" HeaderImageUrl="~/Account/images/download.jpg"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" >
                                                    <ItemTemplate>
                                                         <asp:ImageButton ID="imgdelete" ToolTip="Do" runat="server" ImageUrl="~/Account/images/download.jpg"
                                                            CommandName="Download" CommandArgument='<%# Eval("DocumentFileName") %>'></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField> --%>

                        </Columns>

<PagerStyle CssClass="pgr"></PagerStyle>
                        </asp:GridView>
                        </td>
                        <tr>
                <td>
                <input id="Hidden3" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                <input id="Hidden4" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                </td>
            </tr>
            <tr>
            <td colspan="4">
              <asp:Label ID="Label3" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        </table>

        </fieldset>
    
    </div>
    <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" ForeColor="#416271" Font-Size="10px"></asp:Label>
              </div>
</asp:Content>
