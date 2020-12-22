﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShootyGame
{
    public class HighscoreState : GameState
    {
        float timepassed;

        GraphicsDevice m_graphics;
        Color m_color;

        MenuButton m_backButton;
        Texture2D m_backbtnsprite;
        HighscoreFile m_highscorefile;


        public HighscoreState(GraphicsDevice graphicsDevice)
        : base(graphicsDevice)
        {
            m_graphics = graphicsDevice;
        }
        public override void Initialize()
        {
            m_color = Color.Black;
            m_backButton = new MenuButton(m_backbtnsprite, new Vector2(50, 300), MenuButton.MenuNav.MAINMENU, m_graphics);
            m_highscorefile = new HighscoreFile();


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
