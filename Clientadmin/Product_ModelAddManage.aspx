<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Product_ModelAddManage.aspx.cs" Inherits="AddProduct" Title="Add Product / Version" %>

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
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">


      <ContentTemplate> 
    <div style="float: left;">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div style="clear: both;">
    </div>
    <fieldset>
     <div style="clear: both;">
        </div>
        <legend>
            <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
        </legend>
        <div style="float: right;">
            <asp:Button ID="addnewpanel" runat="server" Text="Product Model Add / Manage " CssClass="btnSubmit" onclick="addnewpanel_Click" />
        </div>
        <div style="clear: both;">
        </div>
        <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
            <table width="100%">
                    <tr>
                            <td width="20%">
                              <label>
                                 <asp:Label ID="Label27" runat="server" Text="Sales Type: "></asp:Label>
                                 </label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"  Width="180px">
                                </asp:DropDownList>
                            </td>
                          
                        </tr>
                     <tr>
                            <td>
                               <label>
                                <asp:Label ID="Label20" runat="server" Text="Product Type: "></asp:Label>
                                </label> 
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"  Width="180px">
                                </asp:DropDownList>
                            </td>
                           
                        </tr>
                        
                        <tr>
                            <td>
                               <label>
                                 <asp:Label ID="Label34" runat="server" Text="Sub Type: "></asp:Label>
                                 </label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged"   Width="180px">
                                </asp:DropDownList>
                            </td>
                           
                       
                        </tr>
                         <tr>
                                       
                                    <td style="width: 20%">
                                        <label>
                                            <asp:Label ID="Label8" runat="server" Text="Product Sub Sub Type:"></asp:Label>
                                             <asp:Label ID="Label22" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsubsubtype"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td colspan="2">
                                        <label>
                                            <asp:DropDownList ID="ddlsubsubtype" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged"   Width="180px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                           
                      
                  
                </tr>

                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label9" runat="server" Text="Product Model Name: "></asp:Label>
                            <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtproductmodelname"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Inavalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z/.0-9_\s]*)"
                                ControlToValidate="txtproductmodelname" ValidationGroup="1"></asp:RegularExpressionValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtproductmodelname" runat="server" Width="190px" MaxLength="50" onKeydown="return mask(event)"
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
                    <td colspan="3">
                        <asp:Panel ID="pnldesc" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="width: 20%" valign="top"> 
                                        <label>
                                            <asp:Label ID="Label14" runat="server" Text="Description: "></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <cc2:HtmlEditor ID="txtdescription" runat="server"></cc2:HtmlEditor>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>

                <tr>
                    <td style="width: 20%" valign="top">
                        <label>
                            <asp:Label ID="Label10" runat="server" Text="Specification: "></asp:Label>
                            <asp:Label ID="Label21" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                           
                        </label>
                    </td>
                    <td colspan="2">
                        <label tyle="width:500px">
                           <cc2:HtmlEditor ID="txtspecification" runat="server"></cc2:HtmlEditor>
                        </label>
                       
                    </td>
                    
                </tr>
               
                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label11" runat="server" Text="Documentation URL: "></asp:Label>
                           
                            
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
                        <br />
                
                    </td>
                   
                </tr>
              
               
                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label12" runat="server" Text="User Guide URL: "></asp:Label>
                            
                           
                        </label>
                    </td>
                    <td colspan="2">
                        <label style="width:500px;">
                            <asp:TextBox ID="txtGaudeURL" runat="server" Width="490px" MaxLength="200" ></asp:TextBox>
                           
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
                        <label>
                            <asp:Label ID="Label29" runat="server" Text="" CssClass="labelcount"></asp:Label>
                            <span id="Span7" cssclass="labelcount"></span>                          
                        </label>
                    </td>
                </tr>
                <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label31" runat="server" Text="Technical Reference URL: "></asp:Label>
                                       
                                    </label>
                                </td>
                                <td colspan="2" >
                                    <label style="width:500px;">
                                        <asp:TextBox ID="txtTechReferanceURL" runat="server" Width="490px" MaxLength="200"></asp:TextBox>
                                    </label>
                                     <label style="width:15px;">
                        OR 
                        </label>   
                         <label  style="width:40px;">
                                  <asp:Button ID="btnadd13" runat="server" CssClass="btnSubmit" Text="Add" OnClick="btnadd_Click13" />
                            </label>                      
                          <label style="width:220px;">
                                   <asp:FileUpload ID="FileUpload13" runat="server" Visible="false"  />                                   
                           </label>
                         
                           <label style="width:80px;">
                                <asp:Button ID="Button13" runat="server" CssClass="btnSubmit" Text="Upload" OnClick="btnUpload_Click13" Visible="false" />
                                </label>


                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label33" runat="server" Text="Product Model Web URL: "></asp:Label>
                                      
                                    </label>
                                </td>
                                <td colspan="2" >
                                    <label style="width:500px;">
                                        <asp:TextBox ID="txtmodelWebURL" runat="server"  Width="490px"></asp:TextBox>
                                    </label>

                                      <label style="width:15px;">
                                  OR 
                                </label>   
                              <label  style="width:40px;">
                                  <asp:Button ID="btnadd14" runat="server" CssClass="btnSubmit" Text="Add" OnClick="btnadd_Click14" />
                            </label>                      
                           <label style="width:220px;">
                                   <asp:FileUpload ID="FileUpload14" runat="server" Visible="false"  />                                   
                           </label>
                         
                           <label style="width:80px;">
                                <asp:Button ID="Button14" runat="server" CssClass="btnSubmit" Text="Upload" OnClick="btnUpload_Click14" Visible="false" />
                                </label>

                                </td>
                            </tr> 
                            <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label53" runat="server" Text="Video URL: "></asp:Label>
                        </label>
                    </td>
                     <td colspan="2" >
                        <label style="width:500px;">
                        <asp:TextBox ID="txtvideourl" runat="server" width="490px"></asp:TextBox>
                        </label> 
                          <label style="width:15px;">
                                  OR 
                                </label>   
                              <label  style="width:40px;">
                                  <asp:Button ID="btnadd15" runat="server" CssClass="btnSubmit" Text="Add" OnClick="btnadd_Click15" />
                            </label>                      
                           <label style="width:220px;">
                                   <asp:FileUpload ID="FileUpload15" runat="server" Visible="false"  />                                   
                           </label>
                         
                           <label style="width:80px;">
                                <asp:Button ID="Button15" runat="server" CssClass="btnSubmit" Text="Upload" OnClick="btnUpload_Click15" Visible="false" />
                                </label>

                    </td>
                    
                   
                </tr>         
                            <tr>
                    <td style="width:20%">
                        <label>
                            <asp:Label ID="Label15" runat="server" Text="Model Start Date: "></asp:Label>
                          
                        </label>
                    </td>
                    <td colspan="2" >
                        <label style="width:172px">
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
                            <asp:Label ID="Label16" runat="server" Text="Model Retired Date: "></asp:Label>
                         
                        </label>
                    </td>
                    <td colspan="2">
                        <label style="width:172px">
                            <asp:TextBox ID="txtEndDate" runat="server" Width="171px"    AutoPostBack="True" OnTextChanged="txtEndDate_TextChanged"></asp:TextBox>
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
                    <td>
                        <label>
                        <asp:Label ID="Label46a" runat="server" Text="Image Front (Small Size): "></asp:Label>
                        </label>
                    </td>
                    
                    <td>
                       
                        <asp:FileUpload ID="fu1_smallfront" runat="server" />
                        <asp:Button ID="btnfu1_smallfront" runat="server" onclick="imgsmallfront_Click"  Text="Upload" />
                    </td>
                    <td >
                        <asp:Image ID="img1" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="lbl1" runat="server"></asp:Label>
                    </td>
                   
                </tr>
                <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label2" runat="server" Text="Image Front (Big Size): "></asp:Label>
                        </label>
                    </td>
                      <td>
                        <asp:FileUpload ID="fu2_bigfront" runat="server" />
                        <asp:Button ID="btnfu2_bigfront" runat="server" onclick="imgbigfront_Click" 
                            Text="Upload" />
                    </td>
                    <td>
                        <asp:Image ID="img2" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="lbl2" runat="server"></asp:Label>
                    </td>
                  
                  
                </tr>
              
                <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label3" runat="server" Text="Image Back (Small Size): "></asp:Label>
                        </label>
                    </td>
                    
                    <td>
                        <asp:FileUpload ID="fu3_smallback" runat="server" />
                        <asp:Button ID="btnfu3_smallback" runat="server" onclick="imgsmallback_Click" Text="Upload" />
                    </td>
                    <td>
                        <asp:Image ID="img3" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="lbl3" runat="server"></asp:Label>
                    </td>
                  
                </tr>
                <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label4" runat="server" Text="Image Back (Big Size): "></asp:Label>
                        </label>
                    </td>
                    <td>
                        <asp:FileUpload ID="fu4_bigback" runat="server" />
                        <asp:Button ID="imgfu4_bigback" runat="server" onclick="imgbigback_Click"  Text="Upload" />
                    </td>
                    <td>
                        <asp:Image ID="img4" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="lbl4" runat="server"></asp:Label>
                    </td>
                    
                   
                </tr>
                <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label5" runat="server" Text="Image Top (Small Size): "></asp:Label>
                        </label>
                    </td>
                    
                    <td>
                        <asp:FileUpload ID="fu5_smalltop" runat="server" />
                        <asp:Button ID="btnfu5_smalltop" runat="server" onclick="imgsmalltop_Click" Text="Upload" />
                    </td>
                    <td>
                        <asp:Image ID="img5" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="lbl5" runat="server"></asp:Label>
                    </td>
                   
                </tr>
                <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label6" runat="server" Text="Image Top (Big Size): "></asp:Label>
                        </label>
                    </td>                   
                    <td>
                        <asp:FileUpload ID="fu6_bigtop" runat="server" />
                        <asp:Button ID="btnfu6_bigtop" runat="server" onclick="imgbigtop_Click" Text="Upload" />
                    </td>
                     <td>
                        <asp:Image ID="img6" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="lbl6" runat="server"></asp:Label>
                    </td>
                  
                </tr>
               
                <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label7" runat="server" Text="Image Bottom (Small Size): "></asp:Label>
                        </label>
                    </td>
                   
                    <td>
                        <asp:FileUpload ID="fu7_smallbottom" runat="server" />
                        <asp:Button ID="btnfu7_smallbottom" runat="server" onclick="imgsmallbottom_Click" Text="Upload" />
                    </td>
                     <td>
                        <asp:Image ID="img7" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="lbl7" runat="server"></asp:Label>
                    </td>
                  
                </tr>
                 <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label43" runat="server" Text="Image Bottom (Big Size): "></asp:Label>
                        </label>
                    </td>
                   
                    <td>
                        <asp:FileUpload ID="fu8_bigbottom" runat="server" />
                        <asp:Button ID="btnfu8_bigbottom" runat="server" onclick="imgbigbottom_Click"  Text="Upload" />
                    </td>
                    <td>
                        <asp:Image ID="img8" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="lbl8" runat="server"></asp:Label>
                    </td>
                </tr>
               
                <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label44" runat="server" Text="Image Right (Small Size): "></asp:Label>
                        </label>
                    </td>
                   
                    <td>
                        <asp:FileUpload ID="fu9_smallright" runat="server" />
                        <asp:Button ID="btnfu9_smallright" runat="server" onclick="imgsmallright_Click"  Text="Upload" />
                    </td>
                     <td >
                        <asp:Image ID="img9" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="lbl9" runat="server"></asp:Label>
                    </td>
                   
                </tr>
                 <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label45" runat="server" Text="Image Right (Big Size): "></asp:Label>
                        </label>
                    </td>
                    <td>
                        <asp:FileUpload ID="fu10_bigright" runat="server" />
                        <asp:Button ID="btnfu10_bigright" runat="server" onclick="imgbigright_Click"  Text="Upload" />
                    </td>
                    <td>
                        <asp:Image ID="img10" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="lbl10" runat="server"></asp:Label>
                    </td>
                   
                   
                </tr>
                <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label46" runat="server" Text="Image Left (Small Size): "></asp:Label>
                        </label>
                    </td>
                    <td>
                        <asp:FileUpload ID="fu11_smallleft" runat="server" />
                        <asp:Button ID="btnfu11_smallleft" runat="server" onclick="imgsmallleft_Click"  Text="Upload" />
                    </td>
                    <td >
                        <asp:Image ID="img11" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="lbl11" runat="server"></asp:Label>
                    </td>
                   
                </tr>
               <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label52" runat="server" Text="Image Left (Big Size): "></asp:Label>
                        </label>
                    </td>
                    <td>
                        <asp:FileUpload ID="fu12_bigleft" runat="server" />
                        <asp:Button ID="btnfu12_bigleft" runat="server" onclick="imgbigleft_Click"  Text="Upload" />
                    </td>
                   
                    <td>
                        <asp:Image ID="img12" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="lbl12" runat="server"></asp:Label>
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
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                            ValidationGroup="1" CssClass="btnSubmit" />
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
            <asp:Label ID="Label30" runat="server" Text="List of Product Model "></asp:Label>
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
                <td>
                         <label style="width:110px">
                        <asp:Label ID="Label17" runat="server" Text="Sales Type: "></asp:Label>
                    </label>
                    <label>
                    <asp:DropDownList ID="ddlsaletypefilter"    runat="server" AutoPostBack="True"   OnSelectedIndexChanged="ddlsaletypefilter_SelectedIndexChanged" Width="190px">
                    </asp:DropDownList>
                    </label> 
                     <label style="width:110px">
                        <asp:Label ID="Label18" runat="server" Text="Product Type:	"></asp:Label>
                    </label>
                    <label>
                    <asp:DropDownList ID="ddlproductypefilter"    runat="server" AutoPostBack="True"   OnSelectedIndexChanged="ddlproductypefilter_SelectedIndexChanged" Width="190px">
                    </asp:DropDownList>
                    </label> 
                     <label style="width:100px">
                        <asp:Label ID="Label35" runat="server" Text="Sub Type:	"></asp:Label>
                    </label>
                    <label>
                    <asp:DropDownList ID="ddlsubtypefilter"    runat="server" AutoPostBack="True"   OnSelectedIndexChanged="ddlsubtypefilter_SelectedIndexChanged" Width="190px">
                    </asp:DropDownList>
                    </label> 
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <label style="width:110px">
                        <asp:Label ID="Label40" runat="server" Text="Sub Sub Type: "></asp:Label>
                    </label>
                    <label>
                    <asp:DropDownList ID="ddlsubsubfilter"    runat="server" AutoPostBack="True"   OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" Width="190px">
                    </asp:DropDownList>
                    </label> 
                    <label style="width:110px">
                        <asp:Label ID="Label1" runat="server" Text="Status: "></asp:Label>
                    </label>
                    <label>
                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" Width="190px"
                            OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
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
                                     OnRowDeleting="GridView1_RowDeleting"   AlternatingRowStyle-CssClass="alt" AllowSorting="True" Width="100%" OnSorting="GridView1_Sorting" EmptyDataText="No Record Found." 
                                     PageSize="15" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="ProductModelName" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Model Name" SortExpression="ProductModelName" />
                                            <asp:BoundField DataField="ProductModelDesc" HeaderStyle-HorizontalAlign="Left" HeaderText="Model Description" ItemStyle-Width="30%" SortExpression="ProductModelName">
                                                <ItemStyle Width="34%" />
                                            </asp:BoundField>
                                         
                                         
                                             <asp:BoundField DataField="ProductModelSpecification" HeaderStyle-HorizontalAlign="Left" HeaderText="Model Specification"  SortExpression="ProductModelName" ItemStyle-Width="5%">
                                                <ItemStyle Width="34%" />
                                            </asp:BoundField>
                                           
                                            <asp:BoundField DataField="ModelStartDate" HeaderStyle-HorizontalAlign="Left" HeaderText="Start Date"
                                                ItemStyle-Width="8%" DataFormatString="{0:d-MMM-yyyy}" SortExpression="ModelStartDate">
                                                <ItemStyle Width="8%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ModelRetiredDate" HeaderStyle-HorizontalAlign="Left" HeaderText="Retired Date"
                                                ItemStyle-Width="8%" DataFormatString="{0:d-MMM-yyyy}" SortExpression="ModelRetiredDate">
                                                <ItemStyle Width="8%" />
                                            </asp:BoundField>
                                                 <asp:BoundField DataField="Active" SortExpression="Active" HeaderStyle-HorizontalAlign="Left" HeaderText="Active" ItemStyle-Width="4%">
                                                <ItemStyle Width="4%" />
                                            </asp:BoundField>
                                            <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderStyle-HorizontalAlign="Left" HeaderText="Edit" ImageUrl="~/Account/images/edit.gif" Text="Button" HeaderImageUrl="~/Account/images/edit.gif" >
                                             <ItemStyle Width="3%" />
                                            </asp:ButtonField>
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
 <asp:PostBackTrigger ControlID="btnfu10_bigright" />
<asp:PostBackTrigger ControlID="btnfu11_smallleft"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnfu12_bigleft"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnfu1_smallfront"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnfu2_bigfront"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnfu3_smallback"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgfu4_bigback"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnfu5_smalltop"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnfu6_bigtop"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnfu7_smallbottom"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnfu8_bigbottom"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnfu9_smallright"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnfu10_bigright"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnfu11_smallleft"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnfu12_bigleft"></asp:PostBackTrigger>


<asp:PostBackTrigger ControlID="btnUpload"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnUpload2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button15"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button14"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button13"></asp:PostBackTrigger>


 </Triggers>
    </asp:UpdatePanel>
</asp:Content>
