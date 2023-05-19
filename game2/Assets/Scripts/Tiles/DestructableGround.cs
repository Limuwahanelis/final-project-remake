using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;


public class DestructableGround : MonoBehaviour
{
    enum direction
    {
        LEFT,
        DOWN,
        RIGHT
    }

    [SerializeField] UnityEvent OnTilesDestroyed;
    [SerializeField] AudioSourcePool sourcePool;
    [SerializeField] GameObject explosion;
    [SerializeField] LayerMask bombLayer;
    [SerializeField] AudioEvent explosionSound;
    [SerializeField] float destructionDelay = 0.1f;
    private Tilemap map;
    private bool destroyTiles = false;
    private Vector3Int firstTileToDestroy;




    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Tilemap>();
        map.SetTile(map.WorldToCell(Vector3.zero), null);
    }
    public void DestroyTiles(float triggerRadius, Vector3 bombPos)
    {
        Debug.Log("Dsadsada");
        if (map.GetTile(map.WorldToCell(new Vector3(bombPos.x - triggerRadius, bombPos.y, 0))))
        {
            firstTileToDestroy = map.WorldToCell(new Vector3(bombPos.x - triggerRadius, bombPos.y, 0));
            StartCoroutine( DestroyTilesLeft());
        }
        if (map.GetTile(map.WorldToCell(new Vector3(bombPos.x + triggerRadius, bombPos.y, 0))))
        {
            firstTileToDestroy = map.WorldToCell(new Vector3(bombPos.x + triggerRadius, bombPos.y, 0));
            StartCoroutine( DestroyTilesRight());
        }
        //if (map.GetTile(map.WorldToCell(new Vector3(bombPos.x, bombPos.y - triggerRadius, 0))))
        //{
        //    firstTileToDestroy = map.WorldToCell(new Vector3(bombPos.x, bombPos.y - triggerRadius, 0));
        //    DestroyTilesDown();
        //    dir = direction.DOWN;
        //}

    }
    IEnumerator DestroyTilesRight()
    {
        Vector3Int curTile = firstTileToDestroy; //map.WorldToCell(new Vector3(firstTileToDestroy.x + 1.01f, firstTileToDestroy.y, 0));
        while (map.GetTile(curTile))
        {
            StartCoroutine(DestroyTileAtCellPos(curTile));
            Vector3Int tileHigher = map.WorldToCell(new Vector3(curTile.x, curTile.y + 1.01f, 0));
            Vector3Int tileLower = map.WorldToCell(new Vector3(curTile.x, curTile.y - 0.5f, 0));
            if (map.GetTile(tileHigher))
            {
                yield return new WaitForSeconds(destructionDelay/2);
                StartCoroutine( DestroyTileAtCellPos(tileHigher));
            }
            if (map.GetTile(tileLower))
            {
                StartCoroutine( DestroyTileAtCellPos(tileLower));
            }
            
            curTile = map.WorldToCell(new Vector3(curTile.x + 1.01f, firstTileToDestroy.y, 0));
            yield return new WaitForSeconds(destructionDelay);
        }
    }
    IEnumerator DestroyTilesLeft()
    {
        Vector3Int curTile = firstTileToDestroy; //map.WorldToCell(new Vector3(firstTileToDestroy.x + 1.01f, firstTileToDestroy.y, 0));
        while (map.GetTile(curTile))
        {
            StartCoroutine( DestroyTileAtCellPos(curTile));
            Vector3Int tileHigher = map.WorldToCell(new Vector3(curTile.x, curTile.y + 1.01f, 0));
            Vector3Int tileLower = map.WorldToCell(new Vector3(curTile.x, curTile.y - 0.5f, 0));
            TileBase lower = map.GetTile(tileLower);
            TileBase upper = map.GetTile(tileHigher);
            while (lower || upper)
            {
                yield return new WaitForSeconds(destructionDelay / 2);
                if(upper) StartCoroutine(DestroyTileAtCellPos(tileHigher));
                if(lower) StartCoroutine(DestroyTileAtCellPos(tileLower));
                tileHigher = map.WorldToCell(new Vector3(tileHigher.x, tileHigher.y + 1.01f, 0));
                tileLower = map.WorldToCell(new Vector3(tileLower.x, tileLower.y -0.5f,0));
                lower = map.GetTile(tileLower);
                upper = map.GetTile(tileHigher);
            }
            //if (map.GetTile(tileHigher))
            //{
            //    yield return new WaitForSeconds(destructionDelay / 2);
            //    StartCoroutine( DestroyTileAtCellPos(tileHigher));
            //}
            //if (map.GetTile(tileLower))
            //{
            //   StartCoroutine( DestroyTileAtCellPos(tileLower));
            //}
            
            curTile = map.WorldToCell(new Vector3(curTile.x - 0.5f, firstTileToDestroy.y, 0));
            yield return new WaitForSeconds(destructionDelay);
            OnTilesDestroyed?.Invoke();
        }
    }

    IEnumerator DestroyTileAtCellPos(Vector3Int cellPos)
    {
       
        yield return new WaitForSeconds(destructionDelay);
        AudioSource audioSourceTmp = sourcePool.GetSource();
        int explosionsNum = 2;
        GameObject[] explosions = new GameObject[explosionsNum];

        explosionSound.Play(audioSourceTmp);
       // StartCoroutine(DestroyAudioSourceAfterPlay(audioSourceTmp));
        for (int i = 0; i < explosionsNum; i++)
        {
            explosions[i] = Instantiate(explosion, map.CellToWorld(cellPos) + new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f)), explosion.transform.rotation, transform);
            Destroy(explosions[i], 1f);
        }

        map.SetTile(cellPos, null);
    }

    IEnumerator DestroyAudioSourceAfterPlay(AudioSource source)
    {
        while(source.isPlaying)
        {
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(source);
    }
}
