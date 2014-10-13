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
        Rectangle srcBalloonRec;
        Rectangle srcHeartRec;
        Rectangle srcPicaRec;
        Rectangle srcManRec;
        KeyboardState ks;
        KeyboardState prevks;
        Koffing newKoffing;
        Gengar newGengar;
        Heart newHeart;
        Pica newPica;
        Balloon newBalloon;
        Scrolling scrolling1;
        Scrolling scrolling2;
        Man stickMan;
        bool upKey;
        bool prevUpKey;
        bool downKey;
        bool prevDownKey;
        bool triggerUp = false;
        bool triggerDown = false;
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
            stickMan = new Man(game.Content.Load<Texture2D>("Animate/run"), new Rectangle(200, 430, 90, 90), srcManRec);
            //NewKoffing(new Random().Next(800, 1000));
            NewGengar(new Random().Next(1100, 1300));
            NewHeart(new Random().Next(5800, 8000));
            NewPica(new Random().Next(800, 2000));
            NewBalloon(0);
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
            if(this.game.score <5)
            {
                this.game.speed = 3;
            }
            else if (this.game.score < 10)
            {
                this.game.speed = 4;
            }
            else if (this.game.score < 15)
            {
                this.game.speed = 5;
            }
            else
                this.game.speed = 6;

            ks = Keyboard.GetState();
            SoundEffect punch;
            punch = game.Content.Load<SoundEffect>("SoundEffect/punch");
            SoundEffectInstance punchInstance = punch.CreateInstance();
            SoundEffect pikachu;
            pikachu = game.Content.Load<SoundEffect>("SoundEffect/pikachu2");
            SoundEffectInstance pikachuInstance = pikachu.CreateInstance();
            SoundEffect pikasad;
            pikasad = game.Content.Load<SoundEffect>("SoundEffect/pikasad");
            SoundEffectInstance pikasadInstance = pikasad.CreateInstance();
            SoundEffect chaching;
            chaching = game.Content.Load<SoundEffect>("SoundEffect/chaching");
            SoundEffectInstance chachingInstance = chaching.CreateInstance();

            //Get key status
            upKey = ks.IsKeyDown(Keys.Up);
            prevUpKey = prevks.IsKeyDown(Keys.Up);
            downKey = ks.IsKeyDown(Keys.Down);
            prevDownKey = prevks.IsKeyDown(Keys.Down);

            //playing running image and other animation
            newGengar.srcRect = newGengar.Animate(gameTime);
            newHeart.srcRect = newHeart.Animate(gameTime);
            newPica.srcRect = newPica.Animate(gameTime);
            stickMan.srcRect = stickMan.Run(gameTime);

            //UP is just pressed
            if (upKey && !prevUpKey)
            {
                triggerUp = true;
            }
            else if (triggerUp)
            {
                stickMan.dstRect.Y = 330;
                stickMan.srcRect = new Rectangle(200, 0, 100, 100);
                jumpElapsed += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (jumpElapsed > 1600/game.speed)
                {
                    triggerUp = false;
                    stickMan.dstRect.Y = 430;
                    jumpElapsed = 0;
                }
            }
            //DOWN is just pressed
            if (downKey && !prevDownKey)
            {
                triggerDown = true;
            }
            else if (triggerDown)
            {
                //stickMan.dstRect.Y = 330;
                stickMan.srcRect = new Rectangle(0, 0, 100, 100);
                stickMan.dstRect = new Rectangle(200, 450, 80, 80);
                jumpElapsed += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (jumpElapsed > 1600 / game.speed)
                {
                    triggerDown = false;
                    stickMan.dstRect = new Rectangle(200, 430, 90, 90);
                    //stickMan.dstRect.Y = 430;
                    jumpElapsed = 0;
                }
            }

            prevks = ks;


            if (newGengar.dstRect.X < 0)
            {
                NewGengar(new Random().Next(1200, 1400));               
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

            if (newBalloon.dstRect.X > 1024)
            {
                NewBalloon(0);
            }

            if (scrolling1.rectangle.X + scrolling1.rectangle.Width <= 1024)
            {
                scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.rectangle.Width;
            }

            if (scrolling2.rectangle.X + scrolling2.rectangle.Width <= 1024)
            {
                scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.rectangle.Width;
            }

            if ((newGengar.dstRect.X >= 201 - game.speed) && (newGengar.dstRect.X <= 200) &&
                (newGengar.dstRect.Y + 60 > stickMan.dstRect.Y) && !((stickMan.dstRect.Y + 70) < newGengar.dstRect.Y))
            {
                punchInstance.Play();
                lives--;
                if (lives == 0)
                    game.EndGame();
            }

            if ((newHeart.dstRect.X >= 201 - game.speed) && (newHeart.dstRect.X <= 200) && triggerUp)
            {
                if (lives < 3)
                {
                    chachingInstance.Play();
                    lives++;
                    heartIsNotHit = false;
                }
            }

            if ((newPica.dstRect.X >= 201 - game.speed) && (newPica.dstRect.X <= 200) && triggerUp && picaIsNotHit)
            {
                pikachuInstance.Play();
                gameScore.gScore++;
                if (gameScore.gScore > game.highScore)
                    game.highScore = gameScore.gScore;
                game.score = gameScore.gScore;
                picaIsNotHit = false;
            }
            if (newBalloon != null)
            {
                if (((newPica.dstRect.X <1024) && (newPica.dstRect.X > 0)) && (newBalloon.dstRect.X - 3 <= newPica.dstRect.X) 
                    && (newBalloon.dstRect.X >= newPica.dstRect.X) && newBalloon.dstRect.Y >= newPica.dstRect.Y)
                {
                    pikasadInstance.Play();
                    picaIsNotHit = false;
                }
                else if ((newBalloon.dstRect.X >= newPica.dstRect.X) && (newBalloon.dstRect.Y < newPica.dstRect.Y) &&
                    (newKoffing == null))
                {
                    NewKoffing(newBalloon.dstRect.X, newBalloon.dstRect.Y);
                } 
                else if ((newBalloon.dstRect.X >= newPica.dstRect.X) && newKoffing != null &&
                    (newBalloon.dstRect.Y < newPica.dstRect.Y) && (newKoffing.dstRect.X > 1024 || newKoffing.dstRect.X < 0))
                {
                    NewKoffing(newBalloon.dstRect.X, newBalloon.dstRect.Y);
                }
               
            }

            if (newKoffing != null)
            {
                if ((newKoffing.dstRect.X >= 201 - game.speed) && (newKoffing.dstRect.X <= 200) && (stickMan.dstRect.Y == 430) && newKoffing.dstRect.Y == 435)
                {
                    punchInstance.Play();
                    lives--;
                    if (lives == 0)
                        game.EndGame();
                }
            }

            scrolling1.Update(game);
            scrolling2.Update(game);
            newGengar.Update(game);
            newHeart.Update(game);
            newPica.Update(game);
            if (newBalloon != null)
            {
                newBalloon.srcRect = newBalloon.Animate(gameTime);
                newBalloon.Update(game, newPica);
            }
            if (newKoffing != null)
            {
            newKoffing.srcRect = newKoffing.Animate(gameTime);
            newKoffing.Update(game);
            }
        }

        private void NewKoffing(int x, int y)
        {
            newKoffing = new Koffing(game.Content.Load<Texture2D>("Animate/koffing2"), new Rectangle(x, y, 80, 74), srcKoffingRec);
        }

        private void NewGengar(int random)
        {
            newGengar = new Gengar(game.Content.Load<Texture2D>("Animate/gengar2"), new Rectangle(random, 435, 90, 74), srcGengarRec);
        }

        private void NewHeart(int random)
        {
            newHeart = new Heart(game.Content.Load<Texture2D>("Animate/heart"), new Rectangle(random, 340, 30, 27), srcHeartRec);
        }

        private void NewPica(int random)
        {
            newPica = new Pica(game.Content.Load<Texture2D>("Animate/Pikachu"), new Rectangle(random, 300, 80, 82), srcPicaRec);
        }

        private void NewBalloon(int random)
        {
            newBalloon = new Balloon(game.Content.Load<Texture2D>("Animate/balloon"), new Rectangle(random, 80, 100, 100), srcBalloonRec);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            stickMan.Draw(spriteBatch);
            if (newKoffing != null)
            {
                newKoffing.Draw(spriteBatch);
            }
            newGengar.Draw(spriteBatch);
            if (newBalloon != null)
            {
                newBalloon.Draw(spriteBatch);
            }
            if (heartIsNotHit)
            {
                newHeart.Draw(spriteBatch);
            }
            if (picaIsNotHit)
            {
                newPica.Draw(spriteBatch);
            }
            spriteBatch.Draw(life, new Rectangle(30, 10, 50 * lives, 44), new Rectangle(0, 0, 50 * lives, 44), Color.White);
            gameScore.Draw(spriteBatch);
        }

    }
}
