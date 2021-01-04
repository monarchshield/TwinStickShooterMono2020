using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AndroidShootyGame
{
    class EnterNameInputButton
    {

        Button m_UpButton;
        Button m_DownButton;

        int m_CharacterIndex;
        char[] m_characterinput = { 'A', 'B', 'C', 'D', 'E', 'F','G','H','I','J','K','L',
        'M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};

        SpriteFont m_spritefont;
        Vector2 m_position;

        public float m_buttoncooldown;
        public float m_currentbuttoncooldown;

        public EnterNameInputButton() { /*Default constructor*/ }
        public EnterNameInputButton(Texture2D buttontextureup, Texture2D buttontexturedown, Vector2 Position, SpriteFont spritefont) 
        {
        
            m_position = Position;
            m_spritefont = spritefont;

            m_CharacterIndex = 0;

            m_UpButton = new Button(buttontextureup, new Vector2(Position.X, Position.Y - 50));
            m_DownButton = new Button(buttontexturedown, new Vector2(Position.X, Position.Y + 50));
            m_buttoncooldown = .15f;
            m_currentbuttoncooldown = 0f;

        }

        public void Update(float deltatime)
        {
            m_currentbuttoncooldown -= deltatime;
            m_UpButton.Update();
            m_DownButton.Update();

            if(m_UpButton.IsClicked() && m_currentbuttoncooldown < 0)
            {
                m_currentbuttoncooldown = m_buttoncooldown;
                m_CharacterIndex += 1;

                if(m_CharacterIndex > 25) { m_CharacterIndex = 0; }
            }

            if(m_DownButton.IsClicked() && m_currentbuttoncooldown < 0)
            {
                m_currentbuttoncooldown = m_buttoncooldown;
                m_CharacterIndex -= 1;

                if (m_CharacterIndex < 0) { m_CharacterIndex = 25; }
            }

        }
        
        public void Draw(SpriteBatch spritebatch)
        {
            m_UpButton.Draw(spritebatch);
            spritebatch.DrawString(m_spritefont, m_characterinput[m_CharacterIndex].ToString(), new Vector2(m_position.X+18, m_position.Y+10), Color.White);
            m_DownButton.Draw(spritebatch);

        }

        public Char GetInputCharacter() { return m_characterinput [m_CharacterIndex]; }

    }
}
