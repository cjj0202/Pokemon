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
    public class StartScreen
    {
        private Texture2D texture;
        private Game1 game;
        private KeyboardState lastState;

        public StartScreen(Game1 game)
        {
            this.game = game;

            //load background and play music
            texture = game.Content.Load<Texture2D>("Start_Stick");
            Song song2 = game.Content.Load<Song>("pokemon");
            MediaPlayer.Play(song2);
            MediaPlayer.IsRepeating = true;
            lastState = Keyboard.GetState();
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
            {
                game.StartGame();
            }

            lastState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, new Vector2(0f, 0f), Color.White);
        }
    }
}
