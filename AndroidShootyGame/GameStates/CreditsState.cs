using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace AndroidShootyGame
{
    public class CreditState : GameState
    {
        float timepassed;

        MenuButton m_backButton;
        Texture2D m_backbtnsprite;

        GraphicsDevice m_graphics;
        Color m_color;


        public CreditState(GraphicsDevice graphicsDevice)
        : base(graphicsDevice)
        {
            m_graphics = graphicsDevice;
        }
        public override void Initialize()
        {
            m_color = Color.Black;
            m_backButton = new MenuButton(m_backbtnsprite, new Vector2(0, 400), MenuButton.MenuNav.MAINMENU, m_graphics);


        }

        public override void LoadContent(ContentManager content)
        {
            m_backbtnsprite = content.Load<Texture2D>("BackButton");
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            
            m_backButton.Update();

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(m_color);
            spriteBatch.Begin();
            // Draw sprites here
            m_backButton.Draw(spriteBatch);
            spriteBatch.End();

        }
    }
}
