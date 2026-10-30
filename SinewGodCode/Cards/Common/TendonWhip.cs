using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using SinewGod.SinewGodCode.Cards;

namespace SinewGod.SinewGodCode.Cards.Common;

public class TendonWhip() : SinewGodCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(9M, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(CombatState);
        await CreatureCmd.Damage(choiceContext, Owner.Creature, 1M, ValueProp.Unblockable, this, play);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this, play)
            .TargetingAllOpponents(CombatState)
            .Execute(choiceContext);

    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4M);
    }
}