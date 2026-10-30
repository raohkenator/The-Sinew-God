using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using SinewGod.SinewGodCode.Cards;
using SinewGod.SinewGodCode.Powers;

namespace SinewGod.SinewGodCode.Cards.Rare;

public class Enlarge() : SinewGodCard(3,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            if(IsUpgraded)
                yield return HoverTipFactory.FromPower<StrengthPower>();
            yield return HoverTipFactory.FromPower<GigantificationPower>();
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (IsUpgraded)
            await PowerCmd.Apply<StrengthPower>(choiceContext, Owner.Creature, 3M, Owner.Creature, this);
        await PowerCmd.Apply<EnlargePower>(choiceContext, Owner.Creature, 1M, Owner.Creature, this);
    }
}