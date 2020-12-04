<%@ Page Language="C#" MasterPageFile="~/Party/Master/Party_Admin.master" AutoEventWireup="true"
    CodeFile="PartyAfterLogin.aspx.cs" Inherits="PartyAfterLogin" Title="Untitled Page" %>

<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

     
         function mask(evt)
        { 

           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186 ||evt.keyCode==59  )
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }
             
             
               
            
        }  
        function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered an invalid character");
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
         function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

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

    <script language="javascript" type="text/javascript">
        function CallPrint1(strid) {
            var prtContent = document.getElementById('<%= pnlpt.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }    
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }  
    </script>

    <script language="javascript" type="text/javascript">


      function itemUpdate()
      {
      
        var amt=document.getElementById("amount");
        var t= document.getElementById("ctl00_ContentPlaceHolder1_txtAmount");
       amt.value=t.value; 
       }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <table>
                        <tr>
                            <td style="width: 50%">
                                <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Document Panel"
                                    OnClick="Button3_Click" />
                                <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Text="General Ledger"
                                    OnClick="Button4_Click" />
                                <asp:Button ID="Button7" runat="server" CssClass="btnSubmit" Text="Message Panel"
                                    OnClick="Button7_Click" />
                            </td>
                            <td style="width: 50%">
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Business Name"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlbusiness" Visible="false" runat="server" Enabled="false">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="lblbusinessname" runat="server" Text="Business Name"></asp:Label>
                                </label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="width: 50%">
                                        <fieldset>
                                            <legend>Document Search </legend>
                                            <asp:Panel ID="Panel2" runat="server" Height="400px" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label1date" runat="server" Text="Search "></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="txtsearch" Width="150px" runat="server" MaxLength="30"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                Filter by
                                                            </label>
                                                            <label>
                                                                <asp:DropDownList ID="ddlDuration" runat="server">
                                                                    <asp:ListItem>Today</asp:ListItem>
                                                                    <asp:ListItem>Yesterday</asp:ListItem>
                                                                    <asp:ListItem>This Week</asp:ListItem>
                                                                    <asp:ListItem Selected="True">This Month</asp:ListItem>
                                                                    <asp:ListItem>This Quarter</asp:ListItem>
                                                                    <asp:ListItem>This Year</asp:ListItem>
                                                                    <asp:ListItem>Last Week</asp:ListItem>
                                                                    <asp:ListItem>Last Month</asp:ListItem>
                                                                    <asp:ListItem>Last Quarter</asp:ListItem>
                                                                    <asp:ListItem>Last Year</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </label>
                                                            <label>
                                                                <asp:Button ID="Button5" runat="server" Text="Go" OnClick="Button5_Click" />
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical" Height="250px" Width="100%">
                                                                <asp:GridView ID="gridocapproval" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                    AllowPaging="true" AllowSorting="true" PageSize="10" AlternatingRowStyle-CssClass="alt"
                                                                    AutoGenerateColumns="False" EmptyDataText="No Record Found." Width="100%" OnSorting="gridocapproval_Sorting"
                                                                    OnPageIndexChanging="gridocapproval_PageIndexChanging">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="DocumentId"
                                                                            HeaderText="ID" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <a id="docviewmasterid" href='ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>'
                                                                                    target="_blank">
                                                                                    <asp:Label ID="lbldocid" runat="server" ForeColor="#426172" Text='<%#Bind("DocumentId")%>'></asp:Label></a></ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="DocumentDate"
                                                                            HeaderText="Document Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldocdatesearch" runat="server" Text='<%#Bind("DocumentDate","{0:MM-dd-yyyy}")%>'></asp:Label></ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="DocumentType"
                                                                            HeaderText="Folder" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldocumenttype" runat="server" Text='<%#Bind("DocumentType")%>'></asp:Label></ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="DocumentTitle"
                                                                            HeaderText="Title" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="titillabel" runat="server" Text='<%#Bind("DocumentTitle")%>'></asp:Label></ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="PartyName"
                                                                            HeaderText="Party" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldocpartyname" runat="server" Text='<%#Bind("PartyName")%>'></asp:Label></ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <input id="hdnsortExpGrdsearch" runat="Server" name="hdnsortExpGrdsearch" style="width: 1px"
                                                                    type="hidden" />
                                                                <input id="hdnsortDirGrdsearch" runat="Server" name="hdnsortDirGrdsearch" style="width: 1px"
                                                                    type="hidden" />
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </fieldset>
                                    </td>
                                    <td style="width: 50%">
                                        <fieldset>
                                            <legend>Upload Documents </legend>
                                            <asp:Panel ID="Panel16" runat="server" Height="400px" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Visible="false" AutoPostBack="True"
                                                                OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Value="0">User Defined</asp:ListItem>
                                                                <asp:ListItem Value="1">Auto Process</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel4" runat="server" Height="350px" Width="100%">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label1" runat="server" Text="Document Type"></asp:Label>
                                                                                <asp:Label ID="Label6" runat="server" Text="*"></asp:Label>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDocType"
                                                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:DropDownList ID="ddlDocType" runat="server" ValidationGroup="1">
                                                                                </asp:DropDownList>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label7" runat="server" Text="Document Title"></asp:Label>
                                                                                <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                                    ControlToValidate="txtdoctitle" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:TextBox ID="txtdoctitle" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'div1',30)"
                                                                                    runat="server" ValidationGroup="1" MaxLength="30" TabIndex="2"></asp:TextBox>
                                                                            </label>
                                                                            <label>
                                                                                <asp:Label ID="Label17" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                                <span id="div1" class="labelcount">30</span>
                                                                                <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label9" runat="server" Text="User Name"></asp:Label>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:DropDownList ID="ddlpartyname" runat="server" Enabled="false" ValidationGroup="1"
                                                                                    TabIndex="3">
                                                                                </asp:DropDownList>
                                                                            </label>
                                                                            <label>
                                                                                <asp:Label ID="Label11" runat="server" Text="Document Date"></asp:Label>
                                                                            </label>
                                                                            <label>
                                                                                <asp:TextBox ID="TxtDocDate" runat="server" Width="70px" TabIndex="4"></asp:TextBox>
                                                                                <cc1:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="TxtDocDate">
                                                                                </cc1:CalendarExtender>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                    <asp:Panel ID="Panel11" runat="server" Visible="false" Width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:Label ID="Label12" runat="server" Text="Ref. Number"></asp:Label>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                                        ControlToValidate="txtdocrefnmbr" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:TextBox ID="txtdocrefnmbr" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'sp11',100)"
                                                                                        MaxLength="100" ValidationGroup="1" TabIndex="5"></asp:TextBox>
                                                                                </label>
                                                                                <label>
                                                                                    <asp:Label ID="Label13" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                                    <span id="sp11" class="labelcount">100</span>
                                                                                    <asp:Label ID="Label18" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:Label ID="Label14" runat="server" Text="Net Amount"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    <asp:TextBox ID="txtnetamount" runat="server" MaxLength="10" onKeydown="return mak('Span1',10,this)"
                                                                                        onkeypress="return RealNumWithDecimal(this,event,2);" Width="180px" TabIndex="6"></asp:TextBox>
                                                                                </label>
                                                                                <label>
                                                                                    <asp:Label ID="Label15" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                                    <span id="Span1" class="labelcount">10</span>
                                                                                    <asp:Label ID="Label19" runat="server" CssClass="labelcount" Text="(0-9 .)"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label16" runat="server" Text="Add Document"></asp:Label>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="FileUpload1"
                                                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="7" />
                                                                            <asp:Button ID="imgbtnAdd" runat="server" OnClick="imgbtnAdd_Click" Text="Add" ValidationGroup="1"
                                                                                CssClass="btnSubmit" TabIndex="8" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:CheckBox ID="Chkautoprcss" runat="server" Text="Filing desk approval not required"
                                                                                Checked="True" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Panel ID="Panel12" runat="server" Height="120px" ScrollBars="Both">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td colspan="2">
                                                                                            <asp:GridView ID="Gridreqinfo" runat="server" AllowPaging="True" DataKeyNames="documenttype"
                                                                                                CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                                                AllowSorting="True" AutoGenerateColumns="False" OnRowCommand="Gridreqinfo_RowCommand"
                                                                                                OnPageIndexChanging="Gridreqinfo_PageIndexChanging1" Width="100%">
                                                                                                <Columns>
                                                                                                    <asp:BoundField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                                        DataField="Businessname"></asp:BoundField>
                                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                                        Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblpid" runat="server" Text='<%#Eval("PartyId") %>'></asp:Label></ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField HeaderText="Cabinet-Drawer-Folder" HeaderStyle-HorizontalAlign="Left"
                                                                                                        ItemStyle-HorizontalAlign="Left" DataField="DocType"></asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="Document Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                                        DataField="DocumentTitle"></asp:BoundField>
                                                                                                    <asp:BoundField HeaderText="Document Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                                        DataField="documentname"></asp:BoundField>
                                                                                                    <asp:BoundField DataField="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                                        HeaderText="Status"></asp:BoundField>
                                                                                                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblwhid" runat="server" Text='<%#Eval("Whid") %>'></asp:Label></ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lbldocdate" runat="server" Text='<%#Eval("docdate") %>'></asp:Label></ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lbldocrefno" runat="server" Text='<%#Eval("docrefno") %>'></asp:Label></ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lbldocamt" runat="server" Text='<%#Eval("docamt") %>'></asp:Label></ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:ButtonField ButtonType="Image" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                                        HeaderImageUrl="~/Account/images/delete.gif" ImageUrl="~/Account/images/delete.gif"
                                                                                                        HeaderText="Delete" ItemStyle-Width="2%" CommandName="del">
                                                                                                        <ItemStyle Width="50px" />
                                                                                                    </asp:ButtonField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="center">
                                                                            <asp:Button ID="imgbtnUpload" runat="server" CssClass="btnSubmit" AlternateText="Upload"
                                                                                OnClick="imgbtnUpload_Click" Visible="False" Text="Upload" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="manualuploaddocpanel" Visible="false" runat="server" Height="350px"
                                                                Width="100%">
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <fieldset>
                                            <legend>List of Document Approved by Me </legend>
                                            <asp:Panel ID="Panel8" runat="server" Height="400px" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label2" runat="server" Text="Related Approval Type"></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:DropDownList ID="ddlapprule" runat="server" AutoPostBack="True" >
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label3" runat="server" Text="Approval Status"></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="false" 
                                                                    RepeatDirection="Horizontal" Width="100px">
                                                                    <asp:ListItem Text="Pending" Value="Pending">Pending</asp:ListItem>
                                                                    <asp:ListItem Text="Accept" Value="True">Accepted</asp:ListItem>
                                                                    <asp:ListItem Text="Reject" Value="False">Rejected</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label4" runat="server" Text="Search By"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSearch"
                                                                    ErrorMessage="*" SetFocusOnError="True" ValidationGroup="3"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSearch"
                                                                    Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                    ValidationGroup="31"></asp:RegularExpressionValidator>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="TextBox1" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                                                    onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',30)"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label10" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="Span2" class="labelcount">30</span>
                                                                <asp:Label ID="Label20" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ )"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="ImageButton1" runat="server" CssClass="btnSubmit" OnClick="ImageButton1_Click1"
                                                                Text="Show" ValidationGroup="31" />
                                                            <asp:Button ID="imgbtnSubmit" CssClass="btnSubmit" runat="server" OnClick="imgbtnSubmit_Click"
                                                                Text="Save" ValidationGroup="2" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Panel ID="Panel13" runat="server" ScrollBars="Both" Height="350px" Width="100%">
                                                                <asp:GridView ID="grid_Rule_master" runat="server" AllowSorting="false" AllowPaging="true"
                                                                    PageSize="5" AutoGenerateColumns="False" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                                    AlternatingRowStyle-CssClass="alt" DataKeyNames="RuleDetailId" EmptyDataText="No Record Found."
                                                                    OnRowCommand="grid_Rule_master_RowCommand" Width="100%" OnPageIndexChanging="grid_Rule_master_PageIndexChanging">
                                                                    <RowStyle />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ID" ItemStyle-Width="5%"
                                                                            SortExpression="DocId">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" ForeColor="#426172" runat="server" Text='<%# Eval("DocId") %>'></asp:Label></ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="5%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval <br/> by Date"
                                                                            SortExpression="ProcessDate" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblapprovetobedate" runat="server" Text='<%# Bind("ProcessDate","{0:MM/dd/yyyy-HH:mm}") %>'></asp:Label></ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="DocumentTitle" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%"
                                                                            HeaderText="Title" SortExpression="DocumentTitle">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="20%" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval Rule"
                                                                            SortExpression="RuleTitle" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <a href='BusinessProcessRules.aspx?Id=<%#DataBinder.Eval(Container.DataItem, "RuleDetailId")%>'
                                                                                    target="_blank">
                                                                                    <asp:Label ID="lblruletitle123" ForeColor="#426172" runat="server" Text='<%# Bind("RuleTitle") %>'></asp:Label></a></ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval Type"
                                                                            ItemStyle-Width="15%" SortExpression="RuleApproveTypeName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtDescraa" runat="server" Text='<%# Bind("RuleApproveTypeName") %>'
                                                                                    Visible="false"></asp:Label><asp:Label ID="lblruleid" runat="server" Text='<%# Bind("RuleId") %>'
                                                                                        Visible="false"></asp:Label><asp:LinkButton ID="LinkButton1" ForeColor="#426172"
                                                                                            runat="server" Text='<%# Bind("RuleApproveTypeName") %>' OnClick="linkdow1_Click"></asp:LinkButton></ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="15%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval Type Description"
                                                                            SortExpression="Description" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtDescr" runat="server" Height="48px" Text='<%# Bind("Description") %>'
                                                                                    TextMode="MultiLine" Width="110px"></asp:TextBox></ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval History"
                                                                            HeaderImageUrl="~/images/ApprovalHistoryDoc.png" Visible="false" ItemStyle-Width="2%">
                                                                            <ItemTemplate>
                                                                                <a onclick="window.open('BusinessRuleManagementreport2.aspx?DocId=<%#DataBinder.Eval(Container.DataItem, "DocId")%>')"
                                                                                    href="javascript:void(0)">
                                                                                    <asp:Label ID="lblapprovalhistory" ForeColor="#426172" runat="server" Visible="false"
                                                                                        Text="Approval History"></asp:Label><asp:ImageButton ID="ImageButton2approval" Height="18px"
                                                                                            Width="18px" runat="server" CausesValidation="false" ImageUrl="~/images/ApprovalHistoryDoc.png"
                                                                                            ToolTip="Approval History" /></a></ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="2%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                                                                            HeaderText="Accept/Reject" ItemStyle-Width="7%">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="rbtnAcceptReject" runat="server" RepeatDirection="Horizontal"
                                                                                    Width="100px">
                                                                                    <asp:ListItem Selected="True" Value="Pending">Pending</asp:ListItem>
                                                                                    <asp:ListItem Value="True">Accepted</asp:ListItem>
                                                                                    <asp:ListItem Value="False">Rejected</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle VerticalAlign="Top" Width="7%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White"
                                                                            HeaderText="Approval Notes" ItemStyle-Width="15%">
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="lbltxtnoteapprovalnote" runat="server" Text="Approval Notes"></asp:Label><asp:LinkButton
                                                                                    ID="LinkButton3" ForeColor="White" runat="server" CausesValidation="false" AutoPostBack="True"
                                                                                    OnClick="LinkButton3_Click" Text="Add/View"> </asp:LinkButton></HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNote" Visible="false" runat="server" Height="30px" MaxLength="100"
                                                                                    Width="250px" TextMode="MultiLine"></asp:TextBox><asp:RegularExpressionValidator
                                                                                        ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic" SetFocusOnError="True"
                                                                                        ValidationExpression="^([a-zA-Z0-9_.\s]*)" ControlToValidate="txtNote" ValidationGroup="2"></asp:RegularExpressionValidator><asp:Label
                                                                                            ID="lbltxtnote" runat="server" Text=""></asp:Label><asp:Button ID="btnaddnotes" runat="server"
                                                                                                Text="Add Notes" Visible="false" /></ItemTemplate>
                                                                            <HeaderStyle ForeColor="White" HorizontalAlign="Left" />
                                                                            <ItemStyle Width="20%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="View &amp; Approve"
                                                                            HeaderImageUrl="~/images/ViewandApprove.png" Visible="false" ItemStyle-Width="2%">
                                                                            <ItemTemplate>
                                                                                <a onclick="window.open('ViewbyApprove.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocId")%> &amp;Rd=<%#DataBinder.Eval(Container.DataItem,"RuleDetailId") %>')"
                                                                                    href="javascript:void(0)">
                                                                                    <asp:ImageButton ID="ImageButton2viewandapproval" Height="18px" Width="18px" runat="server"
                                                                                        CausesValidation="false" ImageUrl="~/images/ViewandApprove.png" ToolTip="View & Approve" /><asp:Label
                                                                                            ID="Labl1" runat="server" ForeColor="#426172" Text="View & Approve" Visible="false"></asp:Label></a></ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="2%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false" HeaderText="Send Message"
                                                                            ItemStyle-Width="2%" HeaderImageUrl="~/images/SendMessage.png">
                                                                            <ItemTemplate>
                                                                                <a onclick="window.open('MessageCompose.aspx?apd=<%#DataBinder.Eval(Container.DataItem, "DocId")%> &amp;Rd=<%#DataBinder.Eval(Container.DataItem,"RuleDetailId") %>')"
                                                                                    href="javascript:void(0)">
                                                                                    <asp:Label ID="Lab1" runat="server" ForeColor="#426172" Text="Send Message" Visible="false"></asp:Label><asp:ImageButton
                                                                                        ID="ImageButton2sendmessage" Height="18px" Width="18px" runat="server" CausesValidation="false"
                                                                                        ImageUrl="~/images/SendMessage.png" ToolTip="Send Message" /></a></ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="2%" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle CssClass="pgr" />
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <fieldset>
                                            <legend>Document Approval History </legend>
                                            <asp:Panel ID="Panel5" runat="server" Height="500px" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label8" runat="server" Text="Cabinet-Drawer-Folder"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddltypeofdoc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltypeofdoc_SelectedIndexChanged1">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label21" runat="server" Text="Rule Type-Rule Name"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="DdlRuleName" DataTextField="RuleTitle" DataValueField="RuleId"
                                                                    runat="server" OnSelectedIndexChanged="DdlRuleName_SelectedIndexChanged" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label22" runat="server" Text="Filter By"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="lbldatefrom" runat="server" Text="From "></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="txtFromDate" runat="server" Width="100px"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="TextBox1_MaskedEditExtender2" runat="server" CultureName="en-AU"
                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfromdate" />
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton2"
                                                                    TargetControlID="txtFromDate">
                                                                </cc1:CalendarExtender>
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="lbldateto" runat="server" Text="To "></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="txtToDate" runat="server" Width="100px"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txttodate" />
                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgbtncal2"
                                                                    TargetControlID="txttodate">
                                                                </cc1:CalendarExtender>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label23" runat="server" Text="Search"></asp:Label>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                                    ControlToValidate="TextBox3" ValidationGroup="12"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="TextBox3" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'Span2',30)"
                                                                    MaxLength="30" runat="server"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label24" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="Span3" class="labelcount">30</span>
                                                                <asp:Label ID="Label25" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ )"></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="imgbtncal2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label26" runat="server" Text="Party Type-Party Name"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlparty" runat="server" DataTextField="PartyName" DataValueField="PartyId">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Button ID="Button1" runat="server" Text="Go" OnClick="ImageButton1Approval_Click1"
                                                                    CssClass="btnSubmit" />
                                                            </label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Panel ID="Panel6" runat="server" Height="400px" ScrollBars="Vertical" Width="100%">
                                                                <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                                                                    AllowSorting="true" AllowPaging="true" PageSize="5" EmptyDataText="No Record Found."
                                                                    Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="ID" SortExpression="DocId" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left"
                                                                            HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <a onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocId")%>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                                                    style="color: Black" href="javascript:void(0)"></a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Title" SortExpression="DocumentTitle" ItemStyle-Width="15%"
                                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <a onclick="window.open('ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocId")%>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                                                    style="color: Black" href="javascript:void(0)"></a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Upload Date" SortExpression="DocumentUploadDate" ItemStyle-Width="12%"
                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldocumentuploaddate" runat="server" Text='<%# Eval("DocumentUploadDate")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                        <asp:BoundField HeaderText="Approval Rule Type" DataField="RuleTypeName" SortExpression="RuleTypeName"
                                                                            ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Approval Rule Name" DataField="RuleTitle" SortExpression="RuleTitle"
                                                                            ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Document Flow Type" Visible="false" DataField="ConditionTypeName"
                                                                            SortExpression="ConditionTypeName" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left"
                                                                            ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Final Status" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Left"
                                                                            ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Image runat="server" ID="imgfinalstatus" ImageUrl="~/Account/images/closeicon.png"
                                                                                    Visible="false" /><asp:Label ID="lblfinalstatusname" runat="server"></asp:Label></ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Approval Info" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left"
                                                                            ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:DataList RepeatColumns="2" runat="server" ID="DRuleDetail" OnItemDataBound="DRuleDetail_ItemDataBound"
                                                                                    RepeatDirection="Horizontal" OnItemCommand="DRuleDetail_ItemCommand" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Panel runat="server" ID="pnlDetail">
                                                                                            <table cellpadding="0" cellspacing="0" id="subinnertbl1">
                                                                                                <tr>
                                                                                                    <td colspan="2">
                                                                                                        <asp:Panel ID="pnlhd" runat="server">
                                                                                                            <asp:Label runat="server" ID="lblStep"></asp:Label></asp:Panel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label runat="server" ID="lblRuleDetail1"></asp:Label>Status
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        Approval Required Date
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label runat="server" ID="lblReqDate"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        Approved Date
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label runat="server" ID="lblApprovedDate"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        Approval Type
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label runat="server" ID="lblApprovalType"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:HyperLink runat="server" ID="HyperLink1" CommandArgument='<%# Eval("RuledetailId")%>'
                                                                                                            Text="Message" Target="_blank"> </asp:HyperLink><asp:LinkButton runat="server" ID="msglink"
                                                                                                                CommandArgument='<%# Eval("RuledetailId")%>' Visible="false" Text="Message"> </asp:LinkButton><%--<asp:LinkButton  runat="server" id="msglink" Text="Message" > </asp:LinkButton>--%>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:LinkButton runat="server" ID="lbtnViewNote" Text="View Note" CommandName="notes"
                                                                                                            CommandArgument='<%# Eval("EmployeeId")%>' ToolTip='<%# Eval("DocId")%>'> </asp:LinkButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </ItemTemplate>
                                                                                </asp:DataList><asp:LinkButton ID="LinkButton1" ForeColor="#426172" runat="server"
                                                                                    OnClick="linkdow1Approval_Click" Text="More Info"></asp:LinkButton><asp:Label ID="lblruleidmaster"
                                                                                        runat="server" Text='<%# Eval("RuleId")%>' Visible="false"></asp:Label><asp:Label
                                                                                            ID="lbldocid" runat="server" Text='<%# Eval("DocId")%>' Visible="false"></asp:Label></ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="View Full History" Visible="false" ItemStyle-Width="8%"
                                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <a href="BusinessRuleManagementreport2.aspx?DocId=<%# Eval("DocId")%>" style="color: Black">
                                                                                    View</a></ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                            <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                                            <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <div style="clear: both;">
                            </div>
                            <table>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Panel ID="Panel7" runat="server" CssClass="modalPopup" Width="600px">
                                            <fieldset>
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <div style="float: right;">
                                                        <label>
                                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                                Width="16px" />
                                                        </label>
                                                    </div>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                Approval Type
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="lblapprovaltype123456" runat="server" Text=""></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                Approval Description
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="lblapprovaldescription123456" runat="server" Text=""></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                            &nbsp;&nbsp;&nbsp;</asp:Panel>
                                        <asp:Button ID="Button9" runat="server" Style="display: none" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                            PopupControlID="Panel7" TargetControlID="Button9" CancelControlID="ImageButton4">
                                        </cc1:ModalPopupExtender>
                                    </td>
                                </tr>
                            </table>
                            <div style="clear: both;">
                            </div>
                            <table>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Panel ID="Panel9" runat="server" CssClass="modalPopup" Width="600px">
                                            <fieldset>
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <div style="float: right;">
                                                        <label>
                                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                                Width="16px" />
                                                        </label>
                                                    </div>
                                                    <div style="clear: both;">
                                                    </div>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                Approval Note
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="TextBox2" TextMode="MultiLine" Height="200px" Width="500px" runat="server"></asp:TextBox>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                            &nbsp;&nbsp;&nbsp;</asp:Panel>
                                        <asp:Button ID="Button6" runat="server" Style="display: none" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                            PopupControlID="Panel9" TargetControlID="Button6" CancelControlID="ImageButton2">
                                        </cc1:ModalPopupExtender>
                                    </td>
                                </tr>
                            </table>
                            <div style="clear: both;">
                            </div>
                            <table>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Width="70%">
                                            <fieldset>
                                                <table width="100%">
                                                    <div style="float: right;">
                                                        <label>
                                                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                                Width="16px" />
                                                        </label>
                                                    </div>
                                                    <div>
                                                        <asp:GridView ID="GridView2" runat="server" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" EmptyDataText="No Record Found."
                                                            Width="100%" AllowSorting="false" OnRowDataBound="GridView2_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Approval No." ItemStyle-ForeColor="Black" ItemStyle-Width="10%"
                                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldetailstepno" runat="server" Text='<%# Eval("StepNo")%>'></asp:Label></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name of Employee" ItemStyle-ForeColor="Black" ItemStyle-Width="25%"
                                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblnameofemployee" runat="server" Text='<%# Eval("EmployeeName")%>'></asp:Label></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status" ItemStyle-ForeColor="Black" ItemStyle-Width="10%"
                                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblstatusdetail" runat="server" Text='<%# Eval("ApprovalStatus")%>'></asp:Label></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Approval Type" ItemStyle-ForeColor="Black" ItemStyle-Width="20%"
                                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblapprovaltype" runat="server" Text='<%# Eval("ApprovalType")%>'></asp:Label></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Approval By Date" ItemStyle-ForeColor="Black" ItemStyle-Width="15%"
                                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblapprovalbydate" runat="server" Text='<%# Eval("ApprovalReqDate")%>'></asp:Label></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Actual Approval Date" ItemStyle-ForeColor="Black"
                                                                    ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblactualapprovaldate" runat="server" Text='<%# Eval("ApprovedDate")%>'></asp:Label></ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </table>
                                            </fieldset></asp:Panel>
                                        <asp:Button ID="Button2" runat="server" Style="display: none" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                            PopupControlID="Panel1" TargetControlID="Button2" CancelControlID="ImageButton5">
                                        </cc1:ModalPopupExtender>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <table width="100%">
                                <tr>
                                    <td colspan="4">
                                        <fieldset>
                                            <legend> <asp:Label ID="lblaccountname" runat="server" ></asp:Label></legend>
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 10%">
                                                        <label>
                                                            <asp:Label ID="Label27" runat="server" Text="Search By "></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="width: 25%">
                                                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                                                            AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Date</asp:ListItem>
                                                            <asp:ListItem Value="1" Selected="True">Period</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td style="width: 55%">
                                                        <asp:Panel ID="Panel10" runat="server" Visible="False">
                                                            <label>
                                                                <asp:Label ID="Label28" runat="server" Text="From "></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                                                    ControlToValidate="txtdatefrom" ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="txtdatefrom" runat="server" Width="75px"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureName="en-AU"
                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="imgbtn1"
                                                                    TargetControlID="txtdatefrom">
                                                                </cc1:CalendarExtender>
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="imgbtn1" runat="server" ImageUrl="~/images/cal_btn.jpg" />
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label29" runat="server" Text="To "></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="true"
                                                                    ControlToValidate="txtdateto" ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </label>
                                                            <label>
                                                                <asp:TextBox ID="txtdateto" runat="server" Width="75px"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureName="en-AU"
                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdateto" />
                                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="ImageButton6"
                                                                    TargetControlID="txtdateto">
                                                                </cc1:CalendarExtender>
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/images/cal_btn.jpg" />
                                                                <input style="width: 1px" id="Hidden1" type="hidden" name="hdnsortExp" runat="Server" />
                                                                <input style="width: 1px" id="Hidden2" type="hidden" name="hdnsortDir" runat="Server" />
                                                            </label>
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel14" runat="server" Visible="False">
                                                            <label>
                                                                <asp:Label ID="Label30" runat="server" Text="Period" Visible="false"></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:DropDownList ID="ddlperiod" runat="server" Width="150px" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlperiod_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="btnSubmit" OnClick="btnGo_Click"
                                                            ValidationGroup="1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:DropDownList ID="ddlcashbank" Enabled="false" runat="server" Visible="false" >
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                        </label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="rdlistfilter" runat="server" RepeatDirection="Horizontal"
                                                            AutoPostBack="True" OnSelectedIndexChanged="rdlistfilter_SelectedIndexChanged"
                                                            Visible="false">
                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="1">Filter By Group</asp:ListItem>
                                                            <asp:ListItem Value="2" Selected="True">Filter By Account</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:DropDownList ID="ddlgroup" runat="server" Width="340px">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <fieldset>
                                            <legend>
                                                <asp:Label ID="Label32" runat="server" Text="My Account Statement"></asp:Label>
                                            </legend>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <div style="float: right">
                                                            <asp:Button ID="Button10" runat="server" Text="Printable Version" CssClass="btnSubmit"
                                                                OnClick="Button2_Click" />
                                                            <input type="button" value="Print" id="Button11" runat="server" onclick="javascript:CallPrint('divPrint')"
                                                                visible="false" class="btnSubmit" />
                                                        </div>
                                                        <div style="clear: both;">
                                                        </div>
                                                        <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <div id="mydiv" class="closed">
                                                                            <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <asp:Label ID="lblcompname" runat="server" Font-Size="20px"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <asp:Label ID="Label33" runat="server" Font-Size="20px" Text="Business : "></asp:Label>
                                                                                        <asp:Label ID="lblstore" runat="server" Font-Size="20px"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <asp:Label ID="Label34" runat="server" Text="General Ledger (List of Account Statements)"
                                                                                            Font-Size="18px"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Panel ID="gpanel" runat="server" Visible="false">
                                                                            <asp:Label ID="gaccname" runat="server" class="subinnertblfc" Text="as on" Visible="true"></asp:Label>
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
                                                                        <asp:GridView ID="grdCashBankReport" runat="server" AutoGenerateColumns="False" EmptyDataText="There is no Record"
                                                                            Width="100%" PageSize="100" OnRowCommand="grdCashBankReport_RowCommand" OnSorting="grdCashBankReport_Sorting"
                                                                            DataKeyNames="Tranction_Master_Id" OnRowDataBound="grdCashBankReport_RowDataBound"
                                                                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Account" SortExpression="Account" Visible="false"
                                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAcc1" Visible="false" runat="server" Text='<%# Bind("Account") %>'></asp:Label></ItemTemplate>
                                                                                    <HeaderStyle Width="80px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label></ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Entry Type" SortExpression="EntryType" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblentrytype" runat="server" Text='<%# Bind("EntryType") %>'></asp:Label></ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                    </EditItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Entry No." SortExpression="EntryNo" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lblentryno" runat="server" Text='<%# Bind("EntryNo") %>' OnClick="linkentry_Click"
                                                                                            ForeColor="#416271"></asp:LinkButton></ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                    </EditItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Group: Account" SortExpression="Accountr" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAcc" runat="server" Text='<%# Bind("Accountr") %>'></asp:Label><asp:Label
                                                                                            ID="lbltradeid" runat="server" Visible="false" Text='<%# Bind("Tranction_Details_Id") %>'></asp:Label><asp:LinkButton
                                                                                                ID="link1" runat="server" Text="Split" ForeColor="#457cec" OnClick="link1_Click"></asp:LinkButton></ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                    </EditItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Credit<br/>(Decrease)" SortExpression="Credit" ItemStyle-HorizontalAlign="Right"
                                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAmtCredit" runat="server" Text='<%# Bind("Credit") %>'></asp:Label></ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                    </EditItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Debit<br/>(Increase)" SortExpression="Debit" ItemStyle-HorizontalAlign="Right"
                                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAmtDetail" runat="server" Text='<%# Bind("Debit") %>'></asp:Label></ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                    </EditItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBalance" runat="server" Text='<%# Bind("Balance") %>'></asp:Label></ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                    </EditItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Detail Memo" SortExpression="DetMemo" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbldetailmemo" runat="server" Text='<%# Bind("DetMemo") %>'></asp:Label></ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                    </EditItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="TranctionMId" Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTransactionMasterId" runat="server" Text='<%# Bind("Tranction_Master_Id") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="View Doc" ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbldocno" runat="server"></asp:Label><asp:ImageButton ID="img1" runat="server"
                                                                                            CausesValidation="true" ImageUrl="~/ShoppingCart/images/Docimg.png" AlternateText=""
                                                                                            Height="22px" Width="22px" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Tranction_Master_Id")%>'
                                                                                            CommandName="AddDoc"></asp:ImageButton></ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                                                    <ItemStyle Width="5%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Add Doc" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%"
                                                                                    Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("Tranction_Master_Id") %>'
                                                                                            CommandName="Docadd" ToolTip="Add Doc" Height="20px" ImageUrl="~/Account/images/attach.png"
                                                                                            Width="20px"></asp:ImageButton></ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="2%" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <PagerStyle CssClass="pgr" />
                                                                            <AlternatingRowStyle CssClass="alt" />
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
                            </table>
                            <div style="clear: both;">
                            </div>
                            <table>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Panel ID="Panel15" runat="server" CssClass="modalPopup" Width="400px">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="lblm" runat="server" ForeColor="Black">You have to switch to 
                                                        previous year to view this report.</asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="height: 26px">
                                                        <asp:Label ID="lblstartdate" runat="server" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="Button8" runat="server" Text="Cancel" OnClick="ImageButton2_Click"
                                                            CssClass="btnSubmit" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Button ID="btnmd" runat="server" Style="display: none" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                                            PopupControlID="Panel15" TargetControlID="btnmd" CancelControlID="Button8">
                                        </cc1:ModalPopupExtender>
                                    </td>
                                </tr>
                            </table>
                            <div style="clear: both;">
                            </div>
                            <table>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Panel ID="Paneldoc" runat="server" Width="600px" CssClass="modalPopup">
                                            <fieldset>
                                                <legend>
                                                    <asp:Label ID="lbldoclab" runat="server" Text="List of Document"></asp:Label>
                                                </legend>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 95%;">
                                                        </td>
                                                        <td align="right">
                                                            <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                                Width="16px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <label>
                                                                <asp:Label ID="lblheadoc" runat="server" Text="List of documents attached to "></asp:Label>
                                                                <asp:Label ID="lbldocentrytype" runat="server" Font-Bold="True" ForeColor="#457cec"></asp:Label>
                                                                <asp:Label ID="Label35" runat="server" Text=" entry no."></asp:Label>
                                                                <asp:Label ID="lbldocentryno" runat="server" Font-Bold="True" ForeColor="#457cec"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Panel ID="pvgris" runat="server" Height="100%" ScrollBars="Vertical" Width="100%">
                                                                <asp:GridView ID="grd" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                    CssClass="mGrid" DataKeyNames="Id" HeaderStyle-HorizontalAlign="Left" OnRowCommand="grd_RowCommand"
                                                                    PagerStyle-CssClass="pgr" Width="100%">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Datetime" HeaderText="Date" HeaderStyle-HorizontalAlign="Left" />
                                                                        <asp:BoundField DataField="IfilecabinetDocId" HeaderText="ID" HeaderStyle-HorizontalAlign="Left" />
                                                                        <asp:BoundField DataField="Titlename" HeaderText="Title" HeaderStyle-HorizontalAlign="Left" />
                                                                        <asp:BoundField DataField="Filename" HeaderText="Cabinet-Drawer-Folder" HeaderStyle-HorizontalAlign="Left" />
                                                                        <asp:TemplateField HeaderText="View Doc" ItemStyle-ForeColor="#416271">
                                                                            <ItemTemplate>
                                                                                <a href="javascript:void(0)" onclick='window.open(&#039;viewdocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "IfilecabinetDocId")%>&#039;, &#039;welcome&#039;,&#039;width=1200,height=700,menubar=no,status=no&#039;)'>
                                                                                    <asp:Label ID="lbldoc" runat="server" ForeColor="#416271" Text="View Doc"></asp:Label></a></ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset></asp:Panel>
                                        <asp:Button ID="Button12" runat="server" Style="display: none" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
                                            PopupControlID="Paneldoc" TargetControlID="Button12" CancelControlID="ImageButton7">
                                        </cc1:ModalPopupExtender>
                                    </td>
                                </tr>
                            </table>
                            <div style="clear: both;">
                            </div>
                            <table>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Panel ID="pnladdress" runat="server" CssClass="modalPopup" Width="520px">
                                            <table width="100%">
                                                <tr>
                                                    <td align="right">
                                                        <input type="button" value="Print" id="Button13" runat="server" onclick="javascript:CallPrint1('pnlpt');"
                                                            class="btnSubmit" visible="true" />
                                                        &nbsp;
                                                        <asp:ImageButton ID="ImageButton8" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                                            Width="16px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pnlpt" runat="server">
                                                            <fieldset>
                                                                <legend>
                                                                    <asp:Label ID="Label36" runat="server" Text="List of Accounts Entry Detail"></asp:Label>
                                                                </legend>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label37" runat="server" Text="Business Name:"> 
                                                                                </asp:Label>
                                                                            </label>
                                                                            <label>
                                                                                <asp:Label ID="lblbnp" runat="server" Text=""> 
                                                                                </asp:Label>
                                                                            </label>
                                                                            <label>
                                                                                &nbsp;</label>
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
                                                                                <asp:Label ID="Label38" runat="server" Text="Entry Type:"> 
                                                                                </asp:Label>
                                                                            </label>
                                                                            <label>
                                                                                <asp:Label ID="lbletp" runat="server" Text=""> 
                                                                                </asp:Label>
                                                                            </label>
                                                                            <label>
                                                                                &nbsp;</label>
                                                                            <label>
                                                                                <asp:Label ID="Label39" runat="server" Text="Entry Number:"> 
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
                                                                            <asp:Panel ID="pnlof" runat="server" ScrollBars="Auto">
                                                                                <asp:GridView ID="grdPartyList" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                                    EmptyDataText="No Record Found." PageSize="20">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Account" HeaderStyle-HorizontalAlign="Left">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblacccc" Visible="true" runat="server" Text=""></asp:Label></ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" Width="32%" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Memo" SortExpression="Memo" HeaderStyle-HorizontalAlign="Left"
                                                                                            ItemStyle-HorizontalAlign="Left">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblmem" Visible="true" runat="server" Text='<%#Eval("Memo") %>'></asp:Label></ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" Width="32%" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Debit" SortExpression="AmountDebit" HeaderStyle-HorizontalAlign="Left"
                                                                                            ItemStyle-HorizontalAlign="Right">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDebit" Visible="true" runat="server" Text='<%#Eval("AmountDebit") %>'></asp:Label></ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" Width="18%" />
                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Credit" SortExpression="AmountCredit" HeaderStyle-HorizontalAlign="Left"
                                                                                            ItemStyle-HorizontalAlign="Right">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCredit" Visible="true" runat="server" Text='<%#Eval("AmountCredit") %>'></asp:Label></ItemTemplate>
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
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender6" BackgroundCssClass="modalBackground"
                                            PopupControlID="pnladdress" TargetControlID="ImgBtnAddress" runat="server" CancelControlID="ImageButton8">
                                        </cc1:ModalPopupExtender>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="View3" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="width: 50%">
                                        <fieldset>
                                            <legend>Customer Complaint </legend>
                                            <asp:Panel ID="Panel17" runat="server" Height="400px" ScrollBars="Auto" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 30%">
                                                            <label>
                                                                <asp:Label ID="Label40" runat="server" Text="Customer ID"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td style="width: 70%">
                                                            <label>
                                                                <asp:Label ID="lblcustomerid" runat="server"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label42" runat="server" Text="Customer Name"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="lblcustomername" runat="server"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label44" runat="server" Text="Support Request ID"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label45" runat="server"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label46" runat="server" Text="Entry Date"></asp:Label>
                                                                <asp:Label ID="Label47" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtEntryDate"
                                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="TxtEntryDate" runat="server" Width="100px">
                                                                </asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="ImageButton9"
                                                                    TargetControlID="TxtEntryDate">
                                                                </cc1:CalendarExtender>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureName="en-AU"
                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtEntryDate" />
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/images/cal_icon.jpg" />
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label48" runat="server" Text="Support Type"></asp:Label>
                                                                <asp:Label ID="Label49" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlProbType" runat="server">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label50" runat="server" Text="Support Request Title"></asp:Label>
                                                                <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtProTitle"
                                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="REG1master" runat="server" ErrorMessage="Invalid Character"
                                                                    SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtProTitle"
                                                                    ValidationGroup="1">
                                                                </asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtProTitle" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',60)"
                                                                    runat="server" Width="250px" MaxLength="60">
                                                                </asp:TextBox>
                                                                <asp:Label ID="lblbbb" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="Span4" class="labelcount">60</span>
                                                                <asp:Label ID="Label52" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label53" runat="server" Text="Description"></asp:Label>
                                                                <asp:Label ID="Label54" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtProbDesc"
                                                                    ErrorMessage="*" ValidationGroup="1">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Character"
                                                                    SetFocusOnError="True" ValidationExpression="^([_a-zA-Z.0-9\s]*)" ControlToValidate="txtProbDesc"
                                                                    ValidationGroup="1">
                                                                </asp:RegularExpressionValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="*"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1000})$"
                                                                    ControlToValidate="txtProbDesc" ValidationGroup="1">
                                                                </asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtProbDesc" runat="server" MaxLength="1000" onkeypress="return checktextboxmaxlength(this,1000,event)"
                                                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z.A-Z0-9_\s]+$/,'Span5',1000)"
                                                                    TextMode="MultiLine" Height="30px" Width="250px"></asp:TextBox>
                                                                <asp:Label ID="Label55" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="Span5" class="labelcount">1000</span>
                                                                <asp:Label ID="Label56" runat="server" Text="(A-Z 0-9 _ .)" CssClass="labelcount"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlUserMaster" runat="server" Visible="False">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button14" runat="server" Text="Submit" OnClick="Button14_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </fieldset>
                                    </td>
                                    <td style="width: 50%">
                                        <fieldset>
                                            <legend>Customer Support Requests </legend>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel19" runat="server" Height="400px" ScrollBars="Both" Width="100%">
                                                            <fieldset>
                                                                <legend>
                                                                    <asp:Label ID="Label41" runat="server" Text="Customer Support Requests"></asp:Label>
                                                                </legend>
                                                                <div>
                                                                    <asp:Panel ID="pnlfromdatetodate" runat="server">
                                                                        <label>
                                                                            <asp:Label ID="Label43" runat="server" Text="Filter by Date "></asp:Label>
                                                                            <asp:Label ID="Label57" runat="server" Text=" From  "></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtFromDate"
                                                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                        </label>
                                                                        <label>
                                                                            <asp:TextBox ID="TextBox4" runat="server" Width="100px"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="imgbtncal"
                                                                                TargetControlID="TextBox4">
                                                                            </cc1:CalendarExtender>
                                                                        </label>
                                                                        <label>
                                                                            <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                                        </label>
                                                                        <label>
                                                                            <asp:Label ID="Label58" runat="server" Text="To"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTodate"
                                                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                        </label>
                                                                        <label>
                                                                            <asp:TextBox ID="TextBox5" runat="server" Width="100px"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="ImageButton10"
                                                                                TargetControlID="TextBox5">
                                                                            </cc1:CalendarExtender>
                                                                        </label>
                                                                        <label>
                                                                            <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                                        </label>
                                                                        <div style="clear: both;">
                                                                        </div>
                                                                        <label>
                                                                            <asp:Label ID="Label59" runat="server" Text="Filter by Status  "></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:DropDownList ID="ddlMainStatus" runat="server" Width="150px">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                        <label>
                                                                            <asp:Button ID="Button15" runat="server" Text="Go" CssClass="btnSubmit" OnClick="Button15_Click"
                                                                                ValidationGroup="1" />
                                                                            <input id="Hidden3" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                                            <input id="Hidden4" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                                        </label>
                                                                    </asp:Panel>
                                                                </div>
                                                            </fieldset>
                                                            <fieldset>
                                                                <legend>
                                                                    <asp:Label ID="Label60" runat="server" Text="List of Support Requests"></asp:Label>
                                                                </legend>
                                                                <div style="clear: both;">
                                                                </div>
                                                                <asp:Panel ID="Panel18" runat="server" Width="100%" Height="250px" ScrollBars="Vertical">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:GridView ID="GridServiceCall" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="Both"
                                                                                    AllowSorting="true" CssClass="mGrid" DataKeyNames="CustomerServiceCallMasterId"
                                                                                    OnPageIndexChanging="GridServiceCall_PageIndexChanging" OnRowCommand="GridServiceCall_RowCommand"
                                                                                    EmptyDataText="No Record Found." OnSorting="GridServiceCall_Sorting" PageSize="100"
                                                                                    Width="100%">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="ID" SortExpression="CustomerServiceCallMasterId" HeaderStyle-HorizontalAlign="Left"
                                                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                                                            <ItemTemplate>
                                                                                                <a href='CustSCUpdatePage2.aspx?id=<%# Eval("CustomerServiceCallMasterId")%>' target="_blank">
                                                                                                    <asp:Label ID="lblInvName" runat="server" Text='<%#Bind("CustomerServiceCallMasterId") %>'
                                                                                                        ForeColor="#416271"></asp:Label></a></ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Date" SortExpression="Entrydate" HeaderStyle-HorizontalAlign="Left"
                                                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("Entrydate","{0:MM/dd/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Request Title" SortExpression="ProblemTitle" HeaderStyle-HorizontalAlign="Left"
                                                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                                                            <ItemTemplate>
                                                                                                <a href='CustSCUpdatePage2.aspx?id=<%# Eval("CustomerServiceCallMasterId")%>' target="_blank">
                                                                                                    <asp:Label ID="lblInvczxName" runat="server" Text='<%#Bind("ProblemTitle") %>' ForeColor="#416271"></asp:Label></a></ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Request Notes" SortExpression="ProblemDescription"
                                                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label123" runat="server" Text='<%# Eval("ProblemDescription") %>'></asp:Label></ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Status" SortExpression="StatusName" HeaderStyle-HorizontalAlign="Left"
                                                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("StatusName") %>'></asp:Label></ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Service Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                            ItemStyle-Width="15%">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label12454" runat="server" Text='<%# Eval("Sdate") %>'></asp:Label></ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Service Notes" HeaderStyle-HorizontalAlign="Left"
                                                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("Snote") %>'></asp:Label></ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:ButtonField CommandName="View" ButtonType="Image" ImageUrl="~/Account/images/viewprofile.jpg"
                                                                                            HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderText="View" Text="View"
                                                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                                                        </asp:ButtonField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </fieldset></asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%">
                                        <fieldset>
                                            <legend>
                                                <asp:Label ID="Label65" runat="server" Text="Internal Inbox"></asp:Label>
                                            </legend>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel20" runat="server" Width="100%" Height="250px" ScrollBars="Vertical">
                                                            <asp:GridView ID="GridView3" runat="server" GridLines="None" AllowPaging="True" CssClass="mGrid"
                                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="MsgDetailId"
                                                                EmptyDataText="There is no Message." AllowSorting="True" Width="100%" AutoGenerateColumns="False"
                                                                EnableModelValidation="True" OnPageIndexChanging="GridView3_PageIndexChanging"
                                                                OnRowDataBound="GridView3_RowDataBound" OnSorting="GridView3_Sorting" PageSize="5">
                                                                <Columns>
                                                                    <asp:BoundField DataField="MsgDate" HeaderStyle-HorizontalAlign="Left" HeaderText="Date"
                                                                        HeaderStyle-Width="10%" SortExpression="MsgDate" DataFormatString="{0:MM/dd/yyyy}" />
                                                                    <asp:TemplateField HeaderText="From" SortExpression="MsgDetailId" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="20%">
                                                                        <ItemTemplate>
                                                                            <a href="MessageView.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>&Status=<%# Eval("MsgStatusId")%>">
                                                                                <b><font color="black">
                                                                                    <%#  Eval ("Compname")  %></b></font></a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" SortExpression="MsgSubject"
                                                                        HeaderStyle-Width="60%">
                                                                        <ItemTemplate>
                                                                            <a href="MessageView.aspx?MsgDetailId=<%# Eval("MsgDetailId")%>&Status=<%# Eval("MsgStatusId")%>">
                                                                                <b><font color="black">
                                                                                    <%#  Eval("MsgSubject") %></b></font></a>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="60%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Image runat="server" ID="ImgFIle" ImageUrl="~/Account/images/attach.png" />
                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <asp:Image runat="server" ID="ImgFIleHeader" ImageUrl="~/Account/images/attach.png" />
                                                                        </HeaderTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="MsgStatusName" HeaderStyle-HorizontalAlign="Left" HeaderText="Status"
                                                                        SortExpression="MsgStatusName" HeaderStyle-Width="10%"></asp:BoundField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 50%">
                                                        <asp:LinkButton ID="LinkButton4" Text="More.." runat="server" CssClass="btnSubmit"
                                                            OnClick="LinkButton2message_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td style="width: 50%">
                                        <fieldset>
                                            <legend>
                                                <asp:Label ID="Label66" runat="server" Text="Internal Compose"></asp:Label>
                                            </legend>
                                            <asp:Label ID="lblmsg2" runat="server" ForeColor="Red"></asp:Label>
                                            <div style="clear: both;">
                                            </div>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Panel Width="100%" runat="server" ID="Panel26">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 9%">
                                                                        <label>
                                                                            <asp:Label ID="Label67" Text="To" runat="server"></asp:Label>
                                                                            <asp:Label ID="Label68" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextBox6"
                                                                                ErrorMessage="*" ValidationGroup="9" Width="13px"></asp:RequiredFieldValidator>
                                                                            <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/Account/images/addbook.png"
                                                                                ToolTip="Click here to add Addresses" />
                                                                        </label>
                                                                    </td>
                                                                    <td style="width: 41%">
                                                                        <label>
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="TextBox6" runat="server" Width="350" ReadOnly="True"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="Button1"></asp:AsyncPostBackTrigger>
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 9%" valign="top">
                                                                        <label>
                                                                            <asp:Label ID="Label69" Text="Subject" runat="server"></asp:Label>
                                                                            <br />
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                                                ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([._:a-zA-Z0-9\s]*)"
                                                                                ControlToValidate="TextBox7" ValidationGroup="9"></asp:RegularExpressionValidator>
                                                                        </label>
                                                                    </td>
                                                                    <td style="width: 41%">
                                                                        <label>
                                                                            <asp:TextBox ID="TextBox7" runat="server" ValidationGroup="1" Width="350px" MaxLength="200"
                                                                                onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\_:a-zA-Z.0-9\s]+$/,'Span4',200)"></asp:TextBox>
                                                                        </label>
                                                                        <label>
                                                                            <asp:Label runat="server" ID="Label70" Text="Max" CssClass="labelcount"></asp:Label>
                                                                            <span id="Span6" cssclass="labelcount">200</span>
                                                                            <asp:Label ID="Label71" runat="server" Text="(A-Z 0-9 _ . :)" CssClass="labelcount"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 9%" valign="top">
                                                                        <label>
                                                                            <asp:Label ID="Label72" runat="server" Text="Message"></asp:Label>
                                                                            <br />
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                                                ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)"
                                                                                ControlToValidate="TextBox8" ValidationGroup="9"></asp:RegularExpressionValidator>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                                                ErrorMessage="*" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1000})$"
                                                                                ControlToValidate="TextBox8" ValidationGroup="9"></asp:RegularExpressionValidator>
                                                                        </label>
                                                                    </td>
                                                                    <td style="width: 41%">
                                                                        <label>
                                                                            <asp:TextBox ID="TextBox8" runat="server" TextMode="MultiLine" Width="350px" Height="60px"
                                                                                onkeypress="return checktextboxmaxlength(this,1000,event)" onKeydown="return mask(event)"
                                                                                onkeyup="return check(this,/[\\/!#$%^'&*>:;={}[]|\/]/g,/^[\a-z().@+A-Z,0-9_-\s]+$/,'Span7',1000)"></asp:TextBox>
                                                                            <br />
                                                                            <asp:Label runat="server" ID="Label73" Text="Max" CssClass="labelcount"></asp:Label>
                                                                            <span id="Span7" cssclass="labelcount">1000</span>
                                                                            <asp:Label ID="Label74" runat="server" Text="(A-Z 0-9 , . @ ) ( - +)" CssClass="labelcount"></asp:Label>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 8%">
                                                                    </td>
                                                                    <td style="width: 42%">
                                                                        <asp:CheckBox ID="CheckBox4" Visible="false" runat="server" Text="Include Signature" />
                                                                        <asp:CheckBox ID="CheckBox5" Visible="false" runat="server" Text="Include Picture" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 9%">
                                                                    </td>
                                                                    <td style="width: 41%">
                                                                        <asp:Button ID="Button16" runat="server" Text="Send" ValidationGroup="9" CssClass="btnSubmit"
                                                                            OnClick="Button16_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input id="Hidden5" runat="server" name="hdnFileName" style="width: 1px" type="hidden" />
                                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Panel ID="Panel27" runat="server" BorderStyle="Outset" ScrollBars="Vertical"
                                                Height="500px" Width="380px" BackColor="#CCCCCC" BorderColor="#666666">
                                                <table id="innertbl1">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblSelectRecipient" Text="Select Receipient" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td width="10%">
                                                                        <asp:Button ID="Button17" runat="server" Text="Insert" Font-Size="13px" Font-Names="Arial"
                                                                            ToolTip="Insert" CssClass="btnSubmit" OnClick="Button17_Click"></asp:Button>
                                                                    </td>
                                                                    <td width="10%">
                                                                        <asp:ImageButton ID="ibtnCancelCabinetAdd" runat="server" ImageUrl="~/Account/images/closeicon.png"
                                                                            AlternateText="Close" CausesValidation="False" ToolTip="Close"></asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Label ID="Label75" Text="User Type" runat="server"></asp:Label>
                                                                            <br />
                                                                            <asp:DropDownList ID="ddlusertype" runat="server" AutoPostBack="True" Width="150px"
                                                                                OnSelectedIndexChanged="ddlusertype_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Label ID="Label76" Text="Search" runat="server"></asp:Label>
                                                                            <br />
                                                                            <asp:TextBox ID="TextBox9" runat="server" AutoPostBack="True" Width="150px" OnTextChanged="TextBox9_TextChanged"></asp:TextBox>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:Panel runat="server" ID="pnlusertypeother1" Visible="false">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <label>
                                                                                            <asp:Label ID="Label77" runat="server" Text="Company Name"></asp:Label>
                                                                                        </label>
                                                                                        <label>
                                                                                            <asp:DropDownList ID="ddlcompany1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcompany1_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:Panel runat="server" ID="pnlusertypecandidate1" Visible="false">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <label>
                                                                                            <asp:Label ID="Label78" runat="server" Text="Job Applied For"></asp:Label>
                                                                                        </label>
                                                                                        <label>
                                                                                            <asp:DropDownList ID="ddlcandi11" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcandi11_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                                                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                            DataKeyNames="PartyId" GridLines="None" EmptyDataText="No Parties are available for e-mail."
                                                                            Width="338px" OnPageIndexChanging="GridView4_PageIndexChanging" OnSorting="GridView4_Sorting">
                                                                            <EmptyDataRowStyle ForeColor="Peru" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox ID="HeaderChkbox1" runat="server" OnCheckedChanged="HeaderChkbox_CheckedChanged"
                                                                                            AutoPostBack="true" />
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkParty1" runat="server" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="Contactperson" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                                                    Visible="false">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Compname" HeaderText="Company Name" HeaderStyle-HorizontalAlign="Left"
                                                                                    Visible="false">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Name" HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="PartType" HeaderText="Type" HeaderStyle-HorizontalAlign="Left"
                                                                                    Visible="false">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="CName" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="VacancyPositionTitle" HeaderText="Job Applied For" HeaderStyle-HorizontalAlign="Left">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                            <PagerStyle CssClass="pgr" />
                                                                            <AlternatingRowStyle CssClass="alt" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender7" runat="server" BackgroundCssClass="modalBackground"
                                                PopupControlID="Panel27" TargetControlID="ImageButton11" CancelControlID="ibtnCancelCabinetAdd"
                                                X="950" Y="-200">
                                            </cc1:ModalPopupExtender>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlmakepayment" Visible="false" runat="server" Width="100%">
                                            <fieldset>
                                                <legend>Make Payment </legend>
                                                <table width="100%">
                                                    <tr>
                                                        <td align="center" valign="top" width="100%">
                                                            <asp:RadioButtonList ID="RadioButtonList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged"
                                                                RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1" Selected="True">Unpaid Orders</asp:ListItem>
                                                                <asp:ListItem>Unpaid Invoice</asp:ListItem>
                                                                <asp:ListItem Value="2">Other Payment</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <asp:Panel ID="pnlpayUn" runat="server" Width="100%">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="gridPayment" runat="server" AutoGenerateColumns="False" DataKeyNames="SalesOrderId"
                                                                                OnRowCommand="gridPayment_RowCommand" Width="98%" CssClass="mGrid" EmptyDataText="No Record Found"
                                                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" HeaderStyle-HorizontalAlign="Left">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="SalesOrderDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Date" />
                                                                                    <asp:BoundField DataField="SalesOrderId" HeaderText="Order #" />
                                                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice#" />
                                                                                    <asp:BoundField DataField="GrossAmount" HeaderText="Amount" />
                                                                                    <asp:BoundField DataField="AmountDue" HeaderText="AmountDue" />
                                                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" />
                                                                                    <asp:ButtonField CommandName="pay" Text="Pay" ItemStyle-ForeColor="#416271"></asp:ButtonField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:LinkButton ID="LinkButton2" runat="server" Text="More..." ForeColor="#416271"
                                                                                OnClick="LinkButton2_Click1"></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Panel ID="pnlOtherPay" runat="server" Visible="false" Width="100%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 25%">
                                                                <label>
                                                                    <asp:Label ID="Label61" runat="server" Text="Order No."></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td style="width: 25%">
                                                                <label>
                                                                    <asp:Label ID="lblorderno" CssClass="lblSuggestion" runat="server"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td style="width: 25%">
                                                                <label>
                                                                    <asp:Label ID="Label62" runat="server" Text="Entry No."></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td style="width: 25%">
                                                                <label>
                                                                    <asp:Label ID="lblentry" runat="server"></asp:Label>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="lblentty" runat="server" Text="Entry Type "></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="lblentrytype" runat="server"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label64" runat="server" Text="Amount Due"></asp:Label>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtamtdue"
                                                                        ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ValidationGroup="12"
                                                                        ErrorMessage="Invalid"> </asp:RegularExpressionValidator>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="txtamtdue" runat="server"></asp:TextBox>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label63" runat="server" Text="Payment For "></asp:Label>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Please enter 300 chars"
                                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1000})$"
                                                                        ControlToValidate="txtPayFor" ValidationGroup="12"></asp:RegularExpressionValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="*"
                                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([._a-zA-Z0-9\s]*)"
                                                                        ControlToValidate="txtPayFor" ValidationGroup="12"></asp:RegularExpressionValidator>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="txtPayFor" runat="server"></asp:TextBox>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="lblamt" runat="server" Text="Amount "></asp:Label>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="txtAmount"
                                                                        ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ValidationGroup="12"
                                                                        ErrorMessage="Invalid"> </asp:RegularExpressionValidator>
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="center">
                                                                <asp:ImageButton ID="submit" ValidationGroup="12" runat="server" AlternateText="Make payments with PayPal - it's fast, free and secure!"
                                                                    ImageUrl="https://www.paypal.com/en_US/i/btn/btn_paynowCC_LG.gif" OnClick="submit_Click"
                                                                    OnClientClick="itemUpdate();" Style="width: 150px; height: 50px" Width="177px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </fieldset></asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgbtnAdd"></asp:PostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
