using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Interfaces;
using UnityEngine;

[CreateAssetMenu(menuName = "Create SimpleSignInventory", fileName = "SimpleSignInventory", order = 0)]
public class SimpleSignInventory : ScriptableObject, IInventory<SignData>
{
    [SerializeField] private List<SignData> signsData;

    public SignData GetModel(int modelId)
    {
        return signsData.FirstOrDefault(s => s.Id.Equals(modelId));
    }
}