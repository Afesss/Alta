using UnityEngine.SceneManagement;

namespace Architecture.Services
{
    public class SceneLoader
    {
        public void RestartScene()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}
