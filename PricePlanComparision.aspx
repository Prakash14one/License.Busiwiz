<%@ Page Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true"
    CodeFile="PricePlanComparision.aspx.cs" Inherits="PricePlanComparision" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    
    <style type="text/css">
        .borderbottom
        {
            border-bottom-style: ridge;
        }
        
        .borderbottomselect
        {
            border-bottom-style: ridge;
            background-color:#f2f2f2;
        }
        
        .fontdesc1 {
       font-family: Verdana;
		font-size: 18px;	
	    color:Black;  
	    text-align:left;  
	    padding-left:20px;
	    padding-top:20px;
	    font-weight:400; 
    }
    
    .fontdesc {
       font-family: Verdana;
		font-size: 12px;	
	    color:Black;  
	    text-align:right;  
    }
    
     .fontlink {
       font-family: Verdana;
		font-size: 12px;	
	    color:Red;  
	    text-align:right;  
    }
     .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }
    .heading {
        font-family: times, Times New Roman, times-roman, georgia, serif;
		font-size: 28px;
		line-height: 40px;
		letter-spacing: -1px;color: #444;
    }
     .fonts {
        font-family: times, Times New Roman, times-roman, georgia, serif;
		font-size: 18px;
	background-color:#488ac7;
	color:White;  
	text-align:left;  
    }
     .font1 {        
        font-family:Arial;
		font-size: 16px;
		
	background-color:#FFF;
	color:Gray;
	text-align:left;    
    }
    
    .BTNTRANS{
     background-color: Transparent;
            background-repeat:no-repeat;
            border: none;
            cursor:pointer;
            overflow: hidden;
    }
    .BTNTorder{
     background-color:#FDB53C;
          font: bold 11px Arial;
  text-decoration: none;
  color: #333333;
  padding: 1px 6px 1px 6px;
  border-top: 1px solid #CCCCCC;
  border-right: 1px solid #333333;
  border-bottom: 1px solid #333333;
  border-left: 1px solid #CCCCCC;
    }
	.TFtableCol{
		width:100%; 
		border-collapse:collapse; 
	}
	.TFtableCol td{ 
		padding:7px; border:#4e95f4 1px solid;
	}
	/* improve visual readability for IE8 and below */
	.TFtableCol tr{
		background: #b8d1f3;
	}
	/*  Define the background color for all the ODD table columns  */
	.TFtableCol tr td:nth-child(odd){ 
		background: #b8d1f3;
	}
	/*  Define the background color for all the EVEN table columns  */
	.TFtableCol tr td:nth-child(even){
		background: #dae5f4;
	}
	.pnlBackGround
{
 position:fixed;
    top:10%;
    left:10px;
    width:300px;
    height:125px;
    text-align:center;
    background-color:White;
    border:solid 3px black;
        }
        .style1
        {
            width: 100%;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {

            var div = document.getElementById(divname);

            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "Images/minus.png";
            } else {
                div.style.display = "none";
                img.src = "Images/plus.png";
            }
        }

        function divexpandcollapseChild(divname) {
            var div1 = document.getElementById(divname);

            var img = document.getElementById('img' + divname);
            if (div1.style.display == "none") {
                div1.style.display = "inline";
                img.src = "images/minus.png";
            } else {
                div1.style.display = "none";
                img.src = "images/plus.png"; ;
            }
        }
    </script>

    <div id="right_content">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <label>
                    <asp:Label ID="lblmsg" runat="server" Text="" Font-Size="15px"  ForeColor="Red"></asp:Label>
                </label> 
                 <asp:Panel ID="pnl_step1" runat="server">
     <h2 style="text-align:center;"class="PortalBackgroudColor">
            Select Price Plan
      </h2>
       
        <div class="products_box">      
        <asp:Panel ID="pnlpl" runat="server" Visible="false">
            <fieldset>              
                <legend>Choose your Price Plan here and Order Now </legend>
                <label class="first" for="title1">
                    Select Product/Version Name :
                </label>
                <label for="firstName1">
                    <asp:DropDownList ID="ddlproduct" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
                <label class="first" for="title1">
                    Portal Name :
                </label>
                <label for="firstName1">
                    <asp:DropDownList ID="ddlportal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlportal_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
            </fieldset>
            </asp:Panel>
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend>Choose you Price Plan here and Order Now</legend>
                <table width="100%">                   
                    <tr>
                        <td>
                            <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" CssClass="mGridcss"
                              HeaderStyle-BackColor="#65bbd2" 
                                                         HeaderStyle-ForeColor="White"  PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="Id"
                                OnRowDataBound="OnRowDataBound" Width="100%">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Panel ID="pnlA" runat="server" Visible="false">
                                                <a href="JavaScript:divexpandcollapse('div<%# Eval("Fid") %>');">
                                                    <img id="imgdiv<%# Eval("Fid") %>" src="images/plus.png" alt="" /></a>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblhcead1" runat="server" Text="Priceplan List" Font-Size="20px"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblcb" runat="server" Text='<%# Bind("Name") %>' Font-Bold="true"></asp:Label>                                             
                                            <asp:Label ID="lblfeid" runat="server" Text='<%# Bind("Fid") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblhead1" runat="server" Text="Free"></asp:Label>
                                            <asp:Label ID="lblhead1id" runat="server" Visible="false"></asp:Label>
                                           <%--  <a id="HR11" runat="server" target="_blank" Visible="false">
                                                <asp:Label ID="lbla11" runat="server" Text="Buy Now" ForeColor="Black" Visible="false"></asp:Label>
                                            </a>--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Image ID="imghead1" runat="server" ImageUrl="~/images/Lright.jpg" Visible="false" />
                                            <a id="HR1" runat="server" target="_blank" visible="false">
                                                <asp:Label ID="lbla1" runat="server" Text="Buy Now" ForeColor="Black" Visible="false"></asp:Label>
                                                </a>
                                                <asp:Button ID="btnBuySelectSer1" runat="server" Visible="false" Text="Buy Now" CssClass="BTNTRANS" OnClick="btnBuySelectSer1_Click" />
                                                <asp:Label ID="lblpid1" runat="server" Text="" ForeColor="Black" Visible="false"></asp:Label>

                                            <asp:Button ID="ddlhead1" runat="server" Visible="false" Text="Customise" CssClass="btnSubmitm" OnClick="ddlhead1_Click" />
                                            <asp:Button ID="Button1" runat="server" Visible="false" Text="More Info" CssClass="BTNTRANS" OnClick="btnmoreinfo1_Click" />
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblhead2" runat="server" Text="Basic"></asp:Label>
                                            <asp:Label ID="lblhead2id" runat="server" Visible="false"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Image ID="imghead2" runat="server" ImageUrl="~/images/Lright.jpg" alt="" Visible="false" />
                                            <a id="HR2" runat="server" target="_blank"  visible="false">
                                                <asp:Label ID="lbla2" runat="server" Text="Buy Now"  ForeColor="Black" Visible="false"></asp:Label>
                                                </a>
                                                <asp:Button ID="btnBuySelectSer2" runat="server" Visible="false" Text="Buy Now" CssClass="BTNTRANS" OnClick="btnBuySelectSer2_Click" />
                                                  <asp:Label ID="lblpid2" runat="server" Text="" ForeColor="Black" Visible="false"></asp:Label>
                                            <asp:Button ID="ddlhead2" runat="server" Visible="false"  Text="Customise" CssClass="btnSubmitm" OnClick="ddlhead2_Click" />
                                            <asp:Button ID="Button2" runat="server" Visible="false" Text="More Info" CssClass="BTNTRANS" OnClick="btnmoreinfo2_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblhead3" runat="server" Text="More Basic"></asp:Label>
                                            <asp:Label ID="lblhead3id" runat="server" Visible="false"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Image ID="imghead3" runat="server" ImageUrl="~/images/Lright.jpg" alt="" Visible="false" />
                                            <a id="HR3" runat="server" target="_blank" visible="false">
                                                <asp:Label ID="lbla3" runat="server" Text="Buy Now" ForeColor="Black" Visible="false"></asp:Label>
                                                </a>
                                                <asp:Button ID="btnBuySelectSer3" runat="server" Visible="false" Text="Buy Now" CssClass="BTNTRANS" OnClick="btnBuySelectSer3_Click" />
                                                  <asp:Label ID="lblpid3" runat="server" Text="" ForeColor="Black" Visible="false"></asp:Label>
                                            <asp:Button ID="ddlhead3" runat="server" Visible="false" Text="Customise" CssClass="btnSubmitm" OnClick="ddlhead3_Click" />
                                            <asp:Button ID="Button3" runat="server" Visible="false" Text="More Info" CssClass="BTNTRANS" OnClick="btnmoreinfo3_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblhead4" runat="server" Text="Intermediate"></asp:Label>
                                            <asp:Label ID="lblhead4id" runat="server" Visible="false"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Image ID="imghead4" runat="server" ImageUrl="~/images/Lright.jpg" alt="" Visible="false" />
                                            <a id="HR4" runat="server" target="_blank" visible="false">
                                                <asp:Label ID="lbla4" runat="server" Text="Buy Now" ForeColor="Black" Visible="false"></asp:Label>
                                                </a>
                                                <asp:Button ID="btnBuySelectSer4" runat="server" Visible="false" Text="Buy Now" CssClass="BTNTRANS" OnClick="btnBuySelectSer4_Click" />
                                                  <asp:Label ID="lblpid4" runat="server" Text="" ForeColor="Black" Visible="false"></asp:Label>
                                                  
                                            <asp:Button ID="ddlhead4" runat="server" Visible="false" Text="Customise" CssClass="btnSubmitm" OnClick="ddlhead4_Click" />
                                            <asp:Button ID="Button4" runat="server" Visible="false" Text="More Info" CssClass="BTNTRANS" OnClick="btnmoreinfo4_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblhead5" runat="server" Text="More<br> Intermediate"></asp:Label>
                                            <asp:Label ID="lblhead5id" runat="server" Visible="false"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Image ID="imghead5" runat="server" ImageUrl="~/images/Lright.jpg" alt="" Visible="false" />
                                            <a id="HR5" runat="server" target="_blank" visible="false">
                                                <asp:Label ID="lbla5" runat="server" Text="Buy Now" ForeColor="Black" Visible="false"></asp:Label>
                                             </a>
                                             <asp:Label ID="lblpid5" runat="server" Text="" ForeColor="Black" Visible="false"></asp:Label>
                                            <asp:Button ID="btnBuySelectSer5" runat="server" Visible="false" Text="Buy Now" CssClass="BTNTRANS" OnClick="btnBuySelectSer5_Click" />
                                            <asp:Button ID="ddlhead5" runat="server" Visible="false" Text="Customise" CssClass="btnSubmitm" OnClick="ddlhead5_Click" />
                                            <asp:Button ID="Button5" runat="server" Visible="false" Text="More Info" CssClass="BTNTRANS" OnClick="btnmoreinfo5_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblhead6" runat="server" Text="Premium"></asp:Label>
                                            <asp:Label ID="lblhead6id" runat="server" Visible="false"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Image ID="imghead6" runat="server" ImageUrl="~/images/Lright.jpg" alt="" Visible="false" />
                                            <a id="HR6" runat="server" target="_blank" visible="false">
                                                <asp:Label ID="lbla6" runat="server" Text="Buy Now" ForeColor="Black" Visible="false"></asp:Label>
                                                </a>
                                                <asp:Label ID="lblpid6" runat="server" Text="" ForeColor="Black" Visible="false"></asp:Label>
                                                <asp:Button ID="btnBuySelectSer6" runat="server" Visible="false" Text="Buy Now" CssClass="BTNTRANS" OnClick="btnBuySelectSer6_Click" />
                                            <asp:Button ID="ddlhead6" runat="server" Visible="false" Text="Customise" CssClass="btnSubmitm" OnClick="ddlhead6_Click" />
                                            <asp:Button ID="Button6" runat="server" Visible="false" Text="More Info" CssClass="BTNTRANS" OnClick="btnmoreinfo6_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblhead7" runat="server" Text="Extra Premium"></asp:Label>
                                            <asp:Label ID="lblhead7id" runat="server" Visible="false"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Image ID="imghead7" runat="server" ImageUrl="~/images/Lright.jpg" alt="" Visible="false" />
                                            <a id="HR7" runat="server" target="_blank" visible="false">
                                                <asp:Label ID="lbla7" runat="server" Text="Buy Now" ForeColor="Black" Visible="false"></asp:Label>
                                                </a>
                                                <asp:Label ID="lblpid7" runat="server" Text="" ForeColor="Black" Visible="false"></asp:Label>
                                                <asp:Button ID="btnBuySelectSer7" runat="server" Visible="false" Text="Buy Now" CssClass="BTNTRANS" OnClick="btnBuySelectSer7_Click" />
                                            <asp:Button ID="ddlhead7" runat="server" Text="Customise" Visible="false" CssClass="btnSubmitm" OnClick="ddlhead7_Click" />
                                            <asp:Button ID="Button7" runat="server" Visible="false" Text="More Info" CssClass="BTNTRANS" OnClick="btnmoreinfo7_Click" />                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                </td>
                                                <td colspan="<%=discat+1 %>">
                                                    <div id="div<%# Eval("Fid") %>" style="overflow: auto; display: none; position: relative;
                                                        overflow: auto">
                                                        <asp:GridView ID="gvOrders" runat="server" ShowHeader="false" CssClass="mGridcss" PagerStyle-CssClass="pgr" AutoGenerateColumns="false" DataKeyNames="Id">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfe1" runat="server" Text='<%# Bind("priceplanfeaturenote") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="imgfree1" runat="server" ImageUrl="~/images/Wrong.gif" alt="" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="imgfree2" runat="server" ImageUrl="~/images/Wrong.gif" alt="" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="imgfree3" runat="server" ImageUrl="~/images/Wrong.gif" alt="" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="imgfree4" runat="server" ImageUrl="~/images/Wrong.gif" alt="" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="imgfree5" runat="server" ImageUrl="~/images/Wrong.gif" alt="" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="imgfree6" runat="server" ImageUrl="~/images/Wrong.gif" alt="" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="imgfree7" runat="server" ImageUrl="~/images/Wrong.gif" alt="" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:GridView ID="grdnoof" runat="server" ShowHeader="false" CssClass="mGridcss"
                                                            PagerStyle-CssClass="pgr" AutoGenerateColumns="false" DataKeyNames="Id">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfe1" runat="server" Text='<%# Bind("NameofRest") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl1" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl2" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl3" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl4" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl5" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl6" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl7" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                     <tr>
                        <td valign="top">
                            <asp:Panel ID="pnlcustomise" runat="server" ScrollBars="Horizontal" Width="970px"  Visible="false">
                                <asp:DataList ID="datalp1" runat="server" ShowFooter="False" ShowHeader="False" RepeatDirection="Horizontal"  DataKeyField="Id" RepeatLayout="Table">
                                <%--OnDataBinding="datalp1databinding" --%> 
                                    <ItemTemplate>
                                        <asp:Panel ID="pnlri" runat="server" BorderStyle="Ridge" BorderWidth="1px">
                                            <table cellpadding="1" cellspacing="1">
                                                <tr>
                                                    <td valign="top" style="padding-right: 05px; margin-top: 0px;"    >
                                                        <asp:Button ID="btndata" runat="server" Height="35px" Text='<%#DataBinder.Eval(Container.DataItem, "NameofRest")%>'  OnClick="lblworkho_Click1" CssClass="btnSubmitm" />
                                                         <asp:Button ID="Button1" runat="server" Height="25px" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "PriceplancatId")%>'  OnClick="lblworkho_Click1" CssClass="btnSubmitm" />
                                                    </td>
                                                    <td>
                                                    <asp:Label ID="lblpricecatid" runat="server" Text='<%# Bind("PriceplancatId") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblnorec" Visible="false" runat="server"> </asp:Label>
                                                        <asp:Label ID="lblval" Visible="false" runat="server"> </asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:DataList>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlData" runat="server" Visible="false" BorderWidth="1px" BorderStyle="Solid">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="lbltextmsg" Font-Size="14px" runat="server" 
                                                ></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:DropDownList ID="ddlrecords" runat="server" Width="100px" Visible="true">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:Button ID="btnnext" runat="server" Text="Next" CssClass="btnSubmitm" OnClick="btnnext_Click" />
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:Button ID="btngo" runat="server" Text="Buy Now"  CssClass="btnSubmitm" OnClick="btngo_Click" />
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                     <asp:Panel ID="pnlpopup" runat="server" ScrollBars="Horizontal"  Visible="false"  background="rgba(0, 0, 0, 0.7)" transition="opacity 500000ms">
                     <tr>
                        <td valign="top" align="center">
                            
                                       <div style="position: absolute; margin:-250px 0px 0px 0px; height:auto; width: auto;  background: #fff; border-style: solid; transition: opacity 500ms;" class="Box">   
                                         <div align="right" style="font-family:Arial;font-size:15px;font-weight:bold;">
                                   <asp:Label ID="lblheaderpop" runat="server" Text="What is Included?"></asp:Label>
                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;      
                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/BAckpo.jpg" runat="server" Visible="false"   Width="24px" OnClick="ImageButton1_Click" />
                                    <asp:ImageButton ID="ImageButton5" ImageUrl="~/images/closeicon.jpeg" runat="server"  Width="24px" OnClick="ImageButton2_Click" />
                                     <table>
                                     <tr align="center">
                                     <td align="center" valign="middle">
                                       <asp:GridView ID="gvpopup" runat="server" ShowHeader="false" CssClass="mGridcss" PagerStyle-CssClass="pgr" AutoGenerateColumns="false" DataKeyNames="Id" onrowcommand="gvpopup_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfe1" runat="server" Text='<%# Bind("NameofRest1") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="">
                                                                    <ItemTemplate>
                                                                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("RecordsAllowed") %>'></asp:Label>
                                                                         <asp:Label ID="lblid" runat="server" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                                                         <asp:Label ID="lblcid" runat="server" Text='<%# Bind("portalid") %>' Visible="false"></asp:Label>
                                                                         
                                                                         <asp:Label ID="lblnorec" Visible="false" runat="server"> </asp:Label>
                                                                             <asp:Label ID="lblval" Visible="false" runat="server"> </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="">
                                                                    <ItemTemplate>
                                                                          <asp:button id="Button2" cssclass="BTNTRANS" runat="server" text="Change" CommandName="GetRow" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                             
                                                            </Columns>
                                                        </asp:GridView>                                                       
                                     </td>
                                     </tr>
                                     <tr>
                                     <td>
                                     <table>
                                         <asp:Panel ID="pnl1search" runat="server" Visible="false">
                                         
                                         
                                            <tr>
                                             <td>
                                                  <label>
                                                        <asp:Label ID="Label48" runat="server" Text="Priceplan Category"></asp:Label>
                                                        <asp:Label ID="Label49" runat="server" CssClass="labelstar" Text="*"></asp:Label>                               
                                                   </label>
                                            </td>
                                            <td>
                                               <label>
                                                    <asp:DropDownList ID="ddlpriceplancatagory" runat="server"  AutoPostBack="True"  onselectedindexchanged="ddlpriceplancatagory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                             </td>
                                             <td>
                                             </td>
                                             <td>
                                              
                                             </td>
                                             <td></td>
                                            </tr> 
                                            <tr>
                                            <td style="font:14px; ">
                                            <asp:Button ID="Button9" runat="server" Text="Filtet  Show plans with" CssClass="BTNTRANS" OnClick="btnnext8sea_Click"  />
                                            </td>
                                            </tr>
                                              <asp:Panel ID="pnl2search" runat="server" Visible="false">
                                            <tr>
                                            <td></td>
                                             <td>
                                                  <label>
                                                         <asp:DropDownList ID="DDLRest1" runat="server"  AutoPostBack="false" >
                                                    </asp:DropDownList>                             
                                                   </label>
                                            </td>
                                            <td>
                                               <label style="width:80px;">
                                                    <asp:DropDownList ID="DDLrescomp1" runat="server"  AutoPostBack="false" Width="80px">
                                                        <asp:ListItem Value=">">More than</asp:ListItem>
                                                        <asp:ListItem Value="<">Less than</asp:ListItem>
                                                    </asp:DropDownList>   
                                                </label>
                                               
                                             </td>
                                             <td>
                                              <label style="width:50px;">
                                                Amount
                                                </label> 
                                             </td>
                                             <td>
                                             <label style="width:50px;">
                                                    <asp:TextBox ID="txtrestam1" MaxLength="4"  runat="server" Width="40px" ></asp:TextBox>
                                             </label> 
                                             </td>
                                            </tr> 
                                             <tr>
                                             <td></td>
                                             <td>
                                                  <label>
                                                         <asp:DropDownList ID="DDLrest2" runat="server"  AutoPostBack="false">
                                                         
                                                        </asp:DropDownList>                             
                                                   </label>
                                            </td>
                                            <td>
                                               <label style="width:80px;">
                                                    <asp:DropDownList ID="DDLrescomp2" runat="server"  AutoPostBack="false" Width="80px">
                                                         <asp:ListItem Value=">">More than</asp:ListItem>
                                                        <asp:ListItem Value="<">Less than</asp:ListItem>
                                                    </asp:DropDownList>   
                                                </label>
                                                 
                                             </td>
                                              <td>
                                              <label style="width:50px;">
                                                Amount
                                                </label> 
                                             </td>
                                             <td>
                                             <label style="width:50px;">
                                                    <asp:TextBox ID="txtrestam2" MaxLength="4"  runat="server" Width="40px" ></asp:TextBox>
                                             </label> 
                                             </td>
                                            </tr> 
                                             <tr>
                                             <td></td>
                                             <td>
                                                  <label>
                                                         <asp:DropDownList ID="DDLrest3" runat="server"  AutoPostBack="false" >
                                                         
                                                    </asp:DropDownList>                             
                                                   </label>
                                            </td>
                                            <td>
                                               <label style="width:80px;">
                                                    <asp:DropDownList ID="DDLrescomp3" runat="server"  AutoPostBack="false" Width="80px">
                                                     <asp:ListItem Value=">">More than</asp:ListItem>
                                                        <asp:ListItem Value="<">Less than</asp:ListItem>
                                                    </asp:DropDownList>   
                                                </label>
                                                 
                                             </td>
                                              <td>
                                              <label style="width:50px;">
                                                Amount
                                                </label> 
                                             </td>
                                             <td>
                                             <label style="width:50px;">
                                                    <asp:TextBox ID="txtrestam3" MaxLength="4"  runat="server" Width="40px" ></asp:TextBox>
                                             </label> 
                                             </td>
                                            </tr> 
                                             <tr>
                                             <td></td>
                                             <td>
                                                  <label>
                                                         <asp:DropDownList ID="DDLrest4" runat="server"  AutoPostBack="false" >
                                                         
                                                    </asp:DropDownList>                             
                                                   </label>
                                            </td>
                                            <td>
                                              <label style="width:80px;">
                                                    <asp:DropDownList ID="DDLrescomp4" runat="server"  AutoPostBack="false" Width="80px">
                                                     <asp:ListItem Value=">">More than</asp:ListItem>
                                                        <asp:ListItem Value="<">Less than</asp:ListItem>
                                                    </asp:DropDownList>   
                                                </label>
                                                
                                             </td>
                                              <td>
                                              <label style="width:50px;">
                                                Amount
                                                </label> 
                                             </td>
                                             <td>
                                             <label style="width:50px;">
                                                    <asp:TextBox ID="txtrestam4" MaxLength="4"  runat="server" Width="40px" ></asp:TextBox>
                                             </label> 
                                             </td>
                                            </tr> 
                                           
                                            <tr>
                                            <td>
                                            <asp:Button ID="Button8" runat="server" Text="Search" CssClass="btnSubmitm" OnClick="btnnext8_Click" />
                                            </td>
                                            </tr>
                                             </asp:Panel>
                                           </asp:Panel>
                                     </table> 
                                     </td>
                                     </tr>
                                     <tr>
                                     <td align="center" valign="middle">
                                      <asp:GridView ID="gvallrest" runat="server" ShowHeader="false" CssClass="mGridcss" PagerStyle-CssClass="pgr" OnRowDataBound="gvallrest_RowCommand" AutoGenerateColumns="false" DataKeyNames="ID"  onrowcommand="gvallrest_RowCommand">
                                                            <Columns>
                                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Price plan Category">
                                                                    <ItemTemplate>
                                                                 <div class="fonts">Category Name</div>   
                                                                        <asp:Label ID="lblfev1" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                                                        <%--  <asp:Label ID="lblpid" runat="server" Text='<%# Bind("pid") %>' Visible="false"></asp:Label>--%>
                                                                         <asp:Label ID="lblcid" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField>
                                                               
                                                                <ItemTemplate>
                                                                   <tr>
                                                                     <td>
                                                                          </td>
                                                                              <td >
                                                                           <div id="div<%# Eval("id") %>" style="overflow: auto; position: relative; overflow: auto">
                                                        <asp:GridView ID="gvOrdersPRICEPLN" runat="server" ShowHeader="true" CssClass="mGridcss"
                                                       onrowcommand="gvOrdersPRICEPLN_RowCommand"  OnRowDataBound="gvOrdersPRICEPLN_RowCommand"    PagerStyle-CssClass="pgr" AutoGenerateColumns="false" DataKeyNames="Id">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Price plan Name">
                                                                    <ItemTemplate>
                                                                         
                                                                        <asp:Label ID="lblfe1" runat="server" Text='<%# Bind("PricePlanName") %>'></asp:Label>
                                                                                    <asp:Label ID="lblpid" runat="server" Text='<%# Bind("pid") %>' Visible="false"></asp:Label>
                                                                         <asp:Label ID="lblcid" runat="server" Text='<%# Bind("cid") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 
                                                               
                                                               <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText=" Restriction">
                                                                    <ItemTemplate>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfcxe1" runat="server" Text='<%# Bind("PricePlanAmount") %>'></asp:Label>
                                                                       
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="">
                                                                    <ItemTemplate>
                                                                          <asp:button id="Button2" cssclass="BTNTorder" runat="server" text="Buy Now" CommandName="GetRowBuy" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                <ItemTemplate>
                                                                   <tr>
                                                                     <td>
                                                                          </td>
                                                                              <td >
                                                                           <div id="div<%# Eval("cid") %>" style="overflow: auto; position: relative;  overflow: auto">
                                                        <asp:GridView ID="gvOrdersRESTRIC" runat="server" ShowHeader="false" CssClass="mGridcss"
                                                           PagerStyle-CssClass="pgr" AutoGenerateColumns="false" DataKeyNames="pid">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfe1" runat="server" Text='<%# Bind("NameofRest") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Records Allowed">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfe1" runat="server" Text='<%# Bind("RecordsAllowed") %>'></asp:Label>
                                                                       
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:GridView ID="grdnoof1" runat="server" ShowHeader="false" CssClass="mGridcss"
                                                            PagerStyle-CssClass="pgr" AutoGenerateColumns="false" DataKeyNames="CId">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfe1" runat="server" Text='<%# Bind("NameofRest") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                       
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                                  </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:GridView ID="grdnoof" runat="server" ShowHeader="false" CssClass="mGridcss"
                                                           PagerStyle-CssClass="pgr" AutoGenerateColumns="false" DataKeyNames="Id">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfe1" runat="server" Text='<%# Bind("NameofRest") %>'></asp:Label>
                                                                        <asp:Label ID="lblpid" runat="server" Text='<%# Bind("pid") %>' Visible="false"></asp:Label>
                                                                         <asp:Label ID="lblcid" runat="server" Text='<%# Bind("cid") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl1" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl2" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl3" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl4" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl5" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl6" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl7" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                                  </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                </asp:TemplateField>
                                                              
                                                            </Columns>
                                        </asp:GridView>


                                       <asp:DataList ID="DataList1" runat="server" ShowFooter="False" ShowHeader="False" RepeatDirection="Vertical"  DataKeyField="Id" RepeatLayout="Table">
                                    <ItemTemplate>
                                        <asp:Panel ID="pnlri" runat="server" BorderStyle="Ridge" BorderWidth="1px">
                                            <table cellpadding="0" cellspacing="0" class="TFtableCol">
                                                <tr>
                                                    
                                                    <td valign="top" style="padding-right: 02px; margin-top: 0px; width:350px;">
                                                       
                                                        <asp:Label ID="lblfe1" runat="server" Text='<%# Bind("NameofRest1") %>'></asp:Label>
                                                         <asp:Button ID="btndata" runat="server" Height="45px" CssClass="btnSubmitm" Visible="false"  Text='<%#DataBinder.Eval(Container.DataItem, "NameofRest")%>'  /> <%-- OnClick="lblworkho_Click1"--%>
                                                    </td>
                                                    <td align="right" style="width:35px;">
                                                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("RecordsAllowed") %>'></asp:Label>
                                                        <asp:Label ID="lblnorec" Visible="false" runat="server"> </asp:Label>
                                                        <asp:Label ID="lblval" Visible="false" runat="server"> </asp:Label>
                                                    </td>
                                                    <td>
                                                    <asp:Button ID="Button1" runat="server"  Text="Change" CssClass="BTNTRANS" OnClick="btnmoreinfo1_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:DataList>
                                     </td>
                                     </tr>
                                     </table> 
                                  </div>                             
                            </td>
                            </tr>
                              </asp:Panel>
                </table>
            </fieldset>
        </div>
        </asp:Panel>
        <div align="center">	
            </div>
          <div id="cover">
          </div>





 <asp:Panel ID="pnl_step2" runat="server" Visible="false">   
        <asp:Panel ID="pnlshowShared" runat="server">   
         <div class="products_box">                   
        <%--CssClass="pnlBackGround" BackColor="White" BorderColor="#999999"  ScrollBars="Vertical" BorderStyle="Solid" BorderWidth="2px" Height="70%"  Width="65%" --%>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                                <table width="100%">
                                <tr>
                                <td>
                                     <fieldset>
                                            <table width="100%">
                            <tr>
                            <td colspan="3">                                                
                                <h2 style="text-align:center;"class="PortalBackgroudColor">
                                    <asp:Label ID="Label21" runat="server" Text="Your Priceplan details" style="font-family:Arial;font-size:15px;"> </asp:Label>
                                </h2>
                            </td>                                               
    
                            </tr>  
                            <tr>                                                                                                      
                            <td align="left" colspan="3">
                                  <label>   <asp:Label ID="lblportal" runat="server" Text=""></asp:Label>                                                                                                            
                                  </label>
                            </td>                  
                            </tr>
                            <tr>                                                                                                      
                            <td align="left" colspan="3">
                                  <label>   <asp:Label ID="lblPPname" runat="server" Text=""></asp:Label> 
                                        <asp:Label ID="lblpopupPPID" runat="server" Text="  " Visible="false" > </asp:Label>                                                                                                           
                                        </label>
                            </td>                                                                                                       
                            </tr>
                            <tr>
                            <td colspan="3"  align="left">
                            <label>
                                 <asp:Label ID="lblPPamt" runat="server" Text=""></asp:Label>
                                 </label>
                            </td> 
                            </tr>
                            <tr>
                                <td align="left" colspan="3"> 
                                    <label>
                                    <asp:Label ID="lblportalCat_subType" runat="server" Text=""></asp:Label>                                                                                                            
                                    <asp:Label ID="lblportalCat_subTypeid" runat="server" Text="" Visible="false"></asp:Label>                                                                                                            
                                    </label> 
                                </td>                                                                                  
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_hostingPortalLink" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_hostingportalid" runat="server" Text="" Visible="false"></asp:Label>
                                      <asp:Label ID="lbl_hosting_productid" runat="server" Text="" Visible="false"></asp:Label>
                                       <asp:Label ID="lbl_portalname" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            </table>

                                        <table width="100%">
        <tr>
            <td>
                 <asp:Panel ID="pnlChkServerType" runat="server" Visible="true">
                    <fieldset>
                    <table width="98%" style="text-align:left;">                                                                                          
                        <tr>
                            <td colspan="3">
                            <h2 style="text-align:center;"class="PortalBackgroudColor">
                                    <asp:Label ID="Label2" runat="server" style="font-family:Arial;font-size:15px;" Text="Hosting Options  where you will be ale to host your portal you are planning to subscribe" > </asp:Label>
                                    </h2>
                            </td>                                                
                        </tr>
                        <tr>
                        <td colspan="3">
                                <asp:Label ID="lblsorry" runat="server" Text=" "  Visible="false"  style="font-family:Arial;font-size:12px;font-weight:bold;color:Red;"> </asp:Label>                                                                                            
                        </td>
                        </tr>
                     </table>
                    
                        
                           <asp:Panel ID="Panel1" runat="server" Visible="true" class="borderbottom">
                            <table width="100%">
                                <tr>
                                    <td colspan="3">                                                                                                                                                                                                                                                                                                                                                                                                                            
                                        <asp:CheckBox ID="chk_ownserver" runat="server" AutoPostBack="True" OnCheckedChanged="FiveServerTypeSelect1_CheckedChanged"                                                                              
                                            Text="Hosting at your own PC / Server (See requirements) at your location."  />                                                
                                    </td>                                                                                              
                                </tr> 
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_ownserver1" runat="server" CssClass="fontdesc"   Text=""></asp:Label>                                              
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">                                       
                                                  <asp:LinkButton ID="Linkbtn_ownserver1" OnClick="Linkbtn1_OnClick"  CssClass="fontlink"  runat="server" Text="Price plan starts from $ xyz per xyz month" ></asp:LinkButton>                                                                                                                                                                            
                                    </td>
                                </tr>
                             </table>
                           </asp:Panel>  
                           <asp:Panel ID="Panel2" runat="server" Visible="true" class="borderbottom">     
                              <table width="100%" >
                                <tr>
                                    <td colspan="3">
                                     <asp:CheckBox ID="chk_CommonServerAllow" runat="server" AutoPostBack="True"  OnCheckedChanged="FiveServerTypeSelect2_CheckedChanged"                                  
                                          CssClass="fontdesc1" Text="Hosting at our secured server." />      
                                </td>                                                                                              
                                </tr>
                                <tr>
                                    <td>
                                         <asp:Label ID="lbl_CommonServerAllow2" runat="server" CssClass="fontdesc" Text=""></asp:Label>  
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">                                        
                                                  <asp:LinkButton ID="Linkbtn_CommonServerAllow2" OnClick="Linkbtn2_OnClick"   CssClass="fontlink"  runat="server" Text="Price plan starts from $ xyz per xyz month" ></asp:LinkButton>                                                
                                    </td>
                                </tr>
                              </table>   
                          </asp:Panel>
                        <asp:Panel ID="Panel3" runat="server" Visible="true" class="borderbottom">     
                              <table  width="100%">
                                 <tr>
                                     <td colspan="3"> 
                                         <asp:CheckBox ID="chk_LeaseServerAllow" runat="server" AutoPostBack="True"  OnCheckedChanged="FiveServerTypeSelect3_CheckedChanged"                                  
                                            CssClass="fontdesc1" Text="Hosting on highly secured server leased exclusively to you and not shared with any other company." />                                                                              
                                        
                                   </td>
                                  </tr>
                                  <tr>
                                    <td>                                            
                                                 <asp:Label ID="lbl_LeaseServerAllow3" runat="server" CssClass="fontdesc" Text=""></asp:Label>                                                
                                    </td>
                                  </tr>
                                    <tr>
                                    <td align="right">                                        
                                                  <asp:LinkButton ID="Linkbtn_LeaseServerAllow3" OnClick="Linkbtn3_OnClick"  CssClass="fontlink"   runat="server" Text="Price plan starts from $ xyz per xyz month" ></asp:LinkButton>                                              
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel4" runat="server" Visible="true" class="borderbottom">     
                          <table width="100%">                                                             
                             <tr>
                                 <td colspan="3">  
                                     <asp:CheckBox ID="Chk_SharedServerAllow" runat="server" AutoPostBack="True"  OnCheckedChanged="FiveServerTypeSelect4_CheckedChanged"                                  
                                        CssClass="fontdesc1"    Text="Hosting on highly secured server leased to you but shared with other companies." />                                                                                                                                                  
                                      <asp:Label ID="lbl_SharedServerAllowRemove" runat="server" Text="(Only sites required for all sharing companies would be hosted on server. It will be considerably higher in performance and security compared to common server options)" Visible="false" ></asp:Label>
                                 </td>                                 
                             </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lbl_SharedServerAllow4" runat="server" CssClass="fontdesc" Text=""></asp:Label>   
                                </td>
                             </tr>
                             <tr>
                                    <td align="right">                                        
                                           <asp:LinkButton ID="Linkbtn_SharedServerAllow4" OnClick="Linkbtn4_OnClick"  CssClass="fontlink"   runat="server" Text="Price plan starts from $ xyz per xyz month" ></asp:LinkButton>                                          
                                    </td>
                                </tr>
                          </table>    
                        </asp:Panel>                                
                        <asp:Panel ID="Panel5" runat="server" Visible="true" class="borderbottom">     
                          <table width="100%"> 
                            <tr>
                               <td colspan="3">             
                                        <asp:CheckBox ID="Chk_SaleServerAllow" runat="server" AutoPostBack="True" OnCheckedChanged="FiveServerTypeSelect5_CheckedChanged"                                   
                                              CssClass="fontdesc1"  Text="Hosting on your server  placed in our server farm" />                                      
                                                                                                                                                              
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_SaleServerAllow5" runat="server" CssClass="fontdesc"  Text=""></asp:Label>  
                                </td>
                            </tr>
                             <tr>
                                    <td align="right">                                        
                                                  <asp:Panel ID="Panel6" runat="server" Visible="false">
                                                      <table class="style1">
                                                          <tr>
                                                              <td align="left">
                                                                  <asp:Label ID="Label50" runat="server" 
                                                                      
                                                                      Text="A) Please select the server model which you wish to buy and host at our server farm" 
                                                                      Font-Bold="True"></asp:Label>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td align="left">
                                                                  <asp:Label ID="Label51" runat="server" Font-Bold="False" Text="Selected server"></asp:Label>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td align="left">
                                                                  <asp:Panel ID="Panel10" runat="server" Visible="false">
                                                                      <table class="style1">
                                                                          <tr>
                                                                              <td align="center" rowspan="4" width="20%">
                                                                                  <asp:Image ID="Image1" runat="server" Height="80px" Width="80px" />
                                                                              </td>
                                                                              <td width="80%">
                                                                                  <asp:Label ID="Label53" runat="server"></asp:Label>
                                                                              </td>
                                                                          </tr>
                                                                          <tr>
                                                                              <td width="80%">
                                                                                  <asp:Label ID="Label54" runat="server"></asp:Label>
                                                                                  <asp:Label ID="Label56" runat="server" Visible="False"></asp:Label>
                                                                              </td>
                                                                          </tr>
                                                                          <tr>
                                                                              <td width="80%">
                                                                                  <asp:Label ID="Label55" runat="server"></asp:Label>
                                                                              </td>
                                                                          </tr>
                                                                          <tr>
                                                                              <td width="80%">
                                                                                  <asp:Button ID="Button11" runat="server" onclick="Button11_Click" 
                                                                                      Text="Change" />
                                                                                  &nbsp;<asp:Button ID="Button12" runat="server" Text="Remove" 
                                                                                      onclick="Button12_Click" />
                                                                              </td>
                                                                          </tr>
                                                                      </table>
                                                                  </asp:Panel>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td align="left">
                                                                  <asp:Label ID="Label52" runat="server" Font-Bold="True" 
                                                                      Text="B) Select hosting "></asp:Label>
                                                                  &nbsp;<asp:LinkButton ID="Linkbtn_SaleServerAllow5" runat="server" 
                                                                      CssClass="fontlink" OnClick="Linkbtn5_OnClick" 
                                                                      Text="Price plan starts from $ xyz per xyz month"></asp:LinkButton>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td align="left">
                                                                  <asp:Panel ID="Panel9" runat="server" BackColor="White" BorderColor="#0287a1" 
                            BorderStyle="Solid" BorderWidth="2px" Height="500px" Width="1000px">
                              
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            <table bgcolor="#0287a1" 
                                            style="width: 100%; font-weight: bold; color: #000000;">
                                <tr>
                                    <td align="left">
                                                     
                                                  <asp:Label ID="Label57" runat="server" Font-Bold="True" CssClass="fontdesc"
                                                      Text="Select the Server to Buy"></asp:Label>
                                                     
                                                  </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="4">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                      <label> 
                                          <asp:Label ID="Label3" runat="server" Text="Server sub type" CssClass="fontdesc"></asp:Label></label>
                                      <label> 
                                          <asp:DropDownList ID="subtype" runat="server" Width="140px" 
                                            AutoPostBack="True" onselectedindexchanged="subtype_SelectedIndexChanged">
                                          </asp:DropDownList>
                                      </label>
                                      <label><asp:Label ID="Label4" runat="server" Text="Server sub sub type" CssClass="fontdesc"></asp:Label></label>
                                      <label>  <asp:DropDownList ID="subsubtype" runat="server" Width="140px" 
                                            AutoPostBack="True" onselectedindexchanged="subsubtype_SelectedIndexChanged">
                                          </asp:DropDownList></label>
                                      <label><asp:Label ID="Label5" runat="server" Text="Product model" CssClass="fontdesc"></asp:Label></label>
                                      <label>  <asp:DropDownList ID="productmodel" runat="server" Width="140px" 
                                            AutoPostBack="True">
                                          </asp:DropDownList></label></td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                                      <label> 
                                          <asp:Label ID="Label6" runat="server" Text="Search" CssClass="fontdesc"></asp:Label></label>
                                      <label> 
                                          <asp:TextBox ID="txtsearch" runat="server" Width="400px"></asp:TextBox>
                                      </label>
                                      <label><asp:Label ID="Label7" runat="server" Text="filter by price below" CssClass="fontdesc"></asp:Label></label>
                                      <label>   <asp:TextBox ID="txtprice" runat="server" Width="50px"></asp:TextBox></label>
                                      </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <label>
                                       <asp:Label ID="Label9" runat="server" Text="Franchisee Name" CssClass="fontdesc"></asp:Label> </label>
                                        <label>
                                        <asp:DropDownList ID="ddlfranchisee" runat="server" Width="140px" 
                                            AutoPostBack="True">
                                          </asp:DropDownList></label>
                                        <label>
                                       <asp:Label ID="Label10" runat="server" Text="Country" CssClass="fontdesc"></asp:Label> </label>
                                        <label>
                                       <asp:DropDownList ID="ddlcountry" runat="server" Width="140px" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="ddlcountry_SelectedIndexChanged">
                                          </asp:DropDownList> </label>
                                          <label>
                                       <asp:Label ID="Label11" runat="server" Text="State" CssClass="fontdesc"></asp:Label> </label>
                                        <label>
                                       <asp:DropDownList ID="ddlstate" runat="server" Width="140px" 
                                            AutoPostBack="True">
                                          </asp:DropDownList> 
                                      
                                        </label>
                                        <label>
                                          <asp:Button ID="Button10" runat="server" onclick="Button10_Click" Text="Go" />
                                        </label>
                                        </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <asp:Panel ID="Panel7" runat="server" Height="300px" ScrollBars="Vertical">
                                            <asp:DataList ID="DataList2" runat="server" RepeatColumns="5" 
                                                RepeatDirection="Horizontal" ShowFooter="False" ShowHeader="False">
                                                <ItemTemplate>
                                                    <table ID="back" border="0" bordercolor="#eae8e8" cellpadding="0" 
                                                        cellspacing="0">
                                                        <tr align="center" valign="top">
                                                            <td height="241" width="1%">
                                                                <table border="0" cellpadding="0" cellspacing="2">
                                                                    <tr align="left" valign="top">
                                                                        <td align="center" class="title1" style="height: 12px">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="left" valign="top">
                                                                        <td align="center">
                                                                            <asp:Label ID="Label1" runat="server" ForeColor="Maroon" 
                                                                                Text='<%# Eval("pname") %>'></asp:Label>
                                                                            <asp:Label ID="Label8" runat="server" ForeColor="Maroon" 
                                                                                Text='<%# Eval("ProductBAtchMasterID") %>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="left" valign="top">
                                                                        <td align="center" style="height: 130px">
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr align="center" valign="top">
                                                                                    <td>
                                                                                        <img src="images/box_top.gif" style="height: 17px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" 
                                                                                        style="background-image: url('images/box_left.gif'); background-repeat: repeat; background-position: left;" 
                                                                                        valign="top">
                                                                                        <a href='ProductDetail.aspx?ProductID=<%#DataBinder.Eval(Container.DataItem, "ProductBAtchMasterID")%>' style="border-left-color: white; border-bottom-color: white; border-top-style: solid;
                                                                                    border-top-color: white; border-right-style: solid; border-left-style: solid;
                                                                                    border-right-color: white; border-bottom-style: solid">
                                                                                        <img src='/images/<%#DataBinder.Eval(Container.DataItem, "ImageSmallFront")%>' style="border-top-style: none;
                                                                                        border-right-style: none; border-left-style: none; height: 80px; border-bottom-style: none"
                                                                                        width="100px" align="middle" />
                                                                                        </a>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr align="center" valign="top">
                                                                                    <td>
                                                                                        <img height="17" src="images/box_bottom.gif" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="left" valign="top">
                                                                        <td align="center">
                                                                            <table border="0" cellpadding="0" cellspacing="2">
                                                                                <tr>
                                                                                    <td align="center" colspan="2" valign="top">
                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td align="left" class="std" valign="top">
                                                                                                    <asp:Label ID="lblpriceHead" runat="server" Font-Size="10px" Text="Normal:$ "></asp:Label>
                                                                                                </td>
                                                                                                <td align="left" class="std" valign="top">
                                                                                                    <asp:Label ID="lblprice" runat="server" 
                                                                                                        Text='<%#DataBinder.Eval(Container.DataItem, "SalePrice")%>'></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" colspan="2">
                                                                                        <%--<A onclick="window.open('EnhanceView.aspx?ProductID=<%#DataBinder.Eval(Container.DataItem, "Inventoryid")%>', 'welcome','width=400,height=400,menubar=no,status=no')" href="javascript:void(0)">
                                                                &nbsp;View Img</a>--%>
                                                                                        <asp:Button ID="btnnext" runat="server" onclick="btnnext_Click1" Text="Buy" 
                                                                                            Width="100px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td ID="favbtn" align="center" valign="top">
                                                                                        <a href="javascript:void(0)" 
                                                                                            onclick='window.open(&#039;viewImg.aspx?ProductID=<%#DataBinder.Eval(Container.DataItem, "ID")%>&#039;, &#039;welcome&#039;,&#039;width=600,height=500,menubar=no,status=no&#039;)'>
                                                                                        View Img</a>
                                                                                        <%-- <A class="eff" href='OrderDetails.aspx?id=<%#DataBinder.Eval(Container.DataItem, "InventoryMasterId")%>'></A>--%>
                                                                                    </td>
                                                                                    <td ID="Td1" align="center" valign="top">
                                                                                        <a href="javascript:void(0)" 
                                                                                            onclick='window.open(&#039;ProductDetail.aspx?ProductID=<%#DataBinder.Eval(Container.DataItem, "ProductBAtchMasterID")%>&#039;, &#039;welcome&#039;,&#039;width=600,height=500,menubar=no,status=no&#039;)'>
                                                                                        &nbsp;More Info..</a>
                                                                                        <%--  <a class="eff" href='ProductDetail.aspx?ProductID=<%#DataBinder.Eval(Container.DataItem, "ProductBAtchMasterID")%>'>
                                                                                    More Info..</a>--%>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;</td>
                                </tr>
                                
                                
                                <tr>
                                    <td colspan="4">
                                        &nbsp;</td>
                                    <td colspan="4">
                                        &nbsp;</td>
                                </tr>
                                
                                
                                <tr>
                                    <td style="height: 30px">
                                                                      &nbsp;</td>
                                    <td style="height: 30px">
                                        <%--   <asp:Button ID="Button14" runat="server" onclick="Button14_Click" 
                                                                          Text="Submit" style="height: 26px" />--%>
                                    </td>
                                    <td style="height: 30px">
                                    </td>
                                    <td style="height: 30px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
              
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"  Drag="true" PopupControlID="Panel9" TargetControlID="Button4">
                                                </cc1:ModalPopupExtender>
                                                <asp:Button ID="Button4" runat="server" Style="display: none" />
            
            </td>
                                                          </tr>
                                                      </table>
                                                  </asp:Panel>
                                         
                                    </td>
                                </tr>
                           </table>
                         </asp:Panel>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="Btn_ownserver" runat="server"  Text="Next" CssClass="btnSubmitm" OnClick="Btn_ownserver_Click" Visible="false"  />
                                <asp:Button ID="Btn_CommonServerAllow" Width="50px"  runat="server" Text="Next" CssClass="btnSubmitm" OnClick="btn_Chk_CommonServerAllow_Click"  Visible="false" />
                                <asp:Button ID="Btn_LeaseServerAllow" Width="168px" runat="server"  Text="Next" 
                                    CssClass="btnSubmitm" OnClick="Btn_LeaseServerAllow_Click"  Visible="false" />
                                <asp:Button ID="Btn_SharedServerAllow" Width="50px" runat="server"  Text="Next" CssClass="btnSubmitm" OnClick="Btn_SharedServerAllow_Click"  Visible="false" />
                                <asp:Button ID="Btn_SaleServerAllow"  Width="50px" runat="server"  Text="Next" CssClass="btnSubmitm" OnClick="Btn_SaleServerAllow_Click"  Visible="false" />
                            </td>
                        </tr> 
                                                          
                </fieldset>
                </asp:Panel>
            </td>
        </tr>
    </table> 
                                  </fieldset>                    
                                </td>
                                </tr>
                                </table> 
                                                                                                  
         </ContentTemplate>
            </asp:UpdatePanel>
            </div>
    </asp:Panel>  
  </asp:Panel> 

      <asp:Panel ID="pnlserverportal" runat="server" Visible="false">
        <table style="vertical-align:top;" width="100%">
        <tr>
                    <td>
                    <label>
                            Select Your Hosting Portal
                    </label> 
                    </td>
                    </tr>
        <tr>
                    <td align="center">
                            <asp:GridView ID="Gv_ServerHostPortal" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
                                        HeaderStyle-ForeColor="White"   EmptyDataText="No Record Found." AllowPaging="True" Width="100%" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="2" OnPageIndexChanging="Gv_ServerHostPortal_PageIndexChanging"
                                        OnRowCommand="Gv_ServerHostPortal_RowCommand" OnRowDeleting="Gv_ServerHostPortal_RowDeleting" OnRowEditing="Gv_ServerHostPortal_RowEditing1"
                                        OnRowDataBound="Gv_ServerHostPortal_RowDataBound">
                                        <Columns>                                                                                       
                                            <asp:TemplateField HeaderText="Portal Name" SortExpression="VersionInfoName" ItemStyle-Width="25%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblportalnameName" runat="server" Text='<%# Bind("PortalName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="25%" />
                                            </asp:TemplateField>                                                                                 
                                            <asp:TemplateField HeaderText="Email ID" SortExpression="WebsiteUrl"
                                                ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsupportemailid" runat="server" Text='<%# Bind("Supportteamemailid")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="25%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone Number" SortExpression="WebsiteUrl" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsuupotphoneno" runat="server" Text='<%# Bind("Supportteamphoneno")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>                                                                                   
                                            <asp:TemplateField HeaderText="Website Name" SortExpression="WebsiteUrl"
                                                ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>                                                                                               
                                                        <asp:LinkButton ID="linkdow1dailywork" runat="server" Text='<%#Bind("Portalmarketingwebsitename") %>' Font-Size="12px"  ForeColor="#b9b9b9"     CommandName="MarketingURL" CommandArgument='<%# Eval("Portalmarketingwebsitename") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="25%" />
                                            </asp:TemplateField>                                                                                                    
                                                <asp:TemplateField HeaderText="Go" HeaderImageUrl="~/images/Btn_go.jpg" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" >
                                                    <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnedit" runat="server" CommandArgument='<%# Eval("Id") %>' ToolTip="Go" CommandName="Go" ImageUrl="~/images/Btn_go.jpg" />
                                                                </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>                                          
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                        </asp:GridView>
                    </td>
                    </tr>
                     <tr>
                    <td >
                
                 
                    </td>
                    </tr>
        </table>
    </asp:Panel>   
    <div>
       
             <%--<cc1:modalpopupextender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                        Enabled="True" PopupControlID="Panel9" TargetControlID="Button4" CancelControlID="ImageButton7">
                </cc1:modalpopupextender>
                <asp:Button ID="Button4" runat="server" Style="display: none" />--%>
               
    </div>           
                   



        <!--end of right content-->
        <div style="clear: both;">            
            <asp:Label ID="lblproductid" runat="server" ForeColor="Red"></asp:Label>
            <input id="hdnPricePlanId" name="hdnPricePlanId" runat="server" type="hidden" style="width: 1px" />
        </div>
    </div>
</asp:Content>
