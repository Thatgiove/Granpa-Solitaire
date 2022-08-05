using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class RulesList
{
    [SerializeField]
    public Rule[] Rules;
}

[Serializable]
class Rule
{
    [SerializeField]
    public string RuleName;
    [SerializeField]
    public string[] RulesDescription;

}

public class TutorialManager : MonoBehaviour
{
    [SerializeField] TMP_Text description;
    [SerializeField] Button nextRuleBtn;

    int index = 0;
    string thirdRuleSeed;

    RulesList ruleList;
    Rule currentRule;

    void Awake()
    {
        CreateTutorial();
    }

    void Start()
    {
        if (!description || !nextRuleBtn) return;

        nextRuleBtn.onClick.AddListener(NextDescription);
    }

    public void CreateTutorial()
    {

        var json = Resources.Load<TextAsset>("Json/rules");

        if (json)
        {
            ruleList = JsonUtility.FromJson<RulesList>(json.text);

            ruleList.Rules[0].RulesDescription[2] =
                ruleList.Rules[0].RulesDescription[2].Replace("*", Resolver(GameInstance.principalCard.cardInfo.Description));

            ruleList.Rules[1].RulesDescription[0] =
               ruleList.Rules[1].RulesDescription[0].Replace("*", GameInstance.principalCard.cardInfo.Id.ToString());

            if (!string.IsNullOrEmpty(thirdRuleSeed))
            {
                ruleList.Rules[2].RulesDescription[0] =
                        ruleList.Rules[2].RulesDescription[0].Replace("*", Resolver(thirdRuleSeed));
            }
           
        }
    }

    public void OpenTutorialPanel(int ruleIndex)
    {
        gameObject.SetActive(true);
        currentRule = ruleList.Rules[ruleIndex];
        index = 0;
        NextDescription();
    }

    void NextDescription()
    {
        var desc = currentRule.RulesDescription.ElementAtOrDefault(index);

        if (currentRule.RulesDescription.ElementAtOrDefault(index + 1) == null)
        {
            nextRuleBtn.gameObject.GetComponentInChildren<TMP_Text>().text = "Ok";
            nextRuleBtn.onClick.RemoveAllListeners();
            nextRuleBtn.onClick.AddListener(Close);
        }

        description.text = desc;
        index++;
    }
    public void SetThirdRule(string seed)
    {
        thirdRuleSeed = seed;
        //Se non c'è la ruleList richiama awake
        if (ruleList == null)
        {
            return;
        }
        ruleList.Rules[2].RulesDescription[0] =
              ruleList.Rules[2].RulesDescription[0].Replace("*", Resolver(thirdRuleSeed));
    }
    void Close()
    {
        gameObject.SetActive(false);
    }
    string Resolver(string seed)
    {
        var _seed = "";

        switch (seed)
        {
            case "Gold":
                _seed = "DENARA";
                break;
            case "Sword":
                _seed = "SPADE";
                break;
            case "Cup":
                _seed = "COPPE";
                break;
            case "Stick":
                _seed = "BASTONI";
                break;
        }

        return _seed;
    }

}
