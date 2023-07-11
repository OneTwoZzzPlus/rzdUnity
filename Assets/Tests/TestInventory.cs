using Interfaces;

namespace Tests
{
    public class TestInventory : AbstractInventory<TestModel, TestSettings>
    {
        public TestInventory(IFactory<TestSettings, TestModel> factory) : base(factory)
        {
        }
    }
}