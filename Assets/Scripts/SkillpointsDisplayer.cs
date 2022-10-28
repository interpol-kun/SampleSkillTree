using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]

public class SkillpointsDisplayer : MonoBehaviour
{
    [SerializeField] private string baseText;
    private TMP_Text _text;
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        SkillTree.OnSkillPointsUpdated += UpdateSkillPoints;
    }

    private void UpdateSkillPoints(int amount)
    {
        _text.text = baseText + " " + amount.ToString();
    }

    private void OnDestroy()
    {
        SkillTree.OnSkillPointsUpdated -= UpdateSkillPoints;
    }
}
