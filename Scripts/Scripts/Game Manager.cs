using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using System;
public class GameManager : MonoBehaviour
{
    [SerializeField] private List<CardsSprite> cardSprites = new List<CardsSprite>();
    private List<int> CardsCountValues = new List<int>();
    private int[] Pintock = new int[24];
    private int[] numberOfCardsPlay = new int[52];
    int coinBack = 1;
    public int coinDisplay;
    public double money = 1000, bets = 0, youWin, coinValueStore, tra = 0;
    private int playertotalScore, playertotalScore1, playertotalScore2, playertotalScore3, playertotalScore4, machintotalScore;
    int isCoins;
    int cardsValues;
    float ClockLoading;
    public float Clock = 1f;
    private bool TimeON = true;
    bool action;
    private bool IsHitBtnOn, dealOnClick, IsStandBtnOn, IsCallAgainOn;
    public Text textTimer;
    public Text MoneyStorage;
    public Text textMinute, textSecond;
    public Text  textmachineScoreShow;
    public Text[] textplayerScoreShow;
    [SerializeField] public Button hitbtnClick, machinebtnClick, standbtn, callcardsAgain;
    public Button callGameStartBtn, undoCoins, callMuiltCards;
    [SerializeField] public Button[] coinNormal;
    public GameObject showPane;
    [SerializeField] public GameObject hitObject, callcardAgainOject;
    [SerializeField] public GameObject callStartGameEnable, undoBtnEnable;
    public GameObject equalPlayer, playerwinner, machinewinner, coin_panel;
    public GameObject[] p1_win, p2_win, p3_win, p4_win, p_win;
    public GameObject[] playerCardsObject;
    public GameObject[] points_score;
    public GameObject[] machineCardsObject;
    public Animator cardAnimate;
    public Animator[] coinAinmate;
    public Animator equalAnimate, playerWinAnimate, playerLoseAnimate;
    public Image[] CardsForPlayer;
    public Image[] CardForMachinePlay;
    public Sprite backCard;
    public Text tra_txt;

    // TODO: Start is called before the first frame update
    void Start()
    {
        textMinute.text = textSecond.text = "00";
        FindAnyObjectByType<AudioManage>().StartPlayMusic("background");
        StartCoroutine(cardsOfGameStant());
        hitbtnClick.enabled = false;
        hitbtnClick.image.color = Color.gray;
        machinebtnClick.enabled = false;
        machinebtnClick.image.color = Color.gray;
        standbtn.enabled = false;
        standbtn.image.color = Color.gray;
        undoCoins.enabled = false;
        undoCoins.image.color = Color.gray;
        callGameStartBtn.enabled = false;
        callGameStartBtn.image.color = Color.gray;
    }

    // TODO: Update is called once per frame
    void Update()
    {
        GameStartRunning();
        MoneyStorage.text = money.ToString() + " k";

        // timer
        ClockLoading = Time.time;
        int minutes = (int)ClockLoading / 60;
        int seconds = (int)ClockLoading % 60;

        textMinute.text = minutes.ToString();
        textSecond.text = seconds.ToString();

    }

    // TODO: Wait chard
    IEnumerator cardsOfGameStant()
    {
        showPane.SetActive(true);
       // cardAnimate.Play("bannerShow");
        yield return new WaitForSeconds(1f);
        playerCardsObject[0].SetActive(false);
        playerCardsObject[1].SetActive(false);
        playerCardsObject[2].SetActive(false);
        playerCardsObject[3].SetActive(false);

        machineCardsObject[0].SetActive(false);
        machineCardsObject[1].SetActive(false);
        machineCardsObject[2].SetActive(false);
        machineCardsObject[3].SetActive(false);
        showPane.SetActive(false);
    }

    // TODO: Random cards
    void GameStartProcessing()
    {
        for (int i = 0; i < 24; i++) // 8
        {
            int Rand = Random.Range(0, 52);
            while (CardsCountValues.Contains(Rand))
            {
                Rand = Random.Range(0, 52);
            }
            CardsCountValues.Add(Rand);
           // print("////////// index of cards: " + CardsCountValues[i] + ", Card Name: " + cardSprites[Rand].CardsName);
        }

    }

