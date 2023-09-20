public class BulletPool : Pool<Bullet>
{
    private static BulletPool _instance;

    public static BulletPool Instance
    {
        get
        {
            if (!_instance) _instance = FindObjectOfType<BulletPool>();
            return _instance;
        }
    }
}