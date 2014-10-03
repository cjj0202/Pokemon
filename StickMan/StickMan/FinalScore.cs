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
    public class FinalScore
    {
        private Vector2 scorePos = new Vector2(300, 100);

        public SpriteFont Font { get; set; }

        public int Score { get; set; }

        public FinalScore()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                Font,                          // SpriteFont
                "Final Score: " + Score.ToString(),  // Text
                scorePos,                      // Position
                Color.Black);                  // Tint
        }
    }
}
