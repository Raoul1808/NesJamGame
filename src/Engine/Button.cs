using Microsoft.Xna.Framework;
using NesJamGame.Engine.Input;

namespace NesJamGame.Engine
{
    public abstract class Button
    {
        public Rectangle rectangle;
        bool pressed;

        public virtual void Update()
        {
            Point mousePos = InputManager.GetMousePos();
            if (rectangle.Contains(mousePos))
            {
                if (InputManager.IsNewPress(MouseButtons.Left))
                {
                    pressed = true;
                    OnStartClick();
                }
                else if (InputManager.IsNewRelease(MouseButtons.Left) && pressed)
                {
                    pressed = false;
                    OnValidClick();
                }
                else if (InputManager.IsButtonHeld(MouseButtons.Left))
                {
                    OnClickHeld();
                }
                else
                {
                    OnHover();
                }
            }
            else if (InputManager.IsNewRelease(MouseButtons.Left) && pressed)
            {
                pressed = false;
                OnClickCancel();
            }
            else
            {
                OnIdle();
            }
        }

        public virtual void OnIdle()
        {
        }

        public virtual void OnHover()
        {
        }

        public virtual void OnStartClick()
        {
        }

        public virtual void OnClickHeld()
        {
        }

        public virtual void OnValidClick()
        {
        }

        public virtual void OnClickCancel()
        {
        }
    }
}
