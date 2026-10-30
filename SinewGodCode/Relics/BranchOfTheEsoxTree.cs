using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using SinewGod.SinewGodCode.Relics;

namespace SinewGod.SinewGodCode.Relics;

public class BranchOfTheEsoxTree() : SinewGodRelic
{
    private bool _wasUsedThisCombat;
    
    public override RelicRarity Rarity => RelicRarity.Rare;

    public bool WasUsedThisCombat
    {
        get => _wasUsedThisCombat;
        set
        {
            AssertMutable();
            _wasUsedThisCombat = value;
        }
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromKeyword(CardKeyword.Exhaust)];
    
    public override Task AfterRoomEntered(AbstractRoom room)
    {
        if (!(room is CombatRoom))
            return Task.CompletedTask;
        WasUsedThisCombat = false;
        this.Status = RelicStatus.Active;
        return Task.CompletedTask;
    }
    public override async Task AfterCardExhausted(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool causedByEthereal)
    {
        if (card.Owner != Owner || WasUsedThisCombat || card.Type != CardType.Attack)
            return;
        Flash();
        CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card.CreateClone(), PileType.Hand, this.Owner);
        Status = RelicStatus.Normal;
        WasUsedThisCombat = true;
    }
    
    public override Task AfterCombatEnd(CombatRoom _)
    {
        WasUsedThisCombat = false;
        Status = RelicStatus.Normal;
        return Task.CompletedTask;
    }
}