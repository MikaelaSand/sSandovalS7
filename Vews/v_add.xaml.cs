using System.Net;

namespace sSandovalS6.Vews;

public partial class v_add : ContentPage
{
	public v_add()
	{
		InitializeComponent();
	}

    private void btnagregar_Clicked(object sender, EventArgs e)
    {

    }

    private void btnagregar_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("nombre", txtNombre.Text);
            parametros.Add("apellido", txtApellido.Text);
            parametros.Add("edad", txtEdad.Text);

            cliente.UploadValues("http://192.168.56.1/moviles/post.php", "POST", parametros);
            Navigation.PushAsync(new Estudiante());
        }
        catch (Exception ex)
        {
            DisplayAlert("Alerta", ex.Message, "Ok");
        }
    }
}