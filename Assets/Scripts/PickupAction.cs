using UnityEngine;
using UnityEngine.AI;

public class PickupAction : Action
{
    private bool pickedUp = false;
    private WeaponComponent targetWeapon;

    public PickupAction()
    {
        AddPrecondition("hasItem", false);
        AddEffect("hasItem", true);
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }

    public override bool CheckProceduralPrecondition(GameObject _agent)
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

    public override bool PerformAction(GameObject _agent)
    {
        _agent.transform.GetComponent<NavMeshAgent>().SetDestination(targetWeapon.transform.position);
        return false;
    }
}