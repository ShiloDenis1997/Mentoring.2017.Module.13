using System.IO;
using System.Runtime.Serialization;

namespace Task.TestHelpers
{
    public class XmlDataContractSerializer<T> : ISerializer<T>
    {
        private readonly XmlObjectSerializer serializer;

        public XmlDataContractSerializer(XmlObjectSerializer serializer)
        {
            this.serializer = serializer;
        }

        public T Deserialize(MemoryStream stream)
        {
            return (T)serializer.ReadObject(stream);
        }

        public void Serialize(T data, MemoryStream stream)
        {
            serializer.WriteObject(stream, data);
        }
    }
}
