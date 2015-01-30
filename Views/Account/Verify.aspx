<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Verify
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Verify</h2>

        <% string s = TempData["tempMessage"] as string;
            if (!string.IsNullOrEmpty(s)) { %>
        <p>
            <b><%: TempData["tempMessage"].ToString() %></b>
        </p>
    <% } %>

</asp:Content>
