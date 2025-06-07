using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using RichardTipantuñaComplementario.Models;

namespace RichardTipantuñaComplementario.Views;

public partial class vActElim : ContentPage
{
    private readonly HttpClient _httpClient;
    private object data;

    public vActElim(Estudiante datos)
    {
        InitializeComponent();
        _httpClient = new HttpClient();


        // Envio los datos
        txtCodigo.Text = datos.Cod_Estudiante.ToString();
        txtNombre.Text = datos.Nombre.ToString();
        txtDescripcion.Text = datos.Apellido.ToString();
        txtStatus.Text = datos.Curso.ToString();
        int id = int.Parse(txtCodigo.Text); // id del registro a actualizar
        var data = new
        {
            nombrerol = txtNombre.Text,
            descripcion = txtDescripcion.Text,
            status = txtStatus.Text
        };

    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        int id = int.Parse(txtCodigo.Text);
        var data = new
        {
            nombrerol = txtNombre.Text,
            descripcion = txtDescripcion.Text,
            status = txtStatus.Text
        };
        try
        {
            string url = $"https://credp-s.net.ec/api.php?table=rol&{GetPrimaryKeyParamName()}={id}";

            string json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Éxito", "Registro actualizado correctamente.", "OK");
            }
            else
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", $"Error al actualizar: {errorResponse}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
        await Navigation.PushAsync(new vEstudiante());
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        int id = int.Parse(txtCodigo.Text);
        var result = await DisplayAlert("Eliminar", "¿Está seguri que desea eliminar este elemento?", "Eliminar", "Cancelar");
        {
            try
            {
                //string url = $"https://credp-s.net.ec/api.php?table=rol&idrol={id}";
                string url = $"https://credp-s.net.ec/api.php?table=rol&{GetPrimaryKeyParamName()}={id}";


                var response = await _httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Registro eliminado correctamente.", "OK");
                    await Navigation.PushAsync(new vEstudiante());
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"Error al eliminar: {errorResponse}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }
    }
    private string GetPrimaryKeyParamName()
    {
        return "idrol"; // Cambia esto si tu clave primaria es diferente
    }

}