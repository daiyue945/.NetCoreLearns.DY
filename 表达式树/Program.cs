using ExpressionTreeToString;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
namespace 表达式树
{
    class Program
    {
        static void Main(string[] args)
        {
            //Expression<Func<Book, bool>> e1 = b => b.Price > 5;

            //Expression<Func<Book, Book, double>> e2 = (b1, b2) => b1.Price + b2.Price;
            //Console.WriteLine(e2.ToString("Factory methods", "C#"));
            //Console.WriteLine(e1.ToString("Object notation", "C#"));





            //var b1 = Parameter(
            //    typeof(Book),
            //    "b1"
            //);
            //var b2 = Parameter(
            //    typeof(Book),
            //    "b2"
            //);

            //Expression<Func<Book, Book, double>> lambdaExpression =Expression.Lambda<Func<Book,Book,double>>(
            //         Add(
            //             MakeMemberAccess(b1,
            //                 typeof(Book).GetProperty("Price")
            //             ),
            //             MakeMemberAccess(b2,
            //                 typeof(Book).GetProperty("Price")
            //             )
            //         ),
            //         b1, b2
            //     );
            //using (MyDbContext ctx = new MyDbContext())
            //{
            //    var book1 = ctx.Books.Where(t => t.Id == 2).SingleOrDefault();
            //    var book2 = ctx.Books.Where(t => t.Id == 3).SingleOrDefault();
            //    var result =lambdaExpression.Compile()(book1, book2);
            //    Console.WriteLine($"结果：{result}");

            //}

            //Func<Book, bool> f1 = b => b.Price > 5;

            //using (MyDbContext ctx = new MyDbContext())
            //{
            //    var c = ctx.Books.Where(e1).ToArray();
            //    //var c = ctx.Books.Where(f1).ToArray();
            //    foreach (var item in c)
            //    {
            //        Console.WriteLine(item.Name);
            //    }
            //}

            ////指定参数
            //ParameterExpression parameterExpression = Expression.Parameter(typeof(Book), "b");
            ////左节点
            //MemberExpression memberExpLeft = Expression.MakeMemberAccess(parameterExpression, typeof(Book).GetProperty("Price"));
            ////右节点
            //ConstantExpression constantExpRight = Expression.Constant(5.0, typeof(double));
            ////body
            //BinaryExpression binaryExpression = Expression.GreaterThan(memberExpLeft, constantExpRight);
            ////组合
            //Expression<Func<Book, bool>> lambdaExpression = Expression.Lambda<Func<Book, bool>>(binaryExpression, parameterExpression);

            //using (MyDbContext ctx = new MyDbContext())
            //{
            //    var c = ctx.Books.Where(lambdaExpression).ToArray();
            //    foreach (var item in c)
            //    {
            //        Console.WriteLine(item.Name);
            //    }
            //}

            //var books = QueryBooks("Price", 54.9);
            //foreach (var item in books)
            //{
            //    Console.WriteLine($"结果是：{item.Name}");
            //}
            //using (MyDbContext ctx = new MyDbContext())
            //{
            //    var books = ctx.Books.Select(b => new object[] { b.Name, b.Price });
            //    foreach (var item in books)
            //    {
            //        Console.WriteLine($"{item[0]}的价格是{item[1]}");
            //    }
            //}

            //var books = QueryProptery<Book>("Name", "Price");
            //foreach (var item in books)
            //{
            //    Console.WriteLine($"{item[0]}的价格是{item[1]}");
            //}

            Book[] books = QueryDynamic("太阳", 10, 80, 2);
            foreach (var item in books)
            {
                Console.WriteLine($"序号为{item.Id}的名称是{item.Name}，价格是{item.Price}");
            }
        }


        static Book[] QueryDynamic(string name, double? lowPrice, double? upPrice, int orderType)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                IQueryable<Book> books = ctx.Books;
                if (!string.IsNullOrEmpty(name))
                {
                    books = books.Where(b => b.Name.Contains(name));
                }
                if (lowPrice != null)
                {
                    books = books.Where(b => b.Price >= lowPrice);
                }
                if (upPrice != null)
                {
                    books = books.Where(b => b.Price <= upPrice);
                }
                switch (orderType)
                {
                    case 1:
                        books.OrderByDescending(b => b.Price);
                        break;
                    case 2:
                        books.OrderBy(b => b.Price);
                        break;
                }
                return books.ToArray();

            }

        }

        //动态查询用户指定列
        static IEnumerable<object[]> QueryProptery<T>(params string[] propertyName) where T : class
        {
            var p = Parameter(typeof(T));
            List<Expression> propExprList = new List<Expression>();
            foreach (var propName in propertyName)
            {
                Expression expression = Convert(MakeMemberAccess(p,
                    typeof(T).GetProperty(propName)),
                    typeof(object));
                propExprList.Add(expression);
            }
            var newArrayExpr = NewArrayInit(typeof(object), propExprList.ToArray());
            var selectExpr = Lambda<Func<T, object[]>>(newArrayExpr, p);
            using (MyDbContext ctx = new MyDbContext())
            {
                return ctx.Set<T>().Select(selectExpr).ToArray();
            }
        }


        static IEnumerable<Book> QueryBooks(string propertyName, object value)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                Expression<Func<Book, bool>> e1;
                var e = Parameter(
                typeof(Book),
                 "e"
             );

                Type valueType = typeof(Book).GetProperty(propertyName).PropertyType;
                if (valueType.IsPrimitive)//原始类型
                {
                    e1 = Lambda<Func<Book, bool>>(
                        Equal(
                            MakeMemberAccess(e,
                            typeof(Book).GetProperty(propertyName)
                            ),
                            Constant(System.Convert.ChangeType(value, valueType))
                            ),
                        e
                      );
                }
                else
                {
                    e1 = Lambda<Func<Book, bool>>(
                        MakeBinary(ExpressionType.Equal,
                            MakeMemberAccess(e,
                            typeof(Book).GetProperty(propertyName)
                            ),
                            Constant(value), false,
                            typeof(string).GetMethod("op_Equality")
                            ),
                        e
                     );
                }
                return ctx.Books.Where(e1).ToList();
            }
        }
    }
}
