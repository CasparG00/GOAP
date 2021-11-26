using System.Collections.Generic;
using UnityEngine;

public class GOAPPlanner : MonoBehaviour
{
    public Queue<Action> plan(GameObject _agent, HashSet<Action> _availableActions,
        HashSet<KeyValuePair<string, object>> _worldState, HashSet<KeyValuePair<string, object>> _goal)
    {
        foreach (var action in _availableActions)
        {
            action.Reset();
        }

        var usableActions = new HashSet<Action>();
        foreach (var action in usableActions)
        {
            if (action.CheckProceduralPrecondition(_agent))
            {
                usableActions.Add(action);
            }
        }

        var leaves = new List<Node>();

        var start = new Node(null, 0, _worldState, null);
        var success = BuildGraph(start, leaves, usableActions, _goal);

        return null;
    }

    private bool BuildGraph (Node _parent, List<Node> _leaves, HashSet<Action> _usableActions, HashSet<KeyValuePair<string, object>> _goal)
    {
        var found = false;

        foreach (var action in _usableActions)
        {
            
        }

        return false;
    }
    
    private bool inState(HashSet<KeyValuePair<string,object>> test, HashSet<KeyValuePair<string,object>> state) {
        var allMatch = true;
        foreach (var t in test) {
            var match = false;
            foreach (var s in state)
            {
                if (!s.Equals(t)) continue;
                match = true;
                break;
            }
            if (!match)
                allMatch = false;
        }
        return allMatch;
    }

    private class Node {
        public Node parent;
        public float runningCost;
        public HashSet<KeyValuePair<string,object>> state;
        public Action action;

        public Node(Node _parent, float _runningCost, HashSet<KeyValuePair<string,object>> _state, Action _action) {
            this.parent = _parent;
            this.runningCost = _runningCost;
            this.state = _state;
            this.action = _action;
        }
    }
}
