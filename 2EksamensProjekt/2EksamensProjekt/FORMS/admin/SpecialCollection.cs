namespace _2EksamensProjekt.FORMS.admin;

public partial class SpecialCollection : Form
{
    private readonly API _api = API.GetInstance();
    public SpecialCollection()
    {
        InitializeComponent();
    }

    public void ShowSpecialCollection(DataGridView gv, API.SPECIALCOLLECTION specialcollection)
    {
        _api.SpecialCollectionList(dataGridView1, specialcollection);
    }
}