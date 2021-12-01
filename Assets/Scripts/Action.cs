using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Action
{
    public string name = "Abstract Action";
    public float cost = 1f;
    public GameObject target;
    public float duration = 0f;

    public Dictionary<string, object> preconditions { get; }
    public Dictionary<string, object> effects { get; }

    public bool IsAchievable()
    {
        return true;
    }

    public void AddPrecondition(string _key, object _value)
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

    public bool IsAchievableGiven(Dictionary<string, object> _conditions)
    {
        foreach (var precondition in effects)
        {
            if (!_conditions.ContainsKey(precondition.Key))
            {
                return false;
            }
        }
        return true;
    }

    public abstract bool DoProceduralPrecondition(NavMeshAgent _agent);
    public abstract bool PerformAction(NavMeshAgent _agent);
    public abstract bool IsCompleted();
    public abstract bool RequiresInRange();
    public abstract bool IsInRange();
}