using ProyectoHibrido2.Models;
using ProyectoHibrido2.Services;
using ProyectoHibrido2.Validations;

namespace ProyectoHibrido2.Pages
{
    public partial class FavoritosPage : ContentPage
    {
        private readonly FavoritosService _favoritosService;
        private readonly ApiService _apiService;
        private readonly IValidator _validator;


        public FavoritosPage(ApiService apiService, IValidator validator)
        {
            InitializeComponent();
            _favoritosService = new FavoritosService();
            _apiService = apiService;
            _validator = validator;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GetProductosFavoritos();
        }

        private async Task GetProductosFavoritos()
        {
            try
            {
                var productosFavoritos = await _favoritosService.ReadAllAsync();

                if (productosFavoritos is null || productosFavoritos.Count == 0)
                {
                    CVProductos.ItemsSource=null;
                    LblAviso.IsVisible=true;
                }
                else
                {
                    CVProductos.ItemsSource = productosFavoritos;
                    LblAviso.IsVisible = false;
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", $"Ocurrió un error inesperado: {ex.Message}", "OK");
            }
        }
        private void CVProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as ProductoFavorito;

            if (currentSelection == null) return;

            Navigation.PushAsync(new ProductoDetallesPage(currentSelection.ProductoId,
                                                          currentSelection.Nombre!,
                                                          _apiService, _validator));

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
