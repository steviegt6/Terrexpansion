using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Skies.CreditsRoll;
using Terraria.Localization;

namespace Terrexpansion.Content.UI
{
    public class TerrexpansionCreditsRollComposer
    {
        private List<ICreditsRollSegment> _segments;

        public void FillSegments(List<ICreditsRollSegment> segmentsList, out int endTime, bool inGame)
        {
            Vector2 anchorOffset = (Vector2.UnitY * -1f * 80) + Vector2.UnitX * 200f;
            int realEndTime = 0;
            int endTimePadding = 210;
            int startingTimePadding = endTimePadding * 3;
            _segments = segmentsList;

            realEndTime += startingTimePadding;
            realEndTime += PlaySegment_TextRoll(realEndTime, "Mods.Terrexpansion.Credits_Creator", anchorOffset);
            realEndTime += endTimePadding;
            anchorOffset.X *= -1f;
            realEndTime += PlaySegment_TextRoll(realEndTime, "Mods.Terrexpansion.Credits_Designers", anchorOffset);
            realEndTime += endTimePadding;
            anchorOffset.X *= -1f;
            realEndTime += PlaySegment_TextRoll(realEndTime, "Mods.Terrexpansion.Credits_Programmers", anchorOffset);
            realEndTime += endTimePadding;
            anchorOffset.X *= -1f;
            realEndTime += PlaySegment_TextRoll(realEndTime, "Mods.Terrexpansion.Credits_Graphics", anchorOffset);
            realEndTime += endTimePadding;
            anchorOffset.X *= 0f;
            realEndTime += PlaySegment_TextRoll(realEndTime, "Mods.Terrexpansion.Credits_Special", anchorOffset);
            realEndTime += (endTimePadding * 3) - startingTimePadding;
            endTime = realEndTime + 10;
        }

        private int PlaySegment_TextRoll(int startTime, string sourceCategory, Vector2 anchorOffset = default)
        {
            anchorOffset.Y -= 40f;
            int timeMultiplier = 80;
            LocalizedText[] sourceCategoryArray = Language.FindAll(Lang.CreateDialogFilter(sourceCategory + ".", null));

            for (int i = 0; i < sourceCategoryArray.Length; i++)
                _segments.Add(new Segments.LocalizedTextSegment(startTime + i * timeMultiplier, sourceCategoryArray[i], anchorOffset));

            return sourceCategoryArray.Length * timeMultiplier + timeMultiplier * -1;
        }
    }
}