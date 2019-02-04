using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void SkullsCollectedHandler();
    public static event SkullsCollectedHandler OnSkullsCollected;
    public static void SkullsCollected()
    {
        OnSkullsCollected?.Invoke();
    }
}
