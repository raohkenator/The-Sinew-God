using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using SinewGod.SinewGodCode.Powers;

namespace SinewGod.SinewGodCode.Powers;

public class ClearAirPower() : SinewGodPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        foreach (CardModel card in Owner.Player!.PlayerCombatState!.AllCards)
        {
            if (card.Type != CardType.Skill || card.Affliction == null)
                continue;
            CardCmd.ClearAffliction(card);
        }
        return Task.CompletedTask;
    }

    public override Task AfterCardEnteredCombat(CardModel card)
    {
        if (card.Owner != Owner.Player || card.Type != CardType.Skill && card.Affliction != null)
            return Task.CompletedTask;
        CardCmd.ClearAffliction(card);
        return Task.CompletedTask;
    }
    
    public override bool ShouldAfflict(CardModel card, AfflictionModel affliction)
    {
        return card.Owner != Owner.Player || card.Type != CardType.Skill;
    }
}