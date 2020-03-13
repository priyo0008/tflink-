<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TflinkTest.Search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="row justify-content-center text-center mb-5" style="margin-top: 30px;">
            <div class="col-7 text-center mb-5">
                <h2>Search People</h2>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row" style="margin-top: 40px; margin-bottom: 40px;">
                    <div class="col-md-6">
                        <asp:Image ID="Image1" ImageUrl="images/search.jpg" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <div runat="server" id="searchpart">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    First Name :
                <asp:TextBox runat="server" placeholder="Enter First" CssClass="form-control" ID="txt_Fname"></asp:TextBox>
                                    <%--    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionListsubcatagory" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" FirstRowSelected="false" TargetControlID="txt_Username">
                    </cc1:AutoCompleteExtender>--%>
                                    <asp:Label ID="lbl_fname" runat="server" ForeColor="Red"></asp:Label><br />
                                    Last Name :<asp:TextBox runat="server" placeholder="Enter Last Name" CssClass="form-control" ID="txt_Lastname"></asp:TextBox>
                                    <%--   <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="GetCompletionListEmail" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" FirstRowSelected="false" TargetControlID="txt_Emailid">
                    </cc1:AutoCompleteExtender>--%>
                                    <asp:Label ID="lbl_lname" runat="server" ForeColor="Red"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            Country :<span style="color: Red; font-weight: bold; font-size: small">*</span> : 
                            <asp:DropDownList ID="ddl_Country" CssClass="form-control" runat="server">
                            
                            </asp:DropDownList>
                            <asp:Label ID="lbl_country" runat="server" ForeColor="Red"></asp:Label>
                   <%--          Country :  <asp:DropDownList ID="DropDownList1" runat="server">

        </asp:DropDownList>--%>
                            <br />
                            <asp:Button runat="server" class="btn btn-primary" OnClick="btn_Save_Click" Text="Search" ID="btn_Save" />
                            <%--  <p>
                Your email is required so we can communicate with you. Please be assured that it will never be shown to anyone.
            </p>--%>
                        </div>
                    </div>
                </div>

                <div class="row" style="margin-top: 50px;">
                    <div class="col-md-12">
                        <div id="showpart" runat="server" visible="false">
                            <asp:Button ID="btn_search" OnClick="btn_search_Click" class="btn btn-primary" runat="server" Text="Start a new search" />
                            <p>This list does not include any persons under 18 years of age.</p>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top: 50px;">
                    <div class="col-md-6">
                        <p id="perdonfound" runat="server"></p>
                    </div>
                    <div class="col-md-6">
                        <p runat="server" id="Administrator"></p>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
