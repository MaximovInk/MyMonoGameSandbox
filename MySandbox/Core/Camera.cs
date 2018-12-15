using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MySandbox.Core
{
    public class Camera
    {
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; protected set; }
        public Rectangle VisibleArea { get; protected set; }
        public Matrix Transform { get; protected set; }

        public static Camera main;

        private float currentMouseWheelValue, previousMouseWheelValue, zoom, previousZoom;

        public delegate void OnMoved(Vector2 pos);
        public delegate void OnZoomed();

        public event OnMoved onMove;
        public event OnZoomed onZoom;

        /// <summary>
        /// Construct camera
        /// </summary>
        /// <param name="viewport">Game viewport</param>
        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
            Zoom = 3f;
            Position = Vector2.Zero;
            main = this;
        }
        /// <summary>
        /// ! NOT VERIFIED ! Covert world position to screen
        /// </summary>
        public Vector2 GetScreenPosition(Vector2 worldPosition)
        {
            return worldPosition - (worldPosition + new Vector2(Bounds.Width / 2, Bounds.Height / 2)) * Zoom;
        }
        /// <summary>
        /// Convert screen to world position
        /// </summary>
        public Vector2 GetWorldPosition(Vector2 screenPosition)
        {
            return Position + (screenPosition - new Vector2(Bounds.Width/2, Bounds.Height/2))/Zoom;
        }
        /// <summary>
        /// Update screen area
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
        /// Update matrix
        /// </summary>
        private void UpdateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y , 0)) *
                    Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            UpdateVisibleArea();
        }
        /// <summary>
        /// Zoom camera
        /// </summary>
        public void AdjustZoom(float zoomAmount)
        {
            Zoom += zoomAmount;
            if (Zoom < 1.2f)
            {
                Zoom = 1.2f;
            }
            if (Zoom > 9f)
            {
                Zoom = 9f;
            }
            if (onZoom != null)
                onZoom();
        }
        /// <summary>
        /// Set position
        /// </summary>
        /// <param name="pos"></param>
        public void SetPosition(Vector2 pos)
        {
            Position = new Vector2( pos.X, pos.Y);
            if(onMove != null)
                onMove(pos);
        }
        /// <summary>
        /// Update camera
        /// </summary>
        public void UpdateCamera(Viewport bounds)
        {
            Bounds = bounds.Bounds;
            UpdateMatrix();

            previousMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

            if (currentMouseWheelValue > previousMouseWheelValue)
            {
                AdjustZoom(.5f);
            }

            if (currentMouseWheelValue < previousMouseWheelValue)
            {
                AdjustZoom(-.5f);
            }

            previousZoom = zoom;
            zoom = Zoom;
        }
        
    }
}
