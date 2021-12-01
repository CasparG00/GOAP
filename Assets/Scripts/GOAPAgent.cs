using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GOAPAgent : MonoBehaviour
{
    public GOAPPlanner planner;
    
    private HashSet<Action> availableActions;
    private Queue<Action> currentActions;

    public AgentState state = AgentState.Idle;

    public NavMeshAgent agent;
    
    private void Start()
    {
        availableActions = new HashSet<Action>();
        currentActions = new Queue<Action>();
        planner = new GOAPPlanner();

        var actions = gameObject.GetComponents<Action>();
        foreach (var action in actions)
        {
            availableActions.Add(action);
        }
    }
    
    private void Update()
    {
        var action = currentActions.Peek();
        
        switch (state)
        {
            case AgentState.Idle:
                var worldData = new Dictionary<string, object>();

                // worldData.Add(new KeyValuePair<string, object>("hasOre", backpack.numOre > 0));
                // worldData.Add(new KeyValuePair<string, object>("hasLogs", backpack.numLogs > 0));
                // worldData.Add(new KeyValuePair<string, object>("hasFirewood", backpack.numFirewood > 0));
                // worldData.Add(new KeyValuePair<string, object>("hasTool", backpack.tool != null));

                var goal = new Dictionary<string, object>();

                var plan = planner.Plan(agent, availableActions, goal, worldData);
                if (plan != null)
                {
                    currentActions = plan;
                    state = AgentState.PerformAction;
                }
                else
                {
                    Debug.Log("<color=orange>Failed to make plan</color>");
                }
                break;
            case AgentState.MoveTo:
                if (action.IsInRange())
                {
                    state = AgentState.PerformAction;
                }
                break;
            case AgentState.PerformAction:
                if (action.IsCompleted()) 
                {
                    currentActions.Dequeue();
                }
                
                if (currentActions.Count == 0)
                {
                    var inRange = !action.RequiresInRange() || action.IsInRange();
                    if (inRange)
                    {
                        var success = action.PerformAction(agent);
                        if (!success)
                        {
                            state = AgentState.Idle;
                        }
                    }
                    else
                    {
                        state = AgentState.MoveTo;
                    }
                }
                else
                {
                    state = AgentState.Idle;
                }
                break;
            default:
                state = AgentState.Idle;
                break;
        }
    }

    public enum AgentState
    {
        Idle,
        MoveTo,
        PerformAction,
    }
}
