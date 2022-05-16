using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    [SerializeField] bool useScriptable;
    public int life;
    public Health lifeScriptable;
    public int maxLife;
    public Health maxLifeScriptable;
    

    public UnityEvent OnLifeReachZero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit(int amount)
    {
        if (useScriptable)
        {
            lifeScriptable.value = lifeScriptable.value - amount < 0 ? 0 : lifeScriptable.value - amount;

            if(lifeScriptable.value <= 0)
            {
                OnLifeReachZero?.Invoke();
            }
        }
        else
        {
            life = life - amount < 0 ? 0 : life - amount;
            if(life <= 0)
            {
                OnLifeReachZero?.Invoke();
            }
        }
    }

    public void OnGain(int amount)
    {
        //GameManager.GetInstance().lifeMusic.Play();
        if (useScriptable)
        {
            lifeScriptable.value = lifeScriptable.value + amount > maxLifeScriptable.value ? maxLifeScriptable.value : lifeScriptable.value + amount;

        }
        else
        {
            life = life + amount > maxLife ? maxLife : life + amount;
            
        }
    }

    
}
