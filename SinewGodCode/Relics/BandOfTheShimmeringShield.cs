using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using SinewGod.SinewGodCode.Relics;

namespace SinewGod.SinewGodCode.Relics;

public class BandOfTheShimmeringShield() : SinewGodRelic
{
    private bool _anySkillsPlayedLastTurn;
    private bool _anySkillsPlayedThisTurn;
    
    public override RelicRarity Rarity => RelicRarity.Uncommon;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(4M, ValueProp.Unpowered)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Block)];

    private bool AnySkillsPlayedLastTurn
    {
        get => _anySkillsPlayedLastTurn;
        set
        {
            AssertMutable();
            _anySkillsPlayedLastTurn = value;
        }
    }

    private bool AnySkillsPlayedThisTurn
    {
        get => _anySkillsPlayedThisTurn;
        set
        {
            AssertMutable();
            _anySkillsPlayedThisTurn = value;
        }
    }

    public override Task AfterRoomEntered(AbstractRoom room)
    {
        if (room is not CombatRoom)
            return Task.CompletedTask;
        Status = RelicStatus.Active;
        return Task.CompletedTask;
    }
    
    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner != cardPlay.Card.Owner || !CombatManager.Instance.IsInProgress || cardPlay.Card.Type != CardType.Skill || AnySkillsPlayedLastTurn)
            return Task.CompletedTask;
        Status = RelicStatus.Normal;
        AnySkillsPlayedThisTurn = true;
        return Task.CompletedTask;
    }

    public override async Task BeforeSideTurnEnd(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner.Creature))
            return;
        AnySkillsPlayedLastTurn = AnySkillsPlayedThisTurn;
        if (!AnySkillsPlayedThisTurn)
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, null);
        AnySkillsPlayedThisTurn = false;
    }
    
    public override Task AfterCombatEnd(CombatRoom _)
    {
        Status = RelicStatus.Normal;
        AnySkillsPlayedLastTurn = false;
        AnySkillsPlayedThisTurn = false;
        return Task.CompletedTask;
    }
}