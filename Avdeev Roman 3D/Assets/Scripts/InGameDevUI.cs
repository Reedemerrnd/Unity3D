using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class InGameDevUI : MonoBehaviour
{
    //перехвват управления
    private FirstPersonController _controller;
    private bool _isEnabled;
    //для редактора
    private Stack<GameObject> _createdObjects;
    private GameObject _currentObj;
    [SerializeField]
    private Material _material;
    private Color _color;
    private Rect _toolArea = new Rect(0,0,100,400);
    private Rect _sliderRect = new Rect(10, 90, 80, 20);


    private void Start()
    {
        _createdObjects = new Stack<GameObject>();
        _isEnabled = false;
        _controller = GetComponent<FirstPersonController>();
        SwitchControls(_isEnabled);
    }
    private void Update()
    {
        // на нажатию показываем/скрываем меню и отключаем/включаем контроллер
        // (чтобы было удобнее рабтать с мышкой)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isEnabled = !_isEnabled;
            SwitchControls(_isEnabled);
        }
        UpdateCubeColor();
    }
    private void OnGUI()
    {
        if (_isEnabled)
        {
            GUI.BeginGroup(_toolArea);
            GUI.Box(_toolArea, "Tool");
            if (GUI.Button(new Rect(10, 30, 80, 20), "Create Cube"))
                CreateCube();
            if (GUI.Button(new Rect(10, 60, 80, 20), "Desroy Cube"))
                DestroyCube();
            _color = RGBaSlider(_color, _sliderRect);
            GUI.EndGroup();
        }
    }
    //сщздание куба, добавление в стек и запоминание последнего созданного для редактирования
    private void CreateCube()
    {
        _currentObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _currentObj.GetComponent<MeshRenderer>().material = _material;
        _createdObjects.Push(_currentObj);
        _currentObj.transform.Translate(transform.position+transform.forward*5);
        _currentObj.transform.rotation = gameObject.transform.rotation;
    }
    //уничтожение кубов в стеке по одному
    private void DestroyCube()
    {
        if (_createdObjects.Count > 0)
        {
            _currentObj = _createdObjects.Peek();
            Destroy(_currentObj);
            _createdObjects.Pop();
        }
    }
    //слайдер цвета
    private Color RGBaSlider(Color color, Rect area)
    {
        Color rgba = color;
        GUI.Label(area, "Red");
        area.y += 20;
        rgba.r = GUI.HorizontalSlider(area, rgba.r, 0.0f, 1.0f);
        area.y += 20;
        GUI.Label(area, "Green");
        area.y += 20;
        rgba.g = GUI.HorizontalSlider(area, rgba.g, 0.0f, 1.0f);
        area.y += 20;
        GUI.Label(area, "Blue");
        area.y += 20;
        rgba.b = GUI.HorizontalSlider(area, rgba.b, 0.0f, 1.0f);
        area.y += 20;
        GUI.Label(area, "Alpha");
        area.y += 20;
        rgba.a = GUI.HorizontalSlider(area, rgba.a, 0.0f, 1.0f);
        return rgba;
    }
    // обновление цвета верхнего в стеке куба
    private void UpdateCubeColor()
    {
       
        if (_createdObjects.Count>0)
        {
            _currentObj = _createdObjects.Peek();
            _currentObj.GetComponent<MeshRenderer>().material.color = _color;
        }
    }
    //отключение/включение движения и вращения персонажа мышкой и освобождение курсора
    private void SwitchControls(bool isEnabled)
    {

        if (isEnabled)
        {
            _controller.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            _controller.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
