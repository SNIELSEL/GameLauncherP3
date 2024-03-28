using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggAnimations : MonoBehaviour
{
    [Tooltip("An array with all the names of the animator triggers")]
    [SerializeField] private string[] animationNames;

    [SerializeField] private Animator animator;
    [SerializeField] private float interval;
    private float timeStamp;

    public void Start()
    {
        timeStamp = Time.time +interval;
    }
    void Update()
    {
        if (Time.time > timeStamp)
        {
            int i = Random.Range(0,animationNames.Length);
            DoRandomAnimation(animationNames[i]);
            timeStamp = Time.time + interval;
        }
    }

    void DoRandomAnimation(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
