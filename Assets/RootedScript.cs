public class RootedScript : DebuffScript
{
    private CharacterStats _characterStats;

    private void Awake()
    {
        text = "Rooted";
        _characterStats = transform.root.GetComponent<CharacterStats>();
        _characterStats.movementSpeed = 0f;
        print(transform.root.name + " " + _characterStats.movementSpeed);
    }

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    private void OnDestroy()
    {
        _characterStats.movementSpeed = _characterStats.InitMovementSpeed;
    }
}
