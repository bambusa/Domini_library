namespace Domini
{
    public interface IGameLoop
    {
        void Start();

        void Update(float time, float deltaTime, long secondsPassed);
    }
}