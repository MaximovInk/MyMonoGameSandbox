using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MySandbox.Core
{
    public class Input
    {
        #region Keyboard
        private KeyboardState kstate;
        private KeyboardState last_kstate;
        /// <summary>
        /// Is key pressed
        /// </summary>
        public bool GetKey(Keys key) => kstate.IsKeyDown(key);
        /// <summary>
        /// Is key pressed once
        /// </summary>
        public bool GetKeyDown(Keys key) => kstate.IsKeyDown(key) && !last_kstate.IsKeyDown(key);
        /// <summary>
        /// Is key released
        /// </summary>
        public bool GetKeyUp(Keys key) => kstate.IsKeyUp(key);
        /// <summary>
        /// Caps lock is active
        /// </summary>
        public bool Caps => kstate.CapsLock;
        #endregion

        #region Mouse
        private MouseState mstate;
        private MouseState last_mstate;
        /// <summary>
        /// Delta of mouse scroll wheel
        /// </summary>
        public int MouseScrollWheelDelta => mstate.ScrollWheelValue - last_mstate.ScrollWheelValue;
        /// <summary>
        /// Mouse position
        /// </summary>
        public Vector2 MousePosition => new Vector2(mstate.X, mstate.Y);
        /// <summary>
        /// Current mouse scroll wheel value
        /// </summary>
        public int MouseScrollWheelValue => mstate.ScrollWheelValue;
        /// <summary>
        /// Mouse button is pressed 
        /// </summary>
        /// <param name="index">0-left , 1 - right , 2 - middle</param>
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
        /// <summary>
        /// Mouse button is pressed once
        /// </summary>
        /// <param name="index">0-left , 1 - right , 2 - middle</param>
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
        /// <summary>
        /// Mouse button is released
        /// </summary>
        /// <param name="index">0-left , 1 - right , 2 - middle</param>
        public bool GetMouseButtonUp(int index)
        {
            return !GetMouseButton(index);
        }
        #endregion
        /// <summary>
        /// Update inputs | In begin Update()
        /// </summary>
        public void Update()
        {
            kstate = Keyboard.GetState();
            mstate = Mouse.GetState();

        }
        /// <summary>
        /// Last update inputs | In end Update()
        /// </summary>
        public void LateUpdate()
        {
            last_kstate = kstate;
            last_mstate = mstate;

        }
    }
}
