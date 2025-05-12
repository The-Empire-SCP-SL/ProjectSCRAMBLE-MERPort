using Exiled.API.Enums;
using CustomPlayerEffects;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using ProjectSCRAMBLE.Extensions;
using System.Collections.Generic;
using Exiled.API.Features.Attributes;
using Exiled.Events.EventArgs.Scp1344;
using Exiled.CustomItems.API.Features;
using InventorySystem.Items.Usables.Scp1344;
using Scp1344event = Exiled.Events.Handlers.Scp1344;
using PlayerRoles.FirstPersonControl.Thirdperson.Subcontrollers.Wearables;

namespace ProjectSCRAMBLE
{
    [CustomItem(ItemType.SCP1344)]
    public class ProjectSCRAMBLE : CustomItem
    {
        public static Dictionary<ushort, float> ScrambleCharges = [];

        public static Dictionary<Player, List<Player>> ActiveScramblePlayers = [];

        public static ProjectSCRAMBLE SCRAMBLE;

        public bool CanWearOff { get; set; } = true;

        public override uint Id { get; set; } = 1730;

        public override float Weight { get; set; } = 1f;

        public override string Name { get; set; } = "Project SCRAMBLE";

        public override ItemType Type { get; set; } = ItemType.SCP1344;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties();

        public override string Description { get; set; } = "An artificial intelligence Visor that censors SCP-096's face";

        public override void Init()
        {
            base.Init();
            SCRAMBLE = this;
        }

        public override void Destroy()
        {
            base.Destroy();
            SCRAMBLE = null;
        }

        protected override void SubscribeEvents()
        {
            Scp1344event.ChangedStatus += OnChangedStatus;
            Scp1344event.ChangingStatus += OnChangingStatus;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Scp1344event.ChangedStatus -= OnChangedStatus;
            Scp1344event.ChangingStatus -= OnChangingStatus;

            base.UnsubscribeEvents();
        }

        private void OnChangedStatus(ChangedStatusEventArgs ev)
        {
            if (!Check(ev.Item))
                return;

            if (ev.Scp1344Status != Scp1344Status.Active)
                return;

            if (Plugin.Instance.Config.ScrambleCharge)
            {
                ushort serial = ev.Item.Serial;

                if (!ScrambleCharges.TryGetValue(serial, out float charge))
                {
                    charge = 100f;
                    ScrambleCharges[serial] = charge;
                    Log.Debug($"Initialized SCRAMBLE charge for serial {serial}.");
                }

                else if (charge <= 0f)
                {
                    if (Plugin.Instance.Config.RemoveOrginal1344Effect) 
                        Methods.RemoveOrginalEffect(ev.Player);

                    ev.Player.DeObfuscateScp96s();
                    ev.Player.AddSCRAMBLEHint(Plugin.Instance.Translation.OffCharge);
                    Log.Debug($"{ev.Player.Nickname}: Tried to wear SCRAMBLE with no charge.");
                    return;
                }

                string hint = Plugin.Instance.Translation.Charge.Replace("{charge}", Methods.FormatCharge(charge));
                ev.Player.AddSCRAMBLEHint(hint);
                Log.Debug($"{ev.Player.Nickname}: SCRAMBLEs charge {charge}.");
            }

            if (Plugin.Instance.Config.RemoveOrginal1344Effect)
                Methods.RemoveOrginalEffect(ev.Player);  

            ActiveScramblePlayers[ev.Player] = [];
            ev.Player.ObfuscateScp96s();
            Log.Debug($"{ev.Player.Nickname}: Activated Project SCRAMBLE");
        }

        private void OnChangingStatus(ChangingStatusEventArgs ev)
        {
            if (!Check(ev.Item))
                return;

            if (!CanWearOff || ev.Scp1344StatusNew != Scp1344Status.Deactivating)
                return;

            ev.IsAllowed = false;

            ev.Scp1344.Base.Owner.DisableWearables(WearableElements.Scp1344Goggles);
            ev.Scp1344.Base.Status = Scp1344Status.Idle;
            ev.Scp1344.Base.ServerDropItem(true);

            ev.Player.DisableEffect(EffectType.Scp1344);
            ev.Player.DisableEffect<Blindness>();
            ev.Player.RemoveSCRAMBLEHint();

            Log.Debug($"{ev.Player.Nickname} Deactivated Project SCRAMBLE");
        }
    }
}
