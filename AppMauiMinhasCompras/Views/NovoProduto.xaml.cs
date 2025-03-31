using AppMauiMinhasCompras.Models;

namespace AppMauiMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();

        picker_categoria.ItemsSource = new List<string> { "Alimentos", "Higiene", "Limpeza" , "Eletrônicos" , "Roupas" , "Beleza" };
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto p = new Produto()
			{
				Descricao = txt_desc.Text,
				Quantidade = Convert.ToDouble(txt_quant.Text),
				Preco = Convert.ToDouble(txt_preco.Text),
				//add categoria
				Categoria = picker_categoria.SelectedItem?.ToString() ?? "Outros"

            };

			await App.DB.Insert(p);
			await DisplayAlert("Sucesso", "Registro Inserido", "0k");

        }
        catch (Exception ex)
		{
            await DisplayAlert("Ops", ex.Message, "Ok");
		}

    }
}