using Microsoft.Xna.Framework.Input;
using NesJamGame.Engine.Input;

namespace NesJamGame.GameContent
{
    public static class GameInput
    {
        public static bool IsNewPress(NESInput input)
        {
            switch(input)
            {
                case NESInput.A:
                    return InputManager.IsNewPress(Keys.Space) || InputManager.IsNewPress(GamePadIndex.Any, Buttons.A);

                case NESInput.B:
                    return InputManager.IsNewPress(Keys.W) || InputManager.IsNewPress(Keys.Z) || InputManager.IsNewPress(GamePadIndex.Any, Buttons.B);

                case NESInput.Up:
                    return InputManager.IsNewPress(Keys.Up) || InputManager.IsNewPress(GamePadIndex.Any, Buttons.DPadUp) || InputManager.IsNewPress(GamePadIndex.Any, Buttons.LeftThumbstickUp);

                case NESInput.Left:
                    return InputManager.IsNewPress(Keys.Left) || InputManager.IsNewPress(GamePadIndex.Any, Buttons.DPadLeft) || InputManager.IsNewPress(GamePadIndex.Any, Buttons.LeftThumbstickLeft);

                case NESInput.Right:
                    return InputManager.IsNewPress(Keys.Right) || InputManager.IsNewPress(GamePadIndex.Any, Buttons.DPadRight) || InputManager.IsNewPress(GamePadIndex.Any, Buttons.LeftThumbstickRight);

                case NESInput.Down:
                    return InputManager.IsNewPress(Keys.Down) || InputManager.IsNewPress(GamePadIndex.Any, Buttons.DPadDown) || InputManager.IsNewPress(GamePadIndex.Any, Buttons.LeftThumbstickDown);

                case NESInput.Start:
                    return InputManager.IsNewPress(Keys.Escape) || InputManager.IsNewPress(GamePadIndex.Any, Buttons.Start);

                case NESInput.Select:
                    return InputManager.IsNewPress(Keys.Enter) || InputManager.IsNewPress(GamePadIndex.Any, Buttons.Back);

                default:
                    return false;
            }
        }

        public static bool IsNewRelease(NESInput input)
        {
            switch (input)
            {
                case NESInput.A:
                    return InputManager.IsNewRelease(Keys.Space) || InputManager.IsNewRelease(GamePadIndex.Any, Buttons.A);

                case NESInput.B:
                    return InputManager.IsNewRelease(Keys.W) || InputManager.IsNewRelease(Keys.Z) || InputManager.IsNewRelease(GamePadIndex.Any, Buttons.B);

                case NESInput.Up:
                    return InputManager.IsNewRelease(Keys.Up) || InputManager.IsNewRelease(GamePadIndex.Any, Buttons.DPadUp) || InputManager.IsNewRelease(GamePadIndex.Any, Buttons.LeftThumbstickUp);

                case NESInput.Left:
                    return InputManager.IsNewRelease(Keys.Left) || InputManager.IsNewRelease(GamePadIndex.Any, Buttons.DPadLeft) || InputManager.IsNewRelease(GamePadIndex.Any, Buttons.LeftThumbstickLeft);

                case NESInput.Right:
                    return InputManager.IsNewRelease(Keys.Right) || InputManager.IsNewRelease(GamePadIndex.Any, Buttons.DPadRight) || InputManager.IsNewRelease(GamePadIndex.Any, Buttons.LeftThumbstickRight);

                case NESInput.Down:
                    return InputManager.IsNewRelease(Keys.Down) || InputManager.IsNewRelease(GamePadIndex.Any, Buttons.DPadDown) || InputManager.IsNewRelease(GamePadIndex.Any, Buttons.LeftThumbstickDown);

                case NESInput.Start:
                    return InputManager.IsNewRelease(Keys.Escape) || InputManager.IsNewRelease(GamePadIndex.Any, Buttons.Start);

                case NESInput.Select:
                    return InputManager.IsNewRelease(Keys.Enter) || InputManager.IsNewRelease(GamePadIndex.Any, Buttons.Back);

                default:
                    return false;
            }
        }

