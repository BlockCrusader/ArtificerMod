using ArtificerMod.Common;
using ArtificerMod.Content.Items.AbilityAccH;
using ArtificerMod.Content.Items.AbilityAccPH;
using ArtificerMod.Content.Items.AccessoriesH;
using ArtificerMod.Content.Items.ArmorH.Tarnished;
using ArtificerMod.Content.Items.Others;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod
{
	// These custom drop conditions are used by the Lunatic Cultist, so he only yields his boss bag and/or loot based on config settings
	public class CultistBagCheck : IItemDropRuleCondition, IProvideItemConditionDescription
	{
		public bool CanDrop(DropAttemptInfo info) => (ModContent.GetInstance<ConfigServer>().CultistExpertDrop == "Drop Bag" && Main.expertMode);
		public bool CanShowItemDropInUI() => (ModContent.GetInstance<ConfigServer>().CultistExpertDrop == "Drop Bag" && Main.expertMode);
		public string GetConditionDescription() => null;
	}
	public class CultistDropsCheck : IItemDropRuleCondition, IProvideItemConditionDescription
	{
		public bool CanDrop(DropAttemptInfo info) => (ModContent.GetInstance<ConfigServer>().CultistExpertDrop != "No Drops");
		public bool CanShowItemDropInUI() => (ModContent.GetInstance<ConfigServer>().CultistExpertDrop != "No Drops");
		public string GetConditionDescription() => null;
	}

	public class ArtificerLoot : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
			LeadingConditionRule isExpertRule = new LeadingConditionRule(new Conditions.IsExpert());
			LeadingConditionRule culstistDropsBag = new LeadingConditionRule(new CultistBagCheck());
			LeadingConditionRule isHardmode = new LeadingConditionRule(new Conditions.IsHardmode());
			Conditions.FrostMoonDropGatingChance frostMoonRNG = new Conditions.FrostMoonDropGatingChance();
			Conditions.PumpkinMoonDropGatingChance spookyMoonRNG = new Conditions.PumpkinMoonDropGatingChance();

			if (npc.type == NPCID.AngryBones || npc.type == NPCID.AngryBonesBig || npc.type == NPCID.AngryBonesBigHelmet 
				|| npc.type == NPCID.AngryBonesBigMuscle || npc.type == NPCID.DarkCaster)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Harvester>(), 250, 150));
			}

			if(npc.type == NPCID.MoonLordCore)
            {
				notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(2,
					ModContent.ItemType<MoonbiteMark>(), ModContent.ItemType<StarlightArmillary>(), ModContent.ItemType<Moonfall>()));
				npcLoot.Add(notExpertRule);
			}

			if (npc.type == NPCID.LunarTowerNebula || npc.type == NPCID.LunarTowerSolar || npc.type == NPCID.LunarTowerStardust || npc.type == NPCID.LunarTowerVortex)
			{
				int itemType = ModContent.ItemType<NovaFragment>();
				var normalParams = new DropOneByOne.Parameters()
				{
					ChanceNumerator = 1,
					ChanceDenominator = 1,
					MinimumStackPerChunkBase = 2,
					MaximumStackPerChunkBase = 4,
					MinimumItemDropsCount = 3,
					MaximumItemDropsCount = 4,
				};
				var expertParams = new DropOneByOne.Parameters()
				{
					ChanceNumerator = 1,
					ChanceDenominator = 1,
					MinimumStackPerChunkBase = 3,
					MaximumStackPerChunkBase = 5,
					MinimumItemDropsCount = 3,
					MaximumItemDropsCount = 5,
				};
				notExpertRule.OnSuccess(new DropOneByOne(itemType, normalParams));
				isExpertRule.OnSuccess(new DropOneByOne(itemType, expertParams));

				npcLoot.Add(notExpertRule);
				npcLoot.Add(isExpertRule);
			}

			if (npc.type == NPCID.MartianSaucerCore)
			{
				int itemType = ModContent.ItemType<MartianScrap>();
				var normalParams = new DropOneByOne.Parameters()
				{
					ChanceNumerator = 1,
					ChanceDenominator = 1,
					MinimumStackPerChunkBase = 1,
					MaximumStackPerChunkBase = 2,
					MinimumItemDropsCount = 5,
					MaximumItemDropsCount = 5,
				};
				var expertParams = new DropOneByOne.Parameters()
				{
					ChanceNumerator = 1,
					ChanceDenominator = 1,
					MinimumStackPerChunkBase = 2,
					MaximumStackPerChunkBase = 3,
					MinimumItemDropsCount = 4,
					MaximumItemDropsCount = 4,
				};
				notExpertRule.OnSuccess(new DropOneByOne(itemType, normalParams));
				isExpertRule.OnSuccess(new DropOneByOne(itemType, expertParams));

				npcLoot.Add(notExpertRule);
				npcLoot.Add(isExpertRule);

				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ObliterationCore>(), 7, 5));
			}

			if (npc.type == NPCID.PossessedArmor)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PossessedHelmet>(), 100, 50));
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PossessedBreastplate>(), 100, 50));
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PossessedLeggings>(), 100, 50));
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BewitchedBulwark>(), 100, 50));
			}

			if (npc.type == NPCID.WallofFlesh)
			{
				notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ArtificerEmblem>(), 5));
				npcLoot.Add(notExpertRule);
			}

			// Makes the Lunatic Cultist drop his normally unobtainable treasure bag, which has an accessory added by this mod
			if (npc.type == NPCID.CultistBoss)
			{
				culstistDropsBag.OnSuccess(ItemDropRule.BossBag(ItemID.CultistBossBag));
				npcLoot.Add(culstistDropsBag);
			}

			if (npc.type == NPCID.Cyborg)
            {
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CyborgCore>(), 2));
			}

			if(npc.type == NPCID.IlluminantBat || npc.type == NPCID.IlluminantSlime || npc.type == NPCID.EnchantedSword || npc.type == NPCID.PigronHallow || npc.type == NPCID.DesertGhoulHallow)
            {
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ChaosGem>(), 350, 250));
            }
			if(npc.type == NPCID.ChaosElemental || npc.type == NPCID.BigMimicHallow)
            {
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ChaosGem>(), 75, 50));
			}

			if (npc.type == NPCID.GoblinSummoner)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadowflameScroll>(), 10));
			}
			if (npc.type == NPCID.GoblinSorcerer)
			{
				isHardmode.OnSuccess(ItemDropRule.NormalvsExpert(ModContent.ItemType<ShadowflameScroll>(), 100, 75));
				npcLoot.Add(isHardmode);
			}

			if (npc.type == NPCID.Mothron)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<LostHeroShield>(), 10, 8));
			}

			if (npc.type == NPCID.IceQueen)
            {
				npcLoot.Add(ItemDropRule.ByCondition(frostMoonRNG, ModContent.ItemType<PermafrostCloak>(), 3));
			}

			if (npc.type == NPCID.SantaNK1)
			{
				npcLoot.Add(ItemDropRule.ByCondition(frostMoonRNG, ModContent.ItemType<MissileArray>(), 3));
			}

			if (npc.type == NPCID.Pumpking)
			{
				npcLoot.Add(ItemDropRule.ByCondition(spookyMoonRNG, ModContent.ItemType<FearReaper>(), 4));
			}

			if (npc.type == NPCID.Everscream || npc.type == NPCID.SantaNK1 || npc.type == NPCID.IceQueen)
			{
				npcLoot.Add(ItemDropRule.ByCondition(spookyMoonRNG, ModContent.ItemType<LuckyPresent>(), 25));
			}
			if (npc.type == NPCID.PresentMimic)
			{
				npcLoot.Add(ItemDropRule.ByCondition(spookyMoonRNG, ModContent.ItemType<LuckyPresent>(), 50));
			}
			if (npc.type == NPCID.GingerbreadMan || npc.type == NPCID.ElfArcher || npc.type == NPCID.ElfCopter
			 || npc.type == NPCID.ZombieElf || npc.type == NPCID.ZombieElfBeard || npc.type == NPCID.ZombieElfGirl
			 || npc.type == NPCID.Flocko || npc.type == NPCID.Yeti || npc.type == NPCID.Nutcracker || npc.type == NPCID.Krampus)
			{
				npcLoot.Add(ItemDropRule.ByCondition(spookyMoonRNG, ModContent.ItemType<LuckyPresent>(), 200));
			}

			if (npc.type == NPCID.MourningWood || npc.type == NPCID.Pumpking)
			{
				npcLoot.Add(ItemDropRule.ByCondition(spookyMoonRNG, ModContent.ItemType<ForebodingCharm>(), 25));
			}
			if(npc.type == NPCID.HeadlessHorseman)
            {
				npcLoot.Add(ItemDropRule.ByCondition(spookyMoonRNG, ModContent.ItemType<ForebodingCharm>(), 50));
			}
			if (npc.type == NPCID.Splinterling || npc.type == NPCID.Hellhound || npc.type == NPCID.Poltergeist
			 || (npc.type >= NPCID.Scarecrow1 && npc.type <= NPCID.Scarecrow10))
			{
				npcLoot.Add(ItemDropRule.ByCondition(spookyMoonRNG, ModContent.ItemType<ForebodingCharm>(), 200));
			}
		}
	}

	public class ArtificerBags : GlobalItem
	{
		public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
		{
			LeadingConditionRule culstistDropsLoot = new LeadingConditionRule(new CultistDropsCheck());

			if (item.type == ItemID.WallOfFleshBossBag)
			{ 
				itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArtificerEmblem>(), 5));
			}

			if (item.type == ItemID.MoonLordBossBag)
			{
				itemLoot.Add(ItemDropRule.OneFromOptions(2,
					ModContent.ItemType<MoonbiteMark>(), ModContent.ItemType<StarlightArmillary>(), ModContent.ItemType<Moonfall>()));
			}

			if (item.type == ItemID.CultistBossBag)
			{
				culstistDropsLoot.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DarkmoonPact>(), 1));
				itemLoot.Add(culstistDropsLoot);
			}
		}
	}
}