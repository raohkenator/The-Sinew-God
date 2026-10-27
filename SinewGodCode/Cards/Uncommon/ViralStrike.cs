using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using SinewGod.SinewGodCode.Cards;

namespace SinewGod.SinewGodCode.Cards.Uncommon;

public class ViralStrike() : SinewGodCard(0,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(4M, ValueProp.Move),
        new PowerVar<PoisonPower>(2M)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this, play)
            .Targeting(play.Target)
            .Execute(choiceContext);
        
        await PowerCmd.Apply<PoisonPower>(choiceContext, play.Target, DynamicVars["PoisonPower"].BaseValue, Owner.Creature, this);
        
        CardModel? card = (await CardSelectCmd.FromCombatPile(choiceContext, PileType.Discard.GetPile(Owner), Owner, new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, 1), ValidOptions)).FirstOrDefault();
        if (card == null) return;
        var cardPileAddResult = await CardCmd.TransformTo<ViralStrike>(card);
        if (!IsUpgraded) return;
        CardModel viralStrike = cardPileAddResult!.Value.cardAdded;
        CardCmd.Upgrade(viralStrike);
    }

    private bool ValidOptions(CardModel card) => IsUpgraded ? (card is ViralStrike && !card.IsUpgraded) || card is not ViralStrike : card is not ViralStrike;

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2M);
        DynamicVars["PoisonPower"].UpgradeValueBy(1M);
    }
}