        public static bool IsButtonDown(NESInput input)
        {
            switch (input)
            {
                case NESInput.A:
                    return InputManager.IsKeyDown(Keys.Space) || InputManager.IsButtonDown(GamePadIndex.Any, Buttons.A);

                case NESInput.B:
                    return InputManager.IsKeyDown(Keys.W) || InputManager.IsKeyDown(Keys.Z) || InputManager.IsButtonDown(GamePadIndex.Any, Buttons.B);

                case NESInput.Up:
                    return InputManager.IsKeyDown(Keys.Up) || InputManager.IsButtonDown(GamePadIndex.Any, Buttons.DPadUp) || InputManager.IsButtonDown(GamePadIndex.Any, Buttons.LeftThumbstickUp);

                case NESInput.Left:
                    return InputManager.IsKeyDown(Keys.Left) || InputManager.IsButtonDown(GamePadIndex.Any, Buttons.DPadLeft) || InputManager.IsButtonDown(GamePadIndex.Any, Buttons.LeftThumbstickLeft);

                case NESInput.Right:
                    return InputManager.IsKeyDown(Keys.Right) || InputManager.IsButtonDown(GamePadIndex.Any, Buttons.DPadRight) || InputManager.IsButtonDown(GamePadIndex.Any, Buttons.LeftThumbstickRight);

                case NESInput.Down:
                    return InputManager.IsKeyDown(Keys.Down) || InputManager.IsButtonDown(GamePadIndex.Any, Buttons.DPadDown) || InputManager.IsButtonDown(GamePadIndex.Any, Buttons.LeftThumbstickDown);

                case NESInput.Start:
                    return InputManager.IsKeyDown(Keys.Escape) || InputManager.IsButtonDown(GamePadIndex.Any, Buttons.Start);

                case NESInput.Select:
                    return InputManager.IsKeyDown(Keys.Enter) || InputManager.IsButtonDown(GamePadIndex.Any, Buttons.Back);

                default:
                    return false;
            }
        }

        public static bool IsButtonUp(NESInput input)
        {
            switch (input)
            {
                case NESInput.A:
                    return InputManager.IsKeyUp(Keys.Space) || InputManager.IsButtonUp(GamePadIndex.Any, Buttons.A);

                case NESInput.B:
                    return InputManager.IsKeyUp(Keys.W) || InputManager.IsKeyUp(Keys.Z) || InputManager.IsButtonUp(GamePadIndex.Any, Buttons.B);

                case NESInput.Up:
                    return InputManager.IsKeyUp(Keys.Up) || InputManager.IsButtonUp(GamePadIndex.Any, Buttons.DPadUp) || InputManager.IsButtonUp(GamePadIndex.Any, Buttons.LeftThumbstickUp);

                case NESInput.Left:
                    return InputManager.IsKeyUp(Keys.Left) || InputManager.IsButtonUp(GamePadIndex.Any, Buttons.DPadLeft) || InputManager.IsButtonUp(GamePadIndex.Any, Buttons.LeftThumbstickLeft);

                case NESInput.Right:
                    return InputManager.IsKeyUp(Keys.Right) || InputManager.IsButtonUp(GamePadIndex.Any, Buttons.DPadRight) || InputManager.IsButtonUp(GamePadIndex.Any, Buttons.LeftThumbstickRight);

                case NESInput.Down:
                    return InputManager.IsKeyUp(Keys.Down) || InputManager.IsButtonUp(GamePadIndex.Any, Buttons.DPadDown) || InputManager.IsButtonUp(GamePadIndex.Any, Buttons.LeftThumbstickDown);

                case NESInput.Start:
                    return InputManager.IsKeyUp(Keys.Escape) || InputManager.IsButtonUp(GamePadIndex.Any, Buttons.Start);

                case NESInput.Select:
                    return InputManager.IsKeyUp(Keys.Enter) || InputManager.IsButtonUp(GamePadIndex.Any, Buttons.Back);

                default:
                    return false;
            }
        }
    }
}
