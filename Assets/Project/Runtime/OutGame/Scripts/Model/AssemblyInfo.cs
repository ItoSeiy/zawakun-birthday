#define DEBUG
// デバック用のアセンブリからModelのinternalメンバーにアクセスできるようにする

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Project.Development")]
[assembly: InternalsVisibleTo("Project.Runtime.OutGame.UseCase")]