using CommunityPack.Items.Weapons;
using CommunityPack.Items.Accessories;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CommunityPack.Buffs
{
    public class Seedless : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Seedless");
            Description.SetDefault("The Seedy Necklace effect can't occur now.");

            Main.debuff[Type] = true;
            canBeCleared = false;
        }
    }
}