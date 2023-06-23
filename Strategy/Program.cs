
using Strategy;

Console.WriteLine("Strategy Design patterns");



var ctx = new Context();

Console.WriteLine("Client: Strategy is set to normal sorting");

ctx.SetStrategy(new NormalSortingConcreteStrategy());
ctx.DoSomeBusinessLogic();
Console.WriteLine();

Console.WriteLine("Client: Strategy is set to reverse sorting");

ctx.SetStrategy(new ReverseSortingConcreteStrategy());
ctx.DoSomeBusinessLogic();





