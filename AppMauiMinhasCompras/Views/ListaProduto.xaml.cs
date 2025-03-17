using AppMauiMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace AppMauiMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    //Searchbar Atualizada
    private ObservableCollection<Produto> lista = new();
    private List<Produto> todosOsProdutos = new();
    private CancellationTokenSource debounceCts;

    public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            todosOsProdutos = await App.DB.GetAll();
            AtualizarLista(todosOsProdutos);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao carregar produtos: {ex.Message}", "OK");
        }
    }

    private void AtualizarLista(List<Produto> novaLista)
    {
        lista.Clear();
        foreach (var produto in novaLista)
            lista.Add(produto);
    }


    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());

		}catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "Ok");
		}

    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);
        string msg = $"O total é {soma:C}";
        DisplayAlert("Total dos Produtos", msg, "Ok");
    }

    // 🚀 Debounce + busca local
    private void Txt_Search_TextChanged(object sender, TextChangedEventArgs e)
    {
        debounceCts?.Cancel();
        debounceCts = new CancellationTokenSource();

        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(300, debounceCts.Token); // 300ms de debounce

                string query = e.NewTextValue?.ToLowerInvariant() ?? "";

                var filtrados = todosOsProdutos
                    .Where(p => p.Descricao.ToLowerInvariant().Contains(query))
                    .ToList();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    AtualizarLista(filtrados);
                });
            }
            catch (TaskCanceledException)
            {
                // ignorado: debounce cancelado
            }
        });
    }

private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}
