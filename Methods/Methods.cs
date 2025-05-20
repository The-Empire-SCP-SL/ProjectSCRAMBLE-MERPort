using MEC;
using UnityEngine;
using Exiled.API.Features;
using ProjectSCRAMBLE.Extensions;
using System.Collections.Generic;
using ProjectMER.Features.Objects;

namespace ProjectSCRAMBLE
{
    public class Methods
    {
        public static void AttachCensorToHead(SchematicObject Censor , Transform Head)
        {
            foreach (var i in Censor.AdminToyBases)
                i.syncInterval = 0;

            Timing.RunCoroutine(TrackHead(Censor.transform, Head));
        }

        private static IEnumerator<float> TrackHead(Transform censor, Transform head)
        {
            float syncInterval = Plugin.Instance.Config.AttachToHeadsyncInterval;
            while (censor != null && head != null)
            {
                censor.position = head.position;
                censor.rotation = head.rotation;
                yield return Timing.WaitForSeconds(syncInterval);
            }
        }

        public static void RemoveForUnGlassesPlayer(SchematicObject Schematic, Player SchematicOwner)
        {
            foreach (Player normalply in Player.List)
            {
                if (ProjectSCRAMBLE.ActiveScramblePlayers.ContainsKey(normalply)) 
                {
                    ProjectSCRAMBLE.ActiveScramblePlayers[normalply].Add(SchematicOwner);
                }
                else
                {
                    normalply.DestroySchematic(Schematic);
                }
            }
        }

        
    }
}
