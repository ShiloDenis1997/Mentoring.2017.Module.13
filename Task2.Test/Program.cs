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
using System.Data.Entity.Infrastructure;

namespace Task2.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new Northwind();
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
    }
}
