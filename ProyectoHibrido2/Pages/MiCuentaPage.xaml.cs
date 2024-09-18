using ProyectoHibrido2.Services;

namespace ProyectoHibrido2.Pages;

public partial class MiCuentaPage : ContentPage
{
	private readonly ApiService _apiService;

	private readonly string NombreUsuarioKey = "usuarionombre";
	private readonly string EmailUsuarioKey = "usuarioemail";
	public MiCuentaPage(ApiService apiService)
	{
		InitializeComponent();
		_apiService = apiService;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		CargarInformacionUsuario();
		ImgBtnPerfil.Source = await GetImagenPerfilAsync();
	}

	private void CargarInformacionUsuario()
	{
		LblNombreUsuario.Text = Preferences.Get(NombreUsuarioKey, string.Empty);
		EntNombre.Text = LblNombreUsuario.Text;
		EntEmail.Text = Preferences.Get(EmailUsuarioKey, string.Empty);
	}

	private async Task<string?> GetImagenPerfilAsync()
	{
		string imagenDefecto = AppConfig.PerfilImagenDefecto;
		var (response, errorMessage) = await _apiService.GetImagenPerfilUsuario();

		if (errorMessage is not null)
		{
			switch (errorMessage)
			{
				case "Unauthorized":
					await DisplayAlert("Error", "No autorizado", "OK");
					return imagenDefecto;
				default:
					await DisplayAlert("Error", errorMessage ?? "No fue posible obtener la imagen", "OK");
					return imagenDefecto;
			}
		}
		if (response?.UrlImagen is not null)
		{
			return response.RutaImagen;
		}
		return imagenDefecto;
	}


      private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
		// para guaradar las preferencias del usuario y las modificaciones
		Preferences.Set(NombreUsuarioKey,EntNombre.Text);
		Preferences.Set(EmailUsuarioKey, EntEmail.Text);
		await DisplayAlert("Información Guardada", "Su información fue guardada con éxito", "OK");
    }
}