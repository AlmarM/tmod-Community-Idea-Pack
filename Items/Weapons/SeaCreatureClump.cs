using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Net;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Events;

namespace CommunityPack.Items.Weapons
{
    public class SeaCreatureClump : ModItem
    {
        public static bool Enabled;
        public static int Damage;
        public static int ManaUsed;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons a pile of sea creatures that launches\n"
                            + "a variety of aquatic plants & animals at enemies");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(gold: 1);
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = false;
            item.useAnimation = 30;
            item.useTime = 30;
            item.rare = ItemRarityID.Blue;
            item.width = 25;
            item.height = 32;
            item.UseSound = SoundID.Item44;
            item.damage = Damage;
            item.knockBack = 2f;
            item.mana = ManaUsed;
            item.shoot = ProjectileType<Projectiles.SeaCreatureClumpProjectile>();
            item.noMelee = true;
            item.summon = true;
            item.sentry = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 worldStartPosition = new Vector2()
            {
                X = Main.mouseX + Main.screenPosition.X,
                Y = Main.mouseY + Main.screenPosition.Y
            };

            int yOffset = 1;

            Vector2 spawnPosition = Utils.FindSentrySpawnSpot(player, worldStartPosition, yOffset);

            Projectile.NewProjectile(spawnPosition.X, spawnPosition.Y, 0f, 0f, item.shoot, damage, knockBack, player.whoAmI);
            
            player.UpdateMaxTurrets();

            return false;
        }
    }
}
