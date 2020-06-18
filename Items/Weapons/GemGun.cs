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

namespace CommunityPack.Items.Weapons
{
    public class GemGun : ModItem
    {
        public static bool Enabled;
        public static int UseTime;
        public static float SpreadAngle;
        public static int GemAmmoWoodRecipeCount;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault( "Uses gems for ammo\n"
                            +   "Higher valued gems do more damage");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(gold: 6);
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.autoReuse = true;
            item.useAnimation = UseTime;
            item.useTime = UseTime;
            item.rare = ItemRarityID.LightPurple;
            item.width = 52;
            item.height = 18;
            item.UseSound = SoundID.Item11;
            item.damage = 0;
            item.knockBack = 2.5f;
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.ranged = true;
            item.shoot = ProjectileID.PurificationPowder;

            // Here I use that same gem ammo type again.
            item.useAmmo = ItemID.Amber;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(SpreadAngle));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;

            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

        public override void AddRecipes()
        {
            if (!Enabled) return;

            ModRecipe recipe = new ModRecipe(mod);

            var ingredients = GetInstance<Config.CommunityPackConfig>().GemDunData.GemGunRecipe;
            foreach (var kv in ingredients)
            {
                recipe.AddIngredient(kv.Key.Type, kv.Value);
            }

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
