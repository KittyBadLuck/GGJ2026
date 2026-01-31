using MyBox;
using UnityEngine;

public class LevelSelectorMenu : MonoBehaviour
{
    public Transform gridParent;
    public GameObject levelSelectorPrefab;

    [Scene]
    public string[] puzzleSceneList;

    private void Start()
    {
        bool precedentIsUnlocked = true;
        int index = 0;
        foreach (var s in puzzleSceneList)
        {
            var go = Instantiate(levelSelectorPrefab, gridParent);
            LevelSelector lS = go.GetComponent<LevelSelector>();
            lS.SetUp(precedentIsUnlocked, s, index);
            index++;

            float score = PlayerPrefs.GetFloat(s, 0);
            precedentIsUnlocked =( score / 100) >( 1 - PuzzleManager.WIN_THRESHOLD);


        }
    }
}
