using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LinqTests
{
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

        public static IEnumerable<TSource> MyTake<TSource>(this IEnumerable<TSource> source, int count)
        {
            var enumerator = source.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (index < count)
                {
                    yield return enumerator.Current;
                    index++;
                }
                else
                {
                    yield break;
                }
            }
        }
        public static IEnumerable<TSource> MySkip<TSource>(this IEnumerable<TSource> source, int count)
        {
            var enumerator = source.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (index < count)
                {
                    index++;
                }
                else
                {
                    yield return enumerator.Current;
                }
            }
        }
        public static IEnumerable<TSource> MySkipWhile<TSource>(this IEnumerable<TSource> source, int count, Func<TSource, bool> func)
        {
            var enumerator = source.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (func(enumerator.Current) && index < count)
                {
                    index++;
                }
                else
                {
                    yield return enumerator.Current;
                }
            }
        }
        public static IEnumerable<TSource> MyTakeWhile<TSource>(this IEnumerable<TSource> source, int count, Func<TSource, bool> func)
        {
            var enumerator = source.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (func(enumerator.Current))
                {
                    if (index >= count)
                    {
                        yield break;
                    }

                    yield return enumerator.Current;
                    index++;
                }
            }
        }
        public static IEnumerable<TSource> MyRealTakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> func)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (func(enumerator.Current))
                {
                    yield return enumerator.Current;

                }
                else
                {
                    yield break;
                }
            }
        }
        public static int MySum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> func)
        {
            var enumerator = source.GetEnumerator();
            var sum = 0;
            while (enumerator.MoveNext())
            {
                sum += func(enumerator.Current);
            }
            return sum;
        }
        public static IEnumerable<int> MySum<TSource>(this IEnumerable<TSource> source, int count, Func<TSource, int> func)
        {
            var enumerator = source.GetEnumerator();
            var sum = 0;
            var index = 0;
            while (enumerator.MoveNext())
            {
                index++;
                sum += func(enumerator.Current);
                if (index == count)
                {
                    yield return sum;
                    sum = 0;
                    index = 0;
                }

            }
            yield return sum;
        }
        public static bool MyAny<TSource>(this IEnumerable<TSource> source)
        {
            var enumerator = source.GetEnumerator();

            return enumerator.MoveNext();
            
        }
        public static bool MyAll<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> func)
        {
            var r = source.GetEnumerator();
            while (r.MoveNext())
            {
                if (func(r.Current))
                {
                    return false;
                }
            }
            return true;
        }
        public static List<TSource> MyDistinct<TSource>(this IEnumerable<TSource> source)
        {
            var r = source.GetEnumerator();
            
            var result = new List<TSource>();
            while (r.MoveNext())
            {
                if (!result.Contains(r.Current))
                {
                    result.Add(r.Current);
                }
            }
            return result;
        }
        
    }
}