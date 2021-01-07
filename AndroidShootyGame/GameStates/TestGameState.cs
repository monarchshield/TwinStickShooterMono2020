using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace AndroidShootyGame
{
    public class TestGameState : GameState
    {
        float timepassed;

        GraphicsDevice m_graphics;
        Color m_color;
        Texture2D Joystickradial;
        Texture2D Joystickthumbnail;
        SpriteFont font;

        TouchCollection touchcollection;


        Viewport viewport;
        VirtualJoystick JoystickLeft;
        VirtualJoystick JoystickRight;
        Matrix scaleMatrix;

        public TestGameState(GraphicsDevice graphicsDevice)
        : base(graphicsDevice)
        {
            m_graphics = graphicsDevice;
            viewport = graphicsDevice.Viewport;
        }
        public override void Initialize()
        {
            m_color = Color.Black;


            scaleMatrix = Matrix.CreateScale(viewport.Width / 800, viewport.Height / 480, 1.0f);
            TouchPanel.DisplayWidth = 800;
            TouchPanel.DisplayHeight = 480;
            TouchPanel.EnableMouseTouchPoint = true;


            JoystickLeft = new VirtualJoystick(Joystickradial, Joystickthumbnail, new Vector2(100, 400));
            JoystickRight = new VirtualJoystick(Joystickradial, Joystickthumbnail, new Vector2(750, 400));

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

            touchcollection = TouchPanel.GetState();
            TouchPanel.DisplayWidth = 875;
            TouchPanel.DisplayHeight = 480;

            // TODO: Add your update logic here
            JoystickLeft.Update();
            JoystickRight.Update();

        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _graphicsDevice.Clear(m_color);
            _spriteBatch.Begin(transformMatrix: scaleMatrix);
            // Draw sprites here



            int touchpointdata = 0;
            foreach (TouchLocation tl in touchcollection)
            {

                //Matrix PointWorldLocation =  Matrix.CreateScale(tl.Position.X / 800, tl.Position.Y / 480, 1.0f);



                _spriteBatch.DrawString(font, "Tapping point:" + tl.Position.ToString(), new Vector2(0, 0 + touchpointdata * 20), Color.White);
                touchpointdata++;


            }

            _spriteBatch.DrawString(font, "Joystick Left:" + JoystickLeft.GetJoystickDirection().ToString(), new Vector2(0, 60), Color.White);
            _spriteBatch.DrawString(font, "Joystick Right:" + JoystickRight.GetJoystickDirection().ToString(), new Vector2(0, 80), Color.White);

            _spriteBatch.DrawString(font, "Tapping point:", new Vector2(0, 0), Color.White);
            JoystickLeft.Draw(_spriteBatch);
            JoystickRight.Draw(_spriteBatch);

            _spriteBatch.End();

        }
    }
}
