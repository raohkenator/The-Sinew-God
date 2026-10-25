using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using SinewGod.SinewGodCode.Character;
using SinewGod.SinewGodCode.Extensions;

namespace SinewGod.SinewGodCode.Potions;

[Pool(typeof(SinewGodPotionPool))]
public abstract class SinewGodPotion : CustomPotionModel
{
    public override string? CustomPackedImagePath =>
        $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath();

    public override string? CustomPackedOutlinePath =>
        $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionOutlineImagePath();
}