using System;

[Serializable]
public enum ElemetType
{
    NEUTRAL = 0,
    WATER = 0x1,
    FIRE = 0x10,
    EARTH = 0x100,
    AIR = 0x1000,
    CLOUD = WATER + FIRE,
    SAND = EARTH + AIR,
    MAGMA = FIRE + EARTH,
    ICE = WATER + AIR,
    BLAZE = FIRE + AIR,
    MUD = EARTH + WATER,
}
