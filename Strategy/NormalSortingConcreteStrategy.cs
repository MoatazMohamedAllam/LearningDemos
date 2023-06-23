using Strategy;

internal class NormalSortingConcreteStrategy : IStrategy
{
    public object DoAlgorithm(List<string> list)
    {
        var newList = list;
        newList.Sort();
        return newList;
    }
}