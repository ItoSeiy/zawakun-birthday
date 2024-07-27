using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Runtime.OutGame.Model;

namespace Project.Runtime.OutGame.Presentation
{
    public partial interface ITransitionService
    {
        void PushSplashPage();
    
        void PushIntroPage();

        void PushLoginPage();

        void PushQuestionPage();

        void PopCommandExecuted();

        UniTask RegisterAllSheets(CancellationToken ct);
    }
}