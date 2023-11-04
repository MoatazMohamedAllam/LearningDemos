namespace linq_imperative_vs_linq_declarative
{
    internal class Program
    {
        public static List<double> MyData = new List<double>() { 2, 1, 3, 6, 9, 10, 11, 12, 13, 18 };
        static void Main(string[] args)
        {
            Declarative();
            Imperative();
        }

        private static void Declarative()
        {
            //MyData does not pushing data through the pipeline, ToList() is pulling data from the pipeline (this is the power of linq)
            var result = MyData.Select(AddOne).Select(Square).Select(SubtractTen).Where(x => x > 5).Take(2).ToList();
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadLine();
        }

        private static void Imperative()
        {
            var result = DoTake(2).ToList();
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }


        private static double AddOne(double x)
        {
            Console.WriteLine("Iam adding one");
            return x + 1;
        }

        private static double Square(double x)
        {
            Console.WriteLine("Iam doing square");
            return Math.Pow(x, 2);
        }

        private static double SubtractTen(double x)
        {
            Console.WriteLine("Iam subtracting ten ---------------");
            return x - 10;
        }

        private static IEnumerable<double> DoAddOne()
        {
            foreach (var item in MyData)
            {
                yield return AddOne(item);
            }
        }

        private static IEnumerable<double> DoSquare()
        {
            foreach (var item in DoAddOne())
            {
                yield return Square(item);
            }
        }

        private static IEnumerable<double> DoSubtractTen()
        {
            foreach (var item in DoSquare())
            {
                yield return SubtractTen(item);
            }
        }

        private static IEnumerable<double> DoWhere()
        {
            Func<double, bool> predicate = x => x > 5;
            foreach (var item in DoSubtractTen())
            {
                if (predicate(item))
                    yield return item;
            }
        }

        private static IEnumerable<double> DoTake(int n)
        {
            int i = 0;
            IEnumerator<double> cursor = DoWhere().GetEnumerator();
            do
            {
                if (i != n)
                    cursor.MoveNext();
                var current = cursor.Current;
                if (i < n)
                {
                    yield return current;
                    i++;
                }
                else yield break;
            } while (true);
        }

    }
}