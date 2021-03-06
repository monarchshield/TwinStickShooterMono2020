﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace AndroidShootyGame
{
    class Player : Pawn
    {

       
        private Vector2 m_velocity; //the players movement direction
       
        private Vector2 m_playerdirection; //This is for where the player is facing
        public float m_playerrotation; //This is the rotation the player is facing towards.
        
        private float m_shootcooldown; //After the cooldown has gone of reset to this value
        private float m_currentshootcooldown; //This stops the bullets from being spammed 
        
        private float m_immunityframes;
        private float m_immunity;


        private Texture2D m_spaceshipTexture; //The texture of the ship
        private Texture2D m_bulletTexture; //The bullet texture
        private Texture2D m_cursorpointer; //The texture for where the cursor is
        private Texture2D m_playerlifeUI;


        private Matrix m_CameraMatrix; //For storing the camera matrix


        private int m_playerscore;

        private KeyboardState keyboardstate; //The state of the keyboard
        private MouseState mousestate;

        private List<Enemy> m_enemies;

        public List<Bullet> m_bullets; //The bullets the player has shot
        public Vector2 m_bulletdirection;

        private Vector2 m_mouseposition; //The current mouse position
        private Color m_color; //Colour of whatever
        public int m_lives; //The lives a player has 

        private SpriteFont m_debugfont; //for debug purposes


        public Player() { }
        public Player(Texture2D playertexture, Texture2D bulletTexture, Texture2D Cursortexture, Texture2D lifetexture, Vector2 position, int lives)
        {
            m_spaceshipTexture = playertexture;
            m_bulletTexture = bulletTexture;
            m_cursorpointer = Cursortexture;
            m_playerlifeUI = lifetexture;

            m_currentposition = position;
            m_mouseposition = Vector2.Zero;
            m_bullets = new List<Bullet>();
            m_lives = lives;
            m_speed = 250;

            m_shootcooldown = .25f;
            m_currentshootcooldown = 0f;

            m_CameraMatrix = new Matrix();
            SetObjectTag("player");

            m_immunityframes = 3.0f;
            m_immunity = 0;

        }

        public void SetMoveDirection(Vector2 Direction) 
        {
            m_velocity = Direction;
        }


        public void SetFont(SpriteFont font)
        {
            m_debugfont = font;
        }

        public void AssignEnemyArray(List<Enemy> EnemyList)
        {
            m_enemies = EnemyList;
        }

        public void LoseLife()
        {
            if(m_immunity < 0)
            {
                m_lives -= 1;
                m_immunity = m_immunityframes;
            }
        }


        public void Update(GameTime gametime, Vector2 MovementDirection, Vector2 ShootDirection)
        {
            Shoot((float)gametime.ElapsedGameTime.TotalSeconds,ShootDirection);
            Move((float)gametime.ElapsedGameTime.TotalSeconds,MovementDirection);


            if(!ShootDirection.Equals(Vector2.Zero))
            {
                //Vector2 Direction = m_currentposition + ShootDirection;
                m_playerrotation = (float)Math.Atan2(ShootDirection.Y, ShootDirection.X);
                m_playerrotation -= .45f;
            }
            

            


            m_immunity -= (float)gametime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < m_bullets.Count; i++)
            {
                if (m_bullets[i].m_alive)
                {
                    m_bullets[i].Update(gametime, m_enemies);
                }

                else
                {
                    if (!m_bullets[i].m_alive) { m_playerscore += 10; }
                    m_bullets.RemoveAt(i);
                }
            }
        }

        public void Update(GameTime gameTime)
        {

            Shoot((float)gameTime.ElapsedGameTime.TotalSeconds);
            Move((float)gameTime.ElapsedGameTime.TotalSeconds);


            m_immunity -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            keyboardstate = Keyboard.GetState();
            mousestate = Mouse.GetState();



            Vector2 Position = new Vector2(mousestate.Position.X, mousestate.Position.Y);
           
            Vector2 TruePosition = Vector2.Transform(Position, Matrix.Invert(m_CameraMatrix));



            m_playerdirection = new Vector2(TruePosition.X - m_currentposition.X, TruePosition.Y - m_currentposition.Y);
            m_playerrotation = (float)Math.Atan2(m_playerdirection.Y, m_playerdirection.X);
            m_playerrotation -= .45f;

            for (int i = 0; i < m_bullets.Count; i++)
            {
                if(m_bullets[i].m_alive)
                {
                    m_bullets[i].Update(gameTime, m_enemies);
                }
                
                else
                {
                    if(!m_bullets[i].m_alive) { m_playerscore += 10; }
                    m_bullets.RemoveAt(i);
                }
            }

            /*
            foreach (Bullet bulletobj in m_bullets)
            {
                bulletobj.Update(gameTime, m_enemies);
            }
            */



         
        }


       

        public void Draw(SpriteBatch spritebatch)
        {
            //spritebatch.Begin();

         
            //spritebatch.Draw(m_spaceshipTexture, new Rectangle((int)m_currentposition.X, (int)m_currentposition.Y, 50, 50), Color.White);
            foreach(Bullet bulletobj in m_bullets)
            {
                bulletobj.Draw(spritebatch);
            }


          

            spritebatch.Draw(m_spaceshipTexture, m_currentposition, null, Color.White, m_playerrotation , new Vector2(m_spaceshipTexture.Width / 2, m_spaceshipTexture.Height / 2), .5f, SpriteEffects.None, 0);
            spritebatch.End();

            spritebatch.Begin();
            spritebatch.Draw(m_cursorpointer, new Rectangle((int)mousestate.Position.X-25, (int)mousestate.Position.Y-25, 50, 50), Color.White);
            spritebatch.End();
        }

        public void Draw(SpriteBatch spritebatch, Matrix CameraMatrix)
        {

            m_CameraMatrix = CameraMatrix;
            spritebatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, CameraMatrix);
               
            foreach (Bullet bulletobj in m_bullets)
            {
                bulletobj.Draw(spritebatch);
                //bulletobj.DrawInfo(spritebatch, m_debugfont);
            }
            

         



            //spritebatch.Draw(m_spaceshipTexture, m_currentposition, null, Color.Black, 0, new Vector2(m_spaceshipTexture.Width / 2, m_spaceshipTexture.Height / 2), 5f, SpriteEffects.None, 0);
            spritebatch.Draw(m_spaceshipTexture, m_currentposition, null, Color.White, m_playerrotation + 90, new Vector2(m_spaceshipTexture.Width / 2, m_spaceshipTexture.Height / 2), .5f, SpriteEffects.None, 0);
            //spritebatch.Draw(m_cursorpointer, new Rectangle((int)TruePosition.X - 25, (int)TruePosition.Y - 25, 50, 50), Color.White);
            spritebatch.End();

            spritebatch.Begin();

            m_CameraMatrix = CameraMatrix;

            for (int i = 0; i < m_lives; i++)
            {
                spritebatch.Draw(m_playerlifeUI, new Rectangle(0 + i * 50, 0, m_playerlifeUI.Width, m_playerlifeUI.Height), Color.White);
            }

            //  spritebatch.Draw(m_cursorpointer, new Rectangle((int)mousestate.Position.X - 25, (int)mousestate.Position.Y - 25, 50, 50), Color.White);
            spritebatch.End();
        }

        public void Shoot(float gametime)
        {


            Vector2 Position = new Vector2(mousestate.Position.X, mousestate.Position.Y);
            Vector2 TruePosition = Vector2.Transform(Position, Matrix.Invert(m_CameraMatrix));

            m_bulletdirection = m_currentposition - TruePosition;
            m_bulletdirection.Normalize();

            if (mousestate.LeftButton == ButtonState.Pressed && m_currentshootcooldown <= 0)
            {
               

                m_bullets.Add(new Bullet(m_bulletTexture, m_currentposition, m_bulletdirection,m_playerrotation));
                m_currentshootcooldown = m_shootcooldown;
                

            }

            else
            {
                m_currentshootcooldown -= gametime;
            }


        }

        /// <summary>
        /// This uses the thumb nail controls to be able to Shoot etc
        /// </summary>
        /// <param name="gametime"></param>
        /// <param name="ShootDirection"></param>
        public void Shoot(float gametime, Vector2 ShootDirection)
        {
            

    
            if (!ShootDirection.Equals(Vector2.Zero) && m_currentshootcooldown <= 0)
            {
                m_bullets.Add(new Bullet(m_bulletTexture, m_currentposition, ShootDirection *-1, m_playerrotation));
                m_currentshootcooldown = m_shootcooldown;

            }

            else
            {
                m_currentshootcooldown -= gametime;
            }
        }


        /// <summary>
        /// This uses the thumb nail controls to be able to move the player etc
        /// </summary>
        /// <param name="gametime"></param>
        /// <param name="ShootDirection"></param>
        public void Move(float deltatime, Vector2 MoveDirection)
        {
            m_currentposition += MoveDirection * m_speed * deltatime;
        }

        public void Move(float deltatime)
        {

            m_velocity = Vector2.Zero;

            if (keyboardstate.IsKeyDown(Keys.W))
            {
                 m_velocity.Y -= 1;
            }

            if (keyboardstate.IsKeyDown(Keys.S))
            {
                 m_velocity.Y += 1;
            }

            if (keyboardstate.IsKeyDown(Keys.A))
            {
                 m_velocity.X -= 1;
            }

            if (keyboardstate.IsKeyDown(Keys.D))
            {
                 m_velocity.X += 1;
            }

            //Update player position
            m_currentposition += m_velocity * m_speed * deltatime ;

            //Halt position when not pressing any key
           
        }

        

       
        public List<Bullet> GetBullets() { return m_bullets; }
        public int GetPlayerScore() { return m_playerscore; }
        public Vector2 GetPlayerDirection() { return m_velocity; }

        public int GetPlayerLifes() { return m_lives; }
    }
}
