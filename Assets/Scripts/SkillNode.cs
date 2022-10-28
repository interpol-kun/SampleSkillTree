using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill", order = 1)]
public class SkillNode : ScriptableObject
{
    public string skillName = "Skillname";
    public int cost;
    public SkillNode[] neighbours;

    public delegate void Changed();
    public event Changed OnChanged;
    
    private bool _isBought;

    [HideInInspector] public int num;
    [HideInInspector] public int low;
    [HideInInspector] public bool isVisited;
    [HideInInspector] public SkillNode parent;

    public bool HasActiveLink()
    {
        foreach (var skill in neighbours)
        {
            if (skill.IsBought())
            {
                return true;
            }
        }

        return false;
    }

    public bool IsBought()
    {
        return _isBought;
    }

    public void SetBought(bool isBought)
    {
        _isBought = isBought;
        OnChanged?.Invoke();
    }
}
