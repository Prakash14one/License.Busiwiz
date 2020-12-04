<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/LicenseMaster.master"
    CodeFile="ViewByapprove.aspx.cs" Inherits="ViewbyApprove" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" Visible="False" ForeColor="Red"></asp:Label></td>
            </div>
            <div style="clear: both;">
            </div>
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Document View and Approve"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="products_box">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Business"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlbusiness" runat="server" ValidationGroup="1" Width="" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Approval Rule"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlapprule" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlapprule_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="List of Document"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddldoc" runat="server" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddldoc_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label15" runat="server" Text="Document ID"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="lbldocid" runat="server"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Doc Title"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="lblDoctitle" runat="server" ValidationGroup="1"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Doc Upload Date"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="lblUploadDate" runat="server"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="Rule Name"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="lblaatyperule" runat="server" ValidationGroup="1"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="Appr Type Description"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="txtappdisc" runat="server"></asp:Label>
                                </label>
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Doc Description" Visible="false"></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="txtdocdiscription" runat="server" ValidationGroup="1" Width="200px"
                                        Height="30px" TextMode="SingleLine" Visible="false"></asp:TextBox>
                                </label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td>
                                <div>
                                    <asp:Panel ID="lblim" runat="server" Height="660px" Width="1800px" ScrollBars="Both">
                                        <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal">
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Image ID="Image2" Height="60%" Width="700px" runat="server" ImageUrl='<%#Eval("image")%>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label11" runat="server" Text="Approval Status"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:DropDownList ID="rdlist" runat="server" Width="100px">
                                                        <asp:ListItem Selected="True" Value="None">Pending</asp:ListItem>
                                                        <asp:ListItem Value="True">Accepted</asp:ListItem>
                                                        <asp:ListItem Value="False">Rejected</asp:ListItem>
                                                    </asp:DropDownList>
                                                </label>
                                                <label>
                                                    <asp:Label ID="Label9" runat="server" Text="Approval Note"></asp:Label>
                                                </label>
                                                <label>
                                                    <asp:TextBox ID="txtupnote" runat="server" MaxLength="100" onKeydown="return mask(event)"
                                                        onkeyup="return check(this,/[\\/!#$%^'@&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_. \s]+$/,'Span2',100)"
                                                        Height="50px" TextMode="MultiLine" ValidationGroup="1" Width="250px"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid"
                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_. \s]*)"
                                                        ControlToValidate="txtupnote" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                </label>
                                                <label>
                                                    <label>
                                                        <asp:Label ID="Label10" runat="server" Text="Max " CssClass="labelcount"></asp:Label>
                                                        <span id="Span2" class="labelcount">100</span>
                                                        <asp:Label ID="Label14" runat="server" Text="(A-Z,0-9 _ .)" CssClass="labelcount"></asp:Label>
                                                    </label>
                                                </label>
                                                <label>
                                                    <asp:Button ID="lblsavenext" CssClass="btnSubmit" runat="server" Text="Save & Next"
                                                        OnClick="lblsavenext_Click" /></label>
                                                <label>
                                                    <asp:Button ID="lblnext" CssClass="btnSubmit" runat="server" Text="Next" OnClick="lblnext_Click" /></label>
                                                <label>
                                                    <asp:Button ID="btnSend" runat="server" CssClass="btnSubmit" Text="Sent Message"
                                                        OnClick="btnSend_Click" /></label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table width="100%">
                                    <tr>
                                        <td colspan="1">
                                            <label>
                                                <asp:Label ID="lblc" runat="server" Text="Approval History" Visible="false"></asp:Label>
                                            </label>
                                        </td>
                                        <td colspan="2">
                                            <asp:Panel ID="pvlApphis" runat="server" Visible="true" Width="100%">
                                                <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                    AlternatingRowStyle-CssClass="alt" DataKeyNames="RuleProcessId" AutoGenerateColumns="False"
                                                    EmptyDataText="No Record Found." Width="90%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Approved by Employee Name" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblemp" runat="server" Text='<%#Bind("EmployeeName") %>'>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approved Date" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblapdate" runat="server" Text='<%#Bind("ProcessDate") %>'>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Approval Type" DataField="RuleApproveTypeName" HeaderStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Notes" DataField="Note" HeaderStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                                </label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
