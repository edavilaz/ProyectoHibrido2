using ProyectoHibrido2.Models;
using ProyectoHibrido2.Services;
using ProyectoHibrido2.Validations;

namespace ProyectoHibrido2.Pages
{
    public partial class HomePage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly IValidator _validator;
        private bool _loginPageDisplayed = false;


        public HomePage(ApiService apiService, IValidator validator)
        {
            InitializeComponent();
            lblNombreUsuario.Text="Hola " + Preferences.Get("usuarionombre",string.Empty);
            _apiService = apiService ??throw new ArgumentNullException(nameof(apiService));
            _validator = validator;
            Title= AppConfig.TitleHomePage;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GetListaCategorias();
            await GetMasVendidos();
            await GetPopulares();
        }

        private async Task<IEnumerable<Categoria>>GetListaCategorias()
        {
            try
            {
                var (categorias, errorMenssage) = await _apiService.GetCategorias();
                if (errorMenssage == "Unauthorized" && !_loginPageDisplayed) 
                {
                    await DisplayLoginPage();
                    return Enumerable.Empty<Categoria>();
                }
                if(categorias == null)
                {
                    await DisplayAlert("Error", errorMenssage ?? "No se pudieron obtener las categorías", "OK");
                    return Enumerable.Empty<Categoria>(); 
                }
                CvCategorias.ItemsSource = categorias;
                return categorias;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error",$"Ocurrió un error inesperado: {ex.Message}","OK");
                return Enumerable.Empty<Categoria>();
                
            }
        }

        private async Task<IEnumerable<Producto>> GetMasVendidos()
        {
            try
            {
                var (productos, errorMessage) = await _apiService.GetProductos("masvendido", string.Empty);
                if (errorMessage == "Unauthorized" && !_loginPageDisplayed)
                {
                    await DisplayLoginPage();
                    return Enumerable.Empty<Producto>();
                }

                if(productos == null)
                {
                    await DisplayAlert("Error", errorMessage ?? "No fue posible obtener las categorías.", "OK");
                    return Enumerable.Empty<Producto>();
                }

                CvMasVendidos.ItemsSource = productos;
                return productos;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error inesperado: {ex.Message}", "OK");
                return Enumerable.Empty<Producto>();
                
            }
        }

        private async Task<IEnumerable<Producto>> GetPopulares()
        {
            try
            {
                var (productos, errorMessage) = await _apiService.GetProductos("popular", string.Empty);
                if (errorMessage == "Unauthorized" && !_loginPageDisplayed)
                {
                    await DisplayLoginPage();
                    return Enumerable.Empty<Producto>();
                }

                if (productos == null)
                {
                    await DisplayAlert("Error", errorMessage ?? "No fue posible obtener las categorías.", "OK");
                    return Enumerable.Empty<Producto>();
                }

                CvPopulares.ItemsSource = productos;
                return productos;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error inesperado: {ex.Message}", "OK");
                return Enumerable.Empty<Producto>();

            }
        }


        private async Task DisplayLoginPage()
        {
            _loginPageDisplayed = true;
            await Navigation.PushAsync(new LoginPage(_apiService, _validator));
        }

        private void CvCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Categoria;
            
            if (currentSelection is null) return;
            
                Navigation.PushAsync(new ListaProductosPage(currentSelection.Id, 
                                                            currentSelection.Nombre!, 
                                                            _apiService, 
                                                            _validator));

                ((CollectionView)sender).SelectedItem = null;
            

        }

        private void CvMasVendidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender is CollectionView collectionView)
            {
                NavigatedToProductoDetallesPage(collectionView, e);
            }
        }

       
        private void CvPopulares_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is CollectionView collectionView)
            {
                NavigatedToProductoDetallesPage(collectionView, e);
            }
        }

        private void NavigatedToProductoDetallesPage(CollectionView collectionView, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Producto;

            if (currentSelection is null)
                return;
            Navigation.PushAsync(new ProductoDetallesPage(currentSelection.Id, 
                                                          currentSelection.Nombre!,
                                                        _apiService,_validator));

            collectionView.SelectedItem = null;
        }


    }
}
