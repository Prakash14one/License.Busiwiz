<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DesignationRequirment.aspx.cs" Inherits="ShoppingCart_Admin_DesignationRequirment"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
       
         function checktextboxmaxlength(txt, maxLen,evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
          function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  
             }
            
           
            if(evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219||evt.keyCode==59||evt.keyCode==186)
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }     
            
        }   
         function check(txt1, regex, reg,id,max_len)
            {
             if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
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
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="statuslable" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    Business Name
                                </label>
                            </td>
                            <td style="width: 70%">
                                <label>
                                    <asp:DropDownList ID="ddlwarehouse" runat="server" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged"
                                        Width="250px" AutoPostBack="True">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    Department-Designation
                                </label>
                            </td>
                            <td style="width: 70%">
                                <label>
                                    <asp:DropDownList ID="ddldesi" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddldesi_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:CheckBox ID="CheckBox3" runat="server" Text="A) Education Qualification" AutoPostBack="true"
                                    TextAlign="Left" OnCheckedChanged="CheckBox3_CheckedChanged" />
                            </td>
                        </tr>
                        <asp:Panel ID="paneleduca" runat="server" Visible="false">
                            <tr>
                                <td colspan="2">
                                    <fieldset>
                                        <legend><b>
                                            <asp:Label ID="lblhead" runat="server" Text="List of Qualifications Required"></asp:Label></b>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 100%" colspan="2">
                                                    <asp:Panel ID="pnledu" runat="server" Visible="false">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="lblareaofstudy" runat="server" Text="Area of Study"></asp:Label>
                                                                        <asp:DropDownList ID="ddlareaofstudy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlareaofstudy_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="lbledd" runat="server" Text="Education Degree"></asp:Label>
                                                                        <asp:DropDownList ID="ddledudeg" runat="server">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="lblpassg" runat="server" Text="Passing Grade"></asp:Label>
                                                                        <asp:DropDownList ID="ddlpassinggrade" runat="server">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="lblspecialsub" runat="server" Text="Specialisation Subject"></asp:Label>
                                                                        <asp:DropDownList ID="ddlspecialisub" runat="server">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" CssClass="btnSubmit" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <%--  <tr>
                                                <td colspan="2" style="font-weight: bolder; text-align: left;">
                                                    <b>
                                                        <asp:Label ID="Label1" runat="server" Text="List of Qualifications Required"></asp:Label></b>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td align="right" colspan="2">
                                                    <asp:Button ID="btnaddnew" runat="server" Text="Add New" OnClick="btnaddnew_Click"
                                                        CssClass="btnSubmit" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="left">
                                                    <asp:Panel ID="pnldoclist" runat="server">
                                                        <asp:GridView ID="Gridreqinfo" runat="server" AllowPaging="false" lternatingRowStyle-CssClass="alt"
                                                            PagerStyle-CssClass="pgr" CssClass="mGrid" DataKeyNames="Id" AllowSorting="false"
                                                            AutoGenerateColumns="False" OnRowCommand="Gridreqinfo_RowCommand" Width="100%"
                                                            EmptyDataText="No Record Found.">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Area of Study" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblareaofstudy" runat="server" Text='<%#Eval("AreaStudy") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Education Degree" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbledudegree" runat="server" Text='<%#Eval("edudegree") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Passing Grade" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblpassgrade" runat="server" Text='<%#Eval("passGrade") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Specialisation Subject" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspsub" runat="server" Text='<%#Eval("spsubject") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblwhid" runat="server" Text='<%#Eval("Whid") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblareaId" runat="server" Text='<%#Eval("areaid") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbledudegreeId" runat="server" Text='<%#Eval("edudegreeid") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblpassgradeid" runat="server" Text='<%#Eval("passgradeid") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspsubid" runat="server" Text='<%#Eval("spsubid") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField ButtonType="Image" HeaderImageUrl="~/Account/images/trash.jpg" ImageUrl="~/Account/images/delete.gif"
                                                                    HeaderText="Delete" ItemStyle-Width="2%" CommandName="del"></asp:ButtonField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    </fieldset>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td colspan="2">
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="B) Experience Requirement" AutoPostBack="true"
                                    OnCheckedChanged="CheckBox1_CheckedChanged" TextAlign="Left" />
                            </td>
                        </tr>
                        <asp:Panel ID="panelexpr" runat="server" Visible="false">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        Experience Required
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtexpreq"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%" align="left">
                                    <label>
                                        <asp:TextBox ID="txtexpreq" runat="server" Width="40px" MaxLength="3" ValidationGroup="2"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="txty" runat="server" Text=" Years"></asp:Label>
                                    </label>
                                    <cc1:FilteredTextBoxExtender ID="txtaddress_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtexpreq" ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td colspan="2">
                                <asp:CheckBox ID="CheckBox2" runat="server" Text="C) Other Requirement" AutoPostBack="true"
                                    TextAlign="Left" OnCheckedChanged="CheckBox2_CheckedChanged" />
                            </td>
                        </tr>
                        <asp:Panel ID="panelother" runat="server" Visible="false">
                            <tr>
                                <td style="width: 30%" valign="top">
                                    <label>
                                        Other Notes
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-z?.,A-Z0-9_\s]*)"
                                            ControlToValidate="txtothenote" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtothenote"
                                            SetFocusOnError="True" ErrorMessage="Please enter maximum 1500 chars" ValidationExpression="^([\S\s]{0,1500})$"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%" align="left">
                                    <label>
                                        <asp:TextBox ID="txtothenote" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div2',1500)"
                                            runat="server" Height="60px" onkeypress="return checktextboxmaxlength(this,1500,event)"
                                            Width="360px" TextMode="MultiLine"></asp:TextBox>
                                        <asp:Label runat="server" ID="Label12" Text="Max " CssClass="labelcount"></asp:Label>
                                        <span id="div2" cssclass="labelcount">1500</span>
                                        <asp:Label ID="Label24" CssClass="labelcount" runat="server" Text="(A-Z 0-9 . , ? _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="panelsant" runat="server" Visible="false">
                            <tr>
                                <td style="width: 30%; height: 30px;">
                                    Preferred Sex :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlsex" runat="server">
                                        <asp:ListItem Selected="True" Value="1">Male</asp:ListItem>
                                        <asp:ListItem Value="2">Female</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    Preferred Age Range From :
                                </td>
                                <td style="width: 70%;">
                                    <asp:TextBox ID="txtfromage" runat="server" Width="40px" MaxLength="3" ValidationGroup="2"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                        ControlToValidate="txtfromage" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromage"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="2"></asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                        FilterType="Custom, Numbers" TargetControlID="txtfromage" ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                    To :
                                    <asp:TextBox ID="txttoage" runat="server" Width="40px" MaxLength="3" ValidationGroup="2"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                        ControlToValidate="txttoage" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                        FilterType="Custom, Numbers" TargetControlID="txttoage" ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttoage"
                                        ValidationGroup="2" SetFocusOnError="true" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td colspan="2">
                                <asp:CheckBox ID="chkyes" runat="server" AutoPostBack="True" TextAlign="Left" OnCheckedChanged="chkyes_CheckedChanged"
                                    Text="D) Skills Required" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                <label>
                                    <asp:Label ID="lblis" runat="Server" Text="Skill Type Required" Visible="false"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 75%;">
                                <asp:Panel ID="pnljobid" runat="server" Visible="false">
                                    <asp:DataList ID="datalistskilltype" runat="server" DataKeyField="Id" RepeatColumns="4"
                                        RepeatDirection="Horizontal" ShowFooter="False" ShowHeader="False">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr align="center" valign="top">
                                                    <td>
                                                        <asp:CheckBox ID="chksp" runat="server" AutoPostBack="true" Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>'
                                                            TextAlign="Left" OnCheckedChanged="chksp_CheckedChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                            </td>
                            <td style="width: 75%;">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="alt"
                                    PagerStyle-CssClass="pgr" CssClass="mGrid" DataKeyNames="Id" EmptyDataText="No Record Found."
                                    Width="100%" AllowSorting="True" Enabled="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Skill Type" HeaderStyle-Width="30%" SortExpression="Name"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblskillname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Skills Required ">
                                            <ItemTemplate>
                                                <asp:DataList ID="datatlistskillname" runat="server" DataKeyField="SkillId" RepeatColumns="4"
                                                    RepeatDirection="Horizontal" ShowFooter="False" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <table border="0">
                                                            <tr align="center" valign="top">
                                                                <td>
                                                                    <asp:Label ID="lblid" runat="server" Visible="false" />
                                                                    <asp:Label ID="lblskillname" runat="server" Text='<%# Bind("SkillName") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chksp" runat="server" Enabled="true" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="Submit"
                                    ValidationGroup="2" CssClass="btnSubmit" />
                                <asp:Button ID="btnupdate" runat="server" Text="Update" ValidationGroup="2" Visible="False"
                                    OnClick="btnupdate_Click" CssClass="btnSubmit" />
                                <asp:Button ID="btnedit" runat="server" Text="Edit" Visible="False" OnClick="btnedit_Click"
                                    CssClass="btnSubmit" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlwarehouse" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="Sorting" />
            <asp:AsyncPostBackTrigger ControlID="btnadd" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnsubmit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnaddnew" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlwarehouse" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddldesi" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnupdate" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnedit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="chkyes" EventName="CheckedChanged" />            
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
