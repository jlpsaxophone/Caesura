using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesura
{
    public struct BoundingRectangle
    {
        public float X;

        public float Y;

        public float Width;

        public float Height;

        public BoundingRectangle(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public BoundingRectangle(Vector2 position, float width, float height)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
        }

        public bool CollidesWith(BoundingRectangle other)
        {
            return !(this.X > other.X + other.Width - 1
                  || this.X + this.Width - 1 < other.X
                  || this.Y > other.Y + other.Height - 1
                  || this.Y + this.Height - 1 < other.Y);
        }

        public static implicit operator Rectangle(BoundingRectangle br)
        {
            return new Rectangle(
                (int)br.X,
                (int)br.Y,
                (int)br.Width,
                (int)br.Height);
        }
    }
}