    // hit card 1 by 1 card
    int GameCompareSpriteCards(int fistIndex, int lastIndex, int getSpriteIndex, Image[] getImage, GameObject[] gameObject, List<int> listOfCards)
    {
        int numbersOfCards = 0;
        for (int item1 = fistIndex; item1 < lastIndex; item1++)
        {
            for (int item2 = 0; item2 < cardSprites.Count; item2++)
            {
                if (listOfCards[item1] == item2)
                {
                    listOfCards[item1] %= 13;
                    cardsValues = numbersOfCards = listOfCards[item1];
                    if (listOfCards[item1] > 10)
                    {
                        numbersOfCards = 10;
                        numberOfCardsPlay[item2] = numbersOfCards;
                    }
                    else if (listOfCards[item1] == 0)
                    {
                        numbersOfCards = 11;
                       //ace_value = 1;
                        numberOfCardsPlay[item2] = numbersOfCards;
                    }
                    else if (listOfCards[item1] == 10)
                        numberOfCardsPlay[item2] = numbersOfCards;
                    else
                        numberOfCardsPlay[item2] = numbersOfCards++;
                    numberOfCardsPlay[item2] = numbersOfCards++;
                    Pintock[fistIndex] = numberOfCardsPlay[item2];
                    gameObject[getSpriteIndex].SetActive(true);
                    getImage[getSpriteIndex].sprite = cardSprites[item2].Cards;
                    //print("////////// index of cards: " + numberOfCardsPlay[item2] + ", Card Name: " + cardSprites[item2].CardsName);
                    //print("Pintock: " + Pintock[fistIndex]);
                }
            }
        }
        // make for check value of card spites 
        return cardsValues;
    }

    // TODO: Start play
    void GameStartRunning()
    {
        if (TimeON)
        {
            Clock -= Time.deltaTime;
            textTimer.text = Mathf.Round(Clock).ToString();
        }
        if (Clock <= 0 && TimeON)
        {
            StartCoroutine(FindAnyObjectByType<PlayerCoin_Suffer>().catch_coin());
            TimeON = false;
        }
    }

    // TODO: Game play again

    IEnumerator GameProccesContinue()
    {
        CardsCountValues.Clear();
        GameStartProcessing();

        cardAnimate.Play("p1_c1");
        yield return new WaitForSeconds(1f);
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        points_score[0].SetActive(true);
        GameCompareSpriteCards(0, 1, 8, CardsForPlayer, playerCardsObject, CardsCountValues);
        playertotalScore1 += Pintock[0];
        textplayerScoreShow[0].text = playertotalScore1.ToString();
        //print("------- Score: " + Pintock[0]);  
        
        cardAnimate.Play("p2_c1");
        yield return new WaitForSeconds(1f);
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        points_score[1].SetActive(true);
        GameCompareSpriteCards(1, 2, 4, CardsForPlayer, playerCardsObject, CardsCountValues);
        playertotalScore2 += Pintock[1];
        textplayerScoreShow[1].text = playertotalScore2.ToString();
        //print("------- Score: " + Pintock[1]);        

        cardAnimate.Play("p_c1");
        yield return new WaitForSeconds(1f);
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        points_score[2].SetActive(true);
        GameCompareSpriteCards(2, 3, 0, CardsForPlayer, playerCardsObject, CardsCountValues);
        playertotalScore += Pintock[2];
        textplayerScoreShow[2].text = playertotalScore.ToString();
        //print("------- Score: " + Pintock[2]);         
        
        cardAnimate.Play("p3_c1");
        yield return new WaitForSeconds(1f);
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        points_score[3].SetActive(true);
        GameCompareSpriteCards(3, 4, 12, CardsForPlayer, playerCardsObject, CardsCountValues);
        playertotalScore3 += Pintock[3];
        textplayerScoreShow[3].text = playertotalScore3.ToString();
        //print("------- Score: " + Pintock[3]);        
        
        cardAnimate.Play("p4_c1");
        yield return new WaitForSeconds(1f);
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        points_score[4].SetActive(true);
        GameCompareSpriteCards(4, 5, 16, CardsForPlayer, playerCardsObject, CardsCountValues);
        playertotalScore4 += Pintock[4];
        textplayerScoreShow[4].text = playertotalScore4.ToString();
       // print("------- Score: " + Pintock[4]);

        //----------------------------------------------------
       // yield return new WaitForSeconds(1f);
        cardAnimate.Play("d_c1");
        yield return new WaitForSeconds(1f);
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        points_score[5].SetActive(true);
        GameCompareSpriteCards(5, 6, 0, CardForMachinePlay, machineCardsObject, CardsCountValues);
        machintotalScore += Pintock[5];
        textmachineScoreShow.text = machintotalScore.ToString();

        //----------------------------------------------------
        cardAnimate.Play("p1_c2");
        yield return new WaitForSeconds(1f);
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        playertotalScore1 = check_ace_cards(6, 7, 9, CardsForPlayer, playerCardsObject, CardsCountValues, playertotalScore1);
        textplayerScoreShow[0].text = playertotalScore1.ToString();
        if (playertotalScore1.Equals(21)) p1_win[0].SetActive(true);

        cardAnimate.Play("p2_c2");
        yield return new WaitForSeconds(1f);
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");;
        playertotalScore2 = check_ace_cards(7, 8, 5, CardsForPlayer, playerCardsObject, CardsCountValues, playertotalScore2);
        textplayerScoreShow[1].text = playertotalScore2.ToString();
        if(playertotalScore2.Equals(21)) p2_win[0].SetActive(true);

        cardAnimate.Play("p_c2");
        yield return new WaitForSeconds(1f);
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        playertotalScore = check_ace_cards(8, 9, 1, CardsForPlayer, playerCardsObject, CardsCountValues, playertotalScore);
        textplayerScoreShow[2].text = playertotalScore.ToString();
        if (playertotalScore.Equals(21)) p_win[0].SetActive(true);

        cardAnimate.Play("p3_c2");
        yield return new WaitForSeconds(1f);
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        playertotalScore3 = check_ace_cards(9, 10, 13, CardsForPlayer, playerCardsObject, CardsCountValues, playertotalScore3);
        textplayerScoreShow[3].text = playertotalScore3.ToString();
        if (playertotalScore3.Equals(21)) p3_win[0].SetActive(true);

        cardAnimate.Play("p4_c2");
        yield return new WaitForSeconds(1f);
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        playertotalScore4 = check_ace_cards(10, 11, 17, CardsForPlayer, playerCardsObject, CardsCountValues, playertotalScore4);
        textplayerScoreShow[4].text = playertotalScore4.ToString();
        if (playertotalScore4.Equals(21)) p4_win[0].SetActive(true);


        yield return new WaitForSeconds(1f);
        cardAnimate.Play("d_c2");
        yield return new WaitForSeconds(1f);
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        machineCardsObject[1].SetActive(true);
        CardForMachinePlay[1].sprite = backCard;

        yield return new WaitForSeconds(0.5f);
        check_other_player_drawing_cards();

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(getButtonHitAction());
    }
    // check ace_cards
    int check_ace_cards(int s, int e, int i, Image[] m, GameObject[] g, List<int> l, int player_scores)
    {
        int check_ace = GameCompareSpriteCards(s, e, i, m, g, l);
        if (player_scores < 11 && check_ace == 0) player_scores += Pintock[s];
        else if (player_scores > 10 && check_ace == 0) player_scores += 1;
        else player_scores += Pintock[s];
        return player_scores;
    }

