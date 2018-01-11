using System;
using System.Runtime.Serialization;

namespace Task.DB
{
    [Serializable]
    public partial class Category
    {
        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            if (context.Context is SerializationContext serializationContext && serializationContext.TypeToSerialize == typeof(Category))
            {
                serializationContext.ObjectContext.LoadProperty(this, c => c.Products);
            }
        }
    }
}
