using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AndroidShootyGame
{
    class Particle
    {
        protected Color color;
        protected Vector2 m_position;
        protected Texture2D m_texture;
        protected bool m_alive;


        public Particle()
        { 
            /* Default constructor this class is used for inheritence mostly */
        }

        public virtual void Update(GameTime gameTime)
        {
           
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            if (m_alive)
            {
                //This is the texture being drawn  
                spritebatch.Draw(m_texture, new Rectangle((int)m_position.X, (int)m_position.Y, m_texture.Width, m_texture.Height), null, Color.White);
            }
        }

    }
}
