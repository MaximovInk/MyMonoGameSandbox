using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MySandbox.Core.UI
{
    class UIButton : UIImage
    {
        public delegate void OnMouseHover();
        public delegate void OnMouseEnter();
        public delegate void OnMouseLeave();

        public delegate void OnMouseDown();
        public delegate void OnMousePress();
        public delegate void OnMouseUp();

        public Color DefaultColor = Color.White;
        public Color OnHoverColor = Color.LightGray;
        public Color OnPressColor = Color.DarkGray;
        
        public event OnMouseHover onMouseHover;
        public event OnMouseEnter onMouseEnter;
        public event OnMouseLeave onMouseLeave;

        public event OnMouseDown onMouseDown;
        public event OnMousePress onMousePress;
        public event OnMouseUp onMouseRelease;

        public bool Selectable = false;

        private bool selected = false;

        public delegate void OnSelect();

        public event OnSelect onSelect;

        private bool is_hover = false;
        private bool is_press = false;

        public UIButton(Texture2DSheet sheet, Vector2 position, int element = 0, int scaleX = 1, int scaleY = 1) : base(sheet, position, element, scaleX , scaleY)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Vector2 s = Core.Input.MousePosition;

            Vector2 m = new Vector2(s.X/Camera.main.Zoom, s.Y/Camera.main.Zoom);

            bool pressed = hover && Core.Input.GetMouseButton(0);
            HaP(hover, pressed);
        }

        private void HaP(bool hover, bool press)
        {
            if (Selectable && press && onSelect != null)
            {
                onSelect();
                Color = OnPressColor;
                selected = true;
            }
            Color = press ? OnPressColor : hover ? OnHoverColor : selected ? OnPressColor : DefaultColor;
            
            if (hover)
            {
                if (!is_hover)
                {
                    if (onMouseEnter != null)
                        onMouseEnter();
                    is_hover = true;
                }

                if (onMouseHover != null)
                    onMouseHover();
            }
            else if (is_hover)
            {
                is_hover = false;
                if (onMouseLeave != null)
                    onMouseLeave();
            }
            if (press)
            {
                if (!is_press)
                {
                    is_press = true;
                    if (onMouseDown != null)
                        onMouseDown();
                }
                if (onMousePress != null)
                    onMousePress();
            }
            else if (is_press)
            {
                is_press = false;
                if (onMouseRelease != null)
                    onMouseRelease();
            }

        }

        public void Select()
        {
            if (Selectable)
            {
                selected = true;
                Color = OnPressColor;
            }
        }

        public void Deselect()
        {
            if(Selectable)
                Color = DefaultColor;
            selected = false;
        }
    }
}
