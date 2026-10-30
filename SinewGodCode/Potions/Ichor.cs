using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using SinewGod.SinewGodCode.Potions;

namespace SinewGod.SinewGodCode.Potions;

public class Ichor() : SinewGodPotion
{
    public override PotionRarity Rarity =>
        PotionRarity.Uncommon;

    public override PotionUsage Usage =>
        PotionUsage.CombatOnly;

    public override TargetType TargetType =>
        TargetType.AnyPlayer;

    protected override async Task OnUse(
        PlayerChoiceContext choiceContext,
        Creature? target)
    {
        ArgumentNullException.ThrowIfNull(target);
        await PowerCmd.Apply<RegenPower>(choiceContext, target, 2M, Owner.Creature, null);
        await CreatureCmd.GainMaxHp(target, 2M);
    }
}
