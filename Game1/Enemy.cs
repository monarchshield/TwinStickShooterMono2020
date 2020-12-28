
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
      
       // Player m_playerobject; //Needs a direct reference to the player for collision purposes

        Color Colours;

        public bool Collision;
        public bool m_alive;

        protected float m_animationtimer;
        protected int m_currentframe;
        protected int m_animationframes;
        protected float m_speed;

        private Matrix m_CameraMatrix; //For storing the camera matrix
        Vector2 m_enemydirRotation;
        private float m_rotation;

        protected float m_dt;
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
            MakeRandom(m_headingPosition);


            m_animationframes = 2;
            m_rotation = (float)Math.Atan2(m_direction.Y, m_direction.X);
            m_rotation -= .20f;
            m_speed = 250;


        }

        public void SetCameraMatrix(Matrix cam)
        {
            m_CameraMatrix = cam;
        }

        public virtual void Update(float _deltaTime)
        {

            if (m_alive)
            {

                m_animationtimer += _deltaTime;
                CheckDeath();
                UpdateAnimation(.10f);

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

        public void UpdateAnimation(float animtime)
        {
            if (m_animationtimer > animtime)
            {
                m_animationtimer = 0;
                m_currentframe += 1;

                if (m_currentframe > m_animationframes)
                {
                    m_currentframe = 0;
                }


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
            if (m_alive)
            {
                spritebatch.Draw(m_texture, new Rectangle((int)m_currentposition.X, (int)m_currentposition.Y, 128, m_texture.Height), new Rectangle(128 * m_currentframe, 0, 128, 128), Color.White, m_rotation+90, new Vector2(64, 64), SpriteEffects.None, 0);

            }
        }

        public void DrawInfo(SpriteBatch spritebatch, SpriteFont spritefont)
        {
            spritebatch.DrawString(spritefont, m_currentposition.ToString(), new Vector2(m_currentposition.X, m_currentposition.Y - 30), Color.White);
            spritebatch.DrawString(spritefont, m_rotation.ToString(), new Vector2(m_currentposition.X, m_currentposition.Y - 50), Color.White);
        }

        public void Seek(Vector2 TargetPos)
        {
            Vector2 MaxVeloxity = new Vector2(5, 5);									//The Max Velocity Will always be Constant. 
            m_direction = new Vector2(TargetPos.X - m_currentposition.X, TargetPos.Y - m_currentposition.Y);
            m_direction.Normalize();



            m_rotation = (float)Math.Atan2(m_direction.Y,m_direction.X);
            m_rotation -= .20f;
            
          

            m_currentposition += m_direction * m_speed * m_dt;


            
        }

        Vector2 MakeRandom(Vector2 rand)
        {
            x = random.Next(330, 3225);
            y = random.Next(315, 2280);

            
            m_headingPosition = new Vector2(x, y);

            //m_rotation = (float)Math.Atan2(m_headingPosition.Y, m_headingPosition.X);
            //m_rotation -= .20f;


            return rand;
        }

      
       


    }
}
