namespace StarDebrisApocalypse
{
    public interface IRenderer
    {
        void EnqueueForRendering(GameObject obj);

        void RenderAll();

        void ClearQueue();
    }
}