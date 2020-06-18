using Terraria.ID;
using Terraria.ModLoader;

namespace CommunityPack.Items
{
    public class KeyOfFright : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Charged with the essence of the jungle'");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.NightKey);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TempleKey);
            recipe.AddIngredient(ItemID.JungleSpores, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
