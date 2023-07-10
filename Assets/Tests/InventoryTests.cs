using Interfaces;
using NUnit.Framework;

namespace Tests
{
    public class InventoryTests
    {
        private IInventory<TestModel> inventory;
        private IFactory<TestSettings,TestModel> factory;
        private TestSettings[] settingsArray = {
            new TestSettings {Id = 1},
            new TestSettings {Id = -1},
            new TestSettings {Id = 65535}
        };
        
        
        [SetUp]
        public void SetUp()
        {
            factory = new TestModel.Factory();
            inventory = new TestInventory(factory);
        }   
        
        [Test]
        public void ModelsCreating()
        {
            var concreteInventory = inventory as TestInventory;
            Assert.NotNull(concreteInventory);
           
            foreach (var settings in settingsArray) {
                var model = concreteInventory.CreateModel(settings);
                Assert.NotNull(model);
                Assert.IsTrue(model.Id.Equals(settings.Id));
            }
            
            foreach (var settings in settingsArray) {
                var model = inventory.GetModel(settings.Id);
                Assert.NotNull(model);
                Assert.IsTrue(model.Id.Equals(settings.Id));
            }
        }
        
        [Test]
        public void ModelsRemoving()
        {
            var concreteInventory = inventory as TestInventory;
            Assert.NotNull(concreteInventory);
            
            foreach (var settings in settingsArray) {
                var model = concreteInventory.CreateModel(settings);
                Assert.NotNull(model);
                Assert.IsTrue(model.Id.Equals(settings.Id));
            }
            
            var modelId = settingsArray[0].Id;
            
            concreteInventory.RemoveModel(modelId);

            var removedModel = inventory.GetModel(modelId);
            
            Assert.IsNull(removedModel);
            Assert.IsNotNull(settingsArray[1].Id);
            Assert.IsNotNull(settingsArray[2].Id);
        }

        [Test]
        public void ModelsChanging()
        {
            var testString = "BigCalls";
            var concreteInventory = inventory as TestInventory;
            Assert.NotNull(concreteInventory);
            
            foreach (var settings in settingsArray) {
                var createdModel = concreteInventory.CreateModel(settings);
                Assert.NotNull(createdModel);
                Assert.IsTrue(createdModel.Id.Equals(settings.Id));
            }
            
            var changedModel = inventory.GetModel(settingsArray[0].Id);
            Assert.IsNotNull(changedModel);
            Assert.IsTrue(string.IsNullOrEmpty(changedModel.Data));
            changedModel.Data = testString;
            var testingModel = inventory.GetModel(settingsArray[0].Id);
            var testingModel1 = inventory.GetModel(settingsArray[1].Id);
            var testingModel2 = inventory.GetModel(settingsArray[2].Id);
            Assert.IsNotNull(testingModel.Data);
            Assert.IsTrue(testingModel.Data.Equals(testString));
            Assert.IsNull(testingModel1.Data);
            Assert.IsNull(testingModel2.Data);
        }
    }
}