using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine.Graphics;
using System.Collections.Generic;

namespace NesJamGame.Engine
{
    public class GameObject
    {
        Dictionary<string, Animation> Animations;
        string CurrentAnimation;
        Vector2 position;

        public GameObject(Vector2 position)
        {
            this.position = position;
            Animations = new Dictionary<string, Animation>();
        }

        public void AddAnimation(Animation animation, string name)
        {
            Animations.Add(name, animation);
            if (!Animations.ContainsKey(CurrentAnimation))
            {
                CurrentAnimation = name;
            }
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Animations[CurrentAnimation].Draw(spriteBatch, new Vector2((int)position.X, (int)position.Y));
        }
    }
}
