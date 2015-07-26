
/// <summary>
/// For determining which animations should be playing, along with other game logic.
/// </summary>
public enum PlayerState 
{
    Standing,
    Walking
}

/// <summary>
/// An input enum that's separate from the actual keys - to allow keys to be changed without affecting input.
public enum Input
{
    None,
    Up,
    Down,
    Left,
    Right,
    Start,
    A,
    B
}

public enum Direction {
    Up,
    Down,
    Left,
    Right,
    None
}