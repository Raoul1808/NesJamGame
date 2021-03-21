using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.Input;
using System.Collections.Generic;

namespace NesJamGame.Engine
{
    public abstract class Button
    {
        public List<Sprite> sprites;
        public Point position;
        protected int currentSprite = 0;
        bool pressed;

        public virtual void Update()
        {
            Point mousePos = InputManager.GetMousePos();
            if (sprites[currentSprite].rectangle.Contains(mousePos))
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
                sprites[currentSprite].color = Color.White;
                OnClickCancel();
            }
            else
            {
                OnIdle();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprites[currentSprite].Draw(spriteBatch, position);
        }

        public virtual void OnIdle()
        {

        }

        public virtual void OnHover()
        {
            sprites[currentSprite].color = Color.LightGray;
        }

        public virtual void OnStartClick()
        {
            sprites[currentSprite].color = Color.Gray;
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
