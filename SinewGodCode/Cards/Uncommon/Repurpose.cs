using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using SinewGod.SinewGodCode.Cards;
using SinewGod.SinewGodCode.Powers;

namespace SinewGod.SinewGodCode.Cards.Uncommon;

public class Repurpose() : SinewGodCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<RupturePower>(1M)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<RupturePower>(choiceContext, Owner.Creature, DynamicVars["RupturePower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["RupturePower"].UpgradeValueBy(1M);
    }
}