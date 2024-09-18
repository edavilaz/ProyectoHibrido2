using ProyectoHibrido2.Models;
using ProyectoHibrido2.Services;
using ProyectoHibrido2.Validations;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace ProyectoHibrido2.Pages;

public partial class CarritoPage : ContentPage
{
	private readonly ApiService _apiService;
	private readonly IValidator _validator;
	private bool _loginPageDisplayed = false;
	private bool _isNavigationToEmptyCartPage = false;

	private ObservableCollection<CarritoCompraItem>
		ItemsCarritoCompra = new ObservableCollection<CarritoCompraItem>();
    public CarritoPage(ApiService apiService, IValidator validator)
    {
        InitializeComponent();
        _apiService = apiService;
        _validator = validator;
				 
    }
		
	protected override async void OnAppearing()
	{
		base.OnAppearing();

		if (IsNavigatingToEmptyCartPage()) return;
		bool hasItems = await GetItemsCarritoCompra();

		if (hasItems)
		{
			ExhibirDireccion();
		}
		else
		{
			await NavegarParaCarritoVacio();
		}
		

	}

	private bool IsNavigatingToEmptyCartPage()
	{
		if(_isNavigationToEmptyCartPage)
		{
			_isNavigationToEmptyCartPage= false;
			return true;
		}
		return false;
	}

	private void ExhibirDireccion()
	{
		bool direccionSalvada = Preferences.ContainsKey("direccion");

        if (direccionSalvada)
        {
            string nombre = Preferences.Get("nombre", string.Empty);
            string direccion = Preferences.Get("direccion", string.Empty);
            string telefono = Preferences.Get("telefono", string.Empty);

            LblDireccion.Text = $"{nombre}\n{direccion}\n{telefono}";

        }
        else
        {
            LblDireccion.Text = "Informe su dirección";
        }
    }

	private async Task NavegarParaCarritoVacio()
	{
		LblDireccion.Text = string.Empty;
		_isNavigationToEmptyCartPage = true;
		await Navigation.PushAsync(new CarritoVacioPage());

	}

    private async Task<bool> GetItemsCarritoCompra()
	{
		try
		{
			var usuarioId = Preferences.Get("usuarioid", 0);
			var (itemsCarritoCompra, errorMessage) = await _apiService.GetItemsCarritoCompra(usuarioId);

			if (errorMessage == "Unauthorized" && !_loginPageDisplayed)
			{
				await DisplayLoginPage();
				return false;	
			}
			if(itemsCarritoCompra == null)
			{
				await DisplayAlert("Error", errorMessage ?? "No fue posible obtener los items", "Ok");
				return false;
			}
			ItemsCarritoCompra.Clear();
			foreach (var item in itemsCarritoCompra)
			{
				ItemsCarritoCompra.Add(item);
			}

			CvCarrito.ItemsSource = ItemsCarritoCompra;
			ActualizaPrecioTotal();

			if(!ItemsCarritoCompra.Any())
			{
				return false;
			}
			return true;
			
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Ocurrió un error inesperado: {ex.Message}", "OK");
			return false;
		}
	}

	private void ActualizaPrecioTotal()
	{
		try
		{
			var precioTotal = ItemsCarritoCompra.Sum(item => item.Precio * item.Cantidad);
			LblPrecioTotal.Text = precioTotal.ToString();
		}
		catch (Exception ex)
		{
			DisplayAlert("Error", $"Ocurrió un error al actualizar el precio: {ex.Message} ", "OK");
		}
	}
	
    private async Task DisplayLoginPage()
    {
        _loginPageDisplayed = true;
        await Navigation.PushAsync(new LoginPage(_apiService, _validator));
    }

    private async void BtnIncrementar_Clicked(object sender, EventArgs e)
    {
		if (sender is ImageButton button && button.BindingContext is CarritoCompraItem itemCarrito)
		{
			itemCarrito.Cantidad++;
			ActualizaPrecioTotal();
			await _apiService.ActualizaCantidadItemCarrito(itemCarrito.ProductoId,"aumentar");
		}

    }

    private async void BtnDecrementar_Clicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.BindingContext is CarritoCompraItem itemCarrito)
        {
			if (itemCarrito.Cantidad == 1) return;
			else
			{   
				itemCarrito.Cantidad--;
				ActualizaPrecioTotal();
				await _apiService.ActualizaCantidadItemCarrito(itemCarrito.ProductoId, "disminuir");

			}
        }
    }

    private async void BtnDelete_Clicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.BindingContext is CarritoCompraItem itemCarrito)
		{
			bool respuesta = await DisplayAlert("Confirmación", "¿Está seguro de eliminar el item?", "Si", "No");
			if (respuesta)
			{
				ItemsCarritoCompra.Remove(itemCarrito);
				ActualizaPrecioTotal();
				await _apiService.ActualizaCantidadItemCarrito(itemCarrito.ProductoId, "borrar");
			}

		}
    }

    private void BtnEditarDireccion_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new DireccionPage());

    }

    private async void TapConfirmarPedido_Tapped(object sender, TappedEventArgs e)
    {
		if(ItemsCarritoCompra == null || !ItemsCarritoCompra.Any())
		{
			await DisplayAlert("Información", "Su carrito de compra está vacío o su pedido ya fue confirmado", "OK");
			return;
		}

		var pedido = new Pedido()
		{
			Direccion = LblDireccion.Text,
			UsuarioId = Preferences.Get("usuarioid", 0),
			ValorTotal = Convert.ToDecimal(LblPrecioTotal.Text)
		};

		var response = await _apiService.ConfirmarPedido(pedido);

		if(response.HasError)
		{
			if (response.ErrorMessage == "Unauthorized")
			{
				// redirecciono a página Login
				await DisplayLoginPage();
				return;

			}

			await DisplayAlert("Error", $"Algo salio mal: {response.ErrorMessage}", "Cancelar");
			return;
		}

		ItemsCarritoCompra.Clear();
		LblDireccion.Text = "Informe su dirección";
		LblPrecioTotal.Text = "0.00";

		await Navigation.PushAsync(new PedidoConfirmadoPage());

    }
}