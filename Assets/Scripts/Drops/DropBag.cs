using System.Collections.Generic;
using UnityEngine;

public class DropBag : MonoBehaviour
{
   [SerializeField] private GameObject _DropPrefab;
   [SerializeField] private List<Drop> Drops = new List<Drop>();

    // Return a drop from a list of drops
    private Drop GetDrop()
    {
        int randomNumber = Random.Range(1, 81);
        List<Drop> randomDrops = new List<Drop>();

        // deciding with drop will be chosen based on it's drop chance
        foreach (Drop drop in Drops)
        {
            if (drop.DropChance - randomNumber >= 0)
            {
                randomDrops.Add(drop);
            }
        }
        // Return one of the different drops that is inside the list 
        if(randomDrops.Count > 0)
        {
            return randomDrops[Random.Range(0, randomDrops.Count)];
        }

        return null;
    }


    // Instantiate one of the drop that we get from our list of drops 
    public void InstantiateDrop(Transform instantiationPosition)
    {
        // we get a drop
        Drop droppedItem = GetDrop();

        if(droppedItem != null)
        {
            // we instantiate it 
            GameObject dropGameObject = Instantiate(_DropPrefab, instantiationPosition.position, Quaternion.identity);
            int expLevel;
            // we Initialize it's different components/variables
            dropGameObject.GetComponent<SpriteRenderer>().color = droppedItem.DropColor;
            dropGameObject.GetComponent<GeneralDrop>().DropName = droppedItem.DropName;
            // we change the different stats of the drops if it's an exp drop
            if (droppedItem.DropName == "Exp")
            {
                expLevel = GetComponent<Enemy>().EnemyLevel;
                dropGameObject.GetComponent<GeneralDrop>().ExpLevel = expLevel;
                dropGameObject.GetComponent<SpriteRenderer>().color = GameManager.Instance.ExpDropColors[expLevel-1];
            }
        }
    }
}
