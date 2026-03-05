using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // MongoDB Atlas Connection String
        const string connectionUri = "mongodb+srv://speedken:Admin818!@cluster0.9e6tav7.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

        // Create a new client and connect to the server
        var client = new MongoClient(connectionUri);

        // Access the database
        var database = client.GetDatabase("testdb"); // You can change your database name here
        var collection = database.GetCollection<BsonDocument>("testcollection"); // You can change your collection name here

        try
        {
            // Insert a document
            var document = new BsonDocument
            {
                { "name", "Flow" },
                { "type", ".NET App" },
                { "date", DateTime.UtcNow }
            };
            await collection.InsertOneAsync(document);
            Console.WriteLine("Document inserted successfully!");

            // Find the inserted document
            var filter = Builders<BsonDocument>.Filter.Eq("name", "Flow");
            var retrievedDocument = await collection.Find(filter).FirstOrDefaultAsync();

            if (retrievedDocument != null)
            {
                Console.WriteLine("Retrieved Document:");
                Console.WriteLine(retrievedDocument.ToJson());
            }
            else
            {
                Console.WriteLine("Document not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}

