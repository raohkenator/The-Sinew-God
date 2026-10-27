using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using SinewGod.SinewGodCode.Cards;

namespace SinewGod.SinewGodCode.Cards.Rare;

public class Mimicry() : SinewGodCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.AnyAlly)
{
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ImitationLearningPower>(2M)];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        ArgumentNullException.ThrowIfNull(play.Target.Player);
        ImitationLearningPower? power = Owner.Creature.Powers.OfType<ImitationLearningPower>().FirstOrDefault(s => s.PlayerTarget == play.Target.Player);
        decimal baseValue = DynamicVars["ImitationLearningPower"].BaseValue;
        if (power != null)
        {
            await PowerCmd.ModifyAmount(choiceContext, power, baseValue, Owner.Creature, this);
        }
        else
        {
            ImitationLearningPower? appliedPower = await PowerCmd.Apply<ImitationLearningPower>(choiceContext, Owner.Creature, baseValue, Owner.Creature, this);
            if (appliedPower != null)
                appliedPower.PlayerTarget = play.Target.Player;
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ImitationLearningPower"].UpgradeValueBy(1M);
    }
}