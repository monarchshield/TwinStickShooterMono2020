using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootyGame
{
    class Button
    {
        protected Vector2 m_position;
        protected bool m_collision;
        protected Texture2D m_texture;
        protected MouseState mousestate;
        private Rectangle CollisionRect;
        private Point CollisionPoint;
        
        
        protected Color m_color;
        
        public Button() { /* Default constructor */ }
        public Button(Texture2D texture, Vector2 Position)
        {
            m_texture = texture;
            m_position = Position;
            CollisionPoint = new Point(10, 10);
            CollisionRect = new Rectangle((int)m_position.X, (int)m_position.Y, m_texture.Height, m_texture.Width);
            m_color = Color.White;
        }

        public bool IsColliding()
        {
            //mousestate.Position

            if (CollisionRect.Intersects(new Rectangle(mousestate.Position,CollisionPoint)))
            {
                m_color = Color.Red;
                m_collision = true;
                return m_collision;
            }

            else
            {
                m_color = Color.White;
                m_collision = false;
                return m_collision;
            }
        }

        public virtual void Update()
        {
           
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
                spritebatch.Draw(m_texture, new Rectangle((int)mousestate.Position.X - (m_texture.Width/2), (int)m_position.Y - (m_texture.Height/2), m_texture.Height, m_texture.Width ), m_color);
            spritebatch.End();
        }

    }
}
