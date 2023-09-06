using Microsoft.Azure.Cosmos;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Xml.Linq;
using Container = Microsoft.Azure.Cosmos.Container;

namespace CosmosDBApp
{
    public class Program
    {
        static string cosmosDBEndpointUri = "https://dileepcosmosdb.documents.azure.com:443/";

        static string cosmosDBKey =
            "I7G7RksostOiPClHQzjKwV4jxBQeV5PajWD1eM5P4AmrbOe3uoTVPBeCpHsPgl6htm3vADConddsACDb5Z8iYw==";

        static string databaseName = "appdb";
        static string containerName = "Orders";

        static void Main(string[] args)
        {
            // CreateDatabase(databaseName);
            // CreateContainer(databaseName, containerName, "/category");
            //AddItem("O1", "Laptop", 100);
            //AddItem("O2", "Mobiles", 200);
            //AddItem("O3", "Desktop", 75);
            //AddItem("O4", "Laptop", 25);

            CallStoredProcedure();
            Console.ReadLine();
        }

        static void CreateDatabase(string databaseName)
        {
            CosmosClient cosmosClient;
            cosmosClient = new CosmosClient(cosmosDBEndpointUri, cosmosDBKey);

            cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName).GetAwaiter().GetResult();
            Console.WriteLine("Database created");
        }

        static void CreateContainer(string databaseName, string containerName, string partitionKey)
        {
            CosmosClient cosmosClient;
            cosmosClient = new CosmosClient(cosmosDBEndpointUri, cosmosDBKey);

            Database database = cosmosClient.GetDatabase(databaseName);

            database.CreateContainerIfNotExistsAsync(containerName, partitionKey).GetAwaiter().GetResult();

            Console.WriteLine("Container created");
        }

        static void AddItem(string orderId, string category, int quantity)
        {
            CosmosClient cosmosClient;
            cosmosClient = new CosmosClient(cosmosDBEndpointUri, cosmosDBKey);

            Database database = cosmosClient.GetDatabase(databaseName);
            Container container = database.GetContainer(containerName);

            Order order = new Order()
            {
                id = Guid.NewGuid().ToString(),
                orderId = orderId,
                category = category,
                quantity = quantity
            };

            container.CreateItemAsync<Order>(order, new PartitionKey(order.category)).GetAwaiter().GetResult();

            Console.WriteLine("Added item with Order Id {0}", orderId);
            //Console.WriteLine("Request Units consumed {0}", response.RequestCharge);

        }

        static void CallStoredProcedure()
        {
            CosmosClient cosmosClient;
            cosmosClient = new CosmosClient(cosmosDBEndpointUri, cosmosDBKey);
            Database database = cosmosClient.GetDatabase(databaseName);
            Container container = database.GetContainer(containerName);

            dynamic[] orderItems = new dynamic[]
            {
                new
                {
                    id = Guid.NewGuid().ToString(),
                    orderId = "01",
                    category = "Laptop",
                    quantity = 100
                },
                new
                {
                    id = Guid.NewGuid().ToString(),
                    orderId = "02",
                    category = "Laptop",
                    quantity = 200
                },
                new
                {
                    id = Guid.NewGuid().ToString(),
                    orderId = "03",
                    category = "Laptop",
                    quantity = 75
                },
            };
            var result = container.Scripts.ExecuteStoredProcedureAsync<string>("createItems",
                new PartitionKey("Laptop"), new[] { orderItems }).GetAwaiter().GetResult();
            Console.WriteLine(result);


        }
    }
}