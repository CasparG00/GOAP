using UnityEngine;
using UnityEngine.AI;

public class PickupStickAction : Action
{
    private bool pickedUp;
    private StickComponent targetStick;

    private float startTime;
    public float duration = 1;
    
    public PickupStickAction()
    {
        AddPrecondition("hasStick", false);
        AddEffect("hasStick", true);
    }

    public override bool IsAchievable(NavMeshAgent _agent)
    {
        var items = FindObjectsOfType<StickComponent>();
        StickComponent closest = null;
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
            Debug.Log("Stick Component not found in scene");
            return false;
        }

        targetStick = closest;
        target = targetStick.gameObject;
        
        return closest != null;
    }

    public override bool PerformAction(NavMeshAgent _agent)
    {
        Debug.Log("Performing Pickup Action");
        if (startTime == 0)
        {
            startTime = Time.time;
        }

        if (Time.time - startTime > duration)
        {
            var inventory = _agent.GetComponent<Inventory>();
            inventory.Add("Stick", 1);
            pickedUp = true;
        }
        return true;
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

    public override void Reset()
    {
        pickedUp = false;
        inRange = false;
        startTime = 0;
    }
}