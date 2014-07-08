<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.WithServices.Master" Inherits="System.Web.Mvc.ViewPage<System.Object>" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%:  Html.ObjectTitle(Model)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.TabbedHistory()%>   
    <div class="<%: Html.ObjectViewClass(Model) %>" id="<%: Html.ObjectTypeAsCssId(Model) %>">
      
        <%: Html.UserMessages() %>
        <div><%: Html.ScalarView(Model) %></div>
    </div>
</asp:Content>
