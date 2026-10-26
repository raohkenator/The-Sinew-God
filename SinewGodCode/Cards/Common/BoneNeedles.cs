using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using SinewGod.SinewGodCode.Cards;

namespace SinewGod.SinewGodCode.Cards.Common;

public class BoneNeedles() : SinewGodCard(0,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(1, ValueProp.Move),
        new RepeatVar(2)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .WithHitCount(DynamicVars.Repeat.IntValue)
            .FromCard(this, play)
            .Targeting(play.Target)
            .Execute(choiceContext);
        IEnumerable<BoneNeedles> boneNeedles = Owner.PlayerCombatState!.AllCards.OfType<BoneNeedles>();
        foreach (BoneNeedles needle in boneNeedles)
            needle.BuffFromCardPlay();
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Repeat.UpgradeValueBy(1M);
    }

    private void BuffFromCardPlay()
    {
        RepeatVar repeat = DynamicVars.Repeat;
        repeat.BaseValue += 1;
    }
}