<%@ Page Title="" Language="C#" MasterPageFile="~/SilkTheory.Master" AutoEventWireup="true" CodeBehind="GetOrderHistory.aspx.cs" Inherits="FinalProject_WA.GetOrderHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/main.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="section-title">📦 Order History</h2>

        <div class="cart-summary">
            <asp:Button ID="btnGoToAdmin" runat="server" Text="Go to Admin" CssClass="btn" OnClick="btnGoToAdmin_Click" />
        </div>

        <asp:Label ID="lblMessage" runat="server" CssClass="auth-message error" />

        <div class="card" style="margin-top:30px">
            <asp:GridView ID="gvOrders" runat="server"
                AutoGenerateColumns="False"
                CssClass="table"
                DataKeyNames="OrderID">

                <Columns>
                    <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                    <asp:BoundField DataField="Username" HeaderText="User" />
                    <asp:BoundField DataField="OrderDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="TotalAmount" HeaderText="Total (RM)" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="ProductName" HeaderText="Product" />
                    <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                    <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price (RM)" DataFormatString="{0:F2}" />
                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>
