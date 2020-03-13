<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TflinkTest.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container site-section mb-5">
        <div class="row justify-content-center text-center">
            <div class="col-7 text-center mb-5">
                <h2>How it works</h2>
                <p>Linking families in a secure and closed system Build your family tree.Reconnect with relatives and discover new ones.Share your family history.</p>
            </div>
        </div>
        <div class="how-it-works d-flex">
            <div class="step">
                <span class="number"><span>01</span></span>
                <span class="caption">Login &amp; Sign up</span>
            </div>
            <div class="step">
                <span class="number"><span>02</span></span>
                <span class="caption">Create Your Profile</span>
            </div>
            <div class="step">
                <span class="number"><span>03</span></span>
                <span class="caption">Add Your Family</span>
            </div>
            <div class="step">
                <span class="number"><span>04</span></span>
                <span class="caption">Search Members</span>
            </div>
            <div class="step">
                <span class="number"><span>05</span></span>
                <span class="caption">View Family</span>
            </div>

        </div>
    </div>

    <div class="site-section bg-light">
        <div class="container">
            <div class="row justify-content-center text-center mb-5">
                <div class="col-7 text-center mb-5">
                    <h2>The Family Link</h2>
                    <p id="content" runat="server">
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
