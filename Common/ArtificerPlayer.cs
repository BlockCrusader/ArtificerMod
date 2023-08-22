using ArtificerMod.Content.Projectiles.ArmorH;
using ArtificerMod.Content.Buffs.ArmorH;
using ArtificerMod.Content.Items.AbilityAccH;
using ArtificerMod.Content.Projectiles.AbilityAccH;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ArtificerMod.Content.Buffs;
using ArtificerMod.Content.Buffs.ArmorPH;
using ArtificerMod.Content.Items.AbilityAccPH;
using ArtificerMod.Content.Buffs.AbilityAccPH;
using ArtificerMod.Content.Buffs.AbilityAccH;
using Terraria.Graphics.Shaders;
using ArtificerMod.Content.Projectiles.AbilityAccPH;
using ArtificerMod.Content.Items.AccessoriesPH;
using ArtificerMod.Content.Buffs.AccessoriesPH;
using ArtificerMod.Content.Buffs.AccessoriesH;
using ArtificerMod.Content.Projectiles.AccessoriesH;
using Terraria.GameContent.Achievements;
using ArtificerMod.Content.Buffs.AbilityAccH.LuckyPresent;
using System.IO;

namespace ArtificerMod.Common
{
    public class ArtificerPlayer : ModPlayer
    {
        // General:
        // Ammo conservation
        public bool ammoCost95Helm;
        public bool ammoCost95Chest;
        public bool ammoCost90Helm;

        // In-combat check
        public int inCombatTimer = -1;

        // Ability Cooldown:
        public bool abilityCooldown;
        public static readonly float baseModifier = 1.25f; // Penalty for no artificer armor
        public bool noArtificerBonuses; // Becomes true if any artificer set bonus is active
        public static readonly float noAbilityModifier = 2.4f; // Penalty for no ability accessory
        // Set bonus related cooldown modifiers
        public static readonly float techModifier = (5f / 6f); // ~.83f
        public static readonly float mechModifier = .8f;
        public static readonly float astralModifier = .75f;
        // Accessory related cooldown modifiers
        public bool energyCellBonus;
        public static readonly float energyCellModifier = .9f;
        public bool emblemBonus;
        public static readonly float emblemModifier = (5f / 6f); // ~.83f
        public bool coolingBonus;
        public static readonly float coolingCellModifier = .8f;
        public bool clockworkBonus;
        public static readonly float clockworkWatchModifier = .75f;
        public bool astralEmblemBonus;
        public static readonly float astralEmblemModifier = (4f / 6f); // ~.67f
        // Journey Mode
        public bool journeyGodmodeBonus = false;
        public static readonly float journeyGodmodeModifier = .25f; 
        // Journey Mode

        // Set Bonuses:
        public bool ornateSetBonus;
        public bool starplateSetBonus;
        public bool techSetBonus;
        public bool prismSetBonus;
        public int prismType;
        bool prismTriggerDown;
        bool prismTriggerUp;
        int prismToggleCool;
        public bool tarnishedSetBonus;
        public bool mechSetBonus;
        public int mechCooldown;
        public bool championSetBonus;
        public bool lihzahrdSetBonus;
        int lihzahrdCooldown;
        public bool xenoSetBonus;
        int xenoShieldCooldown;
        int xenoChargeCounter;
        bool xenoFinishFX = true;
        int xenoChargeFX;
        public static readonly int shieldCapNormal = 100;
        public static readonly int shieldCapExpert = 150;
        public static readonly int shieldCapMaster = 200;
        public int xenoShieldPower;
        public int xenoShieldCap;
        public bool astralSetBonus;

        // Accessories:
        public bool abilityAcc;
        public bool starwriter;
        public Item starwriterEquip;
        int starwriterCountdown = -1;
        bool starwriterActivate = false;
        public bool artificerArmor;
        public bool artificerArmorVanity;
        public bool artificerArmorActive;
        public bool powerArmor;
        public bool powerArmorVanity;
        public bool powerArmorActive;
        public int hpStimType;
        public int manaStimType;
        public int healthStim;
        public int manaStim;
        int stimTimerH = -1;
        int stimTimerM = -1;
        public bool lifeCharger;
        public bool lifeChargerActive;
        public bool manaCharger;
        public bool manaChargerActive;
        public bool lifeChargerSuccess;
        public bool manaChargerSuccess;
        public int debuffRemovalAcc;
        public bool retributionStoker;
        public bool retributionActive;
        int retributionCharge;
        float retributionPower;
        public bool youmeanthechaosemeralds; // Chaos gem
        public bool nanomachinesSon; // Cyborg Core
        public bool senatorMoment; // Cyborg Core buff active 
        public bool xRadar;
        public bool arcanum;
        public Item arcanumEquip;
        public bool arcanum2;
        public Item arcanum2Equip;
        public bool heartAce;
        public bool autoHeal;
        public bool blaster;
        public Item blasterEquip;
        public bool blaster2;
        public Item blaster2Equip;
        public bool prism;
        public Item prismEquip;
        public bool bBulwark;
        public Item bBulwarkEquip;
        public bool lifesurge;
        public int lifesurgeCooldown = -1;
        public bool heartsurge;
        public int heartsurgeCooldown = -1;
        public bool soulHunter;
        public bool heavyShoes;
        public bool sandstormCarpet;
        public bool ultrabrightDiving;
        public bool papermarioreference; // Action and Lucky Star
        public bool pNecklace;
        public int pNecklaceCooldown = -1;
        public bool shadowwizardmoneygang; // Shadowflame Scroll
        public bool superMagnet;
        public bool bloodNecklace;
        public bool permafrost;
        public Item pCloakEquip;
        public int permafrostCooldown = -1;
        public Item pShellEquip; 
        public int blueShellCooldown = -1; // Permafrost Shell respawn
        public bool throwBlueShell; // Permafrost Shell attack
        public bool electricArm;
        public bool lostShield;
        bool noRevive; // Set to true when hit with an insta-kill attack
        public bool terraShield;
        public bool primeGauntlet;
        public Item primeEquip;
        public int primeBoneCooldown = -1;
        public Item tShieldEquip;
        public int twinLaserCooldown = -1;
        public bool illusionMotherboard;
        public bool turboBoots;
        public bool rocketLeg;
        public int rocketJumpTimer = -1;
        public int rocketSafeFall = -1;
        public bool overdriveLeg;
        public bool superArm;
        public bool prosthetics;
        public int needleAttack;
        public Item needleEquip;
        public int needlesToShoot = -1;
        public int needleTimer = -1;
        public bool repulseCharge;
        public Item rChargeEquip;
        public bool repulseRocket;
        public Item rRocketEquip;
        public bool stealthShroud;
        public bool flamboyantCape;
        public bool shadowShroud;
        public bool swaggerCape;
        public bool stealthActive;
        public bool ornateRing;
        public Item ornateEquip;
        public bool spikeManacle;
        public int tetherLeech;
        public Item tetherEquip;
        public bool funiDungeonsArtifact; // Necrotic Harvester
        public Item dungeonsReference;
        public int harvesterCharge;
        public int hChargeCool;
        public int hChargeExpire;
        public bool probePack;
        public Item probeEquip;
        public bool soulEmulator;
        public int emulatorState = 0;
        public bool obliterationCore;
        public Item oCoreEquip;
        public bool missileArray;
        public Item missileEquip;
        public int missilesToShoot = -1;
        public int missilesTimer = -1;
        public bool fearReaper;
        public Item reaperEquip;
        public int reaperDRCounter;
        public bool luckyPresent;
        public int presentLuck;
        public bool fCharm;
        public Item fCharmEquip;
        public bool stellarResonator;
        public Item resonatorEquip;
        public int stellarCountdown = -1;
        public bool resonatorActive;
        public bool lienAcc; // Moonbite Mark
        public Item homageEquip;
        public int knifeWaves;
        public int knifeTimer;
        public bool seymoursBizzareAdventure; // Starlight Armillary
        public Item starlightRiverReference; // Starlight Armillary equip
        public bool orbitalStrike; // Moonfall
        public Item moonfallEquip;
        public bool extraCritDmg;
        public bool superMagnetWeak;

        // Flags
        bool cooldownFlag;
        bool abilityFlag;
        public bool accFlagOr; // Ornate Armor
        public bool accFlagSt; // Starplate Armor
        public bool accFlagTe; // Tech Armor
        public bool accFlagPr; // Prism Armor
        public bool accFlagTa; // Tarnished Armor
        public bool accFlagMe; // Mechanical Armor
        public bool accFlagCh; // Champion Armor
        public bool accFlagLi; // Lihzahrd Armor
        public bool accFlagXe; // Xeno Armor
        public bool accFlagAs; // Astral Armor
        public bool cFlagEn; // Energy Cell
        public bool cFlagEm; // Artificer Emblem
        public bool cFlagCo; // Cooling Cell
        public bool cFlagCl; // Clockwork Watch
        public bool cFlagAs; // Astral Emblem
        public bool cFlagJM; // Journey Mode
        public bool artificerArmorFlag;
        bool artificerArmorFlagA;
        public bool powerArmorFlag;
        bool powerArmorFlagA;
        bool chargerFlagL;
        bool chargerFlagM;
        bool retributionFlag;
        bool pNecklaceFlag;
        public bool throwBlueShellFlag;
        bool tShieldFlag;
        bool repulsionFlag;
        public bool stealthShroudFlag;
        public bool flamboyantCapeFlag;
        public bool shadowShroudFlag;
        public bool swaggerCapeFlag;
        public bool spikeManacleFlag;
        bool stealthAFlag;
        public bool radiantArmorFlag;

        public override void ResetEffects()
        {
            // Flags
            cooldownFlag = abilityCooldown;
            abilityFlag = abilityAcc;
            cFlagJM = journeyGodmodeBonus;
            // Armor
            noArtificerBonuses = !(ornateSetBonus || starplateSetBonus || techSetBonus
                                    || prismSetBonus || tarnishedSetBonus || mechSetBonus
                                    || championSetBonus || lihzahrdSetBonus || xenoSetBonus
                                    || astralSetBonus);
            accFlagOr = ornateSetBonus;
            accFlagSt = starplateSetBonus;
            accFlagTe = techSetBonus;
            accFlagTa = tarnishedSetBonus;
            accFlagPr = prismSetBonus;
            accFlagMe = mechSetBonus;
            accFlagCh = championSetBonus;
            accFlagLi = lihzahrdSetBonus;
            accFlagXe = xenoSetBonus;
            accFlagAs = astralSetBonus;
            radiantArmorFlag = prismSetBonus;
            // Accessories
            cFlagAs = astralEmblemBonus;
            cFlagCo = coolingBonus;
            cFlagEm = emblemBonus;
            cFlagCl = clockworkBonus;
            cFlagEn = energyCellBonus;
            artificerArmorFlag = artificerArmor;
            artificerArmorFlagA = artificerArmorActive;
            powerArmorFlag = powerArmor;
            powerArmorFlagA = powerArmorActive;
            chargerFlagL = lifeChargerActive;
            chargerFlagM = manaChargerActive;
            retributionFlag = retributionActive;
            pNecklaceFlag = pNecklace;
            throwBlueShellFlag = throwBlueShell;
            tShieldFlag = terraShield;
            stealthShroudFlag = stealthShroud;
            flamboyantCapeFlag = flamboyantCape;
            shadowShroudFlag = shadowShroud;
            swaggerCapeFlag = swaggerCape;
            stealthAFlag = stealthActive;
            spikeManacleFlag = spikeManacle;


            // General
            ammoCost95Helm = false;
            ammoCost95Chest = false;

            ammoCost90Helm = false;

            // Ability Cooldown
            abilityCooldown = false;
            journeyGodmodeBonus = false;
            // Accessory related cooldown modifiers
            energyCellBonus = false;
            emblemBonus = false;
            coolingBonus = false;
            clockworkBonus = false;
            astralEmblemBonus = false;

            // Set bonuses
            ornateSetBonus = false;
            starplateSetBonus = false;
            techSetBonus = false;

            // This is part of Radiant (prism) Armor's set bonus:
            if (prismSetBonus && prismToggleCool <= 0 && Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[0] < 15)
            {
                prismTriggerDown = true;
                prismTriggerUp = false;
                prismToggleCool = 30;
            }
            else if (prismSetBonus && prismToggleCool <= 0 && Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[1] < 15)
            {
                prismTriggerUp = true;
                prismTriggerDown = false;
                prismToggleCool = 30;
            }
            else
            {
                prismTriggerDown = false;
                prismTriggerUp = false;
            }
            prismSetBonus = false;

            tarnishedSetBonus = false;
            mechSetBonus = false;
            championSetBonus = false;
            lihzahrdSetBonus = false;
            xenoSetBonus = false;
            astralSetBonus = false;


            // Accessories
            abilityAcc = false;
            starwriter = false;
            starwriterEquip = null;
            artificerArmor = false;
            artificerArmorVanity = false;
            artificerArmorActive = false;
            powerArmor = false;
            powerArmorVanity = false;
            powerArmorActive = false;
            hpStimType = 0;
            manaStimType = 0;
            healthStim = 0;
            manaStim = 0;
            lifeCharger = false;
            lifeChargerActive = false;
            manaCharger = false;
            manaChargerActive = false;
            debuffRemovalAcc = 0;
            retributionStoker = false;
            retributionActive = false;
            youmeanthechaosemeralds = false;
            nanomachinesSon = false;
            senatorMoment = false;
            xRadar = false;
            arcanum = false;
            arcanumEquip = null;
            arcanum2 = false;
            arcanum2Equip = null;
            heartAce = false;
            autoHeal = false;
            blaster = false;
            blasterEquip = null;
            blaster2 = false;
            blaster2Equip = null;
            prism = false;
            prismEquip = null;
            bBulwark = false;
            bBulwarkEquip = null;
            lifesurge = false;
            soulHunter = false;
            heavyShoes = false;
            sandstormCarpet = false;
            ultrabrightDiving = false;
            papermarioreference = false;
            pNecklace = false;
            shadowwizardmoneygang = false;
            superMagnet = false;
            bloodNecklace = false;
            permafrost = false;
            pCloakEquip = null;
            pShellEquip = null;
            throwBlueShell = false;
            electricArm = false;
            lostShield = false;
            noRevive = false;
            terraShield = false;
            primeEquip = null;
            primeGauntlet = false;
            tShieldEquip = null;
            illusionMotherboard = false;
            heartsurge = false;
            turboBoots = false;
            rocketLeg = false;
            overdriveLeg = false;
            superArm = false;
            prosthetics = false;
            needleAttack = -1;
            needleEquip = null;
            repulseCharge = false;
            rChargeEquip = null;
            repulseRocket = false;
            rRocketEquip = null;
            stealthShroud = false;
            flamboyantCape = false;
            shadowShroud = false;
            swaggerCape = false;
            stealthActive = false;
            ornateRing = false;
            ornateEquip = null;
            spikeManacle = false;
            tetherLeech = 0;
            tetherEquip = null;
            funiDungeonsArtifact = false;
            dungeonsReference = null;
            probePack = false;
            probeEquip = null;
            soulEmulator = false;
            obliterationCore = false;
            oCoreEquip = null;
            missileArray = false;
            missileEquip = null;
            fearReaper = false;
            reaperEquip = null;
            fCharm = false;
            fCharmEquip = null;
            luckyPresent = false;
            stellarResonator = false;
            resonatorEquip = null;
            lienAcc = false;
            homageEquip = null;
            seymoursBizzareAdventure = false;
            starlightRiverReference = null;
            orbitalStrike = false;
            moonfallEquip = null;
            extraCritDmg = false;
            superMagnetWeak = false;
        }

