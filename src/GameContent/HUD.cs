using Microsoft.Xna.Framework.Graphics;

namespace NesJamGame.GameContent
{
    public class HUD
    {
        SettingsButton settingsButton;

        public HUD()
        {
            settingsButton = new SettingsButton();
        }

        public void Update()
        {
            settingsButton.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            settingsButton.Draw(spriteBatch);
        }
    }
}
