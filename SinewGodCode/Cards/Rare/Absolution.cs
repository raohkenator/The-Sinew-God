using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using SinewGod.SinewGodCode.Cards;

namespace SinewGod.SinewGodCode.Cards.Rare;

public class Absolution() : SinewGodCard(4,
    CardType.Skill, CardRarity.Rare,
    TargetType.AllEnemies)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        List<Creature> enemies = Owner.Creature.CombatState!.Enemies.Where(static enemy => enemy.IsAlive).ToList();
        if (enemies.Count > 0)
        {
            foreach (Creature enemy in enemies)
            {
                List<PowerModel> powers = enemy.Powers
                    .Where(static power => power.GetTypeForAmount(power.Amount) == PowerType.Buff)
                    .ToList();
                foreach (PowerModel power in powers)
                    await PowerCmd.Remove(power);
            }
        }

        List<PowerModel> powers1 = Owner.Creature.Powers
            .Where(static power => power.GetTypeForAmount(power.Amount) == PowerType.Debuff).ToList();
        if (powers1.Count > 0)
        {
            foreach (PowerModel power in powers1)
                await PowerCmd.Remove(power);
        }
        List<CardModel> cards = (Owner.PlayerCombatState!.AllCards.Where(c => c.Type is CardType.Curse or CardType.Status)).ToList();
        if (cards.Count == 0)
            return;
        foreach (CardModel card in cards)
            await CardCmd.Exhaust(choiceContext, card);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}