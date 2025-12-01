using UnityEngine;

public enum CharacterType
{
    Witch,
    Warrior,
    Archer,
    Asassin
}

public enum SkillExecutionType
{
    fireball, freezer,
    hammer, shield,
    shurikens, lunge,
    multiArrows, poisonedArrows
}

public interface IMovementStrategy
{
    void Move(Transform transform, float deltaTime);
    bool IsTargetValid();
    Vector3 GetTargetPosition();
    Transform GetTargetTransform();
}
