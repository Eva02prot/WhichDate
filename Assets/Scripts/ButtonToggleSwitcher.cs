using UnityEngine;
using UnityEngine.UI;

public class ButtonToggleSwitcher : MonoBehaviour
{
    public Button Button_TB;
    public Button Button_TF;

    private bool isActive = true;

    private void Awake()
    {
        if (Button_TB != null)
            Button_TB.onClick.AddListener(OnButtonAClicked);

        if (Button_TF != null)
            Button_TF.onClick.AddListener(OnButtonBClicked);

        UpdateState();
    }

    private void OnButtonAClicked()
    {
        isActive = false;
        UpdateState();
    }

    private void OnButtonBClicked()
    {
        isActive = true;
        UpdateState();
    }

    private void UpdateState()
    {
        if (Button_TB != null) {
            Button_TB.gameObject.SetActive(isActive);
            Button_TF.gameObject.SetActive(!isActive);
        }

        if (Button_TF != null) {
            Button_TF.gameObject.SetActive(!isActive);
            Button_TB.gameObject.SetActive(isActive);
        }
    }
}