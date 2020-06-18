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

namespace CommunityPack.Items.Ammo
{
    public class DarkGreenSolutionItem : ModItem
    {
        public static bool Enabled;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Green Solution");
            Tooltip.SetDefault( "Used by the Clentaminator"
                            +   "\nSpreads the jungle");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(silver: 5);
            item.consumable = true;
            item.rare = ItemRarityID.Orange;
            item.width = 10;
            item.height = 12;
            item.maxStack = 999;
            item.shoot = ProjectileType<Projectiles.DarkGreenSolutionProjectile>() - ProjectileID.PureSpray;
            item.ammo = AmmoID.Solution;
            item.consumable = true;
        }
    }
}
