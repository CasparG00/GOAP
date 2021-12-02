using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public sealed class GOAPAgent : MonoBehaviour
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
        Action action = null;
        if (currentActions.Count > 0)
        {
            action = currentActions.Peek();
        }

        switch (state)
        {
            case AgentState.Idle:
                var worldData = GetComponent<IGoap>().GetWorldData();
                var goal = GetComponent<IGoap>().CreateGoals();

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
                else
                {
                    agent.SetDestination(action.target.transform.position);
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
