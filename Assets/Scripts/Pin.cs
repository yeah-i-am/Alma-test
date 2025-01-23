using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Pin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IPointerDownHandler
{
    public Sprite Sprite { get; private set; }
    public Vector2 panelPosition => Camera.main.WorldToScreenPoint(pinCollider.bounds.max);
    public PinData Data
    {
        get
        {
            data.Position = transform.position;

            return data;
        }

        set
        {
            data = value;

            transform.position = data.Position;

            if (data.ImageData != null)
            {
                tex = data.GetTexture();
                Sprite = CreateSprite();
            }
        }
    }

    private PinData data;
    private Texture2D tex;
    private Vector2 grabPoint;
    private Collider2D pinCollider;

    private void Awake()
    {
        pinCollider = GetComponent<Collider2D>();
    }

    private Sprite CreateSprite()
    {
        if (tex == null)
        {
            return null;
        }

        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
    }

    public void LoadNewImage()
    {
        Texture2D tmp = Data.LoadImage();

        if (tmp == null)
        {
            return;
        }

        tex = tmp;
        Sprite = CreateSprite();
    }

    public void OnDrag(PointerEventData data)
    {
        // При перетаскивании объекта, меняем его позицию соответсвенно
        transform.position = GameManager.GetPointerPosition() + grabPoint;
        GameUI.HidePreviewPanel(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Когда хватаем объект - запоминаем где схватили, меняем состояние и останавливаем падение
        grabPoint = (Vector2)transform.position - GameManager.GetPointerPosition();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameUI.ShowPreviewPanelAt(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameUI.HidePreviewPanel(this);
    }
}
