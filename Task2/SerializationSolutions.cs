using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

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

    public class OrderDetailSurrogateSelector : ISurrogateSelector
    {
        private ISurrogateSelector _nextSelector;

        public void ChainSelector(ISurrogateSelector selector)
        {
            _nextSelector = selector;
        }

        public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
        {
            selector = this;
            if (type == typeof(Order_Detail))
            {
                return new OrderDetailSerializationSurrogate();
            }
            return null;
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
            if (context.Context is SerializationContext serializationContext && serializationContext.TypeToSerialize == typeof(Order_Detail))
            {
                serializationContext.ObjectContext.LoadProperty(orderDetail, o => o.Order);
                serializationContext.ObjectContext.LoadProperty(orderDetail, o => o.Product);
            }
            info.AddValue(nameof(orderDetail.Discount), orderDetail.Discount);
            info.AddValue(nameof(orderDetail.OrderID), orderDetail.OrderID);
            info.AddValue(nameof(orderDetail.ProductID), orderDetail.ProductID);
            info.AddValue(nameof(orderDetail.Quantity), orderDetail.Quantity);
            info.AddValue(nameof(orderDetail.UnitPrice), orderDetail.UnitPrice);
            info.AddValue(nameof(orderDetail.Order), orderDetail.Order);
            info.AddValue(nameof(orderDetail.Product), orderDetail.Product);

        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var orderDetail = (Order_Detail)obj;
            orderDetail.Discount = info.GetSingle(nameof(orderDetail.Discount));
            orderDetail.OrderID = info.GetInt32(nameof(orderDetail.OrderID));
            orderDetail.ProductID = info.GetInt32(nameof(orderDetail.ProductID));
            orderDetail.Quantity = info.GetInt16(nameof(orderDetail.Quantity));
            orderDetail.UnitPrice = info.GetDecimal(nameof(orderDetail.UnitPrice));
            orderDetail.Order = (Order)info.GetValue(nameof(orderDetail.Order), typeof(Order));
            orderDetail.Product = (Product) info.GetValue(nameof(orderDetail.Product), typeof(Product));
            return orderDetail;
        }
    }
}
