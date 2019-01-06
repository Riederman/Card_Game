using System.Collections;

public interface IBaseState
{
    void Enter();
    IEnumerator Execute();
    void Exit();
}