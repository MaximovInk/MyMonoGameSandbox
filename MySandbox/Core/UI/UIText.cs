using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MySandbox.Core.UI
{
    class UIText : UIElement
    {
        public string Text = string.Empty;
        public SpriteFont font;
        public float size = 1;
        /// <summary>
        /// Construct UIext
        /// </summary>
        /// <param name="font">Font</param>
        /// <param name="position">Start position</param>
        /// <param name="scaleX">Scale x</param>
        /// <param name="scaleY">Scale y</param>
        public UIText(SpriteFont font, Vector2 position, int scaleX = 1, int scaleY = 1) : base( position, scaleX, scaleY)
        {
            this.font = font;
        }
        /// <summary>
        /// Draw text
        /// </summary>
        /// <param name="pos">Parent position</param>
        public override void Draw(Vector2 pos)
        {
            Core.spriteBatch.DrawString(font, Text, position+pos, Color.Black, 0, Vector2.Zero, 1 / Camera.main.Zoom * size, SpriteEffects.None, 0);

            for (int i = 0; i < childrens.Count; i++)
            {
                childrens[i].Draw(pos + position);
            }
        }
    }
}
