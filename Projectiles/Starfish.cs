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
    public class Starfish : ModProjectile
    {
        float rotationSpeed;
        float rotationDirection;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starfish");

            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = 0;
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 5;
            projectile.timeLeft = 3600;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.frame = Main.rand.Next(3);

            rotationSpeed = Main.rand.NextFloat(0.1f, 0.5f);
            rotationDirection = Main.rand.NextBool() ? -1 : 1;
        }

        public override void AI()
        {
            projectile.rotation += rotationSpeed * rotationDirection;

            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(projectile.Center, 10, 10, 29);
            }
        }
    }
}
