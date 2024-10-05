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
                    TotalPrice = decimal.Parse(order["TotalPrice"].ToString())
                });

            }
            return ordersList;

        }

        public void DeleteOrder(string orderId)
        {
            var connection = new MongoDbConnection();
            var ordersCollection = connection.GetOrdersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(orderId));
            ordersCollection.DeleteOne(filter);
        }

        public void updateOrder(Order order)
        {
            var connection = new MongoDbConnection();
            var ordersCollection = connection.GetOrdersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(order.OrderId));
            var update = Builders<BsonDocument>.Update
                .Set("ClientName", order.ClientName)
                .Set("TownName", order.TownName)
                .Set("CityName", order.CityName)
                .Set("TotalPrice", order.TotalPrice);
            ordersCollection.UpdateOne(filter, update);
        }

        public Order GetOrderById(string orderId)
        {
            var connection = new MongoDbConnection();
            var ordersCollection = connection.GetOrdersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(orderId));
            var order = ordersCollection.Find(filter).FirstOrDefault();

            if (order != null)
            {
                return new Order
                {
                    ClientName = order["ClientName"].ToString(),
                    TownName = order["TownName"].ToString(),
                    CityName = order["CityName"].ToString(),
                    OrderId = order["_id"].ToString(),
                    TotalPrice = decimal.Parse(order["TotalPrice"].ToString())
                };
            }
            else
            {
                return null;
            }

        }
    }
}
