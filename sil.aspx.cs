using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sil : System.Web.UI.Page
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
        string sqlSorgu = "SELECT * FROM haberler";
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
            // Hata durumunda burada işlem yapılabilir
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
        // Seçilen satırın indeksini al
        int selectedIndex = GridView1.SelectedIndex;

        // Seçilen satırın ID'sini al
        string secilenID = GridView1.DataKeys[selectedIndex]["NO"].ToString();

        TextBox1.Text = secilenID;

        // Silme işlemi için kullanıcıya onay mesajı göster
        ClientScript.RegisterStartupScript(this.GetType(), "confirm", "confirm('Bu kaydı silmek istediğinizden emin misiniz?')", true);
    }

    protected void btnSil_Click(object sender, EventArgs e)
    {
        // GridView1'de seçili bir satır var mı kontrol edelim
        if (GridView1.SelectedRow != null)
        {
            // Seçili satırın indeksini al
            int selectedIndex = GridView1.SelectedIndex;

            // Seçilen satırın ID'sini alalım
            string secilenID = GridView1.DataKeys[selectedIndex]["NO"].ToString();

            // Silme işlemini gerçekleştirelim
            Sil(secilenID);
        }
        else
        {
            // Eğer seçili bir satır yoksa, kullanıcıya bir hata mesajı gösterin veya işlemi iptal edin.
            // Örneğin:
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Lütfen bir satır seçin.');", true);
        }
    }



    private void Sil(string id)
    {
        // Bağlantıyı aç
        VeriTabaniniBagla();

        // Silme sorgusunu tanımla ve bağlantıya bağlı bir SqlCommand nesnesi oluştur
        string deleteQuery = "DELETE FROM haberler WHERE NO = @ID";
        SqlCommand deleteCommand = new SqlCommand(deleteQuery, baglanti);
        deleteCommand.Parameters.AddWithValue("@ID", id);

        // Komutu çalıştır ve etkilenen satır sayısını geri döndür
        int affectedRows = deleteCommand.ExecuteNonQuery();

        // Bağlantıyı kapat
        baglanti.Close();

        // Eğer etkilenen satır sayısı 0 ise, silme işlemi başarısız olmuş demektir
        if (affectedRows == 0)
        {
            // İşlem başarısız uyarısı ver veya işlemle ilgili başka bir işlem yap
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Silme işlemi başarısız oldu.');", true);
        }
        else
        {
            // Silme işlemi başarılı olduğunda tekrar verileri getir
            VerileriGetir();
        }
    }

}

