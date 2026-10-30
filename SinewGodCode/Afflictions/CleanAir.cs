using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace SinewGod.SinewGodCode.Afflictions;

public sealed class CleanAir : AfflictionModel
{
    public override bool CanAfflictCardType(CardType cardType) => cardType == CardType.Skill;
}