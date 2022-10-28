using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngineInternal;

public class SkillTree : MonoBehaviour
{
    public SkillNode[] skills;
    public delegate void SkillPointsUpdated(int amount);
    public static event SkillPointsUpdated OnSkillPointsUpdated;
    
    [SerializeField] private SkillNode baseSkill;
    [SerializeField] private int maxSkillPoints;
    
    private int availableSkillPoints;

    private List<SkillNode> _cutpoints = new List<SkillNode>();
    private int _counter;

    private void Start()
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
        return skill.IsBought() && !_cutpoints.Contains(skill);
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
        FindCutpoints();
        OnSkillPointsUpdated?.Invoke(availableSkillPoints);
    }
    
    //Articulation points functions

    private void FindCutpoints()
    {
        _cutpoints.Clear();
        _counter = 0;
        foreach (var skill in skills)
        {
            skill.isVisited = false;
        }
        AssignNum(baseSkill);
        AssignLow(baseSkill);
        FindArt(baseSkill);
    }

    private void AssignNum(SkillNode v)
    {
        v.num = _counter++;
        v.isVisited = true;
        foreach (var w in v.neighbours)
        {
            if (!w.IsBought())
                continue;
            
            if (!w.isVisited)
            {
                w.parent = v;
                AssignNum(w);
            }
        }
    }

    private void AssignLow(SkillNode v)
    {
        v.low = v.num;
        foreach (var w in v.neighbours)
        {
            if (!w.IsBought())
                continue;
            
            if (w.num > v.num)
            {
                AssignLow(w);
                if (w.low >= v.num)
                {
                    _cutpoints.Add(v);
                }

                v.low = Mathf.Min(v.low, w.low);
            }
            else
            {
                if (v.parent != w)
                {
                    v.low = Mathf.Min(v.low, w.num);
                }
            }
        }
    }

    private void FindArt(SkillNode v)
    {
        v.isVisited = true;
        v.low = v.num = _counter++;
        foreach (var w in v.neighbours)
        {
            if (!w.IsBought())
                continue;
            
            if (!w.isVisited)
            {
                w.parent = v;
                FindArt(w);
                if (w.low >= v.num)
                {
                    _cutpoints.Add(v);
                }

                v.low = Mathf.Min(v.low, w.low);
            }
            else
            {
                if (v.parent != w)
                {
                    v.low = Mathf.Min(v.low, w.num);
                }
            }
        }
    }
}
