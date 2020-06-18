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

namespace CommunityPack.Projectiles
{
    public class Amber : GemProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Amber Projectile");
        }

        protected override Color GetGemColor()
        {
            return new Color(252, 193, 45);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.BetsysCurse, TimerUtils.Seconds(Items.Ammo.GemAmmo.DebuffDuration));
        }
    }
}