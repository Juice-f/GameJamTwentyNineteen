using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDo : CharacterController
{
    void CheckInput()
    {
        if (Input.GetButton(jumpButtonSrc))
        {
            Debug.Log("Jump button pressed");

        }

        if (Input.GetButton(smolSlapButtonSrc))
        {
            Debug.Log(smolSlapButtonSrc);
        }

        if (Input.GetButton(bigSlapButtonSrc))
        {
            Debug.Log(bigSlapButtonSrc);
        }
        if (Input.GetButton(gimmickButtonSrc))

        Debug.Log(joy1X + " " + joy1Y);




    }
}
