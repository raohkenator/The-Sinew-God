using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using SinewGod.SinewGodCode.Cards;

namespace SinewGod.SinewGodCode.Cards.Rare;

public class FrightfulPresence() : SinewGodCard(0,
    CardType.Skill, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    public override TargetType TargetType => IsUpgraded ? TargetType.AllEnemies : TargetType.AnyEnemy;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<VulnerablePower>()];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (!IsUpgraded)
        {
            if (play.Target == null) return;
            await PowerCmd.Apply<VulnerablePower>(choiceContext, play.Target, 99M, Owner.Creature, this);
        }
        else
        {
            await PowerCmd.Apply<VulnerablePower>(choiceContext, CombatState?.HittableEnemies, 99M, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        
    }
    
}