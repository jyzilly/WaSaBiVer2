using UnityEngine;

public class WSBEyeItem : MonoBehaviour
{

    [SerializeField] private WSBCreature1 creature1;
    [SerializeField] private GameObject eyeItem;

    private float timeLimit = 3.0f;
    private float time = 0.0f;



    private void OnTriggerEnter(Collider other)
    {
        //�÷��̾����� �±� �����ؾ� ��
        if (other.tag == "Player")
        {
            time += Time.deltaTime;
            if (time >= timeLimit)
            {
                time = 0.0f;
                creature1.TrChanged();
            }
        }
    }


}
