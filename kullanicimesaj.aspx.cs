using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class kullanici : System.Web.UI.Page
{
    SqlConnection baglanti;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VeriTabaniniBagla();
            VerileriGetir();
        }
    }

    private void VerileriGetir()
    {
        string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Trusted_Connection=True;";
        SqlConnection baglanti = new SqlConnection(connectionString);
        string sqlSorgu = "SELECT * FROM mesajlar";
        SqlCommand komut = new SqlCommand(sqlSorgu, baglanti);

        try
        {
            baglanti.Open();
            SqlDataReader okuyucu = komut.ExecuteReader();

            if (okuyucu.HasRows)
            {
                GridView1.DataSource = okuyucu;
                GridView1.DataBind();
            }

            okuyucu.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Veri G�r�n�t�leme Hatas�. Tekrar Deneyiniz!');", true);
        }
        finally
        {
            baglanti.Close();
        }
    }

    private void VeriTabaniniBagla()
    {
        string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Trusted_Connection=True;";
        baglanti = new SqlConnection(connectionString);
        baglanti.Open();
    }



    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        // Se�ilen sat�r�n indeksini al
        int selectedIndex = GridView1.SelectedIndex;

        // Se�ilen sat�r�n ID'sini al
        string secilenID = GridView1.DataKeys[selectedIndex]["Kimlik"].ToString();

        TextBox1.Text = secilenID;

        // Silme i�lemi i�in kullan�c�ya onay mesaj� g�ster
        ClientScript.RegisterStartupScript(this.GetType(), "confirm", "confirm('Bu mesaj� silmek istedi�inizden emin misiniz?')", true);
    }

    protected void btnSil_Click(object sender, EventArgs e)
    {
        // GridView1'de se�ili bir sat�r var m� kontrol edelim
        if (GridView1.SelectedRow != null)
        {
            // Se�ili sat�r�n indeksini al
            int selectedIndex = GridView1.SelectedIndex;

            // Se�ilen sat�r�n ID'sini alal�m
            string secilenID = GridView1.DataKeys[selectedIndex]["Kimlik"].ToString();

            // Silme i�lemini ger�ekle�tirelim
            Sil(secilenID);
        }
        else
        {
            // E�er se�ili bir sat�r yoksa, kullan�c�ya bir hata mesaj� g�sterin veya i�lemi iptal edin.
            // �rne�in:
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('L�tfen bir sat�r se�in.');", true);
        }
    }



    private void Sil(string id)
    {
        // Ba�lant�y� a�
        VeriTabaniniBagla();

        // Silme sorgusunu tan�mla ve ba�lant�ya ba�l� bir SqlCommand nesnesi olu�tur
        string deleteQuery = "DELETE FROM mesajlar WHERE Kimlik = @ID";
        SqlCommand deleteCommand = new SqlCommand(deleteQuery, baglanti);
        deleteCommand.Parameters.AddWithValue("@ID", id);

        // Komutu �al��t�r ve etkilenen sat�r say�s�n� geri d�nd�r
        int affectedRows = deleteCommand.ExecuteNonQuery();

        // Ba�lant�y� kapat
        baglanti.Close();

        // E�er etkilenen sat�r say�s� 0 ise, silme i�lemi ba�ar�s�z olmu� demektir
        if (affectedRows == 0)
        {
            // ��lem ba�ar�s�z uyar�s� ver veya i�lemle ilgili ba�ka bir i�lem yap
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Silme i�lemi ba�ar�s�z oldu.');", true);
        }
        else
        {
            // Silme i�lemi ba�ar�l� oldu�unda tekrar verileri getir
            VerileriGetir();
        }
    }

}