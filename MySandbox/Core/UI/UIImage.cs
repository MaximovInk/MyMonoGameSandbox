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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Vector2 s = Core.Input.MousePosition;

            Vector2 m = new Vector2(s.X / Camera.main.Zoom, s.Y / Camera.main.Zoom);

            hover =
                   (m.X > position.X * (1 / Camera.main.Zoom) && m.X < position.X * (1 / Camera.main.Zoom) + Bounds.X) &&
                   (m.Y > position.Y * (1 / Camera.main.Zoom) && m.Y < position.Y * (1 / Camera.main.Zoom) + Bounds.Y);
        }

        public void SetElement(int index)
        {
            if (index < texture.elements.Length)
            {
                curr_rectngle = texture.elements[index];
            }
        }

        public override void Draw(Vector2 pos)
        {
            
            if (IsActive)
            {
                Core.spriteBatch.Draw(texture.sheet,
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
