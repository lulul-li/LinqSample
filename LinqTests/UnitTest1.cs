using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using LinqTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var actual = products.MyOwnWhere(p => p.Price >= 200 && p.Price <= 500 && p.Supplier == "Odd-e");

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" }
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void WhereEm()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MyOwnWhere(p => p.Age >= 25 && p.Age <= 40);

            var expected = new List<Employee>()
            {
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
                new Employee{Name="Mary", Role=RoleType.OP, MonthSalary=180, Age=26, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void ToHttps()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = urls.MySelect(url => url.Replace("http:", "https:") + ":80");

            var expected = new List<string>()
            {
                "https://tw.yahoo.com:80",
                "https://facebook.com:80",
                "https://twitter.com:80",
                "https://github.com:80"
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Add91Port()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = MyUrl.AddPort(urls);

            var expected = new List<string>()
            {
                "http://tw.yahoo.com:91",
                "https://facebook.com",
                "https://twitter.com:91",
                "http://github.com"
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void GetLength()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = urls.MySelect(url => url.Length);

            var expected = new List<int>()
            {
                19,20,19,17
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
    }

    public static class MyUrl
    {
        public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> sources, Func<TSource, TResult> selector)
        {
            foreach (var item in sources)
            {
                yield return selector(item);
            }
        }

        public static IEnumerable<string> AddPort(IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                yield return url.Contains("tw") ? url + ":91" : url;
            }
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

internal static class YourOwnLinq
{
    public static IEnumerable<TSource> MyWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> func, IEnumerable<TSource> products)
    {
        var enumerator = products.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (func(enumerator.Current))
            {
                yield return enumerator.Current;
            }
        }
        //var list = new List<Product>();
        //foreach (var product in products)
        //{
        //    if (func(product))
        //    {
        //        list.Add(product);
        //    }
        //}

        //return list;
    }
}

public static class MyWhere
{
    public static IEnumerable<TSource> MyOwnWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> func)
    {
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (func(enumerator.Current))
            {
                yield return enumerator.Current;
            }
        }
    }
}