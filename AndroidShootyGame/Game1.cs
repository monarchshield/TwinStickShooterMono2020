using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;



namespace AndroidShootyGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static  Matrix m_scaleMatrix;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

        
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

          
            

            //_graphics.GraphicsDevice.Viewport.Width;
        }



        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           

            /*
            viewport = _graphics.GraphicsDevice.Viewport;

            
             scaleMatrix = Matrix.CreateScale(viewport.Width / 800, viewport.Height / 480, 1.0f);
            TouchPanel.DisplayWidth = 800;
            TouchPanel.DisplayHeight = 480;
            TouchPanel.EnableMouseTouchPoint = true;
            
            
            JoystickLeft = new VirtualJoystick(Joystickradial, Joystickthumbnail, new Vector2(100, 400));
            JoystickRight = new VirtualJoystick(Joystickradial, Joystickthumbnail, new Vector2(750, 400));
            */
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
           
            m_scaleMatrix = Matrix.CreateScale(GraphicsDevice.Viewport.Width / 800, GraphicsDevice.Viewport.Height / 480, 1.0f);

            GameStateManager.Instance.SetContent(Content);

            GameStateManager.Instance.AddScreen(new SubmitScoreState(GraphicsDevice,1500));
            //GameStateManager.Instance.AddScreen(new TestGameState(GraphicsDevice));


            /*
            Joystickradial = Content.Load<Texture2D>("JoystickRadial");
            Joystickthumbnail = Content.Load<Texture2D>("JoystickThumb");
            font = Content.Load<SpriteFont>("DefaultFont");
            TODO: use this.Content to load your game content here */
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            TouchPanel.DisplayWidth = 800;
            TouchPanel.DisplayHeight = 480;
            GameStateManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameStateManager.Instance.Draw(_spriteBatch);

            #region Test
            /*
            _spriteBatch.Begin(transformMatrix: scaleMatrix);
            // Draw sprites here


            
           
            int touchpointdata = 0;
            foreach (TouchLocation tl in touchcollection)
            {

               //Matrix PointWorldLocation =  Matrix.CreateScale(tl.Position.X / 800, tl.Position.Y / 480, 1.0f);
               

           
                _spriteBatch.DrawString(font, "Tapping point:" + tl.Position.ToString(), new Vector2(0, 0 + touchpointdata * 20), Color.White);
                touchpointdata++;


            }

            _spriteBatch.DrawString(font, "Joystick Left:" + JoystickLeft.GetJoystickDirection().ToString() , new Vector2(0, 60), Color.White);
            _spriteBatch.DrawString(font, "Joystick Right:" + JoystickRight.GetJoystickDirection().ToString(), new Vector2(0, 80), Color.White);

            _spriteBatch.DrawString(font, "Tapping point:", new Vector2(0, 0), Color.White);
            JoystickLeft.Draw(_spriteBatch);
            JoystickRight.Draw(_spriteBatch);

            _spriteBatch.End(); */
            #endregion
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
