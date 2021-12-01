using UnityEngine;
using UnityEngine.AI;

public class PickupAction : Action
{
    private bool pickedUp = false;
    private bool inRange = false;
    private WeaponComponent targetWeapon;
    
    private PickupAction()
    {
        AddPrecondition("hasItem", false);
        AddEffect("hasItem", true);
    }

    public override bool DoProceduralPrecondition(NavMeshAgent _agent)
    {
        var weapons = Object.FindObjectsOfType<WeaponComponent>();
        WeaponComponent closest = null;
        var minDist = Mathf.Infinity;

        foreach (var weapon in weapons)
        {
            var dist = Vector3.Distance(weapon.transform.position, _agent.transform.position);
            if (dist < minDist)
            {
                closest = weapon;
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