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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    ///         
    enum Screen
        {
            StartScreen,
            GamePlayScreen,
            GameOverScreen
        }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        StartScreen startScreen;
        GamePlayScreen gamePlayScreen;
        GameOverScreen gameOverScreen;
        Screen currentScreen;
        public int score = 0;
        public int speed = 3; // from 1-6
        public int highScore = 0; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1024;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            
            base.Initialize();
        }



        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startScreen = new StartScreen(this);
            currentScreen = Screen.StartScreen;

            base.LoadContent();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            switch (currentScreen)
            {
                case Screen.StartScreen:
                    if (startScreen != null)
                        startScreen.Update();
                    break;
                case Screen.GamePlayScreen:
                    if (gamePlayScreen != null)
                        gamePlayScreen.Update(gameTime);
                    break;
                case Screen.GameOverScreen:
                    if (gameOverScreen != null)
                        gameOverScreen.Update();
                    break;
            }

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            switch (currentScreen)
            {
                case Screen.StartScreen:
                    if (startScreen != null)
                        startScreen.Draw(spriteBatch);
                    break;
                case Screen.GamePlayScreen:
                    if (gamePlayScreen != null)
                    {
                        gamePlayScreen.Update(gameTime);
                        gamePlayScreen.Draw(spriteBatch);
                    }
                    break;
                case Screen.GameOverScreen:
                    if (gameOverScreen != null)
                    {
                        //gameOverScreen.Update(gameTime);
                        gameOverScreen.Draw(spriteBatch);
                    }
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void StartGame()
        {
            gamePlayScreen = new GamePlayScreen(this);
            currentScreen = Screen.GamePlayScreen;
            startScreen = null;
            gameOverScreen = null;
        }

        public void EndGame()
        {
            gameOverScreen = new GameOverScreen(this);
            currentScreen = Screen.GameOverScreen;
            //gamePlayScreen = null;
            MediaPlayer.Stop();
        }

    }
}
