using Interfaces;

namespace Tests
{
    public class TestModel : IModel
    {
        public TestModel(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public string Data;
        
        public class Factory : IFactory<TestSettings,TestModel>
        {
            public TestModel Create(TestSettings settings)
            {
                return new TestModel(settings.Id);
            }
        }
    }

    public class TestSettings
    {
        public int Id;
    }
}