<%@ Page Title="" Language="C#" MasterPageFile="~/SilkTheory.Master" AutoEventWireup="true" CodeBehind="ProductListing.aspx.cs" Inherits="FinalProject_WA.ProductListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/main.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="section-title">Shop by Category</h2>

        <asp:DropDownList ID="ddlCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged" CssClass="category-dropdown" />

        <div class="product-grid">
            <asp:Repeater ID="rptProducts" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    <div class="product-card">
                        <asp:Image ID="imgProduct" runat="server" CssClass="product-image"
                            ImageUrl='<%# Eval("ImageUrl") %>' AlternateText='<%# Eval("ProductName") %>' />
                        <div class="content">
                            <h3 class="product-title"><%# Eval("ProductName") %></h3>
                            <p class="product-price">RM <%# Eval("Price", "{0:F2}") %></p>
                            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn"
                                CommandArgument='<%# Eval("ProductID") %>' OnClick="btnAddToCart_Click" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:SilkTheoryConnectionString %>"
                SelectCommand="SELECT ProductID, ProductName, Price, ImageUrl FROM Products WHERE IsDeleted = 0 AND (@CategoryID = 0 OR CategoryID = @CategoryID)">
                <SelectParameters>
                    <asp:ControlParameter Name="CategoryID" ControlID="ddlCategories" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
