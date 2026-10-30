using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using SinewGod.SinewGodCode.Powers;

namespace SinewGod.SinewGodCode.Powers;

public class PlaguePower() : SinewGodPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override bool ShouldPowerBeRemovedAfterOwnerDeath() => true;

    protected override object? InitInternalData() => new Data();

    public override Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        Data internalData = GetInternalData<Data>();
        internalData.OwnerMaxHp = Owner.MaxHp;
        return Task.CompletedTask;
    }

    public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
    {
        if (creature != Owner) return;
        Data internalData = GetInternalData<Data>();
        decimal damage = internalData.OwnerMaxHp;
        IEnumerable<Creature> enemies = CombatState.HittableEnemies;
        await CreatureCmd.Damage(choiceContext, enemies, damage, ValueProp.Move,null, null, null);
    }

    private class Data
    {
        public int OwnerMaxHp;
    }
}