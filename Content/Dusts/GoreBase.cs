using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Terrexpansion.Content.Dusts
{
    public class GoreBase : ModDust
    {
        public virtual float Speed => 0.5f;

        public virtual float ScaleDownSpeed => 0.005f;

        public virtual int FadeInSpeed => 1;

        public virtual int StartingAlpha => 0;

        public virtual bool Sticky => true;

        private int _fadeInTicks = 0;

        public override void OnSpawn(Dust dust)
        {
            dust.alpha = StartingAlpha;
            dust.active = true;
            dust.noGravity = false;
            dust.noLight = true;
            dust.noLightEmittence = true;

            while (Collision.SolidCollision(dust.position, 2, 2))
            {
                dust.position.Y += 1f;
            }
        }

        public override bool Update(Dust dust)
        {
            if (++_fadeInTicks > 1)
            {
                dust.alpha += FadeInSpeed;
                _fadeInTicks = 0;
            }

            if (dust.alpha >= 255)
            {
                dust.active = false;
            }

            if (Collision.SolidCollision(dust.position, 2, 2))
            {
                dust.velocity = Sticky ? Vector2.Zero : new Vector2(dust.velocity.X, 0);
                dust.scale -= ScaleDownSpeed;

                if (dust.scale <= 0)
                {
                    dust.active = false;
                }
            }
            else
            {
                dust.velocity.Y = dust.velocity.Y < 16f ? dust.velocity.Y += Speed : 16f;
                dust.rotation += Math.Max(dust.velocity.X, dust.velocity.Y);
                dust.position += dust.velocity;
            }

            return false;
        }
    }
}