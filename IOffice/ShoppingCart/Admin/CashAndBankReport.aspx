<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" AutoEventWireup="true"
    CodeFile="CashAndBankReport.aspx.cs" Inherits="account_CashAndBankReport" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    
    <script language="javascript" type="text/javascript">
        function CallPrint1(strid) {
            var prtContent = document.getElementById('<%= pnlpt.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }    
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }  
    </script>
     <asp:UpdatePanel ID="pnlupp" runat="server">
        <ContentTemplate>
           <div class="products_box">
             
                
                
                 <table style="width: 100%;" >
                   <tr>
            <td align="left" class="label" colspan="3">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
       <td colspan="3">   
       <fieldset>
                <legend></legend>
                <table width=100%">
                 <tr>
                                           
                                            <td   width="20%">
                                             <label>
                                                    <asp:Label ID="Label5" runat="server" Text="Select Business Name" ></asp:Label>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="ddlwarehouse" ErrorMessage="*" ValidationGroup="58" 
                                    ></asp:RequiredFieldValidator>
                                                </label>
                                              </td>
                                                 <td colspan="2" >
                                               <label>
                                                   <asp:DropDownList ID="ddlwarehouse" runat="server" 
                            AutoPostBack="True" onselectedindexchanged="ddlwarehouse_SelectedIndexChanged" >
                                    
                                </asp:DropDownList>
                               
                                                </label>
                                            </td>
                                              
                                          
                                           
                                        </tr>
                                             <tr>
                                       <td >
                                       <label> <asp:Label ID="Label17" runat="server" Text="Search By " ></asp:Label></label>
                                       </td>
                                                            <td style="width:20%;">
                                                               <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Value="0">Date</asp:ListItem>
                    <asp:ListItem Value="1" Selected="True">Period</asp:ListItem>
                </asp:RadioButtonList>
                                                            </td>
                                                            <td >
                                                                <asp:Panel ID="Panel1" runat="server" Visible="False">
                                                                   <label>
                                                                        <asp:Label ID="Label2" runat="server" Text="From Date "></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" 
                                                                            ControlToValidate="txtdatefrom" ErrorMessage="*" 
                                                                            ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </label>
                                                                    <label>
                                                                        <asp:TextBox ID="txtdatefrom" runat="server" Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                                                            CultureName= "en-AU"  Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom"/>
                                                                        
                                                                        <cc1:CalendarExtender  ID="CalendarExtender3" runat="server" PopupButtonID="imgbtn1"
                                                                                TargetControlID="txtdatefrom">
                                                                        </cc1:CalendarExtender>
                                                                    </label>
                                                                    <label>
                                                                        <asp:ImageButton ID="imgbtn1" runat="server" ImageUrl="~/images/cal_btn.jpg" />
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="Label4" runat="server" Text="To Date "></asp:Label>
                                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="true"
                                                                                ControlToValidate="txtdateto" ErrorMessage="*" 
                                                                            ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </label>
                                                                    <label>
                                                                        <asp:TextBox ID="txtdateto" runat="server" Width="80px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                                                            CultureName= "en-AU"  Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdateto"/>
                                                                                     
                                                                        <cc1:CalendarExtender   ID="CalendarExtender4" runat="server" PopupButtonID="ImageButton5"
                                                                                TargetControlID="txtdateto">
                                                                        </cc1:CalendarExtender>
                                                                    </label>
                                                                    <label>
                                                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/images/cal_btn.jpg" OnClick="imgbtn2_Click" />
                                                                    <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                          
                                                                    </label>
                                                                    </asp:Panel>
                                                                     <asp:Panel ID="Panel12" runat="server" Visible="False">
                                                                    <label>
                                                                                    <asp:Label ID="Label6" runat="server" Text="Period "></asp:Label>
                                                                                </label>
                                                                                 <label>
                                                                                    <asp:DropDownList ID="ddlperiod" runat="server" 
                                                                             onselectedindexchanged="ddlperiod_SelectedIndexChanged" Width="150px">
                                                                                    </asp:DropDownList>
                                                                                </label>
                                                                    </asp:Panel>
                                                            </td>
                                                        </tr>
                                                          <tr>
                                            <td  >
                                                 <label>
                                                    <asp:Label ID="Label7" runat="server" Text="Filter by Account " Width="143px"></asp:Label>
                                                </label>
                                              </td>  <td colspan="2">
                                                <label>
                                                    <asp:DropDownList ID="ddlcashbank" runat="server" Width="500px">
                                                    </asp:DropDownList>
                                                </label>
                                                
                                            </td>
                                            
                                        </tr>
                                         <tr>
                        <td></td>
                            <td colspan="2">
                                <asp:Button ID="btnGo" runat="server" Text=" Go " CssClass="btnSubmit" 
                                    OnClick="btnGo_Click" ValidationGroup="1"  />
                                
                               
                            </td>
                        </tr>
                        </table>
                        </fieldset>
      
                </td>
        </tr>
                                     
      
       <tr>
                    <td colspan="3">
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label21" runat="server" Text="List of General Ledgers"></asp:Label>
                            </legend>
      <table width="100%">
      <tr>
      <td>
       <div style="float:right">
                        <asp:Button ID="Button2" runat="server" Text="Printable Version" CssClass="btnSubmit" 
                    onclick="Button2_Click" />
                        <input type="button" value="Print" id="Button3" runat="server" onclick="javascript:CallPrint('divPrint')" visible="false" class="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                      <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                      
                         <table width="100%">
                                <tr>
                                    <td>
                                        <div id="mydiv" class="closed">
                                            <table width="100%" style="color:Black; font-weight:bold; font-style:italic; text-align:center">
                                                <tr>
                                                    <td align="center" >
                                                        <asp:Label ID="lblcompname" runat="server" Font-Size="20px"></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td align="center" >
                                                        <asp:Label ID="Label22" runat="server" Font-Size="20px" Text="Business : "></asp:Label>
                                                        <asp:Label ID="lblstore" runat="server" Font-Size="20px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" >
                                                        <asp:Label ID="Label19" runat="server" Text="List of General Ledgers" Font-Size="18px" ></asp:Label>
                                                    </td>
                                                </tr>
                                               
                                            </table>
                                        </div>
                                    </td>
                                    </tr>
                                     <tr>
                                                <td  align="left">
                                                <%-- <asp:Label ID="lbl1" runat="server" class="subinnertblfc" Text="Opening Balance"  Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblOpeningBal" class="subinnertblfc" runat="server" Visible="False"></asp:Label>&nbsp; 
                                                 <asp:Label ID="lbl2" runat="server" class="subinnertblfc" Text="as on Date  " Visible="False"></asp:Label><asp:Label ID="lblOpningBalStartDate" class="subinnertblfc" runat="server" Visible="False"></asp:Label>
          --%><asp:Panel ID="gpanel" runat="server" Visible="false">
                                                      <asp:Label ID="gaccname" runat="server" class="subinnertblfc" Text="as on" Visible="true"></asp:Label>
                                              
                                                        <asp:Label ID="gason" runat="server" class="subinnertblfc" Text="as on" Visible="true"></asp:Label>
                                                        <asp:Label ID="gdate" class="subinnertblfc" runat="server" Visible="true"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="gmes" runat="server" class="subinnertblfc" Text=" Opening Balance "
                                                            Visible="true"></asp:Label>
                                                         <asp:Label ID="lblOpeningBal" class="subinnertblfc" runat="server" Visible="true"></asp:Label>
                                                          <asp:Label ID="lblOpningBalStartDate" class="subinnertblfc" runat="server" Visible="false"></asp:Label>
                                       
                                                   </asp:Panel>
                                                </td>
                                                </tr>
                                    <tr>
                                    <td>
                                           
                <asp:GridView ID="grdCashBankReport" runat="server" AutoGenerateColumns="False"
                    EmptyDataText="There is no Record" Width="100%" 
                    OnRowCommand="grdCashBankReport_RowCommand" 
                    OnSorting="grdCashBankReport_Sorting" 
                    DataKeyNames="Tranction_Master_Id" 
                    OnRowDataBound="grdCashBankReport_RowDataBound"  CssClass="mGrid" PagerStyle-CssClass="pgr" 
                                                                        AlternatingRowStyle-CssClass="alt">
                    <Columns>
                     
                    <asp:TemplateField HeaderText="Account" SortExpression="Account" HeaderStyle-Width="80px"  HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblAcc1" Visible="false" runat="server" Text='<%# Bind("Account") %>'></asp:Label>
                                   </ItemTemplate>
                            <EditItemTemplate>
                            </EditItemTemplate>
                            <HeaderStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" SortExpression="Date"  HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Entry Type"  SortExpression="EntryType"  HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblentrytype" runat="server" Text='<%# Bind("EntryType") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Entry No." SortExpression="EntryNo"  HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblentryno" runat="server" Text='<%# Bind("EntryNo") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Account" SortExpression="Account name"  HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblAcc" runat="server" Text='<%# Bind("Accountr") %>'></asp:Label>
                              
                                      <asp:Label ID="lbltradeid" runat="server" Visible="false" Text='<%# Bind("Tranction_Details_Id") %>'></asp:Label>
                                
                                <asp:LinkButton ID="link1" runat="server" Text="Split" ForeColor="#457cec" OnClick="link1_Click"></asp:LinkButton>
                         
                            </ItemTemplate>
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:TemplateField>
                      
                       
                        <asp:TemplateField HeaderText="Credit<br/>(Decrease)" SortExpression="Credit" ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblAmtCredit" runat="server" Text='<%# Bind("Credit") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debit<br/>(Increase)" SortExpression="Debit" ItemStyle-HorizontalAlign="Right"   HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblAmtDetail" runat="server" Text='<%# Bind("Debit") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:TemplateField>
                      
                         <asp:TemplateField HeaderText="Balance"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right"  >
                            <ItemTemplate>
                              <asp:Label ID="lblBalance" runat="server"  Text='<%# Bind("Balance") %>' ></asp:Label>
                               </ItemTemplate>
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Detail Memo" SortExpression="DetMemo"  HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbldetailmemo" runat="server" Text='<%# Bind("DetMemo") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:TemplateField>
                     
                       
                      
                      
                       
                         <asp:TemplateField HeaderText="TranctionMId" Visible="False">
                         <ItemTemplate>
                                <asp:Label ID="lblTransactionMasterId" runat="server" Text='<%# Bind("Tranction_Master_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="View Doc" ItemStyle-Width="5%" HeaderStyle-Width="5%"  HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                     <asp:Label ID="lbldocno" runat="server"></asp:Label>
                                                        <asp:ImageButton ID="img1" runat="server" CausesValidation="true" ImageUrl="~/ShoppingCart/images/Docimg.png"
                                            AlternateText="" Height="22px" Width="22px" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Tranction_Master_Id")%>'
                                            CommandName="AddDoc">
                                                        </asp:ImageButton>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Add Doc" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lbladdd" runat="server"  CommandArgument='<%# Eval("Tranction_Master_Id") %>' CommandName="Docadd"  ToolTip="Add Doc" Height="20px" ImageUrl="~/Account/images/attach.png" Width="20px">
                                            </asp:ImageButton>
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
      </table>
      </fieldset>
      </td>
      </tr>
    
          
        
        <tr>
        <td colspan="3">
          <asp:Panel ID="Paneldoc" runat="server" Width="600px" CssClass="modalPopup">
                        <fieldset>
                                               <legend>
                                                   <asp:Label ID="lbldoclab" runat="server" Text="List of Document"></asp:Label>
                                               </legend>
                           <table width="100%">
                               <tr>
                                   <td style="width:95%;">
                                   </td>
                                   <td align="right">
                                       <asp:ImageButton ID="ImageButton3" runat="server" 
                                           ImageUrl="~/images/closeicon.jpeg"
                                           Width="16px" />
                                   </td></tr>
                                   <tr>
                                   <td colspan="2">
                                       <label>
                                       <asp:Label ID="lblheadoc" runat="server" 
                                           Text="List of documents attached to "></asp:Label>
                                   <%--    <asp:Label ID="Label22" runat="server" 
                                           Text="List of documents attched to Entry Type"></asp:Label>--%>
                                     <asp:Label ID="lbldocentrytype" runat="server" Font-Bold="True" 
                                           ForeColor="#457cec"></asp:Label>
                                       <asp:Label ID="Label23" runat="server" Text=" entry no."></asp:Label>
                                       <asp:Label ID="lbldocentryno" runat="server" Font-Bold="True" 
                                           ForeColor="#457cec"></asp:Label>
                                       </label>
                                   </td>
                               </tr>
                                   <tr>
                                   <td colspan="2">
                                    
                                                           <asp:Panel ID="pvgris" runat="server" Height="100%" ScrollBars="Vertical" 
                                                               Width="100%">
                                                               <asp:GridView ID="grd" runat="server" AlternatingRowStyle-CssClass="alt" 
                                                                   AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="Id"  HeaderStyle-HorizontalAlign="Left"
                                                                   OnRowCommand="grd_RowCommand" PagerStyle-CssClass="pgr" Width="100%">
                                                                   <Columns>
                                                                       <asp:BoundField DataField="Datetime" HeaderText="Date" HeaderStyle-HorizontalAlign="Left"/>
                                                                       <asp:BoundField DataField="IfilecabinetDocId" HeaderText="ID" HeaderStyle-HorizontalAlign="Left" />
                                                                       <asp:BoundField DataField="Titlename" HeaderText="Title" HeaderStyle-HorizontalAlign="Left"/>
                                                                       <asp:BoundField DataField="Filename" HeaderText="Cabinet-Drawer-Folder" HeaderStyle-HorizontalAlign="Left" />
                                                                       <asp:TemplateField HeaderText="View Doc" ItemStyle-ForeColor="#416271">
                                                                           <ItemTemplate>
                                                                               <a href="javascript:void(0)" 
                                                                                   onclick='window.open(&#039;viewdocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "IfilecabinetDocId")%>&#039;, &#039;welcome&#039;,&#039;width=1200,height=700,menubar=no,status=no&#039;)'>
                                                                               <asp:Label ID="lbldoc" runat="server" ForeColor="#416271" Text="View Doc"></asp:Label>
                                                                               </a>
                                                                           </ItemTemplate>
                                                                       </asp:TemplateField>
                                                                   </Columns>
                                                               </asp:GridView>
                                                           </asp:Panel>
                                                      
                                   </td>
                               </tr>
                           </table>
                        
                        
                         </fieldset></asp:Panel>
                                <asp:Button ID="Button4" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Paneldoc" TargetControlID="Button4" CancelControlID="ImageButton3">
                                </cc1:ModalPopupExtender>
        </td>
        </tr>
            <tr>
            <td class="label" colspan="3">
                           <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="#666666" 
                                                BorderStyle="Outset" Width="300px">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="subinnertblfc">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblm" runat="server" ForeColor="Black">Please check the date.</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="height: 26px">
                                                            <asp:Label ID="Label3" runat="server" ForeColor="Black" 
                                                                Text="Start Date of the Year is "></asp:Label>
                                                            <asp:Label ID="lblstartdate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="height: 26px">
                                                            <asp:Label ID="lblm0" runat="server" ForeColor="Black">You can not select 
                                                            anydate earlier than that. </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="height: 26px">
                                                            <asp:Button ID="ImageButton2" runat="server" Text="Cancel" 
                                                                 OnClick="ImageButton2_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                &nbsp;</asp:Panel>
                                                 <asp:Button ID="btnmd" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                                        ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="Panel5" TargetControlID="btnmd">
                                    </cc1:ModalPopupExtender>
                                         
</td>
        </tr>
        <tr>
        <td colspan="3">
               <asp:Panel ID="pnladdress" runat="server"  CssClass="modalPopup" Width="520px" >
                  <table width="100%">
                  <tr>
                  <td align="right">
                    <input type="button" value="Print" id="Button1" runat="server" onclick="javascript:CallPrint1('pnlpt');"
                                    class="btnSubmit"  visible="true" />
                      &nbsp;
                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                        Width="16px"  />
                  </td>
                  </tr>
                  <tr>
                  <td>
                  <asp:Panel ID="pnlpt" runat="server">
                   <fieldset >
                            <legend >
                               <asp:Label ID="Label25" runat="server" Text="List of Accounts Entry Detail"></asp:Label> 
                             </legend>
                       <table width="100%">
                      
                        <tr>
                                   <td>
                                       <label>
                                      
                                       <asp:Label ID="Label32" runat="server" Text="Business Name:"> 
                                           </asp:Label>
                                           </label>
                                             <label>
                                               <asp:Label ID="lblbnp" runat="server" Text=""> 
                                           </asp:Label>
                                           </label>
                                        <label>&nbsp;</label>
                                           
                                                <label>
                                       <asp:Label ID="flbd" runat="server" Text="Date:"> 
                                           </asp:Label>
                                           </label>
                                             <label>
                                       <asp:Label ID="lbldatep" runat="server" Text=""> 
                                           </asp:Label>
                                           </label>
                                               </td>
                                               </tr>
                                               <tr>  
                                                  <td>
                                       <label>
                                      
                                       <asp:Label ID="Label31" runat="server" Text="Entry Type:"> 
                                           </asp:Label>
                                           </label>
                                             <label>
                                               <asp:Label ID="lbletp" runat="server" Text=""> 
                                           </asp:Label>
                                           </label>
                                          <label>&nbsp;</label>
                                          <label>
                                       <asp:Label ID="Label34" runat="server" Text="Entry Number:"> 
                                           </asp:Label>
                                           </label>
                                             <label>
                                       <asp:Label ID="lblenp" runat="server" Text="Date:"> 
                                           </asp:Label>
                                           </label>
                                               </td>
                               </tr>
                       <tr>
                       <td>
                           <asp:Panel ID="pnlof" runat="server"  ScrollBars="Auto"> 
                            
                             <asp:GridView ID="grdPartyList" runat="server" Width="100%" AutoGenerateColumns="False"
                               CssClass="mGrid" PagerStyle-CssClass="pgr"   
                                   AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found."
                                PageSize="20">
                                                
                                                <%-- <EmptyDataRowStyle ForeColor="Peru" />--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Account" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblacccc" Visible="true" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="32%" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Memo" SortExpression="Memo"  HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmem" Visible="true" runat="server" Text='<%#Eval("Memo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="32%" />
                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit" SortExpression="AmountDebit"  HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebit" Visible="true" runat="server" Text='<%#Eval("AmountDebit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="18%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit" SortExpression="AmountCredit"  HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" Visible="true" runat="server" Text='<%#Eval("AmountCredit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="18%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                   
                                </Columns>
                               
                                                <PagerStyle CssClass="pgr" />
                                                <AlternatingRowStyle CssClass="alt" />
                               
                            </asp:GridView>
                          </asp:Panel>
                       </td>
                       </tr>
                       </table>
                           </fieldset></asp:Panel>
                  </td>
                  </tr>
                  </table>
                      </asp:Panel>
       <asp:Button ID="ImgBtnAddress" runat="server" Style="display: none" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="modalBackground"
        PopupControlID="pnladdress" TargetControlID="ImgBtnAddress"  runat="server" CancelControlID="ImageButton1">
    </cc1:ModalPopupExtender>
        </td>
        </tr>
        
        
           
    </table>
               
                
                
                
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
   
  
</asp:Content>


