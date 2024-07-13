using System;

[Serializable]
public enum CharStates {
    IDLE,ATTACK,DEFEND,FLEE
}

[Serializable]
public enum CharSide {
    ALLY,ENEMY,NEUTRAL
}

[Serializable]
public enum CharType {
    PLAYER,MOB,BOSS
}