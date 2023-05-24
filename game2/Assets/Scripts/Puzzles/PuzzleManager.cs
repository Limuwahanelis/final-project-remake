using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<Puzzle> Puzzels { get => _puzzleList; }
    [SerializeField] List<Puzzle> _puzzleList = new List<Puzzle>();
    [SerializeField] SceneStateManager sceneSaveManager;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _puzzleList.Count; i++)
        {
            _puzzleList[i].OnSolved += SavePuzzlepState;
        }
    }
    private void SavePuzzlepState(Puzzle puzzle)
    {
        sceneSaveManager.ChangePuzzleState(_puzzleList.IndexOf(puzzle), true);
    }
    public void MarkPuzzlesAsSolved(List<bool> isSolved)
    {
        for(int i=0;i<isSolved.Count;i++)
        {
            if(isSolved[i]) _puzzleList[i].MarkAsSolved();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
