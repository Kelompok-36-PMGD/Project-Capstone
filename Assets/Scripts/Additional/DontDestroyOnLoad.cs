using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public bool trigger = true;
    private void Awake()
    {
        if (trigger)
        {
            DontDestroyOnLoad(gameObject);
            trigger = false;
            this.GetComponent<DontDestroyOnLoad>().enabled = false;
        }
    }

    public void Trigger()
    {
        DontDestroyOnLoad(gameObject);
    }
}
