using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIBehaviour : MonoBehaviour, IUpgradeItem
{
    [Header("Player Settings")]
    public PlayerData data;

    [Header("Logic FPS")]
    [Range(0, 60)]
    public int fps;

    [Header("HP")]
    [SerializeField] Image hpImage;

    [Header("Buff")]
    [SerializeField] Image boostImage;

    [Header("Bombs")]
    [SerializeField] Transform bombsPanel;
    [SerializeField] GameObject smartBombIcon;

    [Header("Lifes")]
    [SerializeField] TextMeshProUGUI numLifesText;

    [Header("Gold Rings")]
    [SerializeField] GameObject[] goldRings;

    private CustomFixedUpdate FU_LogicInstance;
    private List<GameObject> bombItems = new List<GameObject>();
    private int numGoldRings = 0;

    private void Awake()
    {
        UpdateGoldRing();
        FU_LogicInstance = new CustomFixedUpdate(OnLogicFixedUpdate, fps);

        /*PlayerActionsHandler.onAddSmartBomb += () => AddSmartBomb();
        PlayerActionsHandler.onRemoveSmartBomb += () => RemoveSmartBomb();
        PlayerActionsHandler.onAddLife += () => AddNumLife();
        PlayerActionsHandler.onRecoverLife += (int value, bool condition) => StartCoroutine(AddGoldRing(condition));*/

        data.onUpdateHP += () => UpdateHPImage();
        data.onUpdateSmartBomb += (int numAdd) => UpdateSmartBomb(numAdd);
    }

    void Update()
    {
        FU_LogicInstance.Update();
    }

    void OnLogicFixedUpdate(float dt)
    {
        UpdateBuffImage();
    }

    public void UpdateHPImage()
    {
        float life = data.lifeAmount * 0.01f;
        DOVirtual.Float(hpImage.fillAmount, life, .5f, x => hpImage.fillAmount = x);
    }

    public void UpdateBuffImage()
    {
        if (data.buffState == BuffState.None && data.buffAmount < 100)
        {
            data.buffAmount += Time.fixedDeltaTime * data.amountBoostIncrease * 100;
            boostImage.fillAmount = data.buffAmount * 0.01f;
        }
        else if (data.buffState != BuffState.None)
        {
            if (data.buffAmount > 0)
            {
                data.buffAmount -= Time.fixedDeltaTime * data.amountBoostDecrease * 100;
                boostImage.fillAmount = data.buffAmount * 0.01f;
            }
        }
    }

    void UpdateSmartBomb(int numAdd)
    {
        if (data.numSmartBombs == data.maxSmartBomb)
            return;

        data.numSmartBombs += numAdd;

        if (data.numSmartBombs > bombItems.Count)
        {
            for (int i = bombItems.Count; i < data.numSmartBombs; i++)
            {
                GameObject obj = Instantiate(smartBombIcon, bombsPanel);
                bombItems.Add(obj);
            }
        } else
        {
            for (int i = bombItems.Count; i > data.numSmartBombs; i--)
            {
                Destroy(bombItems[i - 1]);
                bombItems.RemoveAt(i - 1);
            }
        }

    }

    void Heal(int value, bool isGoldRing)
    {
        int newHP = data.lifeAmount + value;
        if (newHP > 100)
            data.lifeAmount = 100;
        else
            data.lifeAmount = newHP;

        UpdateHPImage();

        if (isGoldRing)
            StartCoroutine(AddGoldRing());
    }

    IEnumerator AddGoldRing()
    {
        numGoldRings += 1;
        UpdateGoldRing();

        if (numGoldRings >= 3)
        {
            AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.ThreeRingsCollect));
            UpdateNumLife(1);

            numGoldRings = 0;

            yield return new WaitForSeconds(3);

            UpdateGoldRing();
        }
    }

    void UpdateGoldRing()
    {
        for (int i = 0; i < goldRings.Length; i++)
        {
            if (i < numGoldRings)
            {
                goldRings[i].transform.GetChild(0).gameObject.SetActive(true);
                goldRings[i].GetComponent<Image>().enabled = false;
                goldRings[i].GetComponentInChildren<Animation>().Rewind();

                goldRings[i].transform.GetChild(0).transform.DOScale(Vector3.one * 2, .25f).SetLoops(2, LoopType.Yoyo);
            }
            else
            {
                goldRings[i].transform.GetChild(0).gameObject.SetActive(false);
                goldRings[i].GetComponent<Image>().enabled = true;
            }
        }
    }

    void UpdateNumLife(int numToAdd)
    {
        data.numLifes += numToAdd;
        numLifesText.text = "+" + data.numLifes.ToString();
        numLifesText.transform.DOScale(Vector3.one * 2, .25f).SetLoops(2, LoopType.Yoyo);
    }


    #region Interface
    public void UpgradeSmartBomb() => UpdateSmartBomb(+1);

    public void HealRing(int healValue, bool isGoldRing) => Heal(healValue, isGoldRing);

    public void AddLife() => UpdateNumLife(1);
    #endregion

}
