using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetTurn : MonoBehaviour
{
    public ActionBasedContinuousTurnProvider cTurnProvider;
    public ActionBasedSnapTurnProvider sTurnProvider;

    public void SetTypeFromIndex(int index) {
        if (index == 0) {
            sTurnProvider.enabled = false;
            cTurnProvider.enabled = true;
        }
        else if (index == 1) {
            cTurnProvider.enabled = false;
            sTurnProvider.enabled = true;
        }
	}
}
