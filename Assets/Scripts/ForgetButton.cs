public class ForgetButton : SkillListenerButton
{
    protected override void Init()
    {
        base.Init();

        _button.interactable = false;
    }

    protected override void HandleSkillSelected(SkillNode skill)
    {
        base.HandleSkillSelected(skill);

        _button.interactable = skillTree.CanForget(skill);
    }

    protected override void HandleSkillChanged()
    {
        _button.interactable = skillTree.CanForget(_selectedSkill);
    }

    public void Forget()
    {
        skillTree.Forget(_selectedSkill);
    }
}
