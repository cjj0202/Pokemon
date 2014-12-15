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
    public class GameOverScreen
    {
        private Texture2D texture;
        private Game1 game;
        private KeyboardState lastState;
        FinalScore finalScore;

        public GameOverScreen(Game1 game)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>("Game Over");
            lastState = Keyboard.GetState();
            finalScore = new FinalScore();
            finalScore.Font = game.Content.Load<SpriteFont>("GameOver");
            finalScore.Score = game.score;        //final score
            game.score = 0;
            Console.WriteLine("High:"+game.highScore);
        }

        public void Update()  //Read input key to exit or restart
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
            {
                game.StartGame();
            }
            else if (keyboardState.IsKeyDown(Keys.Escape) && lastState.IsKeyUp(Keys.Escape))
            {
                game.Exit();
            }

            lastState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, new Vector2(0f, 0f), Color.White);
            finalScore.Draw(spriteBatch);
        }
    }
}
