using UnityEngine;
using UnityEngine.AI;

public class DropOffSticks : Action
{
    private bool droppedOffSticks;
    private HouseComponent targetHouseComponent;

    public DropOffSticks()
    {
        AddPrecondition("hasStick", true);
        AddEffect("hasStick", false);
        AddEffect("deliverStick", true);
    }
    
    public override bool IsAchievable(NavMeshAgent _agent)
    {
        var items = FindObjectsOfType<HouseComponent>();
        HouseComponent closest = null;
        var minDist = Mathf.Infinity;

        foreach (var item in items)
        {
            var dist = Vector3.Distance(item.transform.position, _agent.transform.position);
            if (dist < minDist)
            {
                closest = item;
                minDist = dist;
            }
        }

        if (closest == null)
        {
            Debug.Log("House Component not found in scene");
            return false;
        }
        
        targetHouseComponent = closest;
        target = targetHouseComponent.gameObject;
        
        return closest != null;
    }

    public override bool PerformAction(NavMeshAgent _agent)
    {
        var inventory = _agent.GetComponent<Inventory>();
        inventory.Remove("Stick", inventory.GetAmount("Stick"));

        return true;
    }

    public override bool IsCompleted()
    {
        return droppedOffSticks;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override bool IsInRange()
    {
        return true;
    }

    public override void Reset()
    {
        droppedOffSticks = false;
    }
}
