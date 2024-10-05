using MongoDB.Bson;
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

    }
}
