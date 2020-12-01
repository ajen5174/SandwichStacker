using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Difficulty
{
    public enum Level
    {
        EASY,
        MEDIUM,
        HARD
    }

    public static Level difficulty = Level.EASY;
}
