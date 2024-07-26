using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header(" Sounds ")]
    [SerializeField] private AudioSource bowlerShootSound;
    [SerializeField] private AudioSource ballHitGroundSound;
    [SerializeField] private AudioSource ballFellInWater;
    [SerializeField] private AudioSource wicketSound;
    [SerializeField] private AudioSource batHitBallSound;



    private void Start()
    {
        PlayerBowler.onBallThrown += PlayBowlerSound;
        PlayerBatsman.onBallHit += PlayBatHitBallSound;

        AIBowler.onBallThrown += PlayBowlerSound;
        AIBatsman.onBallHit += PlayBatHitBallSound;

        Ball.onBallHitGround += PlayBallHitGroundSound;
        Ball.onBallFellInWater += PlayBallFellInWaterSound;
        Ball.onBallHitStump += PlayWicketSound;
    }


    private void OnDestroy()
    {
        PlayerBowler.onBallThrown -= PlayBowlerSound;
        PlayerBatsman.onBallHit -= PlayBatHitBallSound;

        AIBowler.onBallThrown -= PlayBowlerSound;
        AIBatsman.onBallHit -= PlayBatHitBallSound;

        Ball.onBallHitGround -= PlayBallHitGroundSound;
        Ball.onBallFellInWater -= PlayBallFellInWaterSound;
        Ball.onBallHitStump -= PlayWicketSound;
    }


    private void PlayBowlerSound(float nothingUseful)
    {
        bowlerShootSound.pitch = Random.Range(1.2f, 1.3f);

        bowlerShootSound.Play();
    }


    private void PlayBallHitGroundSound(Vector3 nothingUseful)
    {
        ballHitGroundSound.pitch = Random.Range(0.95f, 1.05f);

        ballHitGroundSound.Play();
    }


    private void PlayBallFellInWaterSound(Vector3 nothingUseful)
    {
        ballFellInWater.pitch = Random.Range(0.95f, 1.05f);

        ballFellInWater.Play();
    }


    private void PlayWicketSound()
    {
        wicketSound.pitch = Random.Range(0.9f, 1f);

        wicketSound.Play();
    }


    private void PlayBatHitBallSound(Transform nothingUseful)
    {
        batHitBallSound.pitch = Random.Range(0.9f, 1f);

        batHitBallSound.Play();
    }


    public void DisableSounds()
    {
        bowlerShootSound.volume = 0;
        ballHitGroundSound.volume = 0;
        ballFellInWater.volume = 0;
        wicketSound.volume = 0;
        batHitBallSound.volume = 0;
    }


    public void EnableSounds()
    {
        bowlerShootSound.volume = 0.5f;
        ballHitGroundSound.volume = 1;
        ballFellInWater.volume = 0.7f;
        wicketSound.volume = 1;
        batHitBallSound.volume = 1;
    }
}
