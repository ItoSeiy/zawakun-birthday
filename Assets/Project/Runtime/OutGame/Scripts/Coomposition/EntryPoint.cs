using Project.Runtime.OutGame.APIGateway;
using Project.Runtime.OutGame.Model;
using Project.Runtime.OutGame.Presentation;
using Project.Runtime.OutGame.UseCase;
using UnityEngine;

namespace Project.Runtime.OutGame.Composition
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private LetterContents _letterContents;

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            // APIGateways
            var fileAPIGateway = new FileApiGateway();

            // Models
            var userModel = new UserModel();

            // UseCases
            var loginUseCase = new LoginUseCase(userModel, fileAPIGateway);

            // Presenter Factories
            var splashPagePresenterFactory = new SplashPagePresenterFactory();
            var loginPagePresenterFactory = new LoginPagePresenterFactory(
                _letterContents.GetContentsParent(ContentsParentType.Login),
                loginUseCase);

            // Transition Services
            var transitionService = new TransitionService(
                splashPagePresenterFactory,
                loginPagePresenterFactory
            );

            // 最初のページに遷移
            transitionService.PushSplashPage();
        }
    }
}