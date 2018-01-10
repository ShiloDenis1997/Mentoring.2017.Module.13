using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Task;
using Task.DB;
using Task.TestHelpers;
using System.Data.Entity;

namespace Task2.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new Northwind();
            //dbContext.Configuration.ProxyCreationEnabled = true;
            //dbContext.Configuration.LazyLoadingEnabled = true;

            //var serializer = new DataContractSerializer(typeof(IEnumerable<Order>),
            //    new DataContractSerializerSettings
            //    {
            //        DataContractSurrogate = new OrderDataContractSurrogate()
            //    });
            //var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(serializer, true);
            //var orders = dbContext.Orders.ToList();

            //tester.SerializeAndDeserialize(orders);

            dbContext.Configuration.ProxyCreationEnabled = false;

            var serializer = new NetDataContractSerializer();
            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(serializer, true);
            var products = dbContext.Products.Include(p => p.Category).Include(p => p.Supplier).ToList();

            tester.SerializeAndDeserialize(products);
        }
    }
}
