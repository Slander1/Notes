using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class MainScript : MonoBehaviour
{

    [SerializeField] private Note note;
    [SerializeField] private AddForm addForm;
    [SerializeField] private Transform content;
    
    private List<NoteData> _notes = new List<NoteData>();

    private void Awake()
    {
        CreateAllNotes();
    }

    private void OnEnable()
    {
        addForm.OnSaved+= AddFormOnOnSaved;
        addForm.OnDelete += AddFormOnOnDelete;
    }
    private void Ondesable()
    {
            addForm.OnSaved -= AddFormOnOnSaved;
            addForm.OnDelete -= AddFormOnOnDelete;
    }

    private void AddFormOnOnDelete(int id)
    {
        _notes.RemoveAt(id);
        SaveData();
        CreateAllNotes();
    }

    private void AddFormOnOnSaved(int id, NoteData noteData)
    {
        if (id == -1)
            _notes.Add(noteData);
        else
            _notes[id] = noteData;
        SaveData();
        CreateAllNotes();
    }


    public void NewNoteOnClick()
    {
        addForm.Show(-1, new NoteData());
        
    }
    
    private void CreateAllNotes()
    {
        foreach(Transform child in content)
        {
            Destroy(child.gameObject);
        }
        LoadData();
        for (var i = 0; i < _notes.Count; i++)
        {
            var noteData = _notes[i];
            var newNote = Instantiate(note, content);
            newNote.Init(i, new NoteData{name = noteData.name, description = noteData.description} , addForm);
        }

    }

    private void LoadData()
    {
        _notes = JsonConvert.DeserializeObject<List<NoteData>>(PlayerPrefs.GetString(nameof(_notes)));
    }

    private void SaveData()
    {
        var json = JsonConvert.SerializeObject(_notes);
        PlayerPrefs.SetString(nameof(_notes),  json);
    }
}