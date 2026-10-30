using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Rooms;
using SinewGod.SinewGodCode.Powers;
using SinewGod.SinewGodCode.Relics;

namespace SinewGod.SinewGodCode.Relics;

public class CharmOfCleanAir() : SinewGodRelic
{
    public override RelicRarity Rarity => RelicRarity.Common;

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        if (room is not CombatRoom)
            return;
        await PowerCmd.Apply<CleanAirPower>(new ThrowingPlayerChoiceContext(), Owner.Creature, 1, Owner.Creature, null);
    }
}