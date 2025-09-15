using UnityEngine;

public abstract class abPlayer : IPlayer
{
    public string Name { get; set; }

    public IBoard Board { get; set; }

    public abPlayer()
    {
    }

    public abstract void Init();
}
