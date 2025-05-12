using Exiled.API.Interfaces;

namespace ProjectSCRAMBLE
{
    public class Translation : ITranslation
    {
        public string Charge { get; set; } = "<color=green>Project SCRAMBLE ACTIVE charge status: {charge}</color>";
        public string OffCharge { get; set; } = "<color=red>SCRAMBLE = ?? !WARNING!! CRITICIAL ERROR</color>";
    }
}
