using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
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
            Vector2 vector2 = vector;

            if (!inGame)
            {
                vector2 = vector = Vector2.UnitY * 80f;
            }
            int num4 = num3 * 3;
            int num5 = num3 * 3;
            int num6 = num4 - num5;

            if (!inGame)
            {
                num5 = 180;
                num6 = num4 - num5;
            }

            num += num5;
            num += PlaySegment_TextRoll(num, "Mods.Terrexpansion.Credits_Creator", vector).totalTime;
            num += num3;
            vector.X *= -1f;
            num += PlaySegment_TextRoll(num, "Mods.Terrexpansion.Credits_Designers", vector).totalTime;
            num += num3;
            vector.X *= -1f;
            num += PlaySegment_TextRoll(num, "Mods.Terrexpansion.Credits_Programmers", vector).totalTime;
            num += num3;
            vector.X *= -1f;
            num += PlaySegment_TextRoll(num, "Mods.Terrexpansion.Credits_Graphics", vector).totalTime;
            num += num3;
            vector.X *= 0f;
            num += PlaySegment_TextRoll(num, "Mods.Terrexpansion.Credits_Special", vector).totalTime;
            num += num6;
            _endTime = num + 10;
            endTime = _endTime;
        }

        private SegmentInforReport PlaySegment_TextRoll(int startTime, string sourceCategory, Vector2 anchorOffset = default)
        {
            anchorOffset.Y -= 40f;
            int num = 80;
            LocalizedText[] array = Language.FindAll(Lang.CreateDialogFilter(sourceCategory + ".", null));
            for (int i = 0; i < array.Length; i++)
            {
                _segments.Add(new Segments.LocalizedTextSegment(startTime + i * num, array[i], anchorOffset));
            }

            SegmentInforReport result = default;
            result.totalTime = array.Length * num + num * -1;
            return result;
        }
    }
}