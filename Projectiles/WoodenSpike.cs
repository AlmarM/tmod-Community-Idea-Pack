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
    public class WoodenSpike : Spike
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wooden Spike Shot");
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(projectile.Center, 10, 10, 28);
            }
        }
    }
}