<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="DemoApp.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>
    <dl> 
        <dt>IsAuthenticated</dt> <dd><%= HttpContext.Current.User.Identity.IsAuthenticated %></dd> 
        <dt>AuthenticationType</dt> <dd><%= HttpContext.Current.User.Identity.AuthenticationType %></dd> 
        <dt>Name</dt> <dd><%= HttpContext.Current.User.Identity.Name %></dd> 
        <dt>Is in "IN-WSPCIL-Developers"</dt> 
        <dd><%= HttpContext.Current.User.IsInRole("IN-WSPCIL-Developers") %></dd> 
        <dt>Is in "GB-GRP-WSP-AppsDeploy"</dt> 
        <dd><%= HttpContext.Current.User.IsInRole("GB-GRP-WSP-AppsDeploy") %></dd> 
    </dl>
</asp:Content>
