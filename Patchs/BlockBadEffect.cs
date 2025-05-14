using HarmonyLib;
using InventorySystem.Items.Usables.Scp1344;

namespace ProjectSCRAMBLE.Patchs
{
    [HarmonyPatch(typeof(Scp1344Item), nameof(Scp1344Item.ActivateFinalEffects))]
    public class BlockBadEffect
    {
        public static bool Prefix(Scp1344Item __instance)
        {
            if (!Plugin.Instance.Config.ProjectSCRAMBLE.CanWearOff)
                return true;

            if (ProjectSCRAMBLE.SCRAMBLE.TrackedSerials.Contains(__instance.ItemSerial))
            {
                __instance.Scp1344Effect.IsEnabled = false;
                __instance.BlindnessEffect.IsEnabled = false;
                return false;
            }

            return true;
        }
    }
}
