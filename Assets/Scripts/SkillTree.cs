using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public SkillNode[] skills;

    [SerializeField] private SkillNode baseSkill;
    [SerializeField] private int maxSkillPoints;
    
    public delegate void SkillPointsUpdated(int amount);

    public static event SkillPointsUpdated OnSkillPointsUpdated;

    private int availableSkillPoints;
    void Start()
    {
        baseSkill.SetBought(true);
        SetAvailableSkillPoints(maxSkillPoints);
    }

    public bool CanLearn(SkillNode skill)
    {
        return !skill.IsBought() && skill.HasActiveLink() && skill.cost <= availableSkillPoints;
    }

    public bool CanForget(SkillNode skill)
    {
        return skill.IsBought() && skill.CanBeForgotten();
    }

    public void Forget(SkillNode skill)
    {
        if (skill == baseSkill)
        {
            Debug.Log("Can't forget base skill");
            return;
        }
        
        skill.SetBought(false);
        SetAvailableSkillPoints(availableSkillPoints + skill.cost);
    }

    public void ForgetAll()
    {
        foreach (var skill in skills)
        {
            Forget(skill);
        }
    }

    public void Learn(SkillNode skill)
    {
        skill.SetBought(true);
        SetAvailableSkillPoints(availableSkillPoints - skill.cost);
    }

    public void IncreaseMaxSkillPoints(int amount)
    {
        maxSkillPoints += amount;
        SetAvailableSkillPoints(availableSkillPoints + amount);
    }
    private void SetAvailableSkillPoints(int amount)
    {
        availableSkillPoints = Mathf.Clamp(amount, 0, maxSkillPoints);
        OnSkillPointsUpdated?.Invoke(availableSkillPoints);
    }
}
