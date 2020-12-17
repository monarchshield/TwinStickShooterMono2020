using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShootyGame
{
    class Border : Pawn
    {
        public Color m_Color;
        protected Player m_playerref;

        float m_colourtimestamp;
        int m_colorID;


        public Border() { }
        
        public Border(Texture2D texture)
        {
            m_currentposition = new Vector2(200, 200);
            m_texture = texture;
            m_Color = Color.White;
        }

        public Border(Texture2D texture, Player playerref)
        {
            m_currentposition = new Vector2(200, 200);
            m_texture = texture;
            m_playerref = playerref;
            m_Color = Color.White;
            m_colourtimestamp = 0;
            m_colorID = 0;



        }

        public void Update(float gametime)
        {
            ChangeColours(gametime);
            PlayInboundary();
        }

        public void PlayInboundary()
        {
            //m_playerref.m_currentposition.X = MathHelper.Clamp(m_currentposition.X, 330, 2250);
            m_playerref.m_currentposition.X =  MathHelper.Clamp(m_playerref.m_currentposition.X, 330, 3225);
            m_playerref.m_currentposition.Y = MathHelper.Clamp(m_playerref.m_currentposition.Y, 315, 2280);
        }


        public void ChangeColours(float gametime)
        {
            m_colourtimestamp += gametime * .5f;

            if(m_colourtimestamp > 1) 
            {  m_colorID += 1;
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
                    m_Color = Color.Lerp(Color.Orange, Color.LightGoldenrodYellow, m_colourtimestamp);
                    break;
                case 3:
                    m_Color = Color.Lerp(Color.LightGoldenrodYellow, Color.LimeGreen, m_colourtimestamp);
                    break;
                case 4:
                    m_Color = Color.Lerp(Color.LimeGreen, Color.SkyBlue, m_colourtimestamp);
                    break;
                case 5:
                    m_Color = Color.Lerp(Color.SkyBlue, Color.HotPink, m_colourtimestamp);
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

            

            //m_Color = Color.Lerp(Color.White, Color.Red, 30);
        }

        public void Draw(SpriteBatch spritebatch)
        {
           
            spritebatch.Draw(m_texture, new Rectangle((int)m_currentposition.X, (int)m_currentposition.Y, m_texture.Width, m_texture.Height), m_Color);
           
        }

    }
}
