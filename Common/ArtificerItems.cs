using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using ArtificerMod.Content.Items.AbilityAccH;
using ArtificerMod.Content.Items.AbilityAccPH;
using System;
using Terraria.Audio;
using System.Linq;
using Terraria.Localization;
using Humanizer;

namespace ArtificerMod.Common
{
    public class AbilityAccessories : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return FetchCooldown(entity) != -1f; // This logic determines if the accessory has an ability
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.GetModPlayer<ArtificerPlayer>().abilityAcc = true;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            bool equippedAbility = FetchCooldown(equippedItem) != -1f;
            bool incomingAbility = FetchCooldown(incomingItem) != -1f;
            if (equippedAbility) // If the equipped item has an ability, the incoming item may only co-exsit if it doesn't have one
            {
                return !incomingAbility;
            }
            return true;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Player localPlayer = Main.LocalPlayer;
            if(!localPlayer.TryGetModPlayer(out ArtificerPlayer localArtificer) || item.social)
            {
                return;
            }

            float baseCooldown = FetchCooldown(item);
            float netCoolMods = GetCoolMods(localArtificer);
            float finalCooldown = (float)Math.Round(baseCooldown * netCoolMods, 2);

            bool positiveMods = netCoolMods < 1f;
            bool negativeMods = netCoolMods > 1f;

            bool noArmor = localArtificer.noArtificerBonuses;
            bool noAbility = !localArtificer.abilityAcc;
            bool noArmorOrAbility = noArmor && noAbility;
            if (localArtificer.journeyGodmodeBonus)
            {
                noArmor = noAbility = noArmorOrAbility = false;
            }

            string lineName = GetTooltipLineTarget(item);
            TooltipLine target = tooltips.FirstOrDefault(line => line.Mod == "Terraria" && line.Name == lineName);
            if (target.Equals(null) || target.Equals(default(TooltipLine)))
            {
                return;
            }
            int index = tooltips.IndexOf(target);
            if(index == -1)
            {
                return;
            }

