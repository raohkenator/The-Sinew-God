using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using SinewGod.SinewGodCode.Relics;

namespace SinewGod.SinewGodCode.Relics;

public class OrgresEnduranceBelt() : SinewGodRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new EnergyVar(3),
        new BlockVar(10M, ValueProp.Unpowered)
    ];

    public override async Task AfterEnergySpent(CardModel card, int amount)
    {
        if (card.Owner != Owner || amount < 3)
            return;
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, null);
    }
}