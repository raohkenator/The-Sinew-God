using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using SinewGod.SinewGodCode.Cards;

namespace SinewGod.SinewGodCode.Cards.Common;

public class Infect() : SinewGodCard(2,
    CardType.Skill, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("Debuff", 5M)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await PowerCmd.Apply<WeakPower>(choiceContext, play.Target, 1M, Owner.Creature, this);
        await PowerCmd.Apply<VulnerablePower>(choiceContext, play.Target, 1M, Owner.Creature, this);
        await PowerCmd.Apply<PoisonPower>(choiceContext, play.Target, DynamicVars["Debuff"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<DoomPower>(choiceContext, play.Target, DynamicVars["Debuff"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Debuff"].UpgradeValueBy(4M);
    }
}