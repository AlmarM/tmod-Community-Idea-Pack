using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace CommunityPack.Projectiles
{
    public abstract class GemProjectile : ModProjectile
    {
        protected bool pingPongAnimation;

        int frameAnimationDirection;

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.scale = 0.80f;

            pingPongAnimation = true;
            frameAnimationDirection = 1;
        }

        protected abstract Color GetGemColor();

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;

            projectile.frameCounter++;
            if (projectile.frameCounter == 5)
            {
                projectile.frameCounter = 0;

                if (pingPongAnimation)
                {
                    projectile.frame += frameAnimationDirection;
                    if (projectile.frame == Main.projFrames[projectile.type])
                    {
                        frameAnimationDirection *= -1;
                        projectile.frame = Main.projFrames[projectile.type] - 1;
                    }
                    else if (projectile.frame == -1)
                    {
                        frameAnimationDirection *= -1;
                        projectile.frame = 0;
                    }
                }
                else
                {
                    projectile.frame++;
                    if (projectile.frame == Main.projFrames[projectile.type])
                    {
                        projectile.frame = 0;
                    }
                }
            }

            if (Main.rand.NextBool(2))
            {
                Vector2 pos = projectile.Center + Utils.RandomInsideUnitCircle() * 8f;
                Vector2 vel = Utils.RandomInsideUnitCircle() * 0.25f;

                Dust.NewDustPerfect(pos, DustType<Dusts.GemDust>(), vel, newColor: GetGemColor());
            }
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(projectile.Center, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item27, projectile.position);
        }
    }
}