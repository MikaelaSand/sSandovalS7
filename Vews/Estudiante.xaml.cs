using Newtonsoft.Json;
using sSandovalS6.Modelos;
using System.Collections.ObjectModel;
using System.Reflection;

namespace sSandovalS6.Vews;

public partial class Estudiante : ContentPage
{
	private const string Url = "http://192.168.56.1/moviles/post.php";

    private readonly HttpClient cliente = new HttpClient();
	private ObservableCollection<Modelos.Estudiantes> est;
	public Estudiante()
	{
		InitializeComponent();
        mostrar();
	}
    public async void mostrar()
    {
        var content = await cliente.GetStringAsync(Url);
        List<Modelos.Estudiantes> mostrar = JsonConvert.DeserializeObject<List<Modelos.Estudiantes>>(content);
        est = new ObservableCollection<Modelos.Estudiantes>(mostrar);
        listaEstudiantes.ItemsSource = est;

    }
    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Vews.v_add());
    }

    private void listaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objetoEstudiante = (Estudiantes)e.SelectedItem;
        Navigation.PushAsync(new v_delete(objetoEstudiante));
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        mostrar();
    }
}