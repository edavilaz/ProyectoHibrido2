namespace ProyectoHibrido2.Pages;

public partial class CarritoVacioPage : ContentPage
{
	public CarritoVacioPage()
	{
		InitializeComponent();
	}

    private async void BtnRetornar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}