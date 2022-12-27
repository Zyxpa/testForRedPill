using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    Animator animator;

    int show = Animator.StringToHash("show");
    int hide = Animator.StringToHash("hide");
   
    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        else
        {
            animator.SetTrigger(show);
        }
    }

    virtual public void OnClose()
    {
        animator.SetTrigger(hide);
    }

    virtual public void OnCloseAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
