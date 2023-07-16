using System.Collections.Generic;
using System.Linq;
using Interfaces;

public class AbstractInventory<TModel, TSettings> : IInventory<TModel> where TModel : class, IModel
{
    private readonly IFactory<TSettings, TModel> factory;
    private readonly List<TModel> models = new List<TModel>(); 
        
    public AbstractInventory(IFactory<TSettings, TModel> factory)
    {
        this.factory = factory;
    }
    
    public IEnumerable<TModel> GetAll()
    {
        return models;
    }
    public TModel GetModel(int modelId)
    {
        return models.FirstOrDefault(x => x.Id.Equals(modelId));
    }
        
    public bool ContainsModel(int modelId)
    {
        return models.Any(x => x.Id.Equals(modelId));
    }
        
    public TModel CreateModel(TSettings settings)
    {
        if (settings is null) 
            return null;
            
        var model = factory.Create(settings);
        models.Add(model);
            
        return model;
    }
    
    public void RemoveModel(int modelId)
    {
        var model = models.FirstOrDefault(m => m.Id.Equals(modelId));
        if (model is {})
            models.Remove(model);
    }
}