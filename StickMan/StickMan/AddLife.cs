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
    class AddLife
    {
        public Texture2D texture;
        public Rectangle dstRect;
        public Random r = new Random();


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, dstRect, Color.White);
        }
    }

    class Heart : AddLife
    {
        public Heart(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            dstRect = newRectangle;
        }

        public void Update(Game1 game)
        {
            dstRect.X -= game.speed;   //Background image moving speed
        }

    }

    class Pica : AddLife
    {
        public Pica(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            dstRect = newRectangle;
        }

        public void Update(Game1 game)
        {
            dstRect.X -= game.speed;   //Background image moving speed
        }
    }
}
