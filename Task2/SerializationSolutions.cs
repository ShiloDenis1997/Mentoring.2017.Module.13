using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using Task.Surrogates;

namespace Task
{
    [TestClass]
    public class SerializationSolutions
    {
        Northwind dbContext;

        [TestInitialize]
        public void Initialize()
        {
            dbContext = new Northwind();
        }

        [TestMethod]
        public void SerializationCallbacks()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var serializationContext = new SerializationContext
            {
                ObjectContext = (dbContext as IObjectContextAdapter).ObjectContext,
                TypeToSerialize = typeof(Category)
            };
            var serializer = new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, serializationContext));

            var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(serializer, true);
            var categories = dbContext.Categories.ToList();

            tester.SerializeAndDeserialize(categories);
        }

        [TestMethod]
        public void ISerializable()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var serializationContext = new SerializationContext
            {
                ObjectContext = (dbContext as IObjectContextAdapter).ObjectContext,
                TypeToSerialize = typeof(Product)
            };
            var serializer = new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, serializationContext));
            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(serializer, true);
            var products = dbContext.Products.ToList();

            tester.SerializeAndDeserialize(products);
        }

        [TestMethod]
        public void ISerializationSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var serializationContext = new SerializationContext
            {
                ObjectContext = (dbContext as IObjectContextAdapter).ObjectContext,
                TypeToSerialize = typeof(Order_Detail)
            };

            var serializer = new NetDataContractSerializer()
            {
                SurrogateSelector = new OrderDetailSurrogateSelector(),
                Context = new StreamingContext(StreamingContextStates.All, serializationContext)
            };
            var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(serializer, true);
            var orderDetails = dbContext.Order_Details.ToList();

            tester.SerializeAndDeserialize(orderDetails);
        }

        [TestMethod]
        public void IDataContractSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = true;
            dbContext.Configuration.LazyLoadingEnabled = true;

            var serializer = new DataContractSerializer(typeof(IEnumerable<Order>), 
                new DataContractSerializerSettings
                {
                    DataContractSurrogate = new OrderDataContractSurrogate()
                });
            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(serializer, true);
            var orders = dbContext.Orders.ToList();

            tester.SerializeAndDeserialize(orders);
        }
    }
}
