<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" AutoEventWireup="true" CodeFile="FrmAllocatedtask.aspx.cs" Inherits="Admin_FrmAllocatedtask" Title="OnlineMis" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" >    

function CallPrint(strid)
{
    var prtContent = document.getElementById('<%= divPrint.ClientID %>');        
        var  WinPrint=window.open('','','left=0,top=0,width=1000px,height=1000px,toolbar=1,scrollbars=1,status=0');
        WinPrint.document.write(prtContent.innerHTML);              
        WinPrint.document.close();
        WinPrint.focus();
        WinPrint.print();
        WinPrint.close();

      }  
</script> 


 <div class="products_box">
             
        <fieldset>
            <legend>
                <asp:Label ID="statuslable" runat="server" Text="Allocated Task Report"></asp:Label>
            </legend>
          
           <div style="clear: both;">
            </div>
            <label>
            <asp:Label ID="a1" runat="server" Text="Business Name"></asp:Label>
            
           <asp:DropDownList ID="ddlStore" runat="server"  AutoPostBack="True"
                                OnSelectedIndexChanged="ddlStore_SelectedIndexChanged">
                        </asp:DropDownList>
            </label>
            
             <label>
            <asp:Label ID="Label1" runat="server" Text="Division"></asp:Label>
             <asp:DropDownList ID="ddlBusiness" CssClass="dropdowntxtbig" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlBusiness_SelectedIndexChanged">
                <asp:ListItem Selected="True">Select</asp:ListItem>
            </asp:DropDownList><asp:HiddenField ID="hdnid1" runat="server" />
            </label>
            
             <label>
            <asp:Label ID="Label2" runat="server" Text="Project"></asp:Label>
               <asp:DropDownList ID="ddlProject" CssClass="dropdowntxtbig" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlProject_SelectedIndexChanged">
                    <asp:ListItem Value="0">Select</asp:ListItem>
            </asp:DropDownList><asp:HiddenField ID="hdnid2" runat="server" />
            </label>
            
            
             <div style="clear: both;">
            </div>
            
            <table style="width: 100%">
    
   
    
   
  
       
    <tr>
            <td colspan="4" style="width: 100%; text-align: right">
               <input type="button" value="print" id="btnPrint" visible="true" runat="server" onclick="javascript:CallPrint('divPrint')" /></td>
           
              
        </tr>
    <tr>
    <td colspan="2">
     <asp:Panel ID="divPrint" runat="server">
    <table style="width: 100%">
   <tr>
            <td colspan="4" style="height: 30px; color: #ffffff; background-color: White; text-align: center;">
                <strong style="color: #000000">
               <asp:Label ID="lblcname" runat="server" Text="" Font-Bold="true" Font-Size="16px"></asp:Label></strong></td>
        </tr>
            <tr>
            <td colspan="4" style="height: 25px; color: #ffffff; background-color: White; text-align: center;">
                <strong style="color: #000000">
              <asp:Label ID="lblprname" runat="server" Font-Bold="True" Font-Size="14px" 
                    Text="Task Allocated Report" Visible="False"></asp:Label></strong></td>
        </tr>
        <tr>
        <td colspan="4">
            <asp:GridView ID="grid" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" DataKeyNames="TaskId" 
                onrowcommand="grid_RowCommand" PageSize="50" Width="100%" 
                AllowSorting="True" onpageindexchanging="grid_PageIndexChanging" 
                onsorting="grid_Sorting" HeaderStyle-HorizontalAlign="Left"
               CellPadding="4"  GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                AlternatingRowStyle-CssClass="alt">
                
                <Columns>
                    <asp:BoundField DataField="TaskId" HeaderText="TaskId" Visible="False" />
                    <asp:BoundField DataField="TaskName" HeaderText="Task Name">
                        <HeaderStyle HorizontalAlign="Left"  />
                        <ItemStyle HorizontalAlign="Left"  Width="450px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProjectId" HeaderText="ProjectId" Visible="False" />
                    <asp:BoundField DataField="Estartdate" DataFormatString="{0:d-MM-yyyy}" 
                        HeaderText="Start Date" SortExpression = "Start Date" >
                       
                        <ItemStyle  Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Eenddate" DataFormatString="{0:d-MM-yyyy}" 
                        HeaderText="End Date">
                       
                        <ItemStyle  Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Eunitsalloted" HeaderText="Eunitsalloted" 
                        Visible="False" />
                    <asp:BoundField DataField="Status" HeaderText="Status">
                        
                        <ItemStyle  Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Percentage" HeaderText="Percentage" 
                        Visible="False" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                    <asp:ButtonField ButtonType="Button" CommandName="More" HeaderStyle-Width="40px" HeaderText="More" 
                        Text="More">
                    
                        <ItemStyle  Width="100px" />
                    </asp:ButtonField>
                </Columns>
              <PagerSettings FirstPageImageUrl="~/Admin/Images/firstpg.gif" FirstPageText="" LastPageImageUrl="~/Admin/Images/lastpg.gif"
                                LastPageText="" NextPageImageUrl="~/Admin/Images/nextpg.gif" NextPageText=""
                                PreviousPageImageUrl="~/Admin/Images/prevpg.gif" PreviousPageText="" />
                                          
                                            <EmptyDataTemplate>
                                                <b>No Record Found.</b>
                                            </EmptyDataTemplate>
            </asp:GridView>
        </td>
    </tr>
     </table>
     </asp:Panel>
    </td>
    </tr>
    
</table>
            </fieldset>
            </div>
    
</asp:Content>

