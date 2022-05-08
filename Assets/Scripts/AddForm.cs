using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddForm : MonoBehaviour
{
    
    [SerializeField] private TMP_InputField nameNote;
    [SerializeField] private TMP_InputField description;
    [SerializeField] private Button deleteButton;

    private readonly string _warning = "Поле имени не может быть пустым";
    private int _id;

    public event Action<int, NoteData> OnSaved;
    public event Action<int> OnDelete; 
    
    public void OnAddNoteClick()
    {
        if (nameNote.text == string.Empty || nameNote.text == _warning)
        {
            nameNote.text = _warning;
            return;
        }
        gameObject.SetActive(false);
        OnSaved?.Invoke(_id, new NoteData{name = nameNote.text, description = description.text});
    }
    
    public void OnDeleteClick()
    {
        OnDelete?.Invoke(_id);
        gameObject.SetActive(false);
    }

    public void OnCancelClick()
    {
        gameObject.SetActive(false);
    }
    
    public void Show(int id, NoteData noteData)
    {
        _id = id;
        gameObject.SetActive(true);
        deleteButton.gameObject.SetActive((_id > -1));
        nameNote.text = noteData.name;
        description.text = noteData.description;
    }
}
