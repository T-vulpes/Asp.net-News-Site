<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"%>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            string userName = Session["UserName"].ToString();
            Label1.Text = userName;
            // Kullan�c� ad� mevcut, istedi�iniz i�lemleri ger�ekle�tirin
        }
        if (Session["sifre"] != null)
            {
                string sifre = Session["sifre"] as string;
                Label2.Text = sifre;
            }
        else
        {
            Label2.Text = "�ifre bulunamad�!";
            // Kullan�c� giri� yapmam��, iste�e ba�l� olarak farkl� bir i�lem yapabilirsiniz
        }
    }
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div id="kapsayici">
        <div id="yonetici">
            <div id="adminmenu">
                <ul>
                    <li><a href="sil.aspx">Haber Silme</a></li>
                    <li><a href="haberekle.aspx">Haber Ekleme</a></li>
                    <li><a href="guncelle.aspx">Haber G�ncelleme</a></li>
                    <li><a href="kullanicimesaj.aspx">Kullan�c� Mesajlar�</a></li>
                </ul>
            </div>
            <div id="icerik">
                <p id="ho�geldiniz">ADM�N PANEL�NE HO� GELD�N�Z!<br />
                    <strong>Kullan�c� Ad� :</strong><asp:Label ID="Label1" runat="server"></asp:Label>
                </p>
                <p><strong>�ifre:</strong>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                    <br />
                </p>
                <p>&nbsp;</p>
                <br /><br /><br /><br />
            </div>
        </div>
    </div>
</asp:Content>
