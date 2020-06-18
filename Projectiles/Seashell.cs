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
    public class Seashell : ModProjectile
    {
        float rotationSpeed;
        float rotationDirection;

        float GravityTime
        {
            get => projectile.localAI[0];
            set => projectile.localAI[0] = value;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seashell");
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = 0;
            projectile.width = 22;
            projectile.height = 22;
            projectile.friendly = true;
            projectile.timeLeft = 3600;
            projectile.penetrate = 3;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.scale = 0.75f;

            rotationSpeed = Main.rand.NextFloat(0.1f, 0.5f);
            rotationDirection = Main.rand.NextBool() ? -1 : 1;
        }

        public override void AI()
        {
            if (GravityTime < 20)
            {
                GravityTime++;
            }
            else
            {
                projectile.velocity.Y = MathHelper.Min(projectile.velocity.Y + 0.1f, 16f);
            }

            projectile.velocity.X = projectile.velocity.X * 0.97f;
            projectile.rotation += rotationSpeed * rotationDirection;

            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(projectile.Center, 10, 10, 29);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(SoundID.Item10, projectile.position);
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
            }

            return false;
        }
    }
}
