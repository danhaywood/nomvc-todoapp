<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.WithServices.Master" Inherits="System.Web.Mvc.ViewPage<NakedObjects.Web.Mvc.Models.ActionResultModel>" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Html.ObjectTitle(Model.Result)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.TabbedHistory(Model)%>   
    <div class="<%: IdHelper.StandaloneTableName %>" id="<%: Html.ObjectTypeAsCssId(Model.Result) %>">
        <%: Html.ActionResult(Model) %>
        <%: Html.UserMessages() %>
        <%: Html.Collection(Model.Result, Model.Action)%>
    </div> 
</asp:Content>
