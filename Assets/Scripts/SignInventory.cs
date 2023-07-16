using Data;
using Interfaces;
using Model;

namespace DefaultNamespace
{
    public class SignInventory : AbstractInventory<SignModel, SignData>
    {
        public SignInventory(IFactory<SignData, SignModel> factory) : base(factory)
        {

        }
    }
}