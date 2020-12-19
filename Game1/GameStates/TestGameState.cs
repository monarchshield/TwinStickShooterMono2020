using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShootyGame
{
    public class TestGameState : GameState
    {
        float timepassed;

        GraphicsDevice m_graphics;
        Color m_color;


        public TestGameState(GraphicsDevice graphicsDevice)
        : base(graphicsDevice)
        {
            m_graphics = graphicsDevice;
        }
        public override void Initialize()
        {
            m_color = Color.Black;

        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {

            timepassed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(timepassed > 3)
            {
                m_color = Color.SkyBlue;
                
            }


            if(timepassed > 6)
            {
                GameStateManager.Instance.ChangeScreen(new TestGameState(m_graphics));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(m_color);
            spriteBatch.Begin();
            // Draw sprites here
            spriteBatch.End();

        }
    }
}
