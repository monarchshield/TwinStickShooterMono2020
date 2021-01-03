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

        Texture2D Joystickradial;
        Texture2D Joystickthumbnail;
        SpriteFont font;

        VirtualJoystick JoystickTest;

        public TestGameState(GraphicsDevice graphicsDevice)
        : base(graphicsDevice)
        {
            m_graphics = graphicsDevice;
        }
        public override void Initialize()
        {
            m_color = Color.Black;

            JoystickTest = new VirtualJoystick(Joystickradial, Joystickthumbnail, new Vector2(100, 300));

        }

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("DefaultFont");
            Joystickradial = content.Load<Texture2D>("JoystickRadial");
            Joystickthumbnail = content.Load<Texture2D>("JoystickThumb");

        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {

            timepassed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            JoystickTest.Update();
         
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(m_color);
            spriteBatch.Begin();
            // Draw sprites here
            spriteBatch.DrawString(font, "JoyStick Direction:" + JoystickTest.GetJoystickDirection().ToString(), new Vector2(0, 60), Color.White);

            JoystickTest.Draw(spriteBatch);

            spriteBatch.End();

        }
    }
}
