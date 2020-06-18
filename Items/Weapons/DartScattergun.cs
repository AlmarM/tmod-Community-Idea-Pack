using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CommunityPack.Items.Weapons
{
    public class DartScattergun : ModItem
    {
        public static int Damage;
        public static float KnockBack;
        public static int MinProjectileCount;
        public static int MaxProjectileCount;
        public static float ProjectileSpread;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault( "Allows the collection of seeds for ammo\n" + 
                                "\"Is that duct tape?\"");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(gold: 8);
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 45;
            item.useTime = 45;
            item.rare = ItemRarityID.LightPurple;
            item.width = 64;
            item.height = 20;
            item.UseSound = SoundID.Item99;
            item.damage = Damage;
            item.knockBack = KnockBack;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 13f;
            item.noMelee = true;
            item.ranged = true;
            item.useAmmo = AmmoID.Dart;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float projectileCount = MinProjectileCount + Main.rand.Next(MaxProjectileCount - MinProjectileCount);
            Vector2 speed = new Vector2(speedX, speedY);
            Utils.ProjectilesSpawnRandomSpread(projectileCount, ProjectileSpread, position, speed, type, damage, knockBack, player.whoAmI);

            return false;
        }
    }
}
