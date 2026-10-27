using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using SinewGod.SinewGodCode.Cards;

namespace SinewGod.SinewGodCode.Cards.Rare;

public class Pandemic() : SinewGodCard(2,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<NoxiousFumesPower>(2M),
        new PowerVar<AccelerantPower>(1M)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<NoxiousFumesPower>(choiceContext, Owner.Creature, DynamicVars["NoxiousFumesPower"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<AccelerantPower>(choiceContext, Owner.Creature, DynamicVars["AccelerantPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["NoxiousFumesPower"].UpgradeValueBy(1);
        DynamicVars["AccelerantPower"].UpgradeValueBy(1);
    }
}