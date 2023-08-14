using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ArtificerMod.Common
{
	public class ConfigClient : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[DefaultValue(true)]
		public bool xenoHaloShields;

		[DefaultValue(false)]
		public bool cooldownAwareness;
	}

	public class ConfigServer : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[DrawTicks]
		[OptionStrings(new string[] { "Drop Bag", "No Drops", "No Bag"})]
		[DefaultValue("Drop Bag")]
        [ReloadRequired]
		public string CultistExpertDrop;

		[DefaultValue(false)]
		public bool GodmodeCooldown;

		[DefaultValue(true)]
		public bool PresentDeathText;

		[DefaultValue(false)]
		public bool BonusArtificerSlot;
	}
}
