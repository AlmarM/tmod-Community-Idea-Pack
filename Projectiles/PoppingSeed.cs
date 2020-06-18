using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Net;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;

namespace CommunityPack.Projectiles
{
    public class PoppingSeed : ModProjectile
    {
        public static int BuffDuration;

        public static Color[] SeedTypeColors = new Color[]
        {
            new Color(219, 11, 11),
            new Color(11, 109, 219),
            new Color(219, 114, 11),
            new Color(121, 10, 167),
            new Color(11, 219, 11)
        };

        public static int[] OffensiveBuffIds = new int[]
        {
            BuffID.Archery,
            BuffID.MagicPower,
            BuffID.ManaRegeneration,
            BuffID.Rage,
            BuffID.Summoning,
            BuffID.Titan,
            BuffID.Wrath
        };

        public static int[] DefensiveBuffIds = new int[]
        {
            BuffID.Endurance,
            BuffID.Ironskin,
            BuffID.Lifeforce,
            BuffID.Regeneration,
            BuffID.Swiftness,
            BuffID.Thorns
        };

        public enum SeedType
        {
            Health,
            Mana,
            OffensiveBuff,
            DefensiveBuff,
            Empty
        }

        int SeedTypeId => (int)projectile.ai[0];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Popping Seed");

            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = 0;
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.timeLeft = 3600;
            projectile.tileCollide = true;
            projectile.ranged = true;
            projectile.netImportant = true;
        }

        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frameCounter = 0;

                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }

            projectile.velocity.Y = MathHelper.Min(projectile.velocity.Y + 0.1f, 16f);
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(projectile.Center, 4, 4, 31, newColor: SeedTypeColors[SeedTypeId]);
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(lightColor.ToVector3() * SeedTypeColors[SeedTypeId].ToVector3());
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);

            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(projectile.Center, 4, 4, 51, newColor: SeedTypeColors[SeedTypeId]);
            }

            int itemType = -1;
            int[] buffArray = null;
            
            switch ((SeedType)SeedTypeId)
            {
                case SeedType.Health:
                    itemType = ItemID.Heart;
                    break;

                case SeedType.Mana:
                    itemType = ItemID.Star;
                    break;

                case SeedType.OffensiveBuff:
                    buffArray = OffensiveBuffIds;
                    break;

                case SeedType.DefensiveBuff:
                    buffArray = DefensiveBuffIds;
                    break;

                case SeedType.Empty: return;
            }

            // Make sure only 1 client spawns the drops.
            if (itemType > -1)
            {
                if (projectile.owner == Main.myPlayer)
                {
                    int itemId = Item.NewItem(projectile.getRect(), itemType);

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemId, 1f);
                    }
                }

                return;
            }

            // If a buff was chosen instead, apply it.
            int buffIdApplied = buffArray[(int)(projectile.ai[1] * buffArray.Length)];
            List<int> playersAffected = Utils.ApplyBuffInArea(projectile.position, 20 * 16, buffIdApplied, TimerUtils.Seconds(BuffDuration));

            foreach (int id in playersAffected)
            {
                Player plr = Main.player[id];

                int seedDustCount = 15 + Main.rand.Next(11);
                for (int i = 0; i < seedDustCount; i++)
                {
                    Vector2 randomPosition = projectile.Center;
                    randomPosition += Utils.RandomInsideUnitCircle() * 16f;

                    Dust dust = Dust.NewDustDirect(randomPosition, 16, 16, DustType<Dusts.PoppingSeedDust>(), newColor: SeedTypeColors[SeedTypeId]);
                    dust.customData = new Dusts.PoppingSeedDust.BehaviourInfo()
                    {
                        TargetPlayerId = id,
                        SpawnPosition = randomPosition,
                        SpawnTime = Main.GameUpdateCount,
                        LerpTime = Main.rand.Next(TimerUtils.Seconds(0.5f), TimerUtils.Seconds(2f)),
                        AnimationDelay = (uint)Main.rand.Next(TimerUtils.Seconds(0.5f), TimerUtils.Seconds(1f)),
                        MaxScale = Main.rand.NextFloat(0.75f, 1.5f),
                        TargetOffset = Utils.RandomInsideUnitCircle() * plr.getRect().Width * 0.5f
                    };
                }
            }
        }
    }
}
