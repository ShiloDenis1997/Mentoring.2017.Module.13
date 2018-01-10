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
            GetCategoryData(info);
        }

        private void GetCategoryData(SerializationInfo info)
        {
            if (Category == null)
            {
                return;
            }

            info.AddValue(nameof(Category) + nameof(Category.CategoryID), Category.CategoryID);
            info.AddValue(nameof(Category) + nameof(Category.CategoryName), Category.CategoryName);
            info.AddValue(nameof(Category) + nameof(Category.Description), Category.Description);
            info.AddValue(nameof(Category) + nameof(Category.Picture), Category.Picture);
        }

        private void SetCategoryData(SerializationInfo info)
        {
            //foreach (SerializationInfo entry in info)
            //{
            //    if 
            //}
            Category = new Category
            {
                CategoryID = info.GetInt32(nameof(Category) + nameof(Category.CategoryID)),
                CategoryName = info.GetString(nameof(Category) + nameof(Category.CategoryName)),
                Description = info.GetString(nameof(Category) + nameof(Category.Description)),
                Picture = (byte[])info.GetValue(nameof(Category) + nameof(Category.Picture), typeof(byte[]))
            };
        }
    }
}