    public bool p1, p2, p, p3, p4;
    void check_other_player_drawing_cards()
    {
        if (playertotalScore1 < 14)
        {
            StartCoroutine(playe1_card1());
            IEnumerator playe1_card1()
            {
                yield return new WaitForSecondsRealtime(1f);
                cardAnimate.Play("p1_c3");
                yield return new WaitForSeconds(1f);
                cardAnimate.Play("player1");
                FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
                GameCompareSpriteCards(11, 12, 10, CardsForPlayer, playerCardsObject, CardsCountValues);
                playertotalScore1 += Pintock[11];
                textplayerScoreShow[0].text = playertotalScore1.ToString();
                print("------- Score: " + Pintock[11]);

                if (playertotalScore1 < 14)
                {
                    yield return new WaitForSecondsRealtime(1f);
                    cardAnimate.Play("p1_c4");
                    yield return new WaitForSeconds(1f);
                    cardAnimate.Play("player1");
                    FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
                    GameCompareSpriteCards(12, 13, 11, CardsForPlayer, playerCardsObject, CardsCountValues);
                    playertotalScore1 += Pintock[12];
                    textplayerScoreShow[0].text = playertotalScore1.ToString();
                    print("------- Score: " + Pintock[12]);
                    yield return new WaitForSecondsRealtime(0.5f);
                    p2 = true;
                    if (playertotalScore1 > 21) p1_win[3].SetActive(true);
                }
                else if (playertotalScore1 > 21) p1_win[3].SetActive(true);

               // p2 = true;
            }
            p2 = true;
        }
        else p2 = true;
        print("p2: " + p2);


        //--------------------------------------
        if (p2)
        {
            if (playertotalScore2 < 14)
            {
                StartCoroutine(playe1_card1());
                IEnumerator playe1_card1()
                {
                    yield return new WaitForSecondsRealtime(3f);
                    cardAnimate.Play("p2_c3");
                    yield return new WaitForSeconds(1f);
                    cardAnimate.Play("player1");
                    FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
                    GameCompareSpriteCards(13, 14, 6, CardsForPlayer, playerCardsObject, CardsCountValues);
                    playertotalScore2 += Pintock[13];
                    textplayerScoreShow[1].text = playertotalScore2.ToString();
                    print("------- Score: " + Pintock[13]);

                    if (playertotalScore2 < 14)
                    {
                        yield return new WaitForSecondsRealtime(1f);
                        cardAnimate.Play("p2_c4");
                        yield return new WaitForSeconds(1f);
                        cardAnimate.Play("player1");
                        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
                        GameCompareSpriteCards(14, 15, 7, CardsForPlayer, playerCardsObject, CardsCountValues);
                        playertotalScore2 += Pintock[14];
                        textplayerScoreShow[1].text = playertotalScore2.ToString();
                        print("------- Score: " + Pintock[14]);
                        yield return new WaitForSecondsRealtime(0.5f);
                        p = true;
                         if (playertotalScore2 > 21) p2_win[3].SetActive(true);
                    }
                    else if (playertotalScore2 > 21) p2_win[3].SetActive(true);
                    yield return new WaitForSeconds(1f);
                   // p = true;
                }
                p = true;
            }
            else p = true;

        }
        if (p)
        {
            StartCoroutine(wait_button());
            IEnumerator wait_button()
            {
                yield return new WaitForSeconds(3.3f);
                hitbtnClick.enabled = true;
                hitbtnClick.image.color = new Color(255, 255, 255, 255);
                machinebtnClick.enabled = false;
                machinebtnClick.image.color = Color.gray;
                standbtn.enabled = true;
                standbtn.image.color = new Color(255, 255, 255, 255);
                //coin_panel.SetActive(true);
            }

        }
        //--------------------------------------

    }
    void check_other_player_drawing2_cards()
    {
        if (p3)
        {
            if (playertotalScore3 < 14)
            {
                StartCoroutine(playe1_card1());
                IEnumerator playe1_card1()
                {
                    yield return new WaitForSecondsRealtime(1f);
                    cardAnimate.Play("p3_c3");
                    yield return new WaitForSeconds(1f);
                    cardAnimate.Play("player1");
                    FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
                    GameCompareSpriteCards(17, 18, 14, CardsForPlayer, playerCardsObject, CardsCountValues);
                    playertotalScore3 += Pintock[17];
                    textplayerScoreShow[3].text = playertotalScore3.ToString();
                    print("------- Score: " + Pintock[17]);

                    if (playertotalScore3 < 14)
                    {
                        yield return new WaitForSecondsRealtime(1f);
                        cardAnimate.Play("p3_c4");
                        yield return new WaitForSeconds(1f);
                        cardAnimate.Play("player1");
                        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
                        GameCompareSpriteCards(18, 19, 15, CardsForPlayer, playerCardsObject, CardsCountValues);
                        playertotalScore3 += Pintock[18];
                        textplayerScoreShow[3].text = playertotalScore3.ToString();
                        print("------- Score: " + Pintock[18]);
                        yield return new WaitForSeconds(0.5f);
                        p4 = true;
                        if (playertotalScore3 > 21) p3_win[3].SetActive(true);
                    }
                    else if (playertotalScore3 > 21) p3_win[3].SetActive(true);

                    yield return new WaitForSecondsRealtime(0.5f);
                        p4 = true;
                }
                p4 = true;
            }
            else p4 = true;
        }
        //--------------------------------------
        if (p4)
        {
            if (playertotalScore4 < 14)
            {
                StartCoroutine(playe1_card1());
                IEnumerator playe1_card1()
                {
                    yield return new WaitForSecondsRealtime(2f);
                    cardAnimate.Play("p4_c3");
                    yield return new WaitForSeconds(1f);
                    cardAnimate.Play("player1");
                    FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
                    GameCompareSpriteCards(19, 20, 18, CardsForPlayer, playerCardsObject, CardsCountValues);
                    playertotalScore4 += Pintock[19];
                    textplayerScoreShow[4].text = playertotalScore4.ToString();
                    print("------- Score: " + Pintock[19]);

                    if (playertotalScore4 < 14)
                    {
                       // yield return new WaitForSecondsRealtime(1f);
                        cardAnimate.Play("p4_c4");
                        yield return new WaitForSeconds(1f);
                        cardAnimate.Play("player1");
                        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
                        GameCompareSpriteCards(20, 21, 19, CardsForPlayer, playerCardsObject, CardsCountValues);
                        playertotalScore4 += Pintock[20];
                        textplayerScoreShow[4].text = playertotalScore4.ToString();
                        print("------- Score: " + Pintock[20]);
                        yield return new WaitForSecondsRealtime(0.5f);
                         if (playertotalScore4 > 21) p4_win[3].SetActive(true);
                        Invoke("UsedMachineAutoAction", 1f);
                    }
                    else if (playertotalScore4 > 21) p4_win[3].SetActive(true);
                    yield return new WaitForSecondsRealtime(0.5f);
                    Invoke("UsedMachineAutoAction", 1f);
                }  
            }
            else Invoke("UsedMachineAutoAction", 5f);
        }

    }
    
    
    // TODO: Wait for clear
    IEnumerator getButtonHitAction()
    {
        yield return new WaitForSeconds(3f);

        if (IsHitBtnOn)
        {
            IsHitBtnOn = false;
            hitbtnClick.enabled = true;
            hitbtnClick.image.color = new Color(255, 255, 255, 255);
            if (IsStandBtnOn)
            {
                hitbtnClick.enabled = false;
                hitbtnClick.image.color = Color.gray;
                standbtn.enabled = true;
                standbtn.image.color = new Color(255, 255, 255, 255);
                IsStandBtnOn = false;
            }
            hitbtnClick.enabled = false;
            hitbtnClick.image.color = Color.gray;
        }

    }

