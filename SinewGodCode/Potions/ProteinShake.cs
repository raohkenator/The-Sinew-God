using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace SinewGod.SinewGodCode.Potions;

public class ProteinShake() : SinewGodPotion
{
    public override PotionRarity Rarity =>
        PotionRarity.Common;

    public override PotionUsage Usage =>
        PotionUsage.CombatOnly;

    public override TargetType TargetType =>
        TargetType.AnyPlayer;

    protected override async Task OnUse(
        PlayerChoiceContext choiceContext,
        Creature? target)
    {
        ArgumentNullException.ThrowIfNull(target);
        await PowerCmd.Apply<StrengthPower>(choiceContext, target, 1M, Owner.Creature, null);
        await PlayerCmd.GainEnergy(1M, target.Player!);
    }
}
