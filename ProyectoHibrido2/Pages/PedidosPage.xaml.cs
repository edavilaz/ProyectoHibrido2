using ProyectoHibrido2.Models;
using ProyectoHibrido2.Services;
using ProyectoHibrido2.Validations;

namespace ProyectoHibrido2.Pages;

public partial class PedidosPage : ContentPage
{
	private readonly ApiService _apiService;
	private readonly IValidator _validator;

	private bool _loginPageDisplayed = false;


    public PedidosPage(ApiService apiService, IValidator validator)
    {
        InitializeComponent();
        _apiService = apiService;
        _validator = validator;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetListaPedidos();
    }

    private async Task GetListaPedidos() 
    {
        try
        {
            var (pedidos, errorMessage) = await _apiService.GetPedidosUsuario(Preferences.Get("usuarioid", 0));
            if (errorMessage == "Unauthorized" && !_loginPageDisplayed)
            {
                await DisplayLoginPage();
                return;
            }
            if(errorMessage == "NotFound")
            {
                await DisplayAlert("Aviso", "No existen pedidos para el cliente.", "OK");
                return;

            }
            if (pedidos is null)
            {
                await DisplayAlert("Error", errorMessage ?? "No fue posible obtener pedidos.", "OK");
                return;
            }
            else
            {
                CvPedidos.ItemsSource = pedidos;
            }

        }
        catch (Exception)
        {

            await DisplayAlert("Error", "Ocurrió un error al obtener los pedidos", "OK");
        }   
        

    }


    private void CvPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection.FirstOrDefault() as PedidoPorUsuario;
        if (selectedItem == null) return;

        Navigation.PushAsync(new PedidoDetallesPage(selectedItem.Id,
            selectedItem.PedidoTotal,_apiService,_validator));
        ((CollectionView)sender).SelectedItem = null;

    }
    private async Task DisplayLoginPage()
    {
        _loginPageDisplayed = true;
        await Navigation.PushAsync(new LoginPage(_apiService, _validator));
    }
}