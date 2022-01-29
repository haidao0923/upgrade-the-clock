using UnityEngine;
using UnityEngine.UI;

public class BossUIPrefab : MonoBehaviour
{
    Text scriptedName, level, lifetime, skill, skillCooldown, bounty, quote;
    Image image;
    public Boss boss;

    void Start()
    {
        scriptedName = transform.Find("Name").GetComponent<Text>();
        level = transform.Find("Level").GetComponent<Text>();
        lifetime = transform.Find("Lifetime").GetComponent<Text>();
        skill = transform.Find("Skill").GetComponent<Text>();
        skillCooldown = transform.Find("Skill Cooldown").GetComponent<Text>();
        bounty = transform.Find("Bounty").GetComponent<Text>();
        quote = transform.Find("Quote").GetComponent<Text>();
        image = transform.Find("Images/Image").GetComponent<Image>();
    }

    void Update()
    {
        scriptedName.text = boss.name;
        level.text = "Level " + boss.level;
        lifetime.text = TimeFunction.ConvertValueToString(boss.currentLifetime);
        skill.text = boss.skillDisplay;
        skillCooldown.text = boss.skillCooldown > 0 ? boss.timer.ToString("0") + "s" : "Continuous";
        bounty.text = TimeFunction.ConvertValueToString(boss.currentBounty);
        quote.text = boss.quote[boss.level -1];
        image.sprite = boss.image;
    }
}
