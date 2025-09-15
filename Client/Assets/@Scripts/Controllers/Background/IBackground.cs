using UnityEngine;

public interface IBackground
{
    void Init();
    IBoard Board { get; set; }
    public GameObject Root { get; set; }
}