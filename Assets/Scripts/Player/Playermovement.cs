using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    const float WALK_SPEED = 0.3f;
    const float RUN_SPEED = 0.15f;
    const float SURF_SPEED = 0.2f;

    private PlayerData Player;
    private int MostRecentDirectionPressed = 0;
    private float DirectionChangeInputDelay = 0.08f;
    private float Speed;
    private float Increment = 1f;
    private bool CanInput = true;

    public IInteract busyWith;//Gameobject busyWith
    public int Direction = 2; //0 = up, 1 = right, 2 = down, 3 = left

    public bool Moving { get; private set; }
    public bool Running { get; private set; }
    public bool Surfing { get; private set; }
    public bool OnBike { get; private set; }
    public bool Strength = false;

    void Start()
    {
        Player = GetComponent<PlayerData>();
        Direction = 3;

        Speed = WALK_SPEED;
        Running = false;

        StartCoroutine(Control());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MostRecentDirectionPressed = 0;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MostRecentDirectionPressed = 2;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MostRecentDirectionPressed = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MostRecentDirectionPressed = 3;
        }
    }

    private IEnumerator Control()
    {
        bool still;
        while (true)
        {
            still = true;
            //Player is still, moving is still true on this frame however, if they just finished moving a space.
            if (CanInput)
            {
                //if(not surfing or biking check for running
                if (!Surfing && !OnBike)
                {
                    if (Input.GetKey(KeyCode.Space))//(Input.GetButton("Run"))
                    {
                        Running = true;
                        if (Moving)
                        {
                            //anim run
                        }
                        else
                        {
                            //idle/walk
                        }

                        Speed = RUN_SPEED;
                    }
                    else
                    {
                        Running = false;
                        //anim walk
                        Speed = WALK_SPEED;
                    }
                }
                //if(check for start, then interact here?
                if (Input.GetKey(KeyCode.C))//(Input.GetButton("Start"))
                {
                    //if(Moving || Input.GetButtonDown("Select"))
                    //if (SetCheckBusyWith(PAUSE)
                    //do pause stuff
                    //while menu active yield return null
                }
                else if (Input.GetKey(KeyCode.E))//(Input.GetButton("Select"))
                {
                    Tile nextTile = Player.CurrentMap.GetNextTile(Player.CurrentTile, GetForwardVector());
                    if(nextTile.GetInteractable() != null)//(SetCheckBusyWith(nextTile.GetInteractable()))
                    {
                        nextTile.GetInteractable().Interact(Player);
                        //UnsetCheckBusyWith(nextTile.GetInteractable());
                        DestroyObject(nextTile.RemovePickup());
                    }
                }
                //else if (Input.GetButton("Keyitem"))
                {
                    //Keyitem.use;
                }

                //TO BE CHANGED TO INPUT OPTIONS
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A))
                {
                    UpdateDirection(MostRecentDirectionPressed);

                    Tile nextTile = Player.CurrentMap.GetNextTile(Player.CurrentTile, GetForwardVector());
                    if (nextTile.IsWalkable() == true && nextTile.GetInteractable() == null)
                    {
                        //if(key is held if false then turn
                        //Current direction was held then lets move forward
                        //else we move if not turning
                        {
                            Moving = true;
                        }
                    }

                    if (Moving)
                    {
                        still = false;
                        yield return StartCoroutine(MoveForward());
                    }

                }

                //TODO: If input tile.nextTile
                //TODO: implement turning AS WELL AS moving. Maybe?(Turning will set nextTile and moving will move to it.)
            }
            yield return null;
        }
    }

    public void UpdateDirection(int aDir)
    {
        Direction = aDir;
    }

    public Vector2 GetForwardVector()
    {
        Vector2 forwardVector = Vector2.zero;

        if (Direction == 0)
        {
            forwardVector = Vector2.up;//AKA: new Vector2(0, 1);
        }
        else if (Direction == 1)
        {
            forwardVector = Vector2.right;
        }
        else if (Direction == 2)
        {
            forwardVector = Vector2.down;
        }
        else if (Direction == 3)
        {
            forwardVector = Vector2.left;
        }

        return forwardVector;
    }

    public Vector3 GetForwardVector3()
    {
        Vector3 forwardVector = new Vector3(GetForwardVector().x, GetForwardVector().y);

        return forwardVector;
    }

    public IEnumerator MoveForward()
    {
        Tile tile = Player.CurrentTile;
        Vector3 movement = GetForwardVector3();

        bool ableToMove = false;

        if (movement != Vector3.zero)
        {
            //if Not unwalkable tile
            if (tile.IsWalkable() == false)
            {

            }

            //else
            Vector3 startPosition = Player.CurrentTile.GetTileCoordinates();//StartPosition
            Vector2 endPostition = startPosition + movement;
            startPosition = new Vector3(startPosition.x, startPosition.y, 0);
            //Tiles are offset due to how the tilemaps in unity work so there you have it^
            Moving = true;
            Increment = 0;

            while (Increment < 1f)
            {
                Increment += (1f / Speed) * Time.deltaTime;

                if (Increment > 1)
                {
                    Increment = 1;
                }

                transform.position = startPosition + (movement * Increment);
                yield return null;
            }

            Player.CurrentTile = Player.CurrentMap.GetTile(endPostition);
        }

        //Bump
        if (!ableToMove)
        {
            //Play bump sound if implementing
            Moving = false;
        }

        yield return null;
    }

    //Attempts to set player to be busy with "aCaller" and pauses input, returning true if the request worked
    public bool SetCheckBusyWith(IInteract aCaller)
    {
        if (busyWith == null)
        {
            busyWith = aCaller;
        }
        //For busy with aCaller
        if (busyWith == aCaller)
        {
            //TODO: can input = false;
            Debug.Log("Busy with " + busyWith);
            return true;
        }

        return false;
    }

    public void UnsetCheckBusyWith(IInteract aCaller)
    {
        if (busyWith == aCaller)
        {
            busyWith = null;
        }

    }

    public IEnumerator CheckBusinessBeforeUnpause(float aWaitTime)
    {
        yield return new WaitForSeconds(aWaitTime);
        if (busyWith == null)
        {
            UnpauseInput();
        }
        else
        {
            Debug.Log("Busy with " + busyWith);
        }
    }

    public void PauseInput(float secondsToWait = 0f)
    {
        CanInput = false;
        //TODO: If animation = run, anim = walk;
        Running = false;
    }

    private void UnpauseInput()
    {

    }
}
