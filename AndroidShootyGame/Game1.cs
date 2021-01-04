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

        Texture2D Joystickradial;
        Texture2D Joystickthumbnail;
        SpriteFont font;

        TouchCollection touchcollection;


        Viewport viewport;
        VirtualJoystick JoystickTest;

        Matrix scaleMatrix;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.ApplyChanges();
         

            //_graphics.GraphicsDevice.Viewport.Width;
        }

        

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            LoadContent();

            viewport = _graphics.GraphicsDevice.Viewport;

            if(viewport.Width > viewport.Height)
            {
                scaleMatrix = Matrix.CreateScale(viewport.Width / 800, viewport.Height / 480, 1.0f);
            }
            else
            {
                scaleMatrix = Matrix.CreateScale(viewport.Width / 480, viewport.Height / 800, 1.0f);
            }
            JoystickTest = new VirtualJoystick(Joystickradial, Joystickthumbnail, new Vector2(100, 300));


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

          
            Joystickradial = Content.Load<Texture2D>("JoystickRadial");
            Joystickthumbnail = Content.Load<Texture2D>("JoystickThumb");
            font = Content.Load<SpriteFont>("DefaultFont");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            touchcollection = TouchPanel.GetState();

            // TODO: Add your update logic here
            JoystickTest.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: scaleMatrix);
            // Draw sprites here

            int touchpointdata = 0;
            foreach (TouchLocation tl in touchcollection)
            {
                

                _spriteBatch.DrawString(font, "Tapping point:" + tl.Position.ToString(), new Vector2(0, 0 + touchpointdata * 20), Color.White);
                touchpointdata++;


            }
            

            _spriteBatch.DrawString(font, "Tapping point:", new Vector2(0, 0), Color.White);
            JoystickTest.Draw(_spriteBatch);

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
