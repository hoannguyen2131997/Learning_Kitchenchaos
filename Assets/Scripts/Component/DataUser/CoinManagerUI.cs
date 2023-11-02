using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private Transform coinParent;
    [SerializeField] private int coinAmount;
    [SerializeField] private float maxX;
    [SerializeField] private float minx;
    [SerializeField] private float maxY;
    [SerializeField] private float miny;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float duration;
    [SerializeField] private float timeDelayToMoveCoin;
    [SerializeField] private TextMeshProUGUI coinText;

    public static CoinManagerUI Instance;
    private int coin;
    private List<GameObject> CoinList = new List<GameObject>();

    private Tween coinReactionTween;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [Button()]
    private async void CollectionCoins()
    {
        SetCoin(0);
        await CollectionCoinsAnimation();
    }

    public async UniTask CollectionCoinsAnimation(int coin = 0)
    {
        // Spawn Location of coin Object with random value
        for (int i = 0; i < CoinList.Count; i++)
        {
            Destroy(CoinList[i]);
        }

        CoinList.Clear();
        List<UniTask> spawCoinTaskList = new List<UniTask>();
        
        if (coin != 0)
        {
            coinAmount = coin;
        }
        
        for (int i = 0; i < coinAmount; i++)
        {
            GameObject coibInstance = Instantiate(coinPrefab, spawnLocation);
            float x = spawnLocation.position.x + Random.Range(minx, maxX);
            float y = spawnLocation.position.y + Random.Range(miny, maxY);
            coibInstance.transform.position = new Vector3(x, y);
            coibInstance.gameObject.transform.SetParent(coinParent);
            spawCoinTaskList.Add(coibInstance.transform.DOPunchPosition(new Vector3(0, 30, 0), Random.Range(0, 0.1f))
                .SetEase(Ease.InOutElastic)
                .ToUniTask());
            CoinList.Add(coibInstance);
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
        }

        await UniTask.WhenAll(spawCoinTaskList);
        // Move all the coins to the coin label 
        await MoveCoinsTask();
        // Animation the reaction when collecting coin
    }

    public void SetCoin(int _coin)
    {
        coin += _coin;
        coinText.text = coin.ToString();
    }

    private async UniTask MoveCoinsTask()
    {
        List<UniTask> moveCoinTask = new List<UniTask>();
        for (int i = 0; i < CoinList.Count; i++)
        {
            moveCoinTask.Add(MoveCoinTask(i));
            await UniTask.Delay(TimeSpan.FromSeconds(timeDelayToMoveCoin));
        }
    }

    private async UniTask MoveCoinTask(int i)
    {
        await CoinList[i].transform.DOMove(endPoint.position, duration).SetEase(Ease.InBack).ToUniTask();
        await ReactToCollectionCoin();
        SetCoin(1);
    }
    
    private async UniTask ReactToCollectionCoin()
    {
        if (coinReactionTween == null)
        {
            coinReactionTween = endPoint.DOPunchScale(new Vector3(.5f, .5f, .5f), 0.1f).SetEase(Ease.InOutElastic);
            await coinReactionTween.ToUniTask();
            coinReactionTween = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
