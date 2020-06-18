using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CommunityPack.Dusts
{
    public class GemDust : ModDust
    {
        class GemDustData
        {
            public int frame;
            public int frameCounter;
            public int fameTime;
        }

        public override void OnSpawn(Dust dust)
        {
            dust.scale = 1f;

            int randomFirstFrame = Main.rand.Next(3);
            dust.customData = new GemDustData()
            {
                frame = randomFirstFrame,
                frameCounter = 0,
                fameTime = Main.rand.Next(15, 60)
            };

            dust.frame = new Rectangle(0, randomFirstFrame * 10, 10, 10);
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;

            GemDustData dustData = (GemDustData)dust.customData;

            dustData.frameCounter++;
            if (dustData.frameCounter == dustData.fameTime)
            {
                dustData.frameCounter = 0;

                dustData.frame++;
                if (dustData.frame == 3)
                {
                    dustData.frame = 0;
                }

                dust.frame = new Rectangle(0, dustData.frame * 10, 10, 10);
            }

            dust.alpha += 3;
            if (dust.alpha >= 255)
            {
                dust.active = false;
            }

            if (Main.rand.NextBool(10))
            {
                dust.position += Utils.RandomInsideUnitCircle();
            }

            return false;
        }
    }
}