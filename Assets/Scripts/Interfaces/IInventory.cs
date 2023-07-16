using System.Collections.Generic;
namespace Interfaces
{
    public interface IInventory<T> where T: IModel
    {
        public T GetModel(int modelId);
        IEnumerable<T> GetAll();
    }
}