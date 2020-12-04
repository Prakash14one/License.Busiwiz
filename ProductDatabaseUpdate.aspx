<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="ProductDatabaseUpdate.aspx.cs" Inherits="ProductUpdateLastt" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }


        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59) {


                alert("You have entered an invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered an invalid character");
            }

            counter = document.getElementById(id);

            if (txt1.value.length <= max_len) {
                remaining_characters = max_len - txt1.value.length;
                counter.innerHTML = remaining_characters;
            }
        }

        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        } 

    </script>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="margin-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" ></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Create product database new version"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="addnewpanel" runat="server" Text="Create New Version" CssClass="btnSubmit" OnClick="addnewpanel_Click" />
                         <asp:Button ID="btndosyncro" runat="server" CssClass="btnSubmit"  OnClick="btndosyncro_Clickpop" Text="Do Synchronise" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Select product name"></asp:Label>
                                        <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlproductversion"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlproductversion" runat="server" AutoPostBack="True" onselectedindexchanged="ddlproductversion_SelectedIndexChanged" Width="400px" >
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlcodetypecatefory" runat="server" AutoPostBack="True" onselectedindexchanged="ddlcodetypecatefory_SelectedIndexChanged" Visible="false">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <label>
                                         <asp:Label ID="Label7lde" runat="server" Text="Products description"></asp:Label>
                                     </label>
                                </td>
                                <td>
                                    <label>                                                                  
                                        <asp:TextBox ID="txt_prod_desc" BackColor="White" BorderColor="White" BorderStyle="None" ForeColor="Black" TextMode="MultiLine" Width="600px" Height="45px"  runat="server" Enabled="False"></asp:TextBox>
                                    </label> 
                                </td>
                            </tr>
                            
                             <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label2re" runat="server" Text="Select database name"></asp:Label>
                                         <asp:Label ID="Label9re" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlcodetype"
                                            ErrorMessage="*" ValidationGroup="1" ></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlcodetype" runat="server" AutoPostBack="True" Width="250px" onselectedindexchanged="ddlcodetype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label style="width:15px;">                                         
                                          <asp:Label ID="lblconne" runat="server" Text="" ></asp:Label>
                                    </label> 
                                     <label>
                                            
                                            <asp:Panel ID="pnl_websiter1" runat="server" Visible="true">                          
                                                    <input id="Hidden1" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                                        <input id="Hidden2" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                                         <input runat="server" id="hdnProductDetailId" type="hidden" name="hdnProductDetailId" style="width: 3px" />
                                                               <asp:Panel ID="pnlpr" runat="server" Width="400px"  ScrollBars="Horizontal" Height="200px">
                                                                    <asp:GridView ID="GridView2" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                                                                    OnRowCommand="GridView2_RowCommand" EmptyDataText="There is no data." AllowSorting="True"
                                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                     OnRowDataBound="GridView2_RowDataBound"   Width="400px" OnPageIndexChanging="GridView2_PageIndexChanging" OnSorting="GridView2_Sorting"
                                                                         OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                                                                        <Columns>
                                                                            <asp:TemplateField ControlStyle-Width="5%" FooterStyle-Width="5%" HeaderStyle-Width="5%"  Visible="false">
                                                                                <HeaderTemplate>
                                                                                   <asp:CheckBox ID="cbHeader" runat="server" Visible="false"  OnCheckedChanged="ch1_chachedChanged" AutoPostBack="true" />
                                                                                    <asp:Label ID="check" runat="server" ForeColor="White" Text="" HeaderStyle-Width="30px" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>                                                                                                                                                                                                                          
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>                                                                          
                                                                             <asp:TemplateField HeaderText="Database Name" SortExpression="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("CodeTypeName") %>'></asp:Label>
                                                                                      <asp:Label ID="lbl_codetypeid" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Need To Create Version?" SortExpression="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                                                <ItemTemplate>
                                                                                     <asp:CheckBox ID="cbItem" Checked='<%# Bind("chk") %>'  runat="server" Enabled="false" Visible="false" />
                                                                                    <asp:CheckBox ID="chkdef" Checked='<%# Bind("chk") %>' runat="server" Visible="false" />             
                                                                                    <asp:Label ID="lblcheck" runat="server" ></asp:Label>                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Select">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="linkdow" Style="color: #717171;" runat="server" Text="Select" OnClick="linkdow_Click"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                       
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </asp:Panel>
                                    
                                    </label> 
                                </td>
                            </tr>
                            <tr>
                                <td>
                                
                                </td>
                                <td align="right"> 
                                        <asp:Button ID="btn_showdetail" Height="20px"   runat="server" Text="Show More Details" OnClick="btn_showdetail_Click" CssClass="btnSubmit" />
                                </td>
                            </tr>
                         <asp:Panel ID="pnl_showdetail" runat="server" Width="100%" Visible="false">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="New Code Version Number"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:Label ID="lblnewcodetypeNo" runat="server" Text="New code version number"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <label>
                                        Client FTP account name
                                    </label> 
                                </td>
                                <td>
                                    <label>
                                         <asp:Label ID="ftpservename" runat="server" Text="New code version number"></asp:Label>
                                    </label> 
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                       Client FTP user ID
                                    </label> 
                                </td>
                                <td>
                                      <label>
                                         <asp:Label ID="ftpuser" runat="server" Text="New code version number"></asp:Label>
                                    </label> 
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Root folder physical path (MDF file)"></asp:Label>
                                    </label>
                                    
                                </td>
                                <td>
                                    <label>
                                       <asp:TextBox ID="txtmdffile" runat="server" Width="700px" Enabled="false"></asp:TextBox>
                                    </label>
                                    
                                </td>
                            </tr> 
                             <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text="Root folder physical path (LDF file)"></asp:Label>                                       
                                       
                                    </label>
                                   
                                </td>
                                <td>
                                    <label>
                                       <asp:TextBox ID="txtldffilepath" runat="server" Width="700px" Enabled="false"></asp:TextBox>
                                    </label>
                                     <label>
                                    
                                    </label> 
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Labeltemp" runat="server" Text="Temp path folder name of database"></asp:Label>
                                        
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txttemppath" runat="server" Enabled="False" Width="700px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text="Output path folder name of database"></asp:Label>                                       
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtoutputsourcepath" runat="server" Enabled="False" Width="700px"></asp:TextBox>
                                    </label>
                                     
                                </td>
                            </tr>
                            </asp:Panel>
                             <asp:Panel ID="Panel1" runat="server" Visible="false">
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label26" runat="server" Text="MDF File Name"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtmdffilename" runat="server" Width="500px" Enabled="false"></asp:TextBox>                                       
                                    </label>
                                   
                                </td>
                            </tr>                              
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label171" runat="server" Text="LDF File Name"></asp:Label>                                        
                                         
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtldffilename" runat="server" Width="500px" Enabled="false"></asp:TextBox>
                                    </label>
                                    <label>
                                  
                                    </label> 
                                </td>
                            </tr>  
                            </asp:Panel>
                            
                            <tr>
                                <td colspan="2">
                                 <asp:Panel ID="Panel2" runat="server" Visible="false">        
                                      <asp:CheckBox ID="chkday" runat="server" AutoPostBack="true"  oncheckedchanged="chkday_CheckedChanged" Visible="false" Text="Automatically create new version for this database that interval of following days (if any table have changes in the database)" />
                                      </asp:Panel>
                                    <label>
                                        <asp:TextBox ID="txt_day" Visible="false" runat="server"  Width="80px"></asp:TextBox>
                                    </label> 
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                    
                                    <asp:Button ID="btn_VersionCreate" runat="server" Text="Create Version" OnClick="btn_VersionCreate_Click" CssClass="btnSubmit"  />
                                    <asp:Button ID="Btncancle" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="Btncancle_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label10" runat="server" Text="List Of Product Database Version"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right">
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label9" runat="server" Text="Filter by Product"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="FilterProductname" Width="200px" runat="server" AutoPostBack="True" onselectedindexchanged="FilterProductname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                 <label>
                                        <asp:Label ID="Label2" runat="server" Text="Select Category"></asp:Label>                                         
                                    </label>
                                    <label>
                                    <asp:DropDownList ID="ddlCategorySearch" Width="200px"  runat="server" AutoPostBack="True" onselectedindexchanged="FilterProductname_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                    </label> 
                                <label>
                                    <asp:Label ID="Label16" runat="server" Text="Code Name"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlctype" Width="200px" runat="server" AutoPostBack="false">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Button ID="Button2" runat="server" Text="Go" CssClass="btnSubmit" OnClick="Button2_Click1" /></label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="list of product update"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label22" runat="server" Font-Italic="true" Text="Products/version :"></asp:Label>
                                                                <asp:Label ID="lblproductname" runat="server" Font-Italic="true"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="Label23" runat="server" Font-Italic="true" Text="Code Type :"></asp:Label>
                                                                <asp:Label ID="lblcodetype" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                  OnRowDataBound="GridView1_RowDataBound"   PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="Id"
                                                    EmptyDataText="No Record Found." AllowSorting="True" OnSorting="GridView1_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="21%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpversion" runat="server" Text='<%#Bind("ProductName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code Name" SortExpression="CodeType" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="18%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBackColor" runat="server" Text='<%#Bind("CodeTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code Version" SortExpression="CodeVersion" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcodeversion" runat="server" Text='<%#Bind("CodeVersion") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Successfully created?" HeaderStyle-HorizontalAlign="Left" >
                                                                 <ItemTemplate>
                                                                        <asp:Label ID="lblstatus" runat="server"  Text="" ></asp:Label>
                                                                        <%--Text='<%#Bind("Successfullycreated") %>'--%>
                                                                            <asp:Label ID="lbl_latestpath" runat="server" Text='<%#Bind("TemporaryPath") %>' Visible="false" ></asp:Label>
                                                    
                                                                </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />                                               
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File Name" SortExpression="FileName" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfilename" runat="server" Text='<%#Bind("FileName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File Location" SortExpression="FileLocation" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfilelocation" runat="server" Text='<%#Bind("FileLocation") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </td>
                                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>

                        <tr>
                            <td>
                              <asp:Panel ID="Paneldoc" runat="server" Width="55%" CssClass="modalPopup">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label142" runat="server" Text=""></asp:Label>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 95%;">
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <label>
                                                        <asp:Label ID="Label143" runat="server" Text="Was this the last record you are going to add right now to this table?"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdsync" runat="server">
                                                                    <asp:ListItem Value="1" Text="Yes, this is the last record in the series of records I am inserting/editing to this table right now"></asp:ListItem>
                                                                    <asp:ListItem Value="0" Text="No, I am still going to add/edit records to this table right now"
                                                                        Selected="True"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="btnok" runat="server" CssClass="btnSubmit" Text="OK" OnClick="btndosyncro_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground" PopupControlID="Paneldoc" TargetControlID="btnreh" CancelControlID="ImageButton10">
                                </cc1:ModalPopupExtender>
                            </td>
                            </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_VersionCreate" />
            <%-- <asp:PostBackTrigger ControlID="addnewpanel" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
