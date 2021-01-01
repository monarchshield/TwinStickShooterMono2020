using System;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ShootyGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
           
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            _graphics.PreferredBackBufferWidth = 800;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 480;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            


            ///<Summary>
            ///To add more content to the game you just type mgcb-editor in command prompt * *
            ///</Summary>
            GameStateManager.Instance.SetContent(Content);


            GameStateManager.Instance.AddScreen(new SubmitScoreState(GraphicsDevice,2000));
            //GameStateManager.Instance.AddScreen(new ShootyGameState(GraphicsDevice));




        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            GameStateManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameStateManager.Instance.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}
