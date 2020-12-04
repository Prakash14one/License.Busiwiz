<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="AnyTableRecordDataCopy.aspx.cs" Inherits="SyncData" Title="Table Record Display Report" %>

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
                                            <td colspan="2">
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                 <label>
                                                            <asp:Label ID="Label1" runat="server" Text="Copy From Table"></asp:Label>
                                                             <asp:DropDownList ID="DDLLiceTableName" runat="server" Width="200px"   AutoPostBack="True"  onselectedindexchanged="DDLLiceTableName_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txttblsearch" runat="server"   placeholder="Search"  Font-Bold="true"  Width="250px" Height="20px" onKeyDown="submitButton(event)"  Visible="false"></asp:TextBox>                                                           
                                                    </label> 
                                            </td>
                                             <td>
                                                 <label>
                                                            <asp:Label ID="Label7" runat="server" Text="Copy To Table"></asp:Label>
                                                             <asp:DropDownList ID="DDLCopyToTable" runat="server" Width="200px"   AutoPostBack="True"  onselectedindexchanged="DDLCopyToTable_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                           
                                                    </label> 
                                            </td>
                                        </tr>
                                        <tr>                                         
                                            <td>
                                                <label>
                                                  Field to be matched  
                                                  </label> 
                                                  <label>
                                                      <asp:DropDownList ID="DDL1" runat="server" Width="100px"   AutoPostBack="True"  onselectedindexchanged="DDL1_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                  </label> 
                                                  <label>Value (Foreinkey Id)</label> 
                                                   <label>
                                                         <asp:TextBox ID="txt1" runat="server"   Font-Bold="true"  Width="50px" onKeyDown="submitButton(event)"></asp:TextBox>                                                           
                                                  </label> 
                                            </td>
                                             <td>
                                                <label>
                                                Field to be replaced
                                                  </label> 
                                                  <label>
                                                      <asp:DropDownList ID="DDLTo1" runat="server" Width="100px"   AutoPostBack="True"  onselectedindexchanged="DDLTo1_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                  </label> 
                                                  <label>ReplaceValue (Foreinkey Id)</label> 
                                                   <label>
                                                         <asp:TextBox ID="txtto1" runat="server"   Font-Bold="true"  Width="50px" onKeyDown="submitButton(event)"></asp:TextBox>                                                           
                                                  </label> 
                                            </td>
                                        </tr>
                                          <tr>                                         
                                            <td>
                                                <label>
                                                  Field to be matched  
                                                  </label> 
                                                  <label>
                                                      <asp:DropDownList ID="DDL2" runat="server" Width="100px"   AutoPostBack="True"  onselectedindexchanged="DDL2_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                  </label> 
                                                  <label>Value (Foreinkey Id)</label> 
                                                   <label>
                                                         <asp:TextBox ID="txt2" runat="server"   Font-Bold="true"  Width="50px" onKeyDown="submitButton(event)"></asp:TextBox>                                                           
                                                  </label> 
                                            </td>
                                             <td>                                              
                                                    <label>
                                                    Field to be replaced
                                                    </label>
                                                    <label>
                                                    <asp:DropDownList ID="DDLTo2" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="DDLTo2_SelectedIndexChanged" Width="100px">
                                                    </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                    ReplaceValue (Foreinkey Id)</label>
                                                    <label>
                                                    <asp:TextBox ID="txtto2" runat="server" Font-Bold="true" 
                                                        onKeyDown="submitButton(event)" Width="50px"></asp:TextBox>
                                                    </label>                                              
                                            </td>


                                        </tr>
                                          <tr>
                                            <td>
                                                <label>
                                                  Field to be matched  
                                                  </label> 
                                                  <label>
                                                      <asp:DropDownList ID="DDL3" runat="server" Width="100px"   AutoPostBack="True"  onselectedindexchanged="DDL3_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                  </label> 
                                                  <label>Value (Foreinkey Id)</label> 
                                                   <label>
                                                         <asp:TextBox ID="txt3" runat="server"   Font-Bold="true"  Width="50px" onKeyDown="submitButton(event)"></asp:TextBox>                                                           
                                                  </label> 
                                            </td>
                                              <td>                                               
                                                    <label>
                                                    Field to be replaced
                                                    </label>
                                                    <label>
                                                    <asp:DropDownList ID="DDLTo3" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="DDLTo3_SelectedIndexChanged" Width="100px">
                                                    </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                    ReplaceValue (Foreinkey Id)</label>
                                                    <label>
                                                    <asp:TextBox ID="txtto3" runat="server" Font-Bold="true" 
                                                        onKeyDown="submitButton(event)" Width="50px"></asp:TextBox>
                                                    </label>
                                               
                                            </td>
                                        </tr>
                                          <tr>
                                            <td>
                                                <label>
                                                  Field to be matched  
                                                  </label> 
                                                  <label>
                                                      <asp:DropDownList ID="DDL4" runat="server" Width="100px"   AutoPostBack="True"  onselectedindexchanged="DDL4_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                  </label> 
                                                  <label>Value (Foreinkey Id)</label> 
                                                   <label>
                                                         <asp:TextBox ID="txt4" runat="server"   Font-Bold="true"  Width="50px" onKeyDown="submitButton(event)"></asp:TextBox>                                                           
                                                  </label> 
                                            </td>
                                              <td>                                               
                                                    <label>
                                                    Field to be replaced
                                                    </label>
                                                    <label>
                                                    <asp:DropDownList ID="DDLTo4" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="DDLTo4_SelectedIndexChanged" Width="100px">
                                                    </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                    ReplaceValue (Foreinkey Id)</label>
                                                    <label>
                                                    <asp:TextBox ID="txtto4" runat="server" Font-Bold="true" 
                                                        onKeyDown="submitButton(event)" Width="50px"></asp:TextBox>
                                                    </label>
                                                  </td>
                                        </tr>
                                      
                                       
                                           <tr>
                                          
                                           
                                        </tr>
                                          <tr>
                                          <td></td>
                                           
                                        </tr>
                                          <tr><td></td>
                                          
                                        </tr>
                                          <tr><td></td>
                                          
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    Field name note to be copied
                                                </label> 
                                                <label>
                                                  <asp:DropDownList ID="DDLNotCopied" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="DDLTo4_SelectedIndexChanged" Width="150px">
                                                    </asp:DropDownList>
                                                </label> 
                                            </td>
                                        </tr>
                                          <tr>
                                            <td colspan="2">
                                                <label>
                                                    Control field
                                                </label> 
                                                <label>
                                                  <asp:DropDownList ID="DDLcontrol" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="DDLTo4_SelectedIndexChanged" Width="150px">
                                                    </asp:DropDownList>
                                                </label> 
                                                <label>
                                                    <asp:TextBox ID="txtcontrol" runat="server" Font-Bold="true" 
                                                        Width="150px"></asp:TextBox>
                                                </label> 
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="2">
                                                  <label>

                                                        <asp:TextBox ID="TextBox1" runat="server" Font-Bold="true" Height="80"
                                                      TextMode="MultiLine"   Width="100%"></asp:TextBox>
                                                  </label>
                                                  </td>
                                                  </tr>
                                        <tr>
                                            <td colspan="2">
                                                  <label>
                                                                <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" OnClick="btnStart_Click" Text=" Check " ValidationGroup="1" />
                                                     </label>  
                                                     <label>
                                                        <asp:Button ID="btncopynow" runat="server" CssClass="btnSubmit" OnClick="btncopynow_Click" Text="Start Copy Record Now " ValidationGroup="1" />
                                                     </label> 
                                            </td>
                                        </tr>
                                           
                                           <tr>
                                            <td colspan="2">
                                                         <asp:Label ID="lbl_PKid" runat="server" Visible="false"  Text=""></asp:Label>
                                                         <asp:Label ID="lbl_totalrecord" runat="server"   Text=""></asp:Label> 
                                            </td>
                                        </tr>

                                          <asp:Panel ID="Panel2" runat="server" Visible="false"   Height="500px" ScrollBars="Both">
                                           <tr>
                                                <td colspan="2"  align="left" style="border-style: groove hidden groove hidden">  
                                                <label>
                                                            <asp:Label ID="Label4" runat="server" Text="Order By" ></asp:Label>
                                                         <asp:DropDownList ID="ddlorder" runat="server"  Width="140px">                                                               
                                                                <asp:ListItem Text="Descending" Value="DESC" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Ascending" Value="ASC"></asp:ListItem>
                                                            </asp:DropDownList>
                                                     </label>
                                                       <label>
                                                             <asp:Label ID="Label3" runat="server" Text="No of Record" ></asp:Label>
                                                              <asp:TextBox ID="txt_noofrecord"  runat="server" Text="500" Width="50px" Enabled="true"  MaxLength="3" onkeyup="return mak('Span25',20,this)"> </asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_noofrecord" ValidChars="012.3456789">
                                                                </cc1:FilteredTextBoxExtender>       
                                                    </label>  
                                                            <label><br />
                                                                <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" OnClick="btnSubmit_Click" Text=" Go " ValidationGroup="1" />
                                                     </label>                                                                                                 
                                                </td>
                                            </tr>
                                             <tr>
                                                <td colspan="2"  align="right" style="border-style: groove hidden groove hidden">  
                                                                                                                                                      
                                                </td>
                                            </tr>                                                                          
                                          <tr>
                                                <td colspan="2" >
                                                 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" >
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
                                                 <asp:TextBox ID="txt_where" runat="server"    ></asp:TextBox>
                                                 </fieldset>
                                                </td>
                                            </tr>
                                            </asp:Panel>
                                    </table>

                </fieldset>
                  <asp:Panel ID="Panel1" runat="server" Visible="false"   Height="500px" ScrollBars="Both">
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
               </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
