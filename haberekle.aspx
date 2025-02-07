<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="haberekle.aspx.cs" Inherits="haberekle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        
        .auto-style4 {
        text-align: left;}
        .alert {
        background-color: #f8d7da;
        color: #721c24;
        padding: 15px;
        border-radius: 5px;
        margin-bottom: 20px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div id="kapsayici">
    <div id="yonetici">
        <div id="adminmenu">
            <ul>
                <li><a href="sil.aspx">Haber Silme</a></li>
                <li><a href="haberekle.aspx">Haber Ekleme</a></li>
                <li><a href="guncelle.aspx">Haber Güncelleme</a></li>
                <li><a href="kullanicimesaj.aspx">Kullanıcı Mesajları</a></li>
            </ul>
        </div>
    </div>
    <div id="icerik">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <br />
                <div class="haber-ekleme-formu">
                    <h2>HABER EKLEME</h2>
                    <div class="form-alanlar">
                        <div class="auto-style4">
                            <asp:Label runat="server" Text="Haber Başlık:" AssociatedControlID="TextBox1"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </div>
                        <div class="auto-style4">
                            <asp:Label runat="server" Text="Haber İçerik:" AssociatedControlID="TextBox2"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            <br />
                            <asp:Label runat="server" AssociatedControlID="FileUpload1" Text="Haber Resmi:"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="button" />
                        </div>
                        <div class="auto-style4">
                            <asp:Label runat="server" Text="Haber Tarihi:" AssociatedControlID="TextBox3"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </div>
                        <div class="auto-style4">
                            <asp:Label runat="server" Text="Haber Linki:" AssociatedControlID="TextBox4"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        </div>
                        <div class="auto-style4">
                            <asp:Label runat="server" Text="Haber Kategorisi:" AssociatedControlID="kategori"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="kategori" runat="server" Height="29px" Width="175px">
                                <asp:ListItem Text="Seçiniz...."></asp:ListItem>
                                <asp:ListItem Text="Spor"></asp:ListItem>
                                <asp:ListItem Text="Magazin"></asp:ListItem>
                                <asp:ListItem Text="Dünya"></asp:ListItem>
                                <asp:ListItem Text="Ekonomi"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:Button ID="Button1" runat="server" CssClass="button" OnClick="Button1_Click" Text="Haber Ekle"  />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="ClearContentButton" runat="server" CssClass="button"  OnClick="ClearContentButton_Click" Text="Temizle" />
                        </div>
                        <div class="form-alan">
                            <asp:Label ID="Label2" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="Button1" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>

    <script type="text/javascript">
        function ShowPopup() {
            var message = 'Lütfen resmi yükleyiniz!';
            var alertBox = '<div class="alert">' + message + '</div>';
            document.getElementById('icerik').insertAdjacentHTML('afterbegin', alertBox);
        }
    </script>

</asp:Content>
