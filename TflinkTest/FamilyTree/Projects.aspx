<%@ Page Title="" Language="C#" MasterPageFile="~/FamilyTree/Familymaster.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="TflinkTest.FamilyTree.Projects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">

            <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
            <div class="col-md-12">
              <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog"> 
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Add Child </h4>
                            <p runat="server" id="Bindmemnm"></p>
                        </div>
                        <div class="modal-body">
                            <h3>Start a New Project</h3>
                           Title : <asp:TextBox ID="txt_Title" CssClass="form-control" runat="server"></asp:TextBox>
                            Description : <asp:TextBox ID="txt_Desc" TextMode="MultiLine" Rows="6" CssClass="form-control" runat="server"></asp:TextBox>
                            <br />
                            <p>Link URL</p>
                            <p>Show living persons</p> <asp:RadioButton ID="RadioButton1" ValidationGroup="person" runat="server" /><asp:RadioButton ID="RadioButton2" ValidationGroup="person" runat="server" />

                            <p>Show living Children</p><asp:RadioButton ID="RadioButton3" ValidationGroup="children" runat="server" /><asp:RadioButton ID="RadioButton4" ValidationGroup="Children" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btn_save" CssClass="btn btn-default" OnClick="btn_save_Click" OnClientClick="return Validate();" runat="server" Text="Save" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            <div class="col-md-12">
            <h3>Your Projects</h3> :   <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal"> Add New Projects</button>
            </div>
        </div>
    </div>
</asp:Content>
