using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkillButton : MonoBehaviour
{
    [SerializeField] private Color boughtColor;
    [SerializeField] private Color lockedColor;

    [SerializeField] private SkillNode skill;
    
    [SerializeField] private TMP_Text _text;

    public delegate void SkillSelected(SkillNode skill);

    public static event SkillSelected OnSelectSkill;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        skill.OnChanged += HandleSkillChanged;
        SetButtonColor(skill.IsBought() ? boughtColor : lockedColor);

        _text.text = skill.skillName;
    }

    private void HandleSkillChanged()
    {
        SetButtonColor(skill.IsBought() ? boughtColor : lockedColor);
    }

    public void Select()
    {
        Debug.Log("Button selected");
        OnSelectSkill?.Invoke(skill);
    }
    
    private void SetButtonColor(Color color)
    {
        var colors = _button.colors;
        colors.normalColor = color;
        _button.colors = colors;
    }
}
