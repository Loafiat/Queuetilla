using System.Linq;
using Photon.Pun;

namespace Queuetilla
{
    public class CustomQueueManager : MonoBehaviourPunCallbacks
    {
        public override void OnJoinedRoom()
        {
            Queuetilla.currentQueue = Queuetilla.queues.ElementAt(Queuetilla.currentQueueIndex);
            try
            {
                Queuetilla.CallQueueJoinEvent(Queuetilla.currentQueue);
            }
            catch
            {
                //ignore because photon stoopid
            }
        }

        public override void OnLeftRoom()
        {
            try
            {
                Queuetilla.CallQueueLeaveEvent(Queuetilla.currentQueue!);
            }
            catch
            {
                //ignore because photon stoopid
            }
            Queuetilla.currentQueue = null;
        }
    }
}