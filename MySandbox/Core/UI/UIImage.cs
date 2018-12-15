using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MySandbox.Core.UI
{
    class UIImage : UIElement
    {
        public Texture2DSheet texture;
        public Rectangle curr_rectngle { get; protected set; }
        public Vector2 Bounds { get { return new Vector2(curr_rectngle.Width * ScaleX * (1 / Camera.main.Zoom), curr_rectngle.Height * ScaleY * (1/Camera.main.Zoom)); } }
        /// <summary>
        /// Construct UIImage
        /// </summary>
        /// <param name="sheet">Texture atlas</param>
        /// <param name="position">Start position</param>
        /// <param name="element">Atlas element</param>
        /// <param name="scaleX">Scale x</param>
        /// <param name="scaleY">Scale y</param>
        public UIImage(Texture2DSheet sheet, Vector2 position, int element = 0, int scaleX = 1, int scaleY = 1): base(position,scaleX,scaleY)
        {
            texture = sheet;
            this.position = position;
            ScaleX = scaleX;
            ScaleY = scaleY;
            if (sheet != null)
                SetElement(element);
            childrens = new List<UIElement>();
        }
        /// <summary>
        /// Update UIElement
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Vector2 s = Core.Input.MousePosition;

            Vector2 m = new Vector2(s.X / Camera.main.Zoom, s.Y / Camera.main.Zoom);

            hover =
                   (m.X > position.X * (1 / Camera.main.Zoom) && m.X < position.X * (1 / Camera.main.Zoom) + Bounds.X) &&
                   (m.Y > position.Y * (1 / Camera.main.Zoom) && m.Y < position.Y * (1 / Camera.main.Zoom) + Bounds.Y);
        }
        /// <summary>
        /// Set element
        /// </summary>
        /// <param name="index">Atlas index</param>
        public void SetElement(int index)
        {
            if (index < texture.elements.Length)
            {
                curr_rectngle = texture.elements[index];
            }
        }
        /// <summary>
        /// Draw UIImage
        /// </summary>
        /// <param name="pos">Parent position</param>
        public override void Draw(Vector2 pos)
        {
            
            if (IsActive)
            {
                Core.spriteBatch.Draw(texture.texture,
                   (1/Camera.main.Zoom) * position  + pos ,
                   curr_rectngle,
                   Color,
                   0,
                   Vector2.Zero,
                   new Vector2(ScaleX, ScaleY) * (1/Camera.main.Zoom),
                   SpriteEffects.None,
                   0);
            }
            base.Draw(pos);
        }
    }

    public enum DrawType
    {
        Normal,
        Sliced
    }
}
