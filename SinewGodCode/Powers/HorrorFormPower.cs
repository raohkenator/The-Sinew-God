using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using SinewGod.SinewGodCode.Powers;

namespace SinewGod.SinewGodCode.Powers;

public class HorrorFormPower() : SinewGodPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player) return;

        CardSelectorPrefs prefs = new CardSelectorPrefs(SelectionScreenPrompt, Amount)
        {
            PretendCardsCanBePlayed = true
        };
        IEnumerable<CardModel> cards = (await CardSelectCmd.FromCombatPile(choiceContext, PileType.Discard.GetPile(Owner.Player), Owner.Player, prefs)).ToList();
        if (!cards.Any()) return;
        Flash();
        foreach (CardModel card in cards)
        {
            await CardCmd.AutoPlay(choiceContext, card, null);
        }
    }
}