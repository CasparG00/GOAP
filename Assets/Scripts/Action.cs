using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class Action : MonoBehaviour
{
    public float cost = 1f;
    public GameObject target;
    protected bool inRange;

    public Dictionary<string, object> preconditions { get; }
    public Dictionary<string, object> effects { get; }

    public Action()
    {
        preconditions = new Dictionary<string, object>();
        effects = new Dictionary<string, object>();
    }
    
    protected void AddPrecondition(string _key, object _value)
    {
        preconditions.Add(_key, _value);
    }
    
    public void RemovePrecondition(string _key)
    {
        foreach (var precondition in preconditions) {
            if (precondition.Key.Equals(_key))
            {
                preconditions.Remove(_key);
            }
        }
    }
    
    public void AddEffect(string _key, object _value)
    {
        effects.Add(_key, _value);
    }
    
    public void RemoveEffect(string _key)
    {
        foreach (var effect in effects) {
            if (effect.Key.Equals(_key))
            {
                effects.Remove(_key);
            }
        }
    }
    
    public abstract bool IsAchievable(NavMeshAgent _agent);

    public bool IsAchievableGiven(Dictionary<string, object> _conditions)
    {
        foreach (var precondition in preconditions)
        {
            if (!_conditions.ContainsKey(precondition.Key))
            {
                return false;
            }
        }
        return true;
    }

    public void DoReset()
    {
        inRange = false;
        target = null;
        Reset();
    }
    
    public abstract bool PerformAction(NavMeshAgent _agent);
    public abstract bool IsCompleted();
    public abstract bool RequiresInRange();
    public abstract bool IsInRange();
    public abstract void Reset();

    public void SetInRange(bool _inRange)
    {
        this.inRange = _inRange;
    }
}