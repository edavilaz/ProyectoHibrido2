using ProyectoHibrido2.Services;
using ProyectoHibrido2.Validations;

namespace ProyectoHibrido2.Pages
{
    public partial class LoginPage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly IValidator _validator;
        public LoginPage(ApiService apiService, IValidator validator)
        {
            InitializeComponent();
            _apiService = apiService;
            _validator = validator;
        }

        private async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EntEmail.Text))
            {
                await DisplayAlert("Error", "Falta el Email", "Cancelar");
                return;
            }
            if (string.IsNullOrEmpty(EntPassword.Text))
            {
                await DisplayAlert("Error", "Falta el Password", "Cancelar");
                return;

            }
            var response = await _apiService.Login(EntEmail.Text, EntPassword.Text);

            if (!response.HasError)
            {
                Application.Current!.MainPage = new AppShell(_apiService,_validator);
            }
            else
            {
                await DisplayAlert("Error", "Algo está mal", "Cancelar");
            }

        }

        private async void Registrarse_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new RegistrarsePage(_apiService, _validator));

        }
    }
}
