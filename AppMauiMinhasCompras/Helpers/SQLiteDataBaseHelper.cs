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
        }
        public Task<int> Insert(Produto p)
        { 
            return _conn.InsertAsync(p);
        }
        public Task<List<Produto>> Update(Produto p) 
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=?, WHERE Id=?";
            return _conn.QueryAsync<Produto>(
                sql, p.Descricao , p.Quantidade, p.Preco, p.Id
                
                );
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
            string sql = "SELECT * Produto WHERE Descricao LIKE '%" + query + "%'";
            return _conn.QueryAsync<Produto>(sql);
        }
    }
}
