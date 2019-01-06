using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ActorMB player;
    [SerializeField] private ActorMB monster;

    private CombatState combatState;
    private StateMachine stateMachine;

    private void Awake()
    {
        combatState = new CombatState(player, monster);
        stateMachine = new StateMachine(combatState);
    }

    private void Start()
    {
        stateMachine.Run();
    }
}