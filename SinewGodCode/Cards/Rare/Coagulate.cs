using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using SinewGod.SinewGodCode.Cards;

namespace SinewGod.SinewGodCode.Cards.Rare;

public class Coagulate() : SinewGodCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var dexCount = Owner.Creature.GetPowerAmount<RegenPower>() / (IsUpgraded ? 1 : 2);
        await PowerCmd.Apply<DexterityPower>(choiceContext, Owner.Creature, dexCount, Owner.Creature, this);
        await PowerCmd.Remove<RegenPower>(Owner.Creature);
    }
}