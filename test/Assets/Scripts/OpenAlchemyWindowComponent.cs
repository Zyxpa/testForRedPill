using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAlchemyWindowComponent : MonoBehaviour
{
    [SerializeField] Transform canvas;
    GameObject AlchemyWindow;
    AlchemyWindowController alchemyWindowController;
    public void OpenAlchemyWindow()
    {
        if (AlchemyWindow == null)
            AlchemyWindow = Instantiate(Resources.Load<GameObject>("AlchemyWindow"), canvas.position, Quaternion.identity, canvas);
        else
            AlchemyWindow.SetActive(true);

    }

}
