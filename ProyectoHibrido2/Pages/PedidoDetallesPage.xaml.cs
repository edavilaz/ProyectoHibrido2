using ProyectoHibrido2.Services;
using ProyectoHibrido2.Validations;

namespace ProyectoHibrido2.Pages;

public partial class PedidoDetallesPage : ContentPage
{
	private readonly ApiService _apiService;
	private readonly IValidator _validator;

	private bool _loginDisplayedPage = false;


    public PedidoDetallesPage(int pedidoId, decimal precioTotal,
                    ApiService apiService, IValidator validator)

    {
        InitializeComponent();
        _apiService = apiService;
        _validator = validator;
        LblPrecioTotal.Text = "$ " + precioTotal;
        
        GetPedidoDetalle(pedidoId);
    }

    private async void GetPedidoDetalle(int pedidoId)
    {
        try
        {
            var (pedidoDetalles, errorMessage) = await _apiService.GetPedidoDetalles(pedidoId);

            if (errorMessage == "Unauthorized" && !_loginDisplayedPage)
            {
                await DisplayLoginPage();
                return;
            }
            if (pedidoDetalles is null)
            {
                await DisplayAlert("Error", errorMessage ?? "No fue posiblñe obtner detalles del pedido.", "OK");
                return;
            }
            else
            {
                CvPedidoDetalles.ItemsSource = pedidoDetalles;
            }
        }
        catch (Exception)
        {

            await DisplayAlert("Error", "Ocurrió un error al obtener los detalles.", "OK");
        }
    }

    private async Task DisplayLoginPage()
    {
        _loginDisplayedPage = true;
        await Navigation.PushAsync(new LoginPage(_apiService, _validator));
    }
}