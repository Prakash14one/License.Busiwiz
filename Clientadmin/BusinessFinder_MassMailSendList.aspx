<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="BusinessFinder_MassMailSendList.aspx.cs" Inherits="Admin_BusinessFinder" enableEventValidation="false"%>

<%@ Register TagPrefix="cc" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>

    <%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
#tab
{
     width: 100%;
     border-collapse: collapse;
}
#tab td
{
    float:left;
}
    .weburl
    {
       
        margin-top:0px;
        float:left; position:relative;
    }
   
   .side_drp{float:left; margin-right:5px;}    
    .style30
    {
        width: 100%;
    }
    
    
     .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=50);
            opacity: 1.2;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 22px;
            padding-left: 0px;
            width: 300px;
            height: 410px;
            top:250px;
        }
        .lblManan
        {
           display: block;
	color: #333;
    font-family: Georgia,"Times New Roman",Times,serif;
    font-weight: normal
        }
</style>
<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
 <!--<script type="text/javascript" src="Scripts/jquery-1.8.2.js"></script>-->


  

 
 
        
   <table>
   <tr>
   <td>
    <div style="float: left;color:Red;">
                         <asp:Label ID="lbl_msg" runat="server" Text="" ></asp:Label>
                    </div>
      <br />
       <asp:Button ID="Button1" runat="server" onclick="Button1_Click1"  Visible="false" Text="Click Here For Select Business For Send mail" />
   </td>
   </tr>
   </table>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" GroupingText="Mass Mail Send List" HorizontalAlign="Left"><br />
               <table class="style30">
               <tr>
             
                                
             <td>                       <asp:Label CssClass="lblManan" Text="Email Template" ID="lblMailType" 
                                                 runat="server" Width="141px" />    <asp:DropDownList ID="ddlMailType" runat="server" OnSelectedIndexChanged="ddlMailTypeselectedindex" AutoPostBack="true">
                                          </asp:DropDownList>

                                
               </td>
              <td>
              Business Name
                <asp:TextBox ID="txt_business" runat="server"></asp:TextBox>
              
              </td>
               <td align="left">
               From Date
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox1"
                    PopupButtonID="ImageButton2">
                </cc1:CalendarExtender>
                <span lang="en-us"></span><asp:ImageButton ID="ImageButton2" runat="server"
                    ImageUrl="~/images/cal_actbtn.jpg" />
              
               </td>
               <td align="left">
                <span lang="en-us" style="width: 30%; font-weight: bold; font-size: 14px; color: #416271;
                    padding-left: 20px;">&nbsp;To Date:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox2"
                        PopupButtonID="ImageButton1">
                    </cc1:CalendarExtender>
                  <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                     
                      
               </td>
               <td>
               <asp:Button ID="Button3" runat="server"   
                           Text="Go" 
                           onclick="Button2_Click11" />
               </td>
               <td>
               </td>
               <td>
               </td>
               </tr>
               <tr>
               <td></td>
               </tr>

               <tr>
               <td align="right" colspan="6">
                 <asp:Button ID="Button2" runat="server"   
                           Text="We will remove duplicate entries of the same recipent to send out mail" 
                           onclick="Button2_Click" />
               </td>
               </tr>
                   <tr>
                       <td colspan="6">
                           <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="content"  DataKeyNames="MassemailMasterID" EmptyDataText="No Record Has Been Found" Width="100%" PageSize="10" onrowcommand="GridView1_RowCommand" 
                               onrowdatabound="GridView1_RowDataBound" onpageindexchanging="GridView1_PageIndexChanging">
                <%--OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting" --%>
                              <%-- onselectedindexchanged="GridView1_SelectedIndexChanged" 
                              onrowcommand="GridView1_RowCommand"--%> 
                <AlternatingRowStyle BackColor="WhiteSmoke" />
                               <Columns>
                              
                                   <asp:BoundField DataField="MassemailMasterID" HeaderText="ID " />
                                     <asp:TemplateField HeaderText="Business Name" >
                                       <ItemTemplate>
                                           
                                       <asp:Label ID="lbl_businessname" runat="server" Text='<%# Eval("BusinessName") %>' ></asp:Label> 
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MailId">
                                        <ItemTemplate>
                                           <asp:Label ID="lbl_email" runat="server" Text='<%# Eval("BusinessEmailID") %>' ></asp:Label> 
                                              <asp:Label ID="Label1" runat="server" Text='<%# Eval("BusinessEmailID") %>' Visible="false"></asp:Label> 
                                            <asp:Label ID="lbl_Message" runat="server" Text='<%# Eval("Message") %>' Visible="false"></asp:Label> 
                                                    <asp:Label ID="lbl_Master_EmailSentSubject" runat="server" Text='<%# Eval("MasterEmailSentSubject") %>' Visible="false"></asp:Label> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:BoundField DataField="Mail_message_FormatName" HeaderText="Formate Name" />
                                   <asp:BoundField DataField="date" HeaderText="Send Date" />
                                   <asp:BoundField DataField="MasterEmailSentSubject" HeaderText="Mail Subject" />

                                   <asp:TemplateField HeaderText="Send Mail" >
                                       <ItemTemplate>
                                           <asp:CheckBox ID="chkSelect" runat="server" Checked="true"   />
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   
                                    
                                
                               </Columns>
                <EmptyDataRowStyle Font-Bold="True" HorizontalAlign="Center" />
                 <HeaderStyle BackColor="#416271" ForeColor="Black" />
                              
                               <SelectedRowStyle BackColor="White" />
            </asp:GridView>




            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" 
                               AutoGenerateColumns="False" CssClass="content"  DataKeyNames="BusinessEmailID" 
                               EmptyDataText="No Record Has Been Found" Width="100%" PageSize="20" onrowcommand="GridView1_RowCommand" 
                          Visible="false"     onrowdatabound="GridView1_RowDataBound" 
                               onpageindexchanging="GridView1_PageIndexChanging" 
                               onselectedindexchanged="GridView2_SelectedIndexChanged" >
                <%--OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting" --%>
                              <%-- onselectedindexchanged="GridView1_SelectedIndexChanged" 
                              onrowcommand="GridView1_RowCommand"--%> 
                <AlternatingRowStyle BackColor="WhiteSmoke" />
                               <Columns>
                                    <asp:TemplateField HeaderText="MailId">
                                        <ItemTemplate>
                                           <asp:Label ID="lbl_email" runat="server" Text='<%# Eval("BusinessEmailID") %>' ></asp:Label> 
                                            <asp:Label ID="lbl_Message" runat="server" Text='<%# Eval("Message") %>' ></asp:Label> 
                                              <asp:Label ID="lbl_Master_EmailSentSubject" runat="server" Text='<%# Eval("MasterEmailSentSubject") %>' ></asp:Label> 
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     
                               </Columns>
                <EmptyDataRowStyle Font-Bold="True" HorizontalAlign="Center" />
                 <HeaderStyle BackColor="#416271" ForeColor="Black" />
                              
                               <SelectedRowStyle BackColor="White" />
            </asp:GridView>
                       </td>
                   </tr>
                  
               </table>

           </asp:Panel>
               
   
   
     <div style="clear: both;">
        </div>
          


              <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" Text="V5" ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>
</asp:Content>

