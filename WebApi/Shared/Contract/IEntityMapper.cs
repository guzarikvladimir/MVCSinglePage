namespace Shared.Contract
{
    public interface IEntityMapper<in TFrom, out TTo>
    {
        TTo Map(TFrom model);
    }
}