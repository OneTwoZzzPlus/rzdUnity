using System.Collections.Generic;

namespace Interfaces
{
    public interface IRegistry<T>
    {
        T Get(int id);
        IEnumerable<T> GetAll();

    }
}