    // TODO: Wait this
    IEnumerator PlayerCard4Action()
    {
        print("Player card value: " + playertotalScore);
        yield return new WaitForSeconds(0.3f);
        hitObject.SetActive(false);
        callcardAgainOject.SetActive(true);
        if (IsCallAgainOn)
        {
            IsCallAgainOn = false;
            callcardAgainOject.SetActive(false);
        }
    }

    // TODO: Controll hit again button
    public void CallAgainButtonAction()
    {
        FindAnyObjectByType<AudioManage>().StartPlaySFX("button");
        IsCallAgainOn = true;
        IEnumerator waits()
        {
            callMuiltCards.enabled = false;
            yield return new WaitForSeconds(0.5f);
            cardAnimate.Play("p_c4");
            yield return new WaitForSeconds(1f);
            cardAnimate.Play("player1");
            FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
            int playerAceThirdCard = GameCompareSpriteCards(16, 17, 3, CardsForPlayer, playerCardsObject, CardsCountValues);
            if (playertotalScore < 11 && playerAceThirdCard == 0)
            {
                playertotalScore += Pintock[16];
                print("Player Ace Third Card : " + playerAceThirdCard);
            }
            else if (playertotalScore > 10 && playerAceThirdCard == 0) playertotalScore += 1;
            else
            {
                playertotalScore += Pintock[16];
            }
            print("------playerCardsScore: ----" + playertotalScore);
            textplayerScoreShow[2].text = playertotalScore.ToString();
            print("------- Score: " + Pintock[16]);

            if (playertotalScore > 21) p_win[3].SetActive(true);
            yield return new WaitForSeconds(1f);
            callcardAgainOject.SetActive(false);
            hitObject.SetActive(true);
        }
        StartCoroutine(waits());


    }

