using ProyectoHibrido2.Services;
using ProyectoHibrido2.Validations;

namespace ProyectoHibrido2.Pages
{
    public partial class RegistrarsePage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly IValidator _validator;
        public RegistrarsePage(ApiService apiService, IValidator validator)
        {
            InitializeComponent();
            _apiService = apiService;
            _validator = validator;
        }

        private async void BtnSignUp_ClickedAsync(object sender, EventArgs e)
        {
            var response = await _apiService.RegistrarUsuario(EntNombre.Text, EntEmail.Text, EntPassword.Text);

            if (await _validator.Validar(EntNombre.Text, EntEmail.Text, EntPassword.Text))
            {
                if (!response.HasError)
                {
                    await DisplayAlert("Aviso", "Su cuenta fue creada !!", "OK");
                    await Navigation.PushAsync(new LoginPage(_apiService, _validator));
                }
                else
                {
                    await DisplayAlert("Error", "Algo esta mal!!!", "Cancelar");
                }
            }
            else
            {
                string mensajeError = "";
                mensajeError += _validator.NombreError != null ? $"\n- {_validator.NombreError}" : "";
                mensajeError += _validator.EmailError != null ? $"\n- {_validator.EmailError}" : "";
                mensajeError += _validator.PasswordError != null ? $"\n- {_validator.PasswordError}" : "";

                await DisplayAlert("Error", mensajeError, "OK");

            }
        }

        private async void TapLogin_TappedAsync(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new LoginPage(_apiService, _validator));
        }
    }
}
