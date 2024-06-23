using Cysharp.Threading.Tasks;
using Project.Runtime.OutGame.UseCase;
using Project.Runtime.OutGame.View;

namespace Project.Runtime.OutGame.Presentation
{
    /// <summary>
    /// ゲーム進行のPresenter
    /// </summary>
    public sealed class LoginPagePresenter : PagePresenterBase<LoginPage, LoginView, LoginViewState>
    {
        private readonly LoginUseCase _loginUseCase;
        
        public LoginPagePresenter(LoginPage view, ITransitionService transitionService, LoginUseCase loginUseCase) : base(view, transitionService)
        {
            _loginUseCase = loginUseCase;
        }

        protected override UniTask ViewDidSetup(LoginViewState state)
        {
            return UniTask.FromResult(state);
        }
    }
}
