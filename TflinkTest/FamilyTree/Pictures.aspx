<%@ Page Title="" Language="C#" MasterPageFile="~/FamilyTree/Familymaster.Master" AutoEventWireup="true" CodeBehind="Pictures.aspx.cs" Inherits="TflinkTest.FamilyTree.Pictures" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div class="col-md-12">
        <div class="row">
            <h2>Pictures</h2>
            <hr />
            <asp:FileUpload ID="Flud_picture" runat="server" />
            <asp:Image ID="img_Member" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="Save" />
            <asp:Button ID="Button2" runat="server" Text="Cancel" />

        </div>
    </div>
</asp:Content>
