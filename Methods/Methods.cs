using MEC;
using UnityEngine;
using System.Collections.Generic;

namespace ProjectSCRAMBLE
{
    public class Methods
    {
        internal static IEnumerator<float> TrackHead(Transform censor, Transform head)
        {
            float syncInterval = Plugin.Instance.Config.AttachToHeadsyncInterval;
            while (censor != null && head != null)
            {
                censor.position = head.position;
                censor.rotation = head.rotation;
                yield return Timing.WaitForSeconds(syncInterval);
            }
        }
    }
}
