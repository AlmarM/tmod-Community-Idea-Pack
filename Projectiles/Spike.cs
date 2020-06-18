using System.IO;
using Terraria.Localization;
using Terraria;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Terraria.ModLoader.Config;
using static Terraria.ModLoader.ModContent;
using Terraria.UI;
using System;
using Microsoft.Xna.Framework;

namespace CommunityPack.Projectiles
{
    public class Spike : ModProjectile
    {
        protected int state
        {
            get => (int)projectile.ai[0];
            set => projectile.ai[0] = value;
        }

        protected int targetId
        {
            get => (int)projectile.ai[1];
            set => projectile.ai[1] = value;
        }

        protected Vector2 hitOffset
        {
            get
            {
                return new Vector2(projectile.localAI[0], projectile.localAI[1]);
            }

            set
            {
                projectile.localAI[0] = value.X;
                projectile.localAI[1] = value.Y;
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spike Shot");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 18;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.timeLeft = TimerUtils.Seconds(2f);
            projectile.noDropItem = false;
            projectile.netImportant = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (state == 0)
            {
                state = 1;
                targetId = target.whoAmI;

                projectile.netUpdate = true;
            }
        }

        public override void AI()
        {
            if (state == 0)
            {
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
                return;
            }

            NPC targetNpc = Main.npc[targetId];

            if (state == 1)
            {
                // Prevent further automatic damage
                projectile.friendly = false;
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
                projectile.velocity = Vector2.Zero;

                hitOffset = targetNpc.Center - projectile.Center;

                state = 2;

                return;
            }

            if (targetNpc.CanBeChasedBy())
            {
                projectile.Center = targetNpc.Center - hitOffset;

                if (projectile.timeLeft % TimerUtils.Seconds(0.5f) == 0)
                {
                    targetNpc.StrikeNPC(projectile.damage, projectile.knockBack, -targetNpc.direction);
                }
            }
            else
            {
                projectile.Kill();
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(projectile.Center, 10, 10, 1);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(projectile.Center, projectile.velocity, 10, 10);
            Main.PlaySound(SoundID.Item10, projectile.Center);

            return true;
        }
    }
}
