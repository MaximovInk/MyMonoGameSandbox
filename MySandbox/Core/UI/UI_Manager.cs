using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MySandbox.Core.UI
{
    public class UI_Manager
    {
        public bool IsActive = true;

        public List<UIElement> elements = new List<UIElement>();
        public Vector2 pos;

        /// <summary>
        /// Draw UIElements
        /// </summary>
        public void Draw()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].Draw(pos);
            }
        }
        /// <summary>
        /// Update UIElements
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].Update(gameTime);
            }
        }
        /// <summary>
        /// On UIManager move
        /// </summary>
        /// <param name="pos">Camera position</param>
        public void OnMove(Vector2 pos)
        {
            this.pos = pos - new Vector2(Camera.main.Bounds.Width/2/Camera.main.Zoom , Camera.main.Bounds.Height/2/Camera.main.Zoom);
        }
        /// <summary>
        /// Check mouse over UI elements
        /// </summary>
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
