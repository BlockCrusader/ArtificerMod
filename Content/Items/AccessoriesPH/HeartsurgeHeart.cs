using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	public class HeartsurgeHeart : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 0;
			ItemID.Sets.ItemIconPulse[Item.type] = true; 
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 12;
			Item.rare = ItemRarityID.White;
			Item.value = Item.buyPrice(0, 0, 0, 0);
			Item.noGrabDelay = 60;
		}

		private int lifeTime;
        public override void Update(ref float gravity, ref float maxFallSpeed) // Kills the item after it spends 6 seconds alive
        {
			lifeTime++;
			if(lifeTime > 360)
            {
				for (int i = 0; i < 5; i++)
				{
					Dust dust = Dust.NewDustDirect(Item.Hitbox.TopLeft(), Item.Hitbox.Width, Item.Hitbox.Height, DustID.SomethingRed, Scale: Main.rand.NextFloat(0.75f, 1.25f));
					dust.noLight = true;
					dust.alpha = 120;
				}
				Item.active = false;
				Item.TurnToAir();
            }
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
			grabRange = 202; // Manually set base range to ~12.6 tiles
			ArtificerPlayer artificer = player.GetModPlayer<ArtificerPlayer>();
			if(!artificer.superMagnet && !player.treasureMagnet && player.lifeMagnet) // Just Heartreach (+10 tiles)
			{
				grabRange += 160;
            }
			else if(artificer.superMagnet && !player.treasureMagnet && !player.lifeMagnet) // Just Super Magnet (+10 tiles)
			{
				grabRange += 160;
			}
			else if(!artificer.superMagnet && player.treasureMagnet && !player.lifeMagnet) // Just Treasure Magnet (+5 tiles)
            {
				grabRange += 80;
			}
			else if(artificer.superMagnet && !player.treasureMagnet && player.lifeMagnet) // Heartreach + Super (+15 tiles)
			{
				grabRange += 240;
			}
			else if(artificer.superMagnet && player.treasureMagnet && !player.lifeMagnet) // Super + Treasure (+15 tiles)
			{
				grabRange += 240;
			}
			else if(!artificer.superMagnet && player.treasureMagnet && player.lifeMagnet) // Heartreach + Treasure (+15 tiles)
			{
				grabRange += 240;
			}
			else if(artificer.superMagnet && player.treasureMagnet && player.lifeMagnet) // All 3 (+20 tiles)
			{
				grabRange += 320;
			}
		}

        public override bool OnPickup(Player player)
        {
			player.Heal(5);
            return false;
        }
	}
}
