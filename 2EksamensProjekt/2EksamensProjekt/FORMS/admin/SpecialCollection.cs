namespace _2EksamensProjekt.FORMS.admin;

public partial class SpecialCollection : Form
{
    private readonly API _api = API.GetInstance();
    public SpecialCollection()
    {
        InitializeComponent();
        _api.GridviewCollection(dataGridView1, _api.SpecialCollectionSql);
    }
}