    // TODO: Controll hit button
    IEnumerator PlayerControllCardsAction()
    {
        cardAnimate.Play("p_c3");
        yield return new WaitForSeconds(1f);
 
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        int playerAceThirdCard = GameCompareSpriteCards(15, 16, 2, CardsForPlayer, playerCardsObject, CardsCountValues);
        if (playertotalScore < 11 && playerAceThirdCard == 0)
        {
            playertotalScore += Pintock[15];
            print("Player Ace Third Card : " + playerAceThirdCard);
        }else if(playertotalScore > 10 && playerAceThirdCard == 0) playertotalScore += 1;
        else
        {
            playertotalScore += Pintock[15];
        }
        textplayerScoreShow[2].text = playertotalScore.ToString();
        print("------- Score: " + Pintock[15]);
        print("Player Score: " + playertotalScore);
        if (playertotalScore <= 20)
        {
            StartCoroutine(PlayerCard4Action());

        }
         else if (playertotalScore > 21) p_win[3].SetActive(true);
    }

    // TODO: Controll hit button
    public void HitButtonAction()
    {
        FindAnyObjectByType<AudioManage>().StartPlaySFX("button");
        IsHitBtnOn = true;
        hitbtnClick.enabled = false;
        hitbtnClick.image.color = Color.gray;
        StartCoroutine(PlayerControllCardsAction());
    }

    // TODO: Controll stand button
    public void UsedStandButtonAction()
    {
        FindAnyObjectByType<AudioManage>().StartPlaySFX("button");
        hitObject.SetActive(true);
        callcardAgainOject.SetActive(false);
        IsStandBtnOn = true;
        hitbtnClick.enabled = false;
        hitbtnClick.image.color = Color.gray;
        machinebtnClick.enabled = true;
        machinebtnClick.image.color = new Color(255, 255, 255, 255);
        if (dealOnClick)
        {
            hitbtnClick.enabled = false;
            hitbtnClick.image.color = Color.gray;
            standbtn.enabled = false;
            standbtn.image.color = Color.gray;
            machinebtnClick.enabled = true;
            machinebtnClick.image.color = new Color(255, 255, 255, 255);

            dealOnClick = false;
        }
        coin_panel.SetActive(false);
        standbtn.enabled = false;
        standbtn.image.color = Color.gray;
        IsStandBtnOn = false;
        p3 = true;
        check_other_player_drawing2_cards();
        //Invoke("UsedMachineAutoAction", 1f);
    }

