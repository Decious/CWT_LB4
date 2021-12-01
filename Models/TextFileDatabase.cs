using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kaberdin_LB4.Models
{
    public class TextFileDatabase
    {
        private string databasePath = "";
        public TextFileDatabase()
        {
            databasePath = Path.Combine(Directory.GetCurrentDirectory(), "ProductDatabase.txt");
        }

        public void AddProduct(ProductModel item)
        {
            ExecuteQuery(x => x.Add(item), "Add product");
        }

        public ProductModel GetProductById(int id)
        {
            ProductModel product = null;
            ExecuteQuery(x => product = x.FirstOrDefault(x => x.Id == id),"Get product by Id");
            return product;
        }

        public void EditProduct(int id, ProductModel product)
        {
            ExecuteQuery(products =>
            {
                if (products.Any(x => x.Id == id))
                {
                    products.Remove(products.First(x => x.Id == id));
                    products.Add(product);
                }
            }, "Edit product");
        }

        public void RemoveProduct(int id)
        {
            ExecuteQuery(products =>
            {
                if (products.Any(x => x.Id == id))
                {
                    products.Remove(products.First(x => x.Id == id));
                }
            }, "Remove product");
        }

        private void ExecuteQuery(Action<List<ProductModel>> query, string actionName)
        {
            Console.WriteLine($"Executing operation with database. {actionName}");
            var products = GetProducts();
            query(products);
            SaveProducts(products);
        }

        private void SaveProducts(List<ProductModel> products)
        {
            using (var fs = new FileStream(databasePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var strReader = new StreamWriter(fs))
            {
                var jsonWriter = new JsonTextWriter(strReader);

                JsonSerializer.CreateDefault().Serialize(jsonWriter, products);
            }
        }

        private List<ProductModel> GetProducts()
        {
            var existing = new FileInfo(databasePath).Exists;
            using (var fs = new FileStream(Path.Combine(databasePath), FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var strReader = new StreamReader(fs))
            {
                var jsonReader = new JsonTextReader(strReader);

                return existing
                    ? JsonSerializer.CreateDefault().Deserialize<List<ProductModel>>(jsonReader)
                    : new List<ProductModel>();
            }
        }
    }
}
