using System;
using System.Collections.Generic;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace Queuetilla
{
    [BepInPlugin("Lofiat.Queuetilla", "Queuetilla", "1.0.1")]
    public class Queuetilla : BaseUnityPlugin
    {
        public static event Action<string> OnQueueJoinEvent;
        public static event Action<string> OnQueueLeaveEvent;
        //Comp is added in patches if player has allowedInCompetitive
        internal static List<string> queues = new List<string> { "DEFAULT", "MINIGAMES" };
        //this should NOT under ANY CIRCUMSTANCES be changed it will break any queue you're in.
        internal static string? currentQueue;
        
        /// <summary>
        /// The internal queue index, does nothing when changed until you interact with the queue page.
        /// </summary>
        public static int currentQueueIndex = 0;
        
        /// <summary>
        /// The queue of the room you're in.
        /// </summary>
        public static string? CurrentQueue {get => currentQueue;}
        
        private void Awake()
        {
            new Harmony("Lofiat.Queuetilla").PatchAll(Assembly.GetExecutingAssembly());
            GorillaTagger.OnPlayerSpawned(delegate() {new GameObject().AddComponent<CustomQueueManager>();});
        }
        
        /// <summary>
        /// DO NOT CALL THIS EXTERNALLY!!!
        /// </summary>
        internal static void CallQueueJoinEvent(string queueName)
        {
            OnQueueJoinEvent(queueName);
        }

        /// <summary>
        /// DO NOT CALL THIS EXTERNALLY!!!
        /// </summary>
        internal static void CallQueueLeaveEvent(string queueName)
        {
            OnQueueLeaveEvent(queueName);
        }
    }
}
