using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void OnStateEnter();
    void OnStateStay();
    void OnStateExit();
    void PlaySfx();
    void PlayVfx();
    void PlayAnim();    
}
