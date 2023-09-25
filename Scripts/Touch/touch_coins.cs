using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch_coins : MonoBehaviour
{
    public Transform[] local_touch;
    public GameObject[] coin_prefabs;
    public Transform[] coin_pos;
    string touch_coin, values;
    // Update is called once per frame
    void Update()
    {
       // touch_on();
    }
    public void touch_on()
    {
        string touch_btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        switch (touch_btn)
        {
            case "player":
                switch (values)
                {
                    case "1k":
                        if (Input.touchCount > 0)
                        {
                            print("touch");
                            Touch touch = Input.GetTouch(0);
                            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                            touchPosition.z = 0f;
                            local_touch[0].position = touchPosition;
                        }
                         
                         Vector3 con = Camera.main.ScreenToWorldPoint(coin_pos[0].position);
                         Instantiate(coin_prefabs[0], coin_pos[0].position, Quaternion.identity);
                         con.z = 0f;
                         coin_prefabs[0].transform.position = new Vector3(0f,0f, 0f);
                        //coin_prefabs[0].transform.position = con;

                        break;
                    case "5k":
                        if (Input.touchCount > 0)
                        {
                            print("touch");
                            Touch touch = Input.GetTouch(0);
                            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                            touchPosition.z = 0f;
                            local_touch[1].position = touchPosition;
                        }
                        break;
                    case "10k":
                        if (Input.touchCount > 0)
                        {
                            print("touch");
                            Touch touch = Input.GetTouch(0);
                            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                            touchPosition.z = 0f;
                            local_touch[2].position = touchPosition;
                        }
                        break;                    
                    case "20k":
                        if (Input.touchCount > 0)
                        {
                            print("touch");
                            Touch touch = Input.GetTouch(0);
                            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                            touchPosition.z = 0f;
                            local_touch[3].position = touchPosition;
                        }
                        break;
                    case "50k":
                        if (Input.touchCount > 0)
                        {
                            print("touch");
                            Touch touch = Input.GetTouch(0);
                            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                            touchPosition.z = 0f;
                            local_touch[4].position = touchPosition;
                        }
                        break;
                    case "100k":
                        if (Input.touchCount > 0)
                        {
                            print("touch");
                            Touch touch = Input.GetTouch(0);
                            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                            touchPosition.z = 0f;
                            local_touch[5].position = touchPosition;
                        }
                        break;
                }

                break;
        }
        
    }

    public void coins()
    {
        touch_coin = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        switch (touch_coin)
        {
            case "1k":
                values = "1k";
                break;
            case "5k":
                values = "5k";
                break;
            case "10k":
                values = "10k";
                break; 
            case "20k":
                values = "20k";
                break;
            case "50k":
                values = "50k";
                break;
            case "100k":
                values = "100k";
                break;
        }
    }
}
