using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLateral : MonoBehaviour
{

    public Animator animMenu;
    private bool isMenu;

    private void Start() {
        isMenu = true;
    }

    public void Menu() {

        if(isMenu) {
            animMenu.SetBool("isMenu", isMenu);
            isMenu = false;
        }else if (!isMenu) {
            animMenu.SetBool("isMenu", isMenu);
            isMenu = true;
        }

    }



}
