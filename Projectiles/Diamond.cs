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
    public class Diamond : GemProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Diamond Projectile");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.penetrate = -1;

            pingPongAnimation = false;
        }

        protected override Color GetGemColor()
        {
            return new Color(165, 180, 240);
        }
    }
}