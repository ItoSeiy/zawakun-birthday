using Cysharp.Threading.Tasks;
using Project.Development.OutGame;
using Project.Runtime.OutGame.APIGateway;
using Project.Runtime.OutGame.Model;
using Project.Runtime.OutGame.UseCase;
using Project.Runtime.OutGame.View;
using UnityEngine;

namespace Project.Development
{
    public class LoginPageDevelopment : AppViewDevelopment<LoginView, LoginViewState>
    {
        [SerializeField]
        private LetterContents _letterContents;

        private LoginUseCase _useCase;

        protected override bool UseLocalization { get; }

        protected override void InitializeView(LoginView view)
        {
            var model = new UserModel();
            var fileApiGateway = new FileApiGateway();
            _useCase = new LoginUseCase(model, fileApiGateway);
        }

        protected override async void ViewDidSetup(LoginViewState state)
        {
            var contentsParent = _letterContents.GetContentsParent(ContentsParentType.Login);
            var contents = contentsParent.GetContents(ContentsType.LoginEnterUseName);

            {
                await UniTask.WaitForSeconds(contents.WaitForSeconds);
                await _useCase.SaveEnterUserNameLetterAsync(contents.Text);
            }

            {
                contents = contentsParent.GetContents(ContentsType.LoginGreeting);
                await UniTask.WaitForSeconds(contents.WaitForSeconds);

                await _useCase.FetchUserModelAsync(contents.MatchPattern, contents.FallBackText);
            }
        }
    }
}