using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Random;
using SinewGod.SinewGodCode.Cards;

namespace SinewGod.SinewGodCode.Cards.Uncommon;

public class Shapeshift() : SinewGodCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (IsUpgraded)
            await CardPileCmd.Draw(choiceContext, 2M, Owner);
        int hand = Owner.PlayerCombatState!.Hand.Cards.Count;
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, 0, hand);
        IEnumerable<CardModel> cards = (await CardSelectCmd.FromCombatPile(choiceContext, PileType.Hand.GetPile(Owner), Owner, prefs)).ToList();
        foreach (CardModel card in cards)
            await CardCmd.TransformToRandom(card, Owner.RunState.Rng.CombatCardGeneration, CardPreviewStyle.MessyLayout);
    }
}