using UnityEngine;
using UnityEngine.UI;

public class LearnButton : SkillListenerButton
{
    protected override void Init()
    {
        base.Init();
        
        _button.interactable = false;
    }

    protected override void HandleSkillSelected(SkillNode skill)
    {
        base.HandleSkillSelected(skill);
        Debug.Log("Can learn: " + skillTree.CanLearn(skill));
        _button.interactable = skillTree.CanLearn(skill);
    }

    protected override void HandleSkillChanged()
    {
        base.HandleSkillChanged();

        _button.interactable = skillTree.CanLearn(_selectedSkill);
    }

    public void Learn()
    {
        skillTree.Learn(_selectedSkill);
    }
}
