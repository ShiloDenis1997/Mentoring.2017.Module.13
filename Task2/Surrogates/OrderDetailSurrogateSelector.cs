using System;
using System.Runtime.Serialization;
using Task.DB;

namespace Task.Surrogates
{
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
}