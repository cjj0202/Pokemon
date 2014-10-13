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

    class Heart : Obstacle
    {
        public Heart(Texture2D newTexture, Rectangle dstRectangle, Rectangle srcRectangle)
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
            return srcRect = new Rectangle(0, 0, 30, 30);
        }
    }

    class Pica : Obstacle
    {
        public Pica(Texture2D newTexture, Rectangle dstRectangle, Rectangle srcRectangle)
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
            return srcRect = new Rectangle(0, 0, 80, 82);
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

            if (dstRect.Y < 435)
            {
                dstRect.Y++;
                dstRect.X -= 1;
            }
            else
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

    class Balloon : Obstacle
    {
        public Balloon(Texture2D newTexture, Rectangle dstRectangle, Rectangle srcRectangle)
        {
            texture = newTexture;
            srcRect = srcRectangle;
            dstRect = dstRectangle;
        }

        public Rectangle Animate(GameTime gameTime)
        {
            return srcRect = new Rectangle(0, 0, 100, 100);
        }

        public void Update(Game1 game, Pica picachu)
        {
            dstRect.X += 1;
            if ((dstRect.Y < picachu.dstRect.Y) && (picachu.dstRect.X < 1024)) //if picachu appears and balloon is higher than picachu
            {
                dstRect.Y += 1;
            }
        }
    }


}
