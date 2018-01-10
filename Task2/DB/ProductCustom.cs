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
            GetCategoryData(info);
            GetSupplierData(info);
        }

        private void GetSupplierData(SerializationInfo info)
        {
            if (Supplier == null)
            {
                return;
            }

            info.AddValue(nameof(Supplier) + nameof(Supplier.Address), Supplier.Address);
            info.AddValue(nameof(Supplier) + nameof(Supplier.City), Supplier.City);
            info.AddValue(nameof(Supplier) + nameof(Supplier.CompanyName), Supplier.CompanyName);
            info.AddValue(nameof(Supplier) + nameof(Supplier.ContactName), Supplier.ContactName);
            info.AddValue(nameof(Supplier) + nameof(Supplier.ContactTitle), Supplier.ContactTitle);
            info.AddValue(nameof(Supplier) + nameof(Supplier.Country), Supplier.Country);
            info.AddValue(nameof(Supplier) + nameof(Supplier.Fax), Supplier.Fax);
            info.AddValue(nameof(Supplier) + nameof(Supplier.HomePage), Supplier.HomePage);
            info.AddValue(nameof(Supplier) + nameof(Supplier.Phone), Supplier.Phone);
            info.AddValue(nameof(Supplier) + nameof(Supplier.PostalCode), Supplier.PostalCode);
            info.AddValue(nameof(Supplier) + nameof(Supplier.Region), Supplier.Region);
            info.AddValue(nameof(Supplier) + nameof(Supplier.SupplierID), Supplier.SupplierID);
        }

        private void SetSupplierData(SerializationInfo info)
        {
            bool isHasSupplier = false;
            foreach (SerializationEntry entry in info)
            {
                if (entry.Name == nameof(Supplier) + nameof(Supplier.SupplierID))
                {
                    isHasSupplier = true;
                    break;
                }
            }
            if (!isHasSupplier)
            {
                return;
            }

            Supplier = new Supplier
            {
                Address = info.GetString(nameof(Supplier) + nameof(Supplier.Address)),
                City = info.GetString(nameof(Supplier) + nameof(Supplier.City)),
                CompanyName = info.GetString(nameof(Supplier) + nameof(Supplier.CompanyName)),
                ContactName = info.GetString(nameof(Supplier) + nameof(Supplier.ContactName)),
                ContactTitle = info.GetString(nameof(Supplier) + nameof(Supplier.ContactTitle)),
                Country = info.GetString(nameof(Supplier) + nameof(Supplier.Country)),
                Fax = info.GetString(nameof(Supplier) + nameof(Supplier.Fax)),
                HomePage = info.GetString(nameof(Supplier) + nameof(Supplier.HomePage)),
                Phone = info.GetString(nameof(Supplier) + nameof(Supplier.Phone)),
                PostalCode = info.GetString(nameof(Supplier) + nameof(Supplier.PostalCode)),
                Region = info.GetString(nameof(Supplier) + nameof(Supplier.Region)),
                SupplierID = info.GetInt32(nameof(Supplier) + nameof(Supplier.SupplierID)),
        };
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
            bool isHasCategory = false;
            foreach (SerializationEntry entry in info)
            {
                if (entry.Name == nameof(Category) + nameof(Category.CategoryID))
                {
                    isHasCategory = true;
                    break;
                }
            }
            if (!isHasCategory)
            {
                return;
            }

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
