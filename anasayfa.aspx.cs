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
            if (Request.Cookies["ziyaretSayisi"] == null)
            {
                HttpCookie ziyaretSayisiCookie = new HttpCookie("ziyaretSayisi", "1");
                ziyaretSayisiCookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(ziyaretSayisiCookie);
            }
            else
            {
                int ziyaretSayisi = Convert.ToInt32(Request.Cookies["ziyaretSayisi"].Value) + 1;
                Response.Cookies["ziyaretSayisi"].Value = ziyaretSayisi.ToString();
            }
            lblZiyaretSayisi.Text = "Ana sayfaya " + Request.Cookies["ziyaretSayisi"].Value + " kez giriş yapıldı.";
            filtre.SelectedIndexChanged += new EventHandler(Filtre_SelectedIndexChanged);
            VeriGetir("SELECT * FROM haberler");
        }
    }

    protected void Filtre_SelectedIndexChanged(object sender, EventArgs e)
    {
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
                sorgu = "SELECT * FROM haberler";
                break;
        }
        VeriGetir(sorgu);
    }

    private void VeriGetir(string sorgu)
    {
        string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Integrated Security=True;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(sorgu, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable(); 
                    adapter.Fill(dataTable);
                    haberRepeater.DataSource = dataTable; 
                   haberRepeater.DataBind(); 
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Trusted_Connection=True;";
        SqlConnection baglanti = new SqlConnection(connectionString);
        baglanti.Open();

        string sqlSorgu = "SELECT * FROM uyelik WHERE kullaniciadi=@kullaniciadi AND sifre=@sifre";
        SqlCommand komut = new SqlCommand(sqlSorgu, baglanti);
        komut.Parameters.AddWithValue("@kullaniciadi", TextBox1.Text); //u satır, SqlCommand nesnesine parametreler ekler. AddWithValue yöntemi, komut nesnesine bir parametre ekler ve parametrenin adı ve değeri verilir. @kullaniciadi parametresine, TextBox1.Text özelliğinden (yani bir kullanıcı giriş kutusundan) alınan değer atanır. Bu yöntem, SQL sorgusunu daha güvenli hale getirmek için kullanılır, çünkü kullanıcı girdileri parametreler aracılığıyla sorguya bağlanır ve bu şekilde SQL enjeksiyon saldırıları önlenir.
        komut.Parameters.AddWithValue("@sifre", TextBox2.Text);

        SqlDataReader okuyucu = komut.ExecuteReader();
        if (okuyucu.HasRows)
        {
            Session["UserName"] = TextBox1.Text;
            Session["sifre"] = TextBox2.Text;
            Response.Redirect("adminpanel.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Kullanıcı adı veya şifre hatalı!');", true);
        }

        okuyucu.Close();
        baglanti.Close();
    }

    protected void btnGonder_Click(object sender, EventArgs e)
    {
        string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Trusted_Connection=True;";
        using (SqlConnection baglanti = new SqlConnection(connectionString))
        {
            string sqlKomut = "INSERT INTO mesajlar (isim, telefonno, mail, haber) VALUES (@isim, @telefonno, @mail, @haber)";
            using (SqlCommand komut = new SqlCommand(sqlKomut, baglanti))
            {
                komut.Parameters.AddWithValue("@isim", txtIsim.Text);
                komut.Parameters.AddWithValue("@telefonno", txtTelefonNo.Text);
                komut.Parameters.AddWithValue("@mail", txtMail.Text);
                komut.Parameters.AddWithValue("@haber", txtKonu.Text);
                try
                {
                    baglanti.Open();
                    komut.ExecuteNonQuery();

                    lblSonuc.Text = "Mesaj başarıyla gönderildi!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Mesaj başarıyla gönderildi!');", true);

                }
                catch (Exception ex)
                {
                    lblSonuc.Text = "Hata oluştu: " + ex.Message;
                }
            }
        }
    }
}
