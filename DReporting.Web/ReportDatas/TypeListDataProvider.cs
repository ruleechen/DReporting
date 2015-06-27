using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using DReporting.Core;

namespace DReporting.Web.ReportDatas
{
    [Export("Reporting.TypeListDataProvider", typeof(IDataProvider))]
    public class TypeListDataProvider : IDataProvider
    {
        public string DataProviderName
        {
            get { return "TypeListDataProvider"; }
        }

        public object GetDataSource(NameValueCollection args, bool designTime)
        {
            var ds = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource();

            ds.DataSource = CreateData();

            ds.Fill();

            return ds;
        }

        private SupplierCollection CreateData()
        {
            SupplierCollection suppliers = new SupplierCollection();

            Supplier supplier = new Supplier("Exotic Liquids");
            suppliers.Add(supplier);
            supplier.Add(CreateProduct(supplier.SupplierID, "Chai"));
            supplier.Add(CreateProduct(supplier.SupplierID, "Chang"));
            supplier.Add(CreateProduct(supplier.SupplierID, "Aniseed Syrup"));

            supplier = new Supplier("New Orleans Cajun Delights");
            suppliers.Add(supplier);
            supplier.Add(CreateProduct(supplier.SupplierID, "Chef Anton's Cajun Seasoning"));
            supplier.Add(CreateProduct(supplier.SupplierID, "Chef Anton's Gumbo Mix"));

            supplier = new Supplier("Grandma Kelly's Homestead");
            suppliers.Add(supplier);
            supplier.Add(CreateProduct(supplier.SupplierID, "Grandma's Boysenberry Spread"));
            supplier.Add(CreateProduct(supplier.SupplierID, "Uncle Bob's Organic Dried Pears"));
            supplier.Add(CreateProduct(supplier.SupplierID, "Northwoods Cranberry Sauce"));

            return suppliers;
        }

        static Random random = new Random(5);

        private Product CreateProduct(int supplierID, string productName)
        {
            Product product = new Product(supplierID, productName);

            product.OrderDetails.AddRange(new OrderDetail[] { 
                new OrderDetail(product.ProductID, random.Next(0, 100)), 
                new OrderDetail(product.ProductID, random.Next(0, 100)),
                new OrderDetail(product.ProductID, random.Next(0, 100)) });

            return product;
        }
    }

    public class SupplierCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(ProductCollection)))
                    return TypeDescriptor.GetProperties(typeof(Product));
                else if (listAccessor.PropertyType.Equals(typeof(OrderDetailCollection)))
                    return TypeDescriptor.GetProperties(typeof(OrderDetail));
            }
            return TypeDescriptor.GetProperties(typeof(Supplier));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "Suppliers";
        }
    }

    public class Supplier
    {
        static int nextID = 0;
        int id;
        string name;
        ProductCollection products = new ProductCollection();

        public ProductCollection Products { get { return products; } }
        public int SupplierID { get { return id; } }
        public string CompanyName { get { return name; } }

        public Supplier(string name)
        {
            this.name = name;

            this.id = nextID;
            nextID++;
        }
        public void Add(Product product)
        {
            products.Add(product);
        }
    }

    public class ProductCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return TypeDescriptor.GetProperties(typeof(Product));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "Products";
        }
    }

    public class Product
    {
        static int nextID = 0;

        OrderDetailCollection orderDetails = new OrderDetailCollection();
        int suppID;
        int prodID;
        string name;

        public int SupplierID { get { return suppID; } }
        public int ProductID { get { return prodID; } }
        public string ProductName { get { return name; } }
        public OrderDetailCollection OrderDetails { get { return orderDetails; } }

        public Product(int suppID, string name)
        {
            this.suppID = suppID;
            this.name = name;

            this.prodID = nextID;
            nextID++;
        }
    }

    public class OrderDetailCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return TypeDescriptor.GetProperties(typeof(OrderDetail));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "OrderDetails";
        }
    }

    public class OrderDetail
    {
        int prodID;
        short quantity;
        public int ProductID { get { return prodID; } }
        public short Quantity { get { return quantity; } }

        public OrderDetail(int prodID, int quantity)
        {
            this.prodID = prodID;
            this.quantity = Convert.ToInt16(quantity);
        }
    }
}