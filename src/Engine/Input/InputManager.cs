using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NesJamGame.Engine.Input
{
    public static class InputManager
    {
        private static KeyboardState keyboard, oldKeyboard;
        private static MouseState mouse, oldMouse;
        private static GamePadState[] gamePads, oldGamePads;

        public static void Initialize()
        {
            keyboard = Keyboard.GetState();
            oldKeyboard = new KeyboardState();

            gamePads = new GamePadState[4];
            oldGamePads = new GamePadState[4];

            for (int i = 0; i < 4; i++)
            {
                oldGamePads[i] = new GamePadState();
                gamePads[i] = GamePad.GetState((PlayerIndex)i);
            }
        }

        public static void Update()
        {
            oldKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            oldMouse = mouse;
            mouse = Mouse.GetState();

            for (int i = 0; i < 4; i++)
            {
                oldGamePads[i] = gamePads[i];
                gamePads[i] = GamePad.GetState((PlayerIndex)i);
            }
        }

        // Keyboard
        public static bool IsNewPress(Keys key)
        {
            return keyboard.IsKeyDown(key) && oldKeyboard.IsKeyUp(key);
        }

        public static bool IsNewRelease(Keys key)
        {
            return keyboard.IsKeyUp(key) && oldKeyboard.IsKeyDown(key);
        }

        public static bool IsKeyHeld(Keys key)
        {
            return keyboard.IsKeyDown(key) && oldKeyboard.IsKeyDown(key);
        }

        public static bool IsKeyDown(Keys key)
        {
            return keyboard.IsKeyDown(key);
        }

        public static bool IsKeyUp(Keys key)
        {
            return keyboard.IsKeyUp(key);
        }

        public static bool KeyboardStateChanged()
        {
            return keyboard != oldKeyboard;
        }

        // Mouse
        // TODO: Mouse Button Detection
        public static bool IsNewPress(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released;

                case MouseButtons.Right:
                    return mouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Released;

                case MouseButtons.Middle:
                    return mouse.MiddleButton == ButtonState.Pressed && oldMouse.MiddleButton == ButtonState.Released;

                default:
                    return false;
            }
        }

        public static bool IsNewRelease(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return mouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed;

                case MouseButtons.Right:
                    return mouse.RightButton == ButtonState.Released && oldMouse.RightButton == ButtonState.Pressed;

                case MouseButtons.Middle:
                    return mouse.MiddleButton == ButtonState.Released && oldMouse.MiddleButton == ButtonState.Pressed;

                default:
                    return false;
            }
        }

        public static bool IsButtonHeld(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Pressed;

                case MouseButtons.Right:
                    return mouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Pressed;

                case MouseButtons.Middle:
                    return mouse.MiddleButton == ButtonState.Pressed && oldMouse.MiddleButton == ButtonState.Pressed;

                default:
                    return false;
            }
        }

        public static bool IsButtonDown(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return mouse.LeftButton == ButtonState.Pressed;

                case MouseButtons.Right:
                    return mouse.RightButton == ButtonState.Pressed;

                case MouseButtons.Middle:
                    return mouse.MiddleButton == ButtonState.Pressed;

                default:
                    return false;
            }
        }

        public static bool IsButtonUp(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return mouse.LeftButton == ButtonState.Released;

                case MouseButtons.Right:
                    return mouse.RightButton == ButtonState.Released;

                case MouseButtons.Middle:
                    return mouse.MiddleButton == ButtonState.Released;

                default:
                    return false;
            }
        }

        public static bool MouseStateChanged()
        {
            return mouse != oldMouse;
        }

        // GamePad
        public static bool IsNewPress(GamePadIndex index, Buttons button)
        {
            if (index == GamePadIndex.Any)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (gamePads[i].IsButtonDown(button) && gamePads[i].IsButtonUp(button))
                        return true;
                }
                return false;
            }
            return gamePads[(int)index].IsButtonDown(button) && gamePads[(int)index].IsButtonUp(button);
        }

        public static bool IsNewRelease(GamePadIndex index, Buttons button)
        {
            if (index == GamePadIndex.Any)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (gamePads[i].IsButtonUp(button) && gamePads[i].IsButtonDown(button))
                        return true;
                }
                return false;
            }
            return gamePads[(int)index].IsButtonUp(button) && gamePads[(int)index].IsButtonDown(button);
        }

        public static bool IsButtonHeld(GamePadIndex index, Buttons button)
        {
            if (index == GamePadIndex.Any)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (gamePads[i].IsButtonDown(button) && gamePads[i].IsButtonDown(button))
                        return true;
                }
                return false;
            }
            return gamePads[(int)index].IsButtonDown(button) && gamePads[(int)index].IsButtonDown(button);
        }

        public static bool IsButtonDown(GamePadIndex index, Buttons button)
        {
            if (index == GamePadIndex.Any)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (gamePads[i].IsButtonDown(button))
                        return true;
                }
                return false;
            }
            return gamePads[(int)index].IsButtonDown(button);
        }

        public static bool IsButtonUp(GamePadIndex index, Buttons button)
        {
            if (index == GamePadIndex.Any)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (gamePads[i].IsButtonUp(button))
                        return true;
                }
                return false;
            }
            return gamePads[(int)index].IsButtonUp(button);
        }

        public static bool GamePadStateChanged(GamePadIndex index, Buttons button)
        {
            if (index == GamePadIndex.Any) return gamePads != oldGamePads;
            return gamePads[(int)index] != oldGamePads[(int)index];
        }
    }
}
