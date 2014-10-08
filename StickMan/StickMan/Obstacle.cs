using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace StickMan
{
    class Obstacle
    {
        public Texture2D texture;
        public Rectangle dstRect;
        public Rectangle srcRect;
        public Random r = new Random();
        public float opacity = 1f;
        public bool tg_pos = true;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, dstRect, srcRect, Color.White * opacity);
        }
    }

    class Koffing : Obstacle
    {
        int framesKoffing = 0;
        float elapsedKoffing;
        float delayKoffing = 350f;

        public Koffing(Texture2D newTexture, Rectangle dstRectangle, Rectangle srcRectangle)
        {
            texture = newTexture;
            srcRect = srcRectangle;
            dstRect = dstRectangle;
        }

        public void Update(Game1 game)
        {
            dstRect.X -= game.speed;   

        }

        public Rectangle Animate(GameTime gameTime)
        {
            elapsedKoffing += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedKoffing >= delayKoffing)
            {
                if (framesKoffing == 1)
                {
                    framesKoffing = 0;
                }
                else
                {
                    framesKoffing = 1;
                }
                elapsedKoffing = 0;
            }
            return srcRect = new Rectangle(framesKoffing * 80, 0, 80, 74);
        }
    }

    class Gengar : Obstacle
    {
        int framesGengar = 0;
        float elapsedGengar;
        float delayGengar = 350f;

        public Gengar(Texture2D newTexture, Rectangle dstRectangle, Rectangle srcRectangle)
        {
            texture = newTexture;
            srcRect = srcRectangle;
            dstRect = dstRectangle;
        }

        public void Update(Game1 game)
        {
            dstRect.X -= game.speed;
            if (tg_pos)
            {
                if (dstRect.Y > 300)
                    dstRect.Y--;
                else
                    tg_pos = false;
            }
            if (!tg_pos)
            {
                if (dstRect.Y < 435)
                    dstRect.Y++;
                else
                    tg_pos = true;
            }
        }

        public Rectangle Animate(GameTime gameTime)
        {
            elapsedGengar += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedGengar >= delayGengar)
            {
                if (framesGengar == 1)
                {
                    framesGengar = 0;
                    if(opacity > 0.2)
                        opacity -= 0.1f;
                        //Console.WriteLine(opacity);
                }
                else
                {
                    framesGengar = 1;
                }
                elapsedGengar = 0;
            }
            return srcRect = new Rectangle(framesGengar * 80, 0, 80, 74);
        }
    }

}
