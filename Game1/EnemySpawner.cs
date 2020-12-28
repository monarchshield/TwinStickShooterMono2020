using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShootyGame
{
    class EnemySpawner
    {
        private Vector2 m_spawnerlocation; //Where is the spawner located on the map
        private Texture2D m_texture; //This is the texture used for enemies
        private Texture2D m_chasertexture;
        private float m_spawnfrequency; ///How often does the spawner create a entity 
        private float m_currentspawncooldown; //Current cooldown for a spawner
        private List<Enemy> m_enemylist; //List of enemies
        private int m_maxenemies; //This is used to control how many enemies can spawn
        private Matrix m_CameraMatrix;
        private Player m_player;


        /// <summary>
        /// Default constructor
        /// </summary>
        public EnemySpawner() { }

        /// <summary>
        /// //Overload with texture
        /// </summary>
        public EnemySpawner(Texture2D texture) 
        {
            m_enemylist = new List<Enemy>();
            m_spawnerlocation = new Vector2(0, 0);
            m_texture = texture;
            m_spawnfrequency = 3.0f;
            m_currentspawncooldown = 0.0f;
            m_maxenemies = 1;
        }

        /// <summary>
        /// Overload w Texture, Spawn
        /// </summary>
        public EnemySpawner(Texture2D texture, Texture2D chasertexture, float SpawnFrequency) //Overload w Texture, Spawn
        {
            m_enemylist = new List<Enemy>();
            m_spawnerlocation = new Vector2(0, 0);
            m_texture = texture;
            m_spawnfrequency = SpawnFrequency;
            m_currentspawncooldown = 0.0f;
            m_maxenemies = 0;
        }



        /// <summary>
        /// Overload w, Texture, Spawn, Frequency 
        /// </summary>
        public EnemySpawner(Texture2D texture, Texture2D chasertexture, Vector2 SpawnerPosition, float SpawnFrequency, Player playerref) 
        {
            m_enemylist = new List<Enemy>();
            m_spawnerlocation = SpawnerPosition;
            m_texture = texture;
            m_chasertexture = chasertexture;
            m_spawnfrequency = SpawnFrequency;
            m_currentspawncooldown = 0.0f;
            m_maxenemies = 1;
            m_player = playerref;
        }

        public void SetCameraMatrix(Matrix cam)
        {
            m_CameraMatrix = cam;
        }

        /// <summary>
        /// Main update loop, here we will execute our spawning
        /// </summary>
        public void Update(float _deltatime)
        {

            SpawnEnemy(_deltatime);

            for (int i = 0; i < m_enemylist.Count; i++)
            {
                if (m_enemylist[i].m_alive)
                {
                    m_enemylist[i].Update(_deltatime);
                }

                else
                {
                    m_enemylist.RemoveAt(i);
                }
            }
        }


        /// <summary>
        /// Draw each enemy in the list array
        /// </summary>
        public void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < m_enemylist.Count; i++)
            {
                if(m_enemylist[i].m_alive)
                {
                    m_enemylist[i].Draw(spritebatch);

                }
               
            }
        }

        public void DrawInfo(SpriteBatch spritebatch, SpriteFont font)
        {
            for (int i = 0; i < m_enemylist.Count; i++)
            {
                if (m_enemylist[i].m_alive)
                {
                    m_enemylist[i].DrawInfo(spritebatch,font);

                }

            }
        }

        /// <summary>
        /// For testing purposes only , just to spawn an enemy with no conditions
        /// </summary>
        public void SpawnEnemy()
        {
            Enemy NewEnemy = new Enemy(m_texture, m_spawnerlocation);
            NewEnemy.SetCameraMatrix(m_CameraMatrix);
            m_enemylist.Add(NewEnemy);
            m_currentspawncooldown = m_spawnfrequency;
        }




        /// <summary>
        /// This is what is used to spawn a enemy periodically
        /// </summary>
        /// <param name="_deltatime"></param>
        public void SpawnEnemy(float _deltatime)
        {

            m_currentspawncooldown -= _deltatime;
           
            if(m_currentspawncooldown < 0 && m_enemylist.Count <= m_maxenemies)
            {
                //ChaserEnemy NewEnemy = new ChaserEnemy(m_chasertexture, m_spawnerlocation ,m_player);
                //NewEnemy.SetCameraMatrix(m_CameraMatrix);

                Enemy NewEnemy = new Enemy(m_texture, m_spawnerlocation);
                //NewEnemy.SetCameraMatrix(m_CameraMatrix);

                m_enemylist.Add(NewEnemy);
                m_currentspawncooldown = m_spawnfrequency;
            }
        }


      
        public float GetSpawnCooldown() { return m_currentspawncooldown; }
        public List<Enemy> GetEnemies() { return m_enemylist; }

    }
}
