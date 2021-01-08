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

        private RainbowLerpObject m_creditcolourlerp;
        private Texture2D m_credittexture;

        MenuButton m_backButton;
        Texture2D m_backbtnsprite;
        SpriteFont font;

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
            m_creditcolourlerp = new RainbowLerpObject(m_credittexture, new Vector2(235, 200));


        }

        public override void LoadContent(ContentManager content)
        {
            m_backbtnsprite = content.Load<Texture2D>("BackButton");
            m_credittexture = content.Load<Texture2D>("Creditpage");
            font = content.Load<SpriteFont>("DefaultFont");
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            
            m_backButton.Update();
            m_creditcolourlerp.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(m_color);
            spriteBatch.Begin(transformMatrix : Game1.m_scaleMatrix);
            // Draw sprites here
            m_backButton.Draw(spriteBatch);

            m_creditcolourlerp.Draw(spriteBatch);
            //spriteBatch.DrawString(font, "Created by Anthony Bogli", new Vector2(200, 200), Color.White);
            //spriteBatch.DrawString(font, "Github: https://github.com/monarchshield", new Vector2(200, 220), Color.White);
            //spriteBatch.DrawString(font, "Website: https://anthonybogli.com", new Vector2(200, 240), Color.White);


            spriteBatch.End();

        }
    }
}
