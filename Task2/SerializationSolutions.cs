using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

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
            dbContext.Configuration.ProxyCreationEnabled = true;
            dbContext.Configuration.LazyLoadingEnabled = true;

            var serializer = new NetDataContractSerializer();

            var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(serializer, true);
            var categories = dbContext.Categories.ToList();

            var c = categories.First();

            tester.SerializeAndDeserialize(categories);
        }

        [TestMethod]
        public void ISerializable()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var serializer = new NetDataContractSerializer();
            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(serializer, true);
            var products = dbContext.Products.Include(p => p.Category).Include(p => p.Supplier).ToList();

            tester.SerializeAndDeserialize(products);
        }

        [TestMethod]
        public void ISerializationSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = true;
            dbContext.Configuration.LazyLoadingEnabled = true;

            var serializer = new NetDataContractSerializer()
            {
                
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

    public class OrderDetailSurrogateSelector : ISurrogateSelector
    {
        private ISurrogateSelector _nextSelector;

        public void ChainSelector(ISurrogateSelector selector)
        {
            _nextSelector = selector;
        }

        public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
        {
            
            throw new NotImplementedException();
        }

        public ISurrogateSelector GetNextSelector()
        {
            return _nextSelector;
        }
    }

    public class OrderDetailSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var orderDetail = (Order_Detail) obj;
            //info.AddValue("", orderDetail.);
            //info.AddValue();
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            throw new NotImplementedException();
        }
    }
}
