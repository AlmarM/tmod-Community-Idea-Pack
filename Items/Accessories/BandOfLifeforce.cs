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
    //[AutoloadEquip(EquipType.HandsOff)]
    public class BandOfLifeforce : ModItem
    {
        public static bool Enabled;
        public static int ExtraHealth;
        public static float PotionCooldownReduction;
        public static int LifeRegen;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Band of Lifeforce");
            Tooltip.SetDefault( "Provides life regeneration\n"
                            +   $"Reduces the cooldown of healing potions by {PotionCooldownReduction}%\n"
                            +   $"Increases max health by {ExtraHealth}");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(gold: 4);
            item.rare = ItemRarityID.LightPurple;
            item.width = 30;
            item.height = 20;
            item.accessory = true;
            item.lifeRegen = LifeRegen;
        }

        public override void AddRecipes()
        {
            if (!Enabled) return;

            ModRecipe recipe = new ModRecipe(mod);

            var ingredients = GetInstance<Config.CommunityPackConfig>().BandOfLifeforceData.BandOfLifeforceRecipe;
            foreach (var kv in ingredients)
            {
                recipe.AddIngredient(kv.Key.Type, kv.Value);
            }

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CommunityPlayer>().bandOfLifeforce = true;
            player.statLifeMax2 += ExtraHealth;
        }
    }
}
