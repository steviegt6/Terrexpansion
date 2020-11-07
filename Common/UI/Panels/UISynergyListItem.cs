using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Achievements;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terrexpansion.Common.UI.Panels
{
    public class UISynergyListItem : UIPanel
    {
        private Synergy _synergy;
        private UIImage _synergyIcon;
        private UIImage _synergyIconBorders;
        private Asset<Texture2D> _innerPanelTopTexture;
        private Asset<Texture2D> _innerPanelBottomTexture;
        private Asset<Texture2D> _categoryTexture;
        private bool _large;

        public UISynergyListItem(Synergy synergy, bool largeForOtherLanguages)
        {
            _large = largeForOtherLanguages;
            BackgroundColor = new Color(26, 40, 89) * 0.8f;
            BorderColor = new Color(13, 20, 44) * 0.8f;
            float num = 16 + _large.ToInt() * 20;
            float num2 = _large.ToInt() * 6;
            float num3 = _large.ToInt() * 12;
            _synergy = synergy;
            Height.Set(66f + num, 0f);
            Width.Set(0f, 1f);
            PaddingTop = 8f;
            PaddingLeft = 9f;
            _synergyIcon = new UIImage(ModContent.GetTexture("Terrexpansion/Content/Items/MysteryItem"));
            _synergyIcon.Left.Set(num2, 0f);
            _synergyIcon.Top.Set(num3, 0f);
            Append(_synergyIcon);
            _synergyIconBorders = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders"));
            _synergyIconBorders.Left.Set(-4f + num2, 0f);
            _synergyIconBorders.Top.Set(-4f + num3, 0f);
            Append(_synergyIconBorders);
            _innerPanelTopTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_InnerPanelTop");

            if (_large)
            {
                _innerPanelBottomTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_InnerPanelBottom_Large");
            }
            else
            {
                _innerPanelBottomTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_InnerPanelBottom");
            }

            _categoryTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Categories");
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            int num = _large.ToInt() * 6;
            Vector2 value = new Vector2(num, 0f);
            CalculatedStyle innerDimensions = GetInnerDimensions();
            CalculatedStyle dimensions = _synergyIconBorders.GetDimensions();
            float num2 = dimensions.X + dimensions.Width;
            Vector2 value2 = new Vector2(num2 + 7f, innerDimensions.Y);
            float num3 = innerDimensions.Width - dimensions.Width + 1f - (num * 2);
            Vector2 baseScale = new Vector2(0.85f);
            Vector2 baseScale2 = new Vector2(0.92f);
            string text = FontAssets.ItemStack.Value.CreateWrappedText(_synergy.Description, (num3 - 20f) * (1f / baseScale2.X));
            Vector2 stringSize = ChatManager.GetStringSize(FontAssets.ItemStack.Value, text, baseScale2, num3);

            if (!_large)
            {
                stringSize = ChatManager.GetStringSize(FontAssets.ItemStack.Value, _synergy.Description, baseScale2, num3);
            }

            float num4 = 38f + (float)(_large ? 20 : 0);

            if (stringSize.Y > num4)
            {
                baseScale2.Y *= num4 / stringSize.Y;
            }

            Color value3 = Color.Gold;
            value3 = Color.Lerp(value3, Color.White, base.IsMouseHovering ? 0.5f : 0f);
            Color value4 = Color.Silver;
            value4 = Color.Lerp(value4, Color.White, base.IsMouseHovering ? 1f : 0f);
            Color color = base.IsMouseHovering ? Color.White : Color.Gray;
            Vector2 vector = value2 - Vector2.UnitY * 2f + value;
            DrawPanelTop(spriteBatch, vector, num3, color);
            AchievementCategory category = _synergy.Category;
            vector.Y += 2f;
            vector.X += 4f;
            spriteBatch.Draw(_categoryTexture.Value, vector, _categoryTexture.Frame(4, 2, (int)category), base.IsMouseHovering ? Color.White : Color.Silver, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            vector.X += 4f;
            vector.X += 17f;

            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, _synergy.Name, vector, value3, 0f, Vector2.Zero, baseScale, num3);
            vector.X -= 17f;
            Vector2 position = value2 + Vector2.UnitY * 27f + value;
            DrawPanelBottom(spriteBatch, position, num3, color);
            position.X += 8f;
            position.Y += 4f;
            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, position, value4, 0f, Vector2.Zero, baseScale2);
        }

        private void DrawPanelTop(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
        {
            spriteBatch.Draw(_innerPanelTopTexture.Value, position, new Rectangle(0, 0, 2, _innerPanelTopTexture.Height()), color);
            spriteBatch.Draw(_innerPanelTopTexture.Value, new Vector2(position.X + 2f, position.Y), new Rectangle(2, 0, 2, _innerPanelTopTexture.Height()), color, 0f, Vector2.Zero, new Vector2((width - 4f) / 2f, 1f), SpriteEffects.None, 0f);
            spriteBatch.Draw(_innerPanelTopTexture.Value, new Vector2(position.X + width - 2f, position.Y), new Rectangle(4, 0, 2, _innerPanelTopTexture.Height()), color);
        }

        private void DrawPanelBottom(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
        {
            spriteBatch.Draw(_innerPanelBottomTexture.Value, position, new Rectangle(0, 0, 6, _innerPanelBottomTexture.Height()), color);
            spriteBatch.Draw(_innerPanelBottomTexture.Value, new Vector2(position.X + 6f, position.Y), new Rectangle(6, 0, 7, _innerPanelBottomTexture.Height()), color, 0f, Vector2.Zero, new Vector2((width - 12f) / 7f, 1f), SpriteEffects.None, 0f);
            spriteBatch.Draw(_innerPanelBottomTexture.Value, new Vector2(position.X + width - 6f, position.Y), new Rectangle(13, 0, 6, _innerPanelBottomTexture.Height()), color);
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);
            BackgroundColor = new Color(46, 60, 119);
            BorderColor = new Color(20, 30, 56);
        }

        public override void MouseOut(UIMouseEvent evt)
        {
            base.MouseOut(evt);
            BackgroundColor = new Color(26, 40, 89) * 0.8f;
            BorderColor = new Color(13, 20, 44) * 0.8f;
        }
    }
}
