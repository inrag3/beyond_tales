namespace _Project.Runtime.Core
{
    public enum Scene
    {
        Menu = 0, 
        Main = 1,
        MainCopyTestScreenplay = 2,
    }
    
    
    public class SceneManager : ISceneManager
    {
        public void LoadScene(Scene scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
        }
    }

    public interface ISceneManager
    {
        public void LoadScene(Scene scene);
    }
}