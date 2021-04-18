using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{

    //A reference to our game manager.
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameObject gamePanel;


    //Find our game object before the start method
    private void Awake()
    {
        //Get a reference to our game manager, since we need it to know what difficulty we are giving it
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void EasyButton()
    {
        gameManager.moves = 40;

        gameManager.movesLeft.text = "Moves Remaining: " + gameManager.moves;



    }

    public void MediumButton()
    {
        gameManager.moves = 35;

        gameManager.movesLeft.text = "Moves Remaining: " + gameManager.moves;



    }

    public void HardButton()
    {
        gameManager.moves = 28;

        gameManager.movesLeft.text = "Moves Remaining: " + gameManager.moves;


    }

}
