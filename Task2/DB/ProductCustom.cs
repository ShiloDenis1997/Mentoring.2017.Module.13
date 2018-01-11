using System;
using System.Runtime.Serialization;

namespace Task.DB
{
    [Serializable]
    public partial class Product : ISerializable
    {
        public Product(SerializationInfo info, StreamingContext context)
        {
            CategoryID = info.GetInt32(nameof(CategoryID));
            Discontinued = info.GetBoolean(nameof(Discontinued));
            ProductID = info.GetInt32(nameof(ProductID));
            ProductName = info.GetString(nameof(ProductName));
            QuantityPerUnit = info.GetString(nameof(QuantityPerUnit));
            ReorderLevel = info.GetInt16(nameof(ReorderLevel));
            SupplierID = info.GetInt32(nameof(SupplierID));
            UnitPrice = info.GetDecimal(nameof(UnitPrice));
            UnitsInStock = info.GetInt16(nameof(UnitsInStock));
            UnitsOnOrder = info.GetInt16(nameof(UnitsOnOrder));
            SetCategoryData(info);
            SetSupplierData(info);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(CategoryID), CategoryID);
            info.AddValue(nameof(Discontinued), Discontinued);
            info.AddValue(nameof(ProductID), ProductID);
            info.AddValue(nameof(ProductName), ProductName);
            info.AddValue(nameof(QuantityPerUnit), QuantityPerUnit);
            info.AddValue(nameof(ReorderLevel), ReorderLevel);
            info.AddValue(nameof(SupplierID), SupplierID);
            info.AddValue(nameof(UnitPrice), UnitPrice);
            info.AddValue(nameof(UnitsInStock), UnitsInStock);
            info.AddValue(nameof(UnitsOnOrder), UnitsOnOrder);

            if (context.Context is SerializationContext serializationContext && serializationContext.TypeToSerialize == typeof(Product))
            {
                //serializationContext.ObjectContext.LoadProperty(this, p => p.Supplier);
                serializationContext.ObjectContext.LoadProperty(this, p => p.Category);
            }
            GetCategoryData(info);
            GetSupplierData(info);
        }

        private void GetSupplierData(SerializationInfo info)
        {
            //if (Supplier == null)
            //{
            //    return;
            //}

            info.AddValue(nameof(Supplier), Supplier, typeof(Supplier));
        }

        private void SetSupplierData(SerializationInfo info)
        {
            //bool isHasSupplier = false;
            //foreach (SerializationEntry entry in info)
            //{
            //    if (entry.Name == nameof(Supplier))
            //    {
            //        isHasSupplier = true;
            //        break;
            //    }
            //}
            //if (!isHasSupplier)
            //{
            //    return;
            //}

            Supplier = (Supplier) info.GetValue(nameof(Supplier), typeof(Supplier));
        }

        private void GetCategoryData(SerializationInfo info)
        {
            if (Category == null)
            {
                return;
            }

            info.AddValue(nameof(Category), Category, typeof(Category));
        }

        private void SetCategoryData(SerializationInfo info)
        {
            bool isHasCategory = false;
            foreach (SerializationEntry entry in info)
            {
                if (entry.Name == nameof(Category))
                {
                    isHasCategory = true;
                    break;
                }
            }
            if (!isHasCategory)
            {
                return;
            }

            Category = (Category) info.GetValue(nameof(Category), typeof(Category));
        }
    }
}
