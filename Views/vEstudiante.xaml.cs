using System.Collections.ObjectModel;
using Newtonsoft.Json;
using RichardTipantuñaComplementario.Models;

namespace RichardTipantuñaComplementario.Views;

public partial class vEstudiante : ContentPage
{
    private const string Url = "https://credp-s.net.ec/apiba.php?table=estudiantes";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Estudiantes> estud;

    public vEstudiante()
    {
        InitializeComponent();
        mostrar();
    }

    public async void mostrar()
    {
        var content = await cliente.GetStringAsync(Url);

        var mostrarEst = JsonConvert.DeserializeObject<List<Estudiantes>>(content) ?? new();
       
        estud = new ObservableCollection<Estudiantes>(mostrarEst);
        

        
        lvEstudiantes.ItemsSource = estud;
    }

    private async void btnInsertar_Clicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new Views.vInsertar());
    }

    private void lvEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
       var objEstudiante = (Estudiantes)e.SelectedItem;
       Navigation.PushAsync(new Views.vActElim(objEstudiante));
    }
}
