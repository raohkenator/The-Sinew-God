using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using SinewGod.SinewGodCode.Afflictions;
using SinewGod.SinewGodCode.Powers;

namespace SinewGod.SinewGodCode.Powers;

public class CleanAirPower() : SinewGodPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        foreach (CardModel card in Owner.Player!.PlayerCombatState!.AllCards)
        {
            if (card.Type != CardType.Skill)
                return;
            if (card.Affliction != null && card.Affliction is not CleanAir)
                CardCmd.ClearAffliction(card);
            await CardCmd.Afflict<CleanAir>(card, 1M);
        }
    }

    public override async Task AfterCardEnteredCombat(CardModel card)
    {
        if (card.Owner != Owner.Player || card.Type != CardType.Skill)
            return;
        if (card.Affliction != null && card.Affliction is not CleanAir)
            CardCmd.ClearAffliction(card);
        await CardCmd.Afflict<CleanAir>(card, 1M);
    }
    
    public override Task AfterRemoved(Creature oldOwner)
    {
        Player? player = oldOwner.Player;
        object? obj;
        if (player == null)
        {
            obj = null;
        }
        else
        {
            PlayerCombatState? playerCombatState = player.PlayerCombatState;
            obj = playerCombatState != null ? (object) playerCombatState.AllCards : null;
        }
        if (obj == null)
            obj = Array.Empty<CardModel>();
        foreach (CardModel card in (IEnumerable<CardModel>) obj)
        {
            if (card.Affliction is CleanAir)
                CardCmd.ClearAffliction(card);
        }
        return Task.CompletedTask;
    }
}