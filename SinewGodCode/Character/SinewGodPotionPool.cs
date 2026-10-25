using BaseLib.Abstracts;
using SinewGod.SinewGodCode.Extensions;
using Godot;

namespace SinewGod.SinewGodCode.Character;

public class SinewGodPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => SinewGod.Color;


    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}