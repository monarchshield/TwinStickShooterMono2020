using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShootyGame
{
    class ParallaxBackground
    {
        private Texture2D Texture;      //The image to use
        private Vector2 Offset;         //Offset to start drawing our image
        public Vector2 Speed;           //Speed of movement of our parallax effect
        public float Zoom;              //Zoom level of our image

        private Viewport Viewport;      //Our game viewport
        private Player m_playerref;
     

        //Calculate Rectangle dimensions, based on offset/viewport/zoom values
        private Rectangle Rectangle
        {
            get { return new Rectangle((int)(Offset.X), (int)(Offset.Y), (int)(3139), (int)(2191)); }
        }

        public ParallaxBackground(Texture2D texture, Vector2 speed, float zoom, Player playerref)
        {
            Texture = texture;
            Offset = Vector2.Zero;
            Speed = speed;
            Zoom = zoom;
            m_playerref = playerref;
        }

        public void Update(GameTime gametime, Viewport viewport,Vector2 Direction)
        {
            float elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;

            //Store the viewports
            Viewport = viewport;
            
            if(Direction == Vector2.Zero) 
            {
                Direction = new Vector2(0, .1f); 
            }

            //Calculate the distance to move our image, based on speed
            Vector2 distance = Direction  * Speed * elapsed;

            //Update our offset
             Offset += distance;

            //Offset.X = MathHelper.Clamp(Offset.X, -100, 3400);
            //Offset.Y = MathHelper.Clamp(Offset.X, -100, 2400);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(Viewport.X, Viewport.Y), Rectangle, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 1);
        }
    }
}
