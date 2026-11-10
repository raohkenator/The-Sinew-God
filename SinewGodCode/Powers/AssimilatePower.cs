using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using SinewGod.SinewGodCode.Powers;

namespace SinewGod.SinewGodCode.Powers;

public class AssimilatePower() : SinewGodPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(3)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PoisonPower>(),
        HoverTipFactory.Static(StaticHoverTip.Energy)
    ];

    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(Owner.CombatState);
        if (cardPlay.Player != Owner.Player || cardPlay.Card.EnergyCost.GetResolved() < 3)
            return;
        await PowerCmd.Apply<PoisonPower>(new ThrowingPlayerChoiceContext(), Owner.CombatState.Enemies.Where(e => e.IsAlive), Amount, Owner, null);
    }
}