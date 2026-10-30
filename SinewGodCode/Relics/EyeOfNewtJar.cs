using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using SinewGod.SinewGodCode.Relics;

namespace SinewGod.SinewGodCode.Relics;

public class EyeOfNewtJar() : SinewGodRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<RegenPower>(1M)];

    public override async  Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (target != Owner.Creature || result.UnblockedDamage <= 0)
            return;
        Flash();
        await PowerCmd.Apply<RegenPower>(choiceContext, Owner.Creature, DynamicVars["RegenPower"].BaseValue, Owner.Creature, null);
    }
}