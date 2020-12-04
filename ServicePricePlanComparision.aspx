<%@ Page Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true"
    CodeFile="ServicePricePlanComparision.aspx.cs" Inherits="PricePlanComparision" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--  
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    
    <style type="text/css">

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
     <h2 style="text-align:center;">
            Select Services Price Plan
      </h2
        <div class="products_box">
       
        <asp:Panel ID="pnlpl" runat="server" Visible="false">
            <fieldset>
                <legend>Choose your Server Price Plan here and Order Now </legend>
            </fieldset>
            </asp:Panel>            
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend><%--Choose you Services Price Plan here and Order Now--%></legend>
                <table width="100%">                   
                    <tr>
                                            <td>
                         
                                                            <label for="firstName1">
                    <asp:DropDownList ID="ddlproduct" runat="server" AutoPostBack="True" Visible="false"  OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged">
                    </asp:DropDownList>
               
                    <asp:DropDownList ID="ddlportal" runat="server" AutoPostBack="True" Visible="false" OnSelectedIndexChanged="ddlportal_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
                            <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" CssClass="mGridcss"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="Id"
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
                                            <asp:Label ID="lblhcead1" runat="server" Text="Server Priceplan List" Font-Size="20px"></asp:Label>
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
                                                <asp:Label ID="ddlhead7" runat="server" Text="" ForeColor="Black" Visible="false"></asp:Label>
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
                           <asp:Button ID="Btn_checkout1" runat="server" Visible="true" Text="Checkout now" CssClass="btnSubmitm" OnClick="Btn_checkout_Click"  />
                        </td>
                    </tr>

                    <tr>
                        <td>
                             <div id="cover" >
                                                 </div>
                                                 <asp:Panel ID="pnlshowShared" runat="server" BackColor="White" BorderColor="#999999"  ScrollBars="Vertical" BorderStyle="Solid" BorderWidth="2px" Height="70%"  Width="65%"  CssClass="pnlBackGround">                                                
                                                                    <asp:Panel ID="pnlservicetype" runat="server" Visible="false">
                                                                               <table style="vertical-align:top;" width="100%">
                                                              <tr>
                                                                       <td colspan="3">
                                                                            <label>
                                                                              <asp:Label ID="lblservices" runat="server" style="font-family:Arial;font-size:15px;" Text="" > </asp:Label>
                                                                              </label>                                                                              
                                                                        </td>                                                
                                                              </tr>
                                                             <tr>
                                                                    <td colspan="3">
                                                                     <label>
                                                                                The List of services provided are shown below
                                                                      </label>
                                                                    </td>
                                                             </tr>
                                                              <tr>
                                                                  <td>
                                                                      <label>
                                                                      <asp:Label ID="Label1" runat="server" CssClass="font1" 
                                                                          style="font-family:Arial;font-size:12px;" 
                                                                          Text="Pelase select the related priceplan "> </asp:Label>
                                                                      </label>
                                                                  </td>
                                                              </tr>
                                                             <tr>
                                                                <td align="center">
                                                                        <asp:GridView ID="Gv_ServerHostPortal" runat="server" AutoGenerateColumns="false" DataKeyNames="UniqueID"
                                                                                    EmptyDataText="No Record Found." AllowPaging="True" Width="100%" CssClass="mGrid"
                                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="2" OnPageIndexChanging="Gv_ServerHostPortal_PageIndexChanging"
                                                                                    OnRowCommand="Gv_ServerHostPortal_RowCommand" OnRowDeleting="Gv_ServerHostPortal_RowDeleting" OnRowEditing="Gv_ServerHostPortal_RowEditing1"
                                                                                    OnRowDataBound="Gv_ServerHostPortal_RowDataBound">
                                                                                    <Columns>  
                                                                                      <asp:TemplateField  ItemStyle-Width="2"
                                                                                            HeaderStyle-HorizontalAlign="Left">
                                                                                            <ItemTemplate>
                                                                                              
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                            <ItemStyle Width="2%" />
                                                                                        </asp:TemplateField>                                                                                         
                                                                                        <asp:TemplateField HeaderText="Service Name" SortExpression="CategorySubTypeName" ItemStyle-Width="25%"
                                                                                            HeaderStyle-HorizontalAlign="Left">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCategorySubTypeName" runat="server" Text='<%# Bind("CategorySubTypeName")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                            <ItemStyle Width="25%" />
                                                                                        </asp:TemplateField>                                                                                 
                                                                                        <asp:TemplateField HeaderText="Description" SortExpression="Description"
                                                                                            ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Left">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description")%>'></asp:Label>
                                                                                                 <asp:Label ID="lbluniqueid" runat="server" Text='<%# Bind("UniqueID")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                            <ItemStyle Width="25%" />
                                                                                        </asp:TemplateField>                                                                           
                                                                                         <asp:TemplateField HeaderText="Select"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" >
                                                                                                <ItemTemplate>
                                                                                                        <%--<asp:ImageButton ID="imgbtnedit" runat="server" CommandArgument='<%# Eval("UniqueID") %>' ToolTip="Go" CommandName="Go" ImageUrl="~/images/Btn_go.jpg" />--%>
                                                                                                        <asp:LinkButton ID="linkSelect" runat="server" Text="Select" Font-Size="12px"  ForeColor="#b9b9b9" CommandName="Select" CommandArgument='<%# Eval("UniqueID") %>'></asp:LinkButton>
                                                                                                        <asp:LinkButton ID="linkremove" Visible="false"  runat="server" Text="Remove" Font-Size="12px"  ForeColor="#b9b9b9" CommandName="Remove" CommandArgument='<%# Eval("UniqueID") %>'></asp:LinkButton>
                                                                                                         </ItemTemplate>
                                                                                                     <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                                                                 <ItemStyle HorizontalAlign="Left" />
                                                                                          </asp:TemplateField>     
                                                                                         <asp:TemplateField>
                                                                                                 <ItemTemplate>
                                                                                                    <%--<tr>
                                                                                                        <td>
                                                                                                        </td>
                                                                                                        <td colspan="3"> --%>
                                                                                                            <div id="did" style="overflow: auto; display: inline; position: relative; overflow: auto">
                                                                                                                    <asp:GridView ID="gvOrdersservice" runat="server" ShowHeader="false" CssClass="mGridcss" PagerStyle-CssClass="pgr" AutoGenerateColumns="false" DataKeyNames="Id">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblpriceplan" runat="server" Text='<%# Bind("PricePlanName") %>'></asp:Label>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>      
                                                                                                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                                                                                                <ItemTemplate>
                                                                                                                                  
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>                                                                
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                
                                                                                                            </div>
                                                                                                         <%--</td>
                                                                                                    </tr>--%>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>                                    
                                                                                    </Columns>
                                                                                   <PagerStyle CssClass="pgr" />
                                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                 </asp:GridView>
                                                                </td>
                                                              </tr>
                                                              <tr>
                                                                <td align="right" >
                                                               
                                                                    <asp:Button ID="Btn_checkout" runat="server" Visible="true" Text="Not Intrested in any services CheckOut now" CssClass="btnSubmitm" OnClick="Btn_checkout_Click"  />
                                                                </td>
                                                              </tr>
                                                   </table>
                                                                             </asp:Panel> 
                                                  </asp:Panel>  
                                                       
                  
                                                <cc1:ModalPopupExtender ID="ModalPopupExtendershowShared" runat="server" BackgroundCssClass="modalBackground" CancelControlID="Button4" Drag="true" PopupControlID="pnlshowShared" TargetControlID="Button4">
                                                </cc1:ModalPopupExtender>
                                                <asp:Button ID="Button4" runat="server" Style="display: none" />
                                                
                        </td>
                    </tr>
                   
                </table>
            </fieldset>
        </div>
        <div align="center">
	
	
</div>

        <!--end of right content-->
        <div style="clear: both;">
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            <asp:Label ID="lblproductid" runat="server" ForeColor="Red"></asp:Label>
            <input id="hdnPricePlanId" name="hdnPricePlanId" runat="server" type="hidden" style="width: 1px" />
        </div>
    </div>
</asp:Content>
