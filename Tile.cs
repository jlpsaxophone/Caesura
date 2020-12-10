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
    public class Tile
    {
        Texture2D texture;
        public BoundingRectangle bounds;
        public int type;
        public int musicSpeed;

        public Tile(int kind, int speed, BoundingRectangle location, ContentManager content)
        {
            type = kind;
            bounds = location;
            if(type == 1)
            {
                musicSpeed = speed;
                if(speed == 1) texture = content.Load<Texture2D>("slow");
                else if(speed == 2) texture = content.Load<Texture2D>("medium");
                else if(speed == 3) texture = content.Load<Texture2D>("fast");
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
