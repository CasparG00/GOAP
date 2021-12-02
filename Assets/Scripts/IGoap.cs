using System.Collections.Generic;

public interface IGoap
{
    public Dictionary<string, object> GetWorldData();
    public Dictionary<string, object> CreateGoals();
}