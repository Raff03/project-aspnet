<%@ Page Title="" Language="C#" MasterPageFile="~/SilkTheory.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="FinalProject_WA.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/main.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="auth-wrapper">
        <div class="auth-container">
            <h2 class="auth-title">Create an Account</h2>
            <div class="auth-form">
                <label for="txtUsername">Username:</label>
                <asp:TextBox ID="txtUsername" runat="server" />
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server"
                ControlToValidate="txtUsername"
                ErrorMessage="Username is required." CssClass="auth-message error"
                Display="Dynamic" />

                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                ControlToValidate="txtEmail"
                ErrorMessage="Email is required." CssClass="auth-message error"
                Display="Dynamic" />

                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                ControlToValidate="txtPassword"
                ErrorMessage="Password is required." CssClass="auth-message error"
                Display="Dynamic" />


                <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="btn" />

                <asp:Label ID="lblMessage" runat="server" CssClass="auth-message success" />
            </div>

            <div class="auth-switch">
                Already have an account? <a href="Login.aspx">Login here</a>
            </div>

            <img src="Images/LOGO.png" alt="SilkTheory Logo" class="auth-logo" />

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:SilkTheoryConnectionString %>" 
                SelectCommand="SELECT * FROM [Users]" />
        </div>
    </div>
    </div>
</asp:Content>
