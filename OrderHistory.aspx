<%@ Page Title="" Language="C#" MasterPageFile="~/SilkTheory.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="FinalProject_WA.OrderHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/main.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="section-title">Your Order History</h2>

        <asp:Label ID="lblMessage" runat="server" CssClass="auth-message error" />

        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="table"
                      DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                <asp:BoundField DataField="OrderDate" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}" />
                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                <asp:BoundField DataField="Price" HeaderText="Price (RM)" DataFormatString="{0:F2}" />
                <asp:TemplateField HeaderText="Total (RM)">
                    <ItemTemplate>
                        <%# String.Format("{0:F2}", Convert.ToDecimal(Eval("Price")) * Convert.ToInt32(Eval("Quantity"))) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ImageField DataImageUrlField="ImageUrl" HeaderText="Product Image">
                    <ControlStyle Width="100px" Height="100px" />
                </asp:ImageField>
            </Columns>
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
            ConnectionString="<%$ ConnectionStrings:SilkTheoryConnectionString %>"
            SelectCommand="GetOrderHistoryByUser"
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="UserID" SessionField="UserID" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
