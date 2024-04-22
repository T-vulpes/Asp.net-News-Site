<%@ Page Title="" Language="C#"  ValidateRequest="false" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="guncelle.aspx.cs" Inherits="guncelle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css">
    .auto-style1 {
        text-align: left;
    }
</style>
    
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
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
        <div id="icerik" class="auto-style1">
            <h2>Haberler</h2>
            <asp:GridView ID="HaberlerGridView" runat="server" AutoGenerateColumns="False"  AutoGenerateSelectButton="true" DataKeyNames="NO" OnSelectedIndexChanged="HaberlerGridView_SelectedIndexChanged" BackColor="White" BorderColor="#336666"
    BorderStyle="Double" BorderWidth="3px" CellPadding="4"  GridLines="Horizontal" Height="300px" Width="942px" CssClass="custom-grid">
                <Columns>
                    <asp:BoundField DataField="NO" HeaderText="NO" />
                    <asp:BoundField DataField="haber_baslik" HeaderText="Başlık" />
                    <asp:BoundField DataField="haber_icerik" HeaderText="İçerik" />
                    <asp:BoundField DataField="kategori" HeaderText="Kategori" />
                    <asp:BoundField DataField="haber_linki" HeaderText="Link" />
                    <asp:CommandField ShowSelectButton="True" SelectText="Güncelle" />
                </Columns>
            </asp:GridView>
            <br />
            <h2>Haber Güncelleme Formu</h2>
            <asp:TextBox ID="HaberNOTextBox" runat="server" placeholder="Haber NO" CssClass="form-control"></asp:TextBox>
            <br />
            <asp:TextBox ID="HaberBaslikTextBox" runat="server" placeholder="Yeni Başlık" CssClass="form-control"></asp:TextBox>
            <br />
            <asp:TextBox ID="HaberIcerikTextBox" runat="server" TextMode="MultiLine" placeholder="Yeni İçerik" CssClass="form-control"></asp:TextBox>
            <br />
            <asp:TextBox ID="KategoriTextBox" runat="server" placeholder="Yeni Kategori" CssClass="form-control"></asp:TextBox>
            <br />
            <asp:TextBox ID="HaberLinkiTextBox" runat="server" placeholder="Yeni Link" CssClass="form-control"></asp:TextBox>
            <br />
            <asp:Button ID="GuncelleButton" runat="server" Text="Güncelle" CssClass="button" OnClick="GuncelleButton_Click"/>

        </div>
    </div>
</asp:Content>
