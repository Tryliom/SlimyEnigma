using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public enum Room
{
    None = 0, 
    One = 1, 
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8
}

public class ChronoController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject text;

    private Dictionary<Room, float> _timePerRoom = new Dictionary<Room, float>();
    private Room _currentRoom = Room.None;
    private bool _isRunning = false;
    
    private TextMeshPro _textMeshPro;
    private RectTransform _textRectTransform;

    // Start is called before the first frame update
    private void Start()
    {
        _textMeshPro = text.GetComponent<TextMeshPro>();
        _textRectTransform = text.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_currentRoom == Room.None)
        {
            return;
        }
        
        if (_isRunning)
        {
            _timePerRoom[_currentRoom] += Time.deltaTime;
            
            _textMeshPro.text = $"Total: {GetTotalTime()}\nRoom {_currentRoom} : {GetTimeInRoom(_currentRoom)}";
            _textMeshPro.color = Color.yellow;
        }
        else
        {
            _textMeshPro.color = Color.white;
        }
        
        // Set the text on the top left side of the camera
        var pos = cam.ViewportToWorldPoint(new Vector3(0.01f, 0.99f, 0));
        pos.z = 0;
        _textRectTransform.position = pos;
    }
    
    private static string FormatTime(float time)
    {
        var minutes = Mathf.FloorToInt(time / 60);
        var seconds = Mathf.FloorToInt(time % 60);
        var milliseconds = Mathf.FloorToInt((time * 100) % 100);
        
        return $"{minutes:00}m:{seconds:00}s:{milliseconds:00}ms";
    }
    
    private string GetTimeInRoom(Room room)
    {
        if (_timePerRoom.ContainsKey(room))
        {
            return FormatTime(_timePerRoom[room]);
        }
        
        return "00:00:00";
    }
    
    private string GetTotalTime()
    {
        return FormatTime(_timePerRoom.Values.Sum());
    }
    
    public void StartChrono(Room room)
    {
        if ((int) room <= (int) _currentRoom)
        {
            return;
        }
        
        if (!_timePerRoom.ContainsKey(room))
        {
            _timePerRoom.Add(room, 0);
        }
        
        _currentRoom = room;
        _isRunning = true;
    }
    
    public void StopChrono()
    {
        _isRunning = false;
    }
}
