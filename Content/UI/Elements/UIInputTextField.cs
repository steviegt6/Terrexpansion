using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace Terrexpansion.Content.UI.Elements
{
    public class UIInputTextField : UIElement
    {
        private readonly string _hintText;
        private string _currentString = string.Empty;
        private int _textBlinkerCount;

        public string Text
        {
            get => _currentString;
            set
            {
                if (_currentString != value)
                {
                    _currentString = value;
                    OnTextChange?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public delegate void EventHandler(object sender, EventArgs e);

        public event EventHandler OnTextChange;

        public UIInputTextField(string hintText)
        {
            _hintText = hintText;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            PlayerInput.WritingText = true;

            Main.instance.HandleIME();

            string newString = Main.GetInputText(_currentString);

            if (newString != _currentString)
            {
                _currentString = newString;

                OnTextChange?.Invoke(this, EventArgs.Empty);
            }

            CalculatedStyle space = GetDimensions();

            Utils.DrawBorderString(spriteBatch, _currentString.Length == 0 ? _hintText : _currentString + (++_textBlinkerCount / 20 % 2 == 0 ? "|" : ""), new Vector2(GetDimensions().X, GetDimensions().Y), _currentString.Length == 0 ? Color.Gray : Color.White);
        }
    }
}