using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbOrder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbOrder.Services
{
    public class OrderOperation
    {
        public void AddOrder(Order order)
        {
            var connection = new MongoDbConnection(); //Create a connection to the database
            var ordersCollection = connection.GetOrdersCollection();//Get the Orders collection
            var document = new BsonDocument
            {
                {"ClientName",order.ClientName },
                {"TownName",order.TownName },
                {"CityName",order.CityName },
                {"TotalPrice",order.TotalPrice }
            };

            ordersCollection.InsertOne(document);//Insert the document into the collection
        }

        public List<Order> GetAllOrders()
        {
            var connection = new MongoDbConnection();
            var ordersCollection = connection.GetOrdersCollection();
            var orders = ordersCollection.Find(new BsonDocument()).ToList();
            List<Order> ordersList = new List<Order>();
            foreach (var order in orders)
            {
                ordersList.Add(new Order
                {
                    ClientName = order["ClientName"].ToString(),
                    TownName = order["TownName"].ToString(),
                    CityName = order["CityName"].ToString(),
                    OrderId = order["_id"].ToString(),
                    TotalPrice = order["TotalPrice"].AsDecimal
                });

            }
            return ordersList;

        }
    }
}
