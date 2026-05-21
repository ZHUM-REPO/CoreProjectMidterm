using UnityEngine;

public class puzzle : MonoBehaviour
{
    public GameObject First_UI;
    public GameObject second_UI;
    public GameObject third_UI;
    public GameObject fourth_UI;
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;


    void Start()
    {
        Invoke(nameof(ShowMessage1), 0);
        Invoke(nameof(ShowMessage2), 60);
        Invoke(nameof(ShowMessage3), 120);
        Invoke(nameof(ShowMessage4), 180);
    }

    void ShowMessage1()
    {
        First_UI.SetActive(true);
        Invoke(nameof(Hide1), 10);
        door1.SetActive(false);
    }

    void Hide1()
    {
        First_UI.SetActive(false);
    }

    void ShowMessage2()
    {
        second_UI.SetActive(true);
        Invoke(nameof(Hide2), 10);
        door2.SetActive(false);
    }

    void Hide2()
    {
        second_UI.SetActive(false);
    }

    void ShowMessage3()
    {
        third_UI.SetActive(true);
        Invoke(nameof(Hide3), 10);
        door3.SetActive(false);
    }

    void Hide3()
    {
        third_UI.SetActive(false);
    }

    void ShowMessage4()
    {
        fourth_UI.SetActive(true);
        Invoke(nameof(Hide4), 10);
    }

    void Hide4()
    {
        fourth_UI.SetActive(false);
    }
}