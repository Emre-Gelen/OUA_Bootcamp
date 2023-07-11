using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyHolder : MonoBehaviour
{

    [SerializeField] public List<KeyItem> KeyItems;

    public void GiveKey(KeyItem keyItem)
    {
        KeyItems.Add(keyItem);
    }

    public int CompareKeys(IEnumerable<KeyItem> keyItems)
    {
        int matchedKeys = 0;

        foreach (KeyItem playerKey in KeyItems)
        {
            foreach (KeyItem requiredKey in keyItems)
            {
                if (playerKey == requiredKey)
                {
                    matchedKeys++;
                }
            }
        }

        return matchedKeys;
    }

    public void RemoveKeys(IEnumerable<KeyItem> keyItems)
    {
        foreach (KeyItem key in keyItems)
        {
            KeyItems.Remove(key);
        }
    }
}

public enum KeyItem
{
    Red = 1,
    Green = 2,
    Blue = 3,
    Yellow = 4
}