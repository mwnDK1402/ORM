namespace Project.Login
{
    public readonly struct LoginSignal
    {
        public readonly User User;

        public LoginSignal(User user) => User = user;
    }
}