using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShootyGame
{
    class Border : Pawn
    {
        public Color Color;
        public Border() { }
        
        public Border(Texture2D texture)
        {
            m_currentposition = new Vector2(200, 200);
            m_texture = texture;
        }

        public void Update(float gametime)
        {

        }

        public void ChangeColours()
        {

        }

        public void Draw(SpriteBatch spritebatch)
        {
           
            spritebatch.Draw(m_texture, new Rectangle((int)m_currentposition.X, (int)m_currentposition.Y, m_texture.Width, m_texture.Height), Color.White);
           
        }

    }
}
