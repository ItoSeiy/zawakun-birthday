using Project.Runtime.OutGame.UseCase;
using Project.Runtime.OutGame.View;

namespace Project.Runtime.OutGame.Presentation
{
    public sealed class LoginPagePresenterFactory
    {
        private readonly LoginUseCase _loginUseCase;

        public LoginPagePresenterFactory(LoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        public LoginPagePresenter Create(LoginPage view, ITransitionService transitionService)
        {
            return new LoginPagePresenter(view, transitionService, _loginUseCase);
        }
    }
}