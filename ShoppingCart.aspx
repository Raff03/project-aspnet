<%@ Page Title="" Language="C#" MasterPageFile="~/SilkTheory.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="FinalProject_WA.ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/main.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="section-title">🛒 Your Shopping Cart</h2>

        <div class="cart-controls">
            <asp:Button ID="btnBack" runat="server" Text="← Back" CssClass="btn btn-back" OnClick="btnBack_Click" />&nbsp;&nbsp;
            <asp:Label ID="lblMessage" runat="server" CssClass="auth-message error" />
        </div>
        <br />
        <br />
        <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CssClass="table"
            DataKeyNames="ProductID" OnRowCommand="gvCart_RowCommand">
            <Columns>
                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                <asp:BoundField DataField="Price" HeaderText="Price (RM)" DataFormatString="{0:F2}" />
                <asp:ImageField DataImageUrlField="ImageUrl" HeaderText="Product Image">
                    <ControlStyle Width="100px" Height="100px" />
                </asp:ImageField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>' Width="50px" AutoPostBack="true"
                            OnTextChanged="txtQuantity_TextChanged" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" Text="🗑️ Delete" CommandName="DeleteRow"
                            CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-back" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="cart-summary">
            <asp:Label ID="lblTotal" runat="server" CssClass="cart-total-label" Text="Total: RM 0.00" />
            <asp:Button ID="btnCheckout" runat="server" Text="Checkout" CssClass="btn btn-checkout" OnClick="btnCheckout_Click" />
        </div>
    </div>
</asp:Content>
