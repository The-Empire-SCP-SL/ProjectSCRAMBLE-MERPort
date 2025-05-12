using UnityEngine;
using Exiled.API.Interfaces;
using System.ComponentModel;

namespace ProjectSCRAMBLE
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Whether to remove the main 1344 effect when using SCRAMBLES")]
        public bool RemoveOrginal1344Effect { get; set; } = true;

        [Description("Whether the SCRAMBLES will use charge while blocking SCP-096 face")]
        public bool ScrambleCharge { get; set; } = true;
        public float ChargeUsageMultiplayer { get; set; } = 1;

        [Description("Attach to head or Directl attach to player")]
        public bool AttachCensorToHead { get; set; } = true;
        public float AttachToHeadsyncInterval { get; set; } = 0.001f;

        [Description("Censor Schematic settings")]
        public string CensorSchematic { get; set; } = "Censormain";
        public Vector3 CensorSchematicScale { get; set; } = Vector3.one;

        [Description("Custom item settings")]
        public ProjectSCRAMBLE ProjectSCRAMBLE { get; set; } = new ProjectSCRAMBLE();

        [Description("Hint settings")]
        public Hints Hint { get; set; } = new Hints();
        public class Hints 
        {
            public float XCordinate { get; set; } = 330;
            public float YCordinate { get; set; } = 120;
            public int FontSize { get; set; } = 20;
            public int Alligment { get; set; } = 0;
        }

    }
}
