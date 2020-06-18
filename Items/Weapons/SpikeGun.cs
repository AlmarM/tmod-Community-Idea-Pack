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
    public class SpikeGun : ModItem
    {
        public static bool Enabled;
        public static int Damage;
        public static float KnockBack;
        public static int UseTime;
        public static float SpreadAngle;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'This is an even better idea!'");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(silver: 20);
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.autoReuse = true;
            item.useAnimation = UseTime;
            item.useTime = UseTime;
            item.rare = ItemRarityID.Green;
            item.width = 44;
            item.height = 24;
            item.UseSound = SoundID.Item11;
            item.damage = Damage;
            item.knockBack = KnockBack;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.ranged = true;
            item.crit = 6;
            item.useAmmo = ItemID.Spike;
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

            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, -1);
        }

        public override void AddRecipes()
        {
            if (!Enabled) return;

            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Sandgun);
            recipe.AddRecipeGroup("CommunityPack:DungeonBrick", 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
