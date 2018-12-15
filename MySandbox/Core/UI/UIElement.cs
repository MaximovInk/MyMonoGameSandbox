using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MySandbox.Core.UI
{
    public abstract class UIElement
    {
        public Vector2 position;

        public bool IsActive = true;

        public Color Color = Color.White;

        public DrawType drawType = DrawType.Normal;

        public List<UIElement> childrens;

        public bool MouseOver => hover;
        protected bool hover;

        public int ScaleX = 1;
        public int ScaleY = 1;
        /// <summary>
        /// Construct UIElement
        /// </summary>
        /// <param name="position">Start position</param>
        /// <param name="scaleX">Scale x</param>
        /// <param name="scaleY">Scale y</param>
        public UIElement(Vector2 position, int scaleX = 1, int scaleY = 1)
        { 
            this.position = position;
            ScaleX = scaleX;
            ScaleY = scaleY;
            childrens = new List<UIElement>();
        }

        /// <summary>
        /// Update UIElement
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public virtual void Update(GameTime gameTime)
        {
            
        }
        /// <summary>
        /// Draw UIElement
        /// </summary>
        /// <param name="pos">Parent position</param>
        public virtual void Draw(Vector2 pos)
        {
            if (IsActive)
            {
                for (int i = 0; i < childrens.Count; i++)
                {
                    childrens[i].Draw(pos+position);
                }
            }

        }
    }
}
