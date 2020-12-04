<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" AutoEventWireup="true" CodeFile="FrmTaskReportAll.aspx.cs" Inherits="Admin_FrmTaskReportAll" Title="OnlineMis" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register assembly="FreeTextBox" namespace="FreeTextBoxControls" tagprefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
    </style>
<script language="javascript" type="text/javascript" >    



function CallPrint(strid)
{
    var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML,'<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

      } 
      
      function mask(evt)
        { 

           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered invalid character");
                  return false;
             }
             
             
               
            
        }  
        function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
            }
        
        
      
        
            counter=document.getElementById(id);
            
            if(txt1.value.length <= max_len)
            {
                remaining_characters=max_len-txt1.value.length;
                counter.innerHTML=remaining_characters;
            }
        }    
            function mak(id,max_len,myele)
        {
            counter=document.getElementById(id);
            
            if(myele.value.length <= max_len)
            {
                remaining_characters=max_len-myele.value.length;
                counter.innerHTML=remaining_characters;
            }
        }       
</script> 


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
          <div class="products_box">
                         <fieldset>
                         <legend>
                          <asp:Label runat="server" ID="Label4" Text="All Employee Task Report Date Wise"></asp:Label>
                         </legend>
<table width= "100%">
       
       
      
        <tr>
            <td style="width:30%" >
             <label>
                                        <asp:Label ID="lblwname" runat="server"  Text="Business Name "></asp:Label>
                                   </label>
             
               
        
               </td>
               <td  style="width:70%">
               <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddlstore_SelectedIndexChanged" >
    </asp:DropDownList>
               </td>
            
        </tr>
        <tr>
            <td style="width:30%" >
            <label>
             <asp:Label ID="EmpName" runat="server" Text="Employee Name " 
                ></asp:Label> 
                
        </label>
               </td>
               <td style="width:70%">
               <label>
               <asp:DropDownList ID="ddlemp" runat="server" 
                onselectedindexchanged="ddlemp_SelectedIndexChanged" >
            </asp:DropDownList>
            </label>
               </td>
            
        </tr>
        
        
         <tr>
         <td style="width:30%">
          <label>
             <asp:Label ID="Label2" runat="server" Text="Start Date"  ></asp:Label> 
              <asp:Label ID="Label3" runat="server" Text="*"  ></asp:Label> 
             
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtStartDate" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
             </label>
              </td>
         <td style="width:70%">
         <label>
                  <asp:TextBox ID="txtStartDate" runat="server" Width="141px" Height="16px"></asp:TextBox>
       
                <cc1:calendarextender runat="server" id="cal1" targetcontrolid="txtStartDate" 
                    popupbuttonid="img1">
                </cc1:calendarextender> 
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtStartDate">
                </cc1:MaskedEditExtender>
                </label>

                <label>
                 <asp:Image ID="img1" runat="server" ImageUrl="~/ShoppingCart/images/cal_btn.jpg"
                     Width="20px"  />
                     </label>

         </td>
         </tr>
         <tr>
         <td style="width:30%">
          <label>
             <asp:Label ID="Label5" runat="server" Text="End Date"  ></asp:Label> 
              <asp:Label ID="Label6" runat="server" Text="*"  ></asp:Label> 
             
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtEndDate" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
         </label>
         </td>
         <td  style="width:70%">
         <label>
         <asp:TextBox ID="txtEndDate" runat="server" Width="141px" Height="16px"> </asp:TextBox>
               
                <cc1:calendarextender runat="server" id="cal2" targetcontrolid="txtenddate" 
                    popupbuttonid="img2">                  
                                    
                </cc1:calendarextender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtEndDate">
                </cc1:MaskedEditExtender>
                </label>
                <label>
                <asp:Image ID="img2" runat="server" Height="17px"
                    ImageUrl="~/ShoppingCart/images/cal_btn.jpg" Width="20px"  />
                    </label>
         </td>
         </tr>
             
        <tr>
        <td style="width:30%">
        </td>
        <td style="width:70%">
        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" onclick="Button1_Click" Text="Go" 
                     />
        </td>
        
        </tr>
        </table>
       
         <div style="float: right;">
                        <asp:Button ID="btnprintableversion" runat="server" CssClass="btnSubmit" Text="Printable Version" 
                            onclick="btnprintableversion_Click" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                             type="button" class="btnSubmit" value="Print" visible="False" />
                             </div>
        <div style="clear: both;">
                    </div> 
       
       <%-- <input type="button" value="print" id="btnPrint" visible="false" runat="server" onclick="javascript:CallPrint('divPrint')" />--%>
    <asp:Panel ID="pnlgrid" runat="server" Width="100%" >
    <table width="100%">
              <tr align="center">
                                    <td colspan="2">
                                        <div id="mydiv" class="closed">
                                            <table width="100%">
                                            <tr>
                                             <td align="center" style="font-size: 20px; font-family:Calibri;  font-weight: bold; color: #000000">
                                                        <asp:Label ID="lblcmpny" Font-Italic="true" runat="server" ></asp:Label>
                                                    </td>
                                            </tr>
                                               
                                                <tr>
                                                <td align="center" style="text-align: center; font-family:Calibri; font-size:18px; font-weight: bold;">
                                                <asp:Label ID="Label11" runat="server" Font-Italic="true" Text="Task Report by Project"  ></asp:Label>
                                                </td>
                                                </tr>
                                              
                                                
                                               
                                                
                                            </table>
                                        </div>
                                    </td>
              </tr>
              <tr>
          <td colspan="2"> 
  
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                  CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                     OnPageIndexChanging="GridView1_PageIndexChanging"
                    Width="100%" PageSize="12" EmptyDataText="No Record Found."  >
                    <Columns>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="EmployeeName" HeaderText="Name" />
                        <asp:BoundField  HeaderStyle-HorizontalAlign="Left" DataField="TaskAllocationDate" 
                            DataFormatString="{0:MM-dd-yyyy}" HeaderText="Date" />
                        <asp:BoundField DataField="ProjectName" HeaderStyle-HorizontalAlign="Left" HeaderText="Project" />
                        <asp:BoundField DataField="TaskName" HeaderStyle-HorizontalAlign="Left" HeaderText="Task" />
                        <asp:TemplateField HeaderText="Task Report" HeaderStyle-HorizontalAlign="Left">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("TaskReport") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("TaskReport") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supervisor Note" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblsupervisornote" runat="server" TextMode="MultiLine"  Text='<%# Eval("supervisornote")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:TextBox ID="txtsupervisornote" runat="server" Text='<%# Eval("supervisornote")%>' Height="100px" Width="350px">
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EUnitsAlloted" HeaderStyle-HorizontalAlign="Left" HeaderText="Allocated Minute" />
                        <asp:BoundField DataField="UnitsUsed" HeaderStyle-HorizontalAlign="Left" HeaderText="Used Minute" />
                    </Columns>
                    
                </asp:GridView>
            </td>
        </tr>
    </table>
    </asp:Panel>
        
    
    </fieldset>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>  
    
    
</asp:Content>

