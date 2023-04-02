using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dashboard : MonoBehaviour
{
    public Animator anim;
    private bool isMenu;
    void Start()
    {
        isMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ControllerMenu() {

        if(isMenu == true) {
            anim.SetBool("isMenu", isMenu);
            isMenu = false;
        }else if(isMenu == false) {
            anim.SetBool("isMenu", isMenu);
            isMenu = true;
        }

    }
}
