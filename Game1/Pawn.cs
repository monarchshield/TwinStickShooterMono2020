using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShootyGame
{
    class Pawn
    {
        public Vector2 m_currentposition;
        protected float m_speed;
        protected bool m_collision;
        protected Texture2D m_texture;
        protected string m_objectTag;
        protected SpriteFont m_sprintfont;

        public Pawn()
        {
            /* Default constructor*/
        }

        public Vector2 GetPosition() { return m_currentposition; }
        public float GetSpeed() { return m_speed; }
        public bool GetCollision() { return m_collision; }
        public Rectangle GetDimensions() { return m_texture.Bounds; }

        public void SetCollision(bool val) { m_collision = val; }

        public void SetObjectTag(string val) { m_objectTag = val; }
        public string GetobjectTag() { return m_objectTag; }

    }
}