    // TODO: Controll deal button
    public void UsedMachineAutoAction()
    {
        FindAnyObjectByType<AudioManage>().StartPlaySFX("button");
        dealOnClick = true;
        machinebtnClick.enabled = false;
        machinebtnClick.image.color = Color.gray;
        callMuiltCards.enabled = true;
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        //GameCompareSpriteCards(21, 22, 1, CardForMachinePlay, machineCardsObject, CardsCountValues);
        machintotalScore = check_ace_cards(21, 22, 1, CardForMachinePlay, machineCardsObject, CardsCountValues, machintotalScore);
       // machintotalScore += Pintock[21];
        textmachineScoreShow.text = machintotalScore.ToString();
        if (machintotalScore.Equals(21)) machinewinner.SetActive(true);
        //print("------- Score: " + Pintock[21]);
        if (machintotalScore < 17) //machintotalScore < playertotalScore && machintotalScore < 20 && playertotalScore < 21
        {
            cardAnimate.Play("d_c3");
            Invoke("getMachineCard3Action", 1f);
        }
        Invoke("GameControllerCalculater", 2.5f);
        Invoke("GameStartUpdateAlls", 5f);
    }

    // TODO: Wait dealer card3 
    void getMachineCard3Action()
    {
        cardAnimate.Play("d_c3");
        cardAnimate.Play("player1");
        int dealerAceThirdCard = GameCompareSpriteCards(22, 23, 2, CardForMachinePlay, machineCardsObject, CardsCountValues);
        if (machintotalScore < 11 && dealerAceThirdCard == 0) //machintotalScore < 11 && dealerAceThirdCard == 0
        {
            machintotalScore += Pintock[22];
            print("Dealer Ace Third Card : " + dealerAceThirdCard);
        }else if(machintotalScore > 10 && dealerAceThirdCard == 0) machintotalScore += 1;
        else
        {
            machintotalScore += Pintock[22];
        }
        textmachineScoreShow.text = machintotalScore.ToString();
        print("deal score in card3: " + machintotalScore);
        //
        if (machintotalScore < 17) // machintotalScore < 20
        {
            cardAnimate.Play("d_c4");
            //Invoke("waitDealerCard4", 0.5f);
            Invoke("getMachineCard4Action", 1.5f);
        }
    }

    // TODO: Wait dealer card4 
    void getMachineCard4Action()
    {
        cardAnimate.Play("player1");
        FindAnyObjectByType<AudioManage>().StartPlaySFX("card");
        int dealerAceThirdCard = GameCompareSpriteCards(23, 24, 3, CardForMachinePlay, machineCardsObject, CardsCountValues);
        if (machintotalScore < 11 && dealerAceThirdCard == 0)
        {
            machintotalScore += Pintock[23];
            print("Dealer Ace Third Card : " + dealerAceThirdCard);
        }
        else if (machintotalScore > 10 && dealerAceThirdCard == 0) machintotalScore += 1;
        else
        {
            machintotalScore += Pintock[23];
        }
        textmachineScoreShow.text = machintotalScore.ToString();
        print("deal score in card4: " + machintotalScore);
    }

    // TODO: Clear coin items
    void MasterCleanItems()
    {
        //for (int i = 0; i < coinNormal.Length; i++)
        //{
        //    coinNormal[i].enabled = true;
        //    coinNormal[i].image.color = new Color(255, 255, 255, 255);
        //    coinModel[i].SetActive(false);
        //}
        // TODO: Clear all items in Coins class
        for (int i = 0; i < (FindAnyObjectByType<Coins>().coinsActive.Length); i++)
        {
            FindAnyObjectByType<Coins>().coinsActive[i].SetActive(false);
        }
        FindAnyObjectByType<Coins>().coinSuffer = 0;
        FindAnyObjectByType<Coins>().CoinS = 0;
        FindAnyObjectByType<Coins>().txtCoinActive.SetActive(false);
        FindAnyObjectByType<Coins>().i = 0;

        TimeON = true;
        Clock = 1f;
    }

    // TODO: Wait place your bets
    void getBetsCoins()
    {
        if (action)
        {
            StartCoroutine(GameProccesContinue());
            action = false;
        }
    }

