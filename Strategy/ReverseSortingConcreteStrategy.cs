using Strategy;

internal class ReverseSortingConcreteStrategy : IStrategy
{
    public object DoAlgorithm(List<string> list)
    {
        var newList = list;
        newList.Sort();
        newList.Reverse();

        return newList;
    }
}