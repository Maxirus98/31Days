using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void AnimateMovements(string Name,  float movement)
    {
        _animator.SetFloat(Name, movement);
    }

    public void AnimateEnemy(string Name)
    {
        _animator.SetBool(Name, true);
    }
    
    public void StopAnimatingEnemy(string Name)
    {
        _animator.SetBool(Name, false);
    }

}
