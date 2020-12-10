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
    public class Entity
    {
        string entityName;
        int entityNumber;
        Texture2D entityTexture;
        public BoundingRectangle bounds;
        Caesura game;
        Random random;

        bool moving = false;
        int destinationx;
        int destinationy;
        public string direction = "right";
        public string nextdirection = "right";

        int slowDropFrequency = 30;
        int normalDropFrequency = 60;

        public Entity(string color, Caesura currentGame, int number, Random seed)
        {
            entityName = color;
            entityNumber = number;
            game = currentGame;
            random = seed;
            if (entityNumber == 1)
            {
                bounds = new BoundingRectangle(0, game.board.board.GetLength(1) * 20 - 20, 20, 20);
                destinationx = (int)bounds.X;
                destinationy = (int)bounds.Y;
            }
            else if (entityNumber == 2)
            {
                bounds = new BoundingRectangle(game.board.board.GetLength(0) * 20 - 20, 0, 20, 20);
                destinationx = (int)bounds.X;
                destinationy = (int)bounds.Y;
            }
        }

        public void LoadContent(ContentManager content)
        {
            entityTexture = content.Load<Texture2D>(entityName + "Player");
            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)entityName[0];
        }

        public void Update(GameTime timeSpan)
        {
            if(!game.media.caesura && (game.mode != 3))
            {
                moveEntity(timeSpan);
            }
            foreach (Tile i in game.board.items)
            {
                if (bounds.CollidesWith(i.bounds))
                {
                    if (i.type == 1)
                    {
                        game.media.ModeChange(i.musicSpeed - 1);
                        nextdirection = direction;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(entityTexture, bounds, Color.White);
        }

        public void moveEntity(GameTime timeSpan)
        {
            int dropChance = random.Next(0, 10001);
            if (moving == false)
            {
                int movementChoice = random.Next(1, 9);
                int reroll = random.Next(1, 4);
                if (movementChoice == 1)
                {
                    if (destinationy - 20 >= 0)
                    {
                        if(game.board.board[(int)bounds.X / 20, (int)(bounds.Y - 20) / 20] != entityName[0])
                        {
                            direction = "up";
                            moving = true;
                            destinationy -= 20;
                        }
                        else if(reroll == 1)
                        {
                            direction = "up";
                            moving = true;
                            destinationy -= 20;
                        }
                    }
                }
                else if (movementChoice == 2)
                {
                    if (destinationy + 20 < game.board.board.GetLength(1) * 20)
                    {
                        if (game.board.board[(int)bounds.X / 20, (int)(bounds.Y + 20) / 20] != entityName[0])
                        {
                            direction = "down";
                            moving = true;
                            destinationy += 20;
                        }
                        else if(reroll == 1)
                        {
                            direction = "down";
                            moving = true;
                            destinationy += 20;
                        }
                        
                    }
                }
                else if (movementChoice == 3)
                {
                    if (destinationx - 20 >= 0)
                    {
                        if (game.board.board[(int)(bounds.X - 20) / 20, (int)bounds.Y / 20] != entityName[0])
                        {
                            direction = "left";
                            moving = true;
                            destinationx -= 20;
                        }
                        else if(reroll == 1)
                        {
                            direction = "left";
                            moving = true;
                            destinationx -= 20;
                        }
                    }
                }
                else if (movementChoice == 4)
                {
                    if (destinationx + 20 < game.board.board.GetLength(0) * 20)
                    {
                        if (game.board.board[(int)(bounds.X + 20) / 20, (int)bounds.Y / 20] != entityName[0])
                        {
                            direction = "right";
                            moving = true;
                            destinationx += 20;
                        }
                        else if(reroll == 1)
                        {
                            direction = "right";
                            moving = true;
                            destinationx += 20;
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
                    ItemDrop(dropChance);
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
                    ItemDrop(dropChance);
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
                    ItemDrop(dropChance);
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
                    ItemDrop(dropChance);
                }
            }
        }

        public void ItemDrop(int dropChance)
        {
            if (game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] != 'm' && game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] != 's')
            {
                game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = (char)entityName[0];
                if (game.mode == 0)
                {
                    if (dropChance < slowDropFrequency)
                    {
                        if(entityNumber == 1)
                        {
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = 's';
                        }
                        else
                        {
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = 'm';
                        }
                    }
                }
                else
                {
                    if (dropChance < normalDropFrequency)
                    {
                        if (entityNumber == 1)
                        {
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = 's';
                        }
                        else
                        {
                            game.board.board[(int)bounds.X / 20, (int)bounds.Y / 20] = 'm';
                        }
                    }
                }
            }
        }
    }
}
