using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CommunityPack.Items.Weapons
{
    public class SporeTome : ModItem
    {
        public static int UseTime;
        public static int ManaCost;
        public static int MinProjectileCount;
        public static int MaxProjectileCount;
        public static int SporeDamage;
        public static float SporeKnockBack;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons a cloud of jungle spores at the point of your cursor");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(gold: 8);
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.autoReuse = true;
            item.useAnimation = UseTime;
            item.useTime = UseTime;
            item.rare = ItemRarityID.LightPurple;
            item.width = 28;
            item.height = 30;
            item.UseSound = SoundID.Item20;
            item.shoot = ProjectileID.SporeTrap; // To make sure the weapon calls the Shoot().
            item.noMelee = true;
            item.magic = true;
            item.mana = ManaCost;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // Spawn dust for the caster
            SpawnRandomDust(player);

            int sporeCount = MinProjectileCount + Main.rand.Next(MaxProjectileCount - MinProjectileCount);
            for (int i = 0; i < sporeCount; i++)
            {
                Vector2 offset = Utils.RandomInsideUnitCircle() * Main.rand.Next(32);
                Projectile.NewProjectile(Main.MouseWorld + offset, Vector2.Zero, ProjectileID.SporeTrap + Main.rand.Next(2), SporeDamage, SporeKnockBack, player.whoAmI);
            }

            return false;
        }

        public override bool UseItem(Player player)
        {
            // Spawn dust for everyone else
            SpawnRandomDust(player);

            return true;
        }

        void SpawnRandomDust(Player player)
        {
            int dustCount = 2 + Main.rand.Next(3);
            for (int j = 0; j < dustCount; j++)
            {
                Dust.NewDust(player.Center, 3, 3, 44);
            }
        }
    }
}
