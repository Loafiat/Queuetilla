# Queuetilla
A simple implementation of custom queues.
## Example Queue Mod:
```cs
using BepInEx;
using Queuetilla;
using UnityEngine;

[CustomQueue("ExampleQueue")]
[BepInPlugin("Lofiat.ExampleQueue", "ExampleQueue", "1.0.0")]
public class ExampleQueue : BaseUnityPlugin
{
    public void Awake()
    {
        Queuetilla.Queuetilla.OnQueueJoinEvent += OnJoinQueue;
        Queuetilla.Queuetilla.OnQueueLeaveEvent += OnLeaveQueue;
    }

    public void OnJoinQueue(string Queue)
    {
        if (Queue == "ExampleQueue")
            Debug.Log("Join Queue");
    }

    public void OnLeaveQueue(string Queue)
    {
        if (Queue == "ExampleQueue")
            Debug.Log("Leave Queue");
    }
}
```
