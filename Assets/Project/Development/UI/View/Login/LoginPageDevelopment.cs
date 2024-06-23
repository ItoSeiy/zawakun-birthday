using Cysharp.Threading.Tasks;
using Project.Development.OutGame;
using Project.Runtime.OutGame.APIGateway;
using Project.Runtime.OutGame.Model;
using Project.Runtime.OutGame.UseCase;
using Project.Runtime.OutGame.View;
using SFB;

namespace Project.Development
{
    public class LoginPageDevelopment : AppViewDevelopment<LoginView, LoginViewState>
    {
        private LoginUseCase _useCase;

        protected override bool UseLocalization { get; }

        protected override void InitializeView(LoginView view)
        {
            var model = new UserModel();
            var fileApiGateway = new FileApiGateway();
            _useCase = new LoginUseCase(model, fileApiGateway);
        }

        protected override void ViewDidSetup(LoginViewState state)
        {
        }

        private async UniTask TrySetName()
        {
            await _useCase.FetchUserModel();
        }
    }
}
