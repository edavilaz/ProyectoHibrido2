using ProyectoHibrido2.Pages;
using ProyectoHibrido2.Services;
using ProyectoHibrido2.Validations;

namespace ProyectoHibrido2

{
    //public partial class App : Application
    //{
    //    private readonly ApiService _apiService;
    //    private readonly IValidator _validator;
    //    public App(ApiService apiService, IValidator validator)
    //    {
    //        InitializeComponent();


    //        //MainPage = new LoginView();

    //        _apiService = apiService;
    //        //MainPage = new AppShell();
    //        _validator = validator;
    //        MainPage = new NavigationPage(new RegistrarsePage(_apiService, _validator));
    //    }
    //}

    public partial class App : Application
    {
        private readonly ApiService _apiService;
        private readonly IValidator _validator;
        public App(ApiService apiService, IValidator validator)
        {
            InitializeComponent();
            _apiService = apiService;
            _validator = validator;

            SetMainPage();
        }

        private void SetMainPage()
        {
            var accessToken = Preferences.Get("accesstoken", string.Empty);

            if (string.IsNullOrEmpty(accessToken))
            {
                MainPage = new NavigationPage(new LoginPage(_apiService, _validator));
                return;
            }

            MainPage = new AppShell(_apiService, _validator);
        }
    }
}
