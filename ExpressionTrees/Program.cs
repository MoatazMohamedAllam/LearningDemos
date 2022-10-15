using System.Linq.Expressions;

ParameterExpression pe = Expression.Parameter(typeof(int), "s");

MemberExpression me = Expression.Property(pe, "Age");

ConstantExpression constant = Expression.Constant(18, typeof(int));

BinaryExpression body = Expression.GreaterThanOrEqual(me, constant);

var ExpressionTree = Expression.Lambda<Func<int, bool>>(body, new[] { pe });

Console.WriteLine("Expression Tree: {0}", ExpressionTree);

Console.WriteLine("Expression Tree Body: {0}", ExpressionTree.Body);

Console.WriteLine("Number of Parameters in Expression Tree: {0}",
								ExpressionTree.Parameters.Count);

Console.WriteLine("Parameters in Expression Tree: {0}", ExpressionTree.Parameters[0]);