using UnityEngine;

public class GameQuitter : MonoBehaviour
{
    public void exitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
