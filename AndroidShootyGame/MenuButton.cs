using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace AndroidShootyGame
{
    class MenuButton : Button
    {

        public enum MenuNav
        {
            MAINMENU,
            PLAY,
            RULES,
            CREDITS,
            HIGHSCORE,
            EXIT
        }

        protected MenuNav m_navigation;
        protected GraphicsDevice m_graphics;
        public MenuButton() { }

        public MenuButton(Texture2D texture, Vector2 Position)
        {
            m_texture = texture;
            m_position = Position;
            m_color = Color.White;
        }

        public MenuButton(Texture2D texture, Vector2 Position, MenuNav loc)
        {
            m_texture = texture;
            m_position = Position;
            m_navigation = loc;
            m_color = Color.White;
            CollisionPoint = new Point(10, 10);
            CollisionRect = new Rectangle((int)m_position.X, (int)m_position.Y, m_texture.Width, m_texture.Height);
        }

        public MenuButton(Texture2D texture, Vector2 Position, MenuNav loc, GraphicsDevice graphicsDevice)
        {
            m_texture = texture;
            m_position = Position;
            m_navigation = loc;
            m_color = Color.White;
            m_graphics = graphicsDevice;
            CollisionPoint = new Point(1, 1);
            CollisionRect = new Rectangle((int)m_position.X, (int)m_position.Y, m_texture.Width, m_texture.Height);
        }


        public override void Update()
        {
            if(IsColliding() && mousestate.LeftButton == ButtonState.Pressed)
            {
                

                switch (m_navigation)
                {
                    case MenuNav.MAINMENU:
                        GameStateManager.Instance.ChangeScreen(new MainMenuState(m_graphics));
                        break;

                    case MenuNav.PLAY:
                        GameStateManager.Instance.ChangeScreen(new ShootyGameState(m_graphics));
                        break;
                    case MenuNav.RULES:
                        GameStateManager.Instance.ChangeScreen(new TestGameState(m_graphics));
                        break;
                    case MenuNav.CREDITS:
                        GameStateManager.Instance.ChangeScreen(new CreditState(m_graphics));
                        break;
                    case MenuNav.HIGHSCORE:
                        GameStateManager.Instance.ChangeScreen(new HighscoreState(m_graphics));
                        break;
                    case MenuNav.EXIT:
                        
                        break;
                    default:
                        break;
                }
            }
        }

       

    }
}
