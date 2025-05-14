using HarmonyLib;
using InventorySystem;
using InventorySystem.Items;
using Exiled.API.Features.Items;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.Usables.Scp1344;

namespace ProjectSCRAMBLE.Patchs
{
    [HarmonyPatch(typeof(ItemBase), nameof(ItemBase.ServerDropItem), typeof(bool))]
    public static class BlockForceDrop
    {
        public static void Postfix(ItemBase __instance, bool spawn, ref ItemPickupBase __result)
        {
            if (__result == null) 
                return;

            if (!Plugin.Instance.Config.ProjectSCRAMBLE.CanWearOff)
                return;

            if (!ProjectSCRAMBLE.SCRAMBLE.TrackedSerials.Contains(__result.Info.Serial))
                return;

            if (!Item.Get(__instance).Is<Scp1344>(out Scp1344 x))
                return;

            if (x.Status != Scp1344Status.Deactivating)
                return;

            __result.PreviousOwner.Hub.inventory.ServerAddItem(__result.ItemId.TypeId, ItemAddReason.Undefined, __result.Info.Serial, __result);
            __result.DestroySelf();
        }
    }
}
