using System.Collections.Generic;
using UnityEngine;

public abstract class Action
{
    private readonly HashSet<KeyValuePair<string, object>> myPreconditions;
    private readonly HashSet<KeyValuePair<string, object>> myEffects;

    public Action() 
    {
        myPreconditions = new HashSet<KeyValuePair<string, object>> ();
        myEffects = new HashSet<KeyValuePair<string, object>> ();
    }

    public abstract bool PerformAction(GameObject _agent);

    public void AddPrecondition(string _key, object _value)
    {
        myPreconditions.Add(new KeyValuePair<string, object>(_key, _value));
    }

    public void RemovePrecondition(string _key)
    {
        foreach (var precondition in myPreconditions) {
            if (precondition.Key.Equals(_key))
            {
                myPreconditions.Remove(precondition);   
            }
        }
    }
    
    public void AddEffect(string _key, object _value)
    {
        myEffects.Add(new KeyValuePair<string, object>(_key, _value));
    }
    
    public void RemoveEffect(string _key)
    {
        foreach (var effect in myEffects) {
            if (effect.Key.Equals(_key))
            {
                myEffects.Remove(effect);   
            }
        }
    }
    
    public HashSet<KeyValuePair<string, object>> preconditions => myPreconditions;

    public HashSet<KeyValuePair<string, object>> effects => myEffects;
}