<%@ Page Title="" Language="C#" MasterPageFile="~/FamilyTree/Familymaster.Master" AutoEventWireup="true" EnableEventValidation="true" CodeBehind="Addpictures.aspx.cs" Inherits="TflinkTest.FamilyTree.Addpictures" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function previewFile() {
            var preview = document.querySelector('#<%=img_showimg.ClientID %>');
            var file = document.querySelector('#<%=flud_img.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }
            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
        function Validateblog() {

            if (document.getElementById("<%=img_showimg.ClientID%>").value == "") {
                alert("Select a image first !");
                document.getElementById("<%=flud_img.ClientID%>").focus();
                return false;
            }
        }
    </script>
    <style>
        .invisibletext {
            visibility: hidden;
        }
    </style>
    <script type="text/javascript">
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <h2>Add Pictures</h2>
            <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
            <div class="col-md-12">
                <div class="row" style="margin-left: auto; margin-right: auto; display: block;"> 
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-2">
                        <asp:FileUpload ID="flud_img" class="file-path validate" onchange="previewFile()" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <asp:Image ID="img_showimg" ImageUrl="~/FamilyTree/images/defaultimage.jpg" runat="server" Height="100" Width="100" />

                        <asp:Button ID="btn_Addpics" OnClick="btn_Addpics_Click" OnClientClick="return Validateblog();" Style="background-color: cadetblue;" runat="server" Text="Add" class="btn btn-primary" />

                    </div>
                    <div class="col-md-4">
                        <%--     <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                            <ContentTemplate>--%>

                        <div runat="server" id="showaddnames" visible="false">
                            <h4 class="modal-title">Add Name</h4>
                            <asp:Image ID="img_showimage" runat="server" Height="100" Width="100" />
                            <p>Write Names of members in the picture</p>
                            <asp:TextBox ID="txt_Nmaes" CssClass="form-control" runat="server"></asp:TextBox>
                                 <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionListnames" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" FirstRowSelected="false" TargetControlID="txt_Nmaes">
                                                </cc1:AutoCompleteExtender>
                            <br />
                            <asp:Button ID="btn_Addname" OnClick="btn_Addname_Click" Style="background-color: cadetblue;" runat="server" Text="Add" class="btn btn-primary" />
                            <asp:Label ID="lblMessage" ForeColor="Green" Font-Bold="true" Text="Name Added" runat="server" Visible="false" />
                            <asp:Button ID="btn_close" runat="server" OnClick="btn_close_Click" class="btn btn-default" Text="Close" />

                            <%--  <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                        </div>
                        <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="row" style="margin-top: 50px;">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="Grd_Bindpic" Width="100%" CssClass="table-responsive table table-bordered" OnRowEditing="Grd_Bindpic_RowEditing" OnRowDeleting="Grd_Bindpic_RowDeleting" OnRowDataBound="Grd_Bindpic_RowDataBound" OnRowCancelingEdit="Grd_Bindpic_RowCancelingEdit" OnRowUpdating="Grd_Bindpic_RowUpdating" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None" ForeColor="#333333">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <%--        <asp:BoundField DataField="id" HeaderText="ID">
                            <HeaderStyle CssClass="invisibletext" Width="1%" />
                            <ItemStyle CssClass="invisibletext" />
                        </asp:BoundField>--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label CssClass="invisibletext" ID="lbl_ID" runat="server" Text='<%#Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--       <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Photo">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("Photo") %>'></asp:Label>--%>
                                            <asp:Image ID="Image1" runat="server" Height="100" ImageUrl='<%#Eval("Photo") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Image ID="Image2" runat="server" Height="100" ImageUrl='<%#Eval("Photo") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <%--     <asp:ImageField DataImageUrlField="Photo" HeaderText="Photo" ControlStyle-Width="100" FooterText="Photo" ControlStyle-Height="100">
<ControlStyle Height="100px" Width="100px"></ControlStyle>
                    </asp:ImageField>--%>
                                    <asp:TemplateField HeaderText="Describtion">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_desc" runat="server" Text='<%#Eval("Descr") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_desc" TextMode="MultiLine" Rows="4" runat="server" Text='<%#Eval("Descr") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date Taken">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_dot" runat="server" Text='<%# Bind("Datetaken","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_dot" runat="server" ReadOnly="true" Text='<%# Bind("Datetaken","{0:yyyy/MM/dd}") %>'></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txt_dot" Format="yyyy/MM/dd"></cc1:CalendarExtender>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Names">
                                        <ItemTemplate>
                                            <asp:GridView ID="grd_names" BorderColor="White" runat="server" OnRowDeleting="grd_names_RowDeleting" ShowHeader="false" AutoGenerateColumns="false" CssClass="ChildGrid">
                                                <Columns>
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="Name" />
                                                    <asp:TemplateField HeaderText="Photo">
                                                        <ItemTemplate>
                                                            <%-- <asp:Image ID="Imagew" runat="server" Height="20" Width="20" CommandName="Delete" ImageUrl="images/greenimage.jpg" />--%>
                                                            <asp:Button Text="Delete" Visible="false" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return isDelete();" />
                                                            <asp:Label CssClass="invisibletext" ID="lbl_pictureid3" runat="server" Text='<%#Eval("id") %>'></asp:Label>
                                                            <a href='<%# Eval("id", "Addpictures.aspx?delId={0}") %>'>
                                                                <asp:Image ID="Image3" ImageUrl="~/FamilyTree/images/deleteimg.jpg" Height="20" runat="server" />
                                                                <%--<asp:ImageButton ID="ImageButton1" ImageUrl="~/FamilyTree/images/deleteimg.jpg" Height="20" runat="server" />--%>
                                                                <%--<button type="button" class="btn btn-danger">delete</button>--%></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:BoundField DataField="id" HeaderText="ID">
                                        <HeaderStyle CssClass="invisibletext" Width="1%" />
                                        <ItemStyle CssClass="invisibletext" />
                                    </asp:BoundField>--%>
                                                </Columns>
                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#33276A" />
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%--   <a href='<%# &quot;Pictures.aspx?id=&quot; + Eval(&quot;id&quot;)%>' class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">
                         <asp:Image ID="Image3" runat="server" Height="50" ImageUrl="images/image4.png" /></a>--%>
                                            <a href='<%# Eval("id", "Addpictures.aspx?Id={0}") %>'>
                                                <button type="button" style="background-color: cadetblue;" class="btn btn-primary">Add Name</button></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btn_Edit" runat="server" Text="Edit" class="btn btn-success" CommandName="Edit" />
                                            <%--<asp:Image ID="Image3" ImageUrl="~/FamilyTree/images/deleteimg.jpg" Height="20" CommandName="Delete" runat="server" />--%>
                                            <asp:Button ID="btn_Delete" runat="server" class="btn btn-danger" Text="Delete" CommandName="Delete" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Button ID="btn_Update" runat="server" Text="Update" class="btn btn-success" CommandName="Update" />
                                            <asp:Button ID="btn_Cancel" runat="server" class="btn btn-warning" Text="Cancel" CommandName="Cancel" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <%--   <asp:GridView ID="GridView1" runat="server" CellPadding="3" GridLines="None" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1">
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#594B9C" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#33276A" />
        </asp:GridView>--%>
            </div>

        </div>
    </div>
</asp:Content>
