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
    public class GamePlayScreen
    {
        private Game1 game;
        Texture2D life;
        Rectangle sourceRect;
        Rectangle srcKoffingRec;
        Rectangle srcGengarRec;
        float elapsed;
        float delay = 80f;    //Speed of running animation
        int frames = 0;
        KeyboardState ks;
        KeyboardState prevks;
        Koffing newKoffing;
        Gengar newGengar;
        Heart newHeart;
        Pica newPica;
        Scrolling scrolling1;
        Scrolling scrolling2;
        Scrolling scrolling3;
        Scrolling scrolling4;
        RightRun rightRun;
        bool upKey;
        bool prevUpKey;
        bool trigger = false;
        bool heartIsNotHit = true;
        bool picaIsNotHit = true;
        static Random r = new Random();
        int jumpElapsed = 0;
        int lives = 3;
        Score gameScore;

        public GamePlayScreen(Game1 game)
        {
            this.game = game;
            scrolling1 = new Scrolling(game.Content.Load<Texture2D>("Backgrounds/BackGround_1"), new Rectangle(0, 0,
                                game.Window.ClientBounds.Width, game.Window.ClientBounds.Height));
            scrolling2 = new Scrolling(game.Content.Load<Texture2D>("Backgrounds/BackGround_2"), new Rectangle(0, 0,
                                            game.Window.ClientBounds.Width, game.Window.ClientBounds.Height));
            scrolling3 = new Scrolling(game.Content.Load<Texture2D>("Backgrounds/BackGround_3"), new Rectangle(0, 0,
                    game.Window.ClientBounds.Width, game.Window.ClientBounds.Height));
            scrolling4 = new Scrolling(game.Content.Load<Texture2D>("Backgrounds/BackGround_4"), new Rectangle(0, 0,
                                            game.Window.ClientBounds.Width, game.Window.ClientBounds.Height));
            //newMine = new Mine(game.Content.Load<Texture2D>("koffing2"), new Rectangle(new Random().Next(300, 800), 300, 80, 74), new Rectangle(framesMine, 0, 80, 74));
            NewKoffing(new Random().Next(800, 1000));
            NewGengar(new Random().Next(1100, 1300));
            NewHeart(new Random().Next(5800, 8000));
            NewPica(new Random().Next(800, 2000));
            //Song song = game.Content.Load<Song>("action1");
            Song song = game.Content.Load<Song>("Pokemon Medley");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            life = game.Content.Load<Texture2D>("Animate/3heart_small");
            gameScore = new Score();
            gameScore.Font = game.Content.Load<SpriteFont>("Arial");      
        }

        public void Update(GameTime gameTime)
        {
            
            this.game.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 35.0f);
            this.game.IsFixedTimeStep = false;
            ks = Keyboard.GetState();

            //playing running image and scrolling background
            upKey = ks.IsKeyDown(Keys.Up);
            prevUpKey = prevks.IsKeyDown(Keys.Up);
            rightRun = new RightRun(game.Content.Load<Texture2D>("Animate/run"), new Rectangle(200, 355, 90,90), sourceRect);
            newKoffing.srcRect = newKoffing.Animate(gameTime);
            newGengar.srcRect = newGengar.Animate(gameTime);

            //UP is just pressed
            if (upKey && !prevUpKey)
            {
                trigger = true;
            }
            else if (trigger)
            {
                rightRun = new RightRun(game.Content.Load<Texture2D>("Animate/run"), new Rectangle(200, 260, 90, 90), new Rectangle(200, 0, 100, 100));
                jumpElapsed += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (jumpElapsed > 1600/game.speed)
                {
                    trigger = false;
                    jumpElapsed = 0;
                }
            }

            Run(gameTime);               //Play running animation
            
            AnimateBackGround(gameTime);        //Let background move to left
            newKoffing.Update(game);
            newGengar.Update(game);
            newHeart.Update(game);
            newPica.Update(game);
            prevks = ks; 
        }

        //Scrolling Background
        private void AnimateBackGround(GameTime gameTime)
        {
            SoundEffect punch;
            punch = game.Content.Load<SoundEffect>("SoundEffect/punch");
            SoundEffectInstance punchInstance = punch.CreateInstance();
            SoundEffect pikachu;
            pikachu = game.Content.Load<SoundEffect>("SoundEffect/pikachu2");
            SoundEffectInstance pikachuInstance = pikachu.CreateInstance();
            SoundEffect chaching;
            chaching = game.Content.Load<SoundEffect>("SoundEffect/chaching");
            SoundEffectInstance chachingInstance = chaching.CreateInstance();
            scrolling1.Update(game);
            scrolling2.Update(game);
            scrolling3.Update(game);
            scrolling4.Update(game);

            if (newKoffing.dstRect.X < 0)
            {
                    NewKoffing(new Random().Next(1800, 2000));
            }

            if (newGengar.dstRect.X < 0)
            {
                NewGengar(new Random().Next(newKoffing.dstRect.X+1200, newKoffing.dstRect.X+1400));
            }

            if (newHeart.dstRect.X < 0)
            {
                NewHeart(new Random().Next(5800, 8000));
                heartIsNotHit = true;
            }
            if (newPica.dstRect.X < 0)
            {
                NewPica(new Random().Next(2800, 4000));
                picaIsNotHit = true;
            }

            if (scrolling1.rectangle.X + scrolling1.rectangle.Width <= 1024)
            {
                scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.rectangle.Width;
            }

            if (scrolling2.rectangle.X + scrolling2.rectangle.Width <= 1024)
            {
                scrolling3.rectangle.X = scrolling2.rectangle.X + scrolling2.rectangle.Width;
            }

            if (scrolling3.rectangle.X + scrolling3.rectangle.Width <= 1024)
            {
                scrolling4.rectangle.X = scrolling3.rectangle.X + scrolling3.rectangle.Width;
            }

            if (scrolling4.rectangle.X + scrolling4.rectangle.Width <= 1024)
            {
                scrolling1.rectangle.X = scrolling4.rectangle.X + scrolling4.rectangle.Width;
            }

            if ((newKoffing.dstRect.X >= 201-game.speed) && (newKoffing.dstRect.X <= 200) && (rightRun.dstRect.Y == 355))
           // if ((Math.Abs(newKoffing.dstRect.X - 200) < 40) && (rightRun.dstRect.Y == 280))
            {
                punchInstance.Play();
                lives--;
                if (lives == 0)
                    game.EndGame();
            }

            if ((newGengar.dstRect.X >= 201 - game.speed) && (newGengar.dstRect.X <= 200) && (newGengar.dstRect.Y+60 >rightRun.dstRect.Y) && !((rightRun.dstRect.Y+70)<newGengar.dstRect.Y))
            {
                punchInstance.Play();
                lives--;
                if (lives == 0)
                    game.EndGame();
            }

            if ((newHeart.dstRect.X >= 201 - game.speed) && (newHeart.dstRect.X <= 200) && trigger)
            {
                if (lives < 3)
                {
                    chachingInstance.Play();
                    lives++;
                    heartIsNotHit = false;
                }
            }

            if ((newPica.dstRect.X >= 201 - game.speed) && (newPica.dstRect.X <= 200) && trigger)
            {
                    pikachuInstance.Play();
                    gameScore.gScore++;
                    if (gameScore.gScore > game.highScore)
                        game.highScore = gameScore.gScore;
                    game.score = gameScore.gScore;
                    picaIsNotHit = false;

            } 
        }

        private void Run(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames >= 5)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }
            sourceRect = new Rectangle(100 * frames, 0, 100, 100);
        }



        private void NewKoffing(int random)
        {
            newKoffing = new Koffing(game.Content.Load<Texture2D>("Animate/koffing2"), new Rectangle(random, 365, 80, 74), srcKoffingRec);
        }

        private void NewGengar(int random)
        {
            newGengar = new Gengar(game.Content.Load<Texture2D>("Animate/gengar2"), new Rectangle(random, 365, 90, 74), srcGengarRec);
        }

        private void NewHeart(int random)
        {
            newHeart = new Heart(game.Content.Load<Texture2D>("Animate/heart"), new Rectangle(random, 280, 30, 27));
        }

        private void NewPica(int random)
        {
            newPica = new Pica(game.Content.Load<Texture2D>("Animate/Pikachu"), new Rectangle(random, 240, 80, 82));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            scrolling3.Draw(spriteBatch);
            scrolling4.Draw(spriteBatch);
            newKoffing.Draw(spriteBatch);
            newGengar.Draw(spriteBatch);
            if (heartIsNotHit)
            {
                newHeart.Draw(spriteBatch);
            }
            if (picaIsNotHit)
            {
                newPica.Draw(spriteBatch);
            }
            rightRun.Draw(spriteBatch);
            spriteBatch.Draw(life, new Rectangle(30, 10, 50 * lives, 44), new Rectangle(0, 0, 50 * lives, 44), Color.White);
            gameScore.Draw(spriteBatch);
        }

    }
}
