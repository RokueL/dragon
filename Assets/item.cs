using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public enum ItemType
    {
        coin,
        Gem1,
        Gem2,
        Gem3,
        item_Magnet,
        item_Rush,
        item_DoubleShot,
        item_DoubleScore
    }
    public ItemType itemType;

    GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch (itemType)
            {
                case ItemType.coin:
                    scoreManager.Instance.pickCoin(0);
                    break;
                case ItemType.Gem1:
                    scoreManager.Instance.pickCoin(1);
                    break;
                case ItemType.Gem2:
                    scoreManager.Instance.pickCoin(2);
                    break;
                case ItemType.Gem3:
                    scoreManager.Instance.pickCoin(3);
                    break;
                case ItemType.item_Magnet:
                    break;
                case ItemType.item_Rush:
                    GameManager.Instance.useRush();
                    break;
                case ItemType.item_DoubleShot:
                    break;
                case ItemType.item_DoubleScore:
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