        /// <summary>
		/// Dynamically updates the duration of AbilityCooldown based on player conditions.
		/// </summary>
        public void UpdateAbilityCooldown()
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                // Make sure we have the cooldown debuff before messing with it
                int cooldownIndex = Player.FindBuffIndex(ModContent.BuffType<AbilityCooldown>());
                if (cooldownIndex != -1)
                {
                    // First, grab the current cooldown
                    float newCooldown = Player.buffTime[cooldownIndex];

                    if (!journeyGodmodeBonus) // These penalties are bypassed by Journey Mode's Godmode
                    {
                        if (noArtificerBonuses != !(ornateSetBonus || starplateSetBonus || techSetBonus
                                                || prismSetBonus || tarnishedSetBonus || mechSetBonus
                                                || championSetBonus || lihzahrdSetBonus || xenoSetBonus
                                                || astralSetBonus))
                        {
                            newCooldown = ((noArtificerBonuses) ? (newCooldown / baseModifier) : (newCooldown * baseModifier));
                        }
                        if (abilityFlag != abilityAcc)
                        {
                            newCooldown = ((abilityAcc) ? (newCooldown / noAbilityModifier) : (newCooldown * noAbilityModifier));
                        }
                    }
                    
                    if (accFlagTe != techSetBonus)
                    {
                        newCooldown = ((!techSetBonus) ? (newCooldown / techModifier) : (newCooldown * techModifier));
                    }
                    if (accFlagMe != mechSetBonus)
                    {
                        newCooldown = ((!mechSetBonus) ? (newCooldown / mechModifier) : (newCooldown * mechModifier));
                    }
                    if (accFlagAs != astralSetBonus)
                    {
                        newCooldown = ((!astralSetBonus) ? (newCooldown / astralModifier) : (newCooldown * astralModifier));
                    }

                    // Accesories (Energy Cell, Artificer Emblem, Cooling Cell, Astral Emblem):
                    if (cFlagEn != energyCellBonus)
                    {
                        newCooldown = ((!energyCellBonus) ? (newCooldown / energyCellModifier) : (newCooldown * energyCellModifier));
                    }
                    if (cFlagEm != emblemBonus)
                    {
                        newCooldown = ((!emblemBonus) ? (newCooldown / emblemModifier) : (newCooldown * emblemModifier));
                    }
                    if (cFlagCo != coolingBonus)
                    {
                        newCooldown = ((!coolingBonus) ? (newCooldown / coolingCellModifier) : (newCooldown * coolingCellModifier));
                    }
                    if (cFlagCl != clockworkBonus)
                    {
                        newCooldown = ((!clockworkBonus) ? (newCooldown / clockworkWatchModifier) : (newCooldown * clockworkWatchModifier));
                    }
                    if (cFlagAs != astralEmblemBonus)
                    {
                        newCooldown = ((!astralEmblemBonus) ? (newCooldown / astralEmblemModifier) : (newCooldown * astralEmblemModifier));
                    }

                    // Journey Mode
                    if (cFlagJM != journeyGodmodeBonus)
                    {
                        newCooldown = ((!journeyGodmodeBonus) ? (newCooldown / journeyGodmodeModifier) : (newCooldown * journeyGodmodeModifier));
                    }

                    // Finally, apply the adjusted time
                    Player.buffTime[cooldownIndex] = (int)newCooldown;
                }
            }
        }

        /// <summary>
		/// Dynamically updates the duration of TerraCooldown based on player conditions.
		/// </summary>
        public void UpdateTerraShieldCooldown()
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                int cooldownIndex = Player.FindBuffIndex(ModContent.BuffType<TerraCooldown>());
                if (cooldownIndex != -1)
                {
                    float newCooldown = Player.buffTime[cooldownIndex];

                    if (tShieldFlag != terraShield) 
                    {
                        newCooldown = ((!terraShield) ? (newCooldown / 0.75f) : (newCooldown * 0.75f));
                    }

                    Player.buffTime[cooldownIndex] = (int)newCooldown;
                }
            }
        }

        /// <summary>
        /// Applies the AbilityCooldown debuff after making adjustments based on player conditions.
        /// </summary>
        /// <param name="secondsOfCooldown">The base duration of cooldown in seconds. This will be adjusted automatically.</param>
        public void AddCooldown(int secondsOfCooldown)
        {
            // convert seconds to ticks
            int duration = secondsOfCooldown * 60;

            if (journeyGodmodeBonus) 
            {
                duration = (int)(duration * journeyGodmodeModifier);
            }
            else
            {
                if (!(ornateSetBonus || starplateSetBonus || techSetBonus
                   || prismSetBonus || tarnishedSetBonus || mechSetBonus
                   || championSetBonus || lihzahrdSetBonus || xenoSetBonus
                   || astralSetBonus))
                {
                    duration = (int)(duration * baseModifier);
                }
                if (!abilityAcc)
                {
                    duration = (int)(duration * noAbilityModifier);
                }
            }
            
            if (techSetBonus)
            {
                duration = (int)(duration * techModifier);
            }
            if (mechSetBonus)
            {
                duration = (int)(duration * mechModifier);
            }
            if (astralSetBonus)
            {
                duration = (int)(duration * astralModifier);
            }
            if (energyCellBonus)
            {
                duration = (int)(duration * energyCellModifier);
            }
            else if (emblemBonus)
            {
                duration = (int)(duration * emblemModifier);
            }
            else if (coolingBonus)
            {
                duration = (int)(duration * coolingCellModifier);
            }
            else if (clockworkBonus)
            {
                duration = (int)(duration * clockworkWatchModifier);
            }
            else if (astralEmblemBonus)
            {
                duration = (int)(duration * astralEmblemModifier);
            }

            Player.AddBuff(ModContent.BuffType<AbilityCooldown>(), duration);
        }

        public override void PreUpdate()
        {
            if(inCombatTimer > 0)
            {
                inCombatTimer--;
            }

            if (mechCooldown > 0)
            {
                mechCooldown--;
            }
            if (prismToggleCool > 0)
            {
                prismToggleCool--;
            }
            if (lifesurgeCooldown > 0)
            {
                lifesurgeCooldown--;
            }
            if (heartsurgeCooldown > 0)
            {
                heartsurgeCooldown--;
            }
            if (pNecklaceCooldown > 0)
            {
                pNecklaceCooldown--;
            }
            if (pNecklaceCooldown > 0)
            {
                pNecklaceCooldown--;
            }
            if (permafrostCooldown > 0)
            {
                permafrostCooldown--;
            }
            if (blueShellCooldown > 0)
            {
                blueShellCooldown--;
            }
            if (primeBoneCooldown > 0)
            {
                primeBoneCooldown--;
            }
            if (twinLaserCooldown > 0)
            {
                twinLaserCooldown--;
            }
            if(rocketSafeFall > 0)
            {
                rocketSafeFall--;
            }
            if(needleTimer > 0)
            {
                needleTimer--;
            }
            if (missilesTimer > 0)
            {
                missilesTimer--;
            }
            if (knifeTimer > 0)
            {
                knifeTimer--;
            }
            ManageHarvesterChargeTimers();
        }

        public override void PreUpdateBuffs()
        {
            journeyGodmodeBonus = Player.creativeGodMode && ModContent.GetInstance<ConfigServer>().GodmodeCooldown;
        }

        public override void PostUpdateEquips()
        {
            UpdateAbilityCooldown();

            UpdateTerraShieldCooldown(); 

            // Armor
            NovaBoost();
            XenoShield();
            XenoRegenPerk();
            ChampionBuffs();
            RadiantBooster();
            StarplateBuffs();
            // Abilities
            Stims();
            Chargers();
            Retribution();
            ShootNeedles();
            ShootMissiles();
            ReaperDR();
            StellarResonator();
            LienKnives();
            // Accessories
            LowGravResist();
            BlueShellCheck();
            TerraShieldWard();
            ClockworkTrigger();
        }

        public override void PreUpdateMovement()
        {
            AugmentedLegJump();
            RepulseLaunch();
        }

        public override void PostUpdateMiscEffects()
        {
            ArmorLighting(Player.head, Player.body, Player.legs);

            LihzahrdMisc();
            CyborgCoreMisc();
            PrimeGauntletBones();
            TwinShieldLaser();
            AutoHealthPot();
        }

        public override void PostUpdateRunSpeeds()
        {
            // Slows the player while using the Obliteration Core's ability
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<ObliterationBeam>()] > 0)
            {
                Player.runAcceleration *= 0.5f;
                Player.maxRunSpeed *= 0.5f;
                Player.accRunSpeed *= 0.5f;
                Player.runSlowdown *= 2f;
            }
        }

        public override void PostUpdate()
        {
            BuffEndChecks();
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            // Prevent normal sfx when Artificer's Armor is active
            if (artificerArmorActive || powerArmorActive)
            {
                modifiers.DisableSound();
            }

            xenoShieldCooldown = -282;
            xenoChargeCounter = 0;
            xenoFinishFX = true;
        }

        // TODO: Netcode
        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (info.Dodgeable)
            {
                if (Player.HasBuff(ModContent.BuffType<ChampionProtec>()))
                {
                    TriggerChampionProtec();
                    return true;
                }

                if(illusionMotherboard && Main.rand.NextBool(5) && !Player.HasBuff(ModContent.BuffType<CounterstrikeProtocol>()))
                {
                    TriggerMotherboardDodge();
                    return true;
                }
            }
            
            return false;
        }

        public override bool ConsumableDodge(Player.HurtInfo info)
        {
            if(xenoSetBonus)
            {
                if (XenoFullProtec(info.Damage))
                {
                    TriggerXenoBlock(info.Damage);
                    return true;
                }
            }
            return false;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            noRevive = false;
            info.Damage = XenoProtec(info.Damage, true);
            noRevive = !info.Dodgeable;
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            // Special sfx when Artificer's Armor is active
            if (artificerArmorActive || powerArmorActive)
            {
                SoundEngine.PlaySound(SoundID.NPCHit4, Player.position);
            }
            
            // Require more than 1 damage to be taken for these effects, to prevent cheesing
            if (info.Damage > 1)
            {
                if (CheckForHostiles(500f) && lihzahrdCooldown <= 0)
                {
                    lihzahrdCooldown = 60;
                    LihzahrdRetaliation();
                }
                if (CheckForHostiles(1000f) && retributionStoker)
                {
                    retributionCharge += (int)info.Damage;
                    if(retributionCharge >= 300)
                    {
                        // Dust
                        int dustCount = Math.Clamp(info.Damage, 5, 40);
                        for(int i = 0; i < dustCount; i++)
                        {
                            Dust dust;
                            dust = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, DustID.FlameBurst, 0f, 0f, 0, new Color(255, 0, 0), 1f)];
                            dust.noGravity = true;
                            dust.fadeIn = 0.9f;
                        }
                    }
                }

                if (pNecklace && pNecklaceCooldown <= 0)
                {
                    int cooldownIndex = Player.FindBuffIndex(BuffID.PotionSickness);
                    if (cooldownIndex != -1)
                    {
                        float newCooldown = Player.buffTime[cooldownIndex];

                        newCooldown -= Main.rand.Next(60, 121);

                        Player.buffTime[cooldownIndex] = (int)newCooldown;

                        pNecklaceCooldown = 300;
                    }
                }

                if (pCloakEquip != null && permafrostCooldown <= 0)
                {
                    if(Player == Main.LocalPlayer)
                    {
                        int dmg = 60;
                        if (Main.expertMode)
                        {
                            dmg = 120;
                        }
                        if (Main.masterMode)
                        {
                            dmg = 180;
                        }

                        float numberProjectiles = 3 + Main.rand.Next(4);

                        Vector2 secondaryVelocity = new Vector2(4f);
                        float burstOffset = Main.rand.Next(0, 360);

                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = secondaryVelocity.RotatedBy(MathHelper.ToRadians((360f / numberProjectiles) * (i + 1f)) + MathHelper.ToRadians(burstOffset)); // Watch out for dividing by 0 if there is only 1 projectile.
                            Projectile.NewProjectile(Player.GetSource_Accessory(pCloakEquip), Player.Center, perturbedSpeed, ModContent.ProjectileType<PermafrostCloakSpikes>(), dmg, 6f, Player.whoAmI, Main.rand.Next(5));
                        }
                    }
                    permafrostCooldown = 100;
                }

                if(pShellEquip != null && blueShellCooldown <= 0) 
                {
                    throwBlueShell = true; // This will signal an active shell projectile to attack
                }
            }

            if (bloodNecklace)
            {
                Player.AddBuff(ModContent.BuffType<BloodDrive>(), 300);
            }
            if (permafrost)
            {
                Player.AddBuff(ModContent.BuffType<PermafrostProtection>(), Main.rand.Next(180, 300));
            }
            if (illusionMotherboard)
            {
                IllusionMotherboardConfuse(info.Damage);
            }

            if (lifeChargerSuccess || manaChargerSuccess)
            {
                SoundEngine.PlaySound(SoundID.Shatter, Player.Center);
            }
            lifeChargerSuccess = false;
            Player.ClearBuff(ModContent.BuffType<LifeChargerBuff>());
            manaChargerSuccess = false;
            Player.ClearBuff(ModContent.BuffType<ManaChargerBuff>());

            inCombatTimer = 300;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (noRevive)
            {
                return true;
            }

            if(TerraLastStand())
            {
                return false;
            }
            
            if (TarnishedRevive())
            {
                return false;
            }

            return true;
        }

        public override void UpdateLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<TerraRevive>())) // Prevents negative life regen
            {
                if(Player.lifeRegen < 1)
                {
                    Player.lifeRegen = 1;
                }
            }
        }

        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            if(electricArm && !item.noMelee && !item.noUseGraphic && Main.rand.NextBool(3))
            {
                int num11 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Electric, Player.velocity.X * 0.2f, Player.velocity.Y * 0.2f, Scale: 0.6f);
                Main.dust[num11].noGravity = true;
                Main.dust[num11].velocity *= 0.7f;
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (papermarioreference && Player.luck > 0f
                && modifiers.DamageType != DamageClass.Summon && modifiers.DamageType != DamageClass.SummonMeleeSpeed)
            {
                if (LuckyCritReroll(modifiers.DamageType))
                {
                    modifiers.SetCrit();
                }
            }
            if (extraCritDmg)
            {
                modifiers.CritDamage += 0.4f;
            }
            if (Player.HasBuff(ModContent.BuffType<ShadowShroudBuff>()))
            {
                modifiers.ScalingBonusDamage += 4f; // x5 damage
                Player.ClearBuff(ModContent.BuffType<ShadowShroudBuff>());
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit == true) 
            {
                if (lifesurge && lifesurgeCooldown <= 0)
                {
                    Player.AddBuff(ModContent.BuffType<LifesurgeBuff>(), Main.rand.Next(120, 180));
                    lifesurgeCooldown = 300;
                }
                if (heartsurge && heartsurgeCooldown <= 0)
                {
                    Item.NewItem(new EntitySource_OnHit(Player, target, "heartsurgeCharm"), target.Hitbox, ModContent.ItemType<HeartsurgeHeart>(), noGrabDelay: true);
                    heartsurgeCooldown = 300;
                }

                if (mechSetBonus && Main.rand.NextBool(3))
                {
                    if (mechCooldown <= 0)
                    {
                        mechCooldown = 60;
                        target.AddBuff(ModContent.BuffType<MechShock>(), Main.rand.Next(100, 250));
                    }
                }

                if (shadowwizardmoneygang && hit.DamageType == DamageClass.Magic)
                {
                    target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(100, 200));
                }
            }
            if(target.life <= 0) // Killing blow effects
            {
                if (heartAce && Main.rand.NextBool(10))
                {
                    Item.NewItem(new EntitySource_OnHit(Player, target, "aceOfHearts"), target.Hitbox, ItemID.Heart, noGrabDelay: true);
                }
                else if (heartsurge && heartsurgeCooldown <= 0)
                {
                    Item.NewItem(new EntitySource_OnHit(Player, target, "heartsurgeCharm"), target.Hitbox, ModContent.ItemType<HeartsurgeHeart>(), noGrabDelay: true);
                    heartsurgeCooldown = 300;
                }
            } 

            if (soulHunter)
            {
                target.AddBuff(ModContent.BuffType<SoulHunt>(), Main.rand.Next(180, 300));
            }
            if (electricArm)
            {
                if (Main.rand.NextBool(4))
                { 
                    target.AddBuff(ModContent.BuffType<ElectrifiedEnemy>(), Main.rand.Next(50, 100));
                }
            }
            if (funiDungeonsArtifact && hChargeCool <= 0 && target.CanBeChasedBy() && !target.SpawnedFromStatue)
            {
                harvesterCharge += 2;
                hChargeCool = 60;
                hChargeExpire = 0;
                if(harvesterCharge >= 200)
                {
                    Vector2 extraVelocity = Player.Center - target.Center;
                    extraVelocity = extraVelocity.SafeNormalize(Vector2.Zero) * (30f);

                    int dustNum = Main.rand.Next(5, 16);
                    for (int j = 0; j < dustNum; j++)
                    {
                        int dust2 = Dust.NewDust(target.Center, 0, 0, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 0.75f);
                        Main.dust[dust2].velocity *= 1.6f;
                        Main.dust[dust2].velocity.Y -= 1f;
                        Main.dust[dust2].velocity += extraVelocity;
                        Main.dust[dust2].noGravity = true;
                    }
                }
            }
            inCombatTimer = 300;
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            bool ammoConserved = false;
            if (ammoCost90Helm && Main.rand.NextBool(10))
            {
                ammoConserved = true;
            }
            if (ammoCost95Helm && Main.rand.NextBool(20))
            {
                ammoConserved = true;
            }
            if (ammoCost95Chest && Main.rand.NextBool(20))
            {
                ammoConserved = true;
            }

            return !ammoConserved;
        }

        public override void ModifyLuck(ref float luck)
        {
            // Lucky and Action Star both grant 0.1 luck. But only 0.05 of it is given to other players via. equipmentbasedluckbonus
            if (papermarioreference) 
            {
                // This adds the other 0.05
                luck += 0.05f;
            }
        }

        public override void UpdateVisibleVanityAccessories()
        {
            for (int n = 13; n < 18 + Player.GetAmountOfExtraAccessorySlotsToShow(); n++)
            {
                Item item = Player.armor[n];
                if (item.type == ModContent.ItemType<PowerArmor>())
                {
                    powerArmorVanity = true;
                }
                else if (item.type == ModContent.ItemType<ArtificersArmor>())
                {
                    artificerArmorVanity = true;
                }
            }
        }

        public override void FrameEffects()
        {
            if (powerArmorActive || powerArmorVanity)
            {
                var powerArmor = ModContent.GetInstance<PowerArmor>();
                Player.head = EquipLoader.GetEquipSlot(Mod, powerArmor.Name, EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, powerArmor.Name, EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, powerArmor.Name, EquipType.Legs);
            }
            else if (artificerArmorActive || artificerArmorVanity)
            {
                var artificersArmor = ModContent.GetInstance<ArtificersArmor>();
                Player.head = EquipLoader.GetEquipSlot(Mod, artificersArmor.Name, EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, artificersArmor.Name, EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, artificersArmor.Name, EquipType.Legs);
            }
        }

        private void ArmorLighting(int head, int body, int legs)
        {
            float helmR = 0f;
            float helmG = 0f;
            float helmB = 0f;
            float bodyR = 0f;
            float bodyG = 0f;
            float bodyB = 0f;
            float legR = 0f;
            float legG = 0f;
            float legB = 0f;
            Mod mod = ModLoader.GetMod("ArtificerMod");
            if (head == EquipLoader.GetEquipSlot(mod, "AstralHeadgear", EquipType.Head))
            {
                helmR = 0.65f;
                helmG = 0.6f;
                helmB = 0.15f;
            }
            if (body == EquipLoader.GetEquipSlot(mod, "AstralCuirass", EquipType.Body))
            {
                bodyR = 0.65f;
                bodyG = 0.6f;
                bodyB = 0.15f;
            }
            if (legs == EquipLoader.GetEquipSlot(mod, "AstralGreaves", EquipType.Legs))
            {
                legR = 0.65f;
                legG = 0.6f;
                legB = 0.15f;
            }
            if (head == EquipLoader.GetEquipSlot(mod, "UltrasightHelmet", EquipType.Head))
            {
                helmR = 0.7f;
                helmG = 0.95f;
                helmB = 0.82f;
            }
            if (head == EquipLoader.GetEquipSlot(mod, "OmnisightHelmet", EquipType.Head))
            {
                helmR = 0.65f;
                helmG = 1f;
                helmB = 0.75f;
            }
            if (ultrabrightDiving)
            {
                helmR = 0.7f;
                helmG = 0.95f;
                helmB = 0.82f;
            }
            if (Player.HasBuff(ModContent.BuffType<SolarOverdrive>()))
            {
                helmR = 0.75f;
                helmG = 0.65f;
                helmB = 0.2f;
                bodyR = 0.75f;
                bodyG = 0.65f;
                bodyB = 0.2f;
                legR = 0.75f;
                legG = 0.65f;
                legB = 0.2f;
            }
            if (xenoShieldPower > 0)
            {
                helmR = 0.05f;
                helmG = 0.3f;
                helmB = 0.35f;
                bodyR = 0.05f;
                bodyG = 0.3f;
                bodyB = 0.35f;
                legR = 0.05f;
                legG = 0.3f;
                legB = 0.35f;
                if(xenoShieldPower >= xenoShieldCap)
                {
                    helmR = 0.1f;
                    helmG = 0.4f;
                    helmB = 0.45f;
                    bodyR = 0.1f;
                    bodyG = 0.4f;
                    bodyB = 0.45f;
                    legR = 0.1f;
                    legG = 0.4f;
                    legB = 0.45f;
                }
            }
            if (head == EquipLoader.GetEquipSlot(mod, "TechHeadgear", EquipType.Head) &&
                body == EquipLoader.GetEquipSlot(mod, "TechBreastplate", EquipType.Body) &&
                legs == EquipLoader.GetEquipSlot(mod, "TechLeggings", EquipType.Legs))
            {
                helmR = 0.1f;
                helmG = 0.3f;
                helmB = 0.4f;
                bodyR = 0.1f;
                bodyG = 0.3f;
                bodyB = 0.4f;
                legR = 0.1f;
                legG = 0.3f;
                legB = 0.4f;
            }
            if (helmR != 0f || helmG != 0f || helmB != 0f)
            {
                float num3 = 1f;
                if (helmR == bodyR && helmG == bodyG && helmB == bodyB)
                {
                    num3 += 0.5f;
                }
                if (helmR == legR && helmG == legG && helmB == legB)
                {
                    num3 += 0.5f;
                }
                Vector2 spinningpoint2 = new Vector2(Player.width / 2 + 8, Player.height / 2);
                if (Player.fullRotation != 0f)
                {
                    spinningpoint2 = spinningpoint2.RotatedBy(Player.fullRotation, Player.fullRotationOrigin);
                }
                int i3 = (int)(Player.position.X + spinningpoint2.X) / 16;
                int j2 = (int)(Player.position.Y + spinningpoint2.Y) / 16;
                Lighting.AddLight(i3, j2, helmR * num3, helmG * num3, helmB * num3);
            }
            if (bodyR != 0f || bodyG != 0f || bodyB != 0f)
            {
                float num3 = 1f;
                if (bodyR == helmR && bodyG == helmG && bodyB == helmB)
                {
                    num3 += 0.5f;
                }
                if (bodyR == legR && bodyG == legG && bodyB == legB)
                {
                    num3 += 0.5f;
                }
                Vector2 spinningpoint2 = new Vector2(Player.width / 2 + 8, Player.height / 2);
                if (Player.fullRotation != 0f)
                {
                    spinningpoint2 = spinningpoint2.RotatedBy(Player.fullRotation, Player.fullRotationOrigin);
                }
                int i3 = (int)(Player.position.X + spinningpoint2.X) / 16;
                int j2 = (int)(Player.position.Y + spinningpoint2.Y) / 16;
                Lighting.AddLight(i3, j2, bodyR * num3, bodyG * num3, bodyB * num3);
            }
            if (legR != 0f || legG != 0f || legB != 0f)
            {
                float num4 = 1f;
                if (legR == helmR && legG == helmG && legB == helmB)
                {
                    num4 += 0.5f;
                }
                if (legR == bodyR && legG == bodyG && legB == bodyB)
                {
                    num4 += 0.5f;
                }
                Vector2 spinningpoint3 = new Vector2(Player.width / 2 + 8 * Player.direction, (float)Player.height * 0.75f);
                if (Player.fullRotation != 0f)
                {
                    spinningpoint3 = spinningpoint3.RotatedBy(Player.fullRotation, Player.fullRotationOrigin);
                }
                int i4 = (int)(Player.position.X + spinningpoint3.X) / 16;
                int j3 = (int)(Player.position.Y + spinningpoint3.Y) / 16;
                Lighting.AddLight(i4, j3, legR * num4, legG * num4, legB * num4);
            }
        }

        // Dodge netcode:

        private void TriggerMotherboardDodge()
        {
            Player.brainOfConfusionDodgeAnimationCounter = 300;
            Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80);

            if (Player.whoAmI != Main.myPlayer)
            {
                return;
            }

            Player.AddBuff(ModContent.BuffType<CounterstrikeProtocol>(), 300);

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                DodgeNetcode(Player.whoAmI, 0);
            }
        }

        private void TriggerChampionProtec()
        {
            Player.SetImmuneTimeForAllTypes(30);

            if (Player.whoAmI != Main.myPlayer)
            {
                return;
            }

            Player.AddBuff(BuffID.ParryDamageBuff, 300);

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                DodgeNetcode(Player.whoAmI, 1);
            }
        }

        private void TriggerXenoBlock(int shieldDmg)
        {
            Player.SetImmuneTimeForAllTypes(Player.longInvince ? 80 : 40);

            SoundStyle haloShieldDmg1 = new SoundStyle($"{nameof(ArtificerMod)}/Assets/HaloShieldHit")
            {
                Volume = 0.75f,
                PitchVariance = 0,
                MaxInstances = 1,
            };
            if (ModContent.GetInstance<ConfigClient>().xenoHaloShields)
            {
                SoundEngine.PlaySound(haloShieldDmg1, Player.Center);
            }
            else
            {
                SoundEngine.PlaySound(SoundID.NPCDeath43, Player.Center);
            }
            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.75f, 1f);
                Dust d = Dust.NewDustPerfect(Player.Center, DustID.Electric, speed * 2f, Scale: 0.6f);
                d.noGravity = true;
            }

            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, Player.width, Player.height), new Color(100, 200, 200, 255), shieldDmg, false);

            if (Player.whoAmI != Main.myPlayer)
            {
                return;
            }

            xenoShieldPower -= shieldDmg;

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                DodgeNetcode(Player.whoAmI, 2);
            }
        }

        // Adapted from Example Mod. Handles netcode related to dodge effects
        public static void HandleDodgeNetcode(BinaryReader reader, int whoAmI, int dodgeType)
        {
            int player = reader.ReadByte();
            if (Main.netMode == NetmodeID.Server)
            {
                player = whoAmI;
            }

            switch (dodgeType)
            {
                case 0:
                    Main.player[player].GetModPlayer<ArtificerPlayer>().TriggerMotherboardDodge();
                    break;
                case 1:
                    Main.player[player].GetModPlayer<ArtificerPlayer>().TriggerChampionProtec();
                    break;
                default: // Xeno Shield
                    Main.player[player].GetModPlayer<ArtificerPlayer>().TriggerXenoBlock(reader.ReadInt32());
                    break;
            }

            if (Main.netMode == NetmodeID.Server)
            {
                // If the server receives this message, it sends it to all other clients to sync the effects.
                DodgeNetcode(player, dodgeType);
            }
        }
        public static void DodgeNetcode(int whoAmI, int dodgeType, int xenoDmg = 0)
        {
            ModPacket packet = ModContent.GetInstance<ArtificerMod>().GetPacket();
            byte netWrite;
            switch (dodgeType)
            {
                case 0:
                    netWrite = (byte)ArtificerMod.MessageType.MotherboardDodge;
                    break;
                case 1:
                    netWrite = (byte)ArtificerMod.MessageType.HeroShieldDodge;
                    break;
                default:
                    netWrite = (byte)ArtificerMod.MessageType.XenoShieldBlock;
                    break;
            }
            packet.Write(netWrite);
            packet.Write((byte)whoAmI);
            if(xenoDmg > 0)
            {
                packet.Write(xenoDmg);
            }
            packet.Send(ignoreClient: whoAmI);
        }

        // Armor/Set Bonus Methods:

        private void StarplateBuffs()
        {
            if (starplateSetBonus)
            {
                if (Main.dayTime) // Day; damage and speed buffs
                {
                    Player.AddBuff(ModContent.BuffType<StarplateDay>(), 2);
                }
                else // Night; defense and regen buffs
                {
                    Player.AddBuff(ModContent.BuffType<StarplateNight>(), 2);
                }
            }
        }

        private void RadiantBooster()
        {
            if (prismSetBonus)
            {
                if (prismType < 0 || prismType > 3) // Prevent values out of range (0-3)
                {
                    prismType = 0;
                }
                bool triggerSwap = Main.ReversedUpDownArmorSetBonuses;
                if ((!triggerSwap && prismTriggerDown) || (triggerSwap && prismTriggerUp))
                {
                    SoundEngine.PlaySound(SoundID.Item4, Player.Center);
                    prismType++;
                    // prismType controls what damage class is buffed by this set bonus;
                    // 0: Melee, 1: Ranged, 2: Magic, 3: Summon
                    if (prismType < 0 || prismType > 3) 
                    {
                        prismType = 0;
                    }
                }
                int buffType;
                if (prismType == 0)
                {
                    buffType = ModContent.BuffType<PrismMelee>();
                }
                else if (prismType == 1)
                {
                    buffType = ModContent.BuffType<PrismRanged>();
                }
                else if (prismType == 2)
                {
                    buffType = ModContent.BuffType<PrismMagic>();
                }
                else //(prismType == 3)
                {
                    buffType = ModContent.BuffType<PrismSummon>();
                }
                float maxDetectDistance = 1000;
                float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
                for (int k = 0; k < Main.maxPlayers; k++)
                {
                    // If the player is in range and alive, grant them buffs
                    float sqrDistanceToTarget = Vector2.DistanceSquared(Main.player[k].Center, Player.Center);
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        if (Main.player[k].active && !Main.player[k].dead && !Player.dead
                            && ((!Player.hostile && !Main.player[k].hostile) || Player.team == Main.player[k].team))
                        {
                            Main.player[k].AddBuff(buffType, 2);
                        }
                    }
                }
            }
            else
            {
                prismType = 0;
            }
        }

        private bool TarnishedRevive()
        {
            if (tarnishedSetBonus && !Player.HasBuff(ModContent.BuffType<TarnishedRevive>()))
            {
                Player.statLife = 100; // Heal

                Player.AddBuff(ModContent.BuffType<TarnishedRevive>(), 18000); // This controlls the cooldown

                SoundEngine.PlaySound(SoundID.NPCDeath6, Player.Center); 

                Player.SetImmuneTimeForAllTypes(Player.longInvince ? 220 : 180); 

                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(Player.Center + speed * 20, DustID.Shadowflame, speed * 5f, Scale: 1.25f);
                    d.noGravity = true;
                }

                return true;
            }
            return false;
        }

        private void ChampionBuffs()
        {
            if (championSetBonus)
            {
                float percentHP = ((float)Player.statLife / (float)Player.statLifeMax2);
                if (percentHP >= 0.5f)
                {
                    Player.AddBuff(ModContent.BuffType<ChampionOff>(), 2);
                }
                else
                {
                    Player.AddBuff(ModContent.BuffType<ChampionDef>(), 2);
                }
            }
        }

        private void LihzahrdRetaliation()
        {
            if (lihzahrdSetBonus)
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<LihzahrdArmorBeam>()] < 3)
                {
                    List<NPC> targets = new List<NPC>();
                    NPC target1 = FindClosestNPC(750f, false);
                    NPC target2 = FindClosestNPC(750f, false, target1);
                    NPC target3 = FindClosestNPC(750f, false, target1, target2);
                    targets.Add(target1);
                    targets.Add(target2);
                    targets.Add(target3);

                    int beamDMG = 100;
                    if (Main.expertMode)
                    {
                        beamDMG = 200;
                    }
                    if (Main.masterMode)
                    {
                        beamDMG = 300;
                    }

                    for (int i = 0; i < targets.Count; i++)
                    {
                        if (Main.myPlayer == Player.whoAmI)
                        {
                            // Default to a random aim
                            float projSpeed = 15f;
                            Vector2 projVelocity = Main.rand.NextVector2CircularEdge(15f, 15f);
                            if (targets[i] != null) // If a target has been found, aim toward it instead
                            {
                                projVelocity = (targets[i].Center - Player.Center).SafeNormalize(Vector2.Zero) * projSpeed;
                            }
                            Projectile.NewProjectile(Player.GetSource_Misc("SetBonus_LihzahrdArmor"), Player.Center, projVelocity, ModContent.ProjectileType<LihzahrdArmorBeam>(), beamDMG, 10f, Player.whoAmI);
                        }
                    }
                    SoundEngine.PlaySound(SoundID.Item12, Player.Center); 

                    for (int i = 0; i < 30; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
                        Dust d = Dust.NewDustPerfect(Player.Center, DustID.HeatRay, speed * 3f, Scale: 0.35f);
                        d.noGravity = true;
                    }
                }

                Player.AddBuff(ModContent.BuffType<SolarOverdrive>(), Main.rand.Next(200, 300));
            }
        }
        private void LihzahrdMisc()
        {
            lihzahrdCooldown--;
            // Check for buff, and add dust visuals if active
            if (Player.HasBuff(ModContent.BuffType<SolarOverdrive>()))
            {
                if (Main.rand.NextBool(6))
                {
                    int dustIndex = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, DustID.HeatRay, 0f, 0f, 0, default(Color), 1.4f);
                    Main.dust[dustIndex].noGravity = true;
                }
            }
        }

        private void XenoShield()
        {
            int chargeRate = 3;
            xenoShieldCap = shieldCapNormal;
            if (Main.expertMode)
            {
                chargeRate = 4;
                xenoShieldCap = shieldCapExpert;
            }
            if (Main.masterMode)
            {
                chargeRate = 5;
                xenoShieldCap = shieldCapMaster;
            }
            if (xenoSetBonus)
            {
                xenoShieldCooldown++;
                if (xenoShieldCooldown > 0)
                {
                    xenoChargeCounter++;
                    if (xenoShieldPower < 0)
                    {
                        xenoShieldPower = 0;
                    }
                    if (xenoChargeCounter >= 18)
                    {
                        xenoChargeFX++;
                        xenoChargeCounter = 0;
                        if ((xenoShieldPower + chargeRate) > xenoShieldCap)
                        {
                            SoundStyle haloShieldChargeEnd = new SoundStyle($"{nameof(ArtificerMod)}/Assets/HaloShieldRechargeEnd")
                            {
                                Volume = 0.9f,
                                PitchVariance = 0,
                                MaxInstances = 1,
                            };
                            if (xenoFinishFX)
                            {
                                xenoFinishFX = false;
                                if (ModContent.GetInstance<ConfigClient>().xenoHaloShields)
                                {
                                    SoundEngine.PlaySound(haloShieldChargeEnd, Player.Center);
                                }

                                for (int i = 0; i < 40; i++)
                                {
                                    Vector2 speed = Main.rand.NextVector2CircularEdge(0.75f, 1f);
                                    Dust d = Dust.NewDustPerfect(Player.Center + speed * 20, DustID.Electric, speed * 0.5f, Scale: 0.8f);
                                    d.noGravity = true;
                                }
                                for (int i = 0; i < 20; i++)
                                {
                                    Vector2 speed = Main.rand.NextVector2Circular(0.75f, 1f);
                                    Dust d = Dust.NewDustPerfect(Player.Center, DustID.Electric, speed * 2f, Scale: 0.9f);
                                    d.noGravity = true;
                                }
                            }
                            xenoShieldPower = xenoShieldCap;
                        }
                        else
                        {
                            xenoShieldPower += chargeRate;
                        }
                    }
                    if (xenoShieldCooldown == 18)
                    {
                        SoundStyle haloShieldCharge = new SoundStyle($"{nameof(ArtificerMod)}/Assets/HaloShieldRechargeStart")
                        {
                            Volume = 0.9f,
                            PitchVariance = 0,
                            MaxInstances = 1,
                        };
                        if (ModContent.GetInstance<ConfigClient>().xenoHaloShields)
                        {
                            SoundEngine.PlaySound(haloShieldCharge, Player.Center);
                        }
                    }
                    if (xenoChargeFX >= 5)
                    {
                        xenoChargeFX = 0;
                        if (xenoShieldPower < xenoShieldCap)
                        {
                            int dustCount = 10 + (int)(20f * ((float)xenoShieldPower / (float)xenoShieldCap));
                            for (int i = 0; i < dustCount; i++)
                            {
                                Vector2 speed = Main.rand.NextVector2CircularEdge(0.75f, 1f);
                                Dust d = Dust.NewDustPerfect(Player.Center + speed * 20, DustID.Electric, speed * 0, Scale: 0.6f);
                                Vector2 dustVelocity = (Player.Center - d.position).SafeNormalize(Vector2.Zero) * 1.25f;
                                d.velocity = dustVelocity;
                                d.noGravity = true;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 30; i++)
                            {
                                Vector2 speed = Main.rand.NextVector2CircularEdge(0.75f, 1f);
                                Dust d = Dust.NewDustPerfect(Player.Center + speed * 20, DustID.Electric, speed * 0.25f, Scale: 0.7f);
                                d.noGravity = true;
                            }
                        }
                    }
                }
            }
            else
            {
                xenoFinishFX = true;
                xenoShieldPower = 0;
                xenoChargeCounter = 0;
                xenoShieldCooldown = -282;
            }
        }
        private void XenoRegenPerk()
        {
            if(xenoShieldPower > 0)
            {
                Player.AddBuff(ModContent.BuffType<XenoShieldDisplay>(), 2);
                Player.lifeRegen += 1;
            }
            if(xenoShieldPower >= xenoShieldCap)
            {
                Player.lifeRegen += 1;
            }
        }
        private int XenoProtec(int damage, bool preventZeroReturn)
        {
            if (xenoShieldPower > 0)
            {
                // Shield has more charge than damage it is receiving; take no damage and grant i-frames. Reduce shield charge by the damage tanked
                if (damage < xenoShieldPower)
                {
                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, Player.width, Player.height), new Color(100, 200, 200, 255), damage, false);
                    xenoShieldPower -= damage;
                    Player.SetImmuneTimeForAllTypes(Player.longInvince ? 80 : 40);

                    SoundStyle haloShieldDmg1 = new SoundStyle($"{nameof(ArtificerMod)}/Assets/HaloShieldHit")
                    {
                        Volume = 0.75f,
                        PitchVariance = 0,
                        MaxInstances = 1,
                    };
                    if (ModContent.GetInstance<ConfigClient>().xenoHaloShields)
                    {
                        SoundEngine.PlaySound(haloShieldDmg1, Player.Center);
                    }
                    else
                    {
                        SoundEngine.PlaySound(SoundID.NPCDeath43, Player.Center);
                    }
                    for (int i = 0; i < 20; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2Circular(0.75f, 1f);
                        Dust d = Dust.NewDustPerfect(Player.Center, DustID.Electric, speed * 2f, Scale: 0.6f);
                        d.noGravity = true;
                    }

                    if (preventZeroReturn)
                    {
                        return 1;
                    }
                    return 0;
                }

                // damage equals or exceeds damage; shield breaks and damage is reduced by what charge the shield had
                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, Player.width, Player.height), new Color(100, 200, 200, 255), xenoShieldPower, true);
                damage -= xenoShieldPower;
                xenoShieldPower = 0;

                SoundStyle haloShieldDmg2 = new SoundStyle($"{nameof(ArtificerMod)}/Assets/HaloShieldHit")
                {
                    Volume = 1f,
                    PitchVariance = 0,
                    MaxInstances = 1,
                };
                if (ModContent.GetInstance<ConfigClient>().xenoHaloShields)
                {
                    SoundEngine.PlaySound(haloShieldDmg2, Player.Center);
                }
                else
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath45, Player.Center);
                }
                for (int i = 0; i < 45; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(0.75f, 1f);
                    Dust d = Dust.NewDustPerfect(Player.Center, DustID.Electric, speed * 4f, Scale: 0.75f);
                    d.noGravity = true;
                }
            }
            return damage;
        }
        /// <summary>
		/// Variant of the XenoProtec method than instead returns a bool corresponding to if the inflicted damage is fully absorbed or not.
		/// </summary>
        private bool XenoFullProtec(int damage)
        {
            if (xenoShieldPower > 0)
            {
                if (damage < xenoShieldPower)
                {
                    return true;
                }
            }
            return false;
        }

        private void NovaBoost()
        {
            int cooldownIndex = Player.FindBuffIndex(ModContent.BuffType<AbilityCooldown>());
            if (astralSetBonus && cooldownIndex != -1)
            {
                float coolTime = Player.buffTime[cooldownIndex];

                // +.004% per tick. Max of +30% at 90 seconds
                float astralBoost = coolTime * (.3f / 5400f);
                if (astralBoost > .3f)
                {
                    astralBoost = .3f;
                }

                Player.GetDamage(DamageClass.Generic) += astralBoost;
                Player.endurance = 1f - ((1f - astralBoost) * (1f - Player.endurance));
            }
        }
        private void NovaVisual()
        {
            if (astralSetBonus)
            {
                Vector2 secondaryVelocity = new Vector2(10f);
                int splitOffset = Main.rand.Next(0, 360);
                for (int i = 0; i < 5; i++)
                {
                    Vector2 perturbedSpeed = secondaryVelocity.RotatedBy(MathHelper.ToRadians((360f / 5f) * (i + 1f)) + MathHelper.ToRadians(splitOffset));
                    for (int k = 0; k < 15; k++)
                    {
                        int dustID;
                        switch (Main.rand.Next(4))
                        {
                            case 0:
                                dustID = DustID.YellowStarDust;
                                break;
                            case 1:
                                dustID = DustID.FireworkFountain_Yellow;
                                break;
                            case 2:
                                dustID = DustID.Firework_Yellow;
                                break;
                            default:
                                dustID = DustID.YellowTorch;
                                break;
                        }

                        Dust dust = Main.dust[Dust.NewDust(Player.Center, 0, 0, dustID, perturbedSpeed.X, perturbedSpeed.Y)];
                        dust.velocity *= .075f * k;
                        dust.noGravity = true;
                        dust.scale = Main.rand.Next(15, 30) * 0.005f * (16 - k);
                    }
                }
            }
        }

        // Accessory Methods:

        private void IllusionMotherboardConfuse(int dmg)
        {
            // Adapted from Brain of Confsuion's confuse effect
            for (int i = 0; i < 200; i++)
            {
                if (!Main.npc[i].active || Main.npc[i].friendly)
                {
                    continue;
                }
                int confuseChance = 450 + (int)(1.75f * dmg);
                if (Main.rand.Next(600) < confuseChance)
                {
                    float targetDist = (Main.npc[i].Center - Player.Center).Length();
                    float range = Main.rand.Next(200 + (int)dmg, 301 + (int)dmg * 4);
                    if (range > 500f)
                    {
                        range = 500f + (range - 500f) * 0.75f;
                    }
                    if (range > 700f)
                    {
                        range = 700f + (range - 700f) * 0.5f;
                    }
                    if (range > 900f)
                    {
                        range = 900f + (range - 900f) * 0.25f;
                    }
                    if (targetDist < range)
                    {
                        float duration = Main.rand.Next(90 + dmg, 300 + dmg);
                        Main.npc[i].AddBuff(BuffID.Confused, (int)duration);
                    }
                }
            }
        }

        private void BlueShellCheck()
        {
            bool lowHP = Player.statLife < (int)(Player.statLifeMax2 / 2f);
            bool ownerNet = Player == Main.LocalPlayer;
            bool cooldown = blueShellCooldown <= 0;
            bool hasShellEquip = pShellEquip != null;
            bool noShellSpawned = Player.ownedProjectileCounts[ModContent.ProjectileType<BlueShell>()] <= 0;
            if (lowHP && ownerNet && cooldown && hasShellEquip && noShellSpawned)
            {
                int dmg = 60;
                if (Main.expertMode)
                {
                    dmg = 120;
                }
                if (Main.masterMode)
                {
                    dmg = 180;
                }
                Projectile.NewProjectile(Player.GetSource_Accessory(pShellEquip), Player.Center, Vector2.Zero, ModContent.ProjectileType<BlueShell>(), dmg, 10f, Player.whoAmI);
            }
        }

        private void TerraShieldWard() 
        {
            if (terraShield)
            {
                if (Player.statLife > Player.statLifeMax2 / 4) // Above 25% HP; grant to other players
                {
                    float maxDetectDistance = 1000;
                    float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
                    for (int k = 0; k < Main.maxPlayers; k++)
                    {
                        // If the player is in range and alive, grant them buffs
                        float sqrDistanceToTarget = Vector2.DistanceSquared(Main.player[k].Center, Player.Center);
                        if (sqrDistanceToTarget < sqrMaxDetectDistance)
                        {
                            if (Main.player[k].active && !Main.player[k].dead && !Player.dead
                                && !Main.player[k].HasBuff(ModContent.BuffType<TerraCooldown>())
                                && k != Player.whoAmI
                                && ((!Player.hostile && !Main.player[k].hostile) || Player.team == Main.player[k].team))
                            {
                                Main.player[k].AddBuff(ModContent.BuffType<LastStand>(), 2);
                            }
                        }
                    }
                }
                else // <= 25% HP; only grant to self
                {
                    if (!Player.HasBuff(ModContent.BuffType<TerraCooldown>()))
                    {
                        Player.AddBuff(ModContent.BuffType<LastStand>(), 2);
                    }
                }
            }
        }
        private bool TerraLastStand()
        {
            if (Player.HasBuff(ModContent.BuffType<LastStand>()) && !Player.HasBuff(ModContent.BuffType<TerraCooldown>()))
            {
                Player.statLife = 1; 

                if (terraShield)
                {
                    Player.AddBuff(ModContent.BuffType<TerraCooldown>(), 2700);
                }
                else
                {
                    Player.AddBuff(ModContent.BuffType<TerraCooldown>(), 3600);
                }

                Player.AddBuff(BuffID.ParryDamageBuff, 300);
                Player.AddBuff(ModContent.BuffType<TerraRevive>(), 300);

                SoundEngine.PlaySound(SoundID.NPCHit4, Player.position); 
                Player.SetImmuneTimeForAllTypes(Player.longInvince ? 100 : 60); 

                for (int i = 0; i < 30; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(Player.Center + speed * 15, DustID.Terra, speed * 10f, Scale: 1f);
                    d.noGravity = true;
                }

                return true;
            }
            return false;
        }

        private void PrimeGauntletBones()
        {
            if (primeEquip != null && primeBoneCooldown <= 0 && Player.HeldItem != null && Player == Main.LocalPlayer
                && Player.itemAnimation > 0 && Player.ItemAnimationJustStarted)
            {
                if (Player.HeldItem.damage > 0)
                {
                    if (Player.HasBuff(ModContent.BuffType<PrimeRage>()))
                    {
                        Vector2 boneVelocity = SimpleProjVelocity(12f, Player.Center);
                        Projectile.NewProjectile(Player.GetSource_Accessory(primeEquip), Player.Center, boneVelocity, ModContent.ProjectileType<PlasmaCrossbones>(), 80, 5f, Player.whoAmI);
                    }
                    else
                    {
                        Vector2 boneVelocity = SimpleProjVelocity(8f, Player.Center);
                        Projectile.NewProjectile(Player.GetSource_Accessory(primeEquip), Player.Center, boneVelocity, ModContent.ProjectileType<MetalCrossbones>(), 40, 5f, Player.whoAmI);
                    }
                    primeBoneCooldown = 45;
                    if (Player.itemAnimation > primeBoneCooldown)
                    {
                        primeBoneCooldown = Player.itemAnimation;
                    }
                }
            }
        }
        private void TwinShieldLaser()
        {
            if (tShieldEquip != null && twinLaserCooldown <= 0 && Player == Main.LocalPlayer
                && Player.dashDelay <= 0)
            {
                NPC target = null;
                float maxDetectDistance = 1000f;
                float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
                for (int i = 0; i < 200; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.CanBeChasedBy())
                    {
                        Vector2 aimVector = nPC.Center - Player.Center;
                        float sqrDistanceToTarget = Vector2.DistanceSquared(nPC.Center, Player.Center);
                        bool inSight = true;
                        float aimAngle = Math.Abs(aimVector.ToRotation());
                        if (Player.direction == 1 && (double)aimAngle > MathHelper.ToRadians(40f))
                        {
                            inSight = false;
                        }
                        else if (Player.direction == -1 && (double)aimAngle < MathHelper.ToRadians(80f))
                        {
                            inSight = false;
                        }
                        if (sqrDistanceToTarget < sqrMaxDetectDistance
                            && Collision.CanHitLine(Player.Center, 0, 0, nPC.position, nPC.width, nPC.height)
                            && inSight)
                        {
                            sqrMaxDetectDistance = sqrDistanceToTarget;
                            target = nPC;
                        }
                    }
                }
                if (target != null)
                {
                    Vector2 laserVelocity = (target.Center - Player.Center).SafeNormalize(Vector2.Zero) * 15f;
                    float dmg = Player.GetTotalDamage(DamageClass.Ranged).ApplyTo(30f);
                    float kb = Player.GetTotalKnockback(DamageClass.Ranged).ApplyTo(4.5f);
                    Projectile.NewProjectile(Player.GetSource_Accessory(tShieldEquip), Player.Center, laserVelocity, ModContent.ProjectileType<TwinLaser>(), (int)dmg, kb, Player.whoAmI);
                    SoundEngine.PlaySound(SoundID.Item12 with { Volume = 0.5f}, Player.Center); 
                }
                twinLaserCooldown = 60;
            }

        }

        private void LowGravResist()
        {
            if (heavyShoes)
            {
                // This code - adapted from vanilla source - determines gravity loss from high altitudes
                // It has been modified to counteract the loss by 50%, as a benefit of wearing an accessory in the Heavy Boots line
                float altMult = (float)Main.maxTilesX / 4200f;
                altMult *= altMult;
                float gravMult = (float)((double)(Player.position.Y / 16f - (60f + 10f * altMult)) / (Main.worldSurface / 6.0));
                if (Main.remixWorld)
                {
                    gravMult = (float)((double)(Player.position.Y / 16f - (60f + 10f * altMult)) / (Main.worldSurface / 1.0));
                }

                if (gravMult < 1f)
                {
                    // Lower cap
                    if (Main.remixWorld)
                    {
                        if ((double)gravMult < 0.1)
                        {
                            gravMult = 0.1f;
                        }
                    }
                    else if ((double)gravMult < 0.25)
                    {
                        gravMult = 0.25f;
                    }

                    // Invert loss into a partial gain, stacking onto vanilla's gravity loss to result in a total reduction that's weaker than normal
                    gravMult *= 1.5f;
                    gravMult = (1f / gravMult);

                    if (gravMult > 1f)
                    {
                        Player.gravity *= gravMult;
                    }
                }
            }
        }

        private bool LuckyCritReroll(DamageClass damageType)
        {
            float baseCritChance;
            if (Player.HeldItem == null) // If there is no weapon avalible, use the class's crit chance
            {
                baseCritChance = Player.GetTotalCritChance(damageType);
            }
            else 
            {
                baseCritChance = Player.GetWeaponCrit(Player.HeldItem);
            }
            
            // Simulate a crit check with the chance fetched. A 'reroll' will be perfromed if the check fails
            float simCritChance = Math.Clamp(baseCritChance, 1f, 99f);
            bool simmedCrit = Main.rand.NextFloat(100f) < simCritChance;

            if (!simmedCrit && Main.rand.NextFloat() < Player.luck)
            {
                float rerollCritChance = baseCritChance * (0.5f + (0.45f * Player.luck));

                rerollCritChance = Math.Clamp(rerollCritChance, Math.Clamp(10f * Player.luck, 1f, 10f), 99f);
                
                return Main.rand.Next(100) < rerollCritChance;
            }

            return false;
        }

        private void AutoHealthPot()
        {
            if (autoHeal && Player.statLife < Player.statLifeMax2 && Player.potionDelay <= 0 && !Player.dead)
            {
                Item checkPot = Player.QuickHeal_GetItemToUse(); // Get the 'appropriate' potion
                if (checkPot == null) // null will be returned if there's no potions to consume, so stop in that case
                {
                    return;
                }
                if (Player.statLifeMax2 - checkPot.healLife < Player.statLife) // Avoid over-healing and wasting part of the potion's heal
                {
                    return;
                }

                // Proceed with an auto-heal. Code adapted from the QuickHeal hook from vanilla
                SoundEngine.PlaySound(checkPot.UseSound, Player.position);
                if (checkPot.potion)
                {
                    PotionDelay(checkPot);
                }
                PotionRestore(checkPot);
                if (checkPot.type == ItemID.Mushroom)
                {
                    Player.TryToResetHungerToNeutral();
                }
                if (checkPot.buffType > 0)
                {
                    int buffTime = checkPot.buffTime;
                    if (buffTime == 0)
                    {
                        buffTime = 3600;
                    }
                    Player.AddBuff(checkPot.buffType, buffTime);
                }
                if (checkPot.consumable && ItemLoader.ConsumeItem(checkPot, Player))
                {
                    checkPot.stack--;
                }
                if (checkPot.stack <= 0)
                {
                    checkPot.TurnToAir();
                }
                // Handles the 'Unusual Survival Strategies' achievement
                if (Main.myPlayer == Player.whoAmI && checkPot.type == ItemID.BottledWater && Player.breath == 0)
                {
                    AchievementsHelper.HandleSpecialEvent(Player, 25);
                }
                // Updates avalible recipes, in case the potion consumed was the last remaining
                Recipe.FindRecipes();
            }
        }

        /// <summary>
		/// Used by the Life Well's autouse of health potions. Adapted from vanilla hook 'ApplyPotionDelay'.
		/// </summary>
        private void PotionDelay(Item potion)
        {
            if (potion.type == ItemID.StrangeBrew)
            {
                int minValue = 2400;
                int num = 4200;
                Player.potionDelay = Main.rand.Next(minValue, num + 1);
                if (Player.pStone)
                {
                    Player.potionDelay = (int)((float)Player.potionDelay * Player.PhilosopherStoneDurationMultiplier);
                }
                Player.AddBuff(BuffID.PotionSickness, Player.potionDelay);
            }
            else if (potion.type == ItemID.RestorationPotion)
            {
                Player.potionDelay = Player.restorationDelayTime;
                Player.AddBuff(BuffID.PotionSickness, Player.potionDelay);
            }
            else if (potion.type == ItemID.Mushroom)
            {
                Player.potionDelay = Player.mushroomDelayTime;
                Player.AddBuff(BuffID.PotionSickness, Player.potionDelay);
                Player.TryToResetHungerToNeutral();
            }
            else
            {
                Player.potionDelay = Player.potionDelayTime;
                Player.AddBuff(BuffID.PotionSickness, Player.potionDelay);
            }
        }

        /// <summary>
        /// Used by the Life Well's autouse of health potions. Adapted from vanilla hook 'ApplyLifeAndOrMana'.
        /// </summary>
        private void PotionRestore(Item potion)
        {
            int num = Player.GetHealLife(potion, quickHeal: true);
            int healMana = Player.GetHealMana(potion, quickHeal: true);
            if (potion.type == ItemID.StrangeBrew)
            {
                int healLife = potion.healLife;
                int num2 = 120;
                num = Main.rand.Next(healLife, num2 + 1);
                if (Main.myPlayer == Player.whoAmI)
                {
                    float num3 = Main.rand.NextFloat();
                    int num4 = 0;
                    if (num3 <= 0.1f)
                    {
                        num4 = 240;
                    }
                    else if (num3 <= 0.3f)
                    {
                        num4 = 120;
                    }
                    else if (num3 <= 0.6f)
                    {
                        num4 = 60;
                    }
                    if (num4 > 0)
                    {
                        Player.SetImmuneTimeForAllTypes(num4);
                    }
                }
            }
            Player.statLife += num;
            Player.statMana += healMana;
            if (Player.statLife > Player.statLifeMax2)
            {
                Player.statLife = Player.statLifeMax2;
            }
            if (Player.statMana > Player.statManaMax2)
            {
                Player.statMana = Player.statManaMax2;
            }
            if (num > 0 && Main.myPlayer == Player.whoAmI)
            {
                Player.HealEffect(num);
            }
            if (healMana > 0)
            {
                Player.AddBuff(BuffID.ManaSickness, Player.manaSickTime);
                if (Main.myPlayer == Player.whoAmI)
                {
                    Player.ManaEffect(healMana);
                }
            }
        }

        /// <summary>
        /// Used by the Steampunk Stopwatch to auto-trigger abilities.
        /// </summary>
        private void ClockworkTrigger()
        {
            if(clockworkBonus && !Player.HasBuff(ModContent.BuffType<AbilityCooldown>()) && inCombatTimer > 0)
            {
                if(Player.active && !Player.dead
                && Main.myPlayer == Player.whoAmI)
                {
                    ActivateAbilities();
                }
            }
        }

        // Accessory Abilities Methods:

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if ((KeybindSystem.ActivatedAbility.JustPressed || KeybindSystem.AbilityExtraHotkey.JustPressed)
                && Player.active && !Player.dead
                && Main.myPlayer == Player.whoAmI
                && !Player.HasBuff(ModContent.BuffType<AbilityCooldown>()))
            {
                ActivateAbilities();
            }
        }

        /// <summary>
        /// Triggers abilities. The full effects of each ability is typically split off into their own methods.
        /// For the effects of a certain ability, check the corresponding accessory's tooltip in-game.
        /// </summary>
        private void ActivateAbilities()
        {
            // Trigger dust effect as visual feedback for Nova Armor's set bonus, so long as ability accessory is equipped
            if (abilityAcc)
            {
                if (bBulwark)
                {
                    if (BulwarkSpawnCheck(Main.MouseWorld))
                    {
                        NovaVisual();
                    }
                }
                else
                {
                    NovaVisual();
                }
            }

            // Abilities are checked for in a hard-coded manner
            // This prevents players from being able to trigger multiple abilities at once if they're somehow able to get more than 1 ability accessory equipped
            if (fCharm)
            {
                ForebodingCharmAttack();
                AddCooldown(50);
            }
            else if (stealthShroud)
            {
                StealthFX();
                Player.AddBuff(ModContent.BuffType<StealthShroudBuff>(), 600);
                AddCooldown(30);
            }
            else if (flamboyantCape)
            {
                Player.AddBuff(ModContent.BuffType<FlamboyantCapeBuff>(), 600);
                AddCooldown(25);
            }
            else if(xRadar)
            {
                XRadarReveal(1000f);
                AddCooldown(10);
            }
            else if (debuffRemovalAcc > 0)
            {
                RemoveDebuffs(debuffRemovalAcc);
                AddCooldown(10);
            }
            else if (spikeManacle)
            {
                SoundEngine.PlaySound(SoundID.NPCHit4, Player.position);
                Player.AddBuff(ModContent.BuffType<SpikeShield>(), 600);
                AddCooldown(30);
            }
            else if (manaStimType > 0)
            {
                StimActivate(false, manaStimType);
                AddCooldown(25);
            }
            else if (manaCharger)
            {
                manaChargerSuccess = true;

                Player.AddBuff(ModContent.BuffType<ManaChargerBuff>(), 600);
                AddCooldown(30);
            }
            else if (hpStimType > 0)
            {
                StimActivate(true, hpStimType);
                AddCooldown(60);
            }
            else if (lifeCharger)
            {
                lifeChargerSuccess = true;

                Player.AddBuff(ModContent.BuffType<LifeChargerBuff>(), 600);
                AddCooldown(60);
            }
            else if (ornateRing)
            {
                OrnateFlash(200f);
                AddCooldown(15);
            }
            else if (repulseCharge)
            {
                DropRepulse();
                AddCooldown(20);
            }
            else if (soulEmulator)
            {
                EmulatorBuffs(1000f);
                AddCooldown(25);
            }
            else if (luckyPresent)
            {
                LuckyPresentBuffs();
                AddCooldown(30);
            }
            else if (turboBoots)
            {
                Player.AddBuff(ModContent.BuffType<TurboBoost>(), 300);
                AddCooldown(15);
            }
            else if (repulseRocket)
            {
                RepulseRocket();
                AddCooldown(15);
            }
            else if (rocketLeg)
            {
                rocketJumpTimer = 10;
                AddCooldown(10);
            }
            else if (overdriveLeg)
            {
                Player.AddBuff(ModContent.BuffType<OverdriveBoost>(), 300);
                AddCooldown(20);
            }
            else if (youmeanthechaosemeralds)
            {
                ChaosGem();
                AddCooldown(15);
            }
            else if (swaggerCape)
            {
                Player.AddBuff(ModContent.BuffType<SwaggerCapeBuff>(), 600);
                SwagTaunt(1000f);
                AddCooldown(45);
            }
            else if (shadowShroud)
            {
                StealthFX();
                Player.AddBuff(ModContent.BuffType<ShadowShroudBuff>(), 600);
                AddCooldown(45);
            }
            else if(tetherLeech > 0)
            {
                LaunchTether();
                AddCooldown(60);
            }
            else if(needleAttack > 0)
            {
                switch (needleAttack)
                {
                    case 1:
                        needlesToShoot = 1;
                        AddCooldown(10);
                        break;
                    case 2:
                        needlesToShoot = 8;
                        AddCooldown(15);
                        break;
                    case 3:
                        needlesToShoot = 25;
                        AddCooldown(20);
                        break;
                    default:
                        break;
                }
            }
            else if (artificerArmor)
            {
                ArtificerArmors(false);
                AddCooldown(60);
            }
            else if (blaster)
            {
                StarplateBlaster();
                AddCooldown(30);
            }
            else if (bBulwark)
            {
                if (BulwarkSpawnCheck(Main.MouseWorld))
                {
                    BulwarkSummon(Main.MouseWorld);
                    AddCooldown(40);
                }
            }
            else if (lostShield)
            {
                Player.AddBuff(ModContent.BuffType<ChampionProtec>(), 60);
                AddCooldown(20);
            }
            else if (funiDungeonsArtifact)
            {
                MCDomesticBombingMoment(320);
                AddCooldown(50);
            }
            else if (primeGauntlet)
            {
                Player.AddBuff(ModContent.BuffType<PrimeRage>(), 300);
                SoundEngine.PlaySound(SoundID.Item113, Player.position);
                AddCooldown(90);
            }
            else if (retributionStoker)
            {
                Player.AddBuff(ModContent.BuffType<RetributionBuff>(), 900);
                SetRetributionPow();
                AddCooldown(60);
            }
            else if (arcanum)
            {
                ArcanumMissiles();
                AddCooldown(45);
            }
            else if (prism)
            {
                FirstPrism();
                AddCooldown(40);
            }
            else if (fearReaper)
            {
                SpawnReapers();
                AddCooldown(60);
            }
            else if (nanomachinesSon)
            {
                Player.AddBuff(ModContent.BuffType<CyborgCoreBuff>(), 300);
                AddCooldown(45);
            }
            else if (blaster2)
            {
                SolarBlaster();
                AddCooldown(30);
            }
            else if (probePack)
            {
                SpawnProbes();
                AddCooldown(60);
            }
            else if (superArm)
            {
                Player.AddBuff(ModContent.BuffType<Supercharged>(), 600);
                AddCooldown(45);
            }
            else if (powerArmor)
            {
                ArtificerArmors(true);
                AddCooldown(60);
            }
            else if (prosthetics)
            {
                Player.AddBuff(ModContent.BuffType<ProstheticsBuff>(), 600);
                AddCooldown(45);
            }
            else if (missileArray)
            {
                missilesToShoot = 25;
                AddCooldown(40);
            }
            else if (obliterationCore)
            {
                ShootDeathray();
                AddCooldown(50);
            }
            else if (arcanum2)
            {
                PrismaticArcanum();
                AddCooldown(45);
            }
            else if (seymoursBizzareAdventure)
            {
                AuroraBorealis();
                AddCooldown(40);
            }
            else if (lienAcc)
            {
                knifeWaves = 5;
                AddCooldown(90);
            }
            else if (stellarResonator)
            {
                ResonatorTrigger();
                AddCooldown(50);
            }
            else if (orbitalStrike)
            {
                MoonfallOribtalStrike();
                AddCooldown(120);
            }
            else if (starwriter)
            {
                StarwriterActivate();
                AddCooldown(120);
            }
        }

        /// <summary>
        /// Returns a vector pointed towards the cursor from the given origin, with a length corresponding to the given velocity.
        /// </summary>
        /// <param name="velocity">Determines the length of the resultant vector; this corresponds to speed for projectiles.</param>
        /// <param name="origin">The vector location the vector should point from.</param>
        private Vector2 SimpleProjVelocity(float velocity, Vector2 origin)
        {
            if(Player == Main.LocalPlayer)
            {
                return (Main.MouseWorld - origin).SafeNormalize(Vector2.Zero) * velocity;
            }
            return Vector2.Zero;
        }

        private void StarwriterActivate()
        {
            SoundEngine.PlaySound(SoundID.Item4, Player.Center);
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<StarwriterSavePos>()] < 1 && Main.myPlayer == Player.whoAmI)
            {
                Projectile.NewProjectile(Player.GetSource_Accessory(starwriterEquip), Player.Center, new Vector2(0f, 0f), ModContent.ProjectileType<StarwriterSavePos>(), 0, 0f, Player.whoAmI, Player.statLife, Player.statMana);
            }
            else if (Player.ownedProjectileCounts[ModContent.ProjectileType<StarwriterSavePos>()] == 1)
            {
                bool abilityUsed = false;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile target = Main.projectile[i];
                    if (target.type == ModContent.ProjectileType<StarwriterSavePos>() && target.owner == Player.whoAmI)
                    {
                        if (abilityUsed == false)
                        {
                            abilityUsed = true;
                            Player.Teleport(target.position, 6);
                            if (target.ai[0] >= 1)
                            {
                                Player.statLife = (int)target.ai[0];
                                if (Player.statLife > Player.statLifeMax2)
                                {
                                    Player.statLife = Player.statLifeMax2;
                                }
                            }
                            if (target.ai[1] >= 1)
                            {
                                Player.statMana = (int)target.ai[1];
                                if (Player.statMana > Player.statManaMax2)
                                {
                                    Player.statMana = Player.statManaMax2;
                                }
                            }
                            
                            Player.AddBuff(BuffID.PotionSickness, 3600);
                            Player.AddBuff(BuffID.ManaSickness, 600);
                        }
                        
                        target.Kill();
                    }
                }
            }
        }

        private void ArtificerArmors(bool powerArmor)
        {
            SoundEngine.PlaySound(SoundID.DD2_DefenseTowerSpawn, Player.Center);
            if (powerArmor)
            {
                Player.AddBuff(ModContent.BuffType<PowerArmorBuff>(), 600);
                for (int i = 0; i < 60; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(7.5f, 7.5f);
                    Dust d = Dust.NewDustPerfect(Player.Center, DustID.Electric, speed, Scale: 1.1f);
                    d.noGravity = true;
                }
            }
            else
            {
                Player.AddBuff(ModContent.BuffType<ArtificerArmorBuff>(), 600);
                for (int i = 0; i < 60; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(7.5f, 7.5f);
                    Dust d = Dust.NewDustPerfect(Player.Center, DustID.MagicMirror, speed, Scale: 1.1f);
                    d.noGravity = true;
                }
            }
        }

        private void StimActivate(bool hp, int tier)
        {
            if (hp)
            {
                switch (tier)
                {
                    case 1:
                        Player.AddBuff(ModContent.BuffType<HPStim1>(), 300);
                        break;
                    case 2:
                        Player.AddBuff(ModContent.BuffType<HPStim2>(), 300);
                        break;
                    case 3:
                        Player.AddBuff(ModContent.BuffType<HPStim3>(), 300);
                        break;
                    case 4:
                        Player.AddBuff(ModContent.BuffType<HPStim4>(), 300);
                        break;
                    default:
                        break;
                }
                Player.AddBuff(BuffID.PotionSickness, 300);
                stimTimerH = 1;
            }
            else
            {
                switch (tier)
                {
                    case 1:
                        Player.AddBuff(ModContent.BuffType<ManaStim1>(), 300);
                        break;
                    case 2:
                        Player.AddBuff(ModContent.BuffType<ManaStim2>(), 300);
                        break;
                    case 3:
                        Player.AddBuff(ModContent.BuffType<ManaStim3>(), 300);
                        break;
                    case 4:
                        Player.AddBuff(ModContent.BuffType<ManaStim4>(), 300);
                        break;
                    default:
                        break;
                }

                Player.AddBuff(BuffID.ManaSickness, 300);
                stimTimerM = 1;
            }
        }
        private void Stims()
        {
            // Clears active stim buffs if a stim of the corresponding type isn't equipped
            if (healthStim <= 0)
            {
                Player.ClearBuff(ModContent.BuffType<HPStim1>());
                Player.ClearBuff(ModContent.BuffType<HPStim2>());
                Player.ClearBuff(ModContent.BuffType<HPStim3>());
                Player.ClearBuff(ModContent.BuffType<HPStim4>());
            }
            if (manaStim <= 0)
            {
                Player.ClearBuff(ModContent.BuffType<ManaStim1>());
                Player.ClearBuff(ModContent.BuffType<ManaStim2>());
                Player.ClearBuff(ModContent.BuffType<ManaStim3>());
                Player.ClearBuff(ModContent.BuffType<ManaStim4>());
            }
            stimTimerH--;
            stimTimerM--;
            if (stimTimerH <= 0)
            {
                if (healthStim > 0)
                {
                    int hpRate = 0;
                    if (healthStim == 1)
                    {
                        hpRate = 2;
                        stimTimerH = 15;
                    }
                    else if (healthStim == 2)
                    {
                        hpRate = 3;
                        stimTimerH = 15;
                    }
                    else if (healthStim == 3)
                    {
                        hpRate = 4;
                        stimTimerH = 15;
                    }
                    else if (healthStim == 4)
                    {
                        hpRate = 5;
                        stimTimerH = 15;
                    }
                    Player.statLife += hpRate;
                    if (Player.statLife > Player.statLifeMax2)
                    {
                        Player.statLife = Player.statLifeMax2;
                    }
                }
                else
                {
                    stimTimerH = 30;
                }

            }
            if (stimTimerM <= 0)
            {
                if (manaStim > 0)
                {
                    int manaRate = 0;
                    if (healthStim == 1)
                    {
                        manaRate = 1;
                        stimTimerM = 10;
                    }
                    else if (healthStim == 2)
                    {
                        manaRate = 2;
                        stimTimerM = 10;
                    }
                    else if (healthStim == 3)
                    {
                        manaRate = 3;
                        stimTimerM = 10;
                    }
                    else if (healthStim == 4)
                    {
                        manaRate = 4;
                        stimTimerM = 10;
                    }
                    Player.statMana += manaRate;
                    if (Player.statMana > Player.statManaMax2)
                    {
                        Player.statMana = Player.statManaMax2;
                    }
                }
                else
                {
                    stimTimerM = 15;
                }
            }
            StimVisuals(healthStim > 0, manaStim > 0, hpStimType, manaStimType);
        }
        private void StimVisuals(bool doHP, bool doMana, int tierH, int tierM)
        {
            if (doHP)
            {
                int dustDenom = 4;
                float dustSize = 1f;
                if (tierH == 1)
                {
                    dustDenom = 4;
                    dustSize = 0.8f;
                }
                else if (tierH == 2)
                {
                    dustDenom = 3;
                    dustSize = 0.95f;
                }
                else if (tierH == 3)
                {
                    dustDenom = 2;
                    dustSize = 1.05f;
                }
                else if (tierH == 4)
                {
                    dustDenom = 1;
                    dustSize = 1.1f;
                }
                if (Main.rand.NextBool(dustDenom))
                {
                    Dust dust;
                    dust = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, DustID.HealingPlus, 0f, 0f, 0, new Color(255, 0, 0), dustSize)];
                    dust.noGravity = true;
                }
            }
            if (doMana)
            {
                int dustDenom = 4;
                float dustSize = 1f;
                if (tierM == 1)
                {
                    dustDenom = 4;
                    dustSize = 0.8f;
                }
                else if (tierM == 2)
                {
                    dustDenom = 3;
                    dustSize = 0.95f;
                }
                else if (tierM == 3)
                {
                    dustDenom = 2;
                    dustSize = 1.05f;
                }
                else if (tierM == 4)
                {
                    dustDenom = 1;
                    dustSize = 1.1f;
                }
                if (Main.rand.NextBool(dustDenom))
                {
                    Dust dust;
                    dust = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, DustID.ManaRegeneration, 0f, 0f, 130, default(Color), dustSize)];
                    dust.noGravity = true;
                }
            }
        }

        private void Chargers()
        {
            if (!lifeCharger)
            {
                lifeChargerSuccess = false;
                Player.ClearBuff(ModContent.BuffType<LifeChargerBuff>());
            }
            if (!manaCharger)
            {
                manaChargerSuccess = false;
                Player.ClearBuff(ModContent.BuffType<ManaChargerBuff>());
            }
            StimVisuals(lifeChargerActive, manaChargerActive, 4, 4);
        }

        private void OrnateFlash(float maxDetectionDistance)
        {
            float sqrMaxDetectDistance = maxDetectionDistance * maxDetectionDistance;
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];

                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Player.Center);
                if (sqrDistanceToTarget < sqrMaxDetectDistance)
                {
                    target.AddBuff(BuffID.Confused, 300);
                }
            }
            
            for (int j = 0; j < Main.maxPlayers; j++)
            {
                Player target = Main.player[j];

                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Player.Center);
                if (sqrDistanceToTarget < sqrMaxDetectDistance)
                {
                    if (target.active && !target.dead)
                    {
                        target.AddBuff(ModContent.BuffType<OrnateSpark>(), 300);
                    }
                }
            }
            
            SoundEngine.PlaySound(SoundID.Item4, Player.Center);
            // Gets the player's hand position; code adapted from Nebula Blaze's FX
            if (Main.dedServ)
            {
                return;
            }
            Vector2 handOffset = Main.OffsetsPlayerOnhand[Player.bodyFrame.Y / 56] * 2f;
            if (Player.direction != 1)
            {
                handOffset.X = (float)Player.bodyFrame.Width - handOffset.X;
            }
            if (Player.gravDir != 1f)
            {
                handOffset.Y = (float)Player.bodyFrame.Height - handOffset.Y;
            }
            handOffset -= new Vector2((float)(Player.bodyFrame.Width - Player.width), (float)(Player.bodyFrame.Height - 42)) / 2f;
            Vector2 rotatedHandPosition = Player.RotatedRelativePoint(Player.MountedCenter - new Vector2(20f, 42f) / 2f + handOffset) - Player.velocity;

            if (Player == Main.LocalPlayer && ornateEquip != null)
            {
                float numberProjectiles = 4;

                Vector2 flashVelocity = new Vector2(35f);

                float rotOffset = Main.rand.Next(0, 360);

                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = flashVelocity.RotatedBy(MathHelper.ToRadians((360f / 4f) * (i + 1f)) + MathHelper.ToRadians(rotOffset));
                    int proj = Projectile.NewProjectile(Player.GetSource_Accessory(arcanumEquip), rotatedHandPosition, perturbedSpeed, ModContent.ProjectileType<OrnateRingFlash>(), 0, 0f, Player.whoAmI);
                    Main.projectile[proj].Center = rotatedHandPosition;
                }
            }

            for (int j = 0; j < 60; j++)
            {
                int dustType = DustID.MagicMirror;
                int dustChoice = Main.rand.Next(3);
                if(dustChoice == 0)
                {
                    dustType = DustID.Enchanted_Gold;
                }
                else if(dustChoice == 1)
                {
                    dustType = DustID.Enchanted_Pink;
                }
                Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
                Dust d3 = Dust.NewDustPerfect(rotatedHandPosition, dustType, speed * 15f, Scale: 1.25f);
                d3.noGravity = true;
            }
        }

        private void RemoveDebuffs(int accType)
        {
            if (accType == 1 || accType == 3 || accType == 4)
            {
                if (Player.HasBuff(BuffID.Poisoned) ||
                    Player.HasBuff(BuffID.Venom))
                {
                    Player.AddBuff(ModContent.BuffType<Refresh>(), 100);
                }

                Player.ClearBuff(BuffID.Poisoned);
                Player.ClearBuff(BuffID.Venom);
            }
            if (accType == 2 || accType == 3 || accType == 4)
            {
                if (Player.HasBuff(BuffID.OnFire) ||
                    Player.HasBuff(BuffID.Frostburn) ||
                    Player.HasBuff(BuffID.CursedInferno) ||
                    Player.HasBuff(BuffID.ShadowFlame))
                {
                    Player.AddBuff(ModContent.BuffType<Refresh>(), 100);
                }

                Player.ClearBuff(BuffID.OnFire);
                Player.ClearBuff(BuffID.Frostburn);
                Player.ClearBuff(BuffID.CursedInferno);
                Player.ClearBuff(BuffID.ShadowFlame);
            }
            if (accType == 4)
            {
                if (Player.HasBuff(BuffID.OnFire) ||
                    Player.HasBuff(BuffID.Frostburn) ||
                    Player.HasBuff(BuffID.CursedInferno) ||
                    Player.HasBuff(BuffID.ShadowFlame) ||
                    Player.HasBuff(BuffID.Silenced) ||
                    Player.HasBuff(BuffID.Slow) ||
                    Player.HasBuff(BuffID.Cursed) ||
                    Player.HasBuff(BuffID.Confused) ||
                    Player.HasBuff(BuffID.Bleeding) ||
                    Player.HasBuff(BuffID.Stoned) ||
                    Player.HasBuff(BuffID.Darkness) ||
                    Player.HasBuff(BuffID.Confused) ||
                    Player.HasBuff(BuffID.BrokenArmor) ||
                    Player.HasBuff(BuffID.Weak) ||
                    Player.HasBuff(BuffID.Venom) ||
                    Player.HasBuff(BuffID.Blackout) ||
                    Player.HasBuff(BuffID.OgreSpit) ||
                    Player.HasBuff(BuffID.WitheredArmor) ||
                    Player.HasBuff(BuffID.WitheredWeapon) ||
                    Player.HasBuff(BuffID.Ichor) ||
                    Player.HasBuff(BuffID.Rabies) ||
                    Player.HasBuff(BuffID.Electrified) ||
                    Player.HasBuff(BuffID.Frozen) ||
                    Player.HasBuff(BuffID.Burning) ||
                    Player.HasBuff(BuffID.VortexDebuff) ||
                    Player.HasBuff(BuffID.Venom))
                {
                    Player.AddBuff(ModContent.BuffType<Refresh>(), 300);
                }

                // Overlap from ankh charm
                Player.ClearBuff(BuffID.Silenced);
                Player.ClearBuff(BuffID.Slow);
                Player.ClearBuff(BuffID.Confused);
                Player.ClearBuff(BuffID.Poisoned);
                Player.ClearBuff(BuffID.Bleeding);
                Player.ClearBuff(BuffID.Stoned);
                Player.ClearBuff(BuffID.Darkness);
                Player.ClearBuff(BuffID.Cursed);
                Player.ClearBuff(BuffID.BrokenArmor);
                Player.ClearBuff(BuffID.Weak);
                Player.ClearBuff(BuffID.Burning);
                // More debuff removals
                Player.ClearBuff(BuffID.Blackout);
                Player.ClearBuff(BuffID.OgreSpit);
                Player.ClearBuff(BuffID.WitheredArmor);
                Player.ClearBuff(BuffID.Ichor);
                Player.ClearBuff(BuffID.Rabies);
                Player.ClearBuff(BuffID.Electrified);
                Player.ClearBuff(BuffID.WitheredWeapon);
                Player.ClearBuff(BuffID.Frozen);
                Player.ClearBuff(BuffID.VortexDebuff);
            }
            if (accType == 2)
            {
                SoundEngine.PlaySound(SoundID.Splash, Player.Center);
                for (int i = 0; i < 40; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(5f, 5f);
                    Dust.NewDustPerfect(Player.Center, DustID.Water, speed, Scale: 1f);
                }
                Player.AddBuff(BuffID.Wet, 200);
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Item4, Player.Center);
                int dustNum = 20 + (10 * accType);
                float dustSpeed = 3f + (float)accType;
                for (int i = 0; i < dustNum; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(dustSpeed, dustSpeed);
                    Dust d = Dust.NewDustPerfect(Player.Center, 74, speed, Scale: 1f);
                    d.noGravity = true;
                }
            }
        }

        private void Retribution()
        {
            if (!retributionStoker)
            {
                Player.ClearBuff(ModContent.BuffType<RetributionBuff>());
                retributionPower = 0;
                retributionCharge = 0;
                return;
            }
            if (retributionActive)
            {
                float dmgBoost = 0.075f + retributionPower;
                float critBoost = 7.5f + (retributionPower * 100f);
                if (dmgBoost > 0.15f)
                {
                    dmgBoost = 0.15f;
                }
                if (critBoost > 15f)
                {
                    critBoost = 15f;
                }
                Player.GetDamage(DamageClass.Generic) += dmgBoost;
                Player.GetCritChance(DamageClass.Generic) += critBoost;
               
                Dust dust;
                dust = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, DustID.FlameBurst, 0f, 0f, 0, new Color(255, 0, 0), 1.25f + retributionPower)];
                dust.noGravity = true;
                dust.fadeIn = 0f + (retributionPower * 12.5f);
            }
        }
        private void SetRetributionPow()
        {
            retributionPower = 0;
            for (int i = 1; i < 11; i++)
            {
                if (retributionCharge >= 30 * i)
                {
                    retributionPower += 0.0075f;
                }
            }
            if (retributionPower > 0.075f)
            {
                retributionPower = 0.075f;
            }
            
            SoundEngine.PlaySound(SoundID.Item113, Player.Center);
            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(6f, 6f);
                Dust d = Dust.NewDustPerfect(Player.Center, DustID.FlameBurst, speed, 0, new Color(255, 0, 0), 1.15f + retributionPower);
                d.noGravity = true;
                d.fadeIn = 0.5f + (retributionPower * 10f);
            }
        }

        private void ChaosGem()
        {
            // Adapted from RoD teleport

            Vector2 teleportDestination = Main.MouseWorld;
            
            Player.LimitPointToPlayerReachableArea(ref teleportDestination); // Limits teleport range
            if (!(teleportDestination.X > 50f) || !(teleportDestination.X < (float)(Main.maxTilesX * 16 - 50)) || !(teleportDestination.Y > 50f) || !(teleportDestination.Y < (float)(Main.maxTilesY * 16 - 50)))
            {
                return;
            }

            // Checks to prevent teleporting into tiles or the jungle temple early
            int bodyR = (int)(teleportDestination.X / 16f);
            int num2 = (int)(teleportDestination.Y / 16f);
            if ((Main.tile[bodyR, num2].WallType == 87 && !NPC.downedPlantBoss && (double)num2 > Main.worldSurface) || Collision.SolidCollision(teleportDestination, Player.width, Player.height))
            {
                return;
            }
            Player.Teleport(teleportDestination, 1);
        }

        private void CyborgCoreMisc()
        {
            if (!nanomachinesSon)
            {
                Player.ClearBuff(ModContent.BuffType<CyborgCoreBuff>());
                return;
            }
        }

        private void XRadarReveal(float maxDetectionDistance)
        {
            float sqrMaxDetectDistance = maxDetectionDistance * maxDetectionDistance;
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];

                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Player.Center);
                if (sqrDistanceToTarget < sqrMaxDetectDistance)
                {
                    target.AddBuff(ModContent.BuffType<RadarPing>(), 1800);
                }
            }
            
            SoundEngine.PlaySound(SoundID.Item92, Player.Center);
            
            for (int i = 0; i < 250; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Player.Center + speed * 25, DustID.TreasureSparkle, speed * 80f, Scale: 1.25f);
                d.fadeIn = 1f;
                d.noGravity = true;
                d.noLightEmittence = true;
            }
            for (int j = 0; j < 50; j++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d2 = Dust.NewDustPerfect(Player.Center + speed * 25, DustID.TreasureSparkle, speed * 20f, Scale: 0.75f);
                d2.noGravity = true;
            }
        }

        private void StealthFX()
        {
            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(4f, 4f);
                Dust d = Dust.NewDustPerfect(Player.Center, DustID.WitherLightning, speed, Scale: 1.25f);
                d.noGravity = true;
                d.shader = GameShaders.Armor.GetSecondaryShader(82, Main.LocalPlayer);
            }
        }
        private void SwagTaunt(float maxDetectionDistance)
        {
            float sqrMaxDetectDistance = maxDetectionDistance * maxDetectionDistance;
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];

                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Player.Center);
                if (sqrDistanceToTarget < sqrMaxDetectDistance)
                {
                    target.AddBuff(ModContent.BuffType<SwaggerTaunt>(), 600);
                }
            }
        }

        private void ArcanumMissiles()
        {
            if (arcanumEquip != null && Main.myPlayer == Player.whoAmI)
            {
                for (int i = 0; i < 13; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2CircularEdge(6f, 6f);
                    velocity.Y = -1f * (float)Math.Abs(velocity.Y);

                    int dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(arcanumEquip.damage);
                    float kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(arcanumEquip.knockBack);

                    int missile = Projectile.NewProjectile(Player.GetSource_Accessory(arcanumEquip), Player.Center, velocity, ProjectileID.MagicMissile,
                        dmg, kb, Player.whoAmI);
                    Main.projectile[missile].DamageType = DamageClass.Generic;
                    // Tweaks the projectile behavior, preventing player control of the projectiles
                    Main.projectile[missile].GetGlobalProjectile<ArcanumMissiles>().arcanumAI = true;
                    Main.projectile[missile].ai[0] = -1f;
                    Main.projectile[missile].ai[1] = -1f;
                }
                
                SoundEngine.PlaySound(SoundID.Item67, Player.Center);
                for (int j = 0; j < 40; j++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d3 = Dust.NewDustPerfect(Player.Center + speed * 25, DustID.MagicMirror, speed * 20f, 100, Scale: 2f);
                    d3.noGravity = true;
                }
            }
        }
        private void PrismaticArcanum()
        {
            if((arcanum2Equip != null || fCharmEquip != null)
                && Main.myPlayer == Player.whoAmI)
            {
                int dmg;
                float kb;
                IEntitySource source;
                if (fCharmEquip != null)
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(100);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(3f);
                    source = Player.GetSource_Accessory(fCharmEquip);
                }
                else
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(arcanum2Equip.damage);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(arcanum2Equip.knockBack);
                    source = Player.GetSource_Accessory(arcanum2Equip);
                }

                for (int i = 0; i < 8; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2CircularEdge(20f, 20f);
                    float glowColor = Main.rand.NextFloat() * 0.66f + Player.miscCounterNormalized;

                    int nightglow = Projectile.NewProjectile(source, Player.Center, velocity, ProjectileID.FairyQueenMagicItemShot,
                        dmg, kb, Player.whoAmI, -1f, glowColor % 1f);
                    Main.projectile[nightglow].DamageType = DamageClass.Generic;
                }
                for (int i = 0; i < 10; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2CircularEdge(6f, 6f);
                    int missile = Projectile.NewProjectile(source, Player.Center, velocity, ProjectileID.RainbowRodBullet,
                            dmg, kb, Player.whoAmI);
                    Main.projectile[missile].DamageType = DamageClass.Generic;
                    Main.projectile[missile].GetGlobalProjectile<ArcanumMissiles>().arcanumAI = true;
                    Main.projectile[missile].ai[0] = -1f;
                    Main.projectile[missile].ai[1] = -1f;
                }
                
                SoundEngine.PlaySound(SoundID.Item68, Player.Center);
                for (int j = 0; j < 60; j++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d3 = Dust.NewDustPerfect(Player.Center + speed * 25, DustID.RainbowMk2, speed * 20f, 100, Main.hslToRgb(Player.miscCounterNormalized * 9f % 1f, 1f, 0.65f), Scale: 1.5f);
                    d3.fadeIn = 1f + Main.rand.NextFloat() * 0.075f;
                    d3.noGravity = true;
                }
            }
        }

        private void StarplateBlaster()
        {
            if (blasterEquip != null && Main.myPlayer == Player.whoAmI)
            {
                SoundEngine.PlaySound(SoundID.Item33, Player.Center);
                Vector2 velocity = SimpleProjVelocity(5.5f, Player.Center);
                int dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(blasterEquip.damage);
                float kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(blasterEquip.knockBack);
                Projectile.NewProjectile(Player.GetSource_Accessory(blasterEquip), Player.Center, velocity, ModContent.ProjectileType<StarplateBlast>(), 
                   dmg, kb, Player.whoAmI);

                
                for (int i = 0; i < 20; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(5f, 5f);
                    Dust d = Dust.NewDustPerfect(Player.Center, DustID.DungeonSpirit, speed, Scale: 1.25f);
                    d.noGravity = true;
                }
            }
        }
        private void SolarBlaster()
        {
            if ((blaster2Equip != null || fCharmEquip != null)
                && Main.myPlayer == Player.whoAmI)
            {
                SoundEngine.PlaySound(SoundID.Item68, Player.Center);
                Vector2 velocity = SimpleProjVelocity(7.25f, Player.Center);
                int dmg;
                float kb;
                IEntitySource source;
                if (fCharmEquip != null) // Applies stat changes when caused by the Foreboding Charm's ability
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(310);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(8f);
                    source = Player.GetSource_Accessory(fCharmEquip);
                }
                else
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(blaster2Equip.damage);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(blaster2Equip.knockBack);
                    source = Player.GetSource_Accessory(blaster2Equip);
                }

                float numberProjectiles = 3;
                float rotation = MathHelper.ToRadians(5f);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                    Projectile.NewProjectile(source, Player.Center, perturbedSpeed, ModContent.ProjectileType<SolarBlast>(),
                    dmg, kb, Player.whoAmI);
                }

                for (int i = 0; i < 40; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(7.5f, 7.5f);
                    Dust d = Dust.NewDustPerfect(Player.Center, DustID.HeatRay, speed, Scale: 1.25f);
                    d.noGravity = true;
                }
            }
        }

        private void ShootNeedles()
        {
            if ((needleEquip != null || fCharmEquip != null)
                && needlesToShoot > 0 && needleTimer <= 0 && Main.myPlayer == Player.whoAmI)
            {
                SoundEngine.PlaySound(SoundID.Item17, Player.Center);
                Vector2 velocity = SimpleProjVelocity(12f, Player.Center);
                Vector2 shootPos = Player.Center;
                int projType;
                bool varaibleAim = false;
                switch (needleAttack)
                {
                    case 1:
                        projType = ModContent.ProjectileType<HiddenBladeProj>();
                        break;
                    case 2:
                        projType = ModContent.ProjectileType<AssassinNeedle>();
                        varaibleAim = true;
                        needleTimer = 6;
                        break;
                    case 3:
                        projType = ModContent.ProjectileType<BarrageNeedle>();
                        varaibleAim = true;
                        needleTimer = 2; 
                        break;
                    default:
                        if (fCharm)
                        {
                            projType = ModContent.ProjectileType<BarrageNeedle>();
                            varaibleAim = true;
                            needleTimer = 2;
                            break;
                        }
                        else
                        {
                            return;
                        }
                }
                if (varaibleAim)
                {
                    velocity = velocity.RotatedByRandom(MathHelper.ToRadians(2.5f));
                    shootPos.X += Main.rand.NextFloat(-4f, 4f);
                    shootPos.Y += Main.rand.NextFloat(-4f, 4f);
                }
                int dmg;
                float kb;
                IEntitySource source;
                if (fCharmEquip != null)
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(185);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(3f);
                    source = Player.GetSource_Accessory(fCharmEquip);
                }
                else
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(needleEquip.damage);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(needleEquip.knockBack);
                    source = Player.GetSource_Accessory(needleEquip);
                }

                Projectile.NewProjectile(source, shootPos, velocity, projType,
                    dmg, kb, Player.whoAmI);
                needlesToShoot--;
            }
        }

        private void ShootMissiles()
        {
            if ((missileEquip != null || fCharmEquip != null) && missilesToShoot > 0 && missilesTimer <= 0 && Main.myPlayer == Player.whoAmI)
            {
                SoundEngine.PlaySound(SoundID.Item42, Player.Center);

                Vector2 velocity = new Vector2(0f, -12.5f * Player.gravDir);
                velocity = velocity.RotatedBy(MathHelper.ToRadians(15f * Player.direction));
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10f));

                Vector2 shootPos = new Vector2(Player.Center.X ,Player.Center.Y - 8f);
                shootPos.X += -8f * Player.direction;

                int dmg;
                float kb;
                IEntitySource source;
                if(fCharmEquip != null)
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(180);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(8f);
                    source = Player.GetSource_Accessory(fCharmEquip);
                }
                else
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(missileEquip.damage);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(missileEquip.knockBack);
                    source = Player.GetSource_Accessory(missileEquip);
                }

                Projectile.NewProjectile(source, shootPos, velocity, ModContent.ProjectileType<Missile>(),
                    dmg, kb, Player.whoAmI);
                missilesToShoot--;
                missilesTimer = 8;
            }
        }

        private void DropRepulse()
        {
            if(Player == Main.LocalPlayer && rChargeEquip != null)
            {
                SoundEngine.PlaySound(SoundID.Item61, Player.Center);

                int dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(rChargeEquip.damage);
                float kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(rChargeEquip.knockBack);
                Projectile.NewProjectile(Player.GetSource_Accessory(rChargeEquip), Player.Center, new Vector2(0f, 5f),
                    ModContent.ProjectileType<RepulseBomb>(),
                    dmg, kb, Player.whoAmI);
            }
        }

        private void RepulseRocket()
        {
            if (Player == Main.LocalPlayer && rRocketEquip != null)
            {
                SoundEngine.PlaySound(SoundID.Item14, Player.Center);
                Vector2 velocity = SimpleProjVelocity(15f, Player.Center);

                int dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(rRocketEquip.damage);
                float kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(rRocketEquip.knockBack);
                int rocket = Projectile.NewProjectile(Player.GetSource_Accessory(rRocketEquip), Player.Center, velocity, 
                    ModContent.ProjectileType<RepulseMissile>(),
                    dmg, kb, Player.whoAmI);

                Projectile proj = Main.projectile[rocket];

                // Visual FX, adapated from the Cannon/Cannonball
                for (int j = 0; j < 25; j++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(proj.position.X, proj.position.Y), proj.width, proj.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1f);
                    Dust dust = Main.dust[dustIndex];
                    dust.velocity *= 0.5f;
                    dust.velocity += proj.velocity * 0.1f;
                }
                for (int k = 0; k < 5; k++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(proj.position.X, proj.position.Y), proj.width, proj.height, DustID.Torch, 0f, 0f, 100, default(Color), 2f);
                    Dust dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                    dust.velocity *= 3f;
                    dust.velocity += proj.velocity * 0.2f;

                    dustIndex = Dust.NewDust(new Vector2(proj.position.X, proj.position.Y), proj.width, proj.height, DustID.Torch, 0f, 0f, 100);
                    dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                    dust.velocity += proj.velocity * 0.3f;
                }
                for (int l = 0; l < 3; l++)
                {
                    int smokeIndex = Gore.NewGore(Player.GetSource_Accessory(rRocketEquip), 
                        new Vector2(proj.position.X - 10f, proj.position.Y - 10f), default(Vector2), Main.rand.Next(61, 64));
                    Gore smoke = Main.gore[smokeIndex];
                    smoke.position += proj.velocity * 1.25f;
                    smoke.scale = 1f;
                    smoke.velocity += proj.velocity * 0.5f;
                    smoke.velocity *= 0.02f;
                }

                repulsionFlag = true;
            }
        }
        private void RepulseLaunch()
        {
            if (repulsionFlag && repulseRocket)
            {
                Vector2 cursorAim = SimpleProjVelocity(20f, Player.Center);
                Player.velocity += cursorAim * -1f;
                repulsionFlag = false;
            }
        }

        private void FirstPrism()
        {
            if ((prismEquip != null || fCharmEquip != null)
                && Player.ownedProjectileCounts[ModContent.ProjectileType<FirstPrismProj>()] < 1 && Main.myPlayer == Player.whoAmI)
            {
                SoundEngine.PlaySound(SoundID.Item4, Player.Center);

                int dmg;
                float kb;
                IEntitySource source;
                if (fCharmEquip != null)
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(130);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(6f);
                    source = Player.GetSource_Accessory(fCharmEquip);
                }
                else
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(prismEquip.damage);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(prismEquip.knockBack);
                    source = Player.GetSource_Accessory(prismEquip);
                }

                Projectile.NewProjectile(source, Player.Center, Vector2.Zero, ModContent.ProjectileType<FirstPrismProj>(), 
                    dmg, kb, Player.whoAmI);
            }
        }

        private bool BulwarkSpawnCheck(Vector2 aimPos)
        {
            int spawnTileX = (int)aimPos.X / 16;
            int spawnTileY = (int)aimPos.Y / 16;
            if (WorldGen.SolidTile2(spawnTileX, spawnTileY))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void BulwarkSummon(Vector2 spawnPos)
        {
            if ((bBulwarkEquip != null || fCharmEquip != null)
                && Player.ownedProjectileCounts[ModContent.ProjectileType<FirstPrismProj>()] < 1 && Main.myPlayer == Player.whoAmI)
            {
                SoundEngine.PlaySound(SoundID.Item113, Player.Center);

                int dmg;
                float kb;
                IEntitySource source;
                if (fCharmEquip != null)
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(60);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(12.5f);
                    source = Player.GetSource_Accessory(fCharmEquip);
                }
                else
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(bBulwarkEquip.damage);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(bBulwarkEquip.knockBack);
                    source = Player.GetSource_Accessory(bBulwarkEquip);
                }

                Projectile.NewProjectile(source, spawnPos, Vector2.Zero, ModContent.ProjectileType<BewitchedBulwarkProj>(),
                    dmg, kb, Player.whoAmI);
            }
        }

        private void LaunchTether()
        {
            if ((tetherEquip != null || fCharmEquip != null)
                && Main.myPlayer == Player.whoAmI)
            {
                Vector2 velocity = SimpleProjVelocity(15f, Player.Center);
                int projType;
                int dmg = 10;

                IEntitySource source;

                switch (tetherLeech)
                {
                    case 1:
                        if(Player.ownedProjectileCounts[ModContent.ProjectileType<DarkLeechHook>()] >= 1)
                        {
                            return;
                        }
                        projType = ModContent.ProjectileType<DarkLeechHook>();
                        source = Player.GetSource_Accessory(tetherEquip);
                        break;
                    case 2:
                        if (Player.ownedProjectileCounts[ModContent.ProjectileType<LeechingTetherHook>()] >= 1)
                        {
                            return;
                        }
                        projType = ModContent.ProjectileType<LeechingTetherHook>();
                        source = Player.GetSource_Accessory(tetherEquip);
                        break;
                    case 3:
                        if (Player.ownedProjectileCounts[ModContent.ProjectileType<LifelineHook>()] >= 1)
                        {
                            return;
                        }
                        projType = ModContent.ProjectileType<LifelineHook>();
                        dmg = 50;
                        velocity = SimpleProjVelocity(25f, Player.Center);
                        source = Player.GetSource_Accessory(tetherEquip);
                        break;
                    default:
                        if (fCharmEquip != null && Player.ownedProjectileCounts[ModContent.ProjectileType<LifelineHook>()] >= 1)
                        {
                            projType = ModContent.ProjectileType<LifelineHook>();
                            dmg = 50;
                            velocity = SimpleProjVelocity(25f, Player.Center);
                            source = Player.GetSource_Accessory(fCharmEquip);
                            break;
                        }
                        else
                        {
                            return;
                        }
                }

                Projectile.NewProjectile(source, Player.Center, velocity, projType,
                        dmg, 0f, Player.whoAmI);
            }
        }

        private void ManageHarvesterChargeTimers()
        {
            if (!funiDungeonsArtifact)
            {
                hChargeCool = 0;
                harvesterCharge = 0;
                hChargeExpire = 0;
                return;
            }

            if (hChargeCool > 0)
            {
                hChargeCool--;
            }
            hChargeExpire++;
            if (hChargeExpire >= 180 && hChargeExpire % 60 == 0)
            {
                harvesterCharge--;
            }
            if(harvesterCharge > 200)
            {
                harvesterCharge = 200;
            }
        }
        private void MCDomesticBombingMoment(float maxRange)
        {
            if(Player == Main.LocalPlayer && dungeonsReference != null)
            {
                int dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(dungeonsReference.damage);
                float kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(dungeonsReference.knockBack);

                // Buff damage and knockback based on accumulated charge
                float statMult = 1f + (harvesterCharge / 40f);
                dmg = (int)(dmg * statMult);
                kb *= statMult;

                float sqrMaxRange = maxRange * maxRange;
                for (int i = 0; i < 200; i++)
                {
                    NPC target = Main.npc[i];

                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Player.Center);
                    if (sqrDistanceToTarget < sqrMaxRange)
                    {
                        Projectile.NewProjectile(Player.GetSource_Accessory(dungeonsReference), target.Center, Vector2.Zero, ModContent.ProjectileType<HarvesterNuke>(),
                            dmg, kb, Player.whoAmI, harvesterCharge);
                    }
                }

                // Visual FX
                SoundEngine.PlaySound(SoundID.NPCDeath6, Player.Center);
                SoundEngine.PlaySound(SoundID.NPCDeath39, Player.Center);
                SoundEngine.PlaySound(SoundID.NPCDeath51, Player.Center);
                int dustNum = 80 + (int)(harvesterCharge * 1.4f + Main.rand.Next(-10, 21));
                dustNum = Math.Clamp(dustNum, 80, 320);
                int dustNum2 = (int)(dustNum / 2f);
                int dustNum3 = (int)(dustNum2 / 2f);

                float velocityScalar = harvesterCharge / 100f;
                velocityScalar = Math.Clamp(velocityScalar, 0.9f, 1.75f);

                float scaleFactor = harvesterCharge / 150f;
                scaleFactor = Math.Clamp(scaleFactor, 0.5f, 1.3f);

                for (int i = 0; i < dustNum; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(Player.Center + speed * 30, DustID.DungeonSpirit, speed * 30f * velocityScalar, Scale: 1.4f * scaleFactor);
                    d.fadeIn = 1.6f * scaleFactor;
                    d.noGravity = true;
                }
                for (int i = 0; i < dustNum; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
                    Dust d = Dust.NewDustPerfect(Player.Center + speed * 10, DustID.DungeonSpirit, speed * 30f * velocityScalar, Scale: 1.2f * scaleFactor);
                    d.fadeIn = 1.2f * scaleFactor;
                    d.noGravity = true;
                }
                for (int i = 0; i < dustNum2; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                    Dust d = Dust.NewDustPerfect(Player.Center + speed * 20, DustID.DungeonSpirit, speed * 20f * velocityScalar, Scale: 1f * scaleFactor);
                    d.fadeIn = 1f * scaleFactor;
                    d.noGravity = true;
                }
                for (int i = 0; i < dustNum3; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                    Dust d = Dust.NewDustPerfect(Player.Center + speed * 10, DustID.DungeonSpirit, speed * 10f * velocityScalar, Scale: 0.8f * scaleFactor);
                    d.fadeIn = 0.8f * scaleFactor;
                    d.noGravity = true;
                }

                // Reset charge and associated timers
                harvesterCharge = 0;
                hChargeCool = 0;
                hChargeExpire = 0;
            }
        }

        private void SpawnProbes()
        {
            if ((probeEquip != null || fCharmEquip != null)
                && Player.ownedProjectileCounts[ModContent.ProjectileType<MiniProbe>()] < 1 && Main.myPlayer == Player.whoAmI)
            {
                int dmg;
                float kb;
                IEntitySource source;
                if (fCharmEquip != null)
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(65);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(4f);
                    source = Player.GetSource_Accessory(fCharmEquip);
                }
                else
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(probeEquip.damage);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(probeEquip.knockBack);
                    source = Player.GetSource_Accessory(probeEquip);
                }

                SoundEngine.PlaySound(SoundID.Item90, Player.Center);

                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(source, Player.Center, Vector2.Zero, ModContent.ProjectileType<MiniProbe>(),
                            dmg, kb, Player.whoAmI, i);
                }
            }
        }

        private void EmulatorBuffs(float maxRange)
        {
            int buffType = -1;
            int dustType = -1;

            // Sound FX
            SoundEngine.PlaySound(SoundID.Item43, Player.position);

            switch (emulatorState)
            {
                case 0:
                    buffType = ModContent.BuffType<EmulatorF>();
                    dustType = DustID.RedTorch;
                    break;
                case 1:
                    buffType = ModContent.BuffType<EmulatorS>();
                    dustType = DustID.GreenTorch;
                    break;
                case 2:
                    buffType = ModContent.BuffType<EmulatorM>();
                    dustType = DustID.BlueTorch;
                    break;
                default:
                    return;
            }

            // Dust FX
            for (int i = 0; i < 200; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                Dust d = Dust.NewDustPerfect(Player.Center + speed * 20, dustType, speed * 20f, Scale: 1.25f);
                d.fadeIn = 1.25f;
                d.noGravity = true;
            }
            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                Dust d = Dust.NewDustPerfect(Player.Center + speed * 15, dustType, speed * 10f, Scale: 0.75f);
                d.fadeIn = 0.75f;
                d.noGravity = true;
            }

            float sqrMaxRange = maxRange * maxRange;
            for (int k = 0; k < Main.maxPlayers; k++)
            {
                Player target = Main.player[k];

                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Player.Center);
                if (sqrDistanceToTarget < sqrMaxRange)
                {
                    if (target.active && !target.dead 
                        && ((!Player.hostile && !target.hostile)
                           || Player.team == target.team))
                    {
                        int buffTime = 300;

                        bool getArtificer = target.TryGetModPlayer(out ArtificerPlayer artificer);
                        if (getArtificer && artificer.soulEmulator)
                        {
                            buffTime = 600;
                            artificer.emulatorState++;
                            if (artificer.emulatorState > 2)
                            {
                                artificer.emulatorState = 0;
                            }
                        }
                        target.AddBuff(buffType, buffTime);
                    }
                }
            }
        }

        private void ShootDeathray()
        {
            if ((oCoreEquip != null || fCharmEquip != null)
                && Player.ownedProjectileCounts[ModContent.ProjectileType<ObliterationBeam>()] < 1 && Main.myPlayer == Player.whoAmI)
            {
                int dmg;
                float kb;
                IEntitySource source;
                if (fCharmEquip != null)
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(170);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(8f);
                    source = Player.GetSource_Accessory(fCharmEquip);
                }
                else
                {
                    source = Player.GetSource_Accessory(oCoreEquip);
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(oCoreEquip.damage);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(oCoreEquip.knockBack);
                }

                Projectile.NewProjectile(source, Player.Center, Vector2.Zero, ModContent.ProjectileType<ObliterationBeam>(),
                            dmg, kb, Player.whoAmI, Player.direction);
            }
        }

        private void SpawnReapers()
        {
            if ((reaperEquip != null || fCharmEquip != null)
                && Player.ownedProjectileCounts[ModContent.ProjectileType<FearReaperBlade>()] < 1 && Main.myPlayer == Player.whoAmI)
            {
                int dmg;
                float kb;
                IEntitySource source;
                if (fCharmEquip != null)
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(95);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(10f);
                    source = Player.GetSource_Accessory(fCharmEquip);
                }
                else
                {
                    dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(reaperEquip.damage);
                    kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(reaperEquip.knockBack);
                    source = Player.GetSource_Accessory(reaperEquip);
                }

                SoundEngine.PlaySound(SoundID.Item113, Player.Center);

                for (int i = 0; i < 2; i++)
                {
                    Projectile.NewProjectile(source, Player.Center, Vector2.Zero, ModContent.ProjectileType<FearReaperBlade>(),
                            dmg, kb, Player.whoAmI, i);
                }
            }
        }
        private void ReaperDR()
        {
            if(fearReaper && Player.ownedProjectileCounts[ModContent.ProjectileType<FearReaperBlade>()] > 0)
            {
                float frDR = 0.1f;
                int drBoost = Math.Clamp(reaperDRCounter, 0, 30);
                for(int i = 0; i < drBoost; i++)
                {
                    frDR += 0.05f;
                }
                Player.endurance += frDR;
            }
            else
            {
                reaperDRCounter = 0;
            }
        }

        private void AugmentedLegJump()
        {
            if (rocketLeg && rocketJumpTimer > 0)
            {
                if (Player.gravDir == -1) // Reversed gravity
                {
                    if (Player.velocity.Y < 20)
                    {
                        Player.velocity.Y += 4;
                    }
                    else
                    {
                        Player.velocity.Y = 20;
                    }
                }
                else
                {
                    if (Player.velocity.Y > -20)
                    {
                        Player.velocity.Y -= 4;
                    }
                    else
                    {
                        Player.velocity.Y = -20;
                    }
                }

                rocketSafeFall = 300;
            }
            if (rocketJumpTimer > 0)
            {
                rocketJumpTimer--;
            }
            if (rocketLeg && rocketSafeFall > 0)
            {
                Player.extraFall += 50;
            }
            if (rocketLeg && rocketSafeFall > 240) // Dust FX; adapted from rocket boots
            {
                int yOffset = Player.height;
                if (Player.gravDir == -1f)
                {
                    yOffset = 4;
                }
                for (int i = 0; i < 2; i++)
                {
                    int legSide = ((i == 0) ? 2 : (-2));
                    Rectangle r = ((i != 0) ? new Rectangle((int)Player.position.X + Player.width - 4, (int)Player.position.Y + yOffset - 10, 8, 8) : new Rectangle((int)Player.position.X - 4, (int)Player.position.Y + yOffset - 10, 8, 8));
                    if (Player.direction == -1)
                    {
                        r.X -= 4;
                    }
                    Vector2 vector = new Vector2((float)(-legSide) - Player.velocity.X * 0.3f, 2f * Player.gravDir - Player.velocity.Y * 0.3f);
                    Dust dust;
                    dust = Dust.NewDustDirect(r.TopLeft(), r.Width, r.Height, DustID.Torch, 0f, 0f, 100, default(Color), 2.5f);
                    dust.shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
                    dust.velocity += vector;
                    dust.noGravity = true;
                }
            }
        }

        private void ForebodingCharmAttack()
        {
            int attackChoice = Main.rand.Next(10);
            switch (attackChoice)
            {
                case 9:
                    needlesToShoot = 25;
                    break;
                case 8:
                    missilesToShoot = 15;
                    break;
                case 7:
                    SpawnProbes();
                    break;
                case 6:
                    SpawnReapers();
                    break;
                case 5:
                    FirstPrism();
                    break;
                case 4:
                    if (BulwarkSpawnCheck(Main.MouseWorld))
                    {
                        BulwarkSummon(Main.MouseWorld);
                    }
                    else // re-roll if there's no space for a bulwark spawn
                    {
                        ForebodingCharmAttack();
                    }
                    break;
                case 3:
                    SolarBlaster();
                    break;
                case 2:
                    ShootDeathray();
                    break;
                case 1:
                    PrismaticArcanum();
                    break;
                default:
                    LaunchTether();
                    break;
            }
        }

        private void LuckyPresentBuffs()
        {
            int buffTier; 
            int buffType = Main.rand.Next(6); // 0 = Dmg, 1 = Crit, 2 = Def, 3 = DR, 4 = HP, 5 = Mana
            int selectedBuff; // Buff ID

            int buffTime = 600; 
            bool buffTimeInc;

            // presentLuck determines positive/negative buffs and influences later rolls
            switch (presentLuck) 
            {
                case -1: // Last roll was bad RNG
                    presentLuck = (Main.rand.Next(100) < 30) ? -1 : 1;
                    break;
                case 1: // Last roll was good RNG
                    presentLuck = (Main.rand.Next(100) < 45) ? -1 : 1;
                    break;
                default: // Neutral
                    presentLuck = (Main.rand.Next(100) < 40) ? -1 : 1;
                    break;
            }

            LuckyPresentFX(presentLuck == 1);

            // Select buff tier
            if (presentLuck < 0) // Bad RNG
            {
                buffTier = (Main.rand.Next(100) < 20) ? 2 : 1;
            }
            else // Good RNG
            {
                int tierSelect = Main.rand.Next(100);
                if(tierSelect < 25) 
                {
                    buffTier = 3;
                }
                else if(tierSelect < 60) 
                {
                    buffTier = 2;
                }
                else 
                {
                    buffTier = 1;
                }
            }

            // Select exact buff now
            if (presentLuck < 0) 
            {
                switch (buffTier)
                {
                    case 2:
                        switch (buffType)
                        {
                            case 5:
                                LuckyPresentStats(false, true);
                                return;
                            case 4:
                                LuckyPresentStats(false);
                                return;
                            case 3:
                                selectedBuff = ModContent.BuffType<DRNerf2>();
                                break;
                            case 2:
                                selectedBuff = ModContent.BuffType<DefNerf2>();
                                break;
                            case 1:
                                selectedBuff = ModContent.BuffType<CritNerf2>();
                                break;
                            default:
                                selectedBuff = ModContent.BuffType<DmgNerf2>();
                                break;
                        }
                        break;
                    default:
                        switch (buffType)
                        {
                            case 5:
                                LuckyPresentStats(false, true);
                                return;
                            case 4:
                                LuckyPresentStats(false);
                                return;
                            case 3:
                                selectedBuff = ModContent.BuffType<DRNerf1>();
                                break;
                            case 2:
                                selectedBuff = ModContent.BuffType<DefNerf1>();
                                break;
                            case 1:
                                selectedBuff = ModContent.BuffType<CritNerf1>();
                                break;
                            default:
                                selectedBuff = ModContent.BuffType<DmgNerf1>();
                                break;
                        }
                        break;
                }
            }
            else 
            {
                switch (buffTier)
                {
                    case 3:
                        switch (buffType)
                        {
                            case 5:
                                LuckyPresentStats(true, true);
                                return;
                            case 4:
                                LuckyPresentStats(true);
                                return;
                            case 3:
                                selectedBuff = ModContent.BuffType<DRBuff3>();
                                break;
                            case 2:
                                selectedBuff = ModContent.BuffType<DefBuff3>();
                                break;
                            case 1:
                                selectedBuff = ModContent.BuffType<CritBuff3>();
                                break;
                            default:
                                selectedBuff = ModContent.BuffType<DmgBuff3>();
                                break;
                        }
                        break;
                    case 2:
                        switch (buffType)
                        {
                            case 5:
                                LuckyPresentStats(true, true);
                                return;
                            case 4:
                                LuckyPresentStats(true);
                                return;
                            case 3:
                                selectedBuff = ModContent.BuffType<DRBuff2>();
                                break;
                            case 2:
                                selectedBuff = ModContent.BuffType<DefBuff2>();
                                break;
                            case 1:
                                selectedBuff = ModContent.BuffType<CritBuff2>();
                                break;
                            default:
                                selectedBuff = ModContent.BuffType<DmgBuff2>();
                                break;
                        }
                        break;
                    default:
                        switch (buffType)
                        {
                            case 5:
                                LuckyPresentStats(true, true);
                                return;
                            case 4:
                                LuckyPresentStats(true);
                                return;
                            case 3:
                                selectedBuff = ModContent.BuffType<DRBuff1>();
                                break;
                            case 2:
                                selectedBuff = ModContent.BuffType<DefBuff1>();
                                break;
                            case 1:
                                selectedBuff = ModContent.BuffType<CritBuff1>();
                                break;
                            default:
                                selectedBuff = ModContent.BuffType<DmgBuff1>();
                                break;
                        }
                        break;
                }
            }

            // Decide if buff tiem should be greater or less than 10 seconds
            if(presentLuck < 0) 
            {
                buffTimeInc = (Main.rand.Next(100) < 35);
            }
            else 
            {
                buffTimeInc = (Main.rand.Next(100) < 60);
            }

            // Modify buff time
            if (buffTimeInc)
            {
                for (int i = 0; i < 5; i++)
                {
                    buffTime += (Main.rand.Next(100) < 60) ? 60 : 0;
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    buffTime -= (Main.rand.Next(100) < 40) ? -60 : 0;
                }
            }

            Player.AddBuff(selectedBuff, buffTime);
        }
        private void LuckyPresentStats(bool buff, bool mana = false)
        {
            if (!mana)
            {
                int lifeChange;
                if(!buff)
                {
                    float percentageLife = 0.1f;
                    for (int i = 0; i < 5; i++)
                    {
                        percentageLife += (Main.rand.Next(100) < 80) ? 0.01f : 0;
                    }

                    lifeChange = (int)(-1f * (Player.statLife * percentageLife));

                    if(lifeChange > -1)
                    {
                        lifeChange = -1;
                    }
                }
                else
                {
                    lifeChange = 50;
                    for (int i = 0; i < 5; i++)
                    {
                        lifeChange += (Main.rand.Next(100) < 80) ? 5 : 0;
                    }
                }

                if(lifeChange > 0)
                {
                    Player.Heal(lifeChange);
                }
                else
                {
                    Player.statLife += lifeChange;
                    CombatText.NewText(Player.Hitbox, CombatText.DamagedFriendly, lifeChange * -1);
                    if (Player.statLife <= 0)
                    {
                        string deathMessage = LuckyPresentKillText();
                        Player.KillMe(PlayerDeathReason.ByCustomReason(deathMessage), 0, 0, true);
                    }
                }
            }
            else
            {
                int manaChange;
                if (!buff)
                {
                    manaChange = -50;
                    for (int i = 0; i < 5; i++)
                    {
                        manaChange -= (Main.rand.Next(100) < 75) ? 5 : 0;
                    }
                }
                else
                {
                    manaChange = 100;
                    for (int i = 0; i < 10; i++)
                    {
                        manaChange += (Main.rand.Next(100) < 80) ? 10 : 0;
                    }
                }

                if (Player == Main.LocalPlayer)
                {
                    Player.ManaEffect(manaChange);
                }
                Player.statMana += manaChange;
                if (Player.statMana > Player.statManaMax2)
                {
                    Player.statMana = Player.statManaMax2;
                }
                if (Player.statMana < 0)
                {
                    Player.statMana = 0;
                }
            }
        }
        private void LuckyPresentFX(bool goodRng)
        {
            if (goodRng)
            {
                SoundEngine.PlaySound(SoundID.Item129, Player.Center);
                for (int i = 0; i < 20; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
                    Dust d = Dust.NewDustPerfect(Player.Center + speed * 5f, Main.rand.Next(139, 143), speed * 10f, Scale: 1f);
                    d.noGravity = true;
                }
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Item16, Player.Center);
                for (int i = 0; i < 20; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
                    Dust d = Dust.NewDustPerfect(Player.Center + speed * 5f, DustID.FartInAJar, speed * 10f, Scale: 1f);
                    d.noGravity = true;
                }
            }
        }
        private string LuckyPresentKillText()
        {
            if (ModContent.GetInstance<ConfigServer>().PresentDeathText)
            {
                List<string> messageList = new List<string>();
                messageList.Add($"{Player.name} isn't good at Lucky Block.");
                messageList.Add($"{Player.name} got very unlucky.");
                messageList.Add($"{Player.name} opened the wrong present at the wrong time.");
                messageList.Add($"{Player.name} shouldn't have found out what's in the box.");
                messageList.Add($"{Player.name} unpacked the gift of spontaneous death.");
                messageList.Add($"{Player.name} must have been extremley naughty this year.");
                messageList.Add($"{Player.name} went all in and lost.");
                messageList.Add($"{Player.name} certainly didn't get what was on their list.");
                messageList.Add($"{Player.name} has learned that not all presents are good presents.");
                if (Player.Male)
                {
                    messageList.Add($"{Player.name} dosen't even have chance on his side.");
                }
                else
                {
                    messageList.Add($"{Player.name} dosen't even have chance on her side.");
                }
                return messageList[Main.rand.Next(messageList.Count)];
            }
            else
            {
                return $"{Player.name} was slain...";
            }
        }

        private void ResonatorTrigger()
        {
            if (Player == Main.LocalPlayer && resonatorEquip != null)
            {
                SoundEngine.PlaySound(SoundID.Item4, Player.Center);

                // Spawns inital projectiles that act as visual indicators
                for (int i = 0; i < 12; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2CircularEdge(1f, 1f);

                    Projectile.NewProjectile(Player.GetSource_Accessory(resonatorEquip), 
                        Player.Center, velocity, ModContent.ProjectileType<StellarResonance>(),
                        0, 0f, Player.whoAmI);
                }

                resonatorActive = true;
                stellarCountdown = 600;
            }
        }
        private void StellarResonator()
        {
            if (Player == Main.LocalPlayer && resonatorEquip != null && resonatorActive)
            {
                stellarCountdown--;

                if(stellarCountdown <= 0)
                {
                    resonatorActive = false;

                    // Shoot final projectile
                    SoundEngine.PlaySound(SoundID.Item68, Player.Center);

                    int dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(resonatorEquip.damage);
                    float kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(resonatorEquip.knockBack);

                    Projectile.NewProjectile(Player.GetSource_Accessory(resonatorEquip),
                        new Vector2(Player.Center.X, Player.Center.Y - 80f), new Vector2(0f, -5f), ModContent.ProjectileType<StarBeam>(),
                        dmg, kb, Player.whoAmI);
                }
                else
                {
                    // Multiplicative 10% DR
                    Player.endurance = 1f - (0.1f * (1f - Player.endurance));
                }
            }
            else
            {
                resonatorActive = false;
                stellarCountdown = -1;
            }
        }

        private void LienKnives()
        {
            if (homageEquip != null && knifeWaves > 0 && knifeTimer <= 0 && Main.myPlayer == Player.whoAmI)
            {
                SoundEngine.PlaySound(SoundID.Item43, Player.Center);

                Vector2 velocity = SimpleProjVelocity(20f, Player.Center);
                
                int dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(homageEquip.damage);
                float kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(homageEquip.knockBack);
                for(int i = 0; i < 9; i++)
                {
                    Vector2 finalVel = velocity;
                    if (i > 0)
                    {
                        finalVel = finalVel.RotatedByRandom(MathHelper.ToRadians(20f));
                    }
                     
                    Projectile.NewProjectile(Player.GetSource_Accessory(homageEquip), Player.Center, finalVel, ModContent.ProjectileType<LienKnife>(),
                    dmg, kb, Player.whoAmI);
                }
                
                knifeWaves--;
                knifeTimer = 12;
            }
        }

        private void AuroraBorealis()
        {
            SoundStyle sfx = new SoundStyle($"{nameof(ArtificerMod)}/Assets/AuroraSFX")
            {
                Volume = 1f,
                PitchVariance = 0.15f,
                MaxInstances = 3,
            };
            SoundEngine.PlaySound(sfx, Player.Center);
            Player.SetImmuneTimeForAllTypes(Player.longInvince ? 300 : 200); // I-frames
            if (Player == Main.LocalPlayer && starlightRiverReference != null && Player.ownedProjectileCounts[ModContent.ProjectileType<AuroraBorealis>()] < 1) // Aura projectile
            {
                Projectile.NewProjectile(Player.GetSource_Accessory(starlightRiverReference),
                        Player.Center, Vector2.Zero, ModContent.ProjectileType<AuroraBorealis>(),
                        0, 0f, Player.whoAmI);
            }
        }

        private void MoonfallOribtalStrike()
        {
            if (Player == Main.LocalPlayer && moonfallEquip != null && Player.ownedProjectileCounts[ModContent.ProjectileType<MoonfallBeam>()] < 1) 
            {
                // Flare FX
                Projectile.NewProjectile(Player.GetSource_Accessory(moonfallEquip),
                        Player.Center, new Vector2(0f, -15f), ModContent.ProjectileType<MoonfallFlare>(),
                        0, 0f, Player.whoAmI);

                // Actual laser
                int dmg = (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(moonfallEquip.damage);
                float kb = Player.GetTotalDamage(DamageClass.Generic).ApplyTo(moonfallEquip.knockBack);
                Vector2 spawnPos = Main.MouseWorld;
                Projectile.NewProjectile(Player.GetSource_Accessory(moonfallEquip),
                        spawnPos, new Vector2(0f, 1f), ModContent.ProjectileType<MoonfallBeam>(),
                        dmg, kb, Player.whoAmI);
            }
        }

        private void BuffEndChecks()
        {
            CooldownEnd();
            ArtificerArmorsEnd();
            ChargerEnd();
            RetributionEnd();
            StealthEnd();
        }

        private void ArtificerArmorsEnd()
        {
            if(artificerArmorFlagA == true && artificerArmorActive == false)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(4f, 4f);
                    Dust d = Dust.NewDustPerfect(Player.Center, DustID.MagicMirror, speed, Scale: 1.25f);
                    d.noGravity = true;
                }
            }
            if (powerArmorFlagA == true && powerArmorActive == false)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(4f, 4f);
                    Dust d = Dust.NewDustPerfect(Player.Center, DustID.Electric, speed, Scale: 1.25f);
                    d.noGravity = true;
                }
            }
        }
        private void ChargerEnd()
        {
            if (chargerFlagL == true && lifeChargerActive == false && lifeChargerSuccess)
            {
                SoundEngine.PlaySound(SoundID.Item4, Player.Center);
                int healAmount = 100;
                if (autoHeal)
                {
                    healAmount = 125;
                }
                Player.Heal(healAmount);
                if (Player.statLife > Player.statLifeMax2)
                {
                    Player.statLife = Player.statLifeMax2;
                }
                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(6f, 6f);
                    Dust d = Dust.NewDustPerfect(Player.Center, DustID.HealingPlus, speed, 0, new Color(255, 0, 0), 1.25f);
                    d.noGravity = true;
                }
                lifeChargerSuccess = false;
            }
            if (chargerFlagM == true && manaChargerActive == false && manaChargerSuccess)
            {
                SoundEngine.PlaySound(SoundID.Item4, Player.Center);
                Player.statMana += 150;
                Player.ManaEffect(150);
                if (Player.statMana > Player.statManaMax2)
                {
                    Player.statMana = Player.statManaMax2;
                }
                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(6f, 6f);
                    Dust d = Dust.NewDustPerfect(Player.Center, DustID.ManaRegeneration, speed, 130, Scale: 1.25f);
                    d.noGravity = true;
                }
                manaChargerSuccess = false;
            }
        }
        private void RetributionEnd()
        {
            if (retributionFlag == true && retributionActive == false)
            {
                retributionPower = 0;
                retributionCharge = 0;
            }
        }
        private void StealthEnd()
        {
            if (stealthAFlag == true && stealthActive == false)
            {
                StealthFX();
            }
        }
        private void CooldownEnd()
        {
            if (cooldownFlag == true && !Player.HasBuff(ModContent.BuffType<AbilityCooldown>()))
            {
                bool defaultSFX = true;
                for (int i = 0; i < 10; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(15f, 15f);
                    Dust d = Dust.NewDustPerfect(Player.Center + speed, DustID.MagicMirror, speed * 0.35f, Scale: 1.2f);
                    d.noGravity = true;
                }

                if (ModContent.GetInstance<ConfigClient>().cooldownAwareness && Player == Main.LocalPlayer)
                {
                    for(int i = 0; i < 15; i++)
                    {
                        int dustID;
                        switch (Main.rand.Next(4))
                        {
                            case 0:
                                dustID = DustID.YellowStarDust;
                                break;
                            case 1:
                                dustID = DustID.FireworkFountain_Yellow;
                                break;
                            case 2:
                                dustID = DustID.Firework_Yellow;
                                break;
                            default:
                                dustID = DustID.YellowTorch;
                                break;
                        }
                        Vector2 speed = Main.rand.NextVector2Circular(30f, 30f);
                        Dust d = Dust.NewDustPerfect(Player.Center + speed, dustID, speed * 0.35f, Scale: 0.75f);
                        d.noGravity = true;
                    }

                    CombatText.NewText(Player.Hitbox, Colors.RarityYellow, Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.AbilitiesRefreshed"));

                    if (ModContent.GetInstance<ConfigClient>().xenoHaloShields)
                    {
                        defaultSFX = false;
                        SoundStyle haloChargeEnd = new SoundStyle($"{nameof(ArtificerMod)}/Assets/HaloShieldRechargeEnd")
                        {
                            Volume = 0.9f,
                            PitchVariance = 0,
                            MaxInstances = 1,
                        };
                        SoundEngine.PlaySound(haloChargeEnd, Player.Center);
                    }
                }

                if(defaultSFX)
                {
                    SoundEngine.PlaySound(SoundID.MaxMana, Player.Center);
                }
            }
        }

        public NPC FindClosestNPC(float maxDetectionDistance, bool requireLoS, NPC blacklist1 = null, NPC blacklist2 = null)
        {
            NPC closestNPC = null;
            float sqrMaxDetectDistance = maxDetectionDistance * maxDetectionDistance;
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
                
                if (target.CanBeChasedBy() && (Collision.CanHit(Player, target) || !requireLoS))
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Player.Center);
                    if (sqrDistanceToTarget < sqrMaxDetectDistance && target != blacklist1 && target != blacklist2)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }
            return closestNPC;
        }

        public bool CheckForHostiles(float maxDetectionDistance, bool requireLoS = false, bool excludeStatues = true)
        {
            float sqrMaxDetectDistance = maxDetectionDistance * maxDetectionDistance;
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];

                if (target.CanBeChasedBy() && (Collision.CanHit(Player, target) || !requireLoS) && (!target.SpawnedFromStatue || !excludeStatues))
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Player.Center);
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}