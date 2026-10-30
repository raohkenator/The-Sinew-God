using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using SinewGod.SinewGodCode.Powers;

namespace SinewGod.SinewGodCode.Powers;

public class TirelessPower() : SinewGodPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Energy)];

    public override async Task AfterEnergySpent(CardModel card, int amount)
    {
        if (card.Owner != Owner.Player || amount < 3)
            return;
        await PlayerCmd.GainEnergy(Amount, Owner.Player);
    }
}