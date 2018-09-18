using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void find_products_that_price_between_200_and_500()
        //{
        //    var products = RepositoryFactory.GetProducts();
        //    var actual = WithoutLinq.FindProductByPrice(products, 200, 500, "Odd-e");

        //    var expected = new List<Product>()
        //    {
        //        new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
        //        new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
        //    };

        //    expected.ToExpectedObject().ShouldEqual(actual);
        //}

        [TestMethod]
        public void myLinq()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyOwnWhere(p => p.Price >= 200 && p.Price <= 500 && p.Supplier == "Odd-e");

            var expected = new List<Product>()
            {
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
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
                new Employee {Name = "Tom", Role = RoleType.Engineer, MonthSalary = 140, Age = 33, WorkingYear = 2.6},
                new Employee {Name = "Bas", Role = RoleType.Engineer, MonthSalary = 280, Age = 36, WorkingYear = 2.6},
                new Employee {Name = "Mary", Role = RoleType.OP, MonthSalary = 180, Age = 26, WorkingYear = 2.6},
                new Employee {Name = "Joey", Role = RoleType.Engineer, MonthSalary = 250, Age = 40, WorkingYear = 2.6},
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
                19,
                20,
                19,
                17
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void GetEmployeesSalary()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MyOwnWhere(e => e.Role == RoleType.Engineer).MySelect(e => e.MonthSalary);

            var expected = new List<int>()
            {
                100,
                140,
                280,
                120,
                250
            };

            foreach (var VARIABLE in actual)
            {
                Console.WriteLine();
            }
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void MyTake()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyTake(3);

            var expected = new List<Product>()
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"}
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void MySelect()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MyTake(3).MySelect((x, index) => $"{index + 1}-{x.Name}");

            var expected = new List<string>()
            {
                "1-Joe",
                "2-Tom",
                "3-Kevin"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void MySkip()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MySkip(6);

            var expected = new List<Employee>()
            {
                new Employee {Name = "Frank", Role = RoleType.Engineer, MonthSalary = 120, Age = 16, WorkingYear = 2.6},
                new Employee {Name = "Joey", Role = RoleType.Engineer, MonthSalary = 250, Age = 40, WorkingYear = 2.6},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void MySkipWhile()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MySkipWhile(4, p => p.Price > 300);

            var expected = new List<Product>()
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void MyTakeWhile()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyTakeWhile(2, p => p.Price > 300);

            var expected = new List<Product>()
            {
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Real_TakeWhile()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MyRealTakeWhile(p => p.Age > 30);

            var expected = new List<Employee>()
            {
                new Employee {Name = "Joe", Role = RoleType.Engineer, MonthSalary = 100, Age = 44, WorkingYear = 2.6},
                new Employee {Name = "Tom", Role = RoleType.Engineer, MonthSalary = 140, Age = 33, WorkingYear = 2.6},
                new Employee {Name = "Kevin", Role = RoleType.Manager, MonthSalary = 380, Age = 55, WorkingYear = 2.6},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void sum()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MySum(p => p.Price);

            var expected = 3650;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void sumGroup3()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MySum(3, p => p.Price);

            var expected = new List<int>
            {
                630,
                1530,
                1490
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void sumGroup5()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MySum(5, p => p.Price);

            var expected = new List<int>
            {
                1550,
                2100
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void any()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyAny();
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void all()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyAll(x => x.Price > 200);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void FirstOrDefault()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyAll(x => x.Price > 200);
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void distinct()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.Select(x => x.Role).MyDistinct();
            var expected = new List<RoleType>
            {
                RoleType.Engineer,
                RoleType.Manager,
                RoleType.OP
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
          
        }

        [TestMethod]
        public void distinct1()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MyDistinct1(new EmployeeComparer());
            var expected = new List<Employee>()
            {
                new Employee { Name = "Joe", Role = RoleType.Engineer, MonthSalary = 100, Age = 44, WorkingYear = 2.6 },
                new Employee { Name = "Kevin", Role = RoleType.Manager, MonthSalary = 380, Age = 55, WorkingYear = 2.6 },
                new Employee { Name = "Andy", Role = RoleType.OP, MonthSalary = 80, Age = 22, WorkingYear = 2.6 }
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());

        }
        [TestMethod]
        public void single()
        {
            var employees = RepositoryFactory.GetEmployees();
        }

        [TestMethod]
        public void luckball()
        {
            var ball = RepositoryFactory.GetBalls();
            var luckyBall =
                new RepositoryFactory.ColorBall {Color = RepositoryFactory.Color.Purple, Size = "S", Prize = 500};
           
            Assert.IsTrue(myContants(ball,luckyBall));

        }
        [TestMethod]
        public void TestsequenceComparer()
        {
            var ball = RepositoryFactory.GetBalls();
            var luckyBall = RepositoryFactory.GetBalls();
            Assert.IsTrue(MySequenceComparer(ball, luckyBall));

        }

        private bool MySequenceComparer(IEnumerable<RepositoryFactory.ColorBall> ball, IEnumerable<RepositoryFactory.ColorBall> luckyBall)
        {
            return false;

        }

        private bool myContants(IEnumerable<RepositoryFactory.ColorBall> ball, RepositoryFactory.ColorBall luckyBall)
        {
            var ballEnumer = ball.GetEnumerator();
            var ballComparer = new BallComparer();
            while (ballEnumer.MoveNext())
            {
                if (ballComparer.Equals(ballEnumer.Current,luckyBall))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class EmployeeComparer:IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.Role == y.Role;
        }

        public int GetHashCode(Employee obj)
        {
            return obj.Role.GetHashCode();
        }
    }

    internal class BallComparer:IEqualityComparer<RepositoryFactory.ColorBall>
    {
        public bool Equals(RepositoryFactory.ColorBall x, RepositoryFactory.ColorBall y)
        {
            return x.Color == y.Color && x.Prize == y.Prize && x.Size == y.Size;
        }


        public int GetHashCode(RepositoryFactory.ColorBall obj)
        {
            return 0;
        }
    }

    public class MyEqualCompare : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.Role == y.Role;
        }

        public int GetHashCode(Employee obj)
        {
            return 0;
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

        public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> sources, Func<TSource, int, TResult> selector)
        {
            var index = 0;
            foreach (var item in sources)
            {
                yield return selector(item, index++);
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

//internal class WithoutLinq
//{
//    public static List<Product> FindProductByPrice(IEnumerable<Product> products, int lowBoundary, int highBoundary,
//        string supplier)
//    {
//        var list = new List<Product>();
//        foreach (var product in products)
//        {
//            if (product.Price > lowBoundary && product.Price < highBoundary && product.Supplier == supplier)
//            {
//                list.Add(product);
//            }
//        }

//        return list;
//    }
//}

//internal static class YourOwnLinq
//{
//    public static IEnumerable<TSource> MyWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> func, IEnumerable<TSource> products)
//    {
//        var enumerator = products.GetEnumerator();
//        while (enumerator.MoveNext())
//        {
//            if (func(enumerator.Current))
//            {
//                yield return enumerator.Current;
//            }
//        }
//    }
//}}