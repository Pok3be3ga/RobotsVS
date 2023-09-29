using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimator : MonoBehaviour
{
    Animator animator;
    [SerializeField] string AnimationName;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().Play(AnimationName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
