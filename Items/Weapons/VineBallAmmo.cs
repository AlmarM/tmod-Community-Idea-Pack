using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CommunityPack.Items.Weapons
{
    public class VineBallAmmo : ModItem
    {
        public static int Damage;
        public static float KnockBack;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'This should pack a punch'");
            DisplayName.SetDefault("Vine Ball");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(copper: 1);
            item.consumable = true;
            item.rare = ItemRarityID.White;
            item.maxStack = 999;
            item.width = 18;
            item.height = 17;
            item.damage = Damage;
            item.knockBack = KnockBack;
            item.shoot = ProjectileType<Projectiles.VineBallProjectile>();
            item.ranged = true;
            item.ammo = item.type;
        }

        public override void AddRecipes()
        {
            if (!NPCs.JungleMimic.Enabled) return;

            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VineRope, 3);
            recipe.AddIngredient(ItemID.ClayBlock, 1);
            recipe.AddIngredient(ItemID.MudBlock, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
