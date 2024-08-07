using System.Linq;
using GorillaNetworking;
using HarmonyLib;
using UnityEngine;

namespace Queuetilla
{
    [HarmonyPatch(typeof(GorillaComputer))]
    public class HarmonyPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch("InitializeQueueState")]
        static void InitializeQueueState(GorillaComputer __instance)
        {
            //This adds comp to the queues if you are allowed
            if (__instance.allowedInCompetitive)
            {
                Queuetilla.queues.Insert(2, "COMPETITIVE");
            }
            Queuetilla.currentQueueIndex = Queuetilla.queues.IndexOf(__instance.currentQueue);
        }
        
        [HarmonyPrefix]
        [HarmonyPatch("QueueScreen")]
        static bool QueueScreen(GorillaComputer __instance)
        {
            //This adds comp to the queues if allowedInCompetitive is true, and you don't already have comp in queue.
            if (__instance.allowedInCompetitive && !Queuetilla.queues.Contains("COMPETITIVE"))
            {
                Queuetilla.queues.Insert(2, "COMPETITIVE");
            }
            
            if (!__instance.allowedInCompetitive)
            {
                __instance.screenText.Text = "THIS OPTION AFFECTS WHO YOU PLAY WITH. PRESS A AND D TO CHANGE QUEUE.\nNOTE: YOU CANNOT CURRENTLY ACCESS COMPETITIVE.\nCUSTOM QUEUES CAN ADD A VARIETY OF DIFFERENT GAMEPLAY OPTIONS, AND EVEN ACT AS CUSTOM GAMEMODES!\n\nCURRENT QUEUE: " + __instance.currentQueue;
                return false;
            }
            __instance.screenText.Text = "THIS OPTION AFFECTS WHO YOU PLAY WITH. PRESS A AND D TO CHANGE QUEUE.\nCUSTOM QUEUES CAN ADD A VARIETY OF DIFFERENT GAMEPLAY OPTIONS, AND EVEN ACT AS CUSTOM GAMEMODES!\n\nCURRENT QUEUE: " + __instance.currentQueue;
            return false;
        }
        
        [HarmonyPrefix]
        [HarmonyPatch("ProcessQueueState")]
        static bool ProcessQueueState(GorillaComputer __instance, GorillaKeyboardBindings buttonPressed)
        {
            switch (buttonPressed)
            {
                case GorillaKeyboardBindings.A:
                    if (Queuetilla.currentQueueIndex == 0)
                        return false;
                    Queuetilla.currentQueueIndex -= 1;
                    __instance.currentQueue = Queuetilla.queues.ElementAt(Queuetilla.currentQueueIndex);
                    PlayerPrefs.SetString("currentQueue", __instance.currentQueue);
                    PlayerPrefs.Save();
                    break;
                
                case GorillaKeyboardBindings.D:
                    if (Queuetilla.currentQueueIndex == Queuetilla.queues.Count - 1)
                        return false;
                    Queuetilla.currentQueueIndex += 1;
                    __instance.currentQueue = Queuetilla.queues.ElementAt(Queuetilla.currentQueueIndex);
                    PlayerPrefs.SetString("currentQueue", __instance.currentQueue);
                    PlayerPrefs.Save();
                    break;
            }
            return false;
        }
    }
}