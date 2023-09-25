using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoin_Suffer : MonoBehaviour
{
    public Sprite[] sprites_coin;
    public GameObject[] coins0;
    public Image[] coins_im0;    
    public GameObject[] coins1;
    public Image[] coins_im1;   
    public GameObject[] coins2;
    public Image[] coins_im2;   
    public GameObject[] coins3;
    public Image[] coins_im3;
    public Text[] texts;
    public void coins_suffer(GameObject[] coins, Image[] coins_im, int bets, Text[] txts, int loc)
    {
        int p0 = 0, p1 = 0, p2 = 0, p3 = 0, p4 = 0, p5 = 0 ;
        
        for (int i = 0; i < 7; i++)
        {
            int coins_rand = Random.Range(0, 6);
            switch (coins_rand)
            {
                case 0:
                    p0++;
                    coins[0].SetActive(true);
                    coins_im[0].sprite = sprites_coin[0];
                    if (p0 == 1) { bets += 1000; }
                    break;
                case 1:
                    p5++;
                    coins[1].SetActive(true);
                    coins_im[1].sprite = sprites_coin[1];
                    if (p5 == 1) { bets += 5000; }
                    break;
                case 2:
                    p1++;
                    coins[2].SetActive(true);
                    coins_im[2].sprite = sprites_coin[2];
                    if (p1 == 1) { bets += 10000; }
                    break;
                case 3:
                    p2++;
                    coins[3].SetActive(true);
                    coins_im[3].sprite = sprites_coin[3];
                    coins[6].SetActive(true);
                    coins_im[6].sprite = sprites_coin[3];
                    if (p2 == 1) { bets += 2 * 25000; }
                    break;
                case 4:
                    p3++;
                    coins[4].SetActive(true);
                    coins_im[4].sprite = sprites_coin[4];
                    if (p3 == 1) { bets += 50000; }
                    break;
                case 5:
                    p4++;
                    coins[5].SetActive(true);
                    coins_im[5].sprite = sprites_coin[5];
                    if (p4 == 1) { bets += 100000; }
                    break;
            }
            // print("bets: " + bets);
        }
        convertNumber(bets, txts[loc]);
    }
  
    private void player_1()
    {
        int p = 0, bets = 0;
        coins_suffer(coins0, coins_im0, bets, texts, p);
    }    
    private void player_2()
    {
        int p = 1, bets = 0;
        coins_suffer(coins1, coins_im1, bets, texts, p);
    }    
    private void player_3()
    {
        int p = 2, bets = 0;
        coins_suffer(coins2, coins_im2, bets, texts, p);
    }    
    private void player_4()
    {
        int p = 3, bets = 0;
        coins_suffer(coins3, coins_im3, bets, texts, p);
    }
    
    public IEnumerator catch_coin()
    {
        yield return new WaitForSecondsRealtime(0.8f);
        player_1();
        player_3();
        yield return new WaitForSecondsRealtime(0.2f);
        player_4();
        yield return new WaitForSecondsRealtime(0.3f);
        player_2();
    }

    public void clear_coins()
    {
        for(int i = 0; i<coins0.Length; i++)
        {
            coins0[i].SetActive(false);
            coins1[i].SetActive(false);
            coins2[i].SetActive(false);
            coins3[i].SetActive(false);
        }
    }

    public void convertNumber(double coinsConvert, Text text)
    {
        var convert = string.Format("{0:#,#.##}", coinsConvert);
        text.text = convert.ToString();
    }
}
