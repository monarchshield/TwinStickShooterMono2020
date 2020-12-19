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

        public override void Update(float _deltaTime)
        {

        }

        public void SetPlayerRef(Player playerref)
        {
            m_playerref = playerref;
        }

    }
}
