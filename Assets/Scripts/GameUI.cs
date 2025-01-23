using UnityEngine;

public class GameUI : Singleton<GameUI>
{
    [SerializeField] private InfoPanel previewPanel;
    [SerializeField] private InfoPanel fullInfoPanel;

    private void Awake()
    {
        if (previewPanel == null)
        {
            Debug.LogError("There is no preview panel at GameUI");
            return;
        }
    }

    public static void ShowPreviewPanelAt(Pin pin)
    {
        Instance.previewPanel.ShowAt(pin);
    }

    public static void HidePreviewPanel(Pin pin)
    {
        Instance.previewPanel.Hide(pin);
    }

    public static void ShowFullInfoPanelAt(Pin pin)
    {
        Instance.fullInfoPanel.ShowAt(pin);
    }

    public static void HideFullInfoPanel(Pin pin)
    {
        Instance.fullInfoPanel.Hide(pin);
    }
}
