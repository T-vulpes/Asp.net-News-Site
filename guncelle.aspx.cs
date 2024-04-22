using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class guncelle : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VerileriGetir();
        }
        
    }

    protected void VerileriGetir()
    {
        // Veritabanından verileri çekme
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT NO, haber_baslik, haber_icerik, kategori, haber_linki FROM haberler";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            HaberlerGridView.DataSource = dt;
            HaberlerGridView.DataBind();
        }
    }

    protected void HaberlerGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Seçilen haberin bilgilerini güncelleme formuna doldur
        GridViewRow row = HaberlerGridView.SelectedRow;
        HaberNOTextBox.Text = row.Cells[1].Text; // NO
        HaberBaslikTextBox.Text = row.Cells[2].Text; // Başlık
        HaberIcerikTextBox.Text = row.Cells[3].Text; // İçerik
        KategoriTextBox.Text = row.Cells[4].Text; // Kategori
        HaberLinkiTextBox.Text = row.Cells[5].Text; // Link
    }

    protected void GuncelleButton_Click(object sender, EventArgs e)
    {
        int haberNO;
        if (int.TryParse(HaberNOTextBox.Text, out haberNO))
        {
            string haberBaslik = HaberBaslikTextBox.Text;
            string haberIcerik = HaberIcerikTextBox.Text;
            string kategori = KategoriTextBox.Text;
            string haberLinki = HaberLinkiTextBox.Text;

            // Veritabanında güncelleme işlemi
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE haberler SET haber_baslik = @baslik, haber_icerik = @icerik, kategori = @kategori, haber_linki = @linki WHERE NO = @NO";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@baslik", Server.HtmlEncode(haberBaslik));
                command.Parameters.AddWithValue("@icerik", Server.HtmlEncode(haberIcerik));
                command.Parameters.AddWithValue("@kategori", Server.HtmlEncode(kategori));
                command.Parameters.AddWithValue("@linki", haberLinki);
                command.Parameters.AddWithValue("@NO", haberNO);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Güncelleme başarılı mesajı veya başka bir işlem
                        Response.Write("Haber başarıyla güncellendi.");
                        VerileriGetir(); // Tabloyu güncelle
                    }
                    else
                    {
                        // Güncelleme başarısız mesajı veya başka bir işlem
                        Response.Write("Haber güncelleme işlemi başarısız oldu.");
                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda mesajı görüntüle
                    Response.Write("Hata oluştu: " + ex.Message);
                }
            }
        }
        else
        {
            // Kullanıcının girdiği değer geçerli bir tamsayı değilse hata mesajı gösterin
            Response.Write("Geçerli bir haber numarası girin.");
        }
    }


}
