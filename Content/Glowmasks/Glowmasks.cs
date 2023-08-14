using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Glowmasks
{
	// Credit and thanks to Chem's Vanity - and in turn the Clicker Class mod - for this glowmask code!
	public class SimpleBodyGlowPlayer : ModPlayer
	{
        private static Dictionary<int, Func<Color>> BodyColor { get; set; }

        /// <summary>
        /// Add glowmask color associated with the body equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
        /// <para>Doesn't support special drawing with extra textures</para>
        /// <para>Don't forget the !Main.dedServ check!</para>
        /// </summary>
        /// <param name="bodySlot">Body equip slot</param>
        /// <param name="color">Color</param>
        public static void RegisterData(int bodySlot, Func<Color> color)
		{
			if (!BodyColor.ContainsKey(bodySlot))
				BodyColor.Add(bodySlot, color);
		}

		public override void Load()
		{
			BodyColor = new Dictionary<int, Func<Color>>();
		}

		public override void Unload()
		{
			BodyColor = null;
		}

		public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
		{
            bool noWolf = true;
            if(Player.mount.Type == MountID.Wolf)
            {
                if (Player.mount.Active)
                {
                    noWolf = false;
                }
            }

			if (BodyColor.TryGetValue(Player.body, out Func<Color> color)
                && noWolf)
            {
                // Coloring exception for Radiant Armor
                if(Player.body == EquipLoader.GetEquipSlot(ModLoader.GetMod("ArtificerMod"), "RadiantBreastplate", EquipType.Body))
                {
                    Color radiantColor = new Color(255, 255, 255, 0) * 0.75f;
                    Color originalColor = Player.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
                    Color lighting = Lighting.GetColor((int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f));
                    radiantColor = GlowmaskHelpers.AdjustColorForLighting(radiantColor, lighting, originalColor);

                    drawInfo.bodyGlowColor = radiantColor;
                    drawInfo.armGlowColor = radiantColor;
                }
                else
                {
                    drawInfo.bodyGlowColor = color();
                    drawInfo.armGlowColor = color();
                }
            }
		}
	}
	public class DrawLayerData
	{
		public static Color DefaultColor(PlayerDrawSet drawInfo) => Color.White;

		public Asset<Texture2D> Texture { get; init; }

		public Func<PlayerDrawSet, Color> OverrideColor { get; init; } = DefaultColor;

        public Asset<Texture2D> ExtraTextureShield { get; init; }

        public Asset<Texture2D> ExtraTextureVisor { get; init; }

        public Asset<Texture2D> ExtraTextureNight { get; init; }

        public bool RadiantColoring { get; init; } = false;
    }
    public class SpecialBodyLayerData
    {
        public static Color DefaultColor(PlayerDrawSet drawInfo) => Color.White;

        public Func<PlayerDrawSet, Color> OverrideColor { get; init; } = DefaultColor;

        public Asset<Texture2D> TextureShield { get; init; }

        public Asset<Texture2D> TextureNight { get; init; }

        public Asset<Texture2D> TextureDay { get; init; }
    }

    public sealed class HeadLayer : PlayerDrawLayer
	{
		private static Dictionary<int, DrawLayerData> HeadLayerData { get; set; }

        /// <summary>
        /// Add data associated with the head equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
        /// <para>Don't forget the !Main.dedServ check!</para>
        /// </summary>
        /// <param name="headSlot">Head equip slot</param>
        /// <param name="data">Data</param>
        public static void RegisterData(int headSlot, DrawLayerData data)
		{
			if (!HeadLayerData.ContainsKey(headSlot))
                HeadLayerData.Add(headSlot, data);
        }

		public override void Load()
		{
			HeadLayerData = new Dictionary<int, DrawLayerData>();
		}

		public override void Unload()
		{
			HeadLayerData = null;
        }

        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.Head);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            bool wolf = false;
            if (drawPlayer.mount.Type == MountID.Wolf)
            {
                if (drawPlayer.mount.Active)
                {
                    wolf = true;
                }
            }

            if (drawPlayer.dead || drawPlayer.invis || drawPlayer.head == -1 || wolf)
            {
                return false;
            }

            return true;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            if (!HeadLayerData.TryGetValue(drawPlayer.head, out DrawLayerData data))
            {
                return;
            }

            Color originalColor = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Color lighting = Lighting.GetColor((int)(drawPlayer.Center.X / 16f), (int)(drawPlayer.Center.Y / 16f));
            Color color = drawPlayer.GetImmuneAlphaPure(data.OverrideColor(drawInfo), drawInfo.shadow);
            // Additional code for special cases
            if (data.RadiantColoring)
            {
                color = new Color(255, 255, 255, 0) * 0.75f;
                color = GlowmaskHelpers.AdjustColorForLighting(color, lighting, originalColor);
            }

            Texture2D texture = data.Texture.Value;
            // Additional code for special cases
            ArtificerPlayer drawArtificer = drawPlayer.GetModPlayer<ArtificerPlayer>();
            if (drawArtificer == null)
            {
                return;
            }
            if (data.ExtraTextureNight != null)
            {
                if (!drawArtificer.starplateSetBonus)
                {
                    return;
                }
                else if (!Main.dayTime)
                {
                    texture = data.ExtraTextureNight.Value;
                }
            }

            if (texture == null)
            {
                return;
            }
            Vector2 drawPos = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.headPosition;
            Vector2 headVect = drawInfo.headVect;
            DrawData drawData = new DrawData(texture, drawPos.Floor() + headVect, drawPlayer.bodyFrame, color, drawPlayer.headRotation, headVect, 1f, drawInfo.playerEffect, 0)
            {
                shader = drawInfo.cHead
            };
            drawInfo.DrawDataCache.Add(drawData);
            
            // Additional code for special cases

            if(data.ExtraTextureVisor != null)
            {
                color = new Color(255, 255, 255, 0) * 0.6f;
                color.A = 100;
                Texture2D textureVisor = data.ExtraTextureVisor.Value;
                DrawData drawDataVisor = new DrawData(textureVisor, drawPos.Floor() + headVect, drawPlayer.bodyFrame, color, drawPlayer.headRotation, headVect, 1f, drawInfo.playerEffect, 0)
                {
                    shader = drawInfo.cHead
                };
                drawInfo.DrawDataCache.Add(drawDataVisor);
            }

            if (data.ExtraTextureShield != null)
            {
                if (!drawArtificer.xenoSetBonus)
                {
                    return;
                }
                color = GlowmaskHelpers.XenoShieldLighting(drawArtificer, lighting, originalColor);
                Texture2D textureShield = data.ExtraTextureShield.Value;
                DrawData drawDataShield = new DrawData(textureShield, drawPos.Floor() + headVect, drawPlayer.bodyFrame, color, drawPlayer.headRotation, headVect, 1f, drawInfo.playerEffect, 0)
                {
                    shader = drawInfo.cHead
                };
                drawInfo.DrawDataCache.Add(drawDataShield);
            }
        }
    }
    public sealed class LegsLayer : PlayerDrawLayer
    {
        private static Dictionary<int, DrawLayerData> LegsLayerData { get; set; }

        /// <summary>
        /// Add data associated with the leg equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
        /// <para>Don't forget the !Main.dedServ check!</para>
        /// </summary>
        /// <param name="legSlot">Leg equip slot</param>
        /// <param name="data">Data</param>
        public static void RegisterData(int legSlot, DrawLayerData data)
        {
            if (!LegsLayerData.ContainsKey(legSlot))
            {
                LegsLayerData.Add(legSlot, data);
            }
        }

        public override void Load()
        {
            LegsLayerData = new Dictionary<int, DrawLayerData>();
        }

        public override void Unload()
        {
            LegsLayerData = null;
        }

        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.Leggings);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            bool wolf = false;
            if (drawPlayer.mount.Type == MountID.Wolf)
            {
                if (drawPlayer.mount.Active)
                {
                    wolf = true;
                }
            }

            if (drawPlayer.dead || drawPlayer.invis || drawPlayer.legs == -1 || wolf)
            {
                return false;
            }

            return true;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            if (!LegsLayerData.TryGetValue(drawPlayer.legs, out DrawLayerData data))
            {
                return;
            }

            Color originalColor = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Color lighting = Lighting.GetColor((int)(drawPlayer.Center.X / 16f), (int)(drawPlayer.Center.Y / 16f));
            Color color = drawPlayer.GetImmuneAlphaPure(data.OverrideColor(drawInfo), drawInfo.shadow);
            // Additional code for special cases
            if (data.RadiantColoring)
            {
                color = new Color(255, 255, 255, 0) * 0.75f;
                color = GlowmaskHelpers.AdjustColorForLighting(color, lighting, originalColor);
            }

            Texture2D texture = data.Texture.Value;
            // Additional code for special cases
            ArtificerPlayer drawArtificer = drawPlayer.GetModPlayer<ArtificerPlayer>();
            if (drawArtificer == null)
            {
                return;
            }
            if (data.ExtraTextureNight != null)
            {
                if (!drawArtificer.starplateSetBonus)
                {
                    return;
                }
                else if (!Main.dayTime)
                {
                    texture = data.ExtraTextureNight.Value;
                }
            }

            if(texture == null)
            {
                return;
            }

            Vector2 drawPos = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.legFrame.Width / 2, drawPlayer.height - drawPlayer.legFrame.Height + 4f) + drawPlayer.legPosition;
            Vector2 legsOffset = drawInfo.legsOffset;

            if (drawInfo.isSitting && (!DrawSitHelper.ShouldOverrideLegs_CheckShoes(ref drawInfo) || drawPlayer.wearsRobe))
            {
                // Fixes drawing while sitting
                DrawSitHelper.DrawSittingLegsMethod(ref drawInfo, texture, color, drawInfo.cLegs);
            }
            else
            {
                DrawData drawData = new DrawData(texture, drawPos.Floor() + legsOffset, drawPlayer.legFrame, color, drawPlayer.legRotation, drawInfo.legsOffset, 1f, drawInfo.playerEffect, 0)
                {
                    shader = drawInfo.cLegs
                };
                drawInfo.DrawDataCache.Add(drawData);
            }

            // Additional code for special cases
            if (data.ExtraTextureShield != null)
            {
                if (!drawArtificer.xenoSetBonus)
                {
                    return;
                }
                color = GlowmaskHelpers.XenoShieldLighting(drawArtificer, lighting, originalColor);
                Texture2D textureShield = data.ExtraTextureShield.Value;

                // Fixes drawing while sitting
                if (drawInfo.isSitting && (!DrawSitHelper.ShouldOverrideLegs_CheckShoes(ref drawInfo) || drawPlayer.wearsRobe))
                {
                    DrawSitHelper.DrawSittingLegsMethod(ref drawInfo, textureShield, color, drawInfo.cLegs);
                    return;
                }

                DrawData drawDataShield = new DrawData(textureShield, drawPos.Floor() + legsOffset, drawPlayer.legFrame, color, drawPlayer.legRotation, legsOffset, 1f, drawInfo.playerEffect, 0)
                {
                    shader = drawInfo.cLegs
                };
                drawInfo.DrawDataCache.Add(drawDataShield);
            }
        }
    }
    
    public sealed class CapeLayer : PlayerDrawLayer
    {
        private static Dictionary<int, DrawLayerData> BackLayerData { get; set; }

        /// <summary>
        /// Add data associated with the leg equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
        /// <para>Don't forget the !Main.dedServ check!</para>
        /// </summary>
        /// <param name="bodySlot">Body equip slot</param>
        /// <param name="data">Data</param>
        public static void RegisterData(int bodySlot, DrawLayerData data)
        {
            if (!BackLayerData.ContainsKey(bodySlot))
            {
                BackLayerData.Add(bodySlot, data);
            }
        }

        public override void Load()
        {
            BackLayerData = new Dictionary<int, DrawLayerData>();
        }

        public override void Unload()
        {
            BackLayerData = null;
        }

        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.BackAcc);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            bool wolf = false;
            if (drawPlayer.mount.Type == MountID.Wolf)
            {
                if (drawPlayer.mount.Active)
                {
                    wolf = true;
                }
            }

            if (drawPlayer.dead || drawPlayer.invis || drawPlayer.body == -1 || wolf || drawPlayer.back != -1 || drawPlayer.backpack != -1)
            {
                return false;
            }

            return true;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            if (!BackLayerData.TryGetValue(drawPlayer.body, out DrawLayerData data))
            {
                return;
            }

            Color color = drawPlayer.GetImmuneAlphaPure(data.OverrideColor(drawInfo), drawInfo.shadow);

            Texture2D texture = data.Texture.Value;
            Vector2 drawPos = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.bodyPosition;
            drawPos += drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2);
            Vector2 headGearOffset = Main.OffsetsPlayerHeadgear[drawPlayer.bodyFrame.Y / drawPlayer.bodyFrame.Height];
            headGearOffset.Y -= 2f;
            drawPos += headGearOffset * -drawInfo.playerEffect.HasFlag(SpriteEffects.FlipVertically).ToDirectionInt();
            DrawData drawData = new DrawData(texture, drawPos, drawPlayer.bodyFrame, color, drawPlayer.bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect, 0)
            {
                shader = drawInfo.cBody
            };
            drawInfo.DrawDataCache.Add(drawData);
        }
    }

    public sealed class WingsLayer : PlayerDrawLayer
    {
        private static Dictionary<int, DrawLayerData> WingsLayerData { get; set; }

        /// <summary>
        /// Add data associated with the wings equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
        /// <para>Don't forget the !Main.dedServ check!</para>
        /// </summary>
        /// <param name="wingSlot">Wings equip slot</param>
        /// <param name="data">Data</param>
        public static void RegisterData(int wingSlot, DrawLayerData data)
        {
            if (!WingsLayerData.ContainsKey(wingSlot))
            {
                WingsLayerData.Add(wingSlot, data);
            }
        }

        public override void Load()
        {
            WingsLayerData = new Dictionary<int, DrawLayerData>();
        }

        public override void Unload()
        {
            WingsLayerData = null;
        }

        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.Wings);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            if (drawPlayer.dead || drawPlayer.invis || drawPlayer.wings == -1)
            {
                return false;
            }
            return true;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            if (!WingsLayerData.TryGetValue(drawPlayer.wings, out DrawLayerData data))
            {
                return;
            }

            Color color = drawPlayer.GetImmuneAlphaPure(data.OverrideColor(drawInfo) * drawInfo.stealth * (1f - drawInfo.shadow), drawInfo.shadow); // Wing glowmasks need the additional stealth/shadow multiplier for some reason

            Texture2D texture = data.Texture.Value;

            Vector2 directions = drawPlayer.Directions;
            Vector2 offset = new Vector2(0f, 7f);
            Vector2 position = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height / 2) + offset;

            int num11 = 0;
            int num12 = 0;
            int numFrames = 4;

            position += new Vector2(num12 - 9, num11 + 2) * directions;
            position = position.Floor();
            Rectangle frame = new Rectangle(0, texture.Height / numFrames * drawPlayer.wingFrame, texture.Width, texture.Height / numFrames);
            DrawData drawData = new DrawData(texture, position.Floor(), frame, color, drawPlayer.bodyRotation, new Vector2(texture.Width / 2, texture.Height / numFrames / 2), 1f, drawInfo.playerEffect, 0)
            {
                shader = drawInfo.cWings
            };
            drawInfo.DrawDataCache.Add(drawData);
        }
    }
    

    // The below classes are used for special, extra body glowmasks
    // TODO: Find a way to re-write. Code currently taken from decompiled Thorium
    public class SpecialBodyLayer : PlayerDrawLayer
    {
        private static Dictionary<int, SpecialBodyLayerData> BodyLayerData { get; set; }

        /// <summary>
        /// Add data associated with the head equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
        /// <para>Don't forget the !Main.dedServ check!</para>
        /// </summary>
        /// <param name="bodySlot">Head equip slot</param>
        /// <param name="data">Data</param>
        public static void RegisterData(int bodySlot, SpecialBodyLayerData data)
        {
            if (!BodyLayerData.ContainsKey(bodySlot))
                BodyLayerData.Add(bodySlot, data);
        }

        public override void Load()
        {
            BodyLayerData = new Dictionary<int, SpecialBodyLayerData>();
        }

        public override void Unload()
        {
            BodyLayerData = null;
        }

        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.Torso);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            bool wolf = false;
            if (drawPlayer.mount.Type == MountID.Wolf)
            {
                if (drawPlayer.mount.Active)
                {
                    wolf = true;
                }
            }

            if (drawPlayer.dead || drawPlayer.invis || drawPlayer.body == -1 || wolf)
            {
                return false;
            }

            return true;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            if (!BodyLayerData.TryGetValue(drawPlayer.body, out SpecialBodyLayerData data))
            {
                return;
            }

            Color originalColor = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Color lighting = Lighting.GetColor((int)(drawPlayer.Center.X / 16f), (int)(drawPlayer.Center.Y / 16f));
            Color color = drawPlayer.GetImmuneAlphaPure(data.OverrideColor(drawInfo), drawInfo.shadow);

            // Determining the texture to use
            Texture2D texture = null;
            ArtificerPlayer drawArtificer = drawPlayer.GetModPlayer<ArtificerPlayer>();
            if (drawArtificer == null)
            {
                return;
            }
            if (data.TextureNight != null)
            {
                if (!drawArtificer.starplateSetBonus)
                {
                    return;
                }
                texture = data.TextureDay.Value;
                if (!Main.dayTime)
                {
                    texture = data.TextureNight.Value;
                }
            }
            if (data.TextureShield != null)
            {
                if (!drawArtificer.xenoSetBonus)
                {
                    return;
                }
                texture = data.TextureShield.Value;
                color = GlowmaskHelpers.XenoShieldLighting(drawArtificer, lighting, originalColor);
            }

            if (texture == null)
            {
                return;
            }

            // Adapted from vanilla's player drawing code
            Vector2 vector = new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2(drawInfo.drawPlayer.bodyFrame.Width / 2, drawInfo.drawPlayer.bodyFrame.Height / 2);
            Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawInfo.drawPlayer.bodyFrame.Y / drawInfo.drawPlayer.bodyFrame.Height];
            vector2.Y -= 2f;
            vector += vector2 * -((System.Enum)drawInfo.playerEffect).HasFlag((System.Enum)SpriteEffects.FlipVertically).ToDirectionInt();
            float bodyRotation = drawInfo.drawPlayer.bodyRotation;
            PlayerDrawLayers.DrawCompositeArmorPiece(ref drawInfo, CompositePlayerDrawContext.Torso, 
                new DrawData(texture, vector, drawInfo.compTorsoFrame, color, bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect)
            {
                shader = drawInfo.cBody
            });
        }
    }
    public class SpecialFrontArmLayer : PlayerDrawLayer
    {
        private static Dictionary<int, SpecialBodyLayerData> BodyLayerData { get; set; }

        /// <summary>
        /// Add data associated with the head equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
        /// <para>Don't forget the !Main.dedServ check!</para>
        /// </summary>
        /// <param name="bodySlot">Head equip slot</param>
        /// <param name="data">Data</param>
        public static void RegisterData(int bodySlot, SpecialBodyLayerData data)
        {
            if (!BodyLayerData.ContainsKey(bodySlot))
                BodyLayerData.Add(bodySlot, data);
        }

        public override void Load()
        {
            BodyLayerData = new Dictionary<int, SpecialBodyLayerData>();
        }

        public override void Unload()
        {
            BodyLayerData = null;
        }

        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.ArmOverItem);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            bool wolf = false;
            if (drawPlayer.mount.Type == MountID.Wolf)
            {
                if (drawPlayer.mount.Active)
                {
                    wolf = true;
                }
            }

            if (drawPlayer.dead || drawPlayer.invis || drawPlayer.body == -1 || wolf)
            {
                return false;
            }

            return true;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            if (!BodyLayerData.TryGetValue(drawPlayer.body, out SpecialBodyLayerData data))
            {
                return;
            }

            Color originalColor = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Color lighting = Lighting.GetColor((int)(drawPlayer.Center.X / 16f), (int)(drawPlayer.Center.Y / 16f));
            Color color = drawPlayer.GetImmuneAlphaPure(data.OverrideColor(drawInfo), drawInfo.shadow);

            // Determining the texture to use
            Texture2D texture = null;
            ArtificerPlayer drawArtificer = drawPlayer.GetModPlayer<ArtificerPlayer>();
            if (drawArtificer == null)
            {
                return;
            }
            if (data.TextureNight != null)
            {
                if (!drawArtificer.starplateSetBonus)
                {
                    return;
                }
                texture = data.TextureDay.Value;
                if (!Main.dayTime)
                {
                    texture = data.TextureNight.Value;
                }
            }
            else if (data.TextureShield != null)
            {
                if (!drawArtificer.xenoSetBonus)
                {
                    return;
                }
                texture = data.TextureShield.Value;
                color = GlowmaskHelpers.XenoShieldLighting(drawArtificer, lighting, originalColor);
            }

            if (texture == null)
            {
                return;
            }

            // Adapted from vanilla's player drawing code
            Vector2 vector = new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2(drawInfo.drawPlayer.bodyFrame.Width / 2, drawInfo.drawPlayer.bodyFrame.Height / 2);
            Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawInfo.drawPlayer.bodyFrame.Y / drawInfo.drawPlayer.bodyFrame.Height];
            vector2.Y -= 2f;
            vector += vector2 * -((System.Enum)drawInfo.playerEffect).HasFlag((System.Enum)SpriteEffects.FlipVertically).ToDirectionInt();
            float bodyRotation = drawInfo.drawPlayer.bodyRotation;
            float rotation = drawInfo.drawPlayer.bodyRotation + drawInfo.compositeFrontArmRotation;
            Vector2 bodyVect = drawInfo.bodyVect;
            Vector2 compositeOffset_FrontArm = GlowmaskHelpers.GetFrontArmOffset(ref drawInfo);
            bodyVect += compositeOffset_FrontArm;
            vector += compositeOffset_FrontArm;
            Vector2 position = vector + drawInfo.frontShoulderOffset;
            if (drawInfo.compFrontArmFrame.X / drawInfo.compFrontArmFrame.Width >= 7)
            {
                vector += new Vector2((!((System.Enum)drawInfo.playerEffect).HasFlag((System.Enum)SpriteEffects.FlipHorizontally)) ? 1 : (-1), (!((System.Enum)drawInfo.playerEffect).HasFlag((System.Enum)SpriteEffects.FlipVertically)) ? 1 : (-1));
            }
            int num2 = (drawInfo.compShoulderOverFrontArm ? 1 : 0);
            int num3 = ((!drawInfo.compShoulderOverFrontArm) ? 1 : 0);
            for (int i = 0; i < 2; i++)
            {
                if (i == num2 && !drawInfo.hideCompositeShoulders)
                {
                    PlayerDrawLayers.DrawCompositeArmorPiece(ref drawInfo, CompositePlayerDrawContext.FrontShoulder, 
                        new DrawData(texture, position, drawInfo.compFrontShoulderFrame, color, bodyRotation, bodyVect, 1f, drawInfo.playerEffect)
                    {
                        shader = drawInfo.cBody
                    });
                }
                if (i == num3)
                {
                    PlayerDrawLayers.DrawCompositeArmorPiece(ref drawInfo, CompositePlayerDrawContext.FrontArm, 
                        new DrawData(texture, vector, drawInfo.compFrontArmFrame, color, rotation, bodyVect, 1f, drawInfo.playerEffect)
                    {
                        shader = drawInfo.cBody
                    });
                }
            }
        }
    }
    public class SpecialBackArmLayer : PlayerDrawLayer
    {
        private static Dictionary<int, SpecialBodyLayerData> BodyLayerData { get; set; }

        /// <summary>
        /// Add data associated with the head equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
        /// <para>Don't forget the !Main.dedServ check!</para>
        /// </summary>
        /// <param name="bodySlot">Head equip slot</param>
        /// <param name="data">Data</param>
        public static void RegisterData(int bodySlot, SpecialBodyLayerData data)
        {
            if (!BodyLayerData.ContainsKey(bodySlot))
                BodyLayerData.Add(bodySlot, data);
        }

        public override void Load()
        {
            BodyLayerData = new Dictionary<int, SpecialBodyLayerData>();
        }

        public override void Unload()
        {
            BodyLayerData = null;
        }

        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.Skin);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            bool wolf = false;
            if (drawPlayer.mount.Type == MountID.Wolf)
            {
                if (drawPlayer.mount.Active)
                {
                    wolf = true;
                }
            }

            if (drawPlayer.dead || drawPlayer.invis || drawPlayer.body == -1 || wolf)
            {
                return false;
            }

            return true;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            if (!BodyLayerData.TryGetValue(drawPlayer.body, out SpecialBodyLayerData data))
            {
                return;
            }

            Color originalColor = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Color lighting = Lighting.GetColor((int)(drawPlayer.Center.X / 16f), (int)(drawPlayer.Center.Y / 16f));
            Color color = drawPlayer.GetImmuneAlphaPure(data.OverrideColor(drawInfo), drawInfo.shadow);

            // Determining the texture to use
            Texture2D texture = null;
            ArtificerPlayer drawArtificer = drawPlayer.GetModPlayer<ArtificerPlayer>();
            if (drawArtificer == null)
            {
                return;
            }
            if (data.TextureNight != null)
            {
                if (!drawArtificer.starplateSetBonus)
                {
                    return;
                }
                texture = data.TextureDay.Value;
                if (!Main.dayTime)
                {
                    texture = data.TextureNight.Value;
                }
            }
            else if (data.TextureShield != null)
            {
                if (!drawArtificer.xenoSetBonus)
                {
                    return;
                }
                texture = data.TextureShield.Value;
                color = GlowmaskHelpers.XenoShieldLighting(drawArtificer, lighting, originalColor);
            }

            if (texture == null)
            {
                return;
            }

            // Adapted from vanilla's player drawing code
            Vector2 vector = new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2(drawInfo.drawPlayer.bodyFrame.Width / 2, drawInfo.drawPlayer.bodyFrame.Height / 2);
            Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawInfo.drawPlayer.bodyFrame.Y / drawInfo.drawPlayer.bodyFrame.Height];
            vector2.Y -= 2f;
            vector += vector2 * -((System.Enum)drawInfo.playerEffect).HasFlag((System.Enum)SpriteEffects.FlipVertically).ToDirectionInt();
            vector.Y += drawInfo.torsoOffset;
            float bodyRotation = drawInfo.drawPlayer.bodyRotation;
            Vector2 vector3 = vector;
            Vector2 position = vector;
            Vector2 bodyVect = drawInfo.bodyVect;
            Vector2 compositeOffset_BackArm = GlowmaskHelpers.GetBackArmOffset(ref drawInfo);
            vector3 += compositeOffset_BackArm;
            position += drawInfo.backShoulderOffset;
            bodyVect += compositeOffset_BackArm;
            float rotation = bodyRotation + drawInfo.compositeBackArmRotation;
            if (!drawInfo.hideCompositeShoulders)
            {
                PlayerDrawLayers.DrawCompositeArmorPiece(ref drawInfo, CompositePlayerDrawContext.BackShoulder, 
                    new DrawData(texture, position, drawInfo.compBackShoulderFrame, color, bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect)
                {
                    shader = drawInfo.cBody
                });
            }
            PlayerDrawLayers.DrawCompositeArmorPiece(ref drawInfo, CompositePlayerDrawContext.BackArm, 
                new DrawData(texture, vector3, drawInfo.compBackArmFrame, color, rotation, bodyVect, 1f, drawInfo.playerEffect)
            {
                shader = drawInfo.cBody
            });
        }
    }

    // Special code for the Radiant Armor's set bonus crystal
    // Code adapted from the Forbidden Armor's set bonus
    public sealed class RadiantCrystal : PlayerDrawLayer
    {
        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.ForbiddenSetRing);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            if (drawPlayer.dead || drawPlayer.invis)
            {
                return false;
            }
            return true;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            ArtificerPlayer drawArtificer = drawPlayer.GetModPlayer<ArtificerPlayer>();
            if (drawArtificer == null)
            {
                return;
            }
            Mod mod = ModLoader.GetMod("ArtificerMod");
            Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Color originalColor = color;
            Vector2 offset = Vector2.Zero;
            Texture2D baseTexture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/RadiantCrystal").Value;
            Texture2D coreTexture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/RadiantCrystalCore").Value;
            // Gets the lighting at the player's cordinates
            Color lighting = Lighting.GetColor((int)(drawPlayer.Center.X / 16f), (int)(drawPlayer.Center.Y / 16f));
            Color baseColor = new Color(255, 255, 255, 0) * 0.75f;
            baseColor = GlowmaskHelpers.AdjustColorForLighting(baseColor, lighting, originalColor);
            if (baseTexture != null && coreTexture != null && drawArtificer.prismSetBonus && drawInfo.shadow == 0f)
            {
                // Draws main crystal
                int num2 = (int)(((float)drawPlayer.miscCounter / 300f * ((float)Math.PI * 2f)).ToRotationVector2().Y * 6f);
                float num3 = ((float)drawPlayer.miscCounter / 75f * ((float)Math.PI * 2f)).ToRotationVector2().X * 4f;
                Color color2 = new Color(baseColor.R, baseColor.G, baseColor.B, 0) * (num3 / 8f + 0.5f) * 0.8f;
                Vector2 vector = new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2(drawInfo.drawPlayer.bodyFrame.Width / 2, drawInfo.drawPlayer.bodyFrame.Height / 2);
                vector += new Vector2(-drawInfo.drawPlayer.direction * 10, (float)(-20) * drawInfo.drawPlayer.gravDir + (float)num2 * drawInfo.drawPlayer.gravDir);
                bool sunnyDay = drawInfo.drawPlayer.head == 238; // Extra effects occur when Safeman's Sunny Day is equipped
                if (sunnyDay) // Nudge sprite to be centered on the sunny day
                {
                    vector.Y += -4;
                    if (drawPlayer.direction == 1)
                    {
                        vector.X += -2;
                    }
                    else
                    {
                        vector.X += 2;
                    }
                }
                DrawData item = new DrawData(baseTexture, vector, null, baseColor, drawInfo.drawPlayer.bodyRotation, baseTexture.Size() / 2f, 1f, drawInfo.playerEffect, 0);
                item.shader = drawInfo.cBody;
                drawInfo.DrawDataCache.Add(item);
                if (sunnyDay)
                {
                    for (float num6 = 0f; num6 < 4f; num6 += 1f)
                    {
                        item = new DrawData(baseTexture, vector + (num6 * ((float)Math.PI / 2f)).ToRotationVector2() * num3, null, color2, drawInfo.drawPlayer.bodyRotation, baseTexture.Size() / 2f, 1f, drawInfo.playerEffect, 0);
                        drawInfo.DrawDataCache.Add(item);
                    }
                }
                // Draws core with special color
                Color coreColor = RadiantCrystalColor(drawArtificer.prismType, originalColor);
                color2 = new Color(coreColor.R, coreColor.G, coreColor.B, 0) * (num3 / 8f + 0.5f) * 0.8f;
                vector = new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2(drawInfo.drawPlayer.bodyFrame.Width / 2, drawInfo.drawPlayer.bodyFrame.Height / 2);
                vector += new Vector2(-drawInfo.drawPlayer.direction * 10, (float)(-20) * drawInfo.drawPlayer.gravDir + (float)num2 * drawInfo.drawPlayer.gravDir);
                if (sunnyDay) // Nudge sprite to be centered on the sunny day
                {
                    vector.Y += -4;
                    if (drawPlayer.direction == 1)
                    {
                        vector.X += -2;
                    }
                    else
                    {
                        vector.X += 2;
                    }
                }
                DrawData item2 = new DrawData(coreTexture, vector, null, coreColor, drawInfo.drawPlayer.bodyRotation, baseTexture.Size() / 2f, 1f, drawInfo.playerEffect, 0);
                item2.shader = drawInfo.cBody;
                drawInfo.DrawDataCache.Add(item2);
                for (float num6 = 0f; num6 < 4f; num6 += 1f)
                {
                    item2 = new DrawData(coreTexture, vector + (num6 * ((float)Math.PI / 2f)).ToRotationVector2() * num3, null, color2, drawInfo.drawPlayer.bodyRotation, baseTexture.Size() / 2f, 1f, drawInfo.playerEffect, 0);
                    drawInfo.DrawDataCache.Add(item2);
                }
            }
        }

        private Color RadiantCrystalColor(int prismType, Color original)
        {
            Color returnColor = original;
            if (prismType == 0) // Melee; solar colors
            {
                returnColor.R = 255;
                returnColor.G = 160;
                returnColor.B = 5;
            }
            else if (prismType == 1) // Ranged; vortex colors
            {
                returnColor.R = 35;
                returnColor.G = 255;
                returnColor.B = 150;
            }
            else if (prismType == 2) // Ranged; nebula colors
            {
                returnColor.R = 255;
                returnColor.G = 130;
                returnColor.B = 230;
            }
            else // prismType == 3 // Default to summon; stardust colors
            {
                returnColor.R = 125;
                returnColor.G = 230;
                returnColor.B = 255;
            }
            return returnColor;
        }
    }

    // Not actually a glowmask; this just draws the Sandstorm Carpet's carpet while in flight
    // Uses the same code as the vanilla Flying Carpet, and draws right on top of it
    public sealed class SandstormCarpet : PlayerDrawLayer
    {
        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.Carpet);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            if (drawPlayer.dead || drawPlayer.invis || drawInfo.drawPlayer.carpetFrame < 0)
            {
                return false;
            }
            if (!drawPlayer.TryGetModPlayer<ArtificerPlayer>(out ArtificerPlayer artificer))
            {
                return false;
            }
            return artificer.sandstormCarpet;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            if (!drawPlayer.TryGetModPlayer<ArtificerPlayer>(out ArtificerPlayer artificer))
            {
                return;
            }
            if (drawInfo.drawPlayer.carpetFrame >= 0 && artificer.sandstormCarpet)
            {
                Color colorArmorLegs = drawInfo.colorArmorLegs;
                float num = 0f;
                if (drawInfo.drawPlayer.gravDir == -1f)
                {
                    num = 10f;
                }
                Texture2D texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Items/AccessoriesPH/SandstormCarpet_Carpet").Value;
                DrawData item = new DrawData(texture, 
                    new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X + (float)(drawInfo.drawPlayer.width / 2)), 
                    (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)(drawInfo.drawPlayer.height / 2) + 28f * drawInfo.drawPlayer.gravDir + num)), 
                    new Rectangle(0, TextureAssets.FlyingCarpet.Height() / 6 * drawInfo.drawPlayer.carpetFrame, 
                    TextureAssets.FlyingCarpet.Width(), TextureAssets.FlyingCarpet.Height() / 6), 
                    colorArmorLegs, 
                    drawInfo.drawPlayer.bodyRotation, 
                    new Vector2(TextureAssets.FlyingCarpet.Width() / 2, TextureAssets.FlyingCarpet.Height() / 8), 
                    1f, drawInfo.playerEffect);
                item.shader = drawInfo.cCarpet;
                drawInfo.DrawDataCache.Add(item);
            }
        }
    }

    // Below are where helper methods stored for use by a few special cases in the classes above
    
    public class GlowmaskHelpers : ModSystem
    {
        public static Color AdjustColorForLighting(Color color, Color lighting, Color original)
        {
            // Get a white version of lighting
            if (lighting.G > lighting.R && lighting.G > lighting.B)
            {
                lighting.R = lighting.G;
                lighting.B = lighting.G;
            }
            else if (lighting.B > lighting.R && lighting.B > lighting.G)
            {
                lighting.R = lighting.B;
                lighting.G = lighting.B;
            }
            else // Red component brightest
            {
                lighting.G = lighting.R;
                lighting.B = lighting.R;
            }

            // If the shield's light is darker than 'natural' lighting, brighten it to that amount
            if (color.R < lighting.R)
            {
                color.R = lighting.R;
            }
            if (color.G < lighting.G)
            {
                color.G = lighting.G;
            }
            if (color.B < lighting.B)
            {
                color.B = lighting.B;
            }
            color.A = original.A;
            return color;
        }

        public static Color XenoShieldLighting(ArtificerPlayer shieldPlayer, Color lighting, Color original)
        {
            float shieldCharge = (float)shieldPlayer.xenoShieldPower;
            float shieldCap = (float)shieldPlayer.xenoShieldCap;
            float brightnessFactor = 0.25f + (0.75f * (shieldCharge / shieldCap));
            Color color = new Color(255, 255, 255, 0) * brightnessFactor;
            color = AdjustColorForLighting(color, lighting, original);

            return color;
        }

        // These methods are adapted from vanilla source
        public static Vector2 GetBackArmOffset(ref PlayerDrawSet drawinfo)
        {
            return new Vector2(6 * ((!((System.Enum)drawinfo.playerEffect).HasFlag((System.Enum)SpriteEffects.FlipHorizontally)) ? 1 : (-1)), 2 * ((!((System.Enum)drawinfo.playerEffect).HasFlag((System.Enum)SpriteEffects.FlipVertically)) ? 1 : (-1)));
        }

        public static Vector2 GetFrontArmOffset(ref PlayerDrawSet drawinfo)
        {
            return new Vector2(-5 * ((!((System.Enum)drawinfo.playerEffect).HasFlag((System.Enum)SpriteEffects.FlipHorizontally)) ? 1 : (-1)), 0f);
        }
    }

    // Standalone helper for drawing glowmasks properly while sitting, from the Clicker Class open source
    public class DrawSitHelper : ModSystem
    {
        public delegate void DrawSittingLegsDelegate(ref PlayerDrawSet drawinfo, Texture2D textureToDraw, Color matchingColor, int shaderIndex = 0, bool glowmask = false);

        // Note: utilizes reflection; be careful with this and keep an eye it!
        public static DrawSittingLegsDelegate DrawSittingLegsMethod { private set; get; }

        public override void Load()
        {
            var playerDrawLayersType = typeof(PlayerDrawLayers);
            var drawSittingLegsMethodInfo = playerDrawLayersType.GetMethod("DrawSittingLegs", BindingFlags.Static | BindingFlags.NonPublic);
            DrawSittingLegsMethod = (DrawSittingLegsDelegate)Delegate.CreateDelegate(typeof(DrawSittingLegsDelegate), drawSittingLegsMethodInfo);
        }

        public override void Unload()
        {
            DrawSittingLegsMethod = null;
        }

        // From vanilla
        public static bool ShouldOverrideLegs_CheckShoes(ref PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.shoe > 0 && ArmorIDs.Shoe.Sets.OverridesLegs[drawInfo.drawPlayer.shoe];
        }
    }
}