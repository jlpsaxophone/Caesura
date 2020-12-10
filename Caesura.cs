using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Caesura
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Caesura : Game
    {
        public GraphicsDeviceManager graphics;
        public Random random;
        public SpriteBatch spriteBatch;
        public Board board;
        public SongManager media;
        public Player player1;
        public Player player2;
        public Entity scoreEntity;
        public Entity actionEntity;
        public int mode;
        public Scoring scoreboard;

        public Caesura()
        {
            graphics = new GraphicsDeviceManager(this);
            random = new Random();
            board = new Board(this, random);
            media = new SongManager(this);
            player1 = new Player("red", this, 1);
            player2 = new Player("blue", this, 2);
            scoreEntity = new Entity("orange", this, 1, random);
            actionEntity = new Entity("green", this, 2, random);
            Content.RootDirectory = "Content";
            mode = 3;
            scoreboard = new Scoring(this);
        }

        protected override void Initialize()
        {
            //Adjust screen to the size of the board
            graphics.PreferredBackBufferWidth = board.board.GetLength(0) * 20;
            graphics.PreferredBackBufferHeight = board.board.GetLength(1) * 20 + 140;
            graphics.ApplyChanges();
            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            board.LoadContent(Content);
            player1.LoadContent(Content);
            player2.LoadContent(Content);
            scoreEntity.LoadContent(Content);
            actionEntity.LoadContent(Content);
            media.LoadContent(Content);
            scoreboard.LoadContent(Content);
            MediaPlayer.Play(media.songs[3]);
        }

        protected override void Update(GameTime gameTime)
        {
            if(mode != 3)
            {
                player1.Update(gameTime);
                player2.Update(gameTime);
                board.Update();
                if (!media.caesura)
                {
                    scoreEntity.Update(gameTime);
                    actionEntity.Update(gameTime);
                }
                media.Update();
                scoreboard.Update();
                base.Update(gameTime);
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                mode = 1;
                MediaPlayer.Play(media.songs[1]);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {               
                scoreboard.newGame(Content);
                MediaPlayer.Play(media.songs[3]);
                mode = 3;
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
            spriteBatch.Begin();
            board.Draw(spriteBatch);
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            scoreEntity.Draw(spriteBatch);
            actionEntity.Draw(spriteBatch);
            scoreboard.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
