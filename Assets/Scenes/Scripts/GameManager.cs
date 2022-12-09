using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // DEATH YAPILACAK BELKÝ MÜZÝK
    public static GameManager gameManagerInstance;
    public bool gameState;
    public GameObject menuCanvas , gameMenu;
    public Text topText;
    private Animator animator;

    private void Start()
    {
        gameManagerInstance = this;
        gameState = false;
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
      
    }

    public void StartTheGame()
    {
        gameState = true;
        menuCanvas.SetActive(false);
        gameMenu.SetActive(true);
        animator.SetBool("canRun", true);
    }

  

  
}
