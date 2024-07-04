<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your contact page.</h3>
    <address>
       Waiyaki Way<br />
        Equity House, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        +254719810538
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@zilla-fit.com">Support@zilla-fit.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@zilla-fit.com">Marketing@zilla-fit.com</a>
    </address>
</asp:Content>
