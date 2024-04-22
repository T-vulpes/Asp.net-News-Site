<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="~/spor.aspx.cs" Inherits="spor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script>
        // Menü simgesine tıklandığında popup penceresini aç
        document.getElementById("admin-icon").addEventListener("click", function() {
            document.getElementById("popup").style.display = "block";
        });

        // Kapatma işlevi için kapat düğmesine tıklayın
        document.getElementsByClassName("close")[0].addEventListener("click", function() {
            document.getElementById("popup").style.display = "none";
        });

        // Dışarıdaki alana tıklanırsa kapat
        window.addEventListener("click", function(event) {
            if (event.target == document.getElementById("popup")) {
                document.getElementById("popup").style.display = "none";
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <div class="doc-loader" id="loader">
                <img src="resimler/preloader.gif" alt="Seppo">
            </div> 
    <div id="kapsayici">
        <!-- Popup İçeriği -->
        <div id="popup" class="popup">
            <div class="popup-content">
                <span class="close">&times;</span>
                <!-- Popup içeriği buraya gelecek -->
                <table class="style1">
                    <tr>
                        <td class="style3" colspan="2">
                            <strong>YÖNETİCİ GİRİŞİ</strong></td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Kullanıcı Adı :</td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Şifre :</td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="GİRİŞ" 
                                Width="170px" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <br />
                <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
                    DataFile="~/App_Data/habersitesi.accdb" 
                    SelectCommand="SELECT * FROM [haberler] WHERE kategori='Spor'"></asp:AccessDataSource>
            </div>
        </div>
        <!-- İçerik -->
        <div id="icerik">

    <h1>SPOR'DAN HABERLER</h1>
            &nbsp;
            <asp:Repeater ID="haberRepeater" runat="server">
    <ItemTemplate>
        <div class="haber-item">
            <h2><%# Eval("haber_baslik") %></h2>
            <p><%# Eval("haber_icerik") %></p>
            <asp:Image ID="haberResmi" runat="server" ImageUrl='<%# "Resimler/" + Eval("haber_resmi") %>' Width="200px" Height="200px" />
            <p><a href='<%# Eval("haber_linki") %>'>Daha fazla oku</a></p>
        </div>
    </ItemTemplate>
</asp:Repeater>


        </div>


    </div>

    <br />
        <h1 id="frm_bslik">MESAJ <span>GÖNDER</span></h1>
    <hr />
<div class="fr-genel">
    <div class="form-container">
        <h2>Mesaj Formu</h2>
        <asp:TextBox ID="txtIsim" runat="server" placeholder="İsim"></asp:TextBox><br />
<asp:TextBox ID="txtTelefonNo" runat="server" placeholder="Telefon Numarası"></asp:TextBox>
<asp:RegularExpressionValidator ID="regexTelefon" runat="server" ControlToValidate="txtTelefonNo" ErrorMessage="Geçerli bir telefon numarası giriniz." ValidationExpression="^(05(\d{9}))$"></asp:RegularExpressionValidator>
<asp:TextBox ID="txtMail" runat="server" placeholder="E-Posta"></asp:TextBox>
<asp:RegularExpressionValidator ID="regexEmail" runat="server" ControlToValidate="txtMail" ErrorMessage="Geçerli bir e-posta adresi giriniz." ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
<asp:TextBox ID="txtKonu" runat="server" TextMode="MultiLine" placeholder="Mesaj"></asp:TextBox><br />
<asp:Button ID="btnGonder" runat="server" Text="Gönder" OnClick="btnGonder_Click" CssClass="custom-button" /><br />
<asp:Label ID="lblSonuc" runat="server" CssClass="result"></asp:Label>

    </div>
    <div class="map-container">
        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3022.6944955565934!2d-74.00597368459804!3d40.71277508232677!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x89c24fa5d33f083b%3A0xc80b8f06e177fe62!2sNew%20York%2C%20NY%2C%20USA!5e0!3m2!1sen!2suk!4v1644909279368!5m2!1sen!2suk" width="500" height="400" style="border:0;" allowfullscreen="" loading="lazy"></iframe>
    </div>
</div>
    <br /><br />


</asp:Content>
