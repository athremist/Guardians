using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteract
{
    public void Interact()
    {
    }

    /*public IEnumerator drawText(string textLine)
    {
        SfxHandler.Play(selectClip);
        int textSpeed = PlayerPrefs.GetInt("textSpeed") + 1;
        charPerSec = 16 + (textSpeed * textSpeed * 9);
        float secPerChar = 1 / charPerSec;
        //split textLine into an array of each character, so it may be printed 1 bit at a time
        char[] chars = textLine.ToCharArray();
        for (int i = 0; i < textLine.Length; i++)
        {
            if (chars[i] == '{')
            {
                //extended operator
                if (chars[i + 1] == 'p' || chars[i + 1] == 'P')
                {
                    //player name
                    i += 1; //adjust for the extra character in the operator (e.g. "{P" )
                    char[] pChars = SaveData.currentSave.playerName.ToCharArray();
                    for (int i2 = 0; i2 < pChars.Length; i2++)
                    {
                        yield return StartCoroutine(drawChar(pChars[i2], secPerChar));
                    }
                }
            }
            else
            {
                yield return StartCoroutine(drawChar(chars[i], secPerChar));
            }
        }
    }*/

    //https://algonquincollegegamedev.assembla.com/spaces/capstone-f15-s3-bendystrawstudios/subversion/source/HEAD/trunk/TreasuresOfTheLand/Assets/Scripts/Interaction(Redux)/PlayerInteractionHandler.cs
    //https://algonquincollegegamedev.assembla.com/spaces/capstone-f15-s3-bendystrawstudios/subversion/source/HEAD/trunk/TreasuresOfTheLand/Assets/Scripts/Interaction(Redux)/IInteract.cs
    //https://algonquincollegegamedev.assembla.com/spaces/capstone-f15-s3-bendystrawstudios/subversion/source/HEAD/trunk/TreasuresOfTheLand/Assets/Scripts/Interaction(Redux)/BaseObjects/BaseDialogueInteractionObject.cs
    //https://algonquincollegegamedev.assembla.com/spaces/capstone-f15-s3-bendystrawstudios/subversion/source/HEAD/trunk/TreasuresOfTheLand/Assets/Scripts/Interaction(Redux)/BaseObjects/DialogueObjects/InteractShopKeeper.cs
}
