using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class InfoPanel : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] private TMP_InputField pinName;
    [SerializeField] private Image pinImage;
    [SerializeField] private TMP_InputField pinText;

    private Pin pin;
    private bool IsUnderPointer => RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition);

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ChangePinName()
    {
        pin.Data.Name = pinName.text;
    }

    public void ChangePinText()
    {
        pin.Data.Text = pinText.text;
    }

    public void ChangePinImage()
    {
        pin.LoadNewImage();
        pinImage.sprite = pin.Sprite;
    }

    public void DeletePin()
    {
        Hide(pin, true);
        Destroy(pin.gameObject);
    }

    public void ShowFullInfo()
    {
        Hide(pin, true);
        GameUI.ShowFullInfoPanelAt(pin);
    }

    public void ShowAt(Pin newPin)
    {
        pin = newPin;

        pinName.text = pin.Data.Name;
        pinImage.sprite = pin.Sprite;

        if (pinText != null)
        {
            pinText.text = pin.Data.Text;
        }

        transform.position = pin.panelPosition;
        gameObject.SetActive(true);
    }

    public void Hide(Pin newPin, bool forceHide = false)
    {
        if (forceHide || pin == newPin && !IsUnderPointer)
        {
            pin.Data.Name = pinName.text;

            if (pinText != null)
            {
                pin.Data.Text = pinText.text;
            }

            gameObject.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hide(pin);
    }
}
