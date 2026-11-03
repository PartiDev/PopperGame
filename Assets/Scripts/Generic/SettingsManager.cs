namespace Assets.Scripts.Generic
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class SettingsManager
    {
        public static float MOUSE_SENSITIVITY = 0.5f;
    }

    public enum ActionType { UP, DOWN, LEFT, RIGHT, JUMP, DASH, INTERACT, CLICKLEFT, CLICKRIGHT, PAUSE }
}