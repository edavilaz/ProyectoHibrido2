using __XamlGeneratedCode__;
using ProyectoHibrido2.Models;
using ProyectoHibrido2.Services;
using ProyectoHibrido2.Validations;

namespace ProyectoHibrido2.Pages;

public partial class ProductoDetallesPage : ContentPage
{
	private readonly ApiService _apiService;
	private readonly IValidator _validator;
	private int _productoId;
	private bool _loginPageDisplayed = false;
    private FavoritosService _favoritosService= new FavoritosService();
    private string? _imagenUrl;

	public ProductoDetallesPage(int productoId, string productoNombre, 
								ApiService apiService, IValidator validator)
	{
		InitializeComponent();
		_productoId = productoId;
		_apiService = apiService;
		_validator = validator;
        Title = productoNombre ?? "Detalle de Producto";
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetProductoDetalles(_productoId);
        ActualizaFavoritoButton();
    }

    private async Task<Producto?>GetProductoDetalles(int productoId)
    {
        var (productoDetalle, errorMessage) = await _apiService.GetProductoDetalle(productoId);

        if (errorMessage == "Unauthorized" && !_loginPageDisplayed)
        {
            await DisplayLoginPage();
            return null;
        }

        if (productoDetalle is null)
        {
            await DisplayAlert("Error", errorMessage ?? "No fue posible obtener el producto.", "OK");
            return null;
        }

        if(productoDetalle != null)
        {
            ImagenProducto.Source = productoDetalle.RutaImagen;
            LblProductoNombre.Text = productoDetalle.Nombre;
            LblProductoPrecio.Text = productoDetalle.Precio.ToString();
            LblProductoDescripcion.Text = productoDetalle.Detalle;
            LblPrecioTotal.Text = productoDetalle.Precio.ToString();
            _imagenUrl = productoDetalle.RutaImagen;
        }
        else
        {
            await DisplayAlert("Error", errorMessage ?? "No fue posible obtener los detalles del producto", "OK");
            return null;
        }
        return productoDetalle;

    }


    private async void ImagenBtnFavorito_Clicked(object sender, EventArgs e)
    {
        try
        {
            var existeFavorito = await _favoritosService.ReadAsync(_productoId);
            if (existeFavorito is not null)
            {
                await _favoritosService.DeleteAsync(existeFavorito);
            }
            else
            {
                var productoFavorito = new ProductoFavorito()
                {
                    ProductoId = _productoId,
                    IsFavorito = true,
                    Detalle = LblProductoDescripcion.Text,
                    Nombre = LblProductoNombre.Text,
                    Precio = Convert.ToDecimal(LblProductoPrecio.Text),
                    ImagenUrl = _imagenUrl
                };

                await _favoritosService.CreateAsync(productoFavorito);
            }
            ActualizaFavoritoButton();

        }
        catch (Exception ex)
        {

            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }

    private async void ActualizaFavoritoButton()
    {
        var existeFavorito = await
            _favoritosService.ReadAsync(_productoId);

        if (existeFavorito is not null)
            ImagenBtnFavorito.Source = "heartfill";
        else
            ImagenBtnFavorito.Source = "heart";


    }

    private void BtnRemove_Clicked(object sender, EventArgs e)
    {
        if (int.TryParse(LblCantidad.Text, out int cantidad) &&
            decimal.TryParse(LblProductoPrecio.Text, out decimal precioUnitario))
        {
            // Decrementar Cantidad y no permitir que sea menor a 1
            cantidad = Math.Max(1,cantidad -1);
            LblCantidad.Text = cantidad.ToString();

            //Calcula Precio total
            var precioTotal = cantidad * precioUnitario;
            LblPrecioTotal.Text = precioTotal.ToString();
        }
        else
        {
            // en el caso de que la conversión falle

            DisplayAlert("Error", "Valores inválidos", "OK");
        }


    }

    private void BtnAdd_Clicked(object sender, EventArgs e)
    {
        if (int.TryParse(LblCantidad.Text, out int cantidad) &&
            decimal.TryParse(LblProductoPrecio.Text, out decimal precioUnitario))
        {
            // Incrementar Cantidad
            cantidad++;
            LblCantidad.Text = cantidad.ToString();

            //Calcula Precio total
            var precioTotal = cantidad * precioUnitario;
            LblPrecioTotal.Text = precioTotal.ToString();
        }
        else
        {
            // en el caso de que la conversión falle

            DisplayAlert("Error", "Valores inválidos", "OK");
        }

    }

    private async void BtnIncluirCarrito_Clicked(object sender, EventArgs e)
    {
        try
        {
            var carritoCompra = new CarritoCompra()
            {
                Cantidad = Convert.ToInt32((string)LblCantidad.Text),
                PrecioUnitario = Convert.ToDecimal(LblProductoPrecio.Text),
                ValorTotal = Convert.ToDecimal(LblPrecioTotal.Text),
                ProductoId = _productoId,
                ClienteId = Preferences.Get("usuarioid", 0)
            };

            var response = await _apiService.AdicionarItemAlCarrito(carritoCompra);
            if (response.Data)
            {
                await DisplayAlert("Excelente!!!", "El item fue agregado al carrito !", "Ok");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", $"Error al adicionar el item al carrito: {response.ErrorMessage}", "OK");
                
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}","OK");
        }


    }

    private async Task DisplayLoginPage()
    {
        _loginPageDisplayed = true;
        await Navigation.PushAsync(new LoginPage(_apiService,_validator));
    }
}