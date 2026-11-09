using BaseLib.Abstracts;
using BaseLib.Patches.UI;
using BaseLib.Utils.NodeFactories;
using SinewGod.SinewGodCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using SinewGod.SinewGodCode.Cards.Basic;
using SinewGod.SinewGodCode.Relics;

namespace SinewGod.SinewGodCode.Character;

public class SinewGod : PlaceholderCharacterModel
{
    public const string CharacterId = "SinewGod";

    public static readonly Color Color = new("610811");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Masculine;
    public override int StartingHp => 80;
    public override Color MapDrawingColor => Color;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeSinewGod>(),
        ModelDb.Card<StrikeSinewGod>(),
        ModelDb.Card<StrikeSinewGod>(),
        ModelDb.Card<StrikeSinewGod>(),
        ModelDb.Card<DefendSinewGod>(),
        ModelDb.Card<DefendSinewGod>(),
        ModelDb.Card<DefendSinewGod>(),
        ModelDb.Card<DefendSinewGod>(),
        ModelDb.Card<EldritchLimb>(),
        ModelDb.Card<Toughness>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<LesserAuthoritySinew>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<SinewGodCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<SinewGodRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<SinewGodPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_this_man_is_hot.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
    
    // chest screen
    public override string CustomArmPointingTexturePath => "res://SinewGod/images/character/ui/sinew_god_point.png";
    public override string CustomArmRockTexturePath => "res://SinewGod/images/character/ui/sinew_god_rock.png";
    public override string CustomArmPaperTexturePath => "res://SinewGod/images/character/ui/sinew_god_paper.png";
    public override string CustomArmScissorsTexturePath => "res://SinewGod/images/character/ui/sinew_god_scissor.png";
    public override RelicIconData? CustomYummyCookie => new(
        "res://SinewGod/images/relics/big/yummy_cookie_sinew_god.png", 
        "res://SinewGod/images/relics/yummy_cookie_sinew_god.png", 
        "res://SinewGod/images/relics/yummy_cookie_sinew_god_outline.png"
        );
    
    // scenes
    public override string CustomVisualPath => "res://SinewGod/images/scene/sinew_god_sprite.tscn";
    public override string CustomMerchantAnimPath => "res://SinewGod/images/scene/merchant/sinew_god_sprite_merchant.tscn";
    public override string CustomCharacterSelectBg => "res://SinewGod/images/scene/charselect/selection_screen.tscn";
    public override string CustomRestSiteAnimPath => "res://SinewGod/images/scene/restsite/sinew_god_rest_site.tscn";
}