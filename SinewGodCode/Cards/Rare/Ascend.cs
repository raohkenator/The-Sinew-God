using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using SinewGod.SinewGodCode.Cards;

namespace SinewGod.SinewGodCode.Cards.Rare;

public class Ascend() : SinewGodCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips => IsUpgraded
        ? [HoverTipFactory.FromPower<StrengthPower>(), HoverTipFactory.FromPower<RitualPower>()]
        : [HoverTipFactory.FromPower<RitualPower>()];
    

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (IsUpgraded)
            await PowerCmd.Apply<StrengthPower>(choiceContext, Owner.Creature, 1M, Owner.Creature, this);
        await PowerCmd.Apply<RitualPower>(choiceContext, Owner.Creature, 1M, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        
    }
}