<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Blank.master" AutoEventWireup="true"
    CodeFile="Fillreport_mywork.aspx.cs" Inherits="Fillreport_mywork" %>

<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 30%;
            height: 306px;
        }
        .auto-style2 {
            width: 70%;
            height: 306px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <tr>
        <td colspan="4">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Panel ID="Panel2" runat="server" BackColor="White" Width="100%">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                           
                                <%--<tr>
                            <td colspan="2" align="right">
                                <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                    Width="16px" OnClick="ImageButton1_Click" />
                            </td>
                        </tr>--%>
                                <tr>
                                    <td colspan="2" style="padding-left: 20px; color: #416271; font-weight: bold; font-size: 18px;">
                                        <span lang="en-us">My Daily Work Report</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-size: 12px; color:Red;">
                                        <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblpageworkId" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                                        align="left">
                                        WorkTitle <span lang="en-us">:</span>
                                    </td>
                                    <td style="width: 70%; font-size: 12px; color: ##717171;">
                                        <asp:Label ID="lblworltitleatreport" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                                        align="left">
                                        <span lang="en-us">Date :</span>
                                    </td>
                                    <td style="width: 70%; font-size: 12px; color: #717171;">
                                        <asp:TextBox ID="TextBox3" runat="server" Width="110px"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBox3"
                                            PopupButtonID="ImageButton3">
                                        </cc1:CalendarExtender>
                                       <label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox3"
                                            ErrorMessage="*" ValidationGroup="6"></asp:RequiredFieldValidator>
                                            </label> 
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                                        align="left">
                                        <span lang="en-us">Budgeted Hour:</span>
                                    </td>
                                    <td style="width: 70%; font-size: 12px; color: #717171;">
                                        <asp:Label ID="lblnewbujtedhr" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                                        align="left">
                                        <span lang="en-us">Hour Spent :</span>
                                    </td>
                                    <td style="width: 70%; font-size: 12px; color: #717171; height: 22px;">
                                      
                                        <asp:TextBox ID="TextBox4" runat="server" Width="110px"></asp:TextBox>
                                      <label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox4"
                                            ErrorMessage="*" ValidationGroup="6"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox4"
                                            ErrorMessage="*" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                            ValidationGroup="6"></asp:RegularExpressionValidator>
                                            </label> 
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                                        align="left">
                                        <span lang="en-us">Report :</span>
                                    </td>
                                    <td style="width: 70%; font-size: 12px; color: #717171;">
                                        <asp:TextBox ID="TextBox5" runat="server" Width="410px" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox5"
                                            ErrorMessage="*" ValidationGroup="6"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                                        align="left">
                                        <span lang="en-us">Work Done :</span>
                                    </td>
                                    <td style="width: 70%; font-size: 12px; color: #717171;">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged"
                                            AutoPostBack="true" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                                        align="left">
                                        <span id="lblactulhr" runat="server" visible="false" lang="en-us">Actual hour required
                                            to finish today's task :</span>
                                    </td>
                                    <td width="70%">
                                        <label>
                                            <asp:TextBox ID="txtactualhourrequired" runat="server" Width="110px" Visible="false">00:00</asp:TextBox>
                                        </label>
                                    </td>
                                </tr>
                        </td>
                    </tr>
                </table>
                <table> 
                 <%-- <div style="border:1px solid black">--%>
                        
                <asp:Panel id="trtbl" runat="server" visible="false">
                   
                            <tr>
                                <td colspan="2"  style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                                    align="left">
                                 
                              
                                    <asp:CheckBox ID="chkupld" runat="server" OnCheckedChanged="chkupld_CheckedChanged"
                                        AutoPostBack="true" Visible="false" />
                                </td>
                            </tr>
                            <asp:Panel ID="Panel5" runat="server" Visible="false">
                                <tr>
                                    <td style="width: 30%; font-size: 12px; color: #717171; height: 22px;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 70%; font-size: 12px; color: #000000;">
                                        <asp:Label ID="lblftpurl123" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblftpport123" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblftpuserid" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblftppassword123" runat="server" Visible="False"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; font-size: 12px; color: #000000; height: 22px;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 70%; font-size: 12px; color: #000000;">
                                        <asp:Label ID="lblpageworkmasterId" runat="server" Visible="False" ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; font-size: 12px; color: #000000;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 70%; font-size: 12px; color: #717171;">
                                        <asp:Label ID="Label2" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td style="width: 30%;font-size: 14px; color: #416271; padding-left: 20px;">
                                    <span id="lblfnm" runat="server" lang="en-us" style="display: none">File Name  to Replace main code file when certified by supervisor</span>
                                       <span id="lblupldcode" runat="server" lang="en-us" visible="false">Upload completed
                                        code file  :</span>
                                </td>
                                <td style="width: 70%; font-size: 12px; color: #000000;">
                                    <asp:FileUpload ID="fileuploadadattachment" runat="server" Visible="false" />
                                    <span lang="en-us">&nbsp;&nbsp; </span>
                                    <asp:Button ID="Button7" runat="server" Text="Add" Visible="false" OnClick="Button9_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%; font-size: 14px; color: #416271; padding-left: 20px;">
                                    &nbsp;
                                </td>
                                <td style="width: 70%; font-size: 12px; color: #000000;">
                                    <asp:GridView ID="gridFileAttach" runat="server" AutoGenerateColumns="False" EmptyDataText="There is no data."
                                        Visible="false" Width="100%" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                        PagerStyle-CssClass="prg">
                                        <Columns>
                                            <asp:TemplateField HeaderText="File Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpdfurl" runat="server" Text='<%#Bind("PDFURL") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="GridPager" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                                        <FooterStyle CssClass="GridFooter" />
                                        <RowStyle CssClass="GridRowStyle" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%;font-size: 14px; color: #416271; padding-left: 20px;"
                                    align="left">
                                    <span id="lblcopycode" runat="server" lang="en-us" visible="false">Copy Paste Code For
                                        this Version :</span>
                                        <lable>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator225" runat="server" ControlToValidate="txtcopycode" 
                                            ErrorMessage="*" ValidationGroup="6"></asp:RequiredFieldValidator>
                                        </lable>
                                </td>
                                <td style="width: 70%; font-size: 12px; color: #717171;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <cc2:HtmlEditor ID="txtcopycode" Width="450px" runat="server"></cc2:HtmlEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                     </asp:Panel>   
                <%-- </div>--%>
                
                <asp:Panel  runat="server" id="trbdoc"  visible="false">
                  
                           
                            <tr>
                                <td style="font-size: 14px; color: #416271; padding-left: 20px;">
                                    <span id="lbldocname" runat="server" lang="en-us" style="display: none">Document Name</span>
                                   <label>    <span id="lbluplddoc" runat="server" lang="en-us" visible="false">Upload Documentation
                                        file :</span></label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="Fileupldmulti" runat="server" Visible="false" />
                                    <span lang="en-us">&nbsp;&nbsp; </span>
                                    <asp:Button ID="Button8" runat="server" Text="Add" Visible="false" OnClick="Button8_Click" />
                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <td style="width: 70%; font-size: 12px; color: #000000;">
                                            <asp:CheckBox ID="Chkmultiupld" runat="server" AutoPostBack="true" OnCheckedChanged="Chkmultiupld_CheckedChanged"
                                                Visible="false" />
                                        </td>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%;font-size: 14px; color: #416271; padding-left: 20px;">
                                    &nbsp;
                                </td>
                                <td style="width: 70%; font-size: 12px; color: #000000;">
                                    <asp:GridView ID="Griddoc" Width="100%" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"  EmptyDataText="There is no data." Visible="false" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="prg">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Document Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldocname" runat="server" Text='<%#Bind("DocumentTitle") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Show" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                         <ItemTemplate>
                                                                  <asp:LinkButton ID="linkdow1dailyversion" runat="server" Text="Show" Font-Size="12px" ForeColor="#b9b9b9"     CommandName="LinkpageVersion" CommandArgument='<%# Eval("DocumentTitle") %>' ></asp:LinkButton>
                                                          </ItemTemplate>
                                              </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="GridPager" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                                        <FooterStyle CssClass="GridFooter" />
                                        <RowStyle CssClass="GridRowStyle" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 14px; color: #416271; padding-left: 20px;"
                                    align="left" class="auto-style1">
                                    <span id="lblcopydoc" runat="server" lang="en-us" visible="false">Copy Paste Documentation
                                        for this Version :</span>

                                         <lable>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator225ff" runat="server" ControlToValidate="txtdoccopy" 
                                            ErrorMessage="*" ValidationGroup="6"></asp:RequiredFieldValidator>
                                        </lable>
                                </td>
                                <td style="font-size: 12px; color: #717171;" class="auto-style2">
                                    <cc2:HtmlEditor ID="txtdoccopy" Width="450px" runat="server"></cc2:HtmlEditor>
                                </td>
                            </tr>
                            <tr id="Tr1" runat="server" visible="false">
                                <td style="width: 30%;font-size: 14px; color: #416271; padding-left: 20px;"
                                    colspan="2" align="left" visible="false">
                                    <span id="lbltblname" runat="server" lang="en-us" visible="false"><span id="Span1" runat="server" lang="en-us" visible="false">Select Tables Used
                                        In The Page :</span></span> 
                                </td>
                            </tr>
                            <tr id="tbllist" runat="server" visible="false">
                                <%--<td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271; padding-left: 20px;"
                                    align="left">
                                    <span id="lbltblname" runat="server" lang="en-us" visible="false">Select Tables Used
                                        In The Page :</span>
                                </td>--%>
                                <td style="width: 70%; font-size: 12px; color: #000000;" colspan="2">
                                    <%--<asp:GridView ID="Gridtallist" Width="100%" runat="server" AutoGenerateColumns="False"
                                                EmptyDataText="There is no data." CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                                PagerStyle-CssClass="prg" PageSize="5">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select Table">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chktbl" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Table Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltblname" runat="server" Text='<%#Bind("name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="GridPager" />
                                                <HeaderStyle CssClass="GridHeader" />
                                                <AlternatingRowStyle CssClass="GridAlternateRow" />
                                                <FooterStyle CssClass="GridFooter" />
                                                <RowStyle CssClass="GridRowStyle" />
                                            </asp:GridView>--%>
                                    <%--<asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" AllowPaging="True"
                                                PageIndex="5" onpageindexchanging="DetailsView1_PageIndexChanging">
                                                <Fields>
                                                    <asp:TemplateField HeaderText="Select Table">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chktbl" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Table Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltblname" runat="server" Text='<%#Bind("name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Fields>--%>
                                    <%--<itemtemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chktbl" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("name") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </itemtemplate>--%>
                                    <%--</asp:DetailsView>--%>
                                    <asp:DataList ID="DataList1" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" Visible ="false">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chktbl" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="hftblid" runat="server" Value='<%#Bind("id") %>' />
                                                        <asp:Label ID="lbltblname" runat="server" Text='<%#Bind("name") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                       
                              <tr id="trtestscren" runat="server" visible="false">
                                <td style="width: 30%;font-size: 14px; color: #416271; padding-left: 20px;"
                                    align="left">
                                    <span id="Span2test" runat="server" lang="en-us" >Upload Screenshot with Data Shown :</span>
                                </td>
                                <td>
                                      <asp:FileUpload ID="FileUpload1" runat="server"  />
                                    <span lang="en-us">&nbsp;&nbsp; </span>
                                    <asp:Button ID="Button1" runat="server" Text="Add"  OnClick="Button8_Clicksre" />

                                </td>
                            </tr>
                             <tr id="trtestscren2" runat="server"  visible="false">
                                <td style="width: 30%;font-size: 14px; color: #416271; padding-left: 20px;">
                                    &nbsp;
                                </td>
                                <td style="width: 70%; font-size: 12px; color: #000000;">
                                    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                        EmptyDataText="There is no data." Visible="false" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                      DataKeyNames="ScreensortTitle"  PagerStyle-CssClass="prg">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Screenshot Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldocname" runat="server" Text='<%#Bind("ScreensortTitle") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Show" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                               
                                                                                    <asp:LinkButton ID="linkdow1dailyversion" runat="server" Text="Show" Font-Size="12px" 
                                                         ForeColor="#b9b9b9"     CommandName="LinkpageVersion" CommandArgument='<%# Eval("ScreensortTitle") %>'           ></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="GridPager" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                                        <FooterStyle CssClass="GridFooter" />
                                        <RowStyle CssClass="GridRowStyle" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr id="visiblefals" runat="server" visible="false">
                                <td style="width: 30%;font-size: 14px; color: #416271; padding-left: 20px;"
                                    align="left">
                                    <span id="lblcerti" runat="server" lang="en-us" visible="false">Certify :</span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="Chkcertify" runat="server" AutoPostBack="true" OnCheckedChanged="Chkcertify_CheckedChanged1" Checked="true"
                                        Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%;font-size: 14px; color: #416271; padding-left: 20px;">
                                </td>
                                <td style="width: 30%; font-weight: bold; font-size: 14px; color: #416271;">
                                    <asp:Label ID="lbldeveloper" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lbltester" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblsupervior" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="Label3" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lbl_versionFTPID" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lbl_versionFTPPass" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                     </asp:Panel>
                     <%-- </div>--%>
              
                <tr>
                    <td style="width: 30%; font-size: 12px; color: #717171; height: 22px;">
                    </td>
                    <td style="width: 30%; font-size: 12px; color: #717171; height: 22px;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%; font-size: 12px; color: #000000; padding-left: 150px">
                        
                        <span lang="en-us">&nbsp; </span>
                    </td>
                    <td style="width: 70%; font-size: 12px; color: #000000;">
                        <asp:Button ID="Button4" runat="server" Text="Certify" OnClick="Button4_Click" ValidationGroup="6"
                            Visible="false" />
                            <asp:Button ID="btn_Return" runat="server" Text="Return Page" OnClick="Button4_ClickReturn" 
                            Visible="false" />
                        <asp:Button ID="Button5" runat="server" Text="Cancel" OnClick="Button5_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%; font-size: 12px; color: #000000;">
                        &nbsp;
                    </td>
                    <td style="width: 70%; font-size: 12px; color: #000000;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%; font-size: 12px; color: #000000;">
                        &nbsp;
                    </td>
                    <td style="width: 70%; font-size: 12px; color: #000000;">
                        &nbsp;
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
                    <td style="width: 30%; font-size: 12px; color: #000000; height: 22px;">
                        &nbsp;
                    </td>
                    <td style="width: 70%; font-size: 12px; color: #000000; height: 22px;">
                        <asp:Button ID="Button10" runat="server" OnClick="Button10_Click" Text="Submit" />
                    </td>
                </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="Button3" runat="server" Style="display: none" />
            <%-- <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel2" TargetControlID="Button3">
                </cc1:ModalPopupExtender>--%>
        <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" Text="V5" ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>

</asp:Content>
