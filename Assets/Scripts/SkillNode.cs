using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill", order = 1)]
public class SkillNode : ScriptableObject
{
    public string skillName = "Skillname";
    public int cost;

    public SkillNode[] neighbours;
    
    private bool _isBought;

    public delegate void Changed();

    public event Changed OnChanged;

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

    public bool CanBeForgotten()
    {
        return HasActiveLink() && AllNeighboursWontBeOrphans();
    }

    public bool AllNeighboursWontBeOrphans()
    {
        foreach (var skill in neighbours)
        {
            if (skill.GetActiveNeighboursCount() < 2)
            {
                return false;
            }
        }
        
        return true;
    }

    public int GetActiveNeighboursCount()
    {
        int count = 0;
        foreach (var skill in neighbours)
        {
            if (skill.IsBought())
            {
                count++;
            }
        }

        return count;
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
