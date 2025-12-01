using System;
using UnityEngine;

public static class CharacterFactory
{
    public static MainCharacter CreateCharacter(CharacterType type, float commonCooldown)
    {
        switch (type)
        {
            case CharacterType.Witch:
                return new WitchCharacter(commonCooldown);

            case CharacterType.Warrior:
                return new WarriorCharacter(commonCooldown);

            case CharacterType.Archer:
            return new ArcherCharacter(commonCooldown);

            case CharacterType.Asassin:
                return new AsassinCharacter(commonCooldown);

            default:
                throw new ArgumentException($"Not supported tupe of character: {type}");
        }
    }
}
