<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="AddNewServiceCallNote.aspx.cs" Inherits="ShoppingCart_Admin_AddNewServiceCall"
    Title="AddNewServiceCall" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript"> 
    function mask(evt)
        { 

           if(evt.keyCode==13 )
            { 
         
                  return true;
             }
            
           
            if( evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Date and Time"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 40%">
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="label12"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 30%">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label13" runat="server" Text="Business Name"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label12" runat="server" Text="Problem Type"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlProbType" runat="server" OnSelectedIndexChanged="ddlProbType_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Complaint Status"></asp:Label>
                                qw</label></td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlcomplaintstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcomplaintstatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Complaint ID:Date:Party:Complaint Title"></asp:Label>
                                </label>
                            </td>
                            <td colspan="2">
                                <label>
                                    <asp:DropDownList ID="ddlcustomercomplanit" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcustomercomplanit_SelectedIndexChanged"
                                        Width="400px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <asp:Panel ID="Panel1" runat="server">
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Complaint Date"></asp:Label>
                            <br />
                            <asp:Label ID="lblprintcomplaintdate" runat="server"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label7" runat="server" Text="Party Name"></asp:Label>
                            <br />
                            <asp:Label ID="lblprintpartyname" runat="server"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label8" runat="server" Text="Complaint Title"></asp:Label>
                            <br />
                            <asp:Label ID="lblprintcomplainttitle" runat="server"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label14" runat="server" Text="Complaint Description"></asp:Label>
                            <br />
                            <asp:Label ID="lblcomplaintdescription" runat="server"></asp:Label>
                        </label>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Service Provided By"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlUserMaster" runat="server" OnSelectedIndexChanged="ddlUserMaster_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label9" runat="server" Text="Service Note"></asp:Label>
                                    <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1"
                                        ErrorMessage="*" ValidationGroup="1">
                                
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                        SetFocusOnError="True" ValidationExpression="^([_,.a-zA-Z0-9\s]*)" ControlToValidate="TextBox1"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td style="width: 70%" colspan="2">
                                <label>
                                    <%-- onkeyup="return mak('Span1',1000,this)"--%>
                                    <asp:TextBox ID="TextBox1" onkeypress="return checktextboxmaxlength(this,1000,event)"
                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.,\s]+$/,'Span1',1000)"
                                        runat="server" MaxLength="1000" Width="350px" TextMode="MultiLine" Height="70px"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label ID="Label21" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="Span1" class="labelcount">1000</span>
                                    <asp:Label ID="Label22" runat="server" Text="(A-Z 0-9 _ . ,)" CssClass="labelcount"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label10" runat="server" Text="Has this problem been solved?"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Yes" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Button ID="Button1" CssClass="btnSubmit" runat="server" Text="Submit" OnClick="Button1_Click"
                                        ValidationGroup="1" />
                                    <asp:Button ID="Button2" CssClass="btnSubmit" runat="server" Text="Cancel" OnClick="Button2_Click" />
                                </label>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
