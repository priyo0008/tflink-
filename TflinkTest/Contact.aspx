<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="TflinkTest.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="site-section bg-light">
        <div class="container">
            <div class="row justify-content-center text-center mb-5">
                <div class="col-7 text-center mb-5">
                    <h2>Contact</h2>
                    <p id="content" runat="server">

                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
