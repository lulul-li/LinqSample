using System;
using ExpectedObjects;
using LinqTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void find_products_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = WithoutLinq.FindProductByPrice(products, 200, 500, "Odd-e");

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" }
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void myLinq()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = YourOwnLinq.MyWhere(p=>p.Price>=200 &&p.Price<=500 && p.Supplier=="Odd-e", products);

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" }
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}

internal class WithoutLinq
{
    public static List<Product> FindProductByPrice(IEnumerable<Product> products, int lowBoundary, int highBoundary, string supplier)
    {
        var list = new List<Product>();
        foreach (var product in products)
        {
            if (product.Price > lowBoundary && product.Price < highBoundary && product.Supplier == supplier)
            {
                list.Add(product);
            }
        }

        return list;
    }
}

internal class YourOwnLinq
{
    public static List<Product> MyWhere(Func<Product, bool> func, IEnumerable<Product> products)
    {
        var list = new List<Product>();
        foreach (var product in products)
        {
            if (func(product))
            {
                list.Add(product);
            }
        }

        return list;
    }
}