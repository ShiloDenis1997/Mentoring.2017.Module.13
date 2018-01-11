using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class SerializationContext
    {
        public ObjectContext ObjectContext { get; set; }
        public Type TypeToSerialize { get; set; }
    }
}
