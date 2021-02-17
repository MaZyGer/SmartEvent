# SmartEvent

It allows you to send messages without link two objects.

```CSharp
public class DamageEvent 
{
    public string Attacker;
    public int Amount;

    public DamageEvent(int amount)
    {
        Amount = amount;
    }
}

SmartEvent.Broadcast(new DamageEvent(30) { Attacker = "Soldier" });
```

```CSharp
public class Receiver : MonoBehaviour
{
    private void OnEnable()
    {
        SmartEvent.OnReceiveEvent<DamageEvent>(OnReceiveDamage);
    }

    private void OnDisable()
    {
        SmartEvent.OnReceiveEventRemove<DamageEvent>(OnReceiveDamage);
    }

    public void OnReceiveDamage(DamageEvent damage)
    {
        Debug.Log(damage.Attacker);
        Debug.Log(damage.Amount);
    }
}
```