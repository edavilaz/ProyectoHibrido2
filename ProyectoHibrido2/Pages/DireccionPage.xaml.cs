namespace ProyectoHibrido2.Pages;

public partial class DireccionPage : ContentPage
{
	public DireccionPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarDatosSalvados();
    }

    private void CargarDatosSalvados()
    {
        if (Preferences.ContainsKey("nombre"))
            EntNombre.Text = Preferences.Get("nombre", string.Empty);
        
        if (Preferences.ContainsKey("direccion"))
            EntDireccion.Text = Preferences.Get("direccion", string.Empty);
        
        if (Preferences.ContainsKey("telefono"))
            EntTelefono.Text = Preferences.Get("telephone", string.Empty);

    }

    private void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        Preferences.Set("nombre",EntNombre.Text);
        Preferences.Set("direccion",EntDireccion.Text);
        Preferences.Set("telefono",EntTelefono.Text);
        Navigation.PopAsync();

    }
}