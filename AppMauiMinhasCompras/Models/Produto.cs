using SQLite;

namespace AppMauiMinhasCompras.Models
{
    public class Produto
    {
        string _descricao;
        double _preco;
        double _quantidade;
        string _categoria;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } 
        public string Descricao {
            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Profavor, Preencha a descrição");
                }

                _descricao = value;
            }
        }
        public double Quantidade
        {
            get => _quantidade;
            set
            {
                if (value <= 0)
                    throw new Exception("Por Favor, coloque a quantidade de Produtos");

                _quantidade = value;
            }
        }
        public double Preco
        {
            get => _preco;
            set
            {
                if (value <= 0)
                    throw new Exception("Por Favor, coloque o preço Unitatario do Produto");

                _preco = value;
            }
        }

        public string Categoria
        {
            get => _categoria;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Por favor, selecione uma categoria.");
                }
                _categoria = value;
            }
        }
        public double Total { get => Quantidade * Preco; }
    }
}
