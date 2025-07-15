<%@ Page Title="" Language="C#" MasterPageFile="~/SilkTheory.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FinalProject_WA.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/main.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="auth-wrapper">
        <div class="auth-container">
            <h2 class="auth-title">Login</h2>
            <div class="auth-form">
                <label for="txtUsername">Username:</label>
                <asp:TextBox ID="txtUsername" runat="server" />
                <asp:RequiredFieldValidator ID="rfvLoginUsername" runat="server"
                ControlToValidate="txtUsername"
                ErrorMessage="Username is required." CssClass="auth-message error"
                Display="Dynamic" />

                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
                <asp:RequiredFieldValidator ID="rfvLoginPassword" runat="server"
                ControlToValidate="txtPassword"
                ErrorMessage="Password is required." CssClass="auth-message error"
                Display="Dynamic" />


                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn" />

                <asp:Label ID="lblMessage" runat="server" CssClass="auth-message error" />
            </div>

            <div class="auth-switch">
                Don’t have an account? <a href="Register.aspx">Register here</a>
            </div>

            <img src="Images/LOGO.png" alt="SilkTheory Logo" class="auth-logo" />
        </div>
    </div>
    </div>
</asp:Content>
