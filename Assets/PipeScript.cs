using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PipeScript : MonoBehaviour
{
    //Every possible rotation of the pipes
    float[] rotations = { 0.0f, 90.0f, 180.0f, 270.0f };

    //The float Z value of the pipe that is correct for the puzzle
    public float[] correctRotation;

    //Is the pipe in the right rotation?
    [SerializeField]
    bool isPlaced = false;

    //The pipe's number of correct rotations. Since all pipes at least have one, we initialize this to one. 
    int PossibleRotations = 1;

    //A reference to our game manager.
    GameManager gameManager;

    public Sprite emptyPipe, fullPipe;

    public float rotation;

    //Find our game object before the start method
    private void Awake()
    {
        //Get a reference to our game manager, since we need it to kno when we rotate correctly
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        Initialize();



    }

    public void Initialize()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = emptyPipe;
        //The number of possible correct rotations of a pipe is equal to whatever value we give in in the editor. 
        PossibleRotations = correctRotation.Length;

        //Get a random number between 0 and 3 (Or whatever amount of angles you want the pipes to be able to have)
        int rand = Random.Range(0, rotations.Length - 1);

        //Every possible rotation, in this case, either 0, 90, 180 or 270.
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);
        //transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(rotations[rand], 0, 270) /*rotations[rand]*/);

        rotation = transform.eulerAngles.z;
        if (transform.eulerAngles.z == 180)
        {
            Debug.Log(gameObject.name + ": we arent supposed to be here");
            transform.eulerAngles = new Vector3(0, 0, 90.0f);
        }

        //If a pipe has more than one possible correct rotation
        if (PossibleRotations > 1)
        {
            //Are either of the angles correct when the game boots up? 
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1])
            {
                //If yes, it begins placed correctly
                isPlaced = true;
                //Update the game manager, letting it know a pipe began correct
                gameManager.correctMove();
            }
        }
        else
        {
            //The same code, but run it if the pipe only has one correct rotation.
            if (transform.eulerAngles.z == correctRotation[0])
            {
                //If yes, it begins placed correctly
                isPlaced = true;
                //Update the game manager, letting it know a pipe began correct
                gameManager.correctMove();

            }
        }
    }

   void OnMouseDown()
    {
        //Once you click, rotate the clicked pipe by 90 degrees.
        transform.Rotate(new Vector3(0, 0, 90.0f));


        gameManager.moves--;

        gameManager.movesLeft.text = "Moves Remaining: " + gameManager.moves;

        //Does the pipe have more than one correct way to be rotated?
        if (PossibleRotations > 1)
        {
            //Are one of the two correct angles the same as the angle it currently is? If so, was it not placed correctly before?
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1] && isPlaced == false)
            {
                //If yes, now it is placed correctly
                isPlaced = true;
                //Update the game manager
                gameManager.correctMove();

            } //If you clicked and the pipe was already correct
            else if (isPlaced == true)
            {
                //The pipe is no longer correct
                isPlaced = false;
                //Update the game manager
                gameManager.wrongMove();
            }
        }
        else
        {
            //The pipe only has one correct rotation. Is it currently correct? If so, was it wrong before?
            if (transform.eulerAngles.z == correctRotation[0] && isPlaced == false)
            {
                //If yes, now it is placed correctly
                isPlaced = true;
                //Update the game manager
                gameManager.correctMove();

            } //If you clicked and the pipe was already correct
            else if (isPlaced == true)
            {
                //The pipe is no longer correct
                isPlaced = false;
                //Update the game manager
                gameManager.wrongMove();

            }
        }
        

        //if(gameManager.moves <= 0)
        //{
        //    gameManager.gameOver();
        //}
    }

    private void Update()
    {
        //if (!isPlaced)
        //{
        //    //Sets sprite to empty
        //    gameObject.GetComponent<SpriteRenderer>().sprite = emptyPipe;
        //}
        //else
        //{
        //    //Sets sprite to full
        //    gameObject.GetComponent<SpriteRenderer>().sprite = fullPipe;

        //}
    }

    public void SolvedPiece()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = fullPipe;
    }

}