    // TODO: Undo coin button
    public void ControllOnCoinsBackUp()
    {
        //MakeCoinAction(true, false, 1);
        FindAnyObjectByType<AudioManage>().StartPlaySFX("button");
        if (FindAnyObjectByType<Coins>().coinStorage.Count > 0)
        {
            int i = FindAnyObjectByType<Coins>().coinStorage.Count;
            i -= coinBack;
            print("Coin: " + i);
            isCoins = FindAnyObjectByType<Coins>().coinStorage[i];
            print("coin storage " + isCoins);
            FindAnyObjectByType<Coins>().coinsActive[i].SetActive(false);
            FindAnyObjectByType<Coins>().CoinS -= isCoins;
            bets -= isCoins;
            coinDisplay -= isCoins;
            money += isCoins;
            print("balance -: " + bets);
            print("coinsS -: " + coinDisplay);
            print("COINS: " + isCoins);
            // FindAnyObjectByType<Coins>().coinStorage.RemoveAt(coins);
            FindAnyObjectByType<Coins>().coinStorage.RemoveAt(i);
            print("Remove coins: " + FindAnyObjectByType<Coins>().coinStorage.Count);
            FindAnyObjectByType<Coins>().i = FindAnyObjectByType<Coins>().coinStorage.Count;
            FindAnyObjectByType<Coins>().txtCoins.text = coinDisplay.ToString() + " k";
            //FindAnyObjectByType<Coins>().CoinS.ToString() + " k";
            MoneyStorage.text = money.ToString() + " k";

        }
        else if (coinDisplay == 0)
        {
            FindAnyObjectByType<Coins>().CoinS = FindAnyObjectByType<Coins>().coinSuffer = 0;
            FindAnyObjectByType<Coins>().txtCoinActive.SetActive(false);
            bets = 0;
            coinDisplay = 0;
            print("balance 0: " + bets);
            print("coinsS 0: " + coinDisplay);
            FindAnyObjectByType<Coins>().coinsActive[0].SetActive(false);
            FindAnyObjectByType<Coins>().txtCoinActive.SetActive(false);
            FindAnyObjectByType<Coins>().coinSuffer = 0;
            undoCoins.enabled = false;
            undoCoins.image.color = Color.gray;
            callGameStartBtn.enabled = false;
            callGameStartBtn.image.color = Color.gray;
        }
    }

    // TODO: Call dealer button
    public void ButtonStartGotoPlayGame()
    {
        if (coinDisplay > 0)
        {
            coin_panel.SetActive(false);
            FindAnyObjectByType<AudioManage>().StartPlaySFX("button");
            action = true;
            callGameStartBtn.enabled = false;
            callStartGameEnable.SetActive(false);
            undoCoins.enabled = false;
            undoBtnEnable.SetActive(false);

            FindAnyObjectByType<Coins>().coinStorage.Clear();
            //MakeCoinAction(false, false, 0);
            CardsCountValues.Clear();
            playertotalScore = 0;
            textplayerScoreShow[2].text = playertotalScore.ToString();
            machintotalScore = 0;
            textmachineScoreShow.text = machintotalScore.ToString();

            CardsCountValues.Clear();
            getBetsCoins();
        }
    }

    // TODO: Clear button
    void GameStartUpdateAlls()
    {
        cardAnimate.Play("idle");
        playertotalScore = playertotalScore1 = playertotalScore2 = playertotalScore3 = playertotalScore4 = 0;
        machintotalScore = 0;
        bets = coinValueStore = FindAnyObjectByType<Coins>().CoinS = coinDisplay = 0;
        textmachineScoreShow.text = machintotalScore.ToString();
        textplayerScoreShow[2].text = playertotalScore.ToString();
        for(int i = 0; i<playerCardsObject.Length; i++)  playerCardsObject[i].SetActive(false);
        for(int i = 0; i<points_score.Length; i++) points_score[i].SetActive(false);
        for(int i = 0; i<machineCardsObject.Length; i++) machineCardsObject[i].SetActive(false);
        hitbtnClick.enabled = false;
        hitbtnClick.image.color = Color.gray;
        standbtn.enabled = false;
        standbtn.image.color = Color.gray;
        machinebtnClick.enabled = false;
        machinebtnClick.image.color = Color.gray;
        IsHitBtnOn = dealOnClick = IsStandBtnOn = false;

        undoCoins.enabled = false;
        undoCoins.image.color = Color.gray;
        callGameStartBtn.enabled = false;
        callGameStartBtn.image.color = Color.gray;

        FindAnyObjectByType<PlayerCoin_Suffer>().clear_coins();
        coin_panel.SetActive(true);
        for(int i = 0; i<p1_win.Length; i++)
        {
            p1_win[i].SetActive(false);
            p2_win[i].SetActive(false);
            p3_win[i].SetActive(false);
            p4_win[i].SetActive(false);
            p_win[i].SetActive(false);
        }
        MasterCleanItems();
    }

