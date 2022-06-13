using UnityEngine;

/// <summary>
/// Is on ShadowCleaver Warrior Animation
/// </summary>
public class DestroyOnExit : StateMachineBehaviour {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Destroy(animator.gameObject, stateInfo.length);
    }
}
