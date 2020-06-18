using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CommunityPack.Projectiles
{
    public class IvyCrystal : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Leaf;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ivy Crystal");

            Main.projFrames[projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = 40;
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.timeLeft = 600;
            projectile.ranged = true;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(6))
            {
                Dust.NewDust(projectile.Center, 2, 2, DustID.GrassBlades);
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(projectile.Center, 2, 2, DustID.Grass);
            }

            Main.PlaySound(SoundID.Grass, projectile.position);
        }
    }
}