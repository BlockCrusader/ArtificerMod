using ArtificerMod.Content.Items.AccessoriesH;
using ArtificerMod.Content.Items.AccessoriesPH;
using ArtificerMod.Content.Items.ArmorH;
using ArtificerMod.Content.Items.ArmorPH;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Common
{
	class ArsenalShop : GlobalNPC
	{
		public override void ModifyShop(NPCShop shop)
		{
			if (shop.NpcType == NPCID.Princess)
			{
				// Though the Princess is normally post-plantera, this armor is sold post-mechs if the Princess is attained earlier (Ex: Celebration MK10)
                shop.Add<ChampionsHeadpiece>(Condition.DownedMechBossAll);
                shop.Add<ChampionsCuirass>(Condition.DownedMechBossAll);
                shop.Add<ChampionsBoots>(Condition.DownedMechBossAll);
            }
			else if(shop.NpcType == NPCID.GoblinTinkerer)
            {
				shop.Add<TechHeadgear>(Condition.NpcIsPresent(NPCID.Mechanic));
				shop.Add<TechBreastplate>(Condition.NpcIsPresent(NPCID.Mechanic));
				shop.Add<TechLeggings>(Condition.NpcIsPresent(NPCID.Mechanic));
            }
			else if(shop.NpcType == NPCID.Mechanic)
            {
				shop.Add<EnergyCell>();
			}
			else if (shop.NpcType == NPCID.WitchDoctor)
			{
				shop.Add(new Item(ItemID.FrogLeg)
				{
					shopCustomPrice = Item.buyPrice(0, 12, 50)
				}, Condition.Hardmode);
			}
			else if (shop.NpcType == NPCID.PartyGirl)
			{
				var redBalloonCondition = new Condition("Downed late pre-hardmode boss", () => Condition.Hardmode.IsMet() 
				|| Condition.DownedQueenBee.IsMet() || Condition.DownedSkeletron.IsMet() || Condition.DownedDeerclops.IsMet());

				shop.Add(ItemID.ShinyRedBalloon, redBalloonCondition);
			}
			else if (shop.NpcType == NPCID.Wizard)
			{
				shop.Add(ItemID.MagicQuiver, Condition.DownedMechBossAny);
			}
			else if (shop.NpcType == NPCID.Merchant)
			{
				shop.Add(new Item(ItemID.Flipper)
				{
					shopCustomPrice = Item.buyPrice(0, 5)
				}, Condition.InBeach);
			}
			else if (shop.NpcType == NPCID.SkeletonMerchant)
			{
				shop.Add(new Item(ItemID.CobaltShield)
				{
					shopCustomPrice = Item.buyPrice(0, 10)
				}, Condition.Hardmode);
				shop.Add<LuckyStar>(Condition.LanternNight);
			}
			else if (shop.NpcType == NPCID.Steampunker)
			{
				shop.Add<ClockworkStopwatch>(Condition.DownedMechBossAll);
			}
		}

        public override void SetupTravelShop(int[] shop, ref int nextSlot)
        {
			if(Main.rand.NextBool(15)) 
            {
				shop[nextSlot++] = ModContent.ItemType<AceOfHearts>();
			}
			if (Main.rand.NextBool(35)) 
			{
				shop[nextSlot++] = ModContent.ItemType<DeftPouch>();
			}
            if (TrySellLuckStar())
            {
				shop[nextSlot++] = ModContent.ItemType<LuckyStar>();
			}
		}

		/// <summary>
		/// Used to determine if the Travelling Merchant should seel the Lucky Star. It is unique in that it becomes more common as (pre-hardmode) bosses are defeated.
		/// </summary>
		private bool TrySellLuckStar()
        {
			int denom = 100;
			if (Condition.DownedKingSlime.IsMet())
			{
				denom -= 10;
			}
			if (Condition.DownedEyeOfCthulhu.IsMet())
            {
				denom -= 10;
			}
			if (Condition.DownedEowOrBoc.IsMet())
			{
				denom -= 10;
			}
			if (Condition.DownedQueenBee.IsMet())
			{
				denom -= 10;
			}
			if (Condition.DownedSkeletron.IsMet())
			{
				denom -= 10;
			}
			if (Condition.DownedDeerclops.IsMet())
			{
				denom -= 10;
			}
			if (Condition.Hardmode.IsMet()) // WoF
			{
				denom -= 25;
			}
			return Main.rand.NextBool(denom);
        }
    }
}
