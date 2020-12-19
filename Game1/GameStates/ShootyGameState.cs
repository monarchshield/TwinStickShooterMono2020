using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootyGame
{
    public class ShootyGameState : GameState
    {

        float m_deltatime;
        //  float m_cooldown;

        private List<Enemy> m_enemies;
        private List<Pawn> m_totalobjects; //For our quadtree
        private List<Pawn> ReturnObjects;
        //Quadtree attributes
        private QuadTree quadtree;
        //private int quadtreecollision;
        private List<QuadTree> QuadList;



        private Player m_player;
        private Camera m_camera;


        public Texture2D m_background;
        public Texture2D m_bordertexture;
        private Border m_border;

        public Texture2D m_enemytexture;
        public Texture2D m_playertexture;
        public Texture2D m_bulletTexture;
        public Texture2D m_cursorTexture;
        public SpriteFont font;

        public float Distance;
        public Viewport GetViewport;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private List<EnemySpawner> m_enemyspawners;



        public ShootyGameState(GraphicsDevice graphicsDevice)
        : base(graphicsDevice)
        {

            GetViewport = graphicsDevice.Viewport;
        }
        public override void Initialize()
        {
            // TODO: Add your initialization logic here
           // LoadContent();

            

            //_graphics.PreferredBackBufferWidth = 795;
            //_graphics.PreferredBackBufferHeight = 495;
            //_graphics.ApplyChanges();

            Distance = 0;
            m_deltatime = 0;
            quadtree = new QuadTree(0, new Rectangle(0, 0, 3508, 2280));
            QuadList = new List<QuadTree>();

            m_enemies = new List<Enemy>();
            m_totalobjects = new List<Pawn>();
            ReturnObjects = new List<Pawn>();

            

            m_player = new Player(m_playertexture, m_bulletTexture, m_cursorTexture, new Vector2(1445, 982), 5);
            m_camera = new Camera(GetViewport, m_player);
            m_border = new Border(m_bordertexture, m_player);


            m_enemyspawners = new List<EnemySpawner>();

            m_enemyspawners.Add(new EnemySpawner(m_enemytexture, new Vector2(-5, -35), 1.0f));
            m_enemyspawners.Add(new EnemySpawner(m_enemytexture, new Vector2(3550, -35), 1.0f));
            m_enemyspawners.Add(new EnemySpawner(m_enemytexture, new Vector2(3550, 2500), 1.0f));
            m_enemyspawners.Add(new EnemySpawner(m_enemytexture, new Vector2(-5, 2500), 1.0f));








        }

        public override void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("DefaultFont");
            m_enemytexture = Content.Load<Texture2D>("Entitys");
            m_bulletTexture = Content.Load<Texture2D>("Bullet");
            m_playertexture = Content.Load<Texture2D>("killme");
            m_cursorTexture = Content.Load<Texture2D>("Cursorposition");

            m_background = Content.Load<Texture2D>("Background");
            m_bordertexture = Content.Load<Texture2D>("Border2");

        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
           
            // TODO: Add your update logic here

            /* I dont know if this is really terrible but it works for now */
            m_totalobjects.Clear();


            foreach (EnemySpawner enemyspawner in m_enemyspawners)
            {
                m_totalobjects.AddRange(enemyspawner.GetEnemies());
            }


            m_totalobjects.AddRange(m_player.GetBullets());

            m_border.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            //This is just added to allow for numerical data collection




            #region ClearQuadtree to allow for updates
            QuadList.Clear();
            quadtree.clear();
            #endregion

            #region Assign Objects to Quadtree
            for (int i = 0; i < m_totalobjects.Count; i++)
            {
                quadtree.insert(m_totalobjects[i]);
                //Go through all the objects and insert them into there proper quadform
            }
            #endregion


            //If more then 5 objects instantiate
            #region Quadtree instantiation
            if (m_totalobjects.Count >= 5)
            {
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
            }
            #endregion

            //This is for if the quadtree hasnt be instantiated yet, 
            #region RawCollisionDetection
            else
            {
                for (int i = 0; i < m_totalobjects.Count; i++)
                {
                    for (int j = 0; j < m_totalobjects.Count; j++)
                    {
                        if (i == j)
                        {
                            break;
                        }

                        Distance = (m_totalobjects[i].GetPosition() - m_totalobjects[j].GetPosition()).Length();

                        if (Distance < 50 && m_totalobjects[i].GetobjectTag() != m_totalobjects[j].GetobjectTag())
                        {
                            m_totalobjects[i].SetCollision(true);
                            m_totalobjects[j].SetCollision(true);
                        }
                    }
                }
            }
            #endregion

            m_player.Update(gameTime);
            m_camera.UpdateCamera(GetViewport);



            foreach (EnemySpawner enemyspawner in m_enemyspawners)
            {
                enemyspawner.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, m_camera.Transform);

            spriteBatch.Draw(m_background, new Rectangle(0, 0, m_background.Width, m_background.Height), Color.White);
            m_border.Draw(spriteBatch);

            spriteBatch.End();


            //m_enemyspawner.Draw(_spriteBatch);
            // m_player.Draw(_spriteBatch);

            quadtree.Childrenof(quadtree, QuadList);


            /*
                foreach (QuadTree kid in QuadList)
                {
                    if (kid != null)
                    {
                        kid.Draw(_spriteBatch, m_camera.Transform);
                    }
                }
              */



            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Bullet dir normalised:" + m_player.m_bulletdirection.ToString(), new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, "Player position:" + m_player.m_currentposition.ToString(), new Vector2(0, 20), Color.White);
            spriteBatch.DrawString(font, "Bullets currently:" + m_player.GetBullets().Count.ToString(), new Vector2(0, 40), Color.White);

            spriteBatch.DrawString(font, "Player score:" + m_player.GetPlayerScore().ToString(), new Vector2(0, 80), Color.White);
            spriteBatch.DrawString(font, "Pawns:" + m_totalobjects.Count.ToString(), new Vector2(0, 100), Color.White);
            spriteBatch.End();

            m_player.Draw(spriteBatch, m_camera.Transform);



            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, m_camera.Transform);

            foreach (EnemySpawner enemyspawner in m_enemyspawners)
            {
                enemyspawner.Draw(spriteBatch);
            }



            spriteBatch.End();

            //m_enemyspawner.Draw(_spriteBatch);
            // m_player.Draw(_spriteBatch);

            /*
            foreach (QuadTree kid in QuadList)
            {
                if (kid != null)
                {
                    kid.Draw(_spriteBatch);
                }
            }
            */

            // quadtree.Draw(_spriteBatch, m_camera.Transform);

        }
    }
}
