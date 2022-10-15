using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "moataz", "ali", "mohamed" };
            var orderedListByLength = names.OrderBy(n => n.Length);
            var orderedListByAlphabet = names.OrderBy(n => n);
            var resultsContainsA = names.MyWhere(x => x.Contains('a'));
            IEnumerable<string> results = Enumerable.Where(names, x => x.Length >= 4);



            //-----------------------------------------------------------

            //tip: important thing of query operators that they executed not when constructed but when enumerated 

            var numbers = new List<int> { 1 };
            IEnumerable<int> query = numbers.Select(x => x * 10); // here no code executed

            foreach (var item in query)// here the execution of query operator will occur 
            {
                //Console.WriteLine(item);
            }


            // If your query’s lambda expressions capture outer variables, the query will honor the
            //value of those variables at the time the query runs
            int[] nums = { 1, 2 };
            int factor = 10;
            IEnumerable<int> query2 = numbers.Select(n => n * factor);
            factor = 20;
            foreach (int n in query2) Console.Write(n + "|"); // will display 20, 40


            IEnumerable<char> q = "Not what you might expect";
            string vowels = "aeiou";
            for (int i = 0; i < vowels.Length; i++)
                q = q.Where(c => c != vowels[i]); 

            foreach (char c in q) Console.Write(c); // will throw indexoutOfRange

            // to solve this problem
            for (int i = 0; i < vowels.Length; i++)
            {
                char vowel = vowels[i];
                query = query.Where(c => c != vowel);
            } // Or
            foreach (char vowel in vowels)
                query = query.Where(c => c != vowel);
        }
    }

    public static class MyExtensions
    {
        public static IEnumerable<TSource> MyWhere<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            List<TSource> list = new List<TSource>();

            foreach (var element in sequence)
                if (predicate(element))
                    yield return element;
        }

        public static void HowCompilerTranslateExpressions()
        {
            Expression<Func<Student, bool>> isTeenAgerExpr = s => s.Age > 12 && s.Age < 20;

            //will translate to 

            Expression.Lambda<Func<Student, bool>>(
                Expression.AndAlso(
                    Expression.GreaterThan(Expression.Property(pe, "Age"), Expression.Constant(12, typeof(int))),
                    Expression.LessThan(Expression.Property(pe, "Age"), Expression.Constant(20, typeof(int))),
                        new[] { pe });
        }

        private static bool Zarb(Student s)
        {
            return s.Age > 18;
        }

        public static void CreateExpression()
        {
            Func<Student, bool> isAdult = s => s.Age >= 18;
            //we can create this expression like below 

            ParameterExpression pe = Expression.Parameter(typeof(Student), "s");

            MemberExpression me = Expression.Property(pe, "Age");

            ConstantExpression constant = Expression.Constant(18, typeof(int));

            BinaryExpression body = Expression.GreaterThanOrEqual(me, constant);

            var ExpressionTree = Expression.Lambda<Func<Student, bool>>(body, new[] { pe });

            Console.WriteLine("Expression Tree: {0}", ExpressionTree);

            Console.WriteLine("Expression Tree Body: {0}", ExpressionTree.Body);

            Console.WriteLine("Number of Parameters in Expression Tree: {0}",
                                            ExpressionTree.Parameters.Count);

            Console.WriteLine("Parameters in Expression Tree: {0}", ExpressionTree.Parameters[0]);
        }

        public static void InspectExpression()
        {
            Expression<Func<Student, bool>> isTeenAgerExpr = s => s.Age > 12 && s.Age < 20;

            Console.WriteLine("Expression: {0}", isTeenAgerExpr);

            Console.WriteLine("Expression Type: {0}", isTeenAgerExpr.NodeType);

            var parameters = isTeenAgerExpr.Parameters;

            foreach (var param in parameters)
            {
                Console.WriteLine("Parameter Name: {0}", param.Name);
                Console.WriteLine("Parameter Type: {0}", param.Type.Name);
            }
            var bodyExpr = isTeenAgerExpr.Body as BinaryExpression;

            Console.WriteLine("Left side of body expression: {0}", bodyExpr.Left);
            Console.WriteLine("Binary Expression Type: {0}", bodyExpr.NodeType);
            Console.WriteLine("Right side of body expression: {0}", bodyExpr.Right);
            Console.WriteLine("Return Type: {0}", isTeenAgerExpr.ReturnType);

        //Expression: s => ((s.Age > 12) AndAlso(s.Age < 20))
        //Expression Type: Lambda
        //Parameter Name: s
        //Parameter Type: Student
        //Left side of body expression: (s.Age > 12)
        //Binary Expression Type: AndAlso
        //Right side of body expression: (s.Age < 20)
        //Return Type: System.Boolean

        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
