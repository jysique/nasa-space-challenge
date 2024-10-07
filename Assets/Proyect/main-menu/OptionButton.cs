using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public string id;

    public void OnEndHighlight()
    {
        switch (id)
        {
            case MainMenuManager.PLAY:
                MainMenuManager.instance.ActivateTxtBtns(0);
                break;
            case MainMenuManager.CREDITS:
                MainMenuManager.instance.ActivateTxtBtns(1);
                break;
            default:
                break;
        }
    }
}
