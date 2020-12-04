<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="PageWorkMasterNew.aspx.cs" Inherits="PageWorkMaster" Title="Page Work Allocation Master" %>

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
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }

        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }




        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
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
     
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblyes" runat="server" Text="YES" Visible="false" ForeColor="Red"> </asp:Label>
                <asp:CheckBox ID="chkyes" runat="server" Visible="false" AutoPostBack="True" OnCheckedChanged="chkyes_CheckedChanged">
                </asp:CheckBox>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="addnewpanel" runat="server" Text="Add Task" CssClass="btnSubmit"  OnClick="addnewpanel_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                        <fieldset>
                            <table width="100%">
                                <tr>
                                    <td style="width:200px">
                                        <label>
                                            <asp:Label ID="Label3" runat="server" Text="Product"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlWebsiteSection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWebsiteSection_SelectedIndexChanged"
                                                Width="600px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <asp:Panel runat="server" id="pnlclose" Visible="false">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="Menu"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlMainMenu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label6" runat="server" Text="Sub Menu"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlSubmenu" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubmenu_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                                </asp:Panel>
                                <tr>
                    <td>
                   <label style="width:100px">
                                <asp:Label ID="Label60" runat="server" Text="Search"></asp:Label>
                            </label>
                    </td>
                    <td colspan="2">
                    <label style="width:255px;">
                    <asp:TextBox ID="TextBox8" runat="server"   placeholder="Search"  Font-Bold="true"  Width="250px"></asp:TextBox>
                    
                    </label> 
                    <label>
                    <asp:Button ID="Button9" CssClass="btnSubmit" runat="server" Text="Search" OnClick="Button2_ClickSearchAdd"  />
                    </label> 
                    </td>
                    </tr>
                                  <tr>
                        <td><label>
                            <asp:Label ID="Label13Fun" runat="server" Text="Functionality Title"></asp:Label>
                            </label> 
                        </td>
                        <td>
                        <label>
                            <asp:DropDownList ID="ddlfuncti" runat="server" AutoPostBack="True"  Width="220px"
                                onselectedindexchanged="ddlfuncti_SelectedIndexChanged">
                            </asp:DropDownList>
                            </label> 
                        </td>
                        
                    </tr>

                                <tr>
                                    <td>
                                        <label>
                                            Page Name
                                            <asp:Label ID="Label8" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValtor15" runat="server" ControlToValidate="ddlpage"
                                                ErrorMessage="*" InitialValue="0" SetFocusOnError="true" ValidationGroup="1"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged" Width="400px">
                                            </asp:DropDownList>
                                         <asp:Label ID="lbl_pagepath" runat="server" Text="" CssClass="labelcount"></asp:Label>
                                          <asp:Label ID="lbl_pageid" runat="server" Text="" ></asp:Label>
                                        </label>
                                       
                                       
                                    </td>
                                </tr>
                                <tr>
                                <td >
                                
                                </td>
                                <td>
                                <label style="width:600px">
                                <asp:Label ID="lbl_moreversionError" runat="server" Text="" CssClass="labelstar" Visible="false"></asp:Label>
                                </label> 
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                           Pending Page Versions
                                            <asp:Label ID="Label9" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlpageversionid"
                                                ErrorMessage="*" InitialValue="0" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td align="left">
                                        <label style="width:500px;">
                                            <asp:DropDownList ID="ddlpageversionid" runat="server" Width="500px" OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                            </label> 
                                            <label>
                                             <asp:ImageButton ID="ImageButton10" runat="server" Height="20px" ImageUrl="~/images/AddNewRecord.jpg" Visible="false" ToolTip="AddNewVersion" Width="20px" ImageAlign="Bottom" OnClick="ImageButton10_Click" />
                                    <asp:ImageButton ID="ImageButton11" runat="server" AlternateText="Refresh" Height="20px" Visible="false"
                                        ImageUrl="~/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px" ImageAlign="Bottom"
                                        OnClick="ImageButton11_Click" />
                                          <asp:Label ID="Label63" runat="server" Text="*" ></asp:Label>
                                        
                                   </label>
                                         </td>
                                         <td>
                                   
                                              
                                         </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label11" runat="server" Text=" Work Title"></asp:Label>
                                            <asp:Label ID="Label10" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtworkrequtitle"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtworkrequtitle"
                                                Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([-_.a-zA-Z0-9\s]*)"
                                                ValidationGroup="1"> </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtworkrequtitle" MaxLength="50" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\-_a-zA-Z0-9. ]+$/,'Span1',50)"
                                                onkeypress="return checktextboxmaxlength(this,50,event)" runat="server" Width="600px"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="max1" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span1" class="labelcount">50</span>
                                            <asp:Label ID="Label55" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ -)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label12" runat="server" Text="Work Description "></asp:Label>
                                            <asp:RegularExpressionValidator ID="RegularExpressfdionValidator4" runat="server"
                                                ControlToValidate="txtworkreqdesc" Display="Dynamic" ErrorMessage="Invalid Character"
                                                SetFocusOnError="True" ValidationExpression="^([-_a-z.A-Z0-9\s]*)" ValidationGroup="1"> </asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:TextBox ID="txtworkreqdesc" runat="server" Width="600px" MaxLength="300" Height="60px"
                                                onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\-_a-zA-Z0-9. ]+$/,'Span2',300)"
                                                TextMode="MultiLine" onkeypress="return checktextboxmaxlength(this,300,event)"></asp:TextBox>
                                        </label>
                                        <label>
                                            <asp:Label ID="Label56" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                            <span id="Span2" class="labelcount">300</span>
                                            <asp:Label ID="Label57" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ . -)"></asp:Label>
                                        </label>
                                    </td>
                                </tr>

                                <tr>
                                <td style="font-size: 14px;font-weight:bold;  color: #416271;">
                                <lable>
                                     Developer Incentive
                                     
                                </lable>
                                </td>
                                <td>
                                 <asp:TextBox ID="txt_incentive" runat="server" Width="220px" MaxLength="4" onkeyup="return mak('Span1',4,this)"></asp:TextBox>
                      
                                </td>
                                </tr>
                                 <tr>
                                <td style="font-size: 14px;font-weight:bold;  color: #416271;">
                                <lable>
                                     Tester Incentive
                                     
                                </lable>
                                </td>
                                <td>
                                 <asp:TextBox ID="txt_incentivetester" runat="server" Width="220px" MaxLength="4" onkeyup="return mak('Span1',4,this)"></asp:TextBox>
                      
                                </td>
                                </tr>
                                 <tr>
                                <td style="font-size: 14px;font-weight:bold;  color: #416271;">
                                <lable>
                                     Supervisor Incentive
                                     
                                </lable>
                                </td>
                                <td>
                                 <asp:TextBox ID="txt_incentiveSuper" runat="server" Width="220px" MaxLength="4" onkeyup="return mak('Span1',4,this)"></asp:TextBox>
                    
                                </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label13" runat="server" Text="Do you wish to allow the developer to download the relevant source code files to work for this task ? Yes"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkallowedfiledown" runat="server" AutoPostBack="True" OnCheckedChanged="chkallowedfiledown_CheckedChanged" Checked="true">
                                        </asp:CheckBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlupfile" runat="server" Visible="true">
                                        <fieldset>
                                            <legend>Download Source Files </legend>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pnlpv" runat="server">
                                                            <asp:GridView ID="grdpversion" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                DataKeyNames="pvid" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                                AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found." Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" SortExpression="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldate" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Page Version Name" HeaderStyle-HorizontalAlign="Left"
                                                                        SortExpression="VersionName">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblpvname" runat="server" Text='<%#Bind("VersionName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Version No" HeaderStyle-HorizontalAlign="Left" SortExpression="VersionNo"
                                                                        Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblversionname" runat="server" Text='<%#Bind("VersionNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Page Name" HeaderStyle-HorizontalAlign="Left" SortExpression="PageName">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblpname" runat="server" Text='<%# Bind("PageName") %>'></asp:Label>
                                                                            <asp:Label ID="lblpid" runat="server" Text='<%# Bind("PageId") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Download" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="linkdow" ForeColor="Black" runat="server" Text="Download" OnClick="linkdow_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Check" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chk" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <fieldset>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label5" runat="server" Text="Do you wish to send any other files/ sourcecode to developer ? Yes"></asp:Label>
                                                        </label>
                                                        <asp:CheckBox ID="chkothersourcefile" runat="server" AutoPostBack="True" OnCheckedChanged="chkothersourcefile_CheckedChanged" Checked="false">
                                                        </asp:CheckBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <asp:Panel ID="pnlothersourcefile" runat="server" Visible="false" ScrollBars="None">
                                            <fieldset>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label14" runat="server" Text="Folder Name"></asp:Label>
                                                                <asp:Label ID="Label15" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="reqs" runat="server" SetFocusOnError="true" Display="Dynamic"
                                                                    ControlToValidate="txtfoldername" ErrorMessage="*" ValidationGroup="3"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtfoldername" runat="server"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label16" runat="server" Text="File Name"></asp:Label>
                                                                <asp:Label ID="Label17" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="req" runat="server" ControlToValidate="txtfname"
                                                                    ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" ValidationGroup="3"></asp:RequiredFieldValidator>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="txtfname" runat="server"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Button ID="Button5" runat="server" ValidationGroup="3" Text="Add File" OnClick="Button5_Click" />
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Panel ID="pnlgenefile" runat="server" ScrollBars="None" Width="100%">
                                                                <asp:GridView ID="grdgene" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                    EmptyDataText="No Record Found." CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                                    AlternatingRowStyle-CssClass="alt" HeaderStyle-HorizontalAlign="Left" OnRowCommand="grdgene_RowCommand"
                                                                    Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Folderpath" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblfo" runat="server" Text='<%#Bind("Folderpath") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Page Version Name" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblf" runat="server" Text='<%#Bind("Filename") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Download" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="linkdow" runat="server" Text="Download" OnClick="linkdow_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:ButtonField HeaderText="Remove" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Black"
                                                                            ItemStyle-Width="50px" Text="Remove" CommandName="del"></asp:ButtonField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </asp:Panel>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <fieldset>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label7" runat="server" Text="Do you wish to upload any  file for the instructions? Yes"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkupload" runat="server" AutoPostBack="True" OnCheckedChanged="chkupload_CheckedChanged" Checked="true">
                                        </asp:CheckBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlup" runat="server" Visible="true">
                                        <fieldset>
                                            <table width="100%" id="pagetbl">
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label19" runat="server" Text=" Title"></asp:Label>
                                                            <asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txttitlename"
                                                                ErrorMessage="*" ValidationGroup="2"> </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txttitlename"
                                                                Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                ValidationGroup="1"> </asp:RegularExpressionValidator>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:TextBox ID="txttitlename" onKeydown="return mask(event)" MaxLength="30" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span10',30)"
                                                                runat="server"></asp:TextBox>
                                                        </label>
                                                        <label>
                                                            Max <span id="Span10">30</span>
                                                            <asp:Label ID="Label54" runat="server" Text="(A-Z,0-9,_)"></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="Upradio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Upradio_SelectedIndexChanged"
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Upload Audio Instruction File</asp:ListItem>
                                                            <asp:ListItem Value="2">Upload Other Files</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="pnlpdfup" runat="server" Visible="false">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Label ID="Label21" runat="server" Text="Pdf File"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:FileUpload ID="fileuploadadattachment" runat="server" />
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="pnladio" runat="server" Visible="false">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Label ID="Label22" runat="server" Text=" Audio File"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:FileUpload ID="fileuploadaudio" runat="server" />
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button2" CssClass="btnSubmit" runat="server" Text="Add" OnClick="Button2_Click"
                                                            ValidationGroup="2" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:GridView ID="gridFileAttach" runat="server" CssClass="mGrid" GridLines="Both"
                                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                            Width="100%" OnRowCommand="gridFileAttach_RowCommand">
                                                            <Columns>
                                                               
                                                                <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltitle" runat="server" Text='<%#Bind("Title") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Other URL" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblpdfurl" runat="server" Text='<%#Bind("PDFURL") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Audio URL" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblaudiourl" runat="server" Text='<%#Bind("AudioURL") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField CommandName="Delete1" ItemStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderText="Delete" ImageUrl="~/Account/images/delete.gif" Text="Delete" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="testup" runat="server" Visible="false">
                                        <asp:Button ID="btnup" runat="server" CssClass="btnSubmit" OnClick="btnup_Click"
                                            Text="Upload Files" />
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <fieldset>
                            <legend>Select Developer, Tester and Supervisor for this task allocation
                                <asp:ImageButton ID="imgadddept" runat="server" Height="20px" ImageUrl="~/images/add_file.png" Visible="false" 
                                    OnClick="LinkButton97666667_Click" ToolTip="AddNew" Width="20px" ImageAlign="Bottom" />
                                &nbsp;<asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh" Visible="false"
                                    Height="20px" ImageUrl="~/images/refresh.png" OnClick="LinkButton13_Click" ToolTip="Refresh"
                                    Width="20px" ImageAlign="Bottom" />
                            </legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <label style="width:160px">
                                            <asp:Label ID="Label23" runat="server" Text="Assigned to Developer"></asp:Label>
                                         
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:190px">
                                            <asp:DropDownList ID="ddlempassdeve" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlempassdeve_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:120px">
                                            <asp:Label ID="Label25" runat="server" Text="Targate Date"></asp:Label>
                                            <asp:Label ID="Label26" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txttargetdatedeve"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label  style="width:75px">
                                            <asp:TextBox ID="txttargetdatedeve" runat="server" Width="65px"></asp:TextBox>
                                        </label>
                                        <label style="width:30px">
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                        </label>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttargetdatedeve"
                                            PopupButtonID="ImageButton2">
                                        </cc1:CalendarExtender>
                                      
                                    </td>
                                    <td>
                                       <label  style="width:110px">
                                            <asp:Label ID="Label27" runat="server" Text=" Budgeted hour"></asp:Label>
                                            <asp:Label ID="Label28" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtbudgethourdev"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularEessionValidator3" runat="server" ControlToValidate="txtbudgethourdev"
                                                ErrorMessage="Not Valid" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-0][0-8]|[0][9]):(([0-5][0-9]|[0][0]))$"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                         <label  style="width:75px">
                                            <asp:TextBox ID="txtbudgethourdev" runat="server" MaxLength="5" Width="70px" AutoPostBack="True"
                                                OnTextChanged="txtbudgethourdev_TextChanged">03:00</asp:TextBox>
                                        </label>
                                        <label style="width:40px">
                                            <asp:Label ID="lblmsg0" runat="server" ForeColor="Red">HH:MM</asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                      
                                            <asp:Label ID="Label1" runat="server" Visible="false">Rate per Hour </asp:Label>
                                            <asp:Label ID="lbldeveloperrate" runat="server" Text="0" Visible="false"></asp:Label>
                                      
                                    </td>
                                    <td>
                                       
                                            <asp:Label ID="Label2" runat="server" Visible="false">Cost </asp:Label>
                                            <asp:Label ID="lbldevelopercost" runat="server" Text="0" Visible="false"></asp:Label>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label style="width:160px">
                                            <asp:Label ID="Label30" runat="server" Text="Assigned to Tester"></asp:Label>
                                           </label>
                                    </td>
                                    <td>
                                        <label style="width:190px">
                                            <asp:DropDownList ID="ddlempasstester" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlempassdeve_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:120px">
                                            <asp:Label ID="Label29" runat="server" Text=" Targate Date"></asp:Label>
                                            <asp:Label ID="Label32" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txttargatedatetester"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label  style="width:75px">
                                            <asp:TextBox ID="txttargatedatetester" runat="server" Width="65px"></asp:TextBox>
                                        </label>
                                        <label  style="width:30px">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                        </label>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txttargatedatetester"
                                            PopupButtonID="ImageButton1">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                         <label  style="width:110px">
                                            <asp:Label ID="Label33" runat="server" Text=" Budgeted hour"></asp:Label>
                                            <asp:Label ID="Label34" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtbudhourtest"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtbudhourtest"
                                                ErrorMessage="Not Valid" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-0][0-8]|[0][9]):(([0-5][0-9]|[0][0]))$"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                         <label  style="width:75px">
                                            <asp:TextBox ID="txtbudhourtest" runat="server" Width="65px" MaxLength="5" OnTextChanged="txtbudhourtest_TextChanged"
                                                AutoPostBack="True">01:00</asp:TextBox>
                                        </label>
                                        <label style="width:40px">
                                            <asp:Label ID="lblmsg1" runat="server" ForeColor="Red">HH:MM</asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                      
                                            <asp:Label ID="Label18" runat="server" Visible="false">Rate per Hour </asp:Label>
                                            <asp:Label ID="lbltesterrate" runat="server" Text="0" Visible="false"></asp:Label>
                                      
                                    </td>
                                    <td>
                                        
                                            <asp:Label ID="Label31" runat="server" Visible="false">Cost </asp:Label>
                                            <asp:Label ID="lblcosttester" runat="server" Text="0" Visible="false"></asp:Label>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label style="width:160px">
                                            <asp:Label ID="Label35" runat="server" Text=" Assigned to Supervisor"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:190px">
                                            <asp:DropDownList ID="ddlempasssuper" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlempassdeve_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td> 
                                        <label style="width:120px">
                                            <asp:Label ID="Label37" runat="server" Text="Targate Date"></asp:Label>
                                            <asp:Label ID="Label38" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txttarsupapprove"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label  style="width:75px">
                                            <asp:TextBox ID="txttarsupapprove" runat="server" Width="65px"></asp:TextBox>
                                        </label>
                                        <label  style="width:30px">
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                        </label>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txttarsupapprove"
                                            PopupButtonID="ImageButton3">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                          <label  style="width:110px">
                                            <asp:Label ID="Label39" runat="server" Text="Budgeted hour"></asp:Label>
                                            <asp:Label ID="Label40" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtbudhoursupcheck"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtbudhoursupcheck"
                                                ErrorMessage="Not Valid" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-0][0-8]|[0][9]):(([0-5][0-9]|[0][0]))$"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                         <label  style="width:75px">
                                            <asp:TextBox ID="txtbudhoursupcheck" runat="server" Width="70px" 
                                            MaxLength="5" OnTextChanged="txtbudhoursupcheck_TextChanged"
                                                AutoPostBack="True">01:00</asp:TextBox>
                                        </label>
                                        <label style="width:40px">
                                            <asp:Label ID="lblmsg2" runat="server" ForeColor="Red">HH:MM</asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                       
                                            <asp:Label ID="Label24" runat="server" Visible="false">Rate per Hour </asp:Label>
                                            <asp:Label ID="lblratesupervisor" runat="server" Text="0" Visible="false"></asp:Label>
                                       
                                    </td>
                                    <td>
                                      
                                            <asp:Label ID="Label59" runat="server" Visible="false">Cost </asp:Label>
                                            <asp:Label ID="lblsupervisorcost" runat="server" Text="0" Visible="false"></asp:Label>
                                       
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td colspan="6">
                                        <label>
                                            Do you wish to allocate this task to the assigned employee for specific date now
                                            ? Yes
                                        </label>
                                        <asp:CheckBox ID="chktaskallocatetoemployee" runat="server" AutoPostBack="true" OnCheckedChanged="chktaskallocatetoemployee_CheckedChanged" />
                                    </td>
                                </tr>--%>
                               <%-- <tr>
                                    <td colspan="6">
                                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Set Allocation date for tasks to
                                                        </label>
                                                        <asp:CheckBox ID="chkdeveloper" runat="server" Text="Developer" />
                                                        <asp:CheckBox ID="chktester" runat="server" Text="Tester" />
                                                        <asp:CheckBox ID="chksupervisor" runat="server" Text="Supervisor" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>--%>
                                <tr>
                                <td colspan="5">
                                 <label style="width:650px">
                                <asp:CheckBox ID="CheckBox4" runat="server" AutoPostBack="True" Text="Allocate This Work to Developer, Tester & Supervisor" OnCheckedChanged="chkupload_CheckedChangedITwork"  Checked="true">
                                </asp:CheckBox>
                                </label> 

                                  <label style="width:8px">
                                    <asp:TextBox ID="TextBox9" runat="server" Width="70px" OnTextChanged="txttargetdatedeve_TextChanged"   AutoPostBack="True" Visible="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="TextBox9" PopupButtonID="ImageButton13">
                                    </cc1:CalendarExtender>
                                </label>
                                <label style="width:2px">
                                    <asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="~/images/cal_actbtn.jpg" Visible="false" />
                                </label>
                                </td>
                                </tr>
                                <tr>
                                <td colspan="5">
                                   <asp:Panel ID="Panel6" runat="server" Visible="false">
                                        <table width="100%">
                                        <tr>
                                        <td>
                                              <label style="color:Black;width:100%" runat="server" visible="false">
                                    Allocating this work to the specified Developer, Tester, and/or Supervisor will exceed their available hours. 
                                    <br />
                                    Do you wish to select a different date on which to allocate this work to the indicated Developer, Tester and Supervisor?

                                    </label>       
                                        </td>
                                        </tr>
                                        <tr>
                                        <td>
                                        <label style="width:70px;">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="txttargetdatedeve_TextChangedRBL" AutoPostBack="true"  RepeatDirection="Horizontal" Visible="false">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </label> 
                                         <label style="width:80px">
                                    <asp:TextBox ID="TextBox10" runat="server" Width="70px" OnTextChanged="txttargetdatedeve_TextChanged2"   AutoPostBack="True" Visible="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender11" runat="server" TargetControlID="TextBox10" PopupButtonID="ImageButton14">
                                    </cc1:CalendarExtender>
                                </label>
                                <label style="width:20px">
                                    <asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="~/images/cal_actbtn.jpg" Visible="false" />
                                </label>
                                 <label style="color:Red;width:100%">
                                      <asp:Label ID="lbltotalworkallocatedtoday" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                     <asp:Label ID="Lbl_notHour" runat="server" Text="Insufficient Time Available on this Day for Developer/Tester/Supervisor. Select Another Date" Visible="false" ></asp:Label>
                                </label> 
                                        </td>
                                        </tr>
                                        </table> 
                                   </asp:Panel>
                                  
                                </td>
                                </tr>

                                <tr>
                                    <td colspan="5">
                                            <asp:Panel ID="pbl_ITworkallocation" runat="server" Width="100%" Visible="true">

                                                             <table width="100%">
                           
                                <tr>
                                    <td>
                                        <label style="width:70px">
                                            <asp:Label ID="lbl_lbldev" runat="server" Text="Developer:"></asp:Label>
                                         
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:190px">
                                            <asp:DropDownList ID="DDL_devForITwork" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlempassdeve_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:180px">
                                            <asp:Label ID="lbl_workal" runat="server" Text="IT Work Allocation Date"></asp:Label>
                                            <asp:Label ID="Label65ssss" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_dateDevITwork"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:75px">
                                            <asp:TextBox ID="txt_dateDevITwork" runat="server" Width="65px"   OnTextChanged="txttargetdatedeve_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        </label>
                                        <label style="width:40px">
                                            <asp:ImageButton ID="img_devDateITwork" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                      
                                        <cc1:CalendarExtender ID="CalendarExtender15" runat="server" TargetControlID="txt_dateDevITwork" PopupButtonID="img_devDateITwork">
                                        </cc1:CalendarExtender>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:120px">
                                            <asp:Label ID="Label66" runat="server" Text=" Budgeted hour"></asp:Label>
                                            <asp:Label ID="Label67" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_devhourITwork"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_devhourITwork"
                                                ErrorMessage="Not Valid" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-0][0-9]|[0][9]):(([0-5][0-9]|[0][0]))$"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:70px">
                                            <asp:TextBox ID="txt_devhourITwork" runat="server" MaxLength="5" Width="70px" >03:00</asp:TextBox>
                                        </label>
                                        <label style="width:40px">
                                            <asp:Label ID="Label68" runat="server" ForeColor="Red">HH:MM</asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:100px">
                                            <asp:Label ID="Label69" runat="server" Visible="false">Rate per Hour </asp:Label>
                                            <asp:Label ID="Label70" runat="server" Text="0" Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:50px">
                                            <asp:Label ID="Label71" runat="server" Visible="false">Cost </asp:Label>
                                            <asp:Label ID="Label72" runat="server" Text="0" Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label style="width:70px">
                                            <asp:Label ID="Label73" runat="server" Text="Tester:"></asp:Label>
                                           </label>
                                    </td>
                                    <td>
                                        <label style="width:190px">
                                            <asp:DropDownList ID="ddl_testForITwork" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlempassdeve_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:180px">
                                            <asp:Label ID="Label74" runat="server" Text="IT Work Allocation Date"></asp:Label>
                                            <asp:Label ID="Label75" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="TextBox13"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:75px">
                                            <asp:TextBox ID="txt_dateTestITwork" runat="server" Width="65px"  OnTextChanged="txttargetdatedeve_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        </label>
                                        <label style="width:40px">
                                            <asp:ImageButton ID="img_DateITwork" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                       
                                        <cc1:CalendarExtender ID="CalendarExtender16" runat="server" TargetControlID="txt_dateTestITwork" PopupButtonID="img_DateITwork">
                                        </cc1:CalendarExtender>
                                         </label>
                                    </td>
                                    <td>
                                        <label style="width:110px">
                                            <asp:Label ID="Label76" runat="server" Text="Budgeted hour"></asp:Label>
                                            <asp:Label ID="Label77" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="TextBox14"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txt_BudhourITwork"
                                                ErrorMessage="Not Valid" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-0][0-8]|[0][9]):(([0-5][0-9]|[0][0]))$"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:70px">
                                            <asp:TextBox ID="txt_BudhourITwork" runat="server" Width="65px" MaxLength="5" >01:00</asp:TextBox>
                                        </label>
                                        <label style="width:50px">
                                            <asp:Label ID="Label78" runat="server" ForeColor="Red">HH:MM</asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:80px">
                                            <asp:Label ID="Label79" runat="server" Visible="false">Rate per Hour </asp:Label>
                                            <asp:Label ID="Label80" runat="server" Text="0" Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:50px">
                                            <asp:Label ID="Label81" runat="server" Visible="false">Cost </asp:Label>
                                            <asp:Label ID="Label82" runat="server" Text="0" Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label style="width:70px">
                                            <asp:Label ID="Label83" runat="server" Text="Supervisor:"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:190px">
                                            <asp:DropDownList ID="ddl_suppITWork" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlempassdeve_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:180px">
                                            <asp:Label ID="Label84" runat="server" Text="IT Work Allocation Date"></asp:Label>
                                            <asp:Label ID="Label85" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txt_DateSupITwork"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:75px">
                                            <asp:TextBox ID="txt_DateSupITwork" runat="server" Width="65px" OnTextChanged="txttargetdatedeve_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        </label>
                                        <label style="width:40px">
                                            <asp:ImageButton ID="img_dateSupITwork" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                      
                                        <cc1:CalendarExtender ID="CalendarExtender17" runat="server" TargetControlID="txt_DateSupITwork"
                                            PopupButtonID="img_dateSupITwork">
                                        </cc1:CalendarExtender>
                                          </label>
                                    </td>
                                    <td>
                                        <label style="width:110px">
                                            <asp:Label ID="Label86" runat="server" Text="Budgeted hour"></asp:Label>
                                            <asp:Label ID="Label87" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="TextBox16"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txt_SuphourITwork"
                                                ErrorMessage="Not Valid" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-0][0-8]|[0][9]):(([0-5][0-9]|[0][0]))$"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:70px">
                                            <asp:TextBox ID="txt_SuphourITwork" runat="server" Width="70px"   MaxLength="5" >01:00</asp:TextBox>
                                        </label>
                                        <label style="width:40px">
                                            <asp:Label ID="Label88" runat="server" ForeColor="Red">HH:MM</asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:100px">
                                            <asp:Label ID="Label24mSddd" runat="server" Visible="false">Rate per Hour </asp:Label>
                                            <asp:Label ID="lblratesupervisormSddd" runat="server" Text="0" Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:50px">
                                            <asp:Label ID="Label59mddddddd" runat="server" Visible="false">Cost </asp:Label>
                                            <asp:Label ID="lblsupervisorcostmSdddddd" runat="server" Text="0" Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                               
                                </table>    
                                            </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td colspan="4">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btnSubmit" OnClick="btnSubmit_Click"  Text="Submit" ValidationGroup="1" />
                                        <asp:Button ID="Button1" CssClass="btnSubmit" runat="server" OnClick="Button1_Click" Text="Cancel" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="sads" runat="server" Text="List of Task Status"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Text="Printable Version"  OnClick="Button1_Click1" />
                        <input id="Button6" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td >
                                <label style="width:120px">
                                    <asp:Label ID="Label41" runat="server" Text="Product Name"></asp:Label>
                                    <asp:Label ID="Label613" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                </label>
                            </td>
                            <td colspan="5">
                                <label style="width:630px">
                                    <asp:DropDownList ID="ddlfilterwebsite" runat="server" Width="600px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlfilterwebsite_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label style="width:350px">
                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="Show Only Active Products/Versions" OnCheckedChanged="chkupload_CheckedChanged1" Checked="true">
                                </asp:CheckBox>
                                </label> 
                            </td>
                        </tr>

                        <tr>
                    <td>
                   <label style="width:100px">
                                <asp:Label ID="Label16N" runat="server" Text="Search"></asp:Label>
                            </label>
                    </td>
                    <td colspan="5">
                    <label style="width:255px;">
                    <asp:TextBox ID="TextBox7" runat="server"   placeholder="Search"  Font-Bold="true"  Width="250px"></asp:TextBox>
                    
                    </label> 
                    <label>
                    <asp:Button ID="Button7" CssClass="btnSubmit" runat="server" Text="Search" OnClick="Button2_ClickSearch"  />
                    </label> 
                    </td>
                    </tr>

                        <tr>
                            <td>
                               <label style="width:100px">
                                    <asp:Label ID="Label36" runat="server" Text="Page Name"></asp:Label>
                                </label>
                               
                            </td>
                            <td colspan="5">
                                <label style="width:255px">
                                    <asp:DropDownList ID="ddlpagenamefilter" runat="server" Width="255px" AutoPostBack="True" OnSelectedIndexChanged="ddlpagenamefilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                 <label style="width:400px">
                                <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Text="Show_Only_Active_Pages_with_Pending_Versions" OnCheckedChanged="chkupload_CheckedChanged12"  >
                                </asp:CheckBox>
                                </label> 
                                 <label style="width:290px">
                                <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="True" Text="Show_All_Active_Pages" OnCheckedChanged="chkupload_CheckedChanged123" Width="290px" Checked="true" Enabled="false">
                                </asp:CheckBox>
                                </label> 
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                <label style="width:60px">
                                    <asp:Label ID="Label42" runat="server" Text=" Developer"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlfilterdeveloper" runat="server">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                               
                                <label style="width:105px;">
                                <asp:Label ID="Label43" runat="server" Text="Start Date"></asp:Label>
                                    <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="TextBox1"
                                        PopupButtonID="ImageButton4">
                                    </cc1:CalendarExtender>
                                </label>
                                <label style="width:20px;">
                                <br />
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                </label>
                            </td>
                            <td>
                                <label style="width:105px;">
                                <asp:Label ID="Label44" runat="server" Text="End Date "></asp:Label>
                                    <asp:TextBox ID="TextBox2" runat="server" Width="100px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TextBox2"
                                        PopupButtonID="ImageButton5">
                                    </cc1:CalendarExtender>
                                </label>
                                <label style="width:20px;">
                                <br />
                                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                </label>
                            </td>
                            <td>
                                  
                                 <label>
                                  <asp:Label ID="Label45" runat="server" Text=" Status"></asp:Label>
                                    <asp:DropDownList ID="ddlAct1" runat="server" Width="80px" CausesValidation="True">
                                        <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Complete" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <label style="width:60px">
                                    <asp:Label ID="Label46" runat="server" Text="Tester"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlfiltertester" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                 
                                <label style="width:105px;">
                                  <asp:Label ID="Label47" runat="server" Text="Start Date "></asp:Label>
                                    <asp:TextBox ID="TextBox3" runat="server" Width="100px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="TextBox3"
                                        PopupButtonID="ImageButton6">
                                    </cc1:CalendarExtender>
                                </label>
                                <label style="width:20px;">
                                <br />
                                    <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                </label>
                            </td>
                            <td>
                                 
                                <label style="width:105px;">
                                  <asp:Label ID="Label48" runat="server" Text="End Date"></asp:Label>
                                    <asp:TextBox ID="TextBox4" runat="server" Width="100px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="TextBox4"
                                        PopupButtonID="ImageButton7">
                                    </cc1:CalendarExtender>
                                </label>
                                <label style="width:20px;">
                                    <br />
                                    <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                </label>
                            </td>
                            <td>
                                 
                                  
                                
                                 <label>
                                   <asp:Label ID="Label49" runat="server" Text="Status"></asp:Label>
                                    <asp:DropDownList ID="ddlAct2" runat="server" Width="80px" CausesValidation="True">
                                        <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Complete" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <label style="width:60px">
                                    <asp:Label ID="Label50" runat="server" Text=" Supervisor "></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlfiltersupervisor" runat="server">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                 
                                <label style="width:105px;">
                                 <asp:Label ID="Label51" runat="server" Text="Start Date"></asp:Label>
                                    <asp:TextBox ID="TextBox5" runat="server" Width="100px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="TextBox5"
                                        PopupButtonID="ImageButton8">
                                    </cc1:CalendarExtender>
                                </label>
                                <label style="width:20px;">
                                <br />
                                    <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                </label>
                            </td>
                            <td>
                               
                                <label style="width:105px;">
                                 <asp:Label ID="Label52" runat="server" Text="End Date"></asp:Label>
                                    <asp:TextBox ID="TextBox6" runat="server" Width="100px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="TextBox6"
                                        PopupButtonID="ImageButton9">
                                    </cc1:CalendarExtender>
                                </label>
                                <label style="width:20px;">
                                <br />
                                    <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                </label>
                            </td>
                            <td>
                                
                                  <label>
                                 <asp:Label ID="Label53" runat="server" Text=" Status"></asp:Label>
                                    <asp:DropDownList ID="ddlAct3" runat="server" Width="80px" CausesValidation="True">
                                        <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Complete" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                              
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="4">
                                <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Go" OnClick="Button3_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                          <asp:Panel ID="Panel3" runat="server" Visible="false">
                        <tr>
                            <td>
                                <label>
                                    Show Pending Versions
                                    <asp:CheckBox ID="chkver" runat="server" OnCheckedChanged="chkver_CheckedChanged"
                                        AutoPostBack="true" />
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="Drpver" runat="server" Width="600px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        </asp:Panel>

                    </table>
                    <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="None">
                        <table width="100%">
                            <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label58" runat="server" Font-Italic="true" Text="List of Task Status"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <asp:GridView ID="GridView1" Width="100%" runat="server" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
                                     PageSize="50" AllowPaging="True"   OnRowDataBound="GridView2_RowDataBound"   AutoGenerateColumns="False" EmptyDataText="No Record Found." OnSorting="GridView1_Sorting"
                                        AllowSorting="True" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"  OnPageIndexChanging="GridView1_PageIndexChanging"
                                        AlternatingRowStyle-CssClass="alt" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product  Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmasterId" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblgrdproductname" runat="server" Text='<%#Bind("ProductName") %>'></asp:Label>

                                                     <asp:Label ID="Label61" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Page  Name" SortExpression="PageName" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpagename123" runat="server" Text='<%#Bind("PageName") %>' Visible="false"></asp:Label>
                                                     <asp:LinkButton ID="linkdow1dailywork" runat="server" Text='<%#Bind("PageName") %>' Font-Size="12px" 
                                                         ForeColor="#b9b9b9"     CommandName="Linkpage" CommandArgument='<%# Eval("pageid") %>'           ></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ver <br/> No" SortExpression="VersionNo" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblversionname123" runat="server" Text='<%#Bind("VersionNo") %>' Visible="false" ></asp:Label>
                                                     <asp:LinkButton ID="linkdow1dailyversion" runat="server" Text='<%#Bind("VersionNo") %>' Font-Size="12px" 
                                                         ForeColor="#b9b9b9"     CommandName="LinkpageVersion1" CommandArgument='<%# Eval("id") %>'  ></asp:LinkButton>
                                                        
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Dev" SortExpression="BudgetedHourDevelopment"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_devName" runat="server" Text='<%#Bind("Dev_name") %>' ></asp:Label>
                                                </ItemTemplate>
                                         </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Dev Status">
                                          <ItemTemplate>                            
                                            <asp:Label ID="lblDevStatus" runat="server" Text='<%#Bind("DeveloperOK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bud Hr <br/> Dev" SortExpression="BudgetedHourDevelopment"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbudgethrdeveloper123" runat="server" Text='<%#Bind("BudgetedHourDevelopment") %>'></asp:Label>
                                                      <asp:Label ID="lbl_PageWorkTblId" runat="server" Text='<%#Bind("id") %>' Visible="false"></asp:Label>
                                                      <asp:Label ID="lbl_devEmpID" runat="server" Text='<%#Bind("EpmloyeeID_AssignedDeveloper") %>' Visible="false"></asp:Label>
                                                       <asp:Label ID="lbl_TestEmpID" runat="server" Text='<%#Bind("EpmloyeeID_AssignedTester") %>' Visible="false"></asp:Label>
                                                       <asp:Label ID="lbl_SupEmpID" runat="server" Text='<%#Bind("EpmloyeeID_AssignedSupervisor") %>' Visible="false"></asp:Label>
                                                      
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Act Hr <br/> Dev" SortExpression="BudgetedHourDevelopment" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="ActHourDev" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                         </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Tester" SortExpression="BudgetedHourDevelopment" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_TestName" runat="server" Text='<%#Bind("Test_name") %>'  ></asp:Label>
                                                </ItemTemplate>
                                         </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Tester Status">
                                          <ItemTemplate>                            
                                            <asp:Label ID="lblTesterStatus" runat="server" Text='<%#Bind("TesterOk") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bud Hr <br/> Test" SortExpression="BudgetedHourTesting" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbudgethrtester123" runat="server" Text='<%#Bind("BudgetedHourTesting") %>'></asp:Label>
                                                </ItemTemplate>
                                         </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Act Hr <br/> Test" SortExpression="BudgetedHourTesting"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="ActHourTsting" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                         </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Sup" SortExpression="BudgetedHourDevelopment"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_supName" runat="server" Text='<%#Bind("Sup_name") %>' ></asp:Label>
                                                </ItemTemplate>
                                         </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sup Status">
                                          <ItemTemplate>                            
                                            <asp:Label ID="lblSupStatus" runat="server"  Text='<%#Bind("SupervisorOk") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Target Date<br/> Sup" SortExpression="TargetDateSuperviserApproval" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbudgethrtester12fdfff3ddd" runat="server" Text='<%#Bind("TargetDateSuperviserApproval") %>' DataFormatString="{0:d}"></asp:Label>
                                                </ItemTemplate>
                                         </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Bud Hr <br/> Sup" SortExpression="BudgetedHourTesting"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbudgethrtester12fdfff3" runat="server" Text='<%#Bind("BudgetedHourSupervisorChecking") %>'></asp:Label>
                                                </ItemTemplate>
                                         </asp:TemplateField>
                                        

                                         
                                          <asp:TemplateField HeaderText="Act Hr <br/> Sup" SortExpression="BudgetedHourTesting"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="ActHourSup" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                         </asp:TemplateField>
                                            <asp:CommandField HeaderText="Edit" EditImageUrl="~/Account/images/edit.gif" ButtonType="Image" Visible="false"   ShowEditButton="True" ValidationGroup="2" HeaderImageUrl="~/Account/images/edit.gif" />
                                                 <asp:ButtonField CommandName="Edit" HeaderImageUrl="~/Account/images/edit.gif" ButtonType="Image" ImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" Text="Edit" HeaderText="Edit" ValidationGroup="2">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:ButtonField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" ToolTip="Delete" runat="server" CommandName="Delete"
                                                        ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                        CommandArgument='<%# Eval("Id") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Next Work" SortExpression="BudgetedHourTesting" HeaderStyle-HorizontalAlign="Left" Visible="true" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Date_NextWork" runat="server" Visible="false" DataFormatString = "{0:d}"></asp:Label>
                                                </ItemTemplate>
                                         </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allocate Now" HeaderImageUrl="~/Account/images/UpdateGrid.jpg" HeaderStyle-HorizontalAlign="Left" Visible="true" 
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imageBTN_IWork" ToolTip="Update" runat="server" CommandName="ITWork" Visible="true" 
                                                        ImageUrl="~/Account/images/UpdateGrid.jpg" CommandArgument='<%# Eval("Id") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                            </asp:TemplateField>

                                            





                                         <asp:TemplateField HeaderText="Website_Name" SortExpression="WebsiteName" HeaderStyle-HorizontalAlign="Left"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwebsitename123" runat="server" Text='<%#Bind("WebsiteName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Title" HeaderStyle-HorizontalAlign="Left" SortExpression="WorkRequirementTitle" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsupervisorname132" runat="server" Text='<%#Bind("WorkRequirementTitle") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         </Columns>
                                         </asp:GridView>
                                           </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                               
                                <asp:Panel ID="Panel9" runat="server" BackColor="White" BorderColor="Gray" Width="90%"   BorderStyle="Solid" BorderWidth="5px" Height="400px" ScrollBars="Both">
                            
                                 <fieldset>
                            <legend>Select Developer, Tester and Supervisor for this task allocation
                                
                            </legend>
                            <table width="100%">
                             <tr>
                                        <td align="right" colspan="8">
                                            <asp:ImageButton ID="ImageButton16" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                                Width="16px" />
                                        </td>
                                    </tr>

                                    <tr>
                                    <td  colspan="8">
                                        <asp:Label ID="lbl_message" runat="server" 
                                            Text="Insufficient Time Available on this Day for Developer/Tester/Supervisor. Select Another Date" 
                                            Visible="false" style="color: #FF0000" ></asp:Label>
                                    </td>
                                    </tr>
                                <tr>
                                    <td>
                                        <label style="width:70px">
                                            <asp:Label ID="Label6nvb3S" runat="server" Text="Developer:"></asp:Label>
                                         
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:190px">
                                            <asp:DropDownList ID="DropDownList1" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:180px">
                                            <asp:Label ID="Label6nvbn4s" runat="server" Text="IT Work Allocation Date"></asp:Label>
                                            <asp:Label ID="Labelvbvb65s" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1S" runat="server" ControlToValidate="TextBox11S"
                                                ErrorMessage="*" ValidationGroup="1S"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:75px">
                                            <asp:TextBox ID="TextBox11S" runat="server" Width="65px" ></asp:TextBox>
                                            <%--OnTextChanged="txttargetdatedeve_TextChanged2Popup" AutoPostBack="True"--%>
                                        </label>
                                        <label style="width:40px">
                                            <asp:ImageButton ID="ImageButton18S" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                      
                                        <cc1:CalendarExtender ID="CalendarExtender12" runat="server" TargetControlID="TextBox11S"  PopupButtonID="ImageButton18S">
                                        </cc1:CalendarExtender>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:120px">
                                            <asp:Label ID="Label66S" runat="server" Text=" Budgeted hour"></asp:Label>
                                            <asp:Label ID="Label67S" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2S" runat="server" ControlToValidate="TextBox12"
                                                ErrorMessage="*" ValidationGroup="1S"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4S" runat="server" ControlToValidate="TextBox12"
                                                ErrorMessage="Not Valid" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-0][0-8]|[0][9]):(([0-5][0-9]|[0][0]))$"
                                                ValidationGroup="1S"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:70px">
                                            <asp:TextBox ID="TextBox12" runat="server" MaxLength="5" Width="70px"  >03:00</asp:TextBox>
                                          <%--AutoPostBack="True"  OnTextChanged="txtbudgethourdev_TextChangedTextBox12"--%>
                                        </label>
                                        <label style="width:40px">
                                            <asp:Label ID="Label68S" runat="server" ForeColor="Red">HH:MM</asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:100px">
                                            <asp:Label ID="Label69S" runat="server" Visible="false">Rate per Hour </asp:Label>
                                            <asp:Label ID="Label70S" runat="server" Text="0" Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:50px">
                                            <asp:Label ID="Label71S" runat="server" Visible="false">Cost </asp:Label>
                                            <asp:Label ID="Label72S" runat="server" Text="0" Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label style="width:70px">
                                            <asp:Label ID="Label73S" runat="server" Text="Tester:"></asp:Label>
                                           </label>
                                    </td>
                                    <td>
                                        <label style="width:190px">
                                            <asp:DropDownList ID="DropDownList2" runat="server" >
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:180px">
                                            <asp:Label ID="Label74S" runat="server" Text="IT Work Allocation Date"></asp:Label>
                                            <asp:Label ID="Label75S" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox13"
                                                ErrorMessage="*" ValidationGroup="1S"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:75px">
                                            <asp:TextBox ID="TextBox13" runat="server" Width="65px" ></asp:TextBox>
                                            <%--OnTextChanged="txttargetdatedeve_TextChanged2Popup" AutoPostBack="True"--%>
                                        </label>
                                        <label style="width:40px">
                                            <asp:ImageButton ID="ImageButton19" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                       
                                        <cc1:CalendarExtender ID="CalendarExtender13" runat="server" TargetControlID="TextBox13"
                                            PopupButtonID="ImageButton19">
                                        </cc1:CalendarExtender>
                                         </label>
                                    </td>
                                    <td>
                                        <label style="width:110px">
                                            <asp:Label ID="Label76S" runat="server" Text=" Budgeted hour"></asp:Label>
                                            <asp:Label ID="Label77S" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox14"
                                                ErrorMessage="*" ValidationGroup="1S"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="TextBox14"
                                                ErrorMessage="Not Valid" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-0][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                                ValidationGroup="1S"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:70px">
                                            <asp:TextBox ID="TextBox14" runat="server" Width="65px" MaxLength="5" >01:00</asp:TextBox>
                                            <%--AutoPostBack="True" OnTextChanged="txtbudgethourdev_TextChangedTextBox14"--%>
                                        </label>
                                        <label style="width:50px">
                                            <asp:Label ID="Label78S" runat="server" ForeColor="Red">HH:MM</asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:80px">
                                            <asp:Label ID="Label79S" runat="server" Visible="false">Rate per Hour </asp:Label>
                                            <asp:Label ID="Label80S" runat="server" Text="0" Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:50px">
                                            <asp:Label ID="Label81S" runat="server" Visible="false">Cost </asp:Label>
                                            <asp:Label ID="Label82S" runat="server" Text="0" Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label style="width:70px">
                                            <asp:Label ID="Label83S" runat="server" Text="Supervisor:"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:190px">
                                            <asp:DropDownList ID="DropDownList3" runat="server" >
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:180px">
                                            <asp:Label ID="Label84S" runat="server" Text="IT Work Allocation Date"></asp:Label>
                                            <asp:Label ID="Label85S" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBox15"
                                                ErrorMessage="*" ValidationGroup="1S"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:75px">
                                            <asp:TextBox ID="TextBox15" runat="server" Width="65px" ></asp:TextBox>
                                            <%--OnTextChanged="txttargetdatedeve_TextChanged2Popup" AutoPostBack="True"--%>
                                        </label>
                                        <label style="width:40px">
                                            <asp:ImageButton ID="ImageButton20" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                      
                                        <cc1:CalendarExtender ID="CalendarExtender14" runat="server" TargetControlID="TextBox15"
                                            PopupButtonID="ImageButton20">
                                        </cc1:CalendarExtender>
                                          </label>
                                    </td>
                                    <td>
                                        <label style="width:110px">
                                            <asp:Label ID="Label86S" runat="server" Text="Budgeted hour"></asp:Label>
                                            <asp:Label ID="Label87S" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="TextBox16"
                                                ErrorMessage="*" ValidationGroup="1S"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TextBox16"
                                                ErrorMessage="Not Valid" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-0][0-8]|[0][9]):(([0-5][0-9]|[0][0]))$"
                                                ValidationGroup="1S"></asp:RegularExpressionValidator>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:70px">
                                            <asp:TextBox ID="TextBox16" runat="server" Width="70px"   MaxLength="5" >01:00</asp:TextBox>
                                            <%--AutoPostBack="True" OnTextChanged="txtbudgethourdev_TextChangedTextBox16"--%>
                                        </label>
                                        <label style="width:40px">
                                            <asp:Label ID="lblmsg2m" runat="server" ForeColor="Red">HH:MM</asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:100px">
                                            <asp:Label ID="Label24mS" runat="server" Visible="false">Rate per Hour </asp:Label>
                                            <asp:Label ID="lblratesupervisormS" runat="server" Text="0" Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label style="width:50px">
                                            <asp:Label ID="Label59m" runat="server" Visible="false">Cost </asp:Label>
                                            <asp:Label ID="lblsupervisorcostmS" runat="server" Text="0" Visible="false"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                <td>
                                <asp:Button ID="Button12S" runat="server" CssClass="btnSubmit" OnClick="btnSubmit_ClickIT"  Text="Submit" ValidationGroup="1S" />
                                </td>
                                </tr>
                                </table>    
                               </fieldset> 
                               </asp:Panel>
                                <asp:Button ID="Button11" runat="server" Style="display: none" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                PopupControlID="Panel9" TargetControlID="Button11" CancelControlID="ImageButton16">
                            </cc1:ModalPopupExtender>

                            <asp:Panel ID="Panel8" runat="server" BackColor="White" BorderColor="Gray" Width="60%"   BorderStyle="Solid" BorderWidth="5px" Height="470px" ScrollBars="Both">
                             <table style="width: 100%">
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:ImageButton ID="ImageButton15" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                      <tr>
                            <td colspan="2">
                                <span lang="en-us"></span>
                            </td>
                        </tr>
                            <tr>
                            <td colspan="2">
                                            <asp:GridView ID="GridView3" runat="server" EmptyDataText="No file found." AutoGenerateColumns="False"
                                                                    DataKeyNames="pageId" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                    Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Page Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbltitledailywork" runat="server" Text='<%#Bind("Pagename") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Functionality Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblpdfurldailywork" runat="server" Text='<%#Bind("FunctionalityTitle") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Page Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblaudiourldailywork" runat="server" Text='<%#Bind("PageTitle") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    
                                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                               <asp:LinkButton ID="linkdow1dailywork" runat="server" Text="Select" OnClick="linkdow1dailywork_Click" ForeColor="Black"></asp:LinkButton>
                                                                                    
                                                                             <%--   <asp:UpdatePanel ID="UpdatePanel1grd" runat="server">
                                                                                    <ContentTemplate>
                                                                                     
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                       
                                                                                        <asp:PostBackTrigger ControlID="linkdow1dailywork_Click" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>--%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        
                                                                    </Columns>
                                                                </asp:GridView>
                            </td>
                        </tr>
                                    </table>

                        </asp:Panel>
                        <asp:Button ID="Button10" runat="server" Style="display: none" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                PopupControlID="Panel8" TargetControlID="Button10" CancelControlID="ImageButton15">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="Gray" Width="60%"   BorderStyle="Solid" BorderWidth="5px" Height="470px" ScrollBars="Both">

                                <table style="width: 100%">
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:ImageButton ID="ImageButton12" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                                Width="16px" />
                                        </td>
                                    </tr>


                                     <tr>
                            <td colspan="2">
                                <span lang="en-us">Page Work Detail</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="Label62" runat="server" ForeColor="Red" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Website Name<span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblwebsitenamedetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Page Title<span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblpagenamedetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Version no<span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblnewpageversion" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                File Name<span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblnewpagedetaildetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Work Title<span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblworktitledetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000; height: 22px;">
                                Work Description<span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000; height: 22px;">
                                <asp:Label ID="lblworkdescriptiondetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                       <%-- <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Target Date <span lang="en-us">:</span>
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lbltargatedatedetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Budgedet Hour:
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblbudgetedhourdetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                                Actual Hour:
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                <asp:Label ID="lblactualhourdetail" runat="server"></asp:Label>
                            </td>
                        </tr>--%>
                                    <tr>
                                        <td>
                                         <asp:Panel ID="Panel7" runat="server"  HorizontalAlign="Center">
                                            <fieldset>
                                                <legend>Select Page </legend>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                           
                                                                

                                                                <asp:GridView ID="GridView2" runat="server" EmptyDataText="No file found." AutoGenerateColumns="False"
                                                                    DataKeyNames="pageId" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                    Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Page Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbltitledailywork" runat="server" Text='<%#Bind("Pagename") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                     
                                                                        <asp:TemplateField HeaderText="Page Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblaudiourldailywork" runat="server" Text='<%#Bind("PageTitle") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    
                                                                      
                                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                               <asp:LinkButton ID="linkdow1dailywork" runat="server" Text="Select" OnClick="linkdow1dailywork_Clickadd" ForeColor="Black"></asp:LinkButton>
                                                                                    
                                                                              
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                    </Columns>
                                                                </asp:GridView>
                                                           


                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                             </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td colspan="2">
                                            <asp:Panel ID="Panel4" runat="server" ScrollBars="Both" HorizontalAlign="Center" Visible="false"   Height="330px">
                                             <fieldset>
                                                <legend>  Source Code Files : </legend>
                                                 <asp:Label ID="lblftpurl123" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblftpport123" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblftpuserid" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblftppassword123" runat="server" Visible="False"></asp:Label>
                                 <asp:Label ID="lbl_versionuser" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lbl_versionpass" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblpaged" runat="server" ForeColor="Red" Font-Size="12px"></asp:Label>
                                                <asp:GridView ID="grdsourcefile" runat="server" EmptyDataText="No file found." AutoGenerateColumns="False"
                                                    DataKeyNames="Id" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="prg"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Page Title">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblspagetitle" runat="server" Text='<%#Bind("PageTitle") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Version No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblviesionno" runat="server" Text='<%#Bind("VersionNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FileName" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfilename" runat="server" Text='<%#Bind("PageName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FileName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfilenam" runat="server" Text='<%#Bind("PName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="250px" />
                                                            <HeaderStyle Width="250px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldates" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkdow" Style="color: #717171;" runat="server" Text="Download"
                                                                    OnClick="linkdow_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="GridPager" />
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                                                    <RowStyle CssClass="GridRowStyle" />
                                                    <FooterStyle CssClass="GridFooter" />
                                                </asp:GridView>
                                                </fieldset>
                                            </asp:Panel>
                                    </td>
                                    </tr>
                                    <tr>
                            <td style="width: 30%; font-size: 12px; color: #000000;">
                               
                            </td>
                            <td style="width: 70%; font-size: 12px; color: #000000;">
                                &nbsp;
                            </td>
                        </tr>
                                    <tr>
                            <td style="width: 100%; font-size: 12px; color: #000000;" colspan="2" align="center">
                             <fieldset>
                             <legend>   Intruction File : </legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel5" runat="server" ScrollBars="Both" HorizontalAlign="Center"
                                                Height="130px">
                                                <asp:GridView ID="GridView4" runat="server" EmptyDataText="No file found." AutoGenerateColumns="False"
                                                    DataKeyNames="Id" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="prg"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Title">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltitle" runat="server" Text='<%#Bind("FileTitle") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PDF URL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpdfurl" runat="server" Text='<%#Bind("WorkRequirementPdfFilename") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Audio URL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblaudiourl" runat="server" Text='<%#Bind("WorkRequirementAudioFileName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldate123" runat="server" Text='<%#Bind("Date" ,"{0:MM/dd/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkdow1" Style="color: #717171;" runat="server" Text="Download"
                                                                    OnClick="linkdow1_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="GridPager" />
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                                                    <RowStyle CssClass="GridRowStyle" />
                                                    <FooterStyle CssClass="GridFooter" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                </fieldset>
                            </td>
                        </tr>
                                    
                                </table>
                            </asp:Panel>
                            <asp:Button ID="Button8" runat="server" Style="display: none" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                PopupControlID="Panel2" TargetControlID="Button8" CancelControlID="ImageButton5">
                            </cc1:ModalPopupExtender>
                            <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    <asp:Label ID="lblvername" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <%--IT WORK ALLOCATION-----------------------------------------------------------------------------------------------------------------%>
                             
                        </table>
                    </asp:Panel>
                </fieldset>
                <input id="PageId" name="PageId" runat="server" type="hidden" style="width: 1px" />
                <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button2" />
        </Triggers>
    </asp:UpdatePanel>
      <div style="position: fixed;bottom: 0; right:20px;">
              <asp:Label ID="lblVersion" runat="server" Text="V5" ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>
</asp:Content>
