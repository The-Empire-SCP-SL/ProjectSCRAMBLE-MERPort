using MEC;
using UnityEngine;
using Exiled.API.Enums;
using Exiled.API.Features;
using ProjectSCRAMBLE.Extensions;
using System.Collections.Generic;
using ProjectMER.Features.Objects;

namespace ProjectSCRAMBLE
{
    public class Methods
    {
        public static string FormatCharge(float charge) => ((int)charge).ToString();

        public static void AttachCensorToHead(SchematicObject Censor , Transform Head)
        {
            foreach (var i in Censor.AdminToyBases)
                i.syncInterval = 0; //maybe not necesary, probably not necesary

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

        public static void RemoveOrginalEffect(Player player) 
        {
            Timing.CallDelayed(1f, () => 
            {
                if (player == null || player.IsDead)
                    return;

                player.SendFakeEffect(EffectType.Scp1344, 0); 

                if (Plugin.Instance.Config.SimulateTemporaryDarkness)
                    player.EnableEffect(EffectType.Blinded, 255, float.MaxValue, true);
            });
        }
    }
}
