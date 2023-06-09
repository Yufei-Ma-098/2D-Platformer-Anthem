using UnityEngine;

public class FragmentsManager : MonoBehaviour
{
    public int maxFragments = 3;
    public int currentFragments;


    private void Start()
    {
        currentFragments = 0;
        // UpdateFragmentsUI();
    }

    /*
    private void UpdateFragmentsUI()
    {
        for (int i = 0; i < fragmentIcons.Length; i++)
        {
            if (i < currentFragments)
            {
                fragmentIcons[i].gameObject.SetActive(true);
            }
            else
            {
                fragmentIcons[i].gameObject.SetActive(false);
            }
        }
    }
    */

    public void UpdateFragmentsCount(int fragments)
    {
        currentFragments = fragments;
        // UpdateFragmentsUI();
    }
}
