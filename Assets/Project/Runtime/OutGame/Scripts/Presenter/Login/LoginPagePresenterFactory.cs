using Project.Runtime.OutGame.Model;
using Project.Runtime.OutGame.UseCase;
using Project.Runtime.OutGame.View;

namespace Project.Runtime.OutGame.Presentation
{
    public sealed class LoginPagePresenterFactory
    {
        private readonly LetterContents.ContentsParent _letterContents;
        private readonly LoginUseCase _loginUseCase;

        public LoginPagePresenterFactory(LetterContents.ContentsParent letterContents, LoginUseCase loginUseCase)
        {
            _letterContents = letterContents;
            _loginUseCase = loginUseCase;
        }

        public LoginPagePresenter Create(LoginPage view, ITransitionService transitionService)
        {
            return new LoginPagePresenter(view, transitionService, _loginUseCase, _letterContents);
        }
    }
}