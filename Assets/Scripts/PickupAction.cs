using UnityEngine;
using UnityEngine.AI;

public class PickupAction : Action
{
    private bool pickedUp = false;
    private bool inRange = false;
    private ItemComponent targetWeapon;
    
    private PickupAction()
    {
        AddPrecondition("hasItem", false);
        AddEffect("hasItem", true);
    }

    public override bool IsAchievable(NavMeshAgent _agent)
    {
        var items = FindObjectsOfType<ItemComponent>();
        ItemComponent closest = null;
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
        
        targetWeapon = closest;
        return closest != null;
    }

    public override bool PerformAction(NavMeshAgent _agent)
    {
        _agent.SetDestination(targetWeapon.transform.position);
        return false;
    }

    public override bool IsCompleted()
    {
        return pickedUp;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override bool IsInRange()
    {
        return inRange;
    }
}