using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [field: SerializeField]
    public ItemLooter ItemLooter { get; private set; }

    [field: SerializeField]
    public int SceneIndex { get; private set; }

    [field: SerializeField]
    public int LootThreshold { get; private set; }

    private int itemsLooted;

    private void Start()
    {
        ItemLooter.ItemLooted += _ => itemsLooted++;
    }

    private void OnTriggerEnter()
    {
        if (itemsLooted >= LootThreshold)
            SceneManager.LoadScene(SceneIndex);
    }
}
