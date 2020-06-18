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
    public class Crabby : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crabby");

            Main.projFrames[projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = 63;
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = TimerUtils.Seconds(3f);
            projectile.ignoreWater = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(projectile.Center, 10, 10, 85);
            }
        }
    }
}
