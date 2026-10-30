using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using SinewGod.SinewGodCode.Relics;

namespace SinewGod.SinewGodCode.Relics;

public class GreaterAuthoritySinew() : SinewGodRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<StrengthPower>(),
        HoverTipFactory.Static(StaticHoverTip.Energy)
    ];
    
    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        if (room is not CombatRoom)
            return;
        await PowerCmd.Apply<StrengthPower>(new ThrowingPlayerChoiceContext(), Owner.Creature,
            DynamicVars["StrengthPower"].BaseValue, Owner.Creature, null);
    }

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (target != Owner.Creature || result.UnblockedDamage <= 0)
            return;
        await PowerCmd.Apply<StrengthPower>(choiceContext, target, 1M, Owner.Creature, null);
    }

    public override decimal ModifyMaxEnergy(Player player, decimal amount)
    {
        return player != Owner ? amount : amount + 1;
    }
}