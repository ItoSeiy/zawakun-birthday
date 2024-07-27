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

            // UseCases
            var loginUseCase = new LoginUseCase(fileAPIGateway);
            var questionUseCase = new QuestionUseCase(fileAPIGateway);

            // Presenter Factories
            var splashPagePresenterFactory = new SplashPagePresenterFactory();

            var introPagePresenterFactory = new IntroPagePresenterFactory();

            var loginPagePresenterFactory = new LoginPagePresenterFactory(
                _letterContents.GetContentsParent(ContentsParentType.Login),
                loginUseCase);

            var questionPagePresenterFactory = new QuestionPagePresenterFactory(
                _letterContents.GetContentsParent(ContentsParentType.Question),
                questionUseCase);

            // Transition Services
            var transitionService = new TransitionService(
                splashPagePresenterFactory,
                introPagePresenterFactory,
                loginPagePresenterFactory,
                questionPagePresenterFactory
            );

            // 最初のページに遷移
            transitionService.PushSplashPage();
        }
    }
}