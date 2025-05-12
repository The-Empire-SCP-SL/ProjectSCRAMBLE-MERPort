using System;
using Exiled.API.Features;
using Exiled.CustomItems.API;

namespace ProjectSCRAMBLE
{
    public class Plugin : Plugin<Config, Translation>
    {
        public static EventHandlers eventHandlers;

        public override string Author => "ZurnaSever";

        public override string Name => "ProjectSCRAMBLE";

        public override string Prefix => "ProjectSCRAMBLE";

        public static Plugin Instance { get; private set; }

        public override Version Version { get; } = new Version(1, 0, 0);

        public override Version RequiredExiledVersion { get; } = new Version(9, 6, 0);

        public override void OnEnabled()
        {
            Instance = this;
            eventHandlers = new EventHandlers();
            Config.ProjectSCRAMBLE.Register();

            eventHandlers.Sucsribe();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            eventHandlers.UnSucsribe();
            Config.ProjectSCRAMBLE.Unregister();

            eventHandlers = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}
