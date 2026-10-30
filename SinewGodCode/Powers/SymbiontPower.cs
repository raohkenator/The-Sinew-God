using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.DevConsole.ConsoleCommands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;
using SinewGod.SinewGodCode.Powers;

namespace SinewGod.SinewGodCode.Powers;

public class SymbiontPower() : SinewGodPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Block)];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new StringVar("Parasitizing")];

    protected override object? InitInternalData() => new Data();

    public void AddParasitizedCreature(Creature c)
    {
        List<Creature> parasitizedCreatures = GetInternalData<Data>().ParasitizedCreatures;
        if (!GetInternalData<Data>().ParasitizedCreatures.Contains(c))
            parasitizedCreatures.Add(c);
        StringVar dynamicVar = (StringVar)DynamicVars["Parasitizing"];
        dynamicVar.StringValue = "";
        for (int index = 0; index < parasitizedCreatures.Count; index++)
        {
            dynamicVar.StringValue += PlatformUtil.GetPlayerName(RunManager.Instance.NetService.Platform, parasitizedCreatures[index].Player!.NetId);
            if (index == parasitizedCreatures.Count - 2)
                dynamicVar.StringValue += ", and ";
            if (index < parasitizedCreatures.Count - 2)
                dynamicVar.StringValue += ", ";
        }

    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource, CardPlay? cardPlay)
    {
        return target != Owner || !props.IsPoweredAttack() ? 1M : GetInternalData<Data>().ParasitizedCreatures.Count + 1;
    }

    public override async Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, CardModel? cardSource)
    {
        if (!GetInternalData<Data>().ParasitizedCreatures.Contains(creature))
            return;
        await CreatureCmd.LoseBlock(new ThrowingPlayerChoiceContext(), creature, amount, null);
        await CreatureCmd.Heal(Owner, amount / 2);
    }

    public override LocString Title
    {
        get
        {
            var titleLoc = base.Title;
            DynamicVars.AddTo(titleLoc);
            return titleLoc;
        }
    }

    private class Data
    {
        public readonly List<Creature> ParasitizedCreatures = new List<Creature>();
    }
}