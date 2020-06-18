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

namespace CommunityPack.Tiles
{
    public class JungleSpores : GlobalTile
    {
        public override bool Drop(int i, int j, int type)
        {
            if (!Items.Accessories.OvergrownSpore.Enabled) return true;

            if (type == TileID.JunglePlants && Main.rand.NextBool(25))
            {
                Item.NewItem(new Vector2(i * 16, j * 16), ItemType<Items.Accessories.OvergrownSpore>());
            }

            return true;
        }
    }
}
