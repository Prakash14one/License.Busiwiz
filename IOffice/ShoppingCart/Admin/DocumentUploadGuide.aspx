<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentUploadGuide.aspx.cs" Inherits="DocumentUploadGuide"
    Title="Untitled Page" %>

<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
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
                
            
              alert("You have entered invalid character");
                  return false;
             }
             
             
               
            
        }  
        function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend></legend>
                    <label>
                        There are many ways to add documents to your online filing system.
                    </label>
                    <div style="clear: both;">
                    </div>
                     <div style="clear: both;">
                    </div>
                    <label>
                        1) If you wish to upload any documents, please click <a href="DocumentFastUpload.aspx" target="_blank">here.
                        </a>
                    </label>
                     <div style="clear: both;">
                    </div>
                     <label>
                        2) If you would like to set up an auto-document download directly from your FTP account, please click  <a href="Down.aspx?Id=1" target="_blank">here.
                        </a>
                    </label>
                     <div style="clear: both;">
                    </div>
                     <label>
                        3) If you would like to set up an auto-document download directly from email attachments, please click <a href="Down.aspx?Id=2" target="_blank">here.
                        </a>
                    </label>
                     <div style="clear: both;">
                    </div>
                     <label>
                        4) If you have your web folders on our server and you wish to set up an auto-document download from your web folders, please click <a href="Down.aspx?Id=3" target="_blank">here.
                        </a>
                    </label>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
