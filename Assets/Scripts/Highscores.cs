/*
 * Author: Alex Kam and Yuta Kataoka
 * Date: 2-8-19
 * Licence: Unity Personal Editor Licence
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscores : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    HighscoresJSON highscores = new HighscoresJSON { };

    private void Awake()
    {
        entryContainer = transform.Find("HighscoreContainer");
        entryTemplate = entryContainer.Find("template");
        entryTemplate.gameObject.SetActive(false);

        //Quick Create Highscores or Refresh
        //CreateNewHighscores();
        if (PlayerPrefs.HasKey("highscoreTable")==false)
            CreateNewHighscores();
        //Debug.Log(PlayerPrefs.HasKey("highscoreTable"));
        //Load The Table
        LoadTable();

        //Sort the Table
        SortTable();

        //Create the Table
        CreateTable();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
    }

    //===============PROCEDURE===============//
    private void CreateNewHighscores()
    //Purpose:          Creates a new HighscoresJSON file and some placeholder highscores
    {
        highscoreEntryList = new List<HighscoreEntry>()
        {
            //new HighscoreEntry{ name = "Aaron", waves = 5 },
            //new HighscoreEntry{ name = "Phillip", waves = 10},
            //new HighscoreEntry{ name = "Molly", waves = 4 },
            //new HighscoreEntry{ name = "Ben", waves = 6 },
            //new HighscoreEntry{ name = "Kenny", waves = 2 },
        };

        HighscoresJSON highscores = new HighscoresJSON { highscoreEntryList = highscoreEntryList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
    }

    //===============PROCEDURE===============//
    public static void AddHighscoreEntry(int waves, string name)
    //Purpose:          Adds a new highscore entry to the JSON file
    //int waves:        Stores the number of waves that were completed
    //string name:     Stores the name of the last player
    {
        //Create a Highscore Entry
        HighscoreEntry highscoreEntry = new HighscoreEntry { waves = waves, name = name };

        //Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighscoresJSON highscores = JsonUtility.FromJson<HighscoresJSON>(jsonString);

        //Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        //Save the updated highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    //===============PROCEDURE===============//
    private void LoadTable()
    //Purpose:          Loads the highscore JSON file
    {
        //Loads table from PlayerPrefs
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        highscores = JsonUtility.FromJson<HighscoresJSON>(jsonString);
    }

    //===============PROCEDURE===============//
    private void SortTable()
    //Purpose:          Sorts the highscores according to waves completed
    {
        HighscoreEntry temp;
        bool swaps = true;
        do
        {
            swaps = false;
            for (int i = 0; i < highscores.highscoreEntryList.Count - 1; i++)
            {
                if (highscores.highscoreEntryList[i].waves < highscores.highscoreEntryList[i + 1].waves)
                {
                    temp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[i + 1];
                    highscores.highscoreEntryList[i + 1] = temp;
                    swaps = true;
                }
            }
        }
        while (swaps);
    }

    //===============PROCEDURE===============//
    private void CreateTable()
    //Purpose:          Creates a table comprised of game objects on screen
    {
        int i = 0;
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            i++;
            if (i<=10)
                CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList,0);
        }
    }

    //===============PROCEDURE===============//
    public void Search()
    //Purpose:          Deletes previous game objects, searches for player name, and creates list findings
    {
        //Reset the Container and Template
        entryContainer = transform.Find("SearchCanvas").Find("SearchContainer");
        entryTemplate = entryContainer.Find("template");
        entryTemplate.gameObject.SetActive(false);

        //Delete all existing search containers
        GameObject[] existingSearch = GameObject.FindGameObjectsWithTag("search");
        //If array is not empty, then destroy all game objects
        if (existingSearch.Length>0)
        {
            int i = 0;
            while (i < existingSearch.Length)
            {
                Destroy(existingSearch[i].gameObject);
                i++;
            }
        }

        //Find the Text
        string inputText = transform.Find("SearchCanvas").Find("SearchInput").Find("TextArea").Find("Text")
            .GetComponent<TextMeshProUGUI>().text;
        
        //Trim off the space at the end
        string search = "";
        for(int i=0; i<inputText.Length-1; i++)
        {
            search += inputText[i];
            //Debug.Log(search);
        }

        //Redeclare some variables for searching
        int rank = 1;
        highscoreEntryTransformList = new List<Transform>();
        //Find and create all instances
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            //If found then create using rank
            if (highscoreEntry.name.Equals(search))
            {
                CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList,rank);
                Debug.Log("Found!");
            }
            rank++;
        }


        /* Whitebox Testing
        for (int i=0; i<inputText.Length-1; i++)
        {
            Debug.Log(inputText[i]);
        }
        Debug.Log(inputText + inputText.Length);
        Debug.Log(highscores.highscoreEntryList[0].name+ highscores.highscoreEntryList[0].name.Length);
        Debug.Log(inputText.Equals(highscores.highscoreEntryList[0].name));
        */
    }

    //===============PROCEDURE===============//
    private void CreateHighscoreEntryTransform(HighscoreEntry entry, Transform container, List<Transform> transformList, int overrideRank)
    //Purpose:          Creates a game object according to coordinates, and transform
    //HighscoreEntry entry: Holds the values of waves completed and player name
    //List<Transform> transformList: Determines where and what game objects are created
    //int overrideRank: Only to be used when searching as it shows true rank
    {
        float templateHeight = 40;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);
        
        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default: rankString = rank + "TH"; break;
        }
        int waves = entry.waves;
        string name = entry.name;
        if(overrideRank==0)
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;
        else
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = overrideRank.ToString();
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;
        entryTransform.Find("wavesText").GetComponent<TextMeshProUGUI>().text = waves.ToString();

        transformList.Add(entryTransform);
    }

    private class HighscoresJSON
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    public class HighscoreEntry
    {
        public int waves;
        public string name;
    }
}
