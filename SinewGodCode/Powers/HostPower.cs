using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Encounters.Mocks;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;
using SinewGod.SinewGodCode.Powers;

namespace SinewGod.SinewGodCode.Powers;

public class HostPower() : SinewGodPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Block)];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new StringVar("Applier"),];

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        ((StringVar) DynamicVars["Applier"]).StringValue = PlatformUtil.GetPlayerName(RunManager.Instance.NetService.Platform, Applier!.Player!.NetId);
        SymbiontPower? symbiontPower = Applier.GetPower<SymbiontPower>();
        if (symbiontPower == null)
            symbiontPower = await PowerCmd.Apply<SymbiontPower>(new ThrowingPlayerChoiceContext(), Applier, 1M, Owner, null);
        symbiontPower!.AddParasitizedCreature(Owner);
    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource, CardPlay? cardPlay)
    {
        return target != Owner || !props.IsPoweredAttack() ? 1M : 0M;
    }

    public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
    {
        if (wasRemovalPrevented || creature != Applier)
            return;
        await PowerCmd.Remove(this);
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
}