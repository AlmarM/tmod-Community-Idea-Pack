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
using Terraria.Audio;

namespace CommunityPack.Dusts
{
    public class PoppingSeedDust : ModDust
    {
        public class BehaviourInfo
        {
            public int TargetPlayerId;
            public Vector2 SpawnPosition;
            public uint SpawnTime;
            public int LerpTime;
            public uint AnimationDelay;
            public float MaxScale;
            public Vector2 TargetOffset;
        }

        public override void OnSpawn(Dust dust)
        {
            dust.frame = new Rectangle(0, 0, 10, 10);
            dust.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
        }

        public override bool Update(Dust dust)
        {
            BehaviourInfo behaviourInfo = (BehaviourInfo)dust.customData;

            if (Main.GameUpdateCount - behaviourInfo.SpawnTime > behaviourInfo.AnimationDelay)
            {
                float amount = (Main.GameUpdateCount - (behaviourInfo.SpawnTime + behaviourInfo.AnimationDelay)) / (float)behaviourInfo.LerpTime;
                amount = Easing.Quartic.Out(amount);

                Vector2 targetPosition = Main.player[behaviourInfo.TargetPlayerId].Center;
                targetPosition.X += behaviourInfo.TargetOffset.X;
                targetPosition.Y += behaviourInfo.TargetOffset.Y;

                dust.position = Vector2.Lerp(behaviourInfo.SpawnPosition, targetPosition, amount);
                dust.alpha = (int)(Easing.Cubic.In(amount) * 255);

                if (amount >= 1f)
                {
                    dust.active = false;
                }
            }
            else
            {
                float amount = (Main.GameUpdateCount - behaviourInfo.SpawnTime) / (float)behaviourInfo.AnimationDelay;
                dust.scale = amount * behaviourInfo.MaxScale;
                dust.alpha = (int)(255 - (255 * amount));
            }

            return false;
        }
    }
}
