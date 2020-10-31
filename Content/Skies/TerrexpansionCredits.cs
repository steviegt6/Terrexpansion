using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Skies.CreditsRoll;
using Terraria.Graphics.Effects;
using Terrexpansion.Common.UI;

namespace Terrexpansion.Content.Skies
{
    public class TerrexpansionCredits : CustomSky
    {
        private int _endTime;
        private int _currentTime;
        private TerrexpansionCreditsRollComposer _composer = new TerrexpansionCreditsRollComposer();
        private List<ICreditsRollSegment> _segmentsInGame = new List<ICreditsRollSegment>();
        private List<ICreditsRollSegment> _segmentsInMainMenu = new List<ICreditsRollSegment>();
        private bool _isActive;
        private bool _wantsToBeSeen;
        private float _opacity;

        public int AmountOfTimeNeededForFullPlay => _endTime;

        public TerrexpansionCredits()
        {
            EnsureSegmentsAreMade();
        }

        public override void Update(GameTime gameTime)
        {
            _currentTime++;
            float num = 0.008333334f;
            if (Main.gameMenu)
            {
                num = 71f / (339f * (float)Math.PI);
            }

            _opacity = MathHelper.Clamp(_opacity + num * _wantsToBeSeen.ToDirectionInt(), 0f, 1f);
            if (_opacity == 0f && !_wantsToBeSeen)
            {
                _isActive = false;
                return;
            }

            bool flag = true;
            if (!Main.CanPlayCreditsRoll())
            {
                flag = false;
            }

            if (_currentTime >= _endTime)
            {
                flag = false;
            }

            if (!flag)
            {
                SkyManager.Instance.Deactivate("Terrexpansion:Credits");
            }
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            float num = 1f;
            if (!(num < minDepth) && !(num > maxDepth))
            {
                Vector2 anchorPositionOnScreen = Main.ScreenSize.ToVector2() / 2f;
                if (Main.gameMenu)
                {
                    anchorPositionOnScreen.Y = 300f;
                }

                CreditsRollInfo creditsRollInfo = default;
                creditsRollInfo.SpriteBatch = spriteBatch;
                creditsRollInfo.AnchorPositionOnScreen = anchorPositionOnScreen;
                creditsRollInfo.TimeInAnimation = _currentTime;
                creditsRollInfo.DisplayOpacity = _opacity;
                CreditsRollInfo info = creditsRollInfo;
                List<ICreditsRollSegment> list = _segmentsInGame;
                if (Main.gameMenu)
                {
                    list = _segmentsInMainMenu;
                }

                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Draw(ref info);
                }
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
            if (_segmentsInMainMenu.Count <= 0 || _segmentsInGame.Count <= 0)
            {
                _segmentsInGame.Clear();
                _composer.FillSegments(_segmentsInGame, out _endTime, inGame: true);
                _segmentsInMainMenu.Clear();
                _composer.FillSegments(_segmentsInMainMenu, out _endTime, inGame: false);
            }
        }

        public override void Deactivate(params object[] args)
        {
            _wantsToBeSeen = false;
        }
    }
}