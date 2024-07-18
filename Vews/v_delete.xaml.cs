using sSandovalS6.Modelos;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace sSandovalS6.Vews;

public partial class v_delete : ContentPage
{
    Estudiantes estudiante = new Estudiantes();
	public v_delete(Estudiantes datos)
	{
		InitializeComponent();
        estudiante = datos;
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new NameValueCollection();
            parametros.Add("codigo", estudiante.codigo.ToString());
            parametros.Add("nombre", txtNombre.Text);
            parametros.Add("apellido", txtApellido.Text);
            parametros.Add("edad", txtEdad.Text);

            string url = "http://192.168.56.1/moviles/post.php";

            UriBuilder uriBuilder = new UriBuilder(url);
            var query = new StringBuilder();
            foreach (string key in parametros)
            {
                query.AppendFormat("{0}={1}&", key, WebUtility.UrlEncode(parametros[key]));
            }
            uriBuilder.Query = query.ToString().TrimEnd('&');
            Uri uri = uriBuilder.Uri;

            // Realizar la solicitud PUT
            byte[] response = cliente.UploadValues(uri, "PUT", new NameValueCollection());
            string responseString = Encoding.UTF8.GetString(response);
            DisplayAlert("�xito", "Estudiante actualizado correctamente", "Ok");

            Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            DisplayAlert("Alerta", ex.Message, "Ok");
        }
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();

            // Agrega la URL correcta para la eliminaci�n
            string url = $"http://192.168.56.1/moviles/post.php?codigo={estudiante.codigo}";

            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                cliente.UploadString(url, "DELETE", string.Empty);
                DisplayAlert("�xito", "Estudiante eliminado correctamente", "Ok");
            }
            else
            {
                DisplayAlert("Error", "La URL no es v�lida", "Ok");
            }

            Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            DisplayAlert("Alerta", $"Excepci�n general: {ex.Message}", "Ok");
        }
    }
}