using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

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
        SpriteFont m_spritefont;

        int m_HighscoreCount;
        float renderpositionx;


        public HighscoreState(GraphicsDevice graphicsDevice)
        : base(graphicsDevice)
        {
            m_graphics = graphicsDevice;
        }
        public override void Initialize()
        {

            renderpositionx = 50;
            m_color = Color.Black;
            m_backButton = new MenuButton(m_backbtnsprite, new Vector2(0, 400), MenuButton.MenuNav.MAINMENU, m_graphics);
            m_highscorefile = new HighscoreFile();
            m_highscorefile.SortHighScores();
           
            m_HighscoreCount = m_highscorefile.m_playerScores.Count();
            if(m_HighscoreCount > 10) { m_HighscoreCount = 10; }
           

        }

        public override void LoadContent(ContentManager content)
        {
            m_backbtnsprite = content.Load<Texture2D>("BackButton");
            m_spritefont = content.Load<SpriteFont>("DefaultFont");

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

            int textleft = 0;
            int textright = 0;

            for (int i = 0; i < m_HighscoreCount; i++)
            {

                if(i < 5)
                {
                    renderpositionx = 200;
                    spriteBatch.DrawString(m_spritefont, i.ToString() + ")" + m_highscorefile.m_playerScores[i].name + ":" + m_highscorefile.m_playerScores[i].scorevalue, new Vector2(renderpositionx, 60 + textleft * 30), Color.White);
                    textleft += 1;
                }

                else
                {
                    renderpositionx = 400;
                    spriteBatch.DrawString(m_spritefont, i.ToString() + ")" + m_highscorefile.m_playerScores[i].name + ":" + m_highscorefile.m_playerScores[i].scorevalue, new Vector2(renderpositionx, 60 + textright * 30), Color.White);
                    textright += 1;
                }
                

            }

           


            m_backButton.Draw(spriteBatch);
            spriteBatch.End();

        }
    }
}
