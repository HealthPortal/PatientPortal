<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
    <div align="center"><table cellpadding="0" width="900" cellspacing="0">
    <tr>
      
    <td>
    <img src="../../HealthImages/HealthCare.jpg" alt="" width="300" height="300" border="0" />
    </td>

    <td>
        <img src="../../HealthImages/HealthCare1.jpg" alt="" width="300" height="300" border="0" />
    </td>
    
    <td>
        <img src="../../HealthImages/HealthCare2.jpg" alt="" width="300" height="300" border="0" />
   </td></tr></table></div>

 
</asp:Content>

