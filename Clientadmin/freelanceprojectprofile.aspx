<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Login.master" AutoEventWireup="true" CodeFile="freelanceprojectprofile.aspx.cs" Inherits="ShoppingCart_Admin_freelanceprojectprofile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
       .anscss
       {
           color:Black;  
           font-weight:normal;
       }
    </style>
    <form id="formid" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" GroupingText="Freelance Project Profile">
              <table style="width: 100%; margin-left:25px;">
                        <tr>
                            <td style="height: 24px" align="center">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button12" runat="server" 
                                    onclick="Button12_Click" Text="Basic Details" Visible="false"  />
                                &nbsp;&nbsp;<asp:Button ID="Button10" runat="server" onclick="Button10_Click" 
                                    Text="Instruction Files" Width="138px" Visible="false" />
                                &nbsp;
                                <asp:Button ID="Button11" runat="server" onclick="Button11_Click" Visible="false"
                                    Text="Status Reports" />
                            </td>
                        </tr>
                    </table>
                <asp:Panel ID="Panel2" runat="server">

                <div class="products_box">
        <fieldset>
            <legend>
                <asp:Label ID="Label211" runat="server" Text="Basic Details" Visible="false"></asp:Label>
            </legend>
            <table width="100%" style="margin-left:25px;">
                <tr>
                    <td style="width: 30%">
                        <label class="cssLabelCompany_Information">
                            <asp:Label ID="lblwname" runat="server" Text="Project Catagory">
                            </asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%" class="anscss" colspan="3">
                        
                            <asp:Label ID="lblprojectcatagory" runat="server" Text=""></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <label class="cssLabelCompany_Information">
                            <asp:Label ID="Label39" runat="server" Text=" Project Title"></asp:Label>
                            
                        </label>
                    </td>
                    <td style="width: 70%" class="anscss" colspan="3"> 
                        
                            <asp:Label ID="lblprojecttitle" runat="server" Text=""></asp:Label>
                           <%-- &nbsp;<asp:Label ID="Label4" runat="server" Text=":"></asp:Label>
                            &nbsp;<asp:Label ID="lblduration" runat="server" Text=""></asp:Label>--%>
                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%" class="anscss">
                        <label>
                            <asp:Label ID="Label40" runat="server" Text="Project Start Date"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 25%" class="anscss">
                        
                            <asp:Label ID="lblprojectstartdate" runat="server" Text=""></asp:Label>
                        
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Project End Date"></asp:Label>
                        </label> 
                    </td>
                    <td style="width: 25%" class="anscss">
                        
                            <asp:Label ID="lblprojectenddate" runat="server" Text=""></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <label class="cssLabelCompany_Information">
                            <asp:Label ID="Label7" runat="server" Text="Project Duration"></asp:Label>
                        </label>
                    </td>
                    <td style="width:25%" class="anscss">
                        
                            <asp:Label ID="lblprojectduration" runat="server" Text=""></asp:Label>
                        
                    </td>
                    <td>
                        <label class="cssLabelCompany_Information">
                            <asp:Label ID="Label28" runat="server" Text="Maximum Project Hours"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 25%">
                        
                            <asp:Label ID="lblprojectdurationmax" runat="server" Text=""></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Expected Work Hours"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%" class="anscss" colspan="3">
                        <label>
                            <asp:Label ID="lblNo" runat="server" ForeColor="Black"></asp:Label>
                        </label>
                      
                        <label>
                            <asp:Label ID="Label38" runat="server" Text="per"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblDay" runat="server" ForeColor="Black"></asp:Label>
                        </label>
                    </td>
                </tr>
                   <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label17" runat="server" Text="Remuneration"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%" class="anscss" colspan="3">
                        <label>
                            <asp:Label ID="lblCurrency" runat="server" ForeColor="Black"></asp:Label>
                        </label>
                       <label >
                            <asp:Label ID="lblAmount" runat="server" ForeColor="Black"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="dddd" runat="server" Text="per"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblDay1" runat="server" ForeColor="Black"></asp:Label>
                        </label>
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label1" runat="server" Text="Project Description"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%" class="anscss" colspan="3">
                       
                            <asp:Label ID="lblprojectDesc" runat="server" Text=""></asp:Label>
                       
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label2" runat="server" Text="Candidate Qualification Requirements"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%" class="anscss" colspan="3">
                       
                            <asp:Label ID="lblqualifi" runat="server" Text=""></asp:Label>
                       
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="lblterms" runat="server" Text="Terms and Conditions"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%" class="anscss" colspan="3">
                       
                            <asp:Label ID="lblTandC" runat="server" Text=""></asp:Label>
                       
                    </td>
                </tr>
              
            </table>
        </fieldset>
    </div>  

                    <%--<table style="width: 100%">
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label8" runat="server" Text="Project Category" Font-Bold="True" 
                                    Font-Names="Calibri" Font-Size="14px" ForeColor="#416271"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label9" runat="server" Text="Project Title" Font-Bold="True" 
                                    Font-Names="Calibri" Font-Size="14px" ForeColor="#416271"></asp:Label>
                            </td>
                            <td colspan="2" style="width: 40%" width="20%">
                                <asp:TextBox ID="TextBox2" runat="server" Width="250px"></asp:TextBox>
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label10" runat="server" Text="Project Start Date" 
                                    Font-Bold="True" Font-Names="Calibri" Font-Size="14px" ForeColor="#416271"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="TextBox3" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td width="20%">
                                <asp:Label ID="Label11" runat="server" Text="Project Target End Date" 
                                    Font-Bold="True" Font-Names="Calibri" Font-Size="14px" ForeColor="#416271"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="TextBox4" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label12" runat="server" Text="Project Duration" Font-Bold="True" 
                                    Font-Names="Calibri" Font-Size="14px" ForeColor="#416271"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="TextBox5" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td width="20%">
                                <asp:Label ID="Label13" runat="server" Text="Maximum Project Hours" 
                                    Font-Bold="True" Font-Names="Calibri" Font-Size="14px" ForeColor="#416271"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="TextBox6" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 28px" width="20%">
                                <asp:Label ID="Label14" runat="server" Text="Expected Work Hours" 
                                    Font-Bold="True" Font-Names="Calibri" Font-Size="14px" ForeColor="#416271"></asp:Label>
                            </td>
                            <td colspan="2" style="width: 40%; height: 28px" width="20%">
                                <asp:Label ID="Label15" runat="server" Text="Number"></asp:Label>
                                &nbsp;<asp:Label ID="Label16" runat="server" Text="per"></asp:Label>
                                &nbsp;<asp:Label ID="Label18" runat="server" Text="Day/week/month"></asp:Label>
                            </td>
                            <td style="height: 28px" width="20%">
                            </td>
                            <td style="height: 28px" width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label19" runat="server" Text="Remuneration" Font-Bold="True" 
                                    Font-Names="Calibri" Font-Size="14px" ForeColor="#416271"></asp:Label>
                            </td>
                            <td colspan="3" style="width: 40%">
                                <asp:Label ID="Label20" runat="server" Text="Currency"></asp:Label>
                                &nbsp;<asp:Label ID="Label21" runat="server" Text="Amount"></asp:Label>
                                &nbsp;<asp:Label ID="Label22" runat="server" Text="per"></asp:Label>
                                &nbsp;<asp:Label ID="Label23" runat="server" Text="day/week/month/project"></asp:Label>
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 28px" width="20%">
                                <asp:Label ID="Label24" runat="server" Text="Project Description" 
                                    Font-Bold="True" Font-Names="Calibri" Font-Size="14px" ForeColor="#416271"></asp:Label>
                            </td>
                            <td style="height: 28px" width="20%">
                            </td>
                            <td style="height: 28px" width="20%">
                            </td>
                            <td style="height: 28px" width="20%">
                            </td>
                            <td style="height: 28px" width="20%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" rowspan="2" style="width: 40%" width="20%">
                                <asp:TextBox ID="TextBox7" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 40%" width="20%">
                                <asp:Label ID="Label25" runat="server" 
                                    Text="Candidate Qualification Requirements" Font-Bold="True" 
                                    Font-Names="Calibri" Font-Size="14px" ForeColor="#416271"></asp:Label>
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" rowspan="2" style="width: 40%" width="20%">
                                <asp:TextBox ID="TextBox8" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label26" runat="server" Text="Terms and Conditions" 
                                    Font-Bold="True" Font-Names="Calibri" Font-Size="14px" ForeColor="#416271"></asp:Label>
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
                            <td colspan="2" rowspan="2" style="width: 40%" width="20%">
                                <asp:TextBox ID="TextBox9" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                            <td width="20%">
                                &nbsp;</td>
                        </tr>
                    </table>--%>

                  

                </asp:Panel>
            

                <asp:Panel ID="Panel3" runat="server" GroupingText="Instruction Files" >

                    <table style="width: 100%">
                        <tr>
                            <td width="50%">
                               <asp:GridView ID="gridFileAttach" runat="server" CssClass="mGrid" GridLines="Both"
                                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                            Width="100%" OnRowCommand="gridFileAttach_RowCommand">
                                                            <Columns>
                                                               <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltitle" runat="server" Text='<%#Bind("Title") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Other File" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                     <%--   <asp:Label ID="lblpdfurl" runat="server" Text='<%#Bind("PDFURL") %>'></asp:Label>--%>
                                                                          <asp:LinkButton ID="linkdow1daimnbmbmlyversion" runat="server" Text='<%#Bind("PDFURL") %>' Font-Size="12px" 
                                                         ForeColor="#b9b9b9"     CommandName="Download" CommandArgument='<%# Eval("PDFURL") %>'           ></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                             
                                                                <asp:TemplateField HeaderText="Audio URL" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                      <%--  <asp:Label ID="lblaudiourl" runat="server" Text='<%#Bind("AudioURL") %>'></asp:Label>--%>
                                                                         <asp:LinkButton ID="linkdow1dailyversion" runat="server" Text='<%#Bind("AudioURL") %>' Font-Size="12px" 
                                                         ForeColor="#b9b9b9"     CommandName="Download" CommandArgument='<%# Eval("AudioURL") %>'           ></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Document File" HeaderStyle-HorizontalAlign="Left" Visible="false">

                                                                    <ItemTemplate>
                                                                      <%--  <asp:Label ID="lbldoc" runat="server" Text='<%#Bind("AudioURL") %>'></asp:Label>--%>
                                                                         
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                        <%-- <asp:TemplateField HeaderText="Show" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="linkdow1dailyversion" runat="server" Text="Show" Font-Size="12px" 
                                                         ForeColor="#b9b9b9"     CommandName="Download" CommandArgument='<%# Eval("AudioURL") %>'           ></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                               
                                                                             
                                                            </Columns>
                                                        </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%" width="50%">
                                <asp:GridView ID="GridView2" runat="server" CssClass="mGrid">
                                    <Columns>
                                        <asp:BoundField DataField="Audio Instructionfile" 
                                            HeaderText="Audio Instructionfile" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>

                </asp:Panel>


                <asp:Panel ID="Panel4" runat="server" GroupingText="List Of Status Notes" Visible="false">
                    <table style="width: 100%">
                        <tr>
                            <td colspan="3" style="width: 66%">
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                                    CssClass="mGrid">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date"></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Status Notes"></asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <img 
    src="http://localhost:2633/members.ijobcenter/ShoppingCart/Admin/images/Downloads-20.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form> 
</asp:Content>

