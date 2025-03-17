using AppMauiMinhasCompras.Models;

namespace AppMauiMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto p = new Produto()
			{
				Descricao = txt_desc.Text,
				Quantidade = Convert.ToDouble(txt_quant.Text),
				Preco = Convert.ToDouble(txt_preco.Text)
			};

			await App.DB.Insert(p);
			await DisplayAlert("Sucesso", "Registro Inserido", "0k");


		}catch (Exception ex)
		{
            await DisplayAlert("Ops", ex.Message, "Ok");
		}

    }
}