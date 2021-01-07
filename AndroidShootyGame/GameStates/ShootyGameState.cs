using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AndroidShootyGame
{
    public class ShootyGameState : GameState
    {

        float m_deltatime;
        //  float m_cooldown;

        private List<Enemy> m_enemies;
        private List<Pawn> m_totalobjects; //For our quadtree
        private List<Pawn> ReturnObjects;

        private List<DeathParticle> m_deathparticles;

        //Quadtree attributes
        private QuadTree quadtree;
        //private int quadtreecollision;
        private List<QuadTree> QuadList;

        VirtualJoystick JoystickLeft;
        VirtualJoystick JoystickRight;

        private Player m_player;
        private Camera m_camera;


        
        public Texture2D m_bordertexture;
        private Border m_border;
        private List<ParallaxBackground> m_parallaxBackgrounds;

        public Texture2D m_parallaxtexture;
        public Texture2D m_parallaxtexture1;
        public Texture2D m_parallaxtexture2;
        public Texture2D m_parallaxtexture3;


        public Texture2D m_enemychasertexture;
        public Texture2D m_deathparticletexture;
        public Texture2D m_enemytexture;
        public Texture2D m_playertexture;
        public Texture2D m_bulletTexture;
        public Texture2D m_cursorTexture;
        public Texture2D m_lifeTexture;
        Texture2D Joystickradial;
        Texture2D Joystickthumbnail;
        public SpriteFont font;

        public float Distance;
        public Viewport GetViewport;

        private GraphicsDevice _graphics;
        private SpriteBatch _spriteBatch;


        private List<EnemySpawner> m_enemyspawners;



        public ShootyGameState(GraphicsDevice graphicsDevice)
        : base(graphicsDevice)
        {

            GetViewport = graphicsDevice.Viewport;
            _graphics = graphicsDevice;
        }
        public override void Initialize()
        {

            JoystickLeft = new VirtualJoystick(Joystickradial, Joystickthumbnail, new Vector2(100, 400));
            JoystickRight = new VirtualJoystick(Joystickradial, Joystickthumbnail, new Vector2(750, 400));
          


            Distance = 0;
            m_deltatime = 0;
            quadtree = new QuadTree(0, new Rectangle(0, 0, 3508, 2280));
            QuadList = new List<QuadTree>();

            m_enemies = new List<Enemy>();
            m_totalobjects = new List<Pawn>();
            ReturnObjects = new List<Pawn>();

            m_deathparticles = new List<DeathParticle>();
          

            m_player = new Player(m_playertexture, m_bulletTexture, m_cursorTexture,m_lifeTexture, new Vector2(1445, 982), 2);
            m_camera = new Camera(GetViewport, m_player);
            m_border = new Border(m_bordertexture, m_player);

            m_parallaxBackgrounds = new List<ParallaxBackground>();
            m_parallaxBackgrounds.Add(new ParallaxBackground(m_parallaxtexture1, new Vector2(100, 100), .50f, m_player));
            m_parallaxBackgrounds.Add(new ParallaxBackground(m_parallaxtexture2, new Vector2(200, 200), .50f, m_player));
            m_parallaxBackgrounds.Add(new ParallaxBackground(m_parallaxtexture3, new Vector2(400, 400), .50f, m_player));

            m_player.SetFont(font);

            m_enemyspawners = new List<EnemySpawner>();

            m_enemyspawners.Add(new EnemySpawner(m_enemytexture, m_enemychasertexture, new Vector2(-5, -35), .80f, m_player));
            m_enemyspawners.Add(new EnemySpawner(m_enemytexture, m_enemychasertexture, new Vector2(3550, -35), .80f, m_player));
            m_enemyspawners.Add(new EnemySpawner(m_enemytexture, m_enemychasertexture, new Vector2(3550, 2500), .80f, m_player));
            m_enemyspawners.Add(new EnemySpawner(m_enemytexture, m_enemychasertexture, new Vector2(-5, 2500), .80f, m_player));

            foreach(EnemySpawner enemySpawner in m_enemyspawners)
            {
                enemySpawner.SetCameraMatrix(m_camera.Transform);
            }

          





        }

        public override void LoadContent(ContentManager Content)
        {
            m_parallaxtexture = Content.Load<Texture2D>("ParallaxBackground");
            m_parallaxtexture1 = Content.Load<Texture2D>("Parallax1");
            m_parallaxtexture2 = Content.Load<Texture2D>("Parallax2");
            m_parallaxtexture3 = Content.Load<Texture2D>("Parallax3");

            Joystickradial = Content.Load<Texture2D>("JoystickRadial");
            Joystickthumbnail = Content.Load<Texture2D>("JoystickThumb");

            font = Content.Load<SpriteFont>("DefaultFont");
            m_enemytexture = Content.Load<Texture2D>("Meteor");
            m_bulletTexture = Content.Load<Texture2D>("BulletAnimated");
            m_playertexture = Content.Load<Texture2D>("Killme");
            m_cursorTexture = Content.Load<Texture2D>("Cursorposition");
            m_lifeTexture = Content.Load<Texture2D>("Life");
            m_enemychasertexture = Content.Load<Texture2D>("ChaserEnemy");
            
            m_bordertexture = Content.Load<Texture2D>("Border");
            m_deathparticletexture = Content.Load<Texture2D>("Explosion");
     
        }

        public override void UnloadContent()
        {
            m_enemytexture.Dispose();
            m_bulletTexture.Dispose();
            m_playertexture.Dispose();
            m_cursorTexture.Dispose();
    
            m_bordertexture.Dispose();
        }

        public override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here

            JoystickLeft.Update();
            JoystickRight.Update();

            m_player.Update(gameTime,JoystickLeft.GetJoystickDirection(),JoystickRight.GetJoystickDirection());
            m_camera.UpdateCamera(GetViewport);

            //JoystickLeft.Update();
            //JoystickRight.Update();


            if (m_player.GetPlayerLifes() < 0)
            {
              GameStateManager.Instance.ChangeScreen(new SubmitScoreState(_graphics, m_player.GetPlayerScore()));
            }

            foreach (ParallaxBackground background in m_parallaxBackgrounds)
            {
                background.Update(gameTime, GetViewport, JoystickLeft.GetJoystickDirection());
            }
          



            for (int i = 0; i < m_deathparticles.Count; i++)
            {
                m_deathparticles[i].Update(gameTime);

                if (!m_deathparticles[i].isAlive()) { m_deathparticles.RemoveAt(i); }
            }


            
            foreach (EnemySpawner enemyspawner in m_enemyspawners)
            {
                enemyspawner.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            /* I dont know if this is really terrible but it works for now */
            m_totalobjects.Clear();

            m_totalobjects.Add(m_player);


            foreach (EnemySpawner enemyspawner in m_enemyspawners)
            {
                m_totalobjects.AddRange(enemyspawner.GetEnemies());
            }


            m_totalobjects.AddRange(m_player.m_bullets);

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

                        if (m_totalobjects[i].GetobjectTag().Equals("bullet"))
                        {
                            if(i==x) { break; }
                            Distance = (m_totalobjects[i].GetPosition() - ReturnObjects[x].GetPosition()).Length();

                           
                            if (Distance < 64 &&  string.Equals(ReturnObjects[x].GetobjectTag(), "enemy"))
                            {

                                m_deathparticles.Add(new DeathParticle(m_deathparticletexture, ReturnObjects[x].GetPosition()));
                                m_totalobjects[i].SetCollision(true);
                                ReturnObjects[x].SetCollision(true);
                            }

                        }

                        else if (string.Equals(m_totalobjects[i].GetobjectTag(), "player"))
                        {
                            Distance = (m_totalobjects[i].GetPosition() - ReturnObjects[x].GetPosition()).Length();
                            if (Distance < 64 && string.Equals(ReturnObjects[x].GetobjectTag(), "enemy"))
                            {
                                m_deathparticles.Add(new DeathParticle(m_deathparticletexture, ReturnObjects[x].GetPosition()));
                                m_player.LoseLife();
                                ReturnObjects[x].SetCollision(true);
                               
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

                        Distance = (m_totalobjects[i].GetPosition() - m_totalobjects[j].GetPosition()).Length();

                        if (m_totalobjects[i].GetobjectTag().Equals("bullet"))
                        {
                            if (i == j) { break; }
                            Distance = (m_totalobjects[i].GetPosition() - ReturnObjects[j].GetPosition()).Length();


                            if (Distance < 64 && string.Equals(ReturnObjects[j].GetobjectTag(), "enemy"))
                            {

                                m_deathparticles.Add(new DeathParticle(m_deathparticletexture, ReturnObjects[j].GetPosition()));
                                m_totalobjects[i].SetCollision(true);
                                ReturnObjects[j].SetCollision(true);
                            }

                        }

                        else if (string.Equals(m_totalobjects[i].GetobjectTag(), "player"))
                        {
                            Distance = (m_totalobjects[i].GetPosition() - ReturnObjects[j].GetPosition()).Length();
                            if (Distance < 64 && string.Equals(ReturnObjects[j].GetobjectTag(), "enemy"))
                            {
                                m_deathparticles.Add(new DeathParticle(m_deathparticletexture, ReturnObjects[j].GetPosition()));
                                m_player.LoseLife();
                                ReturnObjects[j].SetCollision(true);

                            }
                        }
                    }
                }
                #endregion

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(new Color(01,14,35));


           

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, transformMatrix: Game1.m_scaleMatrix);
            foreach (ParallaxBackground background in m_parallaxBackgrounds)
            {
                background.Draw(spriteBatch);
            }
            spriteBatch.End();



            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, m_camera.Transform);

            m_border.Draw(spriteBatch);

            spriteBatch.End();



            quadtree.Childrenof(quadtree, QuadList);


            /*
                foreach (QuadTree kid in QuadList)
                {
                    if (kid != null)
                    {
                        kid.Draw(spriteBatch, m_camera.Transform);
                    }
                }
            */

            #region DebugInfo
            
            spriteBatch.Begin();
           /* spriteBatch.DrawString(font, "Bullet dir normalised:" + m_player.m_bulletdirection.ToString(), new Vector2(0, 40), Color.White);
            spriteBatch.DrawString(font, "Player position:" + m_player.GetPosition().ToString(), new Vector2(0, 60), Color.White);
            spriteBatch.DrawString(font, "Bullets currently:" + m_player.GetBullets().Count.ToString(), new Vector2(0, 80), Color.White);
           */

            spriteBatch.DrawString(font, "Player score: " + m_player.GetPlayerScore().ToString(), new Vector2(10, 50), Color.White);
           
            /*spriteBatch.DrawString(font, "Pawns:" + m_totalobjects.Count.ToString(), new Vector2(0, 120), Color.White);
            spriteBatch.DrawString(font, "Player rotation:" + m_player.m_playerrotation.ToString(), new Vector2(0, 140), Color.White);
            spriteBatch.DrawString(font, "Player Direction:" + m_player.GetPlayerDirection().ToString(), new Vector2(0, 160), Color.White);
            spriteBatch.DrawString(font, "Zoom amount:" + m_camera.Zoom.ToString(), new Vector2(0, 180), Color.White); */
            spriteBatch.End();
            
            #endregion

            m_player.Draw(spriteBatch, m_camera.Transform);



            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, m_camera.Transform);

            foreach (EnemySpawner enemyspawner in m_enemyspawners)
            {
                enemyspawner.Draw(spriteBatch);
               
                //For debug purposes
                //enemyspawner.DrawInfo(spriteBatch, font);
                

            }

            foreach (DeathParticle deathparticle in m_deathparticles)
            {
                deathparticle.Draw(spriteBatch);
            }



            spriteBatch.End();

            spriteBatch.Begin(transformMatrix: Game1.m_scaleMatrix);
                JoystickLeft.Draw(spriteBatch);
                JoystickRight.Draw(spriteBatch);
            spriteBatch.End();


            // quadtree.Draw(spriteBatch, m_camera.Transform);

        }
    }
}
