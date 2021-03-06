﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShootyGame
{
    public class MainMenuState : GameState
    {
        

       

        GraphicsDevice m_graphics;
        Color m_color;


        private RainbowLerpObject m_titlescreen;

        private MenuButton m_PlayButton;
        private MenuButton m_HighscoreButton;
        private MenuButton m_CreditButton;

        private MenuButton m_ExitButton;

        private Texture2D m_PlayBtnSprite;
        private Texture2D m_HighscoreBtnSprite;
        private Texture2D m_CreditBtnSprite;
        private Texture2D m_ExitBtnSprite;
        private Texture2D m_titleBannerSprite;

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
            m_ExitButton = new MenuButton(m_ExitBtnSprite, new Vector2(325, 400), MenuButton.MenuNav.EXIT, m_graphics);
           }

        public override void LoadContent(ContentManager content)
        {

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

            m_titlescreen.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            m_PlayButton.Update();
            m_HighscoreButton.Update();
         
            m_CreditButton.Update();
            m_ExitButton.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
           
            spriteBatch.Begin();
            // Draw sprites here

            m_titlescreen.Draw(spriteBatch);
            m_PlayButton.Draw(spriteBatch);
            m_HighscoreButton.Draw(spriteBatch);
          
            m_CreditButton.Draw(spriteBatch);
            m_ExitButton.Draw(spriteBatch);

            spriteBatch.End();

        }
    }
}
