using AppMauiMinhasCompras.Models;
using SQLite;

namespace AppMauiMinhasCompras.Helpers
{
    public class SQLiteDataBaseHelper
    {
    
        readonly SQLiteAsyncConnection _conn;
        public SQLiteDataBaseHelper(String path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();

            try
            {
                _conn.ExecuteAsync("ALTER TABLE Produto ADD COLUMN Categoria TEXT;");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar a coluna Categoria: " + ex.Message);
            }
        }
        public Task<int> Insert(Produto p)
        { 
            return _conn.InsertAsync(p);
        }
        public Task<List<Produto>> Update(Produto p) 
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=?, Categoria=? WHERE Id=?";
            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Categoria, p.Id);
        }
        public Task<int> Delete(int id) 
        { 
           return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> GetAll() 
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string query) 
        {
            // Adicionando a coluna Categoria à tabela Produto
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + query + "%'";
            return _conn.QueryAsync<Produto>(sql);
        }

        // Add Categoria no searchBar
        public Task<List<Produto>> SearchByCategory(string categoria)
        {
            string sql = "SELECT * FROM Produto WHERE Categoria = ?";
            return _conn.QueryAsync<Produto>(sql, categoria);
        }
    }
}
