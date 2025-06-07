using System.Text;
using Newtonsoft.Json;
using RichardTipantuñaComplementario.Models;

namespace RichardTipantuñaComplementario.Views;

public partial class Login : ContentPage
{
    private readonly HttpClient _httpClient;

    public Login()
    {
        InitializeComponent();
        _httpClient = new HttpClient();

    }
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        try
        {
            var usuario = txtUserM.Text;
            var contrasena = txtPassword.Text;
            var loginData = new
            {
                usuario = usuario,
                contrasena = contrasena
            };

            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://credp-s.net.ec/apiba.php?table=estudiante_login&action=login", content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var estudiante = JsonConvert.DeserializeObject<Estudiante_Login>(responseData);

                await Navigation.PushAsync(new Views.vEstudiante());
              //  await DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Credenciales inválidas", "OK");
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }
}