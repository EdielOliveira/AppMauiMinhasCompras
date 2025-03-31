using AppMauiMinhasCompras.Helpers;

namespace AppMauiMinhasCompras
{
    public partial class App : Application
    {

        static SQLiteDataBaseHelper _db;

        public static SQLiteDataBaseHelper DB
        { get 
            {
                {
                    if(_db == null)
                    {
                        string caminho = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "banco_sqlite_compras.db3");

                        _db = new SQLiteDataBaseHelper(caminho);
                    }
                }

                return _db; 
            } 
        }


        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
