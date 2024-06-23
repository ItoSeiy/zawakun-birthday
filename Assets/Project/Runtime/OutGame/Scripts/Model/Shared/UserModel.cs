namespace Project.Runtime.OutGame.Model
{
    public class UserModel
    {
        public string Name { get; private set; }
        
        public void SetValue(string name)
        {
            Name = name;
        }
    }
}