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
    class Run
    {
        public Texture2D texture;
        public Rectangle srcRect;
        public Rectangle dstRect;


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, dstRect, srcRect, Color.White);
        }
    }

     class RightRun : Run
     {
            public RightRun(Texture2D newTexture, Rectangle newDstRectangle, Rectangle newSrcRectangle)
            {
                texture = newTexture;
                dstRect = newDstRectangle;
                srcRect = newSrcRectangle;
            }

            public void Update(GameTime gameTime)
            {

            }
      }

}

