using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoxxaneAnimationsEventsCaller : MonoBehaviour
{
    [SerializeField] RoxxaneControl enemyParent;
    public void ActiveAttackCollision()
    {
        enemyParent.ActiveAttackCollision();
    }
    public void DeactiveAttackCollision()
    {
        enemyParent.DeactiveAttackCollision();
    }
    public void ActiveLongAttackCollision()
    {
        enemyParent.ActiveLongAttackCollision();
    }
    public void DeactiveLongAttackCollision()
    {
        enemyParent.DeactiveLongAttackCollision();
    }
    public void TPOnAnimation()
    {
        enemyParent.TPOnAnimation();
    }
    public void FinishAttack()
    {
        enemyParent.FinishAttack();
    }
}
