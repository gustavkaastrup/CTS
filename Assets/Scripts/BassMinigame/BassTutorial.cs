using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BassTutorial : MonoBehaviour
{
    [TextArea]
    public List<string> lines;
    public int linesIndex = 0;
    public GameObject tutorialText;

    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = tutorialText.GetComponent<TextMeshProUGUI>();
        NextLine();
    }
    public void NextLine()
    {
        if (linesIndex < lines.Count)
        {
            textMeshPro.text = lines[linesIndex];
            linesIndex++;
            if (linesIndex == lines.Count)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
