using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShootyGame
{
    public class SubmitScoreState : GameState
    {
        float timepassed;

        Texture2D m_upbuttonTexture;
        Texture2D m_downbuttonTexture;
        SpriteFont m_spritefont;

        private List<EnterNameInputButton> m_characterinputs;

        GraphicsDevice m_graphics;
        Color m_color;
        string characternamestring;


        public SubmitScoreState(GraphicsDevice graphicsDevice)
        : base(graphicsDevice)
        {
            m_graphics = graphicsDevice;
        }
        public override void Initialize()
        {
            m_color = Color.Black;
            characternamestring = "";

            m_characterinputs = new List<EnterNameInputButton>();
            m_characterinputs.Add(new EnterNameInputButton(m_upbuttonTexture, m_downbuttonTexture, new Vector2(120, 100), m_spritefont));
            m_characterinputs.Add(new EnterNameInputButton(m_upbuttonTexture, m_downbuttonTexture, new Vector2(180, 100), m_spritefont));
            m_characterinputs.Add(new EnterNameInputButton(m_upbuttonTexture, m_downbuttonTexture, new Vector2(240, 100), m_spritefont));

        }

        public override void LoadContent(ContentManager content)
        {
            m_upbuttonTexture = content.Load<Texture2D>("SelectKey");
            m_downbuttonTexture = content.Load<Texture2D>("SelectKeyDown");
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
            spriteBatch.Begin();

            characternamestring = "";

            foreach (EnterNameInputButton obj in m_characterinputs)
            {
                obj.Draw(spriteBatch);
                characternamestring += obj.GetInputCharacter();
               
            }

            spriteBatch.DrawString(m_spritefont, "Player Name:" + characternamestring.ToString(), new Vector2(0, 0), Color.White);


            spriteBatch.End();

        }
    }
}
