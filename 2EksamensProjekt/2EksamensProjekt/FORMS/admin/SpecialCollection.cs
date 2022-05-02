namespace _2EksamensProjekt.FORMS.admin;

public partial class SpecialCollection : Form
{
    API api = API.Getinstance();
    public SpecialCollection()
    {
        InitializeComponent();
        api.GridviewCollection(dataGridView1, api.SpecialCollectionSql);
    }
}