<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Sync_Table_Record_AtServer.aspx.cs" Inherits="SyncData" Title="Sync Data" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            
           
                        
                            
                     <fieldset>
                    <legend>
                        <asp:Label ID="Label3" runat="server" Text="Changes done in Tables of Master Server which needs to be syncronsied with satellite server "></asp:Label>
                    </legend>  
                        <table>
                                 <tr>
                                        <td>
                                        </td> 
                                             <label style="background-color:#d5a26d;">
                                                <asp:LinkButton ID="lblnewrecordforsync" OnClick="btnsynTables"    runat="server" Text="" ></asp:LinkButton>
                                                <asp:ImageButton ID="imgrefreshstaname" Visible="false"  runat="server" AlternateText="Refresh" Height="20px" ImageUrl="~/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px" ImageAlign="Bottom" OnClick="imgrefreshstaname_Click" />
                                                </label>         
                                        </tr>
                                        </table>
                                        </fieldset>
                     <fieldset>
                    <legend>
                        <asp:Label ID="Label2" runat="server" Text="How many changes pending for syncronisation at different satellite server"></asp:Label>
                    </legend>  
                        <table style="width:100%">
                                 <tr>
                                            <td align="right" style="background-color:#d5a26d;">  
                                                                                
                                            </td>
                                        </tr>
                            <tr>
                                <td>
                                  <asp:Panel ID="Panel1" runat="server" Width="100%" Height="220px" ScrollBars="Vertical">
                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                                                    OnRowCommand="GridView1_RowCommand"     EmptyDataText="No data required to be synchronise right now" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" Width="100%" 
                                                         AllowSorting="True" >
                                                        <%--PageSize="15" AllowPaging="True" OnPageIndexChanging="grdserver_PageIndexChanging" OnSorting="grdserver_Sorting" --%>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Satellite Server Name" SortExpression="ServerName" HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Width="35%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("ServerName") %>'></asp:Label>  
                                                                    <asp:Label ID="lbl_sId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>  
                                                                         </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>                                                           
                                                          
                                                               <asp:TemplateField HeaderText="Total Records which needs to be changed" SortExpression="RecordID"  HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrecordid" runat="server"  Visible="true"></asp:Label>                                                                                                     
                                                                </ItemTemplate>                                                               
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                           
                                                           <asp:TemplateField HeaderText="Take action to create job to make changes"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                            <ItemTemplate>                                           
                                                                <asp:ImageButton ID="imgbtnbackup" runat="server" Height="25px"  CommandArgument='<%# Eval("Id") %>' ToolTip="CreateJob" CommandName="backup" ImageUrl="~/images/backupgreen.png" />                                                                                                                     
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="30%" />                                                          
                                                        </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>    
                    </fieldset>

                   
            <fieldset>
                <legend>
                   Table wise records at different satellite servers which needs to be syncronsied based on Syncronisation job done in previous step.
                </legend> 
                <table style="width:100%">
                    <tr>
                        <td>
                            <label>
                        <asp:Label ID="Label5" runat="server" Text="Select the satellite server to see number of records needs to be syncronised."></asp:Label>
                            </label>
                            <label>
                        <asp:DropDownList ID="ddlserver" runat="server"  AutoPostBack="True" Width="200px"   onselectedindexchanged="ddlserver_SelectedIndexChanged">
                        </asp:DropDownList>
                        </label> 
                        </td>
                    </tr>
                </table> 
           <asp:Panel ID="pnl_serverselect" runat="server" Visible="true">  
              <div class="products_box">
               <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Pending synchronisation with client databases."></asp:Label>
                    </legend>  
                                    <table width="100%">                                        
                                        <tr>
                                            <td>
                                                  <asp:Panel ID="Panel3" runat="server" Visible="false">  
                                                    <label>
                                                            <asp:Label ID="Label1" runat="server" Text="Table Name"></asp:Label>
                                                    </label> 
                                                    <label>
                                                         <asp:DropDownList ID="DDLLiceTableName" runat="server" Width="200px" >
                                                                    </asp:DropDownList>
                                                    </label> 
                                                    <label>
                                                                <asp:Label ID="Label15" runat="server" Text="From date"></asp:Label>
                                                            </label>
                                                    <label>
                                                                <asp:TextBox ID="txtfdate" runat="server" Width="80px"></asp:TextBox>
                                                            </label>
                                                    <label>
                                                                <asp:ImageButton ID="imgfd" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                                   <cc1:CalendarExtender ID="cd1" runat="server" PopupButtonID="imgfd" TargetControlID="txtfdate">
                                                                </cc1:CalendarExtender>
                                                            </label>
                                                    <label>
                                                                <asp:Label ID="Label16" runat="server" Text="To date"></asp:Label>
                                                            </label>
                                                    <label>
                                                                <asp:TextBox ID="txttodate" runat="server" Width="80px"></asp:TextBox>
                                                            </label>
                                                    <label>
                                                                <asp:ImageButton ID="imgtodate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                                    <cc1:CalendarExtender ID="cedate" runat="server" PopupButtonID="imgtodate" TargetControlID="txttodate">
                                                                </cc1:CalendarExtender>
                                                            </label>                                                            
                                                    <label>
                                                      <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" OnClick="btnSubmit_Click" Text=" Go " ValidationGroup="1" />
                                                   </label> 
                                                  </asp:Panel>
                                            </td>
                                        </tr>    
                                        <tr>
                                            <td>
                                                 
                                            </td>
                                        </tr>
                                            <tr>
                                                <td align="right" style="border-style: groove hidden groove hidden">  
                                                     <asp:Button ID="Button2" runat="server" CssClass="btnSubmit"  OnClick="btnsyncsilent_Click" Text=" Start Syncronise " ValidationGroup="1" />                                                                                                        
                                                </td>
                                            </tr>   
                                            <tr>
                                                <td>
                                                 <asp:Panel ID="pnlsync" runat="server" Width="100%" Height="250px" ScrollBars="Vertical">
                                                    <asp:GridView ID="grdserver" runat="server" DataKeyNames="TableId" AutoGenerateColumns="False"
                                                        EmptyDataText="No data required to be synchronise right now" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" Width="100%" 
                                                         AllowSorting="True" >
                                                        <%--PageSize="15" AllowPaging="True" OnPageIndexChanging="grdserver_PageIndexChanging" OnSorting="grdserver_Sorting" --%>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Satellite Server Name" SortExpression="ServerName" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("ServerName") %>'></asp:Label>  
                                                                    <asp:Label ID="lbl_TableId" runat="server" Text='<%# Eval("TableId") %>' Visible="false"></asp:Label>  
                                                                         </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>                                                           
                                                            <asp:TemplateField HeaderText="Sync requiring table name" SortExpression="TableName"  HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltabname" runat="server" Text='<%# Eval("TableName") %>' Visible="true"></asp:Label>
                                                                    <asp:Label ID="lblsyncreq" runat="server" Text='<%# Eval("TableName") %>' Visible="false" ></asp:Label>
                                                                    <asp:Label ID="lblseid" runat="server" Text='<%# Eval("SatelliteServerID") %>' Visible="false"></asp:Label>                                                                   
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                               <asp:TemplateField HeaderText="Total Panding Record" SortExpression="RecordID"  HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrecordid" runat="server"  Visible="true"></asp:Label>                                                                                                     
                                                                </ItemTemplate>
                                                                <%--Text='<%# Eval("RecordID") %>'--%>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="cbHeader" runat="server" Checked="true" Visible="false"  />
                                                                    <asp:Label ID="check" runat="server" ForeColor="White" Text="Sync" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbItem" runat="server" Checked="true" /></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                           <%--  <asp:TemplateField HeaderText="Full Table Syncronis"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                            <ItemTemplate>                                           
                                                                <asp:ImageButton ID="imgbtnbackup" runat="server" Height="25px"  CommandArgument='<%# Eval("Id") %>' ToolTip="CreateJob" CommandName="backup" ImageUrl="~/images/backupgreen.png" />                                                                                                                     
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="30%" />                                                          
                                                        </asp:TemplateField>--%>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                                       <input id="hdnsortExp1" type="hidden" name="hdnsortExp1" style="width: 3px" runat="Server" />
                                                        <input id="hdnsortDir1" type="hidden" name="hdnsortDir1" style="width: 3px" runat="Server" />
                                                        <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                                        <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                                        <input id="hdnPricePlanId" name="hdnPricePlanId" runat="server" type="hidden" style="width: 1px" />
                                                 </asp:Panel>
                                                </td>
                                            </tr>
                                    </table>

                                        <asp:Timer ID="Timer1" runat="server" Interval="1000"  Enabled="false" ontick="Timer1_Tick">
                                                                    </asp:Timer> 
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>                                                                         
                                                                        <asp:Label ID="Label6" runat="server" Visible="false" Text="1"></asp:Label>
                                                                          <asp:Label ID="lbl_che5time" runat="server" Visible="false" Text="1"></asp:Label>
                                                              <asp:Panel ID="Paneldoc" runat="server"  Width="40%" BackColor="White"   CssClass="modalPopup" Visible="false">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td colspan="2" align="center">
                                                                                <asp:Image ID="Image11234" runat="server"   />
                                                                            </td>                                                
                                                                        </tr>                                           
                                                                        <tr>
                                                                            <td colspan="2">
                                                                            <label>
                                                                            <asp:Label ID="lblportmsg" runat="server" Text="Please waite for some moment we opening TCP port"></asp:Label>
                                                                                   <asp:LinkButton ID="link_openport" OnClick="btnsynTables"    runat="server" Text="" ></asp:LinkButton>                                                             
                                                                                   </label> 
                                                                            </td>
                                                                        </tr>
                                                                    </table>                                   
                                                              </asp:Panel>
                                                               <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick">
                                                                        </asp:AsyncPostBackTrigger>
                                                                    </Triggers>                                                        
                                                                </asp:UpdatePanel>


                </fieldset>               
             </div>
             <div>
                <fieldset>
                    <legend> <asp:Label ID="Label4" runat="server" Text="Fully Table Syncronis Again"></asp:Label>
                    </legend> 
                    <table style="width:100%">
                          <tr>
                              <td align="right" style="border-style: groove hidden groove hidden">  
                                   <asp:Button ID="Btn_GenerateScript" runat="server" CssClass="btnSubmit"  OnClick="Btn_GenerateScript_Click" Text=" Start To Generate Script " ValidationGroup="1" />                                                                                                        
                              </td>
                          </tr>  
                          <tr>
                            <td>
                                <asp:Panel ID="Panel2" runat="server" Width="100%" Height="250px" ScrollBars="Vertical">
                                                    <asp:GridView ID="GridView2" runat="server" DataKeyNames="TableId" AutoGenerateColumns="False"
                                                      OnRowCommand="GridView2_RowCommand"      EmptyDataText="No data required to be synchronise right now" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" Width="100%" 
                                                         AllowSorting="True" >                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Table name ( Table Id )" SortExpression="TableName"  HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltabname" runat="server" Text='<%# Eval("TableName") %>' Visible="true"></asp:Label>                                                                   
                                                                   ( <asp:Label ID="lblTableId" runat="server" Text='<%# Eval("TableId") %>' Visible="true"></asp:Label>                                                                   )
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Table Title" SortExpression="TableTitle"  HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrecordid" runat="server"  Text='<%# Eval("TableTitle") %>'  Visible="true"></asp:Label>                                                                                                     
                                                                </ItemTemplate>                                                              
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Total Record in License / Total Record For Above Server" SortExpression="TableTitle"  HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                <asp:Label ID="lbl_total_record_in_license_table" runat="server"   Visible="true"></asp:Label>                                                                                                     
                                                                  /  <asp:Label ID="lbl_total_record_for_ser" runat="server"    Visible="true"></asp:Label>                                                                                                     
                                                                </ItemTemplate>                                                              
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="cbHeader" runat="server" Checked="true" Visible="false"  />
                                                                    <asp:Label ID="check" runat="server" ForeColor="White" Text="Sync" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbItem" runat="server" Checked="true" /></ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Full Table Syncronis"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                            <ItemTemplate>                                                                                                          
                                                                <asp:Button ID="Btnsyncagain" runat="server" CssClass="btnSubmit"  CommandArgument='<%# Eval("Id") %>'   CommandName="backup" Text=" Start Syncronise " ValidationGroup="1" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="30%" />                                                          
                                                        </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                                      
                                                 </asp:Panel>
                            </td>
                        </tr>
                    </table> 
                </fieldset> 
            </div>
            </asp:Panel>
</fieldset> 
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
