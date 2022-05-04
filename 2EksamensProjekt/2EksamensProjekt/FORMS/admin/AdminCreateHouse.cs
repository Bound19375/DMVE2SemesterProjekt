namespace _2EksamensProjekt.FORMS.admin;

public partial class AdminCreateHouse : Form
{
    public static string HouseType { get; private set; } = "NONE";
    public static int M2 { get; private set; }
    public static int Price { get; private set; }

    private readonly API _api = API.GetInstance();
    public AdminCreateHouse()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        HouseType = comboBox1.Text;
        M2 = Convert.ToInt32(textBox1.Text);
        Price = Convert.ToInt32(textBox2.Text);
        _api.CreateNewHouse();
        Close();
    }
}