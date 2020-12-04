<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="EmployeeAttendanceMoreInfo.aspx.cs" Inherits="ShoppingCart_Admin_EmployeeAttendanceMoreInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
    <script type="text/javascript" language="javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }  
    </script>
   
    <div class="products_box">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label13" runat="server" Text="Employee Daily Presence Report"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnprintableversion" runat="server" Text="Printable Version" 
                            onclick="btnprintableversion_Click" CssClass="btnSubmit" />
                      <input ID="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';" 
                       class="btnSubmit" type="button" value=" Print " visible="false"  />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="lblerror" runat="server" Text="" Visible="false"></asp:Label>
                    </label>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                    <table >
                    <tr align="center">
                                    <td>
                                        <div id="mydiv" class="closed">
                                           <table width="100%" style="color:Black; font-style:italic; text-align:center">
                                            <tr>
                                                <td align="center">                                                  
                                                    <asp:Label ID="lblcompanyname" Font-Bold="true" runat="server" Text="Employee Daily Presence Report" Font-Size="18px"></asp:Label>
                                                </td>
                                            </tr>
                                            </table>
                                         </div>
                                    </td>
                                </tr>
                    <tr>
                        <td>
                            <label class="cssLabelCompany_Information">
                                <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                            </label>
                        </td>
                        <td>
                            <label class="cssLabelCompany_Information_Ans">
                                <asp:Label CssClass="lblSuggestion" ID="lblbusinessName" runat="server" ></asp:Label>
                            </label>
                        </td>
                    </tr>                                        
                    
                    <div class="cleaner">
                    </div>
                    <tr>
                        <td>
                        <label  class="cssLabelCompany_Information">
                        <asp:Label ID="Label2" runat="server" Text="Batch Name"></asp:Label>
                    </label>
                        </td>
                        <td>
                        <label class="cssLabelCompany_Information_Ans">
                        <asp:Label CssClass="lblSuggestion" ID="lblbatchname" runat="server" ></asp:Label>
                    </label>
                        </td>
                    </tr>
                    
                    
                    <div class="cleaner">
                </div>
                <tr>
                        <td>
                        <label class="cssLabelCompany_Information">
                        <asp:Label ID="Label3" runat="server" Text="Employee Name"></asp:Label>
                        </label>
                        </td>
                        <td>
                        <label class="cssLabelCompany_Information_Ans">
                        <asp:Label CssClass="lblSuggestion" ID="lblemployeename" runat="server" ></asp:Label>
                    </label>
                        </td>
                    </tr>
                    
                    
                     <div class="cleaner">
                </div>
                <tr>
                        <td>
                        <label class="cssLabelCompany_Information">
                        <asp:Label ID="Label4" runat="server" Text="Regular Start Time"></asp:Label>
                        </label>
                        </td>
                        <td>
                        <label class="cssLabelCompany_Information_Ans">
                        <asp:Label CssClass="lblSuggestion" ID="lblregularstarttime" runat="server" ></asp:Label>
                    </label>
                        </td>
                    </tr>
                    
                    
                    <div class="cleaner">
                </div>
                <tr>
                        <td>
                            <label class="cssLabelCompany_Information">
                        <asp:Label ID="Label5" runat="server" Text="Actual In Time"></asp:Label>
                         </label>
                        </td>
                        <td>
                        <label class="cssLabelCompany_Information_Ans">
                        <asp:Label CssClass="lblSuggestion" ID="lblactualintime" runat="server" ></asp:Label>
                    </label>
                        </td>
                    </tr>
                    
                    
                    <div class="cleaner">
                </div>
                <tr>
                        <td>
                            <label class="cssLabelCompany_Information">
                        <asp:Label ID="Label6" runat="server" Text="Let/Early In Time"></asp:Label>
                        </label>
                        </td>
                        <td>
                            <label class="cssLabelCompany_Information_Ans">
                        <asp:Label CssClass="lblSuggestion" ID="lbllateearlyintime" runat="server" ></asp:Label>
                    </label>
                        </td>
                    </tr>
                    
                    
                    <div class="cleaner">
                </div>
                <tr>
                        <td>
                            <label class="cssLabelCompany_Information">
                        <asp:Label ID="Label7" runat="server" Text="In Time Note"></asp:Label>
                        </label>
                        </td>
                        <td>
                            <label class="cssLabelCompany_Information_Ans">
                        <asp:Label  CssClass="lblSuggestion" ID="lblintimenote" runat="server" ></asp:Label>
                    </label>
                        </td>
                    </tr>
                    
                    
                    <div class="cleaner">
                </div>
                <tr>
                        <td>
                            <label class="cssLabelCompany_Information">
                        <asp:Label ID="Label8" runat="server" Text="Regular Out Time"></asp:Label>
                        </label>
                        </td>
                        <td>
                            <label class="cssLabelCompany_Information_Ans">
                        <asp:Label CssClass="lblSuggestion" ID="lblregularouttime" runat="server" ></asp:Label>
                    </label>
                        </td>   
                    </tr>
                    
                    
                     <div class="cleaner">
                </div>
                <tr>
                        <td>    
                            <label class="cssLabelCompany_Information">
                        <asp:Label ID="Label9" runat="server" Text="Actual Out Time"></asp:Label>
                         </label>
                        </td>
                        <td>
                            <label class="cssLabelCompany_Information_Ans">
                        <asp:Label CssClass="lblSuggestion" ID="lblactualouttime" runat="server" ></asp:Label>                        
                    </label>
                        </td>
                    </tr>
                    
                    
                     <div class="cleaner">
                </div>
                <tr>
                        <td>
                             <label class="cssLabelCompany_Information">
                        <asp:Label ID="Label10" runat="server" Text="Let/Early Out Time"></asp:Label>
                         </label>
                        </td>
                        <td>
                            <label class="cssLabelCompany_Information_Ans">
                        <asp:Label CssClass="lblSuggestion" ID="lblletearlyouttime" runat="server" ></asp:Label>
                    </label>
                        </td>
                    </tr>
                   
                    
                    <div class="cleaner">
                </div>
                <tr>
                        <td>
                             <label class="cssLabelCompany_Information">
                        <asp:Label ID="Label11" runat="server" Text="Out Time Note"></asp:Label>
                        </label>
                        </td>
                        <td>    
                             <label class="cssLabelCompany_Information_Ans">
                        <asp:Label CssClass="lblSuggestion" ID="lblouttimenote" runat="server" ></asp:Label>
                    </label>
                        </td>
                    </tr>
                   
                   
                    <div class="cleaner">
                </div>
                <tr>
                        <td>
                            <label >
                        <asp:Label ID="Label12" runat="server" Text="Actual Total Worked Hours"></asp:Label>
                         </label>
                        </td>
                        <td>
                            <label class="cssLabelCompany_Information_Ans">
                        <asp:Label CssClass="lblSuggestion" ID="lbltotalworkedhour" runat="server" ></asp:Label>
                    </label>
                        </td>
                    </tr>
                    
                    
                    </table>
                    </asp:Panel>
                </fieldset>
            </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
</asp:Content>

