using AppMauiMinhasCompras.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace AppMauiMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    //Searchbar Atualizada
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    private List<Produto> todosOsProdutos = new();
    private CancellationTokenSource debounceCts;

    public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
	}

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.DB.GetAll();

            tmp.ForEach(i => lista.Add(i));

        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao carregar produtos: {ex.Message}", "OK");
        }
    }

    private async void Txt_Search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try { 
        string q = e.NewTextValue;

        lst_produtos.IsRefreshing = true;

        lista.Clear();

        List<Produto> tmp = await App.DB.Search(q);

        tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
           await DisplayAlert("Ops", ex.Message, "Ok");
        }

    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        {
            if (lista.Count == 0)
            {
                DisplayAlert("Aviso", "Não há produtos cadastrados.", "Ok");
                return;
            }

            double totalGeral = lista.Sum(p => p.Total);

            var totaisPorCategoria = lista
                .GroupBy(p => p.Categoria)
                .Select(g => new { Categoria = g.Key, Total = g.Sum(p => p.Total) })
                .OrderByDescending(g => g.Total)
                .ToList();

            string mensagem = $"Total Geral: {totalGeral:C}\n\n";

            foreach (var categoria in totaisPorCategoria)
            {
                mensagem += $"{categoria.Categoria}: {categoria.Total:C}\n";
            }

            DisplayAlert("Resumo de Compras", mensagem, "Ok");
        }
    }


    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = sender as MenuItem;

            Produto p = selecionado.BindingContext as Produto;
            
            bool confirm = await DisplayAlert("Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                await App.DB.Delete(p.Id);
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
           await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;

            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex) 
        {
             DisplayAlert("Ops", ex.Message, "OK");
        }

    }

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.DB.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }
}