    // TODO: Coins choise button
    public void switchCoinsItems()
    {
        string coinString = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        switch (coinString)
        {
            case "1k":
                FindAnyObjectByType<AudioManage>().StartPlaySFX("coin");
                coinValueStore = 1;
                break;
            case "5k":
                FindAnyObjectByType<AudioManage>().StartPlaySFX("coin");
                coinValueStore = 5;
                break;
            case "10k":
                FindAnyObjectByType<AudioManage>().StartPlaySFX("coin");
                coinValueStore = 10;
                break;
            case "20k":
                FindAnyObjectByType<AudioManage>().StartPlaySFX("coin");
                coinValueStore = 20;
                break;
            case "50k":
                FindAnyObjectByType<AudioManage>().StartPlaySFX("coin");
                coinValueStore = 50;
                break;
            case "100k":
                FindAnyObjectByType<AudioManage>().StartPlaySFX("coin");
                coinValueStore = 100;
                break;
        }
        if (bets > 0 && coinDisplay > 0 || money >= coinValueStore)
        {
            print("balance: " + bets);
            print("coinsS: " + coinDisplay);
            callStartGameEnable.SetActive(true);
            undoBtnEnable.SetActive(true);
            callGameStartBtn.enabled = true;
            callGameStartBtn.image.color = new Color(255, 255, 255, 255);
            undoCoins.enabled = true;
            undoCoins.image.color = new Color(255, 255, 255, 255);
        }
        FindAnyObjectByType<Coins>().CoinS = coinDisplay;
    }

    // TODO: Calculate Winner, Lose and Push Score
    private void GameControllerCalculater()
    {
        if (machintotalScore > playertotalScore && machintotalScore <= 21)
        {
            p_win[1].SetActive(true);
        }
        else if (playertotalScore > 21) p_win[2].SetActive(true);
        if ((machintotalScore < playertotalScore && playertotalScore <= 21) || (playertotalScore < machintotalScore && machintotalScore > 21))
        {
            StartCoroutine(playerYouAreWinner());
            money += (bets * 1.5);
            tra += (bets * 1.5);
            tra_txt.text = tra.ToString() + " k";
            print("balance: " + (bets * 1.5));
        }
        else if ((((machintotalScore > playertotalScore ) && (machintotalScore > playertotalScore1) && (machintotalScore > playertotalScore2)&&
                (machintotalScore > playertotalScore3) && (machintotalScore > playertotalScore4)) && machintotalScore <= 21 ))
        {
            StartCoroutine(MachineHisWinner());
        }
        else if (playertotalScore == machintotalScore)
        {
            StartCoroutine(PushWithPlayer());
            p_win[2].SetActive(true);
            money += bets;
            print("balance: " + bets);
        }

        bets = 0;
        FindAnyObjectByType<Coins>().txtCoins.text = bets.ToString() + " k";
        print("last balance: " + bets);
        //p1
        if((machintotalScore < playertotalScore1 && playertotalScore1 <= 21) || (playertotalScore1 < machintotalScore && machintotalScore > 21))
        {
            p1_win[0].SetActive(true);
        }
        else if (playertotalScore1 == machintotalScore) p1_win[2].SetActive(true);
        else if (machintotalScore > playertotalScore1 && machintotalScore <=21) p1_win[1].SetActive(true);
        //p2
        if ((machintotalScore < playertotalScore2 && playertotalScore2 <= 21) || (playertotalScore2 < machintotalScore && machintotalScore > 21))
        {
            p2_win[0].SetActive(true);
        }
        else if (playertotalScore2 == machintotalScore) p2_win[2].SetActive(true);
        else if (machintotalScore > playertotalScore2 && machintotalScore <= 21) p2_win[1].SetActive(true);
        //p3
        if ((machintotalScore < playertotalScore3 && playertotalScore3 <= 21) || (playertotalScore3 < machintotalScore && machintotalScore > 21))
        {
            p3_win[0].SetActive(true);
        }
        else if (playertotalScore3 == machintotalScore) p3_win[2].SetActive(true);
        else if (machintotalScore > playertotalScore3 && machintotalScore <= 21) p3_win[1].SetActive(true);
        // p4
        if ((machintotalScore < playertotalScore4 && playertotalScore4 <= 21) || (playertotalScore4 < machintotalScore && machintotalScore > 21))
        {
            p4_win[0].SetActive(true);
        }
        else if (playertotalScore4 == machintotalScore) p4_win[2].SetActive(true);
        else if (machintotalScore > playertotalScore4 && machintotalScore <= 21) p4_win[1].SetActive(true);
    }

    // win alert
    IEnumerator playerYouAreWinner()
    {
        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<AudioManage>().StartPlaySFX("win");
        playerwinner.SetActive(true);
        playerWinAnimate.Play("winner");
        yield return new WaitForSeconds(2f);
        playerwinner.SetActive(false);
    }
    IEnumerator MachineHisWinner()
    {
        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<AudioManage>().StartPlaySFX("lose");
        machinewinner.SetActive(true);
        playerLoseAnimate.Play("lose");
        yield return new WaitForSeconds(2f);
        machinewinner.SetActive(false);
    }
    IEnumerator PushWithPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<AudioManage>().StartPlaySFX("push");
        equalPlayer.SetActive(true);
        equalAnimate.Play("push");
        yield return new WaitForSeconds(2f);
        equalPlayer.SetActive(false);

    }

    // Home scene
    public void GotoHomeScene()
    {
        SceneManager.LoadScene("Home Scene");
    }
}
