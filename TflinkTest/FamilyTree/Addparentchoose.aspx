<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Addparentchoose.aspx.cs" Inherits="TflinkTest.FamilyTree.Addparentchoose" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <style type="text/css">
        html, body {
            margin: 0;
            padding: 0;
            height: 100%;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 80%;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                padding: 5px;
                height: 80%;
                background-color: floralwhite;
            }

            .modalPopup .footer {
                padding: 3px;
            }

            .modalPopup .button {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

            .modalPopup td {
                text-align: left;
            }
    </style>
    <%--<script type="text/javascript">
        function ShowModalPopupParents() {
            $find("mpeParents").show();
            return false;
        }
    </script>--%>
    <%--<script type = "text/javascript">
     function RadioCheck(rb) { 
        var gv = document.getElementById("<%=grd_bindmembers.ClientID%>"); 
         var rbs = gv.getElementsByTagName("rdo_check");
        var row = rb.parentNode.parentNode; 
        for (var i = 0; i < rbs.length; i++) { 
            if (rbs[i].type == "radio") { 
                if (rbs[i].checked && rbs[i] != rb) { 
                    rbs[i].checked = false; 
                    break; 
                } 
            } 
        } 
    }    

</script>--%>
    <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script language="javascript">
        function singleRbtnSelect(chb) {
            $(chb).closest("table").find("input:radio").prop("checked", false);
            $(chb).prop("checked", true);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--    <asp:Panel ID="pnlPopUpparents" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Add Your Parents Profile 
            </div>
            <div class="body">
                <iframe id="iFramePersonalParent" src="Addparentchoose.aspx" height="400px" width="100%"></iframe>
                <br />
                <br />
                <a href="MyTree1.aspx">
                    <asp:Button ID="btnCloseparents" runat="server" class="btn btn-danger" Text="Close" /></a>
            </div>
        </asp:Panel>--%>
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12" runat="server" id="Bindnotice">
                                Add Parent/Guardian for Profile Priya Rout
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                Type :<span style="color: Red; font-weight: bold; font-size: small">*</span> : 
                            <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Mother</asp:ListItem>
                                <asp:ListItem>Father</asp:ListItem>
                                <asp:ListItem>Guardian</asp:ListItem>
                                <asp:ListItem>Stepmother</asp:ListItem>
                                <asp:ListItem>Stepfather</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                Email Id : 
                            <asp:TextBox ID="txt_Email" onkeyup="ValidateEmail()" onblur="validateEmail(this);" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <p>
                                    If the person already exists in the system, enter the name here and click on Search
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10">
                                <asp:TextBox ID="txt_search" AutoPostBack="true" OnTextChanged="txt_search_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionListsubcatagory" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" FirstRowSelected="false" TargetControlID="txt_search">
                                </cc1:AutoCompleteExtender>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btn_serch" class="btn btn-dafault" runat="server" Text="Search" />
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-6">
                                

                            </div>
                            <div class="col-md-3">
                             <%--   <asp:Button ID="btn_Gotonextpage" runat="server" class="btn btn-dafault" Text="Click To Add New Person" OnClientClick="return ShowModalPopupParents()" />
                                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpeParents" runat="server" PopupControlID="pnlPopUpparents"
                                    TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="btn_Gotonextpage">
                                </cc1:ModalPopupExtender>--%>
                                <p>OR if the person is not yet set up, </p>
                            </div>
                            <div class="col-md-3">
                                <div>
                                    <%-- <button type="button" style="margin-left: auto; margin-right: auto; display: block;" class="btn btn-primary">+Add Parents</button>--%>
                                    <%--<asp:Button ID="Button1" runat="server" Style="margin-left: auto; margin-right: auto; display: block;" class="btn btn-info" OnClientClick="return ShowModalPopupParents()"
                                        Text="+Add Parents" />
                                    <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpeParents" runat="server" PopupControlID="pnlPopUpparents"
                                        TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="btnCloseparents">
                                    </cc1:ModalPopupExtender>--%> 
                                  <%--  <a class="thickbox"
                                        title="Add Child" runat="server" id="lnkAddChild"
                                        href="popuptest.aspx?From=FamilyTree&=true&height=560&width=950&modal=true">
                                          <asp:Button ID="Button2" runat="server" Text="Add" />
                                    </a>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-row">
                            <div class="col-md-12" style="overflow: scroll; height: 200px;">
                                <asp:GridView ID="grd_bindmembers" Width="100%" CssClass="table-responsive table table-bordered" runat="server" CellPadding="4" AutoGenerateColumns="false" ForeColor="#333333" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_check" runat="server" onclick="singleRbtnSelect(this);" />
                                                <asp:HiddenField ID="hidPlanID" runat="server" Value='<%#Eval("id")%>' />
                                                <%--<asp:RadioButton ID="rdo_check"   onclick="RadioCheck(this);" runat="server" />--%>
                                                <%--<asp:CheckBox ID="rdo_check" runat="server" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Member Id">
                                            <ItemTemplate>
                                                <asp:Label CssClass="invisibletext" ID="Label1" runat="server" Text='<%#Eval("MemberId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="First Name">
                                            <ItemTemplate>
                                                <asp:Label CssClass="invisibletext" ID="Label2" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Name">
                                            <ItemTemplate>
                                                <asp:Label CssClass="invisibletext" ID="Label3" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact">
                                            <ItemTemplate>
                                                <asp:Label CssClass="invisibletext" ID="Label6" runat="server" Text='<%#Eval("Contact") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gender">
                                            <ItemTemplate>
                                                <asp:Label CssClass="invisibletext" ID="Label4" runat="server" Text='<%#Eval("Gender") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dob">
                                            <ItemTemplate>
                                                <asp:Label CssClass="invisibletext" ID="Label5" Text='<%# Bind("Dob","{0:dd/MM/yyyy}") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                </asp:GridView>
                            </div>
                        </div> 
                    </div>
                    <div class="modal-footer">
                        <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                        <%--<button type="button" class="btn btn-primary">Save</button>--%>
                        <asp:Button ID="btn_chk" CssClass="btn btn-primary" OnClick="btn_chk_Click" runat="server" Text="Check" />
                        <asp:Button ID="btn_save"  CssClass="btn btn-primary" OnClientClick="return Validate();" runat="server" Text="Save" />
                    </div>
                    <div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
