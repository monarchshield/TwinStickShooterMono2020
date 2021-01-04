using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ShootyGame
{
    class VirtualJoystick
    {
        //Testing commit comments
        protected Texture2D m_joystickradial; //This is the underlying texture for the joystick
        protected Texture2D m_joystickthumbnail; //The thumbnail that will be used to control the joystick
        
        protected Vector2 m_position; //The position of the joystick itself on screen
        protected Vector2 m_joystickposition; //The position of the joystick thumbnail itself
        protected Vector2 m_joystickcenter; //Used for the hard reset area 
        protected Vector2 m_joystickdirection; //Used for retrieving the direction of the Joystick

        protected MouseState m_mousestate; //The mouse state for desktop testing purposes.
        //protected float m_joystickdistance; //Joystick distance from the radial.

        protected bool isHovering; //Is mousepoint or touchpoint hovering over cursor?
        protected bool isDragging; //Is the element being dragged
        protected Color m_color; //Used for debugging to see if the elements are being triggered as expected.


        public VirtualJoystick() { /*Default constructor */}

        public VirtualJoystick(Texture2D joystickradial, Texture2D joystickthumb, Vector2 pos)
        {
            m_joystickradial = joystickradial;
            m_joystickthumbnail = joystickthumb;
            m_position = pos;

            //m_joystickposition = new Vector2(pos.X + joystickradial.Width / 2, pos.Y + joystickradial.Height / 2); //This is the initial default position so half by half.
            m_joystickcenter = new Vector2(pos.X - m_joystickthumbnail.Width / 2, pos.Y - m_joystickthumbnail.Height / 2);
            m_joystickposition = m_joystickcenter;


            m_color = Color.White;
        }


        public void Update()
        {
            IsColliding();
            MoveThumbnail();
            ClampThumbStickLength();

            m_joystickdirection = m_joystickposition - m_joystickcenter;
            
            if(m_joystickdirection.Length() > 0)
            {
                m_joystickdirection.Normalize();
            }
            

            
        }


        public bool IsColliding()
        {
            m_mousestate = Mouse.GetState();

            Vector2 mousepositionpoint = new Vector2(m_mousestate.Position.X, m_mousestate.Position.Y);
            float LengthProduct = (mousepositionpoint - m_position).Length();


            //Change the joystick radial length product radius later.
            if (LengthProduct < m_joystickradial.Width)
            {
                m_color = Color.Red;
                isHovering = true;

                return isHovering;
                
            }

            m_color = Color.White;
            isHovering = false;
            return isHovering;
        }


        public void MoveThumbnail()
        {

            m_joystickposition = new Vector2(m_position.X + m_joystickradial.Width / 2, m_position.Y + m_joystickradial.Height / 2);
            if (m_mousestate.LeftButton == ButtonState.Released) { isDragging = false; }
            if (m_mousestate.LeftButton == ButtonState.Pressed && isHovering || isDragging)
            {
                
                m_joystickposition = new Vector2(m_mousestate.Position.X, m_mousestate.Position.Y);
                
                isDragging = true;
            }

            
            else
            {
                //Hard Reset the location
                m_joystickposition = m_joystickcenter;
            }
        }


        public void ClampThumbStickLength()
        {
            float LengthProduct = (m_joystickposition - m_joystickcenter).Length();

            if (LengthProduct > m_joystickthumbnail.Width)
            {
                Vector2 NewPosition = m_joystickposition - m_joystickcenter;
                NewPosition *= m_joystickthumbnail.Width / LengthProduct;
                m_joystickposition = m_joystickcenter + NewPosition;
            }
        }


        public Vector2 GetJoystickDirection() { return m_joystickdirection; }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(m_joystickradial, new Rectangle((int)m_position.X - m_joystickradial.Width/2, (int)m_position.Y - m_joystickradial.Height/2, m_joystickradial.Width, m_joystickradial.Height), m_color);
            spritebatch.Draw(m_joystickthumbnail, new Rectangle((int)m_joystickposition.X, (int)m_joystickposition.Y, m_joystickthumbnail.Width, m_joystickthumbnail.Height), Color.White);

        }

    }
}
