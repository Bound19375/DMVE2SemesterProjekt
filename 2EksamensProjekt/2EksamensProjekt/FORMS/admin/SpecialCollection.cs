namespace _2EksamensProjekt.FORMS.admin;

public partial class SpecialCollection : Form
{
    private readonly API api = API.Getinstance();
    public SpecialCollection()
    {
        InitializeComponent();
        api.GridviewCollection(dataGridView1, api.SpecialCollectionSql);
    }
}