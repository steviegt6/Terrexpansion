using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent.Skies.CreditsRoll;
using Terraria.Localization;

namespace Terrexpansion.Common.UI
{
    public class TerrexpansionCreditsRollComposer
    {
        private struct SegmentInforReport
        {
            public int totalTime;
        }

        private int _endTime;
        private List<ICreditsRollSegment> _segments;

        public void FillSegments(List<ICreditsRollSegment> segmentsList, out int endTime, bool inGame)
        {
            _segments = segmentsList;
            int num = 0;
            int num2 = 80;
            Vector2 value = Vector2.UnitY * -1f * num2;
            int num3 = 210;
            Vector2 vector = value + Vector2.UnitX * 200f;
            int num4 = num3 * 3;
            int num5 = num3 * 3;
            int num6 = num4 - num5;

            if (!inGame)
            {
                num5 = 180;
                num6 = num4 - num5;
            }

            num += num5;
            num += PlaySegment_TextRoll(num, new string[] { "Terrexpansion Creator", "convicted tomatophile (Stevie)" }, vector).totalTime;
            num += num3;
            vector.X *= -1f;
            num += PlaySegment_TextRoll(num, new string[] { "Terrexpansion Designer", "convicted tomatophile (Stevie)", "Cloud (Herbert)", "Doodle", "Terra" }, vector).totalTime;
            num += num3;
            vector.X *= -1f;
            num += PlaySegment_TextRoll(num, new string[] { "Terrexpansion Programmers", "convicted tomatophile (Stevie)" }, vector).totalTime;
            num += num3;
            vector.X *= -1f;
            num += PlaySegment_TextRoll(num, new string[] { "Terrexpansion Graphics", "Cloud (Herbert)", "Doodle", "Terra", "RiverOaken" }, vector).totalTime;
            num += num3;
            vector.X *= 0f;
            num += PlaySegment_TextRoll(num, new string[] { "Special Thanks", "Libvaxy for not including itself", "Dradonhunter11 for 64bit", "The tML devs for 100% not making tML at all", "NuovaPrime for emotional support :sungla:", "pollen__ for pollenating Avalon" }, vector).totalTime;
            num += num6;
            _endTime = num + 10;
            endTime = _endTime;
        }

        private SegmentInforReport PlaySegment_TextRoll(int startTime, string[] sourceCategory, Vector2 anchorOffset = default)
        {
            anchorOffset.Y -= 40f;
            int num = 80;

            for (int i = 0; i < sourceCategory.Length; i++)
            {
                _segments.Add(new Segments.LocalizedTextSegment(startTime + i * num, Language.GetText(sourceCategory[i]), anchorOffset));
            }

            SegmentInforReport result = default;
            result.totalTime = sourceCategory.Length * num + num * -1;
            return result;
        }
    }
}