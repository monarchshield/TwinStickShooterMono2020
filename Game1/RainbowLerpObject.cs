using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShootyGame
{
    class RainbowLerpObject
    {
        protected Color m_Color;
        protected float m_colourtimestamp;
        protected int m_colorID;

        protected Texture2D m_texture;
        protected Vector2 m_position;

        public RainbowLerpObject() { }

        public RainbowLerpObject(Texture2D texture, Vector2 Position)
        {
            m_texture = texture;
            m_position = Position;
            m_colorID = 0;
            m_Color = Color.White;
            m_colourtimestamp = 0;
        }

        public void Update(float gametime)
        {
            ChangeColours(gametime);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(m_texture, new Rectangle((int)m_position.X, (int)m_position.Y, m_texture.Width, m_texture.Height), m_Color);

        }


        public void ChangeColours(float gametime)
        {
            m_colourtimestamp += gametime * .5f;

            if (m_colourtimestamp > 1)
            {
                m_colorID += 1;
                m_colourtimestamp = 0;
            }

            switch (m_colorID)
            {
                case 0:
                    m_Color = Color.Lerp(Color.Purple, Color.Red, m_colourtimestamp);
                    break;
                case 1:
                    m_Color = Color.Lerp(Color.Red, Color.Orange, m_colourtimestamp);
                    break;
                case 2:
                    m_Color = Color.Lerp(Color.Orange, Color.Yellow, m_colourtimestamp);
                    break;
                case 3:
                    m_Color = Color.Lerp(Color.Yellow, Color.Green, m_colourtimestamp);
                    break;
                case 4:
                    m_Color = Color.Lerp(Color.Green, Color.Blue, m_colourtimestamp);
                    break;
                case 5:
                    m_Color = Color.Lerp(Color.Blue, Color.HotPink, m_colourtimestamp);
                    break;
                case 6:
                    m_Color = Color.Lerp(Color.HotPink, Color.MediumPurple, m_colourtimestamp);
                    break;
                case 7:
                    m_Color = Color.Lerp(Color.MediumPurple, Color.Purple, m_colourtimestamp);
                    break;


                default:
                    m_colourtimestamp = 0;
                    m_colorID = 0;
                    break;
            }
        }


    }
}
