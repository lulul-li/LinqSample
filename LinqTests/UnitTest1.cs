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
        [TestMethod]
        public void find_products_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = WithoutLinq.FindProductByPrice(products, 200, 500);

            var expected = new List<Product>()
            {
                //todo
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
            var actual = products.myFirstOrDefault();
        }
        [TestMethod]
        public void FirstOrDefault_Predicate()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.myFirstOrDefault(c => c.Price > 200);
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
}

internal class YourOwnLinq
{
}