using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesura
{
    public class Scoring
    {
        Texture2D redWin;
        public bool redWon = false;
        Texture2D blueWin;
        public bool blueWon = false;
        Caesura game;

        public Scoring(Caesura game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content)
        {
            redWin = content.Load<Texture2D>("redwin");
            blueWin = content.Load<Texture2D>("bluewin");            
        }

        public void Update()
        {
            if(game.player1.points >= 500 || game.player2.movementPoints < 0)
            {
                redWon = true;
                game.mode = 3;
            }
            if (game.player2.points >= 500 || game.player1.movementPoints < 0)
            {
                blueWon = true;
                game.mode = 3;
            }
            if ((game.player1.points >= 500 || game.player2.movementPoints < 0) && (game.player2.points >= 500 || game.player1.movementPoints < 0))
            {
                if (game.player1.points > game.player2.points)
                {
                    redWon = true;
                    game.mode = 3;
                }
                else if (game.player2.points > game.player1.points)
                {
                    blueWon = true;
                    game.mode = 3;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(redWon && !blueWon)
            {
                spriteBatch.Draw(redWin, new Rectangle((game.board.board.GetLength(0) * 20) / 2 - 210, (game.board.board.GetLength(1) * 20) + 40, 420, 60), Color.White);
            }
            if(blueWon && !redWon)
            {
                spriteBatch.Draw(blueWin, new Rectangle((game.board.board.GetLength(0) * 20) / 2 - 237, (game.board.board.GetLength(1) * 20) + 40, 420, 60), Color.White);
            }           
        }

        public void newGame(ContentManager content)
        {
            game.random = new Random();
            game.board = new Board(game, game.random);
            game.media = new SongManager(game);
            game.player1 = new Player("red", game, 1);
            game.player2 = new Player("blue", game, 2);
            game.scoreEntity = new Entity("orange", game, 1, game.random);
            game.actionEntity = new Entity("green", game, 2, game.random);

            game.board.LoadContent(content);
            game.player1.LoadContent(content);
            game.player2.LoadContent(content);
            game.scoreEntity.LoadContent(content);
            game.actionEntity.LoadContent(content);
            game.media.LoadContent(content);
            game.scoreboard.LoadContent(content);

            game.graphics.PreferredBackBufferWidth = game.board.board.GetLength(0) * 20;
            game.graphics.PreferredBackBufferHeight = game.board.board.GetLength(1) * 20 + 140;
            game.graphics.ApplyChanges();
        }
    }
}
