using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MySandbox.Core
{
    public class Input
    {
        #region Keyboard
        private KeyboardState kstate;
        private KeyboardState last_kstate;

        public bool IsKey(Keys key) => kstate.IsKeyDown(key);
        public bool IsKeyDown(Keys key) => kstate.IsKeyDown(key) && !last_kstate.IsKeyDown(key);
        public bool IsKeyUp(Keys key) => kstate.IsKeyUp(key);
        public bool Caps => kstate.CapsLock;
        #endregion

        #region Mouse
        private MouseState mstate;
        private MouseState last_mstate;

        public int MouseScrollWheelDelta => mstate.ScrollWheelValue - last_mstate.ScrollWheelValue;
        public Vector2 MousePosition => new Vector2(mstate.X, mstate.Y);
        public int MouseScrollWheelValue => mstate.ScrollWheelValue;

        public bool GetMouseButton(int index)
        {
            switch (index)
            {
                case 0:
                    if (mstate.LeftButton == ButtonState.Pressed)
                        return true;
                    break;
                case 1:
                    if (mstate.RightButton == ButtonState.Pressed)
                        return true;
                    break;
                case 2:
                    if (mstate.MiddleButton == ButtonState.Pressed)
                        return true;
                    break;
            }
            return false;
        }
        public bool GetMouseButtonDown(int index)
        {
            switch (index)
            {
                case 0:
                    if (mstate.LeftButton == ButtonState.Pressed && last_mstate.LeftButton != ButtonState.Pressed)
                        return true;
                    break;
                case 1:
                    if (mstate.RightButton == ButtonState.Pressed && last_mstate.RightButton != ButtonState.Pressed)
                        return true;
                    break;
                case 2:
                    if (mstate.MiddleButton == ButtonState.Pressed && last_mstate.MiddleButton != ButtonState.Pressed)
                        return true;
                    break;
            }
            return false;
        }
        public bool GetMouseButtonUp(int index)
        {
            return !GetMouseButton(index);
        }

        #endregion

        public void Update()
        {
            kstate = Keyboard.GetState();
            mstate = Mouse.GetState();

        }

        public void LateUpdate()
        {
            last_kstate = kstate;
            last_mstate = mstate;

        }
    }
}
