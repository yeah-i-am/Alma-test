using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject pinPrefab;
    private string SavePath => Application.persistentDataPath + "/pins.save";

    private void Awake()
    {
        Debug.Log(DateTime.Now);
        List<PinData> pins = PinSaver.Load(SavePath);

        foreach (PinData pin in pins)
        {
            CreatePin(pin);
        }
        Debug.Log(DateTime.Now);
    }

    public void SavePins()
    {
        PinSaver.Save(SavePath);
    }

    // Вспомогатлеьный метод для получения позиции указателя в мировых координатах
    public static Vector2 GetPointerPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static void CreatePin(PinData data =  null)
    {
        Pin pin = Instantiate(Instance.pinPrefab).GetComponent<Pin>();

        if (data != null)
        {
            pin.Data = data;
        }
        else
        {
            pin.Data = new PinData();
            pin.transform.position = GetPointerPosition();
        }
    }
}
