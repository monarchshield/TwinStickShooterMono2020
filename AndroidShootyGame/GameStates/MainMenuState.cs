using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace AndroidShootyGame
{
    public class MainMenuState : GameState
    {

        GraphicsDevice m_graphics;
        Color m_color;

        SpriteFont font;
        private RainbowLerpObject m_titlescreen;

        private MenuButton m_PlayButton;
        private MenuButton m_HighscoreButton;
        private MenuButton m_CreditButton;

        

        private Texture2D m_PlayBtnSprite;
        private Texture2D m_HighscoreBtnSprite;
        private Texture2D m_CreditBtnSprite;
        private Texture2D m_ExitBtnSprite;
        private Texture2D m_titleBannerSprite;
        TouchCollection touchcollection;

        public MainMenuState(GraphicsDevice graphicsDevice)
        : base(graphicsDevice)
        {
            
            m_graphics = graphicsDevice;
        }
        public override void Initialize()
        {
            m_color = Color.White;

            m_titlescreen = new RainbowLerpObject(m_titleBannerSprite, new Vector2(235, 40));
            m_PlayButton = new MenuButton(m_PlayBtnSprite, new Vector2(325, 180), MenuButton.MenuNav.PLAY,m_graphics);
            m_HighscoreButton = new MenuButton(m_HighscoreBtnSprite, new Vector2(325, 250), MenuButton.MenuNav.HIGHSCORE, m_graphics);
            m_CreditButton = new MenuButton(m_CreditBtnSprite, new Vector2(325, 325), MenuButton.MenuNav.CREDITS, m_graphics);
        
           }

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("DefaultFont");

            m_PlayBtnSprite = content.Load<Texture2D>("PlayButton");
            m_HighscoreBtnSprite = content.Load<Texture2D>("HighscoreButton");
            m_CreditBtnSprite = content.Load<Texture2D>("CreditButton");
            m_ExitBtnSprite = content.Load<Texture2D>("QuitButton");
            m_titleBannerSprite = content.Load<Texture2D>("Title");
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            TouchPanel.DisplayWidth = 800;
            TouchPanel.DisplayHeight = 480;
            touchcollection = TouchPanel.GetState();
            m_titlescreen.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            m_PlayButton.Update();
            m_HighscoreButton.Update();
         
            m_CreditButton.Update();
         
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
           
            spriteBatch.Begin(transformMatrix: Game1.m_scaleMatrix);
            // Draw sprites here

            /*
            for (int i = 0; i < touchcollection.Count; i++)
            {
                spriteBatch.DrawString(font, "Tapping point:" + touchcollection[i].Position.ToString(), new Vector2(0, 0 + i * 20), Color.White);

            }
            */
          


            m_titlescreen.Draw(spriteBatch);
            m_PlayButton.Draw(spriteBatch);
            m_HighscoreButton.Draw(spriteBatch);
          
            m_CreditButton.Draw(spriteBatch);
           

            spriteBatch.End();

        }
    }
}
