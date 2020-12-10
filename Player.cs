using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesura
{
    public class Player
    {

        public string playerName;
        public int playerNumber;
        Texture2D playerTexture;
        BoundingRectangle bounds;
        Caesura game;

        bool moving = false;
        int destinationx;
        int destinationy;
        public string direction = "right";
        public string nextdirection = "right";
        KeyboardState previousKeyboardState;

        public int points = 0;
        public int currentPoints = 0;
        public int movementPoints = 500;

        public Player(string color, Caesura currentGame, int number)
        {
            playerName = color;
            playerNumber = number;
            game = currentGame;
            if (playerNumber == 1)
            {
                bounds = new BoundingRectangle(0, 0, 20, 20);
                destinationx = (int)bounds.X;
                destinationy = (int)bounds.Y;
            }
            else if (playerNumber == 2)
            {
                bounds = new BoundingRectangle(game.board.board.GetLength(0) * 20 - 20, game.board.board.GetLength(1) * 20 - 20, 20, 20);
                destinationx = (int)bounds.X;
                destinationy = (int)bounds.Y;
            }                                  
        }

        public void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>(playerName + "Player");
            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)playerName[0];
        }

        public void Update(GameTime timeSpan)
        {
            movePlayer(timeSpan);
            foreach(Tile i in game.board.items)
            {
                if (bounds.CollidesWith(i.bounds))
                {
                    if(i.type == 1)
                    {
                        game.media.ModeChange(i.musicSpeed - 1);
                        nextdirection = direction;
                    }
                }               
            }
            if(playerNumber == 1 && bounds.CollidesWith(game.player2.bounds))
            {
                if(movementPoints < game.player2.movementPoints)
                {
                    bounds.X = 0;
                    destinationx = 0;
                    bounds.Y = 0;
                    destinationy = 0;
                }
                else if(movementPoints > game.player2.movementPoints)
                {
                    game.player2.bounds.X = game.board.board.GetLength(0) * 20 - 20;
                    game.player2.destinationx = game.board.board.GetLength(0) * 20 - 20;
                    game.player2.bounds.Y = game.board.board.GetLength(1) * 20 - 20;
                    game.player2.destinationy = game.board.board.GetLength(1) * 20 - 20;
                }
            }
            if (bounds.CollidesWith(game.scoreEntity.bounds) || bounds.CollidesWith(game.actionEntity.bounds))
            {
                for (int i = 0; i < game.board.board.GetLength(0); i++)
                {
                    for (int j = 0; j < game.board.board.GetLength(1); j++)
                    {
                        if (game.board.board[i, j] == playerName[0])
                        {
                            game.board.board[i, j] = '0';
                        }
                    }
                }
                if(playerNumber == 1)
                {
                    bounds.X = 0;
                    destinationx = 0;
                    bounds.Y = 0;
                    destinationy = 0;
                }
                else if(playerNumber == 2)
                {
                    bounds.X = game.board.board.GetLength(0) * 20 - 20;
                    destinationx = game.board.board.GetLength(0) * 20 - 20;
                    bounds.Y = game.board.board.GetLength(1) * 20 - 20;
                    destinationy = game.board.board.GetLength(1) * 20 - 20;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, bounds, Color.White);
        }


        public void movePlayer(GameTime timeSpan)
        {
            var newKeyboardState = Keyboard.GetState();
            if (game.mode == 1 || game.mode == 0)
            {
                if(playerNumber == 2)
                {
                    if(moving == false)
                    {
                        if (newKeyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))
                        {
                            if(destinationy - 20 >= 0 && !(game.media.caesura && ((game.board.board[(int)bounds.X / 20, (int)(bounds.Y - 20) / 20] == game.player1.playerName[0]) || (game.board.board[(int)bounds.X / 20, (int)(bounds.Y - 20) / 20] == '0'))))
                            {
                                direction = "up";
                                moving = true;
                                destinationy -= 20;
                                if(!game.media.caesura)
                                {
                                    if (game.mode == 0)
                                    {
                                        movementPoints -= 2;
                                    }
                                    else
                                    {
                                        movementPoints--;
                                    }
                                }                                                            
                            }
                        }
                        else if (newKeyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down))
                        {
                            if(destinationy + 20 < game.board.board.GetLength(1) * 20 && !(game.media.caesura && ((game.board.board[(int)bounds.X / 20, (int)(bounds.Y + 20) / 20] == game.player1.playerName[0]) || (game.board.board[(int)bounds.X / 20, (int)(bounds.Y + 20) / 20] == '0'))))
                            {
                                direction = "down";
                                moving = true;
                                destinationy += 20;
                                if (!game.media.caesura)
                                {
                                    if (game.mode == 0)
                                    {
                                        movementPoints -= 2;
                                    }
                                    else
                                    {
                                        movementPoints--;
                                    }
                                }
                            }                          
                        }
                        else if (newKeyboardState.IsKeyDown(Keys.Left) && previousKeyboardState.IsKeyUp(Keys.Left))
                        {
                            if(destinationx - 20 >= 0 && !(game.media.caesura && ((game.board.board[(int)(bounds.X - 20) / 20, (int)bounds.Y / 20] == game.player1.playerName[0]) || (game.board.board[(int)(bounds.X - 20) / 20, (int)bounds.Y / 20] == '0'))))
                            {
                                direction = "left";
                                moving = true;
                                destinationx -= 20;
                                if (!game.media.caesura)
                                {
                                    if (game.mode == 0)
                                    {
                                        movementPoints -= 2;
                                    }
                                    else
                                    {
                                        movementPoints--;
                                    }
                                }
                            }           
                        }
                        else if (newKeyboardState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyUp(Keys.Right))
                        {
                            if(destinationx + 20 < game.board.board.GetLength(0) * 20 && !(game.media.caesura && ((game.board.board[(int)(bounds.X + 20) / 20, (int)bounds.Y / 20] == game.player1.playerName[0]) || (game.board.board[(int)(bounds.X + 20) / 20, (int)bounds.Y / 20] == '0'))))
                            {
                                direction = "right";
                                moving = true;
                                destinationx += 20;
                                if (!game.media.caesura)
                                {
                                    if (game.mode == 0)
                                    {
                                        movementPoints -= 2;
                                    }
                                    else
                                    {
                                        movementPoints--;
                                    }
                                }
                            }                            
                        }
                    }              
                    else if(direction == "up")
                    {
                        if (destinationy < bounds.Y)
                        {
                            bounds.Y -= 4;
                        }
                        else
                        {
                            moving = false;
                            if(game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 's')
                            {
                                points += 20;
                            }
                            else if(game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 'm')
                            {
                                movementPoints += 20;
                            }
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)playerName[0];
                        }
                    }
                    else if(direction == "down")
                    {
                        if (destinationy > bounds.Y)
                        {
                            bounds.Y += 4;
                        }
                        else
                        {
                            moving = false;
                            if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 's')
                            {
                                points += 20;
                            }
                            else if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 'm')
                            {
                                movementPoints += 20;
                            }
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)playerName[0];
                        }
                    }
                    else if (direction == "left")
                    {
                        if (destinationx < bounds.X)
                        {
                            bounds.X -= 4;
                        }
                        else
                        {
                            moving = false;
                            if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 's')
                            {
                                points += 20;
                            }
                            else if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 'm')
                            {
                                movementPoints += 20;
                            }
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)playerName[0];
                        }
                    }
                    else if (direction == "right")
                    {
                        if (destinationx > bounds.X)
                        {
                            bounds.X += 4;
                        }
                        else
                        {
                            moving = false;
                            if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 's')
                            {
                                points += 20;
                            }
                            else if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 'm')
                            {
                                movementPoints += 20;
                            }
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)playerName[0];
                        }
                    }
                }
                else if(playerNumber == 1)
                {
                    if (moving == false)
                    {
                        if (newKeyboardState.IsKeyDown(Keys.W) && previousKeyboardState.IsKeyUp(Keys.W))
                        {
                            if (destinationy - 20 >= 0 && !(game.media.caesura && ((game.board.board[(int)bounds.X / 20, (int)(bounds.Y - 20) / 20] == game.player2.playerName[0]) || (game.board.board[(int)bounds.X / 20, (int)(bounds.Y - 20) / 20] == '0'))))
                            {
                                direction = "up";
                                moving = true;
                                destinationy -= 20;
                                if (!game.media.caesura)
                                {
                                    if (game.mode == 0)
                                    {
                                        movementPoints -= 2;
                                    }
                                    else
                                    {
                                        movementPoints--;
                                    }
                                }
                            }
                        }
                        else if (newKeyboardState.IsKeyDown(Keys.S) && previousKeyboardState.IsKeyUp(Keys.S))
                        {
                            if (destinationy + 20 < game.board.board.GetLength(1) * 20 && !(game.media.caesura && ((game.board.board[(int)bounds.X / 20, (int)(bounds.Y + 20) / 20] == game.player2.playerName[0]) || (game.board.board[(int)bounds.X / 20, (int)(bounds.Y + 20) / 20] == '0'))))
                            {
                                direction = "down";
                                moving = true;
                                destinationy += 20;
                                if (!game.media.caesura)
                                {
                                    if (game.mode == 0)
                                    {
                                        movementPoints -= 2;
                                    }
                                    else
                                    {
                                        movementPoints--;
                                    }
                                }
                            }
                        }
                        else if (newKeyboardState.IsKeyDown(Keys.A) && previousKeyboardState.IsKeyUp(Keys.A))
                        {
                            if (destinationx - 20 >= 0 && !(game.media.caesura && ((game.board.board[(int)(bounds.X - 20) / 20, (int)bounds.Y / 20] == game.player2.playerName[0]) || (game.board.board[(int)(bounds.X - 20) / 20, (int)bounds.Y / 20] == '0'))))
                            {
                                direction = "left";
                                moving = true;
                                destinationx -= 20;
                                if (!game.media.caesura)
                                {
                                    if (game.mode == 0)
                                    {
                                        movementPoints -= 2;
                                    }
                                    else
                                    {
                                        movementPoints--;
                                    }
                                }
                            }
                        }
                        else if (newKeyboardState.IsKeyDown(Keys.D) && previousKeyboardState.IsKeyUp(Keys.D))
                        {
                            if (destinationx + 20 < game.board.board.GetLength(0) * 20 && !(game.media.caesura && ((game.board.board[(int)(bounds.X + 20) / 20, (int)bounds.Y / 20] == game.player2.playerName[0]) || (game.board.board[(int)(bounds.X + 20) / 20, (int)bounds.Y / 20] == '0'))))
                            {
                                direction = "right";
                                moving = true;
                                destinationx += 20;
                                if (!game.media.caesura)
                                {
                                    if (game.mode == 0)
                                    {
                                        movementPoints -= 2;
                                    }
                                    else
                                    {
                                        movementPoints--;
                                    }
                                }
                            }
                        }
                    }
                    else if (direction == "up")
                    {
                        if (destinationy < bounds.Y)
                        {
                            bounds.Y -= 4;
                        }
                        else
                        {
                            moving = false;
                            if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 's')
                            {
                                points += 20;
                            }
                            else if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 'm')
                            {
                                movementPoints += 20;
                            }
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)playerName[0];
                        }
                    }
                    else if (direction == "down")
                    {
                        if (destinationy > bounds.Y)
                        {
                            bounds.Y += 4;
                        }
                        else
                        {
                            moving = false;
                            if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 's')
                            {
                                points += 20;
                            }
                            else if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 'm')
                            {
                                movementPoints += 20;
                            }
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)playerName[0];
                        }
                    }
                    else if (direction == "left")
                    {
                        if (destinationx < bounds.X)
                        {
                            bounds.X -= 4;
                        }
                        else
                        {
                            moving = false;
                            if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 's')
                            {
                                points += 20;
                            }
                            else if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 'm')
                            {
                                movementPoints += 20;
                            }
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)playerName[0];
                        }
                    }
                    else if (direction == "right")
                    {
                        if (destinationx > bounds.X)
                        {
                            bounds.X += 4;
                        }
                        else
                        {
                            moving = false;
                            if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 's')
                            {
                                points += 20;
                            }
                            else if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] == 'm')
                            {
                                movementPoints += 20;
                            }
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)playerName[0];
                        }
                    }
                }
            }
            else if(game.mode == 2)
            {
                if(playerNumber == 2)
                {
                    if (newKeyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))
                    {
                        nextdirection = "up";
                    }
                    else if (newKeyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down))
                    {
                        nextdirection = "down";
                    }
                    else if (newKeyboardState.IsKeyDown(Keys.Left) && previousKeyboardState.IsKeyUp(Keys.Left))
                    {
                        nextdirection = "left";
                    }
                    else if (newKeyboardState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyUp(Keys.Right))
                    {
                        nextdirection = "right";
                    }
                }
                else if(playerNumber == 1)
                {
                    if (newKeyboardState.IsKeyDown(Keys.W) && previousKeyboardState.IsKeyUp(Keys.W))
                    {
                        nextdirection = "up";
                    }
                    else if (newKeyboardState.IsKeyDown(Keys.S) && previousKeyboardState.IsKeyUp(Keys.S))
                    {
                        nextdirection = "down";
                    }
                    else if (newKeyboardState.IsKeyDown(Keys.A) && previousKeyboardState.IsKeyUp(Keys.A))
                    {
                        nextdirection = "left";
                    }
                    else if (newKeyboardState.IsKeyDown(Keys.D) && previousKeyboardState.IsKeyUp(Keys.D))
                    {
                        nextdirection = "right";
                    }
                }
                
                if (moving == false)
                {
                    if (direction == "up")
                    {
                        if (destinationy - 20 >= 0)
                        {                           
                            destinationy -= 20;
                        }
                        moving = true;
                    }
                    else if (direction == "down")
                    {
                        if (destinationy + 20 < game.board.board.GetLength(1) * 20)
                        {                           
                            destinationy += 20;
                        }
                        moving = true;
                    }
                    else if (direction == "left")
                    {
                        if (destinationx - 20 >= 0)
                        {
                            destinationx -= 20;
                        }
                        moving = true;
                    }
                    else if (direction == "right")
                    {
                        if (destinationx + 20 < game.board.board.GetLength(0) * 20)
                        {
                            destinationx += 20;
                        }
                        moving = true;
                    }
                }
                if (moving == true)
                {
                    if (direction == "up")
                    {
                        if (destinationy < bounds.Y)
                        {
                            bounds.Y -= 4;
                        }
                        else
                        {
                            moving = false;
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)playerName[0];
                            movementPoints--;
                            direction = nextdirection;
                        }
                    }
                    else if (direction == "down")
                    {
                        if (destinationy > bounds.Y)
                        {
                            bounds.Y += 4;
                        }
                        else
                        {
                            moving = false;
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)playerName[0];
                            movementPoints--;
                            direction = nextdirection;
                        }
                    }
                    else if (direction == "left")
                    {
                        if (destinationx < bounds.X)
                        {
                            bounds.X -= 4;
                        }
                        else
                        {
                            moving = false;
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)playerName[0];
                            movementPoints--;
                            direction = nextdirection;
                        }
                    }
                    else if (direction == "right")
                    {
                        if (destinationx > bounds.X)
                        {
                            bounds.X += 4;
                        }
                        else
                        {
                            moving = false;
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)playerName[0];
                            movementPoints--;
                            direction = nextdirection;
                        }
                    }
                }
            }
            previousKeyboardState = newKeyboardState;           
        }
    }
}
