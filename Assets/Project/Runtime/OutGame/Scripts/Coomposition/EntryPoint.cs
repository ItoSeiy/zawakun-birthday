using Project.Runtime.OutGame.Presentation;
using UnityEngine;

namespace Project.Runtime.OutGame.Composition
{
    public sealed class EntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            // APIGateways

            // Models

            // UseCases

            // Presenter Factories
            var splashPagePresenterFactory = new SplashPagePresenterFactory();

            // Transition Services
            var transitionService = new TransitionService(
                splashPagePresenterFactory
            );

            // 最初のページに遷移
            transitionService.PushSplashPage();
        }
    }
}