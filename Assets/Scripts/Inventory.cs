using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private readonly Dictionary<string, int> items = new Dictionary<string, int>();

    public void Add(string _id, int _amount)
    {
        items[_id] += _amount;
    }

    public void Remove(string _id, int _amount)
    {
        if (items[_id] > _amount)
        {
            items[_id] -= _amount;
        }
        else
        {
            items.Remove(_id);
        }
    }

    public int GetAmount(string _id)
    {
        return items[_id];
    }
}
