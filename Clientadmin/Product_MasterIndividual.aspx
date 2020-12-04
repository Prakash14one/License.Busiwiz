<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Product_MasterIndividual.aspx.cs" Inherits="AddProduct" Title="Product Master Individual" %>

<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        } 
        
         function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if(evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
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
        function OpenNewWin(url) {
            //  alert(url);
            var x = window.open(url, 'MynewWin', 'width=1000, height=1000,toolbar=2');
            x.focus();
        }
    </script>
     <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
       
        
      <ContentTemplate>
    <div style="float: left;">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div style="clear: both;">
    </div>
    <fieldset>
        <legend>
            <asp:Label ID="Label19" runat="server" Text="Product Master Individual Add / Manage "></asp:Label>
        </legend>
        <div style="float: right;">
            <asp:Button ID="addnewpanel" runat="server" Text="Product Master Individual Add / Manage "  CssClass="btnSubmit" onclick="addnewpanel_Click" />
        </div>
        <div style="clear: both;">
        </div>
        <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
            <table width="100%">
               
                     <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label37" runat="server" Text="Priceplan Category Type:"></asp:Label>                               
                                <asp:Label ID="Label38" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlcategoryType"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlcategoryType" runat="server">                                
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    
                <tr>             
                      <td style="width: 20%">
                          <label>
                              <asp:Label ID="Label8" runat="server" Text="Product Batch:"></asp:Label>
                               <asp:Label ID="Label16" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlbatch"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                         </label>
                        </td>
                        <td colspan="2">
                            <label>
                                <asp:DropDownList ID="ddlbatch" runat="server" Width="200px" >
                                </asp:DropDownList>
                            </label>
                        </td>
                  
                </tr>
                <tr>
                    <td>
                        <label>
                          <asp:Label ID="Label2" runat="server" Text="Supplier Name: "></asp:Label>
                           <asp:Label ID="Label35" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlsupplier"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </label>                    
                    </td>
                    <td colspan="2">
                          <asp:DropDownList ID="ddlsupplier" runat="server" Width="200px" >
                        </asp:DropDownList>
                        </td>
                </tr>
                  <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label63" runat="server" Text="Brand Name:"></asp:Label>
                         <asp:Label ID="Label36" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlbrand"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </label>                    
                    </td>
                    <td colspan="2">
                          <asp:DropDownList ID="ddlbrand" runat="server" Width="200px" >
                        </asp:DropDownList>
                        </td>
                </tr>

               

                    <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label3" runat="server" Text="Builder Employee Name:"></asp:Label>
                        </label>                    
                    </td>
                    <td colspan="2">
                          <asp:DropDownList ID="ddlbuildempl" runat="server" Width="200px" >
                        </asp:DropDownList>
                        </td>
                </tr>
                  <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label62" runat="server" Text="Quality Check Employee Name:"></asp:Label>
                        </label>                    
                    </td>
                    <td colspan="2">
                          <asp:DropDownList ID="ddlQltyemp" runat="server" Width="200px" >
                        </asp:DropDownList>
                        </td>
                </tr>
              
                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label9" runat="server" Text="Individual Name: "></asp:Label>
                            <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtindividual"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Inavalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z/.0-9_\s]*)"
                                ControlToValidate="txtindividual" ValidationGroup="1"></asp:RegularExpressionValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtindividual" runat="server" Width="190px" MaxLength="50" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z/.0-9_\s]+$/,'div1',50)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="max1" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="div1" cssclass="labelcount">50</span>
                            <asp:Label ID="Label25" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                        </label>
                    </td>
                </tr>
                  <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label4" runat="server" Text="Serial No: "></asp:Label>
                            <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsrno"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Inavalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z/.0-9_\s]*)"
                                ControlToValidate="txtsrno" ValidationGroup="1"></asp:RegularExpressionValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtsrno" runat="server" Width="190px" MaxLength="50" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z/.0-9_\s]+$/,'Span1',50)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span1" cssclass="labelcount">50</span>
                            <asp:Label ID="Label7" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                        </label>
                    </td>
                </tr>

                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label17" runat="server" Text="Model Number:"></asp:Label>
                            <asp:Label ID="Label18" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtmodelno" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Inavalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z/.0-9_\s]*)" ControlToValidate="txtmodelno" ValidationGroup="1"></asp:RegularExpressionValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtmodelno" runat="server" Width="190px" MaxLength="50" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z/.0-9_\s]+$/,'Span3',50)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="Label20" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span3" cssclass="labelcount">50</span>
                            <asp:Label ID="Label27" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label29" runat="server" Text="Size:"></asp:Label>                            
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtsize" runat="server" Width="190px" MaxLength="50" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z/.0-9_\s]+$/,'Span3',50)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="Label32" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span5" cssclass="labelcount">50</span>
                            <asp:Label ID="Label33" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                        </label>
                    </td>
                </tr>

                <tr>
                    <td style="width: 20%" valign="top">
                        <label>
                            <asp:Label ID="Label10" runat="server" Text="Description: "></asp:Label>
                            <asp:Label ID="Label21" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                           
                        </label>
                    </td>
                    <td colspan="2">
                        <label tyle="width:500px">
                           <cc2:HtmlEditor ID="txtdesc" runat="server"></cc2:HtmlEditor>
                        </label>
                       
                    </td>
                    
                </tr>              
                      <tr>
                    <td style="width: 20%" valign="top">
                        <label>
                            <asp:Label ID="Label12" runat="server" Text="Specification:"></asp:Label>
                            <asp:Label ID="Label14" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                           
                        </label>
                    </td>
                    <td colspan="2">
                        <label style="width:500px">
                           <cc2:HtmlEditor ID="txtspecification" runat="server"></cc2:HtmlEditor>
                        </label>
                       
                    </td>
                    
                </tr>              
                      
                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label11" runat="server" Text="Documentation URL:"></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <label  style="width:500px;">
                            <asp:TextBox ID="txtDocumentaionURL" runat="server" Width="490px" MaxLength="200" ></asp:TextBox>
                        </label> 
                        <label style="width:15px;">
                        OR 
                        </label>   
                            <label  style="width:40px;">
                                 <asp:Button ID="btnadd" runat="server" CssClass="btnSubmit" Text="Add" OnClick="btnadd_Click1" />
                            </label>                      
                          <label style="width:220px;">
                                   <asp:FileUpload ID="FileUpload1" runat="server" Visible="false"  />                                   
                           </label>
                         
                           <label style="width:80px;">
                                <asp:Button ID="btnUpload" runat="server" CssClass="btnSubmit" Text="Upload" OnClick="btnUpload_Click1" Visible="false" />
                                </label>
                         
                           <label style="width:60px;">
                            <asp:Label ID="Label28" runat="server" Text="" CssClass="labelcount"></asp:Label>
                            <span id="Span2" cssclass="labelcount"></span>
                        </label>
                       
                                       
                                    
                
                    </td>
                   
                </tr>
                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label15" runat="server" Text="Vendor Product Page URL:"></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <label  style="width:500px;">
                            <asp:TextBox ID="txtvender" runat="server" Width="490px" MaxLength="200" ></asp:TextBox>
                        </label>
                        <label style="width:15px;">
                        OR 
                        </label>   
                         <label  style="width:40px;">
                                  <asp:Button ID="btnadd2" runat="server" CssClass="btnSubmit" Text="Add" OnClick="btnadd_Click2" />
                            </label>                      
                          <label style="width:220px;">
                                   <asp:FileUpload ID="FileUpload2" runat="server" Visible="false"  />                                   
                           </label>
                         
                           <label style="width:80px;">
                                <asp:Button ID="btnUpload2" runat="server" CssClass="btnSubmit" Text="Upload" OnClick="btnUpload_Click2" Visible="false" />
                                </label>
                        <label style="width:60px;">
                            <asp:Label ID="Label26" runat="server" Text="" CssClass="labelcount"></asp:Label>
                            <span id="Span4" cssclass="labelcount"></span>
                        </label>
                        <br />
                
                    </td>
                   
                </tr>

               <tr>
                    <td style="width:20%">
                        <label>
                            <asp:Label ID="Label45" runat="server" Text="Start Date: "></asp:Label>
                            <asp:Label ID="Label46" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtStartdate"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label style="width:171px;">
                            <asp:TextBox ID="txtStartdate" runat="server" Width="171px"></asp:TextBox>
                        </label>
                        <label style="width:40px">
                            <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                        </label>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartdate"
                            PopupButtonID="imgbtnEndDate">
                        </cc1:CalendarExtender>
                        
                    </td>                   
                </tr>
                              <tr>
                    <td style="width:20%">
                        <label>
                            <asp:Label ID="Label52" runat="server" Text="End Date: "></asp:Label>
                            <asp:Label ID="Label53" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEndDate"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label style="width:171px;">
                            <asp:TextBox ID="txtEndDate" runat="server" Width="171px"  AutoPostBack="True" OnTextChanged="txtEndDate_TextChanged"></asp:TextBox>
                        </label>
                        <label style="width:40px">
                            <asp:ImageButton ID="imgbtnCalEnddate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                        </label>
                        <label>
                             <asp:Label ID="lblenddateerror" runat="server" Text="" 
                            style="color: #FF3300" ></asp:Label>
                        </label>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" PopupButtonID="imgbtnCalEnddate">
                        </cc1:CalendarExtender>
                        
                    </td>
                   
                </tr>    


                 
                                                    
                  <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label13" runat="server" Text="Active: "></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:CheckBox ID="chkboxActiveDeactive" runat="server" Text="Active" />
                        </label>
                    </td>
                   
                </tr>
                
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="1" CssClass="btnSubmit" />
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="Button1_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </fieldset>
    <fieldset>
        <legend>
            <asp:Label ID="Label30" runat="server" Text="List of Product Master Individual"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="3" align="right">
                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version"
                      Visible="false"  OnClick="Button1_Click1" />
                    <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                        style="width: 51px;" type="button" value="Print" visible="false" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                   
                    <label style="width:140px">
                        <asp:Label ID="Label40" runat="server" Text="Product Batch: "></asp:Label>
                    </label>
                    <label  style="width:180px">
                    <asp:DropDownList ID="ddlbatchfilter" runat="server" Width="180px">
                                            </asp:DropDownList>
                    </label> 
                     <label style="width:200px">
                        <asp:Label ID="Label22" runat="server" Text="Supplier Name: "></asp:Label>
                    </label>
                    <label style="width:180px">
                                   <asp:DropDownList ID="ddlsupplierfilter" runat="server" Width="180px">
                                            </asp:DropDownList>                    
                    </label> 
                    <label style="width:110px">
                        <asp:Label ID="Label23" runat="server" Text="Brand Name: "></asp:Label>
                    </label>
                    <label>
                                   <asp:DropDownList ID="ddlBrandNamefilter" runat="server" Width="190px">
                                            </asp:DropDownList>
                    
                    </label> 
                    
                </td>
            </tr>
            <tr>
            <td>
                     <label style="width:140px">
                        <asp:Label ID="Label31" runat="server" Text="Builder Employee: "></asp:Label>
                    </label>
                    <label  style="width:180px">
                    <asp:DropDownList ID="ddlbuilderempfilter" runat="server" Width="180px">
                                            </asp:DropDownList>
                    </label> 
                     <label style="width:200px">
                        <asp:Label ID="Label34" runat="server" Text="Quality Check Employee: "></asp:Label>
                    </label>
                    <label style="width:180px">
                                   <asp:DropDownList ID="ddlqlycheckfilter" runat="server" Width="180px">
                                            </asp:DropDownList>                    
                    </label> 
                    <label style="width:110px">
                        <asp:Label ID="Label39" runat="server" Text="Category Type:"></asp:Label>
                    </label>
                    <label>
                    <asp:DropDownList ID="ddlpriceplancateFilter" runat="server"  Width="190px">                                
                                </asp:DropDownList>
                    </label> 
            </td>
            </tr>
            <tr>
            <td colspan="3">
             <label  style="width:140px">
                        <asp:Label ID="Label1" runat="server" Text="Status: "></asp:Label>
                    </label>
                    <label  style="width:180px">
                        <asp:DropDownList ID="ddlstatus" runat="server"  Width="180px">
                           
                            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Active"></asp:ListItem>
                            <asp:ListItem Text="Deactive"></asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    <label>
                    <asp:Button ID="BtnGo" CssClass="btnSubmit" runat="server" Text="Go" OnClick="BtnGo_Click"  />
                    </label> 
            </td>
            </tr>
           
            <tr>
                <td colspan="3">
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <%--<asp:UpdatePanel ID="UpdatePanelSalesOrder" runat="server">
                                            <ContentTemplate>--%>
                        <table width="100%">
                            <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="850Px">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <%--    <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="Label67" runat="server" Text="Business: " Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>--%>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Products"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                
	
	
	
                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                                        OnRowCommand="GridView1_RowCommand" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                      OnRowDeleting="GridView1_RowDeleting"    AlternatingRowStyle-CssClass="alt" AllowSorting="True" Width="100%" OnSorting="GridView1_Sorting"
                                       PageSize="15" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" EmptyDataText="No Record Found.">
                                        <Columns>
                                              <asp:BoundField DataField="ProductName" HeaderStyle-HorizontalAlign="Left" HeaderText="Individual Name" SortExpression="ProductName" ItemStyle-Width="20%" />
                                               <asp:BoundField DataField="CategoryType" HeaderStyle-HorizontalAlign="Left" HeaderText="Category Type" SortExpression="CategoryType" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="SrNumber" HeaderStyle-HorizontalAlign="Left" HeaderText="Sr. Number" ItemStyle-Width="6%" SortExpression="SrNumber">
                                                <ItemStyle Width="8%" />
                                            </asp:BoundField>
                                         
                                         
                                             <asp:BoundField DataField="ModelNumber" HeaderStyle-HorizontalAlign="Left" HeaderText="Model Number"  SortExpression="ModelNumber" ItemStyle-Width="5%">
                                                <ItemStyle Width="8%" />
                                            </asp:BoundField>
                                           
                                            <asp:BoundField DataField="Size" HeaderStyle-HorizontalAlign="Left" HeaderText="Size" ItemStyle-Width="6%" SortExpression="Size">
                                                <ItemStyle Width="6%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StartDate" HeaderStyle-HorizontalAlign="Left" HeaderText="Start Date"  ItemStyle-Width="6%"  SortExpression="StartDate" DataFormatString="{0:d-MMM-yyyy}">
                                                <ItemStyle Width="8%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Retiredate" HeaderStyle-HorizontalAlign="Left" HeaderText="End Date"  ItemStyle-Width="6%"  SortExpression="Retiredate" DataFormatString="{0:d-MMM-yyyy}">
                                                <ItemStyle Width="8%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Product_descSort" HeaderStyle-HorizontalAlign="Left" HeaderText="Description"    SortExpression="Product_descSort">
                                                <ItemStyle />
                                            </asp:BoundField>
                                            
                                                 <asp:BoundField DataField="Active" SortExpression="Active" HeaderStyle-HorizontalAlign="Left" HeaderText="Active" ItemStyle-Width="2%">
                                                <ItemStyle Width="2%" />
                                            </asp:BoundField>
                                            <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%" HeaderText="Edit" ImageUrl="~/Account/images/edit.gif" Text="Button" HeaderImageUrl="~/Account/images/edit.gif" />
                                             <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"  OnClientClick="return confirm('Do you wish to delete this record?');" CommandArgument='<%# Eval("ID") %>' ToolTip="Delete"></asp:ImageButton>
                                                </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
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
    <br />
    <br />
    <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
    <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
    <input runat="server" id="hdnProductDetailId" type="hidden" name="hdnProductDetailId"
        style="width: 3px" />
    <input runat="server" id="hdnProductId" type="hidden" name="hdnProductId" style="width: 3px" />
    </ContentTemplate>
     <Triggers>
     <asp:PostBackTrigger ControlID="btnUpload" />
     <asp:PostBackTrigger ControlID="btnUpload2" />
     </Triggers>
     </asp:UpdatePanel>
</asp:Content>
