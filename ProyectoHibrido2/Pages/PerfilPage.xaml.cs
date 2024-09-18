using ProyectoHibrido2.Services;
using ProyectoHibrido2.Validations;

namespace ProyectoHibrido2.Pages
{
    public partial class PerfilPage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly IValidator _validator;
        private bool _loginPageDisplayed = false;

        public PerfilPage(ApiService apiService, IValidator validator)
        {
            InitializeComponent();
            LblNombreUsuario.Text = Preferences.Get("usuarionombre", string.Empty);
            _apiService = apiService;
            _validator = validator;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ImgBtnPerfil.Source = await GetImagenPerfil(); 
        }

        private async Task<string?> GetImagenPerfil()
        {
            string ImagenDefecto = AppConfig.PerfilImagenDefecto;

            var (response, errorMessage) = await _apiService.GetImagenPerfilUsuario();
            if (errorMessage is not null)
            {
                switch (errorMessage)
                {
                    case "Unauthorized":
                        if (!_loginPageDisplayed)
                        {
                            await DisplayLoginPage();
                            return null;
                        }
                        break;

                    default:
                        
                        await DisplayAlert("Error", errorMessage ?? "No fue posible obtener la imagen.", "OK");
                        break;
                        

                }

            }
            if (response?.UrlImagen is not null)
            {
                return response.RutaImagen;
            }
            return ImagenDefecto;
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {

        }

        private void MiCuenta_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new MiCuentaPage(_apiService));
        }

        private void MisPedidos_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new PedidosPage(_apiService, _validator));
        }

        private void Preguntas_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new PreguntasPage());
        }

        private async void ImgBtnPerfil_Clicked(object sender, EventArgs e)
        {
            try
            {
                var imagenArray = await SeleccionarImagenAsync();
                if(imagenArray is null)
                {
                    await DisplayAlert("Error", "No fue posible cargar la imagen", "OK");
                    return;
                }
                ImgBtnPerfil.Source = ImageSource.FromStream(()=>new MemoryStream(imagenArray));
                
                var response = await _apiService.UploadImagenUsuario(imagenArray);
                if(response.Data)
                {
                    await DisplayAlert("", "Imagen enviada con éxito", "OK");
                }
                else
                {
                    await DisplayAlert("Error", response.ErrorMessage ?? "Ocurrió un error", "Cancelar");
                }
                    
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error inesperado : {ex.Message}","OK");
            }
        }

        private async Task<byte[]?> SeleccionarImagenAsync()
        {
            try
            {
                var archivo = await MediaPicker.PickPhotoAsync();
                if (archivo is null) return null;

                using (var stream = await archivo.OpenReadAsync())
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            catch (FeatureNotSupportedException)
            {

                await DisplayAlert("Error", "La funcionalidad no es soportada por el dispositivo", "OK");
            }
            catch (PermissionException)
            {
                await DisplayAlert("Error", "Permiso no concedido para acceder a la cámara o a la galería", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al seleccionar la imagen {ex.Message}", "OK");
            }
            return null;
        }

            private void BtnLogout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("accesstoken", string.Empty);
            Application.Current!.MainPage = new NavigationPage(new LoginPage(_apiService, _validator));

        }

        private async Task DisplayLoginPage()
        {
            _loginPageDisplayed = true;
            await Navigation.PushAsync(new LoginPage(_apiService, _validator));
        }
    }
}
