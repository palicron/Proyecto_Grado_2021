using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecyclingDiary : MonoBehaviour
{
    #region Singleton
    public static RecyclingDiary instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one diary instance found");
        }
        instance = this;
    }
    #endregion

    public GameObject recyclingDiary;

    List<Entry> entriesList = new List<Entry>();

    public GameObject[] entriesSlots;

    public int actualIndex;

    public GameObject nextArrow;

    public GameObject previousArrow;

    EntryComparer comparer;

    public Text detailTitle;

    public Text detailDescription;

    public GameObject detailWindow;

    public class Entry
    {
        public string entry;
        public string entryName;
        public string entryNpc;
        public int entryId;

        public Entry(string pEntry, string pName, string pNpc, int pId)
        {
            entry = pEntry;
            entryName = pName;
            entryNpc = pNpc;
            entryId = pId;
        }
    }

    class EntryComparer : IComparer<Entry>
    {
        public int Compare(Entry x, Entry y)
        {
            int xId = x.entryId;
            int yId = y.entryId;

            if (xId == 0 || yId == 0)
            {
                return 0;
            }

            // CompareTo() method
            return (xId).CompareTo(yId);

        }
    }

    void Start()
    {
        //ResetEntries();
        comparer = new EntryComparer();
        actualIndex = 0;
        ResetEntries();
        TranslateSavedLists();
        CheckDisableButton();
        BuildPage();
    }

    void ResetEntries()
    {
        PlayerPrefsX.SetStringArray("D_Entries", new string[0]);
        PlayerPrefsX.SetStringArray("D_Names", new string[0]);
        PlayerPrefsX.SetStringArray("D_NPCs", new string[0]);
        PlayerPrefsX.SetIntArray("D_IDs", new int[0]);
    }

    public void AddEntry(string pEntry, string pName, string pNpc, int pId)
    {
        Debug.Log("Se añadió una entrada");
        Entry repeated = entriesList.Find(x => x.entryId == pId);
        if(repeated==null)
        {
            Entry newEntry = new Entry(pEntry, pName, pNpc, pId);
            entriesList.Add(newEntry);
            entriesList.Sort(comparer);
            BuildPage();
            CheckDisableButton();
        }
    }

    void TranslateSavedLists()
    {
        string[] entries = PlayerPrefsX.GetStringArray("D_Entries");
        string[] entriesNames = PlayerPrefsX.GetStringArray("D_Names");
        string[] entriesNpcs = PlayerPrefsX.GetStringArray("D_NPCs");
        int[] entriesId = PlayerPrefsX.GetIntArray("D_IDs");
        for(int i = 0; i < entries.Length ;  i++)
        {
            Entry newEntry = new Entry(entries[i], entriesNames[i], entriesNpcs[i], entriesId[i]);
            entriesList.Add(newEntry);
        }
    }

    void OnDestroy()
    {
        string[] entries = new string[entriesList.Count];
        string[] entriesNames = new string[entriesList.Count];
        string[] entriesNpcs = new string[entriesList.Count];
        int[] entriesId = new int[entriesList.Count];
        for (int i = 0; i < entriesList.Count; i++)
        {
            entries[i] = entriesList[i].entry;
            entriesNames[i] = entriesList[i].entryName;
            entriesNpcs[i] = entriesList[i].entryNpc;
            entriesId[i] = entriesList[i].entryId;
        }
        PlayerPrefsX.SetStringArray("D_Entries", entries);
        PlayerPrefsX.SetStringArray("D_Names", entriesNames);
        PlayerPrefsX.SetStringArray("D_NPCs", entriesNpcs);
        PlayerPrefsX.SetIntArray("D_IDs", entriesId);
    }
        
    void Update()
    {
        if (Input.GetButtonDown("Diary"))
        {
            bool active = !recyclingDiary.activeSelf;
            recyclingDiary.SetActive(active);
            UI_Status.instance.SetOpen(active, MenuType.Diary);
            if(!active)
            {
                CloseDetailButton();
            }
        }
    }

    public void ShowDetail(int p)
    {
        Entry entry = entriesList[p+actualIndex];
        if(entry!=null)
        {
            detailTitle.text = entry.entryName;
            detailDescription.text = entry.entryNpc+": "+entry.entry;
        }
        detailWindow.SetActive(true);
    }

    public void BuildPage()
    {
        for(int i = actualIndex; i < 6 + actualIndex; i++)
        {
            if( entriesList.Count <= i )
            {
                entriesSlots[i - actualIndex].SetActive(false);
            }
            else
            {
                entriesSlots[i - actualIndex].SetActive(true);
                entriesSlots[i - actualIndex].GetComponentInChildren<Text>().text = entriesList[i].entryId+". "+entriesList[i].entryName;
            }
        }
    }

    public void CheckPage(bool p)
    {
        int pMov = 6;
        if(!p)
        {
            pMov = -6;
        }
        ///
        if (entriesList.Count <= actualIndex + pMov)
        {
            return;
        }
        actualIndex += pMov;
        UI_SFX.instance.PlayTurnPage();
        CheckDisableButton();
        BuildPage();
    }

    public void CloseButton()   
    {
        recyclingDiary.SetActive(false);
        CloseDetailButton();
    }

    public void CloseDetailButton()
    {
        detailWindow.SetActive(false);
    }

    void CheckDisableButton()
    {
        if (actualIndex + 6 >= entriesList.Count)
        {
            nextArrow.GetComponent<Button>().interactable = false;
        }
        else
        {
            nextArrow.GetComponent<Button>().interactable = true;
        }
        //
        if (actualIndex == 0)
        {
            previousArrow.GetComponent<Button>().interactable = false;
        }
        else
        {
            previousArrow.GetComponent<Button>().interactable = true;
        }
    }
}
