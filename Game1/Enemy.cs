
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootyGame
{
    class Enemy : Pawn
    {
        static Random random = new Random();

        Vector2 m_headingPosition = new Vector2(400, 200);
       
        Vector2 m_direction;
      
        Player m_playerobject; //Needs a direct reference to the player for collision purposes

        Color Colours;

        public bool Collision;
        public bool m_alive;


        float m_dt;
        float x;
        float y;


        public Enemy() { }

        public Enemy(Texture2D texture, Vector2 position)
        {
            m_texture = texture;
            m_currentposition = position;
            Collision = false;
            m_alive = true;
            SetObjectTag("enemy");
        }

        public void Update(float _deltaTime)
        {

            if (m_alive)
            {
                CheckDeath();



                float range = (m_currentposition - m_headingPosition).Length();
                m_dt = _deltaTime;

                if (range < 10)
                {
                    MakeRandom(m_headingPosition);
                }

                if (Collision == true)
                {
                    Colours = Color.HotPink;
                    // Colours = Color.Red;
                }

                if (Collision == false)
                {

                    Colours = Color.White;
                }

                Seek(m_headingPosition);
            }

        }


        public void CheckDeath()
        {
            if (GetCollision())
            {
                m_alive = false;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            if (m_alive)
            {
                spritebatch.Draw(m_texture, new Rectangle((int)m_currentposition.X, (int)m_currentposition.Y, m_texture.Width, m_texture.Height), Colours);
            }

            spritebatch.End();
        }

        public void Seek(Vector2 TargetPos)
        {
            Vector2 MaxVeloxity = new Vector2(5, 5);									//The Max Velocity Will always be Constant. 
            m_direction = new Vector2(TargetPos.X - m_currentposition.X, TargetPos.Y - m_currentposition.Y);
            m_direction.Normalize();
            m_currentposition += m_direction * 100 * m_dt;
        }

        Vector2 MakeRandom(Vector2 rand)
        {
            x = random.Next(0, 800);
            y = random.Next(0, 500);


            m_headingPosition = new Vector2(x, y);
            return rand;
        }

      
       


    }
}
