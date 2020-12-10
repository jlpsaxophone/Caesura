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
    public class Board
    {
        Caesura game;
        public char[,] board;
        GraphicsDeviceManager graphics;
        Texture2D blankTile;
        Texture2D orangeTile;
        Texture2D redTile;
        Texture2D greenTile;
        Texture2D blueTile;
        Texture2D movementTile;
        Texture2D pointTile;
        SpriteFont text;
        Texture2D caesuraTexture;
        Random random;
        public List<Tile> items = new List<Tile>();

        public Board(Caesura game, Random randomItem)
        {
            this.game = game;
            this.random = randomItem;
            int width = random.Next(40, 61);
            int height = random.Next(26, 42);
            board = new char[width, height];
        }

        public void LoadContent(ContentManager content)
        {
            blankTile = content.Load<Texture2D>("blankTile");
            redTile = content.Load<Texture2D>("redTile");
            blueTile = content.Load<Texture2D>("blueTile");
            greenTile = content.Load<Texture2D>("greenTile");
            orangeTile = content.Load<Texture2D>("orangeTile");
            movementTile = content.Load<Texture2D>("movement");
            pointTile = content.Load<Texture2D>("score");           
            text = content.Load<SpriteFont>("File");
            caesuraTexture = content.Load<Texture2D>("caesura");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for(int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = '0';
                }
            }
            setMusicTiles(content);
        }

        public void Update()
        {
            game.player1.currentPoints = countTiles(game.player1.playerName[0]);
            game.player2.currentPoints = countTiles(game.player2.playerName[0]);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i, j] == '0')
                    {
                        spriteBatch.Draw(blankTile, new Rectangle(20*i, 20*j, 20, 20), Color.White);
                    }
                    else if(board[i, j] == 'r')
                    {
                        spriteBatch.Draw(redTile, new Rectangle(20 * i, 20 * j, 20, 20), Color.White);
                    }
                    else if (board[i, j] == 'b')
                    {
                        spriteBatch.Draw(blueTile, new Rectangle(20 * i, 20 * j, 20, 20), Color.White);
                    }
                    else if (board[i, j] == 'g')
                    {
                        spriteBatch.Draw(greenTile, new Rectangle(20 * i, 20 * j, 20, 20), Color.White);
                    }
                    else if (board[i, j] == 'o')
                    {
                        spriteBatch.Draw(orangeTile, new Rectangle(20 * i, 20 * j, 20, 20), Color.White);
                    }
                    else if (board[i, j] == 's')
                    {
                        spriteBatch.Draw(pointTile, new Rectangle(20 * i, 20 * j, 20, 20), Color.White);
                    }
                    else if (board[i, j] == 'm')
                    {
                        spriteBatch.Draw(movementTile, new Rectangle(20 * i, 20 * j, 20, 20), Color.White);
                    }
                }
            }

            foreach(Tile i in items)
            {
                i.Draw(spriteBatch);
            }

            spriteBatch.DrawString(text,"Tiles: " + game.player1.currentPoints, new Vector2(40, board.GetLength(1) * 20 + 20), Color.Red);
            spriteBatch.DrawString(text, "Score: " + game.player1.points, new Vector2(40, board.GetLength(1) * 20 + 60), Color.Red);
            spriteBatch.DrawString(text, "Actions: " + game.player1.movementPoints, new Vector2(40, board.GetLength(1) * 20 + 100), Color.Red);
            
            spriteBatch.DrawString(text, "Tiles: " + game.player2.currentPoints, new Vector2(board.GetLength(0)*20 - 150, board.GetLength(1) * 20 + 20), Color.Blue);
            spriteBatch.DrawString(text, "Score: " + game.player2.points, new Vector2(board.GetLength(0) * 20 - 150, board.GetLength(1) * 20 + 60), Color.Blue);
            spriteBatch.DrawString(text, "Actions: " + game.player2.movementPoints, new Vector2(board.GetLength(0) * 20 - 150, board.GetLength(1) * 20 + 100), Color.Blue);     
            
            if(game.mode == 3)
            {
                spriteBatch.Draw(caesuraTexture, new Rectangle((game.board.board.GetLength(0) * 20) / 2 - 162, (game.board.board.GetLength(1) * 20) / 2 + 40, 324, 60), Color.White);
                spriteBatch.DrawString(text, "press space to start", new Vector2((board.GetLength(0) * 20) / 2 - 84, board.GetLength(1) * 20 + 20), Color.Turquoise);
            }
            else
            {
                if (!(game.scoreboard.redWon || game.scoreboard.blueWon))
                {
                    spriteBatch.Draw(caesuraTexture, new Rectangle((game.board.board.GetLength(0) * 20) / 2 - 162, (game.board.board.GetLength(1) * 20) + 40, 324, 60), Color.White);
                }
            }           
        }

        public void setMusicTiles(ContentManager content)
        {

            int fastx = random.Next(0, board.GetLength(0));
            int fasty = random.Next(0, board.GetLength(1));
            BoundingRectangle fastbounds = new BoundingRectangle(fastx * 20, fasty * 20, 20, 20);
            items.Add(new Tile(1, 3, fastbounds, content));
            int mediumx = fastx;
            while(mediumx == fastx)
            {
                mediumx = random.Next(0, board.GetLength(0));
            }         
            int mediumy = fasty;
            while (mediumy == fasty)
            {
                mediumy = random.Next(0, board.GetLength(1));
            }
            BoundingRectangle mediumbounds = new BoundingRectangle(mediumx * 20, mediumy * 20, 20, 20);
            items.Add(new Tile(1, 2, mediumbounds, content));

            int slowx = fastx;
            while (slowx == fastx || slowx == mediumx)
            {
                slowx = random.Next(0, board.GetLength(0));
            }
            int slowy = fasty;
            while (slowy == fasty || slowy == mediumy)
            {
                slowy = random.Next(0, board.GetLength(1));
            }
            BoundingRectangle slowbounds = new BoundingRectangle(slowx * 20, slowy * 20, 20, 20);
            items.Add(new Tile(1, 1, slowbounds, content));
        }

        public int countTiles(char c)
        {
            int counter = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i, j] == c)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
    }
}
