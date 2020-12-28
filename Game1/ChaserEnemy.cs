using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShootyGame
{
    class ChaserEnemy : Enemy
    {

        Player m_playerref;

        public ChaserEnemy() { }

        public ChaserEnemy(Texture2D texture, Vector2 Position)
        {
            m_texture = texture;
            m_currentposition = Position;
            Collision = false;
            SetObjectTag("enemy");
            
        }

        public ChaserEnemy(Texture2D texture, Vector2 Position, Player TargetRef)
        {
            m_texture = texture;
            m_currentposition = Position;
            Collision = false;
            m_playerref = TargetRef;
            SetObjectTag("enemy");
            m_animationframes = 1;
            m_alive = true;
            m_speed = 250.0f;
        }

        public override void Update(float _deltaTime)
        {
            m_dt = _deltaTime;   
            if(m_alive)
            {
                m_animationtimer += _deltaTime;
                CheckDeath();
                UpdateAnimation(.10f);
                Seek(m_playerref.GetPosition());
            }
        }

        public void SetPlayerRef(Player playerref)
        {
            m_playerref = playerref;
        }

    }
}
