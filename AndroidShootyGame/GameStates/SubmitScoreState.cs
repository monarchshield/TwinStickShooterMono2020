using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AndroidShootyGame
{
    public class SubmitScoreState : GameState
    {
        float timepassed;

        Texture2D m_upbuttonTexture;
        Texture2D m_downbuttonTexture;
        Texture2D m_submitbuttonTexture;
        SpriteFont m_spritefont;

        private List<EnterNameInputButton> m_characterinputs;

        GraphicsDevice m_graphics;
        Color m_color;
        string characternamestring;
        int m_playerscore;
        Button m_submitscore;

        HighscoreFile SubmitHighscore;

        public SubmitScoreState(GraphicsDevice graphicsDevice, int Score)
        : base(graphicsDevice)
        {
            m_graphics = graphicsDevice;
            m_playerscore = Score;
        }
        public override void Initialize()
        {
            m_color = Color.Black;
            characternamestring = "";

            m_characterinputs = new List<EnterNameInputButton>();
            m_characterinputs.Add(new EnterNameInputButton(m_upbuttonTexture, m_downbuttonTexture, new Vector2(300, 200), m_spritefont));
            m_characterinputs.Add(new EnterNameInputButton(m_upbuttonTexture, m_downbuttonTexture, new Vector2(400, 200), m_spritefont));
            m_characterinputs.Add(new EnterNameInputButton(m_upbuttonTexture, m_downbuttonTexture, new Vector2(500, 200), m_spritefont));
            m_submitscore = new Button(m_submitbuttonTexture, new Vector2(260, 330));
            SubmitHighscore = new HighscoreFile();



        }

        public override void LoadContent(ContentManager content)
        {
            m_upbuttonTexture = content.Load<Texture2D>("SelectKey");
            m_downbuttonTexture = content.Load<Texture2D>("SelectKeyDown");
            m_submitbuttonTexture = content.Load<Texture2D>("SubmitScoreButton");
            m_spritefont = content.Load<SpriteFont>("DefaultFont");
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {

            timepassed += (float)gameTime.ElapsedGameTime.TotalSeconds;


            foreach (EnterNameInputButton obj in m_characterinputs)
            {
                obj.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            m_submitscore.Update();

            if (m_submitscore.IsColliding())
            {
                Console.WriteLine("Amazing");
                SubmitHighscore.WritetoFile(m_playerscore, characternamestring);
                GameStateManager.Instance.ChangeScreen(new HighscoreState(m_graphics));

            }

            /*
            if(timepassed > 3)
            {
                m_color = Color.SkyBlue;
                
            }


            if(timepassed > 6)
            {
                GameStateManager.Instance.ChangeScreen(new TestGameState(m_graphics));
            }
            */
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(m_color);
            spriteBatch.Begin(transformMatrix: Game1.m_scaleMatrix);

            characternamestring = "";

            foreach (EnterNameInputButton obj in m_characterinputs)
            {
                obj.Draw(spriteBatch);
                characternamestring += obj.GetInputCharacter();
               
            }

            m_submitscore.Draw(spriteBatch);

            spriteBatch.DrawString(m_spritefont, "Player Name:" + characternamestring.ToString(), new Vector2(300, 20), Color.White);
            spriteBatch.DrawString(m_spritefont, "Player Score:" + m_playerscore.ToString(), new Vector2(300, 40), Color.White);

            spriteBatch.End();

        }
    }
}
