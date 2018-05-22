﻿using System;
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
        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                //Up
                MostRecentDirectionPressed = 0;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                //Down
                MostRecentDirectionPressed = 2;
            }
        }
        else if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                //Right
                MostRecentDirectionPressed = 1;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                //Left
                MostRecentDirectionPressed = 3;
            }
        }
    }

    private bool IsDirectionKeyHeld(int aDirCheck)
    {
        if ((aDirCheck == 0 && Input.GetAxisRaw("Vertical") > 0) ||
            (aDirCheck == 1 && Input.GetAxisRaw("Horizontal") > 0) ||
            (aDirCheck == 2 && Input.GetAxisRaw("Vertical") < 0) ||
            (aDirCheck == 3 && Input.GetAxisRaw("Horizontal") < 0))
        {
            return true;
        }

        return false;
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
                if (!Surfing && !OnBike)
                {
                    if ((Input.GetButton("Run")))
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
                //Note: *May* need to add getbutton here and if moving beside getbuttondown
                if ((Input.GetButtonDown("Start")))
                {
                    //if(Moving || Input.GetButtonDown("Select"))
                    //if (SetCheckBusyWith(PAUSE)
                    //do pause stuff
                    //while menu active yield return null
                }
                else if ((Input.GetButtonDown("Select")))
                {
                    Tile nextTile = Player.CurrentMap.GetNextTile(Player.CurrentTile, GetForwardVector());
                    if (nextTile.GetInteractable() != null)//(SetCheckBusyWith(nextTile.GetInteractable()))
                    {
                        nextTile.GetInteractable().Interact(Player);
                        //UnsetCheckBusyWith(nextTile.GetInteractable());
                        DestroyObject(nextTile.RemovePickup());
                    }
                }
                else if (Input.GetButtonDown("Keyitem"))
                {
                    //Keyitem.use;
                    yield return new WaitForSeconds(DirectionChangeInputDelay);
                }
                //If interacting/pausing is not being called then we can then move
                else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    //If most recent direction is held, but isn't the current direction, then make it so
                    if (MostRecentDirectionPressed != Direction && IsDirectionKeyHeld(MostRecentDirectionPressed))
                    {
                        UpdateDirection(MostRecentDirectionPressed);
                        if (!Moving)
                        {
                            //Unless player has just moved, wait a bit to make sure
                            yield return new WaitForSeconds(DirectionChangeInputDelay);
                        }   //they have time to let go before moving(Allows only turning)
                    }
                    //If a new direction wasn't found, direction would have been set
                    else
                    {
                        //If current direction isn't held, then turn to new direction
                        if (!IsDirectionKeyHeld(Direction))
                        {   //If we got here, we are for sure turning

                            //It's least likely to move the opposite direction by accident
                            int dirCheck = (Direction + 2 > 3) ? Direction - 2 : Direction + 2;
                            if (IsDirectionKeyHeld(dirCheck))
                            {
                                UpdateDirection(dirCheck);
                                if (!Moving)
                                {
                                    yield return new WaitForSeconds(DirectionChangeInputDelay);
                                }
                            }
                            else
                            {
                                //Now its either 90deg clockwise, counter, or none. Prioritise clockwise
                                dirCheck = (Direction + 1 > 3) ? Direction - 3 : Direction + 1;
                                if (IsDirectionKeyHeld(dirCheck))
                                {
                                    UpdateDirection(dirCheck);
                                    if (!Moving)
                                    {
                                        yield return new WaitForSeconds(DirectionChangeInputDelay);
                                    }
                                }
                                else
                                {
                                    dirCheck = (Direction - 1 < 0) ? Direction + 3 : Direction - 1;
                                    if (IsDirectionKeyHeld(dirCheck))
                                    {
                                        UpdateDirection(dirCheck);
                                        if (!Moving)
                                        {
                                            yield return new WaitForSeconds(DirectionChangeInputDelay);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Moving = true;
                        }

                        if (Moving)
                        {
                            still = false;
                            yield return StartCoroutine(MoveForward());
                        }
                    }
                }
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
            PauseInput();
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
        StartCoroutine(CheckBusinessBeforeUnpause(0.1f));

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

        StartCoroutine(CheckBusinessBeforeUnpause(secondsToWait));
    }

    private void UnpauseInput()
    {
        CanInput = true;
    }

    public bool IsInputPaused()
    {
        return !CanInput;
    }

    public IEnumerator MoveForward()
    {
        Vector3 movement = GetForwardVector3();

        bool ableToMove = false;

        if (movement != Vector3.zero)
        {
            Tile nextTile = Player.CurrentMap.GetNextTile(Player.CurrentTile, movement);

            if (nextTile == null)
            {
                //If we got here, we are most likely trying to check for a tile from a
                //different map, so lets do a map check
                Map nextMap = Player.World.GetNextMap(Player.CurrentMap, Direction);

                if (nextMap != null)
                {
                    nextTile = nextMap.GetTile(nextTile.GetTileCoordinates());
                }
                else
                {
                    Debug.LogError("No map connected. Either map not implemented or HUGE error.");
                }
            }

            if (nextTile.GetInteractable() == null)
            {
                //Nothing occupying the tile(AKA pickup/NPC/etc.)

                if (!Surfing && nextTile.GetTileType() == Tile.TileType.Water)
                {
                    //Not surfing so can't move on water
                }
                else if (nextTile.GetTileType() == Tile.TileType.Ledge)
                {
                    if (nextTile.IsWalkable(Direction))
                    {
                        ableToMove = true;
                        //Jump
                    }
                }
                else
                {
                    if (nextTile.IsWalkable())//Parameter used for ledge tiletypes
                    {
                        if (Surfing && nextTile.GetTileType() != Tile.TileType.Water)
                        {
                            //Moving off of a water tile
                            //Update anim here
                            Speed = WALK_SPEED;
                            Surfing = false;
                        }

                        //Change map here.

                        ableToMove = true;
                        yield return StartCoroutine(Move(movement));
                        //Make sure current tile is set correctly
                        Vector2 endPosition = nextTile.GetTileCoordinates();
                        Player.CurrentTile = Player.CurrentMap.GetTile(endPosition);
                    }
                }
            }
        }

        //Bump
        if (!ableToMove)
        {
            //Play bump sound if implementing
            Moving = false;
        }

        yield return null;
    }

    public IEnumerator Move(Vector3 aMovement, bool aEncounter = true)
    {
        if (aMovement != Vector3.zero)
        {
            Vector3 startPosition = Player.CurrentTile.GetTileCoordinates();
            Moving = true;
            Increment = 0;

            while (Increment < 1f)
            {
                Increment += (1f / Speed) * Time.deltaTime;

                if (Increment > 1)
                {
                    Increment = 1;
                }

                transform.position = startPosition + (aMovement * Increment);
                yield return null;
            }

            //Now we check for encounters(hehe ^_^) unless disabled
            if (aEncounter)
            {
                Tile tile = Player.CurrentMap.GetTile(transform.position);

                if (tile.GetTileType() == Tile.TileType.Water)
                {
                    //Wild encounter surfing
                }
                else if (tile.GetTileType() == Tile.TileType.Grass || tile.GetTileType() == Tile.TileType.Cave)
                {
                    //Wild encounter land
                }

            }
        }
    }
}
