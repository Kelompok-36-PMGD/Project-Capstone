using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    [SerializeField] bool useScriptable;
    public Health lifeScriptable;
    public int life;
    public int maxLife;

    public UnityEvent OnHitEvent;
    public UnityEvent OnLifeReachZero;

    bool isDeath = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //For enemydamage, from the Enemy OBJECT get the EnemyController then call the TakeDamage(AttackType type) function instead.
    public void OnHit(int amount)
    {
        if (!isDeath)
        {
            if (useScriptable)
            {
                lifeScriptable.value = lifeScriptable.value - amount < 0 ? 0 : lifeScriptable.value - amount;
                OnHitEvent?.Invoke();
                if (lifeScriptable.value <= 0)
                {
                    OnLifeReachZero?.Invoke();
                    isDeath = true;
                }
            }
            else
            {
                life = life - amount < 0 ? 0 : life - amount;
                OnHitEvent?.Invoke();
                if (life <= 0)
                {
                    OnLifeReachZero?.Invoke();
                    isDeath = true;
                }

            }
        }
    }

    public void OnGain(int amount)
    {
        //GameManager.GetInstance().lifeMusic.Play();
        if (useScriptable)
        {
            lifeScriptable.value = lifeScriptable.value + amount > lifeScriptable.maxValue ? lifeScriptable.maxValue : lifeScriptable.value + amount;

        }
        else
        {
            life = life + amount > maxLife ? maxLife : life + amount;
            
        }
    }

    //player only scripts =================================
    public void IncreaseMax(int amount)
    {
        lifeScriptable.maxValue += amount;
    }

    public void DecreaseMax(int amount)
    {
        lifeScriptable.maxValue -= amount;
    }
    
}
