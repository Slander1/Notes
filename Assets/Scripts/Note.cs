using TMPro;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI name;
    
    private string _description;
    private AddForm _addForm;
    private int _id; 
    
    public void Init(int id, NoteData noteData, AddForm addForm)
    {
        _id = id;
        _addForm = addForm;
        name.text = noteData.name;
        _description = noteData.description;
    }

    public void OnNoteClick()
    {
        _addForm.Show(_id, new NoteData {name = name.text , description = _description});
    }
}