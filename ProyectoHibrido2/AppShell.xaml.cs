using ProyectoHibrido2.Pages;
using ProyectoHibrido2.Services;
using ProyectoHibrido2.Validations;

namespace ProyectoHibrido2
{
    public partial class AppShell : Shell
    {
        private readonly ApiService _apiService;
        private readonly IValidator _validator;

        public AppShell(ApiService apiService, IValidator validator)
        {
            InitializeComponent();
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _validator = validator;
            ConfigureShell();
        }
        private void ConfigureShell()
        {
                var homePage = new HomePage(_apiService, _validator);
                var carritoPage = new CarritoPage(_apiService, _validator);
                var favoritosPage = new FavoritosPage(_apiService,_validator);
                var perfilPage = new PerfilPage(_apiService, _validator);

                Items.Add(new TabBar
                {
                        Items =
                    {
                        new ShellContent {Title = "Home",Icon="home",Content=homePage},
                        new ShellContent {Title = "Carrito",Icon="cart",Content=carritoPage},
                        new ShellContent {Title = "Favoritos",Icon="heart",Content=favoritosPage},
                        new ShellContent {Title = "Perfil",Icon="profile2",Content=perfilPage}
                    }
                });

        }
    }
}

