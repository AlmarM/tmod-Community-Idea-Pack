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

namespace CommunityPack.Items.Accessories
{
    public class SeedyNecklace : ModItem
    {
        public static int ActivationCooldown = 3;
        public static int SeedSpawnCount = 5;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault( "Causes several popping seeds to fall after taking damage\n"
                            +   "Can only occur once every 20 seconds");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(gold: 8);
            item.rare = ItemRarityID.LightPurple;
            item.width = 21;
            item.height = 31;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CommunityPlayer>().seedyNecklace = true;
        }

        public static void CreatePoppingSeeds(Player player)
        {
            for (int i = 0; i < SeedSpawnCount; i++)
            {
                Vector2 direction = (-Vector2.UnitY).RotatedByRandom(MathHelper.ToRadians(100f));
                Vector2 speed;
                speed.X = direction.X * Main.rand.NextFloat(4f, 7f);
                speed.Y = direction.Y * Main.rand.NextFloat(4f, 9f);

                int seedType = Main.rand.Next(Enum.GetNames(typeof(Projectiles.PoppingSeed.SeedType)).Length);
                Projectile.NewProjectile(player.Center, speed, ProjectileType<Projectiles.PoppingSeed>(), 0, 0, player.whoAmI, seedType, Main.rand.NextFloat());
            }
        }
    }
}
