using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootyGame
{
    class Camera
    {
        protected Player m_playerref;
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; protected set; }
        public Rectangle VisibleArea { get; protected set; }
        public Matrix Transform { get; protected set; }

       


        private float currentMouseWheelValue, previousMouseWheelValue, zoom, previousZoom;

        //Default constructor
        public Camera() { }

        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
            Zoom = .5f;
            Position = Vector2.Zero;
        }

        public Camera(Viewport viewport, Player playerRef)
        {
            Bounds = viewport.Bounds;
            m_playerref = playerRef;
            Zoom = .5f;
            Position = Vector2.Zero;
        }

        


        /// <summary>
        /// Should only be called in the matrix updatte
        /// </summary>
        private void UpdateVisibleArea()
        {
            var inverseViewMatrix = Matrix.Invert(Transform);

            var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
            var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
            var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

            var min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            var max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }


        /// <summary>
        /// Should only be called from Update camera
        /// </summary>
        private void UpdateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0))
                * Matrix.CreateScale(Zoom) 
                * Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0)); 
                UpdateVisibleArea();
        }

        /// <summary>
        /// An Update reference for changing the position of the camera
        /// </summary>
        /// <param name="currentposition"></param>
        /// 
        public void UpdateCameraPosition()
        {
            Position = m_playerref.GetPosition();
        }

        public void MoveCamera(Vector2 currentposition)
        {
            Position = currentposition;
        }


        /// <summary>
        /// Havent read what this does but it seems cool!
        /// </summary>
        /// <param name="zoomAmount"></param>
        public void AdjustZoom(float zoomAmount)
        {
            Zoom += zoomAmount;
            if (Zoom < .35f)
            {
                Zoom = .35f;
            }
            if (Zoom > 2f)
            {
                Zoom = 2f;
            }
        }


        /// <summary>
        /// This should be called in the main update loop
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="Position"></param>
        public void UpdateCamera(Viewport bounds)
        {
            Bounds = bounds.Bounds;
            UpdateMatrix();

            Vector2 cameraMovement = Vector2.Zero;
            int moveSpeed;

            if (Zoom > .8f)
            {
                moveSpeed = 15;
            }
            else if (Zoom < .8f && Zoom >= .6f)
            {
                moveSpeed = 20;
            }
            else if (Zoom < .6f && Zoom > .35f)
            {
                moveSpeed = 25;
            }
            else if (Zoom <= .35f)
            {
                moveSpeed = 30;
            }
            else
            {
                moveSpeed = 10;
            }


            previousMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

            if (currentMouseWheelValue > previousMouseWheelValue)
            {
                //AdjustZoom(.05f);
                Console.WriteLine(moveSpeed);
            }

            if (currentMouseWheelValue < previousMouseWheelValue)
            {
                //AdjustZoom(-.05f);
                Console.WriteLine(moveSpeed);
            }

            previousZoom = zoom;
            zoom = Zoom;
            if (previousZoom != zoom)
            {
                Console.WriteLine(zoom);

            }

            UpdateCameraPosition();
        }
    }
}
