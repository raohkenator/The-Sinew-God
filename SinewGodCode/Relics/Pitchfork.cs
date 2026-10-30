using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Runs;
using SinewGod.SinewGodCode.Relics;

namespace SinewGod.SinewGodCode.Relics;

public class Pitchfork() : SinewGodRelic
{
    public override RelicRarity Rarity => RelicRarity.Shop;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("SownAmount", 1M)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromEnchantment<Sown>(DynamicVars["SownAmount"].IntValue);
    
    public override bool TryModifyCardRewardOptionsLate(
        Player player,
        List<CardCreationResult> cardRewards,
        CardCreationOptions options)
    {
        if (player != Owner)
            return false;
        Sown canonicalSwift = ModelDb.Enchantment<Sown>();
        List<CardCreationResult> list = cardRewards.Where(r => canonicalSwift.CanEnchant(r.Card)).ToList();
        if (list.Count == 0)
            return false;
        CardCreationResult? cardCreationResult = Owner.RunState.Rng.Niche.NextItem(list);
        if (cardCreationResult == null)
            return false;
        CardModel card = Owner.RunState.CloneCard(cardCreationResult.Card);
        CardCmd.Enchant<Sown>(card, DynamicVars["SownAmount"].BaseValue);
        cardCreationResult.ModifyCard(card, this);
        return true;
    }
}