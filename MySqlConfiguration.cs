namespace CatalogoProductos
{
    public class MySqlConfiguration
    {

        public string connectionString { get; set; }

        public MySqlConfiguration(string connectionString)
        {
            this.connectionString = connectionString;
        }


    }
}
