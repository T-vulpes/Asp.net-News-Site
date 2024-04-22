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
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Veri Görünütüleme Hatasý. Tekrar Deneyiniz!');", true);
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
        // Seçilen satýrýn indeksini al
        int selectedIndex = GridView1.SelectedIndex;

        // Seçilen satýrýn ID'sini al
        string secilenID = GridView1.DataKeys[selectedIndex]["Kimlik"].ToString();

        TextBox1.Text = secilenID;

        // Silme iþlemi için kullanýcýya onay mesajý göster
        ClientScript.RegisterStartupScript(this.GetType(), "confirm", "confirm('Bu mesajý silmek istediðinizden emin misiniz?')", true);
    }

    protected void btnSil_Click(object sender, EventArgs e)
    {
        // GridView1'de seçili bir satýr var mý kontrol edelim
        if (GridView1.SelectedRow != null)
        {
            // Seçili satýrýn indeksini al
            int selectedIndex = GridView1.SelectedIndex;

            // Seçilen satýrýn ID'sini alalým
            string secilenID = GridView1.DataKeys[selectedIndex]["Kimlik"].ToString();

            // Silme iþlemini gerçekleþtirelim
            Sil(secilenID);
        }
        else
        {
            // Eðer seçili bir satýr yoksa, kullanýcýya bir hata mesajý gösterin veya iþlemi iptal edin.
            // Örneðin:
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Lütfen bir satýr seçin.');", true);
        }
    }



    private void Sil(string id)
    {
        // Baðlantýyý aç
        VeriTabaniniBagla();

        // Silme sorgusunu tanýmla ve baðlantýya baðlý bir SqlCommand nesnesi oluþtur
        string deleteQuery = "DELETE FROM mesajlar WHERE Kimlik = @ID";
        SqlCommand deleteCommand = new SqlCommand(deleteQuery, baglanti);
        deleteCommand.Parameters.AddWithValue("@ID", id);

        // Komutu çalýþtýr ve etkilenen satýr sayýsýný geri döndür
        int affectedRows = deleteCommand.ExecuteNonQuery();

        // Baðlantýyý kapat
        baglanti.Close();

        // Eðer etkilenen satýr sayýsý 0 ise, silme iþlemi baþarýsýz olmuþ demektir
        if (affectedRows == 0)
        {
            // Ýþlem baþarýsýz uyarýsý ver veya iþlemle ilgili baþka bir iþlem yap
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Silme iþlemi baþarýsýz oldu.');", true);
        }
        else
        {
            // Silme iþlemi baþarýlý olduðunda tekrar verileri getir
            VerileriGetir();
        }
    }

}