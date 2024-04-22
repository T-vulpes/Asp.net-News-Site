using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web;

public partial class anasayfa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Kullanıcının kaç kez ana sayfaya girdiğini kontrol etmek için bir çerez kullanacağız.
            if (Request.Cookies["ziyaretSayisi"] == null)
            {
                // Eğer kullanıcı daha önce ziyaret etmemişse, çereze "1" değerini atayacağız.
                HttpCookie ziyaretSayisiCookie = new HttpCookie("ziyaretSayisi", "1");

                // Çerezin süresini belirleyelim, örneğin 1 yıl.
                ziyaretSayisiCookie.Expires = DateTime.Now.AddYears(1);

                // Çerezi ekleyelim.
                Response.Cookies.Add(ziyaretSayisiCookie);
            }
            else
            {
                // Eğer kullanıcı daha önce ziyaret etmişse, çerezdeki ziyaret sayısını arttıralım.
                int ziyaretSayisi = Convert.ToInt32(Request.Cookies["ziyaretSayisi"].Value) + 1;

                // Ziyaret sayısını çerezde güncelleyelim.
                Response.Cookies["ziyaretSayisi"].Value = ziyaretSayisi.ToString();
            }

            // Kullanıcının ziyaret sayısını gösterelim.
            lblZiyaretSayisi.Text = "Ana sayfaya " + Request.Cookies["ziyaretSayisi"].Value + " kez giriş yapıldı.";

            // Filtre dropdownlistinin seçilen değeri değiştiğinde tetiklenecek olay
            filtre.SelectedIndexChanged += new EventHandler(Filtre_SelectedIndexChanged);

            // Sayfa yüklendiğinde haberleri getir
            VeriGetir("SELECT * FROM haberler");
        }
    }

    protected void Filtre_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Seçilen filtre türüne göre SQL sorgusu oluştur
        string filtreTipi = filtre.SelectedValue;

        string sorgu = "";

        switch (filtreTipi)
        {
            case "A'dan - Z'ye":
                sorgu = "SELECT * FROM haberler ORDER BY haber_baslik ASC";
                break;
            case "En Güncel":
                sorgu = "SELECT * FROM haberler ORDER BY haber_tarihi DESC";
                break;
            case "Z'den - A'ya":
                sorgu = "SELECT * FROM haberler ORDER BY haber_baslik DESC";
                break;
            case "En Eski":
                sorgu = "SELECT * FROM haberler ORDER BY haber_tarihi ASC";
                break;
            default:
                // Varsayılan olarak tüm haberleri getir
                sorgu = "SELECT * FROM haberler";
                break;
        }

        // Haberleri filtrele
        VeriGetir(sorgu);
    }

    private void VeriGetir(string sorgu)
    {
        // SQL Server veritabanı bağlantısı
        string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Integrated Security=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(sorgu, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable(); //datatable oluşturduk.
                    adapter.Fill(dataTable); /*Adaptörü kullanarak veritabanından veri alır ve DataTable nesnesine doldurur.*/
                    haberRepeater.DataSource = dataTable; //haberRepeater adlı Repeater kontrolünün veri kaynağını belirler.Bu durumda, DataTable içindeki veriler haberRepeater kontrolüne bağlanır.
                   haberRepeater.DataBind(); //Repeater kontrolünün veri kaynağını bağlar ve kontrolü sayfada göstermek için verileri bağlar.
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // SQL Server veritabanı bağlantısı
        string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Trusted_Connection=True;";
        SqlConnection baglanti = new SqlConnection(connectionString);
        baglanti.Open();

        // Kullanıcı giriş sorgusu
        string sqlSorgu = "SELECT * FROM uyelik WHERE kullaniciadi=@kullaniciadi AND sifre=@sifre";
        SqlCommand komut = new SqlCommand(sqlSorgu, baglanti);
        komut.Parameters.AddWithValue("@kullaniciadi", TextBox1.Text); //u satır, SqlCommand nesnesine parametreler ekler. AddWithValue yöntemi, komut nesnesine bir parametre ekler ve parametrenin adı ve değeri verilir. @kullaniciadi parametresine, TextBox1.Text özelliğinden (yani bir kullanıcı giriş kutusundan) alınan değer atanır. Bu yöntem, SQL sorgusunu daha güvenli hale getirmek için kullanılır, çünkü kullanıcı girdileri parametreler aracılığıyla sorguya bağlanır ve bu şekilde SQL enjeksiyon saldırıları önlenir.
        komut.Parameters.AddWithValue("@sifre", TextBox2.Text);

        SqlDataReader okuyucu = komut.ExecuteReader();
        if (okuyucu.HasRows)
        {
            // Kullanıcı girişi başarılı olduğunda kullanıcının adını oturum değişkenine kaydet
            Session["UserName"] = TextBox1.Text;
            Session["sifre"] = TextBox2.Text;
            Response.Redirect("adminpanel.aspx");


        }
        else
        {
            // Kullanıcı girişi başarısız olduğunda hata mesajını göster
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Kullanıcı adı veya şifre hatalı!');", true);
        }

        okuyucu.Close();
        baglanti.Close();
    }

    protected void btnGonder_Click(object sender, EventArgs e)
    {
        // SQL Server veritabanı bağlantısı
        string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Trusted_Connection=True;";

        // Veritabanı bağlantısını aç
        using (SqlConnection baglanti = new SqlConnection(connectionString))
        {
            // Veritabanı komutunu hazırla
            string sqlKomut = "INSERT INTO mesajlar (isim, telefonno, mail, haber) VALUES (@isim, @telefonno, @mail, @haber)";
            using (SqlCommand komut = new SqlCommand(sqlKomut, baglanti))
            {
                // Parametreleri ekleyerek SQL komutunu güvenli hale getir
                komut.Parameters.AddWithValue("@isim", txtIsim.Text);
                komut.Parameters.AddWithValue("@telefonno", txtTelefonNo.Text);
                komut.Parameters.AddWithValue("@mail", txtMail.Text);
                komut.Parameters.AddWithValue("@haber", txtKonu.Text);

                try
                {
                    // Veritabanı bağlantısını aç
                    baglanti.Open();

                    // Komutu çalıştır
                    komut.ExecuteNonQuery();

                    // Başarı mesajı göster
                    lblSonuc.Text = "Mesaj başarıyla gönderildi!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Mesaj başarıyla gönderildi!');", true);

                }
                catch (Exception ex)
                {
                    // Hata durumunda hata mesajını göster
                    lblSonuc.Text = "Hata oluştu: " + ex.Message;
                }
            }
        }
    }
}
