using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using SinewGod.SinewGodCode.Cards;

namespace SinewGod.SinewGodCode.Cards.Rare;

public class VibeCheck() : SinewGodCard(4,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(20M, ValueProp.Move),
        new ("Debuff", 1M)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this, play)
            .Targeting(play.Target)
            .Execute(choiceContext);
        await PowerCmd.Apply<VulnerablePower>(choiceContext, play.Target, DynamicVars["Debuff"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<WeakPower>(choiceContext, play.Target, DynamicVars["Debuff"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<FrailPower>(choiceContext, play.Target, DynamicVars["Debuff"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<PoisonPower>(choiceContext, play.Target, DynamicVars["Debuff"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<DisintegrationPower>(choiceContext, play.Target, DynamicVars["Debuff"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<DemisePower>(choiceContext, play.Target, DynamicVars["Debuff"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<DoomPower>(choiceContext, play.Target, DynamicVars["Debuff"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<OblivionPower>(choiceContext, play.Target, DynamicVars["Debuff"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(10M);
        DynamicVars["Debuff"].UpgradeValueBy(1M);
    }
}