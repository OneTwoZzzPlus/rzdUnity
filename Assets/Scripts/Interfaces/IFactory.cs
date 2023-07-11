namespace Interfaces
{
    public interface IFactory<TSettings, TModel>
    {
        public TModel Create(TSettings settings);
    }
}