using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ShootyGame
{
    class Bullet : Pawn
    {
       
       
        private Color m_color;
        
        private Vector2 m_direction;
        private float m_speed;
        public bool m_alive;
        public bool m_enemycollided; //Used for tracking player points
        float deltatime;

        private float m_killtimer;


        public Bullet()
        {
            /* Lmao im not doing anything here */
        }

        public Bullet(Vector2 Position, Vector2 Direction)
        {
            /* Testing constructor here */
            m_currentposition = Position;
            m_currentposition = Direction;
            SetObjectTag("bullet");

        }

        public Bullet(Texture2D texture, Vector2 Position, Vector2 Direction)
        {

            /* Primary overload constructor dont @ me */
            m_texture = texture;
            m_currentposition = Position;
            m_direction = Direction;
            m_alive = true;
            
            m_collision = false;
            m_killtimer = 5;
            m_speed = 300;
            SetObjectTag("bullet");


        }

        public void Update(GameTime _deltatime, List<Enemy> EnemyList)
        {


            deltatime = (float)_deltatime.ElapsedGameTime.TotalSeconds;

            /* This update code will handle collision */
            if (m_alive)
            {
                CheckCollision(EnemyList);
                

                m_killtimer -= deltatime;
                m_currentposition -= m_direction * m_speed * deltatime;
                if(m_killtimer <= 0)
                {
                    m_alive = false;
                }

            }
        }


        public void CheckDeath()
        {
            if(GetCollision())
            {
                m_alive = false;   
            }
        }

        public void CheckCollision(List<Enemy> EnemyList)
        {
            foreach(Enemy badguy in EnemyList)
            {
                float Lengthproduct = (badguy.GetPosition() - m_currentposition).Length();
                
                if(Lengthproduct < 25)
                {
                    badguy.isAlive = false;
                    m_alive = false;
                    m_collision = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (m_alive)
            {
                //This is the texture being drawn 
                spriteBatch.Draw(m_texture, new Rectangle((int)m_currentposition.X, (int)m_currentposition.Y, m_texture.Width, m_texture.Height), Color.Red);
            }
        }
    }
}
