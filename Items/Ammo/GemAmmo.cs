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
    public class GemAmmo : GlobalItem
    {
        public static int AmethystDamage;
        public static int TopazDamage;
        public static int SapphireDamage;
        public static int EmeraldDamage;
        public static int RubyDamage;
        public static int DiamondDamage;
        public static int AmberDamage;
        public static int BuffDuration;
        public static int DebuffDuration;

        public override void SetDefaults(Item item)
        {
            if (!Weapons.GemGun.Enabled) return;

            // Use 1 ItemID as the main ammo source. Amber is chosen arbitrarily.
            switch (item.type)
            {
                case ItemID.Amethyst:
                case ItemID.Topaz:
                case ItemID.Sapphire:
                case ItemID.Emerald:
                case ItemID.Ruby:
                case ItemID.Diamond:
                case ItemID.Amber:
                    item.ammo = ItemID.Amber;
                    item.notAmmo = true;
                    break;
            }
        }

        public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            switch (ammo.type)
            {
                case ItemID.Amethyst:
                    damage = AmethystDamage;
                    player.AddBuff(BuffID.Heartreach, TimerUtils.Seconds(BuffDuration));
                    type = ProjectileType<Projectiles.Amethyst>();
                    break;

                case ItemID.Topaz:
                    damage = TopazDamage;
                    player.AddBuff(BuffID.Ironskin, TimerUtils.Seconds(BuffDuration));
                    type = ProjectileType<Projectiles.Topaz>();
                    break;

                case ItemID.Sapphire:
                    damage = SapphireDamage;
                    player.AddBuff(BuffID.MagicPower, TimerUtils.Seconds(BuffDuration));
                    type = ProjectileType<Projectiles.Sapphire>();
                    break;

                case ItemID.Emerald:
                    damage = EmeraldDamage;
                    player.AddBuff(BuffID.Titan, TimerUtils.Seconds(BuffDuration));
                    type = ProjectileType<Projectiles.Emerald>();
                    break;

                case ItemID.Ruby:
                    damage = RubyDamage;
                    player.AddBuff(BuffID.Rage, TimerUtils.Seconds(BuffDuration));
                    type = ProjectileType<Projectiles.Ruby>();
                    break;

                case ItemID.Diamond:
                    damage = DiamondDamage;
                    player.AddBuff(BuffID.Endurance, TimerUtils.Seconds(BuffDuration));
                    type = ProjectileType<Projectiles.Diamond>();
                    break;

                case ItemID.Amber:
                    damage = AmberDamage;
                    player.AddBuff(BuffID.AmmoReservation, TimerUtils.Seconds(BuffDuration));
                    type = ProjectileType<Projectiles.Amber>();
                    break;
            }
        }
    }
}
