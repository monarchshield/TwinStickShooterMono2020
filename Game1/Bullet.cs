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
     
        public bool m_alive;
       // public bool m_enemycollided; //Used for tracking player points
        float deltatime;

        private float m_killtimer;
       
        private float m_animationtimer;
        private int m_currentframe;

        private float m_rotation;

        
        private Rectangle animationRectangle;


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

        public Bullet(Texture2D texture, Vector2 Position, Vector2 Direction,float playerrot)
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
           
            m_currentframe = 1;
            m_animationtimer = 0;

            m_rotation = playerrot;
            


        }

        public void Update(GameTime _deltatime, List<Enemy> EnemyList)
        {


            deltatime = (float)_deltatime.ElapsedGameTime.TotalSeconds;

            m_animationtimer += deltatime;





            /* This update code will handle collision */
            if (m_alive)
            {
                
                UpdateAnimation(.25f);
            


                 m_killtimer -= deltatime;
                m_currentposition -= m_direction * m_speed * deltatime;
                if(m_killtimer <= 0)
                {
                    m_alive = false;
                }

            }
            CheckDeath();
        }


        public void UpdateAnimation(float animtime)
        {
            if (m_animationtimer > animtime)
            {
                m_animationtimer = 0;
                m_currentframe += 1;

                if (m_currentframe > 2)
                {
                    m_currentframe = 0;
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
                
                if(Lengthproduct < 64)
                {
                    badguy.m_alive = false;
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
                spriteBatch.Draw(m_texture, new Rectangle((int)m_currentposition.X, (int)m_currentposition.Y, 128, m_texture.Height), new Rectangle(128 * m_currentframe, 0, 128, 128), Color.White, m_rotation + 90, new Vector2(64,64), SpriteEffects.None, 0);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Matrix CamMatrix)
        {
            if (m_alive)
            {
                //This is the texture being drawn 
                spriteBatch.Draw(m_texture, new Rectangle((int)m_currentposition.X, (int)m_currentposition.Y, m_texture.Width, m_texture.Height), new Rectangle(0,0,128,128),Color.White,m_rotation,Vector2.Zero, SpriteEffects.None, 0);
            }
        }


        public void DrawInfo(SpriteBatch spritebatch, SpriteFont spritefont)
        {
            spritebatch.DrawString(spritefont, m_rotation.ToString(), new Vector2(m_currentposition.X, m_currentposition.Y - 50), Color.White);

            spritebatch.DrawString(spritefont, m_currentposition.ToString(), new Vector2(m_currentposition.X, m_currentposition.Y - 30), Color.White);
        }
    }
}
