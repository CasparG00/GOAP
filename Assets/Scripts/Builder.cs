using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Builder : MonoBehaviour, IGoap
{
    public Inventory inventory;
    private NavMeshAgent agent;

    private void Start()
    {
        if (inventory == null)
        {
            inventory = gameObject.AddComponent<Inventory>();
        }

        agent = GetComponent<NavMeshAgent>();
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

    public bool MoveAgent(Action _action)
    {
        var position = _action.target.transform.position;
        agent.SetDestination(position);

        var distance = Vector3.Distance(agent.transform.position, position);
        if (distance < 1)
        {
            _action.SetInRange(true);
            return true;
        }
        return false;
    }
}
