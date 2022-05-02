namespace _2EksamensProjekt.FORMS.admin;

public partial class AdminCreateHouse : Form
{
    public static string HouseType { get; set; } = "NONE";
    public static int M2 { get; set; } = 0;
    public static int Price { get; set; } = 0;

    API api = API.Getinstance();
    public AdminCreateHouse()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        HouseType = comboBox1.Text;
        M2 = Convert.ToInt32(textBox1.Text);
        Price = Convert.ToInt32(textBox2.Text);
        api.CreateNewHouse();
        this.Close();
    }
}