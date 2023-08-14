using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod
{
	public class ArtificerMod : Mod
	{
        // Credit and thanks to Chem's Vanity - and in turn the Clicker Class mod - for this glowmask code!
        // TODO: Currently unused => Add more glowmasks!
        /*
        public static void BasicInWorldGlowmask(Item item, SpriteBatch spriteBatch, Texture2D glowTexture, Color color, float rotation, float scale)
        {
            spriteBatch.Draw(
                glowTexture,
                new Vector2(
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - glowTexture.Height * 0.5f
                ),
                new Rectangle(0, 0, glowTexture.Width, glowTexture.Height),
                color,
                rotation,
                glowTexture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f);
        }
        */

        // The below code is adapted from Example Mod and handles netcode, namely dodge effects
        internal enum MessageType : byte
        {
            MotherboardDodge,
            HeroShieldDodge,
            XenoShieldBlock
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();

            switch (msgType)
            {
                case MessageType.MotherboardDodge:
                case MessageType.HeroShieldDodge:
                case MessageType.XenoShieldBlock:
                    int dodgeType;
                    switch (msgType)
                    {
                        case MessageType.MotherboardDodge:
                            dodgeType = 0;
                            break;
                        case MessageType.HeroShieldDodge:
                            dodgeType = 1;
                            break;
                        default:
                            dodgeType = 2;
                            break;
                    }
                    ArtificerPlayer.HandleDodgeNetcode(reader, whoAmI, dodgeType);
                    break;
                default:
                    Logger.WarnFormat("ArtificerMod: Unknown Message type: {0}", msgType);
                    break;
            }
        }
    }
}