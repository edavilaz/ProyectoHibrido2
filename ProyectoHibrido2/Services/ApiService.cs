
using Microsoft.Extensions.Logging;
using ProyectoHibrido2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProyectoHibrido2.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        //private readonly string _baseUrl = "http://www.proyectomaui.somee.com/swagger/";
        //private readonly string _baseUrl = "https://localhost:7110/swagger/";
        
        
        private readonly ILogger<ApiService> _logger;

        JsonSerializerOptions _serializerOptions;

        public ApiService(HttpClient httpClient, ILogger<ApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ApiResponse<bool>> RegistrarUsuario(string nombre, string email, string password)
        {
            try
            {
                var register = new Register()
                {
                    Nombre = nombre,
                    Email = email,
                    Password = password
                };

                var json = JsonSerializer.Serialize(register, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await PostRequest("api/Usuarios/Register", content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error al enviar la información HTTP: {response.StatusCode}");
                    return new ApiResponse<bool>
                    {
                        ErrorMessage = $"Error al enviar la información HTTP: {response.StatusCode}"
                    };
                }
                return new ApiResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al registrar el usuario: {ex.Message}");
                return new ApiResponse<bool> { ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<bool>> Login(string email, string password)
        {
            try
            {
                var login = new Login()
                {
                    Email = email,
                    Password = password
                };

                var json = JsonSerializer.Serialize(login, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await PostRequest("api/Usuarios/Login", content);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error al enviar la información HTTP: {response.StatusCode}");
                    return new ApiResponse<bool>
                    {
                        ErrorMessage = $"Error al enviar la información HTTP: {response.StatusCode}"
                    };
                }

                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Token>(jsonResult, _serializerOptions);

                Preferences.Set("accesstoken", result!.AccessToken);
                Preferences.Set("usuarioid", (int)result.UsuarioId!);
                Preferences.Set("usuarionombre", result.UsuarioNombre);

                return new ApiResponse<bool> { Data = true };
            }

            catch (Exception ex)
            {
                _logger.LogError($"Error en el Login: {ex.Message}");
                return new ApiResponse<bool> { ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<bool>> AdicionarItemAlCarrito(CarritoCompra carritoCompra)
        {
            try
            {
                var json = JsonSerializer.Serialize(carritoCompra, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await PostRequest("api/ItemsCarritoCompra", content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error al enviar la solicitud HTTP: {response.StatusCode}");
                    return new ApiResponse<bool>
                    {
                        ErrorMessage = $"Error al enviar la solicitud HTTP: {response.StatusCode}"
                    };
                }

                return new ApiResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al adicionar el item al carrito: {ex.Message}");
                return new ApiResponse<bool> { ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<bool>> ConfirmarPedido(Pedido pedido)
        {
            try
            {
                var json = JsonSerializer.Serialize(pedido, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await PostRequest("api/Pedidos", content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = response.StatusCode == HttpStatusCode.Unauthorized
                        ? "Unauthorized"
                        : $"Error al enviar la solicitud HTTP: {response.StatusCode}";
                    _logger.LogError($"Error al enviar la solicitud HTTP: {response.StatusCode}");
                    return new ApiResponse<bool> { ErrorMessage = errorMessage };
                }

                return new ApiResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al confirmar el pedido: {ex.Message}");
                return new ApiResponse<bool> { ErrorMessage = ex.Message };
            }

        }

        public async Task<ApiResponse<bool>> UploadImagenUsuario(byte[] imageArray)
        {
            try
            {
                var content = new MultipartFormDataContent();
                content.Add(new ByteArrayContent(imageArray), "imagen", "image.jpg");
                var response = await PostRequest("api/usuarios/uploadfotousuario", content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = response.StatusCode == HttpStatusCode.Unauthorized
                        ? "Unauthorized"
                        : $"Error al enviar solicitud HTTP: {response.StatusCode}";

                    _logger.LogError($"Error al enviar solicitud HTTP: {response.StatusCode}");
                    return new ApiResponse<bool> { ErrorMessage = errorMessage };
                }
                return new ApiResponse<bool> { Data = true };

            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al cargar imagen del usuario: {ex.Message}");
                return new ApiResponse<bool> { ErrorMessage = ex.Message };
            }

        }
        private async Task<HttpResponseMessage> PostRequest(string uri, HttpContent content)
        {
            var dirUrl = AppConfig.BaseUrl + uri;

            try
            {
                var result = await _httpClient.PostAsync(dirUrl, content);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al enviar POST para {uri}:{ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public async Task<(bool Data, string? ErrorMessage)>ActualizaCantidadItemCarrito(int productoId, string accion)
        {
            try
            {
                var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                var response = await PutRequest($"api/ItemsCarritoCompra?productoId={productoId}&accion={accion}", content);
                if (response.IsSuccessStatusCode)
                {
                    return (true, null);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string errorMessage = "Unauthorized";
                        _logger.LogWarning(errorMessage);
                        return (false, errorMessage);
                    }

                    string generalErrorMessage = $"Error en la solicitud: {response.ReasonPhrase}";
                    _logger.LogError(generalErrorMessage);
                    return (false, generalErrorMessage);

                }
            }
            catch (HttpRequestException ex)
            {

                string errorMessage = $"Error de solicitud HTTP: { ex.Message}";
                _logger.LogError(errorMessage); 
                return (false, errorMessage);
            }      
            catch (Exception ex)
            {
                string errorMessage = $"Error Inesperado: {ex.Message}";
                _logger.LogError(errorMessage);
                return (false, errorMessage);
            }
        }

        private async Task<HttpResponseMessage> PutRequest(string uri, HttpContent content)
        {
            var dirUrl = AppConfig.BaseUrl + uri;
            try
            {
                AddAuthorizationHeader();
                var result = await _httpClient.PutAsync(dirUrl, content);
                return result;


            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al enviar solicitud PUT para {uri}: {ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public async Task<(List<Categoria>? Categorias, string? ErrorMessage)> GetCategorias()
        {
            return await GetAsync<List<Categoria>>("api/categorias");
        }

        public async Task<(List<Producto>? Productos, string? ErrorMessage)> GetProductos(string tipoProducto, string categoriaId)
        {
            string endpoint = $"api/Productos?tipoProducto={tipoProducto}&categoriaId={categoriaId}";
            return await GetAsync<List<Producto>>(endpoint);
        }

        public async Task<(Producto? ProductoDetalle, string? ErrorMessage)>GetProductoDetalle(int productoId)
        {
            string endpoint = $"api/productos/{productoId}";
            return await GetAsync<Producto>(endpoint);
        }

        public async Task<(List<CarritoCompraItem>? CarritoCompraItems, string? ErrorMessage)> GetItemsCarritoCompra(int usuarioId)
        {
            var endpoint = $"api/ItemsCarritoCompra/{usuarioId}";
            return  await GetAsync<List<CarritoCompraItem>>(endpoint);
        }

        public async Task<(ImagenPerfil? ImagenPerfil, string? ErrorMessage)>GetImagenPerfilUsuario()
        {
            string endpoint = "api/usuarios/ImagenPerfilUsuario";
            return await GetAsync<ImagenPerfil>(endpoint);
        }
        
        public async Task<(List<PedidoPorUsuario>?, string? ErrorMessage)>GetPedidosUsuario(int usuarioId)
        {
            string endpoint = $"api/pedidos/PedidosPorUsuario/{usuarioId}";
            return await GetAsync<List<PedidoPorUsuario>>(endpoint);
        }
       
        public async Task<(List<PedidoDetalle>?,string? ErrorMessage)>GetPedidoDetalles(int pedidoId)
        {
            string endpoint = $"api/pedidos/DetallesPedido/{pedidoId}";
            return await GetAsync<List<PedidoDetalle>>(endpoint);
        }
        private async Task<(T? Data, string? ErrorMessage)> GetAsync<T>(string endpoint)
        {
            try
            {
                AddAuthorizationHeader();

                var response = await _httpClient.GetAsync(AppConfig.BaseUrl + endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<T>(responseString, _serializerOptions);
                    return (data ?? Activator.CreateInstance<T>(), null);
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        string errorMessage = "Unauthorized";
                        _logger.LogWarning(errorMessage);
                        return (default, errorMessage);
                    }

                    string generalErrorMessage = $"Error en la petición: {response.ReasonPhrase}";
                    _logger.LogError(generalErrorMessage);
                    return (default, generalErrorMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                string errorMessage = $"Error en la petición HTTP: {ex.Message}";
                _logger.LogError(ex, errorMessage);
                return (default, errorMessage);
            }
            catch (JsonException ex)
            {
                string errorMessage = $"Error en la deserialización JSON: {ex.Message}";
                _logger.LogError(ex, errorMessage);
                return (default, errorMessage);
            }

            catch (Exception ex)
            {
                string errorMessage = $"Error inesperado: {ex.Message}";
                _logger.LogError(ex, errorMessage);
                return (default, errorMessage);
            }
        }

        private void AddAuthorizationHeader()
        {
            var token = Preferences.Get("accesstoken", string.Empty);
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

        }

                    
    }
}
