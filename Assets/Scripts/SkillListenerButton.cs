using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkillListenerButton : MonoBehaviour
{
    protected Button _button;
    protected SkillNode _selectedSkill;
    
    [SerializeField] protected SkillTree skillTree;
    
    private void Start()
    {
        _button = GetComponent<Button>();
        SkillButton.OnSelectSkill += HandleSkillSelected;
        
        Init();
    }

    protected virtual void Init() {}
    protected virtual void HandleSkillChanged() {}

    protected virtual void HandleSkillSelected(SkillNode skill)
    {
        if (_selectedSkill != null)
        {
            _selectedSkill.OnChanged -= HandleSkillChanged;
        }

        _selectedSkill = skill;
        _selectedSkill.OnChanged += HandleSkillChanged;
    }

}
