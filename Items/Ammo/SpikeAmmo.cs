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

namespace CommunityPack.Items.Ammo
{
    public class SpikeAmmo : GlobalItem
    {
        public static int SpikeDamage;
        public static int WoodenSpikeDamage;

        public override void SetDefaults(Item item)
        {
            switch (item.type)
            {
                case ItemID.Spike:
                case ItemID.WoodenSpike:
                    item.ammo = ItemID.Spike;
                    item.notAmmo = true;
                    break;
            }
        }

        public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            switch (ammo.type)
            {
                case ItemID.Spike:
                    damage += SpikeDamage;
                    type = ProjectileType<Projectiles.Spike>();
                    break;

                case ItemID.WoodenSpike:
                    damage += WoodenSpikeDamage;
                    type = ProjectileType<Projectiles.WoodenSpike>();
                    break;
            }
        }
    }
}
