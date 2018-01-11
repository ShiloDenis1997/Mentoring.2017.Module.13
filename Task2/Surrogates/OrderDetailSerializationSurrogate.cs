using System.Runtime.Serialization;
using Task.DB;

namespace Task.Surrogates
{
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