            if (KeybindSystem.ActivatedAbility.GetAssignedKeys().Count == 0
               && KeybindSystem.AbilityExtraHotkey.GetAssignedKeys().Count == 0)
            {
                tooltips.Insert(index, new TooltipLine(Mod, "hotkeyTip",
                Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.HotkeySet"))
                { OverrideColor = Color.Red });
            }

            if (noArmor || noAbility)
            {
                if (noArmorOrAbility)
                {
                    tooltips.Insert(index, new TooltipLine(Mod, "penaltyTip",
                    Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.Cooldown.DrasticallyExtended"))
                    { OverrideColor = new Color(190, 120, 120) });
                }
                else if (noAbility)
                {
                    tooltips.Insert(index, new TooltipLine(Mod, "penaltyTip",
                    Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.Cooldown.GreatlyExtended"))
                    { OverrideColor = new Color(190, 120, 120) });
                }
                else
                {
                    tooltips.Insert(index, new TooltipLine(Mod, "penaltyTip",
                    Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.Cooldown.SlightlyExtended"))
                    { OverrideColor = new Color(190, 120, 120) });
                }
            }

            string coolTimeColorHex;
            if (positiveMods)
            {
                coolTimeColorHex = "[c/78be78:";
                tooltips.Insert(index, new TooltipLine(Mod, "cooldownTime",
                Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.Cooldown.CooldowntimeColored").FormatWith(baseCooldown, finalCooldown)));
            }
            else if (negativeMods)
            {
                coolTimeColorHex = "[c/be7878:";
                tooltips.Insert(index, new TooltipLine(Mod, "cooldownTime",
                Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.Cooldown.CooldowntimeColored").FormatWith(baseCooldown, finalCooldown)));
            }
            else
            {
                tooltips.Insert(index, new TooltipLine(Mod, "cooldownTime",
                Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.Cooldown.Cooldowntime").FormatWith(baseCooldown, finalCooldown)));
            }

            if (!HasFlavorText(item)) // Removes placeholder lines
            {
                tooltips.Remove(target);
            }
        }

        /// <summary>
		/// Attempts to get an accessory's ability cooldown time if it has one. Contains hardcoded lists of accessories with abilities, sorted by cooldown.
		/// </summary>
		/// <param name="item">The item whose cooldown should be checked.</param>
        private float FetchCooldown(Item item)
        {
            int type = item.type;
            float returnCool = -1;

            List<int> cooldown10 = new List<int>
            {
                ModContent.ItemType<AugmentedLeg>(),
                ModContent.ItemType<HiddenBlade>(),
                ModContent.ItemType<XRadar>(),
                ModContent.ItemType<Curer>(),
                ModContent.ItemType<Extinguisher>(),
                ModContent.ItemType<Cleanser>(),
                ModContent.ItemType<AnkhRenewer>()
            };
            List<int> cooldown15 = new List<int>
            {
                ModContent.ItemType<ChaosGem>(),
                ModContent.ItemType<TurboBoots>(),
                ModContent.ItemType<RepulsionRocket>(),
                ModContent.ItemType<AssassinNeedles>(),
                ModContent.ItemType<OrnateRing>()
            };
            List<int> cooldown20 = new List<int>
            {
                ModContent.ItemType<NeedleBarrage>(),
                ModContent.ItemType<RepulseCharge>(),
                ModContent.ItemType<OverdriveLeg>(),
                ModContent.ItemType<LostHeroShield>()
            };
            List<int> cooldown25 = new List<int>
            {
                ModContent.ItemType<ArcanaStimulator>(),
                ModContent.ItemType<ArcanaStimulator2>(),
                ModContent.ItemType<ArcanaStimulator3>(),
                ModContent.ItemType<ArcanaStimulator4>(),
                ModContent.ItemType<FlamboyantCape>(),
                ModContent.ItemType<SoulEmulator>()
            };
            List<int> cooldown30 = new List<int>
            {
                ModContent.ItemType<StarplateBlaster>(),
                ModContent.ItemType<SolarBlaster>(),
                ModContent.ItemType<ManaCharger>(),
                ModContent.ItemType<StealthShroud>(),
                ModContent.ItemType<LuckyPresent>(),
                ModContent.ItemType<NeedleSpikeManacle>()
            };
            List<int> cooldown40 = new List<int>
            {
                ModContent.ItemType<Harvester>(),
                ModContent.ItemType<FirstPrism>(),
                ModContent.ItemType<BewitchedBulwark>(),
                ModContent.ItemType<StarlightArmillary>(),
                ModContent.ItemType<MissileArray>()
            };
            List<int> cooldown45 = new List<int>
            {
                ModContent.ItemType<SuperchargedArm>(),
                ModContent.ItemType<ArtificerProsthetics>(),
                ModContent.ItemType<OverchargedArcanum>(),
                ModContent.ItemType<PrismaticArcanum>(),
                ModContent.ItemType<ShadowShroud>(),
                ModContent.ItemType<SwaggerCape>(),
                ModContent.ItemType<FearReaper>(),
                ModContent.ItemType<CyborgCore>()
            };
            List<int> cooldown50 = new List<int>
            {
                ModContent.ItemType<ForebodingCharm>(),
                ModContent.ItemType<ObliterationCore>(),
                ModContent.ItemType<StellarResonator>()
            };
            List<int> cooldown60 = new List<int>
            {
                ModContent.ItemType<VitalStimulator>(),
                ModContent.ItemType<VitalStimulator2>(),
                ModContent.ItemType<VitalStimulator3>(),
                ModContent.ItemType<VitalStimulator4>(),
                ModContent.ItemType<LifeCharger>(),
                ModContent.ItemType<ArtificersArmor>(),
                ModContent.ItemType<PowerArmor>(),
                ModContent.ItemType<RetributionStoker>(),
                ModContent.ItemType<DarkLeech>(),
                ModContent.ItemType<LeechingTether>(),
                ModContent.ItemType<Lifeline>(),
                ModContent.ItemType<ProbePack>()
            };
            List<int> cooldown90 = new List<int>
            {
                ModContent.ItemType<PrimeGauntlet>(),
                ModContent.ItemType<MoonbiteMark>()
            };
            List<int> cooldown120 = new List<int>
            {
                ModContent.ItemType<Starwriter>(),
                ModContent.ItemType<Moonfall>()
            };

            if (cooldown10.Contains(type))
            {
                returnCool = 10;
            }
            else if(cooldown15.Contains(type))
            {
                returnCool = 15;
            }
            else if (cooldown20.Contains(type))
            {
                returnCool = 20;
            }
            else if (cooldown25.Contains(type))
            {
                returnCool = 25;
            }
            else if (cooldown30.Contains(type))
            {
                returnCool = 30;
            }
            else if (cooldown40.Contains(type))
            {
                returnCool = 40;
            }
            else if (cooldown45.Contains(type))
            {
                returnCool = 45;
            }
            else if (cooldown50.Contains(type))
            {
                returnCool = 50;
            }
            else if (cooldown60.Contains(type))
            {
                returnCool = 60;
            }
            else if (cooldown90.Contains(type))
            {
                returnCool = 90;
            }
            else if (cooldown120.Contains(type))
            {
                returnCool = 120;
            }

            return returnCool;
        }

        /// <summary>
		/// Returns the total multiplier on ability cooldown times, based on the given ArtificerPlayer.
		/// </summary>
		/// <param name="artificer">The ArtificerPlayer instance to check.</param>
        private float GetCoolMods(ArtificerPlayer artificer)
        {
            float netMod = 1f;
            if (artificer.journeyGodmodeBonus)
            {
                netMod *= ArtificerPlayer.journeyGodmodeModifier;
            }
            else
            {
                if (artificer.noArtificerBonuses)
                {
                    netMod *= ArtificerPlayer.baseModifier;
                }
                if (!artificer.abilityAcc)
                {
                    netMod *= ArtificerPlayer.noAbilityModifier;
                }
            }

            if (artificer.techSetBonus)
            {
                netMod *= ArtificerPlayer.techModifier;
            }
            if (artificer.mechSetBonus)
            {
                netMod *= ArtificerPlayer.mechModifier;
            }
            if (artificer.astralSetBonus)
            {
                netMod *= ArtificerPlayer.astralModifier;
            }

            if (artificer.energyCellBonus)
            {
                netMod *= ArtificerPlayer.energyCellModifier;
            }
            else if (artificer.emblemBonus)
            {
                netMod *= ArtificerPlayer.emblemModifier;
            }
            else if (artificer.coolingBonus)
            {
                netMod *= ArtificerPlayer.coolingCellModifier;
            }
            else if (artificer.clockworkBonus)
            {
                netMod *= ArtificerPlayer.clockworkWatchModifier;
            }
            else if (artificer.astralEmblemBonus)
            {
                netMod *= ArtificerPlayer.astralEmblemModifier;
            }

            return netMod;
        }

        /// <summary>
		/// Used by ability accessories in tandem with ModifyTooltips to insert cooldown information in the proper place. Returns the name of the TooltipLine to be used.
		/// </summary>
		/// <param name="item">The item to retrieve the TooltipLine for.</param>
        private string GetTooltipLineTarget(Item item)
        {
            int type = item.type;

            List<int> tooltips2 = new List<int>
            {
                ModContent.ItemType<LuckyPresent>(),
                ModContent.ItemType<ForebodingCharm>(),
                ModContent.ItemType<MissileArray>(),
                ModContent.ItemType<ProbePack>(),
                ModContent.ItemType<OrnateRing>(),
                ModContent.ItemType<RepulseCharge>(),
                ModContent.ItemType<NeedleBarrage>(),
                ModContent.ItemType<AssassinNeedles>(),
                ModContent.ItemType<HiddenBlade>(),
                ModContent.ItemType<OverchargedArcanum>(),
                ModContent.ItemType<StarplateBlaster>(),
                ModContent.ItemType<SolarBlaster>(),
                ModContent.ItemType<PrismaticArcanum>(),
                ModContent.ItemType<ChaosGem>()
            };
            List<int> tooltips3 = new List<int>
            {
                ModContent.ItemType<Moonfall>(),
                ModContent.ItemType<MoonbiteMark>(),
                ModContent.ItemType<ObliterationCore>(),
                ModContent.ItemType<Harvester>(),
                ModContent.ItemType<Lifeline>(),
                ModContent.ItemType<LeechingTether>(),
                ModContent.ItemType<DarkLeech>(),
                ModContent.ItemType<NeedleSpikeManacle>(),
                ModContent.ItemType<StealthShroud>(),
                ModContent.ItemType<FlamboyantCape>(),
                ModContent.ItemType<RepulsionRocket>(),
                ModContent.ItemType<TurboBoots>(),
                ModContent.ItemType<PrimeGauntlet>(),
                ModContent.ItemType<LostHeroShield>(),
                ModContent.ItemType<VitalStimulator>(),
                ModContent.ItemType<VitalStimulator2>(),
                ModContent.ItemType<VitalStimulator3>(),
                ModContent.ItemType<VitalStimulator4>(),
                ModContent.ItemType<LifeCharger>(),
                ModContent.ItemType<ManaCharger>(),
                ModContent.ItemType<RetributionStoker>(),
                ModContent.ItemType<Extinguisher>(),
                ModContent.ItemType<Curer>(),
                ModContent.ItemType<Cleanser>(),
                ModContent.ItemType<ArtificersArmor>(),
                ModContent.ItemType<ArcanaStimulator>(),
                ModContent.ItemType<ArcanaStimulator2>(),
                ModContent.ItemType<ArcanaStimulator3>(),
                ModContent.ItemType<ArcanaStimulator4>(),
                ModContent.ItemType<PowerArmor>(),
                ModContent.ItemType<FirstPrism>(),
                ModContent.ItemType<CyborgCore>()
            };
            List<int> tooltips4 = new List<int>
            {
                ModContent.ItemType<StarlightArmillary>(),
                ModContent.ItemType<FearReaper>(),
                ModContent.ItemType<ShadowShroud>(),
                ModContent.ItemType<SuperchargedArm>(),
                ModContent.ItemType<OverdriveLeg>(),
                ModContent.ItemType<XRadar>(),
                ModContent.ItemType<Starwriter>(),
                ModContent.ItemType<AnkhRenewer>()
            };
            List<int> tooltips5 = new List<int>
            {
                ModContent.ItemType<StellarResonator>(),
                ModContent.ItemType<SoulEmulator>(),
                ModContent.ItemType<SwaggerCape>(),
                ModContent.ItemType<AugmentedLeg>(),
                ModContent.ItemType<BewitchedBulwark>()
            };
            List<int> tooltips6 = new List<int>
            {
                ModContent.ItemType<ArtificerProsthetics>()
            };

            if (tooltips3.Contains(type))
            {
                return "Tooltip2";
            }
            else if (tooltips4.Contains(type))
            {
                return "Tooltip3";
            }
            else if (tooltips5.Contains(type))
            {
                return "Tooltip4";
            }
            else if (tooltips6.Contains(type))
            {
                return "Tooltip5";
            }
            else
            {
                return "Tooltip1";
            }
        }

        /// <summary>
		/// Used by ability accessories in tandem with ModifyTooltips to insert cooldown information in the proper place. Some ability accessories have a placeholder tooltip that is replaced; this method returns if one ISN'T present.
		/// </summary>
		/// <param name="item">The item to check.</param>
        private bool HasFlavorText(Item item)
        {
            int type = item.type;

            List<int> flavorText = new List<int>
            {
                ModContent.ItemType<ChaosGem>(),
                ModContent.ItemType<CyborgCore>(),
                ModContent.ItemType<FirstPrism>(),
                ModContent.ItemType<PowerArmor>(),
                ModContent.ItemType<Starwriter>(),
                ModContent.ItemType<ArtificersArmor>(),
                ModContent.ItemType<Extinguisher>(),
                ModContent.ItemType<RetributionStoker>(),
                ModContent.ItemType<XRadar>(),
                ModContent.ItemType<LostHeroShield>(),
                ModContent.ItemType<OverdriveLeg>(),
                ModContent.ItemType<TurboBoots>(),
                ModContent.ItemType<SuperchargedArm>(),
                ModContent.ItemType<ArtificerProsthetics>(),
                ModContent.ItemType<NeedleBarrage>(),
                ModContent.ItemType<RepulseCharge>(),
                ModContent.ItemType<RepulsionRocket>(),
                ModContent.ItemType<SwaggerCape>(),
                ModContent.ItemType<NeedleSpikeManacle>(),
                ModContent.ItemType<Lifeline>(),
                ModContent.ItemType<Harvester>(),
                ModContent.ItemType<ObliterationCore>(),
                ModContent.ItemType<LuckyPresent>(),
                ModContent.ItemType<ForebodingCharm>(),
                ModContent.ItemType<StellarResonator>(),
                ModContent.ItemType<MoonbiteMark>(),
                ModContent.ItemType<StarlightArmillary>(),
                ModContent.ItemType<Moonfall>()
            };

            return flavorText.Contains(type);
        }
    }

    public class HeartPickupBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.Heart || entity.type == ItemID.CandyApple || entity.type == ItemID.CandyCane;
        }

        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            if (!player.TryGetModPlayer(out ArtificerPlayer artificer))
            {
                return;
            }
            else if (artificer.heartAce || artificer.heartsurge)
            {
                grabRange += 42;
            }
        }

        public override bool CanPickup(Item item, Player player)
        {
            // Credit to Terraria Overhaul & Mirasio (mod author) for most of this open-source code (hijacking the on-pickup effect, as well as sound/visual fx)
            // Also includes Overhaul compatibility!
            bool overhaulLoaded = ModLoader.HasMod("TerrariaOverhaul"); // Check for Terraria Overhaul for compatibility
            ArtificerPlayer artificer = player.GetModPlayer<ArtificerPlayer>();

            if (player.getRect().Intersects(item.getRect()) && artificer.heartAce == true)
            {
                // Terraria Overhaul compatibility; reduces heal greatly and accomidates for stack size to work properly with Overhaul's changes
                if(overhaulLoaded)
                {
                    if(player.whoAmI == Main.myPlayer)
                    {
                        player.Heal(item.stack * 6);
                    }
                    if (!Main.dedServ)
                    {
                        DoOverhaulVFX(item, player);
                    }
                }
                else 
                {
                    // Normal behavior; up HP regain to 25
                    if (player.whoAmI == Main.myPlayer)
                    {
                        player.Heal(25);
                    }
                }
                
                // This gets rid of the item, as though it were picked up
                item.active = false;
                item.TurnToAir();

                return false; // Prevents the normal on-pickup effects, i.e; healing 20 HP
            }
            return true;
        }

        public void DoOverhaulVFX(Item item, Player player)
        {
            for (int i = 0; i < 15; i++)
            {
                var dust = Dust.NewDustDirect(player.Hitbox.TopLeft(), player.Hitbox.Width, player.Hitbox.Height, DustID.SomethingRed, Scale: Main.rand.NextFloat(1.5f, 2f));

                dust.noLight = true;
                dust.noGravity = true;
                dust.velocity = (player.velocity + item.velocity.RotatedByRandom(MathHelper.PiOver4)) * 0.5f;
                dust.alpha = 96;
            }
            SoundStyle pickupSound = new SoundStyle($"{nameof(ArtificerMod)}/Assets/OverhaulHeartSFX")
            {
                Volume = 0.33f,
                PitchVariance = 0.15f,
                MaxInstances = 3,
            };
            SoundEngine.PlaySound(pickupSound, !(player == Main.LocalPlayer) ? player.Center : null); 
        }
    }

    public class SuperMagnetGrab : GlobalItem
    {
        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            if (!player.TryGetModPlayer(out ArtificerPlayer artificer))
            {
                return;
            }
            if (artificer.superMagnet)
            {
                // Base +12.5 tiles (~15 total tile grab range with base reach)
                if (player.treasureMagnet) // Overall adds +5 tiles to reach
                {
                    grabRange += 130;
                }
                else
                {
                    grabRange += 200;
                }
                
                if(item.type == ItemID.Star || item.type == ItemID.SoulCake || item.type == ItemID.SugarPlum)
                {
                    // Adds +5 tiles (Total +17.5)
                    if (player.manaMagnet) // Overall adds +5 tiles to reach
                    {
                        // Subtracts to cancel out most of ther other boost in order to 'compound' them
                        grabRange -= 142;
                    }
                    else
                    {
                        grabRange += 80;
                    }
                }
                if(item.type == ItemID.Heart || item.type == ItemID.Heart || item.type == ItemID.Heart)
                {
                    // Adds +5 tiles (Total +17.5)
                    if (player.lifeMagnet) // Overall adds +5 tiles to reach
                    {
                        grabRange -= 92;
                    }
                    else
                    {
                        grabRange += 80;
                    }
                }
                if(item.type == ItemID.CopperCoin || item.type == ItemID.SilverCoin || item.type == ItemID.GoldCoin || item.type == ItemID.PlatinumCoin)
                {
                    // Adds +15 tiles (Total +27.5)
                    if (player.goldRing) // Overall adds +10 tiles to reach
                    {
                        grabRange += 48;
                    }
                    else
                    {
                        grabRange += 240;
                    }
                }
            }

            // Has a weakened effect even if not worn. Works from the inventory like info accessories.
            else if (artificer.superMagnetWeak) 
            {
                if (item.type == ItemID.Star || item.type == ItemID.SoulCake || item.type == ItemID.SugarPlum)
                {
                    if (player.manaMagnet) 
                    {
                        grabRange += 40;
                        return;
                    }
                }
                if (item.type == ItemID.Heart || item.type == ItemID.Heart || item.type == ItemID.Heart)
                {
                    if (player.lifeMagnet)
                    {
                        grabRange += 40;
                        return;
                    }
                }
                if (item.type == ItemID.CopperCoin || item.type == ItemID.SilverCoin || item.type == ItemID.GoldCoin || item.type == ItemID.PlatinumCoin)
                {
                    if (player.goldRing)
                    {
                        grabRange += 40;
                        return;
                    }
                }

                if (player.treasureMagnet)
                {
                    grabRange += 60;
                    return;
                }
                grabRange += 100;
            }
        }
    }
}