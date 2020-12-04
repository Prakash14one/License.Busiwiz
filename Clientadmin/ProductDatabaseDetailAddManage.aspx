<%@ Page Title="" Language="C#"  MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="ProductDatabaseDetailAddManage.aspx.cs" Inherits="productcode_databaseaddmanage" %>
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
    
        TR.updated TD
        {
            background-color:yellow;
        }
        .modalBackground 
        {
            background-color:Gray;
            filter:alpha(opacity=70);
            opacity:0.7;
        }
    .pnlBackGround
{
 position:fixed;
    top:10%;
    left:10px;
    width:300px;
    height:125px;
    text-align:center;
    background-color:White;
    border:solid 3px black;
}
    </style>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <div style="clear: both;">
                </div> 
     <fieldset>
     <div style="float: left;">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <asp:Label ID="lbl_serverattach" runat="server" ForeColor="White"></asp:Label>
    </div>
                         <legend>
                        <asp:Label ID="Label19" runat="server" Text="Product Database Add Manage"></asp:Label>
                 </legend>         
                 <div style="float: right;">                    
                    <asp:Button ID="btn_addnew" runat="server" CssClass="btnSubmit" onclick="btnAddNewDAta_Click"  Text="Add New Database" />                    
                </div>
     
                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                        <table style="width: 100%">
                            <tr>
                                <td width="20%">
                                  <label>
                                     <asp:Label ID="lbl_serverid" runat="server" Text="" Visible="false"></asp:Label>
                                     <asp:Label ID="Label1" runat="server" Text="Select Products"></asp:Label>
                                    <asp:Label ID="Label51" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlproductversion" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFsdfsdfieldValidator34" runat="server" ControlToValidate="ddlproductversion" ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td width="50%">
                                <label>
                                    <asp:DropDownList ID="ddlproductversion" runat="server" AutoPostBack="True" onselectedindexchanged="ddlproductversion_SelectedIndexChanged" Width="200px" >
                                    </asp:DropDownList>
                                    </label> 
                                    <label style="width:150px;"></label> 
                                    <label>
                                        <asp:Button ID="btn_showftp" Height="20px"   runat="server" Text="Show Ftp Details" OnClick="btn_test_Click" CssClass="btnSubmit" />
                                             
                                                    <cc1:ModalPopupExtender   BackgroundCssClass="modalBackground"  ID="modal1" CancelControlID="btnCancelPopup" runat="server" TargetControlID="btn_showftp" PopupControlID="pnlPopup"></cc1:ModalPopupExtender>
                                                    <asp:Panel ID="pnlPopup" Height="200px" Width="400px" runat="server" CssClass="pnlBackGround">                                                       
                                                                  <legend>
                                                                    <asp:Label ID="Label10" runat="server" Text="FTP Detail"></asp:Label>
                                                                </legend>     
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                             <asp:Label ID="lblMsgPopup" runat="server" Text="FTP server name"></asp:Label>
                                                                             </label> 
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="ftpservename" runat="server" Text=""></asp:Label>
                                                                            </label> 
                                                                        </td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td>
                                                                            <label>
                                                                             <asp:Label ID="Label3" runat="server" Text="FTP user name"></asp:Label>
                                                                             </label> 
                                                                        </td>
                                                                        <td>
                                                                        <label>
                                                                            <asp:Label ID="ftpuser" runat="server" Text=""></asp:Label>
                                                                            </label> 
                                                                        </td>
                                                                    </tr>
                                                                </table>                   
                                                        <br /><br />
                                                        <center><asp:Button ID="btnCancelPopup" runat="server" Text="Close" /></center>
                                             </asp:Panel>
                                             
                                    </label> 
                                </td>                               
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <label>
                                         <asp:Label ID="Label11" runat="server" Text="Products Description"></asp:Label>
                                     </label>
                                </td>
                                <td colspan="2">
                                    <label>                                                                  
                                        <asp:TextBox ID="txt_prod_desc" TextMode="MultiLine" Width="600px" Height="45px" runat="server" Enabled="False" BackColor="White" BorderColor="White" BorderStyle="None" ForeColor="Black"></asp:TextBox>
                                         <asp:DropDownList ID="ddlcodetypecategory" runat="server" onselectedindexchanged="ddlcodetypecategory_SelectedIndexChanged"  AutoPostBack="True" Width="200px" Visible="false">
                                         </asp:DropDownList>
                                         
                                                
                                    </label> 
                                </td>
                            </tr>
                           
                           
                            <tr>
                                <td colspan="3">
                                     <table width="100%">
                                                <tr>
                                                    <td colspan="2" width="100%">
                                                     <asp:Panel ID="Pnl_addcode" runat="server" Visible="true" width="100%">
                                                     
                                                       
                                        <fieldset>
                                        <legend>Add product database                                             
                                        </legend>                                             
                                                 <table width="100%">
                                                        <tr>
                                                        <td style="width:35%">
                                                            <asp:Panel ID="pnloption1" runat="server" Visible="true">   
                             <tr>
                                <td style="width:35%;vertical-align:top;">
                                    <label style="vertical-align:top;">
                                        <asp:Label ID="Label17111a" runat="server" Text="Select website using this database"></asp:Label>
                                         <asp:Label ID="Label1811a" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    </label>
                                </td>
                                <td style="width:65%">
                                    <table>
                                        <tr>
                                            <td>
                                                 <label>
                                                  <asp:Label ID="Label13" runat="server" Text="Filter by product" Visible="false"></asp:Label>
                                                 </label> 
                                                 <label>
                                                    <asp:DropDownList ID="DDLProdFolterWeb" runat="server" Width="250px" AutoPostBack="True" onselectedindexchanged="DDLProdFolterWeb_SelectedIndexChanged" Visible="false" >
                                                     </asp:DropDownList>
                                                </label> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                   <asp:Panel ID="pnl_websiter1" runat="server" Visible="true">                          
                                                    <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                                        <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                                         <input runat="server" id="hdnProductDetailId" type="hidden" name="hdnProductDetailId" style="width: 3px" />
                                                               <asp:Panel ID="pnlpr" runat="server" Width="400px"  ScrollBars="Horizontal" Height="200px">
                                                                    <asp:GridView ID="GridView2" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                                                                        OnRowCommand="GridView2_RowCommand" EmptyDataText="There is no data." AllowSorting="True"
                                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                 OnRowDataBound="GridView2_RowDataBound"      Width="100%" OnPageIndexChanging="GridView2_PageIndexChanging" OnSorting="GridView2_Sorting"
                                                                         OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                                                                        <Columns>
                                                                            <asp:TemplateField ControlStyle-Width="5%" FooterStyle-Width="5%" HeaderStyle-Width="5%"  >
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="cbHeader" runat="server" OnCheckedChanged="ch1_chachedChanged" AutoPostBack="true" />
                                                                                    <asp:Label ID="check" runat="server" ForeColor="White" Text="ALL" HeaderStyle-Width="30px" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="cbItem" Checked='<%# Bind("chk") %>'  runat="server" />
                                                                                    <asp:CheckBox ID="chkdef" Checked='<%# Bind("chk") %>' runat="server" Visible="false" />
                                                                
                                                                                    <%--Checked='<%# Bind("chk") %>'--%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Website Name" SortExpression="ID" HeaderStyle-HorizontalAlign="Left"
                                                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="67%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblp" runat="server" Text='<%# Bind("WebsiteName") %>'></asp:Label>
                                                                                    <asp:Label ID="lblwebid" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                       
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </asp:Panel>
                                                 </label> 
                                            </td>
                                            </tr>
                                    </table> 
                                </td>
                            </tr>
                            </asp:Panel>
                                                        </tr>

                                                        





                                                          <tr>
                                                        <td colspan="2">
                                                             <label>
                                                               
                                                            <asp:CheckBox ID="Chk_CodeOp1" runat="server" 
                                                                 Text="This Database is already attached to sql server" 
                                                                 oncheckedchanged="Chk_CodeOp1_CheckedChanged"   AutoPostBack="True" 
                                                                 />
                                                            </label> 
                                                            <label>
                                                               
                                                             <asp:CheckBox ID="Chk_CodeOp2" runat="server" Text="Attach this database to sql server" oncheckedchanged="Chk_CodeOp2_CheckedChanged"   AutoPostBack="True"/>
                                                            </label> 
                                                            <label>
                                                                
                                                            <asp:CheckBox ID="Chk_CodeOp3" runat="server" Text="This database is offline" oncheckedchanged="Chk_CodeOp3_CheckedChanged"  AutoPostBack="True" ToolTip="Select this option if the product is finalised  and there is no development work going on and no update of the code/new version of code will be required to be created in the near future." />
                                                            </label> 
                                                            
                                                          
                                                        </td>                                                        
                                                    </tr>
                                                        <tr>
                                                            <td width="35%">
                                                            <label>
                                                                <asp:Label ID="Label4" runat="server" Text="Code Name (ie. Enter database name)"></asp:Label>
                                                                <asp:Label ID="Label5" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcodetypename" ValidationGroup="1"  ErrorMessage="*" ></asp:RequiredFieldValidator>
                                                                </label> 
                                                            </td>
                                                            <td width="65%">   
                                                                <label>  
                                                                 <asp:TextBox ID="txtcodetypename" runat="server" AutoPostBack="True" OnTextChanged="ddlinstance_SelectedIndexChanged" ></asp:TextBox>
                                                                 </label>
                                                                 <label>
                                                                        <asp:Label ID="lbl_dbatttacherror" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                                                 </label> 
                                                            </td> 
                                                        </tr>   
                                                        <tr>
                                                            <td>
                                                            <label>
                                                                <asp:Label ID="Label14" runat="server" Text="Status"></asp:Label>
                                                                
                                                                </label> 
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="Chk_addactive" runat="server" Checked="true" Text="Active"  />                                                            
                                                            </td> 
                                                        </tr>                                                    
                                                        <tr>
                                                            <td>
                                                                  <label>
                                                                    Database Type
                                                                  </label>   
                                                            </td>
                                                            <td>
                                                                <label>
                                                                <asp:CheckBox ID="CheckBox11" runat="server" Text="Is it Busicontroller Database?" AutoPostBack="True" oncheckedchanged="CheckBox11_CheckedChanged"  />                  
                                                                </label>
                                                                <label>                                                                
                                                                  <asp:CheckBox ID="CheckBox12" runat="server" Text="Is it Company Default Database?" AutoPostBack="True" oncheckedchanged="CheckBox12_CheckedChanged"  />           
                                                                  </label> 
                                           
                                                            </td>
                                                        </tr>
                                                      
                                                     </table> 
                                                     <asp:Panel ID="pnl_selectradio" runat="server" Visible="false">      
                                                     <table width="100%">
                                                        <tr>
                                                             <td colspan="2">
                                                             <label>
                                                                  <asp:CheckBox ID="chk_attechdb" runat="server" AutoPostBack="True" Checked="false" oncheckedchanged="chk_attechdb_CheckedChanged" Text="Would you like to test whether the database is in working order  by attaching the datase to sql server(Recommended) ? " />
                                                                  <div>
                                                                     <asp:Label ID="lblradio3" runat="server"  Text="" Font-Size="12px"></asp:Label>                                                                                                 
                                                                  </div>
                                                                  </label> 
                                                             </td>
                                                         </tr>                                                       
                                                        <asp:Panel ID="pnl_attechdb" runat="server" Visible="false">                                                                       
                                                             <tr>
                                                                                 <td>
                                                                                     <label>
                                                                                     Satellite server
                                                                                     </label>
                                                                                 </td>
                                                                                 <td>
                                                                                 <label>
                                                                                      <asp:Label ID="lbl_settelliteserver" runat="server" Text="Code Name"></asp:Label>
                                                                                     </label> 
                                                                                      <label></label> 
                                                                                     <label></label> 
                                                                                 </td>
                                                                             </tr>
                                                             <tr>
                                                                                 <td>
                                                                                     <label>
                                                                                    SQL server name
                                                                                     </label>
                                                                                 </td>
                                                                                 <td>
                                                                                 <label>
                                                                                      <asp:Label ID="lbl_severname" runat="server" Text="Code Name"></asp:Label>
                                                                                     </label> 
                                                                                      <label></label> 
                                                                                     <label></label> 
                                                                                 </td>
                                                                             </tr>
                                                             <tr>
                                                                                 <td>
                                                                                     <label>
                                                                                     Select Instance<br />
                                                                                     </label>                                                                                    
                                                                                 </td>
                                                                                 <td>
                                                                                    <label>
                                                                                         <asp:DropDownList ID="ddlinstance" runat="server" AutoPostBack="True" onselectedindexchanged="ddlinstance_SelectedIndexChanged" Width="200px">
                                                                                         </asp:DropDownList>
                                                                                     </label>                                                                                    
                                                                                     <label>
                                                                                        <asp:Button ID="btn_serverconncheck"  runat="server" Text="Test Connection" Height="20px" OnClick="btn_serverconncheck_Click" CssClass="btnSubmit" />
                                                                                     </label> 
                                                                                       <label>                                                                                       
                                                                                         <asp:Label ID="lbl_serverconnectioncheck" runat="server" Text=" "></asp:Label>                                                                                                                                                                                   
                                                                                     </label> 
                                                                                 </td>
                                                                             </tr>
                                                             <tr>
                                                                                <td colspan="2">
                                                                                      <label>
                                                                                        <asp:Button ID="btn_view_detail" Height="20px"  runat="server" Text="View file details" OnClick="btn_view_detail_Click" CssClass="btnSubmit" />
                                                                                     </label> 
                                                                                     <label>
                                                                                            <asp:Label ID="lbl_checkAlredyAttechDB" runat="server"  Text="Connection to server is possible but there is no database attached with code name  please attach the code name " Font-Size="10px" ForeColor="#FF6600"></asp:Label>                                                                                                 
                                                                                     </label> 
                                                                                </td>
                                                                             </tr>
                                                         </asp:Panel> 
                                                        <tr>
                                                             <td style="width:35%" valign="top">
                                                                 <label>
                                                                 <asp:Label ID="lbltemppathcodeC" runat="server" Text="Temp path for code compilation"></asp:Label>
                                                                 </label>
                                                             </td>
                                                             <td valign="top">
                                                                 <label>
                                                                 <asp:TextBox ID="txttemppathC" runat="server" Enabled="true" Width="500px"></asp:TextBox>
                                                                  <label style="font-size:10px;width:100%">                                                                 
                                                                 Server default path for Master code\Companyid\Product name\Test path\Code name
                                                                 </label>
                                                                 </label>
                                                                 <label>
                                                                          <asp:Button ID="Button2" Height="20px"   runat="server" Text="Test Connection" OnClick="btn_test_Click" CssClass="btnSubmit" />
                                                                   </label> 
                                                                 <label>
                                                                 <asp:Label ID="lbltemppathtest" runat="server" ForeColor="Green" Text=""></asp:Label>
                                                                 </label>
                                                                
                                                             </td>
                                                         </tr>
                                                        <tr>
                                                             <td valign="top">
                                                                 <label>
                                                                 <asp:Label ID="lbloutputpathcodeC" runat="server" Text="Output path folder name"></asp:Label>
                                                                 </label>
                                                             </td>
                                                             <td valign="top">
                                                                 <label>
                                                                 <asp:TextBox ID="txtoutputsourcepathC" runat="server" Enabled="true" Width="500px"></asp:TextBox>
                                                                 <asp:TextBox ID="txtmastercodepathC" runat="server" Enabled="False" Visible="false" Width="500px"></asp:TextBox>
                                                                    <label style="font-size:10px;width:100%">                                                                                 
                                                                    Server default path for Master code\Companyid\Product name\Master code\Code name
                                                                 </label>
                                                                 </label>
                                                                 <label>
                                                                      <asp:Button ID="Button6" Height="20px"   runat="server" Text="Test Connection" OnClick="btn_test_Click" CssClass="btnSubmit" />
                                                                  </label> 
                                                                 <label>
                                                                 <asp:Label ID="lbloupputpathtest" runat="server" ForeColor="Green" Text=""></asp:Label>
                                                                 </label>
                                                                 
                                                             </td>
                                                         </tr>  
                                                         <tr>
                                                          <td>
                                                         <asp:Panel ID="pnl_uploadMdfLdf" runat="server" Visible="false">
                                                             <tr>
                                                        <td>
                                                           
                                                            <label>
                                                               Upload MDF file for this database
                                                            </label> 
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:FileUpload ID="fu_mdffile" runat="server" />
                                                            </label> 
                                                            <label>                                                            
                                                                <asp:Button ID="btn_uploadmdf" runat="server" OnClick="btn_uploadmdf_Click" Text="Upload" ValidationGroup="1" CssClass="btnSubmit" TabIndex="8" />
                                                            </label> 
                                                            <label>
                                                                    <asp:Label ID="lbl_uploadmdfsucc" runat="server" ForeColor="Green" Font-Size="12px"></asp:Label>
                                                            </label>   
                                                            <label>
                                                             <asp:Label ID="lbl_fumdfname" runat="server" Font-Size="12px"></asp:Label>
                                                            </label> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                              Upload LDF file for this database
                                                            </label> 
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:FileUpload ID="fu_ldffile" runat="server" />
                                                            </label> 
                                                            <label>                                                            
                                                                <asp:Button ID="btn_uploadldf" runat="server" OnClick="btn_uploadldf_Click" Text="Upload" ValidationGroup="1" CssClass="btnSubmit" TabIndex="8" />
                                                            </label> 
                                                            <label>
                                                                    <asp:Label ID="lbl_uploadldfsucc" runat="server" ForeColor="Green" Font-Size="12px"></asp:Label>
                                                            </label>  
                                                              <label>
                                                             <asp:Label ID="lbl_fuldffilename" runat="server"  Font-Size="12px"></asp:Label>
                                                            </label>                                                            
                                                        </td>
                                                    </tr>
                                                       
                                                          
                                                       </asp:Panel>
                                                         </td>
                                                      </tr>                                                           
                                                        <asp:Panel ID="pnl_uploaddatabasefile" runat="server" Visible="false">                                                                                  
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                          MDF file name
                                                                        </label> 
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_mdffilename" runat="server" Width="500px" Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                </tr>                                               
                                                                    <tr>
                                                                    <td>
                                                                        <label style="width:100%" >
                                                                          Path where the MDF file are 
                                                                        </label>
                                                                        <div style="font-size:10px;">
                                                                            (The current Path of MDF file)
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                         <asp:TextBox ID="txt_mdffilepath" runat="server" Width="500px" Enabled="False"></asp:TextBox>
                                                                          <div style="width:100%;font-size:10px;">
                                                                            souce default path mdf file\companyid\productname\
                                                                        </div>
                                                                         </label> 
                                                                           <label>
                                                                                  <asp:Button ID="Button5" Height="20px"   runat="server" Text="Test Connection" OnClick="btn_test_Click" CssClass="btnSubmit" />
                                                                            </label> 
                                                                         <label>
                                                                            <asp:Label ID="lbl_mdffilenamepath" runat="server" ForeColor="Green" Text=""></asp:Label>
                                                                        </label>
                                                                       
                                                                    </td>
                                                                </tr>   
                                                                    <tr>
                                                                    <td>
                                                                        <label>
                                                                          LDF file name
                                                                        </label> 
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_ldffilename" runat="server" Width="500px" Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                </tr>                                                      
                                                                <tr>
                                                                    <td>
                                                                        <label style="width:100%">
                                                                            Path where the LDF file are
                                                                        </label> 
                                                                        <div style="font-size:10px;">
                                                                            (The current Path of LDF file)
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                        <asp:TextBox ID="txt_ldffilepath" runat="server" Width="500px" Enabled="False"></asp:TextBox>
                                                                         <div style="width:100%;font-size:10px;">
                                                                            souce default path ldf file\companyid\productname\
                                                                        </div>
                                                                        </label> 
                                                                        <label>
                                                                                  <asp:Button ID="Button1" Height="20px"   runat="server" Text="Test Connection" OnClick="btn_test_Click" CssClass="btnSubmit" />
                                                                            </label> 
                                                                         <label>
                                                                            <asp:Label ID="lbl_ldffilepath" runat="server" ForeColor="Green" Text=""></asp:Label>
                                                                        </label>
                                                                       
                                                                    </td>
                                                                </tr>
                                                    </asp:Panel>                                              
                                                                                                          
                                                        <tr>
                                                             <td colspan="2">
                                                                 <label style="font-size:12px;">
                                                                Would you like to get the details of tables and their fields and store procedure of the database and syncronise with the entries in your tables master and table field master tables ?(Recommended)
                                                                 </label>
                                                                 <asp:CheckBox ID="chk_syncronice" runat="server"  AutoPostBack="True" oncheckedchanged="chk_uploadcode_CheckedChanged" Text="Yes" />
                                                             </td>
                                                         </tr>
                                                                                                    
                                                        </table> 
                                                     </asp:Panel>                                        
                                             </fieldset> 
                                             </asp:Panel>
                                                    </td>
                                                </tr>
                                               
                                            </table> 
                                </td>
                            </tr>                           
                            <tr>
                                <td width="22%">
                                    &nbsp;</td>
                                <td width="25%">
                                    <asp:Button ID="btn_submitCode" runat="server"  CssClass="btnSubmit" onclick="btn_submitCode_Click" Text="Submit" ValidationGroup="1" />
                                    <asp:Button ID="btn_update" runat="server"  CssClass="btnSubmit" onclick="btn_update_Click" Text="Update" ValidationGroup="1" Visible="False" />
                                    <asp:Button ID="BtnCancel" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="BtnCancel_Click" />
                                </td>                               
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                                                     
                                </td>
                            </tr>
                              
                        </table>

                    </asp:Panel>
              </fieldset>
              <fieldset>
            





                    <asp:Panel ID="Panel4" runat="server" GroupingText="List Of Product Database">
                        <table style="width: 100%">   
                        <tr>
                                <td width="25%">
                                     <label>
                                        <asp:CheckBox ID="chk_Active" runat="server" Checked="true" Text="Show Active Filters Only" AutoPostBack="True" oncheckedchanged="chk_Active_CheckedChanged"/>
                                    </label> 
                                    <label>
                                        <asp:CheckBox ID="chkconnection" runat="server" Checked="false" Text="Check Connection"  />
                                    </label> 
                                    </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>                        
                            <tr>
                                <td colspan="4">
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="Product Name"></asp:Label>
                                    </label> 
                              <label>
                                    <asp:DropDownList ID="ddlProductname" runat="server" AutoPostBack="True" 
                                        DataTextField="ProductName" DataValueField="ProductId" 
                                        OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged">
                                    </asp:DropDownList>
                               </label> 
                                  <label>
                                     <asp:Label ID="Label52" runat="server" Text="Category Type "></asp:Label>
                                     </label>
                                     <label>                               
                                    <asp:DropDownList ID="ddlcodetypecategory0" runat="server"  onselectedindexchanged="ddlcodetypecategory0_SelectedIndexChanged"  AutoPostBack="True">
                                    </asp:DropDownList>
                                    </label> 
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="Status"></asp:Label>
                                    </label>  
                                    <label>
                                           <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" Width="181px" OnSelectedIndexChanged="ddlcodetypecategory0_SelectedIndexChanged">
                                            <asp:ListItem Text="All" ></asp:ListItem>
                                            <asp:ListItem Text="Active" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Inactive"></asp:ListItem>
                                         </asp:DropDownList>
                                    </label>     
                                    <label>
                                                                            <asp:Button ID="Button7" runat="server" CssClass="btnSubmit" Text="Go" OnClick="BtnCancelgo_Click" />
                                
                                    </label>                              
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="GridView1" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" EmptyDataText="No Record Found." PagerStyle-CssClass="pgr" Width="100%" AutoGenerateColumns="False"  DataKeyNames="Id" 
                                    AllowPaging="True" PageSize="10" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" 
                                      OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting"
                                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" >
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Product Name" ItemStyle-Width="20%" SortExpression="VersionInfoName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpversion" runat="server" Text='<%#Bind("ProductName") %>'></asp:Label>
                                                      <asp:Label ID="Label7" Visible="false"  runat="server" Text='<%#Bind("vv") %>'></asp:Label>
                                                      <asp:Label ID="lbl_versionId" Visible="false"  runat="server" Text='<%#Bind("VersionInfoId") %>'></asp:Label>
                                                      <asp:Label ID="lbl_ServerId" Visible="false"  runat="server" Text='<%#Bind("ServerId") %>'></asp:Label>
                                                    <asp:Label ID="lblcodetypecategory" runat="server" Text='<%#Bind("CodeTypeCategory") %>' Visible="false"></asp:Label>  
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                                                                   
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code/Database Name" ItemStyle-Width="12%" SortExpression="CodeTypeName" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldefaultcodename" runat="server" Text='<%#Bind("CodeTypeName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="File path" ItemStyle-Width="20%" SortExpression="CodeTypeName"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmdffile" runat="server"  Text='<%#Bind("CodeTypeName") %>' Width="300px" Font-Size="X-Small"></asp:Label><br />
                                                    <asp:Label ID="lblldffile" runat="server"  Text='<%#Bind("CodeTypeName") %>' Width="300px" Font-Size="X-Small"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="40%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Busiwiz Database?" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label54" runat="server" Text='<%#Bind("BusiwizSynchronization") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Default Application Database?" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label55" runat="server" Text='<%#Bind("CompanyDefaultData") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Active" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActive" runat="server" Text='<%#Bind("Active") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Attach" HeaderStyle-Width="2%" Visible="false">
                                                <ItemTemplate>
                                                        <asp:ImageButton ID="imgattach" runat="server" ImageUrl="Attach" ToolTip="Attach" onclick="imgattach_Click" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                            </asp:TemplateField>   
                                              <asp:TemplateField HeaderText="Database Connection Status" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgdbconn" Visible="false"  runat="server" Height="20px"  ImageUrl="" onclick="ImgBtn_EditGrig"/>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                            </asp:TemplateField>                                         
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Account/images/edit.gif" onclick="ImgBtn_EditGrig"/>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                         <asp:Label ID="Label19" runat="server"  Text='<%#Eval("ID") %>' Visible="False"></asp:Label>
                                                            <asp:ImageButton ID="imgdelete" runat="server" ImageUrl="~/Account/images/delete.gif" ToolTip="Delete" onclick="imgdelete_Click" OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
               
                     <asp:Panel ID="Paneldoc" runat="server" Width="55%" CssClass="modalPopup">
                    <fieldset>
                     <div>
                         <asp:Label ID="Label6" runat="server" ForeColor="Black"></asp:Label>
                     </div>
                     <div>
                     </div>
                     <div align="center">
                         <asp:Button ID="Button3" runat="server" Text="Yes" onclick="Button3_Click" />
                         <asp:Button ID="Button4" runat="server" Text="Cancel" onclick="Button4_Click" />
                     </div>
                     </fieldset>
                     </asp:Panel>
                       <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Paneldoc" TargetControlID="btnreh" >
                                </cc1:ModalPopupExtender> 
                                </fieldset> 



                       
                           <%--Uploaded TAble Detail Add Manage--%>

            <asp:Panel ID="pnlbackupstatus" runat="server"  Visible="false" width="750px">
             <div style="position: absolute; margin:-750px 0px 0px 280px; width:750px; background-color: #CECECE;" class="Box" >
                 <fieldset>                 
                  <legend>
                    <asp:Label ID="Label9" runat="server" Text="Synchronise table and field with database"></asp:Label>   
                      <asp:Label ID="lbl_databaseid" Visible="false" runat="server" Text=""></asp:Label>            
                 </legend>
                 <table width="100%">
                 <tr>
                 <td align="right">
                  <asp:Button ID="Button52" runat="server" Text=" X "   style="color: #FFFFFF; background-color: #FF0000; height: 26px;"  onclick="Btn_pnlbackupstatus_Click" />
                 </td>
                 </tr>
                 <tr>
                        <td>
                         <label style="width:100%;">
                            <asp:Label ID="Label37C" runat="server" Text="List of tables whose all records are  not to be deleted  when creating the new Version of the database"></asp:Label><br />
                            <asp:Label ID="Label2" runat="server" Text="All other data from all other tables will be deleted when creating new Version"></asp:Label>                            									
                            </label>                            
                    </td>
                 </tr>
                 <tr>
                      <td > 
                                <input id="Hidden1" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                <input id="Hidden2" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                    <input runat="server" id="Hidden3" type="hidden" name="hdnProductDetailId" style="width: 3px" />
                                        <asp:Panel ID="Panel3" runat="server" ScrollBars="Horizontal" Height="400px">
                                            <asp:GridView ID="gv_table" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                                           Width="720px" OnRowDataBound="gv_table_RowDataBound"    AlternatingRowStyle-CssClass="alt" 
                                                OnRowCommand="gv_table_RowCommand" EmptyDataText="There is no data." AllowSorting="True"                                                                        
                                                    OnPageIndexChanging="gv_table_PageIndexChanging" OnSorting="gv_table_Sorting"
                                                    OnSelectedIndexChanged="gv_table_SelectedIndexChanged" AllowPaging="false" PageSize="25">
                                                <Columns>
                                                    <asp:TemplateField ControlStyle-Width="5%" FooterStyle-Width="5%" HeaderStyle-Width="5%"  >
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="cbHeader" runat="server" OnCheckedChanged="ch1table_chachedChanged" AutoPostBack="true" />
                                                            <asp:Label ID="check" runat="server" ForeColor="White" Text="ALL" HeaderStyle-Width="30px" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbItem" Checked='<%# Bind("chk") %>'  runat="server" />
                                                            <asp:CheckBox ID="chkdef" Checked='<%# Bind("chk") %>' runat="server" Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Table Name" SortExpression="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="37%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltblname" runat="server" Text='<%# Bind("TableName") %>'></asp:Label>
                                                            <asp:Label ID="lbltblid" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>                                                                                                                                               
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="" SortExpression="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                        <ItemTemplate> 
                                                               <asp:Label ID="lblconnegrid" runat="server" Text="Connection possible" style="color:Green"></asp:Label>                                                      
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                                                                            
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                        <asp:Button ID="btn_finish" runat="server"  CssClass="btnSubmit" onclick="btn_finish_Click" Text="Finish"   />              
                                                                
                                      
                 </td>
                 </tr>
                 </table> 
                       
            </fieldset>
               </div>
           </asp:Panel>


    </ContentTemplate>

     <Triggers>          
            
            <asp:PostBackTrigger ControlID="btn_uploadmdf" />
            <asp:PostBackTrigger ControlID="btn_uploadldf" />
            
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

