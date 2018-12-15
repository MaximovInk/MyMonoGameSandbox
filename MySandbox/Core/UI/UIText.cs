using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MySandbox.Core.UI
{
    class UIText : UIElement
    {
        public string Text = string.Empty;
        public SpriteFont font;
        public float size = 1;

        public UIText(SpriteFont font, Vector2 position, int element = 0, int scaleX = 1, int scaleY = 1) : base( position, scaleX, scaleY)
        {
            this.font = font;
        }

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
