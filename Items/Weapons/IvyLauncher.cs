using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CommunityPack.Items.Weapons
{
    public class IvyLauncher : ModItem
    {
        public static int Damage;
        public static float KnockBack;
        public static int IvyCrystalTriggerCount;
        public static int IvyCrystalSpawnCount;
        public static float IvyCrystalSpawnAngle;

        int shotCount;

        public override void SetStaticDefaults()
        {
            string tooltip = string.Format("Fires a volley of Ivy Crystals every {0} shots", IvyCrystalTriggerCount);
            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(gold: 8);
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.autoReuse = true;
            item.useAnimation = 40;
            item.useTime = 40;
            item.rare = ItemRarityID.LightPurple;
            item.width = 52;
            item.height = 23;
            item.UseSound = SoundID.Item11;
            item.damage = Damage;
            item.knockBack = KnockBack;
            item.shoot = ProjectileType<Projectiles.VineBallProjectile>();
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.ranged = true;
            item.useAmmo = ItemType<VineBallAmmo>();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            shotCount++;
            if (shotCount == IvyCrystalTriggerCount)
            {
                Vector2 speed = new Vector2(speedX, speedY);
                Utils.ProjectilesSpawnEvenSpread(IvyCrystalSpawnCount, IvyCrystalSpawnAngle, position, speed, ProjectileType<Projectiles.IvyCrystal>(), damage, knockBack, player.whoAmI);

                shotCount = 0;
            }

            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-11, 0);
        }
    }
}
