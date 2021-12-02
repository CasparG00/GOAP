using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GOAPPlanner
{
    public Queue<Action> Plan(NavMeshAgent _agent, IEnumerable<Action> _availableActions, Dictionary<string, object> _goal, Dictionary<string, object> _worldStates)
    {
        var usableActions = new List<Action>();

        foreach (var action in _availableActions)
        {
            action.Reset();
            
            if (action.IsAchievable(_agent))
            {
                usableActions.Add(action);
            }
        }

        var leaves = new List<Node>();

        // build graph
        var start = new Node(null, 0, _worldStates, null);
        var success = BuildGraph(start, leaves, usableActions, _goal);

        if (!success)
        {
            Debug.Log("NO PLAN");
            return null;
        }

        Node cheapest = null;
        foreach (var leaf in leaves)
        {
            if (cheapest == null)
            {
                cheapest = leaf;
            }
            else
            {
                if (leaf.cost < cheapest.cost)
                {
                    cheapest = leaf;
                }
            }
        }

        var result = new List<Action>();

        var node = cheapest;

        while (node != null)
        {
            if (node.action != null)
            {
                result.Insert(0, node.action);
            }

            node = node.parent;
        }

        var queue = new Queue<Action>();
        foreach (var action in result)
        {
            queue.Enqueue(action);
        }

        return queue;
    }

    private bool BuildGraph(Node _parent, List<Node> _leaves, List<Action> _usableActions,
        Dictionary<string, object> _goal)
    {
        var pathFound = false;

        foreach (var action in _usableActions)
        {
            if (action.IsAchievableGiven(_parent.state))
            {
                Debug.Log(action + "is achievable");
                var currentState = new Dictionary<string, object>(_parent.state);

                foreach (var effects in action.effects)
                {
                    Debug.Log(effects.Key);
                    if (!currentState.ContainsKey(effects.Key))
                    {
                        currentState.Add(effects.Key, effects.Value);
                        Debug.Log("<color=green>Added State: </color>" + effects.Key);
                    }
                }

                var node = new Node(_parent, _parent.cost + action.cost, currentState, action);

                if (GoalAchieved(_goal, currentState))
                {
                    Debug.Log("Goal Achieved");
                    _leaves.Add(node);
                    pathFound = true;
                }
                else
                {
                    var subset = ActionSubset(_usableActions, action);
                    var found = BuildGraph(node, _leaves, subset, _goal);
                    if (found)
                    {
                        pathFound = true;
                    }
                }
            }
        }

        return pathFound;
    }

    private bool GoalAchieved(Dictionary<string, object> _goal, Dictionary<string, object> _state)
    {
        foreach (var g in _goal)
        {
            if (!_state.ContainsKey(g.Key))
            {
                return false;
            }
        }

        return true;
    }

    private List<Action> ActionSubset(List<Action> _actions, Action _removing)
    {
        var subset = new List<Action>();

        foreach (var action in _actions)
        {
            if (!action.Equals(_removing))
            {
                subset.Add(action);
            }
        }

        return subset;
    }

    private class Node {
        public Node parent;
        public float cost;
        public Dictionary<string,object> state;
        public Action action;

        public Node(Node _parent, float _cost, Dictionary<string,object> _state, Action _action) {
            this.parent = _parent;
            this.cost = _cost;
            this.state = _state;
            this.action = _action;
        }
    }
}
