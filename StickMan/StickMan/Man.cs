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
    class Man
    {
        public Texture2D texture;
        public Rectangle dstRect;
        public Rectangle srcRect;
        int framesRun = 0;
        float elapsedRun;
        float delayRun = 80f;  //interval between two motions

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, dstRect, srcRect, Color.White);
        }

        public Man(Texture2D newTexture, Rectangle dstRectangle, Rectangle srcRectangle)
        {
            texture = newTexture;
            srcRect = srcRectangle;
            dstRect = dstRectangle;
        }

        public void Update(Game1 game)
        {
        }

        //Play 6 motions in a loop
        public Rectangle Run(GameTime gameTime)
        {
            elapsedRun += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedRun >= delayRun)
            {
                if (framesRun >= 5)
                {
                    framesRun = 0;
                }
                else
                {
                    framesRun++;
                }
                elapsedRun = 0;
            }
            return srcRect = new Rectangle(100 * framesRun, 0, 100, 100);
        }
    }
}
