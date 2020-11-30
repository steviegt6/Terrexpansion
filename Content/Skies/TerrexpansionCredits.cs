using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Skies.CreditsRoll;
using Terraria.Graphics.Effects;
using Terrexpansion.Content.UI;

namespace Terrexpansion.Content.Skies
{
    public class TerrexpansionCredits : CustomSky
    {
        private bool _isActive, _wantsToBeSeen;
        private float _opacity;
        private int _endTime, _currentTime;
        private readonly TerrexpansionCreditsRollComposer _composer = new TerrexpansionCreditsRollComposer();
        private readonly List<ICreditsRollSegment> _segmentsInMainMenu = new List<ICreditsRollSegment>();

        public int AmountOfTimeNeededForFullPlay => _endTime;

        public TerrexpansionCredits()
        {
            EnsureSegmentsAreMade();
        }

        public override void Update(GameTime gameTime)
        {
            _currentTime++;

            _opacity = MathHelper.Clamp(_opacity + 71f / (339f * (float)Math.PI) * _wantsToBeSeen.ToDirectionInt(), 0f, 1f);

            if (_opacity == 0f && !_wantsToBeSeen)
            {
                _isActive = false;
                return;
            }

            if (!(Main.CanPlayCreditsRoll() && _currentTime >= _endTime))
                SkyManager.Instance.Deactivate("Terrexpansion:Credits");
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (!(1f < minDepth) && !(1f > maxDepth))
            {
                Vector2 anchorPositionOnScreen = new Vector2(Main.ScreenSize.ToVector2().X / 2f, 300f);

                CreditsRollInfo info = new CreditsRollInfo()
                {
                    SpriteBatch = spriteBatch,
                    AnchorPositionOnScreen = anchorPositionOnScreen,
                    TimeInAnimation = _currentTime,
                    DisplayOpacity = _opacity
                };

                for (int i = 0; i < _segmentsInMainMenu.Count; i++)
                    _segmentsInMainMenu[i].Draw(ref info);
            }
        }

        public override bool IsActive() => _isActive;

        public override void Reset()
        {
            _currentTime = 0;

            EnsureSegmentsAreMade();

            _isActive = false;
            _wantsToBeSeen = false;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            _isActive = true;
            _wantsToBeSeen = true;

            if (_opacity == 0f)
            {
                EnsureSegmentsAreMade();

                _currentTime = 0;
            }
        }

        private void EnsureSegmentsAreMade()
        {
            if (_segmentsInMainMenu.Count <= 0)
            {
                _segmentsInMainMenu.Clear();
                _composer.FillSegments(_segmentsInMainMenu, out _endTime, inGame: false);
            }
        }

        public override void Deactivate(params object[] args) => _wantsToBeSeen = false;
    }
}