using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Herhalingsoefeningen
{
    //Klasse voor het ophalen van items uit database "purchases" en voor het toevoegen van items aan deze database
    internal class ItemRepository
    {
        private readonly SqlConnection _connection;

        //vanaf een instantie van de repository wordt aangemaakt, wordt er een connectie gemaakt met de database
        public ItemRepository()
        {
            _connection = new SqlConnection("Data Source=(local);Initial Catalog=Purchases;Integrated Security=True");

            _connection.Open();
        }


        public List<Item> GetAllItems()
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT [productname], [quantity] FROM [dbo.Items]";

            var items = new List<Item>();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var item = MapItemFromReader(reader);
                items.Add(item);
            }

            return items;
        }

        //Item-instantie wordt gemapt door uit elke kolom van de tabel de juiste variable te halen om de item-instantie aan te maken
        private Item MapItemFromReader(SqlDataReader reader)
        {
            var productname = reader.GetString(0);
            var quantity = reader.GetInt32(1);

            Item item = new Item();
            item.ProductName = productname;
            item.Quantity = quantity;

            return item;
        }

        //Functie om een lijst van items toe te voegen aan de database
        public void AddItemsToDatabase(List<Item> items)
        {
            foreach(Item item in items)
            {
                using var command = _connection.CreateCommand();
                command.CommandText = "INSERT INTO [dbo.Items] ([productname], [quantity]) VALUES (@productname, @quantity)";
                command.Parameters.AddWithValue("@productname", item.ProductName);
                command.Parameters.AddWithValue("@quantity", item.Quantity);
                command.ExecuteNonQuery();
            }
            

        }





    }
}
