using System.Linq;
using Photon.Pun;

namespace Queuetilla
{
    public class CustomQueueManager : MonoBehaviourPunCallbacks
    {
        public override void OnJoinedRoom()
        {
            Queuetilla.currentQueue = Queuetilla.queues.ElementAt(Queuetilla.currentQueueIndex);
            Queuetilla.CallQueueJoinEvent(Queuetilla.currentQueue);
        }

        public override void OnLeftRoom()
        {
            Queuetilla.CallQueueLeaveEvent(Queuetilla.currentQueue!);
            Queuetilla.currentQueue = null;
        }
    }
}