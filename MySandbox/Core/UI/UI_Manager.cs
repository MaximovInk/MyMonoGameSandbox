using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MySandbox.Core.UI
{
    public class UI_Manager
    {
        public bool IsActive = true;

        public List<UIElement> elements = new List<UIElement>();
        public Vector2 pos;

        public void Draw()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].Draw(pos);
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].Update(gameTime);
            }
        }

        public void OnMove(Vector2 pos)
        {
            this.pos = pos - new Vector2(Camera.main.Bounds.Width/2/Camera.main.Zoom , Camera.main.Bounds.Height/2/Camera.main.Zoom);
        }

        public bool IsOverUI()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].MouseOver)
                    return true;
            }
            return false;
        }
    }
}
