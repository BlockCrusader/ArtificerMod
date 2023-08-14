using Terraria.ModLoader;

namespace ArtificerMod
{
	public class KeybindSystem : ModSystem
	{
		public static ModKeybind ActivatedAbility { get; private set; }
		public static ModKeybind AbilityExtraHotkey { get; private set; }

		public override void Load()
		{
			ActivatedAbility = KeybindLoader.RegisterKeybind(Mod, "Activate Accessory Abilities", "C");
			AbilityExtraHotkey = KeybindLoader.RegisterKeybind(Mod, "Accessory Abilities (Extra hotkey)", "X");
		}

		public override void Unload()
		{
			ActivatedAbility = null;
			AbilityExtraHotkey = null;
		}
	}
}