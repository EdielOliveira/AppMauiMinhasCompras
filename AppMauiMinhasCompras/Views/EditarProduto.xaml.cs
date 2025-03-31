using AppMauiMinhasCompras.Models;

namespace AppMauiMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto produto_anexado = BindingContext as Produto;

            Produto p = new Produto()
            {
                Id = produto_anexado.Id,
                Descricao = txt_desc.Text,
                Quantidade = Convert.ToDouble(txt_quant.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
                // Atualizando categoria
                Categoria = picker_categoria.SelectedItem.ToString()
            };

            await App.DB.Update(p);
            await DisplayAlert("Sucesso", "Registro Atualizado", "0k");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");
        }

    }
}