using Project.Runtime.OutGame.Model;
using Project.Runtime.OutGame.UseCase;
using Project.Runtime.OutGame.View;

namespace Project.Runtime.OutGame.Presentation
{
    public sealed class QuestionPagePresenterFactory
    {
        private readonly LetterContents.ContentsParent _letterContents;
        private readonly QuestionUseCase _questionUseCase;

        public QuestionPagePresenterFactory(LetterContents.ContentsParent letterContents,
            QuestionUseCase questionUseCase)
        {
            _letterContents = letterContents;
            _questionUseCase = questionUseCase;
        }

        public QuestionPagePresenter Create(PostLetterPage view, ITransitionService transitionService)
        {
            return new QuestionPagePresenter(view, transitionService, _questionUseCase, _letterContents);
        }
    }
}