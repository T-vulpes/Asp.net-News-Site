<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="~/anasayfa.aspx.cs" Inherits="anasayfa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        document.getElementById("admin-icon").addEventListener("click", function() {
            document.getElementById("popup").style.display = "block";
        });
        document.getElementsByClassName("close")[0].addEventListener("click", function() {
            document.getElementById("popup").style.display = "none";
        });
        window.addEventListener("click", function(event) {
            if (event.target == document.getElementById("popup")) {
                document.getElementById("popup").style.display = "none";
            }
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <div id="header">
        <img src="resimler/background.jpg" style="width: 100%; height: 520px;" />
    </div>

    <div class="doc-loader" id="loader">
                <img src="resimler/preloader.gif" alt="Seppo">
            </div> 
    
    <div id="kapsayici">
        <div id="popup" class="popup">
            <div class="popup-content">
                <span class="close">&times;</span>
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
            </div>
        </div>
        <div class="sayfayagiris">
            <asp:Label ID="lblZiyaretSayisi" runat="server"></asp:Label>
        </div>
        <div id="hakkimizda">
            <div id="hakkimizdaresim"><img src="resimler/hakkimizda.jpg" /></div>
            <div id="hakyazi">
                <h1>BİZ<span> KİMİZ?</span></h1>
                <hr />
                <p>
                    Hoş geldiniz! Biz DenizHaber, tutkulu bir ekip tarafından yönetilen, güvenilir ve 
                    tarafsız haberler sunan bir platformuz. Misyonumuz, 
                    okuyucularımıza her gün en güncel ve doğru bilgileri sağlamak,
                    topluma katkıda bulunmak ve demokratik bir toplumun önemini vurgulamaktır.</p>
                    <p>Ekibimizdeki her bir editör ve yazar, farklı uzmanlık alanlarına sahiptir ve çeşitli
                    perspektifler sunar. Amacımız, çeşitli konuları kapsayan, bilgilendirici ve ilgi çekici içerikler üretmektir. 
                    Siz değerli okuyucularımızın güvenini  kazanmak ve onlara en iyi haber 
                    deneyimini sunmak için sürekli çaba gösteriyoruz.</p>
                    <p>Teşekkür ederiz ki DenizHaber'i tercih ettiniz. 
                    Sizleri bilgilendirmek ve ilham vermek için buradayız.</p>
                

            </div>
        </div>
        <div id="icerik">
            <asp:DropDownList ID="filtre" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Filtre_SelectedIndexChanged" CssClass="dropdownlist">
                <asp:ListItem>Sırala..</asp:ListItem>
                <asp:ListItem>A&#39;dan - Z&#39;ye</asp:ListItem>
                <asp:ListItem>En Güncel</asp:ListItem>
                <asp:ListItem>Z&#39;den - A&#39;ya</asp:ListItem>
                <asp:ListItem>En Eski</asp:ListItem>
            </asp:DropDownList>
    <h1>SON DAKİKA HABERLERİ
            </h1>
            &nbsp;
             <asp:Repeater ID="haberRepeater" runat="server">
    <ItemTemplate>
        <div class="haber-item">
            <h2><%# Eval("haber_baslik") %></h2>
            <p><%# Eval("haber_icerik") %></p>
            <p><%# Eval("haber_tarihi") %></p>
            <asp:Image ID="haberResmi" runat="server" ImageUrl='<%# "Resimler/" + Eval("haber_resmi") %>' Width="200px" Height="200px" />
            <p><a href='<%# Eval("haber_linki") %>'>Daha fazla oku</a></p>
        </div>
<%--Eval ifadesi, ASP.NET Web Forms içinde veri bağlama işlemi için kullanılan bir yöntemdir. Bu yöntem, veri kaynağından gelen verileri bir
    web sayfasındaki HTML şablonlarına yerleştirmek için kullanılır. 
    Eval, belirli bir veri kaynağı alanının değerini almak ve HTML içinde görüntülemek için kullanılır.

    Örneğin, <%# Eval("haber_baslik") %> ifadesi, haber_baslik adlı bir alanın değerini alır ve HTML 
    içinde görüntüler. Bu ifade, Repeater kontrolü içinde bir veri kaynağına bağlı olduğunda, her 
    tekrarlanan öğe için bu alanın değerini alır ve belirtilen HTML etiketi içine yerleştirir.--%>
    </ItemTemplate>
</asp:Repeater>
        </div>
    </div>
    <br /><br /><br />
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
