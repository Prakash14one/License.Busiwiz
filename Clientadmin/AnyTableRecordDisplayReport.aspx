<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="AnyTableRecordDisplayReport.aspx.cs" Inherits="SyncData" Title="Table Record Display Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Table Record Display Report"></asp:Label>
                    </legend>  
                                    <table width="100%">
                                        <tr>
                                            <td align="right">                                               
                                                
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                  <label>
                                                    <asp:Label ID="Label5" runat="server" Text="Select Product/Version"></asp:Label>
                                                    <asp:DropDownList ID="FilterProductname" Width="200px" runat="server" AutoPostBack="True" onselectedindexchanged="FilterProductname_SelectedIndexChanged">
                                                       </asp:DropDownList>
                                                     </label>                                                     
                                                    <label>
                                                            <asp:Label ID="Label2" runat="server" Text="Select Database"></asp:Label>
                                                           <asp:DropDownList ID="ddlctype" Width="200px" runat="server" AutoPostBack="True" onselectedindexchanged="ddlctype_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                     </label>
                                                     <label style="vertical-align:bottom;">
                                                     <asp:Image ID="img_dbconn" runat="server" Width="30px" Visible="false" ImageUrl="" />
                                                     </label>                                                   
                                                     <label>
                                                            <asp:Label ID="Label4" runat="server" Text="Order By"></asp:Label>
                                                         <asp:DropDownList ID="ddlorder" runat="server"  Width="140px" >                                                               
                                                                <asp:ListItem Text="Descending" Value="DESC" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Ascending" Value="ASC"></asp:ListItem>
                                                            </asp:DropDownList>
                                                     </label>
                                                       <label>
                                                             <asp:Label ID="Label3" runat="server" Text="No of Record"></asp:Label>
                                                              <asp:TextBox ID="txt_noofrecord" runat="server" Text="500" Width="50px" Enabled="true"  MaxLength="3" onkeyup="return mak('Span25',20,this)"> </asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_noofrecord" ValidChars="012.3456789">
                                                                </cc1:FilteredTextBoxExtender>       
                                                    </label>    
                                                     <label>
                                                            <asp:Label ID="Label1" runat="server" Text="Table Name"></asp:Label>
                                                            <asp:TextBox ID="txttblsearch" runat="server"   placeholder="Search"  Font-Bold="true"  Width="250px" Height="20px" onKeyDown="submitButton(event)"  ></asp:TextBox>
                                                            <asp:DropDownList ID="DDLLiceTableName" runat="server" Width="200px" Visible="false"  AutoPostBack="True"  onselectedindexchanged="DDLLiceTableName_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                    </label> 
                                                     <label><br />
                                                                <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" OnClick="btnSubmit_Click" Text=" Go " ValidationGroup="1" />
                                                            </label>                                             
                                            </td>
                                        </tr>    
                                        <tr>
                                            <td>                                               
                                                <label>Where <asp:TextBox ID="txt_where" runat="server"    ></asp:TextBox></label> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                         <asp:Label ID="lbl_PKid" runat="server" Visible="false"  Text=""></asp:Label>
                                                         <asp:Label ID="lbl_totalrecord" runat="server"   Text=""></asp:Label>        
                         
                                            </td>
                                        </tr>
                                            <tr>
                                                <td align="right" style="border-style: groove hidden groove hidden">  
                                                                                                                                                       
                                                </td>
                                            </tr>
                                             <tr>
                                                <td align="left">  
                                                                                                                                    
                                                </td>
                                            </tr> 
                                             <tr>
                                                <td align="right" style="border-style: groove hidden groove hidden">  
                                                                                                                                                      
                                                </td>
                                            </tr>                                                                          
                                          <tr>
                                                <td>
                                                 <fieldset>
                                                        <legend>
                                                            <asp:Label ID="Label10" runat="server" Text="Table Design"></asp:Label>
                                                        </legend>
                                                     <asp:GridView ID="GridView2" HeaderStyle-BackColor="#3AC0F2" 
                                                         HeaderStyle-ForeColor="White" OnRowDataBound="OnRowDataBound" runat="server" 
                                                         AutoGenerateColumns="False">
                                                      <Columns>
                                                          <asp:BoundField DataField="column_name" HeaderText="Column Name" SortExpression="column_name" />
                                                          <asp:BoundField DataField="data_type" HeaderText="Data Type" SortExpression="data_type" />
                                                          <asp:BoundField DataField="CHARACTER_MAXIMUM_LENGTH" HeaderText="Size" SortExpression="CHARACTER_MAXIMUM_LENGTH" />
                                                      </Columns>
                                                      <HeaderStyle BackColor="#3AC0F2" ForeColor="White" />
                                                    </asp:GridView>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                 <fieldset>
                                                        <legend>
                                                            <asp:Label ID="Label6" runat="server" Text="Table record"></asp:Label>
                                                        </legend>
                                                 <asp:Panel ID="pnlsync" runat="server"  Height="500px" ScrollBars="Both">
                                                   <asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" OnRowDataBound="OnRowDataBound" runat="server" AutoGenerateColumns="false">
                                                    </asp:GridView>
                                                 
                                                       <input id="hdnsortExp1" type="hidden" name="hdnsortExp1" style="width: 3px" runat="Server" />
                                                        <input id="hdnsortDir1" type="hidden" name="hdnsortDir1" style="width: 3px" runat="Server" />
                                                        <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                                        <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                                        <input id="hdnPricePlanId" name="hdnPricePlanId" runat="server" type="hidden" style="width: 1px" />
                                                 </asp:Panel>
                                                 
                                                 </fieldset>
                                                </td>
                                            </tr>
                                            
                                    </table>

                </fieldset>
               
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
