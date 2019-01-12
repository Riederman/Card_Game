using UnityEngine;

public class MonsterMB : ActorMB
{
    protected override int GetSelectedIndex()
    {
        return Random.Range(0, GameConstants.NUM_CARDS_PER_HAND);
    }

    protected override bool IsPlayer()
    {
        return false;
    }
}