using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace RichardTipantuñaComplementario.Views;

public partial class vInsertar : ContentPage
    {
    private readonly HttpClient _httpClient;

    public vInsertar()
        {
            InitializeComponent();
        _httpClient = new HttpClient();
    }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {

        var nombre = txtNombre.Text;
        var apellido = txtApellido.Text;
        var curso = txtCurso.Text;
        var paralelo=txtParalelo.Text;
        var nota = txtNota.Text;

        var data = new
        {
            nombre = nombre,
            apellido = apellido,
            curso = curso,
            paralelo=paralelo,
            nota=nota
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync("https://credp-s.net.ec/api.php?table=rol", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                // Manejar la respuesta exitosa
                await Navigation.PushAsync(new Views.vEstudiante());

            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                // Manejar el error
                await DisplayAlert("Error", $"Error: {errorResponse}", "OK");
            }
        }
        catch (Exception ex)
        {
            // Manejar excepciones
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }

    }
}