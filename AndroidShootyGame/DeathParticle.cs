using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AndroidShootyGame
{
    class DeathParticle : Particle 
    {

        private float m_killtimer;
        private float m_animationtimer;
        private int m_currentframe;

        public DeathParticle()
        {
            /* Default constructor */
        }

        public DeathParticle(Texture2D texture, Vector2 position)
        {
            m_texture = texture;
            m_position = position;
            m_alive = true;
        }


        public void UpdateAnimation(float animtime)
        {

            if (m_animationtimer > animtime)
            {
                m_animationtimer = 0;
                m_currentframe += 1;

                if (m_currentframe > 7)
                {
                    m_currentframe = 0;
                    m_alive = false;
                }
            }
        }


        public override void Update(GameTime gameTime)
        {
            if(m_alive)
            {
                m_animationtimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                UpdateAnimation(.07f);
            }
        
        }

        public bool isAlive() { return m_alive; }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (m_alive)
            {
                //This is the texture being drawn  
                spritebatch.Draw(m_texture, new Rectangle((int)m_position.X-64, (int)m_position.Y-64, 128, 128), new Rectangle(32 * m_currentframe, 0, 32, 32), Color.White);
            }
        }

    }
}
