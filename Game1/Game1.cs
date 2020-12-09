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

        float m_deltatime;
        float m_cooldown;

        private List<Enemy> m_enemies;
        private List<Pawn> m_totalobjects; //For our quadtree
        private List<Pawn> ReturnObjects;
        //Quadtree attributes
        private QuadTree quadtree;
        private int quadtreecollision;
        private List<QuadTree> QuadList;
       


        private Player m_player; 
        
        public Texture2D m_enemytexture;
        public Texture2D m_playertexture;
        public Texture2D m_bulletTexture;
        public Texture2D m_cursorTexture;
        public SpriteFont font;

        public float Distance;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private EnemySpawner m_enemyspawner;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            LoadContent();

            _graphics.PreferredBackBufferWidth = 795;
            _graphics.PreferredBackBufferHeight = 495;
            _graphics.ApplyChanges();

            Distance = 0;
            quadtree = new QuadTree(0, new Rectangle(0, 0, 794, 490));
            QuadList = new List<QuadTree>();
           
            m_enemies = new List<Enemy>();
            m_totalobjects = new List<Pawn>();
            ReturnObjects = new List<Pawn>();


            m_player = new Player(m_playertexture, m_bulletTexture, m_cursorTexture, new Vector2(50, 50), 5);
            m_enemyspawner = new EnemySpawner(m_enemytexture, new Vector2(200, 200), 1.0f);
            m_enemyspawner.SpawnEnemy();

            m_player.AssignEnemyArray(m_enemyspawner.GetEnemies());


            
            

           

            /*
            for (int i = 0; i < 10; i++)
            {

                m_enemies.Add(new Enemy(m_enemytexture, new Vector2(400, 200)));
            }
            */

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            /* If you want to add more images and content to be loaded 
            
            1)  Open cmd and type the command: Mgcb-editor
            2)  Click file, open file then content.mgcb
            3) Right click content -> Add -> Existing item
            */

            font = Content.Load<SpriteFont>("DefaultFont");
            m_enemytexture = Content.Load<Texture2D>("Entitys");
            m_bulletTexture = Content.Load<Texture2D>("Bullet");
            m_playertexture = Content.Load<Texture2D>("Killme");
            m_cursorTexture = Content.Load<Texture2D>("Cursorposition");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            /* I dont know if this is really terrible but it works for now */
            m_totalobjects.Clear();
            m_totalobjects.AddRange(m_enemyspawner.GetEnemies());
            m_totalobjects.AddRange(m_player.GetBullets());


            quadtreecollision = 0;

            QuadList.Clear();
            quadtree.clear();

            for (int i = 0; i < m_totalobjects.Count; i++)
            {
                quadtree.insert(m_totalobjects[i]);
                //Go through all the objects and insert them into there proper quadform
            }

            for (int i = 0; i < m_totalobjects.Count; i++)
            {
                ReturnObjects.Clear();
                quadtree.retrieve(ReturnObjects, m_totalobjects[i]);

                for (int x = 0; x < ReturnObjects.Count; x++)
                {
                    for (int j = 0; j < ReturnObjects.Count; j++)
                    {
                        if (x == j)
                        {
                            break;
                        }

                        Distance = (ReturnObjects[x].GetPosition() - ReturnObjects[j].GetPosition()).Length();

                        if (Distance < 50 && ReturnObjects[x].GetobjectTag() != ReturnObjects[j].GetobjectTag())
                        {
                            ReturnObjects[x].SetCollision(true);
                            ReturnObjects[j].SetCollision(true);
                        }

                    }

                    //Run Collision etection algorithmn between objects here
                }

            }

            m_player.Update(gameTime);
            m_enemyspawner.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

           /*
            for (int i = 0; i < m_enemies.Count; i++)
            {
                if (m_enemies[i].isAlive)
                {
                    m_enemies[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                else
                {
                    m_enemies.RemoveAt(i);
                }
            }
           */


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            m_enemyspawner.Draw(_spriteBatch);
            m_player.Draw(_spriteBatch);

            quadtree.Childrenof(quadtree, QuadList);
           
            foreach (QuadTree kid in QuadList)
            {
                if (kid != null)
                {
                    kid.Draw(_spriteBatch);
                }
            }
            
      

            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, "Bullet dir normalised:" + m_player.m_bulletdirection.ToString(), new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(font, "Player position:" + m_player.m_currentposition.ToString(), new Vector2(0, 20), Color.White);
            _spriteBatch.DrawString(font, "Bullets currently:" + m_player.GetBullets().Count.ToString(), new Vector2(0, 40), Color.White);
            _spriteBatch.DrawString(font, "Enemies currently:" + m_enemyspawner.GetEnemies().Count.ToString(), new Vector2(0, 60), Color.White);
            _spriteBatch.DrawString(font, "Player score:" + m_player.GetPlayerScore().ToString(), new Vector2(0, 80), Color.White);
            _spriteBatch.DrawString(font, "Pawns:" + m_totalobjects.Count.ToString(), new Vector2(0, 100), Color.White);



            _spriteBatch.End();

            m_enemyspawner.Draw(_spriteBatch);
            m_player.Draw(_spriteBatch);

            foreach (QuadTree kid in QuadList)
            {
                if (kid != null)
                {
                    kid.Draw(_spriteBatch);
                }
            }

            quadtree.Draw(_spriteBatch);


            base.Draw(gameTime);
        }
    }
}
