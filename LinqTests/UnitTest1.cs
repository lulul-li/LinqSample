using System;
using ExpectedObjects;
using LinqTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NSubstitute.Core;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        [Ignore]
        [TestMethod]
        public void find_products_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = WithoutLinq.FindProductByPrice(products, 200, 500);

            var expected = new List<Product>()
            {
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void Any()
        {
            var products = RepositoryFactory.GetProducts();
            Assert.IsTrue(products.myAny());
        }

        [TestMethod]
        public void AnyWithPredicate()
        {
            var products = RepositoryFactory.GetProducts();
            Assert.IsTrue(products.myAny(x => x.Price > 200));
        }

        [TestMethod]
        public void All()
        {
            var products = RepositoryFactory.GetProducts();
            Assert.IsFalse(products.myAll(current => current.Price > 200));

        }
        [TestMethod]
        public void FirstOrDefault()
        {
            var products = RepositoryFactory.GetProducts();
            Assert.AreEqual(1, products.myFirstOrDefault().Id);
        }
        [TestMethod]
        public void FirstOrDefault_Predicate()
        {
            var products = RepositoryFactory.GetProducts();
            Assert.AreEqual(2,products.myFirstOrDefault(c => c.Price > 200).Id);
        }
        [TestMethod]
        public void Single()
        {
            var products = RepositoryFactory.GetProducts();
            Assert.AreEqual(110,products.mySingle(p=>p.Price<200).Price);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Single_Exception()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.mySingle(p => p.Price <0);
        }

        [TestMethod]
        public void Distinct()
        {
            var expected = new List<Employee>
            {
                new Employee {Name = "Joe", Role = RoleType.Engineer, MonthSalary = 100, Age = 44, WorkingYear = 2.6},
                new Employee {Name = "Kevin", Role = RoleType.Manager, MonthSalary = 380, Age = 55, WorkingYear = 2.6},
                new Employee {Name = "Andy", Role = RoleType.OP, MonthSalary = 80, Age = 22, WorkingYear = 2.6},
            };

            var products = RepositoryFactory.GetEmployees();
            WithoutLinq.myDistinct(products);
        }
    }
}

internal static class WithoutLinq
{
    public static List<Product> FindProductByPrice(IEnumerable<Product> products, int lowBoundary, int highBoundary)
    {
        throw new System.NotImplementedException();
    }

    public static bool myAny<TSource>(this IEnumerable<TSource> products)
    {
        return products.GetEnumerator().MoveNext();
    }

    public static bool myAny<TSource>(this IEnumerable<TSource> products, Func<TSource, bool> func)
    {
        var enumerator = products.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (func(enumerator.Current))
            {
                return true;
            }
        }
        return false;
    }

    public static bool myAll(this IEnumerable<Product> products, Func<Product, bool> predicate)
    {
        var enumerator = products.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                return false;
            }
        }
        return false;
    }
    public static TSource myFirstOrDefault<TSource>(this IEnumerable<TSource> products)
    {
        var enumerator = products.GetEnumerator();
        while (enumerator.MoveNext())
        {

            return enumerator.Current;

        }
        return default(TSource);
    }
    public static TSource myFirstOrDefault<TSource>(this IEnumerable<TSource> products, Func<TSource, bool> prediacte)
    {
        var enumerator = products.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (prediacte(enumerator.Current))
            {
                return enumerator.Current;
            }
        }
        return default(TSource);
    }

    public static TScourec mySingle<TScourec>(this IEnumerable<TScourec> products, Func<TScourec, bool> predicate)
    {
        var enumerator = products.GetEnumerator();
        var isMatch = false;
        var singleData = default(TScourec);
        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                if (isMatch)
                {
                    throw new InvalidOperationException();
                }
                isMatch = true;
                singleData = enumerator.Current;
            }
           
        }
        if (!isMatch)
        {
            throw new InvalidOperationException();
        }

        return singleData;
    }

    public static void myDistinct(IEnumerable<Employee> products)
    {
        throw new NotImplementedException();
    }
}

internal class YourOwnLinq
{
}