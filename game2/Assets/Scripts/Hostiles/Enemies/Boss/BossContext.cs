using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossContext
{
    public BossAudioManager bossAudio;
    public BossCrystalManager crystals;
    public GameObject[] beams;
    public GameObject missilePrefab;
    public Transform playerTrans;
    public Vector3 missileSpawnPos;
    public Vector3 attackPos;
    public Vector3 vulnerablePos;
    public Vector3 delayedBeamPos;
    public float speed;
    public float vulnerableTime;
    public float attackDelay;
    public int attackPatten;
    public BossContext()
    {

    }
}
