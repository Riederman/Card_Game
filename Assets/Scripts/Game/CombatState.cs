using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class CombatState : IBaseState
{
    private bool isExecuting;
    private ActorMB player;
    private ActorMB monster;

    public CombatState(ActorMB player, ActorMB monster)
    {
        Assert.IsTrue(player != null);
        Assert.IsTrue(monster != null);
        this.player = player;
        this.monster = monster;
    }

    #region Turn

    public void Enter()
    {
        // Initialization at beginning of combat
        player.OnActorDeath += OnHaltExecution;
        monster.OnActorDeath += OnHaltExecution;
        player.Opposite = monster;
        monster.Opposite = player;
    }

    public IEnumerator Execute()
    {
        isExecuting = true;
        while (isExecuting)
        {
            // Each actor draws a hand of cards
            player.DrawCards();
            monster.DrawCards();
            yield return new WaitUntil(HasDrawnCards);
            // Each actor selects which cards to play
            player.SelectCards();
            monster.SelectCards();
            yield return new WaitUntil(HasSelectedCards);
            // Each actor reveals their selected cards
            player.RevealCards();
            monster.RevealCards();
            yield return new WaitUntil(HasRevealedCards);
            // Each actor applies the effects of their cards
            player.ApplyEffects();
            monster.ApplyEffects();
            yield return new WaitUntil(HasAppliedEffects);
            // Each actor turn is ended
            player.EndTurn();
            monster.EndTurn();
            yield return new WaitUntil(HasEndedTurn);
        }
    }

    public void Exit()
    {
        player.OnActorDeath -= OnHaltExecution;
        monster.OnActorDeath -= OnHaltExecution;
    }

    #endregion

    #region Delegates

    public bool HasDrawnCards()
    {
        return player.HasDrawnCards && monster.HasDrawnCards;
    }

    public bool HasSelectedCards()
    {
        return player.HasSelectedCards && monster.HasSelectedCards;
    }

    public bool HasRevealedCards()
    {
        return player.HasRevealedCards && monster.HasRevealedCards;
    }

    public bool HasAppliedEffects()
    {
        return player.HasAppliedEffects && monster.HasAppliedEffects;
    }

    public bool HasEndedTurn()
    {
        return player.HasEndedTurn && monster.HasEndedTurn;
    }

    public void OnHaltExecution()
    {
        isExecuting = false;
    }

    #endregion
}