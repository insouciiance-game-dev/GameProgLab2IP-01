using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [field: SerializeField]
    public TextMeshProUGUI Text { get; private set; }

    [field: SerializeField]
    public ItemLooter Looter { get; private set; }

    private int _lootCount;

    public void Start()
    {
        Looter.ItemLooted += _ =>
        {
            _lootCount++;
            Text.text = $"Collected: {_lootCount}";
        };
    }
}
