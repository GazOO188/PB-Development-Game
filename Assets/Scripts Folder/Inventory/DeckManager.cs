using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class DeckManager : MonoBehaviour
{
    [SerializeField] GameObject[] cards;
    [SerializeField] GameObject[] cardHolder;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Next Card");
        }

    }

    public void ChangePlaces()
    {
        cardHolder[0] = transform.GetChild(2).gameObject;
        cardHolder[1] = transform.GetChild(0).gameObject;
        cardHolder[2] = transform.GetChild(1).gameObject;

        for (int i = 0; i < cards.Length; i++)
            cards[i].transform.SetParent(null);

        for (int i = 0; i < cardHolder.Length; i++)
        {
            cards[i] = cardHolder[i];
            cards[i].transform.SetParent(this.transform);
        }

        Item[] items = GetComponentsInChildren<Item>();
        foreach (Item item in items)
        {
            if (item.transform.parent == transform.GetChild(2))
                item.inFront = true;
            else
                item.inFront = false;
        }
    }
}
