using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using SinewGod.SinewGodCode.Potions;

namespace SinewGod.SinewGodCode.Potions;

public class CrimsonVapor() : SinewGodPotion
{
    public override PotionRarity Rarity =>
        PotionRarity.Rare;

    public override PotionUsage Usage =>
        PotionUsage.CombatOnly;

    public override TargetType TargetType =>
        TargetType.AllEnemies;

    protected override async Task OnUse(
        PlayerChoiceContext choiceContext,
        Creature? target)
    {
        ICombatState? combatState = Owner.Creature.CombatState;
        await PowerCmd.Apply<VulnerablePower>(choiceContext, combatState!.HittableEnemies, 1M, Owner.Creature, null);
        await PowerCmd.Apply<WeakPower>(choiceContext, combatState!.HittableEnemies, 1M, Owner.Creature, null);
        await PowerCmd.Apply<EnfeeblingTouchPower>(choiceContext, combatState!.HittableEnemies, 5M, Owner.Creature, null);
    }
}
