using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls instance;

    bool rightPress;
    bool leftPress;
    bool actionPress;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rightPress == true)
        {
            print("right pressed");
        }

        if(leftPress == true)
        {
            print("left pressed");
        }

        if(actionPress == true)
        {
            print("action pressed");
        }
    }

    public void RightInputCheck(bool toggle)
    {
        rightPress = toggle;
    }

    public void LeftInputCheck(bool toggle)
    {
        leftPress = toggle;
    }

    public void ActionInputCheck(bool toggle)
    {
        actionPress = toggle;
    }
}
