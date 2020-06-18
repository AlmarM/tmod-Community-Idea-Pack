using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CommunityPack.Projectiles
{
    public class VineBallProjectile : ModProjectile
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
            DisplayName.SetDefault("Vine Ball");

            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = 0;
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.frame = Main.rand.Next(3);
            
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

            projectile.rotation += rotationSpeed * rotationDirection;

            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(projectile.Center, 2, 2, DustID.GrassBlades);
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 15; i++)
            {
                Dust.NewDust(projectile.Center, 2, 2, DustID.Grass);
            }

            Main.PlaySound(SoundID.Dig, projectile.position);
        }
    }
}
