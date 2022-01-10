using UnityEngine;
public class AnimationAutoDestroy : MonoBehaviour {
    void Start () {
        Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - 0.1f); 
    }
}
