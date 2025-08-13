using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractionsController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.WheatTypes.GOLD_WHEAT))
        {
            other.gameObject?.GetComponent<GoldWheatCollectible>().Collect();

        }
        if (other.CompareTag(Consts.WheatTypes.HOLY_WHEAT))
        {
            other.gameObject?.GetComponent<HolyWheatCollectible>().Collect();
        }
        if (other.CompareTag(Consts.WheatTypes.ROTTEN_WHEAT))
        {
            other.gameObject?.GetComponent<RottenWheatCollectible>().Collect();
        }

        if (other.CompareTag(Consts.WheatTypes.DONKEY_WHEAT))
        {
            other.gameObject?.GetComponent<DonkeyWheatCollectible>().Collect();
        }
    }
}
