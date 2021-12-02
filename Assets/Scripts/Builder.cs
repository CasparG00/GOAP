using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour, IGoap
{
    public Inventory inventory;

    private void Start()
    {
        if (inventory == null)
        {
            inventory = gameObject.AddComponent<Inventory>();
        }
    }

    public Dictionary<string, object> GetWorldData()
    {
        var worldData = new Dictionary<string, object>();
        
        worldData.Add("hasStick", inventory.GetAmount("Stick") > 0);
        
        return worldData;
    }

    public Dictionary<string, object> CreateGoals()
    {
        var goal = new Dictionary<string, object>();
        
        goal.Add("deliverStick", true);

        return goal;
    }
}
