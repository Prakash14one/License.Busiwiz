<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="PayrollRemittanceSummary.aspx.cs"
    Inherits="PayrollRemittanceSummary" Title="Untitled Page" %>

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


        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= Panel1.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }  

  
    </script>

   <asp:UpdatePanel ID="udfd" runat="server" >
   <ContentTemplate>
    <div class="products_box">
        <fieldset>
            <legend>
                <asp:Label ID="statuslable" runat="server" Text="Payroll Deductions and Remittance Report"></asp:Label>
            </legend>
            <label>
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </label>
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend></legend>
                <table width="100%">
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label6" runat="server" Text="Business"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <input id="hdnsortExp11" runat="Server" name="hdnsortExp" type="hidden" />
                                            <input id="hdnsortDir11" runat="Server" name="hdnsortDir" type="hidden" />
                                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" type="hidden" />
                                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" type="hidden" /></label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label10" runat="server" Text="Year"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlYear" runat="server" Height="16px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Button ID="btnGo" runat="server" Text=" Go " CssClass="btnSubmit" OnClick="btnGo_Click" />
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div style="clear: both;">
                <asp:Panel ID="pnlsh" runat="server" Visible="false">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lblGr1" runat="server" Text="List of Payroll Deductions and Remittance Report for the Year"></asp:Label></legend>
                             <div style="float: right;">
                       <asp:Button ID="btnmpr" runat="server" CausesValidation="false" Text="Printable Version"
                                                        CssClass="btnSubmit" OnClick="btnmpr_Click" />
                     <input id="btnpm" runat="server" visible="false" onclick="document.getElementById('Div1').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                                        class="btnSubmit" type="button" value="Print" />
                    </div>
                    <div style="clear: both;">
                    </div>
                        <table style="width: 100%">
                           <%-- <tr>
                                <td>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right" style="width: 230px;">
                                                <label>
                                                    <asp:Button ID="btnmpr" runat="server" CausesValidation="false" Text="Printable Version"
                                                        CssClass="btnSubmit" OnClick="btnmpr_Click" />
                                                </label>
                                                <label>
                                                    <input id="btnpm" runat="server" visible="false" onclick="document.getElementById('Div1').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                                        class="btnSubmit" type="button" value="Print" /></label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>--%>
                            <tr>
                                <td align="right">
                                    <asp:Panel ID="Panel1" runat="server" Visible="true">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr align="center">
                                                <td>
                                                    <div id="Div1" class="closed">
                                                        <table width="100%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Label ID="lbltrbal" runat="server" Style="text-align: center; font-family: Calibri;
                                                                        font-size: 18px; font-weight: bold;" Text="List of Payroll Deductions and Remittance Report for the Year"> </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="text-align: center; font-size: 16px;font-weight: bold;"">
                                                                    <asp:Label ID="lblbusnn" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="text-align: center; font-size: 16px;font-weight: bold;"">
                                                                    <asp:Label ID="lblpayperiod"  runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Panel ID="pnlGrdemp" runat="server" Visible="true" Width="100%" ScrollBars="Horizontal">
                                                        <asp:GridView ID="grdallemp" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                            EmptyDataText="No Record Found." FooterStyle-BackColor="#416271" PagerStyle-CssClass="pgr"
                                                            CssClass="mGrid" Width="100%" DataKeyNames="EmployeeId"
                                                            OnSorting="grdallemp_Sorting" HeaderStyle-HorizontalAlign="Left" OnRowCreated="grdallemp_RowCreated"
                                                            ShowFooter="True" AllowPaging="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Pay Period" SortExpression="EmployeeName" Visible="true"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblempname" runat="server" Text='<%#Bind("EmployeeName") %>' ForeColor="Black"></asp:Label><asp:Label
                                                                            ID="lblEmployeeId" runat="server" Text='<%#Bind("EmployeeId") %>' Visible="false"></asp:Label></ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                     <FooterTemplate>
                                                                                           
                                                                                                <asp:Label ID="lblfoll" runat="server" Text="Total" Font-Size="15px" ForeColor="White"></asp:Label>
                                                                                         
                                                                                        </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" SortExpression="G1" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblg1" runat="server" Text='<%#Bind("G1","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                         <FooterStyle BackColor="#8EB4BF" /> <HeaderStyle BackColor="#8EB4BF" /> <ItemStyle BackColor="#8EB4BF" />  <FooterTemplate>
                                                                        <asp:Label ID="lblfootg1" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" SortExpression="G2" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblg2" runat="server" Text='<%#Bind("G2","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                       <FooterStyle BackColor="#8EB4BF" /> <HeaderStyle BackColor="#8EB4BF" /> <ItemStyle BackColor="#8EB4BF" /> <FooterTemplate>
                                                                        <asp:Label ID="lblfootg2" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" SortExpression="G3" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblg3" runat="server" Text='<%#Bind("G3","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                      <FooterStyle BackColor="#8EB4BF" /> <HeaderStyle BackColor="#8EB4BF" /> <ItemStyle BackColor="#8EB4BF" />  <FooterTemplate>
                                                                        <asp:Label ID="lblfootg3" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" SortExpression="G4" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblg4" runat="server" Text='<%#Bind("G4","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                      <FooterStyle BackColor="#8EB4BF" /> <HeaderStyle BackColor="#8EB4BF" /> <ItemStyle BackColor="#8EB4BF" />  <FooterTemplate>
                                                                        <asp:Label ID="lblfootg4" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" SortExpression="G5" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblg5" runat="server" Text='<%#Bind("G5","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                      <FooterStyle BackColor="#8EB4BF" /> <HeaderStyle BackColor="#8EB4BF" /> <ItemStyle BackColor="#8EB4BF" />  <FooterTemplate>
                                                                        <asp:Label ID="lblfootg5" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" SortExpression="NG1" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblng1" runat="server" Text='<%#Bind("NG1","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblfootng1" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" SortExpression="NG2" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblng2" runat="server" Text='<%#Bind("NG2","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblfootng2" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" SortExpression="NG3" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblng3" runat="server" Text='<%#Bind("NG3","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblfootng3" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" SortExpression="NG4" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblng4" runat="server" Text='<%#Bind("NG4","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblfootng4" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" SortExpression="NG5" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblng5" runat="server" Text='<%#Bind("NG5","{0:n}") %>' ForeColor="Black"></asp:Label></ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblfootng5" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Remittance<br>Required" SortExpression="TotRemreq"
                                                                    Visible="true" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltotremreq" runat="server" Text='<%# Bind("TotRemreq","{0:n}") %>'
                                                                            ForeColor="Black"></asp:Label></ItemTemplate>
                                                                        <FooterStyle BackColor="#8EB4BF" /> <HeaderStyle BackColor="#8EB4BF" /> <ItemStyle BackColor="#8EB4BF" />  <FooterTemplate>
                                                                        <asp:Label ID="lblfootremreq" runat="server" Font-Size="15px" ForeColor="White"></asp:Label></FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Date" Visible="true" SortExpression="totremreqi" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblremdatetax" runat="server" Text='<%# Bind("Remdate")%>'  ForeColor="Black"></asp:Label>
                                                                                 </ItemTemplate>
                                                                       <FooterStyle BackColor="#8EB4BF" /> <HeaderStyle BackColor="#8EB4BF" /> <ItemStyle BackColor="#8EB4BF" /> 
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount<br> remitted" Visible="true" SortExpression="amtremited"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblremamt" runat="server" Text='<%# Bind("Remamttax")%>'  ForeColor="Black"></asp:Label>
                                                                         
                                                                     
                                                                    </ItemTemplate>
                                                                        
                                                                   <FooterStyle BackColor="#8EB4BF" /> <HeaderStyle BackColor="#8EB4BF" /> <ItemStyle BackColor="#8EB4BF" />   <FooterTemplate>
                                                                       
                                                                            <asp:Label ID="lblfootremamt" runat="server" Font-Size="15px" ForeColor="White"></asp:Label>
                                                                       
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Notes" Visible="true" SortExpression="RemNotes" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblremnotes" runat="server" Text='<%# Bind("RemNotes")%>'  ForeColor="Black"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterStyle BackColor="#8EB4BF" /> <HeaderStyle BackColor="#8EB4BF" /> <ItemStyle BackColor="#8EB4BF" />     
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#416271" />
                                                            <PagerStyle CssClass="pgr" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                           <%-- <AlternatingRowStyle CssClass="alt" />--%>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
            </div>
        </fieldset>
    </div>
  </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
