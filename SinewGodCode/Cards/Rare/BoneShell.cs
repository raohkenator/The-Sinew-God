using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using SinewGod.SinewGodCode.Cards;

namespace SinewGod.SinewGodCode.Cards.Rare;

public class BoneShell() : SinewGodCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<CrimsonMantlePower>(7M)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Block)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NPowerUpVfx.CreateNormal(Owner.Creature);
        await CreatureCmd.TriggerAnim(Owner.Creature, "PowerUp", Owner.Character.PowerUpAnimDelay);
        (await PowerCmd.Apply<CrimsonMantlePower>(choiceContext, Owner.Creature, this.DynamicVars["CrimsonMantlePower"].BaseValue, Owner.Creature, this))?.IncrementSelfDamage();
    }

    protected override void OnUpgrade() => this.DynamicVars["CrimsonMantlePower"].UpgradeValueBy(3M);
}