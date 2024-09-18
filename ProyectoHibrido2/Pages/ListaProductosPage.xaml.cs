using ProyectoHibrido2.Models;
using ProyectoHibrido2.Services;
using ProyectoHibrido2.Validations;


namespace ProyectoHibrido2.Pages;

public partial class ListaProductosPage : ContentPage
{
	private readonly ApiService _apiService;
	private readonly IValidator _validator;
	private int _categoriaId;
	private bool _loginPageDisplayed = false;
    public ListaProductosPage(int categoriaId, string categoriaNombre, ApiService apiService, IValidator validator)
    {
        InitializeComponent();
        _apiService = apiService;
        _validator = validator;
        _categoriaId = categoriaId;
        Title = categoriaNombre ?? "Productos";
    }

    protected override  async void OnAppearing()
    {
        base.OnAppearing();
        await GetListaProductos(_categoriaId);
    }

    private async Task<IEnumerable<Producto>> GetListaProductos(int categoriaId)
    {
        try
        {
            var (productos, errorMessage) = await _apiService.GetProductos("categoria", categoriaId.ToString());

            if (errorMessage == "Unauthorized" && !_loginPageDisplayed) 
            {
                await DisplayLoginPage();
                return Enumerable.Empty<Producto>();
            }

            if (productos is null)
            {
                await DisplayAlert("Error", errorMessage ?? "No es posible obtener las categorías", "OK");
                return Enumerable.Empty<Producto>();

            }

            CvProductos.ItemsSource = productos;
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
        await Navigation.PushAsync(new LoginPage(_apiService,_validator));
    }

    private void CvProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Producto;

        if (currentSelection is null)
            return;

        Navigation.PushAsync(new ProductoDetallesPage(currentSelection.Id,
                                                      currentSelection.Nombre!,
                                                      _apiService, _validator));

        ((CollectionView)sender).SelectedItem = null;
    }
}