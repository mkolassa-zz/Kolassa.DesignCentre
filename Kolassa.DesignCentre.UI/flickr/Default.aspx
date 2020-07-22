<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="TEST.UI.Pages.Default" Codebehind="Default.aspx.cs" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to Flickr test page!
    </h2>
    <p>
        Click on the Flick menu item to see the results.
        <br />
        Once you set the correct key and secret values in web.config you'll be able to consult
        Flicker sets("albums") from a predefined user, which id also can be modified via
        web.config.
    </p>
    <p>
        For more details about me, check the About page.
    </p>
    <p>
        Happy programing
    </p>
</asp:Content>
