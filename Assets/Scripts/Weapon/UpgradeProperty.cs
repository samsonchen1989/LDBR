using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum UpgradeType
{
    CLIP_SIZE,
    BULLET_DAMAGE,
    RELOAD_TIME
}

public struct LevelData
{
    public int level { get; private set; }
    public float value { get; private set; }
    public int upgradeGold { get; private set; }

    public LevelData(int level, float value, int upgradeGold)
    {
        this.level = level;
        this.value = value;
        this.upgradeGold = upgradeGold;
    }
}

public class UpgradaProperty
{
    List<LevelData> levels;
    int currentLevel;
    string propertyName;

    public int UpgradeCost
    {
        get {
            return levels[currentLevel].upgradeGold;
        }
    }

    public float CurrentValue
    {
        get {
            return levels[currentLevel].value;
        }
    }

    public int Level
    {
        get {
            return currentLevel;
        }
    }

    public string Name
    {
        get {
            return propertyName;
        }
    }

    public bool IsLevelMax()
    {
        if (currentLevel == (levels.Count - 1)) {
            return true;
        }

        return false;
    }

    public UpgradaProperty(List<LevelData> levels, string name, int currentLevel = 0)
    {
        this.levels = levels;
        this.propertyName = name;
        this.currentLevel = currentLevel;
    }

    public bool Upgrade()
    {
        if (currentLevel < levels.Count) {
            currentLevel += 1;
            return true;
        }

        return false;
    }
}
